using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace SMLTransportLabel
{
    public partial class _transport_label : UserControl
    {
        string _oldCode = "";
        int _mode = 0;

        public _transport_label(int mode)
        {
            InitializeComponent();
            if (MyLib._myGlobal._isDesignMode == false)
            {
                this._myToolBar.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            }
            this._mode = mode;
            this._transport_screen_top1._mode = this._mode;

            this._myManageDetail._displayMode = 0;
            this._myManageDetail._dataList._lockRecord = true;
            this._myManageDetail._selectDisplayMode(this._myManageDetail._displayMode);
            this._myManageDetail._dataList._extraWhere = _g.d.ap_ar_transport_label._ar_ap_type + "=" + this._mode.ToString();
            //this._myManageDetail._dataList._columnFieldNameReplace += new MyLib.ColumnFieldNameReplaceEventHandler(_dataList__columnFieldNameReplace);

            this._myManageDetail._dataList._loadViewFormat(((this._mode == 0) ? "screen_ap_transport_label" : "screen_ar_transport_label"), MyLib._myGlobal._userSearchScreenGroup, true);

            this._myManageDetail._loadDataToScreen += new MyLib.LoadDataToScreen(_myManageDetail__loadDataToScreen);
            this._myManageDetail._dataList._deleteData += new MyLib.DeleteDataEventHandler(_dataList__deleteData);
            //this._myManageDetail._manageButton = this._myToolBar;
            this._myManageDetail._manageBackgroundPanel = this._myPanel;
            this._myManageDetail._newDataClick += new MyLib.NewDataEvent(_myManageDetail__newDataClick);
            this._myManageDetail._discardData += new MyLib.DiscardDataEvent(_myManageDetail__discardData);
            this._myManageDetail._clearData += new MyLib.ClearDataEvent(_myManageDetail__clearData);
            this._myManageDetail._closeScreen += new MyLib.CloseScreenEvent(_myManageDetail__closeScreen);
            this._myManageDetail._dataList._referFieldAdd("roworder", 4);
            this._myManageDetail._checkEditData += new MyLib.CheckEditDataEvent(_myManageDetail__checkEditData);
            this._myManageDetail._dataListOpen = true;
            this._myManageDetail._autoSize = true;
            this._myManageDetail._calcArea();
            //this._myManageDetail._autoSizeHeight = 450;
            //this._myManageDetail._dataList._loadViewData(0);
            //_getPicture1._setEnable(false);
            this._myManageDetail.Invalidate();
        }

        void _dataList__deleteData(ArrayList selectRowOrder)
        {
            MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();
            StringBuilder __myQuery = new StringBuilder();
            __myQuery.Append(MyLib._myGlobal._xmlHeader + "<node>");
            for (int __loop = 0; __loop < selectRowOrder.Count; __loop++)
            {
                MyLib._deleteDataType getData = (MyLib._deleteDataType)selectRowOrder[__loop];
                __myQuery.Append(string.Format(MyLib._myUtil._convertTextToXmlForQuery("delete from {0} {1}"), _myManageDetail._dataList._tableName, getData.whereString));
            } // for
            __myQuery.Append("</node>");
            string result = _myFrameWork._queryList(MyLib._myGlobal._databaseName, __myQuery.ToString());
            if (result.Length == 0)
            {
                MyLib._myGlobal._displayWarning(1, MyLib._myGlobal._resource("สำเร็จ"));
                _myManageDetail._dataList._refreshData();
            }
            else
            {
                MessageBox.Show(result, "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }



        //string _dataList__columnFieldNameReplace(string source)
        //{
        //    //throw new NotImplementedException();

        //    return "";
        //}

        bool _myManageDetail__loadDataToScreen(object rowData, string whereString, bool forEdit)
        {
            bool __result = false;
            try
            {
                ArrayList __rowDataArray = (ArrayList)rowData;
                StringBuilder __myquery = new StringBuilder();
                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();

                this._oldCode = __rowDataArray[1].ToString();
                //this._barcode = __rowDataArray[2].ToString();
                __myquery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.ap_ar_transport_label._table + " where roworder=" + _oldCode.ToUpper() + ""));
                __myquery.Append("</node>");
                ArrayList __getData = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());
                this._transport_screen_top1._loadData(((DataSet)__getData[0]).Tables[0]);
                this._detail_screen._loadData(((DataSet)__getData[0]).Tables[0]);
                this._detail_screen._search(false);

                //this._icmainScreenTop._search(true);
                //this._icmainScreenTop.Invalidate();
                //this._getPicture1._clearpic();
                //string _codepic = this._barcode; // this._icmainScreenTop._getDataStr(this._barcode);
                //string _codepic_ = _codepic.Replace("/", "").Trim();
                //this._getPicture1._loadImage(_codepic_);

                //if (_myToolBar.Enabled == false)
                //{
                //    this._getPicture1._setEnable(_myToolBar.Enabled);
                //}
                //else
                //{
                //    this._getPicture1._setEnable(_myToolBar.Enabled);
                //}

                this._saveButton.Enabled = this._myManageDetail._editMode;

                this._transport_screen_top1.Enabled = this._myManageDetail._editMode;
                this._detail_screen.Enabled = this._myManageDetail._editMode;

                __result = true;
            }
            catch (Exception __e)
            {
                MessageBox.Show(__e.Message);
            }
            return __result;
        }

        void _myManageDetail__closeScreen()
        {
            this.Dispose();
        }

        bool _myManageDetail__checkEditData(int row, MyLib._myGrid sender)
        {
            //this._saveButton.Enabled = true;
            return true;
        }

        void _myManageDetail__clearData()
        {
            this._transport_screen_top1._clear();
            this._detail_screen._clear();
            this._oldCode = "";
            this._saveButton.Enabled = false;
            this._transport_screen_top1.Enabled = false;
            this._detail_screen.Enabled = false;
            //this._saveButton.Enabled = true;
        }

        bool _myManageDetail__discardData()
        {
            return true;
        }

        void _myManageDetail__newDataClick()
        {
            this._transport_screen_top1._clear();
            this._detail_screen._clear();

            this._oldCode = "";
            this._saveButton.Enabled = true;
            this._transport_screen_top1.Enabled = true;
            this._detail_screen.Enabled = true;


            //this._saveButton.Enabled = true;
        }

        private void _saveButton_Click(object sender, EventArgs e)
        {
            _saveData();
        }

        private void _printButton_Click(object sender, EventArgs e)
        {
            if (this._oldCode.Length > 0)
            {
                _printLabelForm __print = new _printLabelForm(this._mode, this._oldCode);
                __print.ShowDialog();
            }
        }

        private void _closeButton_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        void _saveData()
        {
            try
            {
                string __getEmtry = this._transport_screen_top1._checkEmtryField();
                if (__getEmtry.Length > 0)
                {
                    MyLib._myGlobal._displayWarning(2, __getEmtry);
                }
                else
                {
                    StringBuilder __myQuery = new StringBuilder();
                    __myQuery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                    ArrayList __getScreen = this._transport_screen_top1._createQueryForDatabase();
                    ArrayList __getDetailData = this._detail_screen._createQueryForDatabase();

                    if (_myManageDetail._mode == 1)  // insert
                    {
                        __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + this._transport_screen_top1._table_name + "(" + __getScreen[0] + "," + __getDetailData[0] + "," + _g.d.ap_ar_transport_label._ar_ap_type + ") values (" + __getScreen[1] + "," + __getDetailData[1] + "," + this._mode + ")"));
                    }
                    else // update
                    {
                        __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + this._transport_screen_top1._table_name + " set " + __getScreen[2] + "," + __getDetailData[2] + " where roworder =" + this._oldCode));
                    }
                    __myQuery.Append("</node>");
                    MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                    string __result = __myFrameWork._queryList(MyLib._myGlobal._databaseName, __myQuery.ToString());
                    if (__result.Length == 0)
                    {
                        //
                        MyLib._myGlobal._displayWarning(1, null);
                        if (this._myManageDetail._mode == 1)
                        {
                            this._myManageDetail._afterInsertData();

                        }
                        else
                        {
                            this._myManageDetail._afterUpdateData();
                        }
                        this._saveButton.Enabled = false;
                        this._transport_screen_top1.Enabled = false;
                        this._detail_screen.Enabled = false;


                    }
                    else
                    {
                        MessageBox.Show(__result, "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception __e)
            {
                MessageBox.Show(__e.Message);
            }

        }
    }
}
