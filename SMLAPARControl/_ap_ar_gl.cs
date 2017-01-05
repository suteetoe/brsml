using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace SMLERPAPARControl
{
    public partial class _ap_ar_gl : Form
    {
        private StringBuilder __resource;

        public StringBuilder _resource
        {
            get { return __resource; }
            set { __resource = value; }
        }

        private string _save_data_name;

        public string save_data_name
        {
            get { return _save_data_name; }
            set { _save_data_name = value; }
        }

        public _ap_ar_gl()
        {
            InitializeComponent();
            if (MyLib._myGlobal._isDesignMode == false)
            {
                this._myToolbar.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            }
            _setControlEnable(_g.d.gl_journal._doc_date, false);
            //_setControlEnable(_g.d.gl_journal._doc_no, false);
            __resource = new StringBuilder();
        }

        void _setControlEnable(string controlName, bool _bool)
        {
            try
            {
                MyLib._myTextBox _getControlName = (MyLib._myTextBox)this._screenTop._getControl(controlName);
                _getControlName.Enabled = _bool;
                _getControlName.TextBox.BackColor = ((_bool == true) ? System.Drawing.Color.White : System.Drawing.Color.WhiteSmoke);
            }
            catch (Exception ex)
            {
            }
        }

        public void _setText(string _fild_name, string _date_value)
        {
            this._screenTop._setDataStr(_fild_name, _date_value);
        }

        public void _setDate(string _fild_name, string _date_value)
        {
            this._screenTop._setDataDate(_fild_name, MyLib._myGlobal._convertDate(_date_value));
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            Keys keyCode = (Keys)(int)keyData & Keys.KeyCode;
            if (keyData == Keys.Escape)
            {
                this.Close();
                return true;
            }
            if (keyData == Keys.F12)
            {
                if (this._save_data())
                {
                    this.Close();
                }
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void _saveButton_Click(object sender, EventArgs e)
        {
            if (this._save_data())
            {
                this.Close();
            }
        }

        private bool _save_data()
        {
            // กำหนด Focus ใหม่ ไม่งั้นข้อมูลจะไม่สมบูรณ์
            try
            {
                this._glDetail1._glDetailGrid.Focus();
                decimal __getTotal = 0;
                try
                {
                    __getTotal = MyLib._myGlobal._decimalPhase(_glDetail1._total_amount.textBox.Text);
                }
                catch
                {
                }
                if (__getTotal != 0)
                {
                    MessageBox.Show(MyLib._myGlobal._resource("ยอด Debit, Credit ไม่เท่ากัน\nไม่สามารถบันทึกรายการได้"), MyLib._myGlobal._resource("เตือน"), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    string __getEmtry = this._screenTop._checkEmtryField();
                    if (__getEmtry.Length > 0)
                    {
                        MyLib._myGlobal._displayWarning(2, __getEmtry);
                    }
                    else
                    {
                        ArrayList __getData = this._screenTop._createQueryForDatabase();
                        __resource = new StringBuilder();
                        Control __getControl = this._screenTop._getControl(_g.d.gl_journal._doc_date);
                        DateTime __getDate = ((MyLib._myDateBox)__getControl)._dateTime;
                        int __periodNumber = _g.g._accountPeriodFind(__getDate);
                        // grid
                        string __fieldList = _g.d.gl_journal._doc_date + "," + _g.d.gl_journal._book_code + "," + _g.d.gl_journal._doc_no + "," + _g.d.gl_journal._period_number + "," + _g.d.gl_journal._journal_type + "," + _g.d.gl_journal._ap_ar_code + "," + _g.d.gl_journal._ap_ar_code + ",";
                        string __dataList = this._screenTop._getDataStrQuery(_g.d.gl_journal._doc_date) + "," + this._screenTop._getDataStrQuery(_g.d.gl_journal._book_code) + "," + this._screenTop._getDataStrQuery(_g.d.gl_journal._doc_no) + "," + __periodNumber.ToString() + "," + this._screenTop._getDataStr(_g.d.gl_journal._journal_type) + "," + this._screenTop._getDataStr(_g.d.gl_journal._ap_ar_code) + "," + this._screenTop._getDataStr(_g.d.gl_journal._ap_ar_originate_from) + ",";
                        //
                        int __getColumnNumberDebit = this._glDetail1._glDetailGrid._findColumnByName(_g.d.gl_journal_detail._debit);
                        int __getColumnNumberCredit = this._glDetail1._glDetailGrid._findColumnByName(_g.d.gl_journal_detail._credit);
                        //
                        string __extData = ((MyLib._myGrid._columnType)this._glDetail1._glDetailGrid._columnList[__getColumnNumberDebit])._total.ToString() + "," +
                            ((MyLib._myGrid._columnType)this._glDetail1._glDetailGrid._columnList[__getColumnNumberCredit])._total.ToString() + "," + __periodNumber.ToString();
                        string __extField = _g.d.gl_journal._debit + "," + _g.d.gl_journal._credit + "," + _g.d.gl_journal._period_number;
                        __resource.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _g.d.gl_journal._table + " (" + __getData[0].ToString() + "," + __extField + ") values (" + __getData[1].ToString() + "," + __extData + ")"));
                        // TransSub
                        __resource.Append(this._glDetail1._glDetailGrid._createQueryForInsert(_g.d.gl_journal_detail._table, __fieldList, __dataList));

                        // Extra
                        for (int __row = 0; __row < this._glDetail1._glDetailGrid._rowData.Count; __row++)
                        {
                            string __accountCode = (string)this._glDetail1._glDetailGrid._cellGet(__row, 0);
                            SMLERPGLControl._glDetailExtraObject __getExtraData = (SMLERPGLControl._glDetailExtraObject)this._glDetail1._glDetailGrid._cellGet(__row, _g.d.gl_journal_detail._dimension);
                            // Side List
                            for (int __loop = 0; __loop < __getExtraData._sideList.Count; __loop++)
                            {
                                int __line_number = __row + 1;
                                int __line_number_detail = __loop + 1;
                                SMLERPGLControl._glDetailExtraDetailClass __getDetailData = (SMLERPGLControl._glDetailExtraDetailClass)__getExtraData._sideList[__loop];
                                if (__getDetailData._code.Length > 0)
                                {
                                    __resource.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _g.d.gl_journal_side_list._table + " (" + __fieldList + _g.d.gl_journal_side_list._side_code + "," + _g.d.gl_journal_side_list._side_name + "," +
                                        _g.d.gl_journal_side_list._allocate_persent + "," + _g.d.gl_journal_side_list._allocate_amount + "," + _g.d.gl_journal_side_list._line_number + "," + _g.d.gl_journal_side_list._line_number_detail + "," +
                                        _g.d.gl_journal_side_list._account_code + ") values (" + __dataList + "\'" + __getDetailData._code + "\',\'" + __getDetailData._name + "\'," + __getDetailData._persent.ToString() + "," + __getDetailData._amount.ToString() + "," +
                                        __line_number.ToString() + "," + __line_number_detail.ToString() + ",\'" + __accountCode + "\')"));
                                }
                            }
                            // Department List
                            for (int __loop = 0; __loop < __getExtraData._departmentList.Count; __loop++)
                            {
                                int __line_number = __row + 1;
                                int __line_number_detail = __loop + 1;
                                SMLERPGLControl._glDetailExtraDetailClass __getDetailData = (SMLERPGLControl._glDetailExtraDetailClass)__getExtraData._departmentList[__loop];
                                if (__getDetailData._code.Length > 0)
                                {
                                    __resource.Append(MyLib._myUtil._convertTextToXml("insert into " + _g.d.gl_journal_depart_list._table + " (" + __fieldList + _g.d.gl_journal_depart_list._department_code + "," + _g.d.gl_journal_depart_list._department_name + "," +
                                        _g.d.gl_journal_depart_list._allocate_persent + "," + _g.d.gl_journal_depart_list._allocate_amount + "," + _g.d.gl_journal_depart_list._line_number + "," + _g.d.gl_journal_depart_list._line_number_detail + "," +
                                        _g.d.gl_journal_depart_list._account_code + ") values (" + __dataList + "\'" + __getDetailData._code + "\',\'" + __getDetailData._name + "\'," + __getDetailData._persent.ToString() + "," + __getDetailData._amount.ToString() + "," +
                                        __line_number.ToString() + "," + __line_number_detail.ToString() + ",\'" + __accountCode + "\')"));
                                }
                            }
                            // Project List
                            for (int __loop = 0; __loop < __getExtraData._projectList.Count; __loop++)
                            {
                                int __line_number = __row + 1;
                                int __line_number_detail = __loop + 1;
                                SMLERPGLControl._glDetailExtraDetailClass __getDetailData = (SMLERPGLControl._glDetailExtraDetailClass)__getExtraData._projectList[__loop];
                                if (__getDetailData._code.Length > 0)
                                {
                                    __resource.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _g.d.gl_journal_project_list._table + " (" + __fieldList + _g.d.gl_journal_project_list._project_code + "," + _g.d.gl_journal_project_list._project_name + "," +
                                        _g.d.gl_journal_project_list._allocate_persent + "," + _g.d.gl_journal_project_list._allocate_amount + "," + _g.d.gl_journal_project_list._line_number + "," + _g.d.gl_journal_project_list._line_number_detail + "," +
                                        _g.d.gl_journal_project_list._account_code + ") values (" + __dataList + "\'" + __getDetailData._code + "\',\'" + __getDetailData._name + "\'," + __getDetailData._persent.ToString() + "," + __getDetailData._amount.ToString() + "," +
                                        __line_number.ToString() + "," + __line_number_detail.ToString() + ",\'" + __accountCode + "\')"));
                                }
                            }
                            // Allocate List
                            for (int __loop = 0; __loop < __getExtraData._allocateList.Count; __loop++)
                            {
                                int __line_number = __row + 1;
                                int __line_number_detail = __loop + 1;
                                SMLERPGLControl._glDetailExtraDetailClass __getDetailData = (SMLERPGLControl._glDetailExtraDetailClass)__getExtraData._allocateList[__loop];
                                if (__getDetailData._code.Length > 0)
                                {
                                    __resource.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _g.d.gl_journal_allocate_list._table + " (" + __fieldList + _g.d.gl_journal_allocate_list._allocate_code + "," + _g.d.gl_journal_allocate_list._allocate_name + "," +
                                        _g.d.gl_journal_allocate_list._allocate_persent + "," + _g.d.gl_journal_allocate_list._allocate_amount + "," + _g.d.gl_journal_allocate_list._line_number + "," + _g.d.gl_journal_allocate_list._line_number_detail + "," +
                                        _g.d.gl_journal_allocate_list._account_code + ") values (" + __dataList + "\'" + __getDetailData._code + "\',\'" + __getDetailData._name + "\'," + __getDetailData._persent.ToString() + "," + __getDetailData._amount.ToString() + "," +
                                        __line_number.ToString() + "," + __line_number_detail.ToString() + ",\'" + __accountCode + "\')"));
                                }
                            }
                            // Job List
                            for (int __loop = 0; __loop < __getExtraData._jobList.Count; __loop++)
                            {
                                int __line_number = __row + 1;
                                int __line_number_detail = __loop + 1;
                                SMLERPGLControl._glDetailExtraDetailClass __getDetailData = (SMLERPGLControl._glDetailExtraDetailClass)__getExtraData._jobList[__loop];
                                if (__getDetailData._code.Length > 0)
                                {
                                    __resource.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _g.d.gl_journal_job_list._table + " (" + __fieldList + _g.d.gl_journal_job_list._job_code + "," + _g.d.gl_journal_job_list._job_name + "," +
                                        _g.d.gl_journal_job_list._allocate_persent + "," + _g.d.gl_journal_job_list._allocate_amount + "," + _g.d.gl_journal_job_list._line_number + "," + _g.d.gl_journal_job_list._line_number_detail + "," +
                                        _g.d.gl_journal_job_list._account_code + ") values (" + __dataList + "\'" + __getDetailData._code + "\',\'" + __getDetailData._name + "\'," + __getDetailData._persent.ToString() + "," + __getDetailData._amount.ToString() + "," +
                                        __line_number.ToString() + "," + __line_number_detail.ToString() + ",\'" + __accountCode + "\')"));
                                }
                            }
                        }
                    }
                }
            }
            catch
            {
                return false;
            }
            return true;
        }

        private void _closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
