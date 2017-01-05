using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLERPCASHBANK
{
    public partial class _cb_chq_in_return : UserControl
    {
        public _cb_chq_in_return()
        {
            InitializeComponent();
            this._bankControl1._closeButton.Click += new EventHandler(_closeButton_Click);
            this._bankControl1._myManageBank._closeScreen += new MyLib.CloseScreenEvent(_myManageBank__closeScreen);
        }

        void _myManageBank__closeScreen()
        {
            this.Dispose();
        }

        void _closeButton_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
