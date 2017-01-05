using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace SMLPPShipment
{
    public class _selectMenu
    {

        public static Control _getObject(string menuName, string screenName)
        {
            switch (menuName)
            {
                case "menu_shipment_load": return new _shipment();
            }
            return null;
        }
    }
}
