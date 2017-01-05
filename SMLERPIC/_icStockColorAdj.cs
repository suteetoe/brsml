using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using System.Threading;

namespace SMLERPIC
{
    public partial class _icStockColorAdj : UserControl
    {
        Thread _processThread = null;
        string _processStatus = "";
        Boolean _processActive = false;
        ArrayList _itemList = null;

        public _icStockColorAdj()
        {
            InitializeComponent();
            //this._dateBeginLabel.Text = MyLib._myResource._findResource(_g.d.ic_resource._table + "." + _g.d.ic_resource._date_process)._str;
            this._dateBegin._dateTime = MyLib._myGlobal._workingDate;
            this._dateBegin.Invalidate();
            this._dateEnd._dateTime = MyLib._myGlobal._workingDate;
            this._dateEnd.Invalidate();
            this.Load += new EventHandler(_icStockColorAdj_Load);
            this._timer.Tick += new EventHandler(_timer_Tick);
            this._processStopButton.Click += new EventHandler(_processStopButton_Click);
            this.Disposed += new EventHandler(_icStockColorAdj_Disposed);
            this._processStopButton.Enabled = false;
            this._ictransItemGridIn._total_show = true;
            this._ictransItemGridOut._total_show = true;
            this._ictransItemGridIn._checkAutoQty = false;
            this._ictransItemGridOut._checkAutoQty = false;
        }

        void _icStockColorAdj_Disposed(object sender, EventArgs e)
        {
            if (this._processThread != null)
            {
                this._processThread.Abort();
            }
        }

        void _processStopButton_Click(object sender, EventArgs e)
        {
            this._processStopButton.Enabled = false;
            this._processStartButton.Enabled = true;
            if (this._processThread != null)
            {
                this._processThread.Abort();
            }
            this._processThread = null;
        }

        void _timer_Tick(object sender, EventArgs e)
        {
            this._processLabel.Text = this._processStatus;
            if (this._processActive == false)
            {
                this._processStartButton.Enabled = true;
                this._timer.Stop();
                this._processLabel.Text = "Success";
            }
            this._processLabel.Invalidate();
        }

        void _icStockColorAdj_Load(object sender, EventArgs e)
        {
            this._dateBegin.Focus();
        }

        private void _closeButton_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void _process()
        {
            try
            {
                DateTime __dateTrial = this._dateBegin._dateTime;
                while (__dateTrial.CompareTo(this._dateEnd._dateTime) <= 0)
                {
                    ArrayList __resultOutItemCode = new ArrayList();
                    ArrayList __resultOutItemCodeMain = new ArrayList();
                    ArrayList __resultOutQty = new ArrayList();
                    ArrayList __resultInItemCode = new ArrayList();
                    ArrayList __resultInQty = new ArrayList();
                    //
                    this._processStatus = __dateTrial.ToShortDateString();
                    //
                    this._ictransItemGridIn._clear();
                    this._ictransItemGridOut._clear();
                    MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                    // Process
                    string __queryTrans = "select " + _g.d.ic_trans._doc_no + " from " + _g.d.ic_trans._table + " where " + _g.d.ic_trans._doc_date + "=\'" + MyLib._myGlobal._convertDateToQuery(__dateTrial) + "\' and " +
                        _g.d.ic_trans._doc_no + " in (select " + _g.d.ic_trans_detail._doc_no + " from " + _g.d.ic_trans_detail._table +
                        " where " + _g.d.ic_trans._last_status + "=0 and " + _g.d.ic_trans_detail._doc_date + "=\'" + MyLib._myGlobal._convertDateToQuery(__dateTrial) + "\' and " +
                        _g.d.ic_trans_detail._trans_flag + " in (" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_ขายสินค้าและบริการ).ToString() + "," + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ขาย_เพิ่มหนี้).ToString() + ") and " +
                        _g.d.ic_trans_detail._item_type + "=5) order by " + _g.d.ic_trans._doc_no;
                    DataTable __trans = __myFrameWork._queryShort(__queryTrans).Tables[0];
                    for (int __rowTrans = 0; __rowTrans < __trans.Rows.Count; __rowTrans++)
                    {
                        // ดึงรายการย่อย
                        string __docNo = __trans.Rows[__rowTrans][_g.d.ic_trans._doc_no].ToString();
                        string __queryDetail = "select " + _g.d.ic_trans_detail._item_code + "," + _g.d.ic_trans_detail._unit_code + "," + _g.d.ic_trans_detail._qty + "," + _g.d.ic_trans_detail._item_type + "," + _g.d.ic_trans_detail._item_code_main +
                            " from " + _g.d.ic_trans_detail._table + " where " + _g.d.ic_trans_detail._last_status + "=0 and " + _g.d.ic_trans_detail._doc_no + "=\'" + __docNo + "\'" +
                            " order by " + _g.d.ic_trans_detail._line_number;
                        DataTable __transDetails = __myFrameWork._queryShort(__queryDetail).Tables[0];
                        decimal __itemQty = 0;
                        string __formulaItemCode = "";
                        for (int __rowTransDetails = 0; __rowTransDetails < __transDetails.Rows.Count; __rowTransDetails++)
                        {
                            decimal __itemQty2 = MyLib._myGlobal._decimalPhase(__transDetails.Rows[__rowTransDetails][_g.d.ic_trans_detail._qty].ToString());
                            int __itemType = MyLib._myGlobal._intPhase(__transDetails.Rows[__rowTransDetails][_g.d.ic_trans_detail._item_type].ToString());
                            if (__itemType == 5)
                            {
                                __itemQty = MyLib._myGlobal._decimalPhase(__transDetails.Rows[__rowTransDetails][_g.d.ic_trans_detail._qty].ToString());
                            }
                            if (__itemType == 6)
                            {
                                // กรณีสูตรสี
                                string __itemCodeMain = __transDetails.Rows[__rowTransDetails][_g.d.ic_trans_detail._item_code_main].ToString();

                                // check sync auto
                                if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLColorStore)
                                {
                                    _checkSyncColor(__itemCodeMain);
                                }

                                string __queryFormula = "select " + _g.d.ic_inventory_set_detail._ic_code + "," + _g.d.ic_inventory_set_detail._qty + "," + _g.d.ic_inventory_set_detail._unit_code +
                                    " from " + _g.d.ic_inventory_set_detail._table + " where " + _g.d.ic_inventory_set_detail._ic_set_code + "=\'" + __itemCodeMain + "\'" +
                                    " order by " + _g.d.ic_inventory_set_detail._line_number;
                                DataTable __formula = __myFrameWork._queryShort(__queryFormula).Tables[0];
                                if (__formula.Rows.Count > 2)
                                {
                                    __formulaItemCode = __formula.Rows[1][_g.d.ic_inventory_set_detail._ic_code].ToString();
                                    decimal __formulaQty = MyLib._myGlobal._round(MyLib._myGlobal._decimalPhase(__formula.Rows[1][_g.d.ic_inventory_set_detail._qty].ToString()) * __itemQty2, _g.g._companyProfile._item_qty_decimal);
                                    int __addr = -1;
                                    for (int __find = 0; __find < __resultInItemCode.Count; __find++)
                                    {
                                        if (__resultInItemCode[__find].ToString().Equals(__formulaItemCode))
                                        {
                                            __addr = __find;
                                            break;
                                        }
                                    }
                                    if (__addr == -1)
                                    {
                                        __resultInItemCode.Add(__formulaItemCode);
                                        __resultInQty.Add(__formulaQty);
                                    }
                                    else
                                    {
                                        decimal __getQty = ((decimal)__resultInQty[__addr]) + __formulaQty;
                                        __resultInQty[__addr] = __getQty;
                                    }
                                }
                                for (int __rowFormula = 2; __rowFormula < __formula.Rows.Count; __rowFormula++)
                                {
                                    // ค้นหาใน ArrayList
                                    string __itemCode = __formula.Rows[__rowFormula][_g.d.ic_inventory_set_detail._ic_code].ToString();
                                    decimal __qty = MyLib._myGlobal._round(MyLib._myGlobal._decimalPhase(__formula.Rows[__rowFormula][_g.d.ic_inventory_set_detail._qty].ToString()) * __itemQty, _g.g._companyProfile._item_qty_decimal);
                                    int __addr = -1;
                                    for (int __find = 0; __find < __resultOutItemCode.Count; __find++)
                                    {
                                        if (__resultOutItemCode[__find].ToString().Equals(__itemCode) && __resultOutItemCodeMain[__find].ToString().Equals(__formulaItemCode))
                                        {
                                            __addr = __find;
                                            break;
                                        }
                                    }
                                    if (__addr == -1)
                                    {
                                        __resultOutItemCode.Add(__itemCode);
                                        __resultOutItemCodeMain.Add(__formulaItemCode);
                                        __resultOutQty.Add(__qty);
                                    }
                                    else
                                    {
                                        decimal __getQty = ((decimal)__resultOutQty[__addr]) + __qty;
                                        __resultOutQty[__addr] = __getQty;
                                    }
                                }
                            }
                        }
                    }
                    // เอาเข้ากริด
                    for (int __row = 0; __row < __resultOutItemCode.Count; __row++)
                    {
                        int __addr = this._ictransItemGridOut._addRow();
                        this._ictransItemGridOut._cellUpdate(__addr, _g.d.ic_trans_detail._item_code, __resultOutItemCode[__row].ToString(), true);
                        this._ictransItemGridOut._cellUpdate(__addr, _g.d.ic_trans_detail._item_code_main, __resultOutItemCodeMain[__row].ToString(), false);
                        
                        DataTable __costPack = __myFrameWork._queryShort("select " + _g.d.ic_inventory._unit_cost + " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._code + "=\'" + __resultOutItemCode[__row].ToString() + "\'").Tables[0];
                        if (__costPack.Rows.Count > 0)
                        {
                            string __unitCode = __costPack.Rows[0][0].ToString();
                            this._ictransItemGridOut._cellUpdate(__addr, _g.d.ic_trans_detail._unit_code, __unitCode, true);
                            this._ictransItemGridOut._searchUnitNameWareHouseNameShelfName(__addr);
                        }
                        
                        this._ictransItemGridOut._cellUpdate(__addr, _g.d.ic_trans_detail._qty, (decimal)__resultOutQty[__row], true);
                    }
                    this._ictransItemGridOut.Invalidate();
                    //
                    for (int __row = 0; __row < __resultInItemCode.Count; __row++)
                    {
                        int __addr = this._ictransItemGridIn._addRow();
                        this._ictransItemGridIn._cellUpdate(__addr, _g.d.ic_trans_detail._item_code, __resultInItemCode[__row].ToString(), true);
                        this._ictransItemGridIn._cellUpdate(__addr, _g.d.ic_trans_detail._qty, (decimal)__resultInQty[__row], true);
                        DataTable __costPack = __myFrameWork._queryShort("select " + _g.d.ic_inventory._unit_cost + " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._code + "=\'" + __resultInItemCode[__row].ToString() + "\'").Tables[0];
                        if (__costPack.Rows.Count > 0)
                        {
                            string __unitCode = __costPack.Rows[0][0].ToString();
                            this._ictransItemGridIn._cellUpdate(__addr, _g.d.ic_trans_detail._unit_code, __unitCode, false);
                        }
                    }
                    this._ictransItemGridIn.Invalidate();
                    if (this._ictransItemGridIn._rowData.Count > 0)
                    {
                        if (this._autoSaveCheckBox.Checked)
                        {
                            this._save(true, __dateTrial);
                        }
                        else
                        {
                            DialogResult __save = MessageBox.Show("Save : " + __dateTrial.ToShortDateString(), "Save", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
                            if (__save == DialogResult.Yes)
                            {
                                this._save(false, __dateTrial);
                            }
                        }
                    }
                    __dateTrial = __dateTrial.AddDays(1);
                }
            }
            catch (Exception __ex)
            {
                MessageBox.Show(__ex.Message.ToString());
            }
            SMLERPGlobal._smlFrameWork __smlFrameWork = new SMLERPGlobal._smlFrameWork();
            // _g.g._updateDateTimeForCalc();
            string __resultStr = __smlFrameWork._process_stock_balance(MyLib._myGlobal._databaseName, _g.g._getItemRepack(this._itemList), "*");
            if (__resultStr.Length > 0)
            {
                MessageBox.Show(__resultStr);
            }
            MessageBox.Show("Process End");
            this._processActive = false;
        }

        private void _processButton_Click(object sender, EventArgs e)
        {
            this._itemList = new ArrayList();
            //
            this._processActive = true;
            this._processThread = new Thread(new ThreadStart(_process));
            this._processThread.Start();
            //
            this._timer.Start();
            //
            this._processStopButton.Enabled = true;
            this._processStartButton.Enabled = false;
        }

        void _checkSyncColor(string itemSetCode)
        {
            string __query = "select count(*) as xcount from " + _g.d.ic_inventory_set_detail._table + " where " + _g.d.ic_inventory_set_detail._ic_set_code + "=\'" + itemSetCode + "\' ";
            MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();
            DataTable __result = _myFrameWork._queryShort(__query).Tables[0];
            if (__result.Rows.Count > 0 && MyLib._myGlobal._intPhase(__result.Rows[0][0].ToString()) == 0)
            {
                // import ใหม่
                if ((MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLColorStore) && _g.g._companyProfile._sync_wbservice_url.Trim().Length > 0 && _g.g._companyProfile._sync_product)
                {
                    SMLProcess._syncClass __sync = new SMLProcess._syncClass();
                    bool __found = __sync._findSetDetail(itemSetCode);

                    if (__found == true)
                    {
                        if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLColorStore)
                        {
                            StringBuilder __querySync = new StringBuilder(MyLib._myGlobal._xmlHeader + "<node>");
                            __querySync.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ic_inventory._item_type + " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._code + "=\'" + itemSetCode + "\'"));
                            __querySync.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.ic_inventory_set_detail._table + " where " + _g.d.ic_inventory_set_detail._ic_set_code + "=\'" + itemSetCode + "\'"));
                            __querySync.Append("</node>");

                            ArrayList __syncResult = _myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __querySync.ToString());
                            DataTable __itemTableResult = ((DataSet)__syncResult[0]).Tables[0];
                            DataTable __itemSetTableResult = ((DataSet)__syncResult[1]).Tables[0];

                            if (__itemTableResult.Rows.Count > 0)
                            {
                                string __getItemType = __itemTableResult.Rows[0][_g.d.ic_inventory._item_type].ToString();
                                if (__getItemType.Equals("3") || __getItemType.Equals("5"))
                                {
                                    for (int __row = 0; __row < __itemSetTableResult.Rows.Count; __row++)
                                    {
                                        string __getItemDetailCode = __itemSetTableResult.Rows[__row][_g.d.ic_inventory_set_detail._ic_code].ToString();
                                        __sync._findProduct(__getItemDetailCode);
                                    }
                                }
                            }
                            //__sync._foundItemCode = __findBarcode;
                        }
                    }
                }
            }
        }

        private void _save(Boolean autoSave, DateTime saveDate)
        {
            if (this._ictransItemGridIn._rowData.Count > 0 && this._ictransItemGridOut._rowData.Count > 0)
            {
                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                //
                string __docDate = MyLib._myGlobal._convertDateToQuery(saveDate);
                string __docNo = "0-ADJ-" + __docDate;
                // ลบวันเก่าออกก่อน
                {
                    __myFrameWork._queryShort("delete from " + _g.d.ic_trans._table + " where " + _g.d.ic_trans._doc_no + "=\'" + __docNo + "\'");
                    __myFrameWork._queryShort("delete from " + _g.d.ic_trans_detail._table + " where " + _g.d.ic_trans_detail._doc_no + "=\'" + __docNo + "\'");
                }
                // ดึงรายการสินค้าเดืม เพื่อสั่งคำนวณใหม่
                DataTable __getItemCode = __myFrameWork._queryShort("select " + _g.d.ic_trans_detail._item_code + " from " + _g.d.ic_trans_detail._table + " where " + _g.d.ic_trans_detail._doc_no + "=\'" + __docNo + "\'").Tables[0];
                for (int __row = 0; __row < __getItemCode.Rows.Count; __row++)
                {
                    this._itemList.Add(__getItemCode.Rows[__row][_g.d.ic_trans_detail._item_code].ToString());
                }
                //
                StringBuilder __myQuery = new StringBuilder();
                __myQuery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                // ลบทั้งเบิกผลิตและรับสำเร็จรูป
                __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.ic_trans._table + " where " + _g.d.ic_trans._doc_no + "=\'" + __docNo + "\'"));
                __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.ic_trans_detail._table + " where " + _g.d.ic_trans_detail._doc_no + "=\'" + __docNo + "\'"));
                // เบิกผลิต
                __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _g.d.ic_trans._table + " " +
                    "(" + _g.d.ic_trans._doc_date + "," + _g.d.ic_trans._doc_time + "," + _g.d.ic_trans._doc_no + "," + _g.d.ic_trans._trans_type + "," + _g.d.ic_trans._trans_flag + ") values " +
                    "(\'" + __docDate + "\',\'08:00\',\'" + __docNo + "\',3,56)"));
                for (int __row = 0; __row < this._ictransItemGridOut._rowData.Count; __row++)
                {
                    __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _g.d.ic_trans_detail._table + " " +
                        "(" + _g.d.ic_trans_detail._item_code + "," + _g.d.ic_trans_detail._item_code_main + "," + _g.d.ic_trans_detail._item_name + "," + _g.d.ic_trans_detail._unit_code + "," + _g.d.ic_trans_detail._wh_code + "," + _g.d.ic_trans_detail._shelf_code + "," +
                        _g.d.ic_trans_detail._qty + "," + _g.d.ic_trans_detail._trans_type + "," + _g.d.ic_trans_detail._trans_flag + "," + _g.d.ic_trans_detail._line_number + "," +
                        _g.d.ic_trans_detail._branch_code + "," + _g.d.ic_trans_detail._stand_value + "," + _g.d.ic_trans_detail._divide_value + "," +
                        _g.d.ic_trans_detail._item_type + "," + _g.d.ic_trans_detail._calc_flag + "," +
                        _g.d.ic_trans_detail._doc_date + "," + _g.d.ic_trans_detail._doc_time + "," +
                        _g.d.ic_trans_detail._doc_date_calc + "," + _g.d.ic_trans_detail._doc_time_calc + "," +
                        _g.d.ic_trans_detail._doc_no + "," +
                        _g.d.ic_trans_detail._status + "," + _g.d.ic_trans_detail._last_status + "," + _g.d.ic_trans_detail._vat_type +"," + _g.d.ic_trans_detail._tax_type  +") values " +
                        "(\'" + this._ictransItemGridOut._cellGet(__row, _g.d.ic_trans_detail._item_code).ToString() + "\'," +
                        "\'" + this._ictransItemGridOut._cellGet(__row, _g.d.ic_trans_detail._item_code_main).ToString() + "\'," +
                        "\'" + this._ictransItemGridOut._cellGet(__row, _g.d.ic_trans_detail._item_name).ToString() + "\'," +
                        "\'" + this._ictransItemGridOut._cellGet(__row, _g.d.ic_trans_detail._unit_code).ToString() + "\'," +
                        "\'" + this._ictransItemGridOut._cellGet(__row, _g.d.ic_trans_detail._wh_code).ToString() + "\'," +
                        "\'" + this._ictransItemGridOut._cellGet(__row, _g.d.ic_trans_detail._shelf_code).ToString() + "\'," +
                        this._ictransItemGridOut._cellGet(__row, _g.d.ic_trans_detail._qty).ToString() + "," +
                        "3,56," + __row.ToString() + ",\'" + MyLib._myGlobal._branchCode + "\',1,1,0,-1," +
                        "\'" + __docDate + "\',\'08:00\',\'" + __docDate + "\',\'08:00\',\'" + __docNo + "\',0,0,0,0)"));
                    this._itemList.Add(this._ictransItemGridOut._cellGet(__row, _g.d.ic_trans_detail._item_code).ToString());
                }
                // รับสำเร็จรูป
                __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _g.d.ic_trans._table + " " +
                    "(" + _g.d.ic_trans._doc_date + "," + _g.d.ic_trans._doc_time + "," + _g.d.ic_trans._doc_no + "," + _g.d.ic_trans._trans_type + "," + _g.d.ic_trans._trans_flag + ") values " +
                    "(\'" + __docDate + "\',\'08:00\',\'" + __docNo + "\',3,60)"));
                for (int __row = 0; __row < this._ictransItemGridIn._rowData.Count; __row++)
                {
                    __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _g.d.ic_trans_detail._table + " " +
                        "(" + _g.d.ic_trans_detail._item_code + "," + _g.d.ic_trans_detail._item_name + "," + _g.d.ic_trans_detail._unit_code + "," + _g.d.ic_trans_detail._wh_code + "," + _g.d.ic_trans_detail._shelf_code + "," +
                        _g.d.ic_trans_detail._qty + "," + _g.d.ic_trans_detail._trans_type + "," + _g.d.ic_trans_detail._trans_flag + "," + _g.d.ic_trans_detail._line_number + "," +
                        _g.d.ic_trans_detail._branch_code + "," + _g.d.ic_trans_detail._stand_value + "," + _g.d.ic_trans_detail._divide_value + "," +
                        _g.d.ic_trans_detail._item_type + "," + _g.d.ic_trans_detail._calc_flag + "," +
                        _g.d.ic_trans_detail._doc_date + "," + _g.d.ic_trans_detail._doc_time + "," +
                        _g.d.ic_trans_detail._doc_date_calc + "," + _g.d.ic_trans_detail._doc_time_calc + "," +
                        _g.d.ic_trans_detail._doc_no + "," +
                        _g.d.ic_trans_detail._status + "," + _g.d.ic_trans_detail._last_status + "," + _g.d.ic_trans_detail._vat_type + "," + _g.d.ic_trans_detail._tax_type + ") values " +
                        "(\'" + this._ictransItemGridIn._cellGet(__row, _g.d.ic_trans_detail._item_code).ToString() + "\'," +
                        "\'" + this._ictransItemGridIn._cellGet(__row, _g.d.ic_trans_detail._item_name).ToString() + "\'," +
                        "\'" + this._ictransItemGridIn._cellGet(__row, _g.d.ic_trans_detail._unit_code).ToString() + "\'," +
                        "\'" + this._ictransItemGridIn._cellGet(__row, _g.d.ic_trans_detail._wh_code).ToString() + "\'," +
                        "\'" + this._ictransItemGridIn._cellGet(__row, _g.d.ic_trans_detail._shelf_code).ToString() + "\'," +
                        this._ictransItemGridIn._cellGet(__row, _g.d.ic_trans_detail._qty).ToString() + "," +
                        "3,60," + __row.ToString() + ",\'" + MyLib._myGlobal._branchCode + "\',1,1,0,1," +
                        "\'" + __docDate + "\',\'08:00\',\'" + __docDate + "\',\'08:00\',\'" + __docNo + "\',0,0,0,0)"));
                    this._itemList.Add(this._ictransItemGridIn._cellGet(__row, _g.d.ic_trans_detail._item_code).ToString());
                }
                // Update last_status,used_status,doc_success is null = 0
                SMLProcess._docFlowThread __flow = new SMLProcess._docFlowThread( _g.g._transControlTypeEnum.ว่าง,"","");
                __myQuery.Append(__flow._clearFlowStatusQuery(_g.g._getItemRepack(this._itemList), __docNo));
                //
                __myQuery.Append("</node>");
                string __result = __myFrameWork._queryList(MyLib._myGlobal._databaseName, __myQuery.ToString());
                if (__result.Length == 0)
                {
                    if (autoSave == false)
                    {
                        MyLib._myGlobal._displayWarning(1, null);
                    }
                    this._processStatus = this._processStatus + " : Save Success";
                    //
                }
                else
                {
                    MessageBox.Show(__result, "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show(MyLib._myGlobal._resource("ไม่มีข้อมูล"));
            }
        }
    }
}
