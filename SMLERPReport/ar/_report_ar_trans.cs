using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace SMLERPReport.ar
{
    public partial class _report_ar_trans : UserControl
    {
        private MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();
        private SMLReport._report._objectListType __detailObject;
        private DataTable _dataTableAr;
        private DataTable _dataTableArTrans;
        private _condition_ar _form_condition = new _condition_ar();
        private DateTime _fromDate;
        private DateTime _toDate;
        private string _fromLot;
        private string _toLot;
        private string _fromAr;
        private string _toAr;
        private bool _newPageByAr;

        DataSet _dsTop;
        DataSet _dsDetial;

        public _report_ar_trans()
        {
            InitializeComponent();

            //this._view1._buttonCondition.Enabled = false;
            _dsTop = new DataSet();
            _dsDetial = new DataSet();
            this._view1._buttonExample.Enabled = false;
            this._view1._loadData += new SMLReport._report.LoadDataEventHandler(_view1__loadData);
            this._view1._getObject += new SMLReport._report.GetObjectEventHandler(_view1__getObject);
            this._view1._getDataObject += new SMLReport._report.GetDataObjectEventHandler(_view1__getDataObject);
            this._view1._buttonCondition.Click += new EventHandler(_buttonCondition_Click);
            this._view1._buttonBuildReport.Click += new EventHandler(_buttonBuildReport_Click);
            this._view1._buttonClose.Click += new EventHandler(_buttonClose_Click);

            this._form_condition._screenType = _screenConditionArType.ArTrans;
            this._showCondition();
        }

        protected void _process()
        {

            SMLProcess._aparProcess smlFrameWork = new SMLProcess._aparProcess();
            _dsDetial = smlFrameWork._queryStream("(cust_code >= 'AR-0') and (cust_code <='AR-10') ", " ", 1);
            int xxx = _dsTop.Tables[0].Rows.Count;
            string cccc = "";

        }


        bool _view1__loadData()
        {
            try
            {
                string _query = "select code ,name_1 from ar_customer where (cust_code >= 'AR-0') and (cust_code <='AR-20')";
                _dsTop = _myFrameWork._query(MyLib._myGlobal._databaseName, _query);

                ThreadStart theprogress = new ThreadStart(_process);
                Thread startprogress = new Thread(theprogress);
                startprogress.Priority = ThreadPriority.Highest;
                startprogress.IsBackground = false;
                startprogress.Start();
                startprogress.Abort();
            }
            catch
            {
            }

            /*
            if (this._dataTableAr == null || this._dataTableArTrans == null)
            {
                try
                {
                    Cursor.Current = Cursors.WaitCursor;

                    this._dataTableAr = new DataTable("ar");
                    this._dataTableAr.Columns.Add("ar_code");
                    this._dataTableAr.Columns.Add("ar_name");
                    DataRow __dataRow = this._dataTableAr.NewRow();
                    __dataRow[0] = "001";
                    __dataRow[1] = "xxx";
                    this._dataTableAr.Rows.Add(__dataRow);

                    this._dataTableArTrans = new DataTable("ar_trans");
                    this._dataTableArTrans.Columns.Add("doc_date");
                    this._dataTableArTrans.Columns.Add("doc_no");
                    this._dataTableArTrans.Columns.Add("tax_no");
                    this._dataTableArTrans.Columns.Add("ref_no");
                    this._dataTableArTrans.Columns.Add("trans_type");
                    this._dataTableArTrans.Columns.Add("debit");
                    this._dataTableArTrans.Columns.Add("credit");
                    this._dataTableArTrans.Columns.Add("remain");
                    this._dataTableArTrans.Columns.Add("profit_and_loss");
                    this._dataTableArTrans.Columns.Add("ar_code");
                    __dataRow = this._dataTableArTrans.NewRow();
                    __dataRow[0] = "01/01/2552";
                    __dataRow[1] = "DN0001";
                    __dataRow[2] = "TN0001";
                    __dataRow[3] = "RN0001";
                    __dataRow[4] = "xxxyyyzzz";
                    __dataRow[5] = "200000";
                    __dataRow[6] = "";
                    __dataRow[7] = "200000";
                    __dataRow[8] = "";
                    __dataRow[9] = "001";
                    this._dataTableArTrans.Rows.Add(__dataRow);
                }
                catch
                {
                    return false;
                }
                finally
                {
                    Cursor.Current = Cursors.Default;
                }
            }*/
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
                this._view1._addCell(__headerObject, __newColumn, true, 1, -1, SMLReport._report._cellType.String, "รายงานเคลื่อนไหวลูกหนี้", SMLReport._report._cellAlign.Left, this._view1._fontHeader2);
                this._view1._addCell(__headerObject, __newColumn, true, 2, -1, SMLReport._report._cellType.String, "วันที่พิมพ์ : " + SMLReport._report._reportValueDefault._currentDateTime, SMLReport._report._cellAlign.Left, this._view1._fontStandard);
                this._view1._addCell(__headerObject, __newColumn, true, 2, -1, SMLReport._report._cellType.String, "หน้า : " + SMLReport._report._reportValueDefault._pageNumber + "/" + SMLReport._report._reportValueDefault._pageTotal, SMLReport._report._cellAlign.Right, this._view1._fontStandard);
                return true;
            }
            //Write Detail Report
            else if (type == SMLReport._report._objectType.Detail)
            {
                //เส้นขอบบนรายละเอียด
                __detailObject = this._view1._addObject(this._view1._objectList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.Top);
                this._view1._addColumn(__detailObject, 10, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "รหัสลูกหนี้", "", SMLReport._report._cellAlign.Left);
                this._view1._addColumn(__detailObject, 10, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "ชื่อลูกหนี้", "", SMLReport._report._cellAlign.Left);
                //พิมพ์ชื่อฟิลด์
                __detailObject = this._view1._addObject(this._view1._objectList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.Bottom);
                this._view1._addColumn(__detailObject, 10, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "วันที่เอกสาร", "", SMLReport._report._cellAlign.Left);
                this._view1._addColumn(__detailObject, 10, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "เลขที่เอกสาร", "", SMLReport._report._cellAlign.Left);
                this._view1._addColumn(__detailObject, 10, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "ใบกำกับภาษี", "", SMLReport._report._cellAlign.Left);
                this._view1._addColumn(__detailObject, 10, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "เลขที่อ้างอิง", "", SMLReport._report._cellAlign.Left);
                this._view1._addColumn(__detailObject, 20, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "ประเภทเอกสาร", "", SMLReport._report._cellAlign.Left);
                this._view1._addColumn(__detailObject, 10, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "ยอดเดบิต", "", SMLReport._report._cellAlign.Right);
                this._view1._addColumn(__detailObject, 10, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "ยอดเครดิต", "", SMLReport._report._cellAlign.Right);
                this._view1._addColumn(__detailObject, 10, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "ยอดคงเหลือ", "", SMLReport._report._cellAlign.Right);
                //เอาออก
                //this._view1._addColumn(__detailObject, 10, true, SMLReport._report._columnBorder.None, SMLReport._report._columnBorder.None, "", "กำไร(ขาดทุน)", "", SMLReport._report._cellAlign.Right);
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
            //this._view1._reportProgressBar.Maximum = this._dataTableArTrans.Rows.Count;

            if (this._dsTop == null) return;
            DataRow[] __dataRowsAr = this._dsTop.Tables[0].Select();
            for (int __rowAr = 0; __rowAr < __dataRowsAr.Length; __rowAr++)
            {
                __dataObject = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                this._view1._createEmtryColumn(__detailObject, __dataObject);
                this._view1._addDataColumn(__detailObject, __dataObject, 0, __dataRowsAr[__rowAr].ItemArray[0].ToString(), __newFont, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.String);
                this._view1._addDataColumn(__detailObject, __dataObject, 1, __dataRowsAr[__rowAr].ItemArray[1].ToString(), __newFont, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.String);

                DataRow[] __dataRowsArTrans = this._dsDetial.Tables[0].Select(" cust_code='" + __dataRowsAr[__rowAr].ItemArray[0].ToString() + "'");
                for (int __rowTrans = 0; __rowTrans < __dataRowsArTrans.Length; __rowTrans++)
                {
                    __dataObject = this._view1._addObject(this._view1._objectDataList, SMLReport._report._objectType.Detail, true, 0, true, SMLReport._report._columnBorder.None);
                    this._view1._createEmtryColumn(__detailObject, __dataObject);
                    this._view1._addDataColumn(__detailObject, __dataObject, 0, __dataRowsArTrans[__rowTrans].ItemArray[0].ToString(), __newFont, SMLReport._report._cellAlign.Default, 5,SMLReport._report._cellType.String);
                    this._view1._addDataColumn(__detailObject, __dataObject, 1, __dataRowsArTrans[__rowTrans].ItemArray[1].ToString(), __newFont, SMLReport._report._cellAlign.Default, 5,SMLReport._report._cellType.String);
                    this._view1._addDataColumn(__detailObject, __dataObject, 2, __dataRowsArTrans[__rowTrans].ItemArray[2].ToString(), __newFont, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.String);
                    this._view1._addDataColumn(__detailObject, __dataObject, 3, __dataRowsArTrans[__rowTrans].ItemArray[3].ToString(), __newFont, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.String);
                    //this._view1._addDataColumn(__detailObject, __dataObject, 4, __dataRowsArTrans[__rowTrans].ItemArray[4].ToString(), __newFont, SMLReport._report._cellAlign.Default, 0,SMLReport._report._cellType.Text);
                    //this._view1._addDataColumn(__detailObject, __dataObject, 5, __dataRowsArTrans[__rowTrans].ItemArray[5].ToString(), __newFont, SMLReport._report._cellAlign.Right, 0,SMLReport._report._cellType.Double);
                    //this._view1._addDataColumn(__detailObject, __dataObject, 6, __dataRowsArTrans[__rowTrans].ItemArray[6].ToString(), __newFont, SMLReport._report._cellAlign.Right, 0,SMLReport._report._cellType.Double);
                    //this._view1._addDataColumn(__detailObject, __dataObject, 7, __dataRowsArTrans[__rowTrans].ItemArray[7].ToString(), __newFont, SMLReport._report._cellAlign.Right, 0,SMLReport._report._cellType.Double);
                    //this._view1._addDataColumn(__detailObject, __dataObject, 8, __dataRowsArTrans[__rowTrans].ItemArray[8].ToString(), __newFont, SMLReport._report._cellAlign.Right, 0,SMLReport._report._cellType.Double);
                    //this._view1._reportProgressBar.Value++;
                }
            }
        }

        void _buttonCondition_Click(object sender, EventArgs e)
        {
            try
            {
                this._showCondition();
                SMLProcess._aparProcess smlFrameWork = new SMLProcess._aparProcess();
                DataSet _dsTop = smlFrameWork._queryStream("", "", 1);
                int xxx = _dsTop.Tables[0].Rows.Count;
                string cccc = "";
            }
            catch
            {
            }

            //__smlFrameWork.(MyLib._myGlobal._databaseName, __query, "Journal");
        }

        void _buttonBuildReport_Click(object sender, EventArgs e)
        {
            if (this._form_condition._screen_condition_ar1._checkEmtryField().Length != 0)
            {
                MessageBox.Show(MyLib._myGlobal._resource("ยังไม่ได้กำหนดเงื่อนไข"), MyLib._myGlobal._resource("เตือน"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                return;
            }
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
            this._form_condition.ShowDialog();
            if (this._form_condition._process)
            {
                string __date = this._form_condition._screen_condition_ar1._getDataStr("จากวันที่");
                string[] __arrayDate = __date.Split('/');
                this._fromDate = DateTime.Parse(__arrayDate[2] + "/" + __arrayDate[1] + "/" + __arrayDate[0], MyLib._myGlobal._cultureInfo());

                __date = this._form_condition._screen_condition_ar1._getDataStr("ถึงวันที่");
                __arrayDate = __date.Split('/');
                this._toDate = DateTime.Parse(__arrayDate[2] + "/" + __arrayDate[1] + "/" + __arrayDate[0], MyLib._myGlobal._cultureInfo());

                this._fromLot = this._form_condition._screen_condition_ar1._getDataStr("จากงวดที่");

                this._toLot = this._form_condition._screen_condition_ar1._getDataStr("ถึงงวดที่");

                this._fromAr = this._form_condition._screen_condition_ar1._getDataStr("จากลูกหนี้");

                this._toAr = this._form_condition._screen_condition_ar1._getDataStr("ถึงลูกหนี้");

                this._newPageByAr = this._form_condition._screen_condition_ar1._getDataStr("ขึ้นหน้าใหม่ตามลูกค้า") == "1" ? true : false;

                this._view1._buildReport(SMLReport._report._reportType.Standard);
            }
        }
    }
}
