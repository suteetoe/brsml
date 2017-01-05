using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLERPConfig
{
    public partial class _dashboardConfigScreen : UserControl
    {
        int _mode = 0;

        public _dashboardConfigScreen()
        {
            InitializeComponent();

            this._screen._table_name = _g.d.erp_option._table;
            this._screen._maxColumn = 1;
            this._screen._addTextBox(1, 0, _g.d.erp_option._mis_shop_code, 0);
            this._screen._addTextBox(2, 0, 1, 1, _g.d.erp_option._mis_shop_password, 1, 10, 0, true, true);
            this._screen._addTextBox(3, 0, 1, 1, _g.d.erp_option._mis_shop_confirm, 1, 10, 0, true, true);
            //this._screen._addTextBox(4, 0, _g.d.erp_option._mis_shop_name, 0);

            this._screen._enabedControl(_g.d.erp_option._mis_shop_code, false);
            //this._screen._enabedControl(_g.d.erp_option._mis_shop_name, false);
            this._screen._enabedControl(_g.d.erp_option._mis_shop_password, false);
            this._screen._enabedControl(_g.d.erp_option._mis_shop_confirm, false);

            if (MyLib._myGlobal._isDesignMode == false)
            {
                _loadDashboardUser();
            }
        }

        void _reset()
        {
            this._screen._clear();
            this._screen._enabedControl(_g.d.erp_option._mis_shop_code, false);
            //this._screen._enabedControl(_g.d.erp_option._mis_shop_name, false);
            this._screen._enabedControl(_g.d.erp_option._mis_shop_password, false);
            this._screen._enabedControl(_g.d.erp_option._mis_shop_confirm, false);
            this._checkShopIDButton.Visible =
                this._loginShopButton.Visible =
                this._registerButton.Visible =
                this._saveEditPassword.Visible = false;
        }

        void _loadDashboardUser()
        {
            if (_checkConnectSMLServer())
            {
                try
                {
                    MyLib._myFrameWork __smlFrameWork = new MyLib._myFrameWork(MyLib._myGlobal._masterWebservice, MyLib._myGlobal._masterConfigXmlName, MyLib._myGlobal._databaseType.PostgreSql);
                    DataTable __result = __smlFrameWork._query(MyLib._myGlobal._masterRegisterDatabaseName, "select user_code from sml_dashboard_group where product_code = '" + MyLib._myGlobal._productCode + "' and database_name = '" + MyLib._myGlobal._databaseName + "' ").Tables[0];
                    if (__result.Rows.Count > 0)
                    {
                        this._screen._setDataStr(_g.d.erp_option._mis_shop_code, __result.Rows[0][0].ToString());
                        this._editPasswordButton.Visible = true;
                    }
                }
                catch
                {
                    // server connect problem
                }

            }
        }

        private void _registerButton_Click(object sender, EventArgs e)
        {
            if (_checkConnectSMLServer())
            {
                // check product code
                if (MyLib._myGlobal._productCode.Equals("FREE"))
                {
                    MessageBox.Show("กรุณาลงทะเบียนผลิตภัณฑ์ก่อน จึงจะสามารถลงทะเบียนได้");
                    return;

                }

                MyLib._myFrameWork __clientFrameWork = new MyLib._myFrameWork();
                string __getPort = __clientFrameWork._getPortURL();

                string __getUserCode = this._screen._getDataStr(_g.d.erp_option._mis_shop_code);
                string __getUserPassword = this._screen._getDataStr(_g.d.erp_option._mis_shop_password);
                string __getUserConfirmPassword = this._screen._getDataStr(_g.d.erp_option._mis_shop_confirm);
                //string __getShopName = this._screen._getDataStr(_g.d.erp_option._mis_shop_name);

                //if (__getShopName.Trim().Length == 0)
                //{
                //    MessageBox.Show("กรุณาป้อนชื่อร้านค้า");
                //    return;
                //}

                StringBuilder __queryList = new StringBuilder();

                // save 
                if (_mode == 1)
                {
                    if (__getUserConfirmPassword.Length == 0 || __getUserConfirmPassword.Equals(__getUserPassword) == false)
                    {
                        MessageBox.Show("รหัสผ่านไม่สัมพันธ์กัน", "ผิดพลาด");
                        return;
                    }

                    __queryList.Append(MyLib._myGlobal._xmlHeader + "<node>");
                    __queryList.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into sml_dashboard_user (user_code, user_password) values(\'" + __getUserCode + "\', \'" + __getUserPassword + "\')"));
                    __queryList.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into sml_dashboard_group (user_code, product_code, provider_code, database_name, server_port) values(\'" + __getUserCode + "\', \'" + MyLib._myGlobal._productCode + "\', \'" + MyLib._myGlobal._providerCode.ToUpper() + "\', \'" + MyLib._myGlobal._databaseName + "\', \'" + __getPort + "\')"));
                    __queryList.Append("</node>");

                }
                else if (_mode == 2)
                {
                    MyLib._myFrameWork __smlFrameWork = new MyLib._myFrameWork(MyLib._myGlobal._masterWebservice, MyLib._myGlobal._masterConfigXmlName, MyLib._myGlobal._databaseType.PostgreSql);
                    __queryList.Append(MyLib._myGlobal._xmlHeader + "<node>");
                    __queryList.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into sml_dashboard_group (user_code, product_code, provider_code, database_name, server_port) values(\'" + __getUserCode + "\', \'" + MyLib._myGlobal._productCode + "\', \'" + MyLib._myGlobal._providerCode.ToUpper() + "\', \'" + MyLib._myGlobal._databaseName + "\', \'" + __getPort + "\')"));
                    __queryList.Append("</node>");

                }

                if (__queryList.Length > 0)
                {
                    MyLib._myFrameWork __smlFrameWork = new MyLib._myFrameWork(MyLib._myGlobal._masterWebservice, MyLib._myGlobal._masterConfigXmlName, MyLib._myGlobal._databaseType.PostgreSql);
                    string __queryResult = __smlFrameWork._queryList(MyLib._myGlobal._masterRegisterDatabaseName, __queryList.ToString());

                    if (__queryResult.Length == 0)
                    {
                        MessageBox.Show("บันทึกการลงทะเบียนสำเร็จ");
                    }
                    else
                    {
                        MessageBox.Show(__queryResult.ToString(), "error");
                    }
                }

            }

        }

        private void _registerNewShopButton_Click(object sender, EventArgs e)
        {
            this._reset();
            if (_checkRegisterDatabaseSuccess())
            {
                MessageBox.Show("มีการลงทะเบียนไปแล้ว");
                return;
            }

            if (_checkConnectSMLServer())
            {
                this._mode = 1;
                this._screen._enabedControl(_g.d.erp_option._mis_shop_code, true);
                this._screen._refresh();
                this._checkShopIDButton.Visible = true;
            }

        }

        private void _checkShopIDButton_Click(object sender, EventArgs e)
        {
            bool __pass = false;

            string __getUserCode = this._screen._getDataStr(_g.d.erp_option._mis_shop_code);
            if (__getUserCode.Length == 0)
            {
                MessageBox.Show("กรุณาป้อนรหัสร้านค้า");
                return;
            }


            if (_checkConnectSMLServer())
            {
                MyLib._myFrameWork __smlFrameWork = new MyLib._myFrameWork(MyLib._myGlobal._masterWebservice, MyLib._myGlobal._masterConfigXmlName, MyLib._myGlobal._databaseType.PostgreSql);
                DataTable __result = __smlFrameWork._query(MyLib._myGlobal._masterRegisterDatabaseName, "select count(user_code) as xCount from sml_dashboard_user where user_code = '" + this._screen._getDataStr(_g.d.erp_option._mis_shop_code) + "' ").Tables[0];
                if (__result.Rows.Count > 0 && MyLib._myGlobal._decimalPhase(__result.Rows[0][0].ToString()) == 0M)
                {
                    MessageBox.Show(MyLib._myGlobal._resource("คุณสามารถใช้งาน รหัสร้านค้า") + " : " + __getUserCode + " " + MyLib._myGlobal._resource("ได้"), "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this._screen._enabedControl(_g.d.erp_option._mis_shop_code, false);
                    __pass = true;
                }

                if (__pass)
                {
                    //this._screen._enabedControl(_g.d.erp_option._mis_shop_name, true);
                    this._screen._enabedControl(_g.d.erp_option._mis_shop_password, true);
                    this._screen._enabedControl(_g.d.erp_option._mis_shop_confirm, true);
                    this._screen._refresh();
                    this._registerButton.Visible = true;

                }
            }
        }

        private void _loginShopButton_Click(object sender, EventArgs e)
        {
            bool __pass = false;

            MyLib._myFrameWork __smlFrameWork = new MyLib._myFrameWork(MyLib._myGlobal._masterWebservice, MyLib._myGlobal._masterConfigXmlName, MyLib._myGlobal._databaseType.PostgreSql);
            DataTable __result = __smlFrameWork._query(MyLib._myGlobal._masterRegisterDatabaseName, "select user_code, user_password from sml_dashboard_user where user_code = '" + this._screen._getDataStr(_g.d.erp_option._mis_shop_code) + "' and user_password = \'" + this._screen._getDataStr(_g.d.erp_option._mis_shop_password) + "\' ").Tables[0];
            if (__result.Rows.Count > 0)
            {
                string __getUserPassword = __result.Rows[0]["user_password"].ToString();

                if (this._screen._getDataStr(_g.d.erp_option._mis_shop_password).Equals(__getUserPassword))
                {
                    this._screen._enabedControl(_g.d.erp_option._mis_shop_code, false);
                    this._screen._refresh();
                    __pass = true;
                }
            }

            if (__pass == false)
            {
                MessageBox.Show("ไม่พบ รหัสร้านค้าและรหัสผ่านดังกล่าว กรุณาลองใหม่อีกครั้ง", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            if (__pass)
            {
                //this._screen._enabedControl(_g.d.erp_option._mis_shop_name, true);
                //this._screen._refresh();
                //this._registerButton.Visible = true;
                if (this._mode == 3)
                {
                    MessageBox.Show("เข้าสู่ระบบสำเร็จ กรุณาป้อนรหัสผ่านใหม่ ในช่องรหัสผ่านและยืนยันรหัสผ่านอีกครั้ง", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this._screen._enabedControl(_g.d.erp_option._mis_shop_password, true);
                    this._screen._enabedControl(_g.d.erp_option._mis_shop_confirm, true);

                    this._screen._setDataStr(_g.d.erp_option._mis_shop_password, "");
                    this._screen._setDataStr(_g.d.erp_option._mis_shop_confirm, "");
                    this._saveEditPassword.Visible = true;
                    this._loginShopButton.Visible = false;

                }
                else
                {
                    MessageBox.Show("เข้าสู่ระบบสำเร็จ", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this._registerButton.Visible = true;
                }
            }
        }

        private void _registerOldShowButton_Click(object sender, EventArgs e)
        {
            this._reset();
            if (_checkRegisterDatabaseSuccess())
            {
                MessageBox.Show("มีการลงทะเบียนไปแล้ว");
                return;
            }

            if (_checkConnectSMLServer())
            {
                this._mode = 2;
                this._screen._enabedControl(_g.d.erp_option._mis_shop_code, true);
                this._screen._enabedControl(_g.d.erp_option._mis_shop_password, true);
                //this._screen._enabedControl(_g.d.erp_option._mis_shop_confirm, true);
                this._screen._refresh();
                this._loginShopButton.Visible = true;
            }

        }

        private bool _checkConnectSMLServer()
        {
            bool __result = false;

            try
            {
                if (MyLib._myGlobal._checkConnectionAvailable(MyLib._myGlobal._masterWebservice))
                {
                    MyLib._myFrameWork __smlFrameWork = new MyLib._myFrameWork(MyLib._myGlobal._masterWebservice, MyLib._myGlobal._masterConfigXmlName, MyLib._myGlobal._databaseType.PostgreSql);
                    __result = __smlFrameWork._testConnect();
                    if (__result == false)
                    {
                        MessageBox.Show(MyLib._myGlobal._resource("ไม่สามารถเชื่อมต่อไปยัง www.smlsoft.com ได้ กรุณาตรวจสอบการเชื่อมต่ออินเตอร์เน็ต"), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show(MyLib._myGlobal._resource("ไม่สามารถเชื่อมต่อไปยัง www.smlsoft.com ได้ กรุณาตรวจสอบการเชื่อมต่ออินเตอร์เน็ต"), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch
            {
            }


            return __result;

        }

        private bool _checkRegisterDatabaseSuccess()
        {
            bool __result = false;

            if (_checkConnectSMLServer())
            {
                MyLib._myFrameWork __smlFrameWork = new MyLib._myFrameWork(MyLib._myGlobal._masterWebservice, MyLib._myGlobal._masterConfigXmlName, MyLib._myGlobal._databaseType.PostgreSql);
                DataTable __queryResult = __smlFrameWork._query(MyLib._myGlobal._masterRegisterDatabaseName, "select count(user_code) as xCount from sml_dashboard_group where product_code = '" + MyLib._myGlobal._productCode + "' and database_name = '" + MyLib._myGlobal._databaseName + "' ").Tables[0];
                if (__queryResult.Rows.Count > 0 && MyLib._myGlobal._decimalPhase(__queryResult.Rows[0][0].ToString()) > 0M)
                {
                    return true;
                }
            }

            return __result;
        }

        private void _editPasswordButton_Click(object sender, EventArgs e)
        {
            this._mode = 3;
            this._reset();
            this._loadDashboardUser();
            this._screen._enabedControl(_g.d.erp_option._mis_shop_password, true);
            //this._screen._enabedControl(_g.d.erp_option._mis_shop_confirm, true);
            this._screen._refresh();
            this._loginShopButton.Visible = true;

        }

        private void _saveEditPassword_Click(object sender, EventArgs e)
        {
            string __getUserCode = this._screen._getDataStr(_g.d.erp_option._mis_shop_code);
            string __getUserPassword = this._screen._getDataStr(_g.d.erp_option._mis_shop_password);
            string __getUserConfirmPassword = this._screen._getDataStr(_g.d.erp_option._mis_shop_confirm);

            StringBuilder __queryList = new StringBuilder();

            // save 
            if (__getUserConfirmPassword.Length == 0 || __getUserConfirmPassword.Equals(__getUserPassword) == false)
            {
                MessageBox.Show("รหัสผ่านไม่สัมพันธ์กัน", "ผิดพลาด");
                return;
            }

            __queryList.Append(MyLib._myGlobal._xmlHeader + "<node>");
            __queryList.Append(MyLib._myUtil._convertTextToXmlForQuery("update sml_dashboard_user set  user_password = \'" + __getUserPassword + "\' where user_code = \'" + __getUserCode + "\'  "));
            __queryList.Append("</node>");


            if (__queryList.Length > 0)
            {
                MyLib._myFrameWork __smlFrameWork = new MyLib._myFrameWork(MyLib._myGlobal._masterWebservice, MyLib._myGlobal._masterConfigXmlName, MyLib._myGlobal._databaseType.PostgreSql);
                string __queryResult = __smlFrameWork._queryList(MyLib._myGlobal._masterRegisterDatabaseName, __queryList.ToString());

                if (__queryResult.Length == 0)
                {
                    MessageBox.Show("บันทึกสำเร็จ");
                }
                else
                {
                    MessageBox.Show(__queryResult.ToString(), "error");
                }
            }

        }
    }
}
