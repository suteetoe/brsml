using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace SMLFastReport
{
    public partial class _loadForm : Form
    {
        public event _AfterSelectReport _ReportSelected;
        public _loadForm()
        {
            InitializeComponent();
            this.Load += new EventHandler(_loadForm_Load);
        }

        void _loadForm_Load(object sender, EventArgs e)
        {
            // disable tools button
            this._myDataList1._buttonDelete.Enabled = false;
            this._myDataList1._buttonSelectAll.Enabled = false;
            this._myDataList1._buttonNewFromTemp.Enabled = false;
            this._myDataList1._buttonNew.Enabled = false;
            this._myDataList1._buttonClose.Click += new EventHandler(_buttonClose_Click);

            this._myDataList1._gridData._isEdit = false;
            this._myDataList1._lockRecord = true;
            this._myDataList1._loadViewFormat("screen_fastreport_loadreport_type", MyLib._myGlobal._userSearchScreenGroup, false);
            // this._myDataList1._gridData._columnList[
            int __columnIndex = this._myDataList1._gridData._findColumnByName(_g.d.sml_fastreport._table + "." + _g.d.sml_fastreport._report_type);
            //MyLib._myGrid._columnType __

            ((MyLib._myGrid._columnType)this._myDataList1._gridData._columnList[__columnIndex])._isColumnFilter = false;

            this._myDataList1._referFieldAdd(_g.d.sml_fastreport._menuid, 1);

            this._myDataList1._gridData._mouseDoubleClick += new MyLib.MouseDoubleClickHandler(_gridData__mouseDoubleClick);
            this._myDataList1._gridData._beforeDisplayRendering -= new MyLib.BeforeDisplayRenderRowEventHandler(_gridData__beforeDisplayRendering);
            this._myDataList1._gridData._beforeDisplayRendering += new MyLib.BeforeDisplayRenderRowEventHandler(_gridData__beforeDisplayRendering);
            /*
            this._myDataList1._gridData._addColumn("menuid", 1, 50, 30, false);
            this._myDataList1._gridData._addColumn("menuname", 1, 50, 70, false);


            try
            {
                string _query = "select menuid, name_1 from sml_fastreport";
                MyLib._myFrameWork __fw = new MyLib._myFrameWork();
                DataSet __result = __fw._query(MyLib._myGlobal._databaseName, _query);

                if (__result.Tables.Count > 0 && __result.Tables[0].Rows.Count > 0)
                {
                    for (int __i = 0; __i < __result.Tables[0].Rows.Count; __i++)
                    {
                        int __row = this._myDataList1._gridData._addRow();
                        this._myDataList1._gridData._cellUpdate(__i, 0, __result.Tables[0].Rows[__i]["menuid"], false);
                        this._myDataList1._gridData._cellUpdate(__i, 1, __result.Tables[0].Rows[__i]["name_1"], false);

                    }

                }

                this._myDataList1._gridData._mouseDoubleClick += (s1, e1) =>
                {
                    if (e1._row != -1)
                    {
                        if (_ReportSelected != null)
                        {
                            _ReportSelected(this, new SelectReportEvent(e1._row, __result.Tables[0].Rows[e1._row]["menuid"].ToString(), __result.Tables[0].Rows[e1._row]["menuname"].ToString()));
                            this.Dispose();
                        }
                    }

                };
            }
            catch (Exception ex)
            {
            }
             * */

        }

        MyLib.BeforeDisplayRowReturn _gridData__beforeDisplayRendering(MyLib._myGrid sender, int row, int columnNumber, string columnName, MyLib.BeforeDisplayRowReturn senderRow, MyLib._myGrid._columnType columnType, System.Collections.ArrayList rowData, Graphics e)
        {
            MyLib.BeforeDisplayRowReturn __result = senderRow;
            if (row < sender._rowData.Count && columnName.Equals(_g.d.sml_fastreport._table + "." + _g.d.sml_fastreport._report_type))
            {
                if (sender._cellGet(row, sender._findColumnByName(columnName)) == null)
                {
                    ((ArrayList)senderRow.newData)[columnNumber] = MyLib._myResource._findResource(_g.d.sml_fastreport._table + "." + _g.d.sml_fastreport._inventory_report)._str;
                }
                else
                {
                    int __mode = (int)MyLib._myGlobal._decimalPhase(sender._cellGet(row, sender._findColumnByName(columnName)).ToString());
                    //int __select = (int)MyLib._myGlobal._decimalPhase(sender._cellGet(row, sender._findColumnByName(this._gridSelect)).ToString());
                    ((ArrayList)senderRow.newData)[columnNumber] = "";
                    //if (__select == 1)
                    //{
                    switch (__mode)
                    {
                        case 1:
                            ((ArrayList)senderRow.newData)[columnNumber] = MyLib._myResource._findResource(_g.d.sml_fastreport._table + "." + _g.d.sml_fastreport._inventory_report)._str;
                            break;
                        case 2:
                            ((ArrayList)senderRow.newData)[columnNumber] = MyLib._myResource._findResource(_g.d.sml_fastreport._table + "." + _g.d.sml_fastreport._purchase_report)._str;
                            break;
                        case 3:
                            ((ArrayList)senderRow.newData)[columnNumber] = MyLib._myResource._findResource(_g.d.sml_fastreport._table + "." + _g.d.sml_fastreport._sale_report)._str;
                            break;
                        case 4:
                            ((ArrayList)senderRow.newData)[columnNumber] = MyLib._myResource._findResource(_g.d.sml_fastreport._table + "." + _g.d.sml_fastreport._supplier_report)._str;
                            break;
                        case 5:
                            ((ArrayList)senderRow.newData)[columnNumber] = MyLib._myResource._findResource(_g.d.sml_fastreport._table + "." + _g.d.sml_fastreport._customer_report)._str;
                            break;
                        case 6:
                            ((ArrayList)senderRow.newData)[columnNumber] = MyLib._myResource._findResource(_g.d.sml_fastreport._table + "." + _g.d.sml_fastreport._cashbank_report)._str;
                            break;
                        case 7:
                            ((ArrayList)senderRow.newData)[columnNumber] = MyLib._myResource._findResource(_g.d.sml_fastreport._table + "." + _g.d.sml_fastreport._asset_report)._str;
                            break;
                        case 8:
                            ((ArrayList)senderRow.newData)[columnNumber] = MyLib._myResource._findResource(_g.d.sml_fastreport._table + "." + _g.d.sml_fastreport._account_report)._str;
                            break;
                        default:
                            ((ArrayList)senderRow.newData)[columnNumber] = MyLib._myResource._findResource(_g.d.sml_fastreport._table + "." + _g.d.sml_fastreport._general_report)._str;
                            break;
                    }
                }
                //}
            }
            return __result;
        }

        void _gridData__mouseDoubleClick(object sender, MyLib.GridCellEventArgs e)
        {
            if (e._row != -1)
            {
                    string __tmp = ((MyLib._myGrid)sender)._cellGet(e._row, 1).ToString();
                    if (__tmp.Length > 0)
                    {
                        if (_ReportSelected != null)
                        {
                            int __type = (int)MyLib._myGlobal._decimalPhase(((MyLib._myGrid)sender)._cellGet(e._row, 2).ToString());
                            _ReportSelected(this, new SelectReportEvent(e._row, ((MyLib._myGrid)sender)._cellGet(e._row, 0).ToString(), ((MyLib._myGrid)sender)._cellGet(e._row, 1).ToString(), __type));
                        }
                        this.Close();
                    }
            }
        }

        void _buttonClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }

    public delegate void _AfterSelectReport(object sender, SelectReportEvent e);

    public class SelectReportEvent : EventArgs
    {
        private int _rowResult;
        private string _menuId;
        private string _menuName;
        private int _menuType;

        public SelectReportEvent()
        {

        }

        public SelectReportEvent(int __rowResult, string __menuId, string __menuName, int __menuType)
        {
            _rowResult = __rowResult;
            _menuId = __menuId;
            _menuName = __menuName;
            _menuType = __menuType;
        }

        public int result { get { return _rowResult; } set { _rowResult = value; } }
        public string menuId { get { return _menuId; } set { _menuId = value; } }
        public string menuName { get { return _menuName; } set { _menuName = value; } }
        public int menuType { get { return _menuType; } set { _menuType = value; } }
    }
}
