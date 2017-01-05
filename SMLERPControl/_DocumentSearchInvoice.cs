using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SMLERPControl
{
    public partial class _DocumentSearchInvoice : Form
    {
        public _DocumentSearchInvoice()
        {
            InitializeComponent();
        }
        protected override void OnClosing(CancelEventArgs e)
        {

            this.Visible = false;
            e.Cancel = true;
        }
    }
}