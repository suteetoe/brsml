using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SMLPOSControl
{
    public partial class _posPointPeriodForm : Form
    {
        MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();

        public _posPointPeriodForm()
        {
            InitializeComponent();

            string __format1 = "#,###,###.##";
            this._grid._table_name = _g.d.pos_point_period._table;
            this._grid._addColumn(_g.d.pos_point_period._from_date, 4, 35, 35, true, false, true);
            this._grid._addColumn(_g.d.pos_point_period._to_date, 4, 35, 35, true, false, true);
            this._grid._addColumn(_g.d.pos_point_period._amount, 3, 20, 20, true, false, true, false, __format1);
            this._grid._addColumn(_g.d.pos_point_period._point_value, 3, 20, 20, true, false, true, false, __format1);
            this._grid._addColumn(_g.d.pos_point_period._point_value_condition, 1, 20, 20, true, false, true, false);
            this._grid._calcPersentWidthToScatter();
            //
            this._screen._table_name = _g.d.pos_point_condition._table;
            this._screen._maxColumn = 2;
            this._screen._addCheckBox(0, 0, _g.d.pos_point_condition._birth_day_use, false, true);
            this._screen._addNumberBox(0, 1, 1, 1, _g.d.pos_point_condition._birth_day, 1, 2, true);
            this._screen._addCheckBox(1, 0, _g.d.pos_point_condition._birth_month_use, false, true);
            this._screen._addNumberBox(1, 1, 1, 1, _g.d.pos_point_condition._birth_month, 1, 2, true);
            //
            this._gridSpecialDay._table_name = _g.d.pos_point_special_day._table;
            this._gridSpecialDay._addColumn(_g.d.pos_point_special_day._order_condition, 3, 10, 10, true, false, true, false, "###");
            this._gridSpecialDay._addColumn(_g.d.pos_point_special_day._from_date, 4, 30, 30, true, false, true);
            this._gridSpecialDay._addColumn(_g.d.pos_point_special_day._to_date, 4, 30, 30, true, false, true);
            this._gridSpecialDay._addColumn(_g.d.pos_point_special_day._multiply_point, 3, 20, 20, true, false, true, false, __format1);
            this._gridSpecialDay._addColumn(_g.d.pos_point_special_day._description, 1, 40, 40);
            this._gridSpecialDay._addColumn(_g.d.pos_point_special_day._day_0, 11, 10, 10);
            this._gridSpecialDay._addColumn(_g.d.pos_point_special_day._day_1, 11, 10, 10);
            this._gridSpecialDay._addColumn(_g.d.pos_point_special_day._day_2, 11, 10, 10);
            this._gridSpecialDay._addColumn(_g.d.pos_point_special_day._day_3, 11, 10, 10);
            this._gridSpecialDay._addColumn(_g.d.pos_point_special_day._day_4, 11, 10, 10);
            this._gridSpecialDay._addColumn(_g.d.pos_point_special_day._day_5, 11, 10, 10);
            this._gridSpecialDay._addColumn(_g.d.pos_point_special_day._day_6, 11, 10, 10);
            this._gridSpecialDay._addColumn(_g.d.pos_point_special_day._day_7, 11, 10, 10);
            this._gridSpecialDay._calcPersentWidthToScatter();
            //
            this._grid._loadFromDataTable(this._myFrameWork._queryShort("select * from " + _g.d.pos_point_period._table + " order by " + _g.d.pos_point_period._from_date).Tables[0]);
            this._gridSpecialDay._loadFromDataTable(this._myFrameWork._queryShort("select * from " + _g.d.pos_point_special_day._table + " order by " + _g.d.pos_point_special_day._order_condition).Tables[0]);
            this._screen._loadData(this._myFrameWork._queryShort("select * from " + _g.d.pos_point_condition._table).Tables[0]);
        }

        private void _closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void _saveButton1_Click(object sender, EventArgs e)
        {
            StringBuilder __myQuery = new StringBuilder();
            __myQuery.Append(MyLib._myGlobal._xmlHeader + "<node>");
            __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.pos_point_period._table));
            __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.pos_point_condition._table));
            __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.pos_point_special_day._table));
            this._grid._updateRowIsChangeAll(true);
            this._gridSpecialDay._updateRowIsChangeAll(true);
            __myQuery.Append(this._grid._createQueryForInsert(_g.d.pos_point_period._table, "", ""));
            __myQuery.Append(this._gridSpecialDay._createQueryForInsert(_g.d.pos_point_special_day._table, "", ""));
            ArrayList __queryScreen = this._screen._createQueryForDatabase();
            __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _g.d.pos_point_condition._table + " (" + __queryScreen[0].ToString() + ") values (" + __queryScreen[1].ToString() + ")"));
            __myQuery.Append("</node>");
            string __resultStr = this._myFrameWork._queryList(MyLib._myGlobal._databaseName, __myQuery.ToString());
            if (__resultStr.Length == 0)
            {
                MessageBox.Show("Success");
                this.Close();
            }
            else
            {
                MessageBox.Show(__resultStr);
            }
        }
    }
}
