using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SMLTemplate
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            if (MyLib._myGlobal._isDesignMode == false)
            {
                this.toolStrip1.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            }
        }

        private void _addTab(String name,Control source)
        {
            source.Dock = DockStyle.Fill;
            this._tabControl.TabPages.Add(name,name);
            this._tabControl.TabPages[this._tabControl.TabPages.Count-1].Controls.Add(source);
        }

        private void _glButton_Click(object sender, EventArgs e)
        {
            this._addTab(this._glButton.Text,new _glUserControl());
        }
    }
}
