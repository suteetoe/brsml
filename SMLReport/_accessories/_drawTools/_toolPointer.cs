using System;
using System.Windows.Forms;
using System.Drawing;


namespace SMLReport._design
{
    /// <summary>
    /// Pointer tool
    /// </summary>
    public class _toolPointer : _tool
    {
        private enum SelectionMode
        {
            None,
            NetSelection,   // group selection is active
            Move,           // object(s) are moves
            Size            // object is resized
        }

        private SelectionMode selectMode = SelectionMode.None;

        // Object which is currently resized:
        private _drawObject _myObjectResized;
        private int _myObjectResizedHandle;

        // Keep state about last and current point (used to move and resize objects)
        private Point _lastPointResult = new Point(0, 0);
        private Point _startPointResult = new Point(0, 0);

        public _toolPointer()
        {
        }

        /// <summary>
        /// Left mouse button is pressed
        /// </summary>
        /// <param name="drawArea"></param>
        /// <param name="e"></param>
        public override void OnMouseDown(_drawPanel drawArea, MouseEventArgs e)
        {
            selectMode = SelectionMode.None;
            Point point = new Point(e.X, e.Y);

            // Test for resizing (only if control is selected, cursor is on the handle)
            int n = drawArea._graphicsList._selectionCount;

            for (int i = 0; i < n; i++)
            {
                _drawObject o = drawArea._graphicsList._getSelectedObject(i);
                int handleNumber = o._hitTest(point);

                if (handleNumber > 0)
                {
                    if (o._lock == false)
                    {
                        selectMode = SelectionMode.Size;

                        // keep resized object in class members
                        _myObjectResized = o;
                        _myObjectResizedHandle = handleNumber;

                        // Since we want to resize only one object, unselect all other objects
                        drawArea._graphicsList._unselectAll();
                    }
                        o._selected = true;

                    break;
                }
            }

            // Test for move (cursor is on the object)
            if (selectMode == SelectionMode.None)
            {
                int n1 = drawArea._graphicsList._count;
                _drawObject o = null;

                for (int i = 0; i < n1; i++)
                {
                    if (drawArea._graphicsList[i]._hitTest(point) == 0)
                    {
                        o = drawArea._graphicsList[i];
                        break;
                    }
                }

                if (o != null)
                {
                    if (o._lock == false)
                    {
                        selectMode = SelectionMode.Move;

                        // Unselect all if Ctrl is not pressed and clicked object is not selected yet
                        if ((Control.ModifierKeys & Keys.Control) == 0 && !o._selected)
                            drawArea._graphicsList._unselectAll();
                    // Select clicked object
                    o._selected = true;

                    drawArea.Cursor = Cursors.SizeAll;
                    }
                }
            }

            // Net selection
            if (selectMode == SelectionMode.None)
            {
                // click on background
                if ((Control.ModifierKeys & Keys.Control) == 0)
                    drawArea._graphicsList._unselectAll();

                selectMode = SelectionMode.NetSelection;
                drawArea._drawNetRectangle = true;
            }

            _lastPointResult.X = e.X;
            _lastPointResult.Y = e.Y;
            _startPointResult.X = e.X;
            _startPointResult.Y = e.Y;

            drawArea.Capture = true;

            drawArea._netRectangle = _drawRectangle._getNormalizedRectangle(_startPointResult, _lastPointResult);

            drawArea.Invalidate();
        }


        /// <summary>
        /// Mouse is moved.
        /// None button is pressed, ot left button is pressed.
        /// </summary>
        /// <param name="drawArea"></param>
        /// <param name="e"></param>
        public override void OnMouseMove(_drawPanel drawArea, MouseEventArgs e)
        {
            Point point = new Point(e.X, e.Y);

            // set cursor when mouse button is not pressed
            if (e.Button == MouseButtons.None)
            {
                Cursor cursor = null;
                for (int i = 0; i < drawArea._graphicsList._count; i++)
                {
                    int n = drawArea._graphicsList[i]._hitTest(point);

                    if (n > 0)
                    {
                        if (drawArea._graphicsList[i]._lock == false)
                        {
                            cursor = drawArea._graphicsList[i]._getHandleCursor(n);
                        }
                        break;
                    }
                }
                if (cursor == null)
                    cursor = Cursors.Default;

                drawArea.Cursor = cursor;

                return;
            }

            if (e.Button != MouseButtons.Left)
                return;

            /// Left button is pressed

            // Find difference between previous and current position
            int dx = e.X - _lastPointResult.X;
            int dy = e.Y - _lastPointResult.Y;

            _lastPointResult.X = e.X;
            _lastPointResult.Y = e.Y;

            // resize
            if (selectMode == SelectionMode.Size)
            {
                if (_myObjectResized != null)
                {
                    _myObjectResized._moveHandleTo(point, _myObjectResizedHandle);
                    drawArea.Invalidate();
                }
            }

            // move
            if (selectMode == SelectionMode.Move)
            {
                if (drawArea.AllowDrop && Control.ModifierKeys == Keys.Shift)
                {
                    drawArea.DoDragDrop(drawArea, DragDropEffects.Move);
                }
                else
                {
                    int n = drawArea._graphicsList._selectionCount;
                    for (int i = 0; i < n; i++)
                    {
                        drawArea._graphicsList._getSelectedObject(i)._move(dx, dy);
                    }
                    drawArea.Cursor = Cursors.SizeAll;
                    drawArea.Invalidate();
                }
            }

            if (selectMode == SelectionMode.NetSelection)
            {
                drawArea._netRectangle = _drawRectangle._getNormalizedRectangle(_startPointResult, _lastPointResult);
                drawArea.Invalidate();
                return;
            }

        }

        /// <summary>
        /// Right mouse button is released
        /// </summary>
        /// <param name="drawArea"></param>
        /// <param name="e"></param>
        public override void OnMouseUp(_drawPanel drawArea, MouseEventArgs e)
        {
            if (selectMode == SelectionMode.NetSelection)
            {
                // Group selection
                drawArea._graphicsList._selectInRectangle(drawArea._netRectangle);

                selectMode = SelectionMode.None;
                drawArea._drawNetRectangle = false;
            }

            if (_myObjectResized != null)
            {
                // after resizing
                _myObjectResized._normalize();
                _myObjectResized = null;
            }

            drawArea.Capture = false;
            drawArea.Invalidate();
        }
    }
}
