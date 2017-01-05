using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLERPCASHBANKReport
{
    public partial class _cb_revenue : UserControl
    {
        MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();
        SMLReport._report._objectListType __ojtReport;
        SMLReport._report._objectListType __ojtReportDetail;
        string __formatNumber = MyLib._myGlobal._getFormatNumber("m01");
        _cbCondition _myCondition = new _cbCondition();        
        DataSet _ds;
        bool _openpop = false;
        private string _company = "";

        public _cb_revenue()
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
            throw new NotImplementedException();
        }

        void _buttonCondition_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        void _view1__getDataObject()
        {
            throw new NotImplementedException();
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
