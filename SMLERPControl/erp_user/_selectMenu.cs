using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SMLERPControl.erp_user
{
    public class _selectMenu
    {
        public static Control _getObject(string menuName)
        {
            switch (menuName.ToLower())
            {
                // กำหนดรูปภาพพนักงาน
                case "menu_setup_staff_pic": return (new erp_user._erp_userDetailpicture());
               
            }
            return null;
        }
    }
}
