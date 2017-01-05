using System;
using System.Collections.Generic;
using System.Text;

namespace SMLInventoryControl
{
    public class _tools
    {
        /// <summary>
        /// คำนวณวันที่อัตโนมัติ
        /// </summary>
        /// <param name="source">dataGrid</param>
        /// <param name="mode">0=ราคาซื้อ,2=ราคาขาย</param>
        public void _priceDetailAutoDate(MyLib._myGrid source, _g.g._priceListType mode)
        {
            string __fieldDateBegin = "";
            string __fieldDateEnd = "";
            switch (mode)
            {
                case _g.g._priceListType.ซื้อ_ราคาซื้อทั่วไป:
                case _g.g._priceListType.ซื้อ_ราคาซื้อตามเจ้าหนี้:
                    __fieldDateBegin = _g.d.ic_inventory_purchase_price._from_date;
                    __fieldDateEnd = _g.d.ic_inventory_purchase_price._to_date;
                    break;
                case _g.g._priceListType.ขาย_ราคาขายตามกลุ่มลูกค้า:
                case _g.g._priceListType.ขาย_ราคาขายตามลูกค้า:
                case _g.g._priceListType.ขาย_ราคาขายทั่วไป:
                    __fieldDateBegin = _g.d.ic_inventory_price._from_date;
                    __fieldDateEnd = _g.d.ic_inventory_price._to_date;
                    break;
            }
            // หาวันที่จากล่างขึ้นบน
            string __beginDate = "";
            int __rowAddr = -1;
            for (int __row = source._rowData.Count - 1; __row > 0; __row--)
            {
                string __getDate = source._cellGet(__row, __fieldDateBegin).ToString();
                if (__getDate.Length > 0)
                {
                    __beginDate = __getDate;
                    __rowAddr = __row;
                    break;
                }
            }
            //
            if (__rowAddr != -1)
            {
                for (int __row = __rowAddr - 1; __row >= 0; __row--)
                {
                    DateTime __getDate = MyLib._myGlobal._convertDate(__beginDate).AddDays(-1);
                    source._cellUpdate(__row, __fieldDateEnd, __getDate, false);
                    __beginDate = source._cellGet(__row, __fieldDateBegin).ToString();
                }
            }
        }
    }
}
