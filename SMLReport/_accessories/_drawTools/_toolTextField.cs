using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace SMLReport._design
{
    class _toolTextField : _toolLabel
    {
        public _toolTextField()
        {
            _cursor = Cursors.Cross;
        }

        public override void OnMouseDown(_drawPanel drawArea, MouseEventArgs e)
        {
            AddNewObject(drawArea, new _drawTextField(e.X, e.Y, 1, 1, drawArea._drawScale));
        }
    }
}
