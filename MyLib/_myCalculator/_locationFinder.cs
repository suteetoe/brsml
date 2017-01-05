using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace MyLib
{
    public static class _locationFinder
    {
        public static Point _GetScreenLocation(Control ctl)
        {
            if (null == ctl)
                return new Point(0, 0);

            int __x = ctl.Location.X;
            int __y = ctl.Location.Y;
            Control __parent = ctl.Parent;
            while (null != __parent.Parent)
            {
                __x += __parent.Location.X;
                __y += __parent.Location.Y;
                __parent = __parent.Parent;
                if (null != __parent && __parent is Form)
                    break;
            }
            Form __f = ctl.FindForm();
            Point __pt = __f.PointToScreen(new Point(__x, __y));

            return __pt;
        }
    }
}
