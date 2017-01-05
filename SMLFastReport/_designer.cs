using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Crom.Controls.Docking;
using System.Xml.Serialization;
using System.IO;
using System.Globalization;

namespace SMLFastReport
{
    public partial class _designer : UserControl
    {
        private _configForm _config = new _configForm();
        private _previewForm _preview = new _previewForm();
        private _genReport _gen;
        private _searchResourceForm _resource = new _searchResourceForm();
        private MyLib._databaseManage._viewManage _viewManageControl;
        private Form _viewManageForm;
        _conditionForm _conditonForm;

        private string _menuId;
        private string _menuName;
        /// <summary>
        /// สำหรับ register
        /// </summary>
        private string _owner_code = "";
        private int _is_systemreport = 0;
        private int _menuTypeResult = 0;

        private DockableFormInfo _formResource;
        private DockableFormInfo _formLeft;
        private DockableFormInfo _formRight;

        public int _menu_type
        {
            get
            {

                _menuTypeResult = this._reportTypeCombo.SelectedIndex;
                return _menuTypeResult;

            }
            set
            {
                _menuTypeResult = value;
                this._reportTypeCombo.SelectedIndex = value;
            }
        }

        /// <summary>อนุญาติให้ทำการบันทึกหรือไม่ (ใช้ในกรณี เปิดแสดงรายงานจากเมนูอื่น ๆ ที่ไม่ใช่ ออกแบบรายงาน )</summary>
        bool _allowUserSave = true;

        void _build()
        {

        }

        Boolean _showConditionScreen()
        {
            if (_conditonForm == null)
            {
                _conditonForm = new _conditionForm();
                _conditonForm.Text = this._menuName;

                _conditonForm._reportNameLabel.Text = this._menuName;
                _conditonForm._conditionPanel.Controls.Add(this._gen._condition);
                _conditonForm.StartPosition = FormStartPosition.CenterScreen;
            }

            DialogResult __result = _conditonForm.ShowDialog();

            return (__result == DialogResult.Yes) ? true : false;
        }

        public _designer()
        {
            InitializeComponent();
            if (MyLib._myGlobal._isDesignMode == false)
            {
                this.toolStrip1.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            }

            // check send server button
            if (MyLib._myGlobal._isUserTest)
            {
                this._reportServerSaveButton.Visible = true;
                //__autoLogin = true;
            }

            // set reportCombo
            //this._reportTypeCombo.ComboBox.DrawMode = DrawMode.OwnerDrawFixed;
            this._reportTypeCombo.ComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            this._reportTypeCombo.Items.Add(MyLib._myResource._findResource(_g.d.sml_fastreport._table + "." + _g.d.sml_fastreport._general_report)._str);
            this._reportTypeCombo.Items.Add(MyLib._myResource._findResource(_g.d.sml_fastreport._table + "." + _g.d.sml_fastreport._inventory_report)._str);
            this._reportTypeCombo.Items.Add(MyLib._myResource._findResource(_g.d.sml_fastreport._table + "." + _g.d.sml_fastreport._purchase_report)._str);
            this._reportTypeCombo.Items.Add(MyLib._myResource._findResource(_g.d.sml_fastreport._table + "." + _g.d.sml_fastreport._sale_report)._str);
            this._reportTypeCombo.Items.Add(MyLib._myResource._findResource(_g.d.sml_fastreport._table + "." + _g.d.sml_fastreport._supplier_report)._str);
            this._reportTypeCombo.Items.Add(MyLib._myResource._findResource(_g.d.sml_fastreport._table + "." + _g.d.sml_fastreport._customer_report)._str);
            this._reportTypeCombo.Items.Add(MyLib._myResource._findResource(_g.d.sml_fastreport._table + "." + _g.d.sml_fastreport._cashbank_report)._str);
            this._reportTypeCombo.Items.Add(MyLib._myResource._findResource(_g.d.sml_fastreport._table + "." + _g.d.sml_fastreport._asset_report)._str);
            this._reportTypeCombo.Items.Add(MyLib._myResource._findResource(_g.d.sml_fastreport._table + "." + _g.d.sml_fastreport._account_report)._str);
            this._reportTypeCombo.SelectedIndexChanged += new EventHandler(_reportTypeCombo_SelectedIndexChanged);
            this._reportTypeCombo.SelectedIndex = 0;
            //
            _formResource = this._dock.Add(this._resource, Crom.Controls.Docking.zAllowedDock.All, Guid.NewGuid());
            this._dock.DockForm(_formResource, DockStyle.Left, zDockMode.Inner);
            this._dock.SetAutoHide(_formResource, true);

            _formLeft = this._dock.Add(this._preview, Crom.Controls.Docking.zAllowedDock.All, Guid.NewGuid());
            this._dock.DockForm(_formLeft, DockStyle.Fill, zDockMode.Inner);

            _formRight = this._dock.Add(this._config, Crom.Controls.Docking.zAllowedDock.All, Guid.NewGuid());
            this._dock.DockForm(_formRight, DockStyle.Right, zDockMode.Inner);

            this._gen = new _genReport(this._preview._view, this._config._conditionScreen);
            this._preview._view._buildSuccess += new SMLReport._report.BuildSuccessEventHandle(_view__buildSuccess);
            //
            this._preview._view._buttonBuildReport.Visible = false;
            this._preview._view._buttonCondition.Visible = false;
            this._preview._view._optionButton.Visible = false;
            this._preview._view._buttonClose.Click += _buttonClose_Click;

            //this._editMode = false;
        }

        private Boolean _editModeResult = false;
        public Boolean _editMode
        {
            get
            {
                return _editModeResult;
            }

            set
            {
                _editModeResult = value;

                if (_editModeResult == true)
                {
                    this.Controls.Clear();
                    this.Controls.Add(this._dock);
                    this.Controls.Add(this.toolStrip1);
                    this.toolStrip1.Visible = true;

                    _formResource = this._dock.Add(this._resource, Crom.Controls.Docking.zAllowedDock.All, Guid.NewGuid());
                    this._dock.DockForm(_formResource, DockStyle.Left, zDockMode.Inner);
                    this._dock.SetAutoHide(_formResource, true);

                    _formLeft = this._dock.Add(this._preview, Crom.Controls.Docking.zAllowedDock.All, Guid.NewGuid());
                    this._dock.DockForm(_formLeft, DockStyle.Fill, zDockMode.Inner);

                    _formRight = this._dock.Add(this._config, Crom.Controls.Docking.zAllowedDock.All, Guid.NewGuid());
                    this._dock.DockForm(_formRight, DockStyle.Right, zDockMode.Inner);

                    //this._gen = new _genReport(this._preview._view, this._config._conditionScreen);
                    //this._config._conditionScreen._build(this._config._xml());
                    this._config._conditionPanel.Controls.Add(this._config._conditionScreen);
                    this._config._conditionScreen.Invalidate();
                }
                else
                {
                    this.toolStrip1.Visible = false;
                    this._preview._view._buttonBuildReport.Visible = true;
                    this._preview._view._buttonCondition.Visible = true;
                    this._preview._view._reportDesignButton.Visible = true;

                    _formResource.Dispose();
                    _formLeft.Dispose();
                    _formRight.Dispose();
                    Boolean __process = _showConditionScreen();
                    this.Controls.Clear();
                    this._preview.Dock = DockStyle.Fill;

                    this._preview._view._buttonCondition.Click -= _buttonCondition_Click;
                    this._preview._view._buttonCondition.Click += _buttonCondition_Click;

                    this._preview._view._buttonBuildReport.Click -= _buttonBuildReport_Click;
                    this._preview._view._buttonBuildReport.Click += _buttonBuildReport_Click;

                    this._preview._view._reportDesignButton.Click -= _reportDesignButton_Click;
                    this._preview._view._reportDesignButton.Click += _reportDesignButton_Click;

                    this.Controls.Add(this._preview);

                    if (__process == true)
                    {
                        _previewButton_Click(this, null);
                    }
                }
            }
        }

        void _reportDesignButton_Click(object sender, EventArgs e)
        {
            if (this._editMode == false)
            {
                this._editMode = true;
            }
        }

        void _buttonBuildReport_Click(object sender, EventArgs e)
        {
            _previewButton_Click(sender, e);
        }

        void _buttonCondition_Click(object sender, EventArgs e)
        {
            Boolean __process = _showConditionScreen();
            if (__process == true)
            {
                _previewButton_Click(sender, e);
            }
        }

        void _reportTypeCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            this._menuTypeResult = this._reportTypeCombo.SelectedIndex;
        }

        void _view__buildSuccess()
        {
            this._preview.Enabled = true;
        }

        public void _loadReportOhter(string menuname)
        {

        }

        public void _load(string menuname)
        {
            _load(menuname, false);
        }

        public void _load(string menuId, string menuName)
        {
            this._menuName = menuName;
            _load(menuId, false);
        }


        public void _load(string menuName, Boolean _allowSave)
        {
            bool _checkNewVersion = false;

            _allowUserSave = _allowSave;

            // sync report timestamp from SMLMASTER
            string __checkVersionQuery = "select " + _g.d.sml_fastreport._timeupdate + ", " + _g.d.sml_fastreport._menuid + ", " + _g.d.sml_fastreport._menuname + "," + _g.d.sml_fastreport._is_system_report + "," + _g.d.sml_fastreport._report_type + "," + _g.d.sml_fastreport._owner_code + " from " + _g.d.sml_fastreport._table + " where UPPER(" + _g.d.sml_fastreport._menuid + ") = '" + menuName.ToUpper() + "'";

            MyLib._myFrameWork __myFrameWorkMaster = new MyLib._myFrameWork(MyLib._myGlobal._masterWebservice, MyLib._myGlobal._masterConfigXmlName, MyLib._myGlobal._databaseType.PostgreSql);

            string __masterTimeUpdate = "";
            string __masterMenuId = "";
            string __owner_code = "";
            string __masterMenuName = this._menuName;
            int __masterMenuType = 0;
            int __systemReport = -1;
            DateTime __reportSaveTime = new DateTime();

            if (__myFrameWorkMaster._testConnect() == true)
            {
                DataSet __masterResult = __myFrameWorkMaster._query(MyLib._myGlobal._masterDatabaseName, __checkVersionQuery);
                if (__masterResult.Tables.Count > 0 && __masterResult.Tables[0].Rows.Count > 0)
                {
                    _checkNewVersion = true;
                    try
                    {
                        __masterTimeUpdate = __masterResult.Tables[0].Rows[0][_g.d.sml_fastreport._timeupdate].ToString();
                        __masterMenuId = __masterResult.Tables[0].Rows[0][_g.d.sml_fastreport._menuid].ToString();
                        __masterMenuName = __masterResult.Tables[0].Rows[0][_g.d.sml_fastreport._menuname].ToString();
                        //__masterByteData = null;
                        __reportSaveTime = MyLib._myGlobal._checkDate(MyLib._myGlobal._convertDateFromQuery(__masterTimeUpdate), 0); // DateTime.Parse(__masterTimeUpdate, MyLib._myGlobal._cultureInfo());

                        //__reportSaveTime = MyLib._myGlobal._convertDate(__masterTimeUpdate,  MyLib._myGlobal._checkYearType(__reportSaveTime));

                        __masterMenuType = (int)MyLib._myGlobal._decimalPhase(__masterResult.Tables[0].Rows[0][_g.d.sml_fastreport._report_type].ToString());
                        __owner_code = __masterResult.Tables[0].Rows[0][_g.d.sml_fastreport._owner_code].ToString();

                    }
                    catch
                    {
                    }
                }
            }

            // get local
            string __localTimeUpdate = "";
            DateTime __localSaveTime = new DateTime();
            int __localMenuType = -1;

            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();

            if (__masterTimeUpdate.Trim().Equals("") == false)
            {
                DataSet __localResult = __myFrameWork._query(MyLib._myGlobal._databaseName, __checkVersionQuery);

                if (__localResult.Tables.Count > 0 && __localResult.Tables[0].Rows.Count > 0)
                {
                    __localTimeUpdate = __localResult.Tables[0].Rows[0][_g.d.sml_fastreport._timeupdate].ToString();
                    __localSaveTime = MyLib._myGlobal._checkDate(MyLib._myGlobal._convertDateFromQuery(__localTimeUpdate), 0); //DateTime.Parse(__localTimeUpdate, MyLib._myGlobal._cultureInfo());

                    //__localSaveTime = MyLib._myGlobal._convertDate(__localTimeUpdate, MyLib._myGlobal._checkYearType(__localSaveTime));

                    __systemReport = (int)MyLib._myGlobal._decimalPhase(__localResult.Tables[0].Rows[0][_g.d.sml_fastreport._is_system_report].ToString());
                    __localMenuType = (int)MyLib._myGlobal._decimalPhase(__localResult.Tables[0].Rows[0][_g.d.sml_fastreport._report_type].ToString());
                }
            }

            if ((__reportSaveTime > __localSaveTime || __localMenuType != __masterMenuType || __systemReport == 0) && _checkNewVersion)
            {
                // start sycn
                __systemReport = 1;
                // select from master
                string __query = string.Format("select " + _g.d.sml_fastreport._reportdata + " from " + _g.d.sml_fastreport._table + " where UPPER(" + _g.d.sml_fastreport._menuid + ") = '{0}'", menuName.ToUpper());
                byte[] __getByte = __myFrameWorkMaster._queryByte(MyLib._myGlobal._masterDatabaseName, __query);


                try
                {
                    // insert to local
                    string __delQuery = string.Format("delete from " + _g.d.sml_fastreport._table + " where UPPER(" + _g.d.sml_fastreport._menuid + ") = '{0}'", menuName.ToUpper());
                    __myFrameWork._query(MyLib._myGlobal._databaseName, __delQuery);

                    string __query2 = string.Format("insert into " + _g.d.sml_fastreport._table + "(" + _g.d.sml_fastreport._menuid + "," + _g.d.sml_fastreport._menuname + ", " + _g.d.sml_fastreport._timeupdate + "," + _g.d.sml_fastreport._guid_code + ", " + _g.d.sml_fastreport._report_type + ", " + _g.d.sml_fastreport._is_system_report + "," + _g.d.sml_fastreport._reportdata + ") VALUES('{0}','{1}', '{2}', '', {3}, 1,?)", __masterMenuId, __masterMenuName, __masterTimeUpdate, __masterMenuType);
                    string __resultStr = __myFrameWork._queryByteData(MyLib._myGlobal._databaseName, __query2, new object[] { __getByte });

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }


            this._menuId = menuName;
            this._menuName = __masterMenuName;
            this._owner_code = __owner_code;
            this._is_systemreport = (__systemReport == -1) ? 0 : __systemReport;
            this._loadData();

            this._editMode = _allowUserSave;
        }

        private void _previewButton_Click(object sender, EventArgs e)
        {
            this._preview.Enabled = false;
            this._gen._condition._saveLastControl();
            _xmlClass __xml = this._config._xml();
            this._gen._init(__xml, this._showQueryCheckedBox.MyCheckBox.Checked);
            //if ()
            //this._preview._view._scaleComboBox.SelectedIndex = 7;
            this._preview._view._splitRowOnOverFlowPage = __xml._splitRow;
            this._preview._view._paper._showLineLastPage = __xml._showLineLastPage;
            this._preview._view.Invalidate();
        }

        public void _saveData()
        {
            // check master report 

            // ตรวจสอบ ซ้ำ
            string __checkQuery = string.Format("select count(" + _g.d.sml_fastreport._menuid + ") as countReport from " + _g.d.sml_fastreport._table + " where UPPER(" + _g.d.sml_fastreport._menuid + ") = '{0}'", this._menuId.ToUpper());
            MyLib._myFrameWork __ws = new MyLib._myFrameWork();

            DataSet __ds = __ws._query(MyLib._myGlobal._databaseName, __checkQuery);
            if (__ds.Tables.Count > 0 && __ds.Tables[0].Rows.Count > 0 && (MyLib._myGlobal._decimalPhase(__ds.Tables[0].Rows[0]["countReport"].ToString()) > 0))
            {
                MessageBox.Show(string.Format("มีการใช้ Menuid : {0} ไปแล้ว", this._menuId));
                //this._menuId = null;
                //this._menuName = null;
                //this._menu_type = 0;
                this._menuId = __oldMenuId;
                this._menuName = __oldMenuName;
                this._menu_type = __oldMenuType;
                this._owner_code = __oldOwner_Code;
                return;
            }

            _xmlClass __xml = this._config._xml();
            __xml._isLandscape = this._preview._view._pageSetupDialog.PageSettings.Landscape;

            //serialize 
            XmlSerializer __xs = new XmlSerializer(typeof(_xmlClass));
            MemoryStream __memoryStream = new MemoryStream();
            __xs.Serialize(__memoryStream, __xml);

            string _query = string.Format("insert into " + _g.d.sml_fastreport._table + "(" + _g.d.sml_fastreport._menuid + "," + _g.d.sml_fastreport._menuname + ", " + _g.d.sml_fastreport._timeupdate + ", " + _g.d.sml_fastreport._report_type + ", " + _g.d.sml_fastreport._owner_code + "," + _g.d.sml_fastreport._reportdata + ") VALUES('{0}','{1}', '{2}', {3}, '{4}',?)", this._menuId, this._menuName, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", new CultureInfo("en-US")), this._menu_type, this._owner_code);

            byte[] __memoryStreamCompress = MyLib._compress._compressString(MyLib._myGlobal._convertMemoryStreamToString(__memoryStream));

            string __result = __ws._queryByteData(MyLib._myGlobal._databaseName, _query, new object[] { __memoryStreamCompress });
            if (__result.Equals(""))
            {
                MessageBox.Show("บันทึกข้อมูลเรียบร้อย");
            }
            else
            {
                MessageBox.Show(__result, "wraning");
            }
        }

        public void _loadData()
        {

            // ตอนโหลด เช็คชื่อเครื่องและ flag ด้วย

            string __query = string.Format("select " + _g.d.sml_fastreport._reportdata + " from " + _g.d.sml_fastreport._table + " where UPPER(" + _g.d.sml_fastreport._menuid + ") = '{0}'", this._menuId.ToUpper());

            MyLib._myFrameWork __fw = new MyLib._myFrameWork();
            byte[] __getByte = __fw._queryByte(MyLib._myGlobal._databaseName, __query);

            try
            {
                if (__getByte.Length == 1024 && this._is_systemreport == 1)
                {
                    // byte เสีย
                    // resync ใหม่

                    string __checkVersionQuery = "select " + _g.d.sml_fastreport._timeupdate + ", " + _g.d.sml_fastreport._menuid + ", " + _g.d.sml_fastreport._menuname + "," + _g.d.sml_fastreport._is_system_report + "," + _g.d.sml_fastreport._report_type + "," + _g.d.sml_fastreport._owner_code + " from " + _g.d.sml_fastreport._table + " where UPPER(" + _g.d.sml_fastreport._menuid + ") = '" + _menuId.ToUpper() + "'";
                    MyLib._myFrameWork __myFrameWorkMaster = new MyLib._myFrameWork(MyLib._myGlobal._masterWebservice, MyLib._myGlobal._masterConfigXmlName, MyLib._myGlobal._databaseType.PostgreSql);
                    string __masterTimeUpdate = "";
                    string __masterMenuId = "";
                    string __owner_code = "";
                    string __masterMenuName = this._menuName;
                    int __masterMenuType = 0;
                    int __systemReport = -1;
                    DateTime __reportSaveTime = new DateTime();

                    if (__myFrameWorkMaster._testConnect() == true)
                    {
                        DataSet __masterResult = __myFrameWorkMaster._query(MyLib._myGlobal._masterDatabaseName, __checkVersionQuery);
                        if (__masterResult.Tables.Count > 0 && __masterResult.Tables[0].Rows.Count > 0)
                        {
                            try
                            {
                                __masterTimeUpdate = __masterResult.Tables[0].Rows[0][_g.d.sml_fastreport._timeupdate].ToString();
                                __masterMenuId = __masterResult.Tables[0].Rows[0][_g.d.sml_fastreport._menuid].ToString();
                                __masterMenuName = __masterResult.Tables[0].Rows[0][_g.d.sml_fastreport._menuname].ToString();
                                //__masterByteData = null;
                                __reportSaveTime = MyLib._myGlobal._checkDate(MyLib._myGlobal._convertDateFromQuery(__masterTimeUpdate), 0); // DateTime.Parse(__masterTimeUpdate, MyLib._myGlobal._cultureInfo());

                                //__reportSaveTime = MyLib._myGlobal._convertDate(__masterTimeUpdate,  MyLib._myGlobal._checkYearType(__reportSaveTime));

                                __masterMenuType = (int)MyLib._myGlobal._decimalPhase(__masterResult.Tables[0].Rows[0][_g.d.sml_fastreport._report_type].ToString());
                                __owner_code = __masterResult.Tables[0].Rows[0][_g.d.sml_fastreport._owner_code].ToString();

                            }
                            catch
                            {
                            }
                        }



                        string __queryReSync = string.Format("select " + _g.d.sml_fastreport._reportdata + " from " + _g.d.sml_fastreport._table + " where UPPER(" + _g.d.sml_fastreport._menuid + ") = '{0}'", _menuId.ToUpper());
                        byte[] __getResyncByte = __myFrameWorkMaster._queryByte(MyLib._myGlobal._masterDatabaseName, __query);

                        try
                        {
                            // insert to local
                            string __delQuery = string.Format("delete from " + _g.d.sml_fastreport._table + " where UPPER(" + _g.d.sml_fastreport._menuid + ") = '{0}'", this._menuId.ToUpper());
                            __fw._query(MyLib._myGlobal._databaseName, __delQuery);

                            string __query2 = string.Format("insert into " + _g.d.sml_fastreport._table + "(" + _g.d.sml_fastreport._menuid + "," + _g.d.sml_fastreport._menuname + ", " + _g.d.sml_fastreport._timeupdate + "," + _g.d.sml_fastreport._guid_code + ", " + _g.d.sml_fastreport._report_type + ", " + _g.d.sml_fastreport._is_system_report + "," + _g.d.sml_fastreport._reportdata + ") VALUES('{0}','{1}', '{2}', '', {3}, 1,?)", _menuId, _menuName, __masterTimeUpdate, __masterMenuType);
                            string __resultStr = __fw._queryByteData(MyLib._myGlobal._databaseName, __query2, new object[] { __getResyncByte });

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.ToString());
                        }
                    }

                    __getByte = __fw._queryByte(MyLib._myGlobal._databaseName, __query);
                }

                MemoryStream __ms = new MemoryStream(MyLib._compress._decompressBytes((byte[])__getByte));
                XmlSerializer __xs = new XmlSerializer(typeof(_xmlClass));
                _xmlClass __xml = (_xmlClass)__xs.Deserialize(__ms);
                this._config._loadFromXML(__xml);
                this._preview._view._pageSetupDialog.PageSettings.Landscape = __xml._isLandscape;
                this._preview._view.Refresh();
                //this._preview._view._scaleComboBox.SelectedIndex = 8;

                this._preview._view._splitRowOnOverFlowPage = __xml._splitRow;
                this._preview._view._paper._showLineLastPage = __xml._showLineLastPage;

                __ms.Close();
                this._gen._condition._build(__xml);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                this._menuId = null;
                this._menuName = null;
                this._owner_code = "";

                this._menu_type = 0;

            }

        }

        private void _buttonSaveReport_Click(object sender, EventArgs e)
        {
            if (_checkReportReadOnly())
            {
                if (this._is_systemreport == 1 && MyLib._myGlobal._isUserTest == false)
                {
                    MessageBox.Show(MyLib._myGlobal._resource("รายงานนี้ไม่อนุญาติให้แก้ไข"), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (this._menuId == null && this._menuName == null)
                {
                    _saveForm __save = new _saveForm();
                    __oldMenuId = this._menuId;
                    __oldMenuName = this._menuName;
                    __oldMenuType = this._menu_type;
                    __save._beforeDispose += (s1, e1) =>
                    {
                        if (__save.DialogResult == DialogResult.Yes)
                        {
                            _saveForm __form = (_saveForm)s1;
                            this._menuId = __form._screen._getDataStr(_g.d.sml_fastreport._menuid); // __form._menuIdTextBox.Text;
                            this._menuName = __form._screen._getDataStr(_g.d.sml_fastreport._menuname); // __form._menuNameTextBox.Text;
                            this._menu_type = (int)MyLib._myGlobal._decimalPhase(__form._screen._getDataStr(_g.d.sml_fastreport._report_type));
                            this._owner_code = __form._screen._getDataStr(_g.d.sml_fastreport._owner_code);
                            _saveData();
                        }
                    };
                    __save.ShowDialog();
                }
                else
                {
                    // delete old record
                    string _query = string.Format("delete from " + _g.d.sml_fastreport._table + " where UPPER(" + _g.d.sml_fastreport._menuid + ") = '{0}'", this._menuId.ToUpper());
                    MyLib._myFrameWork __fw = new MyLib._myFrameWork();
                    __fw._query(MyLib._myGlobal._databaseName, _query);

                    _saveData();
                }
            }
        }

        private bool _checkReportReadOnly()
        {
            if (this._allowUserSave == false)
            {
                MessageBox.Show(MyLib._myGlobal._resource("รายงานนี้อยู่ในโหมดเปิดอ่านได้อย่างเดียว"), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        private void _buttonLoadReport_Click(object sender, EventArgs e)
        {
            if (this._checkReportReadOnly())
            {
                _loadForm __load = new _loadForm();
                __load._ReportSelected += (s1, e1) =>
                {
                    this._menuId = e1.menuId;
                    this._menuName = e1.menuName;
                    //_allowUserSave = true;
                    this._menu_type = e1.menuType;

                    // load config report
                    MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();

                    DataSet __ds = __myFrameWork._queryShort("select " + _g.d.sml_fastreport._is_system_report + "," + _g.d.sml_fastreport._owner_code + " from " + _g.d.sml_fastreport._table + " where " + MyLib._myGlobal._addUpper(_g.d.sml_fastreport._menuid) + "='" + this._menuId.ToUpper() + "'");

                    if (__ds.Tables.Count > 0 && __ds.Tables[0].Rows.Count > 0)
                    {
                        this._is_systemreport = (int)MyLib._myGlobal._decimalPhase(__ds.Tables[0].Rows[0][_g.d.sml_fastreport._is_system_report].ToString());
                        this._owner_code = __ds.Tables[0].Rows[0][_g.d.sml_fastreport._owner_code].ToString();
                    }
                    this._loadData();
                };
                __load.ShowDialog();
            }
        }

        private void _buttonSaveAs_Click(object sender, EventArgs e)
        {
            _saveForm __save = new _saveForm();
            __oldMenuId = this._menuId;
            __oldMenuName = this._menuName;
            __oldMenuType = this._menu_type;
            __save._beforeDispose += (s1, e1) =>
            {
                if (__save.DialogResult == DialogResult.Yes)
                {
                    _saveForm __form = (_saveForm)s1;
                    //this._menuId = __form._menuIdTextBox.Text;
                    //this._menuName = __form._menuNameTextBox.Text;
                    this._menuId = __form._screen._getDataStr(_g.d.sml_fastreport._menuid); // __form._menuIdTextBox.Text;
                    this._menuName = __form._screen._getDataStr(_g.d.sml_fastreport._menuname); // __form._menuNameTextBox.Text;
                    this._menu_type = (int)MyLib._myGlobal._decimalPhase(__form._screen._getDataStr(_g.d.sml_fastreport._report_type));
                    this._owner_code = __form._screen._getDataStr(_g.d.sml_fastreport._owner_code);
                    this._saveData();
                }
            };
            __save.ShowDialog();
        }

        private void _buttonClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void _saveXMLButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog __objFile = new SaveFileDialog() { DefaultExt = "xml", Filter = "XML Files (*.xml)|*.xml|All files (*.*)|*.*", FilterIndex = 0 };
            if (__objFile.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    _xmlClass __xml = this._config._xml();

                    XmlSerializer __xs = new XmlSerializer(typeof(_xmlClass));

                    TextWriter __memoryStream = new StreamWriter(__objFile.OpenFile());
                    __xs.Serialize(__memoryStream, __xml);
                    __memoryStream.Close();

                    MessageBox.Show("Save XML Success", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error : " + ex.ToString(), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void _loadXMLButton_Click(object sender, EventArgs e)
        {
            if (this._checkReportReadOnly())
            {

                OpenFileDialog __openFile = new OpenFileDialog() { DefaultExt = "xml", Filter = "XML Files (*.xml)|*.xml|All files (*.*)|*.*", FilterIndex = 0 };
                if (__openFile.ShowDialog() == DialogResult.OK)
                {
                    Stream __readFileStream = __openFile.OpenFile();
                    XmlSerializer __xs = new XmlSerializer(typeof(_xmlClass));
                    _xmlClass __xml = (_xmlClass)__xs.Deserialize(__readFileStream);

                    // set screen pos from xml
                    //_setPosDesignXML(__xml);
                    this._config._loadFromXML(__xml);
                    __readFileStream.Close();
                    this._gen._condition._build(__xml);
                    this._preview._view._splitRowOnOverFlowPage = __xml._splitRow;
                    this._preview._view._paper._showLineLastPage = __xml._showLineLastPage;

                }
            }
        }

        private void _viewSearchNameButton_Click(object sender, EventArgs e)
        {
            this._viewManageForm = new Form();
            this._viewManageControl = new MyLib._databaseManage._viewManage(false);
            this._viewManageControl.Dock = DockStyle.Fill;
            this._viewManageForm.Controls.Add(this._viewManageControl);
            this._viewManageForm.Text = "View Name";
            this._viewManageForm.WindowState = FormWindowState.Maximized;
            this._viewManageForm.ShowDialog();
        }

        private void _deleteButton_Click(object sender, EventArgs e)
        {
            if (this._checkReportReadOnly())
            {
                _deleteForm __delete = new _deleteForm();
                __delete.ShowDialog();
            }
        }

        private string __oldMenuId = "";
        private string __oldMenuName = "";
        private int __oldMenuType = 0;
        private string __oldOwner_Code = "";

        private void _reportServerSaveButton_Click(object sender, EventArgs e)
        {
            // save to sml master
            _saveForm __save = new _saveForm();

            __oldMenuId = this._menuId;
            __oldMenuName = this._menuName;
            __oldMenuType = this._menu_type;
            __oldOwner_Code = this._owner_code;

            __save._screen._setDataStr(_g.d.sml_fastreport._menuid, this._menuId);
            __save._screen._setDataStr(_g.d.sml_fastreport._menuname, this._menuName);
            __save._screen._setComboBox(_g.d.sml_fastreport._report_type, this._menu_type);
            __save._screen._setDataStr(_g.d.sml_fastreport._owner_code, this._owner_code);
            __save._beforeDispose += (s1, e1) =>
            {
                if (__save.DialogResult == DialogResult.Yes)
                {
                    _saveForm __form = (_saveForm)s1;
                    this._menuId = __form._screen._getDataStr(_g.d.sml_fastreport._menuid); // __form._menuIdTextBox.Text;
                    this._menuName = __form._screen._getDataStr(_g.d.sml_fastreport._menuname); // __form._menuNameTextBox.Text;
                    this._menu_type = (int)MyLib._myGlobal._decimalPhase(__form._screen._getDataStr(_g.d.sml_fastreport._report_type));
                    this._owner_code = __form._screen._getDataStr(_g.d.sml_fastreport._owner_code); // __form._menuNameTextBox.Text;
                    _saveDataServer();
                }
            };
            __save.ShowDialog();
        }

        public void _saveDataServer()
        {
            // ตรวจสอบ ซ้ำ
            string __checkQuery = string.Format("select count(" + _g.d.sml_fastreport._menuid + ") as countReport from " + _g.d.sml_fastreport._table + " where UPPER(" + _g.d.sml_fastreport._menuid + ") = '{0}'", this._menuId.ToUpper());
            MyLib._myFrameWork __ws = new MyLib._myFrameWork(MyLib._myGlobal._masterWebservice, MyLib._myGlobal._masterConfigXmlName, MyLib._myGlobal._masterDatabaseType);

            DataSet __ds = __ws._query(MyLib._myGlobal._masterDatabaseName, __checkQuery);
            if (__ds.Tables.Count > 0 && __ds.Tables[0].Rows.Count > 0 && (MyLib._myGlobal._decimalPhase(__ds.Tables[0].Rows[0]["countReport"].ToString()) > 0))
            {
                if (MessageBox.Show(string.Format("มีการใช้ Menuid : {0} ไปแล้ว ต้องการจะเขียนทับหรือไม่", this._menuId), "ยืนยัน", MessageBoxButtons.YesNo) == DialogResult.No)
                {
                    this._menuId = __oldMenuId;
                    this._menuName = __oldMenuName;
                    this._menu_type = __oldMenuType;
                    this._owner_code = __oldOwner_Code;
                    return;
                }
            }

            // delete old record
            MyLib._myFrameWork __fw = new MyLib._myFrameWork(MyLib._myGlobal._masterWebservice, MyLib._myGlobal._masterConfigXmlName, MyLib._myGlobal._masterDatabaseType);
            __fw._query(MyLib._myGlobal._masterDatabaseName, string.Format("delete from " + _g.d.sml_fastreport._table + " where UPPER(" + _g.d.sml_fastreport._menuid + ") = '{0}'", this._menuId.ToUpper()));

            _xmlClass __xml = this._config._xml();
            __xml._isLandscape = this._preview._view._pageSetupDialog.PageSettings.Landscape;

            //serialize 
            XmlSerializer __xs = new XmlSerializer(typeof(_xmlClass));
            MemoryStream __memoryStream = new MemoryStream();
            __xs.Serialize(__memoryStream, __xml);

            string __query = string.Format("insert into " + _g.d.sml_fastreport._table + "(" + _g.d.sml_fastreport._menuid + "," + _g.d.sml_fastreport._menuname + ", " + _g.d.sml_fastreport._timeupdate + ", " + _g.d.sml_fastreport._report_type + "," + _g.d.sml_fastreport._owner_code + "," + _g.d.sml_fastreport._reportdata + ") VALUES('{0}','{1}', '{2}', {3}, '{4}',?)", this._menuId, this._menuName, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", new CultureInfo("en-US")), this._menu_type, this._owner_code);

            byte[] __memoryStreamCompress = MyLib._compress._compressString(MyLib._myGlobal._convertMemoryStreamToString(__memoryStream));

            string __result = __ws._queryByteData(MyLib._myGlobal._masterDatabaseName, __query, new object[] { __memoryStreamCompress });
            if (__result.Equals(""))
            {
                MessageBox.Show("บันทึกข้อมูลเรียบร้อย");
            }
            else
            {
                MessageBox.Show(__result, "wraning");
            }
        }
    }
}
