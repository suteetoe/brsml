using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace _g
{
    public partial class _ic_add_warehouse_shelf_form : Form
    {
        public _ic_add_warehouse_shelf_form()
        {
            InitializeComponent();
        }

        private void _ic_add_warehouse_shelf_form_Load(object sender, EventArgs e)
        {
            this._descTextBox.Text = MyLib._myResource._findResource("โปรแกรมจะทำการค้นหาสินค้าที่ไม่มีคลังสินค้าและหน่วยนับ เมื่อพบแล้วจะทำการเพิ่มคลังและพื้นที่เก็บทั้งหมดให้โดยอัตโนมัติ")._str;
        }

        private void _processButton_Click(object sender, EventArgs e)
        {
            try
            {
                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                /*string __query = "select " + _g.d.ic_inventory._code + " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._code
                    + " not in (select distinct " + _g.d.ic_wh_shelf._ic_code + " from " + _g.d.ic_wh_shelf._table + ")";*/
                DataTable __getItem = __myFrameWork._queryStream(MyLib._myGlobal._databaseName, "select " + _g.d.ic_inventory._code + " from " + _g.d.ic_inventory._table).Tables[0];
                string __field = MyLib._myGlobal._fieldAndComma(_g.d.ic_wh_shelf._ic_code, _g.d.ic_wh_shelf._wh_code, _g.d.ic_wh_shelf._shelf_code);
                DataTable __getItemWhList = __myFrameWork._queryStream(MyLib._myGlobal._databaseName, "select " + __field + " from " + _g.d.ic_wh_shelf._table + " order by " + __field).Tables[0];

                try
                {
                    __getItemWhList.PrimaryKey = new DataColumn[] 
                    {
	                    __getItemWhList.Columns[_g.d.ic_wh_shelf._ic_code], 
                        __getItemWhList.Columns[_g.d.ic_wh_shelf._wh_code], 
	                    __getItemWhList.Columns[_g.d.ic_wh_shelf._shelf_code]
                    };
                }
                catch
                {
                }
                string __queryWareHouse = "select " + _g.d.ic_shelf._whcode + "," + _g.d.ic_shelf._code + " from " + _g.d.ic_shelf._table;
                DataTable __getWareHouse = __myFrameWork._queryStream(MyLib._myGlobal._databaseName, __queryWareHouse).Tables[0];
                StringBuilder __queryInsert = new StringBuilder(MyLib._myGlobal._xmlHeader + "<node>");
                int __count = 0;
                int __total = 0;
                for (int __row = 0; __row < __getItem.Rows.Count; __row++)
                {
                    if (++__count > 10000)
                    {
                        __count = 0;
                        __queryInsert.Append("</node>");
                        string __resultx = __myFrameWork._queryList(MyLib._myGlobal._databaseName, __queryInsert.ToString());
                        if (__resultx.Length != 0)
                        {
                            MessageBox.Show("Error", __resultx, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        __queryInsert = new StringBuilder(MyLib._myGlobal._xmlHeader + "<node>");
                    }
                    //
                    string __itemCode = __getItem.Rows[__row][0].ToString();
                    for (int __row1 = 0; __row1 < __getWareHouse.Rows.Count; __row1++)
                    {
                        string __whCode = __getWareHouse.Rows[__row1][0].ToString();
                        string __shelfCode = __getWareHouse.Rows[__row1][1].ToString();
                        if (__getItemWhList.Rows.Count == 0)
                        {
                            __queryInsert.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _g.d.ic_wh_shelf._table + " (" + _g.d.ic_wh_shelf._ic_code + "," + _g.d.ic_wh_shelf._wh_code + "," + _g.d.ic_wh_shelf._shelf_code + "," + _g.d.ic_wh_shelf._status + ") values (\'" + MyLib._myGlobal._convertStrToQuery(__itemCode) + "\',\'" + __whCode + "\',\'" + __shelfCode + "\',1)"));
                        }
                        else
                        {
                            DataRow[] __select = __getItemWhList.Select(_g.d.ic_wh_shelf._ic_code + "=\'" + MyLib._myGlobal._convertStrToQuery(__itemCode) + "\' and " + _g.d.ic_wh_shelf._wh_code + "=\'" + __whCode + "\' and " + _g.d.ic_wh_shelf._shelf_code + "=\'" + __shelfCode + "\'");
                            if (__select.Length == 0)
                            {
                                __queryInsert.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _g.d.ic_wh_shelf._table + " (" + _g.d.ic_wh_shelf._ic_code + "," + _g.d.ic_wh_shelf._wh_code + "," + _g.d.ic_wh_shelf._shelf_code + "," + _g.d.ic_wh_shelf._status + ") values (\'" + MyLib._myGlobal._convertStrToQuery(__itemCode) + "\',\'" + __whCode + "\',\'" + __shelfCode + "\',1)"));
                            }
                        }
                    }
                    __total++;
                    this._statusLabel.Text = __total.ToString();
                    this._statusLabel.Invalidate();
                    Application.DoEvents();
                }
                __queryInsert.Append("</node>");
                string __result = __myFrameWork._queryList(MyLib._myGlobal._databaseName, __queryInsert.ToString());
                if (__result.Length != 0)
                {
                    MessageBox.Show("Error", "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("Success");
                }
            }
            catch (Exception __ex)
            {
                MessageBox.Show(__ex.Message.ToString());
            }
        }
    }
}
