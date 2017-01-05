using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Net;
using System.Text.RegularExpressions;

namespace SMLICIAdmin
{
    public partial class _mainForm : SMLERPTemplate._templateMainForm
    {
        public _mainForm()
        {
            InitializeComponent();
            this.Resize += new EventHandler(_mainForm_Resize);
            this.Load += new EventHandler(_mainForm_Load);
            this._programName = "SML Color Admin";
            this._mainMenu = this._mainMenuERP;
            this._menuImageList = this._menuImage;
            MyLib._myGlobal._listMenuAll = this.__getMenuListAll();
            this.Disposed += new EventHandler(_mainForm_Disposed);
            //
            MyLib._myGlobal._tableForAutoUnlock.Add(_g.d.erp_branch_list._table);
            MyLib._myGlobal._tableForAutoUnlock.Add(_g.d.gl_chart_of_account._table);
            MyLib._myGlobal._tableForAutoUnlock.Add(_g.d.gl_journal._table);
            MyLib._myGlobal._tableForAutoUnlock.Add(_g.d.ic_inventory._table);
            MyLib._myGlobal._tableForAutoUnlock.Add(_g.d.ap_supplier._table);
            MyLib._myGlobal._tableForAutoUnlock.Add(_g.d.ar_customer._table);
            MyLib._myGlobal._tableForAutoUnlock.Add(_g.d.ar_customer._table);
            MyLib._myGlobal._tableForAutoUnlock.Add(_g.d.as_asset._table);
            MyLib._myGlobal._tableForAutoUnlock.Add(_g.d.as_asset_maintenance._table);
            MyLib._myGlobal._tableForAutoUnlock.Add(_g.d.as_asset_sale._table);
            MyLib._myGlobal._tableForAutoUnlock.Add(_g.d.erp_pass_book._table);
            MyLib._myGlobal._tableForAutoUnlock.Add(_g.d.erp_province._table);
            MyLib._myGlobal._tableForAutoUnlock.Add(_g.d.erp_amper._table);
            MyLib._myGlobal._tableForAutoUnlock.Add(_g.d.erp_tambon._table);
            MyLib._myGlobal._tableForAutoUnlock.Add(_g.d.ap_ar_trans._table);
            MyLib._myGlobal._tableForAutoUnlock.Add(_g.d.ic_trans._table);
            /* ----------
            MyLib._myGlobal._tableForAutoUnlock.Add(_g.d.po_purchase_request._table);
            MyLib._myGlobal._tableForAutoUnlock.Add(_g.d.po_so_deposit_money._table);
            MyLib._myGlobal._tableForAutoUnlock.Add(_g.d.po_so_credit_note._table);
            MyLib._myGlobal._tableForAutoUnlock.Add(_g.d.po_so_inquiry._table);
            MyLib._myGlobal._tableForAutoUnlock.Add(_g.d.po_so_invoice._table);
            //cash & bank
            MyLib._myGlobal._tableForAutoUnlock.Add(_g.d.cb_bank_income._table);
            MyLib._myGlobal._tableForAutoUnlock.Add(_g.d.cb_bank_income_detail._table);
            MyLib._myGlobal._tableForAutoUnlock.Add(_g.d.cb_cash_trans._table);
            MyLib._myGlobal._tableForAutoUnlock.Add(_g.d.cb_chq_list._table);
            MyLib._myGlobal._tableForAutoUnlock.Add(_g.d.cb_chq_trans._table);
            MyLib._myGlobal._tableForAutoUnlock.Add(_g.d.cb_chq_trans_detail._table);
            MyLib._myGlobal._tableForAutoUnlock.Add(_g.d.cb_credit_card._table);
            MyLib._myGlobal._tableForAutoUnlock.Add(_g.d.cb_credit_card_trans._table);
            MyLib._myGlobal._tableForAutoUnlock.Add(_g.d.cb_credit_card_trans_detail._table);
            MyLib._myGlobal._tableForAutoUnlock.Add(_g.d.cb_other_payment._table);
            MyLib._myGlobal._tableForAutoUnlock.Add(_g.d.cb_petty_cash._table);
            MyLib._myGlobal._tableForAutoUnlock.Add(_g.d.cb_petty_cash_trans._table);
            */
        }

        void _mainForm_Disposed(object sender, EventArgs e)
        {
            try
            {
                this.__process.Abort();
            }
            catch
            {
            }
        }

        Thread __process;

        void _mainForm_Load(object sender, EventArgs e)
        {
            MyLib._myGlobal._mainSize = this.Size;
            //MyLib._myUtil._showTaskBarNotifier(this, "", "Welcome to SMLERP");
            // Process
            // this.__process = new Thread(_process);
            //  this.__process.Start();
            //MOOOOOOOOO
            //---------------------------------------------
            this._getCompanyName();
            //---------------------------------------------
        }

        private void _getCompanyName()
        {
            ThreadStart theprogress = new ThreadStart(_getDataCompany);
            Thread startprogress = new Thread(theprogress);
            startprogress.Priority = ThreadPriority.Highest;
            startprogress.IsBackground = true;
            startprogress.Start();
        }

        // MOOOOOOOOOO ดึงซื่อ ที่อยู่ บริษัท มาเก็บไว้ 
        protected void _getDataCompany()
        {
            try
            {
                MyLib._myFrameWork ___myFrameWork = new MyLib._myFrameWork();
                DataSet __getLastCode = ___myFrameWork._query(MyLib._myGlobal._databaseName, "select company_name_1, address_1, telephone_number, fax_number, tax_number from  erp_company_profile");
                DataRow[] _dr = __getLastCode.Tables[0].Select(string.Empty);
                MyLib._myGlobal._ltdName = "";
                MyLib._myGlobal._ltdAddress = "";
                MyLib._myGlobal._ltdTel = "";
                MyLib._myGlobal._ltdFax = "";
                foreach (DataRow _row in _dr)
                {
                    string[] xx = _row["address_1"].ToString().Split('\n');
                    string str = "";
                    for (int i = 0; i < xx.Length; i++)
                    {
                        str += xx[i].ToString();
                    }
                    MyLib._myGlobal._ltdName = (_row["company_name_1"].ToString());
                    MyLib._myGlobal._ltdAddress = (str);
                    MyLib._myGlobal._ltdTel = (_row["telephone_number"].ToString());
                    MyLib._myGlobal._ltdFax = (_row["fax_number"].ToString());

                }
            }
            catch (Exception ex)
            {
            }
        }




        void _mainForm_Resize(object sender, EventArgs e)
        {
            MyLib._myGlobal._mainSize = this.Size;
        }

        void _selectMenuFlow(string menuName, string screenName)
        {
            bool __selectTabFound = _selectTab(menuName);
            if (__selectTabFound == false)
            {
                Control __getControl = SMLERPControl._flow._selectMenu._getObject(menuName);
                if (__getControl != null)
                {
                    _createAndSelectTab(menuName, menuName, screenName, __getControl);
                }
            }
        }

        void _selectMenuConfig(string menuName, string screenName)
        {
            bool __selectTabFound = _selectTab(menuName);
            if (__selectTabFound == false)
            {
                Control __getControl = SMLERPConfig._selectMenu._getObject(menuName, screenName);
                if (__getControl != null)
                {
                    _createAndSelectTab(menuName, menuName, screenName, __getControl);
                }
            }
        }

        public override void _activeMenu(string mainMenuId, string menuName, string tag)
        {
            if (this.DesignMode == false)
            {
                // start
                string __screenName = MyLib._myResource._findResource(menuName, menuName)._str;
                bool __selectTabFound = _selectTab(menuName);
                if (__selectTabFound == false)
                {
                    //  Control __getControl = _selectMenu._getObject(menuName, __screenName, _programName);
                    Control __getControl = _selectMenu._getObject(menuName, __screenName);
                    if (__getControl != null)
                    {
                        //  _g.g._companyProfileLoad();
                        _createAndSelectTab(menuName, menuName, __screenName, __getControl);
                    }
                }
            }
        }
    }
}
