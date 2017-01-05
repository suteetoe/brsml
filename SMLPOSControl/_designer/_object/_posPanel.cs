using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using SMLReport._design;
using System.Drawing.Drawing2D;

namespace SMLPOSControl._designer._object
{
    class _posPanel :_drawRoundedRectangle
    {
        private bool _useBackGroundGradientResult = true;
        private int _GradientAngleResult = 90;
        private Color _GradientStartColorResult = Color.White;
        private Color _GradientEndColorResult = Color.Goldenrod;

        private RoundedRectangleRadius _roundedRadiusProperty = new RoundedRectangleRadius(10);

        private string _tagResult = "";
        public string _Tag
        {
            get
            {
                return _tagResult;
            }
            set
            {
                _tagResult = value;
            }
        }

        private string _objectIdResult = "";
        public string _Id
        {
            get { return _objectIdResult; }
            set { _objectIdResult = value; }
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

        public RoundedRectangleRadius _roundedRadius
        {
            get { return _roundedRadiusProperty; }
            set { _roundedRadiusProperty = value; }
        }


        public _posPanel()
        {
            this._lineColor = Color.Goldenrod;
        }

        public override void _draw(Graphics g)
        {
            Pen __pen = new Pen(this._lineColor, this._penWidth * (float)this._drawScale);
            __pen.DashStyle = base._LineStyle;

            Rectangle __RectangleNormalized = _drawRectangle._getNormalizedRectangle(Size);

            GraphicsPath gfxPath = new GraphicsPath();

            gfxPath = base._getRectangleGraphic(__RectangleNormalized);

            if (_useBackGroundGradient)
            {
                LinearGradientBrush __brush = new LinearGradientBrush(base._actualSize, _GradientStartColorResult, _GradientEndColorResult, _GradientAngle, true);
                g.FillPath(__brush, gfxPath);
                __brush.Dispose();
            }
            else
            {
                SolidBrush __brush = new SolidBrush(this._backColor);
                g.FillPath(__brush, gfxPath);
                __brush.Dispose();
            }
            g.DrawPath(__pen, gfxPath);

            //g.FillRectangle(__brush, __RectangleNormalized);
            //g.DrawRectangle(__pen, __RectangleNormalized);

            __pen.Dispose();

            //base._draw(g);
        }


    }
}
