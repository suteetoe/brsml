using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SMLPOSControl._designer._object
{
    public partial class _propertyForm : Form
    {
        public bool _showState = false;
        public _propertyForm()
        {
            InitializeComponent();
            this.FormClosing += new FormClosingEventHandler(_propertyForm_FormClosing);
        }

        void _propertyForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            this._showState = false;
            e.Cancel = true;
        }
    }
}
