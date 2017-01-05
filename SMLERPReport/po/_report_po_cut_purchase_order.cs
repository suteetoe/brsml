using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLERPReport.po
{
    public partial class _report_po_cut_purchase_order : UserControl
    {
        private SMLReport._report._objectListType __detailObject1;
        private SMLReport._report._objectListType __detailObject2;
        private DataTable _dataTableCutPO;
        private DataTable _dataTableCutPODetail;

        public _report_po_cut_purchase_order()
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
            if (this._dataTableCutPO == null || this._dataTableCutPODetail == null)
            {
                try
                {
                    Cursor.Current = Cursors.WaitCursor;
                    this._dataTableCutPO = new DataTable("cut_po");
                    this._dataTableCutPO.Columns.Add("doc_no");
                    this._dataTableCutPO.Columns.Add("doc_date");
                    this._dataTableCutPO.Columns.Add("ap_code");
                    this._dataTableCutPO.Columns.Add("ap_name");
                    this._dataTableCutPO.Columns.Add("net_amount");
                    DataRow __dataRow = this._dataTableCutPO.NewRow();
                    __dataRow[0] = "DN0001";
                    __dataRow[1] = "30/6/2552";
                    __dataRow[2] = "AP01";
                    __dataRow[3] = "xxx";
                    __dataRow[4] = "1000";
                    this._dataTableCutPO.Rows.Add(__dataRow);

                    this._dataTableCutPODetail = new DataTable("cut_po_detail");
                    this._dataTableCutPODetail.Columns.Add("ic_code");
                    this._dataTableCutPODetail.Columns.Add("ic_name");
                    this._dataTableCutPODetail.Columns.Add("unit_name");
                    this._dataTableCutPODetail.Columns.Add("purchase_no");
                    this._dataTableCutPODetail.Columns.Add("purchase_date");
                    this._dataTableCutPODetail.Columns.Add("trans_type");
                    this._dataTableCutPODetail.Columns.Add("qty_before");
                    this._dataTableCutPODetail.Columns.Add("qty_purchase");
                    this._dataTableCutPODetail.Columns.Add("qty_remain");
                    this._dataTableCutPODetail.Columns.Add("doc_no");
                    __dataRow = this._dataTableCutPODetail.NewRow();
                    __dataRow[0] = "IC0001";
                    __dataRow[1] = "qwer";
                    __dataRow[2] = "pcs";
                    __dataRow[3] = "PN0001";
                    __dataRow[4] = "30/6/2552";
                    __dataRow[5] = "aa type";
                    __dataRow[6] = "5";
                    __dataRow[7] = "1";
                    __dataRow[8] = "4";
                    __dataRow[9] = "DN0001";
                    this._dataTableCutPODetail.Rows.Add(__dataRow);
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
                this._view1._addCell(__headerObject, __newColumn, true, 1, -1, SMLReport._report._cellType.String, "รายงานการตัดใบสั่งซื้อสินค้า", SMLReport._report._cellAlign.Left, this._view1._fontHeader2);
                this._view1._addCell(__headerObject, __newColumn, true, 2, -1, SMLReport._report._cellType.String, "วันที่พิมพ์ : " + SMLReport._report._reportValueDefault._currentDateTime, SMLReport._report._cellAlign.Left, this._view1._fontStandard);
                this._view1._addCell(__headerObject, __newColumn, true, 2, -1, SMLReport._report._cellType.String, "หน้า : " + SMLReport._report._reportValueDefault._pageNumber + "/" + SMLReport._report._reportValueDefault._pageTotal, SMLReport._report._cellAlign.Right, this._view1._fontStandard);
                return true;
            }
            //Write Detail Report
            else if (type == SMLReport._report._objectType.Detail)
            {
                //เส้นขอบบนรายละเอียด
                __detailObject1 = this._view1._addObject(this._view1._objectList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.Top);
                this._view1._addColumn(__detailObject1, 10, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "เลขที่ใบสั่งซื้อ", "", SMLReport._report._cellAlign.Left);
                this._view1._addColumn(__detailObject1, 10, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "วันที่ซื้อ", "", SMLReport._report._cellAlign.Left);
                this._view1._addColumn(__detailObject1, 10, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "รหัสเจ้าหนี้", "", SMLReport._report._cellAlign.Left);
                this._view1._addColumn(__detailObject1, 10, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "ชื่อเจ้าหนี้", "", SMLReport._report._cellAlign.Left);
                this._view1._addColumn(__detailObject1, 40, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "ยอดสุทธิ", "", SMLReport._report._cellAlign.Right);
                //พิมพ์ชื่อฟิลด์
                __detailObject2 = this._view1._addObject(this._view1._objectList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.Bottom);
                this._view1._addColumn(__detailObject2, 10, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "รหัสสินค้า", "", SMLReport._report._cellAlign.Left);
                this._view1._addColumn(__detailObject2, 20, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "ชื่อสินค้า", "", SMLReport._report._cellAlign.Left);
                this._view1._addColumn(__detailObject2, 10, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "หน่วยนับ", "", SMLReport._report._cellAlign.Left);
                this._view1._addColumn(__detailObject2, 10, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "เลขที่ใบสั่งซื้อ/รับสินค้า", "", SMLReport._report._cellAlign.Left);
                this._view1._addColumn(__detailObject2, 10, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "วันที่ซื้อ", "", SMLReport._report._cellAlign.Left);
                this._view1._addColumn(__detailObject2, 10, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "ประเภทเอกสาร", "", SMLReport._report._cellAlign.Left);
                this._view1._addColumn(__detailObject2, 10, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "จำนวนก่อนทำซื้อ", "", SMLReport._report._cellAlign.Right);
                this._view1._addColumn(__detailObject2, 10, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "จำนวนที่ทำซื้อ", "", SMLReport._report._cellAlign.Right);
                this._view1._addColumn(__detailObject2, 10, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "จำนวนคงเหลือ", "", SMLReport._report._cellAlign.Right);
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
            //this._view1._reportProgressBar.Maximum = this._dataTableCutPODetail.Rows.Count;

            DataRow[] __dataRowsBilling = this._dataTableCutPO.Select();
            for (int __rowBilling = 0; __rowBilling < __dataRowsBilling.Length; __rowBilling++)
            {
                __dataObject = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                this._view1._createEmtryColumn(__detailObject1, __dataObject);
                this._view1._addDataColumn(__detailObject1, __dataObject, 0, __dataRowsBilling[__rowBilling].ItemArray[0].ToString(), __newFont, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.String);
                this._view1._addDataColumn(__detailObject1, __dataObject, 1, __dataRowsBilling[__rowBilling].ItemArray[1].ToString(), __newFont, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.String);
                this._view1._addDataColumn(__detailObject1, __dataObject, 2, __dataRowsBilling[__rowBilling].ItemArray[2].ToString(), __newFont, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.String);
                this._view1._addDataColumn(__detailObject1, __dataObject, 3, __dataRowsBilling[__rowBilling].ItemArray[3].ToString(), __newFont, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.String);
                this._view1._addDataColumn(__detailObject1, __dataObject, 4, __dataRowsBilling[__rowBilling].ItemArray[4].ToString(), __newFont, SMLReport._report._cellAlign.Right, 0,SMLReport._report._cellType.Number);

                DataRow[] __dataRowsBillingDetail = this._dataTableCutPODetail.Select("doc_no='" + __dataRowsBilling[__rowBilling].ItemArray[0].ToString() + "'");
                for (int __rowBillingDetail = 0; __rowBillingDetail < __dataRowsBillingDetail.Length; __rowBillingDetail++)
                {
                    __dataObject = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                    this._view1._createEmtryColumn(__detailObject2, __dataObject);
                    this._view1._addDataColumn(__detailObject2, __dataObject, 0, __dataRowsBillingDetail[__rowBillingDetail].ItemArray[0].ToString(), __newFont, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.String);
                    this._view1._addDataColumn(__detailObject2, __dataObject, 1, __dataRowsBillingDetail[__rowBillingDetail].ItemArray[1].ToString(), __newFont, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.String);
                    this._view1._addDataColumn(__detailObject2, __dataObject, 2, __dataRowsBillingDetail[__rowBillingDetail].ItemArray[2].ToString(), __newFont, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.String);
                    this._view1._addDataColumn(__detailObject2, __dataObject, 3, __dataRowsBillingDetail[__rowBillingDetail].ItemArray[3].ToString(), __newFont, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.String);
                    this._view1._addDataColumn(__detailObject2, __dataObject, 4, __dataRowsBillingDetail[__rowBillingDetail].ItemArray[4].ToString(), __newFont, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.String);
                    this._view1._addDataColumn(__detailObject2, __dataObject, 5, __dataRowsBillingDetail[__rowBillingDetail].ItemArray[5].ToString(), __newFont, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.String);
                    this._view1._addDataColumn(__detailObject2, __dataObject, 6, __dataRowsBillingDetail[__rowBillingDetail].ItemArray[6].ToString(), __newFont, SMLReport._report._cellAlign.Right, 0,SMLReport._report._cellType.Number);
                    this._view1._addDataColumn(__detailObject2, __dataObject, 7, __dataRowsBillingDetail[__rowBillingDetail].ItemArray[7].ToString(), __newFont, SMLReport._report._cellAlign.Right, 0,SMLReport._report._cellType.Number);
                    this._view1._addDataColumn(__detailObject2, __dataObject, 8, __dataRowsBillingDetail[__rowBillingDetail].ItemArray[8].ToString(), __newFont, SMLReport._report._cellAlign.Right, 0,SMLReport._report._cellType.Number);
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
