using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLERPICReport
{
    public partial class _icReportSort : UserControl
    {
        MyLib._searchDataFull _productSearchDialog;

        Boolean __onLoad = false;

        public _icReportSort()
        {
            InitializeComponent();

            this._gridProduct._table_name = _g.d.ic_report_sort_item._table;
            this._gridProduct._addColumn(_g.d.ic_report_sort_item._ic_code, 1, 30, 30, true, false, true, true);
            this._gridProduct._addColumn(_g.d.ic_report_sort_item._ic_name, 1, 70, 70, false, false, false, false);
            this._gridProduct._calcPersentWidthToScatter();
            this._gridProduct._clickSearchButton += _gridProduct__clickSearchButton;
            this._gridProduct._alterCellUpdate += _gridProduct__alterCellUpdate;

            this._loadData();
        }

        private void _gridProduct__alterCellUpdate(object sender, int row, int column)
        {
            int __columnItemCode = this._gridProduct._findColumnByName(_g.d.ic_report_sort_item._ic_code);
            if (column == __columnItemCode)
            {
                // search item name
                if (__onLoad == false)
                {
                    string __productCode = this._gridProduct._cellGet(row, __columnItemCode).ToString();
                    MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();

                    DataTable __product = __myFrameWork._queryShort("select name_1 from ic_inventory where code = \'" + __productCode + "\' ").Tables[0];

                    if (__product.Rows.Count > 0)
                    {
                        string __productName = __product.Rows[0][0].ToString();
                        this._gridProduct._cellUpdate(row, _g.d.ic_report_sort_item._ic_name, __productName, true);
                    }

                }
            }
        }

        private void _gridProduct__clickSearchButton(object sender, MyLib.GridCellEventArgs e)
        {
            if (this._productSearchDialog == null)
            {
                this._productSearchDialog = new MyLib._searchDataFull();

                this._productSearchDialog._dataList._loadViewFormat(_g.g._search_screen_ic_inventory, MyLib._myGlobal._userSearchScreenGroup, false);
                this._productSearchDialog._dataList._gridData._mouseClick += _gridData__mouseClick;
                this._productSearchDialog._searchEnterKeyPress += _productSearchDialog__searchEnterKeyPress;

                this._productSearchDialog.StartPosition = FormStartPosition.CenterScreen;
            }
            this._productSearchDialog.ShowDialog();
        }

        private void _productSearchDialog__searchEnterKeyPress(MyLib._myGrid sender, int row)
        {
            if (row != -1)
            {
                string __getIcCode = this._productSearchDialog._dataList._gridData._cellGet(this._productSearchDialog._dataList._gridData._selectRow, _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code).ToString();
                this._gridProduct._cellUpdate(this._gridProduct._selectRow, _g.d.ic_report_sort_item._ic_code, __getIcCode, true);
                this._productSearchDialog.Close();
            }
        }

        private void _gridData__mouseClick(object sender, MyLib.GridCellEventArgs e)
        {
            if (e._row != -1)
            {
                string __getIcCode = this._productSearchDialog._dataList._gridData._cellGet(this._productSearchDialog._dataList._gridData._selectRow, _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code).ToString();
                this._gridProduct._cellUpdate(this._gridProduct._selectRow, _g.d.ic_report_sort_item._ic_code, __getIcCode, true);
                this._productSearchDialog.Close();

            }
        }

        void _loadData()
        {
            string __query = "select ic_code, (select name_1 from ic_inventory where ic_inventory.code = ic_report_sort_item.ic_code) as ic_name, line_number from ic_report_sort_item " +
                " union all " +
                " select code as ic_code, name_1 as ic_name, 999999 as line_number from ic_inventory where not exists(select ic_code from ic_report_sort_item where ic_report_sort_item.ic_code = ic_inventory.code) " +
                " order by line_number, ic_code ";

            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            DataTable __itemSort = __myFrameWork._queryShort(__query).Tables[0];

            this._gridProduct._loadFromDataTable(__itemSort);

        }

        private void _closeButton_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void _saveButton_Click(object sender, EventArgs e)
        {
            _saveData();
        }

        void _saveData()
        {

            this._gridProduct._removeLastControl();

            StringBuilder __query = new StringBuilder(MyLib._myGlobal._xmlHeader + "<node>");

            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from ic_report_sort_item "));

            this._gridProduct._updateRowIsChangeAll(true);
            __query.Append(this._gridProduct._createQueryForInsert(_g.d.ic_report_sort_item._table, "", "", false, true));

            __query.Append("</node>");

            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();


            string __result = __myFrameWork._queryList(MyLib._myGlobal._databaseName, __query.ToString());

            if (__result.Length == 0)
            {
                MyLib._myGlobal._displayWarning(1, null);
            }
            else
            {
                MessageBox.Show(__result, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
