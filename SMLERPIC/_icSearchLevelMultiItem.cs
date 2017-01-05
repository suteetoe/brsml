using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLERPIC
{
    public partial class _icSearchLevelMultiItem : UserControl
    {
        _icSearchLevelMultiItemScreen _screen = new _icSearchLevelMultiItemScreen();

        public _icSearchLevelMultiItem()
        {
            InitializeComponent();

            //
            //this._screen._toolStrip.EnabledChanged += new EventHandler(_toolStrip_EnabledChanged);
            //
            this._screen.Dock = DockStyle.Fill;
            this._myManageData1._form2.Controls.Add(this._screen);

            // remove button

            this._myManageData1._dataList._buttonDelete.Visible = false;
            //
            this._myManageData1._displayMode = 0;
            this._myManageData1._dataList._lockRecord = false;
            this._myManageData1._selectDisplayMode(this._myManageData1._displayMode);
            this._myManageData1._dataList._loadViewFormat(_g.g._search_screen_ic_inventory_barcode, MyLib._myGlobal._userSearchScreenGroup, true);

            this._myManageData1._dataList._referFieldAdd(_g.d.ic_inventory_barcode_price._barcode, 1);
            this._myManageData1._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
            this._myManageData1._closeScreen += _myManageData1__closeScreen;
            this._myManageData1._dataList._buttonClose.Click += new EventHandler(_buttonClose_Click);

            this._myManageData1._calcArea();
            this._myManageData1._dataListOpen = true;
            this._myManageData1._autoSize = true;
            this._myManageData1._autoSizeHeight = 450;
            this._myManageData1.Invalidate();
            //
            this._screen._saveButton.Click += (s1, e1) =>
            {
                this._saveData();
            };
            this._screen._closeButton.Click += (s1, e1) =>
            {
                this.Dispose();
            };
            this._screen._resetGridButton.Click += new EventHandler(_resetGridButton_Click);
            this._screen._resetLevelButton.Click += _resetLevelButton_Click;

        }

        void _resetLevelButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("คุณต้องการล้างการกำหนดลำดับการค้นหาหรือไม่", "ยืนยัน", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                string __result = __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, "delete from " +  _g.d.ic_inventory_level._table);

                if (__result.Length == 0)
                {
                    MessageBox.Show("ล้างการกำหนดลำดับการค้นหาสำเร็จ");
                }
                else
                {
                    MessageBox.Show(__result, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        void _myManageData1__closeScreen()
        {
            this.Dispose();
        }

        void _resetGridButton_Click(object sender, EventArgs e)
        {
            // reset grid
            this._screen._selectedGid._clear();
        }

        void _buttonClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        void _gridData__mouseClick(object sender, MyLib.GridCellEventArgs e)
        {
            string __barCode = this._myManageData1._dataList._gridData._cellGet(e._row, _g.d.ic_inventory_barcode._table + "." + _g.d.ic_inventory_barcode._barcode).ToString();
            Boolean __found = false;
            for (int __row = 0; __row < this._screen._selectedGid._rowData.Count; __row++)
            {
                if (__barCode.Equals(this._screen._selectedGid._cellGet(__row, _g.d.ic_inventory_barcode._barcode).ToString()))
                {
                    __found = true;
                    break;
                }
            }
            if (__found == false)
            {
                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                DataTable __data = __myFrameWork._queryShort("select *, (select " + _g.d.ic_unit._table + "." + _g.d.ic_unit._name_1 + " from " + _g.d.ic_unit._table + " where " + _g.d.ic_unit._table + "." + _g.d.ic_unit._code + " = " + _g.d.ic_inventory_barcode._table + "." + _g.d.ic_inventory_barcode._unit_code + " ) as " + _g.d.ic_inventory_barcode._unit_name + "  from " + _g.d.ic_inventory_barcode._table + " where " + _g.d.ic_inventory_barcode._barcode + "=\'" + __barCode + "\'").Tables[0];
                if (__data.Rows.Count > 0)
                {
                    int __addr = this._screen._selectedGid._addRow();
                    this._screen._selectedGid._cellUpdate(__addr, _g.d.ic_inventory_barcode._barcode, __data.Rows[0][_g.d.ic_inventory_barcode._barcode].ToString(), false);
                    this._screen._selectedGid._cellUpdate(__addr, _g.d.ic_inventory_barcode._ic_code, __data.Rows[0][_g.d.ic_inventory_barcode._ic_code].ToString(), false);
                    this._screen._selectedGid._cellUpdate(__addr, _g.d.ic_inventory_barcode._description, __data.Rows[0][_g.d.ic_inventory_barcode._description].ToString(), false);
                    this._screen._selectedGid._cellUpdate(__addr, _g.d.ic_inventory_barcode._unit_code, __data.Rows[0][_g.d.ic_inventory_barcode._unit_code].ToString(), false);
                    this._screen._selectedGid._cellUpdate(__addr, _g.d.ic_inventory_barcode._price, MyLib._myGlobal._decimalPhase(__data.Rows[0][_g.d.ic_inventory_barcode._price].ToString()), false);
                    this._screen._selectedGid._cellUpdate(__addr, _g.d.ic_inventory_barcode._price_2, MyLib._myGlobal._decimalPhase(__data.Rows[0][_g.d.ic_inventory_barcode._price_2].ToString()), false);
                    this._screen._selectedGid._cellUpdate(__addr, _g.d.ic_inventory_barcode._price_3, MyLib._myGlobal._decimalPhase(__data.Rows[0][_g.d.ic_inventory_barcode._price_3].ToString()), false);
                    this._screen._selectedGid._cellUpdate(__addr, _g.d.ic_inventory_barcode._price_4, MyLib._myGlobal._decimalPhase(__data.Rows[0][_g.d.ic_inventory_barcode._price_4].ToString()), false);
                    this._screen._selectedGid._cellUpdate(__addr, _g.d.ic_inventory_barcode._unit_name, __data.Rows[0][_g.d.ic_inventory_barcode._unit_name].ToString(), false);
                }
            }

        }

        void _saveData()
        {
            if (MyLib._myGlobal._checkChangeMaster())
            {
                // save level search
                StringBuilder __myQuery = new StringBuilder();
                __myQuery.Append(MyLib._myGlobal._xmlHeader + "<node>");

                string __level_1 = this._screen._icSearchLevelMuitiItemScreenTopScreen1._getDataStr(_g.d.ic_inventory_level._table + "." + _g.d.ic_inventory_level._level_1);
                string __level_2 = this._screen._icSearchLevelMuitiItemScreenTopScreen1._getDataStr(_g.d.ic_inventory_level._table + "." + _g.d.ic_inventory_level._level_2);
                string __level_3 = this._screen._icSearchLevelMuitiItemScreenTopScreen1._getDataStr(_g.d.ic_inventory_level._table + "." + _g.d.ic_inventory_level._level_3);

                int __priceLevel = (int)MyLib._myGlobal._decimalPhase(this._screen._icSearchLevelMuitiItemScreenTopScreen1._getDataStr(_g.d.POS_ID._table + "." + _g.d.POS_ID._price_number));
                // for itemgrid โลด
                for (int __i = 0; __i < this._screen._selectedGid._rowData.Count; __i++)
                {
                    string __barcode = this._screen._selectedGid._cellGet(__i, _g.d.ic_inventory_barcode._barcode).ToString();
                    string __itemCode = this._screen._selectedGid._cellGet(__i, _g.d.ic_inventory_barcode._ic_code).ToString().ToUpper();
                    string __unit_code = this._screen._selectedGid._cellGet(__i, _g.d.ic_inventory_barcode._unit_code).ToString();
                    string __remark = this._screen._icSearchLevelMuitiItemScreenTopScreen1._getDataStr(_g.d.ic_inventory_level._table + "." + _g.d.ic_inventory_level._suggest_remark);
                    decimal __price = 0M;

                    switch (__priceLevel)
                    {
                        case 3:
                            __price = MyLib._myGlobal._decimalPhase(this._screen._selectedGid._cellGet(__i, _g.d.ic_inventory_barcode._price_4).ToString());
                            break;
                        case 2:
                            __price = MyLib._myGlobal._decimalPhase(this._screen._selectedGid._cellGet(__i, _g.d.ic_inventory_barcode._price_3).ToString());
                            break;
                        case 1:
                            __price = MyLib._myGlobal._decimalPhase(this._screen._selectedGid._cellGet(__i, _g.d.ic_inventory_barcode._price_2).ToString());
                            break;
                        default:
                            __price = MyLib._myGlobal._decimalPhase(this._screen._selectedGid._cellGet(__i, _g.d.ic_inventory_barcode._price).ToString());
                            break;
                    }

                    __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery(" delete from " + _g.d.ic_inventory_level._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_inventory_level._ic_code) + " = \'" + __itemCode + "\' and " + _g.d.ic_inventory_level._barcode + " =\'" + __barcode + "\'"));

                    string __level_4 = ((int)MyLib._myGlobal._decimalPhase(this._screen._selectedGid._cellGet(__i, "sort").ToString())).ToString();
                    string __queryInsertLevel = "insert into " + _g.d.ic_inventory_level._table + "(" + _g.d.ic_inventory_level._ic_code + "," + _g.d.ic_inventory_level._barcode + "," + _g.d.ic_inventory_level._unit_code + ", " + _g.d.ic_inventory_level._level_1 + ", " + _g.d.ic_inventory_level._level_2 + ", " + _g.d.ic_inventory_level._level_3 + ", " + _g.d.ic_inventory_level._level_4 + ", " + _g.d.ic_inventory_level._price + "," + _g.d.ic_inventory_level._suggest_remark + ") values (\'" + __itemCode + "\', \'" + __barcode + "\',\'" + __unit_code + "\',\'" + __level_1 + "\',\'" + __level_2 + "\',\'" + __level_3 + "\',\'" + __level_4 + "\',\'" + __price + "\', \'" + __remark + "\')";
                    __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery(__queryInsertLevel));
                }


                __myQuery.Append("</node>");
                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();

                string __result = __myFrameWork._queryList(MyLib._myGlobal._databaseName, __myQuery.ToString());
                if (__result.Length == 0)
                {
                    MyLib._myGlobal._displayWarning(1, null);

                }
                else
                {
                    MessageBox.Show(__result, "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }
    }

    public class _icSearchLevelMuitiItemScreenTopScreen : MyLib._myScreen
    {
        public _icSearchLevelMuitiItemScreenTopScreen()
        {
            this._maxColumn = 1;

            this._addTextBox(0, 0, _g.d.ic_inventory_level._table + "." + _g.d.ic_inventory_level._level_1, 0);
            this._addTextBox(1, 0, _g.d.ic_inventory_level._table + "." + _g.d.ic_inventory_level._level_2, 0);
            this._addTextBox(2, 0, _g.d.ic_inventory_level._table + "." + _g.d.ic_inventory_level._level_3, 0);
            //this._addTextBox(3, 0, _g.d.ic_inventory_level._table + "." + _g.d.ic_inventory_level._level_4, 0);

            this._addComboBox(3, 0, _g.d.POS_ID._table + "." + _g.d.POS_ID._price_number, true, new string[] { _g.d.ic_inventory_barcode._table + "." + _g.d.ic_inventory_barcode._price, _g.d.ic_inventory_barcode._table + "." + _g.d.ic_inventory_barcode._price_2, _g.d.ic_inventory_barcode._table + "." + _g.d.ic_inventory_barcode._price_3, _g.d.ic_inventory_barcode._table + "." + _g.d.ic_inventory_barcode._price_4 }, false);

            this._addTextBox(4, 0, 1, 1, _g.d.ic_inventory_level._table + "." + _g.d.ic_inventory_level._suggest_remark, 1, 0, 1, true, false);

            this._textBoxSearch += _icSearchLevelMuitiItemScreenTopScreen__textBoxSearch;
        }

        void _icSearchLevelMuitiItemScreenTopScreen__textBoxSearch(object sender)
        {
            Form __testForm = new Form();

            __testForm.WindowState = FormWindowState.Maximized;
            SMLInventoryControl._itemSearchLevelControl __control = new SMLInventoryControl._itemSearchLevelControl();
            __control._productBasket = true;
            __control.Dock = DockStyle.Fill;
            __testForm.Controls.Add(__control);
            __control._menuItemClick += (s1, e1) =>
            {
                StringBuilder __reamarkList = new StringBuilder();
                for (int __row = 0; __row < __control._selectGrid._rowData.Count; __row++)
                {
                    string __itemCode = __control._selectGrid._cellGet(__row, _g.d.ic_trans_detail._item_code).ToString();
                    string __BarCode = __control._selectGrid._cellGet(__row, _g.d.ic_trans_detail._barcode).ToString();
                    string __unitCode = __control._selectGrid._cellGet(__row, _g.d.ic_trans_detail._unit_code).ToString();
                    string __itemName = __control._selectGrid._cellGet(__row, _g.d.ic_trans_detail._item_name).ToString();

                    if (__reamarkList.Length > 0)
                    {
                        __reamarkList.Append(",");
                    }
                    if (!__BarCode.Equals(""))
                    {
                        //__reamarkList.Append();
                        
                        //    __reamarkList.Append();
                        

                        {
                            __reamarkList.Append(__BarCode + "-" +__itemName);
                        }
                    }
                }
                this._setDataStr(_g.d.ic_inventory_level._table + "." + _g.d.ic_inventory_level._suggest_remark, __reamarkList.ToString());
                __testForm.Close();
            };
            __testForm.ShowDialog();
        }

      
    }

}
