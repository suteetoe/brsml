using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace SINGHAReport
{
    public partial class _singhaDataSyncControl : UserControl
    {
        int _mode = 0;
        public _singhaDataSyncControl(int mode)
        {
            InitializeComponent();
            this._mode = mode;

            this._myScreen1._table_name = _g.d.resource_report._table;
            this._myScreen1._maxColumn = 2;
            this._myScreen1._addDateBox(0, 0, 1, 0, _g.d.resource_report._from_date, 1, true, true);
            this._myScreen1._addDateBox(0, 1, 1, 0, _g.d.resource_report._to_date, 1, true, true);

            DateTime __today = DateTime.Now;

            this._myScreen1._setDataDate(_g.d.resource_report._from_date, __today);
            this._myScreen1._setDataDate(_g.d.resource_report._to_date, __today);

        }

        private void _syncButton_Click(object sender, EventArgs e)
        {
            // start sync data
            DateTime __getFromDate = this._myScreen1._getDataDate(_g.d.resource_report._from_date);
            DateTime __getToDate = this._myScreen1._getDataDate(_g.d.resource_report._to_date);

            DateTime __fromDateTimeCompare = new DateTime(__getFromDate.Year, __getFromDate.Month, __getFromDate.Day);
            DateTime __toDateTimeCompare = new DateTime(__getToDate.Year, __getToDate.Month, __getToDate.Day);

            _processDay = ((int)(__toDateTimeCompare - __fromDateTimeCompare).TotalDays) + 1;

            __currentProcess = 0;
            __maxProcess = _processDay * 6;
            this.progressBar1.Value = 0;
            this.progressBar1.Maximum = __maxProcess;
            timer1.Start();
            Thread __thred = new Thread(new ThreadStart(_process));
            __thred.Start();
        }


        int _processDay = 0;
        int __currentProcess = 0;
        int __maxProcess = 0;

        void _process()
        {
            __maxProcess = _processDay * 6;

            for (int __process = 0; __process < _processDay; __process++)
            {
                for (int __step = 0; __step <= 6; __step++)
                {
                    __currentProcess++;
                    Thread.Sleep(1500);
                }
            }

            MessageBox.Show("Success");
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.progressBar1.Value = __currentProcess;

            if (__maxProcess == __currentProcess)
            {
                timer1.Stop();
            }
        }

        private void _closeButton_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
