using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using Microsoft.Win32;
using CommandAS.Tools;
using CommandAS.QueryLib;

namespace ProgTor.ParAd
{
  public partial class frmParad : Form
  {
    public const string REG_APP_PATH = @"SOFTWARE\ProgTor\ParAd";
    public const string FILE_NAME_HISTORY = "parad_history.txt";
    
    private Performer _qs;
    private FIAS _fias;
    private Experience _exp;
    private ParAd _pa;

    private void _err(string aTxt)
    {
      _log("Err:\t" + aTxt);
    }
    private void _log()
    {
      _log(null);
    }
    private void _log(string aTxt)
    {
      if (aTxt == null)
        _txtLogs.Text += Environment.NewLine;
      else
        _txtLogs.Text += Environment.NewLine + "[" + DateTime.Now + "] " + aTxt;

      _txtLogs.Focus();
      _txtLogs.Select(_txtLogs.Text.Length - 1, 0);
      _txtLogs.ScrollToCaret();
    }


    public frmParad()
    {
      InitializeComponent();

      _lblSrc.Text = Properties.Resources.lblSrc;
      _lblTgt.Text = Properties.Resources.lblTgt;

//      _tv.BeforeExpand += _tv_BeforeExpand;

      this.Load += frmParad_Load;
      //this.FormClosing += frmParad_FormClosing;
      //this.FormClosed += frmParad_FormClosed;

      _initTT();
    }

    //void _tv_BeforeExpand(object sender, TreeViewCancelEventArgs e)
    //{
    //  if (e.Node.Tag != null && ((fiBase)e.Node.Tag).pChildren != null)
    //  {
    //    foreach (fiBase fb in ((fiBase)e.Node.Tag).pChildren)
    //    {
    //      TreeNode tn = new TreeNode();
    //      tn.Text = fb.ToString();
    //      tn.Tag = fb;
    //      _tv.Nodes.Add(tn);
    //    }
    //    e.Cancel = false;
    //  }
    //  else
    //  {
    //    e.Cancel = true;
    //  }
    //}

    //void frmParad_FormClosed(object sender, FormClosedEventArgs e)
    //{
    //}

    void frmParad_Load(object sender, EventArgs e)
    {
      LoadParameterFromRegister();
    }

   // void frmParad_FormClosing(object sender, FormClosingEventArgs e)
   // {
   //   SaveParameterToRegister();
   //   e.Cancel = false;
   // }


    private void _init()
    {
      _log("beginig initialize program"+Environment.NewLine+"\t Please, waiting...");
      _qs = new Performer();
      _fias = new FIAS(_qs);
      if (_fias.LoadSession(@"T:\CASNet4\ParAd\sql\parad.sq3"))
      {
        _qs.pWDB.pConnectionString = _qs.pSes.DBConnection;
        if (_qs.pWDB.ConnectionOpen())
          _fias.LoadAll();
        else
          _log(_qs.pError.description);
      }
      else
        _err(@"Load session - T:\CASNet4\ParAd\sql\parad.sq3");
      _exp = new Experience();
      _pa = new ParAd(_fias, _exp);

      _log("finished initialize program");
    }

    private void _initTT()
    {
      _dgvTgtS1.Columns.Clear();
      _dgvTgtS1.AutoGenerateColumns = false;

      DataGridViewTextBoxColumn _dgvcText = new DataGridViewTextBoxColumn();
      _dgvcText.Name = "itemTitle";
      _dgvcText.DataPropertyName = "pItemTitle";
      _dgvcText.HeaderText = "Item";
      _dgvTgtS1.Columns.Add(_dgvcText);

      _dgvcText = new DataGridViewTextBoxColumn();
      _dgvcText.Name = "itemProperty";
      _dgvcText.DataPropertyName = "pItemProperty";
      _dgvcText.HeaderText = "Property";
      _dgvTgtS1.Columns.Add(_dgvcText);


      //_dgvcText = new DataGridViewTextBoxColumn();
      //_dgvcText.Name = "itemFIAS";
      //_dgvcText.DataPropertyName = "pItemFIASTitle";
      //_dgvcText.HeaderText = "FIAS";
      //_dgvTgt.Columns.Add(_dgvcText);

    }

    private void _cmdParser_Click(object sender, EventArgs e)
    {
      _log();
      _log("finished parser");
      _dgvTgtS1.DataSource = null;
      _add2comboBox(_cboSrc.Text); 

      if (_pa != null)
      {
        _pa.pSourceText = _cboSrc.Text;
        _pa.Run();

        _log("\t in array _pa count = " + _pa.pArrPaItems.Count);

        BindingSource bs = new BindingSource();
        bs.DataSource = _pa.pArrPaItems;
        _dgvTgtS1.DataSource = bs;


        _tv.Nodes.Clear();
        _loadTV(_tv.Nodes, _fias.pArrFiasItems);
      }
      else
      {
        _err("_pa == null (in [_cmdParser_Click])");
      }
      
      _log("finished parser");
    }

    private void _loadTV(TreeNodeCollection tvNC, ArrayList aAL)
    {
      foreach (fiBase fb in aAL)
      {
        TreeNode tn = new TreeNode();
        tn.Text = fb.ToString();
        tn.Tag = fb;
        tvNC.Add(tn);

        if (fb.pChildren != null && fb.pChildren.Count > 0)
          _loadTV(tn.Nodes, fb.pChildren);
      }
    }

    private void _add2comboBox(String aTxt)
    {
      foreach (String st in _cboSrc.Items)
      {
        if (st.Equals(aTxt))
        {
          _cboSrc.SelectedItem = st;
          return;
        }
      }

      _cboSrc.Items.Add(aTxt);
      _cboSrc.SelectedItem = aTxt;
    }

    private void _saveHistory()
    {
      //using (FileStream fs = File.Create(FILE_NAME_HISTORY))
      using (TextWriter wr = File.CreateText(FILE_NAME_HISTORY))
      {
        foreach (String st in _cboSrc.Items)
          wr.WriteLine(st);
      }
    }

    private void _readHistory()
    {
      if (File.Exists(FILE_NAME_HISTORY))
      {
        using (TextReader rd = File.OpenText(FILE_NAME_HISTORY))
        {
          while (rd.Peek() > -1)
            _cboSrc.Items.Add(rd.ReadLine());

        }
      }
    }

    private void LoadParameterFromRegister()
    {
      _readHistory();

      RegistryKey regKey = Registry.CurrentUser.OpenSubKey(REG_APP_PATH);

      try
      {
        if (regKey != null)
        {
           CASToolsReg.LoadSizeLocationForm(regKey, this);
          if (_cboSrc.Items.Count > 0)
          {
            _cboSrc.SelectedIndex = CASTools.ConvertToInt32Or0(regKey.GetValue("SourceInd"));
          }
          int sp = 0;
          sp = CASTools.ConvertToInt32Or0(regKey.GetValue("SplitePosition"));
          if (sp > 0)
            _sc.SplitterDistance = sp;
          sp = CASTools.ConvertToInt32Or0(regKey.GetValue("SpliteResultPosition"));
          if (sp > 0)
            _scResult.SplitterDistance = sp;
          CASToolsReg.LoadDataGridParameter(regKey, _dgvTgtS1, "ResTab");
        }
      }
      catch { }

      regKey.Close();
    }

    private void SaveParameterToRegister()
    {
      //RegistryKey regKey = Registry.CurrentUser.OpenSubKey(REG_APP_PATH);
      //if (regKey == null)
      RegistryKey  regKey = Registry.CurrentUser.CreateSubKey(REG_APP_PATH);
      CASToolsReg.SaveSizeLocationForm(regKey, this);
      CASToolsReg.SaveDataGridParameter(regKey, _dgvTgtS1, "ResTab");

      //regKey.SetValue("SourceText", _txtSrc.Text);
      regKey.SetValue("SourceInd", _cboSrc.SelectedIndex);
      regKey.SetValue("SplitePosition", _sc.SplitterDistance);
      regKey.SetValue("SpliteResultPosition", _scResult.SplitterDistance);

      regKey.Close();

      _saveHistory();
    }

    private void _cmdInit_Click(object sender, EventArgs e)
    {

      _init();

    }

    private void _cmdExit_Click(object sender, EventArgs e)
    {
      SaveParameterToRegister();
      Close();
    }
  }
}
