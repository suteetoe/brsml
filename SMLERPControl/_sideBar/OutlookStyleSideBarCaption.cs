/*
 * Proje	    : Outlook 2003 Style SideBar v 1.1
 *
 * Hazırlayan   : Muhammed ŞAHİN
 * eMail        : muhammed.sahin@gmail.com
 *
 * Açıklama	    : Outlook 2003 Style Sidebar
 *              : Taşıyıcı panel için başlık
 * 
 */

using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace SMLERPControl
{
    #region Class : OutlookStyleSideBarCaption

    [Browsable(false)]
    [ToolboxItem(false)]
    sealed class OutlookStyleSideBarCaption : ToolStrip
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
                Renderer = new OutlookStyleRenderer(themeColor.DarkColor, themeColor.DarkDarkColor, true);
                tsLabel.ForeColor = themeColor.LightColor;
                Invalidate();
            }
        }
        #endregion

        ToolStripLabel tsLabel = new ToolStripLabel();

        public OutlookStyleSideBarCaption()
        {

            // Control

            AutoSize = false;
            Stretch = true;
            Height = 24;
            Renderer = new OutlookStyleRenderer(SystemColors.ControlDark, SystemColors.ControlDark, false);
            Dock = DockStyle.Top;

            tsLabel.Text = this.Caption;
            tsLabel.Font = new Font(SystemFonts.DialogFont.Name, 12, FontStyle.Bold);
            tsLabel.ForeColor = SystemColors.ControlLightLight;

            Items.Add(tsLabel);
        }
    }

    #endregion
}
