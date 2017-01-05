using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Collections;
using System.Text;
using System.Windows.Forms;

namespace SMLPOSControl._food
{
    public partial class _kitchenFast : UserControl
    {
        public _kitchenFast()
        {
            InitializeComponent();
            if (MyLib._myGlobal._isDesignMode == false)
            {
                this.toolStrip1.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            }
            // ค้นหาครัว
            List<String> __kitchenCodeList = new List<String>();
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            StringBuilder __myquery = new StringBuilder(MyLib._myGlobal._xmlHeader + "<node>");
            __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + MyLib._myGlobal._fieldAndComma(_g.d.kitchen_master._code, _g.d.kitchen_master._name_1) + " from " + _g.d.kitchen_master._table + " order by " + _g.d.kitchen_master._code));
            __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + MyLib._myGlobal._fieldAndComma(_g.d.kitchen_master_item._kitchen_code, _g.d.kitchen_master_item._food_code) + " from " + _g.d.kitchen_master_item._table + " order by " + _g.d.kitchen_master_item._kitchen_code));
            __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + MyLib._myGlobal._fieldAndComma(_g.d.ic_inventory._code, _g.d.ic_inventory._name_1) + " from " + _g.d.ic_inventory._table + " order by " + _g.d.ic_inventory._code));
            __myquery.Append("</node>");
            ArrayList __getData = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());
            DataTable __kitchen = (((DataSet)__getData[0]).Tables[0]);
            DataTable __kitchenAndFood = (((DataSet)__getData[1]).Tables[0]);
            DataTable __item = (((DataSet)__getData[2]).Tables[0]);
            //
            this._grid._getResource = false;
            this._grid._table_name = "";
            this._grid._isEdit = false;
            this._grid._addColumn("รหัส", 1, 10, 10);
            this._grid._addColumn("ชื่ออาหาร/เครื่องดื่ม", 1, 20, 20);
            this._grid._addColumn("สถานะ", 1, 10, 10);
            int __width = (__kitchen.Rows.Count == 0) ? 0 : 90 / __kitchen.Rows.Count;
            for (int __row = 0; __row < __kitchen.Rows.Count; __row++)
            {
                this._grid._addColumn(__kitchen.Rows[__row][1].ToString(), 11, __width, __width);
                __kitchenCodeList.Add(__kitchen.Rows[__row][0].ToString().ToUpper());
            }
            this._grid._calcPersentWidthToScatter();
            for (int __row = 0; __row < __item.Rows.Count; __row++)
            {
                int __addr = this._grid._addRow();
                this._grid._cellUpdate(__addr, 0, __item.Rows[__row][0].ToString(), false);
                this._grid._cellUpdate(__addr, 1, __item.Rows[__row][1].ToString(), false);
            }
            for (int __row = 0; __row < this._grid._rowData.Count; __row++)
            {
                string __code = this._grid._cellGet(__row, 0).ToString();
                Boolean __found = false;
                for (int __find = 0; __find < __kitchenAndFood.Rows.Count; __find++)
                {
                    if (__kitchenAndFood.Rows[__find][1].ToString().Equals(__code))
                    {
                        string __kitchenCode = __kitchenAndFood.Rows[__find][0].ToString().ToUpper();
                        for (int __column = 0; __column < __kitchenCodeList.Count; __column++)
                        {
                            if (__kitchenCodeList[__column].Equals(__kitchenCode))
                            {
                                this._grid._cellUpdate(__row, 3 + __column, 1, false);
                                __found = true;
                                break;
                            }
                        }
                    }
                }
                if (__found == false)
                {
                    this._grid._cellUpdate(__row, 2, "ไม่ส่งครัว/บาร์น้ำ", false);
                }
            }
            this._grid.Invalidate();
        }

        private void _buttonClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
