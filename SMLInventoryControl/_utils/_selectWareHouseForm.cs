using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SMLInventoryControl._utils
{
    public partial class _selectWareHouseForm : Form
    {
        public string _selectStr = "";

        public _selectWareHouseForm()
        {
            if (MyLib._myGlobal._isDesignMode == false)
            {
                InitializeComponent();
            }
        }

        private void _cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void _saveButton1_Click(object sender, EventArgs e)
        {
            StringBuilder __result = new StringBuilder();
            for (int __row = 0; __row < this._wareHouse._grid._rowData.Count; __row++)
            {
                int __checked = (int)MyLib._myGlobal._decimalPhase(this._wareHouse._grid._cellGet(__row, "Check").ToString());
                if (__checked == 1)
                {
                    string __code = this._wareHouse._grid._cellGet(__row, _g.d.ic_warehouse._code).ToString().ToUpper().Trim();
                    if (__code.Length > 0)
                    {
                        if (__result.Length > 0)
                        {
                            __result.Append(",");
                        }
                        __result.Append(__code);
                    }
                }
            }
            this._selectStr = __result.ToString();
            this.Close();
        }
    }
}
