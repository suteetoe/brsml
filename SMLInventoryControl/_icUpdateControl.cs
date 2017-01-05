using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Threading;

namespace SMLInventoryControl
{
    public delegate void saveSuccess(string itemCode, String barcode);

    public partial class _icUpdateControl : UserControl
    {
        public Boolean _saveKey = true;

        private string _screenCode = "IC";
        private string _searchName = "";
        private MyLib._searchDataFull _search_data_full_pointer;
        private string _old_filed_name = "";
        private ArrayList _search_data_full_buffer = new ArrayList();
        private ArrayList _search_data_full_buffer_name = new ArrayList();
        private int _search_data_full_buffer_addr = -1;
        private TextBox _searchTextBox;
        private string _oldCode = "";
        private string _barcodeInput = "";

        Boolean _insertSuccessResult = false;
        public Boolean _insertSuccess
        {
            get
            {
                return _insertSuccessResult;
            }
        }

        public string _getOldCode
        {
            get
            {
                return this._oldCode;
            }
        }

        public string _getOldBarcode
        {
            get
            {
                return this._barcodeInput;
            }
        }

        /// <summary>
        /// ManageMode (1=insert, 2=edit)
        /// </summary>
        public int _manageMode = 1;

        public event saveSuccess _saveSuccess;

        public _icUpdateControl()
        {
            InitializeComponent();



            this._icBarcodeGrid._icTransItemGridSelectUnit._icmainGridUnit._filterUnitUse = false;
            this._icPriceFormulaGrid._icTransItemGridSelectUnit._icmainGridUnit._filterUnitUse = false;

            if (this.DesignMode == false)
            {
                //this._loadWarehouseAndLocationAuto();
                this._icMainScreen._textBoxSearch += _icMainScreen__textBoxSearch;
                this._icMainScreen._textBoxChanged += _icMainScreen__textBoxChanged;


                MyLib._myComboBox __unitType = (MyLib._myComboBox)this._icMainScreen._getControl(_g.d.ic_inventory._unit_type);
                __unitType.SelectedIndexChanged -= __unitType_SelectedIndexChanged;
                __unitType.SelectedIndexChanged += __unitType_SelectedIndexChanged;
                _changeGridEnable(__unitType);

                this._icBarcodeGrid.GetUnitCode += _icBarcodeGrid_GetUnitCode;
                this._icBarcodeGrid.GetUnitType += _icBarcodeGrid_GetUnitType;
                this._icBarcodeGrid.GetItemDesc += _icBarcodeGrid_GetItemDesc;
                this._icBarcodeGrid.GetItemCode += _icBarcodeGrid_GetItemCode;

                this._icPriceFormulaGrid.GetUnitType += _icPriceFormulaGrid_GetUnitType;
                this._icPriceFormulaGrid.GetItemCode += _icPriceFormulaGrid_GetItemCode;
                this._icPriceFormulaGrid.GetItemDesc += _icPriceFormulaGrid_GetItemDesc;
                this._icPriceFormulaGrid.GetUnitCode += _icPriceFormulaGrid_GetUnitCode;
            }

        }

        string _icPriceFormulaGrid_GetUnitCode(object sender)
        {
            return this._icMainScreen._getDataStr(_g.d.ic_inventory._unit_standard);
        }

        string _icPriceFormulaGrid_GetItemDesc(object sender)
        {
            return this._icMainScreen._getDataStr(_g.d.ic_inventory._name_1);
        }

        string _icPriceFormulaGrid_GetItemCode(object sender)
        {
            return this._icMainScreen._getDataStr(_g.d.ic_inventory._code);
        }

        int _icPriceFormulaGrid_GetUnitType(object sender)
        {
            return MyLib._myGlobal._intPhase(this._icMainScreen._getDataStr(_g.d.ic_inventory._unit_type));
        }

        string _icBarcodeGrid_GetItemCode(object sender)
        {
            return this._icMainScreen._getDataStr(_g.d.ic_inventory._code);
        }

        string _icBarcodeGrid_GetItemDesc(object sender)
        {
            return this._icMainScreen._getDataStr(_g.d.ic_inventory._name_1);
        }

        int _icBarcodeGrid_GetUnitType(object sender)
        {
            return MyLib._myGlobal._intPhase(this._icMainScreen._getDataStr(_g.d.ic_inventory._unit_type));
        }

        string _icBarcodeGrid_GetUnitCode(object sender)
        {
            return this._icMainScreen._getDataStr(_g.d.ic_inventory._unit_standard);
        }

        public void _loadData(string itemCode)
        {
            this._manageMode = 2;
            this._oldCode = itemCode;
            this._barcodeInput = itemCode;
            this._insertSuccessResult = false;

            // load data here
        }

        public void _startQuickAddItem(string itemCode)
        {
            this._manageMode = 1;
            this._oldCode = itemCode;
            this._barcodeInput = itemCode;
            this._insertSuccessResult = false;
        }

        void __unitType_SelectedIndexChanged(object sender, EventArgs e)
        {
            _changeGridEnable(sender);
        }

        void _changeGridEnable(object sender)
        {
            MyLib._myComboBox __unitType = (MyLib._myComboBox)sender;
            MyLib._myTextBox __field0 = (MyLib._myTextBox)this._icMainScreen._getControl(_g.d.ic_inventory._unit_standard);

            switch (__unitType.SelectedIndex)
            {
                case 0: // หน่วยนับเดียว
                    this._icUnitGrid.Enabled = false;
                    __field0.Enabled = false;
                    _changeGridUnitAuto();
                    break;
                case 1: // หลายหน่วยนับ
                    this._icUnitGrid.Enabled = true;
                    __field0.Enabled = true;
                    break;
            }
        }

        void _icMainScreen__textBoxChanged(object sender, string name)
        {
            MyLib._myTextBox __textBox = (MyLib._myTextBox)((Control)sender).Parent;
            if (name.Equals(_g.d.ic_inventory._code))
            {
                string __newCode = MyLib._myGlobal._codeRemove(__textBox._textFirst);
                if (__newCode.Equals(__textBox._textFirst) == false)
                {
                    __textBox._textFirst = __newCode;
                    __textBox.textBox.Invalidate();
                }
                // autorun
                string __icCode = __textBox._textFirst;
                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                DataTable __getFormat = __myFrameWork._queryShort("select " + _g.d.erp_doc_format._format + "," + _g.d.erp_doc_format._code + " from " + _g.d.erp_doc_format._table + " where " + MyLib._myGlobal._addUpper(_g.d.erp_doc_format._code) + "=\'" + __icCode.ToUpper() + "\'").Tables[0];
                if (__getFormat.Rows.Count > 0)
                {
                    string __format = __getFormat.Rows[0][0].ToString();
                    string __docFormatCode = __getFormat.Rows[0][1].ToString();
                    string __newICCode = _g.g._getAutoRun(_g.g._autoRunType.สินค้า, __icCode, MyLib._myGlobal._convertDateToString(MyLib._myGlobal._workingDate, true), __format, _g.g._transControlTypeEnum.ว่าง, _g.g._transControlTypeEnum.ว่าง);
                    this._icMainScreen._setDataStr(_g.d.ic_inventory._code, __newICCode, "", true);
                    this._search(true);
                }
                else
                {
                    if (__icCode.Length > 0)
                    {
                        try
                        {
                            string __newICCode = __icCode;
                            DataTable __dt = __myFrameWork._queryShort("select " + _g.d.ic_inventory._code + " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._code + "<\'" + __icCode + "z\' order by " + _g.d.ic_inventory._code + " desc limit 1").Tables[0];
                            if (__dt.Rows.Count > 0)
                            {
                                string __getItemCode = __dt.Rows[0][_g.d.ic_inventory._code].ToString();
                                if (__getItemCode.Length > __icCode.Length)
                                {
                                    string __s1 = __getItemCode.Substring(0, __icCode.Length);
                                    if (__s1.Equals(__icCode))
                                    {
                                        string __s2 = __getItemCode.Remove(0, __icCode.Length);
                                        int __runningNumber = (int)MyLib._myGlobal._decimalPhase(__s2);
                                        if (__runningNumber > 0)
                                        {
                                            StringBuilder __format = new StringBuilder();
                                            for (int __loop = 0; __loop < __s2.Length; __loop++)
                                            {
                                                __format.Append("0");
                                            }
                                            __newICCode = __s1 + String.Format("{0:" + __format.ToString() + "}", ((decimal)(__runningNumber + 1)));
                                            this._icMainScreen._setDataStr(_g.d.ic_inventory._code, __newICCode, "", true);
                                        }
                                    }
                                }
                            }
                        }
                        catch
                        {
                        }
                    }
                }
            }

            if (__textBox._iconNumber == 1 || __textBox._iconNumber == 4)
            {
                __textBox._textFirst = MyLib._myGlobal._codeRemove(__textBox._textFirst);
                this._searchTextBox = (TextBox)sender;
                this._searchName = name;
                this._search(true);
            }

            this._changeGridUnitAuto();
        }

        public void _clear()
        {
            this._icMainScreen._clear();
            this._icUnitGrid._clear();
            this._icBarcodeGrid._clear();
            this._icPriceFormulaGrid._clear();
        }

        string _search_screen_name(string _name)
        {
            if (_name.Equals(_g.d.ic_inventory._code)) return _g.g._search_screen_erp_doc_format;
            if (_name.Equals(_g.d.ic_inventory._unit_cost)) return _g.g._search_master_ic_unit;
            if (_name.Equals(_g.d.ic_inventory._unit_standard)) return _g.g._search_master_ic_unit;
            if (_name.Equals(_g.d.ic_inventory._income_type)) return _g.g._search_screen_income_list;
            if (_name.Equals(_g.d.ic_inventory._item_pattern)) return _g.g._search_master_ic_pattern;
            if (_name.Equals(_g.d.ic_inventory._supplier_code)) return _g.g._search_screen_ap;
            return "";
        }

        string _search_screen_name_extra_where(string _name)
        {
            if (_name.Equals(_g.d.ic_inventory._code)) return MyLib._myGlobal._addUpper(_g.d.erp_doc_format._screen_code) + "=\'" + this._screenCode + "\'";
            return "";
        }

        void _icMainScreen__textBoxSearch(object sender)
        {
            this._icMainScreen._saveLastControl();
            MyLib._myTextBox __getControl = (MyLib._myTextBox)sender;
            this._searchName = __getControl._name.ToLower();
            string label_name = __getControl._labelName;
            string _search_text_new = _search_screen_name(this._searchName);
            string __where_query = _search_screen_name_extra_where(this._searchName);

            if (!this._old_filed_name.Equals(__getControl._name.ToLower()))
            {
                Boolean __found = false;
                this._old_filed_name = __getControl._name.ToLower();
                // jead
                int __addr = 0;
                for (int __loop = 0; __loop < this._search_data_full_buffer_name.Count; __loop++)
                {
                    if (__getControl._name.Equals(((string)this._search_data_full_buffer_name[__loop]).ToString()))
                    {
                        __addr = __loop;
                        __found = true;
                        break;
                    }
                }
                if (__found == false)
                {
                    __addr = this._search_data_full_buffer_name.Add(__getControl._name);
                    MyLib._searchDataFull __searchObject = new MyLib._searchDataFull();
                    this._search_data_full_buffer.Add(__searchObject);
                }
                if (this._search_data_full_pointer != null && this._search_data_full_pointer.Visible == true)
                {
                    this._search_data_full_pointer.Visible = false;
                }
                this._search_data_full_pointer = (MyLib._searchDataFull)this._search_data_full_buffer[__addr];
                this._search_data_full_buffer_addr = __addr;
                //
            }

            if (!this._search_data_full_pointer._name.Equals(_search_text_new.ToLower()))
            {
                if (this._search_data_full_pointer._name.Length == 0)
                {
                    this._search_data_full_pointer._showMode = 0;
                    this._search_data_full_pointer._name = _search_text_new;
                    this._search_data_full_pointer._dataList._loadViewFormat(this._search_data_full_pointer._name, MyLib._myGlobal._userSearchScreenGroup, false);
                    // ถ้าจะเอา event มาแทรกใน function ให้ลบออกก่อนไม่งั้นมันจะวน
                    this._search_data_full_pointer._dataList._gridData._mouseClick -= _gridData__mouseClick;
                    this._search_data_full_pointer._dataList._gridData._mouseClick += _gridData__mouseClick;
                    this._search_data_full_pointer._searchEnterKeyPress -= _search_data_full_pointer__searchEnterKeyPress;
                    this._search_data_full_pointer._searchEnterKeyPress += _search_data_full_pointer__searchEnterKeyPress;
                }
            }
            // jead : กรณีแสดงแล้วไม่ต้องไปดึงข้อมูลมาอีก ช้า และ เสีย Fucus เดิม
            if (this._search_data_full_pointer._show == false)
            {
                MyLib._myGlobal._startSearchBox(this, __getControl, label_name, this._search_data_full_pointer, false, true, __where_query);
            }
            else
            {
                MyLib._myGlobal._startSearchBox(this, __getControl, label_name, this._search_data_full_pointer, false, false, __where_query);
            }
            // jead : ทำให้ข้อมูลเรียกใหม่ทุกครั้ง ไม่ต้องมี เพราะ _startSearchBox มันดึงให้แล้ว this._search_data_full_pointer._dataList._refreshData(); เอาออกสองรอบแล้วเด้อ
            if (__getControl._iconNumber == 1)
            {
                __getControl.Focus();
                __getControl.textBox.Focus();
            }
            else
            {
                this._search_data_full_pointer.Focus();
                this._search_data_full_pointer._firstFocus();
            }
        }

        void _search_data_full_pointer__searchEnterKeyPress(MyLib._myGrid sender, int row)
        {
            this._searchByParent(sender, row);
        }

        void _searchByParent(object sender, int row)
        {
            MyLib._myDataList __getParent1 = (MyLib._myDataList)((MyLib._myGrid)sender).Parent;
            MyLib._searchDataFull __getParent2 = (MyLib._searchDataFull)__getParent1.Parent;
            this._searchAll(__getParent2._name, row);
        }

        void _gridData__mouseClick(object sender, MyLib.GridCellEventArgs e)
        {
            MyLib._myDataList __getParent1 = (MyLib._myDataList)((MyLib._myGrid)sender).Parent;
            MyLib._searchDataFull __getParent2 = (MyLib._searchDataFull)__getParent1.Parent;
            this._searchAll(__getParent2._name, e._row);
        }

        void _searchAll(string name, int row)
        {
            string _search_text_new = name;
            string __result = "";
            if (name.Length > 0)
            {
                __result = (string)this._search_data_full_pointer._dataList._gridData._cellGet(row, 0);
                if (__result.Length != 0)
                {
                    this._search_data_full_pointer.Visible = false;
                    this._icMainScreen._setDataStr(_searchName, __result);
                }
            }
            this._search(true);
            SendKeys.Send("{TAB}");
        }

        public void _search(Boolean warning)
        {
            try
            {
                // Top Screen Search
                StringBuilder __myquery = new StringBuilder();
                __myquery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ic_unit._name_1 + " from " + _g.d.ic_unit._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_unit._code) + "=\'" + this._icMainScreen._getDataStr(_g.d.ic_inventory._unit_standard).ToUpper() + "\'"));
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ic_unit._name_1 + " from " + _g.d.ic_unit._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_unit._code) + "=\'" + this._icMainScreen._getDataStr(_g.d.ic_inventory._unit_cost).ToUpper() + "\'"));
                __myquery.Append("</node>");

                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                ArrayList _getData = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());
                _searchAndWarning(_g.d.ic_inventory._unit_standard, (DataSet)_getData[0], warning);
                _searchAndWarning(_g.d.ic_inventory._unit_cost, (DataSet)_getData[1], warning);
            }
            catch
            {
            }
        }

        void _changeGridUnitAuto()
        {
            if (this._icUnitGrid.Enabled == false)
            {
                string __unitCost = this._icMainScreen._getDataStr(_g.d.ic_inventory._unit_cost);
                string __unitStandard = this._icMainScreen._getDataStr(_g.d.ic_inventory._unit_standard);
                if (__unitCost.Equals(__unitStandard) == false)
                {
                    this._icMainScreen._setDataStr(_g.d.ic_inventory._unit_standard, __unitCost);
                }
                //
                if (__unitCost.Length > 0)
                {
                    this._icUnitGrid._clear();
                    this._icUnitGrid._addRow();
                    this._icUnitGrid._cellUpdate(0, _g.d.ic_unit_use._code, this._icMainScreen._getDataStr(_g.d.ic_inventory._unit_cost), true);
                    this._icUnitGrid._cellUpdate(0, _g.d.ic_unit_use._stand_value, 1.0M, false);
                    this._icUnitGrid._cellUpdate(0, _g.d.ic_unit_use._divide_value, 1.0M, true);
                    this._icUnitGrid._cellUpdate(0, _g.d.ic_unit_use._row_order, 1.0M, false);
                    this._icUnitGrid._cellUpdate(0, _g.d.ic_unit_use._status, 1, false);
                    this._icUnitGrid.Invalidate();
                    //
                    if (this._icBarcodeGrid._rowData.Count > 0)
                    {
                        this._icBarcodeGrid._cellUpdate(0, _g.d.ic_inventory_barcode._unit_code, this._icMainScreen._getDataStr(_g.d.ic_inventory._unit_cost), true);
                        this._icBarcodeGrid.Invalidate();
                    }
                }
            }
            else
            {
                // หลายหน่วยนับ
                string __unitCost = this._icMainScreen._getDataStr(_g.d.ic_inventory._unit_cost);
                string __unitStandard = this._icMainScreen._getDataStr(_g.d.ic_inventory._unit_standard);

                // ตรวจสอบหน่วยต้นในตารางก่อน ตารางว่ามี unit นี้หรือเปล่า หากไม่มีให้เพ่ิมเลย

                Boolean __found = false;
                for (int __row = 0; __row < this._icUnitGrid._rowData.Count; __row++)
                {
                    if (this._icUnitGrid._cellGet(__row, _g.d.ic_unit_use._code).ToString().Equals(__unitCost) == true)
                    {
                        __found = true;
                        break;
                    }
                }

                if (__found == false)
                {
                    int __rowAdd = this._icUnitGrid._addRow();
                    this._icUnitGrid._cellUpdate(__rowAdd, _g.d.ic_unit_use._code, __unitCost, true);
                    this._icUnitGrid._cellUpdate(__rowAdd, _g.d.ic_unit_use._stand_value, 1.0M, false);
                    this._icUnitGrid._cellUpdate(__rowAdd, _g.d.ic_unit_use._divide_value, 1.0M, true);
                    this._icUnitGrid._cellUpdate(__rowAdd, _g.d.ic_unit_use._row_order, 1.0M, false);
                    this._icUnitGrid._cellUpdate(__rowAdd, _g.d.ic_unit_use._status, 1, false);
                    this._icUnitGrid.Invalidate();
                }

                // ตรวจสอบ barcode กรณี เจอ barcode ว่างใส่ให้ auto เลย
                for (int __row = 0; __row < this._icBarcodeGrid._rowData.Count; __row++)
                {
                    if (this._icBarcodeGrid._cellGet(__row, _g.d.ic_inventory_barcode._unit_code).ToString().Length == 0)
                    {
                        this._icBarcodeGrid._cellUpdate(0, _g.d.ic_inventory_barcode._unit_code, this._icMainScreen._getDataStr(_g.d.ic_inventory._unit_cost), true);
                        this._icBarcodeGrid.Invalidate();
                    }
                }

                // ตรวจสอบ มีหน่วยยอดคงเหลือ อยู่หรือเปล่า หากไม่มีใส่ให้เลย
                if (__unitStandard.Length == 0)
                {
                    this._icMainScreen._setDataStr(_g.d.ic_inventory._unit_standard, __unitCost);
                }

                // ตรวจสอบหน่วยยอดคงเหลือในตาราง ใน ตารางว่ามี unit นี้หรือเปล่า หากไม่มีให้เพ่ิมเลย
                __found = false;
                for (int __row = 0; __row < this._icUnitGrid._rowData.Count; __row++)
                {
                    if (this._icUnitGrid._cellGet(__row, _g.d.ic_unit_use._code).ToString().Equals(this._icMainScreen._getDataStr(_g.d.ic_inventory._unit_standard)) == true)
                    {
                        __found = true;
                        break;
                    }
                }

                if (__found == false)
                {
                    int __rowAdd = this._icUnitGrid._addRow();
                    this._icUnitGrid._cellUpdate(__rowAdd, _g.d.ic_unit_use._code, __unitStandard, true);
                    this._icUnitGrid._cellUpdate(__rowAdd, _g.d.ic_unit_use._stand_value, 1.0M, false);
                    this._icUnitGrid._cellUpdate(__rowAdd, _g.d.ic_unit_use._divide_value, 1.0M, true);
                    this._icUnitGrid._cellUpdate(__rowAdd, _g.d.ic_unit_use._row_order, 1.0M, false);
                    this._icUnitGrid._cellUpdate(__rowAdd, _g.d.ic_unit_use._status, 1, false);
                    this._icUnitGrid.Invalidate();
                }
            }

        }

        bool _searchAndWarning(string fieldName, DataSet _dataResult, Boolean warning)
        {
            bool __result = false;
            if (_dataResult.Tables[0].Rows.Count > 0)
            {
                string __getData = _dataResult.Tables[0].Rows[0][0].ToString().Trim();
                string __getDataStr = this._icMainScreen._getDataStr(fieldName).Trim();
                this._icMainScreen._setDataStr(fieldName, __getDataStr, __getData, true);
            }
            if (this._searchTextBox != null)
            {
                if (this._searchName.CompareTo(fieldName) == 0 && this._icMainScreen._getDataStr(fieldName).Trim() != "")
                {
                    if (_dataResult.Tables[0].Rows.Count == 0 && warning)
                    {
                        MyLib._myTextBox __getTextBox = (MyLib._myTextBox)(MyLib._myTextBox)this._searchTextBox.Parent;
                        MessageBox.Show(MyLib._myGlobal._resource("ไม่พบ") + " : " + this._icMainScreen._getLabelName(fieldName), MyLib._myGlobal._resource("เตือน"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                        __getTextBox._textFirst = "";
                        __getTextBox._textSecond = "";
                        __getTextBox._textLast = "";
                        this._icMainScreen._setDataStr(fieldName, "", "", true);
                        __getTextBox.Focus();
                        __getTextBox.textBox.Focus();
                        __result = true;
                    }
                }
            }
            return __result;
        }

        /*
        public void _loadWarehouseAndLocationAuto()
        {
            if (this.DesignMode == false)
            {
                Boolean __process = true;
                if (this._icWarehouseShelfGrid._rowData.Count > 0)
                {
                    DialogResult __check = MessageBox.Show(MyLib._myGlobal._resource("ต้องการดึงข้อมูลใหม่มาแทนของเดิมหรือไม่"), "เตือน", MessageBoxButtons.YesNo);
                    if (__check == DialogResult.No)
                    {
                        __process = false;
                    }
                }
                if (__process)
                {
                    this._icWarehouseShelfGrid._clear();
                    MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                    string __query = "select *,coalesce((select " + _g.d.ic_warehouse._name_1 + " from " + _g.d.ic_warehouse._table + " where " + _g.d.ic_warehouse._table + "." + _g.d.ic_warehouse._code + "=" + _g.d.ic_shelf._whcode + "),'') as wh_name_1 from " + _g.d.ic_shelf._table + " order by " + _g.d.ic_shelf._whcode + "," + _g.d.ic_shelf._code;
                    DataSet __result = __myFrameWork._queryShort(__query);
                    for (int __row = 0; __row < __result.Tables[0].Rows.Count; __row++)
                    {
                        string __whCode = __result.Tables[0].Rows[__row][_g.d.ic_shelf._whcode].ToString();
                        string __shelfCode = __result.Tables[0].Rows[__row][_g.d.ic_shelf._code].ToString();
                        int __addr = this._icWarehouseShelfGrid._addRow();
                        this._icWarehouseShelfGrid._cellUpdate(__addr, _g.d.ic_wh_shelf._wh_code, __whCode, false);
                        this._icWarehouseShelfGrid._cellUpdate(__addr, _g.d.ic_wh_shelf._wh_name, __result.Tables[0].Rows[__row]["wh_name_1"].ToString(), false);
                        this._icWarehouseShelfGrid._cellUpdate(__addr, _g.d.ic_wh_shelf._shelf_code, __shelfCode, false);
                        this._icWarehouseShelfGrid._cellUpdate(__addr, _g.d.ic_wh_shelf._shelf_name, __result.Tables[0].Rows[__row][_g.d.ic_shelf._name_1].ToString(), false);
                    }
                    this._icWarehouseShelfGrid.Invalidate();
                }
            }
        }*/

        public void _saveData()
        {
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();

            try
            {
                string __getEmtry = this._icMainScreen._checkEmtryField();
                if (__getEmtry.Length > 0)
                {
                    MyLib._myGlobal._displayWarning(2, __getEmtry);
                }
                else
                {
                    Boolean __pass = true;
                    // ตรวจสอบหน่วยต้นทุน
                    Boolean __findUnit1 = false;
                    string __getUnit1 = this._icMainScreen._getDataStr(_g.d.ic_inventory._unit_cost);
                    if (__getUnit1.Length > 0)
                    {
                        for (int __row = 0; __row < this._icUnitGrid._rowData.Count; __row++)
                        {
                            string __getUnitCompare = this._icUnitGrid._cellGet(__row, _g.d.ic_unit._code).ToString();
                            if (__getUnit1.Equals(__getUnitCompare))
                            {
                                __findUnit1 = true;
                                break;
                            }
                        }
                    }
                    if (__findUnit1 == false)
                    {
                        MessageBox.Show(MyLib._myGlobal._resource("ไม่พบหน่วยต้นทุนในตารางหน่วยนับ"));
                        __pass = false;
                    }
                    else
                    {
                        // ตรวจสอบหน่วยมาตรฐาน
                        Boolean __findUnit2 = false;
                        string __getUnit2 = this._icMainScreen._getDataStr(_g.d.ic_inventory._unit_standard);
                        if (__getUnit2.Length > 0)
                        {
                            for (int __row = 0; __row < this._icUnitGrid._rowData.Count; __row++)
                            {
                                string __getUnitCompare = this._icUnitGrid._cellGet(__row, _g.d.ic_unit._code).ToString();
                                if (__getUnit2.Equals(__getUnitCompare))
                                {
                                    __findUnit2 = true;
                                    break;
                                }
                            }
                        }
                        if (__findUnit2 == false)
                        {
                            MessageBox.Show(MyLib._myGlobal._resource("ไม่พบหน่วยมาตรฐานในตารางหน่วยนับ"));
                            __pass = false;
                        }
                        else
                        {
                            // ตรวจสอบ wharehouse+shelf
                            //Boolean __findWhShef = false;
                            //for (int __row = 0; __row < this._icMainPanel._icmainGridBranch._rowData.Count; __row++)
                            //{
                            //    string __getWhCode = this._icMainPanel._icmainGridBranch._cellGet(__row, _g.d.ic_wh_shelf._wh_code).ToString().Trim();
                            //    string __getShelfCode = this._icMainPanel._icmainGridBranch._cellGet(__row, _g.d.ic_wh_shelf._shelf_code).ToString().Trim();
                            //    if (__getWhCode.Length > 0 && __getWhCode.Length > 0)
                            //    {
                            //        __findWhShef = true;
                            //        break;
                            //    }
                            //}
                            //if (__findWhShef == false)
                            //{
                            //    Form __ANBridgeCenterAdmin = ((System.Windows.Forms.ContainerControl)(this._icMainPanel._icmainGridBranch)).ParentForm; //somruk
                            //    if (__ANBridgeCenterAdmin.GetType().ToString() == "ANBridgeCenterAdmin._mainScreen")
                            //    {
                            //        __pass = true;
                            //    }
                            //    else
                            //    {
                            //        MessageBox.Show(MyLib._myGlobal._resource("ยังไม่บันทึกคลังที่เก็บ"));
                            //        __pass = false;
                            //    }

                            //}
                        }
                    }
                    //
                    if (__pass)
                    {
                        // Save
                        StringBuilder __myQuery = new StringBuilder();
                        __myQuery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                        __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from ic_inventory_detail where not exists (select code from ic_inventory where ic_inventory.code=ic_inventory_detail.ic_code)"));
                        ArrayList __getDataScreenTop = this._icMainScreen._createQueryForDatabase(); //ic_inventory    

                        // ArrayList __getDataScreenAccount = this._icmainScreenAccount._createQueryForDatabase(); //ic Account                anek
                        string __fieldList = _g.d.ic_inventory_detail._ic_code + " , ";
                        string __dataList = this._icMainScreen._getDataStrQuery(_g.d.ic_inventory._code) + " , ";
                        string __itemCode = this._icMainScreen._getDataStr(_g.d.ic_inventory._code);

                        // 
                        if (this._manageMode == 1)
                        {
                            this._icUnitGrid._updateRowIsChangeAll(true);
                            __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _g.d.ic_inventory._table + " (" + __getDataScreenTop[0].ToString() + "," + _g.d.ic_inventory._update_detail + "," + _g.d.ic_inventory._update_price + ") values (" + __getDataScreenTop[1].ToString() + ", 1, 1)"));
                            __myQuery.Append(this._icUnitGrid._createQueryForInsert(_g.d.ic_unit_use._table, __fieldList, __dataList));

                            // __myQuery.Append("<query>insert into " + _g.d.ic_inventory_detail._table + " (" + __fieldList.ToString() + __getDataScreenAccount[0].ToString() + ") values (" + __dataList.ToString() + __getDataScreenAccount[1].ToString() + ")</query>");
                            //__myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _g.d.ic_inventory_detail._table + " (" + _g.d.ic_inventory_detail._ic_code + "," + _g.d.ic_inventory_detail._have_point + ") values (" + this._icMainPanel._icmainScreenTop._getDataStrQuery(_g.d.ic_inventory._code) + ", " + this._icMainPanel._icmainScreenTop._getDataStrQuery(_g.d.ic_inventory._have_point) + ")"));
                            // BarCode
                            this._icBarcodeGrid._updateRowIsChangeAll(true);
                            __myQuery.Append(this._icBarcodeGrid._createQueryForInsert(_g.d.ic_inventory_barcode._table, __fieldList, __dataList, false));

                            // all warehouse shelf
                            __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _g.d.ic_wh_shelf._table + "(" + _g.d.ic_wh_shelf._ic_code + "," + _g.d.ic_wh_shelf._shelf_code + "," + _g.d.ic_wh_shelf._wh_code + ") select \'" + __itemCode.ToUpper() + "\', " + _g.d.ic_shelf._code + ", " + _g.d.ic_shelf._whcode + " from " + _g.d.ic_shelf._table + " "));

                            // price formula
                            this._icPriceFormulaGrid._updateRowIsChangeAll(true);
                            __myQuery.Append(this._icPriceFormulaGrid._createQueryForInsert(_g.d.ic_inventory_price_formula._table, _g.d.ic_inventory_price_formula._ic_code + ",", __dataList, false));


                        }
                        else
                        {
                            string __whereString = " where " + _g.d.ic_inventory._code + "=\'" + __itemCode.ToUpper() + "\'";
                            __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _g.d.ic_inventory._table + " set " + __getDataScreenTop[2].ToString() + " " + __whereString));

                            this._icUnitGrid._updateRowIsChangeAll(true);
                            //string __fieldUpdate = _g.d.ic_inventory_detail._ic_code + "=" + this._icmainScreenTop._getDataStrQuery(_g.d.ic_inventory._code);
                            __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.ic_unit_use._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_inventory_detail._ic_code) + " in ( \'" + _oldCode.ToUpper() + "\',\'" + __itemCode.ToUpper() + "\')"));
                            __myQuery.Append(this._icUnitGrid._createQueryForInsert(_g.d.ic_unit_use._table, __fieldList, __dataList, false));

                            // BarCode
                            __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.ic_inventory_barcode._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_inventory_barcode._ic_code) + " in ( \'" + _oldCode.ToUpper() + "\',\'" + __itemCode.ToUpper() + "\')"));
                            this._icBarcodeGrid._updateRowIsChangeAll(true);
                            __myQuery.Append(this._icBarcodeGrid._createQueryForInsert(_g.d.ic_inventory_barcode._table, __fieldList, __dataList, false));

                            // toe update have_point
                            //__myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _g.d.ic_inventory_detail._table + " set " + _g.d.ic_inventory_detail._have_point + "=" + this._icMainPanel._icmainScreenTop._getDataStrQuery(_g.d.ic_inventory._have_point) + " where " + MyLib._myGlobal._addUpper(_g.d.ic_inventory_detail._ic_code) + "=\'" + __itemCode + "\'"));
                            // price formula
                            __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.ic_inventory_price_formula._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_inventory_price_formula._ic_code) + " =\'" + __itemCode.ToUpper() + "\' "));
                            this._icPriceFormulaGrid._updateRowIsChangeAll(true);
                            __myQuery.Append(this._icPriceFormulaGrid._createQueryForInsert(_g.d.ic_inventory_price_formula._table, _g.d.ic_inventory_price_formula._ic_code + ",", __dataList, false));

                        }
                        __myQuery.Append("</node>");

                        string __debugQuery = __myQuery.ToString();

                        string __result = __myFrameWork._queryList(MyLib._myGlobal._databaseName, __myQuery.ToString());
                        if (__result.Length == 0)
                        {
                            MyLib._myGlobal._displayWarning(1, null);
                            this._oldCode = __itemCode;
                            // Update ชื่อหน่วยนับ
                            _g._utils __utils = new _g._utils();
                            __utils._updateInventoryMaster(__itemCode);
                            Thread __thread = new Thread(new ThreadStart(__utils._updateInventoryMasterFunction));
                            __thread.Start();

                            _insertSuccessResult = true;
                            if (this._saveSuccess != null)
                            {
                                this._saveSuccess(__itemCode, this._barcodeInput);
                            }

                        }
                        else
                        {
                            MessageBox.Show(__result, "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception __e)
            {
                MessageBox.Show(__e.Message);
            }



        }

        private void _saveButton_Click_1(object sender, EventArgs e)
        {
            _saveData();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F12)
            {
                if (this._saveKey == true)
                {
                    _saveData();
                    return true;
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

    }
}
