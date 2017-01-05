using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SMLERPTemplate
{
    public partial class _myOutLookForm : Form
    {
        public _myOutLookForm()
        {
            InitializeComponent();
            if (MyLib._myGlobal._isDesignMode == false)
            {
                this.toolStrip1.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            }
        }

        private void _editGroupButton_Click(object sender, EventArgs e)
        {
        }

        private void _deleteMenuButton_Click(object sender, EventArgs e)
        {
        }

        private void _addMenuButton_Click(object sender, EventArgs e)
        {
        }
    }
}
