using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Json;
using System.Net;
using MyLib;

namespace SMLSINGHAControl
{
    public partial class _transferControl : UserControl
    {
        protected synctablename type = synctablename.Null;
        
        public synctablename _type
        {
            get { return this.type; }
            set {
                this.type = value;
                this._build();
            }

        }

        public _transferControl()
        {
            InitializeComponent();
        }
        protected virtual void _build()
        {
             // this.uri = "http://dev.smlsoft.com:7400/getdb/erp_expenses_list";
        }

        private void button_selectAll_Click(object sender, EventArgs e)
        {
            
        }

        private void button_selectNone_Click(object sender, EventArgs e)
        {

        }

    }
   
}
