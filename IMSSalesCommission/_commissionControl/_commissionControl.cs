using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace IMSSalesCommission
{
    public partial class _commissionControl : UserControl
    {
        string _getTransTemplate = "";
        int _getTransFlag = 0;
        int _getTransType = 0;
        string _oldDocNo = "";

        // toe for edit
        private string _creator_code = "";
        private DateTime _create_datetime = new DateTime();

        public _commissionControl()
        {
            InitializeComponent();

            this._getTransFlag = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ);
            this._getTransTemplate = _g.g._transGlobalTemplate._transTemplate(_g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ);
            this._getTransType = _g.g._transTypeGlobal._transType(_g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ);

            _myManageData._dataList._lockRecord = true; // ใช้แบบ Lock Record ต้องมี guid_code ด้วยนะ อย่าลืม
            _myManageData._dataList._loadViewFormat(this._getTransTemplate, MyLib._myGlobal._userSearchScreenGroup, true);
            _myManageData._dataList._extraWhereEvent += _dataList__extraWhereEvent;
            //int __columnIndex = this._myManageData._dataList._gridData._findColumnByName(_g.d.coupon_list._table + "." + _g.d.coupon_list._last_status);
            //((MyLib._myGrid._columnType)this._myManageData._dataList._gridData._columnList[__columnIndex])._isColumnFilter = false;

            _myManageData._dataList._referFieldAdd(_g.d.ic_trans._doc_no, 1);
            _myManageData._manageButton = this._myToolBar;
            _myManageData._manageBackgroundPanel = this._myPanel1;

            _myManageData._loadDataToScreen += _myManageData__loadDataToScreen;
            _myManageData._discardData += _myManageData__discardData;
            _myManageData._closeScreen += _myManageData__closeScreen;
            _myManageData._clearData += _myManageData__clearData;
            _myManageData._autoSize = true;
            _myManageData._autoSizeHeight = 500;
            this._myManageData._dataList._buttonDelete.Visible = false;
            this._myManageData._dataList._buttonNew.Visible = false;
            this._myManageData._dataList._buttonNewFromTemp.Visible = false;
            this._myManageData._dataList._buttonSelectAll.Visible = false;

            this._itemGridControl._queryForInsertCheck += _itemGridControl__queryForInsertCheck;
        }

        bool _itemGridControl__queryForInsertCheck(MyLib._myGrid sender, int row)
        {
            string __itemCode = sender._cellGet(row, _g.d.ic_trans_detail._item_code).ToString().Trim();
            string __itemName = "";
            try
            {
                __itemName = sender._cellGet(row, _g.d.ic_trans_detail._item_name).ToString().Trim();
            }
            catch
            {
            }
            return (__itemCode.Length == 0 && __itemName.Length == 0) ? false : true;
        }

        string _dataList__extraWhereEvent()
        {
            string __result = _g.d.ic_trans._trans_flag + "=" + _getTransFlag.ToString() + " and " + _g.d.ic_trans._trans_type + "=" + _getTransType;
            return __result;
        }


        int _getColumnNumberDocNo()
        {
            return this._myManageData._dataList._gridData._findColumnByName(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_no);
        }

        bool _myManageData__loadDataToScreen(object rowData, string whereString, bool forEdit)
        {
            try
            {
                ArrayList __rowDataArray = (ArrayList)rowData;
                this._oldDocNo = __rowDataArray[this._getColumnNumberDocNo()].ToString().ToUpper();

                this._myManageData__clearData();
                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                StringBuilder __query = new StringBuilder();
                __query.Append(MyLib._myGlobal._xmlHeader + "<node>");
                int __tableCount = 0;
                __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.ic_trans._table + " " + whereString + " and " + _g.d.ic_trans._trans_flag + "=" + this._getTransFlag.ToString()));

                __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + MyLib._myGlobal._fieldAndComma(
                    _g.d.ic_trans_detail._item_code,
                    _g.d.ic_trans_detail._item_name,
                    _g.d.ic_trans_detail._unit_code,
                    _g.d.ic_trans_detail._wh_code,
                    _g.d.ic_trans_detail._shelf_code,
                    _g.d.ic_trans_detail._price,
                    _g.d.ic_trans_detail._discount,
                    _g.d.ic_trans_detail._sum_amount,
                    _g.d.ic_trans_detail._sum_amount_exclude_vat) +

                    ", (select " + _g.d.ic_trans_detail_commission._commission +
                    " from " + _g.d.ic_trans_detail_commission._table +
                    " where " + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._doc_no + "=" + _g.d.ic_trans_detail_commission._table + "." + _g.d.ic_trans_detail_commission._doc_no +
                    " and " + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._trans_flag + "=" + _g.d.ic_trans_detail_commission._table + "." + _g.d.ic_trans_detail_commission._trans_flag +
                    " and " + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code + " = " + _g.d.ic_trans_detail_commission._table + "." + _g.d.ic_trans_detail_commission._item_code +
                    " and " + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._line_number + "=" + _g.d.ic_trans_detail_commission._table + "." + _g.d.ic_trans_detail_commission._line_number + ") as " + _g.d.ic_trans_detail_commission._commission +

                    ", (select " + _g.d.ic_trans_detail_commission._commission_amount +
                    " from " + _g.d.ic_trans_detail_commission._table +
                    " where " + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._doc_no + "=" + _g.d.ic_trans_detail_commission._table + "." + _g.d.ic_trans_detail_commission._doc_no +
                    " and " + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._trans_flag + "=" + _g.d.ic_trans_detail_commission._table + "." + _g.d.ic_trans_detail_commission._trans_flag +
                    " and " + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code + " = " + _g.d.ic_trans_detail_commission._table + "." + _g.d.ic_trans_detail_commission._item_code +
                    " and " + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._line_number + "=" + _g.d.ic_trans_detail_commission._table + "." + _g.d.ic_trans_detail_commission._line_number + ") as " + _g.d.ic_trans_detail_commission._commission_amount +

                    ", (select " + _g.d.ic_trans_detail_commission._creator_code +
                    " from " + _g.d.ic_trans_detail_commission._table +
                    " where " + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._doc_no + "=" + _g.d.ic_trans_detail_commission._table + "." + _g.d.ic_trans_detail_commission._doc_no +
                    " and " + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._trans_flag + "=" + _g.d.ic_trans_detail_commission._table + "." + _g.d.ic_trans_detail_commission._trans_flag +
                    " and " + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code + " = " + _g.d.ic_trans_detail_commission._table + "." + _g.d.ic_trans_detail_commission._item_code +
                    " and " + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._line_number + "=" + _g.d.ic_trans_detail_commission._table + "." + _g.d.ic_trans_detail_commission._line_number + ") as " + _g.d.ic_trans_detail_commission._creator_code +

                    ", (select " + _g.d.ic_trans_detail_commission._create_datetime +
                    " from " + _g.d.ic_trans_detail_commission._table +
                    " where " + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._doc_no + "=" + _g.d.ic_trans_detail_commission._table + "." + _g.d.ic_trans_detail_commission._doc_no +
                    " and " + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._trans_flag + "=" + _g.d.ic_trans_detail_commission._table + "." + _g.d.ic_trans_detail_commission._trans_flag +
                    " and " + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code + " = " + _g.d.ic_trans_detail_commission._table + "." + _g.d.ic_trans_detail_commission._item_code +
                    " and " + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._line_number + "=" + _g.d.ic_trans_detail_commission._table + "." + _g.d.ic_trans_detail_commission._line_number + ") as " + _g.d.ic_trans_detail_commission._create_datetime +

                    "," +   _g.d.ic_trans_detail._sum_amount_exclude_vat + " as " + _g.d.ic_trans_detail_commission._commission_base +

                    ", (select " + _g.d.ic_trans_detail_commission._remark +
                    " from " + _g.d.ic_trans_detail_commission._table +
                    " where " + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._doc_no + "=" + _g.d.ic_trans_detail_commission._table + "." + _g.d.ic_trans_detail_commission._doc_no +
                    " and " + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._trans_flag + "=" + _g.d.ic_trans_detail_commission._table + "." + _g.d.ic_trans_detail_commission._trans_flag +
                    " and " + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code + " = " + _g.d.ic_trans_detail_commission._table + "." + _g.d.ic_trans_detail_commission._item_code +
                    " and " + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._line_number + "=" + _g.d.ic_trans_detail_commission._table + "." + _g.d.ic_trans_detail_commission._line_number + ") as " + _g.d.ic_trans_detail_commission._remark +
                    " from " + _g.d.ic_trans_detail._table +
                    " where " + MyLib._myGlobal._addUpper(_g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._doc_no) + "=" + "\'" + this._oldDocNo + "\'" + " and " + _g.d.ic_trans._trans_flag + "=" + this._getTransFlag.ToString() +
                    " order by " + _g.d.ic_trans_detail._line_number));

                __query.Append("</node>");

                ArrayList __result = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __query.ToString());

                DataTable __trans = ((DataSet)__result[0]).Tables[0];
                DataTable __transDetail = ((DataSet)__result[1]).Tables[0];

                this._screenTop._loadData(__trans);
                this._screenBottom._loadData(__trans);

                this._itemGridControl._loadFromDataTable(__transDetail);

                if (__transDetail.Rows.Count > 0)
                {
                    if (__transDetail.Rows[0][_g.d.ic_trans_detail_commission._creator_code].ToString().Length > 0)
                    {
                        this._creator_code = __transDetail.Rows[0][_g.d.ic_trans_detail_commission._creator_code].ToString();
                        this._create_datetime = MyLib._myGlobal._convertDateFromQuery(__transDetail.Rows[0][_g.d.ic_trans_detail_commission._create_datetime].ToString());
                    }
                }

                if (__trans.Rows.Count > 0)
                {
                    string __discountWord = __trans.Rows[0][_g.d.ic_trans._discount_word].ToString();
                    if (__discountWord.IndexOf("%") != -1)
                    {
                        for (int __row = 0; __row < this._itemGridControl._rowData.Count; __row++)
                        {
                            
                            decimal __sumAmountExVat = (decimal)this._itemGridControl._cellGet(__row, _g.d.ic_trans_detail_commission._sum_amount_exclude_vat);
                            decimal __discountDocAmount = MyLib._myGlobal._calcDiscountOnly(__sumAmountExVat, __discountWord, __sumAmountExVat, _g.g._companyProfile._item_amount_decimal)._discountAmount;
                            decimal __lastAmount = MyLib._myGlobal._calcAfterDiscount(__discountWord, __sumAmountExVat, _g.g._companyProfile._item_amount_decimal);
                            decimal __discountAmount = __sumAmountExVat - __lastAmount;

                            this._itemGridControl._cellUpdate(__row, _g.d.ic_trans_detail_commission._discount_doc_word, __discountWord, true);
                            this._itemGridControl._cellUpdate(__row, _g.d.ic_trans_detail_commission._discount_doc_amount, __discountAmount, true);

                            this._itemGridControl._cellUpdate(__row, _g.d.ic_trans_detail_commission._commission_base, __lastAmount, false);
                        }
                    }

                }


                return (true);
            }
            catch (Exception __ex)
            {
                MessageBox.Show(__ex.Message.ToString());
            }
            return (false);
        }

        void _myManageData__clearData()
        {
            this._screenTop._clear();
            this._screenBottom._clear();
            this._itemGridControl._clear();
        }

        bool _myManageData__discardData()
        {
            return true;
        }

        void _myManageData__closeScreen()
        {
            this.Dispose();
        }

        private void _saveButton_Click(object sender, EventArgs e)
        {
            _saveData();
        }

        void _saveData()
        {
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();

            StringBuilder __query = new StringBuilder(MyLib._myGlobal._xmlHeader + "<node>");

            // delete old 
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.ic_trans_detail_commission._table + " where " + _g.d.ic_trans_detail_commission._doc_no + "=\'" + this._oldDocNo + "\' and " + _g.d.ic_trans_detail_commission._trans_flag + " =" + this._getTransFlag.ToString()));


            this._itemGridControl._removeLastControl();
            this._itemGridControl._updateRowIsChangeAll(true);
            string __field = "";
            string __value = "";
            if (this._creator_code.Length == 0)
            {
                __field = MyLib._myGlobal._fieldAndComma(_g.d.ic_trans_detail_commission._doc_no, _g.d.ic_trans_detail_commission._trans_flag, _g.d.ic_trans_detail_commission._creator_code, _g.d.ic_trans_detail_commission._create_datetime) + ",";
                __value = MyLib._myGlobal._fieldAndComma("\'" + this._oldDocNo + "\'", this._getTransFlag.ToString(), "\'" + MyLib._myGlobal._userCode + "\'", "\'" + MyLib._myGlobal._convertDateTimeToQuery(DateTime.Now) + "\'") + ",";
            }
            else
            {
                __field = MyLib._myGlobal._fieldAndComma(_g.d.ic_trans_detail_commission._doc_no, _g.d.ic_trans_detail_commission._trans_flag, _g.d.ic_trans_detail_commission._creator_code, _g.d.ic_trans_detail_commission._create_datetime, _g.d.ic_trans_detail_commission._last_editor_code, _g.d.ic_trans_detail_commission._lastedit_datetime) + ",";
                __value = MyLib._myGlobal._fieldAndComma("\'" + this._oldDocNo + "\'", this._getTransFlag.ToString(), "\'" + this._creator_code + "\'", "\'" + MyLib._myGlobal._convertDateTimeToQuery(this._create_datetime) + "\'", "\'" + MyLib._myGlobal._userCode + "\'", "\'" + MyLib._myGlobal._convertDateTimeToQuery(DateTime.Now) + "\'") + ",";
            }
            __query.Append(this._itemGridControl._createQueryForInsert(_g.d.ic_trans_detail_commission._table, __field, __value, false, true));

            __query.Append("</node>");

            string __result = __myFrameWork._queryList(MyLib._myGlobal._databaseName, __query.ToString());
            if (__result.Length == 0)
            {
                MyLib._myGlobal._displayWarning(1, null);
                this._myManageData._dataList._refreshData();
                this._myManageData._afterUpdateData();
            }
            else
            {
                MessageBox.Show(__result, "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void _closeButtom_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }

}
