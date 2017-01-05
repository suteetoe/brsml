using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace SMLERPASSET._controls
{
    public partial class _resultGrid : UserControl
    {
        struct _columnType
        {
            public int _width;
            public bool _visible;
        }

        public _resultGrid()
        {
            ArrayList __columnWidth = new ArrayList();
            // Maintain Code
            _columnType _column = new _columnType();
            _column._width = 15;
            _column._visible = true;
            __columnWidth.Add(_column);
            // Maintain Name
            _column = new _columnType();
            _column._width = 25;
            _column._visible = true;
            __columnWidth.Add(_column);
            // Maintain Unit
            _column = new _columnType();
            _column._width = 10;
            _column._visible = true;
            __columnWidth.Add(_column);
            // Maintain Date
            _column = new _columnType();
            _column._width = 15;
            _column._visible = true;
            __columnWidth.Add(_column);
            // Maintain Price
            _column = new _columnType();
            _column._width = 15;
            _column._visible = true;
            __columnWidth.Add(_column);
            // Remark
            _column = new _columnType();
            _column._width = 20;
            _column._visible = true;
            __columnWidth.Add(_column);
            // คำนวณความกว้าง
            int __sumPersent = 0;
            for (int __loop = 0; __loop < __columnWidth.Count; __loop++)
            {
                _columnType __getColumn = (_columnType)__columnWidth[__loop];
                if (__getColumn._visible)
                {
                    __sumPersent += __getColumn._width;
                }
            }
            for (int __loop = 0; __loop < __columnWidth.Count; __loop++)
            {
                _columnType __getColumn = (_columnType)__columnWidth[__loop];
                if (__getColumn._visible)
                {
                    __getColumn._width = (__getColumn._width * 100) / __sumPersent;
                }
            }
            //
            InitializeComponent();
            //
            this._myGrid1._table_name = _g.d.as_asset_maintenance_detail._table;
            this._myGrid1._width_by_persent = true;
            this._myGrid1._total_show = true;
            this._myGrid1._displayRowNumber = false;
            this._myGrid1._rowNumberWork = true;
            this._myGrid1._addColumn(_g.d.as_asset_maintenance_detail._maintain_code, 1, 10, ((_columnType)__columnWidth[0])._width, false, false, true, true);
            this._myGrid1._addColumn(_g.d.as_asset_maintenance_detail._maintain_name, 1, 100, ((_columnType)__columnWidth[1])._width, false, false, true, false);
            this._myGrid1._addColumn(_g.d.as_asset_maintenance_detail._maintain_unit, 1, 10, ((_columnType)__columnWidth[2])._width, false, false, true, true);
            this._myGrid1._addColumn(_g.d.as_asset_maintenance_detail._maintain_date, 4, 10, ((_columnType)__columnWidth[3])._width, false, false, true, false);
            this._myGrid1._addColumn(_g.d.as_asset_maintenance_detail._maintain_price, 3, 0, ((_columnType)__columnWidth[4])._width, false, false, true, false, MyLib._myGlobal._getFormatNumber("m02"));
            this._myGrid1._addColumn(_g.d.as_asset_maintenance_detail._remark, 1, 255, ((_columnType)__columnWidth[5])._width, false, false, true, false);
            this._myGrid1._addColumn(this._myGrid1._rowNumberName, 2, 0, 15, false, true, true);
        }
    }
}
