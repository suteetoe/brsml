using System;
using System.Windows.Forms;
using System.Drawing;

namespace SMLReport._design
{
    /// <summary>
    /// Rectangle tool
    /// </summary>
    public class _toolImage : _toolObject
    {

        public _toolImage()
        {
            //Cursor = new Cursor(GetType(), "Rectangle.cur");
            _cursor = Cursors.Cross;
        }

        public override void OnMouseDown(_drawPanel drawArea, MouseEventArgs e)
        {
            AddNewObject(drawArea, new _drawImage(e.X, e.Y, 1, 1, drawArea._drawScale));
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
