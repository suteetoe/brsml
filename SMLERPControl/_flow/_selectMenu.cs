using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SMLERPControl._flow
{
    public class _selectMenu
    {
        public static Control _getObject(string menuName)
        {
            switch (menuName.ToLower())
            {
                // กำหนดค่าเริ่มต้น-รายละเอียดทั่วไป
                case "menu_flow_budget": return (new _flowViewer(new _flowBudget()));
                case "menu_flow_purchase_plan": return (new _flowViewer(new _flowPurchasePlan()));
                case "menu_flow_purchase_request": return (new _flowViewer(new _flowPurchaseRequest()));
                case "menu_flow_purchase": return (new _flowViewer(new _flowPOPanel()));
                case "menu_flow_sale": return (new _flowViewer(new _flowSOPanel()));
                case "menu_flow_ar": return (new _flowViewer(new _flowARPanel()));
                case "menu_flow_ap": return (new _flowViewer(new _flowAPPanel()));
                case "menu_flow_cash": return (new _flowViewer(new _flowCashPanel()));
                case "menu_flow_inventory": return (new _flowViewer(new _flowInventoryPanel()));
                case "menu_flow_warehouse": return (new _flowViewer(new _flowWarehousePanel()));
                case "menu_flow_asset": return (new _flowViewer(new _flowAssetPanel()));
                case "menu_flow_account": return (new _flowViewer(new _flowAccountPanel()));
                case "menu_flow_crm": return (new _flowViewer(new _flowCRMPanel()));
                case "menu_flow_transport": return (new _flowViewer(new _flowTransportPanel()));
            }
            return null;
        }
    }
}
