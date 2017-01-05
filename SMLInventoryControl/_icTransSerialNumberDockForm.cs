using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Crom.Controls.Docking;

namespace SMLInventoryControl
{
    public partial class _icTransSerialNumberDockForm : Form
    {
        public delegate Boolean SaveDataEventHandler();
        //
        public event SaveDataEventHandler _saveData;
        //
        public _icTransSerialNumberForm _serialNumber;
        public _utils._selectSerialNumberForm _searchSerialNumber;

        public _icTransSerialNumberDockForm(string docNo, string docNoOld, string refDocNo, string itemCode, string itemName, int gridRow, _g.g._transControlTypeEnum transControlType, int maxRow)
        {
            InitializeComponent();
            if (MyLib._myGlobal._isDesignMode == false)
            {
                this.toolStrip1.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            }
            this._serialNumber = new _icTransSerialNumberForm(docNo, docNoOld, refDocNo, itemCode, itemName, gridRow, transControlType, maxRow);
            //
            DockableFormInfo __form1 = this._dock.Add(this._serialNumber, Crom.Controls.Docking.zAllowedDock.All, Guid.NewGuid());
            __form1.ShowCloseButton = false;
            __form1.ShowContextMenuButton = false;
            this._dock.DockForm(__form1, DockStyle.Fill, zDockMode.Inner);
            //
            this._searchSerialNumber = new _utils._selectSerialNumberForm(transControlType,itemCode,refDocNo);
            DockableFormInfo __form2 = this._dock.Add(this._searchSerialNumber, Crom.Controls.Docking.zAllowedDock.All, Guid.NewGuid());
            __form2.ShowCloseButton = false;
            __form2.ShowContextMenuButton = false;
            this._dock.DockForm(__form2, DockStyle.Bottom, zDockMode.Inner);
            this._dock.SetWidth(__form2, this.Height / 2);
            //
            this._searchSerialNumber._dataList._gridData._mouseClick += new MyLib.MouseClickHandler(_gridData__mouseClick);
        }

        void _gridData__mouseClick(object sender, MyLib.GridCellEventArgs e)
        {
            int __row = 0;
            while (__row < this._serialNumber._grid._rowData.Count)
            {
                if (this._serialNumber._grid._cellGet(__row, _g.d.ic_trans_serial_number._serial_number).ToString().Trim().Length == 0)
                {
                    this._serialNumber._grid._rowData.RemoveAt(__row);
                }
                else
                {
                    __row++;
                }
            }
            int __addr = this._serialNumber._grid._addRow();
            string __getSerialNumber = e._text;
            this._serialNumber._grid._cellUpdate(__addr, _g.d.ic_trans_serial_number._serial_number, __getSerialNumber, true);
            this._serialNumber._grid.Invalidate();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            const int WM_KEYDOWN = 0x100;
            const int WM_SYSKEYDOWN = 0x104;

            if (msg.Msg == WM_KEYDOWN || msg.Msg == WM_SYSKEYDOWN)
            {
                switch (keyData)
                {
                    case Keys.F10:
                        if (this._saveData())
                        {
                            this.Dispose();
                        }
                        return true;
                    case Keys.Escape:
                        this._cancel();
                        return true;
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void _saveButton_Click(object sender, EventArgs e)
        {
            if (this._saveData())
            {
                this.Dispose();
            }
        }

        void _cancel()
        {
            DialogResult __result = MessageBox.Show("Cancel YES/NO", "Cancel", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2);
            if (__result == System.Windows.Forms.DialogResult.Yes)
            {
                this.Dispose();
            }
        }

        private void _cancelButton_Click(object sender, EventArgs e)
        {
            this._cancel();
        }
    }
}
