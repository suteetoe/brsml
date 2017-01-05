using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLPOSControl._food
{
    public partial class _kitchenControl : UserControl
    {
        SMLInventoryControl._itemSearchLevelControl _itemSearchLevel = new SMLInventoryControl._itemSearchLevelControl();

        public _kitchenControl()
        {
            InitializeComponent();
            if (MyLib._myGlobal._isDesignMode == false)
            {
                this._toolStrip.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
                this.toolStrip1.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            }
            //
            this._myScreen1._autoUpperString = false;
            this._myScreen1._maxColumn = 2;
            this._myScreen1._table_name = _g.d.kitchen_master._table;
            this._myScreen1._addTextBox(0, 0, _g.d.kitchen_master._code, 10);
            this._myScreen1._addTextBox(0, 1, _g.d.kitchen_master._name_1, 10);
            this._myScreen1._addCheckBox(1, 0, _g.d.kitchen_master._print_to_kitchen, true, false);
            this._myScreen1._addCheckBox(1, 1, _g.d.kitchen_master._print_summery, true, false);
            this._myScreen1._addComboBox(2, 0, _g.d.kitchen_master._printer_mode, true, new string[] { _g.d.kitchen_master._printer_mode_1, _g.d.kitchen_master._printer_mode_2, _g.d.kitchen_master._printer_mode_3 }, true);
            this._myScreen1._addTextBox(2, 1, 1, 0, _g.d.kitchen_master._printer_name, 1, 0, 1, true, false, false, true, true);
            this._myScreen1._addTextBox(3, 0, _g.d.kitchen_master._speech_serial, 10);
            this._myScreen1._addCheckBox(4, 0, _g.d.kitchen_master._printer_manual_feed, false, true);
            //this._myScreen1._addCheckBox(3, 1, _g.d.kitchen_master._barcode_print, false, true);
            this._myScreen1._addCheckBox(4, 1, _g.d.kitchen_master._print_order_for_check, false, true);
            //this._myScreen1._addCheckBox(4, 1, _g.d.kitchen_master._print_order_per_unit, false, true);
            this._myScreen1._addCheckBox(5, 0, _g.d.kitchen_master._print_tableopen, false, true);
            this._myScreen1._addCheckBox(5, 1, _g.d.kitchen_master._print_number_order_check, false, true);
            this._myScreen1._addCheckBox(6, 0, _g.d.kitchen_master._print_order_number_barcode, false, true);

            this._myScreen1._textBoxSearch += new MyLib.TextBoxSearchHandler(_myScreen1__textBoxSearch);
            //this._myScreen1._addComboBox(2, 0, _g.d.kitchen_master._printer_name, true, _printers.ToArray(), false);
            //
            // toe
            this._myPrintOptionScreen._table_name = _g.d.kitchen_master._table;
            this._myPrintOptionScreen._maxColumn = 2;
            this._myPrintOptionScreen._addTextBox(0, 0, 1, 0, _g.d.kitchen_master._slip_header_font, 1, 0, 1, true, false);
            this._myPrintOptionScreen._addComboBox(0, 1, _g.d.kitchen_master._print_language_index, true, new string[] { _g.d.kitchen_master._print_thai, _g.d.kitchen_master._print_english, _g.d.kitchen_master._print_malayu, _g.d.kitchen_master._print_chainese, _g.d.kitchen_master._print_india, _g.d.kitchen_master._print_lao }, true, _g.d.kitchen_master._print_language_index, true);

            this._myPrintOptionScreen._addTextBox(1, 0, 1, 0, _g.d.kitchen_master._slip_sub_header_font, 1, 0, 1, true, false);
            this._myPrintOptionScreen._addCheckBox(1, 1, _g.d.kitchen_master._print_barcode, false, true);

            this._myPrintOptionScreen._addTextBox(2, 0, 1, 0, _g.d.kitchen_master._slip_detail_font, 1, 0, 1, true, false);
            this._myPrintOptionScreen._addCheckBox(2, 1, _g.d.kitchen_master._no_price_print, false, true);

            this._myPrintOptionScreen._addNumberBox(3, 0, 1, 0, _g.d.kitchen_master._printer_slip_width, 1, 0, true);
            this._myPrintOptionScreen._addCheckBox(3, 1, _g.d.kitchen_master._no_number_order_for_check, false, true);

            this._myPrintOptionScreen._addCheckBox(4, 0, _g.d.kitchen_master._print_copy, false, true);
            this._myPrintOptionScreen._addCheckBox(4, 1, _g.d.kitchen_master._hide_table_footer_bill, false, true);

            this._myPrintOptionScreen._addComboBox(5, 0, _g.d.kitchen_master._print_copy_mode, true, new string[] { _g.d.kitchen_master._printer_mode_1, _g.d.kitchen_master._printer_mode_2, _g.d.kitchen_master._printer_mode_3 }, true);
            this._myPrintOptionScreen._addCheckBox(5, 1, _g.d.kitchen_master._hide_repeat_order_bill, false, true);

            this._myPrintOptionScreen._addTextBox(6, 0, 1, 0, _g.d.kitchen_master._print_copy_name, 1, 0, 1, true, false, false, true, true);


            this._myPrintOptionScreen._textBoxSearch += new MyLib.TextBoxSearchHandler(_myPrintOptionScreen__textBoxSearch);
            this._myPrintOptionScreen._checkBoxChanged += _myPrintOptionScreen__checkBoxChanged;

            this._itemSearchLevel.Dock = DockStyle.Fill;
            this._itemLevelPanel.Controls.Add(this._itemSearchLevel);
            _itemSearchLevel._renewItemButton.Click += new EventHandler(_renewItemButton_Click);
            _itemSearchLevel._menuItemClick += new SMLInventoryControl.MenuItemClick(_itemSearchLevel__menuItemClick);
            //
            this._orderComputerGrid._table_name = _g.d.kitchen_master_order_id._table;
            this._myPrintOptionScreen._autoUpperString = false;
            this._orderComputerGrid._addColumn(_g.d.kitchen_master_order_id._order_device, 1, 20, 20);
            this._orderComputerGrid._addColumn(_g.d.kitchen_master_order_id._order_device_name, 1, 20, 20);
            this._orderComputerGrid._addColumn(_g.d.kitchen_master_order_id._selected, 11, 10, 10);
            this._orderComputerGrid._isEdit = false;
            this._orderComputerGrid._calcPersentWidthToScatter();
            //
            this._itemGrid._table_name = _g.d.kitchen_master_item._table;
            this._itemGrid._addColumn(_g.d.kitchen_master_item._food_code, 1, 20, 20);
            this._itemGrid._addColumn(_g.d.kitchen_master_item._food_name, 1, 40, 40);
            this._itemGrid._isEdit = false;
            this._itemGrid._calcPersentWidthToScatter();

            this._pos_id_grid._table_name = _g.d.kitchen_master_pos_id._table;
            this._pos_id_grid._addColumn(_g.d.kitchen_master_pos_id._pos_id, 1, 20, 20);
            this._pos_id_grid._addColumn(_g.d.kitchen_master_pos_id._pos_number, 1, 20, 20);
            this._pos_id_grid._addColumn(_g.d.kitchen_master_pos_id._selected, 11, 10, 10);
            this._pos_id_grid._isEdit = false;
            this._pos_id_grid._calcPersentWidthToScatter();

            // ดึงตามกลุ่ม
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            DataTable __itemGroup = __myFrameWork._queryShort("select distinct " + _g.d.ic_inventory._group_main + " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._group_main + "<>\'\' order by " + _g.d.ic_inventory._group_main).Tables[0];
            for (int __row = 0; __row < __itemGroup.Rows.Count; __row++)
            {
                this._itemGroup.Items.Add(__itemGroup.Rows[__row][0].ToString());
            }
            this._itemGroup.SelectedIndexChanged += new EventHandler(_itemGroup_SelectedIndexChanged);

            if (_g.g._companyProfile._print_packing_pos_order == false)
            {
                this._myTabControl.TabPages.RemoveAt(1);
            }

            _printerOptionCheckboxChange();
        }

        void _printerOptionCheckboxChange()
        {
            Boolean __enable = this._myPrintOptionScreen._getDataStr(_g.d.kitchen_master._print_copy).Equals("1");

            this._myPrintOptionScreen._enabedControl(_g.d.kitchen_master._print_copy_mode, __enable);
            this._myPrintOptionScreen._enabedControl(_g.d.kitchen_master._print_copy_name, __enable);
        }

        void _myPrintOptionScreen__checkBoxChanged(object sender, string name)
        {
            _printerOptionCheckboxChange();
        }

        void _itemGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            string __groupCode = this._itemGroup.SelectedItem.ToString();
            this._loadItem(2, __groupCode);
        }

        MyLib._myTextBox __textboxSearch;
        string _searchName;
        void _myPrintOptionScreen__textBoxSearch(object sender)
        {
            __textboxSearch = (MyLib._myTextBox)sender;
            _searchName = __textboxSearch._name.ToLower();

            if (_searchName.Equals(_g.d.kitchen_master._slip_header_font) ||
                _searchName.Equals(_g.d.kitchen_master._slip_sub_header_font) ||
                _searchName.Equals(_g.d.kitchen_master._slip_detail_font))
            {
                FontDialog __dialog = new FontDialog();
                try
                {
                    string __fontinitStr = this._myPrintOptionScreen._getDataStr(_searchName);
                    if (__fontinitStr.Length > 0)
                    {
                        __dialog.Font = new Font(__fontinitStr.Split(',')[0].ToString(), (float)MyLib._myGlobal._decimalPhase(__fontinitStr.Split(',')[1].ToString()));
                    }
                }
                catch
                {
                }
                DialogResult __result = __dialog.ShowDialog(MyLib._myGlobal._mainForm);

                if (__result == DialogResult.OK)
                {
                    string __fontStr = string.Format("{0},{1}", __dialog.Font.Name, __dialog.Font.Size.ToString());
                    this._myPrintOptionScreen._setDataStr(_searchName, __fontStr);
                }
            }
            else if (_searchName.Equals(_g.d.kitchen_master._print_copy_name))
            {
                // select printer name
                _myPrinterSearchDialog __dialog = new _myPrinterSearchDialog();
                __dialog.Text = "ค้นหาเครื่องพิมพ์";
                __dialog._beforeClose += (s1, e1) =>
                {
                    _myPrinterSearchDialog _search = (_myPrinterSearchDialog)s1;
                    if (_search.DialogResult == DialogResult.Yes)
                    {
                        MyLib._myComboBox __printerSelectCombo = (MyLib._myComboBox)_search._dialogScreen._getControl(_g.d.kitchen_master._table + "." + _g.d.kitchen_master._printer_name);
                        string __printerSelect = __printerSelectCombo.Text;
                        this._myPrintOptionScreen._setDataStr(_g.d.kitchen_master._print_copy_name, __printerSelect);
                    }

                };
                __dialog.ShowDialog();
            }

        }

        void _myScreen1__textBoxSearch(object sender)
        {
            MyLib._myTextBox __textbox = (MyLib._myTextBox)sender;

            if (__textbox._name.ToLower().Equals(_g.d.kitchen_master._printer_name))
            {
                // start searh printer
                _myPrinterSearchDialog __dialog = new _myPrinterSearchDialog();
                __dialog.Text = "ค้นหาเครื่องพิมพ์";
                __dialog._beforeClose += (s1, e1) =>
                {
                    _myPrinterSearchDialog _search = (_myPrinterSearchDialog)s1;
                    if (_search.DialogResult == DialogResult.Yes)
                    {
                        MyLib._myComboBox __printerSelectCombo = (MyLib._myComboBox)_search._dialogScreen._getControl(_g.d.kitchen_master._table + "." + _g.d.kitchen_master._printer_name);
                        string __printerSelect = __printerSelectCombo.Text;
                        this._myScreen1._setDataStr(_g.d.kitchen_master._printer_name, __printerSelect);
                    }

                };
                __dialog.ShowDialog();
            }
        }

        void _itemSearchLevel__menuItemClick(object sender, EventArgs e)
        {
            SMLInventoryControl._itemSearchLevelMenuControl __item = (SMLInventoryControl._itemSearchLevelMenuControl)sender;
            Boolean __found = false;
            for (int __loop = 0; __loop < this._itemGrid._rowData.Count; __loop++)
            {
                string __itemCode = this._itemGrid._cellGet(__loop, _g.d.kitchen_master_item._food_code).ToString();
                if (__itemCode.Equals(__item._itemCode))
                {
                    __found = true;
                    break;
                }
            }
            if (__found == false)
            {
                int __addr = this._itemGrid._addRow();
                this._itemGrid._cellUpdate(__addr, _g.d.kitchen_master_item._food_code, __item._itemCode, false);
                this._itemGrid._cellUpdate(__addr, _g.d.kitchen_master_item._food_name, __item._itemName, false);
                this._itemGrid._gotoCell(__addr, 0);
            }
        }

        void _renewItemButton_Click(object sender, EventArgs e)
        {
            this._itemGrid._clear();
        }

        public void _reLoad()
        {
            this._orderComputerGrid._clear();
            this._itemGrid._clear();
            this._gridPrintCopy._clear();
            this._gridCancelCopy._clear();

            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            StringBuilder __myquery = new StringBuilder(MyLib._myGlobal._xmlHeader + "<node>");
            __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.order_device_id._device_id + " as " + _g.d.kitchen_master_order_id._order_device + "," + _g.d.order_device_id._device_name + " as " + _g.d.kitchen_master_order_id._order_device_name + " from " + _g.d.order_device_id._table + " order by " + _g.d.order_device_id._device_id));
            __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.POS_ID._POS_ID + " as " + _g.d.kitchen_master_pos_id._pos_id + "," + _g.d.POS_ID._MACHINECODE + " as " + _g.d.kitchen_master_pos_id._pos_number + " from " + _g.d.POS_ID._table + " order by " + _g.d.POS_ID._POS_ID));
            __myquery.Append("</node>");
            ArrayList __getData = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());
            this._orderComputerGrid._loadFromDataTable(((DataSet)__getData[0]).Tables[0]);

            this._pos_id_grid._clear();
            this._pos_id_grid._loadFromDataTable(((DataSet)__getData[1]).Tables[0]);

        }

        private void _deleteRowButton_Click(object sender, EventArgs e)
        {
            try
            {
                this._itemGrid._rowData.RemoveAt(this._itemGrid._selectRow);
                this._itemGrid.Invalidate();
            }
            catch
            {
            }
        }

        private void _restartButton_Click(object sender, EventArgs e)
        {
            this._itemGrid._clear();
        }

        private void _getFoodButton_Click(object sender, EventArgs e)
        {
            this._loadItem(0, "");
        }

        // mode 0=สินค้า,1=เครื่องดื่ม,3=ตามกลุ่ม
        void _loadItem(int mode, string groupCode)
        {
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            StringBuilder __myquery = new StringBuilder(MyLib._myGlobal._xmlHeader + "<node>");
            string __where = "";
            switch (mode)
            {
                case 0: this._itemGrid._clear();
                    __where = _g.d.ic_inventory._drink_type + "=0 or " + _g.d.ic_inventory._drink_type + " is null";
                    break;
                case 1: this._itemGrid._clear();
                    __where = _g.d.ic_inventory._drink_type + "=1";
                    break;
                case 2: __where = _g.d.ic_inventory._group_main + "=\'" + groupCode + "\'"; break;
            }
            __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ic_inventory._code + " as " + _g.d.kitchen_master_item._food_code + "," + _g.d.ic_inventory._name_1 + " as " + _g.d.kitchen_master_item._food_name + " from " + _g.d.ic_inventory._table + " where " + __where + " order by " + _g.d.ic_inventory._code));
            __myquery.Append("</node>");
            ArrayList __getData = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());
            this._itemGrid._loadFromDataTable(((DataSet)__getData[0]).Tables[0]);
        }

        private void _getDrinkButton_Click(object sender, EventArgs e)
        {
            this._loadItem(1, "");
        }

    }

    public class _myPrinterSearchDialog : MyLib._myDialogForm
    {
        public _myPrinterSearchDialog()
        {
            //ManagementObjectSearcher __printerList = new ManagementObjectSearcher("SELECT * FROM Win32_Printer");

            int __default = 0;
            int __count = 0;
            List<string> _printers = new List<string>();
            //foreach (ManagementObject __getPrinter in __printerList.Get())
            foreach (MyLib._printerListClass __getPrinter in MyLib._myGlobal._printerList)
            {
                string __printerName = __getPrinter._printerName;
                if (__getPrinter._isDefault)
                {
                    __default = __count;
                }
                _printers.Add(__printerName);
                __count++;
            }

            this._dialogScreen._maxColumn = 1;
            this._dialogScreen._addComboBox(0, 0, _g.d.kitchen_master._table + "." + _g.d.kitchen_master._printer_name, true, _printers.ToArray(), false);
            this._buttonOk.ButtonText = "Select";
            this._dialogScreen.Invalidate();
        }
    }

   
    public class _gridPrintCopy : MyLib._myGrid
    {
        string[] _printMode = new string[] { MyLib._myResource._findResource(_g.d.kitchen_master._table + "." + _g.d.kitchen_master._printer_mode_1)._str, MyLib._myResource._findResource(_g.d.kitchen_master._table + "." + _g.d.kitchen_master._printer_mode_2)._str, MyLib._myResource._findResource(_g.d.kitchen_master._table + "." + _g.d.kitchen_master._printer_mode_3)._str };

        printcopyGridType _gridType = printcopyGridType.Order;
        public printcopyGridType gridType
        {
            get
            {
                return _gridType;
            }
            set
            {
                _gridType = value;
            }
        }

        public _gridPrintCopy()
        {
            this._width_by_persent = true;
            this._autoUpperSearchString = false;

            switch (this._gridType)
            {
                case printcopyGridType.Order:
                    this._table_name = _g.d.kitchen_print_copy._table;
                    this._addColumn(_g.d.kitchen_print_copy._print_mode, 10, 20, 20);
                    this._addColumn(_g.d.kitchen_print_copy._print_name, 1, 40, 40, true, false, true, true);
                    this._addColumn(_g.d.kitchen_print_copy._status, 11, 20, 20);
                    break;
                case printcopyGridType.Cancel:
                    this._table_name = _g.d.kitchen_print_cancel_copy._table;
                    this._addColumn(_g.d.kitchen_print_cancel_copy._print_mode, 10, 20, 20);
                    this._addColumn(_g.d.kitchen_print_cancel_copy._print_name, 1, 40, 40, true, false, true, true);
                    this._addColumn(_g.d.kitchen_print_cancel_copy._status, 11, 20, 20);
                    break;
            }


            this._calcPersentWidthToScatter();

            this._clickSearchButton += _gridPrintCopy__clickSearchButton;
            this._cellComboBoxGet += _gridPrintCopy__cellComboBoxGet;
            this._cellComboBoxItem += _gridPrintCopy__cellComboBoxItem;

            this._queryForInsertCheck += _gridPrintCopy__queryForInsertCheck;
        }

        bool _gridPrintCopy__queryForInsertCheck(MyLib._myGrid sender, int row)
        {
            if (sender._cellGet(row, _g.d.kitchen_print_copy._print_name) == null)
            {
                return false;
            }
            // เป็นค่าว่างหรือไม่ (ไม่บันทึก)
            if (((string)sender._cellGet(row, _g.d.kitchen_print_copy._print_name)).ToString().Length == 0)
            {
                return false;
            }
            return true;
        }

        object[] _gridPrintCopy__cellComboBoxItem(object sender, int row, int column)
        {

            if (column == this._findColumnByName(_g.d.kitchen_print_copy._print_mode))
            {
                return _printMode;
            }

            return null;
        }

        string _gridPrintCopy__cellComboBoxGet(object sender, int row, int column, string columnName, int select)
        {

            if (column == this._findColumnByName(_g.d.kitchen_print_copy._print_mode))
            {
                return _printMode[(select == -1) ? 0 : select].ToString(); ;
            }

            return "0";
        }

        void _gridPrintCopy__clickSearchButton(object sender, MyLib.GridCellEventArgs e)
        {
            if (e._columnName.Equals(_g.d.kitchen_print_copy._print_name))
            {

                // select printer name
                _myPrinterSearchDialog __dialog = new _myPrinterSearchDialog();
                __dialog.Text = "ค้นหาเครื่องพิมพ์";
                __dialog._beforeClose += (s1, e1) =>
                {
                    _myPrinterSearchDialog _search = (_myPrinterSearchDialog)s1;
                    if (_search.DialogResult == DialogResult.Yes)
                    {
                        MyLib._myComboBox __printerSelectCombo = (MyLib._myComboBox)_search._dialogScreen._getControl(_g.d.kitchen_master._table + "." + _g.d.kitchen_master._printer_name);
                        string __printerSelect = __printerSelectCombo.Text;

                        //this._myPrintOptionScreen._setDataStr(_g.d.kitchen_master._print_copy_name, __printerSelect);
                        this._cellUpdate(e._row, e._column, __printerSelect, true);
                    }

                };
                __dialog.ShowDialog();
            }
        }
    }
    public enum printcopyGridType
    {
        Order,
        Cancel
    }
}
