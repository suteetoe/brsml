using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SMLERPControl._ic
{
    public partial class _priceFormulaInfoForm : Form
    {
        private int _roworder;
        private int _priceLevel = 0;
        private SMLERPControl._ic._icPriceFormulaDetail _icPriceFormula = new SMLERPControl._ic._icPriceFormulaDetail();

        public _priceFormulaInfoForm(string itemCode, int roworder, string custCode)
        {
            InitializeComponent();
            this._icPriceFormula._createGrid();
            this._icPriceFormula._toolStrip.Visible = false;
            this._icPriceFormula._grid._isEdit = false;
            this._icPriceFormula._grid._beforeDisplayRendering += new MyLib.BeforeDisplayRenderRowEventHandler(_grid__beforeDisplayRendering);
            this._icPriceFormula._gridResult._beforeDisplayRendering += new MyLib.BeforeDisplayRenderRowEventHandler(_gridResult__beforeDisplayRendering);
            this._icPriceFormula.Dock = DockStyle.Fill;
            this.Controls.Add(this._icPriceFormula);
            try
            {
                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                this._roworder = roworder;
                this.Text = "Code : " + itemCode;
                //
                StringBuilder __myquery = new StringBuilder();
                __myquery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ar_customer._price_level + " from " + _g.d.ar_customer._table + " where " + _g.d.ar_customer._code + "=\'" + custCode + "\'"));
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.ic_inventory_price_formula._table + " where " + _g.d.ic_inventory_price_formula._ic_code + "=\'" + itemCode + "\' order by " + _g.d.ic_inventory_price_formula._unit_code));
                __myquery.Append("</node>");
                ArrayList __getData = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());
                try
                {
                    try
                    {
                        DataTable __getCustGroup = ((DataSet)__getData[0]).Tables[0];
                        this._priceLevel = (int)MyLib._myGlobal._decimalPhase(__getCustGroup.Rows[0][_g.d.ar_customer._price_level].ToString());
                    }
                    catch
                    {
                    }
                    this._icPriceFormula._grid._loadFromDataTable(((DataSet)__getData[1]).Tables[0]);
                    this._icPriceFormula._calc();
                }
                catch
                {
                }
            }
            catch (Exception __ex)
            {
                MessageBox.Show(__ex.Message.ToString());
            }
        }

        MyLib.BeforeDisplayRowReturn _gridResult__beforeDisplayRendering(MyLib._myGrid sender, int row, int columnNumber, string columnName, MyLib.BeforeDisplayRowReturn senderRow, MyLib._myGrid._columnType columnType, ArrayList rowData, Graphics e)
        {
            MyLib.BeforeDisplayRowReturn __result = senderRow;
            try
            {
                int __levelColumn = columnNumber - sender._findColumnByName(_g.d.ic_inventory_price_formula._price_0);
                if (__levelColumn == this._priceLevel)
                {
                    __result.newColor = Color.Red;
                }
            }
            catch
            {
            }
            return (__result);
        }

        MyLib.BeforeDisplayRowReturn _grid__beforeDisplayRendering(MyLib._myGrid sender, int row, int columnNumber, string columnName, MyLib.BeforeDisplayRowReturn senderRow, MyLib._myGrid._columnType columnType, ArrayList rowData, Graphics e)
        {
            MyLib.BeforeDisplayRowReturn __result = senderRow;
            try
            {
                int __levelColumn = columnNumber - sender._findColumnByName(_g.d.ic_inventory_price_formula._price_0);
                if (__levelColumn == this._priceLevel)
                {
                    __result.newColor = Color.Red;
                }
            }
            catch
            {
            }
            return (__result);
        }
    }
}
