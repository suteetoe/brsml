using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLERPAR
{
    public partial class ar_point : UserControl
    {
        public ar_point()
        {
            InitializeComponent();
            this._ar_point_balance1._closeButton.Click += new EventHandler(_closeButton_Click);
            this._ar_point_balance1._myManageData1._closeScreen += new MyLib.CloseScreenEvent(_myManageData1__closeScreen);
        }

        void _closeButton_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        void _myManageData1__closeScreen()
        {
            this.Dispose();
        }
    }
}
