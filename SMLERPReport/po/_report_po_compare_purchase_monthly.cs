using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLERPReport.po
{
    public partial class _report_po_compare_purchase_monthly : UserControl
    {
        private SMLReport._report._objectListType __detailObject;
        private DataTable _dataTableIC;
        private DataTable _dataTableICDetail;

        public _report_po_compare_purchase_monthly()
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
            if (this._dataTableIC == null || this._dataTableICDetail == null)
            {
                try
                {
                    Cursor.Current = Cursors.WaitCursor;
                    this._dataTableIC = new DataTable("ic");
                    this._dataTableIC.Columns.Add("ic_code");
                    this._dataTableIC.Columns.Add("ic_name");
                    this._dataTableIC.Columns.Add("unit");
                    this._dataTableIC.Columns.Add("price");
                    DataRow __dataRow = this._dataTableIC.NewRow();
                    __dataRow[0] = "IC001";
                    __dataRow[1] = "ICxxx";
                    __dataRow[2] = "pcs";
                    __dataRow[3] = "100";
                    this._dataTableIC.Rows.Add(__dataRow);

                    this._dataTableICDetail = new DataTable("ic_detail");
                    this._dataTableICDetail.Columns.Add("description");
                    this._dataTableICDetail.Columns.Add("jan");
                    this._dataTableICDetail.Columns.Add("feb");
                    this._dataTableICDetail.Columns.Add("mar");
                    this._dataTableICDetail.Columns.Add("apr");
                    this._dataTableICDetail.Columns.Add("may");
                    this._dataTableICDetail.Columns.Add("jun");
                    this._dataTableICDetail.Columns.Add("jul");
                    this._dataTableICDetail.Columns.Add("aug");
                    this._dataTableICDetail.Columns.Add("sep");
                    this._dataTableICDetail.Columns.Add("oct");
                    this._dataTableICDetail.Columns.Add("mov");
                    this._dataTableICDetail.Columns.Add("dec");
                    this._dataTableICDetail.Columns.Add("sum");
                    this._dataTableICDetail.Columns.Add("ic_code");
                    __dataRow = this._dataTableICDetail.NewRow();
                    __dataRow[0] = "qty";
                    __dataRow[1] = "5";
                    __dataRow[2] = "10";
                    __dataRow[3] = "20";
                    __dataRow[4] = "";
                    __dataRow[5] = "";
                    __dataRow[6] = "";
                    __dataRow[7] = "";
                    __dataRow[8] = "";
                    __dataRow[9] = "";
                    __dataRow[10] = "";
                    __dataRow[11] = "";
                    __dataRow[12] = "";
                    __dataRow[13] = "35";
                    __dataRow[14] = "IC001";
                    this._dataTableICDetail.Rows.Add(__dataRow);
                    __dataRow = this._dataTableICDetail.NewRow();
                    __dataRow[0] = "price";
                    __dataRow[1] = "500";
                    __dataRow[2] = "1000";
                    __dataRow[3] = "2000";
                    __dataRow[4] = "";
                    __dataRow[5] = "";
                    __dataRow[6] = "";
                    __dataRow[7] = "";
                    __dataRow[8] = "";
                    __dataRow[9] = "";
                    __dataRow[10] = "";
                    __dataRow[11] = "";
                    __dataRow[12] = "";
                    __dataRow[13] = "3500";
                    __dataRow[14] = "IC001";
                    this._dataTableICDetail.Rows.Add(__dataRow);
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
                this._view1._addCell(__headerObject, __newColumn, true, 1, -1, SMLReport._report._cellType.String, "รายงานเปรียบเทียบยอดซื้อสินค้า12เดือน(ตามสินค้า-ราคา/ปริมาณ)", SMLReport._report._cellAlign.Left, this._view1._fontHeader2);
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
                this._view1._addColumn(__detailObject, 6, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "รหัสสินค้า", "", SMLReport._report._cellAlign.Left);
                this._view1._addColumn(__detailObject, 17, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "ชื่อสินค้า", "", SMLReport._report._cellAlign.Left);
                this._view1._addColumn(__detailObject, 6, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "หน่วย", "", SMLReport._report._cellAlign.Left);
                this._view1._addColumn(__detailObject, 6, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "ราคาเฉลี่ย", "", SMLReport._report._cellAlign.Right);
                this._view1._addColumn(__detailObject, 5, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "มค", "", SMLReport._report._cellAlign.Right);
                this._view1._addColumn(__detailObject, 5, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "กพ", "", SMLReport._report._cellAlign.Right);
                this._view1._addColumn(__detailObject, 5, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "มีค", "", SMLReport._report._cellAlign.Right);
                this._view1._addColumn(__detailObject, 5, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "เมย", "", SMLReport._report._cellAlign.Right);
                this._view1._addColumn(__detailObject, 5, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "พค", "", SMLReport._report._cellAlign.Right);
                this._view1._addColumn(__detailObject, 5, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "มิย", "", SMLReport._report._cellAlign.Right);
                this._view1._addColumn(__detailObject, 5, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "กค", "", SMLReport._report._cellAlign.Right);
                this._view1._addColumn(__detailObject, 5, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "สค", "", SMLReport._report._cellAlign.Right);
                this._view1._addColumn(__detailObject, 5, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "กย", "", SMLReport._report._cellAlign.Right);
                this._view1._addColumn(__detailObject, 5, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "ตค", "", SMLReport._report._cellAlign.Right);
                this._view1._addColumn(__detailObject, 5, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "พย", "", SMLReport._report._cellAlign.Right);
                this._view1._addColumn(__detailObject, 5, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "ธค", "", SMLReport._report._cellAlign.Right);
                this._view1._addColumn(__detailObject, 5, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "รวม", "", SMLReport._report._cellAlign.Right);
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
            //this._view1._reportProgressBar.Maximum = this._dataTableICDetail.Rows.Count;

            DataRow[] __dataRowsIC = this._dataTableIC.Select();
            for (int __rowIC = 0; __rowIC < __dataRowsIC.Length; __rowIC++)
            {
                __dataObject = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                this._view1._createEmtryColumn(__detailObject, __dataObject);
                this._view1._addDataColumn(__detailObject, __dataObject, 0, __dataRowsIC[__rowIC].ItemArray[0].ToString(), __newFont, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.String);
                this._view1._addDataColumn(__detailObject, __dataObject, 1, __dataRowsIC[__rowIC].ItemArray[1].ToString(), __newFont, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.String);
                this._view1._addDataColumn(__detailObject, __dataObject, 2, __dataRowsIC[__rowIC].ItemArray[2].ToString(), __newFont, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.String);
                this._view1._addDataColumn(__detailObject, __dataObject, 3, __dataRowsIC[__rowIC].ItemArray[3].ToString(), __newFont, SMLReport._report._cellAlign.Right, 0,SMLReport._report._cellType.Number);
                
                DataRow[] __dataRowsICDetail = this._dataTableICDetail.Select("ic_code='" + __dataRowsIC[__rowIC].ItemArray[0].ToString() + "'");
                for (int __rowICDetail = 0; __rowICDetail < __dataRowsICDetail.Length; __rowICDetail++)
                {
                    __dataObject = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                    this._view1._createEmtryColumn(__detailObject, __dataObject);
                    this._view1._addDataColumn(__detailObject, __dataObject, 1, __dataRowsICDetail[__rowICDetail].ItemArray[0].ToString(), __newFont, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.String);
                    this._view1._addDataColumn(__detailObject, __dataObject, 4, __dataRowsICDetail[__rowICDetail].ItemArray[1].ToString(), __newFont, SMLReport._report._cellAlign.Right, 0,SMLReport._report._cellType.Number);
                    this._view1._addDataColumn(__detailObject, __dataObject, 5, __dataRowsICDetail[__rowICDetail].ItemArray[2].ToString(), __newFont, SMLReport._report._cellAlign.Right, 0,SMLReport._report._cellType.Number);
                    this._view1._addDataColumn(__detailObject, __dataObject, 6, __dataRowsICDetail[__rowICDetail].ItemArray[3].ToString(), __newFont, SMLReport._report._cellAlign.Right, 0,SMLReport._report._cellType.Number);
                    this._view1._addDataColumn(__detailObject, __dataObject, 7, __dataRowsICDetail[__rowICDetail].ItemArray[4].ToString(), __newFont, SMLReport._report._cellAlign.Right, 0,SMLReport._report._cellType.Number);
                    this._view1._addDataColumn(__detailObject, __dataObject, 8, __dataRowsICDetail[__rowICDetail].ItemArray[5].ToString(), __newFont, SMLReport._report._cellAlign.Right, 0,SMLReport._report._cellType.Number);
                    this._view1._addDataColumn(__detailObject, __dataObject, 9, __dataRowsICDetail[__rowICDetail].ItemArray[6].ToString(), __newFont, SMLReport._report._cellAlign.Right, 0,SMLReport._report._cellType.Number);
                    this._view1._addDataColumn(__detailObject, __dataObject, 10, __dataRowsICDetail[__rowICDetail].ItemArray[7].ToString(), __newFont, SMLReport._report._cellAlign.Right, 0,SMLReport._report._cellType.Number);
                    this._view1._addDataColumn(__detailObject, __dataObject, 11, __dataRowsICDetail[__rowICDetail].ItemArray[8].ToString(), __newFont, SMLReport._report._cellAlign.Right, 0,SMLReport._report._cellType.Number);
                    this._view1._addDataColumn(__detailObject, __dataObject, 12, __dataRowsICDetail[__rowICDetail].ItemArray[9].ToString(), __newFont, SMLReport._report._cellAlign.Right, 0,SMLReport._report._cellType.Number);
                    this._view1._addDataColumn(__detailObject, __dataObject, 13, __dataRowsICDetail[__rowICDetail].ItemArray[10].ToString(), __newFont, SMLReport._report._cellAlign.Right, 0,SMLReport._report._cellType.Number);
                    this._view1._addDataColumn(__detailObject, __dataObject, 14, __dataRowsICDetail[__rowICDetail].ItemArray[11].ToString(), __newFont, SMLReport._report._cellAlign.Right, 0,SMLReport._report._cellType.Number);
                    this._view1._addDataColumn(__detailObject, __dataObject, 15, __dataRowsICDetail[__rowICDetail].ItemArray[12].ToString(), __newFont, SMLReport._report._cellAlign.Right, 0,SMLReport._report._cellType.Number);
                    this._view1._addDataColumn(__detailObject, __dataObject, 16, __dataRowsICDetail[__rowICDetail].ItemArray[13].ToString(), __newFont, SMLReport._report._cellAlign.Right, 0,SMLReport._report._cellType.Number);
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
