﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLERPSO
{
    public partial class _soInquiry : UserControl
    {
        public _soInquiry()
        {
            InitializeComponent();            
            this._ictrans._myManageTrans._closeScreen += new MyLib.CloseScreenEvent(_myManageTrans__closeScreen);
        }

        void _myManageTrans__closeScreen()
        {
            this.Dispose();
        }
    }
}
