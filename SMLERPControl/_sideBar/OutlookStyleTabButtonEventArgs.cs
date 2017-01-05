/*
 * Proje	    : Outlook 2003 Style SideBar v 1.1
 *
 * Hazırlayan   : Muhammed ŞAHİN
 * eMail        : muhammed.sahin@gmail.com
 *
 * Açıklama	    : Outlook 2003 Style Sidebar
 *              : TabButtonlar için event argüment
 * 
 */

using System;
using System.Collections.Generic;
using System.Text;

namespace SMLERPControl
{
    #region Class : TabButtonEventArgs

    /// <summary>
    /// Tab seçildiğindeki argumanlar
    /// </summary>
    public sealed class OutlookStyleTabButtonEventArgs : EventArgs
    {
        /// <summary>
        /// Seçili olan tabı tutar
        /// </summary>
        public readonly OutlookStyleTabButton TabButton;

        public OutlookStyleTabButtonEventArgs(OutlookStyleTabButton tTabButton)
        {
            if (tTabButton == null)
                throw new NullReferenceException("TabButton null olamaz");

            TabButton = tTabButton;
        }

    }
    #endregion
}
