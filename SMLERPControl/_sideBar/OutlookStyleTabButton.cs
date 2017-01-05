/*
 * Proje	    : Outlook 2003 Style SideBar v 1.1
 *
 * Hazırlayan   : Muhammed ŞAHİN
 * eMail        : muhammed.sahin@gmail.com
 *
 * Açıklama	    : Outlook 2003 Style Sidebar
 *              : Panel içerisine eklenen TabButtonlar
 * 
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace SMLERPControl
{
    #region  Class : OutlookStyleTabButton

    /// <summary>
    /// SidebarTabButton
    /// </summary>
    [Browsable(false)]
    [ToolboxItem(false)]
    public sealed class OutlookStyleTabButton : ToolStrip
    {
        // Delegate

        #region Delegate & Events

        public delegate void OnTabSelectedEventHandler(object sender, OutlookStyleTabButtonEventArgs e);
        public event OnTabSelectedEventHandler OnTabSelected;

        public delegate void OnTabMouseOverEventHandler(OutlookStyleTabButtonEventArgs e);
        public event OnTabMouseOverEventHandler OnTabMouseOver;

        public delegate void OnTabMouseLeaveEventHandler(OutlookStyleTabButtonEventArgs e);
        public event OnTabMouseLeaveEventHandler OnTabMouseLeave;

        public delegate void OnTabCaptionChangedEventHandler(string tOldCaption, string tNewCaption);
        public event OnTabCaptionChangedEventHandler OnTabCaptionChanged;

        public delegate void OnTabCaptionDescriptionChangedEventHandler(string tOldCaptionDesc, string tNewCaptionDesc);
        public event OnTabCaptionDescriptionChangedEventHandler OnTabCaptionDescriptionChanged;

        public delegate void OnTabFontChangeEventHandler(Font tOldFont, Font tNewFont);
        public event OnTabFontChangeEventHandler OnTabFontChanged;

        public delegate void OnTabThemeColorChangeEventHandler(OutlookStyleThemeColor tOldThemeColor, OutlookStyleThemeColor tNewThemeColor);
        public event OnTabThemeColorChangeEventHandler OnTabThemeColorChanged;

        #endregion

        // Properties

        #region ThemeColor
        OutlookStyleThemeColor themeColor = null; //OutlookStyleThemeColor.GetThemeColor(ThemeColors.SystemDefault);
        public OutlookStyleThemeColor ThemeColor
        {
            get { return themeColor; }
            set
            {
                OutlookStyleThemeColor oldThemeColor = themeColor;
                themeColor = value;
                tsLabel.ForeColor = themeColor.TabButtonForeColor;

                // Renderer

                tsrNormal = new OutlookStyleRenderer(themeColor.LightColor, themeColor.DarkColor, true);
                tsrOver = new OutlookStyleRenderer(themeColor.OverLightColor, themeColor.SelectedDarkColor, true);
                tsrSelected = new OutlookStyleRenderer(themeColor.SelectedLightColor, themeColor.SelectedDarkColor, true);

                if (Selected)
                    Renderer = tsrSelected;
                else
                    Renderer = tsrNormal;

                Invalidate();

                if (OnTabThemeColorChanged != null)
                    OnTabThemeColorChanged(oldThemeColor, value);

            }
        }
        #endregion

        #region Image
        private Image image = null;
        /// <summary>
        /// Tabbutton image (24x24 kullanın)
        /// </summary>
        public Image Image
        {
            get { return image; }
            set
            {
                image = value;
                tsLabel.Image = image;
                Invalidate();
            }
        }
        #endregion

        #region HoverImage
        private Image hoverImage = null;
        /// <summary>
        /// Tabbutton image (24x24 kullanın)
        /// </summary>
        public Image HoverImage
        {
            get { return hoverImage; }
            set { hoverImage = value; }
        }
        #endregion

        #region SelectedImage
        private Image selectedImage = null;
        /// <summary>
        /// Tabbutton image (24x24 kullanın)
        /// </summary>
        public Image SelectedImage
        {
            get { return selectedImage; }
            set { selectedImage = value; }
        }
        #endregion

        #region Selected
        private bool selected = false;
        /// <summary>
        /// Seçilimi
        /// </summary>
        public bool Selected
        {
            get { return selected; }
            set
            {
                selected = value;
                this.Renderer = selected ? tsrSelected : tsrNormal;
                this.tsLabel.Image = this.SelectedImage != null ? this.SelectedImage : this.Image;
                if (!selected)
                    this.tsLabel.Image = this.Image;

                Invalidate();

            }
        }

        #endregion

        #region Caption
        private string caption = "Caption";
        /// <summary>
        /// Tabbutton üzerinde gözükecek olan metin
        /// </summary>
        public string Caption
        {
            get { return caption; }
            set
            {
                string oldCaption = caption;
                caption = value;
                tsLabel.Text = caption;
                if (OnTabCaptionChanged != null)
                    OnTabCaptionChanged(oldCaption, caption);

            }
        }
        #endregion

        #region CaptionDescription
        private string captionDesc = "Caption Description";
        /// <summary>
        /// Caption band üzerinde gözükecek metin
        /// </summary>
        public string CaptionDescription
        {
            get { return captionDesc; }
            set
            {
                string oldCaptionDesc = caption;
                captionDesc = value;
                if (OnTabCaptionDescriptionChanged != null)
                    OnTabCaptionDescriptionChanged(oldCaptionDesc, captionDesc);
            }
        }
        #endregion

        #region Control
        private Control control = null;
        /// <summary>
        /// Tabbuton seçildiğinde aktif edilecek kontrol
        /// </summary>
        public Control Control
        {
            get { return control; }
        }
        #endregion

        #region TabButtonFont
        private Font tabButtonFont = new Font("Tahoma", 8, FontStyle.Bold);
        public Font TabButtonFont
        {
            get { return tabButtonFont; }
            set
            {
                Font oldFont = tabButtonFont;
                tabButtonFont = value;
                this.Font = tabButtonFont;
                this.tsLabel.Font = tabButtonFont;

                if (OnTabFontChanged != null)
                    OnTabFontChanged(oldFont, tabButtonFont);

            }
        }
        #endregion

        // Var

        ToolStripLabel tsLabel = new ToolStripLabel();

        private OutlookStyleRenderer tsrNormal;
        private OutlookStyleRenderer tsrOver;
        private OutlookStyleRenderer tsrSelected;

        #region Yapıcı metodlar
        /// <summary>
        /// Mutlaka tab seçildiğinde gösterilecek olan kontrol belirtilmeli
        /// </summary>
        /// <param name="tControl"></param>
        public OutlookStyleTabButton(Control tControl)
        {
            if (tControl == null)
                control = new Panel();
            else
                control = tControl; // Panel içerisinde gösterilecek nesne

            control.Dock = DockStyle.Fill;

            InitOutlookStyleTabButton();
        }

        /// <summary>
        /// İlk Ayarlamalar
        /// </summary>
        void InitOutlookStyleTabButton()
        {

            // Control

            AutoSize = false;
            Stretch = true;
            Dock = DockStyle.Bottom;
            Cursor = Cursors.Hand;
            MouseEnter += new EventHandler(OutlookStyleTabButton_MouseEnter);
            MouseLeave += new EventHandler(OutlookStyleTabButton_MouseLeave);
            Click += new EventHandler(OutlookStyleTabButton_Click);
            Font = tabButtonFont;

            // Renderer

            OutlookStyleThemeColor thmColor = OutlookStyleThemeColor.SystemColor;
            tsrNormal = new OutlookStyleRenderer(thmColor.LightColor, thmColor.DarkColor, true);
            tsrOver = new OutlookStyleRenderer(thmColor.OverLightColor, thmColor.OverDarkColor, true);
            tsrSelected = new OutlookStyleRenderer(thmColor.SelectedLightColor, thmColor.SelectedDarkColor, true);

            thmColor = null;

            this.Renderer = tsrNormal;

            // tsLabel

            tsLabel.Text = "Caption Description";
            tsLabel.ImageScaling = ToolStripItemImageScaling.None;
            tsLabel.TextImageRelation = TextImageRelation.ImageBeforeText;
            tsLabel.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
            tsLabel.Font = tabButtonFont;

            //
            this.Items.Add(tsLabel);
        }
        #endregion

        #region TabButton olayları
        /// <summary>
        /// Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OutlookStyleTabButton_Click(object sender, EventArgs e)
        {
            selected = !selected;

            if (OnTabSelected != null)
                OnTabSelected(this, new OutlookStyleTabButtonEventArgs(this));
        }

        /// <summary>
        /// Fare ile üstüne geldiginde normal render ayarlanacak
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OutlookStyleTabButton_MouseLeave(object sender, EventArgs e)
        {
            if (!selected)
            {
                this.Renderer = tsrNormal;
                this.tsLabel.Image = this.Image;
            }

            if (OnTabMouseLeave != null)
                OnTabMouseLeave(new OutlookStyleTabButtonEventArgs(this));
        }

        /// <summary>
        /// Fare üstünden ayrıldığında normal görünüme getirilecek
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OutlookStyleTabButton_MouseEnter(object sender, EventArgs e)
        {
            if (!selected)
            {
                this.Renderer = tsrOver;
                this.tsLabel.Image = this.HoverImage != null ? this.HoverImage : this.Image;
            }
            else
            {
                this.Renderer = tsrSelected;
                this.tsLabel.Image = this.SelectedImage != null ? this.SelectedImage : this.Image;
            }

            if (OnTabMouseOver != null)
                OnTabMouseOver(new OutlookStyleTabButtonEventArgs(this));
        }
        #endregion
    }

    #endregion
}