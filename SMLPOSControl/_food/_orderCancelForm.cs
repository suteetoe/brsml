using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace SMLPOSControl._food
{
    public partial class _orderCancelForm : Form
    {
        _tableSearchLevelMenuControl _table;
        string _itemCode = "";
        string _itemName = "";
        string _unitCode = "";

        public _orderCancelForm(_tableSearchLevelMenuControl table, string itemCode, string itemName, string unit_code, int qty, int qty_cancel, int qty_balance)
        {
            InitializeComponent();

            this._table = table;
            this._itemCode = itemCode;
            this._itemName = itemName;
            this._unitCode = unit_code;

            this._screen._table_name = _g.d.table_order_cancel._table;
            this._screen._maxColumn = 2;
            this._screen._addTextBox(0, 0, 1, _g.d.table_order_cancel._item_code, 2, 10);
            this._screen._addTextBox(1, 0, 1, _g.d.table_order_cancel._item_name, 2, 10);
            this._screen._addTextBox(2, 0, _g.d.table_order_cancel._unit_code, 10);
            this._screen._addNumberBox(3, 0, 1, 1, _g.d.table_order_cancel._qty_order, 1, 2, true);
            this._screen._addNumberBox(3, 1, 1, 1, _g.d.table_order_cancel._qty_cancel, 1, 2, true);
            this._screen._addNumberBox(4, 0, 1, 1, _g.d.table_order_cancel._qty_balance, 1, 2, true);
            this._screen._addNumberBox(4, 1, 1, 1, _g.d.table_order_cancel._qty, 1, 2, true);

            this._screen._setDataStr(_g.d.table_order_cancel._item_code, _itemCode);
            this._screen._setDataStr(_g.d.table_order_cancel._item_name, _itemName);
            this._screen._setDataStr(_g.d.table_order_cancel._unit_code, _unitCode);
            this._screen._setDataNumber(_g.d.table_order_cancel._qty_order, qty);
            this._screen._setDataNumber(_g.d.table_order_cancel._qty_cancel, qty_cancel);
            this._screen._setDataNumber(_g.d.table_order_cancel._qty_balance, qty_balance);
            //
            //MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            //StringBuilder __myquery = new StringBuilder();
            //__myquery.Append(MyLib._myGlobal._xmlHeader + "<node>");
            ////__myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + MyLib._myGlobal._fieldAndComma(_g.d.table_order._doc_no, _g.d.table_order._item_code, "(select " + _g.d.ic_inventory._name_1 + " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._code + "=" + _g.d.table_order._item_code + ") as " + _g.d.table_order._item_name, _g.d.table_order._qty, _g.d.table_order._qty_send, _g.d.table_order._qty_cancel, _g.d.table_order._qty_balance, _g.d.table_order._unit_code, _g.d.table_order._price, _g.d.table_order._sum_amount, _g.d.table_order._remark, _g.d.table_order._barcode) + " from " + _g.d.table_order._table + " where " + _g.d.table_order._table_number + "=\'" + tableNumber + "\' and " + _g.d.table_order._last_status + " in (0,1) order by " + _g.d.table_order._doc_no + "," + _g.d.table_order._line_number)); // โต๋ เพิ่ม where trans_guid เพื่อดึงสถานะ โต๊ะ ล่าสุด , โต๋ เอา and " + _g.d.table_order._trans_guid + " =\'" + transGuid + "\' ออก
            //__myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select sum(" + _g.d.table_order._qty + ") as " + _g.d.table_order_cancel._qty_order + ", (select " + _g.d.table_order_cancel._table + "." + _g.d.table_order_cancel._qty + " from " + _g.d.table_order_cancel._table + " where " + _g.d.table_order_cancel._table + "." + _g.d.table_order_cancel._table_number + " = \'" + _table._tableNumber + "\' and " + _g.d.table_order_cancel._table + "." + _g.d.table_order_cancel._guid_line + " = \'" + _table._transGuidNumber + "\' and " + _g.d.table_order_cancel._item_code + " = \'" + this._itemCode + "\'  ) as " + _g.d.table_order_cancel._qty_cancel + " from " + _g.d.table_order._table + " where " + _g.d.table_order._table_number + " = \'" + _table._tableNumber + "\' and " + _g.d.table_order._trans_guid + " = \'" + _table._transGuidNumber + "\' and " + _g.d.table_order._item_code + "=\'" + this._itemCode + "\' ")); 
            //__myquery.Append("</node>");
            //string __debug_query = __myquery.ToString();
            //ArrayList __data = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());
            //this._itemGridSource._loadFromDataTable(((DataSet)__data[0]).Tables[0]);

            //
            this._screen._enabedControl(_g.d.table_order_cancel._item_code, false);
            this._screen._enabedControl(_g.d.table_order_cancel._item_name, false);
            this._screen._enabedControl(_g.d.table_order_cancel._unit_code, false);
            this._screen._enabedControl(_g.d.table_order_cancel._qty_order, false);
            this._screen._enabedControl(_g.d.table_order_cancel._qty_cancel, false);
            this._screen._enabedControl(_g.d.table_order_cancel._qty_balance, false);
            //
            this._screen._getControl(_g.d.table_order_cancel._qty).Select();

            this._screen._textBoxChanged += new MyLib.TextBoxChangedHandler(_screen__textBoxChanged);
        }

        void _screen__textBoxChanged(object sender, string name)
        {
            if (name.Equals(_g.d.table_order_cancel._qty))
            {
                decimal __qtyOrder = this._screen._getDataNumber(_g.d.table_order_cancel._qty_order);
                decimal __qtyCancel = this._screen._getDataNumber(_g.d.table_order_cancel._qty_cancel);
                decimal __qty = this._screen._getDataNumber(_g.d.table_order_cancel._qty);
                decimal __qtyBalance = __qtyOrder - (__qtyCancel + __qty);

                this._screen._setDataNumber(_g.d.table_order_cancel._qty_balance, __qtyBalance);
            }
        }

        private void _backButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
