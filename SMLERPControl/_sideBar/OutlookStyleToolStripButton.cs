/*
 * Proje	    : Outlook 2003 Style SideBar v 1.1
 *
 * Hazırlayan   : Muhammed ŞAHİN
 * eMail        : muhammed.sahin@gmail.com
 *
 * Açıklama	    : Outlook 2003 Style Sidebar
 *              : Taşan TabButtonların panel üzerindeki her bir simgesi.
 *              : Aktif ve Selected bilgisine bakarak renklendirme yapılıyor
 * 
 */

using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Windows.Forms;

namespace SMLERPControl
{
    #region Class : OutlookStyleToolStripButton
    /// <summary>
    /// Toolstripte butana basıldığında kontrol edilecek
    /// </summary>
    [Browsable(false)]
    [ToolboxItem(false)]
    sealed class OutlookStyleToolStripButton : ToolStripButton
    {
        private bool active = false;
        /// <summary>
        /// Aktif durumuna göre render işleminde farklılık yapılıyor
        /// </summary>
        public bool Active
        {
            get { return active; }
            set { active = value; }
        }

    }
    #endregion

}
