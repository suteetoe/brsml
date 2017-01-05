using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace SMLReport._formReport
{
    public partial class _dataSearchControl : UserControl
    {
        public _dataSearchControl()
        {
            InitializeComponent();

            _myManageData1._dataList._lockRecord = true; // ใช้แบบ Lock Record ต้องมี guid_code ด้วยนะ อย่าลืม
            _myManageData1._dataList._loadViewFormat("screen_formdesign_loadform", MyLib._myGlobal._userSearchScreenGroup, true);
            _myManageData1._dataList._referFieldAdd(_g.d.formdesign._formcode, 1);
            //_myManageData1._dataList.
            //_myManageData1._manageButton = this._myToolbar;
            //_myManageData1._manageBackgroundPanel = this._myPanel1;

            //_myManageData1._loadDataToScreen += new MyLib.LoadDataToScreen(_myManageData1__loadDataToScreen);
            //_myManageData1._discardData += new MyLib.DiscardDataEvent(_myManageData1__discardData);
            //_myManageData1._closeScreen += new MyLib.CloseScreenEvent(_myManageData1__closeScreen);
            //_myManageData1._newDataClick += new MyLib.NewDataEvent(_myManageData1__newDataClick);
            //_myManageData1._clearData += new MyLib.ClearDataEvent(_myManageData1__clearData);
            //_myManageData1._dataList._deleteData += new MyLib.DeleteDataEventHandler(_dataList__deleteData);

            //_myManageData1._dataListOpen = true;
            //_myManageData1._calcArea();
            //_myManageData1._dataList._loadViewData(0);
            //_myManageData1._autoSize = true;
            //_myManageData1._autoSizeHeight = 350;

        }
       

    }
}
