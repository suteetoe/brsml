using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLERPIC
{
    public partial class _icUnitCheckerControl : UserControl
    {
        public _icUnitCheckerControl()
        {
            InitializeComponent();
            if (MyLib._myGlobal._isDesignMode == false)
            {
                this.toolStrip1.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            }
            //
            this._grid._isEdit = false;
            this._grid._table_name = _g.d.ic_inventory._table;
            this._grid._addColumn(_g.d.ic_inventory._code, 1, 20, 20);
            this._grid._addColumn(_g.d.ic_inventory._name_1, 1, 30, 30);
            this._grid._addColumn(_g.d.ic_inventory._unit_cost, 1, 10, 10);
            this._grid._addColumn(_g.d.ic_inventory._remark, 1, 40, 40);
            //
            this.Load += new EventHandler(_icUnitCheckerControl_Load);
        }

        void _icUnitCheckerControl_Load(object sender, EventArgs e)
        {
            try
            {
                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                StringBuilder __myquery = new StringBuilder();
                string __queryItemList = "select ic_code from (select count(*) as xcount,ic_code from ic_unit_use group by ic_code) as temp1 where xcount>1";
                string __query = "select " + _g.d.ic_inventory._code + "," + _g.d.ic_inventory._name_1 + "," + _g.d.ic_inventory._unit_cost + " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._code + " in (" + __queryItemList + ") order by " + _g.d.ic_inventory._code;
                //
                __myquery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__query));
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ic_unit_use._ic_code + "," + _g.d.ic_unit_use._code + "," + _g.d.ic_unit_use._stand_value + "," + _g.d.ic_unit_use._divide_value + " from " + _g.d.ic_unit_use._table + " where " + _g.d.ic_unit_use._ic_code + " in (" + __queryItemList + ") order by " + _g.d.ic_unit_use._ic_code));
                __myquery.Append("</node>");
                ArrayList __getData = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());
                //
                this._grid._loadFromDataTable(((DataSet)__getData[0]).Tables[0]);
                DataTable __unitUse = ((DataSet)__getData[1]).Tables[0];
                for (int __row = 0; __row < this._grid._rowData.Count; __row++)
                {
                    string __itemCode = this._grid._cellGet(__row, _g.d.ic_inventory._code).ToString();
                    DataRow[] __unit = __unitUse.Select(_g.d.ic_unit_use._ic_code + "=\'" + __itemCode + "\'");
                    List<SMLERPIC._icMainControl._packingClass> __line = new List<SMLERPIC._icMainControl._packingClass>();
                    for (int __loop = 0; __loop < __unit.Length; __loop++)
                    {
                        decimal __standValue = MyLib._myGlobal._decimalPhase(__unit[__loop][_g.d.ic_unit_use._stand_value].ToString());
                        decimal __divideValue = MyLib._myGlobal._decimalPhase(__unit[__loop][_g.d.ic_unit_use._divide_value].ToString());
                        if (__standValue != 0 && __divideValue != 0)
                        {
                            SMLERPIC._icMainControl._packingClass __data = new SMLERPIC._icMainControl._packingClass();
                            __data._code = __unit[__loop][_g.d.ic_unit_use._code].ToString();
                            __data._standValue = __standValue;
                            __data._divideValue = __divideValue;
                            __data._ratio = __standValue / __divideValue;
                            __line.Add(__data);
                        }
                    }
                    //
                    try
                    {
                        StringBuilder __unitProcess = new StringBuilder();
                        __line.Sort(delegate(SMLERPIC._icMainControl._packingClass p1, SMLERPIC._icMainControl._packingClass p2) { return p1._ratio.CompareTo(p2._ratio); });
                        for (int __loop = 1; __loop < __line.Count; __loop++)
                        {
                            if (__unitProcess.Length > 0)
                            {
                                __unitProcess.Append(" , ");
                            }
                            SMLERPIC._icMainControl._packingClass __rowBefore = __line[__loop - 1];
                            SMLERPIC._icMainControl._packingClass __rowNow = __line[__loop];
                            decimal __ratio = __rowNow._ratio / __rowBefore._ratio;
                            __unitProcess.Append(__ratio.ToString() + " ");
                            __unitProcess.Append(__rowBefore._code);
                            __unitProcess.Append(" = ");
                            __unitProcess.Append(__rowNow._code);
                        }
                        this._grid._cellUpdate(__row, _g.d.ic_inventory._remark, __unitProcess.ToString(), false);
                    }
                    catch
                    {
                    }
                }
                this._grid.Invalidate();
            }
            catch (Exception __ex)
            {
                MessageBox.Show(__ex.Message.ToString());
            }
        }

        private void _closeButton_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
