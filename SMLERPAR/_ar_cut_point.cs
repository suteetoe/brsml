using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLERPAR
{
    public partial class _ar_cut_point : UserControl
    {
        public _ar_cut_point()
        {
            InitializeComponent();
            this._ar_cut_point1._closeButton.Click += _closeButton_Click;
            this._ar_cut_point1._myManageData1._dataList._buttonClose.Click += new EventHandler(_closeButton_Click);

        }

        void _closeButton_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
