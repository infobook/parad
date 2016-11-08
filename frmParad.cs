using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CommandAS.QueryLib;

namespace parad
{
  public partial class frmParad : Form
  {
    private Performer _qs;
    private FIAS _fias;
    private ParAd _pa;

    public frmParad()
    {
      InitializeComponent();

      _lblSrc.Text = Properties.Resources.lblSrc;
      _lblTgt.Text = Properties.Resources.lblTgt;

      _init();
      _initTT();
    }

    private void _init()
    {
      _qs = new Performer();
      _fias = new FIAS(_qs);
      _pa = new ParAd(_fias);
    }

    private void _initTT()
    {
      _dgvTgt.Columns.Clear();
      _dgvTgt.AutoGenerateColumns = false;
      _dgvTgt.DataSource = _pa.pArrPaItems;

      DataGridViewTextBoxColumn _dgvcText = new DataGridViewTextBoxColumn();
      _dgvcText.Name = "itemTitle";
      _dgvcText.DataPropertyName = "pItemTitle";
      _dgvcText.HeaderText = "Defined item";
      _dgvTgt.Columns.Add(_dgvcText);

    }

    private void _cmdParser_Click(object sender, EventArgs e)
    {
      _pa.pSourceText = _txtSrc.Text;
      _pa.StepOne_Characters();
    }
  }
}
