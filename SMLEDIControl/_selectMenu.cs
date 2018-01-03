using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace SMLEDIControl
{
    public class _selectMenu
    {
        public static Control _getObject(string menuName, string screenName)
        {
            switch (menuName.ToLower())
            {
                case "menu_edi_external": return (new _ediExternal());
                case "menu_edi_export": return (new _ediExport());
                case "menu_sos_order_interface": return (new BRInterfaceControl.SOS._singhaOnlineOrderImport());
                case "menu_edi_flat_file": return (new _ediFlatFile());
                case "menu_sync_data_arm": return (new BRInterfaceControl.ARM._sendDataARM());
            }

            return null;
        }
    }
}
