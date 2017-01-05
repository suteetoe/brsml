using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
//using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SMLERPAR
{
    public partial class _ar : UserControl
    {
        public _ar()
        {
            InitializeComponent();
            this._ar1._closeButton.Click += new EventHandler(_closeButton_Click);
            this._ar1._myManageData1._closeScreen += new MyLib.CloseScreenEvent(_myManageData1__closeScreen);

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
