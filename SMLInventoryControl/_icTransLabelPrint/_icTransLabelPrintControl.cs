using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace SMLInventoryControl
{
    public partial class _icTransLabelPrintControl : UserControl
    {
        MyLib._myComboBox __selectWareHouse = new MyLib._myComboBox();
        public _icTransLabelPrintControl()
        {
            InitializeComponent();
            if (MyLib._myGlobal._isDesignMode == false)
            {
                this._toolbar.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            }

            _myManageData._displayMode = 0;
            _myManageData._dataList._fullMode = false;
            _myManageData._dataList._lockRecord = true;
            _myManageData._selectDisplayMode(this._myManageData._displayMode);
            _myManageData._loadDataToScreen += _myManageData__loadDataToScreen;
            _myManageData._dataList._buttonClose.Click += _buttonClose_Click;

            this._myManageData._manageButton = this._toolbar;

            this._myManageData._topPanel.BorderStyle = BorderStyle.FixedSingle;
            this._myManageData._topPanel.Padding = new Padding(5, 5, 5, 5);

            MyLib._myLabel __selectWareHouseLabel = new MyLib._myLabel();
            __selectWareHouseLabel.ResourceName = "ประเภทเอกสาร : ";
            __selectWareHouseLabel.TextAlign = ContentAlignment.BottomRight;
            __selectWareHouseLabel.Invalidate();

            __selectWareHouse.DropDownStyle = ComboBoxStyle.DropDownList;
            __selectWareHouse.Width = 300;

            __selectWareHouse.SelectedIndexChanged += __selectWareHouse_SelectedIndexChanged;
            __selectWareHouse.Items.Add(MyLib._myGlobal._resource("กรุณาเลือกประเภทเอกสาร"));
            __selectWareHouse.SelectedIndex = 0;

            // get doc format list
            MyLib._myFrameWork __myFrameWorkWareHouse = new MyLib._myFrameWork();

            StringBuilder __screen_code_list = new StringBuilder();
            // สินค้า
            __screen_code_list.Append("\'IB\',\'IF\',\'IO\',\'IR\',\'IM\',\'CO\',\'IA\',\'IS\',\'IFC\',\'IOC\',\'IRC\',\'IMC\',\'IAC\',\'ISC\'");
            // ซื้อ
            __screen_code_list.Append(",\'PR\',\'PRA\',\'PRC\',\'PO\',\'POA\',\'POC\',\'PD\',\'PDR\',\'PDC\',\'PDRC\',\'PC\',\'PCR\',\'PCC\',\'PRT\',\'PU\',\'PT\',\'PA\',\'PUC\',\'PTC\',\'PAC\',\'PI\',\'PIA\',\'PIU\',\'PIC\',\'PID\'");
            // ขาย
            __screen_code_list.Append(",\'SO\',\'SOA\',\'SOC\',\'SR\',\'SRA\',\'SRC\',\'SS\',\'SSA\',\'SSC\',\'SD\',\'SDR\',\'SDC\',\'SDRC\',\'SRV\',\'SRT\',\'SCR\',\'SCT\',\'SI\',\'ST\',\'SA\',\'SIC\',\'SAC\',\'STC\',\'SIP\'");

            //DataSet __getDocFormat = __myFrameWorkWareHouse._query(MyLib._myGlobal._databaseName, "select " + _g.d.erp_doc_format._code + "," + _g.d.erp_doc_format._name_1 + " from " + _g.d.erp_doc_format._table + " where " + _g.d.erp_doc_format._screen_code + " in (" + __screen_code_list.ToString() + ") order by " + _g.d.erp_doc_format._code);
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

            for (int __row = 0; __row < __getWareHouse.Tables[0].Rows.Count; __row++)
            {
                __selectWareHouse.Items.Add(__getWareHouse.Tables[0].Rows[__row][0].ToString() + "," + __getWareHouse.Tables[0].Rows[__row][1].ToString());
            }

            this._myManageData._topPanel.Controls.Add(__selectWareHouseLabel);
            this._myManageData._topPanel.Controls.Add(__selectWareHouse);

        }

        void _buttonClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        bool _myManageData__loadDataToScreen(object rowData, string whereString, bool forEdit)
        {
            try
            {
                MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();
                DataSet __getData = _myFrameWork._query(MyLib._myGlobal._databaseName, "select * from " + _myManageData._dataList._tableName + whereString);

                ArrayList __rowDataArray = (ArrayList)rowData;
                int __columnDocNo = this._myManageData._dataList._gridData._findColumnByName(_myManageData._dataList._tableName + "." + _g.d.ic_trans._doc_no);

                string __docNo = __rowDataArray[__columnDocNo].ToString().ToUpper();

                StringBuilder __query = new StringBuilder(MyLib._myGlobal._xmlHeader + "<node>");

                __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.ic_trans._table + " where " + _g.d.ic_trans._doc_no + " =\'" + __docNo + "\' and " + _g.d.ic_trans._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(this._icTransType).ToString()));
                __query.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.ic_trans_detail._table + " where " + _g.d.ic_trans_detail._doc_no + " =\'" + __docNo + "\' and " + _g.d.ic_trans_detail._trans_flag + "=" + _g.g._transFlagGlobal._transFlag(this._icTransType).ToString()));

                __query.Append("</node>");

                ArrayList __result = _myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __query.ToString());

                if (__result.Count > 0)
                {
                    DataTable __data1 = ((DataSet)__result[0]).Tables[0];
                    DataTable __data2 = ((DataSet)__result[1]).Tables[0];

                    this._icTransLabelPrintScreen1._loadData(__data1);
                    this._icTransLabelPrintGrid1._loadFromDataTable(__data2);
                }
                //this._documentPictureScreenTopControl1._loadData(__getData.Tables[0]);

                //this._getPicture1._clearpic();
                //string _codepic = this._documentPictureScreenTopControl1._getDataStr(_g.d.ic_trans._doc_no);
                //string _codepic_ = _codepic.Replace("/", "").Trim();
                //this._getPicture1._loadImage(_codepic_);

                if (_toolbar.Enabled == false)
                {
                    //this._getPicture1._setEnable(_myToolBar.Enabled);
                }
                else
                {
                    //this._getPicture1._setEnable(_myToolBar.Enabled);
                }

                if (forEdit)
                {
                    //_documentPictureScreenTopControl1._focusFirst();
                }
                return (true);
            }
            catch (Exception)
            {
            }
            return false;
        }

        string _screen_code = "";
        _g.g._transControlTypeEnum _icTransType = _g.g._transControlTypeEnum.ว่าง;
        DataTable _docFormatList;

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
                    case "DDC":
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

        void __selectWareHouse_SelectedIndexChanged(object sender, EventArgs e)
        {
            MyLib._myComboBox __comboBox = (MyLib._myComboBox)sender;
            if (__comboBox.SelectedIndex > 0)
            {
                Control __p1 = ((Control)sender).Parent;
                if (__p1 != null)
                {
                    Control __p2 = __p1.Parent;
                    MyLib._myManageData __manageData = (MyLib._myManageData)__p2.Parent.Parent.Parent.Parent.Parent.Parent;
                    //SMLERPControl._documentPicture __manageMasterCodeFull = (SMLERPControl._documentPicture)((Control)__manageData).Parent;

                    if (__comboBox.SelectedIndex > 0)
                    {
                        __manageData._dataList.Enabled = true;
                        //__manageMasterCodeFull._myToolBar.Enabled = true;
                        string[] __split = __comboBox.SelectedItem.ToString().Split(',');

                        _screen_code = __split[0].ToString();

                    }
                    else
                    {
                        __manageData._dataList.Enabled = false;
                        //__manageMasterCodeFull._myToolBar.Enabled = false;
                    }

                    if (_screen_code.Length > 0)
                    {
                        if (_myManageData._dataList._screenCode != _getViewFormat)
                        {
                            _myManageData._dataList._gridData._clearGridColumn();
                            _myManageData._dataList._getOrderBy = "";
                            _myManageData._dataList._loadViewFormat(_getViewFormat, MyLib._myGlobal._userSearchScreenGroup, true);
                            _myManageData._dataList._referFieldAdd(_getReferField, 1);
                        }


                        MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();

                        DataTable __screenCodeTable = __myFrameWork._queryShort("select " + _g.d.erp_doc_format._screen_code + " from " + _g.d.erp_doc_format._table + " where " + _g.d.erp_doc_format._code + " = \'" + _screen_code + "\'").Tables[0];
                        if (__screenCodeTable.Rows.Count > 0)
                        {
                            string __getScreenCode = __screenCodeTable.Rows[0][0].ToString();
                            _icTransType = _g.g._transFlagGlobal._transFlagByScreenCode(__getScreenCode);
                        }

                        // load view data
                        __manageData._dataList._extraWhere = "doc_format_code=\'" + _screen_code + "\'";
                        __manageData._dataList._loadViewData(0);

                    }
                }

            }



        }
    }
}
