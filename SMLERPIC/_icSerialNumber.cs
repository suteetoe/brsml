using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLERPIC
{
    public partial class _icSerialNumber : UserControl
    {
        string _oldCode = "";

        public _icSerialNumber()
        {
            InitializeComponent();
            if (MyLib._myGlobal._isDesignMode == false)
            {
                this._myToolBar.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            }

            this.SuspendLayout();
            this._myManageMain._autoSize = true;
            this._myManageMain._displayMode = 0;
            this._myManageMain._readOnly = true;
            this._myManageMain._dataList._lockRecord = true; // ใช้แบบ Lock Record ต้องมี guid_code ด้วยนะ อย่าลืม
            this._myManageMain._selectDisplayMode(this._myManageMain._displayMode);
            this._myManageMain._dataList._loadViewFormat(_g.g._search_screen_ic_serial, MyLib._myGlobal._userSearchScreenGroup, true);
            this._myManageMain._dataList._referFieldAdd(_g.d.ic_serial._serial_number, 1);
            this._myManageMain._manageButton = this._myToolBar;
            //this._myManageMain._manageBackgroundPanel = this._myPanel1;
            this._myManageMain._autoSizeHeight = 450;
            this._myManageMain._dataListOpen = true;
            this._myManageMain._loadDataToScreen += new MyLib.LoadDataToScreen(_myManageMain__loadDataToScreen);
            this._myManageMain._closeScreen += new MyLib.CloseScreenEvent(_myManageMain__closeScreen);
            this._myManageMain._clearData += new MyLib.ClearDataEvent(_myManageMain__clearData);
            this._myManageMain._dataList._deleteData += new MyLib.DeleteDataEventHandler(_dataList__deleteData);
            this._myManageMain._dataList._gridData._beforeDisplayRendering += new MyLib.BeforeDisplayRenderRowEventHandler(_gridData__beforeDisplayRendering);
            //this._myManageMain._dataList._loadViewData(0);
            this.ResumeLayout(false);
            //
        }

        MyLib.BeforeDisplayRowReturn _gridData__beforeDisplayRendering(MyLib._myGrid sender, int row, int columnNumber, string columnName, MyLib.BeforeDisplayRowReturn senderRow, MyLib._myGrid._columnType columnType, ArrayList rowData, Graphics e)
        {
            int __status = (int)MyLib._myGlobal._decimalPhase(sender._cellGet(row, _g.d.ic_serial._table + "." + _g.d.ic_serial._status).ToString());
            if (__status == 1)
            {
                senderRow.newColor = Color.Blue;
            }
            return senderRow;
        }

        void _dataList__deleteData(ArrayList selectRowOrder)
        {
            MessageBox.Show(MyLib._myGlobal._resource(this._myManageMain._readOnlyMessage));
        }

        void _myManageMain__clearData()
        {
            this._serialNumberScreen._clear();
            this._serialNumberScreen._focusFirst();
        }

        void _myManageMain__closeScreen()
        {
            this.Dispose();
        }

        bool _myManageMain__loadDataToScreen(object rowData, string whereString, bool forEdit)
        {
            try
            {
                int __columnItemName = this._myManageMain._dataList._gridData._findColumnByName(_g.d.ic_serial._table + "." + _g.d.ic_serial._ic_code);
                string __whereItemName = " and \'" + ((ArrayList)rowData)[__columnItemName].ToString() + "\' like " + _g.d.ic_serial._ic_code + " || '(%'";

                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                DataSet getData = __myFrameWork._queryShort("select * from " + this._myManageMain._dataList._tableName + whereString + __whereItemName);
                this._serialNumberScreen._loadData(getData.Tables[0]);
                string __serialNumber = this._serialNumberScreen._getDataStr(_g.d.ic_serial._serial_number).ToString();
                string __itemCode = this._serialNumberScreen._getDataStr(_g.d.ic_serial._ic_code).ToString();
                this._movement._load(__serialNumber, __itemCode);
                return (true);
            }
            catch (Exception)
            {
            }
            return (false);
        }
    }
}
