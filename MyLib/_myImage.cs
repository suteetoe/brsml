using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace MyLib
{
    public partial class _myImage : UserControl
    {
        public int _row;
        public int _rowCount;
        public int _column;
        public int _columnCount;
        public string _fieldName;
        public Boolean _displayLabel = true;
        public _myLabel _label;
        public Boolean _isQuery = true;
        public Boolean _isGetData = true;

        public _myImage(int row, int column, int rowCount, int columnCount, string fieldName)
        {
            InitializeComponent();
            this._row = row;
            this._column = column;
            this._rowCount = rowCount;
            this._columnCount = columnCount;
            this._fieldName = fieldName;
            this._pictureBox.DoubleClick += _pictureBox_DoubleClick;
        }

        private void _pictureBox_DoubleClick(object sender, EventArgs e)
        {
            this._loadPicture();
        }

        public String _imageToBase64()
        {
            using (MemoryStream __ms = new MemoryStream())
            {
                // Convert Image to byte[]

                if (this._pictureBox.Image == null)
                    return "";

                this._pictureBox.Image.Save(__ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                byte[] __imageBytes = __ms.ToArray();

                // Convert byte[] to Base64 String
                string __base64String = Convert.ToBase64String(__imageBytes);
                return __base64String;
            }
        }

        public void _base64ToImage(string base64String)
        {
            // Convert Base64 String to byte[]
            byte[] __imageBytes = Convert.FromBase64String(base64String);
            MemoryStream __ms = new MemoryStream(__imageBytes, 0, __imageBytes.Length);
            // Convert byte[] to Image
            __ms.Write(__imageBytes, 0, __imageBytes.Length);
            Image __image = Image.FromStream(__ms, true);
            this._pictureBox.Image = __image;
        }

        private void _selectPictureButton_Click(object sender, EventArgs e)
        {
            this._loadPicture();
        }

        private void _loadPicture()
        {
            using (OpenFileDialog __dlg = new OpenFileDialog())
            {
                __dlg.Title = "Select Image";
                __dlg.Filter = "jpg files (*.jpg; *.jpeg; *.gif; *.bmp)|*.jpg; *.jpeg; *.gif; *.bmp";
                if (__dlg.ShowDialog() == DialogResult.OK)
                {
                    this._pictureBox.Image = new Bitmap(__dlg.FileName);
                }
            }
        }
    }
}
