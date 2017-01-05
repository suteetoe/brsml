using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLERPCASHBANKReport
{
    public partial class _cb_creditenddate : UserControl
    {
        MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();
        SMLReport._report._objectListType __ojtReport;
        SMLReport._report._objectListType __ojtReportDetail;
        string __formatNumber = MyLib._myGlobal._getFormatNumber("m01");
        _cbCondition _myCondition = new _cbCondition();
        DataSet _ds;
        bool _openpop = false;
        private string _company = "";

        public _cb_creditenddate()
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
            this._myCondition.TitleName = ":: รายงานรายละเอียดบัตรเครดิต-ครบกำหนด ::";
            this._myCondition.PageName = "_cb_creditenddate";
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
                    _view1._addDataColumn(__ojtReport, __dataObject, 5, _dr[_row].ItemArray[5].ToString(), null, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.String);
                    _view1._addDataColumn(__ojtReport, __dataObject, 6, _dr[_row].ItemArray[6].ToString(), null, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.String);
                    _view1._addDataColumn(__ojtReport, __dataObject, 7, _dr[_row].ItemArray[7].ToString(), null, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.String);
                    _view1._addDataColumn(__ojtReport, __dataObject, 8, _dr[_row].ItemArray[8].ToString(), null, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.String);

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
                        _view1._addDataColumn(__ojtReport, ___dataTotalObject, 5, "", __newFont, SMLReport._report._cellAlign.Center, 0,SMLReport._report._cellType.String);
                        _view1._addDataColumn(__ojtReport, ___dataTotalObject, 6, "", __newFont, SMLReport._report._cellAlign.Center, 0,SMLReport._report._cellType.String);
                        _view1._addDataColumn(__ojtReport, ___dataTotalObject, 7, "", __newFont, SMLReport._report._cellAlign.Center, 0,SMLReport._report._cellType.String);
                        _view1._addDataColumn(__ojtReport, ___dataTotalObject, 8, "", __newFont, SMLReport._report._cellAlign.Center, 0,SMLReport._report._cellType.String);

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
            throw new NotImplementedException();
        }

        bool _view1__getObject(System.Collections.ArrayList objectList, SMLReport._report._objectType type)
        {
            throw new NotImplementedException();
        }

        void _buttonBuildReport_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
