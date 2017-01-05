using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SMLERPReport.ar
{
    public partial class _condition_ar_new : MyLib._myForm
    {
        public string _where = "";
        public DataTable _grid_where;
        public bool _process = false;

        public _condition_ar_new(string __page)
        {
            InitializeComponent();
            this.Load += new EventHandler(_condition_ar_1_Load);
            this._bt_process.Click += new EventHandler(_bt_process_Click);
            this._bt_exit.Click += new EventHandler(_bt_exit_Click);
            this._form_name(__page);
            this._screen_condition_ar_new1._maxColumn = 1;
            this._screen_condition_ar_new1._init(__page);
            this._grid_condition_ar_new1._setFromToColumn(_g.d.resource_report._from_ar, _g.d.resource_report._to_ar);
        }

        void _condition_ar_1_Load(object sender, EventArgs e)
        {
            this._screen_condition_ar_new1.AutoSize = true;
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (Keys.F11 == keyData)
            {
                this._working();
                return true;
            }
            if (Keys.Escape == keyData)
            {
                this._exit();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        void _bt_process_Click(object sender, EventArgs e)
        {
            this._working();
        }

        void _bt_exit_Click(object sender, EventArgs e)
        {
            this._exit();
        }

        private void _form_name(string __page)
        {
            if (__page.Equals(_enum_screen_report_ar.ar_detail.ToString())) this.Text = "รายงานรายละเอียดลูกหนี้";
        }

        private void _working()
        {
            //this._where = this._screen_condition_ar_new1._createQueryForDatabase()[2].ToString();
            //this._grid_where = this._grid_condition_ar_new1._getCondition();
            this._process = true;
            this.Close();
        }

        private void _exit()
        {
            this._process = false;
            this.Close();
        }
    }

    public enum _enum_screen_report_ar
    {
        ar_detail,
        ar_billing,
        ar_receipt_temp,
        ar_receipt,
        ar_debt_cut_off
    }
}