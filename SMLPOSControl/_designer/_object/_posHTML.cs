using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing.Design;

namespace SMLPOSControl._designer._object
{
    public class _posHTML : _posObject
    {
        private string _contentUrlResult = "";
        public string _contentURL
        {
            get { return _contentUrlResult; }
            set { _contentUrlResult = value; }
        }

        private bool _scrollBarEnableResult;
        public bool _scrollBarEnable
        {
            get { return _scrollBarEnableResult; }
            set { _scrollBarEnableResult = value; }
        }

        private string _htmlSchemaResult = "";


        [Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
        public string _htmlSchema
        {
            get { return _htmlSchemaResult; }
            set { _htmlSchemaResult = value; }
        }

        #region hidden property

        [Browsable(false)]
        public new Color _backColor
        {
            get { return base._backColor; }
            set { base._backColor = value; }
        }

        [Browsable(false)]
        public new Font _font
        {
            get { return base._font; }
            set { base._font = value; }
        }

        [Browsable(false)]
        public new Color _foreColor
        {
            get { return base._foreColor; }
            set { base._foreColor = value; }
        }

        [Browsable(false)]
        public new Color _lineColor
        {
            get { return base._lineColor; }
            set { base._lineColor = value; }
        }

        [Browsable(false)]
        public new System.Drawing.Drawing2D.DashStyle _lineStyle
        {
            get { return base._lineStyle; }
            set { base._lineStyle = value; }
        }

        [Browsable(false)]
        public new Padding _padding
        {
            get { return base._padding; }
            set { base._padding = value; }
        }

        [Browsable(false)]
        public new int _penWidth
        {
            get { return base._penWidth; }
            set { base._penWidth = value; }
        }

        [Browsable(false)]
        public new ContentAlignment _textAlign
        {
            get { return base._textAlign; }
            set { base._textAlign = value; }
        }

        [Browsable(false)]
        public new string _text
        {
            get { return base._text; }
            set { base._text = value; }
        }

        #endregion

        public _posHTML()
        {
            this._lineColor = Color.Black;
            this._textAlign = ContentAlignment.TopCenter;
            this._backColor = Color.Gray;
        }


        public override void _draw(System.Drawing.Graphics g)
        {

            this._text = _contentUrlResult;
            base._draw(g);
            // draw html icon
            //g.DrawImage
            Image __htmlIconImage = SMLReport._design._drawImage._SizeModeImg(this._actualSize, global::SMLPOSControl.Properties.Resources.HTML, System.Windows.Forms.PictureBoxSizeMode.CenterImage);
            g.DrawImage(__htmlIconImage, this._actualSize);
            // draw link
            
        }
    }
}
