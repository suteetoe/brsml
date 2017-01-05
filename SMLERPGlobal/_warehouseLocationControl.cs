using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace _g
{
    public partial class _warehouseLocationControl : UserControl
    {
        public _warehouseLocationControl()
        {
            InitializeComponent();
            if (MyLib._myGlobal._isDesignMode == false)
            {
                this.toolStrip1.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            }

            this._gridWarehouseShelfList._alterCellUpdate += new MyLib.AfterCellUpdateEventHandler(_gridWarehouseShelfList__alterCellUpdate);
        }

        void _gridWarehouseShelfList__alterCellUpdate(object sender, int row, int column)
        {
            _gridWarehouseShelfSelected._clear();

            for (int __row = 0; __row < _gridWarehouseShelfList._rowData.Count; __row++)
            {
                string __select = _gridWarehouseShelfList._cellGet(__row, 0).ToString();
                if (__select.Equals("1"))
                {
                    int __addr = _gridWarehouseShelfSelected._addRow();
                    _gridWarehouseShelfSelected._cellUpdate(__addr, _g.d.erp_user_group_wh_shelf._wh_code, _gridWarehouseShelfList._cellGet(__row, _g.d.erp_user_group_wh_shelf._wh_code), false);
                    _gridWarehouseShelfSelected._cellUpdate(__addr, _g.d.erp_user_group_wh_shelf._wh_name, _gridWarehouseShelfList._cellGet(__row, _g.d.erp_user_group_wh_shelf._wh_name), false);
                    _gridWarehouseShelfSelected._cellUpdate(__addr, _g.d.erp_user_group_wh_shelf._shelf_code, _gridWarehouseShelfList._cellGet(__row, _g.d.erp_user_group_wh_shelf._shelf_code), false);
                    _gridWarehouseShelfSelected._cellUpdate(__addr, _g.d.erp_user_group_wh_shelf._shelf_name, _gridWarehouseShelfList._cellGet(__row, _g.d.erp_user_group_wh_shelf._shelf_name), false);
                }
            }

        }

        private void _selectAllButton_Click(object sender, EventArgs e)
        {
            for (int __row = 0; __row < _gridWarehouseShelfList._rowData.Count; __row++)
            {
                _gridWarehouseShelfList._cellUpdate(__row, 0, 1, true);
            }
        }

        private void _clearButton_Click(object sender, EventArgs e)
        {
            for (int __row = 0; __row < _gridWarehouseShelfList._rowData.Count; __row++)
            {
                _gridWarehouseShelfList._cellUpdate(__row, 0, 0, true);
            }

        }
    }
}
