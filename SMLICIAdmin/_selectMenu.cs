using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SMLICIAdmin
{
	public class _selectMenu
	{
        public static Control _getObject(string menuName, string screenName)
        {            
            switch (menuName.ToLower())
            {
                case "menu_import_data_fomulcolor": return (new _importFcolorDataControl()); //กำหนดสมุดเงินฝากธนาคาร                    
                case "menu_import_data_fomulcolorscreenfile": return (new _importFcolorDataControlScreenfile()); //กำหนดสมุดเงินฝากธนาคาร
                case "menu_permissions_user": return (new MyLib._databaseManage._menupermissions_user());
                case "menu_permissions_group": return (new MyLib._databaseManage._menupermissions_group());
                case "menu_view_manager": return (new MyLib._databaseManage._viewManage(true));
                case "menu_database_struct": return (new MyLib._databaseManage._databaseStruct());
                case "menu_import_data_master": return (new MyLib._databaseManage._importDataControl());
                case "menu_query": return (new MyLib._databaseManage._queryDataView());
                case "menu_verify_database": MyLib._myUtil._startDialog(MyLib._myGlobal._mainForm, screenName, new MyLib._databaseManage._verifyDatabase()); break;
                case "menu_shink_database": MyLib._myUtil._startDialog(MyLib._myGlobal._mainForm, screenName, new MyLib._databaseManage._shrinkDatabase()); break;
                case "menu_change_password": MyLib._myUtil._startDialog(MyLib._myGlobal._mainForm, screenName, new MyLib._databaseManage._smlChangePassword("xxxxxxxx")); break;      
            }
            return null;
        }
	}
}
