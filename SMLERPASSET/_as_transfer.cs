using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace SMLERPASSET
{
    public partial class _as_transfer : UserControl
    {
        private MyLib._myPanel _myPanelGl;
        private System.Windows.Forms.TabPage _tab_gl;
        private SMLERPGLControl._journalScreen _glScreenTop;
        private SMLERPGLControl._glDetail _glDetail;

        string _oldDocNo = "";

        public _as_transfer()
        {
            InitializeComponent();
            if (MyLib._myGlobal._isDesignMode == false)
            {
                this._detailScreen._myToolBar.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            }

            // Manage Data
            this._myManageData1._closeScreen += new MyLib.CloseScreenEvent(_myManageData1__closeScreen);
            //_myManageData1._selectDisplayMode(this._myManageData1._displayMode);
            _myManageData1._dataList._lockRecord = true; // ใช้แบบ Lock Record ต้องมี guid_code ด้วยนะ อย่าลืม
            _myManageData1._dataList._loadViewFormat("screen_asset_trans", MyLib._myGlobal._userSearchScreenGroup, true);
            _myManageData1._dataList._referFieldAdd(_g.d.as_trans._doc_no, 1);

            _myManageData1._manageButton = this._detailScreen._myToolBar;

            //_myManageData1._manageBackgroundPanel = this._myPanel1;
            _myManageData1._loadDataToScreen += _myManageData1__loadDataToScreen;
            //_myManageData1._discardData += new MyLib.DiscardDataEvent(_myManageData1__discardData);
            _myManageData1._closeScreen += new MyLib.CloseScreenEvent(_myManageData1__closeScreen);
            _myManageData1._newDataClick += _myManageData1__newDataClick;
            _myManageData1._clearData += _myManageData1__clearData;
            _myManageData1._dataList._deleteData += _dataList__deleteData;
            this._detailScreen._screenTop._saveKeyDown += _screenTop__saveKeyDown;

            _myManageData1._dataListOpen = true;
            _myManageData1._calcArea();
            //_myManageData1._dataList._loadViewData(0);
            _myManageData1._autoSize = true;
            _myManageData1._autoSizeHeight = 350;

            // screen
            this._detailScreen._buttonSave.Click += _buttonSave_Click;
            this._detailScreen._buttonProcess.Click += _buttonProcess_Click;
            this._detailScreen._screenTop._textBoxChanged += _screenTop__textBoxChanged;

            _glCreate();
            this._detailScreen._tab._getResource();
            _myManageData1__newDataClick();
        }

        private void _screenTop__textBoxChanged(object sender, string name)
        {

            if (name.Equals(_g.d.as_trans._doc_format_code))
            {
                // find book
                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                DataTable __docFormat = __myFrameWork._queryShort("select " + _g.d.erp_doc_format._gl_book + " from " + _g.d.erp_doc_format._table + " where " + _g.d.erp_doc_format._code + " =\'" + this._detailScreen._screenTop._getDataStr(_g.d.as_trans._doc_format_code) + "\' ").Tables[0];
                if (__docFormat.Rows.Count > 0)
                {
                    this._glScreenTop._setDataStr(_g.d.gl_journal._book_code, __docFormat.Rows[0][0].ToString());
                }
            }

            this._glScreenTop._setDataDate(_g.d.gl_journal._doc_date, MyLib._myGlobal._convertDate(this._detailScreen._screenTop._getDataStr(_g.d.as_trans._doc_date)));
            this._glScreenTop._setDataStr(_g.d.gl_journal._doc_no, this._detailScreen._screenTop._getDataStr(_g.d.as_trans._doc_no));

            this._glScreenTop._setDataStr(_g.d.gl_journal._doc_format_code, this._detailScreen._screenTop._getDataStr(_g.d.as_trans._doc_format_code));
            this._glScreenTop._setDataStr(_g.d.gl_journal._description, this._detailScreen._screenTop._getDataStr(_g.d.as_trans._remark));

            //this._glScreenTop._enabedControl(_g.d.gl_journal._ref_date, true);
            //this._glScreenTop._enabedControl(_g.d.gl_journal._ref_no, true);

            //this._glScreenTop._enabedControl(_g.d.gl_journal._book_code, false);
            //this._glScreenTop._enabedControl(_g.d.gl_journal._journal_type, false);
            //this._glScreenTop._enabedControl(_g.d.gl_journal._description, false);

            _g.g._accountPeriodClass __accountPeriod = _g.g._accountPeriodClassFind(this._detailScreen._screenTop._getDataDate(_g.d.ic_trans._doc_date));
            if (__accountPeriod != null)
            {
                this._glScreenTop._setDataStr(_g.d.gl_journal._period_number, __accountPeriod._number.ToString());
                this._glScreenTop._setDataStr(_g.d.gl_journal._account_year, __accountPeriod._year.ToString());
            }

            this._glScreenTop.Invalidate();
        }

        private void _buttonSave_Click(object sender, EventArgs e)
        {
            _saveData();
        }

        private string _glDeleteQuery(string docNo)
        {
            StringBuilder __myQuery = new StringBuilder();
            __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.gl_journal._table + " where " + _g.d.gl_journal._doc_no + "=\'" + docNo + "\'"));
            __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.gl_journal_detail._table + " where " + _g.d.gl_journal_detail._doc_no + "=\'" + docNo + "\'"));
            if (this._glDetail != null)
            {
                __myQuery.Append(this._glDetail._deleteGlExtraList(docNo));
            }
            return __myQuery.ToString();
        }


        void _saveData()
        {
            if (this._detailScreen._myToolBar.Enabled)
            {
                if (this._myManageData1._mode == 1 && this._myManageData1._isAdd == false)
                {
                    MessageBox.Show(MyLib._myGlobal._resource("warning19"), MyLib._myGlobal._resource("warning"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string __emptyField = this._detailScreen._screenTop._checkEmtryField();
                string __emptyGl = this._glScreenTop._checkEmtryField();

                if (__emptyField.Length > 0 || __emptyGl.Length > 0)
                {
                    MessageBox.Show("กรุณาป้อนข้อมูล \n" + __emptyField.ToString() + ((__emptyField.Length > 0) ? "\n" : "") + __emptyGl.ToString(), "warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string __docNo = this._detailScreen._screenTop._getDataStr(_g.d.as_trans._doc_no);
                StringBuilder __myQuery = new StringBuilder(MyLib._myGlobal._xmlHeader + "<node>");

                if (this._myManageData1._mode == 2)
                {
                    // delete first
                    __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.as_trans._table + " where " + _g.d.as_trans._doc_no + " = \'" + this._oldDocNo + "\' "));
                    __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.as_trans_detail._table + " where " + _g.d.as_trans_detail._doc_no + " = \'" + this._oldDocNo + "\' "));

                    __myQuery.Append(this._glDeleteQuery(this._oldDocNo));

                    // _query.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from ic_trans where doc_no = \'" + _docNo + "\' and " + _g.d.ic_trans._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ลูกหนี้_ลดแต้ม).ToString() + " "));
                }

                ArrayList __fieldList = this._detailScreen._screenTop._createQueryForDatabase();
                ArrayList __conditionList = this._detailScreen._asConditionScreen1._createQueryForDatabase();

                __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _g.d.as_trans._table + "(" + __fieldList[0].ToString() + "," + __conditionList[0].ToString() + "," + _g.d.as_trans._trans_flag + ") values(" + __fieldList[1].ToString() + "," + __conditionList[1].ToString() + ", " + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.สินทรัพย์_โอนค่าเสื่อม).ToString() + ")"));

                // detail
                this._detailScreen._gridAssetDetail._updateRowIsChangeAll(true);
                string __fieldListDetail = _g.d.as_trans_detail._doc_no + "," + _g.d.as_trans_detail._doc_date + "," + _g.d.as_trans_detail._doc_time + "," + _g.d.as_trans_detail._trans_flag + ",";
                string __valueListDetail = "\'" + __docNo + "\'," + this._detailScreen._screenTop._getDataStrQuery(_g.d.as_trans._doc_date) + ",\'" + this._detailScreen._screenTop._getDataStr(_g.d.as_trans._doc_time) + "\', " + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.สินทรัพย์_โอนค่าเสื่อม).ToString() + ",";

                __myQuery.Append(this._detailScreen._gridAssetDetail._createQueryForInsert(_g.d.as_trans_detail._table, __fieldListDetail, __valueListDetail, false, true));

                //if (__glManual)
                {
                    Control __getControl = this._glScreenTop._getControl(_g.d.gl_journal._doc_date);
                    DateTime __getDate = ((MyLib._myDateBox)__getControl)._dateTime;
                    int __periodNumber = _g.g._accountPeriodFind(__getDate);
                    int __getColumnNumberDebit = this._glDetail._glDetailGrid._findColumnByName(_g.d.gl_journal_detail._debit);
                    int __getColumnNumberCredit = this._glDetail._glDetailGrid._findColumnByName(_g.d.gl_journal_detail._credit);
                    ArrayList __getData = this._glScreenTop._createQueryForDatabase();
                    string __extData = ((MyLib._myGrid._columnType)this._glDetail._glDetailGrid._columnList[__getColumnNumberDebit])._total.ToString() + "," +
                        ((MyLib._myGrid._columnType)this._glDetail._glDetailGrid._columnList[__getColumnNumberCredit])._total.ToString() + "," + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.สินทรัพย์_โอนค่าเสื่อม).ToString(); // +"," + __periodNumber.ToString();
                    string __extField = _g.d.gl_journal._debit + "," + _g.d.gl_journal._credit + "," + _g.d.gl_journal._trans_flag;// +"," + _g.d.gl_journal._period_number;
                                                                                                                                   // head
                    __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _g.d.gl_journal._table + " (" + __getData[0].ToString() + "," + __extField + ") values (" + __getData[1].ToString() + "," + __extData + ")"));
                    // detail
                    string __fieldListGl = _g.d.gl_journal._doc_date + "," + _g.d.gl_journal._book_code + "," + _g.d.gl_journal._doc_no + "," + _g.d.gl_journal._period_number + "," + _g.d.gl_journal._journal_type + "," + _g.d.gl_journal._trans_flag + ",";
                    string __dataListGl = this._glScreenTop._getDataStrQuery(_g.d.gl_journal._doc_date) + "," + this._glScreenTop._getDataStrQuery(_g.d.gl_journal._book_code) + "," + this._glScreenTop._getDataStrQuery(_g.d.gl_journal._doc_no) + "," + __periodNumber.ToString() + "," + this._glScreenTop._getDataStr(_g.d.gl_journal._journal_type) + "," + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.สินทรัพย์_โอนค่าเสื่อม).ToString() + ",";
                    this._glDetail._glDetailGrid._updateRowIsChangeAll(true);
                    __myQuery.Append(this._glDetail._glDetailGrid._createQueryForInsert(_g.d.gl_journal_detail._table, __fieldListGl, __dataListGl));
                    // Gl Extra
                    __myQuery.Append(this._glDetail._saveGlExtraListQuery(this._glDetail._glDetailGrid, __fieldListGl, __dataListGl));
                }

                __myQuery.Append("</node>");

                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                string result = __myFrameWork._queryList(MyLib._myGlobal._databaseName, __myQuery.ToString());

                if (result.Length == 0)
                {
                    MyLib._myGlobal._displayWarning(1, null);
                    if (_myManageData1._mode == 1)
                    {
                        _myManageData1._afterInsertData();
                    }
                    else
                    {
                        _myManageData1._afterUpdateData();
                    }
                    this._myManageData1._dataList._refreshData();
                    this._myManageData1__clearData();
                    this._detailScreen._screenTop._focusFirst();
                }
                else
                {
                    MessageBox.Show(result, "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void _buttonProcess_Click(object sender, EventArgs e)
        {
            this._detailScreen._gridAssetDetail._clear();
            this._glDetail._glDetailGrid._clear();

            SMLProcess._asProcess __process = new SMLProcess._asProcess();
            DateTime __getDateBegin = MyLib._myGlobal._convertDate(this._detailScreen._asConditionScreen1._getDataStr(_g.d.as_resource._date_begin));
            DateTime __getDateEnd = MyLib._myGlobal._convertDate(this._detailScreen._asConditionScreen1._getDataStr(_g.d.as_resource._date_end));

            ArrayList __result = __process._getAssetDepreciateProcess(__getDateBegin, __getDateEnd, 2, 1);
            if (__result.Count > 0)
            {
                // เอาลงตาราง
                DataSet __getAssets = (DataSet)__result[0];


                int __columnAssetCode = __getAssets.Tables[0].Columns.IndexOf("as_code");
                int __columnAssetName = __getAssets.Tables[0].Columns.IndexOf("as_name");
                int __columnAssetResult = __getAssets.Tables[0].Columns.IndexOf("as_result");

                int __columnAssetDepreciation = __getAssets.Tables[0].Columns.IndexOf(_g.d.as_asset._depreciation_account_code);
                int __columnAssetDepreciationSum = __getAssets.Tables[0].Columns.IndexOf(_g.d.as_asset._depreciation_sum_account_code);

                for (int __row = 0; __row < __getAssets.Tables[0].Rows.Count; __row++)
                {
                    int __newRow = this._detailScreen._gridAssetDetail._addRow();
                    decimal _dResult = (decimal)Double.Parse(__getAssets.Tables[0].Rows[__row].ItemArray[__columnAssetResult].ToString());

                    this._detailScreen._gridAssetDetail._cellUpdate(__newRow, _g.d.as_trans_detail._item_code, __getAssets.Tables[0].Rows[__row].ItemArray[__columnAssetCode].ToString(), false);
                    this._detailScreen._gridAssetDetail._cellUpdate(__newRow, _g.d.as_trans_detail._item_name, __getAssets.Tables[0].Rows[__row].ItemArray[__columnAssetName].ToString(), false);
                    this._detailScreen._gridAssetDetail._cellUpdate(__newRow, _g.d.as_trans_detail._sum_amount, _dResult, false);

                    this._detailScreen._gridAssetDetail._cellUpdate(__newRow, _g.d.as_asset._depreciation_account_code, __getAssets.Tables[0].Rows[__row].ItemArray[__columnAssetDepreciation].ToString(), false);
                    this._detailScreen._gridAssetDetail._cellUpdate(__newRow, _g.d.as_asset._depreciation_sum_account_code, __getAssets.Tables[0].Rows[__row].ItemArray[__columnAssetDepreciationSum].ToString(), false);

                }

                // ใส่ผังบัญชี
                if (this._detailScreen._gridAssetDetail._rowData.Count > 0)
                {

                    /* ช้าไป
                    StringBuilder __asCodeList = new StringBuilder();
                    for (int __row = 0; __row < this._detailScreen._gridAssetDetail._rowData.Count; __row++ )
                    {
                        if (__asCodeList.Length > 0)
                        {
                            __asCodeList.Append(",");
                        }

                        __asCodeList.Append("\'" + this._detailScreen._gridAssetDetail._cellGet(__row, _g.d.as_trans_detail._item_code).ToString() + "\'");
                    }

                    // get data
                    string __query = "select " + _g.d.as_asset._code + "," + _g.d.as_asset._depreciation_account_code + "," + _g.d.as_asset._depreciation_sum_account_code + "  from " + _g.d.as_asset._table + " where " + _g.d.as_asset._code + " in (" + __asCodeList.ToString() + ")";

                    MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                    DataTable __assetDepreciation = __myFrameWork._queryShort(__query).Tables[0];

                    if (__assetDepreciation.Rows.Count > 0)
                    {


                        for (int __row = 0; __row < this._detailScreen._gridAssetDetail._rowData.Count; __row++)
                        {
                            string __asCode = this._detailScreen._gridAssetDetail._cellGet(__row, _g.d.as_trans_detail._item_code).ToString();

                            if (__asCode.Length > 0)
                            {
                                DataRow[] __select = __assetDepreciation.Select(_g.d.as_asset._code + "=\'" + __asCode + "\'");
                                if (__select.Length > 0)
                                {
                                    this._detailScreen._gridAssetDetail._cellUpdate(__row, _g.d.as_asset._depreciation_account_code, __select[0][_g.d.as_asset._depreciation_account_code].ToString(), false);
                                    this._detailScreen._gridAssetDetail._cellUpdate(__row, _g.d.as_asset._depreciation_sum_account_code, __select[0][_g.d.as_asset._depreciation_sum_account_code].ToString(), false);
                                }
                            }

                        }
                    }*/

                    // dr
                    for (int __row = 0; __row < this._detailScreen._gridAssetDetail._rowData.Count; __row++)
                    {
                        string __accountCodeDepreciation = this._detailScreen._gridAssetDetail._cellGet(__row, _g.d.as_asset._depreciation_account_code).ToString();
                        if (__accountCodeDepreciation.Length > 0)
                        {
                            decimal __sumAmount = (decimal)this._detailScreen._gridAssetDetail._cellGet(__row, _g.d.as_trans_detail._sum_amount);
                            int __findRow = this._glDetail._glDetailGrid._findData(this._glDetail._glDetailGrid._findColumnByName(_g.d.gl_journal_detail._account_code), __accountCodeDepreciation);
                            if (__findRow == -1)
                            {
                                int __rowAdd = this._glDetail._glDetailGrid._addRow();
                                this._glDetail._glDetailGrid._cellUpdate(__rowAdd, _g.d.gl_journal_detail._account_code, __accountCodeDepreciation, true);
                                this._glDetail._glDetailGrid._cellUpdate(__rowAdd, _g.d.gl_journal_detail._debit, __sumAmount, true);
                            }
                            else
                            {
                                decimal __oldAmount = (decimal)this._glDetail._glDetailGrid._cellGet(__findRow, _g.d.gl_journal_detail._debit);
                                this._glDetail._glDetailGrid._cellUpdate(__findRow, _g.d.gl_journal_detail._debit, __sumAmount + __oldAmount, true);
                            }
                        }
                    }

                    // cr
                    for (int __row = 0; __row < this._detailScreen._gridAssetDetail._rowData.Count; __row++)
                    {
                        string __accountCodeDepreciationSum = this._detailScreen._gridAssetDetail._cellGet(__row, _g.d.as_asset._depreciation_sum_account_code).ToString();
                        if (__accountCodeDepreciationSum.Length > 0)
                        {
                            decimal __sumAmount = (decimal)this._detailScreen._gridAssetDetail._cellGet(__row, _g.d.as_trans_detail._sum_amount);
                            int __findRow = this._glDetail._glDetailGrid._findData(this._glDetail._glDetailGrid._findColumnByName(_g.d.gl_journal_detail._account_code), __accountCodeDepreciationSum);
                            if (__findRow == -1)
                            {
                                int __rowAdd = this._glDetail._glDetailGrid._addRow();
                                this._glDetail._glDetailGrid._cellUpdate(__rowAdd, _g.d.gl_journal_detail._account_code, __accountCodeDepreciationSum, true);
                                this._glDetail._glDetailGrid._cellUpdate(__rowAdd, _g.d.gl_journal_detail._credit, __sumAmount, true);
                            }
                            else
                            {
                                decimal __oldAmount = (decimal)this._glDetail._glDetailGrid._cellGet(__findRow, _g.d.gl_journal_detail._credit);
                                this._glDetail._glDetailGrid._cellUpdate(__findRow, _g.d.gl_journal_detail._credit, __sumAmount + __oldAmount, true);
                            }
                        }
                    }

                }

                this._detailScreen._gridAssetDetail.Invalidate();


            }
        }

        private void _screenTop__saveKeyDown(object sender)
        {
            _saveData();
        }

        private void _dataList__deleteData(System.Collections.ArrayList selectRowOrder)
        {
            //throw new NotImplementedException();
            StringBuilder __myQuery = new StringBuilder();
            __myQuery.Append(MyLib._myGlobal._xmlHeader + "<node>");
            for (int __loop = 0; __loop < selectRowOrder.Count; __loop++)
            {
                MyLib._deleteDataType getData = (MyLib._deleteDataType)selectRowOrder[__loop];
                __myQuery.Append(string.Format(MyLib._myUtil._convertTextToXmlForQuery("delete from {0} {1}"), _myManageData1._dataList._tableName, getData.whereString));
            } // for
            __myQuery.Append("</node>");
            MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();
            string result = _myFrameWork._queryList(MyLib._myGlobal._databaseName, __myQuery.ToString());
            if (result.Length == 0)
            {
                MyLib._myGlobal._displayWarning(1, MyLib._myGlobal._resource("สำเร็จ"));
                _myManageData1._dataList._refreshData();
            }
            else
            {
                MessageBox.Show(result, "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private bool _myManageData1__loadDataToScreen(object rowData, string whereString, bool forEdit)
        {
            try
            {
                this._myManageData1__clearData();

                ArrayList __rowDataArray = (ArrayList)rowData;
                int __docNoColumnNumber = this._myManageData1._dataList._gridData._findColumnByName(_g.d.as_trans._table + "." + _g.d.as_trans._doc_no);

                this._oldDocNo = __rowDataArray[__docNoColumnNumber].ToString().ToUpper();

                StringBuilder __query = new StringBuilder();
                __query.Append("<node>");

                __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.as_trans._table + " where " + _g.d.as_trans._doc_no + "=\'" + this._oldDocNo + "\'"));
                __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.as_trans_detail._table + " where " + _g.d.as_trans_detail._doc_no + "=\'" + this._oldDocNo + "\'"));

                __query.Append(this._glDetail._loadDataQuery(this._oldDocNo));

                __query.Append("</node>");

                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                ArrayList __result = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __query.ToString());
                if (__result.Count > 0)
                {
                    this._detailScreen._screenTop._loadData(((DataSet)__result[0]).Tables[0]);
                    this._detailScreen._asConditionScreen1._loadData(((DataSet)__result[0]).Tables[0]);
                    this._detailScreen._gridAssetDetail._loadFromDataTable(((DataSet)__result[1]).Tables[0]);

                    this._glScreenTop._loadData((((DataSet)__result[2]).Tables[0]));
                    this._glDetail._glDetailGrid._loadFromDataTable(((DataSet)__result[3]).Tables[0]);
                    this._glDetail._loadDataExtra(this._glDetail._glDetailGrid, __result, 4);
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        private void _myManageData1__clearData()
        {
            this._detailScreen._clear();
            this._oldDocNo = "";

            this._glScreenTop._clear();
            this._glDetail._glDetailGrid._clear();
        }

        private void _myManageData1__newDataClick()
        {
            _myManageData1__clearData();
            this._detailScreen._screenTop._focusFirst();

            this._detailScreen._screenTop._setDataDate(_g.d.as_trans._doc_date, MyLib._myGlobal._workingDate);
            this._detailScreen._screenTop._setDataStr(_g.d.as_trans._doc_time, DateTime.Now.Hour.ToString("D2") + ":" + DateTime.Now.Minute.ToString("D2"), "", true);

            this._glScreenTop._setCheckBox(_g.d.gl_journal._trans_direct, true);
        }

        void _myManageData1__closeScreen()
        {
            this.Dispose();
        }

        public void _glCreate()
        {
            {
                this._myPanelGl = new MyLib._myPanel();
                this._myPanelGl.SuspendLayout();
                //
                // _myPanelGl
                // 
                this._myPanelGl._switchTabAuto = false;
                this._myPanelGl.BeginColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
                this._myPanelGl.CornerPicture = null;
                this._myPanelGl.Dock = System.Windows.Forms.DockStyle.Fill;
                this._myPanelGl.EndColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
                this._myPanelGl.Location = new System.Drawing.Point(0, 0);
                this._myPanelGl.Name = "_myPanel2";
                this._myPanelGl.Padding = new System.Windows.Forms.Padding(4);
                this._myPanelGl.Size = new System.Drawing.Size(880, 282);
                this._myPanelGl.TabIndex = 1;
                //
                this._myPanelGl.ResumeLayout(false);
                //
                this._tab_gl = new System.Windows.Forms.TabPage();
                this._tab_gl.SuspendLayout();
                this._detailScreen._tab.Controls.Add(this._tab_gl);
                // 
                // tab_gl
                // 
                this._tab_gl.Controls.Add(this._myPanelGl);
                this._tab_gl.ImageKey = "document_add.png";
                this._tab_gl.Location = new System.Drawing.Point(4, 23);
                this._tab_gl.Name = "tab_gl";
                this._tab_gl.Size = new System.Drawing.Size(66, 0);
                this._tab_gl.TabIndex = 1;
                this._tab_gl.Text = "tab_gl";
                this._tab_gl.UseVisualStyleBackColor = true;
                //
                this._tab_gl.ResumeLayout(false);

                this._glScreenTop = new SMLERPGLControl._journalScreen();
                this._glDetail = new SMLERPGLControl._glDetail();
                this._glScreenTop.Dock = DockStyle.Top;
                this._glDetail.Dock = DockStyle.Fill;
                this._myPanelGl.Controls.Add(this._glDetail);
                this._myPanelGl.Controls.Add(this._glScreenTop);
                //
                this._glScreenTop._checkBoxChanged += (s1, name) =>
                {
                    if (name.Equals(_g.d.gl_journal._trans_direct))
                    {
                        //this._glScreenCheck();
                    }
                };
                //this._glScreenCheck();

                this._glScreenTop._enabedControl(_g.d.gl_journal._doc_no, false);
                this._glScreenTop._enabedControl(_g.d.gl_journal._doc_date, false);
                this._glScreenTop._enabedControl(_g.d.gl_journal._doc_format_code, false);

                //this._glScreenTop._enabedControl(_g.d.gl_journal._ref_date, true);
                //this._glScreenTop._enabedControl(_g.d.gl_journal._ref_no, true);
                //this._glScreenTop._enabedControl(_g.d.gl_journal._book_code, false);
                this._glScreenTop._enabedControl(_g.d.gl_journal._journal_type, false);
                this._glScreenTop._enabedControl(_g.d.gl_journal._description, false);
            }
        }
    }
}
