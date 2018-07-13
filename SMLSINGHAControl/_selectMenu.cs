using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace SMLSINGHAControl
{
    public class _selectMenu
    {
        public static Control _getObject(string menuName, string screenName)
        {
            switch (menuName)
            {
                case "menu_import_data_master": return (new MyLib._databaseManage._importDataControl());
               // case "menu_sync_data_center": return (new SMLSINGHAControl._singhaMasterTransfer());
            }
            return null;
        }
    }
}
