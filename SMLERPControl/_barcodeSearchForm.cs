using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SMLERPControl
{
    public partial class _barcodeSearchForm : Form
    {
        public _barcodeSearchForm()
        {
            InitializeComponent();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                this.Dispose();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
