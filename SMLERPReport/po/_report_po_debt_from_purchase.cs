﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLERPReport.po
{
    public partial class _report_po_debt_from_purchase : UserControl
    {
        private SMLReport._report._objectListType __detailObject;
        private DataTable _dataTable;

        public _report_po_debt_from_purchase()
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
                    this._dataTable = new DataTable("debt");
                    this._dataTable.Columns.Add("doc_date");
                    this._dataTable.Columns.Add("debt_no");
                    this._dataTable.Columns.Add("due_date");
                    this._dataTable.Columns.Add("tax_no");
                    this._dataTable.Columns.Add("tax_date");
                    this._dataTable.Columns.Add("amount_before_tax");
                    this._dataTable.Columns.Add("amount_tax");
                    this._dataTable.Columns.Add("amount_sum");
                    DataRow __dataRow = this._dataTable.NewRow();
                    __dataRow[0] = "30/6/2552";
                    __dataRow[1] = "DN001";
                    __dataRow[2] = "1/7/2552";
                    __dataRow[3] = "TN001";
                    __dataRow[4] = "30/6/2552";
                    __dataRow[5] = "1000";
                    __dataRow[6] = "70";
                    __dataRow[7] = "1070";
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
                this._view1._addCell(__headerObject, __newColumn, true, 1, -1, SMLReport._report._cellType.String, "รายงานใบตั้งหนี้จากการซื้อแบบสรุป-ตามวันที่", SMLReport._report._cellAlign.Left, this._view1._fontHeader2);
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
                this._view1._addColumn(__detailObject, 12, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "วันที่ออกเอกสาร", "", SMLReport._report._cellAlign.Left);
                this._view1._addColumn(__detailObject, 12, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "เลขที่ใบตั้งหนี้", "", SMLReport._report._cellAlign.Left);
                this._view1._addColumn(__detailObject, 12, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "วันที่ครบกำหนด", "", SMLReport._report._cellAlign.Left);
                this._view1._addColumn(__detailObject, 12, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "เลขที่ใบกำกับภาษี", "", SMLReport._report._cellAlign.Left);
                this._view1._addColumn(__detailObject, 12, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "วันที่ใบกำกับภาษี", "", SMLReport._report._cellAlign.Left);
                this._view1._addColumn(__detailObject, 12, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "ยอดเงินก่อนภาษี", "", SMLReport._report._cellAlign.Right);
                this._view1._addColumn(__detailObject, 12, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "ภาษีมูลค่าเพิ่ม", "", SMLReport._report._cellAlign.Right);
                this._view1._addColumn(__detailObject, 12, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "ยอดรวมทั้งสิ้น", "", SMLReport._report._cellAlign.Right);
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

            DataRow[] __dataRowsDebt = this._dataTable.Select();
            for (int __rowDebt = 0; __rowDebt < __dataRowsDebt.Length; __rowDebt++)
            {
                __dataObject = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                this._view1._createEmtryColumn(__detailObject, __dataObject);
                this._view1._addDataColumn(__detailObject, __dataObject, 0, __dataRowsDebt[__rowDebt].ItemArray[0].ToString(), __newFont, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.String);
                this._view1._addDataColumn(__detailObject, __dataObject, 1, __dataRowsDebt[__rowDebt].ItemArray[1].ToString(), __newFont, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.String);
                this._view1._addDataColumn(__detailObject, __dataObject, 2, __dataRowsDebt[__rowDebt].ItemArray[2].ToString(), __newFont, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.String);
                this._view1._addDataColumn(__detailObject, __dataObject, 3, __dataRowsDebt[__rowDebt].ItemArray[3].ToString(), __newFont, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.String);
                this._view1._addDataColumn(__detailObject, __dataObject, 4, __dataRowsDebt[__rowDebt].ItemArray[4].ToString(), __newFont, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.String);
                this._view1._addDataColumn(__detailObject, __dataObject, 5, __dataRowsDebt[__rowDebt].ItemArray[5].ToString(), __newFont, SMLReport._report._cellAlign.Right, 0,SMLReport._report._cellType.Number);
                this._view1._addDataColumn(__detailObject, __dataObject, 6, __dataRowsDebt[__rowDebt].ItemArray[6].ToString(), __newFont, SMLReport._report._cellAlign.Right, 0,SMLReport._report._cellType.Number);
                this._view1._addDataColumn(__detailObject, __dataObject, 7, __dataRowsDebt[__rowDebt].ItemArray[7].ToString(), __newFont, SMLReport._report._cellAlign.Right, 0,SMLReport._report._cellType.Number);
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
