using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLERPReport.po
{
    public partial class _report_po_purchase_sum_by_tax : UserControl
    {
        private SMLReport._report._objectListType __detailObject;
        private DataTable _dataTable;

        public _report_po_purchase_sum_by_tax()
        {
            InitializeComponent();

            this._view1._buttonCondition.Enabled = false;
            this._view1._buttonExample.Enabled = false;
            this._view1._loadData += new SMLReport._report.LoadDataEventHandler(_view1__loadData);
            this._view1._getObject += new SMLReport._report.GetObjectEventHandler(_view1__getObject);
            this._view1._getDataObject += new SMLReport._report.GetDataObjectEventHandler(_view1__getDataObject);
            this._view1._buttonBuildReport.Click += new EventHandler(_buttonBuildReport_Click);
            this._view1._buttonClose.Click += new EventHandler(_buttonClose_Click);
        }

        bool _view1__loadData()
        {
            if (this._dataTable == null)
            {
                try
                {
                    Cursor.Current = Cursors.WaitCursor;
                    this._dataTable = new DataTable("purchase");
                    this._dataTable.Columns.Add("tax_type");
                    this._dataTable.Columns.Add("po_no");
                    this._dataTable.Columns.Add("ic_code");
                    this._dataTable.Columns.Add("ic_name");
                    this._dataTable.Columns.Add("price");
                    this._dataTable.Columns.Add("qty");
                    this._dataTable.Columns.Add("discount");
                    this._dataTable.Columns.Add("amount");
                    this._dataTable.Columns.Add("amount_before_tax");
                    this._dataTable.Columns.Add("amount_tax");
                    this._dataTable.Columns.Add("amount_include_tax");
                    DataRow __dataRow = this._dataTable.NewRow();
                    __dataRow[0] = "0";
                    __dataRow[1] = "PN001";
                    __dataRow[2] = "IC001";
                    __dataRow[3] = "ICxxx";
                    __dataRow[4] = "100";
                    __dataRow[5] = "5";
                    __dataRow[6] = "";
                    __dataRow[7] = "500";
                    __dataRow[8] = "500";
                    __dataRow[9] = "35";
                    __dataRow[10] = "535";
                    this._dataTable.Rows.Add(__dataRow);
                }
                catch
                {
                    return false;
                }
                finally
                {
                    Cursor.Current = Cursors.Default;
                }
            }
            return true;
        }

        bool _view1__getObject(System.Collections.ArrayList objectList, SMLReport._report._objectType type)
        {
            //Write Header Report
            if (type == SMLReport._report._objectType.Header)
            {
                SMLReport._report._objectListType __headerObject = this._view1._addObject(this._view1._objectList, SMLReport._report._objectType.Header, true, 0, true, SMLReport._report._columnBorder.None);
                int __newColumn = this._view1._addColumn(__headerObject, 100);
                this._view1._addCell(__headerObject, __newColumn, true, 0, -1, SMLReport._report._cellType.String, SMLReport._report._reportValueDefault._ltdName, SMLReport._report._cellAlign.Center, this._view1._fontHeader1);
                this._view1._addCell(__headerObject, __newColumn, true, 1, -1, SMLReport._report._cellType.String, "รายงานการซื้อสินค้าสรุปตามประเภทภาษี", SMLReport._report._cellAlign.Left, this._view1._fontHeader2);
                this._view1._addCell(__headerObject, __newColumn, true, 2, -1, SMLReport._report._cellType.String, "วันที่พิมพ์ : " + SMLReport._report._reportValueDefault._currentDateTime, SMLReport._report._cellAlign.Left, this._view1._fontStandard);
                this._view1._addCell(__headerObject, __newColumn, true, 2, -1, SMLReport._report._cellType.String, "หน้า : " + SMLReport._report._reportValueDefault._pageNumber + "/" + SMLReport._report._reportValueDefault._pageTotal, SMLReport._report._cellAlign.Right, this._view1._fontStandard);
                return true;
            }
            //Write Detail Report
            else if (type == SMLReport._report._objectType.Detail)
            {
                //เส้นขอบบนรายละเอียด
                __detailObject = this._view1._addObject(this._view1._objectList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.Top);
                //พิมพ์ชื่อฟิลด์
                __detailObject = this._view1._addObject(this._view1._objectList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.Bottom);
                this._view1._addColumn(__detailObject, 8, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "ประเภทภาษี", "", SMLReport._report._cellAlign.Left);
                this._view1._addColumn(__detailObject, 8, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "เลขที่ใบซื้อ", "", SMLReport._report._cellAlign.Left);
                this._view1._addColumn(__detailObject, 8, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "รหัสสินค้า", "", SMLReport._report._cellAlign.Left);
                this._view1._addColumn(__detailObject, 20, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "ชื่อสินค้า", "", SMLReport._report._cellAlign.Left);
                this._view1._addColumn(__detailObject, 8, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "ราคาซื้อ/หน่วย", "", SMLReport._report._cellAlign.Right);
                this._view1._addColumn(__detailObject, 8, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "จำนวน", "", SMLReport._report._cellAlign.Right);
                this._view1._addColumn(__detailObject, 8, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "มูลค่าส่วนลด", "", SMLReport._report._cellAlign.Right);
                this._view1._addColumn(__detailObject, 8, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "มูลค่าสินค้า", "", SMLReport._report._cellAlign.Right);
                this._view1._addColumn(__detailObject, 8, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "ยอดก่อนภาษี", "", SMLReport._report._cellAlign.Right);
                this._view1._addColumn(__detailObject, 8, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "ยอดภาษี", "", SMLReport._report._cellAlign.Right);
                this._view1._addColumn(__detailObject, 8, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "ยอดรวมภาษี", "", SMLReport._report._cellAlign.Right);
                return true;
            }
            return false;
        }

        void _view1__getDataObject()
        {
            Font __newFont = null;
            SMLReport._report._columnListType __getColumn = (SMLReport._report._columnListType)__detailObject._columnList[0];
            SMLReport._report._objectListType __dataObject = null;
            //this._view1._reportProgressBar.Value = 0;
            //this._view1._reportProgressBar.Minimum = 0;
            //this._view1._reportProgressBar.Maximum = this._dataTable.Rows.Count;

            DataRow[] __dataRows = this._dataTable.Select();
            for (int __row = 0; __row < __dataRows.Length; __row++)
            {
                __dataObject = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                this._view1._createEmtryColumn(__detailObject, __dataObject);
                this._view1._addDataColumn(__detailObject, __dataObject, 0, __dataRows[__row].ItemArray[0].ToString(), __newFont, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.String);
                this._view1._addDataColumn(__detailObject, __dataObject, 1, __dataRows[__row].ItemArray[1].ToString(), __newFont, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.String);
                this._view1._addDataColumn(__detailObject, __dataObject, 2, __dataRows[__row].ItemArray[2].ToString(), __newFont, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.String);
                this._view1._addDataColumn(__detailObject, __dataObject, 3, __dataRows[__row].ItemArray[3].ToString(), __newFont, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.String);
                this._view1._addDataColumn(__detailObject, __dataObject, 4, __dataRows[__row].ItemArray[4].ToString(), __newFont, SMLReport._report._cellAlign.Right, 0,SMLReport._report._cellType.Number);
                this._view1._addDataColumn(__detailObject, __dataObject, 5, __dataRows[__row].ItemArray[5].ToString(), __newFont, SMLReport._report._cellAlign.Right, 0,SMLReport._report._cellType.Number);
                this._view1._addDataColumn(__detailObject, __dataObject, 6, __dataRows[__row].ItemArray[6].ToString(), __newFont, SMLReport._report._cellAlign.Right, 0,SMLReport._report._cellType.Number);
                this._view1._addDataColumn(__detailObject, __dataObject, 7, __dataRows[__row].ItemArray[7].ToString(), __newFont, SMLReport._report._cellAlign.Right, 0,SMLReport._report._cellType.Number);
                this._view1._addDataColumn(__detailObject, __dataObject, 8, __dataRows[__row].ItemArray[8].ToString(), __newFont, SMLReport._report._cellAlign.Right, 0,SMLReport._report._cellType.Number);
                this._view1._addDataColumn(__detailObject, __dataObject, 9, __dataRows[__row].ItemArray[9].ToString(), __newFont, SMLReport._report._cellAlign.Right, 0,SMLReport._report._cellType.Number);
                this._view1._addDataColumn(__detailObject, __dataObject, 10, __dataRows[__row].ItemArray[10].ToString(), __newFont, SMLReport._report._cellAlign.Right, 0,SMLReport._report._cellType.Number);
                //this._view1._reportProgressBar.Value++;
            }
        }

        void _buttonBuildReport_Click(object sender, EventArgs e)
        {
            this._view1._reportProgressBar.Style = ProgressBarStyle.Marquee;
            this._view1._buildReport(SMLReport._report._reportType.Standard);
            this._view1._reportProgressBar.Style = ProgressBarStyle.Blocks;
        }

        void _buttonClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
