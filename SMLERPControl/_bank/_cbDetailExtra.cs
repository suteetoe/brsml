using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLERPControl._bank
{
    public partial class _cbDetailExtra : Form
    {
        private string _chqNumber;

        public _cbDetailExtra(string chqNumber)
        {
            InitializeComponent();
            if (MyLib._myGlobal._isDesignMode == false)
            {
                this.toolStrip1.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            }
            this._chqNumber = chqNumber;
        }

        private void _cbDetailExtra_Load(object sender, EventArgs e)
        {
            _start();
            ////
            //this._glDetailExtraAllocateGridData._accountCode = this._accountCode;
            //this._glDetailExtraDepartmentGridData._accountCode = this._accountCode;
            //this._glDetailExtraJobGridData._accountCode = this._accountCode;
            //this._glDetailExtraProjectGridData._accountCode = this._accountCode;
            //this._glDetailExtraSideGridData._accountCode = this._accountCode;
        }

        public void _start()
        {
            this._cbDetailExtraTopScreen.Focus();
            this._cbDetailExtraTopScreen._isChange = false;
        }        

        private void _buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }

    public partial class _cbDetailExtraTopScreen : MyLib._myScreen
    {
        public _cbDetailExtraTopScreen()
        {
            this._maxColumn = 2;
            this.SuspendLayout();
            this._addTextBox(0, 0, 1, 0, _g.d.cb_chq_list._chq_number, 1, 25, 0, true, false, true);
            this._addDateBox(0, 1, 1, 0, _g.d.cb_chq_list._chq_due_date, 1, true, true);
            this._addNumberBox(1, 0, 1, 0, _g.d.cb_chq_list._amount, 1, 2, true);
            this._addTextBox(1, 1, 1, 0, _g.d.cb_chq_list._currency_code, 1, 10, 1, true, false, true);
            this._addTextBox(2, 0, 2, 0, _g.d.cb_chq_list._chq_description, 2, 255, 0, true, false, true);
            this._addTextBox(4, 0, 1, 0, _g.d.cb_chq_list._ap_ar_code, 1, 25, 1, true, false, true);
            this._addTextBox(4, 1, 1, 0, _g.d.cb_chq_list._ap_ar_name, 1, 255, 0, false, false, true, false);
            this._addTextBox(5, 0, 1, 0, _g.d.cb_chq_list._book_code, 1, 10, 1, true, false, true);
            this._addTextBox(5, 1, 1, 0, _g.d.cb_chq_list._pass_book_code, 1, 10, 1, true, false, true);
            this._addTextBox(6, 0, 1, 0, _g.d.cb_chq_list._bank_code, 1, 10, 0, true, false, true, true);
            this._addTextBox(6, 1, 1, 0, _g.d.cb_chq_list._bank_branch, 1, 100, 0, true, false, true, true);
            this._addDateBox(7, 0, 1, 0, _g.d.cb_chq_list._chq_get_date, 1, true, true);//"วันที่เก็บเช็ค" 
            this._addTextBox(7, 1, 1, 0, _g.d.cb_chq_list._person_code, 1, 25, 1, true, false, true);//ผู้เก็บเช็ค                                       
            this._addTextBox(8, 0, 1, 0, _g.d.cb_chq_list._side_code, 1, 10, 0, true, false, true);
            this._addTextBox(8, 1, 1, 0, _g.d.cb_chq_list._department_code, 1, 10, 0, true, false, true);
            this._addTextBox(9, 0, 2, 0, _g.d.cb_chq_list._remark, 2, 255, 0, true, false, true);
            this._getControl(_g.d.cb_chq_list._chq_number).Enabled = false;
            this._getControl(_g.d.cb_chq_list._amount).Enabled = false;
            this._getControl(_g.d.cb_chq_list._side_code).Enabled = false;
            this._getControl(_g.d.cb_chq_list._department_code).Enabled = false;
            this.ResumeLayout();
        }
    }

}
