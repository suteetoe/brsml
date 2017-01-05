using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace SMLPosClient
{
    public partial class _myDataGridView : System.Windows.Forms.DataGridView
    {
        public _myDataGridView()
        {
            this.DoubleBuffered = true;
        }

        protected Image _backgroundImage = null;

        public Image OwnerBackgroundImage
        {
            get { return _backgroundImage; }
            set { _backgroundImage = value; }
        }
        protected override void PaintBackground(Graphics graphics, Rectangle clipBounds, Rectangle gridBounds)
        {
            base.PaintBackground(graphics, clipBounds, gridBounds);
            if (OwnerBackgroundImage != null)
            {
                graphics.FillRectangle(Brushes.Black, gridBounds);
                graphics.DrawImage(this.OwnerBackgroundImage, gridBounds);
            }
        }

        public void SetCellsTransparent()
        {
            this.EnableHeadersVisualStyles = false;
            this.ColumnHeadersDefaultCellStyle.BackColor = Color.Transparent;
            this.RowHeadersDefaultCellStyle.BackColor = Color.Transparent;
            foreach (DataGridViewColumn col in this.Columns)
            {
                col.DefaultCellStyle.BackColor = Color.Transparent;
            }
        }

        public void SetCellsNormal()
        {
            this.EnableHeadersVisualStyles = false;
            this.ColumnHeadersDefaultCellStyle.BackColor = Color.White;
            this.RowHeadersDefaultCellStyle.BackColor = Color.White;
            foreach (DataGridViewColumn col in this.Columns)
            {
                col.DefaultCellStyle.BackColor = Color.White;
            }
        }
    }
}
