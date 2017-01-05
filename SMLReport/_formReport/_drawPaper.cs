using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Diagnostics;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace SMLReport._formReport
{
    public partial class _drawPaper : UserControl
    {
        private _design._pageSetup _myPageSetupResult = new _design._pageSetup();
        private float _drawScaleResult;
        public Point _topLeftPaperResult = new Point(20, 20);
        private bool _showGridResult = false;
        public ArrayList _objectForUndoAndRedo = new ArrayList();
        public int _objectForUndoAndRedoIndex = -1;
        public Boolean _backgroundShow = false;
        public Image _backgroundImage = null;
        public float _backgroundLeftMargin = 0;
        public float _backgroundTopMargin = 0;


        private object _ownerResult;

        /// <summary>
        /// get or set ฟอร์มที่ instant เข้ามา
        /// </summary>
        public object _Owner
        {
            get { return _ownerResult; }
            set
            {
                _ownerResult = value;
                _area._ownerForm = _ownerResult;
            }
        }


        public _drawPaper()
        {
            InitializeComponent();
            _drawScale = 1f;
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.UseTextForAccessibility, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.CacheText, true);
            this.Paint += new PaintEventHandler(_drawPaper_Paint);
            this._area._afterPaint += new SMLReport._design.AfterPaintFocusEventHandler(_area__afterPaint);
            this._area._mouseOverDrawPanel += new _design.MouseOverDrawPanelEventHandler(_area__mouseOverDrawPanel);
            this.MouseMove += new MouseEventHandler(_drawPaper_MouseMove);

            this.Disposed += _drawPaper_Disposed;
        }

        private void _drawPaper_Disposed(object sender, EventArgs e)
        {
            // clear resource
            this._area.Dispose();
        }

        void _area__mouseOverDrawPanel(object sender, MouseEventArgs e)
        {
            if (_afterMouseOverPanel != null)
            {
                Point _MousePoint = new Point();
                _MousePoint.X = (int)Math.Round(e.X / this._drawScale);
                _MousePoint.Y = (int)Math.Round(e.Y / this._drawScale);
                _afterMouseOverPanel(this, e, _MousePoint);
            }
        }

        void _drawPaper_MouseMove(object sender, MouseEventArgs e)
        {
            if (_afterMouseOverPanel != null)
            {
                Point _MousePoint = new Point();
                _MousePoint.X = (int)Math.Round((e.X - _area.Location.X) / this._drawScale);
                _MousePoint.Y = (int)Math.Round((e.Y - _area.Location.Y) / this._drawScale);
                _afterMouseOverPanel(this, e, _MousePoint);
            }
        }

        public bool _showGrid
        {
            get
            {
                return this._showGridResult;
            }
            set
            {
                this._showGridResult = value;
            }
        }

        public void _objectForUndoAndRedoAdd()
        {
            MemoryStream __memoryStream = new MemoryStream();
            BinaryFormatter __bin = new BinaryFormatter();
            __bin.Serialize(__memoryStream, this._area._graphicsList);
            _objectForUndoAndRedo.Add((byte[])__memoryStream.ToArray());
            _objectForUndoAndRedoIndex = _objectForUndoAndRedo.Count - 1;
        }

        public void _undo()
        {
            if (this._objectForUndoAndRedoIndex >= 0)
            {
                BinaryFormatter __bin = new BinaryFormatter();
                MemoryStream __memoryStream = new MemoryStream((byte[])this._objectForUndoAndRedo[this._objectForUndoAndRedoIndex]);
                __memoryStream.Position = 0;
                this._area._graphicsList = (SMLReport._design._graphicsList)__bin.Deserialize(__memoryStream);
                this._area.Invalidate();
                this._objectForUndoAndRedoIndex--;
            }
        }

        public void _redo()
        {
            if (this._objectForUndoAndRedoIndex >= 0 && this._objectForUndoAndRedoIndex < this._objectForUndoAndRedo.Count - 1)
            {
                BinaryFormatter __bin = new BinaryFormatter();
                MemoryStream __memoryStream = new MemoryStream((byte[])this._objectForUndoAndRedo[this._objectForUndoAndRedoIndex]);
                __memoryStream.Position = 0;
                this._area._graphicsList = (SMLReport._design._graphicsList)__bin.Deserialize(__memoryStream);
                this._area.Invalidate();
                this._objectForUndoAndRedoIndex++;
            }
        }

        void _area__afterPaint(Graphics sender)
        {
            if (this._backgroundShow && this._backgroundImage != null)
            {
                sender.DrawImage(this._backgroundImage, 0 + (this._backgroundLeftMargin * this._drawScale), 0 + (this._backgroundTopMargin * this._drawScale), this._backgroundImage.Width * this._drawScale, this._backgroundImage.Height * this._drawScale);
            }
            if (this._showGrid)
            {
                Pen __penGrid1 = new Pen(Color.WhiteSmoke, 1);
                Pen __penGrid2 = new Pen(Color.LightGray, 1);
                float __valueDiv = _design._pageSetup._convertUnitToPixel(_myPageSetup.MeasurementUnit) / this._drawScale;
                int __rowCount = (int)(Math.Round((_myPageSetup.PaperDrawHeightPixel * this._drawScale) * __valueDiv) + 1) * 2;
                int __columnCount = (int)(Math.Round((_myPageSetup.PaperDrawWidthPixel * this._drawScale) * __valueDiv) + 1) * 2;
                int __penGridStep = 0;
                for (int __row = 0; __row <= __rowCount; __row++)
                {
                    float __rowStep = (__row / __valueDiv) / 2;
                    sender.DrawLine((__penGridStep == 0) ? __penGrid2 : __penGrid1, new Point(0, (int)__rowStep), new Point(this._area.Width, (int)__rowStep));
                    if (++__penGridStep == 2)
                    {
                        __penGridStep = 0;
                    }
                }
                __penGridStep = 0;
                for (int __column = 0; __column <= __columnCount; __column++)
                {
                    float __columnStep = (__column / __valueDiv) / 2;
                    sender.DrawLine((__penGridStep == 0) ? __penGrid2 : __penGrid1, new Point((int)__columnStep, 0), new Point((int)__columnStep, this._area.Height));
                    if (++__penGridStep == 2)
                    {
                        __penGridStep = 0;
                    }
                }
            }
            Pen __penMargin = new Pen(Color.GreenYellow, 1);
            Point[] __borderMargin = {
                    new Point((int)(_myPageSetup.MarginLeftPixel * this._drawScale),(int)(_myPageSetup.MarginTopPixel* this._drawScale)),
                    new Point((int)((_myPageSetup.MarginLeftPixel+_myPageSetup.PaperDrawWidthPixel)* this._drawScale),(int)(_myPageSetup.MarginTopPixel* this._drawScale)),
                    new Point((int)((_myPageSetup.MarginLeftPixel+_myPageSetup.PaperDrawWidthPixel)* this._drawScale),(int)((_myPageSetup.MarginTopPixel+_myPageSetup.PaperDrawHeightPixel)* this._drawScale)),
                    new Point((int)(_myPageSetup.MarginLeftPixel* this._drawScale),(int)((_myPageSetup.MarginTopPixel+_myPageSetup.PaperDrawHeightPixel)* this._drawScale)),
                    new Point((int)(_myPageSetup.MarginLeftPixel* this._drawScale),(int)((_myPageSetup.MarginTopPixel)* this._drawScale))
                };
            sender.DrawLines(__penMargin, __borderMargin);
        }

        public _design._pageSetup _myPageSetup
        {
            get
            {
                return _myPageSetupResult;
            }
            set
            {
                _myPageSetupResult = value;
            }
        }

        public Point _topLeftPaper
        {
            get
            {
                Size __getPaperSizeByPixel = _myPageSetupResult.PagePixel;
                int __calcWidth = (int)(__getPaperSizeByPixel.Width * _drawScaleResult) + 2;
                int __calcHeight = (int)(__getPaperSizeByPixel.Height * _drawScaleResult) + 2;
                Point __tempPoint = new Point((this.Width / 2) - (__calcWidth / 2), (this.Height / 2) - (__calcHeight / 2));
                if (__tempPoint.Y < 20)
                {
                    __tempPoint = new Point(__tempPoint.X, 20);
                }
                if (__tempPoint.X < 20)
                {
                    __tempPoint = new Point(20, __tempPoint.Y);
                }
                _topLeftPaperResult = __tempPoint;
                return _topLeftPaperResult;
            }
            set
            {
                _topLeftPaperResult = value;
            }
        }

        public float _drawScale
        {
            get
            {
                return _drawScaleResult;
            }
            set
            {
                _drawScaleResult = value;
                _area._drawScale = _drawScaleResult;
            }
        }

        void _drawPaper_Paint(object sender, PaintEventArgs e)
        {
            Graphics __g = e.Graphics;
            Size __getPaperSizeByPixel = _myPageSetupResult.PagePixel;
            int __calcWidth = (int)(__getPaperSizeByPixel.Width * _drawScale) + 2;
            int __calcHeight = (int)(__getPaperSizeByPixel.Height * _drawScale) + 2;
            _area.Location = new Point(_topLeftPaper.X + 1, _topLeftPaper.Y + 1);
            _area.Size = new Size(__calcWidth - 2, __calcHeight - 2);
            Pen __pen = new Pen(Color.Black, 1);
            SolidBrush __brush = new SolidBrush(Color.White);
            Color __colorBegin = Color.Silver;
            Color __colorEnd = Color.DarkGray;
            int __calcGrap = (int)(5f * _drawScale);
            Rectangle __box = new Rectangle(_topLeftPaper.X, _topLeftPaper.Y, __calcWidth, __calcHeight);
            //
            LinearGradientBrush __brushRightShadow = new LinearGradientBrush(new Point(_topLeftPaper.X + __calcWidth + (__calcGrap * 2), _topLeftPaper.X + __calcGrap), new Point(_topLeftPaper.X + __calcWidth, _topLeftPaper.X + __calcGrap), __colorBegin, __colorEnd);
            LinearGradientBrush __brushBottomShadow = new LinearGradientBrush(new Point(_topLeftPaper.Y + __calcGrap, _topLeftPaper.Y + __calcHeight + (__calcGrap * 2)), new Point(_topLeftPaper.Y + __calcGrap, _topLeftPaper.Y + __calcHeight), __colorBegin, __colorEnd);
            Point[] __rightShadow = {
                new Point(_topLeftPaper.X+__calcWidth,_topLeftPaper.Y+__calcGrap),
                new Point(_topLeftPaper.X+__calcWidth,_topLeftPaper.Y+__calcHeight),
                new Point(_topLeftPaper.X+__calcWidth+(__calcGrap*2),_topLeftPaper.Y+__calcHeight + (__calcGrap * 2)),
                new Point(_topLeftPaper.X+__calcWidth+(__calcGrap*2),_topLeftPaper.Y+__calcGrap),
                new Point(_topLeftPaper.X+__calcWidth,_topLeftPaper.Y+__calcGrap)
            };
            Point[] __bottomShadow = {
                new Point(_topLeftPaper.X+__calcGrap,_topLeftPaper.Y+__calcHeight),
                new Point(_topLeftPaper.X+__calcWidth,_topLeftPaper.Y+__calcHeight),
                new Point(_topLeftPaper.X+__calcWidth+(__calcGrap*2),_topLeftPaper.Y+__calcHeight+(__calcGrap*2)),
                new Point(_topLeftPaper.X+__calcGrap,_topLeftPaper.Y+__calcHeight+(__calcGrap*2)),
                new Point(_topLeftPaper.X+__calcGrap,_topLeftPaper.Y+__calcHeight)
            };
            __g.FillPolygon(__brushRightShadow, __rightShadow);
            __g.FillPolygon(__brushBottomShadow, __bottomShadow);
            __g.FillRectangle(__brush, __box);
            __g.DrawRectangle(__pen, __box);
        }

        public event MouseOverPanelEventHandler _afterMouseOverPanel;

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Delete:
                    _objectDelete();
                    return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        public event AfterRemoveDrawObjectEventHandler _afterRemoveObject;

        public void _objectDelete()
        {
            this._objectForUndoAndRedoAdd();
            this._area._graphicsList._deleteSelection();
            this._area._drawNetRectangle = false;
            this._area.Invalidate();

            if (_afterRemoveObject != null)
            {
                _afterRemoveObject(this, null);
            }
        }



    }
    public delegate void MouseOverPanelEventHandler(object sender, MouseEventArgs e, Point drawPanelPoint);
    public delegate void AfterRemoveDrawObjectEventHandler(object sender, MouseEventArgs e);
}
