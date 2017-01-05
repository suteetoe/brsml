// Original RoundedGroupBox was developed by Rea Andrew and can be found at http://weblogs.asp.net/andrewrea/archive/2008/02/18/my-own-version-of-a-winforms-rounded-groupbox-with-transparency.aspx

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace MyLib
{
    public partial class RoundedGroupBox : GroupBox
    {
        //This is the size of the text of the groupbox, e.g. _groupNameDimensions = e.Graphics.MeasureString(this.Text, this.Font);
        private SizeF _groupNameDimensions;
        //The radius of the corners with a default value of 10
        private float _cornerRadius = 10;
        //The leading text margin of the left side
        private float _leadingTextMargin = 25;
        // I need a graphics path here for the corner arcs and then finally close the figure
        private GraphicsPath _roundBoxPath;
        // The first colour of the gradient
        private Color _gradientFrom = Color.Gray;
        //The second colour of the gradient
        private Color _gradientTo = Color.White;
        //The angle of the gradient
        private float _gradientAngle = 90;
        //The fill type, this could be Fill or Linear Gradient
        private FillType _groupFillType = FillType.Fill;
        //The back colour.  This is the colour within the Graphics path, if you change the controls back color that will be behind the groupbox
        private Color _groupBackColor = Color.White;
        //The colour of the border
        private Color _groupBorderColor = Color.Black;
        //The width of the border
        private int _groupBorderWidth = 1;
        //The dash style of the border, this can be set to Solid
        private DashStyle _groupLineStyle = DashStyle.Solid;
        //A Custom pattern for the dashes like 10,50 etc...
        private float[] _dashPattern = new float[] { 5, 5 };
        //A flag to signal if the groupbox has text or not
        private bool _tabbedGroupBoxText = true;

        private _languageEnum _lastLanguage = _languageEnum.Null;
        public string _name = "";

        [Category("_SML")]
        [Description("Name ใช้ในการหา Resource")]
        [DisplayName("Name ใช้ในการหา Resource")]
        public string ResourceName
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }

        [Description("The custom pattern for the Group Box Border")]
        [Category("GroupBox Dash")]
        [NotifyParentProperty(true)]
        public float[] DashPattern
        {
            get { return _dashPattern; }
            set { _dashPattern = value;
            }
        }

        [NotifyParentProperty(true)]
        public DashStyle GroupLineStyle
        {
            get { return _groupLineStyle; }
            set { _groupLineStyle = value; }
        }

        [NotifyParentProperty(true)]
        public int GroupBorderWidth
        {
            get { return _groupBorderWidth; }
            set { _groupBorderWidth = value; }
        }

        [NotifyParentProperty(true)]
        public Color GroupBorderColor
        {
            get { return _groupBorderColor; }
            set { _groupBorderColor = value; }
        }

        [NotifyParentProperty(true)]
        public Color GroupBackColor
        {
            get
            {
                return _groupBackColor;
            }
            set
            {
                _groupBackColor = value;
            }
        }

        int __alpha = 255;
        public int _alpha
        {
            get { return __alpha; }
            set { __alpha = value; }
        }

        [NotifyParentProperty(true)]
        public FillType GroupFillType
        {
            get { return _groupFillType; }
            set { _groupFillType = value; }
        }

        [NotifyParentProperty(true)]
        public float GradientAngle
        {
            get { return _gradientAngle; }
            set { _gradientAngle = value; }
        }

        [NotifyParentProperty(true)]
        public Color GradientTo
        {
            get { return Color.FromArgb(_alpha, _gradientTo); }
            set { _gradientTo = value; }
        }

        [NotifyParentProperty(true)]
        public Color GradientFrom
        {
            get { return Color.FromArgb(_alpha, _gradientFrom); }
            set { _gradientFrom = value; }
        }

        public enum FillType
        {
            Fill,
            LinearGradient
        }
        public enum LineStyle
        {
            Solid,
            Dashed,
        }

        [NotifyParentProperty(true)]
        public float CornerRadius
        {
            get { return _cornerRadius; }
            set { _cornerRadius = value; }
        }

        [NotifyParentProperty ( true )]
        public bool TabbedGroupBoxText
        {
            get { return _tabbedGroupBoxText; }
            set { _tabbedGroupBoxText = value; }
        }

        private PointF p1;
        private PointF p2;
        private PointF p3;
        private PointF p4;
        private PointF p5;
        private PointF p6;
        private PointF p7;
        private PointF p8;


        protected override void OnPaint(PaintEventArgs e)
        {
            if (_myGlobal._language != this._lastLanguage)
            {
                _lastLanguage = _myGlobal._language;
                if (this._name.Length > 0)
                {
                    string __getResourceStr = _myGlobal._resource(this._name);
                    if (__getResourceStr.Length > 0)
                        this.Text = __getResourceStr;
                }
            }

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            _tabbedGroupBoxText = this.Text.Trim().Length > 0 ? _tabbedGroupBoxText : false;
            
            this.DoubleBuffered = true;
            _groupNameDimensions = e.Graphics.MeasureString(this.Text, this.Font);
            p1 = new PointF(_leadingTextMargin > (_cornerRadius * 2) ? _leadingTextMargin : (_cornerRadius * 2), this.Margin.Top);
            p2 = new PointF(p1.X + _cornerRadius + _groupNameDimensions.Width, this.Margin.Top);
            p3 = new PointF(p1.X + (_cornerRadius * 2) + _groupNameDimensions.Width, this.Margin.Top + _cornerRadius + _groupNameDimensions.Height - (_cornerRadius > 0 ? 15 : 0));

            if ( _tabbedGroupBoxText )
            {
                p4 = new PointF ( this.Width - _cornerRadius - this.Margin.Right, this.Margin.Top + ( _cornerRadius * 2 ) + _groupNameDimensions.Height - ( _cornerRadius > 0 ? 15 : 0 ) );
                p5 = new PointF ( this.Width - _cornerRadius - this.Margin.Right, this.Height - _cornerRadius - this.Margin.Bottom );
                p6 = new PointF ( this.Margin.Left, this.Height - _cornerRadius - this.Margin.Bottom );
                p7 = new PointF ( this.Margin.Left, this.Margin.Top + ( _cornerRadius * 2 ) + _groupNameDimensions.Height - ( _cornerRadius > 0 ? 15 : 0 ) );
            }
            else
            {
                p4 = new PointF ( this.Width - _cornerRadius - this.Margin.Right, this.Margin.Top + ( int )( _groupNameDimensions.Height / 2 ) + ( _cornerRadius / 2 ) );
                p5 = new PointF ( this.Width - _cornerRadius - this.Margin.Right, this.Height - _cornerRadius - this.Margin.Bottom );
                p6 = new PointF ( this.Margin.Left, this.Height - _cornerRadius - this.Margin.Bottom );
                p7 = new PointF ( this.Margin.Left, this.Margin.Top + ( int )( _groupNameDimensions.Height / 2 ) + ( _cornerRadius / 2 ) );
            }

            p8 = new PointF ( _leadingTextMargin > ( _cornerRadius * 2 ) ? _leadingTextMargin - _cornerRadius : _cornerRadius, this.Margin.Top + _cornerRadius + _groupNameDimensions.Height - ( _cornerRadius > 0 ? 15 : 0 ) );

            _roundBoxPath = new GraphicsPath();

            // if corner radius is greater than zero, define path for a rounded corner rectangle
            if ( _cornerRadius > 0 )
            {
                if ( _tabbedGroupBoxText )
                {
                    _roundBoxPath.AddArc ( GetRectangle ( p1 ), 180F, 90F );
                    _roundBoxPath.AddArc ( GetRectangle ( p2 ), 270F, 90F );
                    _roundBoxPath.AddArc ( GetRectangle ( p3 ), 180F, -90F );
                }
                _roundBoxPath.AddArc ( GetRectangle ( p4 ), 270F, 90F );
                _roundBoxPath.AddArc ( GetRectangle ( p5 ), 0F, 90F );
                _roundBoxPath.AddArc ( GetRectangle ( p6 ), 90F, 90F );
                _roundBoxPath.AddArc ( GetRectangle ( p7 ), 180F, 90F );
                if ( _tabbedGroupBoxText )
                {
                    _roundBoxPath.AddArc ( GetRectangle ( p8 ), 90F, -90F );
                }
                _roundBoxPath.CloseFigure ( );
            }
            // otherwise, define path for a normal rectangle
            else
            {
                _roundBoxPath.AddRectangle ( new Rectangle ( this.Margin.Left, this.Margin.Top + ( int )( _groupNameDimensions.Height / 2 ), this.Width - 6, this.Height - 6 - ( int )( _groupNameDimensions.Height / 2 ) ) );
            }

            Pen p = new Pen(GroupBorderColor);
            p.DashStyle = GroupLineStyle;
            p.Width = GroupBorderWidth;
            if (GroupLineStyle == DashStyle.Custom)
            {
                p.DashPattern = DashPattern;
            }
            e.Graphics.DrawPath(p, _roundBoxPath);

            if ( this.Text.Trim().Length > 0 && ( _cornerRadius == 0 || !_tabbedGroupBoxText ))
            {
                Pen _ClipColour;
                if ( this.GroupFillType == FillType.Fill )
                {
                    _ClipColour = new Pen ( GroupBackColor );
                }
                else
                {
                    _ClipColour = new Pen ( GradientFrom );
                }
                GraphicsPath _ClipPath = new GraphicsPath ( );
                if ( _cornerRadius == 0 )
                {
                    _ClipPath.AddLine(_leadingTextMargin - 5, this.Margin.Top + (int)(_groupNameDimensions.Height / 2), _leadingTextMargin + _groupNameDimensions.Width + 5, this.Margin.Top + (int)(_groupNameDimensions.Height / 2));
                }
                else if ( !_tabbedGroupBoxText )
                {
                    _ClipPath.AddLine ( _leadingTextMargin - 5 + _cornerRadius , this.Margin.Top + ( int )( _groupNameDimensions.Height / 2 ) + ( int )( _cornerRadius / 2 ), _leadingTextMargin + _groupNameDimensions.Width + _cornerRadius + 5, this.Margin.Top + ( int )( _groupNameDimensions.Height / 2 ) + ( int )( _cornerRadius / 2 ) );
                }
                e.Graphics.DrawPath ( _ClipColour, _ClipPath );
            }

            switch (this.GroupFillType)
            {
                case FillType.Fill:
                    e.Graphics.FillPath(new SolidBrush(GroupBackColor), _roundBoxPath);
                    break;
                default:
                    LinearGradientBrush lgb = new LinearGradientBrush(new Rectangle(0,0,this.Width,this.Height), GradientFrom, GradientTo, GradientAngle);
                    e.Graphics.FillPath(lgb, _roundBoxPath);
                    break;
            }

            e.Graphics.DrawString ( this.Text, this.Font, new SolidBrush ( this.ForeColor ), new PointF ( _leadingTextMargin > ( _cornerRadius * 2 ) ? _leadingTextMargin + _cornerRadius : ( _cornerRadius * 2 ) + _cornerRadius, this.Margin.Top + ( _cornerRadius > 0 ? ( _cornerRadius / 2 ) : 0 ) ) );
            this.MinimumSize = new Size(Convert.ToInt32(_leadingTextMargin + _groupNameDimensions.Width +(_cornerRadius*3)), 0);
        }

        private RectangleF GetRectangle(PointF p)
        {
            return new RectangleF(p, new SizeF(_cornerRadius, _cornerRadius));
        }

        public RoundedGroupBox()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.DoubleBuffer, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.SetStyle(ControlStyles.ContainerControl, true);
            this.BackColor = Color.Transparent;
            this.Width = 175;
            this.Height = 75;
        }
    }
}
