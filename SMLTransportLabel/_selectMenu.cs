using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace SMLTransportLabel
{
    public class _selectMenu
    {
        public static Control _getObject(string menuName)
        {
            switch (menuName.ToLower())
            {
                case "menu_ar_transport_label": return new _transport_label(1);
                case "menu_ap_transport_label": return new _transport_label(0);
                case "menu_ap_transport_label_print": return new _transport_label_print(0);
                case "menu_ar_transport_label_print": return new _transport_label_print(1);
                case "menu_import_ap_transport_label":
                    {
                        _importTransportLabel __import = new _importTransportLabel(0);
                        __import.ShowDialog();
                    }
                    break;
                case "menu_import_ar_transport_label":
                    {
                        _importTransportLabel __import = new _importTransportLabel(1);
                        __import.ShowDialog();
                    }
                    break;
                //case "menu_ic_shipping": return new SMLInventoryControl._clone_icTrans(_g.g._transControlTypeEnum.สินค้า_บันทึกการจัดส่ง, menuName);
            }

            return null;
        }
    }
}
