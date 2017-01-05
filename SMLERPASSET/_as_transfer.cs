using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLERPASSET
{
    public partial class _as_transfer : UserControl
    {
        public _as_transfer()
        {
            InitializeComponent();
            if (MyLib._myGlobal._isDesignMode == false)
            {
                this._myToolBar.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            }
            // Screen Top
            this._screenTop.AutoSize = true;
            this._screenTop._maxColumn = 2;
            //this._screenTop._table_name = "Transfer";
            this._screenTop._addTextBox(0, 0, 1, 0, "เลขที่เอกสาร", 1, 10, 1, true, false, true);
            this._screenTop._addDateBox(0, 1, 1, 0, "วันที่เอกสาร", 1, true);
            this._screenTop._addTextBox(1, 0, 1, 0, "สมุดรายวัน", 1, 10, 1, true, false, false);
            this._screenTop._addTextBox(2, 0, 2, "หมายเหตุ", 2, 255);
            // Condition
            //this._screenTop._addLabel(4, 0, "", "เงื่อนไข");
            MyLib._myGroupBox __conditionGroupBox = _screenTop._addGroupBox(4, 0, 2, 1, 2, "เงื่อนไข", false);
            this._screenTop._addTextBox(5, 0, 1, 0, "รหัสสินทรัพย์", 1, 10, 1, true, false, false);
            this._screenTop._addTextBox(5, 1, 1, 0, "ถึง", 1, 10, 1, true, false, false);
            this._screenTop._addTextBox(6, 0, 1, 0, "ประเภทสินทรัพย์", 1, 10, 1, true, false, false);
            this._screenTop._addTextBox(6, 1, 1, 0, "ถึง", 1, 10, 1, true, false, false);
            this._screenTop._addTextBox(7, 0, 1, 0, "แผนก", 1, 10, 1, true, false, false);
            this._screenTop._addTextBox(7, 1, 1, 0, "ถึง", 1, 10, 1, true, false, false);
            this._screenTop._addNumberBox(8, 0, 0, 0, "ประจำงวดที่", 1, 0, true);
            this._screenTop._addNumberBox(8, 1, 0, 0, "ถึง", 1, 0, true);
            this._screenTop._addNumberBox(9, 0, 0, 0, "ประจำปี", 1, 0, true);
            this._screenTop._addCheckBox(10, 0, "แยกแผนก", false, true);
            // Grid
            this._myGrid1._width_by_persent = true;
            this._myGrid1._total_show = true;
            this._myGrid1._addColumn("รหัสบัญชี", 1, 10, 20, true, false, false, true);
            this._myGrid1._addColumn("ชื่อบัญชี", 1, 20, 40, true, false);
            this._myGrid1._addColumn("เดบิต", 3, 0, 20, true, false, false, false, MyLib._myGlobal._getFormatNumber("m02"));
            this._myGrid1._addColumn("เครดิต", 3, 0, 20, true, false, false, false, MyLib._myGlobal._getFormatNumber("m02"));
            // Manage Data
            this._myManageData1._closeScreen += new MyLib.CloseScreenEvent(_myManageData1__closeScreen);

        }

        void _myManageData1__closeScreen()
        {
            this.Dispose();
        }
    }
}
