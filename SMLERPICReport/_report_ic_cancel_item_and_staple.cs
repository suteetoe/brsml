using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLERPICReport
{
    public partial class _report_ic_cancel_item_and_staple : UserControl
    {
        _report_ic _report_ic1;
        public _report_ic_cancel_item_and_staple()
        {
            InitializeComponent();
            this._report_ic1 = new _report_ic();
            this._report_ic1._icType = SMLERPReportTool._reportEnum.Cancel_Item_and_Staple;
            this._report_ic1.Dock = System.Windows.Forms.DockStyle.Fill;
            this._report_ic1.Location = new System.Drawing.Point(0, 0);
            this._report_ic1.Name = "_report_ic1";
            this._report_ic1.Size = new System.Drawing.Size(1149, 574);
            this._report_ic1.TabIndex = 0;
            this.Controls.Add(this._report_ic1);
            this._report_ic1._view1._buttonClose.Click += new EventHandler(_buttonClose_Click);
        }

        void _buttonClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
