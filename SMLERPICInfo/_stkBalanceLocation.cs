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
    public partial class _stkBalanceLocation : UserControl
    {
        private SMLERPControl._selectWarehouseAndLocationForm _selectWarehouseAndLocation = null;
        private _stkBalanceLocationGrid _resultGrid;

        public _stkBalanceLocation()
        {
            InitializeComponent();
            if (MyLib._myGlobal._isDesignMode == false)
            {
                this.toolStrip1.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            }
            //
            this._resultGrid = new _stkBalanceLocationGrid();
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
            this._resultGrid.Dock = DockStyle.Fill;
            this.Controls.Add(this._resultGrid);
            this._conditionScreen.BringToFront();
            this.toolStrip1.BringToFront();
            this._resultGrid.BringToFront();
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

        private void _closeButton_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void _processNow()
        {
            this._conditionScreen._saveLastControl();
            //
            string __itemBegin = this._conditionScreen._getDataStr(_g.d.ic_resource._ic_code_begin);
            string __itemEnd = this._conditionScreen._getDataStr(_g.d.ic_resource._ic_code_end);
            DateTime __dateBegin = MyLib._myGlobal._convertDate(this._conditionScreen._getDataStr(_g.d.ic_resource._date_begin));
            DateTime __dateEnd = MyLib._myGlobal._convertDate(this._conditionScreen._getDataStr(_g.d.ic_resource._date_end));
            this._resultGrid._processNow( _g.g._productCostType.ปรกติ,this._conditionScreen._getDataStr(_g.d.ic_resource._item_name), __itemBegin, __itemEnd, __dateBegin, __dateEnd, this._selectWarehouseAndLocation._wareHouseSelected(), this._selectWarehouseAndLocation._locationSelected());
            /*SMLERPControl._icInfoProcess __process = new SMLERPControl._icInfoProcess();
            this._resultGrid._loadFromDataTable(__process._stkStockInfoAndBalanceByLocation(null, __itemBegin, __itemEnd, __dateBegin, __dateEnd, false, SMLERPControl._icInfoProcess._stockBalanceType.ยอดคงเหลือตามที่เก็บ, this._selectWarehouseAndLocation._wareHouseSelected(), this._selectWarehouseAndLocation._locationSelected(), true, false, this._conditionScreen._getDataStr(_g.d.ic_resource._item_name)));*/
            //
        }

        private void _processButton_Click(object sender, EventArgs e)
        {
            this._processNow();
        }
    }
}
