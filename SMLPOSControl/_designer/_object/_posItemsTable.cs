using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Collections;
using System.Windows.Forms;
using System.Drawing.Design;
using System.Drawing.Drawing2D;

namespace SMLPOSControl._designer._object
{
    public enum _posItemsFieldEnum
    {
        ItemCode,
        ItemName,
        Qty,
        UnitPrice,
        Total
    }

    public class _posItemsTable : _posObject
    {
        private int _cellHeightResult = 21;
        private bool DisplayRowNumberResult = true;
        private bool _total_showResult = false;
        public int _displayStartY = 41;
        int[] _columnLine = new int[5000];
        private bool ColumnBackgroundAuto = false;

        /// <summary>สีพื้น Row เลขคี่</summary>
        public Color _RowBackground1 = Color.Snow;
        /// <summary>สีพื้น Row เลขคู่</summary>
        public Color _RowBackground2 = Color.White;
        private Color _columnBackgroundBeginAuto = Color.FromArgb(250, 251, 252);
        private Color _columnBackgroundEndAuto = Color.FromArgb(214, 217, 227);
        /// <summary>สีของ Column (ด้านบน,ด้านข้างซ้าย)</summary>
        public Color _columnBackground = Color.FromArgb(250, 251, 252);
        /// <summary>สีของ Column (ด้านบน,ด้านข้างซ้าย)</summary>
        public Color _columnBackgroundEnd = Color.FromArgb(214, 217, 227);



        private int _getTopY = 0;

        private Font _fontResult = MyLib._myGlobal._myFontFormDesigner;
        private Font _fontHeaderResult = MyLib._myGlobal._myFontFormDesigner;
        private _posItemsTableColumnsCollection _columnsResult = new _posItemsTableColumnsCollection();

        [Category("_SML")]
        [Description("รูปแบบตัวอักษร")]
        [DisplayName("Font : รูปแบบตัวอักษร")]
        public Font _font
        {
            get
            {
                return _fontResult;
            }
            set
            {
                _fontResult = value;
                // calc height 
                SizeF __measureText = this._getTextSize("MeaSureSize", this._fontResult);
                _cellHeightResult = (int)__measureText.Height + 6;
                // redraw
            }
        }

        /// <summary>ความสูงของแถว</summary>
        public int _cellHeight
        {
            get { return _cellHeightResult; }
            set { _cellHeightResult = value; }
        }

        [Category("Header")]
        [Description("กำหนด รูปแบบตัวอักษรของหัวตาราง")]
        [DisplayName("Header Font : รูปแบบตัวอักษรหัวตาราง")]
        public Font _headerFont
        {
            get
            {
                return _fontHeaderResult;
            }
            set
            {
                _fontHeaderResult = value;
            }
        }

        [TypeConverter(typeof(_posItemsTableColumnsCollectionConverter))]
        [Editor(typeof(_posItemsTablecolumnEditor), typeof(UITypeEditor))]
        public _posItemsTableColumnsCollection _ItemsColumns
        {
            get { return _columnsResult; }
            set { _columnsResult = value; }
        }

        public _posItemsTable()
        {

        }

        private void _calColumnWidthPoint()
        {
            float __sum = 0;
            foreach (_posItemsTableColumn __getColumn in _ItemsColumns)
            {
                if (__getColumn._isHide == false)
                {
                    __sum += __getColumn._columnWidth;
                }
            }

            for (int __i = 0; __i < _ItemsColumns.Count; __i++) 
            {
                _posItemsTableColumn __getColumn = _ItemsColumns[__i];
                float __width = this._width - ((this.DisplayRowNumberResult) ? 40 : 0 ) ;

                //int __a = (int)(__sum / __getColumn._columnWidth);
                __getColumn._colWidthPoint = (__width / __sum) * __getColumn._columnWidth;
                __getColumn._colWidtPercentRatio = (100 / __sum) * __getColumn._columnWidth;

            }
        }

        public override void _draw(Graphics g)
        {
            //base._draw(g);
            // ดู draw จาก myGrid

            // cal HeadGrid height

            // cal Detail Row height
            //base.OnPaint(e);
            _calColumnWidthPoint();
            float _startXPos = this._actualSize.X;
            float _startYPos = this._actualSize.Y;

            try
            {
                int __topPixel = (int)_cellHeightResult;
                //Graphics __g = pe.Graphics;
                Pen __myPen1 = new Pen(Color.Gainsboro, 0);
                Pen __myPen2 = new Pen(Color.White, 0);
                //int __rowNumber = _rowFirst;
                int __rowNumber = 0;
                int __rowEnd = (int)(this._height / _cellHeightResult);

                //_rowPerPage = __rowEnd;
                for (int __row = 0; __row < __rowEnd; __row++)
                {
                    float __calcPoint = (__row * _cellHeightResult) + __topPixel;
                    Point[] boxPoints = {
                        new Point((int)_startXPos,(int)__calcPoint + (int)_startYPos),
                        new Point((int)_startXPos+ this._width,(int)__calcPoint+ (int)_startYPos),
                        new Point((int)_startXPos +this._width,(int)(__calcPoint+_cellHeightResult)+ (int)_startYPos),
                        new Point((int)_startXPos,(int)(__calcPoint+_cellHeightResult)+ (int)_startYPos)
                    };
                    GraphicsPath __background = new GraphicsPath();
                    __background.AddPolygon(boxPoints);
                    // Define fill mode.
                    FillMode __newFillMode = FillMode.Winding;
                    LinearGradientBrush __blueBrush;
                    Color __lastBackGroundColor;
                    if ((__rowNumber % 2) == 0)
                    {
                        __blueBrush = new LinearGradientBrush(new Point((int)_startXPos, (int)_startYPos), new Point((int)_startXPos, (int)_startYPos + (int)_cellHeightResult), _RowBackground1, _RowBackground1);
                        __lastBackGroundColor = _RowBackground1;
                    }
                    else
                    {
                        __blueBrush = new LinearGradientBrush(new Point((int)_startXPos, (int)_startYPos), new Point((int)_startXPos, (int)_startYPos + (int)_cellHeightResult), _RowBackground2, _RowBackground2);
                        __lastBackGroundColor = _RowBackground2;
                    }
                    //
                    //if (_selectRowFromMouse == __row)
                    //{
                    //    __blueBrush = new LinearGradientBrush(new Point(0, 0), new Point(0, (int)_cellHeightResult), Color.AliceBlue, Color.AliceBlue);
                    //    __lastBackGroundColor = Color.AliceBlue;
                    //}
                    //else
                    //{
                        if ((__rowNumber % 2) == 0)
                        {
                            __blueBrush = new LinearGradientBrush(new Point((int)_startXPos, (int)_startYPos), new Point((int)_startXPos, (int)_cellHeightResult + (int)_startYPos), _RowBackground1, _RowBackground1);
                            __lastBackGroundColor = _RowBackground1;
                        }
                        else
                        {
                            __blueBrush = new LinearGradientBrush(new Point((int)_startXPos, (int)_startYPos), new Point((int)_startXPos, (int)_cellHeightResult + (int)_startYPos), _RowBackground2, _RowBackground2);
                            __lastBackGroundColor = _RowBackground2;
                        }
                    //}
                    if (this._total_showResult && __row == __rowEnd - 1)
                    {
                        __blueBrush = new LinearGradientBrush(new Point((int)_startXPos, (int)_startYPos), new Point((int)_startXPos, (int)_cellHeightResult + (int)_startYPos), Color.LightYellow, Color.LightYellow);
                        __lastBackGroundColor = Color.LightYellow;
                    }

                    // Draw polygon to screen. // unknow fill
                    g.FillPolygon(__blueBrush, boxPoints, __newFillMode);
                    // Background
                    g.DrawPath(__myPen2, __background);

                    // Row Number Background Point
                    Point[] __rowBoxPoints = {
                        new Point((int)_startXPos,(int)__calcPoint+ (int)_startYPos),
                        new Point((int)_startXPos+_displayStartY-1,(int)__calcPoint+ (int)_startYPos),
                        new Point((int)_startXPos+_displayStartY-1,(int)(__calcPoint+_cellHeightResult)+ (int)_startYPos),
                        new Point((int)_startXPos,(int)(__calcPoint+_cellHeightResult)+ (int)_startYPos)
                    };
                    GraphicsPath __background2 = new GraphicsPath();
                    __background2.AddPolygon(__rowBoxPoints);
                    // Define fill mode.
                    FillMode __newFillMode2 = FillMode.Winding;
                    LinearGradientBrush __blueBrush2 = new LinearGradientBrush(new Point((int)_startXPos, (int)_startYPos), new Point((int)_startXPos, (int)_cellHeightResult + (int)_startYPos), (this.ColumnBackgroundAuto) ? _columnBackgroundBeginAuto : _columnBackground, (this.ColumnBackgroundAuto) ? _columnBackgroundEndAuto : _columnBackgroundEnd);

                    // Draw polygon to screen.
                    if (DisplayRowNumberResult)
                    {
                        // fill Line Number Background // fill right

                        g.FillPolygon(__blueBrush2, __rowBoxPoints, __newFillMode2);
                        g.DrawPath(__myPen2, __background2);
                    }
                    string __strGet = (__rowNumber + 1).ToString();
                    if (this._total_showResult && __row == __rowEnd - 1)
                    {
                        __strGet = "Total";
                        //if (this._columnTopActive)
                        //{
                        //    __row++;
                        //}
                    }
                    SizeF __stringSize = g.MeasureString(__strGet, this._font);
                    if (DisplayRowNumberResult)
                    {
                        g.DrawString(__strGet, this._font, new SolidBrush(Color.Black), _startXPos + 20 - (__stringSize.Width / 2), __calcPoint + 2 + (int)_startYPos);
                    }

                    /*
                    if (this._total_show && __row == __rowEnd - 1)
                    {
                        _displayTotal(__row, __g);
                    }
                    else
                    {
                        _displayRowData(__row, __rowNumber, __g);
                    }
                     * */

                    //
                    __rowNumber++;
                    __background.Dispose();
                    __blueBrush2.Dispose();
                    __blueBrush.Dispose();
                } // for row

                float __currentX = this._actualSize.X;

                //_columnType __getColumn = new _columnType();
                _posItemsTableColumn __getColumn = new _posItemsTableColumn();
                g.DrawLine(__myPen1, _startXPos, 21 + (int)_startYPos, _startXPos, this._height + (int)_startYPos);
                int _columnLineMax = 0;
                Color __fontColor = Color.Black;

                int _columnFirst = 0;

                // Show Column Name
                for (int __column = _columnFirst; __column <= _ItemsColumns.Count; __column++)
                {
                    float __columnWidthPoint;
                    string __columnName;
                    if (__column == _columnFirst)
                    {
                        if (DisplayRowNumberResult)
                        {
                            __columnName = "Line";
                            __columnWidthPoint = 40;
                        }
                        else
                        {
                            __columnName = "";
                            __columnWidthPoint = 1;
                        }
                    }
                    else
                    {
                        __getColumn = (_posItemsTableColumn)_ItemsColumns[__column - 1];
                        __columnWidthPoint = __getColumn._colWidthPoint;
                        __columnName = (__getColumn._Header.Length == 0) ? __getColumn._Tag : __getColumn._Header;


                        __fontColor = Color.Black; // header Font Color
                    }
                    Point[] __curvePoints = { 
                        new Point((int)__currentX,this._getTopY+(int)_cellHeightResult+ (int)_startYPos),
                        new Point((int)__currentX,this._getTopY+2+ (int)_startYPos),
                        new Point((int)__currentX+2,this._getTopY+0+ (int)_startYPos),
                        new Point((int)(__currentX+__columnWidthPoint-2),this._getTopY+0+ (int)_startYPos),
                        new Point((int)(__currentX+__columnWidthPoint),this._getTopY+2+ (int)_startYPos),
                        new Point((int)(__currentX+__columnWidthPoint),(int)(this._getTopY+_cellHeightResult)+ (int)_startYPos) };
                    GraphicsPath __background = new GraphicsPath();
                    __background.AddPolygon(__curvePoints);
                    // Define fill mode.
                    LinearGradientBrush __blueBrush = new LinearGradientBrush(new Point((int)_startXPos, (int)_startYPos), new Point((int)_startXPos, (int)_cellHeightResult + (int)_startYPos), (this.ColumnBackgroundAuto) ? _columnBackgroundBeginAuto : _columnBackground, (this.ColumnBackgroundAuto) ? _columnBackgroundEndAuto : _columnBackgroundEnd);

                    // Draw polygon to screen.
                    g.FillPolygon(__blueBrush, __curvePoints, FillMode.Winding);
                    g.DrawPath(__myPen1, __background);
                    string __strGet = __columnName;
                    SizeF __stringSize = g.MeasureString(__strGet, this._font);
                    float __calcFirstPoint = __currentX + (__columnWidthPoint / 2) - (__stringSize.Width / 2);
                    if (__calcFirstPoint <= __currentX)
                    {
                        __calcFirstPoint = __currentX + 2;
                    }
                    if (__getColumn._isHide == false)
                    {
                        // เขียนชื่อ Column
                        g.DrawString(__strGet, this._font, new SolidBrush(__fontColor), __calcFirstPoint, this._getTopY + 2 + (int)_startYPos);
                    }
                    __currentX += __columnWidthPoint;
                    // ขีดเส้นจากบนลงล่าง
                    g.DrawLine(__myPen1, __currentX, this._cellHeightResult + (int)_startYPos, __currentX, this._height + (int)_startYPos);
                    _columnLine[_columnLineMax++] = (int)__currentX;
                    __background.Dispose();
                    __blueBrush.Dispose();
                } //for column


                __myPen1.Dispose();
                __myPen2.Dispose();
                //this._drawing = false;
            }
            catch (Exception __ex)
            {
                MessageBox.Show(__ex.Message.ToString() + " " + __ex.StackTrace.ToString());
            }

        }

    }

    #region POS Items Table Column

    public class _posItemsTableColumn
    {
        private float _columnWidthResult = 0f;
        private string _tagResult = "";
        private string _headerResult = "";
        private ContentAlignment _textAlignment = ContentAlignment.MiddleLeft;
        private bool _isHideResult = false;
        private _tableColumnType _columnTypeResult;
        private string _columnFormatResult = "";

        public float _colWidthPoint = 0;
        public float _colWidtPercentRatio = 0f;
        
        public float _columnWidth
        {
            get { return _columnWidthResult; }
            set { _columnWidthResult = value; }
        }

        public string _Tag
        {
            get { return _tagResult; }
            set { _tagResult = value; }
        }

        public string _Header
        {
            get { return _headerResult; }
            set { _headerResult = value; }
        }

        public ContentAlignment TextAlignment
        {
            get { return _textAlignment; }
            set { _textAlignment = value; }
        }

        public bool _isHide
        {
            get { return _isHideResult; }
            set { _isHideResult = value; }
        }

        public override string ToString()
        {
            if (!_headerResult.Equals(""))
            {
                return _headerResult;
            }
            return this._tagResult.Replace("&", string.Empty);
        }

        public _tableColumnType _columnType
        {
            get { return _columnTypeResult; }
            set { _columnTypeResult = value; }
        }

        public string _columnFormat
        {
            get { return _columnFormatResult; }
            set { _columnFormatResult = value; }
        }

        public _posItemsTableColumn()
        {

        }

        public _posItemsTableColumn(_posItemsFieldEnum __ColumnType, string __header, float __width )
        {
            this._Tag = __ColumnType.ToString();
            this._Header = __header;
            this._columnWidth = __width;
        }
    }

    #endregion

    #region POS Items Table Columns Collection

    public class _posItemsTableColumnsCollection : CollectionBase, ICustomTypeDescriptor, ICloneable
    {
        public _posItemsTableColumn this[int index]
        {
            get
            {
                if (this.List.Count > index)
                {
                    return (_posItemsTableColumn)this.List[index];
                }
                return null;
            }
        }

        public void Add(_posItemsTableColumn __col)
        {
            this.List.Add(__col);
        }

        #region Imprement ICloneable

        public object Clone()
        {
            return this.MemberwiseClone();
        }

        #endregion

        #region Imprement ICustomtypeDescriptior

        public AttributeCollection GetAttributes()
        {
            return TypeDescriptor.GetAttributes(this, true);
        }

        public string GetClassName()
        {
            return TypeDescriptor.GetClassName(true, true);
        }

        public string GetComponentName()
        {
            return TypeDescriptor.GetComponentName(this, true);
        }

        public TypeConverter GetConverter()
        {
            return TypeDescriptor.GetConverter(this, true);
        }

        public EventDescriptor GetDefaultEvent()
        {
            return TypeDescriptor.GetDefaultEvent(this, true);
        }

        public PropertyDescriptor GetDefaultProperty()
        {
            return TypeDescriptor.GetDefaultProperty(this, true);
        }

        public object GetEditor(Type editorBaseType)
        {
            return TypeDescriptor.GetEditor(this, editorBaseType, true);
        }

        public EventDescriptorCollection GetEvents(Attribute[] attributes)
        {
            return TypeDescriptor.GetEvents(this, attributes, true);
        }

        public EventDescriptorCollection GetEvents()
        {
            return TypeDescriptor.GetEvents(this, true);
        }

        public PropertyDescriptorCollection GetProperties(Attribute[] attributes)
        {
            return GetProperties();
        }

        public PropertyDescriptorCollection GetProperties()
        {
            PropertyDescriptorCollection pds = new PropertyDescriptorCollection(null);
            for (int __i = 0; __i < this.List.Count; __i++)
            {
                if (this != null)
                {
                    _posItemsTableColumnsCollectionPropertyDescriptor __pd = new _posItemsTableColumnsCollectionPropertyDescriptor(this, __i);
                    pds.Add(__pd);
                }
            }

            return pds;
        }

        public object GetPropertyOwner(PropertyDescriptor pd)
        {
            return this;
        }

        #endregion

    }

    #endregion

    #region POS Items Table Expand Object

    internal class _posItemsTableColumnsCollectionConverter : ExpandableObjectConverter
    {
        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string) && value is _posItemsTableColumnsCollection)
            {
                /*
                if (context.Instance.GetType() == typeof(_posItemsTable))
                {
                    _posItemsTable __activeTool = (_posItemsTable)context.Instance;

                    // set default field
                    //string[] __standardValue = __activeTool._getFieldStandardValue(__activeTool);

                    // set default image list
                    //string[] __imageNameList = __activeTool._getNameInImageList(__activeTool);

                    _posItemsTableColumnsCollection __collection = (_posItemsTableColumnsCollection)value;
                    for (int __i = 0; __i < __collection.Count; __i++)
                    {
                        ((_posItemsTableColumn)__collection[__i]).__defaultValueTmp = __standardValue;
                        ((_posItemsTableColumn)__collection[__i])._nameImageListResultTmp = __imageNameList;
                    }

                }
                */

                return "(Collection)";
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }

    internal class _posItemsTableColumnsConverter : ExpandableObjectConverter
    {
        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string) && value is _posItemsTableColumn)
            {
                return value.ToString();
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }

    #endregion

    #region POS Items Table Column Collection Editolr

    internal class _posItemsTablecolumnEditor : CollectionEditor
    {
        private ComboBox __drawFieldCombo = new ComboBox();

        public _posItemsTablecolumnEditor(Type type)
            : base(type)
        {

        }

        /// <summary>
        /// get Collection Form Can Add Custom Object Here!
        /// </summary>
        /// <returns></returns>
        protected override CollectionEditor.CollectionForm CreateCollectionForm()
        {
            __drawFieldCombo = new ComboBox();
            __drawFieldCombo.Name = "comboDrawType";
            __drawFieldCombo.Items.Add(_posItemsFieldEnum.ItemCode.ToString());
            __drawFieldCombo.Items.Add(_posItemsFieldEnum.ItemName.ToString());
            __drawFieldCombo.Items.Add(_posItemsFieldEnum.Qty.ToString());
            __drawFieldCombo.Items.Add(_posItemsFieldEnum.UnitPrice.ToString());
            __drawFieldCombo.Items.Add(_posItemsFieldEnum.Total.ToString());
            __drawFieldCombo.SelectedIndex = 0;

            CollectionForm __collectionForm = base.CreateCollectionForm();

            Form frmCollectionEditorForm = (Form)__collectionForm;

            Control __tmpControl = frmCollectionEditorForm.Controls[0];
            if (__tmpControl.GetType() == typeof(TableLayoutPanel))
            {
                TableLayoutPanel __mainLayout = (TableLayoutPanel)frmCollectionEditorForm.Controls[0];


                __mainLayout.SetColumnSpan(__tmpControl.Controls[6], 1);
                __mainLayout.SetColumn(__tmpControl.Controls[6], 2);

                __drawFieldCombo.Location = new Point(0, __mainLayout.ClientRectangle.Bottom - 35);
                __drawFieldCombo.Anchor = AnchorStyles.Left;

                __mainLayout.Controls.Add(__drawFieldCombo);
                __mainLayout.SetRow(__drawFieldCombo, 4);
                __mainLayout.SetColumn(__drawFieldCombo, 0);

            }

            // set Caption Of Form
            frmCollectionEditorForm.Text = "Items Table Collection Editor";

            return (CollectionEditor.CollectionForm)frmCollectionEditorForm;
        }

        /// <summary>
        /// On Click Add Object
        /// </summary>
        /// <param name="itemType"></param>
        /// <returns></returns>
        protected override object CreateInstance(Type itemType)
        {
            _posItemsTableColumn __col = new _posItemsTableColumn();

            switch (__drawFieldCombo.SelectedIndex)
            {
                case (int)_posItemsFieldEnum.ItemCode:
                    __col._Tag = _posItemsFieldEnum.ItemCode.ToString();
                    break;
                case (int)_posItemsFieldEnum.ItemName:
                    __col._Tag = _posItemsFieldEnum.ItemName.ToString();
                    break;
                case (int)_posItemsFieldEnum.Qty:
                    __col._Tag = _posItemsFieldEnum.Qty.ToString();
                    break;
                case (int)_posItemsFieldEnum.UnitPrice:
                    __col._Tag = _posItemsFieldEnum.UnitPrice.ToString();
                    break;
                case (int)_posItemsFieldEnum.Total:
                    __col._Tag = _posItemsFieldEnum.Total.ToString();
                    break;
            }
            return __col;
        }

    }

    #endregion

    #region POS Items Table Column PropertyDescriptor

    public class _posItemsTableColumnsCollectionPropertyDescriptor : PropertyDescriptor
    {
        private _posItemsTableColumnsCollection _collection = null;
        private int _index = -1;

        public _posItemsTableColumnsCollectionPropertyDescriptor(_posItemsTableColumnsCollection __ColumnCollection, int __idx)
            : base("#" + __idx.ToString(), null)
        {
            _collection = __ColumnCollection;
            _index = __idx;
        }

        public override string Name
        {
            get
            {
                return "#" + _index.ToString();
            }
        }

        public override AttributeCollection Attributes
        {
            get
            {
                return new AttributeCollection(null);
            }
        }

        public override string DisplayName
        {
            get
            {
                //_tableColumns __columns = this._collection[_index];
                //if (__columns == null)
                //{
                //    return "";
                //}
                //return __columns.HeaderText;
                return "[" + _index + "]";
            }
        }

        public override string Description
        {
            get
            {
                _posItemsTableColumn __columns = this._collection[_index];
                if (__columns == null)
                {
                    return "";
                }
                StringBuilder __desc = new StringBuilder();
                __desc.Append("Custom Description");
                return __desc.ToString();
            }
        }

        public override bool CanResetValue(object component)
        {
            return true;
        }

        public override Type ComponentType
        {
            get
            {
                return _collection.GetType();
            }
        }

        public override object GetValue(object component)
        {
            return _collection[_index];
        }

        public override bool IsReadOnly
        {
            get { return false; }
        }

        public override Type PropertyType
        {
            get
            {
                if (_collection[_index] == null)
                {
                    return base.GetType();
                }
                return _collection[_index].GetType();
            }
        }

        public override void ResetValue(object component)
        {
            //throw new NotImplementedException();
        }

        public override void SetValue(object component, object value)
        {
            //throw new NotImplementedException();
        }

        public override bool ShouldSerializeValue(object component)
        {
            return true;
        }
    }

    #endregion

    public enum _tableColumnType
    {
        String,
        Number,
        Date
    }


}
