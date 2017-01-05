using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SINGHAReport.SO
{
    public partial class _soReport : UserControl
    {
        _conditionForm _conditionScreen;
        _singhaReportEnum _mode;

        public _soReport(_singhaReportEnum reportType, string screenName)
        {
            this._mode = reportType;

            InitializeComponent();

            _view1._buttonExample.Enabled = false;
            _view1._buttonCondition.Click += new EventHandler(_buttonCondition_Click);
            _view1._buttonBuildReport.Click += new EventHandler(_buttonBuildReport_Click);
            _view1._loadData += new SMLReport._report.LoadDataEventHandler(_view1__loadData);
            _view1._getObject += new SMLReport._report.GetObjectEventHandler(_view1__getObject);
            _view1._getDataObject += new SMLReport._report.GetDataObjectEventHandler(_view1__getDataObject);
            _view1._buttonClose.Click += new EventHandler(_buttonClose_Click);


            // condition
            _conditionScreen = new _conditionForm(reportType, screenName);
            _showCondition();
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
            _view1._buildReport(SMLReport._report._reportType.Standard);

        }

        void _view1__getDataObject()
        {
        }

        bool _view1__loadData()
        {
            return false;
        }

        bool _view1__getObject(System.Collections.ArrayList objectList, SMLReport._report._objectType type)
        {

            if (type == SMLReport._report._objectType.Header)
            {
                return true;
            }
            else if (type == SMLReport._report._objectType.Detail)
            {

                return true;
            }
            return false;
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
