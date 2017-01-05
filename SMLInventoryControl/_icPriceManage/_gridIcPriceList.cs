using System;
using System.Collections.Generic;
using System.Text;

namespace SMLInventoryControl._icPriceManage
{
    public class _gridIcPriceList : MyLib._myGrid
    {
        public _gridIcPriceList()
        {
            this._clear();
            this._columnListTop.Clear();
            this._columnTopActive = true;
            this._width_by_persent = false;
            this._isEdit = false;

            this._table_name = _g.d.ic_price_adjust_detail._table;
            this._addColumn(_g.d.ic_price_adjust_detail._ic_code, 1, 10, 70);
            this._addColumn(_g.d.ic_price_adjust_detail._ic_name, 1, 10, 150);
            this._addColumn(_g.d.ic_price_adjust_detail._unit_code, 2, 10, 60);
            this._addColumn(_g.d.ic_price_adjust_detail._price_mode, 2, 10, 80);
            
            // ราคาหลังร้าน
            this._addColumn(_g.d.ic_price_adjust_detail._sale_type, 1, 10, 80);

            this._addColumn(_g.d.ic_price_adjust_detail._price_exclude_vat, 3, 10, 80, false);
            this._addColumn(_g.d.ic_price_adjust_detail._price_include_vat, 3, 10, 80, false);

            // ราคา barcode
            this._addColumn(_g.d.ic_price_adjust_detail._barcode_price_1, 3, 10, 70, false);
            this._addColumn(_g.d.ic_price_adjust_detail._barcode_member_price_1, 3, 10, 70, false);
            this._addColumn(_g.d.ic_price_adjust_detail._barcode_price_2, 3, 10, 70, false);
            this._addColumn(_g.d.ic_price_adjust_detail._barcode_member_price_2, 3, 10, 70, false);
            this._addColumn(_g.d.ic_price_adjust_detail._barcode_price_3, 3, 10, 70, false);
            this._addColumn(_g.d.ic_price_adjust_detail._barcode_member_price_3, 3, 10, 70, false);
            this._addColumn(_g.d.ic_price_adjust_detail._barcode_price_4, 3, 10, 70, false);
            this._addColumn(_g.d.ic_price_adjust_detail._barcode_member_price_4, 3, 10, 70, false);

            // ราคาสูตร
            this._addColumn(_g.d.ic_price_adjust_detail._price_0, 3, 10, 50, false);
            this._addColumn(_g.d.ic_price_adjust_detail._price_1, 3, 10, 50, false);
            this._addColumn(_g.d.ic_price_adjust_detail._price_2, 3, 10, 50, false);
            this._addColumn(_g.d.ic_price_adjust_detail._price_3, 3, 10, 50, false);
            this._addColumn(_g.d.ic_price_adjust_detail._price_4, 3, 10, 50, false);
            this._addColumn(_g.d.ic_price_adjust_detail._price_5, 3, 10, 50, false);
            this._addColumn(_g.d.ic_price_adjust_detail._price_6, 3, 10, 50, false);
            this._addColumn(_g.d.ic_price_adjust_detail._price_7, 3, 10, 50, false);
            this._addColumn(_g.d.ic_price_adjust_detail._price_8, 3, 10, 50, false);
            this._addColumn(_g.d.ic_price_adjust_detail._price_9, 3, 10, 50, false);

            // ราคาใหม่
            this._addColumn(_g.d.ic_price_adjust_detail._new_price_exclude_vat, 3, 10, 80, false);
            this._addColumn(_g.d.ic_price_adjust_detail._new_price_include_vat, 3, 10, 80, false);

            // ราคา barcode
            this._addColumn(_g.d.ic_price_adjust_detail._new_barcode_price_1, 3, 10, 70, false);
            this._addColumn(_g.d.ic_price_adjust_detail._new_barcode_member_price_1, 3, 10, 70, false);
            this._addColumn(_g.d.ic_price_adjust_detail._new_barcode_price_2, 3, 10, 70, false);
            this._addColumn(_g.d.ic_price_adjust_detail._new_barcode_member_price_2, 3, 10, 70, false);
            this._addColumn(_g.d.ic_price_adjust_detail._new_barcode_price_3, 3, 10, 70, false);
            this._addColumn(_g.d.ic_price_adjust_detail._new_barcode_member_price_3, 3, 10, 70, false);
            this._addColumn(_g.d.ic_price_adjust_detail._new_barcode_price_4, 3, 10, 70, false);
            this._addColumn(_g.d.ic_price_adjust_detail._new_barcode_member_price_4, 3, 10, 70, false);

            // ราคาสูตร
            this._addColumn(_g.d.ic_price_adjust_detail._new_price_0, 3, 10, 60, false);
            this._addColumn(_g.d.ic_price_adjust_detail._new_price_1, 3, 10, 60, false);
            this._addColumn(_g.d.ic_price_adjust_detail._new_price_2, 3, 10, 60, false);
            this._addColumn(_g.d.ic_price_adjust_detail._new_price_3, 3, 10, 60, false);
            this._addColumn(_g.d.ic_price_adjust_detail._new_price_4, 3, 10, 60, false);
            this._addColumn(_g.d.ic_price_adjust_detail._new_price_5, 3, 10, 60, false);
            this._addColumn(_g.d.ic_price_adjust_detail._new_price_6, 3, 10, 60, false);
            this._addColumn(_g.d.ic_price_adjust_detail._new_price_7, 3, 10, 60, false);
            this._addColumn(_g.d.ic_price_adjust_detail._new_price_8, 3, 10, 60, false);
            this._addColumn(_g.d.ic_price_adjust_detail._new_price_9, 3, 10, 60, false);

            this._addColumnTop(_g.d.ic_price_adjust_detail._price_standard, this._findColumnByName(_g.d.ic_price_adjust_detail._price_exclude_vat), this._findColumnByName(_g.d.ic_price_adjust_detail._price_include_vat));

            this._addColumnTop(_g.d.ic_price_adjust_detail._price_group_1, this._findColumnByName(_g.d.ic_price_adjust_detail._barcode_price_1), this._findColumnByName(_g.d.ic_price_adjust_detail._barcode_member_price_1));
            this._addColumnTop(_g.d.ic_price_adjust_detail._price_group_2, this._findColumnByName(_g.d.ic_price_adjust_detail._barcode_price_2), this._findColumnByName(_g.d.ic_price_adjust_detail._barcode_member_price_2));
            this._addColumnTop(_g.d.ic_price_adjust_detail._price_group_3, this._findColumnByName(_g.d.ic_price_adjust_detail._barcode_price_3), this._findColumnByName(_g.d.ic_price_adjust_detail._barcode_member_price_3));
            this._addColumnTop(_g.d.ic_price_adjust_detail._price_group_4, this._findColumnByName(_g.d.ic_price_adjust_detail._barcode_price_4), this._findColumnByName(_g.d.ic_price_adjust_detail._barcode_member_price_4));

            this._addColumnTop(_g.d.ic_price_adjust_detail._price_formula, this._findColumnByName(_g.d.ic_price_adjust_detail._price_0), this._findColumnByName(_g.d.ic_price_adjust_detail._price_9));

            this._addColumnTop(_g.d.ic_price_adjust_detail._new_price, this._findColumnByName(_g.d.ic_price_adjust_detail._new_price_exclude_vat), this._findColumnByName(_g.d.ic_price_adjust_detail._new_price_9));

            //this._calcPersentWidthToScatter();


        }
    }
}
