using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLERPReport.po
{
    public partial class _report_po_rank_purchase_total : UserControl
    {
        private SMLReport._report._objectListType __detailObject1;
        private SMLReport._report._objectListType __detailObject2;
        private DataTable _dataTableRank;
        private DataTable _dataTableRankDetail;

        public _report_po_rank_purchase_total()
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
            if (this._dataTableRank == null || this._dataTableRankDetail == null)
            {
                try
                {
                    Cursor.Current = Cursors.WaitCursor;
                    this._dataTableRank = new DataTable("rank");
                    this._dataTableRank.Columns.Add("ic_group_code");
                    this._dataTableRank.Columns.Add("ic_group_name");
                    this._dataTableRank.Columns.Add("qty");
                    this._dataTableRank.Columns.Add("amount");
                    DataRow __dataRow = this._dataTableRank.NewRow();
                    __dataRow[0] = "ICG001";
                    __dataRow[1] = "IGCxxx";
                    __dataRow[2] = "10";
                    __dataRow[3] = "1000";
                    this._dataTableRank.Rows.Add(__dataRow);

                    this._dataTableRankDetail = new DataTable("rank_detail");
                    this._dataTableRankDetail.Columns.Add("ic_code");
                    this._dataTableRankDetail.Columns.Add("ic_name");
                    this._dataTableRankDetail.Columns.Add("unit");
                    this._dataTableRankDetail.Columns.Add("qty");
                    this._dataTableRankDetail.Columns.Add("price");
                    this._dataTableRankDetail.Columns.Add("amount");
                    this._dataTableRankDetail.Columns.Add("sum_amount");
                    this._dataTableRankDetail.Columns.Add("ic_group_code");
                    __dataRow = this._dataTableRankDetail.NewRow();
                    __dataRow[0] = "IC001";
                    __dataRow[1] = "ICxxx";
                    __dataRow[2] = "pcs";
                    __dataRow[3] = "10";
                    __dataRow[4] = "100";
                    __dataRow[5] = "1000";
                    __dataRow[6] = "1000";
                    __dataRow[7] = "ICG001";
                    this._dataTableRankDetail.Rows.Add(__dataRow);
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
                this._view1._addCell(__headerObject, __newColumn, true, 1, -1, SMLReport._report._cellType.String, "รายงานการจัดอันดับยอดซื้อ(ตามสินค้า-กลุ่มสินค้า)", SMLReport._report._cellAlign.Left, this._view1._fontHeader2);
                this._view1._addCell(__headerObject, __newColumn, true, 2, -1, SMLReport._report._cellType.String, "วันที่พิมพ์ : " + SMLReport._report._reportValueDefault._currentDateTime, SMLReport._report._cellAlign.Left, this._view1._fontStandard);
                this._view1._addCell(__headerObject, __newColumn, true, 2, -1, SMLReport._report._cellType.String, "หน้า : " + SMLReport._report._reportValueDefault._pageNumber + "/" + SMLReport._report._reportValueDefault._pageTotal, SMLReport._report._cellAlign.Right, this._view1._fontStandard);
                return true;
            }
            //Write Detail Report
            else if (type == SMLReport._report._objectType.Detail)
            {
                //เส้นขอบบนรายละเอียด
                __detailObject1 = this._view1._addObject(this._view1._objectList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.Top);
                this._view1._addColumn(__detailObject1, 15, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "รหัสกลุ่มสินค้า", "", SMLReport._report._cellAlign.Left);
                this._view1._addColumn(__detailObject1, 30, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "ชื่อกลุ่มสินค้า", "", SMLReport._report._cellAlign.Left);
                this._view1._addColumn(__detailObject1, 15, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "จำนวน", "", SMLReport._report._cellAlign.Right);
                this._view1._addColumn(__detailObject1, 15, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "มูลค่า", "", SMLReport._report._cellAlign.Right);
                //พิมพ์ชื่อฟิลด์
                __detailObject2 = this._view1._addObject(this._view1._objectList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.Bottom);
                this._view1._addColumn(__detailObject2, 10, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "รหัสสินค้า", "", SMLReport._report._cellAlign.Left);
                this._view1._addColumn(__detailObject2, 20, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "ชื่อสินค้า", "", SMLReport._report._cellAlign.Left);
                this._view1._addColumn(__detailObject2, 10, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "หน่วยนับ", "", SMLReport._report._cellAlign.Left);
                this._view1._addColumn(__detailObject2, 15, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "จำนวนซื้อ", "", SMLReport._report._cellAlign.Right);
                this._view1._addColumn(__detailObject2, 15, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "ราคา/หน่วย", "", SMLReport._report._cellAlign.Right);
                this._view1._addColumn(__detailObject2, 15, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "มูลค่าซื้อ", "", SMLReport._report._cellAlign.Right);
                this._view1._addColumn(__detailObject2, 15, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "ยอดสะสม", "", SMLReport._report._cellAlign.Right);
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
            //this._view1._reportProgressBar.Maximum = this._dataTableRankDetail.Rows.Count;

            DataRow[] __dataRowsRank = this._dataTableRank.Select();
            for (int __rowRank = 0; __rowRank < __dataRowsRank.Length; __rowRank++)
            {
                __dataObject = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                this._view1._createEmtryColumn(__detailObject1, __dataObject);
                this._view1._addDataColumn(__detailObject1, __dataObject, 0, __dataRowsRank[__rowRank].ItemArray[0].ToString(), __newFont, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.String);
                this._view1._addDataColumn(__detailObject1, __dataObject, 1, __dataRowsRank[__rowRank].ItemArray[1].ToString(), __newFont, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.String);
                this._view1._addDataColumn(__detailObject1, __dataObject, 2, __dataRowsRank[__rowRank].ItemArray[2].ToString(), __newFont, SMLReport._report._cellAlign.Right, 0,SMLReport._report._cellType.Number);
                this._view1._addDataColumn(__detailObject1, __dataObject, 3, __dataRowsRank[__rowRank].ItemArray[3].ToString(), __newFont, SMLReport._report._cellAlign.Right, 0,SMLReport._report._cellType.Number);

                DataRow[] __dataRowsRankDetail = this._dataTableRankDetail.Select("ic_group_code='" + __dataRowsRank[__rowRank].ItemArray[0].ToString() + "'");
                for (int __rowRankDetail = 0; __rowRankDetail < __dataRowsRankDetail.Length; __rowRankDetail++)
                {
                    __dataObject = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                    this._view1._createEmtryColumn(__detailObject2, __dataObject);
                    this._view1._addDataColumn(__detailObject2, __dataObject, 0, __dataRowsRankDetail[__rowRankDetail].ItemArray[0].ToString(), __newFont, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.String);
                    this._view1._addDataColumn(__detailObject2, __dataObject, 1, __dataRowsRankDetail[__rowRankDetail].ItemArray[1].ToString(), __newFont, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.String);
                    this._view1._addDataColumn(__detailObject2, __dataObject, 2, __dataRowsRankDetail[__rowRankDetail].ItemArray[2].ToString(), __newFont, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.String);
                    this._view1._addDataColumn(__detailObject2, __dataObject, 3, __dataRowsRankDetail[__rowRankDetail].ItemArray[3].ToString(), __newFont, SMLReport._report._cellAlign.Right, 0,SMLReport._report._cellType.Number);
                    this._view1._addDataColumn(__detailObject2, __dataObject, 4, __dataRowsRankDetail[__rowRankDetail].ItemArray[4].ToString(), __newFont, SMLReport._report._cellAlign.Right, 0,SMLReport._report._cellType.Number);
                    this._view1._addDataColumn(__detailObject2, __dataObject, 5, __dataRowsRankDetail[__rowRankDetail].ItemArray[5].ToString(), __newFont, SMLReport._report._cellAlign.Right, 0,SMLReport._report._cellType.Number);
                    this._view1._addDataColumn(__detailObject2, __dataObject, 6, __dataRowsRankDetail[__rowRankDetail].ItemArray[6].ToString(), __newFont, SMLReport._report._cellAlign.Right, 0,SMLReport._report._cellType.Number);
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
