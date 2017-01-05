using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace SMLPosClient.control
{
    class _posTextbox : TextBox
    {
        private string _idResult;
        public string _id
        {
            get { return _idResult; }
            set { _idResult = value; }
        }

        public _posTextbox()
        {

        }

        public ContentAlignment _setAlignment
        {
            set
            {
                switch (value)
                {
                    case ContentAlignment.BottomLeft:
                    case ContentAlignment.MiddleLeft :
                    case ContentAlignment.TopLeft :
                        this.TextAlign = HorizontalAlignment.Left;
                        break;

                    case ContentAlignment.BottomCenter :
                    case ContentAlignment.MiddleCenter :
                    case ContentAlignment.TopCenter :
                        this.TextAlign = HorizontalAlignment.Center;
                        break;

                    case ContentAlignment.BottomRight :
                    case ContentAlignment.MiddleRight :
                    case ContentAlignment.TopRight :
                        this.TextAlign = HorizontalAlignment.Right;
                        break;


                }
            }
        }
    }
}
