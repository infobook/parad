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

        public frmParad()
        {
            InitializeComponent();


        }

        private void _init()
        {
            _qs = new Performer();
            _fias = new FIAS(_qs);

        }
    }
}
