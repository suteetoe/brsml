using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BCTOSML
{
    public partial class _errorForm : Form
    {
        public _errorForm(string message)
        {
            InitializeComponent();
            this._textBox.Text = message;
        }
    }
}
