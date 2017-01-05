using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MyLib
{
    public partial class _myDialogForm : Form
    {
        public event _beforeCloseScreen _beforeClose;

        public _myDialogForm()
        {
            InitializeComponent();
        }
        private void _buttonCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            if (_beforeClose != null)
            {
                _beforeClose(this, e);
            }
            this.Dispose();

        }

        private void _buttonOk_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Yes;
            if (_beforeClose != null)
            {
                _beforeClose(this, e);
            }
            this.Dispose();
        }
    }
    public delegate void _beforeCloseScreen(object sender, EventArgs e);
    
}
