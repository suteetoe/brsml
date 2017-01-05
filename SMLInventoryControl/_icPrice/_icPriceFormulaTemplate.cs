using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLInventoryControl._icPrice
{
    public partial class _icPriceFormulaTemplate : UserControl
    {
        public _icPriceFormulaTemplate()
        {
            InitializeComponent();

            if (MyLib._myGlobal._isDesignMode == false)
            {
                this.toolStrip1.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            }
            this._grid._table_name = _g.d.ic_price_formula_template._table;
            this._grid._addColumn(_g.d.ic_price_formula_template._code, 1, 10, 10, true, false);
            this._grid._addColumn(_g.d.ic_price_formula_template._price_1, 1, 10, 10, true, false);
            this._grid._addColumn(_g.d.ic_price_formula_template._price_2, 1, 10, 10, true, false);
            this._grid._addColumn(_g.d.ic_price_formula_template._price_3, 1, 10, 10, true, false);
            this._grid._addColumn(_g.d.ic_price_formula_template._price_4, 1, 10, 10, true, false);
            this._grid._addColumn(_g.d.ic_price_formula_template._price_5, 1, 10, 10, true, false);
            this._grid._addColumn(_g.d.ic_price_formula_template._price_6, 1, 10, 10, true, false);
            this._grid._addColumn(_g.d.ic_price_formula_template._price_7, 1, 10, 10, true, false);
            this._grid._addColumn(_g.d.ic_price_formula_template._price_8, 1, 10, 10, true, false);
            this._grid._addColumn(_g.d.ic_price_formula_template._price_9, 1, 10, 10, true, false);
            this._grid._calcPersentWidthToScatter();
            this._grid._queryForInsertCheck += new MyLib.QueryForInsertCheckEventHandler(_grid__queryForInsertCheck);
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            StringBuilder __myquery = new StringBuilder();
            __myquery.Append(MyLib._myGlobal._xmlHeader + "<node>");
            __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.ic_price_formula_template._table + " order by " + _g.d.ic_price_formula_template._code));
            __myquery.Append("</node>");
            ArrayList __getData = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());
            this._grid._loadFromDataTable(((DataSet)__getData[0]).Tables[0]);
        }

        bool _grid__queryForInsertCheck(MyLib._myGrid sender, int row)
        {
            return (this._grid._cellGet(row, _g.d.ic_price_formula_template._code).ToString().Trim().Length == 0) ? false : true;
        }

        private void _closeButton_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void _saveButton_Click(object sender, EventArgs e)
        {
            if (MyLib._myGlobal._checkChangeMaster())
            {
                StringBuilder __myQuery = new StringBuilder();
                __myQuery.Append(MyLib._myGlobal._xmlHeader + "<node>");

                // Delete
                __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.ic_price_formula_template._table));
                this._grid._updateRowIsChangeAll(true);
                __myQuery.Append(this._grid._createQueryForInsert(_g.d.ic_price_formula_template._table, "", ""));
                __myQuery.Append("</node>");

                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                string __result = __myFrameWork._queryList(MyLib._myGlobal._databaseName, __myQuery.ToString());
                if (__result.Length == 0)
                {
                    MyLib._myGlobal._displayWarning(1, null);
                    this.Dispose();
                }
                else
                {
                    MessageBox.Show(__result, "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
