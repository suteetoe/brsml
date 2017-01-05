using System;
using System.Runtime.Serialization;
using System.Windows.Forms;
using System.Drawing;
using System.Security.Permissions;
using System.Globalization;
using System.Collections;
using System.Diagnostics;
using System.Reflection;

namespace SMLReport._design
{
    /// <summary>
    /// List of graphic objects
    /// </summary>
    [Serializable]
    public class _graphicsList : ISerializable
    {
        private ArrayList _graphicsListResult;

        private const string entryCount = "Count";
        private const string entryType = "Type";

        public _graphicsList()
        {
            _graphicsListResult = new ArrayList();
        }

        protected _graphicsList(SerializationInfo info, StreamingContext context)
        {
            _graphicsListResult = new ArrayList();

            int n = info.GetInt32(entryCount);
            string typeName;
            object drawObject;

            for (int i = 0; i < n; i++)
            {
                typeName = info.GetString(String.Format(CultureInfo.InvariantCulture, "{0}{1}", entryType, i));

                drawObject = Assembly.GetExecutingAssembly().CreateInstance(typeName);

                ((_drawObject)drawObject)._loadFromStream(info, i);

                _graphicsListResult.Add(drawObject);
            }

        }

        /// <summary>
        /// Save object to serialization stream
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        [SecurityPermissionAttribute(SecurityAction.Demand, SerializationFormatter = true)]
        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(entryCount, _graphicsListResult.Count);

            int i = 0;

            foreach (_drawObject o in _graphicsListResult)
            {
                info.AddValue(String.Format(CultureInfo.InvariantCulture, "{0}{1}", entryType, i), o.GetType().FullName);

                o._saveToStream(info, i);

                i++;
            }
        }

        public void _drawObjectHighlight(Graphics g, float scale)
        {
            int n = _graphicsListResult.Count;
            _drawTextField o;

            // Enumerate list in reverse order
            // to get first object on the top
            for (int i = n - 1; i >= 0; i--)
            {
                if (_graphicsListResult[i].GetType() == typeof(_drawTextField))
                {
                    o = (_drawTextField)_graphicsListResult[i];
                    o._drawScale = scale;

                    o._draw(g, true);

                    if (o._selected == true)
                    {
                        o._drawTracker(g);
                    }
                }
            }            
        }

        public void _draw(Graphics g, float scale)
        {
            int n = _graphicsListResult.Count;
            _drawObject o;

            // Enumerate list in reverse order
            // to get first object on the top
            for (int i = n - 1; i >= 0; i--)
            {
                o = (_drawObject)_graphicsListResult[i];
                o._drawScale = scale;

                o._draw(g);

                if (o._selected == true)
                {
                    o._drawTracker(g);
                }
            }
        }

        /// <summary>
        /// Clear all objects in the list
        /// </summary>
        /// <returns>
        /// true if at least one object is deleted
        /// </returns>
        public bool _clear()
        {
            bool result = (_graphicsListResult.Count > 0);
            _graphicsListResult.Clear();
            return result;
        }

        /// <summary>
        /// Count and this [nIndex] allow to read all graphics objects
        /// from GraphicsList in the loop.
        /// </summary>
        public int _count
        {
            get
            {
                return _graphicsListResult.Count;
            }
        }

        public _drawObject this[int index]
        {
            get
            {
                if (index < 0 || index >= _graphicsListResult.Count)
                    return null;

                return ((_drawObject)_graphicsListResult[index]);
            }
        }

        /// <summary>
        /// SelectedCount and GetSelectedObject allow to read
        /// selected objects in the loop
        /// </summary>
        public int _selectionCount
        {
            get
            {
                int n = 0;

                foreach (_drawObject o in _graphicsListResult)
                {
                    if (o._selected)
                        n++;
                }

                return n;
            }
        }

        public _drawObject _getSelectedObject(int index)
        {
            int n = -1;

            foreach (_drawObject o in _graphicsListResult)
            {
                if (o._selected)
                {
                    n++;

                    if (n == index)
                        return o;
                }
            }

            return null;
        }

        public void _add(_drawObject obj)
        {
            // insert to the top of z-order
            obj._onGetStandardValue += new OnGetStandardValue(obj__onGetStandardValue);

            if (obj.GetType() == typeof(_drawImageField))
            {
                _drawImageField __imageField = (_drawImageField)obj;
                __imageField.GetNameImageList += new _getImageListName(__imageField_GetNameImageList);
            }
            if (obj.GetType() == typeof(_drawTable))
            {
                _drawTable __imageField = (_drawTable)obj;
                __imageField.GetNameImageList += new _getImageListName(__imageField_GetNameImageList);
            }

            _graphicsListResult.Insert(0, obj);

        }

        void __imageField_GetNameImageList(_drawObject sender)
        {
            //throw new NotImplementedException();
            if (_getImageListNameConv != null)
            {
                _getImageListNameConv(sender);
            }
        }

        void obj__onGetStandardValue(_drawObject sender)
        {
            if (_getDefaultFieldList != null)
            {
                _getDefaultFieldList(sender);
            }
        }

        public void _selectInRectangle(Rectangle rectangle)
        {
            _unselectAll();

            foreach (_drawObject o in _graphicsListResult)
            {
                if (o._intersectsWith(rectangle))
                    o._selected = true;
            }

        }

        public void _unselectAll()
        {
            foreach (_drawObject o in _graphicsListResult)
            {
                o._selected = false;
            }
        }

        public void _selectAll()
        {
            foreach (_drawObject o in _graphicsListResult)
            {
                o._selected = true;
            }
        }

        /// <summary>
        /// Delete selected items
        /// </summary>
        /// <returns>
        /// true if at least one object is deleted
        /// </returns>
        public bool _deleteSelection()
        {
            bool result = false;

            int n = _graphicsListResult.Count;

            for (int i = n - 1; i >= 0; i--)
            {
                if (((_drawObject)_graphicsListResult[i])._selected)
                {
                    _graphicsListResult.RemoveAt(i);
                    result = true;
                }
            }

            return result;
        }


        /// <summary>
        /// Move selected items to front (beginning of the list)
        /// </summary>
        /// <returns>
        /// true if at least one object is moved
        /// </returns>
        public bool _moveSelectionToFront()
        {
            int n;
            int i;
            ArrayList tempList;

            tempList = new ArrayList();
            n = _graphicsListResult.Count;

            // Read source list in reverse order, add every selected item
            // to temporary list and remove it from source list
            for (i = n - 1; i >= 0; i--)
            {
                if (((_drawObject)_graphicsListResult[i])._selected)
                {
                    tempList.Add(_graphicsListResult[i]);
                    _graphicsListResult.RemoveAt(i);
                }
            }

            // Read temporary list in direct order and insert every item
            // to the beginning of the source list
            n = tempList.Count;

            for (i = 0; i < n; i++)
            {
                _graphicsListResult.Insert(0, tempList[i]);
            }

            return (n > 0);
        }

        public enum _moveEnum
        {
            Up,
            Down,
            Left,
            Right
        }

        public void _movePosition(_moveEnum moveMode, int scale)
        {
            int n;
            int i;
            ArrayList tempList;

            tempList = new ArrayList();
            n = _graphicsListResult.Count;

            // Read source list in reverse order, add every selected item
            // to temporary list and remove it from source list
            for (i = n - 1; i >= 0; i--)
            {
                _drawObject __object = (_drawObject)_graphicsListResult[i];
                if (__object._selected)
                {
                    if (moveMode == _moveEnum.Up || moveMode == _moveEnum.Down)
                    {
                        __object._moveToPoint(new Point(__object._actualSize.X, __object._actualSize.Y + scale));
                    }
                    else
                    {
                        __object._moveToPoint(new Point(__object._actualSize.X + scale, __object._actualSize.Y));
                    }
                }
            }
        }

        /// <summary>
        /// Move selected items to back (end of the list)
        /// </summary>
        /// <returns>
        /// true if at least one object is moved
        /// </returns>
        public bool _moveSelectionToBack()
        {
            int n;
            int i;
            ArrayList tempList;

            tempList = new ArrayList();
            n = _graphicsListResult.Count;

            // Read source list in reverse order, add every selected item
            // to temporary list and remove it from source list
            for (i = n - 1; i >= 0; i--)
            {
                if (((_drawObject)_graphicsListResult[i])._selected)
                {
                    tempList.Add(_graphicsListResult[i]);
                    _graphicsListResult.RemoveAt(i);
                }
            }

            // Read temporary list in reverse order and add every item
            // to the end of the source list
            n = tempList.Count;

            for (i = n - 1; i >= 0; i--)
            {
                _graphicsListResult.Add(tempList[i]);
            }

            return (n > 0);
        }

        /// <summary>
        /// set Top edges Position selection 
        /// </summary>
        /// <returns>true if least one object is top edges</returns>
        public bool _alignSelectionTopEdges()
        {
            int n;
            int i;
            int topY = 9999;
            ArrayList tempList;

            tempList = new ArrayList();
            n = _graphicsListResult.Count;


            for (i = n - 1; i >= 0; i--)
            {
                if (((_drawObject)_graphicsListResult[i])._selected)
                {
                    tempList.Add(i);
                    if (((_drawObject)_graphicsListResult[i])._actualSize.Y < topY)
                    {
                        topY = ((_drawObject)_graphicsListResult[i])._actualSize.Y;
                    }
                    //_graphicsListResult.RemoveAt(i);
                }
            }

            n = tempList.Count;

            foreach (int __index in tempList)
            {
                // move by size
                Rectangle __tmpRect = ((_drawObject)_graphicsListResult[__index])._actualSize;
                __tmpRect.Y = topY;
                ((_drawObject)_graphicsListResult[__index])._actualSize = __tmpRect;

                //// move to _move
                //if (((_drawObject)_graphicsListResult[__index])._actualSize.Y != topY)
                //{
                //    int moveY = ((_drawObject)_graphicsListResult[__index])._actualSize.Y - topY;
                //    ((_drawObject)_graphicsListResult[__index])._move(0, moveY);
                //}

            }

            return (n > 0);
        }

        /// <summary>
        /// set Left edges position selection
        /// </summary>
        /// <returns></returns>
        public bool _alignSelectionLeftEdges()
        {
            int n;
            int i;
            int leftX = 9999;
            ArrayList tempList;

            tempList = new ArrayList();
            n = _graphicsListResult.Count;


            for (i = n - 1; i >= 0; i--)
            {
                if (((_drawObject)_graphicsListResult[i])._selected)
                {
                    tempList.Add(i);
                    if (((_drawObject)_graphicsListResult[i])._actualSize.X < leftX)
                    {
                        leftX = ((_drawObject)_graphicsListResult[i])._actualSize.X;
                    }
                }
            }

            n = tempList.Count;

            foreach (int __index in tempList)
            {
                // move by size
                Rectangle __tmpRect = ((_drawObject)_graphicsListResult[__index])._actualSize;
                __tmpRect.X = leftX;
                ((_drawObject)_graphicsListResult[__index])._actualSize = __tmpRect;

            }

            return (n > 0);
        }

        public bool _alignRightSelectionEdges()
        {
            int n;
            int i;
            int rightX = 0;
            ArrayList tempList;

            tempList = new ArrayList();
            n = _graphicsListResult.Count;


            for (i = n - 1; i >= 0; i--)
            {
                if (((_drawObject)_graphicsListResult[i])._selected)
                {
                    tempList.Add(i);
                    if ((((_drawObject)_graphicsListResult[i])._actualSize.X + ((_drawObject)_graphicsListResult[i])._actualSize.Width) > rightX)
                    {
                        rightX = ((_drawObject)_graphicsListResult[i])._actualSize.X + ((_drawObject)_graphicsListResult[i])._actualSize.Width;
                    }
                }
            }

            n = tempList.Count;

            foreach (int __index in tempList)
            {
                // move by size
                Rectangle __tmpRect = ((_drawObject)_graphicsListResult[__index])._actualSize;
                __tmpRect.X = rightX - __tmpRect.Width;
                ((_drawObject)_graphicsListResult[__index])._actualSize = __tmpRect;

            }

            return (n > 0);
        }

        public bool _alignBottomSelectionEdges()
        {
            int n;
            int i;
            int bottomY = 0;
            ArrayList tempList;

            tempList = new ArrayList();
            n = _graphicsListResult.Count;


            for (i = n - 1; i >= 0; i--)
            {
                if (((_drawObject)_graphicsListResult[i])._selected)
                {
                    tempList.Add(i);
                    if ((((_drawObject)_graphicsListResult[i])._actualSize.Y + ((_drawObject)_graphicsListResult[i])._actualSize.Height) > bottomY)
                    {
                        bottomY = ((_drawObject)_graphicsListResult[i])._actualSize.Y + ((_drawObject)_graphicsListResult[i])._actualSize.Height;
                    }
                }
            }

            n = tempList.Count;

            foreach (int __index in tempList)
            {
                // move by size
                Rectangle __tmpRect = ((_drawObject)_graphicsListResult[__index])._actualSize;
                __tmpRect.Y = bottomY - __tmpRect.Height;
                ((_drawObject)_graphicsListResult[__index])._actualSize = __tmpRect;

            }

            return (n > 0);
        }

        /// <summary>
        /// Get properties from selected objects and fill GraphicsProperties instance
        /// </summary>
        /// <returns></returns>
        private GraphicsProperties _getProperties()
        {
            GraphicsProperties properties = new GraphicsProperties();

            int n = _selectionCount;

            if (n < 1)
                return properties;

            _drawObject o = _getSelectedObject(0);

            int firstColor = o._lineColor.ToArgb();
            int firstPenWidth = o._penWidth;

            bool allColorsAreEqual = true;
            bool allWidthAreEqual = true;

            for (int i = 1; i < n; i++)
            {
                if (_getSelectedObject(i)._lineColor.ToArgb() != firstColor)
                    allColorsAreEqual = false;

                if (_getSelectedObject(i)._penWidth != firstPenWidth)
                    allWidthAreEqual = false;
            }

            if (allColorsAreEqual)
            {
                properties.ColorDefined = true;
                properties.Color = Color.FromArgb(firstColor);
            }

            if (allWidthAreEqual)
            {
                properties.PenWidthDefined = true;
                properties.PenWidth = firstPenWidth;
            }

            return properties;
        }

        /// <summary>
        /// Apply properties for all selected objects
        /// </summary>
        private void _applyProperties(GraphicsProperties properties)
        {
            foreach (_drawObject o in _graphicsListResult)
            {
                if (o._selected)
                {
                    if (properties.ColorDefined)
                    {
                        o._lineColor = properties.Color;
                        _drawObject.LastUsedColor = properties.Color;
                    }

                    if (properties.PenWidthDefined)
                    {
                        o._penWidth = properties.PenWidth;
                        _drawObject._lastUsedPenWidth = properties.PenWidth;
                    }
                }
            }
        }

        /// <summary>
        /// Show Properties dialog. Return true if list is changed
        /// </summary>
        /// <param name="parent"></param>
        /// <returns></returns>
        public bool _showPropertiesDialog(IWin32Window parent)
        {
            /*            if (SelectionCount < 1)
                            return false;

                        GraphicsProperties properties = GetProperties();
                        PropertiesDialog dlg = new PropertiesDialog();
                        dlg.Properties = properties;

                        if (dlg.ShowDialog(parent) != DialogResult.OK)
                            return false;

                        ApplyProperties(properties);
            */
            return true;
        }

        public event getCollectionTypeConverter _getDefaultFieldList;
        public event getImagesNameListConverter _getImageListNameConv;
    }

    public delegate void getCollectionTypeConverter(_drawObject sender);
    public delegate void getImagesNameListConverter(_drawObject sender);
}
