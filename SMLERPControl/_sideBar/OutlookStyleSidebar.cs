/*
 * Proje	    : Outlook 2003 Style SideBar v 1.1
 *
 * Hazırlayan   : Muhammed ŞAHİN
 * eMail        : muhammed.sahin@gmail.com
 *
 * Açıklama	    : Outlook 2003 Style Sidebar
 *              : Taşıyıcı Panel
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

    #region Enums

    /// <summary>
    /// Görünüm tipi
    /// </summary>
    [Flags]
    enum VisibleType
    {
        Visible = 1,
        Unvisible
    }

    /// <summary>
    /// Hareket yönü
    /// </summary>
    internal enum MoveType
    {
        Up,
        Down
    }

    #endregion

    #region Class : OutlookStyleSidebar

    [ToolboxBitmap(typeof(Bitmap), "Properties.Resources.OutlookStyleSidebar")]
    [ToolboxItem(true)]
    [Description("Outlook 2003 Style Sidebar")]
    [Browsable(true)]
    public sealed class OutlookStyleSidebar : UserControl
    {
        // Delegate & Events

        #region Delegate & Events

        public delegate void OnTabHeightChangedEventHandler(int tOldHeight, int tNewHeight);
        public event OnTabHeightChangedEventHandler OnTabHeightChanged;

        public delegate void OnVisibleItemCountChangeEventHandler(int tVisibleItemCount);
        public event OnVisibleItemCountChangeEventHandler OnVisibleItemCountChange;

        public delegate void OnTabButtonAddedEventHandler(object sender, OutlookStyleTabButton tTabButton);
        public event OnTabButtonAddedEventHandler OnTabButtonAdded;

        public delegate void OnTabButtonSelectedEventHandler(object sender, OutlookStyleTabButtonEventArgs e);
        public event OnTabButtonSelectedEventHandler OnTabButtonSelected;

        public delegate void OnTabButtonRemoveEventHandler(object sender, OutlookStyleTabButtonEventArgs e);
        public event OnTabButtonRemoveEventHandler OnTabButtonRemove;

        #endregion

        // Properties

        #region TabButton Collection
        OutlookStyleTabButtonCollection tabButtonCollection = new OutlookStyleTabButtonCollection();

        /// <summary>
        /// TabButton Collection (Get)
        /// </summary>
        [Browsable(false)] // özellik olarak gözükmesin (convert hazırlanacak)
        public OutlookStyleTabButtonCollection TabButtons
        {
            get { return tabButtonCollection; }
        }

        #endregion

        #region TabHeight

        private int tabHeight = 32;
        /// <summary>
        /// TabButton yüksekliği
        /// </summary>
        [DefaultValue(32)]
        public int TabHeight
        {
            get { return tabHeight; }
            set
            {
                int oldHeight = tabHeight;
                if (value < 15) value = 32;
                tabHeight = value;

                splitTabItems.FixedPanel = FixedPanel.None;

                splitTabItems.Panel1MinSize = tabHeight;
                splitTabItems.Panel2MinSize = tabHeight;
                splitBase.SplitterIncrement = tabHeight;

                foreach (OutlookStyleTabButton otb in tabButtonCollection)
                    otb.Height = tabHeight;

                splitTabItems.FixedPanel = FixedPanel.Panel2;

                SplitReCalculate();

                if (OnTabHeightChanged != null)
                    OnTabHeightChanged(oldHeight, tabHeight);
            }
        }

        #endregion

        #region SelectedTabButton
        private OutlookStyleTabButton selectedTabButton = null;
        /// <summary>
        /// Seçili tab buton
        /// </summary>
        public OutlookStyleTabButton SelectedTabButton
        {
            get { return selectedTabButton; }
            set
            {
                foreach (OutlookStyleTabButton otb in tabButtonCollection)
                    otb.Selected = false; // Diğer tüm seçimler iptal

                value.Selected = true;
                selectedTabButton = value;
                tButton_OnTabSelected(this, new OutlookStyleTabButtonEventArgs(selectedTabButton)); // Seçili olan TaButtona geç
                overflowItems.ReDrawIcon(); // Degişen tabı taşan tablara yansıt
            }
        }
        #endregion

        #region ThemeColors
        OutlookStyleThemeColor themeColor = OutlookStyleThemeColor.SystemColor;
        public OutlookStyleThemeColor ThemeColor
        {
            get { return themeColor; }
            set
            {
                themeColor = value;
                foreach (OutlookStyleTabButton tButton in TabButtons)
                    tButton.ThemeColor = themeColor;
                overflowItems.ThemeColor = themeColor;
                sideBarCaption.ThemeColor = themeColor;
                sideBarCaptionDesc.ThemeColor = themeColor;
                splitBase.ThemeColor = themeColor;
            }
        }
        #endregion

        //SplitContainers

        OutlookStyleSplitContainer splitBase = new OutlookStyleSplitContainer();
        OutlookStyleSplitContainer splitTabItems = new OutlookStyleSplitContainer();
        OutlookStyleSplitContainer splitCaption = new OutlookStyleSplitContainer();

        //ToolStrips

        OutlookStyleSideBarCaption sideBarCaption = new OutlookStyleSideBarCaption();
        OutlookStyleSideBarCaptionDescription sideBarCaptionDesc = new OutlookStyleSideBarCaptionDescription();
        OutlookStyleOverflowItems overflowItems;

        //Var

        private int m_halfHeight = 0;
        private int m_lastVisibleItemCount = 0;

        #region Constructor Methods
        public OutlookStyleSidebar()
        {
            InitOutlookStyleSideBar();
        }

        void InitOutlookStyleSideBar()
        {
            m_halfHeight = (int)(this.Height / 2);
            m_lastVisibleItemCount = GetVisibleItemCount(VisibleType.Visible);

            // Collection

            tabButtonCollection.OnTabButtonItemAdded += new OutlookStyleTabButtonCollection.OnItemAddedEventHandler(tabButtonCollection_OnTabButtonItemAdded);
            tabButtonCollection.OnTabButtonItemRemoved += new OutlookStyleTabButtonCollection.OnItemRemovedEventHandler(tabButtonCollection_OnTabButtonItemRemoved);

            // Control

            this.BackColor = SystemColors.ControlLightLight;
            this.Dock = DockStyle.Fill;
            this.BorderStyle = BorderStyle.None;
            this.Resize += new EventHandler(OutlookStyleSideBar_Resize);

            #region overflowItems
            overflowItems = new OutlookStyleOverflowItems(tabButtonCollection);
            overflowItems.Height = this.TabHeight;
            overflowItems.ItemClicked += new ToolStripItemClickedEventHandler(overflowItems_ItemClicked);
            overflowItems.OnMenuItemMove += new OutlookStyleOverflowItems.MenuItemMoveEventHandler(overflowItems_OnMenuItemMove);
            #endregion

            #region splitTabItems (Taşan tablar ve eklenen tablar için)
            splitTabItems.Panel1MinSize = this.TabHeight;
            splitTabItems.Panel2MinSize = this.TabHeight;
            splitTabItems.IsSplitterFixed = true;
            splitTabItems.SplitterWidth = 1;
            splitTabItems.FixedPanel = FixedPanel.Panel2;
            #endregion

            #region splitCaption (Başlık bilgileri ve kontrol bilgisi için)
            splitCaption.SplitterDistance = sideBarCaption.Height + sideBarCaptionDesc.Height - 1;
            splitCaption.MinimumSize = new Size(this.Width, sideBarCaption.Height + sideBarCaptionDesc.Height);
            splitCaption.FixedPanel = FixedPanel.Panel1;
            splitCaption.IsSplitterFixed = true;
            splitCaption.SplitterWidth = 1;
            splitCaption.Panel1MinSize = sideBarCaption.Height + sideBarCaptionDesc.Height;
            #endregion

            #region splitBase (Tüm kontrollerin taşıyıcısı)
            splitBase.SplitterWidth = 6;
            splitBase.SplitterIncrement = this.TabHeight;
            splitBase.SplitterMoving += new SplitterCancelEventHandler(splitBase_SplitterMoving);
            splitBase.SplitterMoved += new SplitterEventHandler(splitBase_SplitterMoved);
            splitBase.BackColor = SystemColors.ControlLightLight;
            #endregion

            // User Control

            splitCaption.Panel1.Controls.Add(sideBarCaptionDesc);
            splitCaption.Panel1.Controls.Add(sideBarCaption);

            splitTabItems.Panel2.Controls.Add(overflowItems);

            splitBase.Panel1.Controls.Add(splitCaption);
            splitBase.Panel2.Controls.Add(splitTabItems);

            Controls.Add(splitBase);

            // Set Default Theme

            OutlookStyleThemeColor thmColor = new OutlookStyleThemeColor();
            this.ThemeColor = thmColor;
            thmColor = null;

            // 

            SplitReCalculate();

        }

        #endregion

        #region Event : OnTabButtonItemRemoved (TabButton çıkarıldığında)
        /// <summary>
        /// Collection içerisinden TabButton kaldırıldığında
        /// </summary>
        /// <param name="e"></param>
        void tabButtonCollection_OnTabButtonItemRemoved(OutlookStyleTabButtonEventArgs e)
        {
            try
            {

                if (tabButtonCollection.Count >= 0)
                {

                    // Control list içerisinden kaldır

                    splitTabItems.Panel1.Controls.Remove(e.TabButton as Control);
                    splitCaption.Panel2.Controls.Clear();

                    // Başlıkları boşalt

                    sideBarCaption.Caption = "";
                    sideBarCaptionDesc.Caption = "";

                    // Görünen tab sayısını yenile 

                    m_lastVisibleItemCount = GetVisibleItemCount(VisibleType.Visible);

                    // Sonraki elemanı aktif hale getir

                    if (tabButtonCollection.Count > 0)
                        this.SelectedTabButton = tabButtonCollection[0];
                    else
                        overflowItems.ReDrawIcon();

                    // Hesapla

                    SplitReCalculate();

                    // Event çalıştır

                    if (OnTabButtonRemove != null)
                        OnTabButtonRemove(this, new OutlookStyleTabButtonEventArgs(e.TabButton));
                }
            }
            catch { }

        }
        #endregion

        #region Event : OnTabButtonItemAdded (TabButton eklendiğinde)
        /// <summary>
        /// Collection içerisine yeni TabButton eklendiğinde
        /// </summary>
        /// <param name="e"></param>
        void tabButtonCollection_OnTabButtonItemAdded(OutlookStyleTabButtonEventArgs e)
        {

            OutlookStyleTabButton tButton = e.TabButton;

            if (tButton == null)
                return;

            // ThemeColor (Eğer kendi renk belirtmemişse varsayılan renk düzenini kullan)

            if (tButton.ThemeColor == null)
                tButton.ThemeColor = this.ThemeColor;

            // taba yüksekliğini belirt

            tButton.Height = this.TabHeight;

            // Eğer panel doluysa eklenen tabları görünmez yaparak itemtaba gönder
            if (GetAllTabItemsHeight() > m_halfHeight)
            {
                tButton.Visible = false;
                overflowItems.ReDrawIcon();
            }
            else
                m_lastVisibleItemCount++;

            // Tab ekele
            splitTabItems.Panel1.Controls.Add(tButton); // Butonu item panele ekle

            // Eğer ilk nesne eklendiyse Captionlar için tıkla
            if (tabButtonCollection.Count == 1) // ilk tıklama
                tButton_OnTabSelected(tButton, new OutlookStyleTabButtonEventArgs(tButton));

            // Tab olayları

            tButton.OnTabSelected += new OutlookStyleTabButton.OnTabSelectedEventHandler(tButton_OnTabSelected);
            tButton.VisibleChanged += new EventHandler(tButton_VisibleChanged);
            tButton.EnabledChanged += new EventHandler(tButton_EnabledChanged);

            // Event
            if (OnTabButtonAdded != null)
                OnTabButtonAdded(this, tButton);

            // Tekrar oluştur
            SplitReCalculate();

        }
        #endregion

        #region Event : OnTabSelected

        void tButton_OnTabSelected(object sender, OutlookStyleTabButtonEventArgs e)
        {
            OutlookStyleTabButton tButton = e.TabButton;

            // Control içerisindeki tüm butonların Selected = false

            foreach (OutlookStyleTabButton otb in tabButtonCollection)
                otb.Selected = false;

            // Seçilen tab için Control

            SetControlForTabButton(e.TabButton.Control);

            // Taşan buttonları Aktif durumunu kaldır

            overflowItems.ClearAktif();

            // Seçili tabbuttonun özelliklerini aktar

            sideBarCaption.Caption = tButton.Caption;
            sideBarCaptionDesc.Caption = tButton.CaptionDescription;

            tButton.Selected = true;

            selectedTabButton = tButton;

            if (OnTabButtonSelected != null)
                OnTabButtonSelected(this, e);

        }

        void SetControlForTabButton(Control tControl)
        {
            // Seçilen tab için Control

            splitCaption.Panel2.Controls.Clear();
            splitCaption.Panel2.Controls.Add(tControl);
            tControl.Select();
        }

        #endregion

        #region Diğer Metodlar

        void tButton_VisibleChanged(object sender, EventArgs e)
        {
            SplitReCalculate();
        }

        /// <summary>
        /// Enable değiştiğinde taşanlar içerisindeki durumlarıda değiştirmeli
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void tButton_EnabledChanged(object sender, EventArgs e)
        {
            overflowItems.ReDrawIcon();
        }

        /// <summary>
        /// Taşan elemanların button üzerinden yapılan click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void overflowItems_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem is OutlookStyleToolStripButton)
            {
                OutlookStyleTabButton tbtn = e.ClickedItem.Tag as OutlookStyleTabButton;
                tButton_OnTabSelected(null, new OutlookStyleTabButtonEventArgs(tbtn));
            }
        }

        /// <summary>
        /// Dügme Göster / Gizle menüsünden geliş
        /// Göster dediğinde yada gizle dediğinde görünen tab sayısını değiştir
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="mt"></param>
        void overflowItems_OnMenuItemMove(object sender, MoveType mt)
        {

            switch (mt)
            {
                case MoveType.Up: // Yukarı çek
                    {
                        if (m_lastVisibleItemCount < tabButtonCollection.Count)
                            m_lastVisibleItemCount++;
                        break;
                    }
                case MoveType.Down: // Aşağı çek
                    {
                        if (m_lastVisibleItemCount >= 1)
                            m_lastVisibleItemCount--;
                        break;
                    }
            }

            splitBase_SplitterMoved(null, null);

        }

        /// <summary>
        /// Sidebar tekrar boyutlandır
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OutlookStyleSideBar_Resize(object sender, EventArgs e)
        {
            SplitReCalculate();
            Invalidate(true);
        }


        #endregion

        #region Splitters Events

        /// <summary>
        /// Base Splitter taşındığında
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void splitBase_SplitterMoving(object sender, SplitterCancelEventArgs e)
        {
            try
            {

                // Boyut içerisine sığacak olan tabbutton sayısını hesapla
                float tabCount = ((this.Height - e.SplitY)) / this.TabHeight;
                int adet = (int)Math.Round(tabCount, 1);

                if (adet > m_lastVisibleItemCount) adet--; // fare yukarı harekette olduğundan bir fazla gidiyordu
                m_lastVisibleItemCount = adet > tabButtonCollection.Count ? tabButtonCollection.Count : adet;
                m_lastVisibleItemCount = m_lastVisibleItemCount < 0 ? 0 : m_lastVisibleItemCount;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        /// Tabların boyutunu ayarlama
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void splitBase_SplitterMoved(object sender, SplitterEventArgs e)
        {

            // Tab görünümlerini ayarla

            for (int i = 0; i < tabButtonCollection.Count; i++)
                tabButtonCollection[i].Visible = (i < m_lastVisibleItemCount);

            // sığmayan tabları ayarla
            if (m_lastVisibleItemCount != GetVisibleItemCount(VisibleType.Visible | VisibleType.Unvisible))
                overflowItems.ReDrawIcon();
            else
                overflowItems.ClearItems();


            // Eğer tüm tablar gözüküyor ve tab yükseklikleri yüksekliğin 
            // ortasından küçükse split hareketini geri al

            if (tabButtonCollection.Count == GetVisibleItemCount(VisibleType.Visible))
                SplitReCalculate();

            //

            if (m_lastVisibleItemCount != 0)
                SplitReCalculate();

            // 
            if (OnVisibleItemCountChange != null)
                OnVisibleItemCountChange(m_lastVisibleItemCount);
        }

        #endregion

        #region Method : SplitReCalculate

        void SplitReCalculate()
        {
            try
            {

                m_halfHeight = ((int)this.Height / 2);

                // Base Split

                int nDistance = splitBase.Height - ((m_lastVisibleItemCount * TabHeight) + TabHeight + splitBase.SplitterWidth + 1);
                splitBase.SplitterDistance = nDistance;

                // Item Split
                nDistance = splitTabItems.Height - TabHeight + splitBase.SplitterWidth;
                splitTabItems.SplitterDistance = nDistance;

            }
            catch { }

        }

        #endregion

        #region Method : GetAllTabItemsHeight
        /// <summary>
        /// Container içerisindeki tabbutonların yüksekligini geri gönderir
        /// </summary>
        /// <returns></returns>
        int GetAllTabItemsHeight()
        {
            int itemsHeight = 0;

            foreach (OutlookStyleTabButton otb in tabButtonCollection)
                if (otb.Visible)
                    itemsHeight += otb.Height;

            return itemsHeight;
        }

        #endregion

        #region Method : GetVisibleItemCount
        /// <summary>
        /// Tabların sayısını alma
        /// </summary>
        /// <param name="tVisibleType"></param>
        /// <returns></returns>
        int GetVisibleItemCount(VisibleType tVisibleType)
        {

            int visibleItemCount = 0;

            foreach (OutlookStyleTabButton otb in tabButtonCollection)
            {
                switch (tVisibleType)
                {
                    case VisibleType.Unvisible:
                        {
                            if (!otb.Visible) visibleItemCount++;
                            break;
                        }
                    case VisibleType.Visible:
                        {
                            if (otb.Visible) visibleItemCount++;
                            break;
                        }
                    case VisibleType.Visible | VisibleType.Unvisible:
                        {
                            visibleItemCount++;
                            break;
                        }
                }
            }

            return visibleItemCount;
        }
        #endregion

    }
    #endregion
}
