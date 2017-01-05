using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SMLInventoryControl._utils
{
    public partial class _selectLocaionForm : Form
    {
        public string _selectStr = "";

        public _selectLocaionForm()
        {
            InitializeComponent();
        }

        private void _saveButton_Click(object sender, EventArgs e)
        {
            StringBuilder __result = new StringBuilder();
            for (int __row = 0; __row < this._location._grid._rowData.Count; __row++)
            {
                int __checked = (int)MyLib._myGlobal._decimalPhase(this._location._grid._cellGet(__row, "Check").ToString());
                if (__checked == 1)
                {
                    string __locationCode = this._location._grid._cellGet(__row, _g.d.ic_shelf._code).ToString().ToUpper().Trim();
                    if (__locationCode.Length > 0)
                    {
                        string __wareHouseCode = this._location._grid._cellGet(__row, _g.d.ic_shelf._whcode).ToString().ToUpper().Trim();
                        if (__result.Length > 0)
                        {
                            __result.Append(",");
                        }
                        __result.Append("[" + __wareHouseCode + ":" + __locationCode + "]");
                    }
                }
            }
            this._selectStr = __result.ToString();
            this.Close();
        }

        private void _cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
