/*
 * Proje	    : Outlook 2003 Style SideBar v 1.1
 *
 * Hazırlayan   : Muhammed ŞAHİN
 * eMail        : muhammed.sahin@gmail.com
 *
 * Açıklama	    : Outlook 2003 Style Sidebar
 *              : Tüm panelleri için ayıraç. Yatay düzlemde ayıraç göstergesi sağlıyor
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
    #region Class : OutlookStyleSplitContainer

    [Browsable(false)]
    [ToolboxItem(false)]
    sealed class OutlookStyleSplitContainer : SplitContainer
    {

        #region ThemeColor
        OutlookStyleThemeColor themeColor = OutlookStyleThemeColor.SystemColor;
        internal OutlookStyleThemeColor ThemeColor
        {
            set
            { 
                themeColor = value;
                Invalidate();
            }
        }
        #endregion

        public OutlookStyleSplitContainer()
        {
            InitOutlookStyleSplitContainer();
        }

        void InitOutlookStyleSplitContainer()
        {
            this.Orientation = Orientation.Horizontal;
            this.BorderStyle = BorderStyle.None;
            this.Dock = DockStyle.Fill;
            this.SplitterWidth = 4;
            this.Paint += new PaintEventHandler(OutlookStyleSplitContainer_Paint);
            this.Resize += new EventHandler(OutlookStyleSplitContainer_Resize);
            this.SplitterMoving += new SplitterCancelEventHandler(OutlookStyleSplitContainer_SplitterMoving);
            this.SplitterMoved += new SplitterEventHandler(OutlookStyleSplitContainer_SplitterMoved);
        }

        /// <summary>
        /// Splitter taşındıktan sonra imleçi default haline getir
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OutlookStyleSplitContainer_SplitterMoved(object sender, SplitterEventArgs e)
        {
            Cursor.Current = Cursors.Default;
        }

        /// <summary>
        /// İmleçi değiştir
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OutlookStyleSplitContainer_SplitterMoving(object sender, SplitterCancelEventArgs e)
        {
            if (this.Orientation == Orientation.Horizontal)
                Cursor.Current = Cursors.SizeNS;
            else
                Cursor.Current = Cursors.SizeWE;
        }

        /// <summary>
        /// Tekrar görünümü ayarla
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OutlookStyleSplitContainer_Resize(object sender, EventArgs e)
        {
            Invalidate();
        }

        /// <summary>
        /// Splitter içerisine ayıraç noktaları ekleme
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void OutlookStyleSplitContainer_Paint(object sender, PaintEventArgs e)
        {

            if (!(SplitterRectangle.Width > 0 && SplitterRectangle.Height > 0))
                return;

            int noktaBoyut = 4, noktaYukseklik = 2;

            using (Brush b = new LinearGradientBrush(SplitterRectangle, themeColor.DarkColor, themeColor.DarkDarkColor, LinearGradientMode.Vertical))
            {
                e.Graphics.FillRectangle(b, SplitterRectangle);
            }

            int noktaSayisi = Math.Min((SplitterRectangle.Width / noktaBoyut), 10);
            int ilkNoktaKoor = (SplitterRectangle.Width - (noktaSayisi * noktaBoyut)) / 2;
            int Y = SplitterRectangle.Y + 1;

            Brush koyuRenk = new SolidBrush(SystemColors.ControlDark);
            Brush acikRenk = new SolidBrush(SystemColors.ControlLightLight);

            // Kareleri oluştur
            for (int i = 0; i < noktaSayisi; i++)
            {
                e.Graphics.FillRectangle(koyuRenk, ilkNoktaKoor, Y, noktaYukseklik, noktaYukseklik);
                e.Graphics.FillRectangle(acikRenk, ilkNoktaKoor + 1, Y + 1, noktaYukseklik, noktaYukseklik);
                ilkNoktaKoor += noktaBoyut;
            }

            koyuRenk.Dispose();
            acikRenk.Dispose();

        }

    }
    #endregion
}
