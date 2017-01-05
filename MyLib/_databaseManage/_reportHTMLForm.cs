using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MyLib._databaseManage
{
    public partial class _reportHTMLForm : Form
    {
        public _reportHTMLForm()
        {
            InitializeComponent();
        }

        private void _buttonPrint_Click(object sender, EventArgs e)
        {
            _htmlWebBrowser.ShowPrintPreviewDialog();
        }
    }
}
