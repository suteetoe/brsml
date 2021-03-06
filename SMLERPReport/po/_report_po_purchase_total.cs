﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLERPReport.po
{
    public partial class _report_po_purchase_total : UserControl
    {
        private SMLReport._report._objectListType __detailObject1;
        private SMLReport._report._objectListType __detailObject2;
        private DataTable _dataTablePurchase;
        private DataTable _dataTablePurchaseDetail;

        public _report_po_purchase_total()
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
            if (this._dataTablePurchase == null || this._dataTablePurchaseDetail == null)
            {
                try
                {
                    Cursor.Current = Cursors.WaitCursor;
                    this._dataTablePurchase = new DataTable("purchase");
                    this._dataTablePurchase.Columns.Add("doc_date");
                    DataRow __dataRow = this._dataTablePurchase.NewRow();
                    __dataRow[0] = "30/6/2552";
                    this._dataTablePurchase.Rows.Add(__dataRow);

                    this._dataTablePurchaseDetail = new DataTable("purchase_detail");
                    this._dataTablePurchaseDetail.Columns.Add("ap_code");
                    this._dataTablePurchaseDetail.Columns.Add("ap_name");
                    this._dataTablePurchaseDetail.Columns.Add("tax_no");
                    this._dataTablePurchaseDetail.Columns.Add("purchase_no");
                    this._dataTablePurchaseDetail.Columns.Add("money");
                    this._dataTablePurchaseDetail.Columns.Add("money_tax");
                    this._dataTablePurchaseDetail.Columns.Add("return");
                    this._dataTablePurchaseDetail.Columns.Add("return_tax");
                    this._dataTablePurchaseDetail.Columns.Add("debt");
                    this._dataTablePurchaseDetail.Columns.Add("debt_tax");
                    this._dataTablePurchaseDetail.Columns.Add("net_amount");
                    this._dataTablePurchaseDetail.Columns.Add("doc_date");
                    __dataRow = this._dataTablePurchaseDetail.NewRow();
                    __dataRow[0] = "AP001";
                    __dataRow[1] = "APxxx";
                    __dataRow[2] = "TN001";
                    __dataRow[3] = "PN001";
                    __dataRow[4] = "1000";
                    __dataRow[5] = "70";
                    __dataRow[6] = "";
                    __dataRow[7] = "";
                    __dataRow[8] = "";
                    __dataRow[9] = "";
                    __dataRow[10] = "1000";
                    __dataRow[11] = "30/6/2552";
                    this._dataTablePurchaseDetail.Rows.Add(__dataRow);
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
                this._view1._addCell(__headerObject, __newColumn, true, 1, -1, SMLReport._report._cellType.String, "รายงานยอดซื้อ(ตามวันที่)", SMLReport._report._cellAlign.Left, this._view1._fontHeader2);
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
                this._view1._addColumn(__detailObject2, 8, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "รหัสเจ้าหนี้", "", SMLReport._report._cellAlign.Left);
                this._view1._addColumn(__detailObject2, 20, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "ชื่อเจ้าหนี้", "", SMLReport._report._cellAlign.Left);
                this._view1._addColumn(__detailObject2, 8, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "เลขที่ใบกำกับภาษี", "", SMLReport._report._cellAlign.Left);
                this._view1._addColumn(__detailObject2, 8, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "เลขที่ใบซื้อ", "", SMLReport._report._cellAlign.Left);
                this._view1._addColumn(__detailObject2, 8, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "จำนวนเงิน", "", SMLReport._report._cellAlign.Right);
                this._view1._addColumn(__detailObject2, 8, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "ภาษี", "", SMLReport._report._cellAlign.Right);
                this._view1._addColumn(__detailObject2, 8, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "มูลค่าส่งคืน", "", SMLReport._report._cellAlign.Right);
                this._view1._addColumn(__detailObject2, 8, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "ภาษี", "", SMLReport._report._cellAlign.Right);
                this._view1._addColumn(__detailObject2, 8, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "มูลค่าเพิ่มหนี้", "", SMLReport._report._cellAlign.Right);
                this._view1._addColumn(__detailObject2, 8, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "ภาษี", "", SMLReport._report._cellAlign.Right);
                this._view1._addColumn(__detailObject2, 8, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "มูลค่าสุทธิ", "", SMLReport._report._cellAlign.Right);
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
            //this._view1._reportProgressBar.Maximum = this._dataTablePurchaseDetail.Rows.Count;

            DataRow[] __dataRowsPurchase = this._dataTablePurchase.Select();
            for (int __rowPurchase = 0; __rowPurchase < __dataRowsPurchase.Length; __rowPurchase++)
            {
                __dataObject = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                this._view1._createEmtryColumn(__detailObject1, __dataObject);
                this._view1._addDataColumn(__detailObject1, __dataObject, 0, __dataRowsPurchase[__rowPurchase].ItemArray[0].ToString(), __newFont, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.String);

                DataRow[] __dataRowsPurchaseDetail = this._dataTablePurchaseDetail.Select("doc_date='" + __dataRowsPurchase[__rowPurchase].ItemArray[0].ToString() + "'");
                for (int __rowPurchaseDetail = 0; __rowPurchaseDetail < __dataRowsPurchaseDetail.Length; __rowPurchaseDetail++)
                {
                    __dataObject = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                    this._view1._createEmtryColumn(__detailObject2, __dataObject);
                    this._view1._addDataColumn(__detailObject2, __dataObject, 0, __dataRowsPurchaseDetail[__rowPurchaseDetail].ItemArray[0].ToString(), __newFont, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.String);
                    this._view1._addDataColumn(__detailObject2, __dataObject, 1, __dataRowsPurchaseDetail[__rowPurchaseDetail].ItemArray[1].ToString(), __newFont, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.String);
                    this._view1._addDataColumn(__detailObject2, __dataObject, 2, __dataRowsPurchaseDetail[__rowPurchaseDetail].ItemArray[2].ToString(), __newFont, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.String);
                    this._view1._addDataColumn(__detailObject2, __dataObject, 3, __dataRowsPurchaseDetail[__rowPurchaseDetail].ItemArray[3].ToString(), __newFont, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.String);
                    this._view1._addDataColumn(__detailObject2, __dataObject, 4, __dataRowsPurchaseDetail[__rowPurchaseDetail].ItemArray[4].ToString(), __newFont, SMLReport._report._cellAlign.Right, 0,SMLReport._report._cellType.Number);
                    this._view1._addDataColumn(__detailObject2, __dataObject, 5, __dataRowsPurchaseDetail[__rowPurchaseDetail].ItemArray[5].ToString(), __newFont, SMLReport._report._cellAlign.Right, 0,SMLReport._report._cellType.Number);
                    this._view1._addDataColumn(__detailObject2, __dataObject, 6, __dataRowsPurchaseDetail[__rowPurchaseDetail].ItemArray[6].ToString(), __newFont, SMLReport._report._cellAlign.Right, 0,SMLReport._report._cellType.Number);
                    this._view1._addDataColumn(__detailObject2, __dataObject, 7, __dataRowsPurchaseDetail[__rowPurchaseDetail].ItemArray[7].ToString(), __newFont, SMLReport._report._cellAlign.Right, 0,SMLReport._report._cellType.Number);
                    this._view1._addDataColumn(__detailObject2, __dataObject, 8, __dataRowsPurchaseDetail[__rowPurchaseDetail].ItemArray[8].ToString(), __newFont, SMLReport._report._cellAlign.Right, 0,SMLReport._report._cellType.Number);
                    this._view1._addDataColumn(__detailObject2, __dataObject, 9, __dataRowsPurchaseDetail[__rowPurchaseDetail].ItemArray[9].ToString(), __newFont, SMLReport._report._cellAlign.Right, 0,SMLReport._report._cellType.Number);
                    this._view1._addDataColumn(__detailObject2, __dataObject, 10, __dataRowsPurchaseDetail[__rowPurchaseDetail].ItemArray[10].ToString(), __newFont, SMLReport._report._cellAlign.Right, 0,SMLReport._report._cellType.Number);
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
