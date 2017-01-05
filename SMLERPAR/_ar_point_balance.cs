using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLERPAR
{
    public partial class _ar_point_balance : UserControl
    {
        public _ar_point_balance()
        { 
            InitializeComponent();
            this._ar_point_balance1._closeButton.Click += new EventHandler(_closeButton_Click);
            this._ar_point_balance1._myManageData1._dataList._buttonClose.Click += new EventHandler(_closeButton_Click);
        }

        void _closeButton_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
