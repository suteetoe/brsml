using System;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using System.Globalization;

namespace SMLReport._design
{
    /// <summary>
    /// Ellipse graphic object
    /// </summary>
    public class _drawEllipse : _drawRectangle
    {
        #region Ellipse Field

        private const string _entryEllipse = "Ellipse";

        #endregion

        #region Ellipse Constructor

        public _drawEllipse()
        {
            SetRectangle(0, 0, 1, 1, _drawScale);
            _initialize();
        }

        public  _drawEllipse(float scale)
        {
            SetRectangle(0, 0, 1, 1, scale);
            _initialize();
        }

        public _drawEllipse(int x, int y, int width, int height, float scale)
        {
            _drawScale = scale;
            x = (int)(x / _drawScale);
            y = (int)(y / _drawScale);
            width = (int)(width / _drawScale);
            height = (int)(height / _drawScale);
            _actualSize = new Rectangle(x, y, width, height);
            _initialize();
        }

        #endregion

        #region Ellipse Methods

        public override void _draw(Graphics g)
        {
            Pen pen = new Pen(_lineColor, _penWidth * (float)_drawScale);
            pen.DashStyle = this._LineStyle;
            SolidBrush brush = new SolidBrush(this._backColor);

            g.FillEllipse(brush, _drawRectangle._getNormalizedRectangle(Size));
            g.DrawEllipse(pen, _drawRectangle._getNormalizedRectangle(Size));

            pen.Dispose();
            brush.Dispose();
        }

        #endregion

        #region Save Load From Stream

        public override void _saveToStream(System.Runtime.Serialization.SerializationInfo info, int orderNumber)
        {
            base._saveToStream(info, orderNumber);
        }

        public override void _loadFromStream(System.Runtime.Serialization.SerializationInfo info, int orderNumber)
        {
            base._loadFromStream(info, orderNumber);
        }

        #endregion

    }
}
