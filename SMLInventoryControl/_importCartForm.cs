using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SMLInventoryControl
{
    public partial class _importCartForm : Form
    {
        public string _custCode = "";
        public int _billType = 0;
        public int _taxType = 0;
        private MyLib._myFrameWork _myFrameWork;
        public string _selectCartNumber = "";

        public _importCartForm()
        {
            InitializeComponent();

            this._myFrameWork = new MyLib._myFrameWork();

            string __formatNumberQty = _g.g._getFormatNumberStr(1);
            string __formatNumberPrice = _g.g._getFormatNumberStr(2);
            string __formatNumberAmount = _g.g._getFormatNumberStr(3);
            this._itemGrid._isEdit = false;
            this._itemGrid._table_name = _g.d.ic_trans_detail._table;
            this._itemGrid._addColumn(_g.d.ic_trans_detail._barcode, 1, 20, 20);
            this._itemGrid._addColumn(_g.d.ic_trans_detail._item_code, 1, 20, 20);
            this._itemGrid._addColumn(_g.d.ic_trans_detail._item_name, 1, 40, 40);
            this._itemGrid._addColumn(_g.d.ic_trans_detail._unit_code, 1, 10, 10);
            this._itemGrid._addColumn(_g.d.ic_trans_detail._qty, 3, 10, 10, true, false, true, false, __formatNumberQty);
            this._itemGrid._addColumn(_g.d.ic_trans_detail._price, 3, 10, 10, true, false, true, false, __formatNumberQty);
            this._itemGrid._addColumn(_g.d.ic_trans_detail._discount, 1, 10, 10);
            this._itemGrid._addColumn(_g.d.ic_trans_detail._sum_amount, 3, 10, 10, true, false, true, false, __formatNumberQty);
            this._itemGrid._addColumn(_g.d.ic_trans_detail._wh_code, 1, 10, 10, false,  true);
            this._itemGrid._addColumn(_g.d.ic_trans_detail._shelf_code, 1, 10, 10, false, true);

            this._itemGrid._calcPersentWidthToScatter();

            this._saleCode.TextChanged += new EventHandler(_saleCode_TextChanged);
            this._cartNumber.SelectedIndexChanged += new EventHandler(_cartNumber_SelectedIndexChanged);
        }

        void _cartNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] __split = this._cartNumber.SelectedItem.ToString().Split(',');
            string __saleCode = this._saleCode.Text.ToUpper().Trim();
            string __cartNumber = __split[0].ToString();
            StringBuilder __myquery = new StringBuilder();
            __myquery.Append(MyLib._myGlobal._xmlHeader + "<node>");
            __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.order_cart._table + " where " + MyLib._myGlobal._addUpper(_g.d.order_cart._user_owner) + "=\'" + __saleCode.ToUpper() + "\' and " + _g.d.order_cart._cart_number + "=\'" + __cartNumber + "\'"));
            __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " +
               MyLib._myGlobal._fieldAndComma(_g.d.table_order._barcode + " as " + _g.d.ic_trans_detail._barcode,
               _g.d.order_item._item_code + " as " + _g.d.ic_trans_detail._item_code,
               "(select " + _g.d.ic_inventory._name_1 + " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code + "=" + _g.d.order_item._item_code + ") as " + _g.d.ic_trans_detail._item_name,
               _g.d.order_item._unit_code + " as " + _g.d.ic_trans_detail._unit_code,
               _g.d.order_item._qty + " as " + _g.d.ic_trans_detail._qty, _g.d.order_item._price + " as " + _g.d.ic_trans_detail._price,
               _g.d.order_item._discount_word + " as " + _g.d.ic_trans_detail._discount, _g.d.order_item._amount + " as " + _g.d.ic_trans_detail._sum_amount,
               _g.d.order_item._wh_code,
               _g.d.order_item._shelf_code) +
               " from " + _g.d.order_item._table +
               " where " + MyLib._myGlobal._addUpper(_g.d.order_item._user_owner) + "=\'" + __saleCode.ToUpper() + "\' and " + _g.d.order_item._cart_number + "=\'" + __cartNumber + "\' order by " + _g.d.order_item._order_date + "," + _g.d.order_item._order_time));
            __myquery.Append("</node>");
            ArrayList _getData = this._myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());
            DataTable __order = ((DataSet)_getData[0]).Tables[0];
            this._custCode = __order.Rows[0][_g.d.order_cart._cust_code].ToString();
            this._billType = (int)MyLib._myGlobal._decimalPhase(__order.Rows[0][_g.d.order_cart._bill_type].ToString());
            this._taxType = (int)MyLib._myGlobal._decimalPhase(__order.Rows[0][_g.d.order_cart._tax_type].ToString());
            DataTable __items = ((DataSet)_getData[1]).Tables[0];
            this._itemGrid._loadFromDataTable(__items);
            this._selectCartNumber = __cartNumber;
        }

        void _saleCode_TextChanged(object sender, EventArgs e)
        {
            string __custNameField = "cust_name";

            this._itemGrid._clear();
            this._cartNumber.Items.Clear();
            string __saleCode = this._saleCode.Text.ToUpper().Trim();
            StringBuilder __myquery = new StringBuilder();
            __myquery.Append(MyLib._myGlobal._xmlHeader + "<node>");
            __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.order_cart._cart_number + ",(select " + _g.d.ar_customer._name_1 + " from " + _g.d.ar_customer._table + " where " + _g.d.ar_customer._table + "." + _g.d.ar_customer._code + "=" + _g.d.order_cart._table + "." + _g.d.order_cart._cust_code + ") as " + __custNameField + "," + _g.d.order_cart._cust_code + " from " + _g.d.order_cart._table + " where " + MyLib._myGlobal._addUpper(_g.d.order_cart._user_owner) + "=\'" + __saleCode + "\' and " + _g.d.order_cart._cust_code + "<>\'\' order by " + _g.d.order_cart._cart_number));
            __myquery.Append("</node>");
            ArrayList _getData = this._myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());
            DataTable __cart = ((DataSet)_getData[0]).Tables[0];
            for (int __row = 0; __row < __cart.Rows.Count; __row++)
            {
                this._cartNumber.Items.Add(__cart.Rows[__row][_g.d.order_cart._cart_number].ToString() + "," + __cart.Rows[__row][_g.d.order_cart._cust_code].ToString() + "," + __cart.Rows[__row][__custNameField].ToString());
            }
        }
    }
}
