using System;
using System.Windows.Forms;
using System.Drawing;

namespace SMLReport._design
{
    /// <summary>
    /// Base class for all tools which create new graphic object
    /// </summary>
    public abstract class _toolObject : _tool
    {
        private Cursor _cursorResult;

        /// <summary>
        /// Tool cursor.
        /// </summary>
        protected Cursor _cursor
        {
            get
            {
                return _cursorResult;
            }
            set
            {
                _cursorResult = value;
            }
        }


        /// <summary>
        /// Left mouse is released.
        /// New object is created and resized.
        /// </summary>
        /// <param name="drawArea"></param>
        /// <param name="e"></param>
        public override void OnMouseUp(_drawPanel drawArea, MouseEventArgs e)
        {
            drawArea._graphicsList[0]._normalize();
            drawArea._activeTool = _design._drawToolType.Pointer;

            drawArea.Capture = false;
            drawArea.Invalidate();
        }

        /// <summary>
        /// Add new object to draw area.
        /// Function is called when user left-clicks draw area,
        /// and one of ToolObject-derived tools is active.
        /// </summary>
        /// <param name="drawArea"></param>
        /// <param name="o"></param>
        protected void AddNewObject(_drawPanel drawArea, _drawObject o)
        {
            o._Owner = drawArea._ownerForm;
            drawArea._graphicsList._unselectAll();

            o._selected = true;
            drawArea._graphicsList._add(o);

            drawArea.Capture = true;
            drawArea.Invalidate();
        }
    }
}
