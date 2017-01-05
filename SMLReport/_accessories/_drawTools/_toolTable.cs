using System;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace SMLReport._design
{
    class _toolTable : _toolRectangle
    {
        public _toolTable()
        {
            _cursor = Cursors.Cross;
        }

        public override void OnMouseDown(_drawPanel drawArea, MouseEventArgs e)
        {
            AddNewObject(drawArea, new _drawTable(e.X, e.Y, 1, 1, drawArea._drawScale, drawArea));
        }

    }
}
