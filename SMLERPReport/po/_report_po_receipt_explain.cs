using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLERPReport.po
{
    public partial class _report_po_receipt_explain : UserControl
    {
        private SMLReport._report._objectListType __detailObject1;
        private SMLReport._report._objectListType __detailObject2;
        private DataTable _dataTableReceipt;
        private DataTable _dataTableReceiptDetail;

        public _report_po_receipt_explain()
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
            if (this._dataTableReceipt == null || this._dataTableReceiptDetail == null)
            {
                try
                {
                    Cursor.Current = Cursors.WaitCursor;
                    this._dataTableReceipt = new DataTable("receipt");
                    this._dataTableReceipt.Columns.Add("doc_date");
                    DataRow __dataRow = this._dataTableReceipt.NewRow();
                    __dataRow[0] = "30/6/2552";
                    this._dataTableReceipt.Rows.Add(__dataRow);

                    this._dataTableReceiptDetail = new DataTable("receipt_detail");
                    this._dataTableReceiptDetail.Columns.Add("doc_no");
                    this._dataTableReceiptDetail.Columns.Add("purchase_no");
                    this._dataTableReceiptDetail.Columns.Add("tax_no");
                    this._dataTableReceiptDetail.Columns.Add("ic_code");
                    this._dataTableReceiptDetail.Columns.Add("ic_name");
                    this._dataTableReceiptDetail.Columns.Add("group_name");
                    this._dataTableReceiptDetail.Columns.Add("shelf_name");
                    this._dataTableReceiptDetail.Columns.Add("unit_name");
                    this._dataTableReceiptDetail.Columns.Add("qty");
                    this._dataTableReceiptDetail.Columns.Add("price");
                    this._dataTableReceiptDetail.Columns.Add("amount");
                    this._dataTableReceiptDetail.Columns.Add("doc_date");
                    __dataRow = this._dataTableReceiptDetail.NewRow();
                    __dataRow[0] = "DN001";
                    __dataRow[1] = "PN001";
                    __dataRow[2] = "TN001";
                    __dataRow[3] = "IC001";
                    __dataRow[4] = "ICxxx";
                    __dataRow[5] = "Group";
                    __dataRow[6] = "Shelf";
                    __dataRow[7] = "pcs";
                    __dataRow[8] = "10";
                    __dataRow[9] = "500";
                    __dataRow[10] = "5000";
                    __dataRow[11] = "30/6/2552";
                    this._dataTableReceiptDetail.Rows.Add(__dataRow);
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
                this._view1._addCell(__headerObject, __newColumn, true, 1, -1, SMLReport._report._cellType.String, "รายงานใบรับสินค้าแบบแจกแจง-ตามวันที่", SMLReport._report._cellAlign.Left, this._view1._fontHeader2);
                this._view1._addCell(__headerObject, __newColumn, true, 2, -1, SMLReport._report._cellType.String, "วันที่พิมพ์ : " + SMLReport._report._reportValueDefault._currentDateTime, SMLReport._report._cellAlign.Left, this._view1._fontStandard);
                this._view1._addCell(__headerObject, __newColumn, true, 2, -1, SMLReport._report._cellType.String, "หน้า : " + SMLReport._report._reportValueDefault._pageNumber + "/" + SMLReport._report._reportValueDefault._pageTotal, SMLReport._report._cellAlign.Right, this._view1._fontStandard);
                return true;
            }
            //Write Detail Report
            else if (type == SMLReport._report._objectType.Detail)
            {
                //เส้นขอบบนรายละเอียด
                __detailObject1 = this._view1._addObject(this._view1._objectList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.Top);
                this._view1._addColumn(__detailObject1, 15, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "วันที่เอกสาร", "", SMLReport._report._cellAlign.Left);
                //พิมพ์ชื่อฟิลด์
                __detailObject2 = this._view1._addObject(this._view1._objectList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.Bottom);
                this._view1._addColumn(__detailObject2, 8, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "เลขที่เอกสาร", "", SMLReport._report._cellAlign.Left);
                this._view1._addColumn(__detailObject2, 8, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "เลขที่ใบสั่งซื้อ", "", SMLReport._report._cellAlign.Left);
                this._view1._addColumn(__detailObject2, 8, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "เลขที่ใบกำกับภาษี", "", SMLReport._report._cellAlign.Left);
                this._view1._addColumn(__detailObject2, 8, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "รหัสสินค้า", "", SMLReport._report._cellAlign.Left);
                this._view1._addColumn(__detailObject2, 20, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "ชื่อสินค้า", "", SMLReport._report._cellAlign.Left);
                this._view1._addColumn(__detailObject2, 8, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "ชื่อกลุ่มสินค้า", "", SMLReport._report._cellAlign.Left);
                this._view1._addColumn(__detailObject2, 8, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "พื้นที่เก็บ", "", SMLReport._report._cellAlign.Left);
                this._view1._addColumn(__detailObject2, 8, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "หน่วยนับ", "", SMLReport._report._cellAlign.Left);
                this._view1._addColumn(__detailObject2, 8, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "จำนวน", "", SMLReport._report._cellAlign.Right);
                this._view1._addColumn(__detailObject2, 8, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "ราคาซื้อ/หน่วย", "", SMLReport._report._cellAlign.Right);
                this._view1._addColumn(__detailObject2, 8, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "มูลค่า", "", SMLReport._report._cellAlign.Right);
                return true;
            }
            return false;
        }

        void _view1__getDataObject()
        {
            Font __newFont = null;
            SMLReport._report._columnListType __getColumn = (SMLReport._report._columnListType)__detailObject2._columnList[0];
            SMLReport._report._objectListType __dataObject = null;
            //this._view1._reportProgressBar.Value = 0;
            //this._view1._reportProgressBar.Minimum = 0;
            //this._view1._reportProgressBar.Maximum = this._dataTableReceiptDetail.Rows.Count;

            DataRow[] __dataRowsReceipt = this._dataTableReceipt.Select();
            for (int __rowBilling = 0; __rowBilling < __dataRowsReceipt.Length; __rowBilling++)
            {
                __dataObject = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                this._view1._createEmtryColumn(__detailObject1, __dataObject);
                this._view1._addDataColumn(__detailObject1, __dataObject, 0, __dataRowsReceipt[__rowBilling].ItemArray[0].ToString(), __newFont, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.String);

                DataRow[] __dataRowsReceiptDetail = this._dataTableReceiptDetail.Select("doc_date='" + __dataRowsReceipt[__rowBilling].ItemArray[0].ToString() + "'");
                for (int __rowBillingDetail = 0; __rowBillingDetail < __dataRowsReceiptDetail.Length; __rowBillingDetail++)
                {
                    __dataObject = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                    this._view1._createEmtryColumn(__detailObject2, __dataObject);
                    this._view1._addDataColumn(__detailObject2, __dataObject, 0, __dataRowsReceiptDetail[__rowBillingDetail].ItemArray[0].ToString(), __newFont, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.String);
                    this._view1._addDataColumn(__detailObject2, __dataObject, 1, __dataRowsReceiptDetail[__rowBillingDetail].ItemArray[1].ToString(), __newFont, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.String);
                    this._view1._addDataColumn(__detailObject2, __dataObject, 2, __dataRowsReceiptDetail[__rowBillingDetail].ItemArray[2].ToString(), __newFont, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.String);
                    this._view1._addDataColumn(__detailObject2, __dataObject, 3, __dataRowsReceiptDetail[__rowBillingDetail].ItemArray[3].ToString(), __newFont, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.String);
                    this._view1._addDataColumn(__detailObject2, __dataObject, 4, __dataRowsReceiptDetail[__rowBillingDetail].ItemArray[4].ToString(), __newFont, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.String);
                    this._view1._addDataColumn(__detailObject2, __dataObject, 5, __dataRowsReceiptDetail[__rowBillingDetail].ItemArray[5].ToString(), __newFont, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.String);
                    this._view1._addDataColumn(__detailObject2, __dataObject, 6, __dataRowsReceiptDetail[__rowBillingDetail].ItemArray[6].ToString(), __newFont, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.String);
                    this._view1._addDataColumn(__detailObject2, __dataObject, 7, __dataRowsReceiptDetail[__rowBillingDetail].ItemArray[7].ToString(), __newFont, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.String);
                    this._view1._addDataColumn(__detailObject2, __dataObject, 8, __dataRowsReceiptDetail[__rowBillingDetail].ItemArray[8].ToString(), __newFont, SMLReport._report._cellAlign.Right, 0,SMLReport._report._cellType.Number);
                    this._view1._addDataColumn(__detailObject2, __dataObject, 9, __dataRowsReceiptDetail[__rowBillingDetail].ItemArray[9].ToString(), __newFont, SMLReport._report._cellAlign.Right, 0,SMLReport._report._cellType.Number);
                    this._view1._addDataColumn(__detailObject2, __dataObject, 10, __dataRowsReceiptDetail[__rowBillingDetail].ItemArray[10].ToString(), __newFont, SMLReport._report._cellAlign.Right, 0,SMLReport._report._cellType.Number);
                    //this._view1._reportProgressBar.Value++;
                }
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
