using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using SMLReport._design;

namespace SMLPOSControl._designer._object
{
    /// <summary>
    /// Label สำหรับ POS
    /// </summary>
    public class _posLabel : _posObject
    {
        private Color _shadowColorResult = Color.Black;
        private bool _useShadowResult = false;
        private float _shadowXOffsetResult = 1;
        private float _shadowYOffsetResult = 1;

        private Color _backColorResult = Color.Transparent;
        private Color _LineColor = Color.Transparent;

        /// <summary>การใช้เงา</summary>
        public bool _useShadow
        {
            get { return _useShadowResult; }
            set { _useShadowResult = value; }
        }

        /// <summary>สีเงา</summary>
        public Color _shadowColor
        {
            get { return _shadowColorResult; }
            set { _shadowColorResult = value; }
        }

        /// <summary>เงา ด้าน X</summary>
        public float _shadowXOffset
        {
            get { return _shadowXOffsetResult; }
            set { _shadowXOffsetResult = value; }
        }

        /// <summary>เงา ด้าน Y</summary>
        public float _shadowYOffset
        {
            get { return _shadowYOffsetResult; }
            set { _shadowYOffsetResult = value; }
        }

        /// <summary>สีพิ้นหลัง</summary>
        public new Color _backColor
        {
            get { return _backColorResult; }
            set { _backColorResult = value; }
        }

        

        public _posLabel()
        {
        }

        public override void _draw(System.Drawing.Graphics g)
        {

            // fill bg
            RectangleF __drawStrRect = _drawRectangle._getNormalizedRectangle(this._actualSize);
            SolidBrush __BgBrush = new SolidBrush(this._backColor);

            g.FillRectangle(__BgBrush, Rectangle.Round(__drawStrRect));

            // draw shadow
            if (_useShadowResult)
            {
                PointF __getDrawPoint = _getPointTextAlingDraw(__drawStrRect.Width, __drawStrRect.Height, _textSizeResult.Width, _textSizeResult.Height, this._textAlign, _padding);
                g.DrawString(this._text, _font, new SolidBrush(_shadowColor), (_shadowXOffset + __getDrawPoint.X + _actualSize.X), (_shadowYOffset + __getDrawPoint.Y + _actualSize.Y), StringFormat.GenericTypographic);

            }

            base._backColor = Color.Transparent;
            base._draw(g);

            // draw string and shadow

        }
    }
}
