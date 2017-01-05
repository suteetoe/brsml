using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.Xml;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;

namespace SMLReport._design
{
    [XmlRootAttribute(ElementName = "_data", IsNullable = false)]
    public partial class _linkList : UserControl
    {
        [XmlArray("_linkLine"), XmlArrayItem("_linkLineList", typeof(_linkPanel))]
        public ArrayList _linkLine = new ArrayList();
        /// <summary>
        /// จำนวน Table มากที่สุด
        /// </summary>
        public int _parentMax = 0;
        //
        public _linkList()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.UseTextForAccessibility, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.CacheText, true);
            //
            this._panel.AllowDrop = true;
            this._panel.AutoSize = false;
            this._panel.DragDrop += new DragEventHandler(_panel_DragDrop);
            this._panel.DragEnter += new DragEventHandler(_panel_DragEnter);
            this._panel.ControlAdded += new ControlEventHandler(_panel_ControlAdded);
            this._panel.MouseEnter += new EventHandler(_panel_MouseEnter);
            this._panel.SizeChanged += new EventHandler(_panel_SizeChanged);
            _vScrollBar.ValueChanged += new EventHandler(_vScrollBar_ValueChanged);
            _hScrollBar.ValueChanged += new EventHandler(_hScrollBar_ValueChanged);
            this.MouseWheel += new MouseEventHandler(_linkList_MouseWheel);
            this._panel.Paint += new PaintEventHandler(_panel_Paint);
        }

        public event AfterCellUpdateEventHandler _alterCellUpdate;
        public event AfterUpdateEventHandler _afterUpdate;
        /// <summary>
        /// ดึงชื่อ Database + Table + Field เพื่อนำไปใช้งานอื่นต่อไป
        /// </summary>
        /// <returns></returns>
        public ArrayList _getAllFieldFormPanel()
        {
            ArrayList __result = new ArrayList();
            for (int __loop = 0; __loop < this._panel.Controls.Count; __loop++)
            {
                if (this._panel.Controls[__loop].GetType() == typeof(_tablePanel))
                {
                    _tablePanel __getControl = (_tablePanel)this._panel.Controls[__loop];
                    for (int __getField = 0; __getField < __getControl._tableGrid._rowData.Count; __getField++)
                    {
                        if ((int)__getControl._tableGrid._cellGet(__getField, 0) == 1)
                        {
                            string __getFieldString = __getControl._tableGrid._cellGet(__getField, 1).ToString();
                            if (__getFieldString.Split(' ')[0].ToString().Length > 0)
                            {
                                __result.Add(__getControl._tableName + "." + __getFieldString);
                            }
                        }
                    }
                }
            }
            return __result;
        }

        void _hScrollBar_ValueChanged(object sender, EventArgs e)
        {
            this._panel.Location = new Point(0 - _hScrollBar.Value, this._panel.Location.Y);
        }

        void _vScrollBar_ValueChanged(object sender, EventArgs e)
        {
            this._panel.Location = new Point(this._panel.Location.X, 0 - _vScrollBar.Value);
        }

        void _linkList_MouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta < 0)
            {
                if (_vScrollBar.Value < _vScrollBar.Maximum)
                {
                    if (_vScrollBar.Value + 50 > _vScrollBar.Maximum)
                    {
                        _vScrollBar.Value = _vScrollBar.Maximum;
                    }
                    else
                    {
                        _vScrollBar.Value += 50;
                    }
                }
            }
            else
            {
                if (_vScrollBar.Value > 1)
                {
                    if (_vScrollBar.Value - 50 < 0)
                    {
                        _vScrollBar.Value = 0;
                    }
                    else
                    {
                        _vScrollBar.Value -= 50;
                    }
                }
            }
            this.Invalidate();
        }

        void _calcScrollSize()
        {
            _hScrollBar.Visible = (this._panel.Width > this.Width) ? true : false;
            _vScrollBar.Visible = (this._panel.Height > this.Height) ? true : false;
            _vScrollBar.Minimum = 0;
            _vScrollBar.Maximum = this._panel.Height - Height;
            _vScrollBar.LargeChange = _vScrollBar.Maximum * 10 / 100;
            _hScrollBar.Minimum = 0;
            _hScrollBar.Maximum = this._panel.Width - Width;
            _hScrollBar.LargeChange = _hScrollBar.Maximum * 10 / 100;
            if (_hScrollBar.Visible == false && _vScrollBar.Visible == false)
            {
                this._panel.Location = new Point(0, 0);
            }
        }

        void _panel_SizeChanged(object sender, EventArgs e)
        {
            _calcScrollSize();
        }

        void _panel_MouseEnter(object sender, EventArgs e)
        {
            this._panel.Focus();
        }

        void _calcPanelSize()
        {
            int __maxHeight = this.Height;
            int __maxWidth = this.Width;
            for (int __loop = 0; __loop < this._panel.Controls.Count; __loop++)
            {
                if (this._panel.Controls[__loop].GetType() == typeof(_tablePanel))
                {
                    _tablePanel __getControl = (_tablePanel)this._panel.Controls[__loop];
                    if (__getControl.Location.Y + __getControl.Height > __maxHeight)
                    {
                        __maxHeight = __getControl.Location.Y + __getControl.Height + 50;
                    }
                    if (__getControl.Location.X + __getControl.Width > __maxWidth)
                    {
                        __maxWidth = __getControl.Location.X + __getControl.Width + 50;
                    }
                }
            }
            this._panel.Size = new Size(__maxWidth, __maxHeight);
            this._panel.Invalidate();
            this.Invalidate();
        }

        /// <summary>
        /// กำลังลาก Table มาวาง
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void _panel_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(MyLib._myGrid).ToString()))
            {
                MyLib._myGrid __getTableGrid = (MyLib._myGrid)e.Data.GetData(typeof(MyLib._myGrid));
                if (__getTableGrid._gridType == 1)
                {
                    e.Effect = DragDropEffects.Copy;
                }
                else
                {
                    e.Effect = DragDropEffects.None;
                }
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        _tablePanel _calcSize(_tablePanel tablePanel)
        {
            tablePanel.Height = 90 + (tablePanel._tableGrid._rowData.Count * (int)tablePanel._tableGrid._cellHeight);
            return (tablePanel);
        }

        _tablePanel _getFieldFromTable(string tableName, _tablePanel tablePanel)
        {
            this.Cursor = Cursors.WaitCursor;
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            string __getXml = __myFrameWork._getSchemaTable(MyLib._myGlobal._databaseName, tableName);
            //
            tablePanel._tableGrid._clear();
            XmlDocument __xmlDoc = new XmlDocument();
            __xmlDoc.LoadXml(__getXml);
            __xmlDoc.DocumentElement.Normalize();
            XmlElement __xmlRoot = __xmlDoc.DocumentElement;
            XmlNodeList __xmlReader = __xmlRoot.GetElementsByTagName("detail");
            for (int detail = 0; detail < __xmlReader.Count; detail++)
            {
                XmlNode __xmlFirstNode = __xmlReader.Item(detail);
                if (__xmlFirstNode.NodeType == XmlNodeType.Element)
                {
                    XmlElement __xmlTable = (XmlElement)__xmlFirstNode;
                    int __addr = tablePanel._tableGrid._addRow();
                    string __columnName = __xmlTable.GetAttribute("column_name");
                    string __columnLength = __xmlTable.GetAttribute("length");
                    string __mixedName = tableName + "." + __columnName;
                    string __getFromResource = MyLib._myResource._findResource(__mixedName, __mixedName)._str;
                    tablePanel._tableGrid._cellUpdate(__addr, tablePanel._resourceFieldName, __columnName + " : " + __getFromResource, false);
                    tablePanel._tableGrid._cellUpdate(__addr, tablePanel._resourceFieldType, __xmlTable.GetAttribute("type") + " (" + __columnLength + ")", false);
                }
            }
            this.Cursor = Cursors.Default;
            return _calcSize(tablePanel);
        }

        /// <summary>
        /// วางบน Panel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void _panel_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(MyLib._myGrid).ToString()))
            {
                MyLib._myGrid __getTableGrid = (MyLib._myGrid)e.Data.GetData(typeof(MyLib._myGrid));
                _tablePanel __newTablePanel = new _tablePanel();
                string __tableName = __getTableGrid._cellGet(__getTableGrid._selectRow, 0).ToString();
                // ตรวจสอบว่าซ้ำหรือไม่
                bool __duplicateScan = false;
                for (int __loop = 0; __loop < this._panel.Controls.Count; __loop++)
                {
                    if (this._panel.Controls[__loop].GetType() == typeof(_tablePanel))
                    {
                        _tablePanel __getControl = (_tablePanel)this._panel.Controls[__loop];
                        if (__getControl._tableName.CompareTo(__tableName) == 0)
                        {
                            __duplicateScan = true;
                            break;
                        }
                    }
                }
                if (__duplicateScan == false)
                {
                    __newTablePanel._grouper.GroupTitle = __tableName + ":" + __getTableGrid._cellGet(__getTableGrid._selectRow, 1).ToString();
                    __newTablePanel = _getFieldFromTable( __tableName, __newTablePanel);
                    __newTablePanel._tableName = __tableName;
                    __newTablePanel._tableGrid._database_name = MyLib._myGlobal._databaseName;
                    __newTablePanel._tableGrid._table_name = __tableName;
                    __newTablePanel._refresh += new _tablePanel.BottonRefreshEvent(_newTablePanel__refresh);
                    __newTablePanel._remove += new _tablePanel.BottonRemoveEvent(__newTablePanel__remove);
                    __newTablePanel._movePanel += new _tablePanel.MovePanelEvent(_newTablePanel__movePanel);
                    __newTablePanel._tableDrop += new _tablePanel.TableDropLinkEvent(_newTablePanel__tableDrop);
                    __newTablePanel._tableGrid._afterAddRow += new MyLib.AfterAddRowEventHandler(_tableGrid__afterAddRow);
                    __newTablePanel._tableGrid._alterCellUpdate += new MyLib.AfterCellUpdateEventHandler(_tableGrid__alterCellUpdate);
                    __newTablePanel.Location = this._panel.PointToClient(new Point(e.X, e.Y));
                    __newTablePanel._afterUpdate += new _tablePanel.AfterUpdateEventHandler(__newTablePanel__afterUpdate);
                    __newTablePanel._tableGrid.MouseClick += new MouseEventHandler(_tableGrid_MouseClick);
                    Graphics __myGraphics = this.CreateGraphics();
                    __myGraphics.SmoothingMode = SmoothingMode.HighQuality;
                    SizeF __stringSize = __myGraphics.MeasureString(__newTablePanel._grouper.GroupTitle, MyLib._myGlobal._myFont);
                    if ((int)__stringSize.Width + 50 > __newTablePanel.Width)
                    {
                        __newTablePanel.Width = (int)__stringSize.Width + 60;
                    }
                    this._panel.Controls.Add(__newTablePanel);
                    _parentProcess();
                }
                else
                {
                    MessageBox.Show((MyLib._myGlobal._language == 0) ? "มีรายการนี้อยู่แล้ว" : "Duplicate", "Warning");
                }
            }
        }

        /// <summary>
        /// คำนวณว่ามี Table กี่ตัว แล้วเอาไว้ใน _parentComboBox
        /// </summary>
        void _parentProcess()
        {
            _parentMax = 0;
            ArrayList __getTableControl = new ArrayList();
            for (int __loop = 0; __loop < this._panel.Controls.Count; __loop++)
            {
                if (this._panel.Controls[__loop].GetType() == typeof(_tablePanel))
                {
                    _parentMax++;
                    __getTableControl.Add((_tablePanel)this._panel.Controls[__loop]);
                }
            }
            for (int __loop = 0; __loop < __getTableControl.Count; __loop++)
            {
                _tablePanel __getControl = (_tablePanel)__getTableControl[__loop];
                __getControl._parentComboBox.SelectedIndexChanged -= new EventHandler(_parentComboBox_SelectedIndexChanged);
                __getControl._parentComboBox.Items.Clear();
                for (int __add = 1; __add <= _parentMax; __add++)
                {
                    __getControl._parentComboBox.Items.Add(__add.ToString());
                }
                __getControl._parentComboBox.SelectedIndex = __loop;
                __getControl._parentComboBox.SelectedIndexChanged += new EventHandler(_parentComboBox_SelectedIndexChanged);
            }
        }

        /// <summary>
        /// ให้ทำการย้ายตำแหน่งขึ้นมา เพื่อเรียงลำดับใหม่
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void _parentComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            _tablePanel __myTablePanel = (_tablePanel)((System.Windows.Forms.ToolStripComboBox)sender).ComboBox.Parent.Parent.Parent;
            int __getNewSelectIndex = ((ToolStripComboBox)sender).SelectedIndex;
            if (__getNewSelectIndex == _parentMax-1)
            {
                __myTablePanel.SendToBack();
            }
            else
            {
                __myTablePanel.BringToFront();
            }
            int __maxParent = 0;
            ArrayList __getTableControl = new ArrayList();
            for (int __loop = 0; __loop < this._panel.Controls.Count; __loop++)
            {
                if (this._panel.Controls[__loop].GetType() == typeof(_tablePanel))
                {
                    __maxParent++;
                    __getTableControl.Add((_tablePanel)this._panel.Controls[__loop]);
                }
            }
            for (int __parent = 0; __parent < __getTableControl.Count; __parent++)
            {
                for (int __loop = 0; __loop < __getTableControl.Count; __loop++)
                {
                    _tablePanel __getControl = (_tablePanel)__getTableControl[__loop];
                    if (__parent == __getControl._parentComboBox.SelectedIndex)
                    {
                        __getControl.SendToBack();
                    }
                }
            }
            _parentProcess();
        }

        void _tableGrid_MouseClick(object sender, MouseEventArgs e)
        {
            if (_afterUpdate != null)
            {
                _afterUpdate();
            }
        }

        void __newTablePanel__afterUpdate()
        {
            if (_afterUpdate != null)
            {
                _afterUpdate();
            }
        }

        void _tableGrid__alterCellUpdate(object sender, int row, int column)
        {
            if (_alterCellUpdate != null)
            {
                _alterCellUpdate(this, (MyLib._myGrid)sender, row, column);
            }
        }

        void _tableGrid__afterAddRow(object sender, int row)
        {
            MyLib._myGrid __getGrid = (MyLib._myGrid)sender;
            _tablePanel __getTablePanel = (_tablePanel)__getGrid.Parent.Parent;
            __getTablePanel = _calcSize(__getTablePanel);
        }

        void __newTablePanel__remove(_tablePanel sender)
        {
            int __loop = 0;
            while (__loop < _linkLine.Count)
            {
                _linkPanel __getControl = (_linkPanel)_linkLine[__loop];
                if ((__getControl._sourceTableName.CompareTo(sender._tableName) == 0) ||
                    (__getControl._targetTableName.CompareTo(sender._tableName) == 0))
                {
                    _linkLine.RemoveAt(__loop);
                    __getControl._thisControl.Dispose();
                }
                else
                {
                    __loop++;
                }
            }
            sender.Dispose();
            _parentProcess();
            this._panel.Invalidate();
        }

        void _newTablePanel__tableDrop(MyLib._myGrid source, MyLib._myGrid target)
        {
            string __getSourceFieldName = source._cellGet(source._selectRow, 1).ToString().Split(' ')[0].ToString();
            string __getTargetFieldName = target._cellGet(target._selectRowFromMouse, 1).ToString().Split(' ')[0].ToString();
            bool __found = false;
            for (int __loop = 0; __loop < _linkLine.Count; __loop++)
            {
                _linkPanel __getControl = (_linkPanel)_linkLine[__loop];
                if ((__getControl._sourceTableName.CompareTo(source._table_name) == 0 && __getControl._sourceFieldName.CompareTo(__getSourceFieldName) == 0) &&
                    (__getControl._targetTableName.CompareTo(target._table_name) == 0 && __getControl._targetFieldName.CompareTo(__getTargetFieldName) == 0))
                {
                    __found = true;
                    break;
                }
                if ((__getControl._sourceTableName.CompareTo(target._table_name) == 0 && __getControl._sourceFieldName.CompareTo(__getTargetFieldName) == 0) &&
                    (__getControl._targetTableName.CompareTo(source._table_name) == 0 && __getControl._targetFieldName.CompareTo(__getSourceFieldName) == 0))
                {
                    __found = true;
                    break;
                }
            }
            if (__found)
            {
                MessageBox.Show("Duplicate", "Warning");
            }
            else
            {
                source._cellUpdate(source._selectRow, 0, 1, true);
                target._cellUpdate(target._selectRowFromMouse, 0, 1, true);
                _linkPanel __link = new _linkPanel();
                __link._sourceTableName = source._table_name;
                __link._sourceFieldName = __getSourceFieldName;
                __link._sourceRowNumber = source._selectRow;
                __link._targetTableName = target._table_name;
                __link._targetFieldName = __getTargetFieldName;
                __link._targetRowNumber = target._selectRowFromMouse;
                __link._guid = System.Guid.NewGuid();
                __link._joinComboBox.SelectedIndex = 0;
                __link._removeLink += new _linkPanel.RemoveEvent(_link__removeLink);
                for (int __loop = 0; __loop <= 7; __loop++)
                {
                    string __strGen = "";
                    string __strSource = __link._sourceTableName + "." + __link._sourceFieldName;
                    string __strTarget = __link._targetTableName + "." + __link._targetFieldName;
                    switch (__loop)
                    {
                        case 0: __strGen = __strSource + " = " + __strTarget; break;
                        case 1: __strGen = __strSource + " = " + __strTarget + "(+)"; break;
                        case 2: __strGen = __strSource + "(+)" + " = " + __strTarget; break;
                        case 3: __strGen = __strSource + " > " + __strTarget; break;
                        case 4: __strGen = __strSource + " >= " + __strTarget; break;
                        case 5: __strGen = __strSource + " < " + __strTarget; break;
                        case 6: __strGen = __strSource + " <= " + __strTarget; break;
                        case 7: __strGen = __strSource + " != " + __strTarget; break;
                    }
                    __link._listBox.Items.Add(__strGen);
                    Graphics __myGraphics = this.CreateGraphics();
                    __myGraphics.SmoothingMode = SmoothingMode.HighQuality;
                    SizeF __stringSize = __myGraphics.MeasureString(__strGen, MyLib._myGlobal._myFont);
                    if (__link._listBox.Width < (int)__stringSize.Width + 5)
                    {
                        __link._listBox.Width = (int)__stringSize.Width + 5;
                    }
                }
                __link._listBox.SelectedIndex = 0;
                __link._calcSize();
                __link._joinComboBox.SelectedIndexChanged += new EventHandler(_joinComboBox_SelectedIndexChanged);
                __link._listBox.SelectedValueChanged += new EventHandler(_listBox_SelectedValueChanged);
                this._panel.Controls.Add(__link);
                _parentProcess();
            }
        }

        void _listBox_SelectedValueChanged(object sender, EventArgs e)
        {
            if (_afterUpdate != null)
            {
                _afterUpdate();
            }
        }

        void _joinComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_afterUpdate != null)
            {
                _afterUpdate();
            }
        }

        void _link__removeLink(_linkPanel sender)
        {
            for (int __loop = 0; __loop < _linkLine.Count; __loop++)
            {
                if (((_linkPanel)_linkLine[__loop])._guid == sender._guid)
                {
                    _linkLine.RemoveAt(__loop);
                    sender._thisControl.Dispose();
                    break;
                }
            }
            this._panel.Invalidate();
        }

        void _newTablePanel__movePanel(_tablePanel sender)
        {
            _calcPanelSize();
        }

        /// <summary>
        /// ดึงชื่อ Field ทั้งหมดใหม่
        /// </summary>
        /// <param name="sender"></param>
        void _newTablePanel__refresh(_tablePanel sender)
        {
            sender = _getFieldFromTable( sender._tableName, sender);
        }

        private void toolStripContainer1_ContentPanel_Load(object sender, EventArgs e)
        {
        }

        void _linkCalcPosition(_linkPanel link)
        {
            _tablePanel __source = null;
            _tablePanel __target = null;
            for (int __loop = 0; __loop < this._panel.Controls.Count; __loop++)
            {
                if (this._panel.Controls[__loop].GetType() == typeof(_tablePanel))
                {
                    _tablePanel __getControl = (_tablePanel)this._panel.Controls[__loop];
                    if (__source == null)
                    {
                        if (__getControl._tableName.CompareTo(link._sourceTableName) == 0)
                        {
                            __source = __getControl;
                        }
                    }
                    if (__target == null)
                    {
                        if (__getControl._tableName.CompareTo(link._targetTableName) == 0)
                        {
                            __target = __getControl;
                        }
                    }
                    if (__source != null && __target != null)
                    {
                        break;
                    }
                }
            }
            if (__source != null && __target != null)
            {
                link.Location = new Point(-100, -100);
                link._source = __source;
                link._target = __target;
                _linkLine.Add(link);
                this._panel.Invalidate();
            }
        }

        void _panel_ControlAdded(object sender, ControlEventArgs e)
        {
            e.Control.SendToBack();
            if (e.Control.GetType() == typeof(_linkPanel))
            {
                _linkCalcPosition((_linkPanel)e.Control);
            }
            if (e.Control.GetType() == typeof(_tablePanel))
            {
                _calcPanelSize();
            }
            if (_afterUpdate != null)
            {
                _afterUpdate();
            }
        }

        Point _calcLineSource(_linkPanel myLink)
        {
            Point __result = new Point();
            __result.X = myLink._source.Location.X;
            __result.Y = myLink._source.Location.Y + myLink._source._tableGrid.Location.Y + ((myLink._sourceRowNumber + 1) * (int)myLink._source._tableGrid._cellHeight) + ((int)myLink._source._tableGrid._cellHeight / 2);
            return __result;
        }

        Point _calcLineTarget(_linkPanel myLink)
        {
            Point __result = new Point();
            __result.X = myLink._target.Location.X;
            __result.Y = myLink._target.Location.Y + myLink._target._tableGrid.Location.Y + ((myLink._targetRowNumber + 1) * (int)myLink._target._tableGrid._cellHeight) + ((int)myLink._target._tableGrid._cellHeight / 2);
            return __result;
        }

        void _panel_Paint(object sender, PaintEventArgs pe)
        {
            Graphics __g = pe.Graphics;
            Pen __myPen = new Pen(Color.Black, 0);
            for (int __loop = 0; __loop < _linkLine.Count; __loop++)
            {
                _linkPanel __getLink = (_linkPanel)_linkLine[__loop];
                int __holdLinePlusSource = -10;
                int __holdLinePlusTarget = -10;
                Point __sourcePosition = _calcLineSource(__getLink);
                Point __targetPosition = _calcLineTarget(__getLink);
                if (__sourcePosition.X + (__getLink._source.Width / 2) < __targetPosition.X)
                {
                    __sourcePosition.X = __getLink._source.Location.X + __getLink._source.Width;
                    __holdLinePlusSource = 10;
                    __holdLinePlusTarget = -10;
                }
                else
                    if (__sourcePosition.X > __targetPosition.X + __getLink._target.Width)
                    {
                        __targetPosition.X = __getLink._target.Location.X + __getLink._target.Width;
                        __holdLinePlusTarget = 10;
                    }
                Point[] __myWay = {
                    __sourcePosition,
                    new Point(__sourcePosition.X +__holdLinePlusSource,__sourcePosition.Y),
                    new Point(__targetPosition.X + __holdLinePlusTarget,__targetPosition.Y),
                    __targetPosition
                };
                __g.DrawLines(__myPen, __myWay);
                // draw Arrow
                if (__getLink._target.Location.X == __targetPosition.X)
                {
                    Point[] __myArrow = { 
                        new Point(__targetPosition.X-5,__targetPosition.Y-5),
                        __targetPosition,
                        new Point(__targetPosition.X-5,__targetPosition.Y+5)
                    };
                    __g.DrawLines(__myPen, __myArrow);
                }
                else
                {
                    Point[] __myArrow = { 
                        new Point(__targetPosition.X+5,__targetPosition.Y-5),
                        __targetPosition,
                        new Point(__targetPosition.X+5,__targetPosition.Y+5)
                    };
                    __g.DrawLines(__myPen, __myArrow);
                }
                Point __newPosition = new Point();
                if (__sourcePosition.Y < __targetPosition.Y)
                {
                    __newPosition.Y = __sourcePosition.Y + ((__targetPosition.Y - __sourcePosition.Y) / 2);
                }
                else
                {
                    __newPosition.Y = __targetPosition.Y + ((__sourcePosition.Y - __targetPosition.Y) / 2);
                }
                if (__sourcePosition.X < __targetPosition.X)
                {
                    __newPosition.X = __sourcePosition.X + ((__targetPosition.X - __sourcePosition.X) / 2);
                }
                else
                {
                    __newPosition.X = __targetPosition.X + ((__sourcePosition.X - __targetPosition.X) / 2);
                }
                __newPosition.X -= __getLink.Width / 2;
                __newPosition.Y -= __getLink.Height / 2;
                __getLink.Location = __newPosition;
            }
        }
    }

    public delegate void AfterCellUpdateEventHandler(object sender, MyLib._myGrid _grid, int row, int column);
    public delegate void AfterUpdateEventHandler();
}
