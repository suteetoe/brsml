using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SMLERPControl
{
   public  class _selectMenu
    {
       public static Control _getObject(string menuName)
       {
           switch (menuName.ToLower())
           {
               case "menu_setup_control_company": 
                    MyLib._myUtil._startDialog(MyLib._myGlobal._mainForm, menuName, new SMLERPControl._companyProfilePicturecs()); break;               
           }
           return null;
       }
    }
}
