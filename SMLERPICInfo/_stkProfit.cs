using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLERPICInfo
{
    public partial class _stkProfit : UserControl
    {
        private string __formatNumberAmount = _g.g._getFormatNumberStr(3);
        private _infoStkProfitEnum _mode;
        private string _itemCode = "";
        private string _dateBegin = "";
        private string _dateEnd = "";
        private string _custCode = "";
        private string _titleName = "";
        private _stkProfitConditionScreen _condition;
        private _g.g._productCostType _costMode;

        /// <summary>
        /// รายงานกำไรขั้นต้น
        /// </summary>
        /// <param name="mode">0=ตามสินค้า,1=ตามเอกสาร,2=ตามลูกค้า</param>
        /// 
        public _stkProfit(_infoStkProfitEnum mode, _g.g._productCostType costMode, Boolean autoProcess, string titleName, string itemCode, string custCode, string dateBegin, string dateEnd)
        {
            InitializeComponent();
            if (MyLib._myGlobal._isDesignMode == false)
            {
                this._toolStrip.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            }
            //
            this._costMode = costMode;
            this._mode = mode;
            this._titleName = titleName;
            this._itemCode = itemCode;
            this._custCode = custCode;
            this._dateBegin = dateBegin;
            this._dateEnd = dateEnd;
            this._condition = new _stkProfitConditionScreen(this._mode, this._dateBegin, this._dateEnd);
            this._condition.Dock = DockStyle.Top;
            this._condition.AutoSize = true;
            this.Controls.Add(this._condition);
            //
            this._resultGrid._columnTopActive = true;
            this._resultGrid._table_name = _g.d.ic_resource._table;
            this._resultGrid._beforeDisplayTotal += new MyLib.BeforeDisplayTotalHandler(_resultGrid__beforeDisplayTotal);
            switch (this._mode)
            {
                case _infoStkProfitEnum.กำไรขั้นต้น_สินค้า:
                    // ตามสินค้า
                    this._resultGrid._addColumn(_g.d.ic_resource._ic_code, 1, 10, 10);
                    this._resultGrid._addColumn(_g.d.ic_resource._ic_name, 1, 10, 20);
                    this._resultGrid._addColumn(_g.d.ic_resource._ic_unit_code, 1, 5, 10);
                    break;
                case _infoStkProfitEnum.กำไรขั้นต้น_เอกสาร:
                    // ตามเอกสาร
                    this._resultGrid._addColumn(_g.d.ic_resource._doc_date, 4, 10, 10);
                    this._resultGrid._addColumn(_g.d.ic_resource._doc_no, 1, 10, 15);
                    break;
                case _infoStkProfitEnum.กำไรขั้นต้น_ลูกค้า:
                    // ตามลูกค้า
                    this._resultGrid._addColumn(_g.d.ic_resource._ar_code, 1, 10, 10);
                    this._resultGrid._addColumn(_g.d.ic_resource._ar_detail, 1, 10, 15);
                    break;
            }
            //
            this._resultGrid._addColumn(_g.d.ic_resource._qty_sale, 3, 10, 8, false, false, false, false, __formatNumberAmount);
            this._resultGrid._addColumn(_g.d.ic_resource._amount_sale, 3, 10, 8, false, false, false, false, __formatNumberAmount);
            this._resultGrid._addColumn(_g.d.ic_resource._cost_sale, 3, 10, 10, false, false, false, false, __formatNumberAmount);
            //
            this._resultGrid._addColumn(_g.d.ic_resource._qty_sale_return, 3, 10, 8, false, false, false, false, __formatNumberAmount);
            this._resultGrid._addColumn(_g.d.ic_resource._amount_sale_return, 3, 10, 8, false, false, false, false, __formatNumberAmount);
            this._resultGrid._addColumn(_g.d.ic_resource._cost_sale_return, 3, 10, 8, false, false, false, false, __formatNumberAmount);
            //
            this._resultGrid._addColumn(_g.d.ic_resource._amount_net, 3, 8, 8, false, false, false, false, __formatNumberAmount);
            this._resultGrid._addColumn(_g.d.ic_resource._cost_net, 3, 10, 8, false, false, false, false, __formatNumberAmount);
            this._resultGrid._addColumn(_g.d.ic_resource._profit_lost_amount, 3, 8, 8, false, false, false, false, __formatNumberAmount);
            this._resultGrid._addColumn(_g.d.ic_resource._profit_lost_persent, 3, 8, 8, false, false, false, false, __formatNumberAmount);
            //this._resultGrid._addColumn(_g.d.ic_resource._profit_lost_persent_by_cost, 3, 8, 8, false, false, false, false, __formatNumberAmount);
            //this._resultGrid._addColumn(_g.d.ic_resource._profit_lost_persent_by_amount, 3, 8, 8, false, false, false, false, __formatNumberAmount);
            //
            this._resultGrid._addColumnTop(_g.d.ic_resource._sale, this._resultGrid._findColumnByName(_g.d.ic_resource._qty_sale), this._resultGrid._findColumnByName(_g.d.ic_resource._cost_sale));
            this._resultGrid._addColumnTop(_g.d.ic_resource._sale_return, this._resultGrid._findColumnByName(_g.d.ic_resource._qty_sale_return), this._resultGrid._findColumnByName(_g.d.ic_resource._cost_sale_return));
            this._resultGrid._addColumnTop(_g.d.ic_resource._net, this._resultGrid._findColumnByName(_g.d.ic_resource._amount_net), this._resultGrid._findColumnByName(_g.d.ic_resource._profit_lost_persent));
            //
            this._resultGrid._setColumnBackground(_g.d.ic_resource._qty_sale, Color.AliceBlue);
            this._resultGrid._setColumnBackground(_g.d.ic_resource._amount_sale, Color.AliceBlue);
            this._resultGrid._setColumnBackground(_g.d.ic_resource._cost_sale, Color.AliceBlue);
            //
            this._resultGrid._setColumnBackground(_g.d.ic_resource._amount_net, Color.AliceBlue);
            this._resultGrid._setColumnBackground(_g.d.ic_resource._cost_net, Color.AliceBlue);
            this._resultGrid._setColumnBackground(_g.d.ic_resource._profit_lost_amount, Color.AliceBlue);
            //this._resultGrid._setColumnBackground(_g.d.ic_resource._profit_lost_persent_by_cost, Color.AliceBlue);
            //this._resultGrid._setColumnBackground(_g.d.ic_resource._profit_lost_persent_by_amount, Color.AliceBlue);
            //
            this._resultGrid._total_show = true;
            this._resultGrid._calcPersentWidthToScatter();
            this._resultGrid._mouseDoubleClick += new MyLib.MouseDoubleClickHandler(_resultGrid__mouseDoubleClick);
            if (autoProcess)
            {
                this._process();
            }
        }

        void _resultGrid__beforeDisplayTotal(object sender)
        {
            decimal __amount = ((MyLib._myGrid._columnType)this._resultGrid._columnList[this._resultGrid._findColumnByName(_g.d.ic_resource._amount_net)])._total;
            decimal __profitAmount = ((MyLib._myGrid._columnType)this._resultGrid._columnList[this._resultGrid._findColumnByName(_g.d.ic_resource._profit_lost_amount)])._total;

            decimal __calcPersent = (__amount == 0M) ? 0 : (__profitAmount * 100M) / __amount;
            ((MyLib._myGrid._columnType)this._resultGrid._columnList[this._resultGrid._findColumnByName(_g.d.ic_resource._profit_lost_persent)])._total = __calcPersent;
        }

        void _resultGrid__mouseDoubleClick(object sender, MyLib.GridCellEventArgs e)
        {
            switch (this._mode)
            {
                case _infoStkProfitEnum.กำไรขั้นต้น_สินค้า:
                    {
                        // ตามสินค้า
                        _stkProfitChoiceForm __choice = new _stkProfitChoiceForm("ตามเอกสาร", "ตามลูกค้า");
                        __choice.ShowDialog();
                        _stkProfitChoiceEnum __choiceSelect = __choice._result;
                        __choice.Dispose();
                        _stkProfit __drillDown = null;
                        string __itemCode = this._resultGrid._cellGet(e._row, _g.d.ic_resource._ic_code).ToString();
                        string __itemName = this._resultGrid._cellGet(e._row, _g.d.ic_resource._ic_name).ToString();
                        string __dateBegin = this._condition._getDataStr(_g.d.ic_resource._date_begin);
                        string __dateEnd = this._condition._getDataStr(_g.d.ic_resource._date_end);
                        string __title = (this._titleName.Length != 0) ? " : " : "";
                        __title = this._titleName + __title + __itemCode + " (" + __itemName + ")";
                        switch (__choiceSelect)
                        {
                            case _stkProfitChoiceEnum.B1:
                                __drillDown = new _stkProfit(_infoStkProfitEnum.กำไรขั้นต้น_เอกสาร, this._costMode, true, __title, __itemCode, this._custCode, __dateBegin, __dateEnd);
                                break;
                            case _stkProfitChoiceEnum.B2:
                                __drillDown = new _stkProfit(_infoStkProfitEnum.กำไรขั้นต้น_ลูกค้า, this._costMode, true, __title, __itemCode, this._custCode, __dateBegin, __dateEnd);
                                break;
                        }
                        if (__drillDown != null)
                        {
                            _stkDrillDownForm __drillDownForm = new _stkDrillDownForm();
                            __drillDownForm.Text = __title;
                            __drillDown.Dock = DockStyle.Fill;
                            __drillDown._toolStrip.Visible = false;
                            __drillDownForm.Controls.Add(__drillDown);
                            __drillDownForm.ShowDialog();
                        }
                    }
                    break;
                case _infoStkProfitEnum.กำไรขั้นต้น_เอกสาร:
                    {
                        // ตามเอกสาร
                        string __docNo = this._resultGrid._cellGet(e._row, _g.d.ic_resource._doc_no).ToString();
                        MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                        int __transFlag = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ);
                        DataTable __findDoc = __myFrameWork._queryShort("select " + _g.d.ic_trans._trans_flag + " from " + _g.d.ic_trans._table + " where " + _g.d.ic_trans._doc_no + "=\'" + __docNo + "\'").Tables[0];
                        if (__findDoc.Rows.Count > 0)
                        {
                            __transFlag = (int)MyLib._myGlobal._decimalPhase(__findDoc.Rows[0][_g.d.ic_trans._trans_flag].ToString());
                        }
                        string __title = (this._titleName.Length != 0) ? " : " : "";
                        __title = this._titleName + __title + __docNo;
                        _stkDrillDownForm __drillDownForm = new _stkDrillDownForm();
                        __drillDownForm.Text = __title;
                        SMLInventoryControl._icTransControl __drillDown = new SMLInventoryControl._icTransControl();
                        __drillDown._transControlDisplayOnly = true;
                        __drillDown._transControlType = _g.g._transFlagGlobal._transFlagByNumber(__transFlag);
                        __drillDown._myManageTrans._dataList._button.Enabled = false;
                        __drillDown._myToolBar.Visible = false;
                        __drillDown._mySelectBar.Visible = false;
                        __drillDown.Dock = DockStyle.Fill;
                        __drillDown._getConfig();
                        __drillDown._loadDataToScreen(__docNo, " where " + _g.d.ic_trans._doc_no + "=\'" + __docNo + "\' and " + _g.d.ic_trans._trans_flag + "=" + __transFlag.ToString());
                        __drillDownForm.Controls.Add(__drillDown);
                        __drillDownForm.ShowDialog();
                    }
                    break;
                case _infoStkProfitEnum.กำไรขั้นต้น_ลูกค้า:
                    {
                        // ตามลูกค้า
                        _stkProfitChoiceForm __choice = new _stkProfitChoiceForm("ตามเอกสาร", "ตามสินค้า");
                        __choice.ShowDialog();
                        _stkProfitChoiceEnum __choiceSelect = __choice._result;
                        __choice.Dispose();
                        _stkProfit __drillDown = null;
                        string __arCode = this._resultGrid._cellGet(e._row, _g.d.ic_resource._ar_code).ToString();
                        string __arName = this._resultGrid._cellGet(e._row, _g.d.ic_resource._ar_detail).ToString();
                        string __dateBegin = this._condition._getDataStr(_g.d.ic_resource._date_begin);
                        string __dateEnd = this._condition._getDataStr(_g.d.ic_resource._date_end);
                        string __title = (this._titleName.Length != 0) ? " : " : "";
                        __title = this._titleName + __title + __arCode + " (" + __arName + ")";
                        switch (__choiceSelect)
                        {
                            case _stkProfitChoiceEnum.B1:
                                __drillDown = new _stkProfit(_infoStkProfitEnum.กำไรขั้นต้น_เอกสาร, this._costMode, true, __title, this._itemCode, __arCode, __dateBegin, __dateEnd);
                                break;
                            case _stkProfitChoiceEnum.B2:
                                __drillDown = new _stkProfit(_infoStkProfitEnum.กำไรขั้นต้น_สินค้า, this._costMode, true, __title, this._itemCode, __arCode, __dateBegin, __dateEnd);
                                break;
                        }
                        if (__drillDown != null)
                        {
                            _stkDrillDownForm __drillDownForm = new _stkDrillDownForm();
                            __drillDownForm.Text = __title;
                            __drillDown.Dock = DockStyle.Fill;
                            __drillDown._toolStrip.Visible = false;
                            __drillDownForm.Controls.Add(__drillDown);
                            __drillDownForm.ShowDialog();
                        }
                    }
                    break;
            }
        }

        private void _closeButton_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void _process()
        {
            try
            {
                SMLERPGlobal._smlFrameWork __smlFrameWork = new SMLERPGlobal._smlFrameWork();
                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                string __reportGuid = Guid.NewGuid().ToString().ToLower();
                string __itemCodeBegin = (this._itemCode.Length > 0) ? this._itemCode : this._condition._getDataStr(_g.d.ic_resource._ic_code_begin);
                string __itemCodeEnd = (this._itemCode.Length > 0) ? this._itemCode : this._condition._getDataStr(_g.d.ic_resource._ic_code_end);
                string __arCodeBegin = (this._custCode.Length > 0) ? this._custCode : this._condition._getDataStr(_g.d.ic_resource._ar_code_begin);
                string __arCodeEnd = (this._custCode.Length > 0) ? this._custCode : this._condition._getDataStr(_g.d.ic_resource._ar_code_end);
                string __dateBegin = (this._dateBegin.Length > 0) ? this._dateBegin : this._condition._getDataStr(_g.d.ic_resource._date_begin);
                string __dateEnd = (this._dateEnd.Length > 0) ? this._dateEnd : this._condition._getDataStr(_g.d.ic_resource._date_end);
                int __profitMode = _global._infoStkProfitNumber(this._mode);
                if (this._costMode == _g.g._productCostType.รวมต้นทุนแฝง)
                {
                    __profitMode = __profitMode + 100;
                }
                long __fileSize = MyLib._myGlobal._intPhase(__smlFrameWork._process_profit_and_lost_by_product_stream(__profitMode, MyLib._myGlobal._databaseName, __itemCodeBegin, __itemCodeEnd, __arCodeBegin, __arCodeEnd, __dateBegin, __dateEnd, "", "", __reportGuid));
                DataSet __resultDataSet = MyLib._myGlobal._convertStringToDataSet(__myFrameWork._loadStream(__reportGuid, __fileSize, "Report"));
                this._resultGrid._loadFromDataTable(__resultDataSet.Tables[0]);
                __resultDataSet.Dispose();
            }
            catch (Exception __ex)
            {
                MessageBox.Show(__ex.Message.ToString());
            }
        }

        private void _processButton_Click(object sender, EventArgs e)
        {
            this._process();
        }
    }
}
