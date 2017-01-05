using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace SMLERPICInfo
{
    public partial class _stkMovementSum : UserControl
    {
        string __formatNumber;
        string __formatDate = "dd/MM/yyyy";
        int _mode;

        /// <summary>
        /// 0=ตามจำนวน,1=ตามมูลค่า
        /// </summary>
        /// <param name="mode"></param>
        public _stkMovementSum(int mode)
        {
            InitializeComponent();
            if (MyLib._myGlobal._isDesignMode == false)
            {
                this.toolStrip1.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            }
            //
            this._mode = mode;
            this._resultGrid._columnTopActive = true;
            this._resultGrid._table_name = _g.d.ic_resource._table;
            this._resultGrid._addColumn(_g.d.ic_resource._ic_code, 1, 10, 10);
            this._resultGrid._addColumn(_g.d.ic_resource._ic_name, 1, 10, 20);
            this._resultGrid._addColumn(_g.d.ic_resource._ic_unit_code, 1, 5, 10);
            //
            this.__formatNumber = _g.g._getFormatNumberStr((this._mode == 0) ? 1 : 3);
            this._resultGrid._addColumn(_g.d.ic_resource._balance_qty_first, 3, 10, 8, false, false, false, false, this.__formatNumber);
            this._resultGrid._addColumn(_g.d.ic_resource._trans_flag_12, 3, 10, 8, false, false, false, false, this.__formatNumber);
            this._resultGrid._addColumn(_g.d.ic_resource._trans_flag_48, 3, 10, 8, false, false, false, false, this.__formatNumber);
            this._resultGrid._addColumn(_g.d.ic_resource._trans_flag_58, 3, 10, 8, false, false, false, false, this.__formatNumber);
            this._resultGrid._addColumn(_g.d.ic_resource._trans_flag_60, 3, 10, 8, false, false, false, false, this.__formatNumber);
            this._resultGrid._addColumn(_g.d.ic_resource._trans_flag_66, 3, 10, 8, false, false, false, false, this.__formatNumber);
            this._resultGrid._addColumn(_g.d.ic_resource._trans_flag_54, 3, 10, 8, false, false, false, false, this.__formatNumber);
            this._resultGrid._addColumn(_g.d.ic_resource._trans_flag_44, 3, 10, 8, false, false, false, false, this.__formatNumber);
            this._resultGrid._addColumn(_g.d.ic_resource._trans_flag_16, 3, 10, 8, false, false, false, false, this.__formatNumber);
            this._resultGrid._addColumn(_g.d.ic_resource._trans_flag_56, 3, 10, 8, false, false, false, false, this.__formatNumber);
            this._resultGrid._addColumn(_g.d.ic_resource._trans_flag_68, 3, 10, 8, false, false, false, false, this.__formatNumber);
            this._resultGrid._addColumn(_g.d.ic_resource._balance_qty, 3, 10, 8, false, false, false, false, this.__formatNumber);
            //
            this._resultGrid._addColumnTop(_g.d.ic_resource._in, this._resultGrid._findColumnByName(_g.d.ic_resource._trans_flag_12), this._resultGrid._findColumnByName(_g.d.ic_resource._trans_flag_54));
            this._resultGrid._addColumnTop(_g.d.ic_resource._out, this._resultGrid._findColumnByName(_g.d.ic_resource._trans_flag_44), this._resultGrid._findColumnByName(_g.d.ic_resource._trans_flag_68));
            //
            this._resultGrid._setColumnBackground(_g.d.ic_resource._trans_flag_12, Color.AliceBlue);
            this._resultGrid._setColumnBackground(_g.d.ic_resource._trans_flag_48, Color.AliceBlue);
            this._resultGrid._setColumnBackground(_g.d.ic_resource._trans_flag_58, Color.AliceBlue);
            this._resultGrid._setColumnBackground(_g.d.ic_resource._trans_flag_60, Color.AliceBlue);
            this._resultGrid._setColumnBackground(_g.d.ic_resource._trans_flag_66, Color.AliceBlue);
            this._resultGrid._setColumnBackground(_g.d.ic_resource._trans_flag_54, Color.AliceBlue);
            //
            this._resultGrid._setColumnBackground(_g.d.ic_resource._balance_qty_first, Color.LightCyan);
            this._resultGrid._setColumnBackground(_g.d.ic_resource._balance_qty, Color.LightCyan);
            //
            this._resultGrid._total_show = true;
            this._resultGrid._calcPersentWidthToScatter();
        }

        private void _closeButton_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void _processButton_Click(object sender, EventArgs e)
        {
            this._conditionScreen._saveLastControl();
            string __itemBegin = this._conditionScreen._getDataStr(_g.d.ic_resource._ic_code_begin);
            string __itemEnd = this._conditionScreen._getDataStr(_g.d.ic_resource._ic_code_end);
            DateTime __dateBegin = MyLib._myGlobal._convertDate(this._conditionScreen._getDataStr(_g.d.ic_resource._date_begin));
            DateTime __dateEnd = MyLib._myGlobal._convertDate(this._conditionScreen._getDataStr(_g.d.ic_resource._date_end));
            string __getICGroup = this._conditionScreen._getDataStr(_g.d.ic_resource._ic_group);

            string __extraWhere = "";
            if (__getICGroup.Length > 0)
            {
                string __genGroupLis = MyLib._myUtil._genCodeList(_g.d.ic_inventory._table + "." + _g.d.ic_inventory._group_main, __getICGroup);
                __extraWhere = " exists (select code from ic_inventory where ic_inventory.code=" + _g.d.ic_resource._ic_code + " and " + __genGroupLis + ")";
            }

            SMLERPControl._icInfoProcess __process = new SMLERPControl._icInfoProcess();
            this._resultGrid._loadFromDataTable(__process._stkStockMovementSum(_g.g._productCostType.ปรกติ, this._mode, null, __itemBegin, __itemEnd, __dateBegin, __dateEnd, false, __extraWhere));
        }

    }
}
