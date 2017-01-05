using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLERPICInfo
{
    public partial class _stkBalance : UserControl
    {
        private string _formatNumberQty = _g.g._getFormatNumberStr(1);
        private string _formatNumberCost = _g.g._getFormatNumberStr(2);
        private string _formatNumberAmount = _g.g._getFormatNumberStr(3);
        private SMLERPControl._selectWarehouseAndLocationForm _selectWarehouseAndLocation = null;
        private _g.g._productCostType _costMode;

        public _stkBalance(_g.g._productCostType costMode)
        {
            InitializeComponent();
            if (MyLib._myGlobal._isDesignMode == false)
            {
                this.toolStrip1.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            }
            //
            this._costMode = costMode;
            //
            this._resultGrid._columnTopActive = true;
            this._resultGrid._table_name = _g.d.ic_resource._table;
            this._resultGrid._addColumn(_g.d.ic_resource._ic_code, 1, 10, 10);
            this._resultGrid._addColumn(_g.d.ic_resource._ic_name, 1, 10, 20);
            this._resultGrid._addColumn(_g.d.ic_resource._ic_unit_code, 1, 5, 10);
            //
            this._resultGrid._addColumn(_g.d.ic_resource._qty_in, 3, 10, 8, false, false, false, false, _formatNumberQty);

            if ((_g.g._companyProfile._disable_item_cost == true && _g.g._companyProfile._is_User_show_item_cost == false) == false)
            {
                this._resultGrid._addColumn(_g.d.ic_resource._average_cost_in, 3, 10, 8, false, false, false, false, _formatNumberQty);
                this._resultGrid._addColumn(_g.d.ic_resource._amount_in, 3, 10, 10, false, false, false, false, _formatNumberCost);
            }

            this._resultGrid._addColumn(_g.d.ic_resource._qty_out, 3, 10, 8, false, false, false, false, _formatNumberQty);

            if ((_g.g._companyProfile._disable_item_cost == true && _g.g._companyProfile._is_User_show_item_cost == false) == false)
            {
                this._resultGrid._addColumn(_g.d.ic_resource._average_cost_out, 3, 10, 8, false, false, false, false, _formatNumberCost);
                this._resultGrid._addColumn(_g.d.ic_resource._amount_out, 3, 10, 10, false, false, false, false, _formatNumberAmount);
            }

            this._resultGrid._addColumn(_g.d.ic_resource._balance_qty, 3, 10, 8, false, false, false, false, _formatNumberQty);
            if ((_g.g._companyProfile._disable_item_cost == true && _g.g._companyProfile._is_User_show_item_cost == false) == false)
            {

                this._resultGrid._addColumn(_g.d.ic_resource._average_cost, 3, 10, 8, false, false, false, false, _formatNumberCost);
                this._resultGrid._addColumn(_g.d.ic_resource._average_cost_end, 3, 10, 8, false, false, false, false, _formatNumberCost);
                this._resultGrid._addColumn(_g.d.ic_resource._balance_amount, 3, 10, 10, false, false, false, false, _formatNumberAmount);
            }
            //
            if ((_g.g._companyProfile._disable_item_cost == true && _g.g._companyProfile._is_User_show_item_cost == false) == false)
            {
                this._resultGrid._addColumnTop(_g.d.ic_resource._in, this._resultGrid._findColumnByName(_g.d.ic_resource._qty_in), this._resultGrid._findColumnByName(_g.d.ic_resource._amount_in));
                this._resultGrid._addColumnTop(_g.d.ic_resource._out, this._resultGrid._findColumnByName(_g.d.ic_resource._qty_out), this._resultGrid._findColumnByName(_g.d.ic_resource._amount_out));
            }
            else
            {
                this._resultGrid._addColumnTop(_g.d.ic_resource._in, this._resultGrid._findColumnByName(_g.d.ic_resource._qty_in), this._resultGrid._findColumnByName(_g.d.ic_resource._qty_in));
                this._resultGrid._addColumnTop(_g.d.ic_resource._out, this._resultGrid._findColumnByName(_g.d.ic_resource._qty_out), this._resultGrid._findColumnByName(_g.d.ic_resource._qty_out));
            }

            if ((_g.g._companyProfile._disable_item_cost == true && _g.g._companyProfile._is_User_show_item_cost == false) == false)
            {

                this._resultGrid._addColumnTop(_g.d.ic_resource._net, this._resultGrid._findColumnByName(_g.d.ic_resource._balance_qty), this._resultGrid._findColumnByName(_g.d.ic_resource._balance_amount));
            }
            else
            {
                this._resultGrid._addColumnTop(_g.d.ic_resource._net, this._resultGrid._findColumnByName(_g.d.ic_resource._balance_qty), this._resultGrid._findColumnByName(_g.d.ic_resource._balance_qty));
            }
            //
            this._resultGrid._setColumnBackground(_g.d.ic_resource._qty_in, Color.AliceBlue);
            this._resultGrid._setColumnBackground(_g.d.ic_resource._average_cost_in, Color.AliceBlue);
            this._resultGrid._setColumnBackground(_g.d.ic_resource._amount_in, Color.AliceBlue);
            //
            this._resultGrid._setColumnBackground(_g.d.ic_resource._balance_qty, Color.AliceBlue);
            this._resultGrid._setColumnBackground(_g.d.ic_resource._average_cost, Color.AliceBlue);
            this._resultGrid._setColumnBackground(_g.d.ic_resource._average_cost_end, Color.AliceBlue);
            this._resultGrid._setColumnBackground(_g.d.ic_resource._balance_amount, Color.AliceBlue);
            //
            this._resultGrid._total_show = true;
            this._resultGrid._calcPersentWidthToScatter();
            this._resultGrid._mouseDoubleClick += new MyLib.MouseDoubleClickHandler(_resultGrid__mouseDoubleClick);
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

            ToolStripButton __movement = new ToolStripButton();
            __movement.Text = "เคลื่อนไหว";
            __movement.Click += (s1, e1) =>
            {
                Form __form = new Form();
                __form.WindowState = FormWindowState.Maximized;
                _stkMovement __movementControl = new _stkMovement(0);
                __movementControl.Dock = DockStyle.Fill;
                __form.Controls.Add(__movementControl);
                __form.ShowDialog();
            };
            this.toolStrip1.Items.Add(__movement);

        }

        void _resultGrid__mouseDoubleClick(object sender, MyLib.GridCellEventArgs e)
        {
            string __itemCode = this._resultGrid._cellGet(e._row, _g.d.ic_resource._ic_code).ToString();
            SMLERPControl._icBalanceForm __icBalance = new SMLERPControl._icBalanceForm();
            __icBalance._load(__itemCode);
            __icBalance.ShowDialog();
            __icBalance.Dispose();
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
                string __itemBegin = this._conditionScreen._getDataStr(_g.d.ic_resource._ic_code_begin);
                string __itemEnd = this._conditionScreen._getDataStr(_g.d.ic_resource._ic_code_end);
                DateTime __dateBegin = MyLib._myGlobal._convertDate(this._conditionScreen._getDataStr(_g.d.ic_resource._date_begin));
                DateTime __dateEnd = MyLib._myGlobal._convertDate(this._conditionScreen._getDataStr(_g.d.ic_resource._date_end));
                SMLERPControl._icInfoProcess __process = new SMLERPControl._icInfoProcess();
                this._resultGrid._loadFromDataTable(__process._stkStockInfoAndBalance(this._costMode, null, __itemBegin, __itemEnd, __dateBegin, __dateEnd, false, SMLERPControl._icInfoProcess._stockBalanceType.ยอดคงเหลือตามสินค้า, this._selectWarehouseAndLocation._wareHouseSelected(), this._selectWarehouseAndLocation._locationSelected(), true, false, this._conditionScreen._getDataStr(_g.d.ic_resource._item_name)));
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
