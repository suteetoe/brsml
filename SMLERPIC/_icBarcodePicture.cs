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
    public partial class _icBarcodePicture : UserControl
    {
        string _oldCode = "";
        string _barcode = "";
        public _icBarcodePicture()
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
            this._myManageDetail._dataList._loadViewFormat(_g.g._search_screen_ic_inventory_barcode, MyLib._myGlobal._userSearchScreenGroup, true);
            this._myManageDetail._loadDataToScreen += new MyLib.LoadDataToScreen(_myManageDetail__loadDataToScreen);
            this._myManageDetail._dataList._deleteData += new MyLib.DeleteDataEventHandler(_dataList__deleteData);
            this._myManageDetail._manageButton = this._myToolBar;
            this._myManageDetail._manageBackgroundPanel = this._myPanel1;
            this._myManageDetail._newDataClick += new MyLib.NewDataEvent(_myManageDetail__newDataClick);
            this._myManageDetail._discardData += new MyLib.DiscardDataEvent(_myManageDetail__discardData);
            this._myManageDetail._clearData += new MyLib.ClearDataEvent(_myManageDetail__clearData);
            this._myManageDetail._closeScreen += new MyLib.CloseScreenEvent(_myManageDetail__closeScreen);
            this._myManageDetail._dataList._referFieldAdd(_g.d.ic_inventory_barcode._barcode, 1);
            this._myManageDetail._checkEditData += new MyLib.CheckEditDataEvent(_myManageDetail__checkEditData);
            this._myManageDetail._dataListOpen = true;
            this._myManageDetail._autoSize = true;
            this._myManageDetail._calcArea();
            this._myManageDetail._autoSizeHeight = 450;
            //this._myManageDetail._dataList._loadViewData(0);
            _getPicture1._setEnable(false);
            this._myManageDetail.Invalidate();
            this._buttonSave.Click += new EventHandler(_buttonSave_Click);

            this.ResumeLayout(false);
        }

        void _dataList__deleteData(ArrayList selectRowOrder)
        {
            if (MyLib._myGlobal._checkChangeMaster())
            {

                StringBuilder __myQuery = new StringBuilder();
                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();

                __myQuery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                for (int __loop = 0; __loop < selectRowOrder.Count; __loop++)
                {
                    MyLib._deleteDataType __getData = (MyLib._deleteDataType)selectRowOrder[__loop];
                    int __getColumnCode = _myManageDetail._dataList._gridData._findColumnByName(_g.d.ic_inventory_barcode._table + "." + _g.d.ic_inventory_barcode._barcode);
                    string __getItemCode = this._myManageDetail._dataList._gridData._cellGet(__getData.row, __getColumnCode).ToString().ToUpper();
                    //                        
                    {
                        // ลบรายละเอียดที่เกี่ยวข้อง
                        __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.images._table + " where " + MyLib._myGlobal._addUpper(_g.d.images._image_id) + " = \'" + __getItemCode + "\'"));
                    }
                }
                __myQuery.Append("</node>");

                string __result = __myFrameWork._queryList(MyLib._myGlobal._databaseName, __myQuery.ToString());
                if (__result.Length == 0)
                {
                    MyLib._myGlobal._displayWarning(0, null);
                }
                else
                {
                    MessageBox.Show(__result, "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        bool _myManageDetail__checkEditData(int row, MyLib._myGrid sender)
        {
            int __itemCodeColumnNumber = sender._findColumnByName(_g.d.ic_inventory_barcode._table + "." + _g.d.ic_inventory_barcode._ic_code);
            string __itemCode = sender._cellGet(row, __itemCodeColumnNumber).ToString();
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            DataTable __getData = __myFrameWork._queryShort("select " + _g.d.ic_inventory._update_detail + " from " + _g.d.ic_inventory._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_inventory._code) + "=\'" + __itemCode.ToUpper() + "\'").Tables[0];
            int __updateDetail = MyLib._myGlobal._intPhase(__getData.Rows[0][0].ToString());
            return (__updateDetail == 1) ? true : false;
        }

        bool _myManageDetail__discardData()
        {
            return true;
        }

        void _myManageDetail__closeScreen()
        {
            this.Dispose();
        }

        void _myManageDetail__clearData()
        {
            this._icmainScreenTop._clear();
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
                this._barcode = __rowDataArray[2].ToString();
                __myquery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.ic_inventory._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_inventory._code) + "=\'" + _oldCode.ToUpper() + "\'"));
                __myquery.Append("</node>");
                ArrayList __getData = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());
                this._icmainScreenTop._loadData(((DataSet)__getData[0]).Tables[0]);
                this._icmainScreenTop._search(true);
                this._icmainScreenTop.Invalidate();
                this._getPicture1._clearpic();
                string _codepic = this._barcode; // this._icmainScreenTop._getDataStr(this._barcode);
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
                __result = true;
            }
            catch (Exception __e)
            {
                MessageBox.Show(__e.Message);
            }
            return __result;
        }

        void _buttonSave_Click(object sender, EventArgs e)
        {
            this._saveData();
        }

        void _myManageDetail__newDataClick()
        {
            this._icmainScreenTop._clear();
            this._myManageDetail._dataList._refreshData();
        }

        void _saveData()
        {
            if (MyLib._myGlobal._checkChangeMaster())
            {
                // กำหนด Focus ใหม่ ไม่งั้นข้อมูลจะไม่สมบูรณ์
                string __result = "";
                if (_myManageDetail._manageButton.Enabled)
                {
                    string __getEmtry = this._icmainScreenTop._checkEmtryField();
                    if (__getEmtry.Length > 0)
                    {
                        MyLib._myGlobal._displayWarning(2, __getEmtry);
                    }
                    else
                    {
                        string _codepic = this._barcode; // this._icmainScreenTop._getDataStr(this._barcode);
                        string _codepic_ = _codepic.Replace("/", "").Trim();
                        if (_myManageDetail._mode == 1)
                        {

                        }
                        else
                        {
                            __result = this._getPicture1._updateImage(_codepic_);
                        }

                        if (__result.Length == 0)
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
                            _icmainScreenTop._clear();
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
        }
    }
}
