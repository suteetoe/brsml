using System;
using System.Windows.Forms;
using System.Drawing;

namespace SMLReport._design
{
    /// <summary>
    /// Line tool
    /// </summary>
    public class _toolLine : _toolObject
    {
        public _toolLine()
        {
            //Cursor = new Cursor(GetType(), "Line.cur");
            _cursor = Cursors.Cross;
        }

        public override void OnMouseDown(_drawPanel drawArea, MouseEventArgs e)
        {
            AddNewObject(drawArea, new _drawLine(e.X, e.Y, e.X + 1, e.Y + 1, drawArea._drawScale));
        }

        public override void OnMouseMove(_drawPanel drawArea, MouseEventArgs e)
        {
            drawArea.Cursor = _cursor;

            if (e.Button == MouseButtons.Left)
            {
                Point point = new Point(e.X, e.Y);
                drawArea._graphicsList[0]._moveHandleTo(point, 2);
                drawArea.Invalidate();
            }
        }
    }
}