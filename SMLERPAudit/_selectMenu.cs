using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SMLERPAudit
{
    public static class _selectMenu
    {
        public static Control _getObject(string menuName, string screenName)
        {
            switch (menuName.ToLower())
            {
                case "menu_audit_login": return (new _loginLog());
                case "menu_audit_menu": return (new _menuLog());
                case "menu_audit_trans": return (new _transLog());

                case "menu_audit_master": return (new _masterLog());
            }
            return null;
        }
    }
}
