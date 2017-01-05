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
using MyLib;
using System.Xml.Serialization;

namespace SMLOffTakeSalesAdmin
{
    public partial class _mainScreen : SMLERPTemplate._templateMainForm
    {

        public _mainScreen()
        {
            InitializeComponent();
            this.Resize += new EventHandler(_mainForm_Resize);
            this.Load += new EventHandler(_mainForm_Load);
            this._programName = "OffTakeSalesAdmin";
            this._mainMenu = this._mainMenuERP;
            this._menuImageList = this._menuImage;
            MyLib._myGlobal._listMenuAll = this.__getMenuListAll();
            this.Disposed += new EventHandler(_mainForm_Disposed);
            //
           
        }
        void _mainForm_Disposed(object sender, EventArgs e)
        {
            for (int __loop = 0; __loop < this._threadList.Count; __loop++)
            {
                try
                {
                    ((Thread)this._threadList[__loop]).Abort();
                    //this.__process.Abort();
                }
                catch
                {
                }
            }
        }

        public ArrayList _threadList = new ArrayList();

        void _mainForm_Load(object sender, EventArgs e)
        {
            MyLib._myGlobal._mainSize = this.Size;

            this._getCompanyName();

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
                MyLib._myGlobal._ltdTax = "";
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
                    MyLib._myGlobal._ltdTax = (_row["tax_number"].ToString());
                }
                Thread.Sleep(5000);
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// ประมวลผลทุกหนึ่งวินาที
        /// </summary>



        void _mainForm_Resize(object sender, EventArgs e)
        {
            MyLib._myGlobal._mainSize = this.Size;
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


        void _selectMenuTakeOffSale(string menuName, string screenName)
        {
            bool __selectTabFound = _selectTab(menuName);
            if (__selectTabFound == false)
            {
                Control __getControl = _selectMenu._getObject(menuName);
                if (__getControl != null)
                {
                    _createAndSelectTab(menuName, menuName, screenName, __getControl);
                }
            }
        }
        //void _selectMenuReportIC(string menuName, string screenName)
        //{
        //    bool __selectTabFound = _selectTab(menuName);
        //    if (__selectTabFound == false)
        //    {
        //        Control __getControl = SMLERPICReport._selectMenu._getObject(menuName, screenName);
        //        if (__getControl != null)
        //        {
        //            _createAndSelectTab(menuName, menuName, screenName, __getControl);
        //        }
        //    }
        //}

        //void _selectMenuICInfo(string menuName, string screenName)
        //{
        //    bool __selectTabFound = _selectTab(menuName);
        //    if (__selectTabFound == false)
        //    {
        //        Control __getControl = SMLERPICInfo._selectMenu._getObject(menuName);
        //        if (__getControl != null)
        //        {
        //            _createAndSelectTab(menuName, menuName, screenName, __getControl);
        //        }
        //    }
        //}

        //void _selectMenuARInfo(string menuName, string screenName)
        //{
        //    bool __selectTabFound = _selectTab(menuName);
        //    if (__selectTabFound == false)
        //    {
        //        Control __getControl = SMLERPARInfo._selectMenu._getObject(menuName);
        //        if (__getControl != null)
        //        {
        //            _createAndSelectTab(menuName, menuName, screenName, __getControl);
        //        }
        //    }
        //}

        //private void _selectMenuAPReport(string menuName, string screenName)
        //{
        //    bool __selectTabFound = _selectTab(menuName);
        //    if (__selectTabFound == false)
        //    {
        //        Control __getControl = SMLERPAPReport._selectMenu._getObject(menuName);
        //        if (__getControl != null)
        //        {
        //            _createAndSelectTab(menuName, menuName, screenName, __getControl);
        //        }
        //    }
        //}

        //private void _selectMenuARReport(string menuName, string screenName)
        //{
        //    bool __selectTabFound = _selectTab(menuName);
        //    if (__selectTabFound == false)
        //    {
        //        Control __getControl = SMLERPARReport._selectMenu._getObject(menuName);
        //        if (__getControl != null)
        //        {
        //            _createAndSelectTab(menuName, menuName, screenName, __getControl);
        //        }
        //    }
        //}
        //private void _selectMenuPOReport(string menuName, string screenName)
        //{
        //    bool __selectTabFound = _selectTab(menuName);
        //    if (__selectTabFound == false)
        //    {
        //        Control __getControl = SMLERPPOReport._selectMenu._getObject(menuName);
        //        if (__getControl != null)
        //        {
        //            _createAndSelectTab(menuName, menuName, screenName, __getControl);
        //        }
        //    }
        //}
        //private void _selectMenuSOReport(string menuName, string screenName)
        //{
        //    bool __selectTabFound = _selectTab(menuName);
        //    if (__selectTabFound == false)
        //    {
        //        Control __getControl = SMLERPSOReport._selectMenu._getObject(menuName);
        //        if (__getControl != null)
        //        {
        //            _createAndSelectTab(menuName, menuName, screenName, __getControl);
        //        }
        //    }
        //}

        //void _selectMenuCBReport(string menuName, string screenName)
        //{
        //    bool __selectTabFound = _selectTab(menuName);
        //    if (__selectTabFound == false)
        //    {
        //        Control __getControl = SMLERPCASHBANKReport._selectMenu._getObject(menuName);
        //        if (__getControl != null)
        //        {
        //            _createAndSelectTab(menuName, menuName, screenName, __getControl);
        //        }
        //    }
        //}

        public static string _googleTranslateText(string msg, string fromLang, string toLang)
        {
            string target = "http://www.google.com/translate_t?ie=UTF-8&oe=UTF-8&text={2}&langpair={0}|{1}";
            string url = String.Format(target, fromLang, toLang, msg);
            WebClient webClient = new WebClient();
            webClient.Encoding = System.Text.Encoding.UTF8;
            string result = webClient.DownloadString(url);
            string sign = "<div id=result_box dir=\"ltr\">";
            result = result.Substring(result.IndexOf(sign) + sign.Length);
            result = result.Substring(0, result.IndexOf("</div"));
            return result.Replace("<br>", "\n"); ;
        }

        public override void _activeMenu(string mainMenuId, string menuName, string tag)
        {
            // start
            string __screenName = MyLib._myResource._findResource(menuName, menuName)._str;
            MyLib._myFrameWork __myFrameWork = new _myFrameWork();
            //
            MyLib._mainMenuClass __listmenu = new MyLib._mainMenuClass();
            __listmenu = MyLib._myGlobal._listMenuAll;
            bool __ischeckMainmenu = (mainMenuId.Equals(menuName)) ? true : false;
            string _mainMenuCode = "";

            if (__ischeckMainmenu == false)
            {
                //MyLib._myGlobal._mainMenuCode = menuName;
                _mainMenuCode = menuName;
                _PermissionsType __permission = MyLib._myGlobal._isAccessMenuPermision(mainMenuId, _mainMenuCode);
                MyLib._myGlobal._mainMenuIdPassTrue = mainMenuId;
                MyLib._myGlobal._mainMenuCodePassTrue = _mainMenuCode;

                if (__permission._isRead)
                // if (MyLib._myGlobal._isAccessMenu(menuName))
                {
                    //if (menuName.Equals("menu_import_data"))
                    //{
                    //    _createAndSelectTab(menuName, menuName, __screenName, new MyLib._databaseManage._importDataControl());
                    //}
                    //else

                        //if (menuName.Equals("menu_google_translate"))
                        //{
                        //    _g.g._companyProfileLoad();
                        //    DialogResult __result = MessageBox.Show("Confirm.", "", MessageBoxButtons.YesNo);
                        //    if (__result == DialogResult.Yes)
                        //    {
                        //        this.Cursor = Cursors.WaitCursor;
                        //        DataTable __resource = __myFrameWork._query(MyLib._myGlobal._mainDatabase, "select roworder,name_1,name_2 from sml_resource where name_6 is null or length(name_6) <= 2").Tables[0];
                        //        for (int __row = 0; __row < __resource.Rows.Count; __row++)
                        //        {
                        //            string __roworder = __resource.Rows[__row][0].ToString();
                        //            string __name1 = __resource.Rows[__row][1].ToString().Replace("_", " ").Replace(".", " ");
                        //            string __name2 = _googleTranslateText(__name1, "th", "en");
                        //            string __name3 = _googleTranslateText(__name1, "th", "zh-TW");
                        //            string __name4 = _googleTranslateText(__name1, "th", "zh-CN");
                        //            string __name5 = _googleTranslateText(__name1, "th", "ms");
                        //            string __name6 = _googleTranslateText(__name1, "th", "id");
                        //            if (__name2.Length > 0)
                        //            {
                        //                string __query = "update sml_resource set name_2=\'" + __name2 + "\',name_3=\'" + __name3 + "\',name_4=\'"
                        //                    + __name4 + "\' ,name_5=\'" + __name5 + "\',name_6=\'" + __name6 + "\' where roworder=" + __roworder;
                        //                MyLib._myGlobal._guid = "SMLX";
                        //                string __result2 = __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._mainDatabase, __query);
                        //                if (__result2.Length > 0)
                        //                {
                        //                    MessageBox.Show(__result2);
                        //                }
                        //            }
                        //            MyLib._myGlobal._statusLabel.Text = string.Concat("Google Trans : ", __name1 + "=" + __name2 + "," + __name3 + "," + __name4 + "," + __name5 + "," + __name6);
                        //            MyLib._myGlobal._statusLabel.Invalidate();
                        //            MyLib._myGlobal._statusStrip.Refresh();
                        //        }
                        //        this.Cursor = Cursors.Default;
                        //        MessageBox.Show("Success.");
                        //    }
                        //}
                        //else
                    if (tag.IndexOf("&take&") != -1)
                            {
                                _g.g._companyProfileLoad();
                                _selectMenuTakeOffSale(menuName, __screenName);
                            }        
                                else
                                {
                                    bool __selectTabFound = _selectTab(menuName);
                                    if (__selectTabFound == false)
                                    {
                                      //  Control __getControl = _g._selectMenu._getObject(menuName, __screenName, _programName);
                                          Control __getControl =(new _g._selectMenu())._getObject(menuName, __screenName, _programName);
                                        if (__getControl != null)
                                        {
                                            _g.g._companyProfileLoad();
                                            _createAndSelectTab(menuName, menuName, __screenName, __getControl);
                                        }
                                    }
                                }
                }
                else
                {
                  MessageBox.Show(MyLib._myGlobal._resource("warning126") + __screenName, MyLib._myGlobal._resource("warning"), MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
            }

        }
    }
}
