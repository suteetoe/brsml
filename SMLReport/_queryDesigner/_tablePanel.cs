using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLReport._design
{
    public partial class _tablePanel : UserControl
    {
        string _gridName = "MyLib._myGrid";
        public string _resourceFieldCheck = "Check";
        public string _resourceFieldName = "field_name";
        public string _resourceFieldType = "field_type";
        bool _controlMoving = false;
        Point _oldLocation;
        public string _tableName;
        //
        public _tablePanel()
        {
            InitializeComponent();
            if (MyLib._myGlobal._isDesignMode == false)
            {
                this._toolBar.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            }
            this._tableGrid._isEdit = false;
            this._tableGrid._gridType = 2;
            this._tableGrid.AllowDrop = true;
            this._tableGrid._displayRowNumber = false;
            this._tableGrid._addColumn(_resourceFieldCheck, 11, 1, 10, true, false, false, false);
            this._tableGrid._addColumn(_resourceFieldName, 1, 255, 60, false, false, false, false);
            this._tableGrid._addColumn(_resourceFieldType, 1, 255, 30, false, false, false, false);
            this._grouper.MouseDown += new MouseEventHandler(_grouper_MouseDown);
            this._grouper.MouseUp += new MouseEventHandler(_grouper_MouseUp);
            this._grouper.MouseMove += new MouseEventHandler(_grouper_MouseMove);
            this._tableGrid.DragDrop += new DragEventHandler(_tableGrid_DragDrop);
            this._tableGrid.DragEnter += new DragEventHandler(_tableGrid_DragEnter);
        }

        public event AfterUpdateEventHandler _afterUpdate;

        void _tableGrid_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(_gridName))
            {
                MyLib._myGrid __getTableGrid = (MyLib._myGrid)e.Data.GetData(typeof(MyLib._myGrid));
                if (__getTableGrid._gridType == 2 && (__getTableGrid._table_name.CompareTo(this._tableGrid._table_name) != 0))
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

        void _tableGrid_DragDrop(object sender, DragEventArgs e)
        {
            MyLib._myGrid __getTableGrid = (MyLib._myGrid)e.Data.GetData(typeof(MyLib._myGrid));
            if (__getTableGrid._gridType == 2 && __getTableGrid._table_name.CompareTo(this._tableGrid._table_name) != 0)
            {
                if (_tableDrop != null)
                {
                    _tableDrop(__getTableGrid, this._tableGrid);
                }
            }
        }

        void _grouper_MouseMove(object sender, MouseEventArgs e)
        {
            if (_controlMoving)
            {
                Point __newLocation = Parent.PointToClient(Control.MousePosition);
                Point __newPoint = new Point(__newLocation.X - _oldLocation.X, __newLocation.Y - _oldLocation.Y);
                if (__newPoint.X < 0)
                {
                    __newPoint.X = 0;
                }
                if (__newPoint.Y < 0)
                {
                    __newPoint.Y = 0;
                }
                this.Location = __newPoint;
                if (_movePanel != null)
                {
                    _movePanel(this);
                }
                ((Control)sender).Invalidate();
            }
        }

        void _grouper_MouseUp(object sender, MouseEventArgs e)
        {
            _controlMoving = false;
        }

        void _grouper_MouseDown(object sender, MouseEventArgs e)
        {
            _controlMoving = true;
            _oldLocation = this._grouper.PointToClient(Control.MousePosition);
        }

        private void _buttonSelectAll_Click(object sender, EventArgs e)
        {
            for (int __loop = 0; __loop < _tableGrid._rowData.Count; __loop++)
            {
                _tableGrid._cellUpdate(__loop, _resourceFieldCheck, 1, true);
            }
            this._tableGrid.Invalidate();
            if (_afterUpdate != null)
            {
                _afterUpdate();
            }
        }

        private void _buttonDeselectAll_Click(object sender, EventArgs e)
        {
            for (int __loop = 0; __loop < _tableGrid._rowData.Count; __loop++)
            {
                _tableGrid._cellUpdate(__loop, _resourceFieldCheck, 0, true);
            }
            this._tableGrid.Invalidate();
            if (_afterUpdate != null)
            {
                _afterUpdate();
            }
        }

        public event BottonRefreshEvent _refresh;
        public event BottonRemoveEvent _remove;
        public event MovePanelEvent _movePanel;
        public event TableDropLinkEvent _tableDrop;

        private void _buttonRefresh_Click(object sender, EventArgs e)
        {
            for (int __loop = 0; __loop < _tableGrid._rowData.Count; __loop++)
            {
                _tableGrid._cellUpdate(__loop, _resourceFieldCheck, 0, true);
            }
            if (_refresh != null)
            {
                _refresh(this);
            }
            this._tableGrid.Invalidate();
            if (_afterUpdate != null)
            {
                _afterUpdate();
            }
        }

        public delegate void BottonRefreshEvent(_tablePanel sender);
        public delegate void BottonRemoveEvent(_tablePanel sender);
        public delegate void MovePanelEvent(_tablePanel sender);
        public delegate void TableDropLinkEvent(MyLib._myGrid source, MyLib._myGrid target);

        private void _buttonRemove_Click(object sender, EventArgs e)
        {
            for (int __loop = 0; __loop < _tableGrid._rowData.Count; __loop++)
            {
                _tableGrid._cellUpdate(__loop, _resourceFieldCheck, 0, true);
            }
            if (_afterUpdate != null)
            {
                _afterUpdate();
            }
            if (_remove != null)
            {
                _remove(this);
            }
        }
        public delegate void AfterUpdateEventHandler();

        private void _tableGrid_Load(object sender, EventArgs e)
        {

        }
    }
}
