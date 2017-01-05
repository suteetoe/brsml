using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLERPCASHBANKReport
{
    public partial class _cb_deposit : UserControl
    {
        MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();
        SMLReport._report._objectListType __ojtReport;
        SMLReport._report._objectListType __ojtReportDetail;
        string __formatNumber = MyLib._myGlobal._getFormatNumber("m01");
        _cbCondition _myCondition = new _cbCondition();
        DataSet _ds;
        bool _openpop = false;
        private string _company = "";

        public _cb_deposit()
        {
            InitializeComponent();
            _view1._buttonBuildReport.Click += new EventHandler(_buttonBuildReport_Click);
            _view1._getObject += new SMLReport._report.GetObjectEventHandler(_view1__getObject);
            _view1._loadData += new SMLReport._report.LoadDataEventHandler(_view1__loadData);
            _view1._getDataObject += new SMLReport._report.GetDataObjectEventHandler(_view1__getDataObject);
            _view1._buttonCondition.Click += new EventHandler(_buttonCondition_Click);
            _view1._buttonClose.Click += new EventHandler(_buttonClose_Click);
        }

        void _buttonClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        void _buttonCondition_Click(object sender, EventArgs e)
        {
            this._myCondition.TitleName = ":: รายงานการฝากเงินสด ::";
            this._myCondition.PageName = "_cb_deposit";
            this._myCondition.ShowDialog();
            _openpop = true;
            _ds = null;
        }

        void _view1__getDataObject()
        {
            try
            {
                SMLReport._report._columnListType __getColumn = (SMLReport._report._columnListType)__ojtReport._columnList[0];
                string __formatNumber = MyLib._myGlobal._getFormatNumber("m01");
                DataRow[] _dr = _ds.Tables[0].Select("");
                double __total_amount = 0;
                for (int _row = 0; _row < _dr.Length; _row++)
                {
                    SMLReport._report._objectListType __dataObject = _view1._addObject(_view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                    Font __newFont = new Font(__getColumn._fontData, FontStyle.Bold);
                    _view1._createEmtryColumn(__ojtReport, __dataObject);
                    _view1._addDataColumn(__ojtReport, __dataObject, 0, _dr[_row].ItemArray[0].ToString(), null, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.String);
                    _view1._addDataColumn(__ojtReport, __dataObject, 1, _dr[_row].ItemArray[1].ToString(), null, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.String);
                    _view1._addDataColumn(__ojtReport, __dataObject, 2, _dr[_row].ItemArray[2].ToString(), null, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.String);
                    _view1._addDataColumn(__ojtReport, __dataObject, 3, _dr[_row].ItemArray[3].ToString(), null, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.String);
                    _view1._addDataColumn(__ojtReport, __dataObject, 4, _dr[_row].ItemArray[4].ToString(), null, SMLReport._report._cellAlign.Center, 0,SMLReport._report._cellType.String);

                    if (_row == (_dr.Length - 1))
                    {
                        _row++;
                        SMLReport._report._objectListType ___dataTotalObject = _view1._addObject(_view1._objectDataList, SMLReport._report._objectType.Detail, true, 8, true, SMLReport._report._columnBorder.All);
                        _view1._createEmtryColumn(__ojtReport, ___dataTotalObject);
                        _view1._addDataColumn(__ojtReport, ___dataTotalObject, 0, "", __newFont, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.String);
                        _view1._addDataColumn(__ojtReport, ___dataTotalObject, 1, "", __newFont, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.String);
                        _view1._addDataColumn(__ojtReport, ___dataTotalObject, 2, "", __newFont, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.String);
                        _view1._addDataColumn(__ojtReport, ___dataTotalObject, 3, "", __newFont, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.String);
                        _view1._addDataColumn(__ojtReport, ___dataTotalObject, 4, "", __newFont, SMLReport._report._cellAlign.Center, 0,SMLReport._report._cellType.String);
                        
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(":: ERR :: \n" + ex.Message);
            }
        }

        bool _view1__loadData()
        {
            try
            {
                String _getConditonResult = "";
                if (_openpop) _getConditonResult = (this._myCondition.Result.Equals("")) ? "" : " and " + this._myCondition.Result;
                string __query = "select doc_date,doc_no,pass_book_code,remark,"
                                            + "((select coalesce(sum(sum_amount),0) from cb_trans_detail where trans_type=1 and trans_flag=5 )"
                                            + "-"
                                            + "(select coalesce(sum(sum_amount),0) from cb_trans_detail where trans_type=1 and trans_flag=6 ))"
                                            + " as sum_received"
                                            + " from cb_trans_detail " +_getConditonResult;
                string _query = "select doc_date ,doc_no ,pass_book_code,remark,sum_received from (" + __query + ") as temp1  LIMIT 10";
                _ds = _myFrameWork._query(MyLib._myGlobal._databaseName, _query);

            }
            catch
            {
                this.Cursor = Cursors.Default;
                _view1._reportProgressBar.Style = ProgressBarStyle.Blocks;
                return false;
            }
            _view1._reportProgressBar.Style = ProgressBarStyle.Blocks;
            this.Cursor = Cursors.Default;
            return true;
        }

        public void _getCompanyValue()
        {
            DataSet __getLastCode = _myFrameWork._query(MyLib._myGlobal._databaseName, "select company_name_1  from  erp_company_profile order by company_name_1 asc");
            if (__getLastCode.Tables[0].Rows.Count > 0)
            {
                this._company = __getLastCode.Tables[0].Rows[0].ItemArray[0].ToString();
            }
        }

        bool _view1__getObject(System.Collections.ArrayList objectList, SMLReport._report._objectType type)
        {
            if (type == SMLReport._report._objectType.Header)
            {
                _getCompanyValue();
                SMLReport._report._objectListType __headerObject = _view1._addObject(_view1._objectList, SMLReport._report._objectType.Header, true, 0, true, SMLReport._report._columnBorder.None);
                int __newColumn = _view1._addColumn(__headerObject, 100);
                _view1._addCell(__headerObject, __newColumn, true, 0, -1, SMLReport._report._cellType.String, this._company, SMLReport._report._cellAlign.Center, _view1._fontHeader1);
                _view1._addCell(__headerObject, __newColumn, true, 1, -1, SMLReport._report._cellType.String, "รายงานการฝากเงินสด", SMLReport._report._cellAlign.Center, _view1._fontHeader2);
                _view1._addCell(__headerObject, __newColumn, true, 2, -1, SMLReport._report._cellType.String, "วันที่พิมพ์ : " + SMLReport._report._reportValueDefault._currentDateTime, SMLReport._report._cellAlign.Left, _view1._fontStandard);
                _view1._addCell(__headerObject, __newColumn, true, 2, -1, SMLReport._report._cellType.String, "หน้า : " + SMLReport._report._reportValueDefault._pageNumber + "/" + SMLReport._report._reportValueDefault._pageTotal, SMLReport._report._cellAlign.Right, _view1._fontStandard);
                return true;
            }
            else
                if (type == SMLReport._report._objectType.Detail)
                {
                    __ojtReport = _view1._addObject(_view1._objectList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.Top);
                    _view1._addColumn(__ojtReport, 10, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "วันที่", "", SMLReport._report._cellAlign.Default);
                    _view1._addColumn(__ojtReport, 10, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "เลขที่เอกสาร", "", SMLReport._report._cellAlign.Default);
                    _view1._addColumn(__ojtReport, 20, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "สมุดเงินฝาก", "", SMLReport._report._cellAlign.Default);
                    _view1._addColumn(__ojtReport, 10, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "หมายเหตุ/รายละเอียด", "", SMLReport._report._cellAlign.Default);
                    _view1._addColumn(__ojtReport, 10, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "จำนวนเงิน", "", SMLReport._report._cellAlign.Default);                    
                    __ojtReportDetail = _view1._addObject(_view1._objectList, SMLReport._report._objectType.Detail, true, 8, true, SMLReport._report._columnBorder.Bottom);


                    return true;
                }
            return false;
        }

        void _buttonBuildReport_Click(object sender, EventArgs e)
        {
            _view1._buildReport(SMLReport._report._reportType.Standard);
        }
    }
}
