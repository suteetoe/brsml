using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLERPReport.po
{
    public partial class _report_po_purchase_analyze : UserControl
    {
        private SMLReport._report._objectListType __detailObject;
        private DataTable _dataTable;

        public _report_po_purchase_analyze()
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
                    this._dataTable.Columns.Add("ic_code");
                    this._dataTable.Columns.Add("ic_name");
                    this._dataTable.Columns.Add("unit");
                    this._dataTable.Columns.Add("qty_purchase");
                    this._dataTable.Columns.Add("qty_return");
                    this._dataTable.Columns.Add("qty_add");
                    this._dataTable.Columns.Add("amount_purchase");
                    this._dataTable.Columns.Add("amount_return");
                    this._dataTable.Columns.Add("amount_add");
                    this._dataTable.Columns.Add("profit_and_lost");
                    DataRow __dataRow = this._dataTable.NewRow();
                    __dataRow[0] = "IC001";
                    __dataRow[1] = "ICxxx";
                    __dataRow[2] = "pcs";
                    __dataRow[3] = "50";
                    __dataRow[4] = "5";
                    __dataRow[5] = "";
                    __dataRow[6] = "500";
                    __dataRow[7] = "50";
                    __dataRow[8] = "";
                    __dataRow[9] = "";
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
                this._view1._addCell(__headerObject, __newColumn, true, 1, -1, SMLReport._report._cellType.String, "รายงานวิเคราะห์การซื้อสุทธิ-ตามสินค้า", SMLReport._report._cellAlign.Left, this._view1._fontHeader2);
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
                this._view1._addColumn(__detailObject, 9, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "รหัสสินค้า", "", SMLReport._report._cellAlign.Left);
                this._view1._addColumn(__detailObject, 19, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "ชื่อสินค้า", "", SMLReport._report._cellAlign.Left);
                this._view1._addColumn(__detailObject, 9, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "หน่วยนับ", "", SMLReport._report._cellAlign.Left);
                this._view1._addColumn(__detailObject, 9, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "จำนวนซื้อ", "", SMLReport._report._cellAlign.Right);
                this._view1._addColumn(__detailObject, 9, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "จำนวนคืน", "", SMLReport._report._cellAlign.Right);
                this._view1._addColumn(__detailObject, 9, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "จำนวนเพิ่ม", "", SMLReport._report._cellAlign.Right);
                this._view1._addColumn(__detailObject, 9, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "มูลค่าซื้อ", "", SMLReport._report._cellAlign.Right);
                this._view1._addColumn(__detailObject, 9, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "มูลค่าคืน", "", SMLReport._report._cellAlign.Right);
                this._view1._addColumn(__detailObject, 9, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "มูลค่าเพิ่มหนี้", "", SMLReport._report._cellAlign.Right);
                this._view1._addColumn(__detailObject, 9, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "กำไร(ขาดทุน)", "", SMLReport._report._cellAlign.Right);
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

            DataRow[] __dataRowsBilling = this._dataTable.Select();
            for (int __rowBilling = 0; __rowBilling < __dataRowsBilling.Length; __rowBilling++)
            {
                __dataObject = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                this._view1._createEmtryColumn(__detailObject, __dataObject);
                this._view1._addDataColumn(__detailObject, __dataObject, 0, __dataRowsBilling[__rowBilling].ItemArray[0].ToString(), __newFont, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.String);
                this._view1._addDataColumn(__detailObject, __dataObject, 1, __dataRowsBilling[__rowBilling].ItemArray[1].ToString(), __newFont, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.String);
                this._view1._addDataColumn(__detailObject, __dataObject, 2, __dataRowsBilling[__rowBilling].ItemArray[2].ToString(), __newFont, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.String);
                this._view1._addDataColumn(__detailObject, __dataObject, 3, __dataRowsBilling[__rowBilling].ItemArray[3].ToString(), __newFont, SMLReport._report._cellAlign.Right, 0,SMLReport._report._cellType.Number);
                this._view1._addDataColumn(__detailObject, __dataObject, 4, __dataRowsBilling[__rowBilling].ItemArray[4].ToString(), __newFont, SMLReport._report._cellAlign.Right, 0,SMLReport._report._cellType.Number);
                this._view1._addDataColumn(__detailObject, __dataObject, 5, __dataRowsBilling[__rowBilling].ItemArray[5].ToString(), __newFont, SMLReport._report._cellAlign.Right, 0,SMLReport._report._cellType.Number);
                this._view1._addDataColumn(__detailObject, __dataObject, 6, __dataRowsBilling[__rowBilling].ItemArray[6].ToString(), __newFont, SMLReport._report._cellAlign.Right, 0,SMLReport._report._cellType.Number);
                this._view1._addDataColumn(__detailObject, __dataObject, 7, __dataRowsBilling[__rowBilling].ItemArray[7].ToString(), __newFont, SMLReport._report._cellAlign.Right, 0,SMLReport._report._cellType.Number);
                this._view1._addDataColumn(__detailObject, __dataObject, 8, __dataRowsBilling[__rowBilling].ItemArray[8].ToString(), __newFont, SMLReport._report._cellAlign.Right, 0,SMLReport._report._cellType.Number);
                this._view1._addDataColumn(__detailObject, __dataObject, 9, __dataRowsBilling[__rowBilling].ItemArray[9].ToString(), __newFont, SMLReport._report._cellAlign.Right, 0,SMLReport._report._cellType.Number);
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
