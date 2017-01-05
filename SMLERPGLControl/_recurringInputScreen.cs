using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace SMLERPGLControl
{
    public partial class _recurringInputScreen : Form
    {
        public _recurringInputScreen()
        {
            InitializeComponent();
        }

        [DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr handle, UInt32 message, int wParam, int lParam);

        private const UInt32 WM_LBUTTONDOWN = 0x201;
        private const UInt32 WM_LBUTTONUP = 0x202;
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F12)
            {

                SendMessage(_saveButton.Handle, WM_LBUTTONDOWN, 0, 0);
                SendMessage(_saveButton.Handle, WM_LBUTTONUP, 0, 0);
                return true;
            }
            else
                if (keyData == Keys.Escape)
                {
                    SendMessage(_cancelButton.Handle, WM_LBUTTONDOWN, 0, 0);
                    SendMessage(_cancelButton.Handle, WM_LBUTTONUP, 0, 0);
                    return true;
                }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void _cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void _recurringInputScreen_Load(object sender, EventArgs e)
        {
            this._codeTextBox.textBox.Focus();
        }
    }
}