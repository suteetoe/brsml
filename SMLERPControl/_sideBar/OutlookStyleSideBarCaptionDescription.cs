/*
 * Proje	    : Outlook 2003 Style SideBar v 1.1
 *
 * Hazırlayan   : Muhammed ŞAHİN
 * eMail        : muhammed.sahin@gmail.com
 *
 * Açıklama	    : Outlook 2003 Style Sidebar
 *              : Taşıyıcı panel için başlık bilgisi
 * 
 */

using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace SMLERPControl
{
    #region Class : TSSideBarCaptionDescription

    [Browsable(false)]
    [ToolboxItem(false)]
    sealed class OutlookStyleSideBarCaptionDescription : ToolStrip
    {

        #region Caption
        private string caption = "Caption";
        public string Caption
        {
            get { return caption; }
            set
            {
                caption = value;
                tsLabel.Text = caption;
                Invalidate();
            }
        }
        #endregion

        #region ThemeColor
        OutlookStyleThemeColor themeColor;
        internal OutlookStyleThemeColor ThemeColor
        {
            set
            {
                themeColor = value;
                Renderer = new OutlookStyleRenderer(themeColor.LightColor, themeColor.DarkColor, true);
                tsLabel.ForeColor = themeColor.TabButtonForeColor;
                Invalidate();
            }
        }
        #endregion

        ToolStripLabel tsLabel = new ToolStripLabel();

        public OutlookStyleSideBarCaptionDescription()
        {

            // Control

            AutoSize = false;
            Stretch = true;
            Height = 20;
            Renderer = new OutlookStyleRenderer(SystemColors.ControlLightLight, SystemColors.Control, false);
            Dock = DockStyle.Top;

            tsLabel.Text = this.Caption;

            Items.Add(tsLabel);
        }

    }

    #endregion
}
