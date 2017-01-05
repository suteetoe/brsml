using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLERPReport.ap
{
    public partial class _report_ap_summary : UserControl
    {
        SMLReport._generate _report;
        String _screenName = "";
        DataTable _dataTableDetail;
        DataTable _dataTableRoot;
        string _levelNameRoot = "root";
        string _levelNameDetail = "detail";
        _apEnum _apType;
        string _transFlag;
        _condition_type_1 _condition;
        Boolean _displayDetail = false;

        /// <summary>
        /// ยกเลิกก่อน 
        /// </summary>
        /// <param name="apType"></param>
        /// <param name="screenName"></param>
        public _report_ap_summary(_apEnum apType, string screenName)
        {
            InitializeComponent();

            this._apType = apType;
            //this._transFlag = "44"; // (SMLERPReportTool._global._reportType(this._mode) == SMLERPReportTool._reportTypeEnum.ขาย_ลูกหนี้) ? "44" : "12";
            this._screenName = screenName;
            this._report = new SMLReport._generate(screenName, false);
            this._report._query += new SMLReport._generate.QueryEventHandler(_report__query);
            this._report._init += new SMLReport._generate.InitEventHandler(_report__init);
            this._report._showCondition += new SMLReport._generate.ShowConditionEventHandler(_report__showCondition);
            this._report._dataRowSelect += new SMLReport._generate.DataRowSelectEventHandler(_report__dataRowSelect);
            this._report._renderValue += new SMLReport._generate.RenderValueEventHandler(_report__renderValue);
            this._report.Disposed += new EventHandler(_report_Disposed);
            ////
            this._report.Dock = DockStyle.Fill;
            this.Controls.Add(this._report);
            //this._report__showCondition(screenName);
            this._showCondition();
        }

        void _report__query()
        {
            StringBuilder __query = new StringBuilder();
            StringBuilder __queryDetail = new StringBuilder();

            // ประกอบ query

            switch (this._apType)
            {
                case _apEnum.Invoice_Arrears_by_Date:
                    {
                        SMLERPARAPInfo._process __process = new SMLERPARAPInfo._process();
                        string __queryar = __process._createQuery(SMLERPARAPInfo._apArConditionEnum.ลูกหนี้_ยอดคงเหลือตามเอกสาร, 1, "", "", "");

                        __query.Append(__queryar);
                    }
                    break;
            }

            // start query 

            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            string __queryDebug = __query.ToString();
            string __queryDetailDebug = __queryDetail.ToString();
            this._dataTableDetail = __myFrameWork._queryStream(MyLib._myGlobal._databaseName, __queryDebug).Tables[0];
            this._dataTableRoot = (_displayDetail) ? __myFrameWork._queryStream(MyLib._myGlobal._databaseName, __queryDetailDebug).Tables[0] : null;
        }

        void _report__init()
        {
            switch (this._apType)
            {
                case _apEnum.Invoice_Arrears_by_Date:
                    this._displayDetail = false;
                    break;
            }
        }

        void _report__showCondition(string screenName)
        {
            throw new NotImplementedException();
        }

        DataRow[] _report__dataRowSelect(SMLReport._generateLevelClass levelParent, SMLReport._generateLevelClass level, DataRow source)
        {
            throw new NotImplementedException();
        }

        void _report__renderValue(SMLReport._generateLevelClass sender, int columnNumber, SMLReport._generateColumnStyle isTotal)
        {
            
        }

        void _report_Disposed(object sender, EventArgs e)
        {
            this.Dispose();
        }

        public void _showCondition()
        {
            if (this._condition == null)
            {
                this._condition = new _condition_type_1(this._apType.ToString());
                this._condition.Text = _screenName;

                switch (this._apType)
                {
                    case _apEnum.Detail_Payable:
                        this._condition._whereControl._tableName = _g.d.ap_supplier._table;
                        this._condition._whereControl._addFieldComboBox(this._ap_detail_column());
                        break;
                    case _apEnum.Documents_Early_Year:
                        this._condition._whereControl._tableName = _g.d.ap_ar_trans._table;
                        this._condition._whereControl._addFieldComboBox(this._ap_year_balance_column());
                        break;
                }
                //
                this._condition.Size = new Size(500, 500);
            }

            this._condition.ShowDialog();
            if (this._condition.__check_submit)
            {
                // old
                //this._data_condition = this._con_1.__where;
                //this.__check_submit = this._con_1.__check_submit;
                //this._ap_config();
                //this.__data_ap = this._con_1.__grid_where;
                //_view1._buildReport(SMLReport._report._reportType.Standard);

                // new
                // gen condition
                this._report._build();
            }
        }

        private string[] _ap_detail_column()
        {
            string[] __result = { _g.d.ap_supplier._table+"."+_g.d.ap_supplier._code, 
                                  _g.d.ap_supplier._table+"."+_g.d.ap_supplier._name_1, 
                                  _g.d.ap_supplier._table+"."+_g.d.ap_supplier._address, 
                                  _g.d.ap_supplier._table+"."+_g.d.ap_supplier._telephone, 
                                  _g.d.ap_supplier._table+"."+_g.d.ap_supplier._fax, 
                                  _g.d.ap_supplier._table +"."+_g.d.ap_supplier._debt_balance+"*"}; // มี * เพื่อบังคับให้ชิดขวา -- jead เอาดอกจันออกทำไม ผมจะให้มันชิดขวา
            return __result;
        }

        private string[] _ap_year_balance_column()
        {
            //string[] __column = { "วัน", "เลขที่เอกสาร", "รหัสเจ้าหนี้", "ชื่อเจ้าหนี้", "หมายเหตุ",  "ยอดสุทธิ", "ยอดคงเหลือ" };
            string[] __result = { _g.d.ap_ar_trans._table+"."+_g.d.ap_ar_trans._doc_date,
                                    _g.d.ap_ar_trans._table+"."+_g.d.ap_ar_trans._doc_no,
                                    _g.d.ap_ar_trans._table+"."+_g.d.ap_ar_trans._cust_code,
                                    _g.d.ap_ar_trans._table+"."+_g.d.ap_ar_trans._cust_name,
                                    _g.d.ap_ar_trans._table+"."+_g.d.ap_ar_trans._remark,
                                    _g.d.ap_ar_trans._table+"."+_g.d.ap_ar_trans._total_net_value+"*",
                                    _g.d.ap_ar_trans._table+"."+_g.d.ap_ar_trans._total_debt_balance+"*"};

            return __result;
        }
    }
}
