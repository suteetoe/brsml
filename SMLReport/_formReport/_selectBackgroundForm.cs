using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SMLReport._formReport
{
    public partial class _selectBackgroundForm : Form
    {
        public _selectBackgroundForm()
        {
            InitializeComponent();
        }

        private void _loadButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog __openFile = new OpenFileDialog();
            __openFile.Filter = "JPEG Images|*.jpg|GIF Images|*.gif|BITMAPS|*.bmp";
            __openFile.ShowDialog();
            try
            {
                Image __bgImage = Image.FromFile(__openFile.FileName);
                this._pictureBox.Image = __bgImage;
                //this._imageWidthText.Text = __bgImage.Width.ToString();
                //this._imageHeightText.Text = __bgImage.Height.ToString();
            }
            catch (Exception __ex)
            {
                MessageBox.Show(__ex.Message.ToString());
            }
        }

        private void _removeButton_Click(object sender, EventArgs e)
        {
            this._pictureBox.Image = null;
        }

        private void _cancelButton_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
