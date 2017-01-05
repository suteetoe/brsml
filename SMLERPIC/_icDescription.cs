using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace SMLERPIC
{
    public partial class _icDescription : UserControl
    {
        string _oldCode = "";

        public _icDescription()
        {
            InitializeComponent();
            if (MyLib._myGlobal._isDesignMode == false)
            {
                this._myToolBar.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            }

            this.SuspendLayout();
            this._myManageDetail._displayMode = 0;
            this._myManageDetail._dataList._lockRecord = true;
            this._myManageDetail._selectDisplayMode(this._myManageDetail._displayMode);
            this._myManageDetail._dataList._columnFieldNameReplace += new MyLib.ColumnFieldNameReplaceEventHandler(_dataList__columnFieldNameReplace);
            this._myManageDetail._dataList._loadViewFormat(_g.g._search_screen_ic_inventory, MyLib._myGlobal._userSearchScreenGroup, true);
            this._myManageDetail._loadDataToScreen += new MyLib.LoadDataToScreen(_myManageDetail__loadDataToScreen);
            // this._myManageDetail._dataList._deleteData += new MyLib.DeleteDataEventHandler(_dataList__deleteData);
            this._myManageDetail._manageButton = this._myToolBar;
            this._myManageDetail._manageBackgroundPanel = this._myPanel1;
            this._myManageDetail._newDataClick += new MyLib.NewDataEvent(_myManageDetail__newDataClick);
            this._myManageDetail._discardData += new MyLib.DiscardDataEvent(_myManageDetail__discardData);
            this._myManageDetail._clearData += new MyLib.ClearDataEvent(_myManageDetail__clearData);
            this._myManageDetail._closeScreen += new MyLib.CloseScreenEvent(_myManageDetail__closeScreen);
            this._myManageDetail._dataList._referFieldAdd(_g.d.ic_inventory._code, 1);
            this._myManageDetail._checkEditData += new MyLib.CheckEditDataEvent(_myManageDetail__checkEditData);
            this._myManageDetail._dataListOpen = true;
            this._myManageDetail._autoSize = true;
            this._myManageDetail._calcArea();
            this._myManageDetail._autoSizeHeight = 450;
            //this._myManageDetail._dataList._loadViewData(0);
            //_getPicture1._setEnable(false);
            this._myManageDetail.Invalidate();
            this._saveButton.Click += new EventHandler(_saveButton_Click);
            this._icmainScreenTop.Enabled = false;
            this._htmlwysiwyg._previewButton.Click += new EventHandler(_previewButton_Click);

            this.ResumeLayout(false);

        }

        void _previewButton_Click(object sender, EventArgs e)
        {
            string __html = this._htmlwysiwyg._getHTML();

            Form __previewForm = new Form();
            WebBrowser __browser = new WebBrowser();
            __browser.Dock = DockStyle.Fill;
            __browser.DocumentText = __html;
            __previewForm.Controls.Add(__browser);

            __previewForm.WindowState = FormWindowState.Maximized;
            __previewForm.ShowDialog();
        }

        string _dataList__columnFieldNameReplace(string source)
        {
            return source.Replace("sml_value_branch_code", MyLib._myGlobal._branchCode);
        }

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
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.ic_inventory._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_inventory._code) + "=\'" + _oldCode.ToUpper() + "\'"));
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ic_description._description + " from " + _g.d.ic_description._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_description._ic_code) + "=\'" + _oldCode.ToUpper() + "\'"));
                __myquery.Append("</node>");
                ArrayList __getData = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());
                this._icmainScreenTop._loadData(((DataSet)__getData[0]).Tables[0]);
                this._icmainScreenTop._search(true);
                this._icmainScreenTop.Invalidate();

                string __html = "";
                if (((DataSet)__getData[1]).Tables[0].Rows.Count > 0)
                {
                    __html = ((DataSet)__getData[1]).Tables[0].Rows[0][_g.d.ic_description._description].ToString();
                }
                this._htmlwysiwyg._setHTML(__html);

                if (_myToolBar.Enabled == false)
                {
                    this._htmlwysiwyg.Enabled = _myToolBar.Enabled;
                }
                else
                {
                    this._htmlwysiwyg.Enabled = _myToolBar.Enabled;
                }
               
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

                __result = true;
            }
            catch (Exception __e)
            {
                MessageBox.Show(__e.Message);
            }
            return __result;
        }

        void _myManageDetail__newDataClick()
        {
            this._icmainScreenTop._clear();
            this._myManageDetail._dataList._refreshData();
        }

        bool _myManageDetail__discardData()
        {
            return true;
        }

        void _myManageDetail__clearData()
        {

        }

        void _myManageDetail__closeScreen()
        {
            this.Dispose();
        }

        bool _myManageDetail__checkEditData(int row, MyLib._myGrid sender)
        {
            int __itemCodeColumnNumber = sender._findColumnByName(_g.d.ic_inventory._table + "." + _g.d.ic_inventory._code);
            string __itemCode = sender._cellGet(row, __itemCodeColumnNumber).ToString();
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            DataTable __getData = __myFrameWork._queryShort("select " + _g.d.ic_inventory._update_detail + " from " + _g.d.ic_inventory._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_inventory._code) + "=\'" + __itemCode.ToUpper() + "\'").Tables[0];
            int __updateDetail = MyLib._myGlobal._intPhase(__getData.Rows[0][0].ToString());
            return (__updateDetail == 1) ? true : false;
        }

        void _saveButton_Click(object sender, EventArgs e)
        {
            _save_Data();
        }

        public void _save_Data()
        {
            if (MyLib._myGlobal._checkChangeMaster())
            {
                string __result = "";

                StringBuilder __query = new StringBuilder(MyLib._myGlobal._xmlHeader + "<node>");
                __query.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.ic_description._table + " where " + _g.d.ic_description._ic_code + " = \'" + _oldCode + "\'"));

                string __htmlDebug = this._htmlwysiwyg._getHTML();
                if (__htmlDebug == null)
                    __htmlDebug = "";
                string __insQuery = "insert into " + _g.d.ic_description._table + "(" + _g.d.ic_description._ic_code + "," + _g.d.ic_description._description + ") values(\'" + this._icmainScreenTop._getDataStr(_g.d.ic_inventory._code) + "\', \'" + MyLib._myGlobal._convertStrToQuery(__htmlDebug) + "\')";
                __query.Append(MyLib._myUtil._convertTextToXmlForQuery(__insQuery));
                __query.Append("</node>");


                if (this._myManageDetail._mode == 1)
                {

                }
                else
                {
                    MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                    string result = __myFrameWork._queryList(MyLib._myGlobal._databaseName, __query.ToString());
                    if (result.Length == 0)
                    {
                        MyLib._myGlobal._displayWarning(1, null);

                        _icmainScreenTop._isChange = false;
                        if (_myManageDetail._mode == 1)
                        {
                            _myManageDetail._afterInsertData();
                        }
                        else
                        {
                            _myManageDetail._afterUpdateData();
                        }


                    }
                    else
                    {
                        MessageBox.Show(result, "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

            }
        }

        private void _closeButton_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
