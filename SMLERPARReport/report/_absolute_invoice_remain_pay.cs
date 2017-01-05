using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using SMLERPARAPReport.condition;

namespace SMLERPARAPReport.report
{
    public partial class _absolute_invoice_remain_pay : UserControl
    {
        public _absolute_invoice_remain_pay()
        {
            InitializeComponent();
            this._view1._buttonClose.Click += new EventHandler(_buttonClose_Click);
        }

        void _buttonClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}