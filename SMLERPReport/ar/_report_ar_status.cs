using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLERPReport.ar
{
    public partial class _report_ar_status : UserControl
    {
        private MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();
        private SMLReport._report._objectListType __detailObject;
        private DataTable _dataTable;
        private _condition_ar _form_condition = new _condition_ar();
        private DateTime _fromDate;
        private DateTime _toDate;
        private string _lot;
        private string _fromAr;
        private string _toAr;
        private bool _notShowAmountZero;

        public _report_ar_status()
        {
            InitializeComponent();

            //this._view1._buttonCondition.Enabled = false;
            this._view1._buttonExample.Enabled = false;
            this._view1._loadDataByThread += new SMLReport._report.LoadDataByThreadEventHandle(_view1__loadDataByThread);
            this._view1._getObject += new SMLReport._report.GetObjectEventHandler(_view1__getObject);
            this._view1._getDataObject += new SMLReport._report.GetDataObjectEventHandler(_view1__getDataObject);
            this._view1._buttonCondition.Click += new EventHandler(_buttonCondition_Click);
            this._view1._buttonBuildReport.Click += new EventHandler(_buttonBuildReport_Click);
            this._view1._buttonClose.Click += new EventHandler(_buttonClose_Click);

            //this._view1._buildReport(SMLReport._report._reportType.Standard);

            this._form_condition._screenType = _screenConditionArType.ArStatus;
            this._showCondition();
        }

        void _view1__loadDataByThread()
        {
            // this._dataTable == null ไม่ต้อง load ซ้ำ
            if (this._dataTable == null)
            {
                SMLERPGlobal._smlFrameWork __smlFrameWork = new SMLERPGlobal._smlFrameWork();
                try
                {
                    string __dateBegin = MyLib._myGlobal._convertDateToQuery(this._fromDate);
                    string __dateEnd = MyLib._myGlobal._convertDateToQuery(this._toDate);
                    StringBuilder __where = new StringBuilder();
                    __where.Append(" where " + _g.d.ar_customer._table + "." + _g.d.ar_customer._code + " between \'" + this._fromAr + "\' and \'" + this._toAr + "\'");
                    string __orderBy = "order by " + _g.d.ar_customer._table + "." + _g.d.ar_customer._code;
                    this._dataTable = __smlFrameWork._processArStatus(MyLib._myGlobal._databaseName, __dateBegin, __dateEnd, __where.ToString(), __orderBy, 1).Tables[0];
                }
                catch
                {
                    this._view1._loadDataByThreadSuccess = false;
                }
            }
            this._view1._loadDataByThreadSuccess = true;
        }

        bool _view1__getObject(System.Collections.ArrayList objectList, SMLReport._report._objectType type)
        {
            //Write Header Report
            if (type == SMLReport._report._objectType.Header)
            {
                SMLReport._report._objectListType __headerObject = this._view1._addObject(this._view1._objectList, SMLReport._report._objectType.Header, true, 0, true, SMLReport._report._columnBorder.None);
                int __newColumn = this._view1._addColumn(__headerObject, 100);
                this._view1._addCell(__headerObject, __newColumn, true, 0, -1, SMLReport._report._cellType.String, SMLReport._report._reportValueDefault._ltdName, SMLReport._report._cellAlign.Center, this._view1._fontHeader1);
                this._view1._addCell(__headerObject, __newColumn, true, 1, -1, SMLReport._report._cellType.String, "รายงานสถานะลูกหนี้", SMLReport._report._cellAlign.Left, this._view1._fontHeader2);
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
                this._view1._addColumn(__detailObject, 10, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "รหัสลูกหนี้", "", SMLReport._report._cellAlign.Left);
                this._view1._addColumn(__detailObject, 30, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "ชื่อลูกหนี้", "", SMLReport._report._cellAlign.Left);
                float __calcWidth = 60f / 6f; // คำนวณความกว้างเฉลี่ยนแต่ละช่อง เป็น %
                this._view1._addColumn(__detailObject, __calcWidth, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "ยอดยกมา", "", SMLReport._report._cellAlign.Right);
                this._view1._addColumn(__detailObject, __calcWidth, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "ยอดตั้งหนี้", "", SMLReport._report._cellAlign.Right);
                this._view1._addColumn(__detailObject, __calcWidth, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "ยอดลดหนี้", "", SMLReport._report._cellAlign.Right);
                this._view1._addColumn(__detailObject, __calcWidth, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "ยอดเพิ่มหนี้", "", SMLReport._report._cellAlign.Right);
                this._view1._addColumn(__detailObject, __calcWidth, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "ยอดตัดชำระหนี้", "", SMLReport._report._cellAlign.Right);
                this._view1._addColumn(__detailObject, __calcWidth, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "ยอดยกไป", "", SMLReport._report._cellAlign.Right);
                //this._view1._addColumn(__detailObject, __calcWidth, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "กำไรขาดทุนประจำงวด", "", SMLReport._report._cellAlign.Right);
                return true;
            }
            return false;
        }

        void _view1__getDataObject()
        {
            try
            {
                Font __newFont = null;
                SMLReport._report._columnListType __getColumn = (SMLReport._report._columnListType)__detailObject._columnList[0];
                SMLReport._report._objectListType __dataObject = null;

                string _formatNumber = MyLib._myGlobal._getFormatNumber("m02");
                if (this._dataTable == null) return;
                DataRow[] __dataRows = this._dataTable.Select();
                for (int __row = 0; __row < __dataRows.Length; __row++)
                {
                    if (this._notShowAmountZero)
                    {
                        if (__dataRows[__row].ItemArray[7].ToString().Equals("0")) continue;
                    }
                    __dataObject = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                    this._view1._createEmtryColumn(__detailObject, __dataObject);
                    this._view1._addDataColumn(__detailObject, __dataObject, 0, __dataRows[__row].ItemArray[0].ToString(), __newFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                    this._view1._addDataColumn(__detailObject, __dataObject, 1, __dataRows[__row].ItemArray[1].ToString(), __newFont, SMLReport._report._cellAlign.Default, 0, SMLReport._report._cellType.String);
                    this._view1._addDataColumn(__detailObject, __dataObject, 2, MyLib._myUtil._moneyFormat(MyLib._myGlobal._decimalPhase(__dataRows[__row].ItemArray[2].ToString()), _formatNumber), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
                    this._view1._addDataColumn(__detailObject, __dataObject, 3, MyLib._myUtil._moneyFormat(MyLib._myGlobal._decimalPhase(__dataRows[__row].ItemArray[4].ToString()), _formatNumber), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
                    this._view1._addDataColumn(__detailObject, __dataObject, 4, MyLib._myUtil._moneyFormat(MyLib._myGlobal._decimalPhase(__dataRows[__row].ItemArray[7].ToString()), _formatNumber), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
                    this._view1._addDataColumn(__detailObject, __dataObject, 5, MyLib._myUtil._moneyFormat(MyLib._myGlobal._decimalPhase(__dataRows[__row].ItemArray[5].ToString()), _formatNumber), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
                    this._view1._addDataColumn(__detailObject, __dataObject, 6, MyLib._myUtil._moneyFormat(MyLib._myGlobal._decimalPhase(__dataRows[__row].ItemArray[6].ToString()), _formatNumber), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
                    this._view1._addDataColumn(__detailObject, __dataObject, 7, MyLib._myUtil._moneyFormat(MyLib._myGlobal._decimalPhase(__dataRows[__row].ItemArray[8].ToString()), _formatNumber), __newFont, SMLReport._report._cellAlign.Right, 0, SMLReport._report._cellType.Number);
                }
            }
            catch (Exception ex)
            {
            }
        }

        void _buttonCondition_Click(object sender, EventArgs e)
        {
            this._showCondition();
        }

        void _buttonBuildReport_Click(object sender, EventArgs e)
        {
            if (this._form_condition._screen_condition_ar1._checkEmtryField().Length != 0)
            {
                MessageBox.Show(MyLib._myGlobal._resource("ยังไม่ได้กำหนดเงื่อนไข"), MyLib._myGlobal._resource("เตือน"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }
            this._dataTable = null; // จะได้ load data ใหม่
            this._view1._reportProgressBar.Style = ProgressBarStyle.Marquee;
            this._view1._buildReport(SMLReport._report._reportType.Standard);
            this._view1._reportProgressBar.Style = ProgressBarStyle.Blocks;
        }

        void _buttonClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void _showCondition()
        {
            this._dataTable = null; // จะได้ load data ใหม่
            this._form_condition._process = false;
            this._form_condition.ShowDialog();
            if (this._form_condition._process)
            {
                string __date = this._form_condition._screen_condition_ar1._getDataStr("จากวันที่");
                string[] __arrayDate = __date.Split('/');
                this._fromDate = DateTime.Parse(__arrayDate[2] + "/" + __arrayDate[1] + "/" + __arrayDate[0], MyLib._myGlobal._cultureInfo());

                __date = this._form_condition._screen_condition_ar1._getDataStr("ถึงวันที่");
                __arrayDate = __date.Split('/');
                this._toDate = DateTime.Parse(__arrayDate[2] + "/" + __arrayDate[1] + "/" + __arrayDate[0], MyLib._myGlobal._cultureInfo());

                this._lot = this._form_condition._screen_condition_ar1._getDataStr("ประจำงวดที่");

                this._fromAr = this._form_condition._screen_condition_ar1._getDataStr("จากลูกหนี้");

                this._toAr = this._form_condition._screen_condition_ar1._getDataStr("ถึงลูกหนี้");

                this._notShowAmountZero = this._form_condition._screen_condition_ar1._getDataStr("ไม่แสดงรายการที่มียอดยกไปเท่ากับศูนย์") == "1" ? true : false;

                this._view1._buildReport(SMLReport._report._reportType.Standard);
            }
        }
    }
}