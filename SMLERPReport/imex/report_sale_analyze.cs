using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLERPReport.imex
{
    public partial class report_sale_analyze : UserControl
    {
        SMLReport._generate _report;
        string _screen_name = "";

        public report_sale_analyze(string screenName)
        {
            InitializeComponent();
            this._screen_name = screenName;

            this._report = new SMLReport._generate(screenName, true);
            this._report._query += _report__query;
            this._report._init += _report__init;
            this._report._showCondition += _report__showCondition;
            this._report._dataRowSelect += _report__dataRowSelect;
            this._report._renderValue += _report__renderValue;
            this._report.Disposed += _report_Disposed;

        }

        void _report_Disposed(object sender, EventArgs e)
        {
            this.Dispose();
        }

        void _report__renderValue(SMLReport._generateLevelClass sender, int columnNumber, SMLReport._generateColumnStyle isTotal)
        {
            throw new NotImplementedException();
        }

        DataRow[] _report__dataRowSelect(SMLReport._generateLevelClass levelParent, SMLReport._generateLevelClass level, DataRow source)
        {
            throw new NotImplementedException();
        }

        void _report__showCondition(string screenName)
        {
            throw new NotImplementedException();
        }

        void _report__init()
        {
            throw new NotImplementedException();
        }

        void _report__query()
        {
            
        }
    }
}
