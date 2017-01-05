using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLERPGL._report._chartOfAccount
{
    public partial class _report : UserControl
    {
        DataSet _getData;
        MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();
        SMLReport._report._objectListType __detailObject;
        //
        public _report()
        {
            InitializeComponent();
            // กำหนดว่าในรายงานมี Object กี่แบบ (ต้องเรียงให้เรียบร้อยด้วย จะได้ทำงานตามขั้นตอนไม่กระโดด)
            //
            this._view1._buttonCondition.Enabled = false;
            this._view1._buttonExample.Enabled = false;
            _view1._buttonBuildReport.Click += new EventHandler(_buttonBuildReport_Click);
            _view1._loadData += new SMLReport._report.LoadDataEventHandler(_view1__loadData);
            _view1._getObject += new SMLReport._report.GetObjectEventHandler(_view1__getObject);
            _view1._getDataObject += new SMLReport._report.GetDataObjectEventHandler(_view1__getDataObject);
            _view1._buttonClose.Click += new EventHandler(_buttonClose_Click);
            // แสดงผลทันที เพราะเป็นรายงานผังบัญชี
            _view1._buildReport(SMLReport._report._reportType.Standard);
        }

        void _buttonClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        bool _view1__loadData()
        {
            if (_getData == null)
            {
                try
                {
                    _getData = _myFrameWork._query(MyLib._myGlobal._databaseName, "select " + _g.d.gl_chart_of_account._code + "," + _g.d.gl_chart_of_account._name_1 + "," + 
                        _g.d.gl_chart_of_account._name_2 + "," + _g.d.gl_chart_of_account._account_level + "," + _g.d.gl_chart_of_account._account_type + "," + _g.d.gl_chart_of_account._status +
                        " from " + _g.d.gl_chart_of_account._table + " order by " + _g.d.gl_chart_of_account._code);
                }
                catch
                {
                    return false;
                }
            }
            return true;
        }

        void _buttonBuildReport_Click(object sender, EventArgs e)
        {
            _view1._reportProgressBar.Style = ProgressBarStyle.Marquee;
            _view1._buildReport(SMLReport._report._reportType.Standard);
            _view1._reportProgressBar.Style = ProgressBarStyle.Blocks ;
        }

        void _view1__getDataObject()
        {
            int __accountCodeColumn = _view1._findColumn(__detailObject, _g.d.gl_chart_of_account._code);
            int __accountNameColumn = _view1._findColumn(__detailObject, _g.d.gl_chart_of_account._name_1);
            int __accountName2Column = _view1._findColumn(__detailObject, _g.d.gl_chart_of_account._name_2);
            int __accountTypeColumn = _view1._findColumn(__detailObject, _g.d.gl_chart_of_account._account_type);
            int __accountLevel = _view1._findColumn(__detailObject, _g.d.gl_chart_of_account._account_level);
            int __accountStatus = _view1._findColumn(__detailObject, _g.d.gl_chart_of_account._status);
            SMLReport._report._columnListType __getColumn = (SMLReport._report._columnListType)__detailObject._columnList[0];
            for (int __row = 0; __row < _getData.Tables[0].Rows.Count; __row++)
            {
                SMLReport._report._objectListType __dataObject = _view1._addObject(_view1._objectDataList, SMLReport._report._objectType.Detail, true,0,true, SMLReport._report._columnBorder.None);
                _view1._createEmtryColumn(__detailObject, __dataObject);
                int __getAccountLevel = MyLib._myGlobal._intPhase(_getData.Tables[0].Rows[__row].ItemArray[3].ToString());
                int __getAccountStatus = MyLib._myGlobal._intPhase(_getData.Tables[0].Rows[__row].ItemArray[5].ToString());
                Font __newFont = (__getAccountStatus == 1) ? new Font(__getColumn._fontData, FontStyle.Bold) : null;
                _view1._addDataColumn(__detailObject, __dataObject, __accountCodeColumn, _getData.Tables[0].Rows[__row].ItemArray[0].ToString(), __newFont, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.String);
                _view1._addDataColumn(__detailObject, __dataObject, __accountNameColumn, _getData.Tables[0].Rows[__row].ItemArray[1].ToString(), __newFont, SMLReport._report._cellAlign.Default, __getAccountLevel * 8, SMLReport._report._cellType.String);
                _view1._addDataColumn(__detailObject, __dataObject, __accountName2Column, _getData.Tables[0].Rows[__row].ItemArray[2].ToString(), __newFont, SMLReport._report._cellAlign.Default, __getAccountLevel * 8, SMLReport._report._cellType.String);
                string __getAccountTypeName = "";
                int __getAccountType = 0;
                try
                {
                    __getAccountType = MyLib._myGlobal._intPhase(_getData.Tables[0].Rows[__row].ItemArray[4].ToString());
                }
                catch
                {
                }
                switch (__getAccountType)
                {
                    case 0: __getAccountTypeName = _g.d.gl_chart_of_account._account_type_asset; break;
                    case 1: __getAccountTypeName = _g.d.gl_chart_of_account._account_type_debt; break;
                    case 2: __getAccountTypeName = _g.d.gl_chart_of_account._account_type_capital; break;
                    case 3: __getAccountTypeName = _g.d.gl_chart_of_account._account_type_income; break;
                    case 4: __getAccountTypeName = _g.d.gl_chart_of_account._account_type_expense; break;

                }
                if (__getAccountTypeName.Length > 0)
                {
                    __getAccountTypeName = _g.d.gl_chart_of_account._table + "." + __getAccountTypeName;
                    __getAccountTypeName = ((MyLib._myResourceType)MyLib._myResource._findResource(__getAccountTypeName, __getAccountTypeName))._str;
                }
                _view1._addDataColumn(__detailObject, __dataObject, __accountTypeColumn, __getAccountTypeName, __newFont, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.String);
            }
        }

        bool _view1__getObject(System.Collections.ArrayList objectList, SMLReport._report._objectType type)
        {
            if (type == SMLReport._report._objectType.Header)
            {
                SMLReport._report._objectListType __headerObject = _view1._addObject(_view1._objectList, SMLReport._report._objectType.Header, true,0,true, SMLReport._report._columnBorder.None);
                int __newColumn = _view1._addColumn(__headerObject, 100);
                _view1.__excelFlieName = "รายงานรายละเอียดผังบัญชี";
                _view1._addCell(__headerObject, __newColumn, true, 0, -1, SMLReport._report._cellType.String, SMLReport._report._reportValueDefault._ltdName, SMLReport._report._cellAlign.Center, _view1._fontHeader1);
                _view1._addCell(__headerObject, __newColumn, true, 1, -1, SMLReport._report._cellType.String, "รายงานรายละเอียดผังบัญชี", SMLReport._report._cellAlign.Center, _view1._fontHeader2);
                _view1._addCell(__headerObject, __newColumn, true, 1, -1, SMLReport._report._cellType.String, "วันที่พิมพ์ : " + SMLReport._report._reportValueDefault._currentDateTime, SMLReport._report._cellAlign.Left, _view1._fontStandard);
                _view1._addCell(__headerObject, __newColumn, true, 1, -1, SMLReport._report._cellType.String, "หน้า : " + SMLReport._report._reportValueDefault._pageNumber + "/" + SMLReport._report._reportValueDefault._pageTotal, SMLReport._report._cellAlign.Right, _view1._fontStandard);
                return true;
            }
            else
                if (type == SMLReport._report._objectType.Detail)
                {
                    __detailObject = _view1._addObject(_view1._objectList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                    _view1._addColumn(__detailObject, 20, true, SMLReport._report._columnBorder.All, SMLReport._report._columnBorder.Left, _g.d.gl_chart_of_account._code, _g.d.gl_chart_of_account._table + "." + _g.d.gl_chart_of_account._code, _g.d.gl_chart_of_account._code, SMLReport._report._cellAlign.Left);
                    _view1._addColumn(__detailObject, 35, true, SMLReport._report._columnBorder.All, SMLReport._report._columnBorder.Left, _g.d.gl_chart_of_account._name_1, _g.d.gl_chart_of_account._table + "." + _g.d.gl_chart_of_account._name_1, _g.d.gl_chart_of_account._name_1, SMLReport._report._cellAlign.Left);
                    _view1._addColumn(__detailObject, 30, true, SMLReport._report._columnBorder.All, SMLReport._report._columnBorder.Left, _g.d.gl_chart_of_account._name_2, _g.d.gl_chart_of_account._table + "." + _g.d.gl_chart_of_account._name_2, _g.d.gl_chart_of_account._name_2, SMLReport._report._cellAlign.Left);
                    _view1._addColumn(__detailObject, 15, true, SMLReport._report._columnBorder.All, SMLReport._report._columnBorder.LeftRight, _g.d.gl_chart_of_account._account_type, _g.d.gl_chart_of_account._table + "." + _g.d.gl_chart_of_account._account_type, _g.d.gl_chart_of_account._account_type, SMLReport._report._cellAlign.Left);
                    return true;
                }
            return false;
        }
    }
}
