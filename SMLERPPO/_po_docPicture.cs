using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLERPPO
{
    public partial class _po_docPicture : UserControl
    {
        public _po_docPicture()
        {
            InitializeComponent();
            this._documentPicture1._closeButton.Click += new EventHandler(_closeButton_Click);
            this._documentPicture1._myManageData1._closeScreen += new MyLib.CloseScreenEvent(_myManageData1__closeScreen);
        }

        void _myManageData1__closeScreen()
        {
            this.Dispose();
        }

        void _closeButton_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
