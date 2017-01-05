/*
 * Proje	    : Outlook 2003 Style SideBar v 1.1
 *
 * Hazırlayan   : Muhammed ŞAHİN
 * eMail        : muhammed.sahin@gmail.com
 *
 * Açıklama	    : Outlook 2003 Style Sidebar
 *              : Taşan TabButtonları Taşıyıcı Panel
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

    #region Class : OutlookStyleOverflowItems

    /// <summary>
    /// Allta taşan simgeleri taşır
    /// </summary>
    [Browsable(false)]
    [ToolboxItem(false)]
    sealed class OutlookStyleOverflowItems : ToolStrip
    {

        public delegate void MenuItemMoveEventHandler(object sender, MoveType mt);
        public event MenuItemMoveEventHandler OnMenuItemMove;

        #region ThemeColor
        OutlookStyleThemeColor themeColor;
        internal OutlookStyleThemeColor ThemeColor
        {
            set
            {
                themeColor = value;
                Renderer = new OutlookStyleRenderer(themeColor.LightColor, themeColor.DarkColor, true);
                Invalidate();
            }
        }
        #endregion

        ToolStripDropDownButton tsSideBarButton = new ToolStripDropDownButton();
        ToolStripMenuItem tsDugmeMenu = new ToolStripMenuItem();
        ToolStripMenuItem tsDugmeGoster = new ToolStripMenuItem();
        ToolStripMenuItem tsDugmeGizle = new ToolStripMenuItem();

        OutlookStyleRenderer render = new OutlookStyleRenderer(SystemColors.ControlLightLight, SystemColors .Control, true);
        OutlookStyleTabButtonCollection collection = null;

        public OutlookStyleOverflowItems(OutlookStyleTabButtonCollection tCollection)
        {

            collection = tCollection;

            // Control
            Stretch = true;
            AutoSize = false;
            GripStyle = ToolStripGripStyle.Hidden;
            Dock = DockStyle.Fill;
            Renderer = render;
            this.Resize += new EventHandler(OutlookStyleOverflowItems_Resize);
            this.Click += new EventHandler(OutlookStyleOverflowItems_Click);

            // Düğmeler
            tsSideBarButton = new ToolStripDropDownButton();
            tsSideBarButton.Alignment = ToolStripItemAlignment.Right;
            tsSideBarButton.DropDownDirection = ToolStripDropDownDirection.Right;
            tsSideBarButton.ShowDropDownArrow = false;
            tsSideBarButton.Image = Properties.Resources.Ok;

            tsDugmeGoster.Text = "Düğme Göster";
            tsDugmeGizle.Text = "Düğme Gizle";
            tsDugmeMenu.Text = "Dügmeler";

            // Eğer Türkçe değilse makine
            if (!System.Globalization.CultureInfo.CurrentCulture.Name.Equals("tr-TR"))
            {
                tsDugmeGoster.Text = "Show TabButton";
                tsDugmeGizle.Text = "Hide TabButton";
                tsDugmeMenu.Text = "Tabs";
            }

            tsDugmeGoster.Image = Properties.Resources.YukariOk;
            tsDugmeGizle.Image = Properties.Resources.AsagiOk;

            tsDugmeGoster.Click += new EventHandler(tsDugmeGoster_Click);
            tsDugmeGizle.Click += new EventHandler(tsDugmeGizle_Click);

        }

        /// <summary>
        /// Tab Button tıklandı tüm aktifler iptal
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OutlookStyleOverflowItems_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < Items.Count; i++)
                if (Items[i] is OutlookStyleToolStripButton)
                    (Items[i] as OutlookStyleToolStripButton).Active = false;
        }

        /// <summary>
        /// Resize edildiğinde
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OutlookStyleOverflowItems_Resize(object sender, EventArgs e)
        {
            Invalidate(true);
        }

        /// <summary>
        /// Menuden click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void overMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem tsmi = (sender as ToolStripMenuItem);
            OutlookStyleToolStripButton tbtn = (OutlookStyleToolStripButton)tsmi.Tag;
            OutlookStyleTabButton tabbtn = (OutlookStyleTabButton)tbtn.Tag;

            tbtn.Active = true;
            tbtn.PerformClick(); // Burada menüden seçim yapılsa da taşan buttona basılması sağlanıyor

        }

        /// <summary>
        /// Button click aktif = true yapılıyor bu şekilde fare üzerinden gitse bile
        /// buton basılı kalacak
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void overButton_Click(object sender, EventArgs e)
        {
            (sender as OutlookStyleToolStripButton).Active = true;
        }

        /// <summary>
        /// Taşan bölüme simgeleri ekle
        /// </summary>
        /// <param name="control"></param>
        public void ReDrawIcon()
        {
            ClearItems();

            //
            int itemCount = 0;
            for (int i = collection.Count - 1; i >= 0; i--)
                if (collection[i] is OutlookStyleTabButton && collection[i].Visible == false)
                {
                    AddButton(collection[i]);
                    itemCount++;
                }

            tsDugmeGoster.Enabled = (Items.Count > 1); // 1 olan Menuyü açan ok
            tsDugmeGizle.Enabled = (itemCount != collection.Count); // Kontrol sayısı görünen sayıya eşit değilse

        }

        /// <summary>
        /// Buttonu ekle
        /// </summary>
        /// <param name="tButton"></param>
        void AddButton(OutlookStyleTabButton tTabButton)
        {
            if (tTabButton == null) return;

            OutlookStyleToolStripButton overButton = new OutlookStyleToolStripButton();

            overButton.Image = tTabButton.Image;
            overButton.Alignment = ToolStripItemAlignment.Right;
            overButton.ToolTipText = tTabButton.CaptionDescription;
            overButton.Tag = tTabButton;
            overButton.Enabled = tTabButton.Enabled; // Tabbuttonun enabled durumu
            overButton.Active = tTabButton.Selected; // Split bar kapandığında taşan butonun seçili olması için
            overButton.Click += new EventHandler(overButton_Click);

            this.Items.Add(overButton);

            ToolStripMenuItem overMenuItem = new ToolStripMenuItem(tTabButton.Caption, tTabButton.Image);
            overMenuItem.Click += new EventHandler(overMenuItem_Click);
            overMenuItem.Tag = overButton;
            overMenuItem.Enabled = tTabButton.Enabled;

            tsDugmeMenu.DropDownItems.Insert(0, overMenuItem); // Menu sırasıda tab sırası gibi olmalı
            tsDugmeMenu.Visible = true;

        }

        /// <summary>
        /// Aktif durumlarını kaldır. Bu duruma göre butonun seçimi belirleniyor
        /// </summary>
        public void ClearAktif()
        {
            for (int i = 0; i < Items.Count; i++)
                if (Items[i] is OutlookStyleToolStripButton)
                    (Items[i] as OutlookStyleToolStripButton).Active = false;

            this.Refresh();
        }

        /// <summary>
        /// Taşan simgeler için menyüyü boşaltır
        /// </summary>
        public void ClearItems()
        {
            try
            {
                // Temizle ve Ekle
                this.Items.Clear();
                this.tsSideBarButton.DropDownItems.Clear();
                this.tsDugmeMenu.DropDownItems.Clear();

                tsDugmeMenu.Visible = false; // Gösterecek düğme yok
                tsDugmeGoster.Enabled = false; // Gösterecek düğme yok

                tsSideBarButton.DropDownItems.Add(tsDugmeGoster);
                tsSideBarButton.DropDownItems.Add(tsDugmeGizle);

                tsSideBarButton.DropDownItems.Add(tsDugmeMenu);

                this.Items.Add(tsSideBarButton);
            }
            catch { }
        }

        /// <summary>
        /// Bir menü item gizle seçildi
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void tsDugmeGizle_Click(object sender, EventArgs e)
        {
            if (OnMenuItemMove != null)
                OnMenuItemMove(this, MoveType.Down);
        }

        /// <summary>
        /// Bir menu item göster seçildi
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void tsDugmeGoster_Click(object sender, EventArgs e)
        {
            if (OnMenuItemMove != null)
                OnMenuItemMove(this, MoveType.Up);
        }

        /// <summary>
        /// Seçim görünümünü iptal etmek için
        /// </summary>
        public override void Refresh()
        {
            this.Renderer.DrawToolStripBackground(new ToolStripRenderEventArgs(Graphics.FromHwnd(this.Handle), this));
            base.Refresh();
        }

    }

    #endregion

}
