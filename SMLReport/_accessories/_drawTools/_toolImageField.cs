using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace SMLReport._design
{
    class _toolImageField : _toolObject
    {
        private bool _speialLabel = false;
        public string _specialText = "";
        
        public _toolImageField()
        {
            _cursor = Cursors.Cross;
        }

        public _toolImageField(Boolean _isSpecial)
        {
            _speialLabel = _isSpecial;
            _cursor = Cursors.Cross;
        }

        public override void OnMouseDown(_drawPanel drawArea, MouseEventArgs e)
        {
            if (_speialLabel)
            {
                _drawImageField __imageField = new _drawImageField(e.X, e.Y, 1, 1, drawArea._drawScale);
                __imageField._showText = _specialText;
                if (_specialText.IndexOf("&barcode&") != -1)
                {
                    __imageField.FieldType = _FieldType.Barcode;
                }

                if (_specialText.IndexOf("&ean13&") != -1)
                {
                    __imageField._typeBarcode = _barcodeType.BarCode_EAN13;
                }

                if (_specialText.IndexOf("&code39&") != -1)
                {
                    __imageField._typeBarcode = _barcodeType.BarCode_Code39;
                }

                if (_specialText == "&itemimage&")
                {
                    __imageField.FieldType = _FieldType.Image;
                }
                AddNewObject(drawArea, __imageField);
            }
            else
            {
                AddNewObject(drawArea, new _drawImageField(e.X, e.Y, 1, 1, drawArea._drawScale));
            }
        }

        public override void OnMouseMove(_drawPanel drawArea, MouseEventArgs e)
        {
            drawArea.Cursor = _cursor;

            if (e.Button == MouseButtons.Left)
            {
                Point point = new Point(e.X, e.Y);
                drawArea._graphicsList[0]._moveHandleTo(point, 5);
                drawArea.Invalidate();
            }
        }

    }
}
