using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLPOSControl
{
    public class _selectMenu
    {
        public static Control _getObject(string menuName, string screenName)
        {
            switch (menuName.ToLower())
            {
                case "menu_pos_sound_download_install": MyLib._myUtil._startDialog(MyLib._myGlobal._mainForm, screenName, new _posDownloadSound()); break;
                case "menu_config_pos_startup": MyLib._myUtil._startDialog(MyLib._myGlobal._mainForm, screenName, new _posConfig()); break;
                case "menu_config_pos_machine": return new _posMachine();
                case "menu_coupon_list": return new SMLERPControl._coupon._couponList();
                case "menu_coupon_generate": return new SMLERPControl._coupon._couponGenerate();
                case "check_price":
                    return new SMLPOSControl._checkPriceUserControl();
                case "menu_printer_order_check":
                    {
                        MyLib._manageMasterCodeFull _screenFull = new MyLib._manageMasterCodeFull();
                        _screenFull._labelTitle.Text = screenName;
                        _screenFull._dataTableName = _g.d.printer_order_check._table;
                        _screenFull._addColumn(_g.d.printer_order_check._code, 10, 20);
                        _screenFull._inputScreen._setUpper(_g.d.printer_order_check._code);
                        _screenFull._addColumn(_g.d.printer_order_check._name_1, 100, 40);
                        
                        _screenFull._inputScreen._addComboBox(_screenFull._rowScreen++, 0, _g.d.printer_order_check._print_mode, true, new string[] { _g.d.printer_order_check._print_mode_1, _g.d.printer_order_check._print_mode_2, _g.d.printer_order_check._print_mode_3 }, true);
                        _screenFull._inputScreen._addTextBox(_screenFull._rowScreen++, 0, 1, 0, _g.d.printer_order_check._printer_name, 1, 0, 1, true, false, false, true, true);

                        _screenFull._inputScreen._addCheckBox(_screenFull._rowScreen++, 0, _g.d.printer_order_check._status, true, false);

                        _g._userGroupDetailControl _detailControl = new _g._userGroupDetailControl();
                        //
                        _detailControl._userListGrid._table_name = _g.d.printer_order_check_table._table;
                        _detailControl._userListGrid._addColumn("Select", 11, 1, 10);
                        _detailControl._userListGrid._addColumn(_g.d.printer_order_check_table._table_number, 1, 10, 30);
                        _detailControl._userListGrid._addColumn(_g.d.printer_order_check_table._table_name, 1, 10, 60);

                        _detailControl._userListGrid._alterCellUpdate += (sender , row, column) =>
                        {
                            ArrayList __selectUserCode = new ArrayList();
                            ArrayList __selectUserName = new ArrayList();
                            for (int __row = 0; __row < _detailControl._userListGrid._rowData.Count; __row++)
                            {
                                string __select = _detailControl._userListGrid._cellGet(__row, 0).ToString();
                                if (__select.Equals("1"))
                                {
                                    string __code = _detailControl._userListGrid._cellGet(__row, 1).ToString();
                                    if (__code.Trim().Length > 0)
                                    {
                                        string __name = _detailControl._userListGrid._cellGet(__row, 2).ToString();
                                        __selectUserCode.Add(__code);
                                        __selectUserName.Add(__name);
                                    }
                                }
                            }
                            _detailControl._userSelectedGrid._clear();
                            for (int __loop = 0; __loop < __selectUserCode.Count; __loop++)
                            {
                                int __addr = _detailControl._userSelectedGrid._addRow();
                                _detailControl._userSelectedGrid._cellUpdate(__addr, 0, __selectUserCode[__loop], false);
                                _detailControl._userSelectedGrid._cellUpdate(__addr, 1, __selectUserName[__loop], false);
                            }
                        };

                        _detailControl._userSelectedGrid._table_name = _g.d.printer_order_check_table._table;
                        _detailControl._userSelectedGrid._addColumn(_g.d.printer_order_check_table._table_number, 1, 10, 30, true);
                        _detailControl._userSelectedGrid._addColumn(_g.d.printer_order_check_table._table_name, 1, 10, 70, false);

                        _detailControl.Dock = DockStyle.Fill;
                        _screenFull._extraPanel.Controls.Add(_detailControl);
                        _screenFull._extraPanel.AutoSize = false;
                        _screenFull._extraPanel.Height = 400;
                        _screenFull._inputScreen._textBoxSearch += (sender) =>
                        {
                            MyLib._myTextBox __textbox = (MyLib._myTextBox)sender;

                            if (__textbox._name.ToLower().Equals(_g.d.kitchen_master._printer_name))
                            {
                                // start searh printer
                                _food._myPrinterSearchDialog __dialog = new _food._myPrinterSearchDialog();
                                __dialog.Text = "ค้นหาเครื่องพิมพ์";
                                __dialog._beforeClose += (s1, e1) =>
                                {
                                    _food._myPrinterSearchDialog _search = (_food._myPrinterSearchDialog)s1;
                                    if (_search.DialogResult == DialogResult.Yes)
                                    {
                                        MyLib._myComboBox __printerSelectCombo = (MyLib._myComboBox)_search._dialogScreen._getControl(_g.d.kitchen_master._table + "." + _g.d.kitchen_master._printer_name);
                                        string __printerSelect = __printerSelectCombo.Text;
                                        _screenFull._inputScreen._setDataStr(_g.d.printer_order_check._printer_name, __printerSelect);
                                    }

                                };
                                __dialog.ShowDialog();
                            }
                        };

                        string __query = "select " + _g.d.table_master._number + " as \"" + _g.d.printer_order_check_table._table_number + "\"," +
                            _g.d.table_master._name_1 + " as \"" + _g.d.printer_order_check_table._table_name + "\"" +
                            " from " + _g.d.table_master._table + " where not exists (select table_number from printer_order_check_table as x where x.table_number =  table_master.number) " + " order by " + _g.d.table_master._number;
                        MyLib._myFrameWork __frameWork = new MyLib._myFrameWork();

                        _screenFull._afterNewData += (sender) =>
                       {
                           _detailControl._userListGrid._clear();
                           _detailControl._userSelectedGrid._clear();
                           // ดึงชื่อพนักงานทั้งหมด
                           DataTable __getDataNew = __frameWork._query(MyLib._myGlobal._databaseName, __query).Tables[0];
                           _detailControl._userListGrid._loadFromDataTable(__getDataNew);
                       };

                        _screenFull._afterClearData += (sender) =>
                        {
                            _detailControl._userListGrid._clear();
                            _detailControl._userSelectedGrid._clear();
                            // ดึงชื่อพนักงานทั้งหมด
                            DataTable __getDataClear = __frameWork._query(MyLib._myGlobal._databaseName, __query).Tables[0];
                            _detailControl._userListGrid._loadFromDataTable(__getDataClear);
                        };

                        _screenFull._loadData += (sender) =>
                        {
                            _detailControl._userListGrid._clear();
                            _detailControl._userSelectedGrid._clear();
                            // ดึงชื่อพนักงานทั้งหมด

                            string __extraLoadQueyr = "select " + _g.d.table_master._number + " as \"" + _g.d.printer_order_check_table._table_number + "\"," +
                            _g.d.table_master._name_1 + " as \"" + _g.d.printer_order_check_table._table_name + "\"" +
                            " from " + _g.d.table_master._table + " where not exists (select table_number from printer_order_check_table as x where x.table_number =  table_master.number and x.zone_code != \'" + sender._oldCode + "\') " + " order by " + _g.d.table_master._number;

                            DataTable __getDataLoad = __frameWork._query(MyLib._myGlobal._databaseName, __extraLoadQueyr).Tables[0];
                            _detailControl._userListGrid._loadFromDataTable(__getDataLoad);

                            // update check
                            string __queryLoadSelect = "select " + _g.d.printer_order_check_table._table_number + ", (select name_1 from table_master where table_master.number  = printer_order_check_table.table_number) as " + _g.d.printer_order_check_table._table_name + " from " + _g.d.printer_order_check_table._table + " where " + _g.d.printer_order_check_table._zone_code + "=\'" + sender._oldCode + "\'";
                            
                            DataTable __getDataSelect = __frameWork._query(MyLib._myGlobal._databaseName, __queryLoadSelect).Tables[0];
                            for (int __row = 0; __row < __getDataSelect.Rows.Count; __row++)
                            {
                                string __userCode = __getDataSelect.Rows[__row][0].ToString();
                                /*string __name = __getDataSelect.Rows[__row][1].ToString();
                                int __addr = _detailControl._userSelectedGrid._addRow();
                                _detailControl._userSelectedGrid._cellUpdate(__addr, 0, __userCode, false);
                                _detailControl._userSelectedGrid._cellUpdate(__addr, 1, __name, false);*/

                                int __addr = _detailControl._userListGrid._findData(1, __userCode);
                                if (__addr != -1)
                                {
                                    _detailControl._userListGrid._cellUpdate(__addr, 0, 1, true);
                                }

                            }
                        };

                        _screenFull._deleteData += (sender, fieldData) =>
                        {
                            return MyLib._myUtil._convertTextToXmlForQuery("delete from " + _detailControl._userSelectedGrid._table_name + " where " + _g.d.printer_order_check_table._zone_code + "=\'" + fieldData + "\'");
                        };

                        _screenFull._saveData += (sender) =>
                        {
                            _detailControl._userSelectedGrid._updateRowIsChangeAll(true);
                            string __mainCode = sender._inputScreen._getDataStr(_g.d.printer_order_check._code).ToString();
                            return _detailControl._userSelectedGrid._createQueryForInsert(_detailControl._userSelectedGrid._table_name, _g.d.printer_order_check_table._zone_code + ",", "\'" + __mainCode + "\',");
                        };

                        _detailControl._userListGrid._clear();
                        _detailControl._userSelectedGrid._clear();
                        // ดึงชื่อพนักงานทั้งหมด

                        DataTable __getData = __frameWork._query(MyLib._myGlobal._databaseName, __query).Tables[0];
                        _detailControl._userListGrid._loadFromDataTable(__getData);


                        _screenFull._finish();
                        return _screenFull;
                    }
                default: break;
            }
            return null;
        }

    }
}
