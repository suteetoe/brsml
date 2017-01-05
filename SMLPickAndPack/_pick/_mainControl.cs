using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Crom.Controls.Docking;

namespace SMLPickAndPack._pick
{
    public partial class _mainControl : UserControl
    {
        private Form _formInvoice;
        private Form _formItemGrid;
        private DockContainer _dock;
        private _invoiceControl _invoice;
        private _selectItemControl _itemGrid;

        public _mainControl()
        {
            InitializeComponent();
            this.Load += (object sender, EventArgs e) =>
            {
                this._formInvoice = new Form();
                this._formItemGrid = new Form();
                //
                this._dock = new DockContainer();
                this._invoice = new _invoiceControl();
                this._itemGrid = new _selectItemControl();
                //
                this._dock.Dock = DockStyle.Fill;
                this.Controls.Add(this._dock);
                //
                this._invoice.Dock = DockStyle.Fill;
                DockableFormInfo __formDock1 = this._dock.Add(this._formInvoice, Crom.Controls.Docking.zAllowedDock.All, Guid.NewGuid());
                __formDock1.ShowCloseButton = false;
                __formDock1.ShowContextMenuButton = false;
                this._dock.DockForm(__formDock1, DockStyle.Left, zDockMode.Inner);
                //
                this._itemGrid.Dock = DockStyle.Fill;
                DockableFormInfo __formDock2 = this._dock.Add(this._formItemGrid, Crom.Controls.Docking.zAllowedDock.All, Guid.NewGuid());
                __formDock2.ShowCloseButton = false;
                __formDock2.ShowContextMenuButton = false;
                this._dock.DockForm(__formDock2, DockStyle.Top, zDockMode.Inner);
                //
                this._formInvoice.BackColor = Color.WhiteSmoke;
                this._formInvoice.Padding = new Padding(0, 0, 0, 0);
                this._formInvoice.Text = MyLib._myGlobal._resource("เลือกเอกสาร");
                this._formInvoice.Controls.Add(this._invoice);
                //
                this._formItemGrid.BackColor = Color.WhiteSmoke;
                this._formItemGrid.Padding = new Padding(0, 0, 0, 0);
                this._formItemGrid.Text = MyLib._myGlobal._resource("เลือกสินค้าเพื่อสั่งหยิบ");
                this._formItemGrid.Controls.Add(this._itemGrid);
            };
        }
    }
}
