using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace SMLPOSControl._designer._object
{
    public class _posObject : SMLReport._design._drawLabel
    {
        private string _tagResult = "";
        public string _Tag
        {
            get { return _tagResult; }
            set { _tagResult = value; }
        }

        private string _objectIdResult = "";
        public string _Id
        {
            get { return _objectIdResult; }
            set { _objectIdResult = value; }
        }
        

        // override some method

        // override some property

        #region remove Property

        [Browsable(false)]
        public new string PringPage
        {
            get
            {
                return base.PringPage;
            }
            set
            {
                base.PringPage = value;
            }
        }

        [Browsable(false)]
        public new float _FontCharWidth
        {
            get
            {
                return base._FontCharWidth;
            }
        }

        [Browsable(false)]
        public new float _charWidth
        {
            get
            {
                return base._charWidth;
            }
            set
            {
                base._charWidth = value;
            }
        }

        [Browsable(false)]
        public new bool _allowLineBreak
        {
            get
            {
                return base._allowLineBreak;
            }
            set
            {
                base._allowLineBreak = value;
            }
        }

        [Browsable(false)]
        public new float _charSpace
        {
            get
            {
                return base._charSpace;
            }
            set
            {
                base._charSpace = value;
            }
        }

        [Browsable(false)]
        public new bool _autoSize
        {
            get
            {
                return base._autoSize;
            }
            set
            {
                base._autoSize = value;
            }
        }

        #endregion

        public _posObject()
        {
            this._beforeDraw += new BeforeDrawEventHandler(_posObject__beforeDraw);
        }

        void _posObject__beforeDraw(System.Drawing.Graphics g, System.Drawing.PointF __startPoint)
        {
            
        }
    }
}
