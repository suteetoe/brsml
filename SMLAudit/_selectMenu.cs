using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SMLAudit
{
    public class _selectMenu
    {
        public static Control _getObject(string menuName)
        {
            switch (menuName.ToLower())
            {
                /*case "menu_ic_info_stk_movement": return (new _stkMovement()); // บัญชีคุมพิเศษสินค้า
                case "menu_ic_info_stk_profit": return (new _stkProfit()); // กำไรขั้นต้นตามสินค้า
                case "menu_ic_info_stk_reorder": return (new _stkReorder(0)); // สินค้าถึงจุดสั่งซื้อ*/
            }
            return null;
        }
    }
}
