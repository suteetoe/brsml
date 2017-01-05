using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using SMLReport._design;
using SMLReport._formReport;
using System.IO;
using System.Xml.Serialization;
using System.Drawing.Printing;
using System.Drawing.Drawing2D;
using SMLBarcodeManage;

namespace SMLTransportLabel
{
    public partial class _transport_label_print : UserControl
    {
        int _mode = 0;

        public _transport_label_print(int mode)
        {
            this._mode = mode;
            InitializeComponent();
            if (MyLib._myGlobal._isDesignMode == false)
            {
                this._myManageBar.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
                this._myToolbar.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            }
            this._myManageData._displayMode = 0;
            this._myManageData._dataList._lockRecord = true;
            this._myManageData._selectDisplayMode(this._myManageData._displayMode);
            this._myManageData._dataList._extraWhere = _g.d.ap_ar_transport_label._ar_ap_type + "=" + this._mode.ToString();
            //this._myManageData._dataList._columnFieldNameReplace += new MyLib.ColumnFieldNameReplaceEventHandler(_dataList__columnFieldNameReplace);

            this._myManageData._dataList._loadViewFormat(((this._mode == 0) ? "screen_ap_transport_label" : "screen_ar_transport_label"), MyLib._myGlobal._userSearchScreenGroup, true);

            this._myManageData._loadDataToScreen += new MyLib.LoadDataToScreen(_myManageData__loadDataToScreen);
            //this._myManageData._dataList._deleteData += new MyLib.DeleteDataEventHandler(_dataList__deleteData);
            ////this._myManageData._manageButton = this._myToolBar;
            ////this._myManageData._manageBackgroundPanel = this._myPanel;
            //this._myManageData._newDataClick += new MyLib.NewDataEvent(_myManageData__newDataClick);
            //this._myManageData._discardData += new MyLib.DiscardDataEvent(_myManageData__discardData);
            //this._myManageData._clearData += new MyLib.ClearDataEvent(_myManageData__clearData);
            this._myManageData._closeScreen += new MyLib.CloseScreenEvent(_myManageData__closeScreen);
            this._myManageData._dataList._referFieldAdd("roworder", 4);
            //this._myManageData._checkEditData += new MyLib.CheckEditDataEvent(_myManageData__checkEditData);
            this._myManageData._dataListOpen = true;
            this._myManageData._autoSize = true;
            this._myManageData._calcArea();
            //this._myManageData._autoSizeHeight = 450;
            //this._myManageData._dataList._loadViewData(0);
            //_getPicture1._setEnable(false);
            this._myManageData.Invalidate();

            this._build();
        }

        void _build()
        {
            // set printer 
            int __default = 0;
            int __count = 0;
            //foreach (ManagementObject __getPrinter in __printerList.Get())
            foreach (MyLib._printerListClass __getPrinter in MyLib._myGlobal._printerList)
            {
                string __printerName = __getPrinter._printerName;
                if (__getPrinter._isDefault)
                {
                    __default = __count;
                }

                _printerCombobox.Items.Add(__printerName);
                __count++;
            }

            _printerCombobox.SelectedIndex = __default;

            // set form
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();

            StringBuilder __query = new StringBuilder(MyLib._myGlobal._xmlHeader + "<node>");
            __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.formdesign._formname + ", " + _g.d.formdesign._formcode + " from " + _g.d.formdesign._table + " where coalesce(" + _g.d.formdesign._form_type + ", 0) in (0,3) "));

            //string __custName = (this._mode.Equals(0)) ? " (select " + _g.d.ap_supplier._name_1 + " from " + _g.d.ap_supplier._table + " where " + _g.d.ap_supplier._table + "." + _g.d.ap_supplier._code + " = " + _g.d.ap_ar_transport_label._table + "." + _g.d.ap_ar_transport_label._cust_code + ") as " + _g.d.ap_ar_transport_label._receivename :
            //    " (select " + _g.d.ar_customer._name_1 + " from " + _g.d.ar_customer._table + " where " + _g.d.ar_customer._table + "." + _g.d.ar_customer._code + " = " + _g.d.ap_ar_transport_label._table + "." + _g.d.ap_ar_transport_label._cust_code + ") as " + _g.d.ap_ar_transport_label._receivename;

            //__query.Append(MyLib._myUtil._convertTextToXmlForQuery("select *, " + __custName + " from " + _g.d.ap_ar_transport_label._table + " where roworder = " + this._addrId + ""));
            // group list
            if (this._mode == 0)
            {
                // group supplier
                __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ap_group._code + ", " + _g.d.ap_group._name_1 + " from " + _g.d.ap_group._table));
            }
            else
            {
                // group customer
                __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ar_group._code + ", " + _g.d.ar_group._name_1 + " from " + _g.d.ar_group._table));
            }
            __query.Append("</node>");

            ArrayList __queryResult = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __query.ToString());

            if (__queryResult.Count > 0)
            {

                DataSet __result = (DataSet)__queryResult[0]; //__myFrameWork._queryShort("select " + _g.d.formdesign._formname + ", " + _g.d.formdesign._formcode +" from " + _g.d.formdesign._table + "");
                //if (__result.Tables.Count > 0 && __result.Tables[0].Rows.Count > 0)
                //{
                //    //for (int __i = 0; __i < __result.Tables[0].Rows.Count; __i++)
                //    //{
                //    //    _formObject __obj = new _formObject();
                //    //    __obj.Name = __result.Tables[0].Rows[__i][_g.d.formdesign._formcode].ToString();
                //    //    __obj.Value = __result.Tables[0].Rows[__i][_g.d.formdesign._formname].ToString();

                //    //    this._formComboBox.Items.Add(__obj);
                //    //}

                //}

                this._formCombobox.DataSource = __result.Tables[0];
                this._formCombobox.DisplayMember = _g.d.formdesign._formname;
                this._formCombobox.ValueMember = _g.d.formdesign._formcode;

                // 
                DataSet __groupList = (DataSet)__queryResult[1];
                if (__groupList.Tables.Count > 0 && __groupList.Tables[0].Rows.Count > 0)
                {
                    //this._groupList.ComboBox.DataSource = __groupList.Tables[0];
                    //this._groupList.ComboBox.ValueMember= _g.d.ar_group._code;
                    //this._groupList.ComboBox.DisplayMember = _g.d.ar_group._name_1;
                    //this._groupList.ComboBox.SelectedItem = null;
                    for (int __row = 0; __row < __groupList.Tables[0].Rows.Count; __row++)
                    {
                        KeyValuePair<string, string> __item = new KeyValuePair<string, string>(__groupList.Tables[0].Rows[__row][_g.d.ar_group._code].ToString(), __groupList.Tables[0].Rows[__row][_g.d.ar_group._name_1].ToString());
                        this._groupList.ComboBox.Items.Add(__item);
                    }
                    this._groupList.ComboBox.DisplayMember = "Value";
                    this._groupList.ComboBox.ValueMember = "Key";
                    this._groupList.SelectedIndexChanged += _groupList_SelectedIndexChanged;
                }
            }
        }

        void _groupList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this._groupList.SelectedIndex != -1)
            {
                string __getGroupCode = ((KeyValuePair<string, string>)this._groupList.SelectedItem).Key;

                StringBuilder __query = new StringBuilder();
                __query.Append("select *, name_1 as " + _g.d.ap_ar_transport_label._receivename + ", 1 as " + _g.d.ap_ar_transport_label._label_count + "  from " + _g.d.ap_ar_transport_label._table);
                __query.Append(" where " + _g.d.ap_ar_transport_label._ar_ap_type + "=" + this._mode);
                if (this._mode == 0)
                {
                    __query.Append(" and exists (select " + _g.d.ap_supplier_detail._ap_code + " from " + _g.d.ap_supplier_detail._table + " where " + _g.d.ap_supplier_detail._group_main + "='" + __getGroupCode + "' and " + _g.d.ap_supplier_detail._table + "." + _g.d.ap_supplier_detail._ap_code + "=" + _g.d.ap_ar_transport_label._table + "." + _g.d.ap_ar_transport_label._cust_code + ") ");
                }
                else
                {
                    __query.Append(" and exists (select " + _g.d.ar_customer_detail._ar_code + " from " + _g.d.ar_customer_detail._table + " where " + _g.d.ar_customer_detail._group_main + "='" + __getGroupCode + "' and " + _g.d.ar_customer_detail._table + "." + _g.d.ar_customer_detail._ar_code + "=" + _g.d.ap_ar_transport_label._table + "." + _g.d.ap_ar_transport_label._cust_code + ") ");
                }

                MyLib._myFrameWork __myFramework = new MyLib._myFrameWork();
                DataSet __result = __myFramework._queryShort(__query.ToString());
                this._transport_labe_grid._loadFromDataTable(__result.Tables[0]);
            }
        }

        bool _myManageData__loadDataToScreen(object rowData, string whereString, bool forEdit)
        {
            bool __result = false;

            try
            {
                ArrayList __rowDataArray = (ArrayList)rowData;

                string __roworder = __rowDataArray[1].ToString();

                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();

                string __custName = (this._mode.Equals(0)) ?
                    " (select " + _g.d.ap_supplier._name_1 + " from " + _g.d.ap_supplier._table + " where " + _g.d.ap_supplier._table + "." + _g.d.ap_supplier._code + " = " + _g.d.ap_ar_transport_label._table + "." + _g.d.ap_ar_transport_label._cust_code + ") as " + _g.d.ap_ar_transport_label._receivename :
                    " (select " + _g.d.ar_customer._name_1 + " from " + _g.d.ar_customer._table + " where " + _g.d.ar_customer._table + "." + _g.d.ar_customer._code + " = " + _g.d.ap_ar_transport_label._table + "." + _g.d.ap_ar_transport_label._cust_code + ") as " + _g.d.ap_ar_transport_label._receivename;

                string __query = "select *, " + __custName + " from " + _g.d.ap_ar_transport_label._table + " where roworder = " + __roworder + "";

                DataSet __dataResult = __myFrameWork._queryShort(__query);

                if (__dataResult.Tables.Count > 0 && __dataResult.Tables[0].Rows.Count > 0)
                {
                    bool __pass = true;
                    int __addr = -1;
                    // check old

                    //for (int __i = 0; __i < this._transport_labe_grid._rowData.Count; __i++)
                    //{
                    //    if (this._transport_labe_grid._cellGet(__i, _g.d.ap_ar_transport_label._cust_code).ToString().Equals(__dataResult.Tables[0].Rows[0][_g.d.ap_ar_transport_label._cust_code].ToString()))
                    //    {
                    //        __addr = __i;
                    //        __pass = false;
                    //        break;
                    //    }
                    //}

                    if (__pass)
                    {
                        // add row
                        __addr = this._transport_labe_grid._addRow();

                        // update cell value
                        this._transport_labe_grid._cellUpdate(__addr, _g.d.ap_ar_transport_label._cust_code, __dataResult.Tables[0].Rows[0][_g.d.ap_ar_transport_label._cust_code].ToString(), false);
                        this._transport_labe_grid._cellUpdate(__addr, _g.d.ap_ar_transport_label._receivename, __dataResult.Tables[0].Rows[0][_g.d.ap_ar_transport_label._receivename].ToString(), false);
                        this._transport_labe_grid._cellUpdate(__addr, _g.d.ap_ar_transport_label._name_1, __dataResult.Tables[0].Rows[0][_g.d.ap_ar_transport_label._name_1].ToString(), false);
                        this._transport_labe_grid._cellUpdate(__addr, _g.d.ap_ar_transport_label._address, __dataResult.Tables[0].Rows[0][_g.d.ap_ar_transport_label._address].ToString(), false);
                        this._transport_labe_grid._cellUpdate(__addr, _g.d.ap_ar_transport_label._telephone, __dataResult.Tables[0].Rows[0][_g.d.ap_ar_transport_label._telephone].ToString(), false);
                        this._transport_labe_grid._cellUpdate(__addr, _g.d.ap_ar_transport_label._fax, __dataResult.Tables[0].Rows[0][_g.d.ap_ar_transport_label._fax].ToString(), false);
                        this._transport_labe_grid._cellUpdate(__addr, _g.d.ap_ar_transport_label._email, __dataResult.Tables[0].Rows[0][_g.d.ap_ar_transport_label._email].ToString(), false);
                        this._transport_labe_grid._cellUpdate(__addr, _g.d.ap_ar_transport_label._label_count, 1, false);
                    }
                    else
                    {
                        // update row cellget + 1
                        //if (MessageBox.Show("มีการเลือกรายการนี้แล้ว ต้องการเพิ่มจำนวนหรือไม่", "ยืนยัน", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        //{
                        //    this._transport_labe_grid._cellUpdate(__addr, _g.d.ap_ar_transport_label._label_count, (int)this._transport_labe_grid._cellGet(__addr, _g.d.ap_ar_transport_label._label_count) + 1, false);
                        //}
                    }
                    __result = true;

                }
            }
            catch
            {
            }
            return __result;
        }

        void _myManageData__closeScreen()
        {
            this.Dispose();
        }

        private void _closeButton_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void _previewButton_Click(object sender, EventArgs e)
        {
            // call print
            if (this._printModeCheckbox.Checked == true)
            {
                _printLabel(true);
            }
            else
            {
                this._print(true);
            }
        }

        private void _printButton_Click(object sender, EventArgs e)
        {
            // call print
            if (this._printModeCheckbox.Checked == true)
            {
                _printLabel(false);
            }
            else
            {
                this._print(false);
            }
        }

        private void _clearButton_Click(object sender, EventArgs e)
        {
            this._transport_labe_grid._clear();
        }

        /// <summary>
        /// Concept 1 : 1
        /// </summary>
        /// <param name="__isPreview"></param>
        private void _print(Boolean __isPreview)
        {
            if (this._transport_labe_grid._rowData.Count > 0)
            {

                // print 
                string __formCode = (this._formCombobox.SelectedValue != null) ? this._formCombobox.SelectedValue.ToString() : "";


                if (__formCode.Length > 0)
                {
                    string __printerName = this._printerCombobox.SelectedItem.ToString();

                    List<_transportLabelObj> __objList = new List<_transportLabelObj>();
                    for (int __i = 0; __i < this._transport_labe_grid._rowData.Count; __i++)
                    {
                        int __countLabel = (int)this._transport_labe_grid._cellGet(__i, _g.d.ap_ar_transport_label._label_count);
                        for (int __count = 0; __count < __countLabel; __count++)
                        {

                            _transportLabelObj __label = new _transportLabelObj();
                            __label._custCode = this._transport_labe_grid._cellGet(__i, _g.d.ap_ar_transport_label._cust_code).ToString();
                            __label._custName = this._transport_labe_grid._cellGet(__i, _g.d.ap_ar_transport_label._receivename).ToString();
                            __label._receiveName = this._transport_labe_grid._cellGet(__i, _g.d.ap_ar_transport_label._name_1).ToString();
                            __label._address = this._transport_labe_grid._cellGet(__i, _g.d.ap_ar_transport_label._address).ToString();
                            __label._telephone = this._transport_labe_grid._cellGet(__i, _g.d.ap_ar_transport_label._telephone).ToString();
                            __label._fax = this._transport_labe_grid._cellGet(__i, _g.d.ap_ar_transport_label._fax).ToString();
                            __label._email = this._transport_labe_grid._cellGet(__i, _g.d.ap_ar_transport_label._email).ToString();
                            __label._page = __count + 1;
                            __label._total = __countLabel;
                            __objList.Add(__label);
                        }
                    }
                    _labelPrintClass __printClass = new _labelPrintClass();
                    __printClass._print(__isPreview, __formCode, __printerName, __objList);

                    /*
                    SMLReport._formReport._formDesigner __form = new _formDesigner();

                    // get form cache 
                    string __currentConfigFileName = string.Format("_cache-{0}-{1}-{2}-{3}.xml", MyLib._myGlobal._webServiceServer.Replace(".", "_").Replace(":", "__").Replace("/", string.Empty), MyLib._myGlobal._providerCode, MyLib._myGlobal._databaseName, __formCode); // "_cache + formCode + ".xml";
                    string __path = Path.GetTempPath() + "\\" + __currentConfigFileName.ToLower();
                    string __lastTimeUpdate = "";

                    // check cache  update version
                    bool _isCache = false;
                    try
                    {
                        // check xml 
                        StringBuilder __queryCheckCode = new StringBuilder(MyLib._myGlobal._xmlHeader + "<node>");
                        __queryCheckCode.Append(MyLib._myUtil._convertTextToXmlForQuery("SELECT " + _g.d.formdesign._formcode + ", " + _g.d.formdesign._timeupdate + " FROM " + _g.d.formdesign._table + " WHERE " + MyLib._myGlobal._addUpper(_g.d.formdesign._formcode) + "=\'" + __formCode.ToUpper() + "\'"));
                        __queryCheckCode.Append("</node>");

                        MyLib._myFrameWork __ws = new MyLib._myFrameWork();
                        ArrayList __result = __ws._queryListGetData(MyLib._myGlobal._databaseName, __queryCheckCode.ToString());

                        DataTable __da = ((DataSet)__result[0]).Tables[0];
                        __lastTimeUpdate = __da.Rows[0]["timeupdate"].ToString();

                        SMLReport._formReport.SMLFormDesignXml __cacheXML = new SMLFormDesignXml();

                        TextReader readFile = new StreamReader(__path);
                        XmlSerializer __xsLoad = new XmlSerializer(typeof(SMLReport._formReport.SMLFormDesignXml));
                        __cacheXML = (SMLReport._formReport.SMLFormDesignXml)__xsLoad.Deserialize(readFile);
                        readFile.Close();

                        if (__lastTimeUpdate == __cacheXML._lastUpdate)
                        {
                            _isCache = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        _isCache = false;
                    }

                    // not exit get form and write cache
                    try
                    {
                        if (_isCache)
                        {
                            SMLReport._formReport.SMLFormDesignXml __cacheXML = new SMLFormDesignXml();
                            XmlSerializer __xsLoad = new XmlSerializer(typeof(SMLFormDesignXml));
                            FileStream __readFileStream = new FileStream(__path, FileMode.Open, FileAccess.Read, FileShare.Read);
                            __form._loadFromStream(__readFileStream, null, _openFormMethod.OpenFromServer);
                            __readFileStream.Close();

                        }
                        else
                        {
                            string __query = "SELECT " + _g.d.formdesign._formcode + "," + _g.d.formdesign._guid_code + "," + _g.d.formdesign._formname + "," + _g.d.formdesign._timeupdate + "," + _g.d.formdesign._formdesigntext + " FROM " + _g.d.formdesign._table + " WHERE " + MyLib._myGlobal._addUpper(_g.d.formdesign._formcode) + " =\'" + __formCode.ToUpper() + "\'";

                            MyLib._myFrameWork __ws = new MyLib._myFrameWork();
                            MyLib.SMLJAVAWS.formDesignType __formDesign = __ws._loadForm(MyLib._myGlobal._databaseName, __query);

                            try
                            {
                                // ลองดึงดู ถ้าข้อมูล Compress แล้ว ก็ผ่าน ถ้าไม่ ก็ไปดึงแบบเดิม
                                MemoryStream __ms = new MemoryStream(MyLib._compress._decompressBytes((byte[])__formDesign._formdesign));
                                __form._loadFromStream(__ms, null, _openFormMethod.OpenFromServer);
                                __ms.Close();
                            }
                            catch (Exception _ex_xmlcompress_1)
                            {
                                // กรณีที่ดึงของเก่าที่ไม่ได้ Compress
                                try
                                {
                                    MemoryStream __ms = new MemoryStream((byte[])__formDesign._formdesign);
                                    __form._loadFromStream(__ms, null, _openFormMethod.OpenFromServer);
                                    __ms.Close();
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.ToString());
                                }
                            }

                            // write cache
                            SMLFormDesignXml __formCacheXML = __form._writeXMLSource(_writeXMLSourceOption.DrawObjectOnly);
                            __formCacheXML._lastUpdate = __lastTimeUpdate;

                            XmlSerializer __colXs = new XmlSerializer(typeof(SMLFormDesignXml));
                            TextWriter __memoryStream = new StreamWriter(__path);
                            __colXs.Serialize(__memoryStream, __formCacheXML);
                            __memoryStream.Close();

                        }
                    }
                    catch (Exception __ex)
                    {
                        MessageBox.Show(__ex.Message.ToString());
                    }
                    // print 

                    List<_transportLabelObj> __objList = null;
                    if (__form != null)
                    {
                        //int __maxPage = __barCodeList.Count;

                        // pack label list
                        __objList = new List<_transportLabelObj>();
                        for (int __i = 0; __i < this._transport_labe_grid._rowData.Count; __i++)
                        {
                            int __countLabel = (int)this._transport_labe_grid._cellGet(__i, _g.d.ap_ar_transport_label._label_count);
                            for (int __count = 0; __count < __countLabel; __count++)
                            {

                                _transportLabelObj __label = new _transportLabelObj();
                                __label._custCode = this._transport_labe_grid._cellGet(__i, _g.d.ap_ar_transport_label._cust_code).ToString();
                                __label._custName = this._transport_labe_grid._cellGet(__i, _g.d.ap_ar_transport_label._receivename).ToString();
                                __label._receiveName = this._transport_labe_grid._cellGet(__i, _g.d.ap_ar_transport_label._name_1).ToString();
                                __label._address = this._transport_labe_grid._cellGet(__i, _g.d.ap_ar_transport_label._address).ToString();
                                __label._telephone = this._transport_labe_grid._cellGet(__i, _g.d.ap_ar_transport_label._telephone).ToString();
                                __label._fax = this._transport_labe_grid._cellGet(__i, _g.d.ap_ar_transport_label._fax).ToString();
                                __label._email = this._transport_labe_grid._cellGet(__i, _g.d.ap_ar_transport_label._email).ToString();

                                __objList.Add(__label);
                            }
                        }

                        float _startYPos = 0f;
                        float _startXPos = 0f;
                        float __paperWidth = 0f;
                        float __paperHeight = 0f;

                        String __paperSize = ((SMLReport._formReport._drawPaper)__form._paperList[0])._myPageSetup.PaperSize.ToString();
                        int __labelHeight = ((SMLReport._formReport._drawPaper)__form._paperList[0])._myPageSetup.PagePixel.Height;
                        int __labelWidth = ((SMLReport._formReport._drawPaper)__form._paperList[0])._myPageSetup.PagePixel.Width;

                        int __currentLabel = 0;
                        // get margin bound                

                        PrintPreviewDialog __preview = new PrintPreviewDialog();
                        PrintDocument __printDoc = new PrintDocument();
                        //__printDoc.PrinterSettings.PrinterName = "Canon LBP5050";
                        if (__printDoc.PrinterSettings.IsValid)
                        {

                            __printDoc.PrinterSettings.PrinterName = this._printerCombobox.SelectedItem.ToString();

                            __printDoc.DefaultPageSettings.Margins = new Margins(0, 0, 0, 0);
                            __printDoc.DefaultPageSettings.PaperSize =
                                                    __printDoc.DefaultPageSettings.PaperSize = new PaperSize(__paperSize, __labelWidth, __labelHeight);


                            int __currentPage = 0;
                            __printDoc.BeginPrint += (_beginPrintSender, _beginPrintEvent) =>
                            {
                                __currentPage = 0;
                                __currentLabel = 0;
                                _startYPos = 0f;
                                _startXPos = 0f;
                            };

                            __printDoc.PrintPage += (s1, e1) =>
                            {
                                e1.PageSettings.Margins.Top = 0;
                                e1.PageSettings.Margins.Left = 0;
                                e1.PageSettings.Margins.Bottom = 0;
                                e1.PageSettings.Margins.Right = 0;

                                _startYPos = 0f;
                                _startXPos = 0f;

                                //e1.Graphics.PageUnit = GraphicsUnit.Millimeter;
                                Graphics __g = e1.Graphics;
                                float __leftMargin = 0f;// (float)this._convertCentimeter((float)MyLib._myGlobal._decimalPhase(this._leftMarginTextBox.Text));
                                float __topMargin = 0f; // (float)this._convertCentimeter((float)MyLib._myGlobal._decimalPhase(this._topMarginTextBox.Text));

                                //__g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

                                __paperWidth = e1.PageBounds.Width;
                                __paperHeight = e1.PageBounds.Height;


                                // ยังไม่เสร็จ
                                _drawBarcodeLabelForm(e1.Graphics, ((SMLReport._formReport._drawPaper)__form._paperList[0]), __objList[__currentPage], new PointF(_startXPos, _startYPos));
                                if (__objList.Count - 1 > __currentPage)
                                {
                                    __currentPage++;
                                    e1.HasMorePages = true;
                                }
                                else
                                {
                                    __currentPage++;
                                    e1.HasMorePages = false;
                                }

                            };
                            if (__isPreview)
                            {
                                __preview.Document = __printDoc;
                                __preview.ShowDialog();
                            }
                            else
                            {
                                __printDoc.Print();
                            }

                        }
                        else
                        {
                            MessageBox.Show("Printer is invalid.");
                        }
                    }

                    //// save last print form
                    //_recentPrintLabel __recent = new _recentPrintLabel();
                    //__recent._printerName = this._printerCombo.SelectedItem.ToString();
                    //__recent._formCode = __formCode;

                    //// write xml

                    //try
                    //{
                    //    XmlSerializer __colXs = new XmlSerializer(typeof(_recentPrintLabel));
                    //    TextWriter __memoryStream = new StreamWriter(this._printrecentFileName);
                    //    __colXs.Serialize(__memoryStream, __recent);
                    //    __memoryStream.Close();
                    //}
                    //catch
                    //{
                    //}
                    */
                }
            }
        }

        private void _printLabel(Boolean isPreview)
        {
            if (this._transport_labe_grid._rowData.Count > 0)
            {

                // print 
                string __formCode = (this._formCombobox.SelectedValue != null) ? this._formCombobox.SelectedValue.ToString() : "";
                if (__formCode.Length > 0)
                {

                    string __printerName = this._printerCombobox.SelectedItem.ToString();
                    int __currentRow = (int)MyLib._myGlobal._decimalPhase(this._startRowTextbox.Text) - 1;
                    int __currentColumn = (int)MyLib._myGlobal._decimalPhase(this._startColumnTextbox.Text) - 1;

                    List<_transportLabelObj> __objList = new List<_transportLabelObj>();
                    for (int __i = 0; __i < this._transport_labe_grid._rowData.Count; __i++)
                    {
                        int __countLabel = (int)this._transport_labe_grid._cellGet(__i, _g.d.ap_ar_transport_label._label_count);
                        for (int __count = 0; __count < __countLabel; __count++)
                        {

                            _transportLabelObj __label = new _transportLabelObj();
                            __label._custCode = this._transport_labe_grid._cellGet(__i, _g.d.ap_ar_transport_label._cust_code).ToString();
                            __label._custName = this._transport_labe_grid._cellGet(__i, _g.d.ap_ar_transport_label._receivename).ToString();
                            __label._receiveName = this._transport_labe_grid._cellGet(__i, _g.d.ap_ar_transport_label._name_1).ToString();
                            __label._address = this._transport_labe_grid._cellGet(__i, _g.d.ap_ar_transport_label._address).ToString();
                            __label._telephone = this._transport_labe_grid._cellGet(__i, _g.d.ap_ar_transport_label._telephone).ToString();
                            __label._fax = this._transport_labe_grid._cellGet(__i, _g.d.ap_ar_transport_label._fax).ToString();
                            __label._email = this._transport_labe_grid._cellGet(__i, _g.d.ap_ar_transport_label._email).ToString();
                            __label._total = __countLabel;
                            __label._page = __count;
                            __objList.Add(__label);
                        }
                    }

                    _labelPrintClass __printClass = new _labelPrintClass();
                    __printClass._printLabel(isPreview, __formCode, __printerName, __currentRow, __currentColumn, __objList);
                }

                /*
                if (__formCode.Length > 0)
                {

                    SMLReport._formReport._formDesigner __form = new _formDesigner();

                    // get form cache 
                    string __currentConfigFileName = string.Format("_cache-{0}-{1}-{2}-{3}.xml", MyLib._myGlobal._webServiceServer.Replace(".", "_").Replace(":", "__").Replace("/", string.Empty), MyLib._myGlobal._providerCode, MyLib._myGlobal._databaseName, __formCode); // "_cache + formCode + ".xml";
                    string __path = Path.GetTempPath() + "\\" + __currentConfigFileName.ToLower();
                    string __lastTimeUpdate = "";

                    // check cache  update version
                    bool _isCache = false;
                    try
                    {
                        // check xml 
                        StringBuilder __queryCheckCode = new StringBuilder(MyLib._myGlobal._xmlHeader + "<node>");
                        __queryCheckCode.Append(MyLib._myUtil._convertTextToXmlForQuery("SELECT " + _g.d.formdesign._formcode + ", " + _g.d.formdesign._timeupdate + " FROM " + _g.d.formdesign._table + " WHERE " + MyLib._myGlobal._addUpper(_g.d.formdesign._formcode) + "=\'" + __formCode.ToUpper() + "\'"));
                        __queryCheckCode.Append("</node>");

                        MyLib._myFrameWork __ws = new MyLib._myFrameWork();
                        ArrayList __result = __ws._queryListGetData(MyLib._myGlobal._databaseName, __queryCheckCode.ToString());

                        DataTable __da = ((DataSet)__result[0]).Tables[0];
                        __lastTimeUpdate = __da.Rows[0]["timeupdate"].ToString();

                        SMLReport._formReport.SMLFormDesignXml __cacheXML = new SMLFormDesignXml();

                        TextReader readFile = new StreamReader(__path);
                        XmlSerializer __xsLoad = new XmlSerializer(typeof(SMLReport._formReport.SMLFormDesignXml));
                        __cacheXML = (SMLReport._formReport.SMLFormDesignXml)__xsLoad.Deserialize(readFile);
                        readFile.Close();

                        if (__lastTimeUpdate == __cacheXML._lastUpdate)
                        {
                            _isCache = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        _isCache = false;
                    }

                    // not exit get form and write cache
                    try
                    {
                        if (_isCache)
                        {
                            SMLReport._formReport.SMLFormDesignXml __cacheXML = new SMLFormDesignXml();
                            XmlSerializer __xsLoad = new XmlSerializer(typeof(SMLFormDesignXml));
                            FileStream __readFileStream = new FileStream(__path, FileMode.Open, FileAccess.Read, FileShare.Read);
                            __form._loadFromStream(__readFileStream, null, _openFormMethod.OpenFromServer);
                            __readFileStream.Close();

                        }
                        else
                        {
                            string __query = "SELECT " + _g.d.formdesign._formcode + "," + _g.d.formdesign._guid_code + "," + _g.d.formdesign._formname + "," + _g.d.formdesign._timeupdate + "," + _g.d.formdesign._formdesigntext + " FROM " + _g.d.formdesign._table + " WHERE " + MyLib._myGlobal._addUpper(_g.d.formdesign._formcode) + " =\'" + __formCode.ToUpper() + "\'";

                            MyLib._myFrameWork __ws = new MyLib._myFrameWork();
                            MyLib.SMLJAVAWS.formDesignType __formDesign = __ws._loadForm(MyLib._myGlobal._databaseName, __query);

                            try
                            {
                                // ลองดึงดู ถ้าข้อมูล Compress แล้ว ก็ผ่าน ถ้าไม่ ก็ไปดึงแบบเดิม
                                MemoryStream __ms = new MemoryStream(MyLib._compress._decompressBytes((byte[])__formDesign._formdesign));
                                __form._loadFromStream(__ms, null, _openFormMethod.OpenFromServer);
                                __ms.Close();
                            }
                            catch (Exception _ex_xmlcompress_1)
                            {
                                // กรณีที่ดึงของเก่าที่ไม่ได้ Compress
                                try
                                {
                                    MemoryStream __ms = new MemoryStream((byte[])__formDesign._formdesign);
                                    __form._loadFromStream(__ms, null, _openFormMethod.OpenFromServer);
                                    __ms.Close();
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.ToString());
                                }
                            }

                            // write cache
                            SMLFormDesignXml __formCacheXML = __form._writeXMLSource(_writeXMLSourceOption.DrawObjectOnly);
                            __formCacheXML._lastUpdate = __lastTimeUpdate;

                            XmlSerializer __colXs = new XmlSerializer(typeof(SMLFormDesignXml));
                            TextWriter __memoryStream = new StreamWriter(__path);
                            __colXs.Serialize(__memoryStream, __formCacheXML);
                            __memoryStream.Close();

                        }
                    }
                    catch (Exception __ex)
                    {
                        MessageBox.Show(__ex.Message.ToString());
                    }
                    // print 

                    List<_transportLabelObj> __objList = null;
                    if (__form != null)
                    {
                        //int __maxPage = __barCodeList.Count;

                        // pack label list
                        __objList = new List<_transportLabelObj>();
                        for (int __i = 0; __i < this._transport_labe_grid._rowData.Count; __i++)
                        {
                            int __countLabel = (int)this._transport_labe_grid._cellGet(__i, _g.d.ap_ar_transport_label._label_count);
                            for (int __count = 0; __count < __countLabel; __count++)
                            {

                                _transportLabelObj __label = new _transportLabelObj();
                                __label._custCode = this._transport_labe_grid._cellGet(__i, _g.d.ap_ar_transport_label._cust_code).ToString();
                                __label._custName = this._transport_labe_grid._cellGet(__i, _g.d.ap_ar_transport_label._receivename).ToString();
                                __label._receiveName = this._transport_labe_grid._cellGet(__i, _g.d.ap_ar_transport_label._name_1).ToString();
                                __label._address = this._transport_labe_grid._cellGet(__i, _g.d.ap_ar_transport_label._address).ToString();
                                __label._telephone = this._transport_labe_grid._cellGet(__i, _g.d.ap_ar_transport_label._telephone).ToString();
                                __label._fax = this._transport_labe_grid._cellGet(__i, _g.d.ap_ar_transport_label._fax).ToString();
                                __label._email = this._transport_labe_grid._cellGet(__i, _g.d.ap_ar_transport_label._email).ToString();

                                __objList.Add(__label);
                            }
                        }

                        float _startYPos = 0f;
                        float _startXPos = 0f;
                        float __paperWidth = 0f;
                        float __paperHeight = 0f;

                        String __paperSize = ((SMLReport._formReport._drawPaper)__form._paperList[0])._myPageSetup.PaperSize.ToString();
                        int __labelHeight = ((SMLReport._formReport._drawPaper)__form._paperList[0])._myPageSetup.PagePixel.Height;
                        int __labelWidth = ((SMLReport._formReport._drawPaper)__form._paperList[0])._myPageSetup.PagePixel.Width;

                        int __currentLabel = 0;
                        // get margin bound                

                        PrintPreviewDialog __preview = new PrintPreviewDialog();
                        PrintDocument __printDoc = new PrintDocument();
                        //__printDoc.PrinterSettings.PrinterName = "Canon LBP5050";
                        if (__printDoc.PrinterSettings.IsValid)
                        {

                            __printDoc.PrinterSettings.PrinterName = this._printerCombobox.SelectedItem.ToString();

                            __printDoc.DefaultPageSettings.Margins = new Margins(0, 0, 0, 0);
                            // ใช้ั auto __printDoc.DefaultPageSettings.PaperSize =__printDoc.DefaultPageSettings.PaperSize = new PaperSize(__paperSize, __labelWidth, __labelHeight);


                            int __currentPage = 0;
                            __printDoc.BeginPrint += (_beginPrintSender, _beginPrintEvent) =>
                            {
                                __currentPage = 0;
                                __currentLabel = 0;
                                _startYPos = 0f;
                                _startXPos = 0f;
                            };

                            __printDoc.PrintPage += (s1, e1) =>
                            {
                                e1.PageSettings.Margins.Top = 0;
                                e1.PageSettings.Margins.Left = 0;
                                e1.PageSettings.Margins.Bottom = 0;
                                e1.PageSettings.Margins.Right = 0;

                                _startYPos = 0f;
                                _startXPos = 0f;

                                //e1.Graphics.PageUnit = GraphicsUnit.Millimeter;
                                Graphics __g = e1.Graphics;
                                float __leftMargin = 0f; // (float)this._convertCentimeter((float)MyLib._myGlobal._decimalPhase(this._leftMarginTextBox.Text));
                                float __topMargin = 0f; // (float)this._convertCentimeter((float)MyLib._myGlobal._decimalPhase(this._topMarginTextBox.Text));

                                //__g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

                                __paperWidth = e1.PageBounds.Width;
                                __paperHeight = e1.PageBounds.Height;
                                if ((__leftMargin + __labelWidth) < __paperWidth)
                                {
                                    if (__currentPage == 0)
                                    {
                                        int __currentRow = (int)MyLib._myGlobal._decimalPhase(this._startRowTextbox.Text) - 1;
                                        int __currentColumn = (int)MyLib._myGlobal._decimalPhase(this._startColumnTextbox.Text) - 1;

                                        if (__currentRow != 0)
                                        {
                                            _startYPos += (__currentRow * __labelHeight);
                                        }

                                        if (__currentColumn != 0)
                                        {
                                            _startXPos += (__currentColumn * __labelWidth);
                                        }
                                    }

                                    if (__objList.Count > __currentLabel)
                                    {
                                        for (int __ibarcode = __currentLabel; __ibarcode < __objList.Count; __ibarcode++)
                                        {
                                            _transportLabelObj __barcode = __objList[__currentLabel];

                                            if ((_startXPos + __labelWidth) <= __paperWidth && (_startYPos + __labelHeight) <= __paperHeight)
                                            {
                                                _drawBarcodeLabelForm(e1.Graphics, ((SMLReport._formReport._drawPaper)__form._paperList[0]), __barcode, new PointF(_startXPos, _startYPos));
                                                __currentLabel++;
                                                _startXPos += __labelWidth;

                                            }
                                            else
                                            {
                                                if ((_startXPos + __labelWidth) > __paperWidth && (_startYPos + __labelHeight + __labelHeight) < __paperHeight) // ถ้าขึ้นบรรทัดใหม่ แล้วไม่ล้นหน้า
                                                {
                                                    // ขึ้นบรรทัดใหม่ ถ้าล้นหน้าขึ้นหน้าใหม่ไปเลย
                                                    _startXPos = 0;
                                                    _startXPos += __leftMargin;
                                                    _startYPos += __labelHeight;

                                                    _drawBarcodeLabelForm(e1.Graphics, ((SMLReport._formReport._drawPaper)__form._paperList[0]), __barcode, new PointF(_startXPos, _startYPos));
                                                    _startXPos += __labelWidth;
                                                    __currentLabel++;

                                                }
                                                else
                                                {
                                                    // ขึ้นหน้าใหม่
                                                    __currentPage++;
                                                    e1.HasMorePages = true;
                                                    break;
                                                }
                                            }

                                        }
                                    }

                                }
                                else
                                {
                                    e1.HasMorePages = false;
                                }

                                //// ยังไม่เสร็จ
                                //_drawBarcodeLabelForm(e1.Graphics, ((SMLReport._formReport._drawPaper)__form._paperList[0]), __objList[__currentPage], new PointF(_startXPos, _startYPos));
                                //if (__objList.Count - 1 > __currentPage)
                                //{
                                //    __currentPage++;
                                //    e1.HasMorePages = true;
                                //}
                                //else
                                //{
                                //    __currentPage++;
                                //    e1.HasMorePages = false;
                                //}

                            };
                            if (isPreview)
                            {
                                __preview.Document = __printDoc;
                                __preview.ShowDialog();
                            }
                            else
                            {
                                __printDoc.Print();
                            }

                        }
                        else
                        {
                            MessageBox.Show("Printer is invalid.");
                        }
                    }

                    //// save last print form
                    //_recentPrintLabel __recent = new _recentPrintLabel();
                    //__recent._printerName = this._printerCombo.SelectedItem.ToString();
                    //__recent._formCode = __formCode;

                    //// write xml

                    //try
                    //{
                    //    XmlSerializer __colXs = new XmlSerializer(typeof(_recentPrintLabel));
                    //    TextWriter __memoryStream = new StreamWriter(this._printrecentFileName);
                    //    __colXs.Serialize(__memoryStream, __recent);
                    //    __memoryStream.Close();
                    //}
                    //catch
                    //{
                    //}

                }*/
            }
        }

        /* ย้ายไป _labelPrintClass.cs
        void _drawBarcodeLabelForm(Graphics g, SMLReport._formReport._drawPaper paperStruct, _transportLabelObj label, PointF drawPoint)
        {
            foreach (Control __control1 in paperStruct.Controls)
            {
                if (__control1.GetType() == typeof(SMLReport._design._drawPanel))
                {
                    SMLReport._design._drawPanel __getControl = (SMLReport._design._drawPanel)__control1;
                    //Graphics __g = __getControl.CreateGraphics();
                    for (int __loop = __getControl._graphicsList._count - 1; __loop >= 0; __loop--)
                    {
                        // call print object
                        SMLReport._design._drawObject __obj = ((SMLReport._design._drawObject)__getControl._graphicsList[__loop]);

                        if (__obj.GetType() == typeof(_drawLabel))
                        {

                            _drawLabel __drawLabel = (_drawLabel)__obj;


                            Pen __pen = new Pen(__drawLabel._lineColor, __drawLabel._penWidth);
                            __pen.DashStyle = __drawLabel._lineStyle;
                            SolidBrush __brush = new SolidBrush(__drawLabel._foreColor);
                            SolidBrush __BgBrush = new SolidBrush(__drawLabel._backColor);
                            string __str = __drawLabel._text.Replace("&cust_code&", label._custCode).Replace("&cust_name&", label._custName).Replace("&receive_name&", label._receiveName).Replace("&address&", label._address).Replace("&telephone&", label._telephone).Replace("&fax&", label._fax).Replace("&email&", label._email);
                            RectangleF __LabelRect = __drawLabel._actualSize;
                            Font __newFont = new Font(__drawLabel._font.FontFamily, __drawLabel._font.Size, __drawLabel._font.Style, __drawLabel._font.Unit, __drawLabel._font.GdiCharSet, __drawLabel._font.GdiVerticalFont);

                            PointF __strDrawPoint = new PointF(drawPoint.X + __LabelRect.X, drawPoint.Y + __LabelRect.Y);

                            // จัดตำแนห่ง แล้วพิมพ์ ตัดบรรทัดด้วยนะ
                            ArrayList __getString = SMLReport._design._drawLabel._cutString(__str, __drawLabel._font, __LabelRect.Width, __drawLabel._charSpace, __drawLabel._charWidth, __drawLabel._padding);
                            SizeF __dataStrSize = SMLReport._formReport._formPrint._getTextSize(__getString, __drawLabel._font, g);
                            PointF __tmpPoint = SMLReport._formReport._formPrint._getPointTextAlingDraw(__LabelRect.Width, __LabelRect.Height, __dataStrSize.Width, __dataStrSize.Height, __drawLabel._textAlign, __drawLabel._padding);

                            //g.DrawString(__str, __newFont, __brush, __LabelRect.X + drawPoint.X, __LabelRect.Y + drawPoint.Y, StringFormat.GenericTypographic);

                            //__strDrawPoint.Y += __tmpPoint.Y;
                            //__strDrawPoint.X += __tmpPoint.X;


                            for (int __line = 0; __line < __getString.Count; __line++)
                            {
                                SizeF __strLineSize = SMLReport._formReport._formPrint._getTextSize((string)__getString[__line], __drawLabel._font, g);

                                PointF __getDrawPoint = SMLReport._formReport._formPrint._getPointTextAlingDraw(__LabelRect.Width, __LabelRect.Height, __strLineSize.Width, __strLineSize.Height, __drawLabel._textAlign, __drawLabel._padding);

                                g.DrawString((string)__getString[__line], __drawLabel._font, new SolidBrush(__drawLabel._foreColor), (__strDrawPoint.X + __getDrawPoint.X), __strDrawPoint.Y, StringFormat.GenericTypographic);

                                __strDrawPoint.Y += __strLineSize.Height;

                            }

                            //g.DrawString(__str, __newFont, __brush, __LabelRect.X + drawPoint.X, __LabelRect.Y + drawPoint.Y, StringFormat.GenericTypographic);

                        }
                        else if (__obj.GetType() == typeof(_drawImageField))
                        {
                            _drawImageField __imageField = (_drawImageField)__obj;
                            if (__imageField.FieldType == _FieldType.Barcode)
                            {
                                string __barcode = __imageField._showText.Replace("&cust_code&", label._custCode).Replace("&cust_name&", label._custName).Replace("&receive_name&", label._receiveName).Replace("&address&", label._address).Replace("&telephone&", label._telephone).Replace("&fax&", label._fax).Replace("&email&", label._email);
                                if (__imageField._typeBarcode == _barcodeType.BarCode_EAN13)
                                {
                                    try
                                    {
                                        SMLBarcodeManage.Ean13 __barcodeEan13 = new Ean13();
                                        Ean13Settings __setting = new Ean13Settings();
                                        //__setting.Font = __imageField._font;
                                        __setting.BarWidth = 4;
                                        //__setting.BarCodeHeight = __imageField._actualSize.Height;
                                        //__setting.BottomMargin = 20;

                                        __barcodeEan13._createEan13(__barcode, null, __setting);
                                        //__barcodeEan13._createEan13(barcode._barCode, null);
                                        Image __imgBarcode = __barcodeEan13.Paint();

                                        g.DrawImage(__imgBarcode, new RectangleF(__imageField.X + drawPoint.X, __imageField.Y + drawPoint.Y, __imageField._actualSize.Width, __imageField._actualSize.Height));

                                        //Image __image = __imageField._getBarcodeImage(barcode._barCode, Rectangle.Round(__imageField.Size).Size, __imageField._barcodeAlignment, __imageField._typeBarcode, __imageField._showBarcodeLabel, __imageField._barcodeLabelPosition, __imageField._font, __imageField.RotateFlip, __imageField._foreColor, __imageField._backColor);
                                        //g.DrawImage(__image, new RectangleF(__imageField.X + drawPoint.X, __imageField.Y + drawPoint.Y, __imageField._actualSize.Width, __imageField._actualSize.Height));

                                    }
                                    catch
                                    {

                                    }

                                }
                                else if (__imageField._typeBarcode == _barcodeType.BarCode_Code39)
                                {
                                    SMLBarcodeManage._createCode39 __code39 = new SMLBarcodeManage._createCode39();
                                    try
                                    {
                                        g.DrawImage(__code39._createBarCode(__barcode), new RectangleF(__imageField.X + drawPoint.X, __imageField.Y + drawPoint.Y, __imageField._actualSize.Width, __imageField._actualSize.Height));
                                    }
                                    catch
                                    {
                                    }

                                }
                                else if (__imageField._typeBarcode == _barcodeType.BarCode_QRCode)
                                {
                                    //QRCodeLib.QRCode __qrCode = new QRCodeLib.QRCode();
                                    //try
                                    //{
                                    //    g.DrawImage(__qrCode._createQRCode(barcode._barCode), new RectangleF(__imageField.X + drawPoint.X, __imageField.Y + drawPoint.Y, __imageField._actualSize.Width, __imageField._actualSize.Height));
                                    //}
                                    //catch
                                    //{
                                    //}

                                }
                                else if (__imageField._typeBarcode == _barcodeType.BarCode_UPCA)
                                {
                                    BarcoderLib.BarcodeUPCA __barcode = new BarcoderLib.BarcodeUPCA();

                                    try
                                    {
                                        g.DrawImage(__barcode.Encode(__barcode.Substring(0, 11)), new RectangleF(__imageField.X + drawPoint.X, __imageField.Y + drawPoint.Y, __imageField._actualSize.Width, __imageField._actualSize.Height));
                                    }
                                    catch
                                    {
                                    }

                                }
                            }
                        }
                        else if (__obj.GetType() == typeof(_drawImage))
                        {
                            _drawImage __drawImage = (_drawImage)__obj;

                            Pen __pen = new Pen(__drawImage._lineColor, __drawImage._penWidth);
                            SolidBrush __brush = new SolidBrush(__drawImage._backColor);


                            if (__drawImage.Image != null)
                            {

                                float __x = __drawImage._actualSize.X + drawPoint.X;
                                float __y = __drawImage._actualSize.Y + drawPoint.Y;
                                float __width = __drawImage._actualSize.Width;
                                float __height = __drawImage._actualSize.Height;

                                // get Image from sizeType


                                Image __newImage = SMLReport._design._drawImage._SizeModeImg(__drawImage._actualSize, __drawImage.Image, __drawImage.SizeMode);

                                if (__drawImage.BorderStyle != SMLReport._design._drawImage.ImageBorderStyleType.None)
                                {
                                    if (__drawImage._penWidth < 1)
                                    {
                                        __drawImage._penWidth = 1;
                                    }
                                    if (__drawImage.BorderStyle == SMLReport._design._drawImage.ImageBorderStyleType.Line)
                                    {
                                        int __newLineWidth = __drawImage._penWidth / 2;
                                        if (__newLineWidth == 0)
                                        {
                                            __newLineWidth = 1;
                                        }
                                        __x += __newLineWidth;
                                        __y += __newLineWidth;
                                        __width -= __newLineWidth;
                                        __height -= __newLineWidth;
                                    }
                                    Pen __newPen = new Pen(__drawImage._lineColor, __drawImage._penWidth);

                                    //g.DrawImage(__newImage, __x, __y, __width, __height);
                                    g.DrawRectangle(__newPen, __drawImage._actualSize);

                                }

                                RectangleF __tmpNewRect = new RectangleF(__x, __y, __width, __height);

                                g.DrawImage(__newImage, __tmpNewRect);
                                //g.DrawRectangle(pen, _drawRectangle._getNormalizedRectangle(__tmpNewRect));
                            }
                            else
                            {
                                g.FillRectangle(__brush, __drawImage._actualSize);
                            }

                            __pen.DashStyle = __drawImage._LineStyle;
                            g.DrawRectangle(__pen, __drawImage._actualSize);

                            __pen.Dispose();
                            __brush.Dispose();
                        }
                        else if (__obj.GetType() == typeof(_drawLine))
                        {
                            _drawLine __drawLine = (_drawLine)__obj;

                            Pen __linePen = new Pen(__drawLine._lineColor, __drawLine._penWidth);
                            __linePen.DashStyle = __drawLine._LineStyle;

                            PointF __tmpStartPoint = __drawLine.StartPoint;
                            __tmpStartPoint.X += drawPoint.X;
                            __tmpStartPoint.Y += drawPoint.Y;

                            PointF __tmpEndPoint = __drawLine.EndPoint;
                            __tmpEndPoint.X += drawPoint.X;
                            __tmpEndPoint.Y += drawPoint.Y;

                            //e.DrawLine(__linePen, Point.Round(__tmpStartPoint), Point.Round(__tmpEndPoint));
                            //onDrawLine(e, __linePen, Point.Round(__tmpStartPoint), Point.Round(__tmpEndPoint));
                            g.DrawLine(__linePen, __tmpStartPoint, __tmpEndPoint);


                            __linePen.Dispose();
                        }
                        else if (__obj.GetType() == typeof(_drawEllipse))
                        {
                            _drawEllipse __drawEllipse = (_drawEllipse)__obj;
                            Pen __lineEllipse = new Pen(__drawEllipse._lineColor, __drawEllipse._penWidth);
                            __lineEllipse.DashStyle = __drawEllipse._LineStyle;
                            SolidBrush __ellipseBG = new SolidBrush(__drawEllipse._backColor);

                            Rectangle __rect = new Rectangle(__drawEllipse._actualSize.X, __drawEllipse._actualSize.Y, __drawEllipse._actualSize.Width, __drawEllipse._actualSize.Height);

                            g.FillEllipse(__ellipseBG, __rect);
                            g.DrawEllipse(__lineEllipse, __rect);

                            __lineEllipse.Dispose();
                            __ellipseBG.Dispose();

                        }
                        else if (__obj.GetType() == typeof(_drawRectangle))
                        {
                            _drawRectangle __drawRect = (_drawRectangle)__obj;
                            Pen __rectPen = new Pen(__drawRect._lineColor, __drawRect._penWidth);
                            __rectPen.DashStyle = __drawRect._LineStyle;

                            SolidBrush __rectBrush = new SolidBrush(__drawRect._backColor);

                            RectangleF __rect = __drawRect._actualSize;
                            __rect.X += drawPoint.X;
                            __rect.Y += drawPoint.Y;

                            g.FillRectangle(__rectBrush, Rectangle.Round(__rect));
                            //e.DrawRectangle(__rectPen, Rectangle.Round(__rect));
                            g.DrawRectangle(__rectPen, Rectangle.Round(__rect));

                            __rectBrush.Dispose();
                            __rectPen.Dispose();

                        }
                        else if (__obj.GetType() == typeof(_drawRoundedRectangle))
                        {
                            _drawRoundedRectangle __drawRoundedRect = (_drawRoundedRectangle)__obj;

                            Pen __pen = new Pen(__drawRoundedRect._lineColor, __drawRoundedRect._penWidth);
                            __pen.DashStyle = __drawRoundedRect._LineStyle;
                            SolidBrush __brush = new SolidBrush(__drawRoundedRect._backColor);
                            GraphicsPath gfxPath = new GraphicsPath();

                            RectangleF __rect = __drawRoundedRect._actualSize;
                            __rect.X += drawPoint.X;
                            __rect.Y += drawPoint.Y;


                            gfxPath = __drawRoundedRect._getRectangleGraphic(Rectangle.Round(__rect));

                            g.FillPath(__brush, gfxPath);
                            g.DrawPath(__pen, gfxPath);

                            __pen.Dispose();
                            __brush.Dispose();
                        }
                    }
                }
            }
        }
        */
        private void _printModeCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            _enableRowColumnStart(this._printModeCheckbox.Checked);

        }

        public void _enableRowColumnStart(bool enabled)
        {
            _startColumnLabel.Enabled = enabled;
            _startRowLabel.Enabled = enabled;
            _startColumnTextbox.Enabled = enabled;
            _startRowTextbox.Enabled = enabled;
        }

        private float _convertCentimeter(float centimeter)
        {
            return centimeter * 10F;
        }
    }
}
