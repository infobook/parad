using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using CommandAS.QueryLib;

namespace parad
{
    public class FIAS
    {
        private Performer _qs;

        private DataTable _socrbase;

        public DataTable SocrBase
        {
            get { return _socrbase; }
        }

        public FIAS(Performer aQS)
        {
            _qs = aQS;

        }

        public void LoadSocrBase()
        {

        }
    }
}
