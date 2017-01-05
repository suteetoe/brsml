using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SMLERPCASHBANKReport._condition
{
    public partial class _condition_form : MyLib._myForm
    {
        public string _where = "";
        public DataTable _grid_where;
        public bool _process = false;        

        public _condition_form(string __page)
        {
            InitializeComponent();
            this.Load += new EventHandler(_condition_form_Load);
            this._bt_process.Click += new EventHandler(_bt_process_Click);
            this._bt_exit.Click += new EventHandler(_bt_exit_Click);
            this._form_name(__page);
            this._condition_screen1._maxColumn = 1;
            this._condition_screen1._init(__page);
            this._condition_grid1.Visible = false;
            this._grouper2.Visible = false;
            ////ตั้งค่า where ให้ popup
            //if (__page.Equals(_enum_screen_report_cb._chq_receive.ToString()))
            //{
            //    this._condition_screen1.__where_query = " chq_type = 1 ";
            //}
            //else if (__page.Equals(_enum_screen_report_cb._chq_payment.ToString()))
            //{
            //    this._condition_screen1.__where_query = " chq_type = 2 ";
            //}
            //else if (__page.Equals(_enum_screen_report_cb._credit_receive.ToString()))
            //{
            //    this._condition_screen1.__where_query = " trans_type = 1 ";
            //}
            //else if (__page.Equals(_enum_screen_report_cb._credit_payment.ToString()))
            //{
            //    this._condition_screen1.__where_query = " trans_type = 2 ";
            //} 
            
        }

        void _condition_form_Load(object sender, EventArgs e)
        {
            this._condition_screen1.AutoSize = true;
            this._condition_screen1._focusFirst();            
        }

        //////////public _condition_form(string __page, string __form_name)
        //////////{
        //////////    InitializeComponent();
        //////////    this.Load += new EventHandler(_condition_form_Load);
        //////////    this._bt_process.Click += new EventHandler(_bt_process_Click);
        //////////    this._bt_exit.Click += new EventHandler(_bt_exit_Click);
        //////////    this.Text = __form_name;
        //////////    this._condition_screen1._maxColumn = 1;
        //////////    this._condition_screen1._init(__page);
        //////////    //this._condition_grid1._setFromToColumn(_g.d.resource_report._from_ar, _g.d.resource_report._to_ar);
        //////////    //this._condition_grid1._setSearchScreen(_g.g._search_screen_ar);
            
        //////////}

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
            if (__page.Equals(_enum_screen_report_cb._cb_chqinstatus.ToString()))
            {
                this.Text = "รายงานสถานะเช็ครับ";
            }
            else if (__page.Equals(_enum_screen_report_cb._cb_chqinreceive.ToString()))
            {
                this.Text = "รายงานเช็ครับ";
            }
            else if (__page.Equals(_enum_screen_report_cb._cb_chqindeposit.ToString()))
            {
                this.Text = "รายงานนำฝากเช็ครับ";
            }
            else if (__page.Equals(_enum_screen_report_cb._cb_chqinpass.ToString()))
            {
                this.Text = "รายงานเช็ครับผ่าน";
            }
            else if (__page.Equals(_enum_screen_report_cb._cb_chqinpass.ToString()))
            {
                this.Text = "รายงานเช็ครับผ่าน";
            }
            else if (__page.Equals(_enum_screen_report_cb._cb_chqinreturn.ToString()))
            {
                this.Text = "รายงานเช็ครับคืน";
            }
            else if (__page.Equals(_enum_screen_report_cb._cb_chqinchange.ToString()))
            {
                this.Text = "รายงานเปลี่ยนเช็คนำฝาก";
            }
            else if (__page.Equals(_enum_screen_report_cb._cb_chqinrenew.ToString()))
            {
                this.Text = "รายงานนำเช็คเข้าใหม่";
            }
            else if (__page.Equals(_enum_screen_report_cb._cb_chqoutstatus.ToString()))
            {
                this.Text = "รายงานสถานะเช็คจ่าย";
            }
            else if (__page.Equals(_enum_screen_report_cb._cb_chqoutpayment.ToString()))
            {
                this.Text = "รายงานเช็คจ่าย";
            }
            else if (__page.Equals(_enum_screen_report_cb._cb_chqoutpass.ToString()))
            {
                this.Text = "รายงานเช็คจ่ายผ่าน";
            }
            else if (__page.Equals(_enum_screen_report_cb._cb_chqoutreturn.ToString()))
            {
                this.Text = "รายงานเช็คจ่ายคืน";
            }
            else if (__page.Equals(_enum_screen_report_cb._cb_chqoutchange.ToString()))
            {
                this.Text = "รายงานเปลี่ยนเช็คจ่าย";
            }
            else if (__page.Equals(_enum_screen_report_cb._cb_chqincancel.ToString()))
            {
                this.Text = "รายงานยกเลิกเช็ครับ";
            }
            else if (__page.Equals(_enum_screen_report_cb._cb_chqindepositcancel.ToString()))
            {
                this.Text = "รายงานยกเลิกนำฝากเช็ครับ";
            }
            else if (__page.Equals(_enum_screen_report_cb._cb_chqinpasscancel.ToString()))
            {
                this.Text = "รายงานยกเลิกเช็ครับผ่าน";
            }
            else if (__page.Equals(_enum_screen_report_cb._cb_chqinreturncancel.ToString()))
            {
                this.Text = "รายงานยกเลิกเช็ครับคืน";
            }
            else if (__page.Equals(_enum_screen_report_cb._cb_chqinchangecancel.ToString()))
            {
                this.Text = "รายงานยกเลิกเปลี่ยนเช็คนำฝาก";
            }
            else if (__page.Equals(_enum_screen_report_cb._cb_chqinrenewcancel.ToString()))
            {
                this.Text = "รายงานยกเลิกนำเช็คเข้าใหม่";
            }
            else if (__page.Equals(_enum_screen_report_cb._cb_chqoutcancel.ToString()))
            {
                this.Text = "รายงานยกเลิกเช็คจ่าย";
            }
            else if (__page.Equals(_enum_screen_report_cb._cb_chqoutpasscancel.ToString()))
            {
                this.Text = "รายงานยกเลิกเช็คจ่ายผ่าน";
            }
            else if (__page.Equals(_enum_screen_report_cb._cb_chqoutreturncancel.ToString()))
            {
                this.Text = "รายงานยกเลิกเช็คจ่ายคืน";
            }
            else if (__page.Equals(_enum_screen_report_cb._cb_chqoutchangecancel.ToString()))
            {
                this.Text = "รายงานยกเลิกเปลี่ยนเช็คจ่าย";
            }
            else if (__page.Equals(_enum_screen_report_cb._cb_creditcard.ToString()))
            {
                this.Text = "รายงานรายการบัตรเครดิต";
            }
            else if (__page.Equals(_enum_screen_report_cb._cb_creditcardcancel.ToString()))
            {
                this.Text = "รายงานยกเลิกรายการบัตรเครดิต";
            }
            else if (__page.Equals(_enum_screen_report_cb._cb_craditcardgetmoney.ToString()))
            {
                this.Text = "รายงานขึ้นเงินบัตรเครดิต";
            }
            else if (__page.Equals(_enum_screen_report_cb._cb_craditcardgetmoneycancel.ToString()))
            {
                this.Text = "รายงานยกเลิกขึ้นเงินบัตรเครดิต";
            }
            else if (__page.Equals(_enum_screen_report_cb._cb_subcashopen.ToString()))
            {
                this.Text = "รายงานกำหนดวงเงินสดย่อย";
            }
            else if (__page.Equals(_enum_screen_report_cb._cb_subcashreceive.ToString()))
            {
                this.Text = "รายงานรับเงินสดย่อย";
            }
            else if (__page.Equals(_enum_screen_report_cb._cb_subcashreceivecancel.ToString()))
            {
                this.Text = "รายงานยกเลิกรับเงินสดย่อย";
            }
            else if (__page.Equals(_enum_screen_report_cb._cb_subcashwithdraw.ToString()))
            {
                this.Text = "รายงานขอเบิกเงินสดย่อย";
            }
            else if (__page.Equals(_enum_screen_report_cb._cb_subcashwithdrawcancel.ToString()))
            {
                this.Text = "รายงานยกเลิกขอเบิกเงินสดย่อย";
            }
            else if (__page.Equals(_enum_screen_report_cb._cb_subcashreturn.ToString()))
            {
                this.Text = "รายงานรับคือเงินสดย่อย";
            }
            else if (__page.Equals(_enum_screen_report_cb._cb_subcashreturncancel.ToString()))
            {
                this.Text = "รายงานยกเลิกรับคือเงินสดย่อย";
            }
            else if (__page.Equals(_enum_screen_report_cb._cb_subcashmovements.ToString()))
            {
                this.Text = "รายงานเคลื่อนไหวเงินสดย่อย";
            }

        }

        private void _working()
        {
            this._condition_screen1._focusFirst();
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
