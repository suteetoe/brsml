using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace SMLERPAPARControl
{
    public partial class _ap_ar_deposit_control : UserControl
    {
        private _ApArDepositControlFlagEnum __ApArDepositControlFlagTemp;
        private _ApArDepositControlTypeEnum __ApArDepositControlTypeTemp;

        public _ApArDepositControlFlagEnum ApArDepositControlFlag
        {
            set
            {
                this.__ApArDepositControlFlagTemp = value;
                this._ApArDepositScreenTopControl1.ApArDepositControlFlag = value;
                this._ApArDepositScreenBottomControl1.ApArDepositControlFlag = value;
                //////////_EvenScreenSearch();
                this.Invalidate();
            }
            get
            {
                return this.__ApArDepositControlFlagTemp;
            }
        }

        public _ApArDepositControlTypeEnum ApArDepositControlType
        {
            set
            {
                this.__ApArDepositControlTypeTemp = value;
            }
            get
            {
                return this.__ApArDepositControlTypeTemp;
            }
        }

        int _getColumnDocDate = 0;
        int _getColumnDocNo = 0;
        string _oldDocNo = "";
        // DateTime _oldDate = new DateTime(1000, 1, 1);

        public _ap_ar_deposit_control()
        {
            InitializeComponent();
            //_myManageData       
            if (MyLib._myGlobal._isDesignMode == false)
            {
                this._myToolbar.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            }

            this._myManageData1._dataList._lockRecord = true;
            this._myManageData1._displayMode = 0;
            this._myManageData1._selectDisplayMode(this._myManageData1._displayMode);
            this._myManageData1._manageButton = this._myToolbar;
            this._myManageData1._manageBackgroundPanel = this._myPanel1;
            this._myManageData1._autoSize = true;
            this._myManageData1._autoSizeHeight = 400;
            this._myManageData1.Invalidate();

            //even
            this.Load += new EventHandler(_ap_ar_deposit_control_Load);
            //ManageData
            // ตัวนี้เอาไว้อ้างเพื่อเป็นตัวเชื่อม (relations) เพื่อใช้ในการแก้ไข จะได้ Update ถูก Record
            this._myManageData1._dataList._referFieldAdd(_g.d.ap_ar_trans._doc_no, 1);
            this._myManageData1._loadDataToScreen += new MyLib.LoadDataToScreen(_myManageData1__loadDataToScreen);
            this._myManageData1._dataList._deleteData += new MyLib.DeleteDataEventHandler(_dataList__deleteData);
            this._myManageData1._newDataClick += new MyLib.NewDataEvent(_myManageData1__newDataClick);
            this._myManageData1._discardData += new MyLib.DiscardDataEvent(_myManageData1__discardData);
            this._myManageData1._clearData += new MyLib.ClearDataEvent(_myManageData1__clearData);
            //////////this._myManageData1._dataList._buttonDelete.Enabled = false;
            //////////this._myManageData1._dataList._buttonNew.Enabled = false;
            //////////this._myManageData1._dataList._buttonNewFromTemp.Enabled = false;
            //////////this._myManageData1._dataList._buttonSelectAll.Enabled = false;
            //////////this._saveButton.Enabled = false;
        }

        /// <summary>
        /// Even การทำงาน ของ Grid และ Screen ทั่วไปไม่เกี่ยวกับค้นหา
        /// </summary>
        /// <param name="number"></param>
        /// 
        void _myManageData1__clearData()
        {
            this._ApArDepositScreenTopControl1._clear();
            this._ApArDepositScreenTopControl1._isChange = false;
            this._ApArDepositScreenBottomControl1._clear();
        }

        bool _myManageData1__discardData()
        {
            bool result = true;
            if (this._ApArDepositScreenTopControl1._isChange)
            {
                if (DialogResult.No == MyLib._myGlobal._displayWarning(3, null))
                {
                    result = false;
                }
                else
                {
                    this._ApArDepositScreenTopControl1._isChange = false;
                }
            }
            return (result);
        }

        void _myManageData1__newDataClick()
        {
            Control codeControl = this._ApArDepositScreenTopControl1._getControl(_g.d.ap_ar_trans._doc_no);
            codeControl.Enabled = true;
            this._ApArDepositScreenTopControl1._focusFirst();
        }

        void _dataList__deleteData(System.Collections.ArrayList selectRowOrder)
        {
            StringBuilder __myQuery = new StringBuilder();
            __myQuery.Append(MyLib._myGlobal._xmlHeader + "<node>");
            for (int __loop = 0; __loop < selectRowOrder.Count; __loop++)
            {
                MyLib._deleteDataType getData = (MyLib._deleteDataType)selectRowOrder[__loop];
                __myQuery.Append(string.Format(MyLib._myUtil._convertTextToXmlForQuery("delete from {0} {1}"), this._myManageData1._dataList._tableName, getData.whereString + " and " + _g.d.ap_ar_trans._trans_flag +
                    " = " + _ApArDepositGlobal._ApArDepositFlag(ApArDepositControlFlag).ToString() + " and " +
                    _g.d.ap_ar_trans._trans_type + "=" + _ApArDepositGlobalType._ApArDepositType(ApArDepositControlType).ToString()));
            } // for
            __myQuery.Append("</node>");
            MyLib._myFrameWork myFrameWork = new MyLib._myFrameWork();
            string result = myFrameWork._queryList(MyLib._myGlobal._databaseName, __myQuery.ToString());
            if (result.Length == 0)
            {
                MyLib._myGlobal._displayWarning(1, MyLib._myGlobal._resource("ทำงานสำเร็จ"));
                this._myManageData1._dataList._refreshData();
            }
            else
            {
                MessageBox.Show(result, "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        bool _myManageData1__loadDataToScreen(object rowData, string whereString, bool forEdit)
        {
            try
            {
                ArrayList __rowDataArray = (ArrayList)rowData;
                _get_column_number();
                //_oldDate = MyLib._myGlobal._convertDate(__rowDataArray[_getColumnDocDate].ToString());
                _oldDocNo = __rowDataArray[_getColumnDocNo].ToString();
                MyLib._myFrameWork myFrameWork = new MyLib._myFrameWork();
                StringBuilder __myquery = new StringBuilder();
                __myquery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.ap_ar_trans._table + whereString + " and " + _g.d.ap_ar_trans._trans_flag +
                    " = " + _ApArDepositGlobal._ApArDepositFlag(ApArDepositControlFlag).ToString() + " and " +
                    _g.d.ap_ar_trans._trans_type + "=" + _ApArDepositGlobalType._ApArDepositType(ApArDepositControlType).ToString()));
                __myquery.Append("</node>");
                ArrayList _getData = myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());

                this._ApArDepositScreenTopControl1._loadData(((DataSet)_getData[0]).Tables[0]);
                this._ApArDepositScreenBottomControl1._loadData(((DataSet)_getData[0]).Tables[0]);

                if (forEdit)
                {
                    this._ApArDepositScreenTopControl1._focusFirst();
                }
                // ให้คำนวณยอดรวม             
                this._ApArDepositScreenTopControl1.Invalidate();
                this._ApArDepositScreenBottomControl1.Invalidate();

            }
            catch (Exception)
            {
            }
            return (false);
        }

        private void _saveButton_Click(object sender, EventArgs e)
        {
            _saveData();
        }

        private void _saveData()
        {
            string getEmtry = this._ApArDepositScreenTopControl1._checkEmtryField();
            if (getEmtry.Length > 0)
            {
                MyLib._myGlobal._displayWarning(2, getEmtry);
            }
            else
            {
                //จับข้อมูลเข้า ArrayList ก่อน
                ArrayList getData = this._ApArDepositScreenTopControl1._createQueryForDatabase();
                ArrayList getData2 = this._ApArDepositScreenBottomControl1._createQueryForDatabase();
                //เตรียมแพค xml
                StringBuilder __myQuery = new StringBuilder();
                __myQuery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                if (this._myManageData1._mode == 1)
                {
                    __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery(" insert into " + _g.d.ap_ar_trans._table
                        + " (" + getData[0].ToString()
                        + ","
                        + getData2[0].ToString()
                        + ","
                        + _g.d.ap_ar_trans._trans_flag
                        + ","
                        + _g.d.ap_ar_trans._trans_type
                        + ") values ("
                        + getData[1].ToString()
                        + ","
                        + getData2[1].ToString()
                        + ","
                        + _ApArDepositGlobal._ApArDepositFlag(ApArDepositControlFlag).ToString()
                        + ","
                        + _ApArDepositGlobalType._ApArDepositType(ApArDepositControlType).ToString()
                        + ")"));
                }
                else
                {
                    __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery(" update " + this._myManageData1._dataList._tableName
                        + " set "
                        + getData[2].ToString() + "," + getData2[2].ToString() + this._myManageData1._dataList._whereString
                        + " and "
                        + _g.d.ap_ar_trans._trans_flag + " = " + _ApArDepositGlobal._ApArDepositFlag(ApArDepositControlFlag).ToString()
                        + " and "
                        + _g.d.ap_ar_trans._trans_type + " = " + _ApArDepositGlobalType._ApArDepositType(ApArDepositControlType).ToString()));
                }
                //                
                // update ค่าอื่นๆ เผื่อหลุด
                __myQuery.Append(_g.g._queryUpdateTrans());
                //
                __myQuery.Append("</node>");
                MyLib._myFrameWork myFrameWork = new MyLib._myFrameWork();
                string result = myFrameWork._queryList(MyLib._myGlobal._databaseName, __myQuery.ToString());
                if (result.Length == 0)
                {
                    MyLib._myGlobal._displayWarning(1, null);
                    this._ApArDepositScreenTopControl1._isChange = false;
                    if (this._myManageData1._mode == 1)
                    {
                        this._myManageData1._afterInsertData();
                    }
                    else
                    {
                        this._myManageData1._afterUpdateData();
                    }

                    this._ApArDepositScreenTopControl1._clear();
                    this._ApArDepositScreenBottomControl1._clear();
                    this._ApArDepositScreenTopControl1._focusFirst();
                    //_autoRunning();

                }
                else
                {
                    MessageBox.Show(result, "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        /// ฟังชั่น ทั่วไป
        /// </summary>
        /// <param name="number">autoRuning=เลขที่เอกสารอัตโนมัต,CalcTotalGrid=คำนวนผลรวมใส่ Screen Bottom</param>        

        void _get_column_number()
        {
            _getColumnDocDate = this._myManageData1._dataList._gridData._findColumnByName(_g.d.ap_ar_trans._table + "." + _g.d.ap_ar_trans._doc_date);
            _getColumnDocNo = this._myManageData1._dataList._gridData._findColumnByName(_g.d.ap_ar_trans._table + "." + _g.d.ap_ar_trans._doc_no);
        }

        /// <summary>
        /// Even การทำงาน From Load
        /// </summary>
        /// <param name="number">ทำงานครั้งแรกที่เปิดหน้าจอ</param>
        void _ap_ar_deposit_control_Load(object sender, EventArgs e)
        {
            /*switch (ApArDepositControlFlag)
            {
                case _ApArDepositControlFlagEnum.ap_advance:
                    this._myManageData1._dataList._extraWhere = _g.d.ap_ar_trans._trans_flag + "=" + _ApArDepositGlobal._ApArDepositFlag(ApArDepositControlFlag).ToString() +
                        " and " + _g.d.ap_ar_trans._trans_type + "=" + _ApArDepositGlobalType._ApArDepositType(ApArDepositControlType).ToString();
                    this._myManageData1._dataList._loadViewFormat(_g.g._screen_po_so_deposit, MyLib._myGlobal._userSearchScreenGroup, true);
                    break;
                case _ApArDepositControlFlagEnum.ap_advance_cancel:
                    this._myManageData1._dataList._extraWhere = _g.d.ap_ar_trans._trans_flag + "=" + _ApArDepositGlobal._ApArDepositFlag(ApArDepositControlFlag).ToString() +
                        " and " + _g.d.ap_ar_trans._trans_type + "=" + _ApArDepositGlobalType._ApArDepositType(ApArDepositControlType).ToString();
                    this._myManageData1._dataList._loadViewFormat(_g.g._screen_po_so_deposit, MyLib._myGlobal._userSearchScreenGroup, true);
                    break;
                case _ApArDepositControlFlagEnum.ap_deposit:
                    this._myManageData1._dataList._extraWhere = _g.d.ap_ar_trans._trans_flag + "=" + _ApArDepositGlobal._ApArDepositFlag(ApArDepositControlFlag).ToString() +
                        " and " + _g.d.ap_ar_trans._trans_type + "=" + _ApArDepositGlobalType._ApArDepositType(ApArDepositControlType).ToString();
                    this._myManageData1._dataList._loadViewFormat(_g.g._screen_po_so_deposit, MyLib._myGlobal._userSearchScreenGroup, true);
                    break;
                case _ApArDepositControlFlagEnum.ap_deposit_cancel:
                    this._myManageData1._dataList._extraWhere = _g.d.ap_ar_trans._trans_flag + "=" + _ApArDepositGlobal._ApArDepositFlag(ApArDepositControlFlag).ToString() +
                        " and " + _g.d.ap_ar_trans._trans_type + "=" + _ApArDepositGlobalType._ApArDepositType(ApArDepositControlType).ToString();
                    this._myManageData1._dataList._loadViewFormat(_g.g._screen_po_so_deposit, MyLib._myGlobal._userSearchScreenGroup, true);
                    break;
                case _ApArDepositControlFlagEnum.ar_advance:
                    this._myManageData1._dataList._extraWhere = _g.d.ap_ar_trans._trans_flag + "=" + _ApArDepositGlobal._ApArDepositFlag(ApArDepositControlFlag).ToString() +
                        " and " + _g.d.ap_ar_trans._trans_type + "=" + _ApArDepositGlobalType._ApArDepositType(ApArDepositControlType).ToString();
                    this._myManageData1._dataList._loadViewFormat(_g.g._screen_po_so_deposit, MyLib._myGlobal._userSearchScreenGroup, true);
                    break;
                case _ApArDepositControlFlagEnum.ar_advance_cancle:
                    this._myManageData1._dataList._extraWhere = _g.d.ap_ar_trans._trans_flag + "=" + _ApArDepositGlobal._ApArDepositFlag(ApArDepositControlFlag).ToString() +
                        " and " + _g.d.ap_ar_trans._trans_type + "=" + _ApArDepositGlobalType._ApArDepositType(ApArDepositControlType).ToString();
                    this._myManageData1._dataList._loadViewFormat(_g.g._screen_po_so_deposit, MyLib._myGlobal._userSearchScreenGroup, true);
                    break;
                case _ApArDepositControlFlagEnum.ar_deposit:
                    this._myManageData1._dataList._extraWhere = _g.d.ap_ar_trans._trans_flag + "=" + _ApArDepositGlobal._ApArDepositFlag(ApArDepositControlFlag).ToString() +
                        " and " + _g.d.ap_ar_trans._trans_type + "=" + _ApArDepositGlobalType._ApArDepositType(ApArDepositControlType).ToString();
                    this._myManageData1._dataList._loadViewFormat(_g.g._screen_po_so_deposit, MyLib._myGlobal._userSearchScreenGroup, true);
                    break;
                case _ApArDepositControlFlagEnum.ar_deposit_cancel:
                    this._myManageData1._dataList._extraWhere = _g.d.ap_ar_trans._trans_flag + "=" + _ApArDepositGlobal._ApArDepositFlag(ApArDepositControlFlag).ToString() +
                        " and " + _g.d.ap_ar_trans._trans_type + "=" + _ApArDepositGlobalType._ApArDepositType(ApArDepositControlType).ToString();
                    this._myManageData1._dataList._loadViewFormat(_g.g._screen_po_so_deposit, MyLib._myGlobal._userSearchScreenGroup, true);
                    break;
            }*/
            //this._myManageData1._dataList._refreshData();
            this._myManageData1._dataListOpen = true;
            this._myManageData1._calcArea();
        }

    }

    public class _ApArDepositScreenTopControl : MyLib._myScreen
    {
        private _ApArDepositControlFlagEnum __ApArDepositControlFlagTemp;

        public _ApArDepositControlFlagEnum ApArDepositControlFlag
        {
            set
            {
                this.__ApArDepositControlFlagTemp = value;
                this._build();
                this.Invalidate();
            }
            get
            {
                return this.__ApArDepositControlFlagTemp;
            }
        }

        private _ApArDepositControlTypeEnum __ApArDepositControlTypeTemp;

        public _ApArDepositControlTypeEnum ApArDepositControlType
        {
            set
            {
                this.__ApArDepositControlTypeTemp = value;
            }
            get
            {
                return this.__ApArDepositControlTypeTemp;
            }
        }

        public string _autorun_old_doc_no = "";

        public _ApArDepositScreenTopControl()
        {
            this._build();
        }

        void _build()
        {
            this.SuspendLayout();
            this._reset();
            this._table_name = _g.d.ap_ar_trans._table;
            switch (ApArDepositControlFlag)
            {
                case _ApArDepositControlFlagEnum.ap_advance:
                    this._maxColumn = 2;
                    this._addTextBox(0, 0, 1, 0, _g.d.ap_ar_trans._doc_no, 1, 25, 3, true, false, false);
                    this._addDateBox(0, 1, 1, 0, _g.d.ap_ar_trans._doc_date, 1, true, false);
                    this._addTextBox(1, 0, 1, 0, _g.d.ap_ar_trans._cust_code, 1, 25, 1, true, false, true, true, true, _g.d.ap_ar_trans._ap_code);
                    this._addTextBox(1, 1, 1, 0, _g.d.ap_ar_trans._ap_name, 1, 10, 0, true, false, true, false);
                    this._addTextBox(2, 0, 1, 0, _g.d.ap_ar_trans._book_no, 1, 255, 1, true, false, true);
                    this._addTextBox(2, 1, 1, 0, _g.d.ap_ar_trans._payment_method, 1, 25, 1, true, false, true);
                    this._addTextBox(3, 0, 1, 0, _g.d.ap_ar_trans._sale_code, 1, 255, 1, true, false, true);
                    this._addTextBox(3, 1, 1, 0, _g.d.ap_ar_trans._department_code, 1, 10, 1, true, false, true);
                    this._addTextBox(4, 0, 1, 0, _g.d.ap_ar_trans._project_code, 1, 10, 1, true, false, true);
                    this._addTextBox(4, 1, 1, 0, _g.d.ap_ar_trans._allocate_code, 1, 10, 1, true, false, true);
                    this._addTextBox(5, 0, 1, 0, _g.d.ap_ar_trans._pay_installments, 1, 100, 0, true, false, true);
                    this._addDateBox(5, 1, 1, 0, _g.d.ap_ar_trans._delivery_date, 1, true, false);
                    this._addTextBox(6, 0, 2, 0, _g.d.ap_ar_trans._remark, 2, 255, 0, true, false, true);
                    this._addTextBox(8, 0, 1, 0, _g.d.ap_ar_trans._currency_code, 1, 255, 1, true, false, true);
                    this._addTextBox(8, 1, 1, 0, _g.d.ap_ar_trans._exchange_rate, 1, 255, 1, true, false, true);
                    this._addTextBox(9, 0, 1, 0, _g.d.ap_ar_trans._doc_group, 1, 255, 1, true, false, true);
                    break;
                case _ApArDepositControlFlagEnum.ap_advance_cancel:
                    this._maxColumn = 2;
                    this._addTextBox(0, 0, 1, 0, _g.d.ap_ar_trans._doc_no, 1, 25, 3, true, false, false);
                    this._addDateBox(0, 1, 1, 0, _g.d.ap_ar_trans._doc_date, 1, true, false);
                    this._addTextBox(1, 0, 1, 0, _g.d.ap_ar_trans._cust_code, 1, 25, 1, true, false, true, true, true, _g.d.ap_ar_trans._ap_code);
                    this._addTextBox(1, 1, 1, 0, _g.d.ap_ar_trans._ap_name, 1, 10, 0, true, false, true, false);
                    this._addTextBox(2, 0, 1, 0, _g.d.ap_ar_trans._book_no, 1, 255, 1, true, false, true);
                    this._addTextBox(2, 1, 1, 0, _g.d.ap_ar_trans._payment_method, 1, 25, 1, true, false, true);
                    this._addTextBox(3, 0, 1, 0, _g.d.ap_ar_trans._sale_code, 1, 255, 1, true, false, true);
                    this._addTextBox(3, 1, 1, 0, _g.d.ap_ar_trans._department_code, 1, 10, 1, true, false, true);
                    this._addTextBox(4, 0, 1, 0, _g.d.ap_ar_trans._project_code, 1, 10, 1, true, false, true);
                    this._addTextBox(4, 1, 1, 0, _g.d.ap_ar_trans._allocate_code, 1, 10, 1, true, false, true);
                    this._addTextBox(5, 0, 1, 0, _g.d.ap_ar_trans._pay_installments, 1, 100, 0, true, false, true);
                    this._addDateBox(5, 1, 1, 0, _g.d.ap_ar_trans._delivery_date, 1, true, false);
                    this._addTextBox(6, 0, 2, 0, _g.d.ap_ar_trans._remark, 2, 255, 0, true, false, true);
                    this._addTextBox(8, 0, 1, 0, _g.d.ap_ar_trans._currency_code, 1, 255, 1, true, false, true);
                    this._addTextBox(8, 1, 1, 0, _g.d.ap_ar_trans._exchange_rate, 1, 255, 1, true, false, true);
                    this._addTextBox(9, 0, 1, 0, _g.d.ap_ar_trans._doc_group, 1, 255, 1, true, false, true);
                    break;
                case _ApArDepositControlFlagEnum.ap_deposit:
                    this._maxColumn = 2;
                    this._addTextBox(0, 0, 1, 0, _g.d.ap_ar_trans._doc_no, 1, 25, 3, true, false, false);
                    this._addDateBox(0, 1, 1, 0, _g.d.ap_ar_trans._doc_date, 1, true, false);
                    this._addTextBox(1, 0, 1, 0, _g.d.ap_ar_trans._cust_code, 1, 25, 1, true, false, true, true, true, _g.d.ap_ar_trans._ap_code);
                    this._addTextBox(1, 1, 1, 0, _g.d.ap_ar_trans._ap_name, 1, 10, 0, true, false, true, false);
                    this._addTextBox(2, 0, 1, 0, _g.d.ap_ar_trans._book_no, 1, 255, 1, true, false, true);
                    this._addTextBox(2, 1, 1, 0, _g.d.ap_ar_trans._payment_method, 1, 25, 1, true, false, true);
                    this._addTextBox(3, 0, 1, 0, _g.d.ap_ar_trans._sale_code, 1, 255, 1, true, false, true);
                    this._addTextBox(3, 1, 1, 0, _g.d.ap_ar_trans._department_code, 1, 10, 1, true, false, true);
                    this._addTextBox(4, 0, 1, 0, _g.d.ap_ar_trans._project_code, 1, 10, 1, true, false, true);
                    this._addTextBox(4, 1, 1, 0, _g.d.ap_ar_trans._allocate_code, 1, 10, 1, true, false, true);
                    this._addTextBox(5, 0, 1, 0, _g.d.ap_ar_trans._pay_installments, 1, 100, 0, true, false, true);
                    this._addDateBox(5, 1, 1, 0, _g.d.ap_ar_trans._delivery_date, 1, true, false);
                    this._addTextBox(6, 0, 2, 0, _g.d.ap_ar_trans._remark, 2, 255, 0, true, false, true);
                    this._addTextBox(8, 0, 1, 0, _g.d.ap_ar_trans._currency_code, 1, 255, 1, true, false, true);
                    this._addTextBox(8, 1, 1, 0, _g.d.ap_ar_trans._exchange_rate, 1, 255, 1, true, false, true);
                    this._addTextBox(9, 0, 1, 0, _g.d.ap_ar_trans._doc_group, 1, 255, 1, true, false, true);
                    break;
                case _ApArDepositControlFlagEnum.ap_deposit_cancel:
                    this._maxColumn = 2;
                    this._addTextBox(0, 0, 1, 0, _g.d.ap_ar_trans._doc_no, 1, 25, 3, true, false, false);
                    this._addDateBox(0, 1, 1, 0, _g.d.ap_ar_trans._doc_date, 1, true, false);
                    this._addTextBox(1, 0, 1, 0, _g.d.ap_ar_trans._cust_code, 1, 25, 1, true, false, true, true, true, _g.d.ap_ar_trans._ap_code);
                    this._addTextBox(1, 1, 1, 0, _g.d.ap_ar_trans._ap_name, 1, 10, 0, true, false, true, false);
                    this._addTextBox(2, 0, 1, 0, _g.d.ap_ar_trans._book_no, 1, 255, 1, true, false, true);
                    this._addTextBox(2, 1, 1, 0, _g.d.ap_ar_trans._payment_method, 1, 25, 1, true, false, true);
                    this._addTextBox(3, 0, 1, 0, _g.d.ap_ar_trans._sale_code, 1, 255, 1, true, false, true);
                    this._addTextBox(3, 1, 1, 0, _g.d.ap_ar_trans._department_code, 1, 10, 1, true, false, true);
                    this._addTextBox(4, 0, 1, 0, _g.d.ap_ar_trans._project_code, 1, 10, 1, true, false, true);
                    this._addTextBox(4, 1, 1, 0, _g.d.ap_ar_trans._allocate_code, 1, 10, 1, true, false, true);
                    this._addTextBox(5, 0, 1, 0, _g.d.ap_ar_trans._pay_installments, 1, 100, 0, true, false, true);
                    this._addDateBox(5, 1, 1, 0, _g.d.ap_ar_trans._delivery_date, 1, true, false);
                    this._addTextBox(6, 0, 2, 0, _g.d.ap_ar_trans._remark, 2, 255, 0, true, false, true);
                    this._addTextBox(8, 0, 1, 0, _g.d.ap_ar_trans._currency_code, 1, 255, 1, true, false, true);
                    this._addTextBox(8, 1, 1, 0, _g.d.ap_ar_trans._exchange_rate, 1, 255, 1, true, false, true);
                    this._addTextBox(9, 0, 1, 0, _g.d.ap_ar_trans._doc_group, 1, 255, 1, true, false, true);
                    break;
                case _ApArDepositControlFlagEnum.ar_advance:
                    this._maxColumn = 2;
                    this._addTextBox(0, 0, 1, 0, _g.d.ap_ar_trans._doc_no, 1, 25, 3, true, false, false);
                    this._addDateBox(0, 1, 1, 0, _g.d.ap_ar_trans._doc_date, 1, true, false);
                    this._addTextBox(1, 0, 1, 0, _g.d.ap_ar_trans._tax_doc_no, 1, 25, 0, true, false, true);
                    this._addDateBox(1, 1, 1, 0, _g.d.ap_ar_trans._tax_doc_date, 1, true, false);
                    this._addComboBox(2, 0, _g.d.ap_ar_trans._vat_type, true, _g.g._po_so_tax_type, false);
                    this._addTextBox(2, 1, 1, 0, _g.d.ap_ar_trans._doc_group, 1, 255, 1, true, false, true);
                    this._addTextBox(3, 0, 1, 0, _g.d.ap_ar_trans._cust_code, 1, 25, 1, true, false, true, true, true, _g.d.ap_ar_trans._ar_code);
                    this._addTextBox(3, 1, 1, 0, _g.d.ap_ar_trans._ar_name, 1, 10, 0, true, false, true, false);
                    this._addTextBox(4, 0, 1, 0, _g.d.ap_ar_trans._book_no, 1, 255, 1, true, false, true);
                    this._addTextBox(4, 1, 1, 0, _g.d.ap_ar_trans._payment_method, 1, 25, 0, true, false, true);
                    this._addTextBox(5, 0, 1, 0, _g.d.ap_ar_trans._sale_code, 1, 255, 1, true, false, true);
                    this._addTextBox(5, 1, 1, 0, _g.d.ap_ar_trans._department_code, 1, 10, 1, true, false, true);
                    this._addTextBox(6, 0, 1, 0, _g.d.ap_ar_trans._project_code, 1, 10, 1, true, false, true);
                    this._addTextBox(6, 1, 1, 0, _g.d.ap_ar_trans._allocate_code, 1, 10, 1, true, false, true);
                    this._addTextBox(7, 0, 1, 0, _g.d.ap_ar_trans._pay_installments, 1, 100, 0, true, false, true);
                    this._addDateBox(7, 1, 1, 0, _g.d.ap_ar_trans._delivery_date, 1, true, false);
                    this._addTextBox(8, 0, 2, 0, _g.d.ap_ar_trans._remark, 2, 255, 0, true, false, true);
                    this._addTextBox(10, 0, 1, 0, _g.d.ap_ar_trans._currency_code, 1, 255, 1, true, false, true);
                    this._addTextBox(10, 1, 1, 0, _g.d.ap_ar_trans._exchange_rate, 1, 255, 1, true, false, true);
                    break;
                case _ApArDepositControlFlagEnum.ar_advance_cancle:
                    this._maxColumn = 2;
                    this._addTextBox(0, 0, 1, 0, _g.d.ap_ar_trans._doc_no, 1, 25, 3, true, false, false);
                    this._addDateBox(0, 1, 1, 0, _g.d.ap_ar_trans._doc_date, 1, true, false);
                    this._addTextBox(1, 0, 1, 0, _g.d.ap_ar_trans._tax_doc_no, 1, 25, 0, true, false, true);
                    this._addDateBox(1, 1, 1, 0, _g.d.ap_ar_trans._tax_doc_date, 1, true, false);
                    this._addComboBox(2, 0, _g.d.ap_ar_trans._vat_type, true, _g.g._po_so_tax_type, false);
                    this._addTextBox(2, 1, 1, 0, _g.d.ap_ar_trans._doc_group, 1, 255, 1, true, false, true);
                    this._addTextBox(3, 0, 1, 0, _g.d.ap_ar_trans._cust_code, 1, 25, 1, true, false, true, true, true, _g.d.ap_ar_trans._ar_code);
                    this._addTextBox(3, 1, 1, 0, _g.d.ap_ar_trans._ar_name, 1, 10, 0, true, false, true, false);
                    this._addTextBox(4, 0, 1, 0, _g.d.ap_ar_trans._book_no, 1, 255, 1, true, false, true);
                    this._addTextBox(4, 1, 1, 0, _g.d.ap_ar_trans._payment_method, 1, 25, 0, true, false, true);
                    this._addTextBox(5, 0, 1, 0, _g.d.ap_ar_trans._sale_code, 1, 255, 1, true, false, true);
                    this._addTextBox(5, 1, 1, 0, _g.d.ap_ar_trans._department_code, 1, 10, 1, true, false, true);
                    this._addTextBox(6, 0, 1, 0, _g.d.ap_ar_trans._project_code, 1, 10, 1, true, false, true);
                    this._addTextBox(6, 1, 1, 0, _g.d.ap_ar_trans._allocate_code, 1, 10, 1, true, false, true);
                    this._addTextBox(7, 0, 1, 0, _g.d.ap_ar_trans._pay_installments, 1, 100, 0, true, false, true);
                    this._addDateBox(7, 1, 1, 0, _g.d.ap_ar_trans._delivery_date, 1, true, false);
                    this._addTextBox(8, 0, 2, 0, _g.d.ap_ar_trans._remark, 2, 255, 0, true, false, true);
                    this._addTextBox(10, 0, 1, 0, _g.d.ap_ar_trans._currency_code, 1, 255, 1, true, false, true);
                    this._addTextBox(10, 1, 1, 0, _g.d.ap_ar_trans._exchange_rate, 1, 255, 1, true, false, true);
                    break;
                case _ApArDepositControlFlagEnum.ar_deposit:
                    this._maxColumn = 2;
                    this._addTextBox(0, 0, 1, 0, _g.d.ap_ar_trans._doc_no, 1, 25, 3, true, false, false);
                    this._addDateBox(0, 1, 1, 0, _g.d.ap_ar_trans._doc_date, 1, true, false);
                    this._addTextBox(1, 0, 1, 0, _g.d.ap_ar_trans._tax_doc_no, 1, 25, 0, true, false, true);
                    this._addDateBox(1, 1, 1, 0, _g.d.ap_ar_trans._tax_doc_date, 1, true, false);
                    this._addComboBox(2, 0, _g.d.ap_ar_trans._vat_type, true, _g.g._po_so_tax_type, false);
                    this._addTextBox(2, 1, 1, 0, _g.d.ap_ar_trans._doc_group, 1, 255, 1, true, false, true);
                    this._addTextBox(3, 0, 1, 0, _g.d.ap_ar_trans._cust_code, 1, 25, 1, true, false, true, true, true, _g.d.ap_ar_trans._ar_code);
                    this._addTextBox(3, 1, 1, 0, _g.d.ap_ar_trans._ar_name, 1, 10, 0, true, false, true, false);
                    this._addTextBox(4, 0, 1, 0, _g.d.ap_ar_trans._book_no, 1, 255, 1, true, false, true);
                    this._addTextBox(4, 1, 1, 0, _g.d.ap_ar_trans._payment_method, 1, 25, 0, true, false, true);
                    this._addTextBox(5, 0, 1, 0, _g.d.ap_ar_trans._sale_code, 1, 255, 1, true, false, true);
                    this._addTextBox(5, 1, 1, 0, _g.d.ap_ar_trans._department_code, 1, 10, 1, true, false, true);
                    this._addTextBox(6, 0, 1, 0, _g.d.ap_ar_trans._project_code, 1, 10, 1, true, false, true);
                    this._addTextBox(6, 1, 1, 0, _g.d.ap_ar_trans._allocate_code, 1, 10, 1, true, false, true);
                    this._addTextBox(7, 0, 1, 0, _g.d.ap_ar_trans._pay_installments, 1, 100, 0, true, false, true);
                    this._addDateBox(7, 1, 1, 0, _g.d.ap_ar_trans._delivery_date, 1, true, false);
                    this._addTextBox(8, 0, 2, 0, _g.d.ap_ar_trans._remark, 2, 255, 0, true, false, true);
                    this._addTextBox(10, 0, 1, 0, _g.d.ap_ar_trans._currency_code, 1, 255, 1, true, false, true);
                    this._addTextBox(10, 1, 1, 0, _g.d.ap_ar_trans._exchange_rate, 1, 255, 1, true, false, true);
                    break;
                case _ApArDepositControlFlagEnum.ar_deposit_cancel:
                    this._maxColumn = 2;
                    this._addTextBox(0, 0, 1, 0, _g.d.ap_ar_trans._doc_no, 1, 25, 3, true, false, false);
                    this._addDateBox(0, 1, 1, 0, _g.d.ap_ar_trans._doc_date, 1, true, false);
                    this._addTextBox(1, 0, 1, 0, _g.d.ap_ar_trans._tax_doc_no, 1, 25, 0, true, false, true);
                    this._addDateBox(1, 1, 1, 0, _g.d.ap_ar_trans._tax_doc_date, 1, true, false);
                    this._addComboBox(2, 0, _g.d.ap_ar_trans._vat_type, true, _g.g._po_so_tax_type, false);
                    this._addTextBox(2, 1, 1, 0, _g.d.ap_ar_trans._doc_group, 1, 255, 1, true, false, true);
                    this._addTextBox(3, 0, 1, 0, _g.d.ap_ar_trans._cust_code, 1, 25, 1, true, false, true, true, true, _g.d.ap_ar_trans._ar_code);
                    this._addTextBox(3, 1, 1, 0, _g.d.ap_ar_trans._ar_name, 1, 10, 0, true, false, true, false);
                    this._addTextBox(4, 0, 1, 0, _g.d.ap_ar_trans._book_no, 1, 255, 1, true, false, true);
                    this._addTextBox(4, 1, 1, 0, _g.d.ap_ar_trans._payment_method, 1, 25, 0, true, false, true);
                    this._addTextBox(5, 0, 1, 0, _g.d.ap_ar_trans._sale_code, 1, 255, 1, true, false, true);
                    this._addTextBox(5, 1, 1, 0, _g.d.ap_ar_trans._department_code, 1, 10, 1, true, false, true);
                    this._addTextBox(6, 0, 1, 0, _g.d.ap_ar_trans._project_code, 1, 10, 1, true, false, true);
                    this._addTextBox(6, 1, 1, 0, _g.d.ap_ar_trans._allocate_code, 1, 10, 1, true, false, true);
                    this._addTextBox(7, 0, 1, 0, _g.d.ap_ar_trans._pay_installments, 1, 100, 0, true, false, true);
                    this._addDateBox(7, 1, 1, 0, _g.d.ap_ar_trans._delivery_date, 1, true, false);
                    this._addTextBox(8, 0, 2, 0, _g.d.ap_ar_trans._remark, 2, 255, 0, true, false, true);
                    this._addTextBox(10, 0, 1, 0, _g.d.ap_ar_trans._currency_code, 1, 255, 1, true, false, true);
                    this._addTextBox(10, 1, 1, 0, _g.d.ap_ar_trans._exchange_rate, 1, 255, 1, true, false, true);
                    break;
            }
            this._setDataDate(_g.d.ap_ar_trans._doc_date, MyLib._myGlobal._workingDate);
            this._setDataDate(_g.d.ap_ar_trans._delivery_date, MyLib._myGlobal._workingDate);
            // AutoRun
            MyLib._myTextBox __getControlDocNo = (MyLib._myTextBox)this._getControl(_g.d.ap_ar_trans._doc_no);
            if (__getControlDocNo != null)
            {
                __getControlDocNo.textBox.Leave += new EventHandler(_docNoTextBox_Leave);
            }
            this.Invalidate();
            this.ResumeLayout();
        }

        /// <summary>
        /// Autorun
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void _docNoTextBox_Leave(object sender, EventArgs e)
        {
            _getAutoRun(this._getDataStr(_g.d.ap_ar_trans._doc_no));
        }

        public void _getAutoRun(string source)
        {
            //Debug.Print("Autorun=" + source);
            string __getDoc = source.ToUpper();
            // jead : ต้องย้ายไปเป็น Parameter เพราะมี autorun 2 แบบ คือ แบบป้อนเอง และหลังจาก save , string __getDoc = this._getDataStr(_g.d.ap_ar_trans._doc_no).ToUpper();
            // jead : this._autorun_old_doc_no = __getDoc;
            if (__getDoc.Length > 0)
            {
                string[] __getDocSplit = __getDoc.Split('-');
                // jead : __getDocSplit[1].Length == 0 เพราะว่าจะ autorun ได้ต้องเป็น AP- ถ้าเป็น AP-1 ไม่ต้อง Autorun เด้อ
                if (__getDocSplit.Length > 1)
                {
                    // jead : เอาไว้ตอนรายการต่อไป เบอร์ก็จะ autorun เอง
                    this._autorun_old_doc_no = __getDocSplit[0].ToString() + "-";
                    if (__getDocSplit[1].ToString().Length == 0)
                    {
                        // jead : ผิดเด้อ string __getDocQuery = __getDoc[0].ToString() + "-" + "z";
                        string __getDocQuery = __getDocSplit[0].ToString() + "-" + "z";
                        string _qw = " " + _g.d.ap_ar_trans._trans_flag + " = " + _ApArDepositGlobal._ApArDepositFlag(ApArDepositControlFlag).ToString()
                                    + " and " + _g.d.ap_ar_trans._trans_type + " =  " + _ApArDepositGlobalType._ApArDepositType(ApArDepositControlType).ToString()
                                    + " and " + _g.d.ap_ar_trans._doc_no + " <= '" + __getDocQuery + "'";
                        // jead : string __getDoc2 = this._getDataStr(_g.d.ap_ar_trans._doc_no).ToUpper();
                        string __getDoc3 = MyLib._myGlobal._getAutoRun(_g.d.ap_ar_trans._table, _g.d.ap_ar_trans._doc_no, _qw, this._autorun_old_doc_no, true).ToString();
                        this._setDataStr(_g.d.ap_ar_trans._doc_no, __getDoc3, "", true);
                    }
                }
            }
        }
    }

    public class _ApArDepositScreenBottomControl : MyLib._myScreen
    {
        private _ApArDepositControlFlagEnum __ApArDepositControlFlagTemp;

        public _ApArDepositControlFlagEnum ApArDepositControlFlag
        {
            set
            {
                this.__ApArDepositControlFlagTemp = value;
                this._build();
                this.Invalidate();
            }
            get
            {
                return this.__ApArDepositControlFlagTemp;
            }
        }

        public _ApArDepositScreenBottomControl()
        {
            this._build();
        }

        void _build()
        {
            this.SuspendLayout();
            this._reset();
            this._table_name = _g.d.ap_ar_trans._table;
            switch (ApArDepositControlFlag)
            {
                case _ApArDepositControlFlagEnum.ap_advance:
                    this._maxColumn = 3;
                    this._addNumberBox(0, 0, 1, 0, _g.d.ap_ar_trans._total_value, 1, 2, true);
                    this._addNumberBox(0, 1, 1, 0, _g.d.ap_ar_trans._total_net_value, 1, 2, true);
                    this._addNumberBox(0, 2, 1, 0, _g.d.ap_ar_trans._amount, 1, 2, true);
                    this._addNumberBox(1, 2, 1, 0, _g.d.ap_ar_trans._money_balance, 1, 2, true);
                    break;
                case _ApArDepositControlFlagEnum.ap_advance_cancel:
                    this._maxColumn = 3;
                    this._addNumberBox(0, 0, 1, 0, _g.d.ap_ar_trans._total_value, 1, 2, true);
                    this._addNumberBox(0, 1, 1, 0, _g.d.ap_ar_trans._total_net_value, 1, 2, true);
                    this._addNumberBox(0, 2, 1, 0, _g.d.ap_ar_trans._amount, 1, 2, true);
                    this._addNumberBox(1, 2, 1, 0, _g.d.ap_ar_trans._money_balance, 1, 2, true);
                    break;
                case _ApArDepositControlFlagEnum.ap_deposit:
                    this._maxColumn = 3;
                    this._addNumberBox(0, 0, 1, 0, _g.d.ap_ar_trans._total_value, 1, 2, true);
                    this._addNumberBox(0, 1, 1, 0, _g.d.ap_ar_trans._total_net_value, 1, 2, true);
                    this._addNumberBox(0, 2, 1, 0, _g.d.ap_ar_trans._amount, 1, 2, true);
                    this._addNumberBox(1, 2, 1, 0, _g.d.ap_ar_trans._money_balance, 1, 2, true);
                    break;
                case _ApArDepositControlFlagEnum.ap_deposit_cancel:
                    this._maxColumn = 3;
                    this._addNumberBox(0, 0, 1, 0, _g.d.ap_ar_trans._total_value, 1, 2, true);
                    this._addNumberBox(0, 1, 1, 0, _g.d.ap_ar_trans._total_net_value, 1, 2, true);
                    this._addNumberBox(0, 2, 1, 0, _g.d.ap_ar_trans._amount, 1, 2, true);
                    this._addNumberBox(1, 2, 1, 0, _g.d.ap_ar_trans._money_balance, 1, 2, true);
                    break;
                case _ApArDepositControlFlagEnum.ar_advance:
                    this._maxColumn = 3;
                    this._addNumberBox(0, 0, 1, 0, _g.d.ap_ar_trans._vat_rate, 1, 2, true);
                    this._addNumberBox(0, 1, 1, 0, _g.d.ap_ar_trans._total_vat_value, 1, 2, true);
                    this._addNumberBox(0, 2, 1, 0, _g.d.ap_ar_trans._total_value, 1, 2, true);
                    this._addNumberBox(1, 0, 1, 0, _g.d.ap_ar_trans._total_after_vat, 1, 2, true);
                    this._addNumberBox(1, 1, 1, 0, _g.d.ap_ar_trans._total_net_value, 1, 2, true);
                    this._addNumberBox(1, 2, 1, 0, _g.d.ap_ar_trans._amount, 1, 2, true);
                    this._addNumberBox(2, 2, 1, 0, _g.d.ap_ar_trans._money_balance, 1, 2, true);
                    break;
                case _ApArDepositControlFlagEnum.ar_advance_cancle:
                    this._maxColumn = 3;
                    this._addNumberBox(0, 0, 1, 0, _g.d.ap_ar_trans._vat_rate, 1, 2, true);
                    this._addNumberBox(0, 1, 1, 0, _g.d.ap_ar_trans._total_vat_value, 1, 2, true);
                    this._addNumberBox(0, 2, 1, 0, _g.d.ap_ar_trans._total_value, 1, 2, true);
                    this._addNumberBox(1, 0, 1, 0, _g.d.ap_ar_trans._total_after_vat, 1, 2, true);
                    this._addNumberBox(1, 1, 1, 0, _g.d.ap_ar_trans._total_net_value, 1, 2, true);
                    this._addNumberBox(1, 2, 1, 0, _g.d.ap_ar_trans._amount, 1, 2, true);
                    this._addNumberBox(2, 2, 1, 0, _g.d.ap_ar_trans._money_balance, 1, 2, true);
                    break;
                case _ApArDepositControlFlagEnum.ar_deposit:
                    this._maxColumn = 3;
                    this._addNumberBox(0, 0, 1, 0, _g.d.ap_ar_trans._vat_rate, 1, 2, true);
                    this._addNumberBox(0, 1, 1, 0, _g.d.ap_ar_trans._total_vat_value, 1, 2, true);
                    this._addNumberBox(0, 2, 1, 0, _g.d.ap_ar_trans._total_value, 1, 2, true);
                    this._addNumberBox(1, 0, 1, 0, _g.d.ap_ar_trans._total_after_vat, 1, 2, true);
                    this._addNumberBox(1, 1, 1, 0, _g.d.ap_ar_trans._total_net_value, 1, 2, true);
                    this._addNumberBox(1, 2, 1, 0, _g.d.ap_ar_trans._amount, 1, 2, true);
                    this._addNumberBox(2, 2, 1, 0, _g.d.ap_ar_trans._money_balance, 1, 2, true);
                    break;
                case _ApArDepositControlFlagEnum.ar_deposit_cancel:
                    this._maxColumn = 3;
                    this._addNumberBox(0, 0, 1, 0, _g.d.ap_ar_trans._vat_rate, 1, 2, true);
                    this._addNumberBox(0, 1, 1, 0, _g.d.ap_ar_trans._total_vat_value, 1, 2, true);
                    this._addNumberBox(0, 2, 1, 0, _g.d.ap_ar_trans._total_value, 1, 2, true);
                    this._addNumberBox(1, 0, 1, 0, _g.d.ap_ar_trans._total_after_vat, 1, 2, true);
                    this._addNumberBox(1, 1, 1, 0, _g.d.ap_ar_trans._total_net_value, 1, 2, true);
                    this._addNumberBox(1, 2, 1, 0, _g.d.ap_ar_trans._amount, 1, 2, true);
                    this._addNumberBox(2, 2, 1, 0, _g.d.ap_ar_trans._money_balance, 1, 2, true);
                    break;
            }
            this.Invalidate();
            this.ResumeLayout();
        }
    }

    public static class _ApArDepositGlobalType
    {
        public static int _ApArDepositType(_ApArDepositControlTypeEnum ApArDepositControlType)
        {
            switch (ApArDepositControlType)
            {
                case _ApArDepositControlTypeEnum.ap: return 1;
                case _ApArDepositControlTypeEnum.ar: return 2;

            }
            return 0;
        }
    }

    public enum _ApArDepositControlTypeEnum
    {
        /// <summary>
        /// (เจ้าหนี้)
        /// </summary>
        ap,
        /// <summary>
        /// (ลูกหนี้)
        /// </summary>
        ar
    }

    public static class _ApArDepositGlobal
    {
        public static int _ApArDepositFlag(_ApArDepositControlFlagEnum ApArDepositControlFlag)
        {
            switch (ApArDepositControlFlag)
            {
                case _ApArDepositControlFlagEnum.ap_advance: return 43;
                case _ApArDepositControlFlagEnum.ap_advance_cancel: return 44;
                case _ApArDepositControlFlagEnum.ap_deposit: return 45;
                case _ApArDepositControlFlagEnum.ap_deposit_cancel: return 46;
                case _ApArDepositControlFlagEnum.ar_advance: return 47;
                case _ApArDepositControlFlagEnum.ar_advance_cancle: return 48;
                case _ApArDepositControlFlagEnum.ar_deposit: return 49;
                case _ApArDepositControlFlagEnum.ar_deposit_cancel: return 50;
            }
            return 0;
        }
    }

    public enum _ApArDepositControlFlagEnum
    {
        /// <summary>
        /// บันทึกเงินล่วงหน้า(เจ้าหนี้)
        /// </summary>
        ap_advance,
        /// <summary>
        /// บันทึกยกเลิกเงินล่วงหน้า(เจ้าหนี้)
        /// </summary>
        ap_advance_cancel,
        /// <summary>
        /// บันทึกเงินมัดจำ(เจ้าหนี้)
        /// </summary>
        ap_deposit,
        /// <summary>
        /// บันทึกยกเลิกเงินมัดจำ(เจ้าหนี้)
        /// </summary>
        ap_deposit_cancel,
        /// <summary>
        /// บันทึกเงินล่วงหน้า(ลูกหนี้)
        /// </summary>
        ar_advance,
        /// <summary>
        /// บันทึกยกเลิกเงินล่วงหน้า(ลูกหนี้)
        /// </summary>
        ar_advance_cancle,
        /// <summary>
        /// บันทึกเงินมัดจำ(ลูกหนี้)
        /// </summary>
        ar_deposit,
        /// <summary>
        /// บันทึกยกเลิกเงินมัดจำ(ลูกหนี้)
        /// </summary>
        ar_deposit_cancel
    }
}
