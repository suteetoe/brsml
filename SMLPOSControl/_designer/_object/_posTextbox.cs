using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace SMLPOSControl._designer._object
{
    public class _posTextbox : _posObject
    {
        public _posTextbox()
        {
            this._lineColor = Color.Black;
            this._text = "Textbox";
        }

        public override void _draw(System.Drawing.Graphics g)
        {
            // set client size from font size
            SizeF __tbSize = g.MeasureString("XXX", this._font);

            ///this._width = (int)__tbSize.Width;
            this._height = (int)__tbSize.Height;

            //Pen pen = new Pen(this._lineColor, this._penWidth * (float)_drawScale);
            //pen.DashStyle = this._lineStyle;
            //SolidBrush brush = new SolidBrush(this._backColor);

            //RectangleF __tmpRect = this._actualSize;

            //g.FillRectangle(brush, Rectangle.Round(__tmpRect));
            //g.DrawRectangle(pen, Rectangle.Round(__tmpRect));

            //pen.Dispose();
            //brush.Dispose();

            // draw เหมือน rect
            // draw sample text
            base._draw(g);
        }
    }
}
