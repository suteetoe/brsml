using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using Crom.Controls.Docking;
using System.Xml.Serialization;
using System.Windows.Forms;
using System.Drawing;

namespace SMLPOSControl._designer
{
    [Serializable]
    public class _posDesignXML
    {
        /// <summary>ชื่อของ POS Screen</summary>
        public string _posName;
        public string _guid;
        [XmlArrayItem("_forms", typeof(_posFormXML))]
        public ArrayList _form = new ArrayList();
    }

    public class _posFormXML
    {
        /// <summary>Title Form</summary>
        public string _text;
        public DockStyle _dock;
        public zDockMode _dockMode;

        public int _width;
        public int _height;

        public string _backImage;
        public ImageLayout _backImageLayout;
        public string _backColor;

        [XmlArrayItem("_drawObjects", typeof(_posDrawObjectXML))]
        public ArrayList _drawObject = new ArrayList();
    }

    /// <summary>XML ของ POS Manager Tools</summary>
    public class _posDrawObjectXML : SMLReport._formReport._xmlDrawObjectClass
    {
        /// <summary>ประเภท Control</summary>
        public _posControls _controlType;

        /// <summary>สีที่ 1 Panel = startColor, Button = highlight Color</summary>
        public string _color1;
        /// <summary>สีที่ 2 Panel = endColor, Button = BaseColor</summary>
        public string _color2;
        /// <summary>สีที่ 3 Button = GrowColor</summary>
        public string _color3;
        /// <summary>สีที่ 4 item Panel = _buttonBackColor</summary>
        public string _color4;
        /// <summary>ระยะเงาด้าน X</summary>
        public float _xOffset;
        /// <summary>ระยะเงาด้าน Y</summary>
        public float _yOffset;
        /// <summary>ตำแหน่งของการวาง Icon บนปุ่ม</summary>
        public ContentAlignment _imageIconAlignment = ContentAlignment.MiddleLeft;
        /// <summary>ขนาดของ Icon</summary>
        public Size _imageSize;
        /// <summary>ภาพพิ้นหลัง</summary>
        public string _backImage;
        /// <summary>Tag สำหรับระบุประเภท</summary>
        public string _tag;

        /// <summary>ID object</summary>
        public string _id;

        /// <summary>POS Columns</summary>
        [XmlArrayItem("_posTableColumns", typeof(_posItemsTableColumnXML))]
        public ArrayList _posTableColumn = new ArrayList();

        // item panel
        public int _buttonWidth;
        public int _buttonHeight;
        public Padding _buttonMargin = new Padding();

        public bool _scrollBarEnable;
        public bool _drawShadow;

    }

    public class _posItemsTableColumnXML : SMLReport._formReport._XmlTableColumns
    {
        public string _tag;
        public _object._tableColumnType _columnType;
    }
}
