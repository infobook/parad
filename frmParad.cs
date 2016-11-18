using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;
using CommandAS.Tools;
using CommandAS.QueryLib;

namespace ProgTor.ParAd
{
  public partial class frmParad : Form
  {
    public const string REG_APP_PATH = @"SOFTWARE\ProgTor\ParAd";
    
    private Performer _qs;
    private FIAS _fias;
    private Experience _exp;
    private ParAd _pa;

    public frmParad()
    {
      InitializeComponent();

      _lblSrc.Text = Properties.Resources.lblSrc;
      _lblTgt.Text = Properties.Resources.lblTgt;


      this.Load += frmParad_Load;
      //this.FormClosing += frmParad_FormClosing;
      //this.FormClosed += frmParad_FormClosed;

      _initTT();
    }

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
        _log(@"Err: Load session - T:\CASNet4\ParAd\sql\parad.sq3");
      _exp = new Experience();
      _pa = new ParAd(_fias, _exp);

      _log("finished initialize program");
    }

    private void _initTT()
    {
      _dgvTgt.Columns.Clear();
      _dgvTgt.AutoGenerateColumns = false;

      DataGridViewTextBoxColumn _dgvcText = new DataGridViewTextBoxColumn();
      _dgvcText.Name = "itemTitle";
      _dgvcText.DataPropertyName = "pItemTitle";
      _dgvcText.HeaderText = "Item";
      _dgvTgt.Columns.Add(_dgvcText);

      _dgvcText = new DataGridViewTextBoxColumn();
      _dgvcText.Name = "itemProperty";
      _dgvcText.DataPropertyName = "pItemProperty";
      _dgvcText.HeaderText = "Property";
      _dgvTgt.Columns.Add(_dgvcText);


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

      _txtLogs.Invalidate();
      this.Refresh();
    }

    private void _cmdParser_Click(object sender, EventArgs e)
    {
      _log();
      _dgvTgt.DataSource = null;

      _pa.pSourceText = _txtSrc.Text;
      _pa.Run();

      _log("\t in array _pa count = " + _pa.pArrPaItems.Count);

      BindingSource bs = new BindingSource();
      bs.DataSource = _pa.pArrPaItems;
      _dgvTgt.DataSource = bs;
      
      _log("finished parser");
    }

    private void LoadParameterFromRegister()
    {
      RegistryKey regKey = Registry.CurrentUser.OpenSubKey(REG_APP_PATH);

      try
      {
        if (regKey != null)
        {
          CASToolsReg.LoadSizeLocationForm(regKey, this);
          object obj = regKey.GetValue("SourceText");
          if (obj != null)
            _txtSrc.Text = obj.ToString();

          int sp = 0;
          sp = CASTools.ConvertToInt32Or0(regKey.GetValue("SplitePosition"));
          if (sp > 0)
            _sc.SplitterDistance = sp; 
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

      regKey.SetValue("SourceText", _txtSrc.Text);
      regKey.SetValue("SplitePosition", _sc.SplitterDistance);

      regKey.Close();
    }

    private void toolStripButton1_Click(object sender, EventArgs e)
    {
      SaveParameterToRegister();
      Close();
    }

    private void _cmdInit_Click(object sender, EventArgs e)
    {

      _init();

    }
  }
}
