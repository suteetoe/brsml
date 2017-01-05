using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace _viewCapture
{
    public class _listBoxItem
    {
        private string _id;
        private string _myText;
        private int _myImageIndex;
        // properties 

        public string ID
        {
            get { return _id; }
            set { _id = value; }
        }
        public string Text
        {
            get { return _myText; }
            set { _myText = value; }
        }
        public int ImageIndex
        {
            get { return _myImageIndex; }
            set { _myImageIndex = value; }
        }
        //constructor

        public _listBoxItem(string id,string text, int index)
        {
            _id = id;
            _myText = text;
            _myImageIndex = index;
        }
        public _listBoxItem(string id,string text) : this(id,text, -1) { }
        public _listBoxItem(string text) : this("",text, -1) { }
        public _listBoxItem() : this("") { }
        public override string ToString()
        {
            return _myText;
        }
    }

    public class _imageListBox : ListBox
    {
        private ImageList _myImageList;
        public ImageList ImageList
        {
            get { return _myImageList; }
            set { _myImageList = value; }
        }
        public _imageListBox()
        {
            // Set owner draw mode

            this.DrawMode = DrawMode.OwnerDrawFixed;
        }
        protected override void OnDrawItem(System.Windows.Forms.DrawItemEventArgs e)
        {
            if (MyLib._myGlobal._isDesignMode  == false && _myImageList != null)
            {
                e.DrawBackground();
                e.DrawFocusRectangle();
                _listBoxItem item;
                Rectangle bounds = e.Bounds;
                Size imageSize = _myImageList.ImageSize;
                try
                {
                    item = (_listBoxItem)Items[e.Index];
                    if (item.ImageIndex != -1)
                    {
                        ImageList.Draw(e.Graphics, bounds.Left, bounds.Top, item.ImageIndex);
                        e.Graphics.DrawString(item.Text, e.Font, new SolidBrush(e.ForeColor), bounds.Left, bounds.Top + imageSize.Height);
                    }
                    else
                    {
                        e.Graphics.DrawString(item.Text, e.Font, new SolidBrush(e.ForeColor), bounds.Left, bounds.Top);
                    }
                }
                catch
                {
                    if (e.Index != -1)
                    {
                        e.Graphics.DrawString(Items[e.Index].ToString(), e.Font,
                            new SolidBrush(e.ForeColor), bounds.Left, bounds.Top);
                    }
                    else
                    {
                        e.Graphics.DrawString(Text, e.Font, new SolidBrush(e.ForeColor),
                            bounds.Left, bounds.Top);
                    }
                }
            }
            base.OnDrawItem(e);
        }
    }
}
