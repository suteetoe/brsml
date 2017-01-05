using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLPOSControl._food
{
    public partial class _tableViewControl : UserControl
    {
        private _tableSearchLevelControl _tableSearchLevel = new _tableSearchLevelControl();

        public _tableViewControl()
        {
            InitializeComponent();
            //
            this._tableSearchLevel.Dock = DockStyle.Fill;
            this._tableLevelPanel.Controls.Add(this._tableSearchLevel);
            //
            this._orderItemGrid._table_name = _g.d.table_order._table;
            this._orderItemGrid._addColumn(_g.d.table_order._count_number, 1, 10, 10);
            this._orderItemGrid._addColumn(_g.d.table_order._item_code, 1, 10, 10);
            this._orderItemGrid._addColumn(_g.d.table_order._item_name, 1, 10, 10);
            this._orderItemGrid._addColumn(_g.d.table_order._qty, 1, 10, 10);
            this._orderItemGrid._addColumn(_g.d.table_order._qty_send, 1, 10, 10);
        }
    }
}
