using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLERPICReport.ReportPO
{
    public partial class _report_po_request : UserControl
    {
        private MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();
        private SMLReport._report._objectListType __detailObject;
        private DataTable _dataTable;
        private DataTable _dataTableDetail;
        public _report_po_request()
        {
            InitializeComponent();
            this._view1._buttonExample.Enabled = false;
            this._view1._loadDataByThread += new SMLReport._report.LoadDataByThreadEventHandle(_view1__loadDataByThread);
            this._view1._getObject += new SMLReport._report.GetObjectEventHandler(_view1__getObject);
            this._view1._getDataObject += new SMLReport._report.GetDataObjectEventHandler(_view1__getDataObject);
            this._view1._buttonCondition.Click += new EventHandler(_buttonCondition_Click);
            this._view1._buttonBuildReport.Click += new EventHandler(_buttonBuildReport_Click);
            this._view1._buttonClose.Click += new EventHandler(_buttonClose_Click);
        }

        void _view1__loadDataByThread()
        {
            if (this._dataTable == null || this._dataTableDetail == null)
            {
                StringBuilder __where = new StringBuilder();
                StringBuilder __whereDetail = new StringBuilder();
                try
                {
                    
                }
                catch (Exception ex)
                {
                }
            }
        }

        bool _view1__getObject(System.Collections.ArrayList objectList, SMLReport._report._objectType type)
        {
            //Write Header Report
            if (type == SMLReport._report._objectType.Header)
            {
                SMLReport._report._objectListType __headerObject = this._view1._addObject(this._view1._objectList, SMLReport._report._objectType.Header, true, 0, true, SMLReport._report._columnBorder.None);
                int __newColumn = this._view1._addColumn(__headerObject, 100);
                this._view1._addCell(__headerObject, __newColumn, true, 0, -1, SMLReport._report._cellType.String, SMLReport._report._reportValueDefault._ltdName, SMLReport._report._cellAlign.Center, this._view1._fontHeader1);
                this._view1._addCell(__headerObject, __newColumn, true, 1, -1, SMLReport._report._cellType.String, "รายงานใบเสนอซื้อสินค้า", SMLReport._report._cellAlign.Left, this._view1._fontHeader2);
                this._view1._addCell(__headerObject, __newColumn, true, 2, -1, SMLReport._report._cellType.String, "วันที่พิมพ์ : " + SMLReport._report._reportValueDefault._currentDateTime, SMLReport._report._cellAlign.Left, this._view1._fontStandard);
                this._view1._addCell(__headerObject, __newColumn, true, 2, -1, SMLReport._report._cellType.String, "หน้า : " + SMLReport._report._reportValueDefault._pageNumber + "/" + SMLReport._report._reportValueDefault._pageTotal, SMLReport._report._cellAlign.Right, this._view1._fontStandard);
                return true;
            }
            //Write Detail Report
            else if (type == SMLReport._report._objectType.Detail)
            {
                //เส้นขอบบนรายละเอียด
                __detailObject = this._view1._addObject(this._view1._objectList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.Top);
                this._view1._addColumn(__detailObject, 15, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "เลขที่เอกสาร", "", SMLReport._report._cellAlign.Left);
                this._view1._addColumn(__detailObject, 15, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "วันที่เอกสาร", "", SMLReport._report._cellAlign.Left);
                this._view1._addColumn(__detailObject, 20, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "แผนก", "", SMLReport._report._cellAlign.Left);
                this._view1._addColumn(__detailObject, 20, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "ผู้ขออนุมัติ", "", SMLReport._report._cellAlign.Left);
                this._view1._addColumn(__detailObject, 30, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "หมายเหตุ", "", SMLReport._report._cellAlign.Left);                
                //พิมพ์ชื่อฟิลด์
                __detailObject = this._view1._addObject(this._view1._objectList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.Bottom);
                this._view1._addColumn(__detailObject, 15, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "รหัสสินค้า", "", SMLReport._report._cellAlign.Left);
                this._view1._addColumn(__detailObject, 25, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "ชื่อสินค้า", "", SMLReport._report._cellAlign.Left);
                this._view1._addColumn(__detailObject, 15, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "หน่วยนับ", "", SMLReport._report._cellAlign.Left);
                this._view1._addColumn(__detailObject, 25, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "คำอธิบาย", "", SMLReport._report._cellAlign.Left);
                this._view1._addColumn(__detailObject, 10, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "จำนวนที่ขออนุมัติ", "", SMLReport._report._cellAlign.Left);
                this._view1._addColumn(__detailObject, 10, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "จำนวนคงเหลือที่ยังไม่ได้อนุมัติ", "", SMLReport._report._cellAlign.Left);                
                return true;
            }
            return false;
        }

        void _view1__getDataObject()
        {
            throw new NotImplementedException();
        }

        void _buttonCondition_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        void _buttonBuildReport_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        void _buttonClose_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
