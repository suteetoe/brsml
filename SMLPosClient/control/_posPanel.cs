using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace SMLPosClient.control
{
    class _posPanel : Panel
    {
        private bool _useBackGroundGradientResult = true;
        private int _GradientAngleResult = 90;
        private Color _GradientStartColorResult = Color.White;
        private Color _GradientEndColorResult = Color.Goldenrod;
        private Color _lineColorResult = Color.Black;

        private Padding _roundedRadiusProperty = new Padding(10);

        private string _idResult;
        public string _id
        {
            get { return _idResult; }
            set { _idResult = value; }
        }

        public _posPanel()
        {
            //this.BackColor = Color.Transparent;
        }

        public bool _useBackGroundGradient
        {
            get { return _useBackGroundGradientResult; }
            set { _useBackGroundGradientResult = value; }
        }

        public int _GradientAngle
        {
            get { return _GradientAngleResult; }
            set { _GradientAngleResult = value; }
        }

        public Color _lineColor
        {
            get { return _lineColorResult; }
            set { _lineColorResult = value; }
        }

        public Color _GradientStartColor
        {
            get { return _GradientStartColorResult; }
            set { _GradientStartColorResult = value; }
        }

        public Color _GradientEndColor
        {
            get { return _GradientEndColorResult; }
            set { _GradientEndColorResult = value; }
        }

        public Padding _roundedRadius
        {
            get { return _roundedRadiusProperty; }
            set { _roundedRadiusProperty = value; }
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            //base.OnPaint(e);
            Graphics g = e.Graphics;
            g.PageUnit = GraphicsUnit.Pixel;

            g.SmoothingMode = SmoothingMode.HighQuality;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;

            Pen __pen = new Pen(this._lineColor);
            //__pen.DashStyle = base._LineStyle;

            //Rectangle __RectangleNormalized = _drawRectangle._getNormalizedRectangle(Size);
            Rectangle __rect = this.ClientRectangle;
            __rect.Width--;
            __rect.Height--;

            GraphicsPath gfxPath = new GraphicsPath();

            gfxPath = _getRectangleGraphic(__rect, _roundedRadius.All, _roundedRadius.All, _roundedRadius.All, _roundedRadius.All);

            if (_useBackGroundGradient)
            {
                LinearGradientBrush __brush = new LinearGradientBrush(__rect, _GradientStartColorResult, _GradientEndColorResult, _GradientAngle, true);
                g.FillPath(__brush, gfxPath);
                __brush.Dispose();
            }
            else
            {
                SolidBrush __brush = new SolidBrush(this.BackColor);
                g.FillPath(__brush, gfxPath);
                __brush.Dispose();
            }
            g.DrawPath(__pen, gfxPath);

            __pen.Dispose();

        }

        public GraphicsPath _getRectangleGraphic(RectangleF __rectangle, float __topLeft, float __topRight, float __bottomRight, float __bottomLeft)
        {
            GraphicsPath gfxPath = new GraphicsPath();

            PointF Point1 = new PointF();
            PointF Point2 = new PointF();
            // TopLeft Corner
            if (__topLeft > 0)
            {
                gfxPath.AddArc(__rectangle.X, __rectangle.Y, __topLeft, __topLeft, 180, 90); // topLeft
            }
            else
            {
                Point1.X = __rectangle.X;
                Point1.Y = __rectangle.Y + (__rectangle.Height - __bottomLeft);
                Point2.X = __rectangle.X;
                Point2.Y = __rectangle.Y;
                gfxPath.AddLine(Point1, Point2);

                Point1.X = __rectangle.X;
                Point1.Y = __rectangle.Y;
                Point2.X = __rectangle.X + (__rectangle.Width - __topLeft);
                Point2.Y = __rectangle.Y;
                gfxPath.AddLine(Point1, Point2);
            }

            // TopRight Corner
            if (__topRight > 0)
            {
                gfxPath.AddArc(__rectangle.X + __rectangle.Width - __topRight, __rectangle.Y, __topRight, __topRight, 270, 90); // topRight
            }
            else
            {
                Point1.X = __rectangle.X + __topLeft;
                Point1.Y = __rectangle.Y;
                Point2.X = __rectangle.X + __rectangle.Width;
                Point2.Y = __rectangle.Y;
                gfxPath.AddLine(Point1, Point2);

                Point1.X = __rectangle.X + __rectangle.Width;
                Point1.Y = __rectangle.Y;
                Point2.X = __rectangle.X + __rectangle.Width;
                Point2.Y = __rectangle.Y + (__rectangle.Height - __bottomRight);
                gfxPath.AddLine(Point1, Point2);
            }

            // Bottom Right 
            if (__bottomRight > 0)
            {
                gfxPath.AddArc(__rectangle.X + __rectangle.Width - __bottomRight, __rectangle.Y + __rectangle.Height - __bottomRight, __bottomRight, __bottomRight, 0, 90); // bottomRight
            }
            else
            {
                Point1.X = __rectangle.X + __rectangle.Width;
                Point1.Y = __rectangle.Y + __topRight;
                Point2.X = __rectangle.X + __rectangle.Width;
                Point2.Y = __rectangle.Y + __rectangle.Height;
                gfxPath.AddLine(Point1, Point2);

                Point1.X = __rectangle.X + __rectangle.Width;
                Point1.Y = __rectangle.Y + __rectangle.Height;
                Point2.X = __rectangle.X + __bottomLeft;
                Point2.Y = __rectangle.Y + __rectangle.Height;
                gfxPath.AddLine(Point1, Point2);
            }

            // Bottom Left 

            if (__bottomLeft > 0)
            {
                gfxPath.AddArc(__rectangle.X, __rectangle.Y + __rectangle.Height - __bottomLeft, __bottomLeft, __bottomLeft, 90, 90); // bottomLeft
            }
            else
            {
                Point1.X = __rectangle.X + (__rectangle.Width - __bottomRight);
                Point1.Y = __rectangle.Y + __rectangle.Height;
                Point2.X = __rectangle.X;
                Point2.Y = __rectangle.Y + __rectangle.Height;
                gfxPath.AddLine(Point1, Point2);

                Point1.X = __rectangle.X;
                Point1.Y = __rectangle.Y + __rectangle.Height;
                Point2.X = __rectangle.X;
                Point2.Y = __rectangle.Y + __topLeft;
                gfxPath.AddLine(Point1, Point2);
            }

            gfxPath.CloseAllFigures();

            return gfxPath;
        }

    }
}
