using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLERPReport.po
{
    public partial class _report_po_purchase_order_due_payment : UserControl
    {
        private SMLReport._report._objectListType __detailObject1;
        private SMLReport._report._objectListType __detailObject2;
        private DataTable _dataTableDue;
        private DataTable _dataTableDueDetail;

        public _report_po_purchase_order_due_payment()
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
            if (this._dataTableDue == null || this._dataTableDueDetail == null)
            {
                try
                {
                    Cursor.Current = Cursors.WaitCursor;
                    this._dataTableDue = new DataTable("due");
                    this._dataTableDue.Columns.Add("due_date");
                    this._dataTableDue.Columns.Add("doc_no");
                    this._dataTableDue.Columns.Add("doc_date");
                    this._dataTableDue.Columns.Add("ap_code");
                    this._dataTableDue.Columns.Add("ap_name");
                    this._dataTableDue.Columns.Add("sum_amount");
                    this._dataTableDue.Columns.Add("discount_footer");
                    this._dataTableDue.Columns.Add("tax");
                    this._dataTableDue.Columns.Add("tax_exclude");
                    this._dataTableDue.Columns.Add("net_amount");
                    DataRow __dataRow = this._dataTableDue.NewRow();
                    __dataRow[0] = "1/8/2552";
                    __dataRow[1] = "DN0001";
                    __dataRow[2] = "1/7/2552";
                    __dataRow[3] = "AP001";
                    __dataRow[4] = "APxx";
                    __dataRow[5] = "1000";
                    __dataRow[6] = "";
                    __dataRow[7] = "70";
                    __dataRow[8] = "";
                    __dataRow[9] = "1070";
                    this._dataTableDue.Rows.Add(__dataRow);

                    this._dataTableDueDetail = new DataTable("due_detail");
                    this._dataTableDueDetail.Columns.Add("ic_code");
                    this._dataTableDueDetail.Columns.Add("ic_name");
                    this._dataTableDueDetail.Columns.Add("unit");
                    this._dataTableDueDetail.Columns.Add("price");
                    this._dataTableDueDetail.Columns.Add("qty");
                    this._dataTableDueDetail.Columns.Add("sum_amount");
                    this._dataTableDueDetail.Columns.Add("discount_amount");
                    this._dataTableDueDetail.Columns.Add("money_amount");
                    this._dataTableDueDetail.Columns.Add("net_amount");
                    this._dataTableDueDetail.Columns.Add("doc_no");
                    __dataRow = this._dataTableDueDetail.NewRow();
                    __dataRow[0] = "IC0001";
                    __dataRow[1] = "ICxxx";
                    __dataRow[2] = "pcs";
                    __dataRow[3] = "100";
                    __dataRow[4] = "10";
                    __dataRow[5] = "1000";
                    __dataRow[6] = "";
                    __dataRow[7] = "1000";
                    __dataRow[8] = "1000";
                    __dataRow[9] = "DN0001";
                    this._dataTableDueDetail.Rows.Add(__dataRow);
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
                this._view1._addCell(__headerObject, __newColumn, true, 1, -1, SMLReport._report._cellType.String, "รายงานใบซื้อสินค้าที่ถึงกำหนดจ่ายเงิน", SMLReport._report._cellAlign.Left, this._view1._fontHeader2);
                this._view1._addCell(__headerObject, __newColumn, true, 2, -1, SMLReport._report._cellType.String, "วันที่พิมพ์ : " + SMLReport._report._reportValueDefault._currentDateTime, SMLReport._report._cellAlign.Left, this._view1._fontStandard);
                this._view1._addCell(__headerObject, __newColumn, true, 2, -1, SMLReport._report._cellType.String, "หน้า : " + SMLReport._report._reportValueDefault._pageNumber + "/" + SMLReport._report._reportValueDefault._pageTotal, SMLReport._report._cellAlign.Right, this._view1._fontStandard);
                return true;
            }
            //Write Detail Report
            else if (type == SMLReport._report._objectType.Detail)
            {
                //เส้นขอบบนรายละเอียด
                __detailObject1 = this._view1._addObject(this._view1._objectList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.Top);
                this._view1._addColumn(__detailObject1, 9, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "วันที่ถึงกำหนดส่ง", "", SMLReport._report._cellAlign.Left);
                this._view1._addColumn(__detailObject1, 9, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "เลขที่เอกสาร", "", SMLReport._report._cellAlign.Left);
                this._view1._addColumn(__detailObject1, 9, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "วันที่เอกสาร", "", SMLReport._report._cellAlign.Left);
                this._view1._addColumn(__detailObject1, 9, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "รหัสเจ้าหนี้", "", SMLReport._report._cellAlign.Left);
                this._view1._addColumn(__detailObject1, 19, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "ชื่อเจ้าหนี้", "", SMLReport._report._cellAlign.Left);
                this._view1._addColumn(__detailObject1, 9, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "รวมมูลค่าสินค้า", "", SMLReport._report._cellAlign.Right);
                this._view1._addColumn(__detailObject1, 9, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "ส่วนลดท้ายบิล", "", SMLReport._report._cellAlign.Right);
                this._view1._addColumn(__detailObject1, 9, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "ยอดภาษี", "", SMLReport._report._cellAlign.Right);
                this._view1._addColumn(__detailObject1, 9, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "ยอดเยกเว้นภาษี", "", SMLReport._report._cellAlign.Right);
                this._view1._addColumn(__detailObject1, 9, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "มูลค่าสุทธิทั้งบิล", "", SMLReport._report._cellAlign.Right);
                //พิมพ์ชื่อฟิลด์
                __detailObject2 = this._view1._addObject(this._view1._objectList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.Bottom);
                this._view1._addColumn(__detailObject2, 10, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "รหัสสินค้า", "", SMLReport._report._cellAlign.Left);
                this._view1._addColumn(__detailObject2, 20, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "ชื่อสินค้า", "", SMLReport._report._cellAlign.Left);
                this._view1._addColumn(__detailObject2, 10, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "หน่วยนับ", "", SMLReport._report._cellAlign.Left);
                this._view1._addColumn(__detailObject2, 10, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "ราคาซื้อ/หน่วย", "", SMLReport._report._cellAlign.Right);
                this._view1._addColumn(__detailObject2, 10, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "จำนวน", "", SMLReport._report._cellAlign.Right);
                this._view1._addColumn(__detailObject2, 10, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "ยอดรวม", "", SMLReport._report._cellAlign.Right);
                this._view1._addColumn(__detailObject2, 10, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "ส่วนลด", "", SMLReport._report._cellAlign.Right);
                this._view1._addColumn(__detailObject2, 10, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "จำนวนเงิน", "", SMLReport._report._cellAlign.Right);
                this._view1._addColumn(__detailObject2, 10, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "ยอดสุทธิ", "", SMLReport._report._cellAlign.Right);
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
            //this._view1._reportProgressBar.Maximum = this._dataTableDueDetail.Rows.Count;

            DataRow[] __dataRowsDue = this._dataTableDue.Select();
            for (int __rowDue = 0; __rowDue < __dataRowsDue.Length; __rowDue++)
            {
                __dataObject = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                this._view1._createEmtryColumn(__detailObject1, __dataObject);
                this._view1._addDataColumn(__detailObject1, __dataObject, 0, __dataRowsDue[__rowDue].ItemArray[0].ToString(), __newFont, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.String);
                this._view1._addDataColumn(__detailObject1, __dataObject, 1, __dataRowsDue[__rowDue].ItemArray[1].ToString(), __newFont, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.String);
                this._view1._addDataColumn(__detailObject1, __dataObject, 2, __dataRowsDue[__rowDue].ItemArray[2].ToString(), __newFont, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.String);
                this._view1._addDataColumn(__detailObject1, __dataObject, 3, __dataRowsDue[__rowDue].ItemArray[3].ToString(), __newFont, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.String);
                this._view1._addDataColumn(__detailObject1, __dataObject, 4, __dataRowsDue[__rowDue].ItemArray[4].ToString(), __newFont, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.String);
                this._view1._addDataColumn(__detailObject1, __dataObject, 5, __dataRowsDue[__rowDue].ItemArray[5].ToString(), __newFont, SMLReport._report._cellAlign.Right, 0,SMLReport._report._cellType.Number);
                this._view1._addDataColumn(__detailObject1, __dataObject, 6, __dataRowsDue[__rowDue].ItemArray[6].ToString(), __newFont, SMLReport._report._cellAlign.Right, 0,SMLReport._report._cellType.Number);
                this._view1._addDataColumn(__detailObject1, __dataObject, 7, __dataRowsDue[__rowDue].ItemArray[7].ToString(), __newFont, SMLReport._report._cellAlign.Right, 0,SMLReport._report._cellType.Number);
                this._view1._addDataColumn(__detailObject1, __dataObject, 8, __dataRowsDue[__rowDue].ItemArray[8].ToString(), __newFont, SMLReport._report._cellAlign.Right, 0,SMLReport._report._cellType.Number);
                this._view1._addDataColumn(__detailObject1, __dataObject, 9, __dataRowsDue[__rowDue].ItemArray[9].ToString(), __newFont, SMLReport._report._cellAlign.Right, 0,SMLReport._report._cellType.Number);

                DataRow[] __dataRowsDueDetail = this._dataTableDueDetail.Select("doc_no='" + __dataRowsDue[__rowDue].ItemArray[1].ToString() + "'");
                for (int __rowDueDetail = 0; __rowDueDetail < __dataRowsDueDetail.Length; __rowDueDetail++)
                {
                    __dataObject = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                    this._view1._createEmtryColumn(__detailObject2, __dataObject);
                    this._view1._addDataColumn(__detailObject2, __dataObject, 0, __dataRowsDueDetail[__rowDueDetail].ItemArray[0].ToString(), __newFont, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.String);
                    this._view1._addDataColumn(__detailObject2, __dataObject, 1, __dataRowsDueDetail[__rowDueDetail].ItemArray[1].ToString(), __newFont, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.String);
                    this._view1._addDataColumn(__detailObject2, __dataObject, 2, __dataRowsDueDetail[__rowDueDetail].ItemArray[2].ToString(), __newFont, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.String);
                    this._view1._addDataColumn(__detailObject2, __dataObject, 3, __dataRowsDueDetail[__rowDueDetail].ItemArray[3].ToString(), __newFont, SMLReport._report._cellAlign.Right, 0,SMLReport._report._cellType.Number);
                    this._view1._addDataColumn(__detailObject2, __dataObject, 4, __dataRowsDueDetail[__rowDueDetail].ItemArray[4].ToString(), __newFont, SMLReport._report._cellAlign.Right, 0,SMLReport._report._cellType.Number);
                    this._view1._addDataColumn(__detailObject2, __dataObject, 5, __dataRowsDueDetail[__rowDueDetail].ItemArray[5].ToString(), __newFont, SMLReport._report._cellAlign.Right, 0,SMLReport._report._cellType.Number);
                    this._view1._addDataColumn(__detailObject2, __dataObject, 6, __dataRowsDueDetail[__rowDueDetail].ItemArray[6].ToString(), __newFont, SMLReport._report._cellAlign.Right, 0,SMLReport._report._cellType.Number);
                    this._view1._addDataColumn(__detailObject2, __dataObject, 7, __dataRowsDueDetail[__rowDueDetail].ItemArray[7].ToString(), __newFont, SMLReport._report._cellAlign.Right, 0,SMLReport._report._cellType.Number);
                    this._view1._addDataColumn(__detailObject2, __dataObject, 8, __dataRowsDueDetail[__rowDueDetail].ItemArray[8].ToString(), __newFont, SMLReport._report._cellAlign.Right, 0,SMLReport._report._cellType.Number);
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
