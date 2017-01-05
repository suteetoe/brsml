/*
 * Proje	    : Outlook 2003 Style SideBar v 1.1
 *
 * Hazırlayan   : Muhammed ŞAHİN
 * eMail        : muhammed.sahin@gmail.com
 *
 * Açıklama	    : Outlook 2003 Style Sidebar
 *              : TabButtonlar için renklendirme bölümü
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
    #region Class : OutlookStyleRenderer
    /// <summary>
    /// ToolStrip Render
    /// </summary>
    [Browsable(false)]
    [ToolboxItem(false)]
    sealed class OutlookStyleRenderer : ToolStripRenderer
    {

        // Properties

        #region LightColor
        private Color lightColor = SystemColors.ControlLightLight;
        /// <summary>
        /// Gradient rengin açık olanı
        /// </summary>
        public Color LightColor
        {
            get { return lightColor; }
            set
            {
                lightColor = value;
            }
        }
        #endregion

        #region DarkColor
        private Color darkColor = SystemColors.Control;
        /// <summary>
        /// Gradient rengin koyu olanı
        /// </summary>
        public Color DarkColor
        {
            get { return darkColor; }
            set
            {
                darkColor = value;
            }
        }
        #endregion

        #region DrawOutRectangle
        bool drawOutRectangle = true;
        /// <summary>
        /// Dış tarafa çizgi çizsin mi ?
        /// </summary>
        public bool DrawOutRectangle
        {
            get { return drawOutRectangle; }
            set { drawOutRectangle = value; }
        }

        #endregion

        public OutlookStyleRenderer()
        {
        }

        public OutlookStyleRenderer(Color tLightColor, Color tDarkColor, bool tDrawOutRectange)
        {
            this.DarkColor = tDarkColor;
            this.LightColor = tLightColor;
            this.DrawOutRectangle = tDrawOutRectange;
        }

        /// <summary>
        /// Menü yanında yer alan button image
        /// </summary>
        /// <param name="e"></param>
        protected override void OnRenderImageMargin(ToolStripRenderEventArgs e)
        {
            using (Brush b = new SolidBrush(SystemColors.ControlLightLight))
            {
                e.Graphics.FillRectangle(b, e.AffectedBounds);
            }
            base.OnRenderImageMargin(e);
        }

        /// <summary>
        /// Seçili menu için gradient boyamadan kaynaklanan seçim rengini düzenleme
        /// </summary>
        /// <param name="e"></param>
        protected override void OnRenderMenuItemBackground(ToolStripItemRenderEventArgs e)
        {
            if (e.Item.Selected)
            {
                using (Brush b = new SolidBrush(SystemColors.ControlLightLight))
                {
                    e.Item.ForeColor = SystemColors.MenuText;
                    e.Graphics.FillRectangle(b, e.Item.ContentRectangle);
                }
                using (Pen p = new Pen(SystemColors.Control))
                {
                    e.Graphics.DrawRectangle(p, e.Item.ContentRectangle);
                }
            }
            base.OnRenderMenuItemBackground(e);
        }

        /// <summary>
        /// TabButton dışana çizgi çiz
        /// </summary>
        /// <param name="e"></param>
        protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e)
        {
            Pen pen = new Pen(SystemColors.ControlDark);
            if (this.DrawOutRectangle)
                e.Graphics.DrawRectangle(pen, 0, 0, e.ToolStrip.Width - 1, e.ToolStrip.Height);
            pen.Dispose();
            base.OnRenderToolStripBorder(e);
        }

        /// <summary>
        /// Rengini gradient olarak boya
        /// Tab ait button nesnelerinide ayarla
        /// </summary>
        /// <param name="e"></param>
        protected override void OnRenderToolStripBackground(ToolStripRenderEventArgs e)
        {
            PaintThis(e.Graphics, new Rectangle(0, 0, e.ToolStrip.Width, e.ToolStrip.Height), LightColor, DarkColor);

            foreach (ToolStripItem tsi in e.ToolStrip.Items)
            {
                if (tsi.IsOnOverflow) continue; // Eğer taştıysa uğraşma

                OutlookStyleToolStripButton tsbtn = tsi as OutlookStyleToolStripButton;
                if (tsbtn != null)
                {
                    if (tsbtn.Selected || tsbtn.Active || tsbtn.Pressed)
                    {
                        //e.Graphics.FillRectangle(SystemBrushes.ControlLightLight, tsbtn.Bounds);
                        //e.Graphics.DrawRectangle(new Pen(SystemColors.Control), tsbtn.Bounds );
                        PaintThis(e.Graphics, tsbtn.Bounds, DarkColor, LightColor);
                    }
                    else
                        PaintThis(e.Graphics, tsbtn.Bounds, LightColor, DarkColor);
                    //e.Graphics.DrawRectangle(Pens.Black, tsbtn.Bounds);
                }

            }

            base.OnRenderToolStripBackground(e);
        }


        /// <summary>
        /// Boyama fonksiyonu
        /// </summary>
        /// <param name="g"></param>
        /// <param name="r"></param>
        /// <param name="LightColor"></param>
        /// <param name="DarkColor"></param>
        void PaintThis(Graphics g, Rectangle r, Color LightColor, Color DarkColor)
        {
            using (LinearGradientBrush lgb = new LinearGradientBrush(r, LightColor, DarkColor, 90F))
            {
                g.FillRectangle(lgb, r);
            }
        }

    } // Class Render

    #endregion
}
