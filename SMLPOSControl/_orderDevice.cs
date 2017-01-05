using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace SMLPOSControl
{
    public partial class _orderDevice : UserControl
    {
        private MyLib._myFrameWork _myFrameWork = new MyLib._myFrameWork();

        public _orderDevice()
        {
            InitializeComponent();
            if (MyLib._myGlobal._isDesignMode == false)
            {
                this.toolStrip1.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            }

            _myManageData1._dataList._lockRecord = true;
            _myManageData1._dataList._loadViewFormat("screen_order_device_id", MyLib._myGlobal._userSearchScreenGroup, true);
            _myManageData1._dataList._referFieldAdd(_g.d.order_device_id._device_id, 1);
            _myManageData1._manageButton = this.toolStrip1;
            _myManageData1._manageBackgroundPanel = this._myPanel1;
            _myManageData1._loadDataToScreen += new MyLib.LoadDataToScreen(_myManageData1__loadDataToScreen);
            _myManageData1._discardData += new MyLib.DiscardDataEvent(_myManageData1__discardData);
            _myManageData1._closeScreen += new MyLib.CloseScreenEvent(_myManageData1__closeScreen);
            _myManageData1._newDataClick += new MyLib.NewDataEvent(_myManageData1__newDataClick);
            _myManageData1._clearData += new MyLib.ClearDataEvent(_myManageData1__clearData);
            _myManageData1._dataList._deleteData += new MyLib.DeleteDataEventHandler(_dataList__deleteData);

            this._orderDeviceScreenTop1._saveKeyDown += new MyLib.SaveKeyDownHandler(_orderDeviceScreenTop1__saveKeyDown);
            this._orderDeviceScreenTop1._checkKeyDown += new MyLib.CheckKeyDownHandler(_orderDeviceScreenTop1__checkKeyDown);
            _myManageData1._autoSize = true;
            _myManageData1._autoSizeHeight = 500;

            this.Disposed += new EventHandler(_orderDevice_Disposed);
            this.Resize += new EventHandler(_orderDevice_Resize);
        }

        void _saveButton_Click(object sender, System.EventArgs e)
        {
            _save_data();
        }

        void _orderDevice_Resize(object sender, EventArgs e)
        {
            if (_myManageData1._dataList._loadViewDataSuccess == false)
            {
                _myManageData1._dataListOpen = true;
                _myManageData1._calcArea();
                _myManageData1._dataList._loadViewData(0);
            }
        }

        void _save_data()
        {
            _orderDeviceScreenTop1._saveLastControl();
            string __getEmtry = _orderDeviceScreenTop1._checkEmtryField();
            if (__getEmtry.Length > 0)
            {
                MyLib._myGlobal._displayWarning(2, __getEmtry);
            }
            else
            {
                ArrayList __getData = _orderDeviceScreenTop1._createQueryForDatabase();
                StringBuilder __myQuery = new StringBuilder();
                __myQuery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                if (_myManageData1._mode == 1)
                {
                    __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("insert into " + _myManageData1._dataList._tableName + " (" + __getData[0].ToString() + ") values (" + __getData[1].ToString() + ")"));
                }
                else
                {
                    __myQuery.Append(MyLib._myUtil._convertTextToXmlForQuery("update " + _myManageData1._dataList._tableName + " set " + __getData[2].ToString() + _myManageData1._dataList._whereString));
                }
                //
                __myQuery.Append("</node>");
                string result = _myFrameWork._queryList(MyLib._myGlobal._databaseName, __myQuery.ToString());
                if (result.Length == 0)
                {
                    MyLib._myGlobal._displayWarning(1, null);
                    if (_myManageData1._mode == 1)
                    {
                        _myManageData1._afterInsertData();
                    }
                    else
                    {
                        _myManageData1._afterUpdateData();
                    }
                    _orderDeviceScreenTop1._clear();
                    _orderDeviceScreenTop1._focusFirst();
                    _myManageData1._dataList._refreshData();
                }
                else
                {
                    MessageBox.Show(result, "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        void _orderDevice_Disposed(object sender, EventArgs e)
        {

        }

        bool _orderDeviceScreenTop1__checkKeyDown(object sender, Keys keyData)
        {
            if (toolStrip1.Enabled == false)
            {
                MyLib._myGlobal._displayWarning(4, null);
                this._orderDeviceScreenTop1._isChange = false;
            }
            return true;
        }

        void _orderDeviceScreenTop1__saveKeyDown(object sender)
        {
            _save_data();
        }

        void _dataList__deleteData(System.Collections.ArrayList selectRowOrder)
        {
            StringBuilder __myQuery = new StringBuilder();
            __myQuery.Append(MyLib._myGlobal._xmlHeader + "<node>");
            for (int __loop = 0; __loop < selectRowOrder.Count; __loop++)
            {
                MyLib._deleteDataType getData = (MyLib._deleteDataType)selectRowOrder[__loop];
                __myQuery.Append(string.Format(MyLib._myUtil._convertTextToXmlForQuery("delete from {0} {1}"), _myManageData1._dataList._tableName, getData.whereString));
            } // for
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

        void _myManageData1__clearData()
        {
            this._orderDeviceScreenTop1._clear();
        }

        void _myManageData1__newDataClick()
        {
            Control __codeControl = _orderDeviceScreenTop1._getControl(_g.d.order_device_id._device_id);
            __codeControl.Enabled = true;
            _orderDeviceScreenTop1._focusFirst();
        }

        void _myManageData1__closeScreen()
        {
            this.Dispose();
        }

        void _closeButton_Click(object sender, System.EventArgs e)
        {
            this.Dispose();
        }

        bool _myManageData1__discardData()
        {
            bool __result = true;
            if (_orderDeviceScreenTop1._isChange)
            {
                if (DialogResult.No == MyLib._myGlobal._displayWarning(3, null))
                {
                    __result = false;
                }
                else
                {
                    _orderDeviceScreenTop1._isChange = false;
                }
            }
            return (__result);
        }

        bool _myManageData1__loadDataToScreen(object rowData, string whereString, bool forEdit)
        {
            try
            {
                DataSet __getData = _myFrameWork._query(MyLib._myGlobal._databaseName, "select * from " + _myManageData1._dataList._tableName + whereString);
                _orderDeviceScreenTop1._loadData(__getData.Tables[0]);
                Control __codeControl = _orderDeviceScreenTop1._getControl(_g.d.order_device_id._device_id);
                __codeControl.Enabled = false;
                if (forEdit)
                {
                    _orderDeviceScreenTop1._focusFirst();
                }
                return (true);
            }
            catch (Exception)
            {
            }
            return (false);
        }
    }

    public class _orderDeviceScreenTop : MyLib._myScreen
    {
        public _orderDeviceScreenTop()
        {
            this._maxColumn = 1;
            this.SuspendLayout();
            this._table_name = _g.d.order_device_id._table;
            this._addTextBox(0, 0, 1, 0, _g.d.order_device_id._device_id, 1, 15, 0, true, false, false);
            this._addTextBox(1, 0, 1, 0, _g.d.order_device_id._device_name, 1, 50, 0, true, false, false);
            this._addTextBox(2, 0, _g.d.order_device_id._doc_format_order, 0);
            this._addNumberBox(3, 0, 1, 0, _g.d.order_device_id._pos_config_number, 1, 0, true);
            this._addNumberBox(4, 0, 1, 0, _g.d.order_device_id._price_number, 1, 0, true);
            this._addNumberBox(5, 0, 1, 0, _g.d.order_device_id._close_form_number, 1, 0, true);
            this._addCheckBox(6, 0, _g.d.order_device_id._device_disable_password, false, true);
            this._addCheckBox(7, 0, _g.d.order_device_id._device_status, false, true);
            this._addCheckBox(8, 0, _g.d.order_device_id._device_android, false, true);
            this._addCheckBox(9, 0, _g.d.order_device_id._device_html, false, true);
            this._addCheckBox(10, 0, _g.d.order_device_id._print_from_center, false, true);
            this._addTextBox(11, 0, 1, 0, _g.d.order_device_id._user_access, 1, 50, 0, true, false, true);
            this.ResumeLayout();
        }
    }
}
