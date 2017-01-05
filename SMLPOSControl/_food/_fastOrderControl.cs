using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLPOSControl._food
{
    public partial class _fastOrderControl : UserControl
    {
        private _orderScreenControl _orderScreen = new _orderScreenControl();
        private SMLInventoryControl._itemSearchLevelControl _itemSearchLevel = new SMLInventoryControl._itemSearchLevelControl();
        public _itemGridControl _itemGrid = new _itemGridControl(0, false);

        public _fastOrderControl()
        {
            InitializeComponent();

            this._screenPanel.Controls.Add(_orderScreen);
            _orderScreen.Dock = DockStyle.Fill;

            this._itemSearchLevel.Dock = DockStyle.Fill;
            this._itemLevelPanel.Controls.Add(this._itemSearchLevel);

            this._itemGrid.Dock = DockStyle.Fill;
            this._gridPanel.Controls.Add(this._itemGrid);

        }

        private void _closeButton_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        void _clear()
        {
            this._orderScreen._clear();
            this._itemGrid._clear();
        }

        void _saveData()
        {

        }
    }
}
