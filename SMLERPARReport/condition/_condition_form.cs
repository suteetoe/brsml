using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SMLERPARAPReport.condition
{
    public partial class _condition_form : MyLib._myForm
    {
        public string _where = "";
        public DataTable _grid_where;
        public bool _process = false;
        public _condition_screen _condition_screen1;

        public _condition_form(SMLERPARAPInfo._apArConditionEnum mode, string __page, string screenName)
        {
            InitializeComponent();
            this._condition_screen1 = new _condition_screen(mode);
            this._condition_screen1.Dock = DockStyle.Fill;
            this._grouperConditon.Controls.Add(this._condition_screen1);
            this._grouperConditon.AutoSize = true;
            this.Load += new EventHandler(_condition_ar_1_Load);
            this._bt_process.Click += new EventHandler(_bt_process_Click);
            this._bt_exit.Click += new EventHandler(_bt_exit_Click);
            this.Text = screenName;
            if (SMLERPARAPInfo._apAr._apArCheck(mode) == SMLERPARAPInfo._apArEnum.ลูกหนี้)
            {
                this._condition_grid1._setFromToColumn(mode, _g.d.resource_report._from_ar, _g.d.resource_report._to_ar);
                this._condition_grid1._setSearchScreen(_g.g._search_screen_ar);
            }
            if (SMLERPARAPInfo._apAr._apArCheck(mode) == SMLERPARAPInfo._apArEnum.เจ้าหนี้)
            {
                this._condition_grid1._setFromToColumn(mode, _g.d.resource_report._from_ap, _g.d.resource_report._to_ap);
                this._condition_grid1._setSearchScreen(_g.g._search_screen_ap);
            }
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
}