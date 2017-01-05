using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System;
using System.ComponentModel;

namespace SMLPOSControl._designer._object
{
    public class _number : SMLReport._design._drawLabel
    {
        private MyLib.VistaButton _button = new MyLib.VistaButton();

        public _number()
        {
            this._beforeDraw += new BeforeDrawEventHandler(_number__beforeDraw);
        }

        void _number__beforeDraw(Graphics g, PointF __startPoint)
        {
        }

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
        

    }
}
