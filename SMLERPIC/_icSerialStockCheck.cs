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
    public partial class _icSerialStockCheck : UserControl
    {
        private SMLERPControl._selectWarehouseAndLocationForm _selectWarehouseAndLocation = new SMLERPControl._selectWarehouseAndLocationForm(false);
        private string _wareHouseCode = "";
        private string _locationCode = "";

        public _icSerialStockCheck()
        {
            InitializeComponent();
            if (MyLib._myGlobal._isDesignMode == false)
            {
                this.toolStrip1.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
                this.toolStrip2.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            }
            _clear();
            //this._gridSerialCheckerList1._addRowEnabled = false;

            this._icSerialStockCheckScreen1._afterClear += _icSerialStockCheckScreen1__afterClear;
            this._gridSerialCheckerList1._afterCalcTotal += new MyLib.AfterCalcTotalEventHandler(_gridSerialCheckerList1__afterCalcTotal);
            this._gridSerialCheckerList1._clickSearchButton += _gridSerialCheckerList1__clickSearchButton;
            this._serialNumberTextBox.KeyPress += new KeyPressEventHandler(_barcodeTextbox_KeyPress);
            this._icSerialStockCheckScreen1._textBoxSearch += new MyLib.TextBoxSearchHandler(_icSerialStockCheckScreen1__textBoxSearch);
            this._icSerialStockCheckScreen1._textBoxChanged += new MyLib.TextBoxChangedHandler(_icSerialStockCheckScreen1__textBoxChanged);
            this._gridSerialCheckerList1._showMenuInsertAndDeleteRow = true;
            this._selectWarehouseAndLocation._closeButton.Click += (s1, e1) =>
            {
                this._selectWarehouseAndLocation.Close();
                this._wareHouseCode = "";
                this._locationCode = "";
                //
                for (int __row = 0; __row < this._selectWarehouseAndLocation._whGrid._rowData.Count; __row++)
                {
                    if (this._selectWarehouseAndLocation._whGrid._cellGet(__row, 0).ToString().Equals("1"))
                    {
                        this._wareHouseCode = this._selectWarehouseAndLocation._whGrid._cellGet(__row, _g.d.ic_warehouse._code).ToString();
                        break;
                    }
                }
                //
                for (int __row = 0; __row < this._selectWarehouseAndLocation._locationGrid._rowData.Count; __row++)
                {
                    if (this._selectWarehouseAndLocation._locationGrid._cellGet(__row, this._selectWarehouseAndLocation._fieldCheck).ToString().Equals("1"))
                    {
                        this._locationCode = this._selectWarehouseAndLocation._locationGrid._cellGet(__row, _g.d.ic_shelf._code).ToString();
                        break;
                    }
                }

                this._whlocationLabel.Text = "";
                if (this._wareHouseCode.Length > 0 && this._locationCode.Length > 0)
                {
                    this._whlocationLabel.Text = this._wareHouseCode + "/" + this._locationCode;
                }
            };
            //
            this._selectWarehouseAndLocation.ShowDialog();
            _clearScreen();
        }

        void _icSerialStockCheckScreen1__afterClear(object sender)
        {
            this._icSerialStockCheckScreen1._setDataDate(_g.d.ic_trans._doc_date, MyLib._myGlobal._workingDate);
        }

        void _gridSerialCheckerList1__clickSearchButton(object sender, MyLib.GridCellEventArgs e)
        {
            //throw new NotImplementedException();
            int __columnIccode = this._gridSerialCheckerList1._findColumnByName(_g.d.ic_trans_serial_number._table + "+" + _g.d.ic_trans_serial_number._ic_code);
            if (e._column == __columnIccode)
            {


            }
        }

        void _gridSerialCheckerList1__afterCalcTotal(object sender)
        {
            int __total_amount_column = this._gridSerialCheckerList1._findColumnByName(_g.d.ic_trans_serial_number._price);
            MyLib._myGrid._columnType __getColumn = (MyLib._myGrid._columnType)this._gridSerialCheckerList1._columnList[__total_amount_column];

            decimal __total = __getColumn._total;
            this._icSerialStockCheckScreen1._setDataNumber(_g.d.ic_trans._total_amount, __total);
        }

        string _docFormatCode = "";

        void _icSerialStockCheckScreen1__textBoxChanged(object sender, string name)
        {
            //throw new NotImplementedException();
            MyLib._myTextBox __textBox = (MyLib._myTextBox)((Control)sender).Parent;
            if (name.Equals(_g.d.ic_trans._doc_no))
            {
                string __docNo = this._icSerialStockCheckScreen1._getDataStr(_g.d.ic_trans._doc_no);

                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                DataTable __getFormat = __myFrameWork._queryShort("select " + _g.d.erp_doc_format._format + "," + _g.d.erp_doc_format._code + "," + _g.d.erp_doc_format._doc_running + " from " + _g.d.erp_doc_format._table + " where " + _g.d.erp_doc_format._code + "=\'" + __docNo + "\'").Tables[0];
                if (__getFormat.Rows.Count > 0)
                {
                    string __format = __getFormat.Rows[0][0].ToString();
                    this._docFormatCode = __getFormat.Rows[0][1].ToString();
                    string __startRunningNumber = __getFormat.Rows[0][_g.d.erp_doc_format._doc_running].ToString();

                    string __newDoc = _g.g._getAutoRun(_g.g._autoRunType.ข้อมูลรายวัน, __docNo, this._icSerialStockCheckScreen1._getDataStr(_g.d.ic_trans._doc_date).ToString(), __format, _g.g._transControlTypeEnum.สินค้า_ตรวจนับสินค้า, _g.g._transControlTypeEnum.ว่าง, _g.d.ic_trans._table, __startRunningNumber);
                    this._icSerialStockCheckScreen1._setDataStr(_g.d.ic_trans._doc_no, __newDoc, "", true);
                    this._icSerialStockCheckScreen1._setDataStr(_g.d.ic_trans._doc_format_code, _docFormatCode, "", true);
                }

                if (this._docFormatCode.Trim().Length == 0)
                {
                    string __firstString = MyLib._myGlobal._firstString(__docNo);
                    if (__firstString.Length > 0)
                    {
                        this._docFormatCode = __firstString;
                        this._icSerialStockCheckScreen1._setDataStr(_g.d.ic_trans._doc_format_code, _docFormatCode, "", true);
                    }
                }
                /*if (this._docFormatCode.Equals(MyLib._myGlobal._firstString(__docNo)) == false)
                {
                    DialogResult __message = MessageBox.Show("ประเภทเอกสารไม่สัมพันธ์กับเลขที่เอกสาร ต้องการเปลี่ยนตามเลขที่เอกสารเลยหรือไม่", "Doc Number", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1);
                    if (__message == DialogResult.Yes)
                    {
                        this._docFormatCode = MyLib._myGlobal._firstString(__docNo);
                        this._setDataStr(_g.d.ic_trans._doc_format_code, _docFormatCode, "", true);
                    }
                }*/
            }
        }

        string _searchName = "";
        MyLib._searchDataFull _searchItem = null;
        TextBox _searchTextBox;
        string _doc_format_code = "CO";

        void _icSerialStockCheckScreen1__textBoxSearch(object sender)
        {
            MyLib._myTextBox __getControl = (MyLib._myTextBox)sender;
            _searchName = __getControl._name;
            string label_name = __getControl._labelName;
            string _searchWhere = "";

            if (_searchName.Equals(_g.d.ic_trans._doc_no))
            {
                this._searchItem = new MyLib._searchDataFull();
                _searchTextBox = __getControl.textBox;
                this._searchItem._dataList._loadViewFormat(_g.g._screen_erp_doc_format, MyLib._myGlobal._userSearchScreenGroup, false);
                _searchWhere = MyLib._myGlobal._addUpper(_g.d.erp_doc_format._screen_code, false) + "=\'" + this._doc_format_code.ToString() + "\'";
            }

            if (this._searchItem._name.Length == 0)
            {
                this._searchItem._name = _searchName;
                // start search and bind event
                this._searchItem._dataList._gridData._mouseClick -= new MyLib.MouseClickHandler(_gridData__mouseClick);
                this._searchItem._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);

                this._searchItem._searchEnterKeyPress -= new MyLib.SearchEnterKeyPressEventHandler(_searchItem__searchEnterKeyPress);
                this._searchItem._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_searchItem__searchEnterKeyPress);

                MyLib._myGlobal._startSearchBox(__getControl, label_name, this._searchItem, false, true, _searchWhere);

            }

        }

        void _searchByParent(string name, int row)
        {
            int __columnNumber = 0;

            string __result = (string)this._searchItem._dataList._gridData._cellGet(row, __columnNumber);
            if (__result.Length > 0)
            {
                this._searchItem.Visible = false;
                this._icSerialStockCheckScreen1._setDataStr(this._searchName, __result, "", false);
                //this._search(true);
            }
        }


        void _searchItem__searchEnterKeyPress(MyLib._myGrid sender, int row)
        {
            //throw new NotImplementedException();
            _searchByParent("", row);
        }

        void _gridData__mouseClick(object sender, MyLib.GridCellEventArgs e)
        {
            //throw new NotImplementedException();
            MyLib._myDataList __getParent1 = (MyLib._myDataList)((MyLib._myGrid)sender).Parent;
            MyLib._searchDataFull __getParent2 = (MyLib._searchDataFull)__getParent1.Parent;

            this._searchByParent(__getParent2._name, e._row);
            SendKeys.Send("{ENTER}");
        }

        void _barcodeTextbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                this._search();
            }
        }

        void _search()
        {
            string __serialNumber = this._serialNumberTextBox.Text;


            //this._barcodeLabel.Text = __barcode;
            this._serialNumberTextBox.Text = "";
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            StringBuilder __myquery = new StringBuilder();
            __myquery.Append(MyLib._myGlobal._xmlHeader + "<node>");
            __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ic_serial._ic_code + " from " + _g.d.ic_serial._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_serial._serial_number) + "=\'" + __serialNumber + "\'"));
            //__myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ic_inventory_barcode._ic_code + " from " + _g.d.ic_inventory_barcode._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_inventory_barcode._barcode) + "=\'" + __barcode + "\'"));
            __myquery.Append("</node>");
            ArrayList __getData = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());
            DataTable __itemTable = ((DataSet)__getData[0]).Tables[0];
            //DataTable __barcodeTable = ((DataSet)__getData[1]).Tables[0];
            if (__itemTable.Rows.Count > 0)
            {
                this._loadData(__itemTable.Rows[0][_g.d.ic_serial._ic_code].ToString(), __serialNumber);
            }
            else
            {
                int __row = this._gridSerialCheckerList1._addRow();
                this._gridSerialCheckerList1._cellUpdate(__row, _g.d.ic_trans_serial_number._serial_number, __serialNumber, false);

                //if (__barcodeTable.Rows.Count > 0)
                //{
                //    string __itemCode = __barcodeTable.Rows[0][0].ToString();
                //    //this._loadData(__itemCode);
                //}
                //else
                //{
                //    //this.panel3.BackColor = Color.Red;
                //    //this._clear();
                //}
                this.panel2.BackColor = Color.Red;
                this._clear();
            }

            this._serialNumberLabel.Text = __serialNumber;
        }

        void _loadData(string itemCode, string serialNumber)
        {
            try
            {
                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                StringBuilder __query = new StringBuilder(MyLib._myGlobal._xmlHeader + "<node>");
                __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._code + "=\'" + itemCode + "\'"));
                __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.ic_serial._table + " where " + _g.d.ic_serial._serial_number + "=\'" + serialNumber + "\' and " + _g.d.ic_serial._ic_code + "=\'" + itemCode + "\'"));
                __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.ic_trans_serial_number._table + " where " + _g.d.ic_trans_serial_number._serial_number + "=\'" + serialNumber + "\' and " + _g.d.ic_serial._ic_code + "=\'" + itemCode + "\'"));
                __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ic_unit_use._stand_value + "," + _g.d.ic_unit_use._divide_value + " from " + _g.d.ic_unit_use._table + " where " + _g.d.ic_unit_use._ic_code + "=\'" + itemCode + "\' and " + _g.d.ic_unit_use._code + "= (select " + _g.d.ic_serial._ic_unit + " from " + _g.d.ic_serial._table + " where " + _g.d.ic_serial._serial_number + " =\'" + serialNumber + "\')"));
                __query.Append("</node>");

                String __debugQuery = __query.ToString();

                ArrayList __result = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __query.ToString());

                if (__result.Count > 0)
                {
                    this.panel2.BackColor = Color.Black;
                    DataTable __tableItem = ((DataSet)__result[0]).Tables[0];
                    DataTable __tableSerial = ((DataSet)__result[1]).Tables[0];
                    DataTable __tableTransSerial = ((DataSet)__result[2]).Tables[0];
                    DataTable __tableUnit = ((DataSet)__result[3]).Tables[0];

                    Boolean __found = false;
                    for (int __i = 0; __i < this._gridSerialCheckerList1._rowData.Count; __i++)
                    {
                        if (serialNumber.Equals(this._gridSerialCheckerList1._cellGet(__i, _g.d.ic_trans_serial_number._serial_number).ToString()))
                        {
                            __found = true;
                            MessageBox.Show("มีการป้อน Serial Number " + serialNumber + " ไปแล้ว", "เตือน", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            break;
                        }
                    }

                    String __itemName = "";
                    String __unitCode = "";
                    decimal __price = 0M;

                    if (__found == false)
                    {
                        __itemName = __tableItem.Rows[0][_g.d.ic_inventory._name_1].ToString();
                        __unitCode = __tableSerial.Rows[0][_g.d.ic_serial._ic_unit].ToString();
                        __price = MyLib._myGlobal._decimalPhase(__tableItem.Rows[0][_g.d.ic_inventory._average_cost].ToString());
                        // add item grid
                        int __row = this._gridSerialCheckerList1._addRow();
                        this._gridSerialCheckerList1._cellUpdate(__row, _g.d.ic_trans_serial_number._serial_number, serialNumber, false);
                        this._gridSerialCheckerList1._cellUpdate(__row, _g.d.ic_trans_serial_number._ic_code, itemCode, false);
                        this._gridSerialCheckerList1._cellUpdate(__row, _g.d.ic_trans_serial_number._ic_name, __itemName, false);
                        //this._gridSerialCheckerList1._cellUpdate(__row, _g.d.ic_trans_detail._item_code, itemCode, false);
                        this._gridSerialCheckerList1._cellUpdate(__row, _g.d.ic_trans_serial_number._unit_code, __unitCode, false);
                        this._gridSerialCheckerList1._cellUpdate(__row, _g.d.ic_trans_serial_number._price, __price, true);

                        // behide value
                        this._gridSerialCheckerList1._cellUpdate(__row, _g.d.ic_trans_detail._stand_value, MyLib._myGlobal._decimalPhase(__tableUnit.Rows[0][_g.d.ic_unit_use._stand_value].ToString()), false);
                        this._gridSerialCheckerList1._cellUpdate(__row, _g.d.ic_trans_detail._divide_value, MyLib._myGlobal._decimalPhase(__tableUnit.Rows[0][_g.d.ic_unit_use._divide_value].ToString()), false);


                        this._iccodeLabel.Text = itemCode;
                        this._icnameLabel.Text = __itemName;
                        this._icunitLabel.Text = __unitCode;
                    }
                    else
                    {
                        this._clear();
                    }
                }
                else
                {
                    this.panel2.BackColor = Color.Red;
                    _clear();
                }
            }
            catch (Exception ex)
            {

            }
        }

        void _clearScreen()
        {
            _clear();
            this._icSerialStockCheckScreen1._clear();
            this._gridSerialCheckerList1._clear();
        }

        void _clear()
        {
            this._serialNumberLabel.Text = "";
            this._iccodeLabel.Text = "";
            this._icnameLabel.Text = "";
            this._icunitLabel.Text = "";
        }

        private void _saveButton_Click(object sender, EventArgs e)
        {
            _saveData();
        }

        _gridCheckSerial _checkgrid;
        void _saveData()
        {

            int __getTransFlag = _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.สินค้า_ตรวจนับสินค้า);
            int __getTransType = _g.g._transTypeGlobal._transType(_g.g._transControlTypeEnum.สินค้า_ตรวจนับสินค้า);
            string __docNoQuery = this._icSerialStockCheckScreen1._getDataStrQuery(_g.d.ic_trans._doc_no);
            string __docDateQuery = this._icSerialStockCheckScreen1._getDataStrQuery(_g.d.ic_trans._doc_date);
            string __docTimeQuery = this._icSerialStockCheckScreen1._getDataStrQuery(_g.d.ic_trans._doc_time);
            int __inquiry_type = 0;

            bool __pass = true;
            // check
            string __checkField = this._icSerialStockCheckScreen1._checkEmtryField();
            if (__checkField.Length > 0)
            {
                MessageBox.Show("กรุณาป้อนช่องดังต่อไปนี้ \n" + __checkField, "กรุณาป้อน", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                // check empty ic_code
                for (int __row = 0; __row < this._gridSerialCheckerList1._rowData.Count; __row++)
                {
                    if (this._gridSerialCheckerList1._cellGet(__row, _g.d.ic_trans_serial_number._ic_code) == null || this._gridSerialCheckerList1._cellGet(__row, _g.d.ic_trans_serial_number._ic_code).ToString().Length == 0)
                    {
                        MessageBox.Show("กรุณาป้อนรหัสสินค้า " + this._gridSerialCheckerList1._cellGet(__row, _g.d.ic_trans_serial_number._serial_number).ToString());
                        __pass = false;
                        break;
                    }


                    if (this._gridSerialCheckerList1._cellGet(__row, _g.d.ic_trans_serial_number._unit_code) == null || this._gridSerialCheckerList1._cellGet(__row, _g.d.ic_trans_serial_number._unit_code).ToString().Length == 0)
                    {
                        MessageBox.Show("กรุณาป้อนหน่วยนับ " + this._gridSerialCheckerList1._cellGet(__row, _g.d.ic_trans_serial_number._serial_number).ToString());
                        __pass = false;
                        break;
                    }
                }

                if (__pass == true)
                {
                    MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();

                    StringBuilder __query = new StringBuilder(MyLib._myGlobal._xmlHeader + "<node>");
                    ArrayList __screenTop = this._icSerialStockCheckScreen1._createQueryForDatabase();

                    __query.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _g.d.ic_trans._table
                        + "(" + _g.d.ic_trans._trans_flag + "," + _g.d.ic_trans._trans_type + "," + __screenTop[0] + "," + _g.d.ic_trans._wh_from + "," + _g.d.ic_trans._location_from + ")"
                        + " values "
                        + "(" + __getTransFlag + "," + __getTransType + "," + __screenTop[1] + ",\'" + this._wareHouseCode + "\',\'" + this._locationCode + "\')"));

                    _checkgrid = new _gridCheckSerial();
                    _checkgrid._alterCellUpdate -= new MyLib.AfterCellUpdateEventHandler(_checkgrid__alterCellUpdate);
                    _checkgrid._alterCellUpdate += new MyLib.AfterCellUpdateEventHandler(_checkgrid__alterCellUpdate);

                    // packquery trans serial

                    // packquery trans detail
                    for (int __i = 0; __i < this._gridSerialCheckerList1._rowData.Count; __i++)
                    {
                        int __found = -1;

                        string __itemCode = this._gridSerialCheckerList1._cellGet(__i, _g.d.ic_trans_serial_number._ic_code).ToString();
                        string __itemName = this._gridSerialCheckerList1._cellGet(__i, _g.d.ic_trans_serial_number._ic_name).ToString();
                        string __unitCode = this._gridSerialCheckerList1._cellGet(__i, _g.d.ic_trans_serial_number._unit_code).ToString();
                        decimal __price = (decimal)this._gridSerialCheckerList1._cellGet(__i, _g.d.ic_trans_serial_number._price);

                        for (int __row = 0; __row < _checkgrid._rowData.Count; __row++)
                        {
                            if (__itemCode.Equals(this._checkgrid._cellGet(__row, _g.d.ic_trans_detail._item_code).ToString()))
                            {
                                __found = __row;
                                break;
                            }
                        }

                        if (__found != -1)
                        {
                            // update row มีรายการก่อนหน้า
                            // ให้ update doc_line_number เพื่อดึงรายการ

                            decimal __oldQty = (decimal)this._checkgrid._cellGet(__found, _g.d.ic_trans_detail._qty);
                            this._checkgrid._cellUpdate(__found, _g.d.ic_trans_detail._qty, __oldQty + 1, true);

                            this._gridSerialCheckerList1._cellUpdate(__i, _g.d.ic_trans_serial_number._doc_line_number, __found, false);
                            this._gridSerialCheckerList1._cellUpdate(__i, _g.d.ic_trans_serial_number._line_number, __i, false);


                        }
                        else
                        {
                            // add row
                            int __rowAdd = this._checkgrid._addRow();
                            this._checkgrid._cellUpdate(__rowAdd, _g.d.ic_trans_detail._item_code, __itemCode, false);
                            this._checkgrid._cellUpdate(__rowAdd, _g.d.ic_trans_detail._item_name, __itemName, false);
                            this._checkgrid._cellUpdate(__rowAdd, _g.d.ic_trans_detail._unit_code, __unitCode, false);
                            this._checkgrid._cellUpdate(__rowAdd, _g.d.ic_trans_detail._is_serial_number, 1, false);
                            this._checkgrid._cellUpdate(__rowAdd, _g.d.ic_trans_detail._qty, 1M, false);
                            this._checkgrid._cellUpdate(__rowAdd, _g.d.ic_trans_detail._price, __price, true);
                            this._checkgrid._cellUpdate(__rowAdd, _g.d.ic_trans_detail._stand_value, this._gridSerialCheckerList1._cellGet(__i, _g.d.ic_trans_detail._stand_value), false);
                            this._checkgrid._cellUpdate(__rowAdd, _g.d.ic_trans_detail._divide_value, this._gridSerialCheckerList1._cellGet(__i, _g.d.ic_trans_detail._divide_value), false);

                            //
                            this._gridSerialCheckerList1._cellUpdate(__i, _g.d.ic_trans_serial_number._doc_line_number, (this._checkgrid._rowData.Count -1), false);
                            this._gridSerialCheckerList1._cellUpdate(__i, _g.d.ic_trans_serial_number._line_number, __i, false);
                        }
                    }


                    // doc_line_number คือ หมายเลขอ้างอิง

                    this._gridSerialCheckerList1._updateRowIsChangeAll(true);
                    this._checkgrid._updateRowIsChangeAll(true);

                    string __fieldTransDetail = _g.d.ic_trans_detail._doc_no + "," + _g.d.ic_trans_detail._doc_date + "," + _g.d.ic_trans_detail._doc_time + "," + _g.d.ic_trans_detail._doc_date_calc + "," + _g.d.ic_trans_detail._doc_time_calc + "," + _g.d.ic_trans_detail._trans_type + "," + _g.d.ic_trans_detail._trans_flag + "," + _g.d.ic_trans_detail._wh_code + "," + _g.d.ic_trans_detail._shelf_code + "," + _g.d.ic_trans_detail._is_pos + "," + _g.d.ic_trans_detail._inquiry_type + "," + _g.d.ic_trans_detail._calc_flag + "," + _g.d.ic_trans_detail._status + "," + _g.d.ic_trans_detail._last_status + ",";
                    string __valuetransDetail = __docNoQuery + "," + __docDateQuery + "," + __docTimeQuery + "," + __docDateQuery + "," + __docTimeQuery + "," + __getTransType + "," + __getTransFlag + ",\'" + this._wareHouseCode + "\',\'" + this._locationCode + "\',0," + __inquiry_type + "," + _g.g._transCalcTypeGlobal._transStockCalcType(_g.g._transControlTypeEnum.สินค้า_ตรวจนับสินค้า) + ",1,0,";
                    __query.Append(this._checkgrid._createQueryForInsert(_g.d.ic_trans_detail._table, __fieldTransDetail, __valuetransDetail, false, true));

                    // serial trans
                    string __fieldSerialTrans = _g.d.ic_trans_detail._doc_no + "," + _g.d.ic_trans_detail._doc_date + "," + _g.d.ic_trans_detail._doc_time + "," + _g.d.ic_trans_serial_number._wh_code + "," + _g.d.ic_trans_serial_number._shelf_code + "," + _g.d.ic_trans_serial_number._trans_flag + "," + _g.d.ic_trans_serial_number._calc_flag + "," + _g.d.ic_trans_serial_number._inquiry_type + ",";
                    string __valueSerialTrans = __docNoQuery + "," + __docDateQuery + "," + __docTimeQuery + ",\'" + this._wareHouseCode + "\',\'" + this._locationCode + "\'," + __getTransFlag + "," + _g.g._transCalcTypeGlobal._transStockCalcType(_g.g._transControlTypeEnum.สินค้า_ตรวจนับสินค้า) + "," + __inquiry_type + ",";
                    __query.Append(this._gridSerialCheckerList1._createQueryForInsert(_g.d.ic_trans_serial_number._table, __fieldSerialTrans, __valueSerialTrans));
                    __query.Append("</node>");

                    string __queryStr = __query.ToString();

                    string __result = __myFrameWork._queryList(MyLib._myGlobal._databaseName, __queryStr);
                    if (__result.Length == 0)
                    {
                        MyLib._myGlobal._displayWarning(1, null);
                        _clearScreen();
                    }
                    else
                    {
                        MessageBox.Show(__result, "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }

        }

        void _checkgrid__alterCellUpdate(object sender, int row, int column)
        {
            int __columnQty = _checkgrid._findColumnByName(_g.d.ic_trans_detail._qty);
            int __columnPrice = _checkgrid._findColumnByName(_g.d.ic_trans_detail._price);

            if (column == __columnQty || column == __columnPrice)
            {
                // update amount
                decimal __qty = (decimal)_checkgrid._cellGet(row, __columnQty);
                decimal __price = (decimal)_checkgrid._cellGet(row, __columnPrice);

                decimal __amount = __qty * __price;
                _checkgrid._cellUpdate(row, _g.d.ic_trans_detail._sum_amount, __amount, false);
            }
        }

        private void _closeButton_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void _clearButton_Click(object sender, EventArgs e)
        {
            this._gridSerialCheckerList1._clear();
        }
    }

    public class _icSerialStockCheckScreen : MyLib._myScreen
    {
        public _icSerialStockCheckScreen()
        {
            int __row = 0;
            this._table_name = _g.d.ic_trans._table;
            this._maxColumn = 4;
            this._addDateBox(__row, 0, 1, 0, _g.d.ic_trans._doc_date, 1, true, false);
            this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_time, 1, 1, 0, true, false, false);
            this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._doc_no, 1, 1, 1, true, false, false);
            this._addTextBox(__row, 1, 1, 0, _g.d.ic_trans._doc_format_code, 1, 1, 0, true, false, true);
            this._addTextBox(__row, 2, 1, 0, _g.d.ic_trans._doc_ref, 1, 1, 0, true, false, true);
            this._addDateBox(__row++, 3, 1, 0, _g.d.ic_trans._doc_ref_date, 1, true, true);
            //this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._wh_from, 1, 1, 0, true, false, false);
            //this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._location_from, 1, 1, 0, true, false, true);
            //this._addCheckBox(__row, 1, _g.d.ic_trans._recheck_count, true, false);
            //this._addNumberBox(__row++, 0, 1, 0, _g.d.ic_trans._recheck_count_day, 1, 2, true, __formatNumber);
            this._addNumberBox(__row++, 0, 0, 0, _g.d.ic_trans._total_amount, 1, 2, true, MyLib._myGlobal._getFormatNumber(""));
            this._addTextBox(__row++, 0, 3, 0, _g.d.ic_trans._remark, 4, 1, 0, true, false, true);

            this._setDataDate(_g.d.ic_trans._doc_date, MyLib._myGlobal._workingDate);
            this._setDataStr(_g.d.ic_trans._doc_time, DateTime.Now.Hour.ToString("D2") + ":" + DateTime.Now.Minute.ToString("D2"));
            this._enabedControl(_g.d.ic_trans._total_amount, false);
            this._enabedControl(_g.d.ic_trans._doc_format_code, false);

            this._afterClear += new MyLib.AfterClearHandler(_icSerialStockCheckScreen__afterClear);
        }

        void _icSerialStockCheckScreen__afterClear(object sender)
        {
            this._setDataStr(_g.d.ic_trans._doc_time, DateTime.Now.Hour.ToString("D2") + ":" + DateTime.Now.Minute.ToString("D2"));
        }
    }

    public class _gridSerialCheckerList : MyLib._myGrid
    {
        public _gridSerialCheckerList()
        {
            this._table_name = _g.d.ic_trans_serial_number._table;
            this.ShowTotal = true;
            this._isEdit = true;
            this._showMenuInsertAndDeleteRow = true;

            this._addColumn(_g.d.ic_trans_serial_number._serial_number, 1, 0, 20, false, false, true);
            this._addColumn(_g.d.ic_trans_serial_number._ic_code, 1, 0, 20, false, false, true, true);
            this._addColumn(_g.d.ic_trans_serial_number._ic_name, 1, 0, 30, false, false, false);
            this._addColumn(_g.d.ic_trans_serial_number._unit_code, 1, 0, 20, false, false, true, true);
            this._addColumn(_g.d.ic_trans_serial_number._price, 3, 0, 10, false, false, true, false, "#,###,###.00");
            //this._addColumn(_g.d.ic_trans_detail._doc_no, 1, 0, 10);
            //this._addColumn(_g.d.ic_trans_detail._doc_date, 1, 0, 10);
            //this._addColumn(_g.d.ic_trans_detail._inquiry_type, 1, 0, 10);
            this._columnExtraWord(_g.d.ic_trans_serial_number._ic_code, "(F4)");
            this._columnExtraWord(_g.d.ic_trans_serial_number._unit_code, "(F5)");

            // field ซ่อน
            this._addColumn(_g.d.ic_trans_detail._stand_value, 3, 0, 0, false, true, false);
            this._addColumn(_g.d.ic_trans_detail._divide_value, 3, 0, 0, false, true, false);
            this._addColumn(_g.d.ic_trans_serial_number._line_number, 2, 0, 0, false, true, true);
            this._addColumn(_g.d.ic_trans_serial_number._doc_line_number, 2, 0, 0, false, true, true);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            const int WM_KEYDOWN = 0x100;
            const int WM_SYSKEYDOWN = 0x104;

            if (msg.Msg == WM_KEYDOWN || msg.Msg == WM_SYSKEYDOWN)
            {
                switch (keyData)
                {
                    case Keys.F4:
                        if (this._selectRow != -1)
                        {
                            if (this._selectRow <= this._rowData.Count - 1)
                            {
                                //this._searchItemDialog(_g.d.ic_trans_detail._item_code, this._selectRow);
                                this._selectItem();
                            }
                        }
                        return true;
                    case Keys.F5:
                        if (this._selectRow != -1)
                        {
                            if (this._selectRow <= this._rowData.Count - 1)
                            {
                                object __selectItemCode = this._cellGet(this._selectRow, _g.d.ic_trans_serial_number._ic_code);
                                if (__selectItemCode != null && __selectItemCode.ToString().Length > 0)
                                {
                                    this._selectUnitCode();
                                }
                                else
                                {
                                    MessageBox.Show("กรุณาเลือกสินค้า");

                                }

                            }
                        }
                        return true;
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        void _selectItem()
        {
            MyLib._searchDataFull __searchIC = new MyLib._searchDataFull();

            __searchIC.Text = MyLib._myGlobal._resource("ค้นหาสินค้า");
            __searchIC._dataList._loadViewFormat(_g.g._search_screen_ic_inventory, MyLib._myGlobal._userSearchScreenGroup, false);

            __searchIC.WindowState = FormWindowState.Maximized;
            __searchIC._dataList._extraWhere = " " + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._ic_serial_no + "=1";
            __searchIC._dataList._gridData._mouseClick += (s1, e1) =>
            {
                object __itemCode = __searchIC._dataList._gridData._cellGet(__searchIC._dataList._gridData._selectRow, _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code);
                object __itemName = __searchIC._dataList._gridData._cellGet(__searchIC._dataList._gridData._selectRow, _g.d.ic_inventory._table + "." + _g.d.ic_inventory._name_1);

                //object __unit_code = __searchIC._dataList._gridData._cellGet(__searchIC._dataList._gridData._selectRow, _g.d.ic_inventory._table + "." + _g.d.ic_inventory._unit_standard);

                __searchIC.Close();
                //this._searchItem.Dispose();
                //this._command("append:textbox:input=" + __code + "@end:textbox:input", serialNumber);
                this._cellUpdate(this._selectRow, _g.d.ic_trans_serial_number._ic_code, __itemCode, true);
                this._cellUpdate(this._selectRow, _g.d.ic_trans_serial_number._ic_name, __itemName, true);
                if (__itemCode != null)
                    _searchUnitCode(__itemCode.ToString());
            };
            __searchIC._searchEnterKeyPress += (s1, e1) =>
            {
                object __itemCode = __searchIC._dataList._gridData._cellGet(__searchIC._dataList._gridData._selectRow, _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code);
                object __itemName = __searchIC._dataList._gridData._cellGet(__searchIC._dataList._gridData._selectRow, _g.d.ic_inventory._table + "." + _g.d.ic_inventory._name_1);

                //object __unit_code = __searchIC._dataList._gridData._cellGet(__searchIC._dataList._gridData._selectRow, _g.d.ic_inventory._table + "." + _g.d.ic_inventory._unit_standard);
                //string __code = (__barCode != null) ? __barCode.ToString() : __itemCode.ToString();

                __searchIC.Close();
                //this._searchIC.Dispose();
                //this._command("append:textbox:input=" + __code + "@end:textbox:input", serialNumber);
                this._cellUpdate(this._selectRow, _g.d.ic_trans_serial_number._ic_code, __itemCode, true);
                this._cellUpdate(this._selectRow, _g.d.ic_trans_serial_number._ic_name, __itemName, true);
                if (__itemCode != null)
                    _searchUnitCode(__itemCode.ToString());
            };

            __searchIC.ShowDialog(this);
        }

        void _selectUnitCode()
        {
            MyLib._searchDataFull __searchUnit = new MyLib._searchDataFull();

            __searchUnit.Text = MyLib._myGlobal._resource("ค้นหาหน่วยนับ");
            __searchUnit._dataList._loadViewFormat(_g.g._search_master_ic_unit_use, MyLib._myGlobal._userSearchScreenGroup, false);

            //__searchUnit.WindowState = FormWindowState.Maximized;
            __searchUnit.StartPosition = FormStartPosition.CenterScreen;
            __searchUnit._dataList._extraWhere = _g.d.ic_unit_use._ic_code + "=\'" + this._cellGet(this._selectRow, _g.d.ic_trans_serial_number._ic_code).ToString() + "\'";
            __searchUnit._dataList._gridData._mouseClick += (s1, e1) =>
            {
                object __unitCode = __searchUnit._dataList._gridData._cellGet(__searchUnit._dataList._gridData._selectRow, _g.d.ic_unit_use._table + "." + _g.d.ic_unit_use._code);
                //object __itemName = __searchUnit._dataList._gridData._cellGet(__searchUnit._dataList._gridData._selectRow, _g.d.ic_unit_use._table + "." + _g.d.ic_unit_use._name_1);


                __searchUnit.Close();
                //this._searchItem.Dispose();
                //this._command("append:textbox:input=" + __code + "@end:textbox:input", serialNumber);
                this._cellUpdate(this._selectRow, _g.d.ic_trans_serial_number._unit_code, __unitCode, true);
                //this._cellUpdate(this._selectRow, _g.d.ic_trans_serial_number._ic_name, __itemName, true);
            };
            __searchUnit._searchEnterKeyPress += (s1, e1) =>
            {
                object __unitCode = __searchUnit._dataList._gridData._cellGet(__searchUnit._dataList._gridData._selectRow, _g.d.ic_unit_use._table + "." + _g.d.ic_unit_use._code);
                //object __itemName = __searchUnit._dataList._gridData._cellGet(__searchUnit._dataList._gridData._selectRow, _g.d.ic_unit_use._table + "." + _g.d.ic_unit_use._name_1);

                //string __code = (__barCode != null) ? __barCode.ToString() : __itemCode.ToString();

                __searchUnit.Close();
                //this._searchIC.Dispose();
                //this._command("append:textbox:input=" + __code + "@end:textbox:input", serialNumber);
                this._cellUpdate(this._selectRow, _g.d.ic_trans_serial_number._unit_code, __unitCode, true);
                //this._cellUpdate(this._selectRow, _g.d.ic_trans_serial_number._ic_name, __itemName, true);
            };

            __searchUnit.ShowDialog(this);
        }

        void _searchUnitCode(string itemCode)
        {
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            DataTable __result = __myFrameWork._queryShort("select " + _g.d.ic_inventory._unit_cost + " from ic_inventory where ic_inventory.code = \'" + itemCode + "\'").Tables[0];

            if (__result.Rows.Count > 0)
            {
                this._cellUpdate(this.SelectRow, _g.d.ic_trans_serial_number._unit_code, __result.Rows[0][0].ToString(), true);
            }
        }

    }

    public class _gridCheckSerial : MyLib._myGrid
    {
        public _gridCheckSerial()
        {
            this._table_name = _g.d.ic_trans_detail._table;

            this._addColumn(_g.d.ic_trans_detail._item_code, 1, 0, 10);
            this._addColumn(_g.d.ic_trans_detail._item_name, 1, 0, 10);
            this._addColumn(_g.d.ic_trans_detail._unit_code, 1, 0, 10);
            this._addColumn(_g.d.ic_trans_detail._is_serial_number, 2, 0, 10);
            this._addColumn(_g.d.ic_trans_detail._qty, 3, 0, 10);
            this._addColumn(_g.d.ic_trans_detail._price, 3, 0, 10);
            this._addColumn(_g.d.ic_trans_detail._sum_amount, 3, 0, 10);

            this._addColumn(_g.d.ic_trans_detail._stand_value, 3, 0, 0);
            this._addColumn(_g.d.ic_trans_detail._divide_value, 3, 0, 0);


        }
    }

}
