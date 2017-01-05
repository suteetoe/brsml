using System;
using System.Windows.Forms;

namespace SMLReport._design
{
    /// <summary>
    /// Ellipse tool
    /// </summary>
    public class _toolEllipse : _toolRectangle
    {
        public _toolEllipse()
        {
            //Cursor = new Cursor(GetType(), "Ellipse.cur");
            _cursor = Cursors.Cross;
        }

        public override void OnMouseDown(_drawPanel drawArea, MouseEventArgs e)
        {
            AddNewObject(drawArea, new _drawEllipse(e.X, e.Y, 1, 1, drawArea._drawScale));
        }

    }
}
