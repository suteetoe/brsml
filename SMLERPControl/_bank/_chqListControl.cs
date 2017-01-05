using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace SMLERPControl._bank
{
    public partial class _chqListControl : UserControl
    {
        private _chqListControlTypeEnum __chqListControlTypeTemp;

        public _chqListControlTypeEnum chqListControlType
        {
            set
            {
                if (value != _chqListControlTypeEnum.ว่าง)
                {
                    this.SuspendLayout();
                    this.__chqListControlTypeTemp = value;
                    this._chqListScreenTop._chqListControlType = value;
                    //_myManageData       

                    this._myManagechqList._dataList._lockRecord = true;
                    this._myManagechqList._displayMode = 0;
                    this._myManagechqList._selectDisplayMode(this._myManagechqList._displayMode);
                    this._myManagechqList._manageButton = this._myToolbar;
                    this._myManagechqList._manageBackgroundPanel = this._myPanel1;

                    //this._myManagechqList.Invalidate();

                    //even
                    this._load();
                    //ManageData
                    // ตัวนี้เอาไว้อ้างเพื่อเป็นตัวเชื่อม (relations) เพื่อใช้ในการแก้ไข จะได้ Update ถูก Record
                    this._myManagechqList._dataList._referFieldAdd(_g.d.cb_chq_list._chq_number, 1);
                    this._myManagechqList._dataList._referFieldAdd(_g.d.cb_chq_list._doc_ref, 1);
                    this._myManagechqList._dataList._referFieldAdd(_g.d.cb_chq_list._doc_line_number, 4);
                    this._myManagechqList._loadDataToScreen += new MyLib.LoadDataToScreen(_myManagechqList__loadDataToScreen);
                    this._myManagechqList._clearData += new MyLib.ClearDataEvent(_myManagechqList__clearData);
                    this._myManagechqList._dataList._buttonDelete.Enabled = false;
                    this._myManagechqList._dataList._buttonNew.Enabled = false;
                    this._myManagechqList._dataList._buttonNewFromTemp.Enabled = false;
                    this._myManagechqList._dataList._buttonSelectAll.Enabled = false;
                    this._myManagechqList._dataList._gridData._beforeDisplayRow += _gridData__beforeDisplayRow;
                    this._myManagechqList._calcArea();
                    this._myManagechqList._autoSize = true;
                    this._myManagechqList._autoSizeHeight = 400;

                    this._saveButton.Enabled = false;

                    this._chqMovement._chqListControlType = value;
                    this.Invalidate();
                    this.ResumeLayout(false);
                }
            }
            get
            {
                return this.__chqListControlTypeTemp;
            }
        }

        MyLib.BeforeDisplayRowReturn _gridData__beforeDisplayRow(MyLib._myGrid sender, int row, int columnNumber, string columnName, MyLib.BeforeDisplayRowReturn senderRow, MyLib._myGrid._columnType columnType, ArrayList rowData)
        {
            MyLib.BeforeDisplayRowReturn __result = senderRow;

            if (this.chqListControlType == _chqListControlTypeEnum.ทะเบียนบัตรเครดิต)
            {
                int __statusColumn = sender._findColumnByName(_g.d.cb_chq_list._table + "." + _g.d.cb_chq_list._status);

                if (__statusColumn != -1)
                {
                    // มีการนำไปใช้แล้ว
                    int __status = MyLib._myGlobal._intPhase(sender._cellGet(row, __statusColumn).ToString());
                    if (__status == 1)
                    {
                        __result.newColor = Color.LightSeaGreen;
                    }
                    else if (__status == 2)
                    {
                        __result.newColor = Color.Red;
                    }
                }
            }

            return (__result);
        }

        public _chqListControl()
        {
            InitializeComponent();
            if (MyLib._myGlobal._isDesignMode == false)
            {
                this._myToolbar.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            }
        }

        /// <summary>
        /// ฟังชั่น ทั่วไป
        /// </summary>
        /// <param name="number">autoRuning=เลขที่เอกสารอัตโนมัต,CalcTotalGrid=คำนวนผลรวมใส่ Screen Bottom</param>        
        //string _autoRunning()
        //{
        //return MyLib._myGlobal._getAutoRun(_g.d.cb_chq_list._table, _g.d.cb_chq_list._doc_no, _g.d.cb_chq_list._transection_flag, _chqListGlobal._chqListType(chqListControlType).ToString());
        //}      

        /// <summary>
        /// Even การทำงาน ของ MyManageData
        /// </summary>
        /// <param name="number"></param> 
        void _myManagechqList__clearData()
        {
            this._chqListScreenTop._clear();
            this._chqListScreenTop._isChange = false;
        }

        bool _myManagechqList__loadDataToScreen(object rowData, string whereString, bool forEdit)
        {
            try
            {
                MyLib._myFrameWork myFrameWork = new MyLib._myFrameWork();

                StringBuilder __myquery = new StringBuilder();
                __myquery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.cb_chq_list._table + whereString.Replace(_g.d.cb_chq_list._table + "." + _g.d.cb_chq_list._doc_line_number, "coalesce(" + _g.d.cb_chq_list._table + "." + _g.d.cb_chq_list._doc_line_number + ", 0)") + " and " + _g.d.cb_chq_list._chq_type + " = " + _chqListGlobal._chqListType(chqListControlType).ToString()));

                /*// load chq movement                
                string __field = MyLib._myGlobal._fieldAndComma(_g.d.cb_trans_detail._doc_date,
                    _g.d.cb_trans_detail._doc_time,
                    _g.d.cb_trans_detail._doc_no,
                    _g.d.cb_trans_detail._trans_flag,
                    _g.d.cb_trans_detail._ap_ar_code,
                    _g.d.cb_trans_detail._pass_book_code,
                    _g.d.cb_trans_detail._bank_code,
                    _g.d.cb_trans_detail._bank_branch,
                    _g.d.cb_trans_detail._sum_amount,
                    _g.d.cb_trans_detail._remark);

                string __queryChqIn = "select " + __field + " from " + _g.d.cb_trans_detail._table + " where " + _g.d.cb_trans_detail._trans_number + "=\'" + ((ArrayList)rowData)[this._myManagechqList._dataList._gridData._findColumnByName(_g.d.cb_chq_list._table + "." + _g.d.cb_chq_list._chq_number)].ToString() + "\' and " + _g.d.cb_trans_detail._doc_type + "=2 order by " + _g.d.cb_trans_detail._doc_date + " ," + _g.d.cb_trans_detail._doc_time;

                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__queryChqIn));*/

                __myquery.Append("</node>");
                string __debugQuery = __myquery.ToString();
                ArrayList _getData = myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());
                this._chqListScreenTop._loadData(((DataSet)_getData[0]).Tables[0]);

                //this._chqMovement._loadFromDataTable(((DataSet)_getData[1]).Tables[0]);

                //Control codeControl = this._chqListScreenTop._getControl(_g.d.cb_chq_list._chq_number);
                //codeControl.Enabled = false;
                string __chqNo = this._chqListScreenTop._getDataStr(_g.d.cb_chq_list._chq_number);
                if (__chqNo.Length > 0)
                {
                    string __doc_ref = this._chqListScreenTop._getDataStr(_g.d.cb_chq_list._doc_ref);
                    int __doc_line_number = MyLib._myGlobal._intPhase(((DataSet)_getData[0]).Tables[0].Rows[0][_g.d.cb_chq_list._doc_line_number].ToString());
                    this._chqMovement._load(__chqNo, __doc_ref, __doc_line_number);
                }
                if (forEdit)
                {
                    this._chqListScreenTop._focusFirst();
                }
                return (true);
            }
            catch (Exception)
            {
            }
            return (false);
        }

        /// <summary>
        /// Even การทำงาน From Load
        /// </summary>
        /// <param name="number">ทำงานครั้งแรกที่เปิดหน้าจอ</param>
        void _load()
        {
            switch (chqListControlType)
            {
                case _chqListControlTypeEnum.ทะเบียนเช็ครับ:
                    this._myManagechqList._dataList._extraWhere = _g.d.cb_chq_list._chq_type + "=" + _chqListGlobal._chqListType(chqListControlType).ToString();
                    this._myManagechqList._dataList._loadViewFormat(_g.g._search_screen_cb_เช็ครับ, MyLib._myGlobal._userSearchScreenGroup, true);
                    break;
                case _chqListControlTypeEnum.ทะเบียนเช็คจ่าย:
                    this._myManagechqList._dataList._extraWhere = _g.d.cb_chq_list._chq_type + "=" + _chqListGlobal._chqListType(chqListControlType).ToString();
                    this._myManagechqList._dataList._loadViewFormat(_g.g._search_screen_cb_เช็คจ่าย, MyLib._myGlobal._userSearchScreenGroup, true);
                    break;
                case _chqListControlTypeEnum.ทะเบียนบัตรเครดิต:
                    this._myManagechqList._dataList._extraWhere = _g.d.cb_chq_list._chq_type + "=" + _chqListGlobal._chqListType(chqListControlType).ToString();
                    this._myManagechqList._dataList._loadViewFormat(_g.g._search_screen_บัตรเครดิต, MyLib._myGlobal._userSearchScreenGroup, true);
                    break;
            }
            //this._myManagechqList._dataList._refreshData();
            this._myManagechqList._dataListOpen = true;
            this._myManagechqList._calcArea();
        }
    }

    public class _chqListScreenTopControl : MyLib._myScreen
    {
        private _chqListControlTypeEnum _chqListControlTypeTemp;
        int _buildCount = 0;

        public _chqListControlTypeEnum _chqListControlType
        {
            set
            {
                this._chqListControlTypeTemp = value;
                this._build();
                this.Invalidate();
            }
            get
            {
                return this._chqListControlTypeTemp;
            }
        }

        public _chqListScreenTopControl()
        {
        }

        void _build()
        {
            if (this._chqListControlType == _chqListControlTypeEnum.ว่าง)
            {
                return;
            }
            if (this._buildCount++ > 0)
            {
                MessageBox.Show("Screen Dup");
            }
            this._table_name = _g.d.cb_chq_list._table;
            switch (_chqListControlType)
            {
                case _chqListControlTypeEnum.ทะเบียนเช็ครับ:
                    this._maxColumn = 2;
                    this._addTextBox(0, 0, 1, 0, _g.d.cb_chq_list._chq_number, 1, 25, 0, true, false, true);
                    this._addDateBox(1, 0, 1, 0, _g.d.cb_chq_list._chq_get_date, 1, true, true);//"วันที่เก็บเช็ค" 
                    this._addDateBox(1, 1, 1, 0, _g.d.cb_chq_list._chq_due_date, 1, true, true);
                    this._addTextBox(2, 0, 1, 0, _g.d.cb_chq_list._doc_ref, 1, 10, 0, true, false, true);
                    this._addTextBox(3, 0, 1, 0, _g.d.cb_chq_list._owner_name, 1, 10, 0, true, false, true);
                    this._addNumberBox(3, 1, 1, 0, _g.d.cb_chq_list._amount, 1, 2, true);
                    this._addTextBox(4, 0, 1, 0, _g.d.cb_chq_list._bank_code, 1, 10, 0, true, false, true);
                    this._addTextBox(4, 1, 1, 0, _g.d.cb_chq_list._bank_branch, 1, 100, 0, true, false, true);
                    this._addTextBox(5, 0, 1, 0, _g.d.cb_chq_list._ap_ar_code, 1, 100, 0, true, false, true);
                    this._addTextBox(6, 0, 1, 0, _g.d.cb_chq_list._remark, 1, 10, 0, true, false, true);
                    break;
                case _chqListControlTypeEnum.ทะเบียนเช็คจ่าย:
                    this._maxColumn = 2;
                    this._addTextBox(0, 0, 1, 0, _g.d.cb_chq_list._chq_number, 1, 25, 0, true, false, true);
                    this._addDateBox(0, 1, 1, 0, _g.d.cb_chq_list._chq_due_date, 1, true, true);
                    this._addTextBox(1, 0, 1, 0, _g.d.cb_chq_list._doc_ref, 1, 25, 0, true, false, true);
                    this._addDateBox(1, 1, 1, 0, _g.d.cb_chq_list._chq_get_date, 1, true, true, true, _g.d.cb_chq_list._date_pay);
                    this._addTextBox(2, 0, 1, 0, _g.d.cb_chq_list._pass_book_code, 1, 25, 0, true, false, true);
                    this._addTextBox(3, 0, 1, 0, _g.d.cb_chq_list._bank_code, 1, 10, 0, true, false, true);
                    this._addTextBox(3, 1, 1, 0, _g.d.cb_chq_list._bank_branch, 1, 100, 0, true, false, true);
                    this._addNumberBox(4, 0, 1, 0, _g.d.cb_chq_list._amount, 1, 2, true);
                    this._addTextBox(5, 0, 1, 0, _g.d.cb_chq_list._ap_ar_code, 1, 100, 0, true, false, true);
                    this._addTextBox(6, 0, 1, 0, _g.d.cb_chq_list._remark, 1, 10, 0, true, false, true);
                    break;
                case _chqListControlTypeEnum.ทะเบียนบัตรเครดิต:
                    this._maxColumn = 2;
                    this._addTextBox(0, 0, 1, 0, _g.d.cb_chq_list._chq_number, 1, 25, 0, true, false, true, true, true, _g.d.cb_chq_list._credit_card_number);
                    this._addDateBox(1, 0, 1, 0, _g.d.cb_chq_list._chq_get_date, 1, true, true);//"วันที่เก็บเช็ค" 
                    this._addTextBox(2, 0, 1, 0, _g.d.cb_chq_list._doc_ref, 1, 10, 0, true, false, true);
                    this._addTextBox(3, 0, 1, 0, _g.d.cb_chq_list._owner_name, 1, 10, 0, true, false, true);
                    this._addNumberBox(4, 0, 1, 0, _g.d.cb_chq_list._amount, 1, 2, true);
                    this._addNumberBox(4, 1, 1, 0, _g.d.cb_chq_list._charge, 1, 2, true);
                    this._addNumberBox(5, 1, 1, 0, _g.d.cb_chq_list._sum_amount, 1, 2, true);
                    this._addTextBox(6, 0, 1, 0, _g.d.cb_chq_list._ap_ar_code, 1, 100, 0, true, false, true);
                    this._addTextBox(7, 0, 1, 0, _g.d.cb_chq_list._remark, 1, 10, 0, true, false, true);
                    break;
            }
            this.Invalidate();
            this.ResumeLayout();
        }
    }

    public static class _chqListGlobal
    {
        public static int _chqListType(_chqListControlTypeEnum chqListControlType)
        {
            switch (chqListControlType)
            {
                case _chqListControlTypeEnum.ทะเบียนเช็ครับ: return 1;
                case _chqListControlTypeEnum.ทะเบียนเช็คจ่าย: return 2;
                case _chqListControlTypeEnum.ทะเบียนบัตรเครดิต: return 3;
            }
            return 0;
        }
    }

    public enum _chqListControlTypeEnum
    {
        ว่าง,
        /// <summary>
        /// 1.in_receive : บันทึกเช็ครับ
        /// </summary>
        ทะเบียนเช็ครับ,
        /// <summary>
        /// 2.out_payment : บันทึกเช็คจ่าย
        /// </summary>
        ทะเบียนเช็คจ่าย,
        ทะเบียนบัตรเครดิต
    }
}
