using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Diagnostics;
using System.Threading;

namespace SMLERPIC
{
    public partial class _icMainControl : UserControl
    {
        public delegate void SaveEventHandler(Boolean clear);

        public event SaveEventHandler _saveData;

        int _displayMode = 0;
        string _refField = "";

        public _icMainControl(int mode)
        {
            this._displayMode = mode;
            this._refField = (this._displayMode == 0) ? _g.d.ic_inventory._table + "." + _g.d.ic_inventory._code : _g.d.ic_inventory_barcode._table + "." + _g.d.ic_inventory_barcode._ic_code;
            //
            InitializeComponent();
            if (MyLib._myGlobal._isDesignMode == false)
            {
                this._myToolBar.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
                this.toolStrip1.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            }
            this.SuspendLayout();
            this._myTabControlMain.TableName = _g.d.ic_resource._table;
            this._myTabControlMain._getResource();
            this._icmainScreenTop._screenCode = "IC";
            this._icmainScreenTop._textBoxChanged += new MyLib.TextBoxChangedHandler(_icmainScreenTop__textBoxChanged);
            this._icmainScreenTop._saveKeyDown += new MyLib.SaveKeyDownHandler(_icmainScreenTop__saveKeyDown);

            this._saveButton.Click += new EventHandler(_saveButton_Click);
            this._icmainGridUnit._queryForUpdateWhere += new MyLib.QueryForUpdateWhereEventHandler(_icmainGridUnit__queryForUpdateWhere);
            this._icmainGridUnit._queryForUpdateCheck += new MyLib.QueryForUpdateCheckEventHandler(_icmainGridUnit__queryForUpdateCheck);
            this._icmainGridUnit._queryForInsertCheck += new MyLib.QueryForInsertCheckEventHandler(_icmainGridUnit__queryForInsertCheck);
            this._icmainGridUnit._queryForRowRemoveCheck += new MyLib.QueryForRowRemoveCheckEventHandler(_icmainGridUnit__queryForRowRemoveCheck);

            if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountProfessional ||
                MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccount ||
                MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOSProfessional)
            {
                // หน่วยนับขนาด
                this._icmainGridUnitOpposi._queryForUpdateWhere += new MyLib.QueryForUpdateWhereEventHandler(_icmainGridUnitOpposi__queryForUpdateWhere);
                this._icmainGridUnitOpposi._queryForUpdateCheck += new MyLib.QueryForUpdateCheckEventHandler(_icmainGridUnitOpposi__queryForUpdateCheck);
                this._icmainGridUnitOpposi._queryForInsertCheck += new MyLib.QueryForInsertCheckEventHandler(_icmainGridUnitOpposi__queryForInsertCheck);
                this._icmainGridUnitOpposi._queryForRowRemoveCheck += new MyLib.QueryForRowRemoveCheckEventHandler(_icmainGridUnitOpposi__queryForRowRemoveCheck);
            }

            this._icmainGridBranch._queryForUpdateWhere += new MyLib.QueryForUpdateWhereEventHandler(_icmainGridBranch__queryForUpdateWhere);
            this._icmainGridBranch._queryForUpdateCheck += new MyLib.QueryForUpdateCheckEventHandler(_icmainGridBranch__queryForUpdateCheck);
            this._icmainGridBranch._queryForInsertCheck += new MyLib.QueryForInsertCheckEventHandler(_icmainGridBranch__queryForInsertCheck);
            this._icmainGridBranch._queryForRowRemoveCheck += new MyLib.QueryForRowRemoveCheckEventHandler(_icmainGridBranch__queryForRowRemoveCheck);
            this.ResumeLayout(false);
            //
            this._myTabControlMain.KeyDown += new KeyEventHandler(_myTabControlMain_KeyDown);
            _newData();
            if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLColorStore ||
                MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccount ||
                MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountProfessional ||
                MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOSProfessional)
            {
                // ลบ Tab หน่วยนับขนาดออก
                this._myTabControlMain.TabPages.RemoveAt(1);
                this._myTabControlMain.TabPages.RemoveAt(1);
            }

            // toe
            if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLBIllFree)
            {
                this._myTabControlMain.TabPages.RemoveAt(1);
                this._myTabControlMain.TabPages.RemoveAt(1);
                this._myTabControlMain.TabPages.RemoveAt(1);

                this.splitContainer3.Panel1.Controls.Clear();
                this.splitContainer3.Panel2.Controls.Clear();

                this.splitContainer1.Panel2.Controls.Clear();
                this.splitContainer1.Panel2.Controls.Add(this._icmainGridBarCode);

                this.toolStrip1.Visible = false;
                this._icmainGridBranch.Visible = false;
            }

            this._icmainGridUnit._alterCellUpdate += new MyLib.AfterCellUpdateEventHandler(_icmainGridUnit__alterCellUpdate);
            //
            this._icmainGridBarCode.GetItemCode += new SMLInventoryControl.GetItemCodeEventHandler(_icmainGridBarCode_GetItemCode);
            this._icmainGridBarCode.GetItemDesc += new SMLInventoryControl.GetItemDescEventHandler(_icmainGridBarCode_GetItemDesc);
            this._icmainGridBarCode.GetUnitCode += new SMLInventoryControl.GetUnitCodeEventHandler(_icmainGridBarCode_GetUnitCode);
            this._icmainGridBarCode.GetUnitType += new SMLInventoryControl.GetUnitTypeEventHandler(_icmainGridBarCode_GetUnitType);
            this._icmainGridBarCode._icTransItemGridSelectUnit._loadData += new SMLERPControl._ic._icTransItemGridSelectUnitForm._loadDataEventHandler(_icTransItemGridSelectUnit__loadData);

            this._icStandardCost._afterAddRow += _icStandardCost__afterAddRow;
        }

        private void _icStandardCost__afterAddRow(object sender, int row)
        {
            MyLib._myTextBox __getUnitCostControl = (MyLib._myTextBox)this._icmainScreenTop._getControl(_g.d.ic_inventory._unit_cost);

            this._icStandardCost._cellUpdate(row, _g.d.ic_standard_cost._unit_code, __getUnitCostControl._textFirst, true);
            this._icStandardCost._cellUpdate(row, _g.d.ic_standard_cost._unit_name, __getUnitCostControl._textSecond, true);
        }

        DataTable _icTransItemGridSelectUnit__loadData()
        {
            DataTable __result = new DataTable();
            __result.Columns.Add(new DataColumn(_g.d.ic_unit_use._code, typeof(string)));
            __result.Columns.Add(new DataColumn(_g.d.ic_unit_use._name_1, typeof(string)));
            __result.Columns.Add(new DataColumn(_g.d.ic_unit_use._stand_value, typeof(decimal)));
            __result.Columns.Add(new DataColumn(_g.d.ic_unit_use._divide_value, typeof(decimal)));
            __result.Columns.Add(new DataColumn(_g.d.ic_unit_use._ratio, typeof(decimal)));
            __result.Columns.Add(new DataColumn(_g.d.ic_unit_use._row_order, typeof(int)));
            __result.Columns.Add(new DataColumn(_g.d.ic_unit_use._width_length_height, typeof(decimal)));
            __result.Columns.Add(new DataColumn(_g.d.ic_unit_use._weight, typeof(decimal)));
            __result.Columns.Add(new DataColumn(_g.d.ic_unit_use._status, typeof(int)));
            for (int __row = 0; __row < this._icmainGridUnit._rowData.Count; __row++)
            {
                DataRow __rowData = __result.NewRow();
                __rowData[_g.d.ic_unit_use._code] = this._icmainGridUnit._cellGet(__row, _g.d.ic_unit_use._code).ToString();
                __rowData[_g.d.ic_unit_use._name_1] = this._icmainGridUnit._cellGet(__row, _g.d.ic_unit_use._name_1).ToString();
                __rowData[_g.d.ic_unit_use._stand_value] = MyLib._myGlobal._decimalPhase(this._icmainGridUnit._cellGet(__row, _g.d.ic_unit_use._stand_value).ToString());
                __rowData[_g.d.ic_unit_use._divide_value] = MyLib._myGlobal._decimalPhase(this._icmainGridUnit._cellGet(__row, _g.d.ic_unit_use._divide_value).ToString());
                __rowData[_g.d.ic_unit_use._ratio] = MyLib._myGlobal._decimalPhase(this._icmainGridUnit._cellGet(__row, _g.d.ic_unit_use._ratio).ToString());
                __rowData[_g.d.ic_unit_use._row_order] = (int)MyLib._myGlobal._decimalPhase(this._icmainGridUnit._cellGet(__row, _g.d.ic_unit_use._row_order).ToString());
                __rowData[_g.d.ic_unit_use._width_length_height] = MyLib._myGlobal._decimalPhase(this._icmainGridUnit._cellGet(__row, _g.d.ic_unit_use._width_length_height).ToString());
                __rowData[_g.d.ic_unit_use._weight] = MyLib._myGlobal._decimalPhase(this._icmainGridUnit._cellGet(__row, _g.d.ic_unit_use._weight).ToString());
                __rowData[_g.d.ic_unit_use._status] = (int)MyLib._myGlobal._decimalPhase(this._icmainGridUnit._cellGet(__row, _g.d.ic_unit_use._status).ToString());
                __result.Rows.Add(__rowData);
            }
            return __result;
        }

        int _icmainGridBarCode_GetUnitType(object sender)
        {
            return MyLib._myGlobal._intPhase(this._icmainScreenTop._getDataStr(_g.d.ic_inventory._unit_type));
        }

        string _icmainGridBarCode_GetUnitCode(object sender)
        {
            if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLBIllFree)
                return this._icmainScreenTop._getDataStr(_g.d.ic_inventory._unit_cost);
            else
                return this._icmainScreenTop._getDataStr(_g.d.ic_inventory._unit_standard);
        }

        string _icmainGridBarCode_GetItemDesc(object sender)
        {
            return this._icmainScreenTop._getDataStr(_g.d.ic_inventory._name_1);
        }

        string _icmainGridBarCode_GetItemCode(object sender)
        {
            return this._icmainScreenTop._getDataStr(_g.d.ic_inventory._code);
        }

        void _icmainGridUnit__alterCellUpdate(object sender, int row, int column)
        {
            this._processPack();
        }

        public void _processPack()
        {
            StringBuilder __html = new StringBuilder(@"
                            <head>
                            <style type='text/css'>
                            body,table,tr,td {
	                            font-family: Tahoma,Arial, Helvetica, sans-serif;
                                font-size:12px;
                            }
                            </style>
                            </head>
                            <body>");
            List<_packingClass> __line = new List<_packingClass>();
            for (int __row = 0; __row < this._icmainGridUnit._rowData.Count; __row++)
            {
                string __code = this._icmainGridUnit._cellGet(__row, _g.d.ic_unit_use._code).ToString().Trim();
                if (__code.Length > 0)
                {
                    decimal __standValue = MyLib._myGlobal._decimalPhase(this._icmainGridUnit._cellGet(__row, _g.d.ic_unit_use._stand_value).ToString().Trim());
                    decimal __divideValue = MyLib._myGlobal._decimalPhase(this._icmainGridUnit._cellGet(__row, _g.d.ic_unit_use._divide_value).ToString().Trim());
                    int __lineNumber = (int)MyLib._myGlobal._decimalPhase(this._icmainGridUnit._cellGet(__row, _g.d.ic_unit_use._row_order).ToString().Trim());
                    if (__standValue != 0 && __divideValue != 0)
                    {
                        _packingClass __data = new _packingClass();
                        __data._code = __code;
                        __data._standValue = __standValue;
                        __data._divideValue = __divideValue;
                        __data._ratio = __standValue / __divideValue;
                        __data._lineNumber = __lineNumber;
                        __line.Add(__data);
                    }
                }
            }
            //
            try
            {
                __line.Sort(delegate (_packingClass p1, _packingClass p2) { return p1._ratio.CompareTo(p2._ratio); });
                for (int __loop = 1; __loop < __line.Count; __loop++)
                {
                    _packingClass __rowBefore = __line[__loop - 1];
                    _packingClass __row = __line[__loop];
                    decimal __ratio = __row._ratio / __rowBefore._ratio;
                    __html.Append("<b>" + __ratio.ToString() + "</b>" + " ");
                    __html.Append(__rowBefore._code);
                    __html.Append(" = ");
                    __html.Append(__row._code);
                    __html.Append("<br/>");
                }
            }
            catch
            {
            }
            //
            try
            {
                __line.Sort(delegate (_packingClass p1, _packingClass p2) { return p1._lineNumber.CompareTo(p2._lineNumber); });
                Boolean __first = false;
                for (int __loop = __line.Count - 1; __loop >= 0; __loop--)
                {
                    if (__line[__loop]._lineNumber > 0)
                    {
                        if (__first == true)
                        {
                            __html.Append("x");
                        }
                        else
                        {
                            __first = true;
                            __html.Append("<br/>");
                        }
                        __html.Append("<b>" + __line[__loop]._code + "</b>");
                    }
                }
                __html.Append("<br/>");
            }
            catch
            {
            }
            __html.Append("</body>");
            this._infoWebBrowser.DocumentText = __html.ToString();
        }

        void _icmainScreenTop__textBoxChanged(object sender, string name)
        {
            _changeGridUnitAuto();
        }

        void _icmainScreenTop__saveKeyDown(object sender)
        {
            this._saveData(true);
        }

        public void _newData()
        {
            this._clear();
            MyLib._myComboBox __unitType = (MyLib._myComboBox)this._icmainScreenTop._getControl(_g.d.ic_inventory._unit_type);
            __unitType.SelectedIndexChanged -= new EventHandler(__unitType_SelectedIndexChanged);
            __unitType.SelectedIndexChanged += new EventHandler(__unitType_SelectedIndexChanged);
            _changeGridEnable(__unitType);
            this._loadWarehouseAndLocationAuto();
            this._icmainScreenTop._newData();
        }

        public void _newDataFromTemp()
        {
            this._icmainScreenTop._newData();
        }

        void _changeGridUnitAuto()
        {
            if (this._icmainGridUnit.Enabled == false)
            {
                string __unitCost = this._icmainScreenTop._getDataStr(_g.d.ic_inventory._unit_cost);
                string __unitStandard = this._icmainScreenTop._getDataStr(_g.d.ic_inventory._unit_standard);
                if (__unitCost.Equals(__unitStandard) == false)
                {
                    this._icmainScreenTop._setDataStr(_g.d.ic_inventory._unit_standard, __unitCost);
                }
                //
                if (__unitCost.Length > 0)
                {
                    this._icmainGridUnit._clear();
                    this._icmainGridUnit._addRow();
                    this._icmainGridUnit._cellUpdate(0, _g.d.ic_unit_use._code, this._icmainScreenTop._getDataStr(_g.d.ic_inventory._unit_cost), true);
                    this._icmainGridUnit._cellUpdate(0, _g.d.ic_unit_use._stand_value, 1.0M, false);
                    this._icmainGridUnit._cellUpdate(0, _g.d.ic_unit_use._divide_value, 1.0M, true);
                    this._icmainGridUnit._cellUpdate(0, _g.d.ic_unit_use._row_order, 1.0M, false);
                    this._icmainGridUnit._cellUpdate(0, _g.d.ic_unit_use._width_length_height, this._icmainScreenTop._getDataStr(_g.d.ic_inventory._width_length_height), false);
                    this._icmainGridUnit._cellUpdate(0, _g.d.ic_unit_use._weight, this._icmainScreenTop._getDataStr(_g.d.ic_inventory._weight), false);
                    this._icmainGridUnit._cellUpdate(0, _g.d.ic_unit_use._status, 1, false);
                    this._icmainGridUnit.Invalidate();
                    //
                    if (this._icmainGridBarCode._rowData.Count > 0)
                    {
                        this._icmainGridBarCode._cellUpdate(0, _g.d.ic_inventory_barcode._unit_code, this._icmainScreenTop._getDataStr(_g.d.ic_inventory._unit_cost), true);
                        this._icmainGridBarCode.Invalidate();
                    }
                }
            }

        }

        void _changeGridEnable(object sender)
        {
            MyLib._myComboBox __unitType = (MyLib._myComboBox)sender;

            if (MyLib._myGlobal._isVersionEnum != MyLib._myGlobal._versionType.SMLBIllFree)
            {
                MyLib._myTextBox __field0 = (MyLib._myTextBox)this._icmainScreenTop._getControl(_g.d.ic_inventory._unit_standard);
                MyLib._myTextBox __field1 = (MyLib._myTextBox)this._icmainScreenTop._getControl(_g.d.ic_inventory._width_length_height);
                MyLib._myTextBox __field2 = (MyLib._myTextBox)this._icmainScreenTop._getControl(_g.d.ic_inventory._weight);
                switch (__unitType.SelectedIndex)
                {
                    case 0: // หน่วยนับเดียว
                        this._icmainGridUnit.Enabled = false;
                        if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountProfessional ||
                            MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccount ||
                            MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOSProfessional)
                        {
                            // หน่วยนับขนาน
                            this._icmainGridUnitOpposi.Enabled = false;
                        }
                        __field0.Enabled = false;
                        __field1.Enabled = true;
                        __field2.Enabled = true;
                        _changeGridUnitAuto();
                        break;
                    case 1: // หลายหน่วยนับ
                        this._icmainGridUnit.Enabled = true;
                        if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountProfessional ||
                            MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccount ||
                            MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOSProfessional)
                        {
                            // หน่วยนับขนาน
                            this._icmainGridUnitOpposi.Enabled = false;
                        }
                        __field0.Enabled = true;
                        __field1.Enabled = false;
                        __field2.Enabled = false;
                        break;
                    case 2: // หน่วยนับขนาน+หน่วยนับเดียว
                        this._icmainGridUnit.Enabled = false;
                        if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountProfessional ||
                            MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccount ||
                            MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOSProfessional)
                        {
                            // หน่วยนับขนาน
                            this._icmainGridUnitOpposi.Enabled = true;
                        }
                        __field0.Enabled = false;
                        __field1.Enabled = true;
                        __field2.Enabled = true;
                        _changeGridUnitAuto();
                        break;
                    case 3: // หน่วยนับขนาน+หลายหน่วยนับ
                        this._icmainGridUnit.Enabled = true;
                        if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountProfessional ||
                            MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccount ||
                            MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOSProfessional)
                        {
                            // หน่วยนับขนาน
                            this._icmainGridUnitOpposi.Enabled = true;
                        }
                        __field0.Enabled = true;
                        __field1.Enabled = false;
                        __field2.Enabled = false;
                        break;
                }
                this._icmainScreenTop.Invalidate();
            }
            else
            {
                switch (__unitType.SelectedIndex)
                {
                    case 0: // หน่วยนับเดียว
                        this._icmainGridUnit.Enabled = false;
                        _changeGridUnitAuto();
                        break;
                    case 1: // หลายหน่วยนับ
                        this._icmainGridUnit.Enabled = true;
                        break;
                }
                this._icmainScreenTop.Invalidate();
            }
        }

        void __unitType_SelectedIndexChanged(object sender, EventArgs e)
        {
            _changeGridEnable(sender);
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            const int WM_KEYDOWN = 0x100;
            const int WM_SYSKEYDOWN = 0x104;
            if ((msg.Msg == WM_KEYDOWN) || (msg.Msg == WM_SYSKEYDOWN))
            {
                Keys __keyCode = (Keys)(int)keyData & Keys.KeyCode;
                if ((keyData & Keys.Control) == Keys.Control && (__keyCode == Keys.Home))
                {
                    this._icmainScreenTop.Focus();
                    this._icmainScreenTop._focusFirst();
                    return true;
                }
                if (__keyCode == Keys.F12)
                {
                    if (this._saveData != null)
                    {

                        this._saveData(true);
                        return true;
                    }
                }
                if (__keyCode == Keys.F11)
                {
                    if (this._saveData != null)
                    {
                        this._saveData(false);
                        return true;
                    }
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        void _myTabControlMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control)
            {
                switch (this._myTabControlMain.SelectedIndex)
                {
                    case 0:
                        if (this._icmainGridUnit.Enabled)
                        {
                            if (this._icmainGridUnit._selectRow == -1) this._icmainGridUnit._selectRow = 0;
                            if (this._icmainGridUnit._selectColumn == -1) this._icmainGridUnit._selectColumn = 0;
                            this._icmainGridUnit._inputCell(this._icmainGridUnit._selectRow, this._icmainGridUnit._selectColumn);
                        }
                        break;
                    case 1:
                        if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountProfessional ||
                            MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccount ||
                            MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOSProfessional)
                        {
                            // หน่วยนับขนาน
                            if (this._icmainGridUnitOpposi.Enabled)
                            {
                                if (this._icmainGridUnitOpposi._selectRow == -1) this._icmainGridUnitOpposi._selectRow = 0;
                                if (this._icmainGridUnitOpposi._selectColumn == -1) this._icmainGridUnitOpposi._selectColumn = 0;
                                this._icmainGridUnitOpposi._inputCell(this._icmainGridUnitOpposi._selectRow, this._icmainGridUnitOpposi._selectColumn);
                            }
                        }
                        break;
                    case 2:
                        if (this._icmainGridBranch.Enabled)
                        {
                            if (this._icmainGridBranch._selectRow == -1) this._icmainGridBranch._selectRow = 0;
                            if (this._icmainGridBranch._selectColumn == -1) this._icmainGridBranch._selectColumn = 0;
                            this._icmainGridBranch._inputCell(this._icmainGridBranch._selectRow, this._icmainGridBranch._selectColumn);
                        }
                        break;
                }
            }
        }


        bool _icmainGridBranch__queryForRowRemoveCheck(MyLib._myGrid sender, int row)
        {
            if (sender._cellGet(row, 0) == null)
            {
                return true;
            }
            // เป็นค่าว่างหรือไม่ (ให้ลบออก)
            if (((string)sender._cellGet(row, 0)).ToString().Length == 0)
            {
                return true;
            }
            return false;
        }

        bool _icmainGridBranch__queryForInsertCheck(MyLib._myGrid sender, int row)
        {
            if (sender._cellGet(row, 0) == null)
            {
                return false;
            }
            // เป็นค่าว่างหรือไม่ (ไม่บันทึก)
            if (((string)sender._cellGet(row, 0)).ToString().Length == 0)
            {
                return false;
            }
            return true;
        }

        bool _icmainGridBranch__queryForUpdateCheck(MyLib._myGrid sender, int row)
        {
            if (sender._cellGet(row, 0) == null)
            {
                return false;
            }
            // เป็นค่าว่างหรือไม่ (ไม่บันทึก)
            if (((string)sender._cellGet(row, 0)).ToString().Length == 0)
            {
                return false;
            }
            // ดูว่าเป็น row_number เป็น 0 หรือไม่ ถ้าเป็น 0 คือเป็นรายการใหม่ (ห้ามใช้คำสั่ง Update เดี๋ยวมันจะไป Insert ให้ต่อไป)
            if (((int)sender._cellGet(row, sender._rowNumberName)) == 0)
            {
                return false;
            }
            return true;
        }

        string _icmainGridBranch__queryForUpdateWhere(MyLib._myGrid sender, int row)
        {
            int __getInt = (int)sender._cellGet(row, sender._rowNumberName);
            return (sender._rowNumberName + "=" + __getInt.ToString());
        }

        bool _icmainGridUnitOpposi__queryForRowRemoveCheck(MyLib._myGrid sender, int row)
        {
            if (sender._cellGet(row, 0) == null)
            {
                return true;
            }
            // เป็นค่าว่างหรือไม่ (ให้ลบออก)
            if (((string)sender._cellGet(row, 0)).ToString().Length == 0)
            {
                return true;
            }
            return false;
        }

        bool _icmainGridUnitOpposi__queryForInsertCheck(MyLib._myGrid sender, int row)
        {
            if (sender._cellGet(row, 0) == null)
            {
                return false;
            }
            // เป็นค่าว่างหรือไม่ (ไม่บันทึก)
            if (((string)sender._cellGet(row, 0)).ToString().Length == 0)
            {
                return false;
            }
            return true;
        }

        bool _icmainGridUnitOpposi__queryForUpdateCheck(MyLib._myGrid sender, int row)
        {
            if (sender._cellGet(row, 0) == null)
            {
                return false;
            }
            // เป็นค่าว่างหรือไม่ (ไม่บันทึก)
            if (((string)sender._cellGet(row, 0)).ToString().Length == 0)
            {
                return false;
            }
            // ดูว่าเป็น row_number เป็น 0 หรือไม่ ถ้าเป็น 0 คือเป็นรายการใหม่ (ห้ามใช้คำสั่ง Update เดี๋ยวมันจะไป Insert ให้ต่อไป)
            if (((int)sender._cellGet(row, sender._rowNumberName)) == 0)
            {
                return false;
            }
            return true;
        }

        string _icmainGridUnitOpposi__queryForUpdateWhere(MyLib._myGrid sender, int row)
        {
            int __getInt = (int)sender._cellGet(row, sender._rowNumberName);
            return (sender._rowNumberName + "=" + __getInt.ToString());
        }

        bool _icmainGridUnit__queryForRowRemoveCheck(MyLib._myGrid sender, int row)
        {
            if (sender._cellGet(row, 0) == null)
            {
                return true;
            }
            // เป็นค่าว่างหรือไม่ (ให้ลบออก)
            if (((string)sender._cellGet(row, 0)).ToString().Length == 0)
            {
                return true;
            }
            return false;
        }

        bool _icmainGridUnit__queryForInsertCheck(MyLib._myGrid sender, int row)
        {
            if (sender._cellGet(row, 0) == null)
            {
                return false;
            }
            // เป็นค่าว่างหรือไม่ (ไม่บันทึก)
            if (((string)sender._cellGet(row, 0)).ToString().Length == 0)
            {
                return false;
            }
            return true;
        }

        bool _icmainGridUnit__queryForUpdateCheck(MyLib._myGrid sender, int row)
        {
            if (sender._cellGet(row, 0) == null)
            {
                return false;
            }
            // เป็นค่าว่างหรือไม่ (ไม่บันทึก)
            if (((string)sender._cellGet(row, 0)).ToString().Length == 0)
            {
                return false;
            }
            // ดูว่าเป็น row_number เป็น 0 หรือไม่ ถ้าเป็น 0 คือเป็นรายการใหม่ (ห้ามใช้คำสั่ง Update เดี๋ยวมันจะไป Insert ให้ต่อไป)
            if (((int)sender._cellGet(row, sender._rowNumberName)) == 0)
            {
                return false;
            }
            return true;
        }

        string _icmainGridUnit__queryForUpdateWhere(MyLib._myGrid sender, int row)
        {
            int __getInt = (int)sender._cellGet(row, sender._rowNumberName);
            return (sender._rowNumberName + "=" + __getInt.ToString());
        }

        public void _clear()
        {
            this._icmainScreenTop._clear();
            this._icmainScreenMoreControl._clear();
            this._icmainGridUnit._clear();
            this._icmainGridBranch._clear();
            this._icmainScreenAccount._clear();
            this._icmainGridBarCode._clear();
            //_icmainScreenAccountControl
            if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountProfessional ||
                MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccount ||
                MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLAccountPOSProfessional)
            {
                // หน่วยนับขนาน
                this._icmainGridUnitOpposi._clear();
            }
            this._icmainScreenAccount._clear();
            this._icmainScreenTop._setCheckBox(_g.d.ic_inventory._update_price, "1");
            this._icmainScreenTop._setCheckBox(_g.d.ic_inventory._update_detail, "1");
        }

        void _saveButton_Click(object sender, EventArgs e)
        {
            _saveData(true);
        }

        public void _loadWarehouseAndLocationAuto()
        {
            Boolean __process = true;
            if (this._icmainGridBranch._rowData.Count > 0)
            {
                DialogResult __check = MessageBox.Show(MyLib._myGlobal._resource("ต้องการดึงข้อมูลใหม่มาแทนของเดิมหรือไม่"), "เตือน", MessageBoxButtons.YesNo);
                if (__check == DialogResult.No)
                {
                    __process = false;
                }
            }
            if (__process)
            {
                this._icmainGridBranch._clear();
                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                string __query = "select *,coalesce((select " + _g.d.ic_warehouse._name_1 + " from " + _g.d.ic_warehouse._table + " where " + _g.d.ic_warehouse._table + "." + _g.d.ic_warehouse._code + "=" + _g.d.ic_shelf._whcode + "),'') as wh_name_1 from " + _g.d.ic_shelf._table + " order by " + _g.d.ic_shelf._whcode + "," + _g.d.ic_shelf._code;
                DataSet __result = __myFrameWork._queryShort(__query);
                for (int __row = 0; __row < __result.Tables[0].Rows.Count; __row++)
                {
                    string __whCode = __result.Tables[0].Rows[__row][_g.d.ic_shelf._whcode].ToString();
                    string __shelfCode = __result.Tables[0].Rows[__row][_g.d.ic_shelf._code].ToString();
                    int __addr = this._icmainGridBranch._addRow();
                    this._icmainGridBranch._cellUpdate(__addr, _g.d.ic_wh_shelf._wh_code, __whCode, false);
                    this._icmainGridBranch._cellUpdate(__addr, _g.d.ic_wh_shelf._wh_name, __result.Tables[0].Rows[__row]["wh_name_1"].ToString(), false);
                    this._icmainGridBranch._cellUpdate(__addr, _g.d.ic_wh_shelf._shelf_code, __shelfCode, false);
                    this._icmainGridBranch._cellUpdate(__addr, _g.d.ic_wh_shelf._shelf_name, __result.Tables[0].Rows[__row][_g.d.ic_shelf._name_1].ToString(), false);
                }
                this._icmainGridBranch.Invalidate();
            }
        }

        private void _getWareHouseAndLocationButton_Click(object sender, EventArgs e)
        {
            this._loadWarehouseAndLocationAuto();
        }

        public class _packingClass
        {
            public string _code;
            public decimal _standValue;
            public decimal _divideValue;
            public decimal _ratio;
            public int _lineNumber;
        }

        private void _clearButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(MyLib._myGlobal._resource("ต้องการล้างข้อมูลหรือไม่"), "เตือน", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                this._icmainGridBranch._clear();
            }
        }
    }
}
