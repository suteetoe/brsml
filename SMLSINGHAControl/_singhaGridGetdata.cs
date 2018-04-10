using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;


namespace SMLSINGHAControl
{
    public partial class _singhaGridGetdata : MyLib._myGrid
    {
        public _singhaGridGetdata()
        {
            this._build();
        }

        public object CurrentCell { get; internal set; }

        public void _build()
        {
            this._addColumn("check", 11, 0, 20, true, false, false, false);
            this._addColumn("Master Name", 1, 255, 20, true, false, true, false);
            this._addColumn("Table Name", 1, 255, 20, true, false, true, false);
            this._addColumn("Status", 1, 255, 20, true, false, true, false);
            this._addColumn("Filter", 12, 255, 20, true, false, true, false);
        }

        public class _priceStruct
        {
            public string _code = "";
            public string _name = "";
        }
    }
}
