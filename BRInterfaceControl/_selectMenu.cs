using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace BRInterfaceControl
{
    public class _selectMenu
    {
        public static Control _getObject(string menuName, string screenName)
        {
            switch (menuName)
            {
                case "menu_sos_order_interface":
                    return (new SOS._singhaOnlineOrderImport());
            }
            return null;
        }
    }
}
