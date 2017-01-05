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
    public partial class _pettyCashMasterControl : UserControl
    {
        private _pettyCashMasterControlTypeEnum __pettyCashMasterControlTypeTemp;

        public _pettyCashMasterControlTypeEnum pettyCashMasterControlType
        {
            set
            {
                if (value != _pettyCashMasterControlTypeEnum.ว่าง)
                {
                    this.__pettyCashMasterControlTypeTemp = value;
                    this._pettyCashMasterScreenTop.pettyCashMasterControlType = value;
                    //_myManageData        
                    this._myManagePettyCash._dataList._lockRecord = true;
                    this._myManagePettyCash._displayMode = 0;
                    this._myManagePettyCash._selectDisplayMode(this._myManagePettyCash._displayMode);
                    this._myManagePettyCash._manageButton = this._myToolbar;
                    this._myManagePettyCash._manageBackgroundPanel = this._myPanel1;

                    this._myManagePettyCash._autoSize = true;
                    this._myManagePettyCash._autoSizeHeight = 400;
                    this._myManagePettyCash.Invalidate();

                    //even
                    this._load();
                    //ManageData
                    // ตัวนี้เอาไว้อ้างเพื่อเป็นตัวเชื่อม (relations) เพื่อใช้ในการแก้ไข จะได้ Update ถูก Record
                    this._myManagePettyCash._dataList._referFieldAdd(_g.d.cb_petty_cash._code, 1);
                    this._myManagePettyCash._loadDataToScreen += new MyLib.LoadDataToScreen(_myManagePettyCash__loadDataToScreen);
                    this._myManagePettyCash._dataList._deleteData += new MyLib.DeleteDataEventHandler(_dataList__deleteData);
                    this._myManagePettyCash._newDataClick += new MyLib.NewDataEvent(_myManagePettyCash__newDataClick);
                    this._myManagePettyCash._discardData += new MyLib.DiscardDataEvent(_myManagePettyCash__discardData);
                    this._myManagePettyCash._clearData += new MyLib.ClearDataEvent(_myManagePettyCash__clearData);
                    //Even Screen Top
                    this._pettyCashMasterScreenTop._textBoxSearch += new MyLib.TextBoxSearchHandler(_pettyCashMasterScreenTop__textBoxSearch);
                    this._pettyCashMasterScreenTop._textBoxChanged += new MyLib.TextBoxChangedHandler(_pettyCashMasterScreenTop__textBoxChanged);
                    this.Invalidate();
                }
            }
            get
            {
                return this.__pettyCashMasterControlTypeTemp;
            }
        }

        MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();
        string _chkSearchName = "";

        public _pettyCashMasterControl()
        {
            InitializeComponent();
            if (MyLib._myGlobal._isDesignMode == false)
            {
                this._myToolbar.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            }
        }


        /// <summary>
        /// ฟังชั่น การค้น ของ Grid และ Screen 
        /// </summary>
        /// <param name="number"></param> 
        void _pettyCashMasterScreenTop__textBoxSearch(object sender)
        {

        }

        /// <summary>
        /// Even การทำงาน ของ Grid และ Screen ทั่วไปไม่เกี่ยวกับค้นหา
        /// </summary>
        /// <param name="number"></param> 
        private void _saveButton_Click(object sender, EventArgs e)
        {
            _saveData();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            Keys keyCode = (Keys)(int)keyData & Keys.KeyCode;
            if ((keyData & Keys.Control) == Keys.Control)
            {
                switch (keyCode)
                {
                    case Keys.Home:
                        {
                            this._pettyCashMasterScreenTop.Focus();
                            return true;
                        }
                }
            }
            if (keyData == Keys.Escape)
            {
                this.Dispose();
                return true;
            }
            if (keyData == Keys.F12)
            {
                _saveData();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        void _pettyCashMasterScreenTop__textBoxChanged(object sender, string name)
        {
        }

        /// <summary>
        /// Even การทำงาน ของ MyManageData
        /// </summary>
        /// <param name="number"></param>  
        void _myManagePettyCash__clearData()
        {
            this._pettyCashMasterScreenTop._clear();
            this._pettyCashMasterScreenTop._isChange = false;
        }

        bool _myManagePettyCash__discardData()
        {
            bool result = true;
            if (this._pettyCashMasterScreenTop._isChange)
            {
                if (DialogResult.No == MyLib._myGlobal._displayWarning(3, null))
                {
                    result = false;
                }
                else
                {
                    this._pettyCashMasterScreenTop._isChange = false;
                }
            }
            return (result);
        }

        void _myManagePettyCash__newDataClick()
        {
            Control codeControl = this._pettyCashMasterScreenTop._getControl(_g.d.cb_petty_cash._code);
            codeControl.Enabled = true;
            this._pettyCashMasterScreenTop._focusFirst();
        }
        /// <summary>
        /// Even การทำงาน Delete,Load,Save
        /// </summary>
        /// <param name="number"></param>
        void _dataList__deleteData(System.Collections.ArrayList selectRowOrder)
        {
            StringBuilder __myQuery = new StringBuilder();
            __myQuery.Append(MyLib._myGlobal._xmlHeader + "<node>");
            for (int __loop = 0; __loop < selectRowOrder.Count; __loop++)
            {
                MyLib._deleteDataType getData = (MyLib._deleteDataType)selectRowOrder[__loop];
                __myQuery.Append(string.Format(MyLib._myUtil._convertTextToXmlForQuery("delete from {0} {1}"), this._myManagePettyCash._dataList._tableName, getData.whereString));
            } // for
            __myQuery.Append("</node>");
            string result = _myFrameWork._queryList(MyLib._myGlobal._databaseName, __myQuery.ToString());
            if (result.Length == 0)
            {
                MyLib._myGlobal._displayWarning(1, MyLib._myGlobal._resource("สำเร็จ"));
                this._myManagePettyCash._dataList._refreshData();
            }
            else
            {
                MessageBox.Show(result, "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        bool _myManagePettyCash__loadDataToScreen(object rowData, string whereString, bool forEdit)
        {
            try
            {
                MyLib._myFrameWork myFrameWork = new MyLib._myFrameWork();
                DataSet getData = myFrameWork._query(MyLib._myGlobal._databaseName, "select * from " + _g.d.cb_petty_cash._table + whereString);
                this._pettyCashMasterScreenTop._loadData(getData.Tables[0]);
                Control codeControl = this._pettyCashMasterScreenTop._getControl(_g.d.cb_petty_cash._code);
                codeControl.Enabled = false;
                if (forEdit)
                {
                    this._pettyCashMasterScreenTop._focusFirst();
                }
                return (true);
            }
            catch (Exception)
            {
            }
            return (false);
        }

        private void _saveData()
        {
            string getEmtry = this._pettyCashMasterScreenTop._checkEmtryField();
            if (getEmtry.Length > 0)
            {
                MyLib._myGlobal._displayWarning(2, getEmtry);
            }
            else
            {
                //จับข้อมูลเข้า ArrayList ก่อน
                ArrayList getData = this._pettyCashMasterScreenTop._createQueryForDatabase();
                //เตรียมแพค xml
                StringBuilder __myQuery = new StringBuilder();
                __myQuery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                if (this._myManagePettyCash._mode == 1)
                {
                    __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery(" insert into " + _g.d.cb_petty_cash._table + " (" + getData[0].ToString() + ") values (" + getData[1].ToString() + ")"));
                }
                else
                {
                    __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery(" update " + this._myManagePettyCash._dataList._tableName + " set " + getData[2].ToString() + this._myManagePettyCash._dataList._whereString));
                }
                //                
                __myQuery.Append("</node>");
                MyLib._myFrameWork myFrameWork = new MyLib._myFrameWork();
                string result = myFrameWork._queryList(MyLib._myGlobal._databaseName, __myQuery.ToString());
                if (result.Length == 0)
                {
                    MyLib._myGlobal._displayWarning(1, null);
                    this._pettyCashMasterScreenTop._isChange = false;
                    if (this._myManagePettyCash._mode == 1)
                    {
                        this._myManagePettyCash._afterInsertData();
                    }
                    else
                    {
                        this._myManagePettyCash._afterUpdateData();
                    }
                    this._pettyCashMasterScreenTop._clear();
                    this._pettyCashMasterScreenTop._focusFirst();
                }
                else
                {
                    MessageBox.Show(result, "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        /// <summary>
        /// Even การทำงาน From Load
        /// </summary>
        /// <param name="number">ทำงานครั้งแรกที่เปิดหน้าจอ</param>
        void _load()
        {
            switch (pettyCashMasterControlType)
            {
                case _pettyCashMasterControlTypeEnum.pettycash_master:
                    this._myManagePettyCash._dataList._loadViewFormat(_g.g._search_screen_cb_petty_cash, MyLib._myGlobal._userSearchScreenGroup, true);
                    break;
            }
            this._myManagePettyCash._dataList._refreshData();
            this._myManagePettyCash._dataListOpen = true;
            this._myManagePettyCash._calcArea();
        }

    }
    public class _pettyCashMasterScreenTopControl : MyLib._myScreen
    {
        string _searchName = "";
        _g._searchChartOfAccountDialog _chartOfAccountScreen = null;

        private _pettyCashMasterControlTypeEnum _pettyCashMasterControlTypeTemp;

        public _pettyCashMasterControlTypeEnum pettyCashMasterControlType
        {
            set
            {
                this._pettyCashMasterControlTypeTemp = value;
                this._build();
                this.Invalidate();
            }
            get
            {
                return this._pettyCashMasterControlTypeTemp;
            }
        }

        public _pettyCashMasterScreenTopControl()
        {
            //this._build();
        }

        void _build()
        {
            switch (pettyCashMasterControlType)
            {
                case _pettyCashMasterControlTypeEnum.pettycash_master:
                    this._maxColumn = 2;
                    this._table_name = _g.d.cb_petty_cash._table;
                    this._addTextBox(0, 0, 1, 0, _g.d.cb_petty_cash._code, 1, 1, 0, true, false, true);
                    this._addTextBox(1, 0, 1, 0, _g.d.cb_petty_cash._name_1, 2, 1, 0, true, false, true);
                    this._addTextBox(2, 0, 1, 0, _g.d.cb_petty_cash._name_2, 2, 1, 0, true, false, true);
                    this._addNumberBox(3, 0, 1, 0, _g.d.cb_petty_cash._credit_money, 1, 2, true);
                    this._addNumberBox(3, 1, 1, 0, _g.d.cb_petty_cash._balance_money, 1, 2, true);
                    this._addTextBox(4, 0, 1, 0, _g.d.cb_petty_cash._chart_of_account, 1, 20, 1, true, false, true);
                    this._addTextBox(5, 0, 2, 0, _g.d.cb_petty_cash._remark, 4, 100, 0, true, false, true);
                    this._textBoxSearch += _pettyCashMasterScreenTopControl__textBoxSearch;
                    this._chartOfAccountScreen = new _g._searchChartOfAccountDialog();
                    this._chartOfAccountScreen._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_chartOfAccountScreen__searchEnterKeyPress);
                    this._chartOfAccountScreen._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_chartOfAccount_gridData__mouseClick);
                    break;
            }


        }

        void _pettyCashMasterScreenTopControl__textBoxSearch(object sender)
        {
            string name = ((MyLib._myTextBox)sender)._name;
            string label_name = ((MyLib._myTextBox)sender)._labelName;
            this._searchName = name;
            if (name.Equals(_g.d.cb_petty_cash._chart_of_account))
            {
                this._chartOfAccountScreen.ShowDialog();
            }

        }

        void _chartOfAccount_gridData__mouseClick(object sender, MyLib.GridCellEventArgs e)
        {
            this._chartOfAccount_search((MyLib._myGrid)sender, e._row);
        }

        void _chartOfAccountScreen__searchEnterKeyPress(MyLib._myGrid sender, int row)
        {
            this._chartOfAccount_search(sender, row);
        }

        void _chartOfAccount_search(MyLib._myGrid sender, int row)
        {
            this._chartOfAccountScreen.Close();
            string __accountCode = sender._cellGet(row, 0).ToString();
            string __accountName = sender._cellGet(row, 1).ToString();
            if (this._searchName.Equals(_g.d.cb_petty_cash._chart_of_account))
            {
                //this._setDataStr(_g.d.erp_pass_book._account_code_1, __accountCode);
                this._setDataStr(_g.d.cb_petty_cash._chart_of_account, __accountCode, __accountName, true);
            }
        }


    }
    public static class _pettyCashMasterGlobal
    {
        public static int _pettyCashMasterType(_pettyCashMasterControlTypeEnum pettyCashMasterControlType)
        {
            switch (pettyCashMasterControlType)
            {
                case _pettyCashMasterControlTypeEnum.pettycash_master: return 1;
            }
            return 0;
        }
    }

    public enum _pettyCashMasterControlTypeEnum
    {
        /// <summary>
        /// 1.PettyCash Master : กำหนดวงเงินสดย่อย
        /// </summary>
        ว่าง,
        pettycash_master
    }
}
