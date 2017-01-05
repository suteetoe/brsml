using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MyLib.My_DataList
{
    public partial class _lockForEdit : Form
    {
        /// <summary>
        /// กดปุ่ม Yes
        /// </summary>
        public bool _selectYes = false;
        public _lockForEdit()
        {
            InitializeComponent();
            _message.Text = MyLib._myGlobal._userName + " : ";
            _message.Text += MyLib._myGlobal._resource("w1");
            _showAgain.Text = MyLib._myGlobal._resource("w2");
        }

        private void _buttonYes_Click(object sender, EventArgs e)
        {
            _selectYes = true;
            this.Close();
        }

        private void _buttonNo_Click(object sender, EventArgs e)
        {
            _selectYes = false;
            this.Close();
        }

        private void _lockForEdit_Load(object sender, EventArgs e)
        {
        }
    }
}