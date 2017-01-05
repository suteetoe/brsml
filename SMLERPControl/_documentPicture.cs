using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace SMLERPControl
{
    public partial class _documentPicture : UserControl
    {
        _g.g._transSystemEnum _transTypeTemp = _g.g._transSystemEnum.ว่าง;

        public _g.g._transSystemEnum _transType
        {
            get
            {
                return _transTypeTemp;
            }

            set
            {
                _transTypeTemp = value;
                _build();
            }
        }

        void _build()
        {
            if (DesignMode == false)
            {
                if (this._transTypeTemp != _g.g._transSystemEnum.ว่าง)
                {
                    string __screen_code_list = "";
                    switch (this._transTypeTemp)
                    {
                        case _g.g._transSystemEnum.สินค้า:
                            {
                                __screen_code_list = "\'IB\',\'IF\',\'IO\',\'IR\',\'IM\',\'CO\',\'IA\',\'IS\',\'IFC\',\'IOC\',\'IRC\',\'IMC\',\'IAC\',\'ISC\'";
                            }
                            break;
                        case _g.g._transSystemEnum.ซื้อ:
                            {
                                __screen_code_list = "\'PR\',\'PRA\',\'PRC\',\'PO\',\'POA\',\'POC\',\'PD\',\'PDR\',\'PDC\',\'PDRC\',\'PC\',\'PCR\',\'PCC\',\'PRT\',\'PU\',\'PT\',\'PA\',\'PUC\',\'PTC\',\'PAC\',\'PI\',\'PIA\',\'PIU\',\'PIC\',\'PID\'";
                            }
                            break;
                        case _g.g._transSystemEnum.ขาย:
                            {
                                __screen_code_list = "\'SO\',\'SOA\',\'SOC\',\'SR\',\'SRA\',\'SRC\',\'SS\',\'SSA\',\'SSC\',\'SD\',\'SDR\',\'SDC\',\'SDRC\',\'SRV\',\'SRT\',\'SCR\',\'SCT\',\'SI\',\'ST\',\'SA\',\'SIC\',\'SAC\',\'STC\',\'SIP\'";
                            }
                            break;
                        case _g.g._transSystemEnum.เจ้าหนี้:
                            {
                                __screen_code_list = "\'DA\',\'DC\',\'DB\',\'COB\',\'CCO\',\'CDO\',\'COC\',\'CNO\',\'CIC\',\'DD\',\'CPD\',\'DE\',\'CWO\',\'DDC\',\'CCP\',\'DEC\',\'CWC\'";
                            }
                            break;
                        case _g.g._transSystemEnum.ลูกหนี้:
                            {
                                __screen_code_list = "\'POB\',\'EA\',\'EC\',\'EB\',\'AOB\',\'ACO\',\'ADO\',\'AOC\',\'ADC\',\'AIC\',\'AAS\',\'ED\',\'ART\',\'EE\',\'AWO\',\'EDC\',\'ATC\',\'EEC\',\'AWC\'";
                            }
                            break;
                        case _g.g._transSystemEnum.เงินสดธนาคาร:
                            {
                                __screen_code_list = "\'OI\',\'OCN\',\'ODN\',\'OIC\',\'OCC\',\'ODC\',\'EPO\',\'EPC\',\'EPD\',\'EOC\',\'ECC\',\'EDC\',\'PC\',\'PCR\',\'PCD\',\'PRM\',\'PRC\',\'PDC\',\'PMC\',\'DM\',\'WM\',\'CCC\',\'CCP\',\'CDC\',\'CDE\',\'CEC\',\'CED\',\'CEP\',\'CHC\',\'CHD\',\'CHN\',\'CHP\',\'CNC\',\'CP\',\'CPC\',\'CPE\',\'CPP\',\'CPR\',\'CR\',\'CRC\',\'CRD\',\'CRN\',\'CRT\',\'CSR\',\'CTC\',\'DMC\',\'IBC\',\'IEC\',\'IRC\',\'TMC\',\'TOC\',\'WMC\',\'CPB\',\'CHB\'";
                            }
                            break;
                        case _g.g._transSystemEnum.บัญชี:
                            {
                                __screen_code_list = "";
                            }
                            break;

                    }

                    MyLib._myFrameWork __myFrameWorkWareHouse = new MyLib._myFrameWork();
                    StringBuilder __query = new StringBuilder(MyLib._myGlobal._xmlHeader + "<node>");
                    __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.erp_doc_format._code + "," + _g.d.erp_doc_format._name_1 + "," + _g.d.erp_doc_format._screen_code + " from " + _g.d.erp_doc_format._table));
                    __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.erp_doc_format._code + "," + _g.d.erp_doc_format._name_1 + " from " + _g.d.erp_doc_format._table + " where " + _g.d.erp_doc_format._screen_code + " in (" + __screen_code_list + ") order by " + _g.d.erp_doc_format._screen_code));
                    __query.Append("</node>");

                    ArrayList __result = __myFrameWorkWareHouse._queryListGetData(MyLib._myGlobal._databaseName, __query.ToString());

                    DataSet __ds1 = (DataSet)__result[0];
                    DataSet __getWareHouse = (DataSet)__result[1];

                    if (__ds1.Tables.Count > 0)
                    {
                        _docFormatList = __ds1.Tables[0];
                    }

                    if (__getWareHouse.Tables.Count > 0)
                    {
                        for (int __row = 0; __row < __getWareHouse.Tables[0].Rows.Count; __row++)
                        {
                            __selectWareHouse.Items.Add(__getWareHouse.Tables[0].Rows[__row][0].ToString() + "," + __getWareHouse.Tables[0].Rows[__row][1].ToString());
                        }
                    }
                }
            }
        }

        MyLib._myComboBox __selectWareHouse = new MyLib._myComboBox();
        public _documentPicture()
        {
            InitializeComponent();
            if (MyLib._myGlobal._isDesignMode == false)
            {
                this._myToolBar.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            }

            this._getPicture1._Tablename = _g.d.sml_doc_images._table;

            // toe full size
            //this._getPicture1.panel1.AutoScroll = true;
            //this._getPicture1._pictureZoom.SizeMode = PictureBoxSizeMode.AutoSize;

            _myManageData1._displayMode = 0;
            _myManageData1._dataList._fullMode = false;
            _myManageData1._dataList._lockRecord = true;
            _myManageData1._selectDisplayMode(this._myManageData1._displayMode);
            _myManageData1._loadDataToScreen += new MyLib.LoadDataToScreen(_myManageData1__loadDataToScreen);

            _myManageData1._manageButton = this._myToolBar;

            this._myManageData1._topPanel.BorderStyle = BorderStyle.FixedSingle;
            this._myManageData1._topPanel.Padding = new Padding(5, 5, 5, 5);

            MyLib._myLabel __selectWareHouseLabel = new MyLib._myLabel();
            this._myManageData1._topPanel.Controls.Add(__selectWareHouseLabel);
            __selectWareHouseLabel.ResourceName = "ประเภทเอกสาร";
            __selectWareHouseLabel.TextAlign = ContentAlignment.BottomRight;
            __selectWareHouseLabel.Invalidate();

            this._myManageData1._topPanel.Controls.Add(__selectWareHouse);
            MyLib._myFrameWork __myFrameWorkWareHouse = new MyLib._myFrameWork();

            // get doc format list
            //DataSet __getWareHouse = __myFrameWorkWareHouse._query(MyLib._myGlobal._databaseName, "select " + _g.d.ic_warehouse._code + "," + _g.d.ic_warehouse._name_1 + " from " + _g.d.ic_warehouse._table + " order by " + _g.d.ic_warehouse._code);


            __selectWareHouse.DropDownStyle = ComboBoxStyle.DropDownList;
            __selectWareHouse.Width = 300;
            //__selectWareHouse.SelectedIndexChanged += new EventHandler(__selectWareHouse_SelectedIndexChanged);
            __selectWareHouse.SelectedIndexChanged += new EventHandler(__selectWareHouse_SelectedIndexChanged);
            __selectWareHouse.Items.Add(MyLib._myGlobal._resource("กรุณาเลือกประเภทเอกสาร"));
            __selectWareHouse.SelectedIndex = 0;
            //for (int __row = 0; __row < __getWareHouse.Tables[0].Rows.Count; __row++)
            //{
            //    __selectWareHouse.Items.Add(__getWareHouse.Tables[0].Rows[__row][0].ToString() + "," + __getWareHouse.Tables[0].Rows[__row][1].ToString());
            //}
        }

        bool _myManageData1__loadDataToScreen(object rowData, string whereString, bool forEdit)
        {
            try
            {
                MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();
                DataSet __getData = _myFrameWork._query(MyLib._myGlobal._databaseName, "select * from " + _myManageData1._dataList._tableName + whereString);
                this._documentPictureScreenTopControl1._loadData(__getData.Tables[0]);

                this._getPicture1._clearpic();
                string _codepic = this._documentPictureScreenTopControl1._getDataStr(_g.d.ic_trans._doc_no);
                string _codepic_ = _codepic.Replace("/", "").Trim();
                this._getPicture1._loadImage(_codepic_);

                if (_myToolBar.Enabled == false)
                {
                    this._getPicture1._setEnable(_myToolBar.Enabled);
                }
                else
                {
                    this._getPicture1._setEnable(_myToolBar.Enabled);
                }

                if (forEdit)
                {
                    _documentPictureScreenTopControl1._focusFirst();
                }
                return (true);
            }
            catch (Exception)
            {
            }
            return (false);
        }

        string _screen_code = "";
        public string _getViewFormat
        {
            get
            {

                string __doc_screen_code = "";

                if (_docFormatList != null)
                {

                    DataRow[] __rows = _docFormatList.Select(_g.d.erp_doc_format._code + "=\'" + _screen_code + "\'");
                    if (__rows.Length > 0)
                        __doc_screen_code = __rows[0][_g.d.erp_doc_format._screen_code].ToString();
                }

                switch (__doc_screen_code)
                {
                    case "DD":
                    case "DE":
                    case "ED":
                    case "EE":
                    case "DDC" :
                    case "DEC":
                    case "EDC":
                    case "EEC":
                        return "screen_ar_ap_doc";
                }
                return "screen_ic_doc";
            }
        }

        public string _getReferField
        {
            get
            {
                return _g.d.ic_trans._doc_no;
            }
        }

        DataTable _docFormatList;

        void __selectWareHouse_SelectedIndexChanged(object sender, EventArgs e)
        {
            // load data
            Control __p1 = ((Control)sender).Parent;
            Control __p2 = __p1.Parent;
            MyLib._myManageData __manageData = (MyLib._myManageData)__p2.Parent.Parent.Parent.Parent.Parent.Parent;
            SMLERPControl._documentPicture __manageMasterCodeFull = (SMLERPControl._documentPicture)((Control)__manageData).Parent;
            MyLib._myComboBox __comboBox = (MyLib._myComboBox)sender;
            if (__comboBox.SelectedIndex > 0)
            {
                __manageData._dataList.Enabled = true;
                __manageMasterCodeFull._myToolBar.Enabled = true;
                string[] __split = __comboBox.SelectedItem.ToString().Split(',');

                _screen_code = __split[0].ToString();

                //if (_docFormatList == null)
                //{
                //    MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                //    _docFormatList = __myFrameWork._queryShort("select " + _g.d.erp_doc_format._code + "," + _g.d.erp_doc_format._name_1 + "," + _g.d.erp_doc_format._screen_code + " from " + _g.d.erp_doc_format._table).Tables[0];
                //}

                /*
                __manageMasterCodeFull._extraInsertField = _g.d.ic_shelf._whcode + ",";
                __manageMasterCodeFull._extraInsertData = "\'" + __split[0].ToString() + "\',";
                __manageMasterCodeFull._extraUpdateQuery = _g.d.ic_shelf._whcode + "=\'" + __split[0].ToString() + "\',";
                __manageMasterCodeFull._extraUpdateWhere = "and " + _g.d.ic_shelf._table + "." + _g.d.ic_shelf._whcode + "=\'" + __split[0].ToString() + "\'";
                __manageData._dataList._extraWhere = _g.d.ic_shelf._table + "." + _g.d.ic_shelf._whcode + "=\'" + __split[0].ToString() + "\'";
                __manageData._dataList._loadViewData(0);
                 * */
            }
            else
            {
                __manageData._dataList.Enabled = false;
                __manageMasterCodeFull._myToolBar.Enabled = false;
            }

            if (_screen_code.Length > 0)
            {
                if (_myManageData1._dataList._screenCode != _getViewFormat)
                {
                    _myManageData1._dataList._gridData._clearGridColumn();
                    _myManageData1._dataList._getOrderBy = "";
                    _myManageData1._dataList._loadViewFormat(_getViewFormat, MyLib._myGlobal._userSearchScreenGroup, true);
                    _myManageData1._dataList._referFieldAdd(_getReferField, 1);
                }

                // load view data
                __manageData._dataList._extraWhere = "doc_format_code=\'" + _screen_code + "\'";
                __manageData._dataList._loadViewData(0);

            }

        }

        void _save_data()
        {
            string __result = "";
            if (_myManageData1._manageButton.Enabled)
            {
                string __getEmtry = this._documentPictureScreenTopControl1._checkEmtryField();
                if (__getEmtry.Length > 0)
                {
                    MyLib._myGlobal._displayWarning(2, __getEmtry);
                }
                else
                {
                    string _codepic = this._documentPictureScreenTopControl1._getDataStr(_g.d.ic_trans._doc_no);
                    string _codepic_ = _codepic.Replace("/", "").Trim();
                    if (_myManageData1._mode == 1)
                    {

                    }
                    else
                    {
                        __result = this._getPicture1._updateImage(_codepic_);
                    }

                    if (__result.Length == 0)
                    {
                        MyLib._myGlobal._displayWarning(1, null);
                        _documentPictureScreenTopControl1._isChange = false;
                        if (_myManageData1._mode == 1)
                        {
                            _myManageData1._afterInsertData();
                        }
                        else
                        {
                            _myManageData1._afterUpdateData();
                        }
                        _getPicture1._clearpic();
                        _getPicture1._setEnable(false);


                    }
                    else
                    {
                        MessageBox.Show(__result, "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void _saveButton_Click(object sender, EventArgs e)
        {
            _save_data();
        }

        private void _closeButton_Click(object sender, EventArgs e)
        {

        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            Keys keyCode = (Keys)(int)keyData & Keys.KeyCode;

            if (keyData == Keys.F12)
            {
                _save_data();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

    }

    
}
