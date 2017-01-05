using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SMLERPControl
{
    public partial class _getPictureFullScreen : Form
    {
        public _getPictureFullScreen()
        {
            InitializeComponent();
            if (MyLib._myGlobal._isDesignMode == false)
            {
                this.toolStrip1.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            }

        }

        private void _getPictureFullScreen_Load(object sender, EventArgs e)
        {

        }

        private void _buttonFull_Click(object sender, EventArgs e)
        {
            this._pictureBox.BringToFront();
            this._pictureBox.Dock = DockStyle.Fill;
            this._pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void _buttonNormal_Click(object sender, EventArgs e)
        {
            this._pictureBox.BringToFront();
            this._pictureBox.Dock = DockStyle.None;
            this._pictureBox.SizeMode = PictureBoxSizeMode.Normal;
        }
    }
}