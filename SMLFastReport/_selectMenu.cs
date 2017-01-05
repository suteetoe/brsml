using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace SMLFastReport
{
    public class _selectMenu
    {
        public static Control _getObject(string menuName, string screenName)
        {
            Control __control = null;

            int __type = 0;
            switch (menuName.ToLower())
            {
                case "menu_fastreport_other_ic":
                    __type = 1;
                    break;
                case "menu_fastreport_other_purchase":
                    __type = 2;
                    break;
                case "menu_fastreport_other_sale":
                case "menu_fastreport_other_sale_pos" :
                    __type = 3;
                    break;
                case "menu_fastreport_other_supplier":
                    __type = 4;
                    break;
                case "menu_fastreport_other_customer":
                    __type = 5;
                    break;
                case "menu_fastreport_other_cashbank":
                    __type = 6;
                    break;
                case "menu_fastreport_other_asset":
                    __type = 7;
                    break;
                case "menu_fastreport_other_gl" :
                    __type = 8;
                    break;
            }

            _loadForm __load = new _loadForm();
            __load._myDataList1._extraWhere = _g.d.sml_fastreport._report_type + "=" + __type.ToString();
            __load._ReportSelected += (s1, e1) =>
            {
                //this._menuId = e1.menuId;
                //this._menuName = e1.menuName;
                //this._menu_type = e1.menuType;
                ////_allowUserSave = true;
                //this._loadData();
                if (e1.menuId != null && e1.menuId.Length > 0)
                {
                    SMLFastReport._designer __fastReport = new SMLFastReport._designer();
                    __fastReport._load(e1.menuId, e1.menuName);

                    __control = __fastReport;
                }
            };

            __load.ShowDialog();

            return __control;
        }
    }
}
