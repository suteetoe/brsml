using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Threading;

namespace SMLERPAR
{
    public partial class ar_point_recal : UserControl
    {
        int _currentProcessIndex = 0;
        int _totalProcess = 0;
        Thread _processThread;
        public ar_point_recal()
        {
            InitializeComponent();
            this.Disposed += ar_point_recal_Disposed;
        }

        void ar_point_recal_Disposed(object sender, EventArgs e)
        {
            try
            {
                if (_processThread != null && _processThread.IsAlive)
                    _processThread.Abort();
            }
            catch
            {

            }
        }

        private void _closeButton_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }


        private void _processButton_Click(object sender, EventArgs e)
        {
            _processThread = new Thread(new ThreadStart(_process));
            _processThread.IsBackground = true;
            _processThread.Start();
            //this._process();
            timer1.Start();
        }

        void _process()
        {

            StringBuilder __queryList = new StringBuilder(MyLib._myGlobal._xmlHeader + "<node>");
            __queryList.Append(MyLib._myUtil._convertTextToXmlForQuery("select doc_no from ic_trans where trans_flag = 44 and is_pos = 1 and last_status = 0 and doc_date between \'" + MyLib._myGlobal._convertDateToQuery(this._fromDocDate._dateTime) + "\' and \'" + MyLib._myGlobal._convertDateToQuery(this._toDocDate._dateTime) + "\' "));
            __queryList.Append("</node>");

            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            ArrayList __result = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __queryList.ToString());

            DataTable __invoiceTable = null;

            if (__result.Count > 0)
            {
                __invoiceTable = ((DataSet)__result[0]).Tables[0];
            }

            if (__invoiceTable != null && __invoiceTable.Rows.Count > 0)
            {
                _totalProcess = __invoiceTable.Rows.Count;
                SMLProcess._posProcess __process = new SMLProcess._posProcess();
                for (int __row = 0; __row < __invoiceTable.Rows.Count; __row++)
                {
                    // start process
                    //__process._pointReCal("");
                    __process._pointReCal(__invoiceTable.Rows[__row][_g.d.ic_trans._doc_no].ToString());
                    _currentProcessIndex = __row;
                }

            }


            MessageBox.Show("Success");

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this._resultTextbox.Text = string.Format("{0}/{1}", this._currentProcessIndex, this._totalProcess);

            if (this._currentProcessIndex == this._totalProcess - 1)
                timer1.Stop();
        }

        private void _stopButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (_processThread != null && _processThread.IsAlive)
                    _processThread.Abort();
            }
            catch
            {

            }

        }
    }
}
