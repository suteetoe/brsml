using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Diagnostics;

namespace SMLERPAPARControl
{
    public class _ar_ap_trans_screen_top : MyLib._myScreen
    {
        private _g.g._transControlTypeEnum _controlTypeTemp;
        MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();
        // เอ้ต้องแยกการค้นหาเป็นคนละตัวไม่งั้นมันก็จะ load ใหม่ตลอดทำให้ช้า
        ArrayList _search_data_full_buffer = new ArrayList();
        ArrayList _search_data_full_buffer_name = new ArrayList();
        int _search_data_full_buffer_addr = -1;
        MyLib._searchDataFull _search_data_full_pointer;
        _ap_ar_doc_ref _doc_ref = new _ap_ar_doc_ref();
        string __formatNumber_1 = _g.g._getFormatNumberStr(0, 0);
        string __formatNumber = MyLib._myGlobal._getFormatNumber("m02");
        string _searchName = "";
        /// <summary>
        /// กรณีไปแก้รหัสอ้างอิง ให้การค้นหาบิลอ้างอืง Load ใหม่
        /// </summary>
        private string _oldCustCode = "";
        TextBox _searchTextBox;
        public string _autorun_old_doc_no = "";
        string _old_cust_code = "";
        string _old_filed_name = "";
        /// <summary>
        /// ให้คำนวณวันที่เครดิตหรือไม่
        /// </summary>
        Boolean _processCredit = false;
        public string _screen_code = "";
        private _g.g._transControlTypeEnum _ictransControlTypeTemp;
        public MyLib._myManageData _managerData;
        // รหัสเอกสาร เอาไว้พิมพ์ฟอร์ม
        public string _docFormatCode = "";

        public _ar_ap_trans_screen_top()
        {
        }

        public _g.g._transControlTypeEnum _icTransControlType
        {
            set
            {
                this._ictransControlTypeTemp = value;
            }
            get
            {
                return this._ictransControlTypeTemp;
            }
        }

        public _g.g._transControlTypeEnum _transControlType
        {
            set
            {
                this._controlTypeTemp = value;
                this._build();
                this.Invalidate();
            }
            get
            {
                return this._controlTypeTemp;
            }
        }

        private int _buildCount = 0;
        //sormuk
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            Keys keyCode = (Keys)(int)keyData & Keys.KeyCode;
            if (keyData == Keys.F9)
            {
                _screenApControl__textBoxSearch(this._getControl(_g.d.ap_ar_trans._doc_format_code));
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        //somruk
        //void _setReadonlyControl(string name, bool readOnly)
        //{
        //    MyLib._myTextBox __docCodeControl = (MyLib._myTextBox)(this._getControl(name));
        //    __docCodeControl.TextBox.ReadOnly = readOnly;
        //}
        void _build()
        {
            if (this._controlTypeTemp == _g.g._transControlTypeEnum.ว่าง)
            {
                return;
            }
            this._buildCount++;
            if (this._buildCount > 1)
            {
                MessageBox.Show("_ar_ap_trans_screen_top : มีการสร้างจอสองครั้ง");
            }
            this.SuspendLayout();
            this._reset();
            this._clear();
            this._table_name = _g.d.ic_trans._table;
            int __row = 0;

            switch (this._controlTypeTemp)
            {
                //----------------------------------------------------------------------------------
                //(เจ้าหนี้) 
                #region เจ้าหนี้
                #region เจ้าหนี้ ตั้งหนี้ยกมา
                //----------------------------------------------------------------------------------
                // ยกมา
                //----------------------------------------------------------------------------------
                /// APDebtBalance : 1=ตั้งหนี้
                case _g.g._transControlTypeEnum.เจ้าหนี้_ตั้งหนี้ยกมา:
                    this._maxColumn = 2;
                    this._addDateBox(__row, 0, 1, 0, _g.d.ic_trans._doc_date, 1, true, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_no, 1, 1, 1, true, false, false);
                    // jead : iconnumber = 4 คืนค้นหาแบบเต็มจอ
                    this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._cust_code, 1, 1, 4, true, false, false, true, true, _g.d.ic_trans._ap_code);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_format_code, 1, 1, 0, true, false, true);
                    // jead this._addTextBox(1, 1, 1, 0, _g.d.ic_trans._cust_name, 1, 0, 0, false, false, true, false, false);
                    this._addNumberBox(__row, 0, 1, 0, _g.d.ic_trans._credit_day, 1, 1, true, __formatNumber_1);
                    this._addDateBox(__row++, 1, 1, 0, _g.d.ic_trans._due_date, 1, true, true);

                    if (MyLib._myGlobal._OEMVersion.Equals("SINGHA"))
                    {
                        this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._tax_doc_no, 1, 1, 0, true, false, true);
                        this._addDateBox(__row++, 1, 1, 0, _g.d.ic_trans._tax_doc_date, 1, true, true, true);
                        this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._doc_ref, 1, 1, 0, true, false, true, true, true);
                        this._addDateBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_ref_date, 1, true, true, true);
                    }

                    this._addNumberBox(__row++, 1, 1, 0, _g.d.ic_trans._total_amount, 1, 2, true, __formatNumber);
                    //this._addTextBox(4, 0, 1, 0, _g.d.ic_trans._currency_code, 1, 1, 1, true, false, true);
                    //this._addNumberBox(4, 1, 1, 0, _g.d.ic_trans._currency_money, 1, 1, true, __formatNumber);
                    // if (MyLib._myGlobal._isVersion.Equals("SMLColorStore"))
                    if (MyLib._myGlobal._isVersionEnum != MyLib._myGlobal._versionType.SMLColorStore)
                    {
                        this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._project_code, 1, 1, 1, true, false, true);
                        this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._allocate_code, 1, 1, 1, true, false, true);
                        this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._job_code, 1, 1, 1, true, false, true);
                        this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._side_code, 1, 1, 1, true, false, true);
                        this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._department_code, 1, 1, 1, true, false, true);
                        this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_group, 1, 1, 1, true, false, true);
                        this._addTextBox(__row++, 0, 2, 0, _g.d.ic_trans._remark, 2, 0, 0, true, false);
                    }
                    else
                    {
                        this._addTextBox(__row++, 0, 2, 0, _g.d.ic_trans._remark, 2, 0, 0, true, false);
                    }
                    this._processCredit = true;
                    this._enabedControl(_g.d.ic_trans._doc_format_code, false); ;
                    this._screen_code = "DA";
                    break;
                #endregion
                // เพิ่มหนี้
                case _g.g._transControlTypeEnum.เจ้าหนี้_เพิ่มหนี้ยกมา:
                    this._maxColumn = 2;
                    this._addDateBox(__row, 0, 1, 0, _g.d.ic_trans._doc_date, 1, true, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_no, 1, 1, 1, true, false, false);
                    // jead : iconnumber = 4 คืนค้นหาแบบเต็มจอ
                    this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._cust_code, 1, 1, 4, true, false, false, true, true, _g.d.ic_trans._ap_code);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_format_code, 1, 1, 0, true, false, true);
                    // jead this._addTextBox(1, 1, 1, 0, _g.d.ic_trans._cust_name, 1, 0, 0, false, false, true, false, false);
                    this._addNumberBox(__row, 0, 1, 0, _g.d.ic_trans._credit_day, 1, 1, true, __formatNumber_1);
                    this._addDateBox(__row++, 1, 1, 0, _g.d.ic_trans._due_date, 1, true, true);

                    if (MyLib._myGlobal._OEMVersion.Equals("SINGHA"))
                    {
                        this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._tax_doc_no, 1, 1, 0, true, false, true);
                        this._addDateBox(__row++, 1, 1, 0, _g.d.ic_trans._tax_doc_date, 1, true, true, true);
                        this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._doc_ref, 1, 1, 0, true, false, true, true, true);
                        this._addDateBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_ref_date, 1, true, true, true);
                    }

                    this._addNumberBox(__row++, 1, 1, 0, _g.d.ic_trans._total_amount, 1, 2, true, __formatNumber);
                    //this._addTextBox(4, 0, 1, 0, _g.d.ic_trans._currency_code, 1, 1, 1, true, false, true);
                    //this._addNumberBox(4, 1, 1, 0, _g.d.ic_trans._currency_money, 1, 1, true, __formatNumber);
                    if (MyLib._myGlobal._isVersionEnum != MyLib._myGlobal._versionType.SMLColorStore)
                    {
                        this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._project_code, 1, 1, 1, true, false, true);
                        this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._allocate_code, 1, 1, 1, true, false, true);
                        this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._job_code, 1, 1, 1, true, false, true);
                        this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._side_code, 1, 1, 1, true, false, true);
                        this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._department_code, 1, 1, 1, true, false, true);
                        this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_group, 1, 1, 1, true, false, true);
                        this._addTextBox(__row++, 0, 2, 0, _g.d.ic_trans._remark, 2, 0, 0, true, false);
                    }
                    else
                    {
                        this._addTextBox(__row++, 0, 2, 0, _g.d.ic_trans._remark, 2, 0, 0, true, false);
                    }
                    this._processCredit = true;
                    this._enabedControl(_g.d.ic_trans._doc_format_code, false); ;
                    this._screen_code = "DB";
                    break;
                /// APCNBalance : 2 ลดหนี้(ยกมา)
                case _g.g._transControlTypeEnum.เจ้าหนี้_ลดหนี้ยกมา:
                    this._maxColumn = 2;
                    this._addDateBox(__row, 0, 1, 0, _g.d.ic_trans._doc_date, 1, true, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_no, 1, 1, 1, true, false, false);
                    // jead : iconnumber = 4 คืนค้นหาแบบเต็มจอ
                    this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._cust_code, 1, 1, 4, true, false, false, true, true, _g.d.ic_trans._ap_code);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_format_code, 1, 1, 0, true, false, true);
                    // jead this._addTextBox(1, 1, 1, 0, _g.d.ic_trans._cust_name, 1, 0, 0, false, false, true, false, false);
                    this._addNumberBox(__row, 0, 1, 0, _g.d.ic_trans._credit_day, 1, 1, true, __formatNumber_1);
                    this._addDateBox(__row++, 1, 1, 0, _g.d.ic_trans._due_date, 1, true, true);

                    if (MyLib._myGlobal._OEMVersion.Equals("SINGHA"))
                    {
                        this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._tax_doc_no, 1, 1, 0, true, false, true);
                        this._addDateBox(__row++, 1, 1, 0, _g.d.ic_trans._tax_doc_date, 1, true, true, true);
                        this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._doc_ref, 1, 1, 0, true, false, true, true, true);
                        this._addDateBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_ref_date, 1, true, true, true);
                    }

                    this._addNumberBox(__row++, 1, 1, 0, _g.d.ic_trans._total_amount, 1, 2, true, __formatNumber);
                    //this._addTextBox(4, 0, 1, 0, _g.d.ic_trans._currency_code, 1, 1, 1, true, false, true);
                    //this._addNumberBox(4, 1, 1, 0, _g.d.ic_trans._currency_money, 1, 1, true, __formatNumber);
                    if (MyLib._myGlobal._isVersionEnum != MyLib._myGlobal._versionType.SMLColorStore)
                    {
                        this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._project_code, 1, 1, 1, true, false, true);
                        this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._allocate_code, 1, 1, 1, true, false, true);
                        this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._job_code, 1, 1, 1, true, false, true);
                        this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._side_code, 1, 1, 1, true, false, true);
                        this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._department_code, 1, 1, 1, true, false, true);
                        this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_group, 1, 1, 1, true, false, true);
                        this._addTextBox(__row++, 0, 2, 0, _g.d.ic_trans._remark, 2, 0, 0, true, false);
                    }
                    else
                    {
                        this._addTextBox(__row++, 0, 2, 0, _g.d.ic_trans._remark, 2, 0, 0, true, false);
                    }
                    this._processCredit = true;
                    this._enabedControl(_g.d.ic_trans._doc_format_code, false); ;
                    this._screen_code = "DC";
                    break;
                //----------------------------------------------------------------------------------
                // อื่นๆๆๆ
                //----------------------------------------------------------------------------------
                /// APDebtBalance : 1=ตั้งหนี้
                case _g.g._transControlTypeEnum.เจ้าหนี้_ตั้งหนี้อื่น_ยกเลิก:
                    this._maxColumn = 2;
                    this._addDateBox(0, 0, 1, 0, _g.d.ic_trans._doc_date, 1, true, false);
                    this._addTextBox(0, 1, 1, 0, _g.d.ic_trans._doc_no, 1, 1, 1, true, false, false);
                    this._addTextBox(1, 0, 1, 0, _g.d.ic_trans._doc_format_code, 1, 1, 0, true, false, true);
                    this._addTextBox(2, 0, 1, 0, _g.d.ic_trans._cust_code, 1, 1, 4, true, false, false, true, true, _g.d.ic_trans._ap_code);
                    // jead this._addTextBox(1, 1, 1, 0, _g.d.ic_trans._cust_name, 1, 0, 0, false, false, true, false, false);
                    this._addTextBox(2, 1, 1, 0, _g.d.ic_trans._doc_ref, 1, 1, 4, true, false, false);

                    this._addNumberBox(3, 0, 1, 0, _g.d.ic_trans._credit_day, 1, 1, true, __formatNumber_1);
                    this._addDateBox(3, 1, 1, 0, _g.d.ic_trans._due_date, 1, true, true);
                    //this._addTextBox(4, 0, 1, 0, _g.d.ic_trans._currency_code, 1, 1, 1, true, false, true);
                    //this._addNumberBox(4, 1, 1, 0, _g.d.ic_trans._currency_money, 1, 1, true, __formatNumber);
                    this._addNumberBox(4, 1, 1, 0, _g.d.ic_trans._total_amount, 1, 2, true, __formatNumber);
                    if (MyLib._myGlobal._isVersionEnum != MyLib._myGlobal._versionType.SMLColorStore)
                    {
                        this._addTextBox(5, 0, 1, 0, _g.d.ic_trans._project_code, 1, 1, 1, true, false, true);
                        this._addTextBox(5, 1, 1, 0, _g.d.ic_trans._allocate_code, 1, 1, 1, true, false, true);
                        this._addTextBox(6, 0, 1, 0, _g.d.ic_trans._job_code, 1, 1, 1, true, false, true);
                        this._addTextBox(6, 1, 1, 0, _g.d.ic_trans._side_code, 1, 1, 1, true, false, true);
                        this._addTextBox(7, 0, 1, 0, _g.d.ic_trans._department_code, 1, 1, 1, true, false, true);
                        this._addTextBox(4, 1, 1, 0, _g.d.ic_trans._doc_group, 1, 1, 1, true, false, true);
                        this._addTextBox(8, 0, 2, 0, _g.d.ic_trans._remark, 2, 0, 0, true, false);
                    }
                    else
                    {
                        this._addTextBox(5, 0, 2, 0, _g.d.ic_trans._remark, 2, 0, 0, true, false);
                    }
                    this._processCredit = false;
                    this._enabedControl(_g.d.ic_trans._doc_format_code, false); ;
                    break;
                // เพิ่มหนี้
                case _g.g._transControlTypeEnum.เจ้าหนี้_เพิ่มหนี้อื่น_ยกเลิก:
                    this._maxColumn = 2;
                    this._addDateBox(0, 0, 1, 0, _g.d.ic_trans._doc_date, 1, true, false);
                    this._addTextBox(0, 1, 1, 0, _g.d.ic_trans._doc_no, 1, 1, 1, true, false, false);
                    this._addTextBox(1, 0, 1, 0, _g.d.ic_trans._doc_format_code, 1, 1, 0, true, false, true);
                    this._addTextBox(2, 0, 1, 0, _g.d.ic_trans._cust_code, 1, 1, 4, true, false, false, true, true, _g.d.ic_trans._ap_code);
                    // jead this._addTextBox(1, 1, 1, 0, _g.d.ic_trans._cust_name, 1, 0, 0, false, false, true, false, false);
                    this._addTextBox(2, 1, 1, 0, _g.d.ic_trans._doc_ref, 1, 1, 4, true, false, false);
                    this._addNumberBox(3, 0, 1, 0, _g.d.ic_trans._credit_day, 1, 1, true, __formatNumber_1);
                    this._addDateBox(3, 1, 1, 0, _g.d.ic_trans._due_date, 1, true, true);
                    //this._addTextBox(4, 0, 1, 0, _g.d.ic_trans._currency_code, 1, 1, 1, true, false, true);
                    //this._addNumberBox(4, 1, 1, 0, _g.d.ic_trans._currency_money, 1, 1, true, __formatNumber);
                    this._addNumberBox(4, 1, 1, 0, _g.d.ic_trans._total_amount, 1, 2, true, __formatNumber);
                    if (MyLib._myGlobal._isVersionEnum != MyLib._myGlobal._versionType.SMLColorStore)
                    {
                        this._addTextBox(5, 0, 1, 0, _g.d.ic_trans._project_code, 1, 1, 1, true, false, true);
                        this._addTextBox(5, 1, 1, 0, _g.d.ic_trans._allocate_code, 1, 1, 1, true, false, true);
                        this._addTextBox(6, 0, 1, 0, _g.d.ic_trans._job_code, 1, 1, 1, true, false, true);
                        this._addTextBox(6, 1, 1, 0, _g.d.ic_trans._side_code, 1, 1, 1, true, false, true);
                        this._addTextBox(7, 0, 1, 0, _g.d.ic_trans._department_code, 1, 1, 1, true, false, true);
                        this._addTextBox(7, 1, 1, 0, _g.d.ic_trans._doc_group, 1, 1, 1, true, false, true);
                        this._addTextBox(8, 0, 2, 0, _g.d.ic_trans._remark, 2, 0, 0, true, false);
                    }
                    else
                    {
                        this._addTextBox(5, 0, 2, 0, _g.d.ic_trans._remark, 2, 0, 0, true, false);
                    }
                    this._processCredit = true;
                    this._enabedControl(_g.d.ic_trans._doc_format_code, false); ;
                    break;
                /// APCNBalanceOther : 2 ลดหนี้
                case _g.g._transControlTypeEnum.เจ้าหนี้_ลดหนี้อื่น_ยกเลิก:
                    this._maxColumn = 2;
                    this._addDateBox(0, 0, 1, 0, _g.d.ic_trans._doc_date, 1, true, false);
                    this._addTextBox(0, 1, 1, 0, _g.d.ic_trans._doc_no, 1, 1, 1, true, false, false);
                    this._addTextBox(1, 0, 1, 0, _g.d.ic_trans._doc_format_code, 1, 1, 0, true, false, true);
                    this._addTextBox(2, 0, 1, 0, _g.d.ic_trans._cust_code, 1, 1, 4, true, false, false, true, true, _g.d.ic_trans._ap_code);
                    // jead this._addTextBox(1, 1, 1, 0, _g.d.ic_trans._cust_name, 1, 0, 0, false, false, true, false, false);
                    this._addTextBox(2, 1, 1, 0, _g.d.ic_trans._doc_ref, 1, 1, 4, true, false, false);
                    this._addNumberBox(3, 0, 1, 0, _g.d.ic_trans._credit_day, 1, 1, true, __formatNumber_1);
                    this._addDateBox(3, 1, 1, 0, _g.d.ic_trans._due_date, 1, true, true);
                    //this._addTextBox(4, 0, 1, 0, _g.d.ic_trans._currency_code, 1, 1, 1, true, false, true);
                    //this._addNumberBox(4, 1, 1, 0, _g.d.ic_trans._currency_money, 1, 1, true, __formatNumber);
                    this._addNumberBox(4, 1, 1, 0, _g.d.ic_trans._total_amount, 1, 2, true, __formatNumber);
                    if (MyLib._myGlobal._isVersionEnum != MyLib._myGlobal._versionType.SMLColorStore)
                    {
                        this._addTextBox(5, 0, 1, 0, _g.d.ic_trans._project_code, 1, 1, 1, true, false, true);
                        this._addTextBox(5, 1, 1, 0, _g.d.ic_trans._allocate_code, 1, 1, 1, true, false, true);
                        this._addTextBox(6, 0, 1, 0, _g.d.ic_trans._job_code, 1, 1, 1, true, false, true);
                        this._addTextBox(6, 1, 1, 0, _g.d.ic_trans._side_code, 1, 1, 1, true, false, true);
                        this._addTextBox(7, 0, 1, 0, _g.d.ic_trans._department_code, 1, 1, 1, true, false, true);
                        //this._addTextBox(6, 1, 1, 0, _g.d.ic_trans._doc_group, 1, 1, 1, true, false, true);
                        this._addTextBox(8, 0, 2, 0, _g.d.ic_trans._remark, 2, 0, 0, true, false);
                    }
                    else
                    {
                        this._addTextBox(5, 0, 2, 0, _g.d.ic_trans._remark, 2, 0, 0, true, false);
                    }
                    this._enabedControl(_g.d.ic_trans._doc_format_code, false); ;
                    this._processCredit = true;
                    break;
                //----------------------------------------------------------------------------------
                /// APPayBill :2=รับวางบิล
                case _g.g._transControlTypeEnum.เจ้าหนี้_ใบรับวางบิล:
                    this._table_name = _g.d.ap_ar_trans._table;
                    this._maxColumn = 2;
                    this._addDateBox(0, 0, 1, 0, _g.d.ap_ar_trans._doc_date, 1, true, false);
                    this._addTextBox(0, 1, 1, 0, _g.d.ap_ar_trans._doc_no, 1, 1, 1, true, false, false);
                    this._addTextBox(1, 0, 1, 0, _g.d.ap_ar_trans._cust_code, 1, 1, 4, true, false, false, true, true, _g.d.ap_ar_trans._ap_code);
                    this._addTextBox(1, 1, 1, 0, _g.d.ap_ar_trans._doc_format_code, 1, 1, 0, true, false, true);
                    this._addDateBox(2, 0, 1, 0, _g.d.ap_ar_trans._due_date, 1, true, true, true, _g.d.ap_ar_trans._due_pay_date);
                    // toe เพิ่ม วันเครดิต
                    this._addNumberBox(3, 0, 1, 0, _g.d.ap_ar_trans._credit_day, 1, 1, true, __formatNumber_1);
                    this._addTextBox(4, 0, 1, 0, _g.d.ap_ar_trans._doc_ref, 1, 0, 0, true, false);
                    this._addDateBox(4, 1, 1, 0, _g.d.ap_ar_trans._doc_ref_date, 1, true);
                    this._addTextBox(5, 0, 2, 0, _g.d.ap_ar_trans._remark, 2, 0, 0, true, false);
                    this._processCredit = true;
                    this._screen_code = "DD";
                    this._enabedControl(_g.d.ic_trans._doc_format_code, false);
                    break;
                case _g.g._transControlTypeEnum.เจ้าหนี้_ใบรับวางบิล_ยกเลิก:
                    this._table_name = _g.d.ap_ar_trans._table;
                    this._maxColumn = 2;
                    this._addDateBox(0, 0, 1, 0, _g.d.ap_ar_trans._doc_date, 1, true, false);
                    this._addTextBox(0, 1, 1, 0, _g.d.ap_ar_trans._doc_no, 1, 1, 1, true, false, false);
                    this._addTextBox(1, 0, 1, 0, _g.d.ap_ar_trans._cust_code, 1, 1, 4, true, false, false, true, true, _g.d.ap_ar_trans._ap_code);
                    this._addTextBox(1, 1, 1, 0, _g.d.ap_ar_trans._doc_format_code, 1, 1, 0, true, false, true);
                    this._addTextBox(2, 0, 1, 0, _g.d.ap_ar_trans._doc_ref, 1, 1, 4, true, false, false, true, true, _g.d.ap_ar_trans._po_pay_bill);
                    this._addDateBox(2, 1, 1, 0, _g.d.ap_ar_trans._doc_ref_date, 1, true, false);
                    //this._addTextBox(2, 1, 1, 0, _g.d.ap_ar_trans._doc_group, 1, 1, 1, true, false, true);
                    this._addTextBox(3, 0, 2, 0, _g.d.ap_ar_trans._remark, 2, 0, 0, true, false);
                    this._processCredit = true;
                    this._enabedControl(_g.d.ap_ar_trans._doc_ref_date, false);
                    this._enabedControl(_g.d.ic_trans._doc_format_code, false); ;
                    this._screen_code = "DDC";
                    break;
                case _g.g._transControlTypeEnum.เจ้าหนี้_จ่ายชำระหนี้_ยกเลิก:
                    this._table_name = _g.d.ap_ar_trans._table;
                    this._maxColumn = 2;
                    this._addDateBox(0, 0, 1, 0, _g.d.ap_ar_trans._doc_date, 1, true, false);
                    this._addTextBox(0, 1, 1, 0, _g.d.ap_ar_trans._doc_no, 1, 1, 1, true, false, false);
                    this._addTextBox(1, 0, 1, 0, _g.d.ap_ar_trans._cust_code, 1, 1, 4, true, false, false, true, true, _g.d.ap_ar_trans._ap_code);
                    this._addTextBox(1, 1, 1, 0, _g.d.ap_ar_trans._doc_format_code, 1, 1, 0, true, false, true);
                    this._addTextBox(2, 0, 1, 0, _g.d.ap_ar_trans._doc_ref, 1, 1, 4, true, false, false, true, true, _g.d.ap_ar_trans._pay_bill_no_1);
                    this._addDateBox(2, 1, 1, 0, _g.d.ap_ar_trans._doc_ref_date, 1, true, false);
                    //this._addTextBox(2, 1, 1, 0, _g.d.ap_ar_trans._doc_group, 1, 1, 1, true, false, true);
                    this._addTextBox(3, 0, 2, 0, _g.d.ap_ar_trans._remark, 2, 0, 0, true, false);
                    if (_g.g._companyProfile._branchStatus == 1)
                    {
                        this._addTextBox(5, 0, 1, 0, _g.d.ic_trans._branch_code, 1, 1, 1, true, false, false);
                        if (_g.g._companyProfile._change_branch_code == false)
                        {
                            this._enabedControl(_g.d.ic_trans._branch_code, false);
                        }
                    }

                    this._processCredit = true;
                    this._enabedControl(_g.d.ap_ar_trans._doc_ref_date, false);
                    this._enabedControl(_g.d.ic_trans._doc_format_code, false);
                    this._screen_code = "DEC";
                    break;
                #endregion
                case _g.g._transControlTypeEnum.ลูกหนี้_ใบวางบิล_ยกเลิก:
                    this._table_name = _g.d.ap_ar_trans._table;
                    this._maxColumn = 2;
                    this._addDateBox(0, 0, 1, 0, _g.d.ap_ar_trans._doc_date, 1, true, false);
                    this._addTextBox(0, 1, 1, 0, _g.d.ap_ar_trans._doc_no, 1, 1, 1, true, false, false);
                    this._addTextBox(1, 0, 1, 0, _g.d.ap_ar_trans._cust_code, 1, 1, 4, true, false, false, true, true, _g.d.ap_ar_trans._ap_code);
                    this._addTextBox(1, 1, 1, 0, _g.d.ap_ar_trans._doc_format_code, 1, 1, 0, true, false, true);
                    this._addTextBox(2, 0, 1, 0, _g.d.ap_ar_trans._doc_ref, 1, 1, 4, true, false, false, true, true, _g.d.ap_ar_trans._so_pay_bill);
                    this._addDateBox(2, 1, 1, 0, _g.d.ap_ar_trans._doc_ref_date, 1, true, false);
                    //this._addTextBox(2, 1, 1, 0, _g.d.ap_ar_trans._doc_group, 1, 1, 1, true, false, true);
                    this._addTextBox(3, 0, 2, 0, _g.d.ap_ar_trans._remark, 2, 0, 0, true, false);
                    this._processCredit = true;
                    this._enabedControl(_g.d.ap_ar_trans._doc_ref_date, false);
                    this._enabedControl(_g.d.ic_trans._doc_format_code, false);
                    this._screen_code = "EDC";
                    break;
                case _g.g._transControlTypeEnum.ลูกหนี้_รับชำระหนี้_ยกเลิก:
                    this._table_name = _g.d.ap_ar_trans._table;
                    this._maxColumn = 2;
                    this._addDateBox(0, 0, 1, 0, _g.d.ap_ar_trans._doc_date, 1, true, false);
                    this._addTextBox(0, 1, 1, 0, _g.d.ap_ar_trans._doc_no, 1, 1, 1, true, false, false);
                    this._addTextBox(1, 0, 1, 0, _g.d.ap_ar_trans._cust_code, 1, 1, 4, true, false, false, true, true, _g.d.ap_ar_trans._ar_code);
                    this._addTextBox(1, 1, 1, 0, _g.d.ap_ar_trans._doc_format_code, 1, 1, 0, true, false, true);
                    this._addTextBox(2, 0, 1, 0, _g.d.ap_ar_trans._doc_ref, 1, 1, 4, true, false, false, true, true, _g.d.ap_ar_trans._pay_bill_no_2);
                    this._addDateBox(2, 1, 1, 0, _g.d.ap_ar_trans._doc_ref_date, 1, true, false);
                    //this._addTextBox(2, 1, 1, 0, _g.d.ap_ar_trans._doc_group, 1, 1, 1, true, false, true);
                    this._addTextBox(3, 0, 2, 0, _g.d.ap_ar_trans._remark, 2, 0, 0, true, false);
                    if (_g.g._companyProfile._branchStatus == 1)
                    {
                        __row++;
                        this._addTextBox(5, 0, 1, 0, _g.d.ic_trans._branch_code, 1, 1, 1, true, false, false);
                        if (_g.g._companyProfile._change_branch_code == false)
                        {
                            this._enabedControl(_g.d.ic_trans._branch_code, false);
                        }
                    }
                    this._processCredit = true;
                    this._enabedControl(_g.d.ap_ar_trans._doc_ref_date, false);
                    this._screen_code = "EEC";
                    this._enabedControl(_g.d.ic_trans._doc_format_code, false);
                    break;
                ///3=เตรียมจ่าย
                case _g.g._transControlTypeEnum.เจ้าหนี้_เตรียมจ่าย:
                    this._maxColumn = 2;
                    this._table_name = _g.d.ap_ar_trans._table;
                    this._addDateBox(0, 0, 1, 0, _g.d.ap_ar_trans._doc_date, 1, true, false);
                    this._addTextBox(0, 1, 1, 0, _g.d.ap_ar_trans._doc_no, 1, 1, 1, true, false, false);
                    this._addTextBox(1, 0, 1, 0, _g.d.ap_ar_trans._cust_code, 1, 1, 4, true, false, false, true, true, _g.d.ap_ar_trans._ap_code);
                    this._addTextBox(1, 1, 1, 0, _g.d.ap_ar_trans._doc_format_code, 1, 1, 0, true, false, true);
                    // jead this._addTextBox(1, 1, 1, 0, _g.d.ap_ar_trans._cust_name, 1, 0, 0, false, false, true, false, false);
                    if (MyLib._myGlobal._isVersionEnum != MyLib._myGlobal._versionType.SMLColorStore)
                    {
                        this._addTextBox(2, 0, 1, 0, _g.d.ap_ar_trans._project_code, 1, 1, 1, true, false, true);
                        this._addTextBox(2, 1, 1, 0, _g.d.ap_ar_trans._allocate_code, 1, 1, 1, true, false, true);
                        this._addTextBox(3, 0, 1, 0, _g.d.ap_ar_trans._job_code, 1, 1, 1, true, false, true);
                        this._addTextBox(3, 1, 1, 0, _g.d.ap_ar_trans._side_code, 1, 1, 1, true, false, true);
                        this._addTextBox(4, 0, 1, 0, _g.d.ap_ar_trans._department_code, 1, 1, 1, true, false, true);
                        this._addTextBox(4, 1, 1, 0, _g.d.ap_ar_trans._doc_group, 1, 1, 1, true, false, true);
                        this._addTextBox(5, 0, 2, 0, _g.d.ap_ar_trans._remark, 2, 0, 0, true, false);
                    }
                    else
                    {
                        this._addTextBox(2, 0, 2, 0, _g.d.ic_trans._remark, 2, 0, 0, true, false);
                    }

                    this._processCredit = true;
                    this._enabedControl(_g.d.ic_trans._doc_format_code, false);
                    break;
                case _g.g._transControlTypeEnum.เจ้าหนี้_เตรียมจ่าย_ยกเลิก:
                    this._maxColumn = 2;
                    this._table_name = _g.d.ap_ar_trans._table;
                    this._addDateBox(0, 0, 1, 0, _g.d.ap_ar_trans._doc_date, 1, true, false);
                    this._addTextBox(0, 1, 1, 0, _g.d.ap_ar_trans._doc_no, 1, 1, 1, true, false, false);
                    this._addTextBox(1, 0, 1, 0, _g.d.ap_ar_trans._doc_format_code, 1, 1, 0, true, false, true);
                    this._addTextBox(2, 0, 1, 0, _g.d.ap_ar_trans._cust_code, 1, 1, 4, true, false, false, true, true, _g.d.ap_ar_trans._ap_code);
                    // jead this._addTextBox(1, 1, 1, 0, _g.d.ap_ar_trans._cust_name, 1, 0, 0, false, false, true, false, false);
                    this._addTextBox(2, 1, 1, 0, _g.d.ap_ar_trans._doc_ref, 1, 1, 4, true, false, false);
                    if (MyLib._myGlobal._isVersionEnum != MyLib._myGlobal._versionType.SMLColorStore)
                    {
                        this._addTextBox(3, 0, 1, 0, _g.d.ap_ar_trans._project_code, 1, 1, 1, true, false, true);
                        this._addTextBox(3, 1, 1, 0, _g.d.ap_ar_trans._allocate_code, 1, 1, 1, true, false, true);
                        this._addTextBox(4, 0, 1, 0, _g.d.ap_ar_trans._job_code, 1, 1, 1, true, false, true);
                        this._addTextBox(4, 1, 1, 0, _g.d.ap_ar_trans._side_code, 1, 1, 1, true, false, true);
                        this._addTextBox(5, 0, 1, 0, _g.d.ap_ar_trans._department_code, 1, 1, 1, true, false, true);
                        this._addTextBox(5, 1, 1, 0, _g.d.ap_ar_trans._doc_group, 1, 1, 1, true, false, true);
                        this._addTextBox(6, 0, 2, 0, _g.d.ap_ar_trans._remark, 2, 0, 0, true, false);
                    }
                    else
                    {
                        this._addTextBox(3, 0, 2, 0, _g.d.ic_trans._remark, 2, 0, 0, true, false);
                    }
                    this._enabedControl(_g.d.ic_trans._doc_format_code, false);
                    break;
                /// APDebtBilling : 5=จ่ายชำระหนี้
                case _g.g._transControlTypeEnum.เจ้าหนี้_จ่ายชำระหนี้:
                    {
                        this._maxColumn = 2;
                        this._table_name = _g.d.ap_ar_trans._table;
                        this._addDateBox(__row, 0, 1, 0, _g.d.ap_ar_trans._doc_date, 1, true, false);
                        this._addTextBox(__row++, 1, 1, 0, _g.d.ap_ar_trans._doc_no, 1, 1, 1, true, false, false);
                        this._addTextBox(__row, 0, 1, 0, _g.d.ap_ar_trans._cust_code, 1, 1, 4, true, false, false, true, true, _g.d.ap_ar_trans._ap_code);
                        this._addTextBox(__row++, 1, 1, 0, _g.d.ap_ar_trans._doc_format_code, 1, 1, 0, true, false, true);

                        if (MyLib._myGlobal._OEMVersion.Equals("SINGHA"))
                        {
                            this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._tax_doc_no, 1, 1, 0, true, false, true, true, false, _g.d.ap_ar_trans._receipt_doc_no);
                            this._addDateBox(__row++, 1, 1, 0, _g.d.ic_trans._tax_doc_date, 1, true, true, true, _g.d.ap_ar_trans._receipt_doc_date);
                            this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._doc_ref, 1, 1, 0, true, false, true, true, true);
                            this._addDateBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_ref_date, 1, true, true, true);
                        }

                        // jead this._addTextBox(1, 1, 1, 0, _g.d.ap_ar_trans._cust_name, 1, 0, 0, false, false, true, false, false);
                        if (MyLib._myGlobal._isVersionEnum != MyLib._myGlobal._versionType.SMLColorStore)
                        {
                            this._addTextBox(__row, 0, 1, 0, _g.d.ap_ar_trans._project_code, 1, 1, 1, true, false, true);
                            this._addTextBox(__row++, 1, 1, 0, _g.d.ap_ar_trans._allocate_code, 1, 1, 1, true, false, true);
                            this._addTextBox(__row, 0, 1, 0, _g.d.ap_ar_trans._job_code, 1, 1, 1, true, false, true);
                            this._addTextBox(__row++, 1, 1, 0, _g.d.ap_ar_trans._side_code, 1, 1, 1, true, false, true);
                            this._addTextBox(__row, 0, 1, 0, _g.d.ap_ar_trans._department_code, 1, 1, 1, true, false, true);
                            this._addTextBox(__row++, 1, 1, 0, _g.d.ap_ar_trans._doc_group, 1, 1, 1, true, false, true);
                        }


                        this._addTextBox(__row, 0, 2, 0, _g.d.ic_trans._remark, 2, 0, 0, true, false);
                        this._processCredit = true;
                        this._screen_code = "DE";
                        this._enabedControl(_g.d.ic_trans._doc_format_code, false);
                    }
                    break;
                /// APDebtBillingCut : 6=ตัดหนี้สูญ
                case _g.g._transControlTypeEnum.เจ้าหนี้_ตัดหนี้สูญ:
                    this._maxColumn = 2;
                    this._table_name = _g.d.ap_ar_trans._table;
                    this._addDateBox(0, 0, 1, 0, _g.d.ap_ar_trans._doc_date, 1, true, false);
                    this._addTextBox(0, 1, 1, 0, _g.d.ap_ar_trans._doc_no, 1, 1, 1, true, false, false);
                    this._addTextBox(1, 0, 1, 0, _g.d.ap_ar_trans._cust_code, 1, 1, 4, true, false, false, true, true, _g.d.ap_ar_trans._ap_code);
                    this._addTextBox(1, 1, 1, 0, _g.d.ap_ar_trans._doc_format_code, 1, 1, 0, true, false, true);
                    // jead this._addTextBox(1, 1, 1, 0, _g.d.ap_ar_trans._cust_name, 1, 0, 0, false, false, true, false, false);
                    if (MyLib._myGlobal._isVersionEnum != MyLib._myGlobal._versionType.SMLColorStore)
                    {
                        this._addTextBox(2, 0, 1, 0, _g.d.ap_ar_trans._project_code, 1, 1, 1, true, false, true);
                        this._addTextBox(2, 1, 1, 0, _g.d.ap_ar_trans._allocate_code, 1, 1, 1, true, false, true);
                        this._addTextBox(3, 0, 1, 0, _g.d.ap_ar_trans._job_code, 1, 1, 1, true, false, true);
                        this._addTextBox(3, 1, 1, 0, _g.d.ap_ar_trans._side_code, 1, 1, 1, true, false, true);
                        this._addTextBox(4, 0, 1, 0, _g.d.ap_ar_trans._department_code, 1, 1, 1, true, false, true);
                        this._addTextBox(4, 1, 1, 0, _g.d.ap_ar_trans._doc_group, 1, 1, 1, true, false, true);
                        this._addTextBox(5, 0, 2, 0, _g.d.ap_ar_trans._remark, 2, 0, 0, true, false);
                    }
                    else
                    {
                        this._addTextBox(2, 0, 2, 0, _g.d.ic_trans._remark, 2, 0, 0, true, false);
                    }
                    this._enabedControl(_g.d.ic_trans._doc_format_code, false); ;
                    this._processCredit = true;
                    break;
                case _g.g._transControlTypeEnum.เจ้าหนี้_ตัดหนี้สูญ_ยกเลิก:
                    this._maxColumn = 2;
                    this._table_name = _g.d.ap_ar_trans._table;
                    this._addDateBox(0, 0, 1, 0, _g.d.ap_ar_trans._doc_date, 1, true, false);
                    this._addTextBox(0, 1, 1, 0, _g.d.ap_ar_trans._doc_no, 1, 1, 1, true, false, false);
                    this._addTextBox(1, 0, 1, 0, _g.d.ap_ar_trans._cust_code, 1, 1, 4, true, false, false, true, true, _g.d.ap_ar_trans._ap_code);
                    this._addTextBox(1, 1, 1, 0, _g.d.ap_ar_trans._doc_format_code, 1, 1, 0, true, false, true);
                    // jead this._addTextBox(1, 1, 1, 0, _g.d.ap_ar_trans._cust_name, 1, 0, 0, false, false, true, false, false);
                    this._addTextBox(2, 0, 1, 0, _g.d.ap_ar_trans._doc_ref, 1, 1, 4, true, false, false);
                    if (MyLib._myGlobal._isVersionEnum != MyLib._myGlobal._versionType.SMLColorStore)
                    {
                        this._addTextBox(2, 0, 1, 0, _g.d.ap_ar_trans._project_code, 1, 1, 1, true, false, true);
                        this._addTextBox(2, 1, 1, 0, _g.d.ap_ar_trans._allocate_code, 1, 1, 1, true, false, true);
                        this._addTextBox(3, 0, 1, 0, _g.d.ap_ar_trans._job_code, 1, 1, 1, true, false, true);
                        this._addTextBox(3, 1, 1, 0, _g.d.ap_ar_trans._side_code, 1, 1, 1, true, false, true);
                        this._addTextBox(4, 0, 1, 0, _g.d.ap_ar_trans._department_code, 1, 1, 1, true, false, true);
                        this._addTextBox(4, 1, 1, 0, _g.d.ap_ar_trans._doc_group, 1, 1, 1, true, false, true);
                        this._addTextBox(5, 0, 2, 0, _g.d.ap_ar_trans._remark, 2, 0, 0, true, false);
                    }
                    else
                    {
                        this._addTextBox(2, 0, 2, 0, _g.d.ic_trans._remark, 2, 0, 0, true, false);
                    }
                    this._enabedControl(_g.d.ic_trans._doc_format_code, false);
                    break;
                //--------------------------------------------------------------------------------------------------------------
                //ตั้งหนี้
                //--------------------------------------------------------------------------------------------------------------
                case _g.g._transControlTypeEnum.ลูกหนี้_ตั้งหนี้ยกมา:
                    this._maxColumn = 2;
                    //this._isSelecctLabel
                    this._addDateBox(__row, 0, 1, 0, _g.d.ic_trans._doc_date, 1, true, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_no, 1, 1, 1, true, false, false);
                    this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._cust_code, 1, 1, 4, true, false, false, true, true, "ar_code");
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_format_code, 1, 1, 0, true, false, true);
                    // jead this._addTextBox(1, 1, 1, 0, _g.d.ic_trans._cust_name, 1, 0, 0, false, false, true, false, false);
                    this._addNumberBox(__row, 0, 1, 0, _g.d.ic_trans._credit_day, 1, 1, true, __formatNumber_1);
                    this._addDateBox(__row++, 1, 1, 0, _g.d.ic_trans._due_date, 1, true, true);
                    //this._addTextBox(3, 0, 1, 0, _g.d.ic_trans._currency_code, 1, 1, 1, true, false, true);
                    //this._addNumberBox(3, 1, 1, 0, _g.d.ic_trans._currency_money, 1, 1, true, __formatNumber);

                    if (MyLib._myGlobal._OEMVersion.Equals("SINGHA"))
                    {
                        this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._tax_doc_no, 1, 1, 0, true, false, true);
                        this._addDateBox(__row++, 1, 1, 0, _g.d.ic_trans._tax_doc_date, 1, true, true, true);
                        this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._doc_ref, 1, 1, 0, true, false, true, true, true);
                        this._addDateBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_ref_date, 1, true, true, true);
                    }

                    this._addNumberBox(__row++, 1, 1, 0, _g.d.ic_trans._total_amount, 1, 2, true, __formatNumber);
                    if (MyLib._myGlobal._isVersionEnum != MyLib._myGlobal._versionType.SMLColorStore)
                    {
                        this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._project_code, 1, 1, 1, true, false, true);
                        this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._allocate_code, 1, 1, 1, true, false, true);
                        this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._job_code, 1, 1, 1, true, false, true);
                        this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._side_code, 1, 1, 1, true, false, true);
                        this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._department_code, 1, 1, 1, true, false, true);
                        this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_group, 1, 1, 1, true, false, true);
                        this._addTextBox(__row++, 0, 1, 0, _g.d.ic_trans._sale_code, 1, 1, 1, true, false, true);
                        this._addTextBox(__row++, 0, 2, 0, _g.d.ic_trans._remark, 2, 0, 0, true, false);
                    }
                    else
                    {
                        this._addTextBox(__row++, 0, 1, 0, _g.d.ic_trans._sale_code, 1, 1, 1, true, false, true);
                        this._addTextBox(__row++, 0, 2, 0, _g.d.ic_trans._remark, 2, 0, 0, true, false);
                    }
                    this._screen_code = "EA";
                    this._enabedControl(_g.d.ic_trans._doc_format_code, false); ;
                    this._processCredit = true;
                    break;
                case _g.g._transControlTypeEnum.ลูกหนี้_เพิ่มหนี้ยกมา:
                    this._maxColumn = 2;
                    //this._isSelecctLabel
                    this._addDateBox(__row, 0, 1, 0, _g.d.ic_trans._doc_date, 1, true, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_no, 1, 1, 1, true, false, false);
                    this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._cust_code, 1, 1, 4, true, false, false, true, true, "ar_code");
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_format_code, 1, 1, 0, true, false, true);
                    // jead this._addTextBox(1, 1, 1, 0, _g.d.ic_trans._cust_name, 1, 0, 0, false, false, true, false, false);
                    this._addNumberBox(__row, 0, 1, 0, _g.d.ic_trans._credit_day, 1, 1, true, __formatNumber_1);
                    this._addDateBox(__row++, 1, 1, 0, _g.d.ic_trans._due_date, 1, true, true);
                    //this._addTextBox(3, 0, 1, 0, _g.d.ic_trans._currency_code, 1, 1, 1, true, false, true);
                    //this._addNumberBox(3, 1, 1, 0, _g.d.ic_trans._currency_money, 1, 1, true, __formatNumber);

                    if (MyLib._myGlobal._OEMVersion.Equals("SINGHA"))
                    {
                        this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._tax_doc_no, 1, 1, 0, true, false, true);
                        this._addDateBox(__row++, 1, 1, 0, _g.d.ic_trans._tax_doc_date, 1, true, true, true);
                        this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._doc_ref, 1, 1, 0, true, false, true, true, true);
                        this._addDateBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_ref_date, 1, true, true, true);
                    }


                    this._addNumberBox(__row++, 1, 1, 0, _g.d.ic_trans._total_amount, 1, 2, true, __formatNumber);
                    if (MyLib._myGlobal._isVersionEnum != MyLib._myGlobal._versionType.SMLColorStore)
                    {
                        this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._project_code, 1, 1, 1, true, false, true);
                        this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._allocate_code, 1, 1, 1, true, false, true);
                        this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._job_code, 1, 1, 1, true, false, true);
                        this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._side_code, 1, 1, 1, true, false, true);
                        this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._department_code, 1, 1, 1, true, false, true);
                        this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_group, 1, 1, 1, true, false, true);
                        this._addTextBox(__row++, 0, 1, 0, _g.d.ic_trans._sale_code, 1, 1, 1, true, false, true);
                        this._addTextBox(__row++, 0, 2, 0, _g.d.ic_trans._remark, 2, 0, 0, true, false);
                    }
                    else
                    {
                        this._addTextBox(__row++, 0, 1, 0, _g.d.ic_trans._sale_code, 1, 1, 1, true, false, true);
                        this._addTextBox(__row++, 0, 2, 0, _g.d.ic_trans._remark, 2, 0, 0, true, false);
                    }
                    this._screen_code = "EB";
                    this._enabedControl(_g.d.ic_trans._doc_format_code, false); ;
                    this._processCredit = true;
                    break;
                case _g.g._transControlTypeEnum.ลูกหนี้_ลดหนี้ยกมา:
                    this._maxColumn = 2;
                    this._addDateBox(__row, 0, 1, 0, _g.d.ic_trans._doc_date, 1, true, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_no, 1, 1, 1, true, false, false);
                    this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._cust_code, 1, 1, 4, true, false, false, true, true, "ar_code");
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_format_code, 1, 1, 0, true, false, true);
                    // jead this._addTextBox(1, 1, 1, 0, _g.d.ic_trans._cust_name, 1, 0, 0, false, false, true, false, false);
                    this._addNumberBox(__row, 0, 1, 0, _g.d.ic_trans._credit_day, 1, 1, true, __formatNumber_1);
                    this._addDateBox(__row++, 1, 1, 0, _g.d.ic_trans._due_date, 1, true, true);
                    //this._addTextBox(3, 0, 1, 0, _g.d.ic_trans._currency_code, 1, 1, 1, true, false, true);
                    //this._addNumberBox(3, 1, 1, 0, _g.d.ic_trans._currency_money, 1, 1, true, __formatNumber);

                    if (MyLib._myGlobal._OEMVersion.Equals("SINGHA"))
                    {
                        this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._tax_doc_no, 1, 1, 0, true, false, true);
                        this._addDateBox(__row++, 1, 1, 0, _g.d.ic_trans._tax_doc_date, 1, true, true, true);
                        this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._doc_ref, 1, 1, 0, true, false, true, true, true);
                        this._addDateBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_ref_date, 1, true, true, true);
                    }

                    this._addNumberBox(__row++, 1, 1, 0, _g.d.ic_trans._total_amount, 1, 2, true, __formatNumber);
                    if (MyLib._myGlobal._isVersionEnum != MyLib._myGlobal._versionType.SMLColorStore)
                    {
                        this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._project_code, 1, 1, 1, true, false, true);
                        this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._allocate_code, 1, 1, 1, true, false, true);
                        this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._job_code, 1, 1, 1, true, false, true);
                        this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._side_code, 1, 1, 1, true, false, true);
                        this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._department_code, 1, 1, 1, true, false, true);
                        this._addTextBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_group, 1, 1, 1, true, false, true);
                        this._addTextBox(__row++, 0, 1, 0, _g.d.ic_trans._sale_code, 1, 1, 1, true, false, true);
                        this._addTextBox(__row++, 0, 2, 0, _g.d.ic_trans._remark, 2, 0, 0, true, false);
                    }
                    else
                    {
                        this._addTextBox(__row++, 0, 1, 0, _g.d.ic_trans._sale_code, 1, 1, 1, true, false, true);
                        this._addTextBox(__row++, 0, 2, 0, _g.d.ic_trans._remark, 2, 0, 0, true, false);
                    }
                    this._screen_code = "EC";
                    this._enabedControl(_g.d.ic_trans._doc_format_code, false); ;
                    this._processCredit = true;
                    break;
                //----------------------------------------------------------------------------------------------------

                case _g.g._transControlTypeEnum.ลูกหนี้_ตั้งหนี้อื่น:
                    this._maxColumn = 2;
                    //this._isSelecctLabel
                    this._addDateBox(0, 0, 1, 0, _g.d.ic_trans._doc_date, 1, true, false);
                    this._addTextBox(0, 1, 1, 0, _g.d.ic_trans._doc_no, 1, 1, 1, true, false, false);
                    this._addTextBox(1, 0, 1, 0, _g.d.ic_trans._cust_code, 1, 1, 4, true, false, false, true, true, "ar_code");
                    this._addTextBox(1, 1, 1, 0, _g.d.ic_trans._doc_format_code, 1, 1, 0, true, false, true);
                    // jead this._addTextBox(1, 1, 1, 0, _g.d.ic_trans._cust_name, 1, 0, 0, false, false, true, false, false);
                    this._addNumberBox(2, 0, 1, 0, _g.d.ic_trans._credit_day, 1, 1, true, __formatNumber_1);
                    this._addDateBox(2, 1, 1, 0, _g.d.ic_trans._due_date, 1, true, true);
                    //this._addTextBox(3, 0, 1, 0, _g.d.ic_trans._currency_code, 1, 1, 1, true, false, true);
                    //this._addNumberBox(3, 1, 1, 0, _g.d.ic_trans._currency_money, 1, 1, true, __formatNumber);
                    this._addNumberBox(3, 1, 1, 0, _g.d.ic_trans._total_amount, 1, 2, true, __formatNumber);
                    if (MyLib._myGlobal._isVersionEnum != MyLib._myGlobal._versionType.SMLColorStore)
                    {
                        this._addTextBox(4, 0, 1, 0, _g.d.ic_trans._project_code, 1, 1, 1, true, false, true);
                        this._addTextBox(4, 1, 1, 0, _g.d.ic_trans._allocate_code, 1, 1, 1, true, false, true);
                        this._addTextBox(5, 0, 1, 0, _g.d.ic_trans._job_code, 1, 1, 1, true, false, true);
                        this._addTextBox(5, 1, 1, 0, _g.d.ic_trans._side_code, 1, 1, 1, true, false, true);
                        this._addTextBox(6, 0, 1, 0, _g.d.ic_trans._department_code, 1, 1, 1, true, false, true);
                        this._addTextBox(6, 1, 1, 0, _g.d.ic_trans._doc_group, 1, 1, 1, true, false, true);
                        this._addTextBox(7, 0, 1, 0, _g.d.ic_trans._sale_code, 1, 1, 1, true, false, true);
                        this._addTextBox(8, 0, 2, 0, _g.d.ic_trans._remark, 2, 0, 0, true, false);
                    }
                    else
                    {
                        this._addTextBox(4, 0, 1, 0, _g.d.ic_trans._sale_code, 1, 1, 1, true, false, true);
                        this._addTextBox(5, 0, 2, 0, _g.d.ic_trans._remark, 2, 0, 0, true, false);
                    }
                    this._enabedControl(_g.d.ic_trans._doc_format_code, false); ;
                    this._processCredit = true;
                    break;
                case _g.g._transControlTypeEnum.ลูกหนี้_ตั้งหนี้อื่น_ยกเลิก:
                    this._maxColumn = 2;
                    this._addDateBox(0, 0, 1, 0, _g.d.ic_trans._doc_date, 1, true, false);
                    this._addTextBox(0, 1, 1, 0, _g.d.ic_trans._doc_no, 1, 1, 1, true, false, false);
                    this._addTextBox(1, 0, 1, 0, _g.d.ic_trans._doc_format_code, 1, 1, 0, true, false, true);
                    this._addTextBox(2, 0, 1, 0, _g.d.ic_trans._cust_code, 1, 1, 4, true, false, false, true, true, "ar_code");
                    this._addTextBox(2, 1, 1, 0, _g.d.ic_trans._doc_ref, 1, 1, 4, true, false, false);
                    // jead this._addTextBox(1, 1, 1, 0, _g.d.ic_trans._cust_name, 1, 0, 0, false, false, true, false, false);
                    this._addNumberBox(3, 0, 1, 0, _g.d.ic_trans._credit_day, 1, 1, true, __formatNumber_1);
                    this._addDateBox(3, 1, 1, 0, _g.d.ic_trans._due_date, 1, true, true);
                    //this._addTextBox(3, 0, 1, 0, _g.d.ic_trans._currency_code, 1, 1, 1, true, false, true);
                    //this._addNumberBox(3, 1, 1, 0, _g.d.ic_trans._currency_money, 1, 1, true, __formatNumber);
                    this._addNumberBox(4, 1, 1, 0, _g.d.ic_trans._total_amount, 1, 2, true, __formatNumber);
                    if (MyLib._myGlobal._isVersionEnum != MyLib._myGlobal._versionType.SMLColorStore)
                    {
                        this._addTextBox(5, 0, 1, 0, _g.d.ic_trans._project_code, 1, 1, 1, true, false, true);
                        this._addTextBox(5, 1, 1, 0, _g.d.ic_trans._allocate_code, 1, 1, 1, true, false, true);
                        this._addTextBox(6, 0, 1, 0, _g.d.ic_trans._job_code, 1, 1, 1, true, false, true);
                        this._addTextBox(6, 1, 1, 0, _g.d.ic_trans._side_code, 1, 1, 1, true, false, true);
                        this._addTextBox(7, 0, 1, 0, _g.d.ic_trans._department_code, 1, 1, 1, true, false, true);
                        this._addTextBox(7, 1, 1, 0, _g.d.ic_trans._doc_group, 1, 1, 1, true, false, true);
                        this._addTextBox(8, 0, 1, 0, _g.d.ic_trans._sale_code, 1, 1, 1, true, false, true);
                        this._addTextBox(9, 0, 2, 0, _g.d.ic_trans._remark, 2, 0, 0, true, false);
                    }
                    else
                    {
                        this._addTextBox(5, 0, 1, 0, _g.d.ic_trans._sale_code, 1, 1, 1, true, false, true);
                        this._addTextBox(6, 0, 2, 0, _g.d.ic_trans._remark, 2, 0, 0, true, false);
                    }
                    this._enabedControl(_g.d.ic_trans._doc_format_code, false); ;
                    break;
                case _g.g._transControlTypeEnum.ลูกหนี้_เพิ่มหนี้อื่น:
                    this._maxColumn = 2;
                    //this._isSelecctLabel
                    this._addDateBox(0, 0, 1, 0, _g.d.ic_trans._doc_date, 1, true, false);
                    this._addTextBox(0, 1, 1, 0, _g.d.ic_trans._doc_no, 1, 1, 1, true, false, false);
                    this._addTextBox(1, 0, 1, 0, _g.d.ic_trans._cust_code, 1, 1, 4, true, false, false, true, true, "ar_code");
                    this._addTextBox(1, 1, 1, 0, _g.d.ic_trans._doc_format_code, 1, 1, 0, true, false, true);
                    // jead this._addTextBox(1, 1, 1, 0, _g.d.ic_trans._cust_name, 1, 0, 0, false, false, true, false, false);
                    this._addNumberBox(2, 0, 1, 0, _g.d.ic_trans._credit_day, 1, 1, true, __formatNumber_1);
                    this._addDateBox(2, 1, 1, 0, _g.d.ic_trans._due_date, 1, true, true);
                    //this._addTextBox(3, 0, 1, 0, _g.d.ic_trans._currency_code, 1, 1, 1, true, false, true);
                    //this._addNumberBox(3, 1, 1, 0, _g.d.ic_trans._currency_money, 1, 1, true, __formatNumber);
                    this._addNumberBox(3, 1, 1, 0, _g.d.ic_trans._total_amount, 1, 2, true, __formatNumber);
                    if (MyLib._myGlobal._isVersionEnum != MyLib._myGlobal._versionType.SMLColorStore)
                    {
                        this._addTextBox(4, 0, 1, 0, _g.d.ic_trans._project_code, 1, 1, 1, true, false, true);
                        this._addTextBox(4, 1, 1, 0, _g.d.ic_trans._allocate_code, 1, 1, 1, true, false, true);
                        this._addTextBox(5, 0, 1, 0, _g.d.ic_trans._job_code, 1, 1, 1, true, false, true);
                        this._addTextBox(5, 1, 1, 0, _g.d.ic_trans._side_code, 1, 1, 1, true, false, true);
                        this._addTextBox(6, 0, 1, 0, _g.d.ic_trans._department_code, 1, 1, 1, true, false, true);
                        this._addTextBox(6, 1, 1, 0, _g.d.ic_trans._doc_group, 1, 1, 1, true, false, true);
                        this._addTextBox(7, 0, 1, 0, _g.d.ic_trans._sale_code, 1, 1, 1, true, false, true);
                        this._addTextBox(8, 0, 2, 0, _g.d.ic_trans._remark, 2, 0, 0, true, false);
                    }
                    else
                    {
                        this._addTextBox(4, 0, 1, 0, _g.d.ic_trans._sale_code, 1, 1, 1, true, false, true);
                        this._addTextBox(5, 0, 2, 0, _g.d.ic_trans._remark, 2, 0, 0, true, false);
                    }
                    this._processCredit = true;
                    this._enabedControl(_g.d.ic_trans._doc_format_code, false); ;
                    break;
                case _g.g._transControlTypeEnum.ลูกหนี้_เพิ่มหนี้อื่น_ยกเลิก:
                    this._maxColumn = 2;
                    //this._isSelecctLabel
                    this._addDateBox(0, 0, 1, 0, _g.d.ic_trans._doc_date, 1, true, false);
                    this._addTextBox(0, 1, 1, 0, _g.d.ic_trans._doc_no, 1, 1, 1, true, false, false);
                    this._addTextBox(1, 0, 1, 0, _g.d.ic_trans._doc_format_code, 1, 1, 0, true, false, true);
                    this._addTextBox(2, 0, 1, 0, _g.d.ic_trans._cust_code, 1, 1, 4, true, false, false, true, true, "ar_code");
                    this._addTextBox(2, 1, 1, 0, _g.d.ic_trans._doc_ref, 1, 1, 4, true, false, false);
                    // jead this._addTextBox(1, 1, 1, 0, _g.d.ic_trans._cust_name, 1, 0, 0, false, false, true, false, false);
                    this._addNumberBox(3, 0, 1, 0, _g.d.ic_trans._credit_day, 1, 1, true, __formatNumber_1);
                    this._addDateBox(3, 1, 1, 0, _g.d.ic_trans._due_date, 1, true, true);
                    //this._addTextBox(3, 0, 1, 0, _g.d.ic_trans._currency_code, 1, 1, 1, true, false, true);
                    //this._addNumberBox(3, 1, 1, 0, _g.d.ic_trans._currency_money, 1, 1, true, __formatNumber);
                    this._addNumberBox(4, 1, 1, 0, _g.d.ic_trans._total_amount, 1, 2, true, __formatNumber);
                    if (MyLib._myGlobal._isVersionEnum != MyLib._myGlobal._versionType.SMLColorStore)
                    {
                        this._addTextBox(5, 0, 1, 0, _g.d.ic_trans._project_code, 1, 1, 1, true, false, true);
                        this._addTextBox(5, 1, 1, 0, _g.d.ic_trans._allocate_code, 1, 1, 1, true, false, true);
                        this._addTextBox(6, 0, 1, 0, _g.d.ic_trans._job_code, 1, 1, 1, true, false, true);
                        this._addTextBox(6, 1, 1, 0, _g.d.ic_trans._side_code, 1, 1, 1, true, false, true);
                        this._addTextBox(7, 0, 1, 0, _g.d.ic_trans._department_code, 1, 1, 1, true, false, true);
                        this._addTextBox(7, 1, 1, 0, _g.d.ic_trans._doc_group, 1, 1, 1, true, false, true);
                        this._addTextBox(8, 0, 1, 0, _g.d.ic_trans._sale_code, 1, 1, 1, true, false, true);
                        this._addTextBox(9, 0, 2, 0, _g.d.ic_trans._remark, 2, 0, 0, true, false);
                    }
                    else
                    {
                        this._addTextBox(5, 0, 1, 0, _g.d.ic_trans._sale_code, 1, 1, 1, true, false, true);
                        this._addTextBox(6, 0, 2, 0, _g.d.ic_trans._remark, 2, 0, 0, true, false);
                    }
                    this._enabedControl(_g.d.ic_trans._doc_format_code, false); ;
                    break;
                case _g.g._transControlTypeEnum.ลูกหนี้_ลดหนี้อื่น:
                    this._maxColumn = 2;
                    this._addDateBox(0, 0, 1, 0, _g.d.ic_trans._doc_date, 1, true, false);
                    this._addTextBox(0, 1, 1, 0, _g.d.ic_trans._doc_no, 1, 1, 1, true, false, false);
                    this._addTextBox(1, 0, 1, 0, _g.d.ic_trans._cust_code, 1, 1, 4, true, false, false, true, true, "ar_code");
                    this._addTextBox(1, 1, 1, 0, _g.d.ic_trans._doc_format_code, 1, 1, 0, true, false, true);
                    // jead this._addTextBox(1, 1, 1, 0, _g.d.ic_trans._cust_name, 1, 0, 0, false, false, true, false, false);
                    this._addNumberBox(2, 0, 1, 0, _g.d.ic_trans._credit_day, 1, 1, true, __formatNumber_1);
                    this._addDateBox(2, 1, 1, 0, _g.d.ic_trans._due_date, 1, true, true);
                    //this._addTextBox(3, 0, 1, 0, _g.d.ic_trans._currency_code, 1, 1, 1, true, false, true);
                    //this._addNumberBox(3, 1, 1, 0, _g.d.ic_trans._currency_money, 1, 1, true, __formatNumber);
                    this._addNumberBox(3, 1, 1, 0, _g.d.ic_trans._total_amount, 1, 2, true, __formatNumber);
                    if (MyLib._myGlobal._isVersionEnum != MyLib._myGlobal._versionType.SMLColorStore)
                    {
                        this._addTextBox(4, 0, 1, 0, _g.d.ic_trans._project_code, 1, 1, 1, true, false, true);
                        this._addTextBox(4, 1, 1, 0, _g.d.ic_trans._allocate_code, 1, 1, 1, true, false, true);
                        this._addTextBox(5, 0, 1, 0, _g.d.ic_trans._job_code, 1, 1, 1, true, false, true);
                        this._addTextBox(5, 1, 1, 0, _g.d.ic_trans._side_code, 1, 1, 1, true, false, true);
                        this._addTextBox(6, 0, 1, 0, _g.d.ic_trans._department_code, 1, 1, 1, true, false, true);
                        this._addTextBox(6, 1, 1, 0, _g.d.ic_trans._doc_group, 1, 1, 1, true, false, true);
                        this._addTextBox(7, 0, 1, 0, _g.d.ic_trans._sale_code, 1, 1, 1, true, false, true);
                        this._addTextBox(8, 0, 2, 0, _g.d.ic_trans._remark, 2, 0, 0, true, false);
                    }
                    else
                    {
                        this._addTextBox(4, 0, 1, 0, _g.d.ic_trans._sale_code, 1, 1, 1, true, false, true);
                        this._addTextBox(5, 0, 2, 0, _g.d.ic_trans._remark, 2, 0, 0, true, false);
                    }
                    this._processCredit = true;
                    this._enabedControl(_g.d.ic_trans._doc_format_code, false); ;
                    break;
                case _g.g._transControlTypeEnum.ลูกหนี้_ลดหนี้อื่น_ยกเลิก:
                    this._maxColumn = 2;
                    this._addDateBox(0, 0, 1, 0, _g.d.ic_trans._doc_date, 1, true, false);
                    this._addTextBox(0, 1, 1, 0, _g.d.ic_trans._doc_no, 1, 1, 1, true, false, false);
                    this._addTextBox(1, 0, 1, 0, _g.d.ic_trans._cust_code, 1, 1, 4, true, false, false, true, true, "ar_code");
                    this._addTextBox(1, 1, 1, 0, _g.d.ic_trans._doc_format_code, 1, 1, 0, true, false, true);
                    // jead this._addTextBox(1, 1, 1, 0, _g.d.ic_trans._cust_name, 1, 0, 0, false, false, true, false, false);
                    this._addNumberBox(2, 0, 1, 0, _g.d.ic_trans._credit_day, 1, 1, true, __formatNumber_1);
                    this._addDateBox(2, 1, 1, 0, _g.d.ic_trans._due_date, 1, true, true);
                    //this._addTextBox(3, 0, 1, 0, _g.d.ic_trans._currency_code, 1, 1, 1, true, false, true);
                    //this._addNumberBox(3, 1, 1, 0, _g.d.ic_trans._currency_money, 1, 1, true, __formatNumber);
                    this._addNumberBox(3, 1, 1, 0, _g.d.ic_trans._total_amount, 1, 2, true, __formatNumber);
                    if (MyLib._myGlobal._isVersionEnum != MyLib._myGlobal._versionType.SMLColorStore)
                    {
                        this._addTextBox(4, 0, 1, 0, _g.d.ic_trans._project_code, 1, 1, 1, true, false, true);
                        this._addTextBox(4, 1, 1, 0, _g.d.ic_trans._allocate_code, 1, 1, 1, true, false, true);
                        this._addTextBox(5, 0, 1, 0, _g.d.ic_trans._job_code, 1, 1, 1, true, false, true);
                        this._addTextBox(5, 1, 1, 0, _g.d.ic_trans._side_code, 1, 1, 1, true, false, true);
                        this._addTextBox(6, 0, 1, 0, _g.d.ic_trans._department_code, 1, 1, 1, true, false, true);
                        this._addTextBox(6, 1, 1, 0, _g.d.ic_trans._doc_group, 1, 1, 1, true, false, true);
                        this._addTextBox(7, 0, 1, 0, _g.d.ic_trans._sale_code, 1, 1, 1, true, false, true);
                        this._addTextBox(8, 0, 2, 0, _g.d.ic_trans._remark, 2, 0, 0, true, false);
                    }
                    else
                    {
                        this._addTextBox(4, 0, 1, 0, _g.d.ic_trans._sale_code, 1, 1, 1, true, false, true);
                        this._addTextBox(5, 0, 2, 0, _g.d.ic_trans._remark, 2, 0, 0, true, false);
                    }
                    this._enabedControl(_g.d.ic_trans._doc_format_code, false); ;
                    break;
                //----------------------------------------------------------------------------------------------------
                case _g.g._transControlTypeEnum.ลูกหนี้_ใบวางบิล:
                    this._table_name = _g.d.ap_ar_trans._table;

                    this._maxColumn = 2;
                    this._addDateBox(0, 0, 1, 0, _g.d.ap_ar_trans._doc_date, 1, true, false);
                    this._addTextBox(0, 1, 1, 0, _g.d.ap_ar_trans._doc_no, 1, 1, 1, true, false, false);
                    this._addTextBox(1, 0, 1, 0, _g.d.ap_ar_trans._cust_code, 1, 1, 4, true, false, false, true, true, _g.d.ap_ar_trans._ar_code);
                    this._addTextBox(1, 1, 1, 0, _g.d.ap_ar_trans._doc_format_code, 1, 1, 0, true, false, true);
                    this._addDateBox(2, 0, 1, 0, _g.d.ap_ar_trans._due_date, 1, true, true, true, _g.d.ap_ar_trans._due_pay_date);
                    // toe เพิ่ม วันเครดิต
                    this._addNumberBox(3, 0, 1, 0, _g.d.ic_trans._credit_day, 1, 1, true, __formatNumber_1);

                    this._addTextBox(4, 0, 1, 0, _g.d.ap_ar_trans._doc_ref, 1, 0, 0, true, false);
                    this._addDateBox(4, 1, 1, 0, _g.d.ap_ar_trans._doc_ref_date, 1, true);
                    this._addTextBox(5, 0, 2, 0, _g.d.ap_ar_trans._remark, 2, 0, 0, true, false);
                    this._addTextBox(7, 0, 1, 0, _g.d.ap_ar_trans._sale_code, 1, 1, 1, true, false, true, true, true, _g.d.ap_ar_trans._employee);

                    this._processCredit = true;
                    this._screen_code = "ED";
                    this._enabedControl(_g.d.ic_trans._doc_format_code, false); ;
                    break;
                case _g.g._transControlTypeEnum.ลูกหนี้_ใบเสร็จชั่วคราว:
                    //ออกใบเสร็จชั่วคราว
                    this._maxColumn = 2;
                    this._table_name = _g.d.ap_ar_trans._table;
                    this._addDateBox(0, 0, 1, 0, _g.d.ap_ar_trans._doc_date, 1, true, false);
                    this._addTextBox(0, 1, 1, 0, _g.d.ap_ar_trans._doc_no, 1, 1, 1, true, false, false);
                    this._addTextBox(1, 1, 1, 0, _g.d.ap_ar_trans._doc_format_code, 1, 1, 0, true, false, true);
                    this._addTextBox(2, 0, 1, 0, _g.d.ap_ar_trans._cust_code, 1, 1, 4, true, false, false, true, true, "ar_code");
                    // jead this._addTextBox(1, 1, 1, 0, _g.d.ap_ar_trans._cust_name, 1, 0, 0, false, false, true, false, false);
                    this._addTextBox(3, 0, 2, 0, _g.d.ap_ar_trans._remark, 2, 0, 0, true, false);
                    this._screen_code = "ART";
                    this._enabedControl(_g.d.ic_trans._doc_format_code, false); ;
                    break;
                case _g.g._transControlTypeEnum.ลูกหนี้_ใบเสร็จชั่วคราว_ยกเลิก:
                    //ออกใบเสร็จชั่วคราว
                    this._maxColumn = 2;
                    this._table_name = _g.d.ic_trans._table;
                    this._addDateBox(0, 0, 1, 0, _g.d.ap_ar_trans._doc_date, 1, true, false);
                    this._addTextBox(0, 1, 1, 0, _g.d.ap_ar_trans._doc_no, 1, 1, 1, true, false, false);
                    this._addTextBox(1, 1, 1, 0, _g.d.ap_ar_trans._doc_format_code, 1, 1, 0, true, false, true);
                    this._addTextBox(2, 0, 1, 0, _g.d.ap_ar_trans._cust_code, 1, 1, 4, true, false, false, true, true, "ar_code");
                    this._addTextBox(2, 1, 1, 0, _g.d.ap_ar_trans._doc_ref, 1, 1, 4, true, false, false);
                    // jead this._addTextBox(1, 1, 1, 0, _g.d.ap_ar_trans._cust_name, 1, 0, 0, false, false, true, false, false);
                    this._addTextBox(3, 0, 2, 0, _g.d.ap_ar_trans._remark, 2, 0, 0, true, false);
                    this._screen_code = "ATC";
                    this._enabedControl(_g.d.ic_trans._doc_format_code, false); ;
                    break;
                case _g.g._transControlTypeEnum.ลูกหนี้_รับชำระหนี้:
                    //10=รับชำระหนี้
                    this._maxColumn = 2;
                    this._table_name = _g.d.ap_ar_trans._table;
                    this._addDateBox(__row, 0, 1, 0, _g.d.ap_ar_trans._doc_date, 1, true, false);
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ap_ar_trans._doc_no, 1, 1, 1, true, false, false);
                    this._addTextBox(__row, 0, 1, 0, _g.d.ap_ar_trans._cust_code, 1, 1, 4, true, false, false, true, true, "ar_code");
                    this._addTextBox(__row++, 1, 1, 0, _g.d.ap_ar_trans._doc_format_code, 1, 1, 0, true, false, true);

                    if (MyLib._myGlobal._OEMVersion.Equals("SINGHA"))
                    {
                        this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._tax_doc_no, 1, 1, 0, true, false, true, true, false, _g.d.ap_ar_trans._receipt_doc_no);
                        this._addDateBox(__row++, 1, 1, 0, _g.d.ic_trans._tax_doc_date, 1, true, true, true, _g.d.ap_ar_trans._receipt_doc_date);
                        this._addTextBox(__row, 0, 1, 0, _g.d.ic_trans._doc_ref, 1, 1, 0, true, false, true, true, true);
                        this._addDateBox(__row++, 1, 1, 0, _g.d.ic_trans._doc_ref_date, 1, true, true, true);
                    }

                    this._addTextBox(__row++, 0, 1, 0, _g.d.ap_ar_trans._sale_code, 1, 1, 1, true, false, true, true, true, _g.d.ap_ar_trans._employee);
                    this._addTextBox(__row++, 0, 2, 0, _g.d.ap_ar_trans._remark, 2, 0, 0, true, false);

                    this._screen_code = "EE";
                    this._enabedControl(_g.d.ic_trans._doc_format_code, false); ;
                    break;
                case _g.g._transControlTypeEnum.ลูกหนี้_ตัดหนี้สูญ:
                    //11=ตัดหนี้สูญ
                    this._maxColumn = 2;
                    this._table_name = _g.d.ap_ar_trans._table;
                    this._addDateBox(0, 0, 1, 0, _g.d.ap_ar_trans._doc_date, 1, true, false);
                    this._addTextBox(0, 1, 1, 0, _g.d.ap_ar_trans._doc_no, 1, 1, 1, true, false, false);
                    this._addTextBox(1, 0, 1, 0, _g.d.ap_ar_trans._cust_code, 1, 1, 4, true, false, false, true, true, "ar_code");
                    this._addTextBox(1, 1, 1, 0, _g.d.ap_ar_trans._doc_format_code, 1, 1, 0, true, false, true);
                    // jead this._addTextBox(1, 1, 1, 0, _g.d.ap_ar_trans._cust_name, 1, 0, 0, false, false, true, false, false);
                    this._addTextBox(2, 0, 2, 0, _g.d.ap_ar_trans._remark, 2, 0, 0, true, false);
                    this._screen_code = "AWO";
                    this._enabedControl(_g.d.ic_trans._doc_format_code, false); ;
                    break;
                case _g.g._transControlTypeEnum.ลูกหนี้_ตัดหนี้สูญ_ยกเลิก:
                    //11=ตัดหนี้สูญ
                    this._maxColumn = 2;
                    this._table_name = _g.d.ap_ar_trans._table;
                    this._addDateBox(0, 0, 1, 0, _g.d.ap_ar_trans._doc_date, 1, true, false);
                    this._addTextBox(0, 1, 1, 0, _g.d.ap_ar_trans._doc_no, 1, 1, 1, true, false, false);
                    this._addTextBox(1, 0, 1, 0, _g.d.ap_ar_trans._doc_format_code, 1, 1, 0, true, false, true);
                    this._addTextBox(2, 0, 1, 0, _g.d.ap_ar_trans._cust_code, 1, 1, 4, true, false, false, true, true, "ar_code");
                    this._addTextBox(2, 1, 1, 0, _g.d.ap_ar_trans._doc_ref, 1, 1, 4, true, false, false);
                    // jead this._addTextBox(1, 1, 1, 0, _g.d.ap_ar_trans._cust_name, 1, 0, 0, false, false, true, false, false);
                    this._addTextBox(3, 0, 2, 0, _g.d.ap_ar_trans._remark, 2, 0, 0, true, false);
                    this._screen_code = "AWC";
                    this._enabedControl(_g.d.ic_trans._doc_format_code, false); ;
                    break;
                case _g.g._transControlTypeEnum.IMEX_Bill_Collector:
                    this._maxColumn = 2;
                    this._table_name = _g.d.ap_ar_trans._table;
                    this._addDateBox(0, 0, 1, 0, _g.d.ap_ar_trans._doc_date, 1, true, false);
                    this._addTextBox(0, 1, 1, 0, _g.d.ap_ar_trans._doc_no, 1, 1, 1, true, false, false);
                    this._addTextBox(1, 0, 1, 0, _g.d.ap_ar_trans._sale_code, 1, 1, 1, true, false, false, true, true, _g.d.ap_ar_trans._employee);
                    this._addTextBox(1, 1, 1, 0, _g.d.ap_ar_trans._doc_format_code, 1, 1, 0, true, false, true);
                    this._screen_code = "BLC";
                    this._enabedControl(_g.d.ic_trans._doc_format_code, false); ;

                    break;
            }

            // jead : เอา iconNumber=1 มาเพิ่ม Event
            foreach (Control __getControlAll in this.Controls)
            {
                if (__getControlAll.GetType() == typeof(MyLib._myTextBox))
                {
                    MyLib._myTextBox __getControlTextBox = (MyLib._myTextBox)__getControlAll;
                    if (__getControlTextBox._iconNumber == 1)
                    {
                        // กำหนดให้การค้นหาแบบจอเล็กแสดง
                        __getControlTextBox.textBox.Leave -= new EventHandler(textBox_Leave);
                        __getControlTextBox.textBox.Enter -= new EventHandler(textBox_Enter);
                        __getControlTextBox.textBox.KeyDown -= new KeyEventHandler(textBox_KeyDown);
                        //
                        __getControlTextBox.textBox.Leave += new EventHandler(textBox_Leave);
                        __getControlTextBox.textBox.Enter += new EventHandler(textBox_Enter);
                        __getControlTextBox.textBox.KeyDown += new KeyEventHandler(textBox_KeyDown);
                    }
                    else if (__getControlTextBox._iconNumber == 4)
                    {
                        __getControlTextBox.textBox.Leave -= new EventHandler(textBox_Leave);
                        __getControlTextBox.textBox.KeyDown -= new KeyEventHandler(textBox_KeyDown);
                        //
                        __getControlTextBox.textBox.Leave += new EventHandler(textBox_Leave);
                        __getControlTextBox.textBox.KeyDown += new KeyEventHandler(textBox_KeyDown);
                    }
                }
            }


            // วันเครดิต
            MyLib._myTextBox __getControlCreditDay = (MyLib._myTextBox)this._getControl(_g.d.ap_ar_trans._credit_day);
            if (__getControlCreditDay != null)
            {
                __getControlCreditDay.textBox.Leave -= new EventHandler(_creditDayTextBox_Leave);
                //
                __getControlCreditDay.textBox.Leave += new EventHandler(_creditDayTextBox_Leave);
                //__getControlCreditDay.textBox.Enter -= new EventHandler(textBox_Enter);
                //__getControlCreditDay.textBox.Enter += new EventHandler(textBox_Enter);
            }

            // วันที่เครดิต
            MyLib._myTextBox __getControlCreditDate = (MyLib._myTextBox)this._getControl(_g.d.ap_ar_trans._due_date);
            if (__getControlCreditDate != null)
            {
                __getControlCreditDate.textBox.Leave -= new EventHandler(_creditDateTextBox_Leave);
                //
                __getControlCreditDate.textBox.Leave += new EventHandler(_creditDateTextBox_Leave);
            }

            MyLib._myTextBox __getControlDocDate = (MyLib._myTextBox)this._getControl(_g.d.ap_ar_trans._doc_date);
            if (__getControlDocDate != null)
            {
                __getControlDocDate.textBox.Leave -= new EventHandler(textBox_Leave);
                //
                __getControlDocDate.textBox.Leave += new EventHandler(textBox_Leave);
            }
            // ดึงเอกสารอ้างอืง

            this._setControlEnable(_g.d.ap_ar_trans._currency_money, false);
            this._setControlEnable(_g.d.ap_ar_trans._currency_code, false);
            //this._getControl(_g.d.ap_ar_trans._sale_name).Visible = false;
            // jead this._setControlEnable(_g.d.ap_ar_trans._cust_name, false);
            this._textBoxSearch -= new MyLib.TextBoxSearchHandler(_screenApControl__textBoxSearch);
            this._textBoxChanged -= new MyLib.TextBoxChangedHandler(_screenApControl__textBoxChanged);
            //
            this._textBoxSearch += new MyLib.TextBoxSearchHandler(_screenApControl__textBoxSearch);
            this._textBoxChanged += new MyLib.TextBoxChangedHandler(_screenApControl__textBoxChanged);

            this.Dock = DockStyle.Top;
            this.AutoSize = true;
            this.Invalidate();
            this.ResumeLayout();
        }

        void docRefNoTextBox_TextChanged(object sender, EventArgs e)
        {
            MessageBox.Show("ok");
        }

        /// <summary>
        /// หลังป้อนวันเครดิต เพื่อคำนวณหาวันที่
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void _creditDayTextBox_Leave(object sender, EventArgs e)
        {
            _calcCreditDate();
        }

        void _calcCreditDate()
        {
            if (this._processCredit)
            {
                DateTime __dDate = MyLib._myGlobal._convertDate(this._getDataStr(_g.d.ap_ar_trans._doc_date));
                if ((__dDate.Year > 1979))
                {
                    decimal __getDay = this._getDataNumber(_g.d.ap_ar_trans._credit_day);
                    if (__getDay < 0)
                    {
                        __getDay = 0;
                        this._setDataNumber(_g.d.ap_ar_trans._credit_day, __getDay);
                    }
                    else
                    {
                        DateTime __calcDate = __dDate.AddDays((double)__getDay);
                        //_g.g._count_credit_day = __getDay;
                        this._setDataDate(_g.d.ap_ar_trans._due_date, __calcDate);
                    }

                }
            }

        }

        /// <summary>
        /// หลังป้อนวันที่ครบกำหนด เพื่อคำนวณวันที่เครดิต
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void _creditDateTextBox_Leave(object sender, EventArgs e)
        {
            if (this._processCredit)
            {
                DateTime __dDate = MyLib._myGlobal._convertDate(this._getDataStr(_g.d.ap_ar_trans._doc_date));
                string __dateStr = this._getDataStr(_g.d.ap_ar_trans._due_date);
                DateTime __date = MyLib._myGlobal._convertDate(__dateStr);
                if (__date.Year < 1979)
                {
                    this._setDataDate(_g.d.ap_ar_trans._due_date, __dDate);
                }
                if (__dDate.Year > 1979)
                {
                    DateTime __dt = MyLib._myGlobal._convertDate(this._getDataStr(_g.d.ap_ar_trans._due_date));
                    TimeSpan __ts = __dt - __dDate;
                    double __creditDay = Double.Parse(__ts.Days.ToString());
                    if (__creditDay >= 0)
                    {
                        this._setDataNumber(_g.d.ap_ar_trans._credit_day, decimal.Parse(__creditDay.ToString()));

                        //__creditDay = 0;
                        _calcCreditDate();
                    }
                }
            }
        }

        void textBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                if (this._search_data_full_pointer != null && this._search_data_full_pointer.Visible)
                {
                    this._search_data_full_pointer.Focus();
                    this._search_data_full_pointer._firstFocus();
                }
            }
        }

        void __getGroupControl_Leave(object sender, EventArgs e)
        {
            try
            {
                MyLib._myGroupBox __getData = (MyLib._myGroupBox)this._getControl(_g.d.ap_ar_trans._ap_ar_debt_type);
                foreach (Control __getControlInGroupBox in __getData.Controls)
                {
                    if (__getControlInGroupBox.GetType() == typeof(MyLib._myRadioButton))
                    {
                        MyLib._myRadioButton __getRadioButton = (MyLib._myRadioButton)__getControlInGroupBox;
                        __getRadioButton.CheckedChanged -= new EventHandler(__getRadioButton_CheckedChanged);
                        //
                        __getRadioButton.CheckedChanged += new EventHandler(__getRadioButton_CheckedChanged);
                    }
                }
            }
            catch
            {
            }
        }

        void __getRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                MyLib._myRadioButton __e = (MyLib._myRadioButton)sender;
                if (__e.Checked)
                {
                    if (__e.Name.Equals(_g.d.ap_ar_trans._po_purchase))
                    {
                        _g.g.ap_ar_pay_bill = 12;
                    }
                    else if (__e.Name.Equals(_g.d.ap_ar_trans._po_addition_debt))
                    {
                        _g.g.ap_ar_pay_bill = 14;
                    }
                    else if (__e.Name.Equals(_g.d.ap_ar_trans._po_debt_balance))
                    {
                        _g.g.ap_ar_pay_bill = 1;
                    }
                    else if (__e.Name.Equals(_g.d.ap_ar_trans._po_cn_balance))
                    {
                        _g.g.ap_ar_pay_bill = 5;
                    }
                    else if (__e.Name.Equals(_g.d.ap_ar_trans._po_debt_balance_other))
                    {
                        _g.g.ap_ar_pay_bill = 9;
                    }
                    else if (__e.Name.Equals(_g.d.ap_ar_trans._po_cn_balance_other))
                    {
                        _g.g.ap_ar_pay_bill = 11;
                    }
                    //----------------------------------------------------------
                    else if (__e.Name.Equals(_g.d.ap_ar_trans._so_billing))
                    {
                        _g.g.ap_ar_pay_bill = 44;
                    }
                    else if (__e.Name.Equals(_g.d.ap_ar_trans._so_addition_debt))
                    {
                        _g.g.ap_ar_pay_bill = 46;
                    }
                    else if (__e.Name.Equals(_g.d.ap_ar_trans._so_debt_balance))
                    {
                        _g.g.ap_ar_pay_bill = 23;
                    }
                    else if (__e.Name.Equals(_g.d.ap_ar_trans._so_cn_balance))
                    {
                        _g.g.ap_ar_pay_bill = 27;
                    }
                    else if (__e.Name.Equals(_g.d.ap_ar_trans._so_debt_balance_other))
                    {
                        _g.g.ap_ar_pay_bill = 29;
                    }
                    else if (__e.Name.Equals(_g.d.ap_ar_trans._so_cn_balance_other))
                    {
                        _g.g.ap_ar_pay_bill = 33;
                    }
                }
            }
            catch
            {
            }
        }


        void _setControlEnable(string controlName, bool _bool)
        {
            try
            {
                MyLib._myTextBox _getControlName = (MyLib._myTextBox)this._getControl(controlName);
                if (_getControlName != null)
                {
                    _getControlName.Enabled = _bool;
                    _getControlName.TextBox.BackColor = ((_bool == true) ? System.Drawing.Color.White : System.Drawing.Color.WhiteSmoke);
                }
            }
            catch
            {
            }
        }

        void _screenApControl__textBoxChanged(object sender, string name)
        {
            // jead
            MyLib._myTextBox __getControl = (MyLib._myTextBox)((Control)sender).Parent;

            if (__getControl._name.Equals(_g.d.ap_ar_trans._doc_date))
            {
                string __dateStr = this._getDataStr(_g.d.ap_ar_trans._doc_date);
                if (__dateStr.Length == 0)
                {
                    this._setDataDate(_g.d.ap_ar_trans._doc_date, MyLib._myGlobal._workingDate);
                }
                string __dateStrDue = this._getDataStr(_g.d.ap_ar_trans._due_date);
                if (__dateStrDue.Length == 0)
                {
                    this._setDataDate(_g.d.ap_ar_trans._due_date, MyLib._myGlobal._workingDate);
                }
                // วันที่ครบกำหนด ห้ามน้อบกว่าวันที่เอกสาร
                //DateTime __d1 = MyLib._myGlobal._convertDate(this._getDataStr(_g.d.ap_ar_trans._doc_date));
                //DateTime __d2 = MyLib._myGlobal._convertDate(this._getDataStr(_g.d.ap_ar_trans._due_date));
                //if (__d1.CompareTo(__d2) > 0)
                //{
                //this._setDataNumber(_g.d.ap_ar_trans._credit_day, 0.0M);
                //_calcCreditDate();
                // }
            }
            MyLib._myTextBox __textBox = (MyLib._myTextBox)((Control)sender).Parent;
            if ((__textBox._iconNumber == 1 || __textBox._iconNumber == 4))
            {
                // AutoRun
                if (__textBox._name.Equals(_g.d.ic_trans._doc_format_code))
                {
                    string __docNo = this._getDataStr(_g.d.ic_trans._doc_format_code).ToUpper();
                    MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                    DataTable __getFormat = __myFrameWork._queryShort("select " + _g.d.erp_doc_format._format + "," + _g.d.erp_doc_format._code + " from " + _g.d.erp_doc_format._table + " where " + _g.d.erp_doc_format._code + "=\'" + __docNo + "\'").Tables[0];
                    if (__getFormat.Rows.Count > 0)
                    {
                        string __format = __getFormat.Rows[0][0].ToString();
                        this._docFormatCode = __getFormat.Rows[0][1].ToString();
                        this._setDataStr(_g.d.ic_trans._doc_format_code, _docFormatCode, "", true);
                    }
                }
                else
                    if (__textBox._name.Equals(_g.d.ic_trans._doc_no))
                {
                    string __docNo = this._getDataStr(_g.d.ic_trans._doc_no).ToUpper();
                    MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                    DataTable __getFormat = __myFrameWork._queryShort("select " + _g.d.erp_doc_format._format + "," + _g.d.erp_doc_format._code + " from " + _g.d.erp_doc_format._table + " where " + _g.d.erp_doc_format._code + "=\'" + __docNo + "\'").Tables[0];
                    if (__getFormat.Rows.Count > 0)
                    {
                        string __format = __getFormat.Rows[0][0].ToString();
                        this._docFormatCode = __getFormat.Rows[0][1].ToString();

                        string __newDoc = "";
                        if (_transControlType.Equals(_g.g._transControlTypeEnum.เจ้าหนี้_จ่ายชำระหนี้) ||
                            _transControlType.Equals(_g.g._transControlTypeEnum.เจ้าหนี้_จ่ายชำระหนี้_ยกเลิก) ||
                            _transControlType.Equals(_g.g._transControlTypeEnum.ลูกหนี้_รับชำระหนี้) ||
                            _transControlType.Equals(_g.g._transControlTypeEnum.ลูกหนี้_รับชำระหนี้_ยกเลิก) ||
                            _transControlType.Equals(_g.g._transControlTypeEnum.เจ้าหนี้_ใบรับวางบิล) ||
                            _transControlType.Equals(_g.g._transControlTypeEnum.ลูกหนี้_ใบวางบิล) ||
                            _transControlType.Equals(_g.g._transControlTypeEnum.เจ้าหนี้_ใบรับวางบิล_ยกเลิก) ||
                            _transControlType.Equals(_g.g._transControlTypeEnum.ลูกหนี้_ใบวางบิล_ยกเลิก))
                        {
                            __newDoc = _g.g._getAutoRun(_g.g._autoRunType.เจ้าหนี้_ลูกหนี้รายวัน, __docNo, this._getDataStr(_g.d.ap_ar_trans._doc_date).ToString(), __format, _g.g._transControlTypeEnum.ว่าง, this._transControlType);
                        }
                        else
                        {
                            __newDoc = _g.g._getAutoRun(_g.g._autoRunType.ข้อมูลรายวัน, __docNo, this._getDataStr(_g.d.ic_trans._doc_date).ToString(), __format, _g.g._transControlTypeEnum.ว่าง, this._transControlType);
                        }
                        this._setDataStr(_g.d.ic_trans._doc_no, __newDoc, "", true);
                        this._setDataStr(_g.d.ic_trans._doc_format_code, _docFormatCode, "", true);
                    }
                }
                else
                        if (__textBox._name.Equals(_g.d.ap_ar_trans._cust_code))
                {

                    if (MyLib._myGlobal._OEMVersion == ("SINGHA")
                        && (
                        this._transControlType == _g.g._transControlTypeEnum.ลูกหนี้_ตั้งหนี้ยกมา ||
                        this._transControlType == _g.g._transControlTypeEnum.ลูกหนี้_เพิ่มหนี้ยกมา ||
                        this._transControlType == _g.g._transControlTypeEnum.ลูกหนี้_ลดหนี้ยกมา ||

                        this._transControlType == _g.g._transControlTypeEnum.ลูกหนี้_ตั้งหนี้อื่น_ยกเลิก ||
                        this._transControlType == _g.g._transControlTypeEnum.ลูกหนี้_เพิ่มหนี้อื่น_ยกเลิก ||
                        this._transControlType == _g.g._transControlTypeEnum.ลูกหนี้_ลดหนี้อื่น_ยกเลิก ||

                        this._transControlType == _g.g._transControlTypeEnum.ลูกหนี้_รับชำระหนี้ ||
                        this._transControlType == _g.g._transControlTypeEnum.ลูกหนี้_ใบวางบิล ||
                        this._transControlType == _g.g._transControlTypeEnum.ลูกหนี้_ใบวางบิล_ยกเลิก ||
                        this._transControlType == _g.g._transControlTypeEnum.ลูกหนี้_รับชำระหนี้_ยกเลิก))
                    {
                        string __cust_code = this._getDataStr(_g.d.ic_trans._cust_code);
                        MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                        DataTable __custStatusDataTable = __myFrameWork._queryShort("select " + _g.d.ar_customer._status + " from " + _g.d.ar_customer._table + " where " + _g.d.ar_customer._code + "=\'" + __cust_code + "\'").Tables[0];
                        if (__custStatusDataTable.Rows.Count > 0)
                        {
                            int __arStatus = MyLib._myGlobal._intPhase(__custStatusDataTable.Rows[0][_g.d.ar_customer._status].ToString());

                            if (__arStatus == 1)
                            {
                                MessageBox.Show("สถานะ ลูกค้าปิดการใช้งาน");
                                this._setDataStr(__textBox._name, "", "", true);
                                return;
                            }

                        }
                    }



                    this._oldCustCode = __textBox._textFirst;
                    // กรณีมีการแก้ไขรหัส ให้ clear เอกสารอ้างอิง
                    MyLib._myTextBox __docRef = (MyLib._myTextBox)this._getControl(_g.d.ap_ar_trans._doc_ref);
                    if (__docRef != null)
                    {
                        _deleteSearchList(__docRef);
                        this._setDataStr(_g.d.ap_ar_trans._doc_ref, "");
                    }
                }
                this._searchTextBox = (TextBox)sender;
                this._searchName = name;
                this._search(true);
            }
            this._searchTextBox = (TextBox)sender;
            this._searchName = name;
            this._search(true);
        }

        public void _deleteSearchList(MyLib._myTextBox source)
        {
            Boolean __found = false;
            int __addr = 0;
            for (int __loop = 0; __loop < this._search_data_full_buffer_name.Count; __loop++)
            {
                if (source._name.Equals(((string)this._search_data_full_buffer_name[__loop]).ToString()))
                {
                    __addr = __loop;
                    __found = true;
                    break;
                }
            }
            if (__found)
            {
                this._search_data_full_buffer_name.RemoveAt(__addr);
                this._search_data_full_buffer.RemoveAt(__addr);
            }
        }

        void textBox_Leave(object sender, EventArgs e)
        {
            // jead
            if (this._search_data_full_pointer != null)
            {
                this._search_data_full_pointer.Visible = false;
                this._old_filed_name = "";
            }
            //
        }

        void textBox_Enter(object sender, EventArgs e)
        {
            MyLib._myTextBox __getControl = (MyLib._myTextBox)((Control)sender).Parent;
            _screenApControl__textBoxSearch(__getControl);
        }

        void _screenApControl__textBoxSearch(object sender)
        {

            //Debug.Print(Environment.StackTrace.ToString()); 

            this._saveLastControl();
            MyLib._myTextBox __getControl = (MyLib._myTextBox)sender;
            this._searchName = __getControl._name.ToLower();
            string label_name = __getControl._labelName;
            string _search_text_new = _search_screen_neme(this._transControlType, this._searchName);
            if (!this._old_filed_name.Equals(__getControl._name.ToLower()))
            {
                Debug.Print("-- _screenApControl__textBoxSearch  : " + this._screen_code);

                Boolean __found = false;
                this._old_filed_name = __getControl._name.ToLower();
                // jead
                int __addr = 0;
                for (int __loop = 0; __loop < this._search_data_full_buffer_name.Count; __loop++)
                {
                    if (__getControl._name.Equals(((string)this._search_data_full_buffer_name[__loop]).ToString()))
                    {
                        __addr = __loop;
                        __found = true;
                        break;
                    }
                }
                if (__found == false)
                {
                    __addr = this._search_data_full_buffer_name.Add(__getControl._name);
                    MyLib._searchDataFull __searchObject = new MyLib._searchDataFull();
                    if (_searchName.Equals(_g.d.ap_ar_trans._cust_code))
                    {
                        if (!_search_text_new.Equals("screen_ar_customer"))
                        {
                            // ค้นหาเจ้าหนี้
                            __searchObject._dataList._ownerQuery -= new MyLib.OwnerQueryHandler(_dataList__ownerQuery_ap);
                            //
                            __searchObject._dataList._ownerQuery += new MyLib.OwnerQueryHandler(_dataList__ownerQuery_ap);
                            //
                            __searchObject._dataList._addQuotWhere = true;
                        }
                    }
                    if (_searchName.Equals(_g.d.ap_ar_trans._doc_ref))
                    {
                        // ค้นหาเอกสารอ้างอิง
                        // Query เอง โดยให้ไปดึง Process มาแสดง
                        __searchObject._dataList._ownerQuery -= new MyLib.OwnerQueryHandler(_dataList__ownerQuery_doc_ref);
                        switch (this._controlTypeTemp)
                        {
                            case _g.g._transControlTypeEnum.เจ้าหนี้_ใบรับวางบิล_ยกเลิก:
                            case _g.g._transControlTypeEnum.เจ้าหนี้_จ่ายชำระหนี้_ยกเลิก:
                            case _g.g._transControlTypeEnum.ลูกหนี้_ใบวางบิล_ยกเลิก:
                            case _g.g._transControlTypeEnum.ลูกหนี้_รับชำระหนี้_ยกเลิก:
                                __searchObject._dataList._gridData._beforeDisplayRow += new MyLib.BeforeDisplayRowEventHandler(_gridData__beforeDisplayRow);
                                break;
                            default:
                                __searchObject._dataList._ownerQuery += new MyLib.OwnerQueryHandler(_dataList__ownerQuery_doc_ref);
                                __searchObject._dataList._getOrderBy = _g.d.ap_ar_trans._table + "." + _g.d.ap_ar_trans._doc_date + " desc ";
                                break;
                        }
                    }

                    if (_searchName.Equals(_g.d.ap_ar_trans._doc_no))
                    {
                        if (_g.g._companyProfile._branchStatus == 1 && _g.g._companyProfile._change_branch_code == false)
                        {
                            __searchObject._dataList._extraWhere2 = " ((coalesce(" + _g.d.erp_doc_format._use_branch_select + ", 0) = 0 ) or (" + _g.d.erp_doc_format._branch_list + " like '%" + MyLib._myGlobal._branchCode + "%'))";
                        }
                    }
                    if (__getControl._iconNumber == 4)
                    {
                        __searchObject.WindowState = FormWindowState.Maximized;
                    }
                    this._search_data_full_buffer.Add(__searchObject);
                }
                if (this._search_data_full_pointer != null && this._search_data_full_pointer.Visible == true)
                {
                    this._search_data_full_pointer.Visible = false;
                }
                this._search_data_full_pointer = (MyLib._searchDataFull)this._search_data_full_buffer[__addr];
                this._search_data_full_buffer_addr = __addr;
                //
            }
            Debug.Print("aaa  : " + this._screen_code);
            //ค้นหารหัสลูกหนี้
            //ค้นหาหน้าจอ Top
            // jead : เอาออก this._searchTextBox = __getControl.textBox;
            /*this._searchName = ((MyLib._myTextBox)sender)._name.ToLower();
            string label_name = ((MyLib._myTextBox)sender)._labelName;
            string _search_text_new = _search_screen_neme(this._searchName);
            string __where_query = "";*/
            /* ย้ายไปที่ autorun ไม่งั้นมันก็ทำงานตลอดตอนค้นหา มันควรจะอยู่ในช่องของมัน
            if (_searchName.Equals(_g.d.ap_ar_trans._doc_no))
            {
                string _getName = this._getDataStr(_g.d.ap_ar_trans._doc_no).ToUpper();
                if (_getName.Length > 0 && !_autorun_old_doc_no.Equals(_getName))
                {
                    this._getAutoRun();
                    this._setDataStr(_g.d.ap_ar_trans._doc_no, this._autorun_old_doc_no, "", true);
                }

            }
            else if (_searchName.Equals(_g.d.ap_ar_trans._due_date) || _searchName.Equals(_g.d.ap_ar_trans._credit_day))
            {

            }
            else 
            */
            if (_searchName.Equals(_g.d.ap_ar_trans._doc_ref))
            {
                /*this._search_data_full_pointer.Visible = false;
                if (this._search_doc_ref == null)
                {
                    this._search_doc_ref = new MyLib._searchDataFull();
                    this._search_doc_ref._showMode = 0;
                    this._search_doc_ref._name = "ค้นหาเอกสาร";
                    this._search_doc_ref._dataList._ownerQuery += new MyLib.OwnerQueryHandler(_dataList__ownerQuery);
                    this._search_doc_ref._dataList._loadViewFormat(_arapLoadViweGlobal._loadViewName(this._controlTypeTemp, 1), MyLib._myGlobal._userSearchScreenGroup, false);
                    //
                }*/
                // แบบนี้ไม่เอา 
                /*if (!this._getDataStr(_g.d.ap_ar_trans._cust_code).Equals(""))
                {
                    this._doc_ref._is_ap_ar = _arapTransTypeGlobal._transType(this._controlTypeTemp);
                    this._doc_ref._gerwhere = "(" + _g.d.ap_ar_trans._cust_code + " = '" + this._getDataStr(_g.d.ap_ar_trans._cust_code) + "') and "
                   + " (" + _g.d.ap_ar_trans._trans_flag + " = " + (MyLib._myGlobal._intPhase(_arapTransFlagGlobal._transFlag(this._controlTypeTemp).ToString()) - 1) + ") and "
                   + " (" + _g.d.ap_ar_trans._trans_type + " = " + _arapTransTypeGlobal._transType(this._controlTypeTemp) + ")";
                    _doc_ref.ShowDialog();
                    this._setDataStr(_g.d.ap_ar_trans._doc_ref, this._doc_ref._result);
                    // jead : SendKeys.Send("{ENTER}");
                }*/
            }
            if (!this._search_data_full_pointer._name.Equals(_search_text_new.ToLower()))
            {
                /*if (_searchName.Equals(_g.d.ap_ar_trans._cust_code))
                {
                    // ตรสจสอบว่า ถ้า == 0 หมายถึง คลิกค้นหา หน้า ยกเลิกเอการทั้งหมด จะดึงข้อมูลมาจากที่ทำรายการเท่าเท่านั้น
                    if ((MyLib._myGlobal._intPhase(_arapTransFlagGlobal._transFlag(this._controlTypeTemp).ToString()) % 2) == 0)
                    {
                        // 1 = AP , 2 AR
                        if (_arapTransTypeGlobal._transType(this._controlTypeTemp).ToString().Equals("1"))
                        {
                            _search_text_new = _g.g._search_screen_ap;
                            __where_query = " code in (select cust_code from ap_ar_trans  where (" + _g.d.ap_ar_trans._trans_flag + " = " + (MyLib._myGlobal._intPhase(_arapTransFlagGlobal._transFlag(this._controlTypeTemp).ToString()) - 1) + ") and "
                               + " (" + _g.d.ap_ar_trans._trans_type + " = " + _arapTransTypeGlobal._transType(this._controlTypeTemp) + "))";
                        }
                        else
                        {
                            _search_text_new = _g.g._search_screen_ar;
                            __where_query = " code in (select cust_code from ap_ar_trans  where (" + _g.d.ap_ar_trans._trans_flag + " = " + (MyLib._myGlobal._intPhase(_arapTransFlagGlobal._transFlag(this._controlTypeTemp).ToString()) - 1) + ") and "
                               + " (" + _g.d.ap_ar_trans._trans_type + " = " + _arapTransTypeGlobal._transType(this._controlTypeTemp) + "))";
                        }
                    }
                }*/
                // แก้แบบนี้เด้อ
                if (this._search_data_full_pointer._name.Length == 0)
                {
                    Debug.Print("-+- _screenApControl__textBoxSearch  : ");

                    this._search_data_full_pointer._showMode = 0;
                    this._search_data_full_pointer._name = _search_text_new;
                    this._search_data_full_pointer._dataList._loadViewFormat(this._search_data_full_pointer._name, MyLib._myGlobal._userSearchScreenGroup, false);
                    // ถ้าจะเอา event มาแทรกใน function ให้ลบออกก่อนไม่งั้นมันจะวน
                    this._search_data_full_pointer._dataList._gridData._mouseClick -= new MyLib.MouseClickHandler(_gridData__mouseClick);
                    this._search_data_full_pointer._searchEnterKeyPress -= new MyLib.SearchEnterKeyPressEventHandler(_search_data_full__searchEnterKeyPress);
                    //
                    this._search_data_full_pointer._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
                    this._search_data_full_pointer._searchEnterKeyPress += new MyLib.SearchEnterKeyPressEventHandler(_search_data_full__searchEnterKeyPress);
                }
            }
            Debug.Print("sss  : " + this._screen_code);
            // jead ไม่ใช้
            /*if (_searchName.Equals(_g.d.ap_ar_trans._doc_ref))
            {
                __where_query = "(" + _g.d.ap_ar_trans._cust_code + " = '" + this._getDataStr(_g.d.ap_ar_trans._cust_code) + "') and "
                   + " (" + _g.d.ap_ar_trans._trans_flag + " = " + (MyLib._myGlobal._intPhase(_arapTransFlagGlobal._transFlag(this._controlTypeTemp).ToString()) - 1) + ") and "
                   + " (" + _g.d.ap_ar_trans._trans_type + " = " + _arapTransTypeGlobal._transType(this._controlTypeTemp) + ") and "
                   + _g.d.ap_ar_trans._doc_no + " not in (select doc_no from ap_ar_trans where " + " (" + _g.d.ap_ar_trans._trans_flag + " = " + (MyLib._myGlobal._intPhase(_arapTransFlagGlobal._transFlag(this._controlTypeTemp).ToString())) + ") and "
                   + " (" + _g.d.ap_ar_trans._trans_type + " = " + _arapTransTypeGlobal._transType(this._controlTypeTemp) + "))";
            }*/
            // jead : กรณีแสดงแล้วไม่ต้องไปดึงข้อมูลมาอีก ช้า และ เสีย Fucus เดิม
            if (_searchName.Equals(_g.d.ic_trans._doc_no) || _searchName.Equals(_g.d.ic_trans._doc_format_code))
            {
                // auto run
                this._search_data_full_pointer._dataList._extraWhere = MyLib._myGlobal._addUpper(_g.d.erp_doc_format._screen_code) + "=\'" + this._screen_code.ToUpper() + "\'";
            }
            if (_searchName.Equals(_g.d.ic_trans._doc_ref))
            {
                switch (this._controlTypeTemp)
                {
                    case _g.g._transControlTypeEnum.เจ้าหนี้_ใบรับวางบิล_ยกเลิก:
                        this._search_data_full_pointer._dataList._extraWhere = _g.d.ap_ar_trans._cust_code + "=\'" + this._getDataStr(_g.d.ap_ar_trans._cust_code) + "\' and " + _g.d.ap_ar_trans._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เจ้าหนี้_ใบรับวางบิล).ToString();
                        this._search_data_full_pointer._show = false;
                        break;
                    case _g.g._transControlTypeEnum.เจ้าหนี้_จ่ายชำระหนี้_ยกเลิก:
                        this._search_data_full_pointer._dataList._extraWhere = _g.d.ap_ar_trans._cust_code + "=\'" + this._getDataStr(_g.d.ap_ar_trans._cust_code) + "\' and " + _g.d.ap_ar_trans._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.เจ้าหนี้_จ่ายชำระหนี้).ToString();
                        this._search_data_full_pointer._show = false;
                        break;
                    case _g.g._transControlTypeEnum.ลูกหนี้_ใบวางบิล_ยกเลิก:
                        this._search_data_full_pointer._dataList._extraWhere = _g.d.ap_ar_trans._cust_code + "=\'" + this._getDataStr(_g.d.ap_ar_trans._cust_code) + "\' and " + _g.d.ap_ar_trans._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ลูกหนี้_ใบวางบิล).ToString();
                        this._search_data_full_pointer._show = false;
                        break;
                    case _g.g._transControlTypeEnum.ลูกหนี้_รับชำระหนี้_ยกเลิก:
                        this._search_data_full_pointer._dataList._extraWhere = _g.d.ap_ar_trans._cust_code + "=\'" + this._getDataStr(_g.d.ap_ar_trans._cust_code) + "\' and " + _g.d.ap_ar_trans._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(_g.g._transControlTypeEnum.ลูกหนี้_รับชำระหนี้).ToString();
                        this._search_data_full_pointer._show = false;
                        break;
                }
            }

            if (MyLib._myGlobal._OEMVersion.Equals("SINGHA") && _searchName.Equals(_g.d.ap_ar_trans._cust_code) && (this._transControlType == _g.g._transControlTypeEnum.ลูกหนี้_ใบวางบิล ||
                this._transControlType == _g.g._transControlTypeEnum.ลูกหนี้_รับชำระหนี้))
            {
                this._search_data_full_pointer._dataList._extraWhere = _g.d.ar_customer._status + "=0 ";
            }


            if (this._search_data_full_pointer._show == false)
            {
                MyLib._myGlobal._startSearchBox(__getControl, label_name, this._search_data_full_pointer, false, true, this._search_data_full_pointer._dataList._extraWhere);
            }
            else
            {
                MyLib._myGlobal._startSearchBox(__getControl, label_name, this._search_data_full_pointer, false);
            }
            // jead : ทำให้ข้อมูลเรียกใหม่ทุกครั้ง ไม่ต้องมี เพราะ _startSearchBox มันดึงให้แล้ว this._search_data_full_pointer._dataList._refreshData(); เอาออกสองรอบแล้วเด้อ
            if (__getControl._iconNumber == 1)
            {
                __getControl.Focus();
                __getControl.textBox.Focus();
            }
            else
            {
                this._search_data_full_pointer.Focus();
                this._search_data_full_pointer._firstFocus();
            }
        }

        MyLib.BeforeDisplayRowReturn _gridData__beforeDisplayRow(MyLib._myGrid sender, int row, int columnNumber, string columnName, MyLib.BeforeDisplayRowReturn senderRow, MyLib._myGrid._columnType columnType, ArrayList rowData)
        {
            MyLib.BeforeDisplayRowReturn __result = senderRow;
            try
            {
                int __usedStatusColumn = sender._findColumnByName(_g.d.ap_ar_trans._table + "." + _g.d.ap_ar_trans._used_status);
                int __docSuccessColumn = sender._findColumnByName(_g.d.ap_ar_trans._table + "." + _g.d.ap_ar_trans._doc_success);
                int __lastStatusColumn = sender._findColumnByName(_g.d.ap_ar_trans._table + "." + _g.d.ap_ar_trans._last_status);
                if (__usedStatusColumn != -1)
                {
                    // มีการนำไปใช้แล้ว
                    int __status = MyLib._myGlobal._intPhase(sender._cellGet(row, __usedStatusColumn).ToString());
                    if (__status == 1)
                    {
                        __result.newColor = Color.LightSeaGreen;
                    }
                }
                if (__docSuccessColumn != -1)
                {
                    // มีการอ้างอิงครบแล้ว
                    int __status = MyLib._myGlobal._intPhase(sender._cellGet(row, __docSuccessColumn).ToString());
                    if (__status == 1)
                    {
                        __result.newColor = Color.SlateBlue;
                    }
                }
                if (__lastStatusColumn != -1)
                {
                    // เอกสารมีการยกเลิก
                    int __status = MyLib._myGlobal._intPhase(sender._cellGet(row, __lastStatusColumn).ToString());
                    if (__status == 1)
                    {
                        __result.newColor = Color.Red;
                    }
                }
            }
            catch
            {
            }
            return (__result);
        }

        MyLib._queryReturn _dataList__ownerQuery_ap(string where, string orderBy, int recordTop, int searchRecordPerPage)
        {
            SMLERPGlobal._smlFrameWork __smlFrameWork = new SMLERPGlobal._smlFrameWork();
            string __where1 = "";
            return __smlFrameWork._processApStatus(MyLib._myGlobal._databaseName, "", __where1, where, orderBy, 0, recordTop, searchRecordPerPage);
        }

        MyLib._queryReturn _dataList__ownerQuery_doc_ref(string where, string orderBy, int recordTop, int searchRecordPerPage)
        {
            // jead ประเภทต้องไปดูอีกที
            SMLERPGlobal._smlFrameWork __smlFrameWork = new SMLERPGlobal._smlFrameWork();
            string __where1 = " where " + _g.d.ap_ar_trans._table + "." + _g.d.ap_ar_trans._cust_code + "=\'" + this._getDataStr(_g.d.ap_ar_trans._cust_code) + "\'";
            return __smlFrameWork._process_ap_trans(MyLib._myGlobal._databaseName, "", "", __where1, where, "", 0, 1, recordTop, searchRecordPerPage);
        }

        void _search_data_full__searchEnterKeyPress(MyLib._myGrid sender, int row)
        {
            this._searchByParent(sender, row);
        }

        void _gridData__mouseClick(object sender, MyLib.GridCellEventArgs e)
        {
            MyLib._myDataList __getParent1 = (MyLib._myDataList)((MyLib._myGrid)sender).Parent;
            MyLib._searchDataFull __getParent2 = (MyLib._searchDataFull)__getParent1.Parent;
            this._searchAll(__getParent2._name, e._row);
            //SendKeys.Send("{ENTER}");
        }

        void _searchByParent(object sender, int row)
        {
            MyLib._myDataList __getParent1 = (MyLib._myDataList)((MyLib._myGrid)sender).Parent;
            MyLib._searchDataFull __getParent2 = (MyLib._searchDataFull)__getParent1.Parent;
            _searchAll(__getParent2._name, row);
        }

        void _searchAll(string name, int row)
        {
            string _search_text_new = name;
            string __result = "";
            if (name.Length > 0)
            {
                if (name.Equals(_g.g._screen_ap_trans) ||
                    name.Equals(_g.g._screen_ar_trans) ||
                    name.Equals(_g.g._screen_ap_trans_cancel) ||
                    name.Equals(_g.g._screen_ar_trans_cancel) ||
                    name.Equals(_g.g._screen_ap_ar_trans_ref))
                {
                    __result = (string)this._search_data_full_pointer._dataList._gridData._cellGet(row, 1);
                }
                else
                {
                    __result = (string)this._search_data_full_pointer._dataList._gridData._cellGet(row, 0);
                }
                if (__result.Length != 0)
                {
                    this._search_data_full_pointer.Visible = false;
                    //this._setDataStr(_searchName, __result, "", true);
                    this._setDataStr(_searchName, __result);
                    this._search(true);
                }
            }
        }

        public void _search(Boolean warning)
        {
            try
            {
                // ค้นหาชื่อกลุ่มลูกค้า
                StringBuilder __myquery = new StringBuilder();
                __myquery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.erp_doc_group._name_1 + " from " + _g.d.erp_doc_group._table + " where code=\'" + this._getDataStr(_g.d.ap_ar_trans._doc_group).ToUpper() + "\'"));
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.erp_side_list._name_1 + " from " + _g.d.erp_side_list._table + " where code=\'" + this._getDataStr(_g.d.ap_ar_trans._side_code).ToUpper() + "\'"));
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.erp_department_list._name_1 + " from " + _g.d.erp_department_list._table + " where code=\'" + this._getDataStr(_g.d.ap_ar_trans._department_code).ToUpper() + "\'"));
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.erp_project_list._name_1 + " from " + _g.d.erp_project_list._table + " where code=\'" + this._getDataStr(_g.d.ap_ar_trans._project_code).ToUpper() + "\'"));
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.erp_allocate_list._name_1 + " from " + _g.d.erp_allocate_list._table + " where code=\'" + this._getDataStr(_g.d.ap_ar_trans._allocate_code).ToUpper() + "\'"));
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ar_credit_group._name_1 + " from " + _g.d.ar_credit_group._table + " where code=\'" + this._getDataStr(_g.d.ap_ar_trans._credit_code).ToUpper() + "\'"));
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.erp_user._name_1 + " from " + _g.d.erp_user._table + " where code=\'" + this._getDataStr(_g.d.ap_ar_trans._sale_code).ToUpper() + "\'"));
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.erp_job_list._name_1 + " from " + _g.d.erp_job_list._table + " where code=\'" + this._getDataStr(_g.d.ap_ar_trans._job_code).ToUpper() + "\'"));
                if ((_g.g._arapTransTypeGlobal._transType(this._controlTypeTemp) == 1) ||
                    (_g.g._arapTransTypeGlobal._transType(this._controlTypeTemp) == 4))
                {
                    __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ap_supplier._name_1 + " from " + _g.d.ap_supplier._table + " where code=\'" + this._getDataStr(_g.d.ap_ar_trans._cust_code).ToUpper() + "\'"));
                }
                else
                {
                    __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ar_customer._name_1 + " from " + _g.d.ar_customer._table + " where code=\'" + this._getDataStr(_g.d.ap_ar_trans._cust_code).ToUpper() + "\'"));
                }
                __myquery.Append("</node>");
                ArrayList _getData = _myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());
                _searchAndWarning(_g.d.ap_ar_trans._doc_group, (DataSet)_getData[0], warning);
                _searchAndWarning(_g.d.ap_ar_trans._side_code, (DataSet)_getData[1], warning);
                _searchAndWarning(_g.d.ap_ar_trans._department_code, (DataSet)_getData[2], warning);
                _searchAndWarning(_g.d.ap_ar_trans._project_code, (DataSet)_getData[3], warning);
                _searchAndWarning(_g.d.ap_ar_trans._allocate_code, (DataSet)_getData[4], warning);
                _searchAndWarning(_g.d.ap_ar_trans._credit_code, (DataSet)_getData[5], warning);
                _searchAndWarning(_g.d.ap_ar_trans._sale_code, (DataSet)_getData[6], warning);
                _searchAndWarning(_g.d.ap_ar_trans._job_code, (DataSet)_getData[7], warning);
                _searchAndWarning(_g.d.ap_ar_trans._cust_code, (DataSet)_getData[8], warning);
                // ค้นหาเพิ่มเติม
                switch (this._controlTypeTemp)
                {
                    case _g.g._transControlTypeEnum.เจ้าหนี้_ใบรับวางบิล_ยกเลิก:
                    case _g.g._transControlTypeEnum.เจ้าหนี้_จ่ายชำระหนี้_ยกเลิก:
                    case _g.g._transControlTypeEnum.ลูกหนี้_ใบวางบิล_ยกเลิก:
                    case _g.g._transControlTypeEnum.ลูกหนี้_รับชำระหนี้_ยกเลิก:
                        {
                            string __query = "select " + _g.d.ap_ar_trans._doc_date + "," + _g.d.ap_ar_trans._last_status + " from " + _g.d.ap_ar_trans._table + " where " + _g.d.ap_ar_trans._doc_no + "=\'" + this._getDataStr(_g.d.ap_ar_trans._doc_ref) + "\'";
                            DataTable __getData = _myFrameWork._queryShort(__query).Tables[0];
                            string __getDateRef = "";
                            if (__getData.Rows.Count > 0)
                            {
                                int __getLastStatus = MyLib._myGlobal._intPhase(__getData.Rows[0][_g.d.ap_ar_trans._last_status].ToString());
                                if (this._managerData._mode == 1 && __getLastStatus == 1)
                                {
                                    MessageBox.Show(MyLib._myGlobal._resource("เอกสารไม่สามารถนำมายกเลิกได้อีก"));
                                    this._setDataStr(_g.d.ap_ar_trans._doc_ref, "");
                                }
                                else
                                {
                                    __getDateRef = __getData.Rows[0][_g.d.ap_ar_trans._doc_date].ToString();
                                }
                            }
                            this._setDataDate(_g.d.ap_ar_trans._doc_ref_date, MyLib._myGlobal._convertDate(__getDateRef));
                        }
                        break;
                }
            }
            catch
            {
            }
        }

        bool _searchAndWarning(string fieldName, DataSet dataResult, Boolean warning)
        {
            bool __result = false;
            if (dataResult.Tables[0].Rows.Count > 0)
            {
                string __getData = dataResult.Tables[0].Rows[0][0].ToString();
                string __getDataStr = this._getDataStr(fieldName);
                this._setDataStr(fieldName, __getDataStr, __getData, true); // jead เพิ่มให้
                if (fieldName.Equals(_g.d.ap_ar_trans._cust_code))
                {
                    Debug.Print("searchAndWarning  : ");
                    //this._setDataStr(fieldName, __getDataStr);
                    // jead this._setDataStr(_g.d.ap_ar_trans._cust_name, __getData);
                    if (!this._old_cust_code.Equals(__getDataStr))
                    {
                        this._old_cust_code = __getDataStr;
                        //MyLib._myTextBox __getControl_currency_money = (MyLib._myTextBox)this._getControl(_g.d.ap_ar_trans._currency_money);
                        //if (__getControl_currency_money != null)
                        //{
                        //    this._setDataNumber(_g.d.ap_ar_trans._currency_money, 0.00);
                        //}
                        //MyLib._myTextBox __getControltotal_debt_amount = (MyLib._myTextBox)this._getControl(_g.d.ap_ar_trans._total_amount);
                        //if (__getControltotal_debt_amount != null)
                        //{
                        //    this._setDataNumber(_g.d.ap_ar_trans._total_amount, 0.00);
                        //}
                        MyLib._myTextBox __getControl = (MyLib._myTextBox)this._getControl(_g.d.ap_ar_trans._currency_code);
                        if (__getControl != null)
                        {
                            string _getCurrency = "select " + _g.d.ap_supplier_detail._currency_code + " from " + _g.d.ap_supplier_detail._table + " where ap_code=\'" + this._getDataStr(_g.d.ap_ar_trans._cust_code).ToUpper() + "\'";
                            DataSet _getData = _myFrameWork._query(MyLib._myGlobal._databaseName, _getCurrency.ToString());
                            string __getDB = _getData.Tables[0].Rows[0][0].ToString();
                            this._setDataStr(_g.d.ap_ar_trans._currency_code, __getDB);
                            if (__getDB.Length == 0)
                            {
                                this._setControlEnable(_g.d.ap_ar_trans._currency_money, false);
                            }
                            else
                            {
                                this._setControlEnable(_g.d.ap_ar_trans._currency_money, true);
                            }
                        }
                    }
                }
                /* jead else if (fieldName.Equals(_g.d.ap_ar_trans._sale_code))
                {
                    this._setDataStr(fieldName, __getDataStr);
                    this._setDataStr(_g.d.ap_ar_trans._sale_name, __getData);
                }*/
                else
                {
                    this._setDataStr(fieldName, __getDataStr, __getData, true);
                }
            }
            else
            {
                if (this._searchTextBox != null)
                {
                    if (this._searchName.CompareTo(fieldName) == 0 && this._getDataStr(fieldName) != "")
                    {
                        if (dataResult.Tables[0].Rows.Count == 0 && warning)
                        {
                            MyLib._myTextBox __getTextBox = (MyLib._myTextBox)(MyLib._myTextBox)this._searchTextBox.Parent;
                            MessageBox.Show(MyLib._myGlobal._resource("ไม่พบ") + " : " + __getTextBox._labelName, MyLib._myGlobal._resource("เตือน"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                            __getTextBox._textFirst = "";
                            __getTextBox._textSecond = "";
                            __getTextBox._textLast = "";
                            this._setDataStr(fieldName, "", "", true);
                            __getTextBox.Focus();
                            __getTextBox.textBox.Focus();
                            __result = true;
                        }
                    }
                }
            }
            return __result;
        }

        string _search_screen_neme(_g.g._transControlTypeEnum mode, string _name)
        {
            if (_name.Equals(_g.d.ap_ar_trans._doc_no) || _name.Equals(_g.d.ap_ar_trans._doc_format_code)) return _g.g._search_screen_erp_doc_format;
            if (_name.Equals(_g.d.ap_ar_trans._doc_group)) return _g.g._search_screen_erp_doc_group;
            if (_name.Equals(_g.d.ap_ar_trans._cust_code)) return _geteSarchViewName();
            if (_name.Equals(_g.d.ap_ar_trans._project_code)) return _g.g._search_master_erp_project_list;
            if (_name.Equals(_g.d.ap_ar_trans._allocate_code)) return _g.g._search_master_erp_allocate_list;
            if (_name.Equals(_g.d.ap_ar_trans._job_code)) return _g.g._search_master_erp_job_list;
            if (_name.Equals(_g.d.ap_ar_trans._side_code)) return _g.g._search_screen_erp_side_list;
            if (_name.Equals(_g.d.ap_ar_trans._department_code)) return _g.g._search_screen_erp_department_list;
            if (_name.Equals(_g.d.ap_ar_trans._sale_area_code)) return _g.g._search_master_ar_area_code;
            if (_name.Equals(_g.d.ap_ar_trans._sale_code)) return _g.g._search_screen_erp_user;
            if (_name.Equals(_g.d.ap_ar_trans._credit_code)) return _g.g._search_screen_erp_credit_type;
            if (_name.Equals(_g.d.ap_ar_trans._currency_code)) return _g.g._search_screen_erp_currency;
            switch (mode)
            {
                case _g.g._transControlTypeEnum.เจ้าหนี้_จ่ายชำระหนี้_ยกเลิก: return _g.g._arapLoadViewGlobal._loadViewName(_g.g._transControlTypeEnum.เจ้าหนี้_จ่ายชำระหนี้_ยกเลิก, 1);
            }
            if ((MyLib._myGlobal._intPhase(_g.g._transFlagGlobal._transFlag(this._controlTypeTemp).ToString()) % 2) == 0)
            {
                // jead ให้ไปดึงอีกอัน ค่าสุดท้ายเป็น 1
                if (_name.Equals(_g.d.ap_ar_trans._doc_ref)) return _g.g._arapLoadViewGlobal._loadViewName(_g.g._transControlTypeEnum.เจ้าหนี้_เตรียมจ่าย_อนุมัติ, 1);
            }
            else
            {
                // jead ให้ไปดึงอีกอัน
                if (_name.Equals(_g.d.ap_ar_trans._doc_ref)) return _g.g._arapLoadViewGlobal._loadViewName(_g.g._transControlTypeEnum.ลูกหนี้_ใบวางบิล_ยกเลิก, 1);
            }
            return "";
        }

        private string _geteSarchViewName()
        {
            if (_g.g._arapTransTypeGlobal._transType(this._transControlType).Equals(4) ||
                _g.g._arapTransTypeGlobal._transType(this._transControlType).Equals(1))
            {
                return _g.g._screen_ap_supplier_search;
            }
            else
            {
                return _g.g._search_screen_ar;
            }
        }
    }
}
