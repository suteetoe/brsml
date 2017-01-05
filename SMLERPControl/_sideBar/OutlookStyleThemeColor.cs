/*
 * Proje	    : Outlook 2003 Style SideBar v 1.1
 *
 * Hazırlayan   : Muhammed ŞAHİN
 * eMail        : muhammed.sahin@gmail.com
 *
 * Açıklama	    : Outlook 2003 Style Sidebar
 *              : OutlookSidebar ve TabButtonlar için renk düzenleyici
 * 
 */

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace SMLERPControl
{

    #region OutlookStyleThemeColor
    /// <summary>
    /// ThemeColor sınıfı
    /// Default olarak sistem kontrol renklerini kullanıyor
    /// </summary>
    public sealed class OutlookStyleThemeColor
    {

        #region ThemeColor : SystemColor
        /// <summary>
        /// Default ThemeColor
        /// SystemColors sınıfının Control renkleri kullanılarak
        /// </summary>
        public static OutlookStyleThemeColor SystemColor
        {
            get { return new OutlookStyleThemeColor(); }
        }
        #endregion

        #region ThemeColor : BlueColor
        /// <summary>
        /// Mavi renk görünümünde
        /// </summary>
        public static OutlookStyleThemeColor BlueColor
        {
            get
            {
                OutlookStyleThemeColor themeColor = new OutlookStyleThemeColor();
                themeColor.TabButtonForeColor = SystemColors.ControlText;

                themeColor.LightColor = Color.FromArgb(203, 225, 252);
                themeColor.DarkColor = Color.FromArgb(126, 166, 225);
                themeColor.DarkDarkColor = Color.FromArgb(0, 84, 227);

                themeColor.OverLightColor = Color.FromArgb(203, 225, 252);
                themeColor.OverDarkColor = Color.FromArgb(203, 225, 252);

                themeColor.SelectedLightColor = Color.FromArgb(203, 225, 252);
                themeColor.SelectedDarkColor = Color.FromArgb(203, 225, 252);

                return themeColor;
            }
        }
        #endregion

        #region ThemeColor : OliverColor
        /// <summary>
        /// Yeşil görünümü uygular
        /// </summary>
        public static OutlookStyleThemeColor OliverColor
        {
            get
            {
                OutlookStyleThemeColor themeColor = new OutlookStyleThemeColor();

                themeColor.TabButtonForeColor = SystemColors.ControlText;

                themeColor.LightColor = Color.FromArgb(234, 240, 207);
                themeColor.DarkColor = Color.FromArgb(178, 193, 140);
                themeColor.DarkDarkColor = Color.FromArgb(139, 161, 105);

                themeColor.OverLightColor = Color.FromArgb(234, 240, 207);
                themeColor.OverDarkColor = Color.FromArgb(234, 240, 207);

                themeColor.SelectedLightColor = Color.FromArgb(234, 240, 207);
                themeColor.SelectedDarkColor = Color.FromArgb(234, 240, 207);

                return themeColor;
            }
        }
        #endregion

        #region ThemeColor : Silver
        /// <summary>
        /// Gümüş görünümü uygular
        /// </summary>
        public static OutlookStyleThemeColor SilverColor
        {
            get
            {
                OutlookStyleThemeColor themeColor = new OutlookStyleThemeColor();

                themeColor.TabButtonForeColor = SystemColors.ControlText;

                themeColor.LightColor = Color.FromArgb(225, 226, 236);
                themeColor.DarkColor = Color.FromArgb(150, 148, 178);
                themeColor.DarkDarkColor = Color.FromArgb(192, 192, 192);

                themeColor.OverLightColor = Color.FromArgb(225, 226, 236);
                themeColor.OverDarkColor = Color.FromArgb(225, 226, 236);

                themeColor.SelectedLightColor = Color.FromArgb(225, 226, 236);
                themeColor.SelectedDarkColor = Color.FromArgb(225, 226, 236);

                return themeColor;
            }
        }
        #endregion

        #region Properties
        public Color TabButtonForeColor;

        public Color DarkColor;
        public Color LightColor;
        public Color DarkDarkColor;

        public Color OverDarkColor;
        public Color OverLightColor;

        public Color SelectedDarkColor;
        public Color SelectedLightColor;
        #endregion

        #region Yapıcı Metod : System renkleri alınıyor
        public OutlookStyleThemeColor()
        {
            TabButtonForeColor = SystemColors.ControlText;

            LightColor = SystemColors.ControlLightLight;
            DarkColor = SystemColors.Control;
            DarkDarkColor = SystemColors.ControlDark;

            OverLightColor = SystemColors.ControlLightLight;
            OverDarkColor = SystemColors.ControlLightLight;

            SelectedLightColor = SystemColors.ControlLightLight;
            SelectedDarkColor = SystemColors.ControlLightLight;
        }
        #endregion

        /// <summary>
        /// LightColor ve DarkColor bilgisini geri gönderir
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "LightColor : " + LightColor.ToString() + " DarkColor : " + DarkColor.ToString();
        }
    }
    #endregion

}
