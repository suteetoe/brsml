using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLERPICInfo
{
    public partial class _stkBalanceWareHouse : UserControl
    {
        private SMLERPControl._selectWarehouseAndLocationForm _selectWarehouseAndLocation = null;

        public _stkBalanceWareHouse()
        {
            InitializeComponent();
            if (MyLib._myGlobal._isDesignMode == false)
            {
                this.toolStrip1.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            }
            //
            ToolStripButton __wh = new ToolStripButton();
            __wh.Text = "เลือกคลัง/ที่เก็บสินค้า";
            this._selectWarehouseAndLocation = new SMLERPControl._selectWarehouseAndLocationForm(true);
            __wh.Click += (s1, e1) =>
            {
                this._selectWarehouseAndLocation.ShowDialog();
            };
            this._selectWarehouseAndLocation._closeButton.Click += (s1, e1) =>
            {
                this._selectWarehouseAndLocation.Close();
            };
            this.toolStrip1.Items.Add(__wh);
        }

        private void _closeButton_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F12)
            {
                this._processNow();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void _processNow()
        {
            try
            {
                this._conditionScreen._saveLastControl();
                //
                string __itemBegin = this._conditionScreen._getDataStr(_g.d.ic_resource._ic_code_begin);
                string __itemEnd = this._conditionScreen._getDataStr(_g.d.ic_resource._ic_code_end);
                DateTime __dateBegin = MyLib._myGlobal._convertDate(this._conditionScreen._getDataStr(_g.d.ic_resource._date_begin));
                DateTime __dateEnd = MyLib._myGlobal._convertDate(this._conditionScreen._getDataStr(_g.d.ic_resource._date_end));
                this._resultGrid._processNow( _g.g._productCostType.ปรกติ,this._conditionScreen._getDataStr(_g.d.ic_resource._item_name), __itemBegin, __itemEnd, __dateBegin, __dateEnd, this._selectWarehouseAndLocation._wareHouseSelected());
            }
            catch (Exception __ex)
            {
                MessageBox.Show(__ex.Message.ToString());
            }
        }

        private void _processButton_Click(object sender, EventArgs e)
        {
            this._processNow();
        }
    }
}
