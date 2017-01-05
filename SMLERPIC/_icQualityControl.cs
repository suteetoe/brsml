using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace SMLERPIC
{
    public partial class _icQualityControl : UserControl
    {
        object[] __comboboxStatusItem = new object[] { "Quarantine", "Pass", "Reject", "InProcess" };
        string _lastDocNo = "";
        int _lastDocType = -1;

        public _icQualityControl()
        {
            InitializeComponent();

            this._myManageData._displayMode = 0;
            this._myManageData._dataList._lockRecord = true;
            //this._myManageData._selectDisplayMode(this._myManageData._displayMode);
            this._myManageData._dataList._loadViewFormat(_g.g._search_screen_purchase, MyLib._myGlobal._userSearchScreenGroup, true);
            this._myManageData._dataList._gridData._addColumn(_g.d.ic_trans._trans_flag, 2, 0, 0, false, true);
            this._myManageData._dataList._referFieldAdd(_g.d.ic_trans._doc_no, 1);
            this._myManageData._dataList._extraWhere = _g.d.ic_trans._trans_flag + " in (" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_ซื้อสินค้าและค่าบริการ) + "," + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ซื้อ_พาเชียล_รับสินค้า) + ")";
            this._myManageData._loadDataToScreen += _myManageData__loadDataToScreen;
            this._myManageData._manageButton = this._myToolStrip1;
            this._myManageData._manageBackgroundPanel = this._myPanel;
            //this._myManageData._discardData += new MyLib.DiscardDataEvent(_myManageData__discardData);
            this._myManageData._clearData += _myManageData__clearData;
            this._myManageData._closeScreen += _myManageData__closeScreen;
            //this._myManageData._checkEditData += new MyLib.CheckEditDataEvent(_myManageData__checkEditData);
            this._myManageData._calcArea();
            this._myManageData._dataListOpen = true;
            this._myManageData._autoSize = true;
            this._myManageData._autoSizeHeight = 450;
            this._myManageData._dataList._buttonNew.Visible = false;
            this._myManageData._dataList._buttonNewFromTemp.Visible = false;
            this._myManageData._dataList._buttonDelete.Visible = false;
            this._myManageData._dataList._buttonSelectAll.Visible = false;
            this._myManageData.Invalidate();

            // fix screen
            this._myScreen._table_name = _g.d.ic_trans._table;
            this._myScreen._addTextBox(0, 0, _g.d.ic_trans._doc_no, 25);
            this._myScreen._addDateBox(1, 0, 1, 1, _g.d.ic_trans._doc_date, 1, true);

            this._myGrid._table_name = _g.d.ic_quality_control._table;
            this._myGrid._addColumn(_g.d.ic_quality_control._ic_qc_status, 10, 15, 15, true);
            this._myGrid._addColumn(_g.d.ic_quality_control._ic_code, 1, 15, 15, false, false);
            this._myGrid._addColumn(_g.d.ic_quality_control._ic_name, 1, 20, 20, false, false, false);
            this._myGrid._addColumn(_g.d.ic_quality_control._lot_number, 1, 15, 15, false, false);
            this._myGrid._addColumn(_g.d.ic_quality_control._mfn, 1, 15, 15, false, false, true);
            this._myGrid._addColumn(_g.d.ic_quality_control._mfd_date, 4, 15, 15, false, false, true);
            this._myGrid._addColumn(_g.d.ic_quality_control._exp_date, 4, 15, 15, false, false);
            this._myGrid._addColumn(_g.d.ic_quality_control._lab_no, 1, 10, 10);
            this._myGrid._addColumn(_g.d.ic_quality_control._lab_retest_date, 4, 10, 10);
            //this._myGrid._beforeDisplayRendering += _myGrid__beforeDisplayRendering;
            this._myGrid._cellComboBoxItem += _myGrid__cellComboBoxItem;
            this._myGrid._cellComboBoxGet += _myGrid__cellComboBoxGet;
            this._myGrid._addRowEnabled = false;
            this._myGrid._calcPersentWidthToScatter();
        }

        void _myManageData__clearData()
        {
            _clear();
            _lastDocNo = "";
            _lastDocType = -1;
        }

        void _clear()
        {
            this._myScreen._clear();
            this._myGrid._clear();
        }

        void _myManageData__closeScreen()
        {
            this.Dispose();
        }

        string _myGrid__cellComboBoxGet(object sender, int row, int column, string columnName, int select)
        {
            return __comboboxStatusItem[(select == -1) ? 0 : select].ToString();
        }

        object[] _myGrid__cellComboBoxItem(object sender, int row, int column)
        {
            return __comboboxStatusItem;
        }

        MyLib.BeforeDisplayRowReturn _myGrid__beforeDisplayRendering(MyLib._myGrid sender, int row, int columnNumber, string columnName, MyLib.BeforeDisplayRowReturn senderRow, MyLib._myGrid._columnType columnType, ArrayList rowData, Graphics e)
        {
            MyLib.BeforeDisplayRowReturn __result = senderRow;
            if (row < sender._rowData.Count && columnName.Equals(_g.d.ic_quality_control._table + "." + _g.d.ic_quality_control._ic_qc_status))
            {
                int __mode = (int)MyLib._myGlobal._decimalPhase(sender._cellGet(row, sender._findColumnByName(_g.d.ic_quality_control._ic_qc_status)).ToString());

                ((ArrayList)senderRow.newData)[columnNumber] = "";

                switch (__mode)
                {
                    case 1:
                        __result.newColor = Color.Red;
                        ((ArrayList)senderRow.newData)[columnNumber] = "ส่งออกจาก Data Center";
                        break;
                    case 2:
                        __result.newColor = Color.Blue;
                        ((ArrayList)senderRow.newData)[columnNumber] = "รับข้อมูลเข้า Data Center";
                        break;
                    case 3:
                        __result.newColor = Color.Green;
                        ((ArrayList)senderRow.newData)[columnNumber] = "แลกเปลี่ยนข้อมูลกับ Data Center";
                        break;
                }

            }
            return __result;
        }

        bool _myManageData__loadDataToScreen(object rowData, string whereString, bool forEdit)
        {
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();

            try
            {

                int __getTransFlagColumnNumber = this._myManageData._dataList._gridData._findColumnByName(_g.d.ic_trans._trans_flag);
                int __getDocNoColumnNumber = this._myManageData._dataList._gridData._findColumnByName(_g.d.ic_trans._table + "." + _g.d.ic_trans._doc_no);
                int __transFlag = MyLib._myGlobal._intPhase(((ArrayList)rowData)[__getTransFlagColumnNumber].ToString());
                string __getDocNo = ((ArrayList)rowData)[__getDocNoColumnNumber].ToString();
                StringBuilder __query = new StringBuilder(MyLib._myGlobal._xmlHeader + "<node>");
                __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ic_trans._doc_no + "," + _g.d.ic_trans._doc_date + " from " + _g.d.ic_trans._table + " " + whereString + " and " + _g.d.ic_trans._trans_flag + " = " + __transFlag.ToString()));

                string __field = MyLib._myGlobal._fieldAndComma(_g.d.ic_trans_detail._item_code + " as " + _g.d.ic_quality_control._ic_code
                    , _g.d.ic_trans_detail._item_name + " as " + _g.d.ic_quality_control._ic_name
                    , _g.d.ic_trans_detail._lot_number_1 + " as " + _g.d.ic_quality_control._lot_number
                    , _g.d.ic_trans_detail._date_expire + " as " + _g.d.ic_quality_control._exp_date
                    , _g.d.ic_trans_detail._mfn_name + " as " + _g.d.ic_quality_control._mfn
                    , " ( select " + _g.d.ic_quality_control._ic_qc_status + " from " + _g.d.ic_quality_control._table + " where " + _g.d.ic_quality_control._table + "." + _g.d.ic_quality_control._doc_no + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._doc_no + " and  " + _g.d.ic_quality_control._table + "." + _g.d.ic_quality_control._trans_flag + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._trans_flag + " and  " + _g.d.ic_quality_control._table + "." + _g.d.ic_quality_control._ic_code + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code + " and  " + _g.d.ic_quality_control._table + "." + _g.d.ic_quality_control._line_number + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._line_number + " ) as " + _g.d.ic_quality_control._ic_qc_status
                    , _g.d.ic_trans_detail._mfd_date + " as " + _g.d.ic_quality_control._mfd_date
                    //, " ( select " + _g.d.ic_quality_control._mfd_date + " from " + _g.d.ic_quality_control._table + " where " + _g.d.ic_quality_control._table + "." + _g.d.ic_quality_control._doc_no + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._doc_no + " and  " + _g.d.ic_quality_control._table + "." + _g.d.ic_quality_control._trans_flag + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._trans_flag + " ) as " + _g.d.ic_quality_control._mfd_date
                    , " ( select " + _g.d.ic_quality_control._lab_no + " from " + _g.d.ic_quality_control._table + " where " + _g.d.ic_quality_control._table + "." + _g.d.ic_quality_control._doc_no + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._doc_no + " and  " + _g.d.ic_quality_control._table + "." + _g.d.ic_quality_control._trans_flag + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._trans_flag + "   and  " + _g.d.ic_quality_control._table + "." + _g.d.ic_quality_control._ic_code + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code + " and  " + _g.d.ic_quality_control._table + "." + _g.d.ic_quality_control._line_number + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._line_number + ") as " + _g.d.ic_quality_control._lab_no
                    , " ( select " + _g.d.ic_quality_control._lab_retest_date + " from " + _g.d.ic_quality_control._table + " where " + _g.d.ic_quality_control._table + "." + _g.d.ic_quality_control._doc_no + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._doc_no + " and  " + _g.d.ic_quality_control._table + "." + _g.d.ic_quality_control._trans_flag + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._trans_flag + "  and  " + _g.d.ic_quality_control._table + "." + _g.d.ic_quality_control._ic_code + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._item_code + "  and  " + _g.d.ic_quality_control._table + "." + _g.d.ic_quality_control._line_number + "=" + _g.d.ic_trans_detail._table + "." + _g.d.ic_trans_detail._line_number + " ) as " + _g.d.ic_quality_control._lab_retest_date);
                __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + __field + " from " + _g.d.ic_trans_detail._table + " where " + _g.d.ic_trans_detail._doc_no + "=\'" + __getDocNo + "\' and " + _g.d.ic_trans_detail._trans_flag + " = " + __transFlag.ToString() + " order by line_number "));
                __query.Append("</node>");

                ArrayList __result = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __query.ToString());
                if (__result.Count > 0)
                {
                    DataSet __trans = (DataSet)__result[0];
                    DataSet __transDetail = (DataSet)__result[1];
                    if (__trans.Tables.Count > 0)
                    {
                        this._myScreen._loadData(__trans.Tables[0]);
                    }

                    if (__transDetail.Tables.Count > 0)
                    {
                        this._myGrid._loadFromDataTable(__transDetail.Tables[0]);
                    }

                    this._lastDocNo = __getDocNo;
                    this._lastDocType = __transFlag;
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

            return false;
        }

        private void _closeButton_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void _printButton_Click(object sender, EventArgs e)
        {
            _printFormData("IQC");
        }

        private void _printFormData(string docTypeCode)
        {
            /*
            MyLib._myFrameWork __myFramework = new MyLib._myFrameWork();
            // check 
            bool __printForm = false;
            //if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLColorStore)
            //{
            //    string __query = "select " + _g.d.erp_doc_format._form_code + " from " + _g.d.erp_doc_format._table + " where " + MyLib._myGlobal._addUpper(_g.d.erp_doc_format._code) + "=\'" + this._icTransScreenTop._docFormatCode + "\'";
            //    DataTable __result = __myFramework._queryShort(__query).Tables[0];
            //    if (__result.Rows.Count > 0)
            //    {
            //        if (__result.Rows[0][_g.d.erp_doc_format._form_code].ToString().Length > 0)
            //        {
            //            __printForm = true;
            //            docTypeCode = this._icTransScreenTop._docFormatCode;
            //        }
            //    }
            //}

            string __docNo = this._myScreen._getDataStr(_g.d.ic_trans._doc_no).ToString().Trim();
            bool __printResult = SMLERPReportTool._global._printForm(docTypeCode, __docNo, this._lastDocType.ToString(), true);
            if (__printResult == true)
            {
                // update print count
                // MyLib._myFrameWork __myFramework = new MyLib._myFrameWork();
                __myFramework._queryInsertOrUpdate(MyLib._myGlobal._databaseName, "insert into " + _g.d.erp_print_logs._table + "(" + _g.d.erp_print_logs._doc_no + "," + _g.d.erp_print_logs._trans_flag + "," + _g.d.erp_print_logs._user_code + "," + _g.d.erp_print_logs._print_datetime + ") values (\'" + __docNo + "\'," + this._lastDocType.ToString() + ",\'" + MyLib._myGlobal._userCode + "\', \'" + MyLib._myGlobal._convertDateTimeToQuery(DateTime.Now) + "\')");
            }*/
            string __docNo = this._myScreen._getDataStr(_g.d.ic_trans._doc_no).ToString().Trim();

            _icQualityLabelPrint __print = new _icQualityLabelPrint(__docNo, this._lastDocType.ToString());
            __print.StartPosition = FormStartPosition.CenterScreen;
            __print.ShowDialog();

        }


        private void _saveButton_Click(object sender, EventArgs e)
        {
            _saveData();
        }

        void _saveData()
        {
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();

            try
            {
                StringBuilder __query = new StringBuilder(MyLib._myGlobal._xmlHeader + "<node>");

                __query.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.ic_quality_control._table + " where " + _g.d.ic_quality_control._doc_no + " =\'" + this._lastDocNo + "\' and " + _g.d.ic_quality_control._trans_flag + "=" + _lastDocType));

                // grid
                ArrayList _queryScreen = this._myScreen._createQueryForDatabase();
                string __masterField = _queryScreen[0].ToString() + "," + _g.d.ic_quality_control._trans_flag + ",";
                string __masterValue = _queryScreen[1].ToString() + "," + _lastDocType + ",";
                this._myGrid._updateRowIsChangeAll(true);
                __query.Append(this._myGrid._createQueryForInsert(_g.d.ic_quality_control._table, __masterField, __masterValue, false, true));

                __query.Append("</node>");
                string __result = __myFrameWork._queryList(MyLib._myGlobal._databaseName, __query.ToString());
                if (__result.Length == 0)
                {
                    MyLib._myGlobal._displayWarning(1, null);
                    _printFormData("IQC");

                    this._clear();
                    //
                    this._myManageData._afterUpdateData();
                }
                else
                {
                    MessageBox.Show(__result);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F12)
            {
                if (this._myToolStrip1.Enabled == true)
                {
                    _saveData();
                    return true;
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
