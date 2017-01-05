using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SMLERPICInfo
{
    public class _selectMenu
    {
        public static Control _getObject(string menuName)
        {
            switch (menuName.ToLower())
            {
                case "menu_ic_info_stk_balance": return (new _stkBalance( _g.g._productCostType.ปรกติ)); // ยอดคงเหลือสินค้า
                case "menu_ic_info_stk_balance_warehouse": return (new _stkBalanceWareHouse()); // ยอดคงเหลือสินค้าตามคลัง
                case "menu_ic_info_stk_balance_shelf": return (new _stkBalanceLocation()); // ยอดคงเหลือสินค้าตามที่เก็บ
                case "menu_ic_info_stk_balance_lot": return (new _stkBalanceByLot()); // ยอดคงเหลือสินค้า ตาม LOT
                case "menu_ic_info_stk_move": return (new _stkMovement(_g.g._productCostType.เคลื่อนไหวสินค้า)); // บัญชีคุมพิเศษสินค้า
                case "menu_ic_info_stk_movement": return (new _stkMovement(_g.g._productCostType.ปรกติ)); // บัญชีคุมพิเศษสินค้า
                case "menu_ic_info_stk_movement_hidden": return (new _stkMovement( _g.g._productCostType.รวมต้นทุนแฝง)); // บัญชีคุมพิเศษสินค้า (รวมต้นทุนแฝง)
                case "menu_ic_info_color_profit": return (new _stkProfitByColor(_infoStkProfitEnum.กำไรขั้นต้น_สินค้า, false, "", "", "", "", "")); // กำไรขั้นต้นสูตรสี
                case "menu_ic_info_color_profit_by_doc": return (new _stkProfitByColor(_infoStkProfitEnum.กำไรขั้นต้น_เอกสาร, false, "", "", "", "", "")); // กำไรขั้นต้นสูตรสีตามเอกสาร
                case "menu_ic_info_color_profit_by_ar": return (new _stkProfitByColor(_infoStkProfitEnum.กำไรขั้นต้น_ลูกค้า, false, "", "", "", "", "")); // กำไรขั้นต้นสูตรสีตามลูกค้า
                case "menu_ic_info_stk_profit": return (new _stkProfit(_infoStkProfitEnum.กำไรขั้นต้น_สินค้า, _g.g._productCostType.ปรกติ, false, "", "", "", "", "")); // กำไรขั้นต้นตามสินค้า
                case "menu_ic_info_stk_profit_hidden": return (new _stkProfit(_infoStkProfitEnum.กำไรขั้นต้น_สินค้า, _g.g._productCostType.รวมต้นทุนแฝง, false, "", "", "", "", "")); // กำไรขั้นต้นตามสินค้า
                case "menu_ic_info_stk_profit_by_doc": return (new _stkProfit(_infoStkProfitEnum.กำไรขั้นต้น_เอกสาร, _g.g._productCostType.ปรกติ, false, "", "", "", "", "")); // กำไรขั้นต้นตามเอกสารขาย
                case "menu_ic_info_stk_profit_by_ar": return (new _stkProfit(_infoStkProfitEnum.กำไรขั้นต้น_ลูกค้า, _g.g._productCostType.ปรกติ, false, "", "", "", "", "")); // กำไรขั้นต้นตามลูกค้า
                case "menu_ic_info_stk_reorder": return (new SMLInventoryControl._stkReorder(0)); // สินค้าถึงจุดสั่งซื้อ
                case "menu_ic_info_purchase_history": return (new SMLInventoryControl._transHistory(SMLInventoryControl._transHistoryType.ประวัติการซื้อ, true)); // ประวัติการซื้อสินค้า
                case "menu_ic_info_sale_history": return (new SMLInventoryControl._transHistory(SMLInventoryControl._transHistoryType.ประวัติการขาย, true)); // ประวัติการขายสินค้า
                case "menu_ic_info_product_no_movement": return (new _stkNoMovement()); // สินค้าไม่เคลื่อนไหว
                case "menu_ic_info_stk_movement_sum": return (new _stkMovementSum(0)); // สรุปการเคลื่อนไหวสินค้าตามปริมาณ
                case "menu_ic_info_stk_movement_sum_by_amount": return (new _stkMovementSum(1)); // สรุปการเคลื่อนไหวสินค้าตามมูลค่า
                case "menu_checking_ic_trans" : return new _icTransectionCheck(); // ตรวจสอบรายการรายวัน

                case "menu_ic_information" : return new _ic_inventory_information();
                case "menu_ic_information_manager" : return new _ic_inventory_information_2();
            }
            return null;
        }
    }
}
