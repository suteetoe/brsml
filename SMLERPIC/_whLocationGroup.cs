using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Threading;

/*
* @author MooAe 
* @copyright 2009
* @mail naiay@msn.com
*/

namespace SMLERPIC
{
    public partial class _whLocationGroup : UserControl
    {
        MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();
        _warehouseLocationGroup _screen = new _warehouseLocationGroup();

        public _whLocationGroup()
        {
            InitializeComponent();
            if (MyLib._myGlobal._isDesignMode == false)
            {
                this._screen._myToolBar.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            }
            this._screen.Dock = DockStyle.Fill;
            _myManageData1._form2.Controls.Add(this._screen);
            _myManageData1._dataList._lockRecord = true; // ใช้แบบ Lock Record ต้องมี guid_code ด้วยนะ อย่าลืม
            _myManageData1._dataList._loadViewFormat("screen_warehouse_location_group", MyLib._myGlobal._userSearchScreenGroup, true);
            _myManageData1._dataList._referFieldAdd(_g.d.ic_warehouse_location_group._wh_code, 1);
            _myManageData1._manageButton = this._screen._myToolBar;

            _myManageData1._loadDataToScreen += new MyLib.LoadDataToScreen(_myManageData1__loadDataToScreen);
            _myManageData1._discardData += new MyLib.DiscardDataEvent(_myManageData1__discardData);
            _myManageData1._closeScreen += new MyLib.CloseScreenEvent(_myManageData1__closeScreen);
            _myManageData1._newDataClick += new MyLib.NewDataEvent(_myManageData1__newDataClick);
            _myManageData1._clearData += new MyLib.ClearDataEvent(_myManageData1__clearData);
            _myManageData1._dataList._deleteData += new MyLib.DeleteDataEventHandler(_dataList__deleteData);
            this._screen._screenTop._saveKeyDown += new MyLib.SaveKeyDownHandler(_screen_saveKeyDown);
            this._screen._saveButton.Click += _saveButton_Click1;

            _myManageData1._dataListOpen = true;
            _myManageData1._calcArea();
            //_myManageData1._dataList._loadViewData(0);
            _myManageData1._autoSize = true;
            _myManageData1._autoSizeHeight = 350;
            this.Disposed += new EventHandler(_disposed);
        }

        private void _saveButton_Click1(object sender, EventArgs e)
        {
            _save_data();
        }

        void _disposed(object sender, EventArgs e)
        {
            this.Dispose();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            Keys keyCode = (Keys)(int)keyData & Keys.KeyCode;
            if ((keyData & Keys.Control) == Keys.Control)
            {
                switch (keyCode)
                {
                    case Keys.Home:
                        {
                            this._screen._screenTop._focusFirst();
                            return true;
                        }
                }
            }
            if (keyData == Keys.F2)
            {
                return true;
            }
            if (keyData == Keys.F12 && this._screen._myToolBar.Enabled == true)
            {
                _save_data();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        void _screen_saveKeyDown(object sender)
        {
            _save_data();
        }

        private void _save_data()
        {
            if (this._screen._myToolBar.Enabled == true)
            {
                string getEmtry = this._screen._screenTop._checkEmtryField();
                if (getEmtry.Length > 0)
                {
                    MyLib._myGlobal._displayWarning(2, getEmtry);
                }
                else
                {
                    ArrayList __getData = this._screen._screenTop._createQueryForDatabase();
                    StringBuilder __myQuery = new StringBuilder();

                    __myQuery.Append(MyLib._myGlobal._xmlHeader + "<node>");

                    __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _g.d.ic_warehouse_location_group._table + " (" + __getData[0].ToString() + ") values (" + __getData[1].ToString() + ")"));

                    string __whCode = this._screen._screenTop._getDataStr(_g.d.ic_warehouse_location_group_detail._wh_code) ;
                    string __locationCode = this._screen._screenTop._getDataStr(_g.d.ic_warehouse_location_group_detail._location_group);

                    __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.ic_warehouse_location_group_detail._table + " where " + _g.d.ic_warehouse_location_group_detail._wh_code + "=\'" + __whCode.ToUpper() + "\' and " + _g.d.ic_warehouse_location_group_detail._location_group + "=\'" + __locationCode.ToUpper() + "\' "));
                    this._screen._listGrid._updateRowIsChangeAll(true);
                    __myQuery.Append( this._screen._listGrid._createQueryForInsert(_g.d.ic_warehouse_location_group_detail._table, _g.d.ic_warehouse_location_group_detail._wh_code + "," + _g.d.ic_warehouse_location_group_detail._location_group + ",", "\'" + __whCode.ToUpper() + "\', \'" + __locationCode.ToUpper() + "\',"));

                    __myQuery.Append("</node>");
                    string result = _myFrameWork._queryList(MyLib._myGlobal._databaseName, __myQuery.ToString());
                    if (result.Length == 0)
                    {
                        MyLib._myGlobal._displayWarning(1, null);
                        this._screen._screenTop._isChange = false;
                        this._screen._screenTop._clear();
                        this._screen._screenTop._focusFirst();
                    }
                    else
                    {
                        MessageBox.Show(result, "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show(MyLib._myGlobal._resource("ไม่สามารถบันทึกข้อมูลได้"), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        void _dataList__deleteData(System.Collections.ArrayList selectRowOrder)
        {
            if (MyLib._myGlobal._checkChangeMaster())
            {
                StringBuilder __myQuery = new StringBuilder();
                __myQuery.Append(MyLib._myGlobal._xmlHeader + "<node>");

                __myQuery.Append("</node>");
                string result = _myFrameWork._queryList(MyLib._myGlobal._databaseName, __myQuery.ToString());
                if (result.Length == 0)
                {
                    MyLib._myGlobal._displayWarning(1, MyLib._myGlobal._resource("สำเร็จ"));
                    _myManageData1._dataList._refreshData();
                }
                else
                {
                    MessageBox.Show(result, "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        void _myManageData1__clearData()
        {
            this._screen._screenTop._clear();
            this._screen._listGrid._clear();
            this._screen._selectedGrid._clear();
        }

        void _myManageData1__newDataClick()
        {
            this._screen._screenTop._clear();
            this._screen._listGrid._clear();
            this._screen._selectedGrid._clear();
        }

        void _myManageData1__closeScreen()
        {
            this.Dispose();
        }

        bool _myManageData1__discardData()
        {
            bool result = true;
            if (this._screen._screenTop._isChange)
            {
                if (DialogResult.No == MyLib._myGlobal._displayWarning(3, null))
                {
                    result = false;
                }
                else
                {
                    this._screen._screenTop._isChange = false;
                }
            }
            return (result);
        }

        bool _myManageData1__loadDataToScreen(object rowData, string whereString, bool forEdit)
        {
            this._screen._screenTop._clear();

            try
            {
                StringBuilder __myQuery = new StringBuilder(MyLib._myGlobal._xmlHeader + "<node>");
                __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.ic_warehouse_location_group._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_warehouse_location_group._wh_code) + "=\'" + ((ArrayList)rowData)[1].ToString().ToUpper() + "\' and "+ MyLib._myGlobal._addUpper(_g.d.ic_warehouse_location_group._location_group) + "=\'" + ((ArrayList)rowData)[2].ToString().ToUpper() + "\'"));
                __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("select * from " + _g.d.ic_warehouse_location_group_detail._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_warehouse_location_group_detail._wh_code) + "=\'" + ((ArrayList)rowData)[1].ToString().ToUpper() + "\' and " + MyLib._myGlobal._addUpper(_g.d.ic_warehouse_location_group_detail._location_group) + "=\'" + ((ArrayList)rowData)[2].ToString().ToUpper() + "\'"));
                __myQuery.Append("</node>");

                ArrayList getDataList = _myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myQuery.ToString());

                DataSet getData = (DataSet)getDataList[0];
                DataSet dealerData = (DataSet)getDataList[1];

                this._screen._screenTop._loadData(getData.Tables[0]);
                this._screen._search(false);

                this._screen._screenTop._isChange = false;

                for (int __row = 0; __row < dealerData.Tables[0].Rows.Count; __row++)
                {
                    string __locationCode = dealerData.Tables[0].Rows[__row][_g.d.ic_warehouse_location_group_detail._location].ToString();
                    int __addr = this._screen._selectedGrid._findData(this._screen._selectedGrid._findColumnByName(_g.d.ic_shelf._code), __locationCode);
                    if (__addr != -1)
                    {
                        this._screen._selectedGrid._cellUpdate(__addr, 0, 1, true);
                    }
                }
                if (forEdit)
                {
                    this._screen._screenTop._focusFirst();
                }
                return (true);
            }
            catch (Exception)
            {
            }
            return (false);
        }

        private void _saveButton_Click(object sender, EventArgs e)
        {
            this._save_data();
        }

        private void _closeButton_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void _myManageData1_Load(object sender, EventArgs e)
        {

        }
    }
}
