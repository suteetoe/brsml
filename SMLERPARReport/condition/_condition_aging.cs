using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SMLERPARAPReport.condition
{
    public partial class _condition_aging : MyLib._myForm
    {
        public _condition_screen _screenTop;
        string __title;
        public string _title
        {
            get { return __title; }
            set { __title = value; }
        }

        private bool __process;
        public bool _process
        {
            get { return __process; }
            set { __process = value; }
        }

        string __process_date;

        public string _process_date
        {
            get { return __process_date; }
            set { __process_date = value; }
        }

        private int __order_by;

        public int _order_by
        {
            get { return __order_by; }
            set { __order_by = value; }
        }

        public _condition_aging(SMLERPARAPInfo._apArConditionEnum mode)
        {
            InitializeComponent();
            this._screenTop = new _condition_screen(mode);
            this._screenTop.Dock = DockStyle.Fill;
            this._grouperCondition.Controls.Add(this._screenTop);
            this._bt_process.Click += new EventHandler(_bt_process_Click);
            this._bt_exit.Click += new EventHandler(_bt_exit_Click);
            //this._condition_grid1._setFromToColumn(_g.d.resource_report._from_payable, _g.d.resource_report._to_payable);
            //this._condition_grid1._setSearchScreen(_g.g._search_screen_ar);
            //this._arConditionScreen1._setDataDate(_g.d.resource_report_column._process_date, MyLib._myGlobal._workingDate);
        }

        void __getControl_4_Enter(object sender, EventArgs e)
        {
           
        }

        void ___getRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                MyLib._myRadioButton __e = (MyLib._myRadioButton)sender;
                if (__e.Checked)
                {
                    if (__e.Name.Equals(_g.d.ap_ar_resource._by_ar_code))
                    {
                        this.__order_by = 1;
                    }
                    else
                    {
                        this.__order_by = 2;
                    }
                }
            }
            catch
            {
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (Keys.F11 == keyData)
            {
                this._procress_ok();
                return true;
            }
            if (Keys.Escape == keyData)
            {
                this._close();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        void _bt_exit_Click(object sender, EventArgs e)
        {
            this._close();
        }

        void _bt_process_Click(object sender, EventArgs e)
        {
            this._procress_ok();
        }

        private void _procress_ok()
        {
            try
            {
                this.__process_date = this._screenTop._getDataStr(_g.d.ap_ar_resource._date_end);
                this.__process = true;
                this.Close();
            }
            catch
            {
                this.__process = false;
                this.Close();
            }
        }

        private void _close()
        {
            this.__process = false;
            this.Close();
        }

        private void _condition_aging_Load(object sender, EventArgs e)
        {
            this._screenTop._setDataDate(_g.d.ap_ar_resource._date_end, MyLib._myGlobal._workingDate);
            this.__process = false;
            this.Text = __title;
        }
    }
}