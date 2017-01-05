using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SINGHAReport.AP
{
    public partial class _apReport : UserControl
    {
        _conditionForm _conditionScreen;
        DataSet _getAccount;
        _singhaReportEnum _mode;

        SMLReport._report._objectListType __accountObject;
        SMLReport._report._objectListType __jounalListObject;
        SMLReport._report._objectListType __ojtReport;

        public _apReport(_singhaReportEnum reportType, string screenName)
        {
            InitializeComponent();

            _mode = reportType;

            InitializeComponent();

            _view1._buttonExample.Enabled = false;
            _view1._buttonCondition.Click += new EventHandler(_buttonCondition_Click);
            _view1._buttonBuildReport.Click += new EventHandler(_buttonBuildReport_Click);

            switch (this._mode)
            {
                /*
                case _singhaReportEnum.GL_สมุดบัญชีแยกประเงินโอน:
                case _singhaReportEnum.GL_สมุดบัญชีแยกประเภทเงินสด:
                    _view1._loadData += new SMLReport._report.LoadDataEventHandler(_view1__loadData);
                    break;
                */
                default:
                    _view1._loadDataByThread += _view1__loadDataByThread;
                    //_view1._getTotalCurrentRow += _view1__getTotalCurrentRow;
                    //_view1._beforeReportDrawPaperArgs += _view1__beforeReportDrawPaperArgs;
                    break;

            }

            _view1._getObject += new SMLReport._report.GetObjectEventHandler(_view1__getObject);
            _view1._getDataObject += new SMLReport._report.GetDataObjectEventHandler(_view1__getDataObject);
            _view1._buttonClose.Click += new EventHandler(_buttonClose_Click);


            if (this._mode == _singhaReportEnum.GL_รายงานภาษีหักณที่จ่าย_3 ||
                this._mode == _singhaReportEnum.GL_รายงานภาษีหักณที่จ่าย_53)
            {
                // this._view1._pageSetupDialog.PageSettings.Landscape = true;
            }

            // condition
            _conditionScreen = new _conditionForm(reportType, screenName);
            _showCondition();
        }

        bool _view1__loadData()
        {
            return false;
        }

        void _view1__getDataObject()
        {

        }

        private void _view1__loadDataByThread()
        {

        }

        bool _view1__getObject(System.Collections.ArrayList objectList, SMLReport._report._objectType type)
        {
            return false;
        }

        void _showCondition()
        {
            _conditionScreen.ShowDialog();
            if (_conditionScreen.DialogResult == DialogResult.Yes)
            {
                // start process
                this._build();
            }
        }

        void _build()
        {
            this._getAccount = null;
            _view1._buildReport(SMLReport._report._reportType.Standard);

        }

        void _buttonBuildReport_Click(object sender, EventArgs e)
        {
            this._build();
        }

        void _buttonCondition_Click(object sender, EventArgs e)
        {
            _showCondition();
        }

        private void _buttonClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
