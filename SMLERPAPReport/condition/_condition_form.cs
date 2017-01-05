using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SMLERPAPReport.condition
{
    public partial class _condition_form : MyLib._myForm
    {
        public string _where = "";
        public DataTable _grid_where;
        public bool _process = false;

        public _condition_form(string __page, string __form_name)
        {
            InitializeComponent();
            this.Load += new EventHandler(_condition_ar_1_Load);
            this._bt_process.Click += new EventHandler(_bt_process_Click);
            this._bt_exit.Click += new EventHandler(_bt_exit_Click);
            this.Text = __form_name;
            this._condition_screen1._maxColumn = 1;
            this._condition_screen1._init(__page);
            this._condition_grid1._setFromToColumn(_g.d.resource_report._from_payable, _g.d.resource_report._to_payable);
            this._condition_grid1._setSearchScreen(_g.g._search_screen_ap);
        }

        void _condition_ar_1_Load(object sender, EventArgs e)
        {
            this._condition_screen1.AutoSize = true;
            this._condition_screen1._focusFirst();
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

        private void _working()
        {
            this._condition_screen1._focusFirst();
            this._bt_process.Focus();
            this._process = true;
            this.Close();
        }

        private void _exit()
        {
            this._process = false;
            this.Close();
        }
    }

    public enum _enum_screen_report_ap
    {
        เจ้าหนี้_รายละเอียดเจ้าหนี้,
        early_debt_setup,
        early_debt_setup_cancel,
        early_debt_increase,
        early_debt_increase_cancel,
        early_debt_decrease,
        early_debt_decrease_cancel,
        other_debt_setup,
        other_debt_setup_cancel,
        other_debt_increase,
        other_debt_increase_cancel,
        other_debt_decrease,
        other_debt_decrease_cancel,
        billing,
        prepare_payment,
        payment,
        debt_cut_off,
        billing_cancel,
        prepare_payment_cancel,
        payment_cancel,
        debt_cut_off_cancel,
        absolute_movement,
        absolute_status,
        รายงานอายุเจ้าหนี้,
        absolute_not_movement,
        payment_daily

    }
}