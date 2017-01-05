using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using System.Globalization;

namespace SMLReport._formReport
{
    public partial class _formDesigner : UserControl
    {
        #region Private Field

        private float _drawScale = 1f;
        private ArrayList _paperListName = new ArrayList();
        private int _paperNumber = 0;
        private _drawPaper _paper;
        private ArrayList _objectForCutAndCopy = new ArrayList();
        private _selectBackgroundForm _selectBackground;

        private string _GUID;
        private string _formCodeProperty;
        private string _formNameProperty;
        private string _lastUpdateProperty;
        private bool _stateSave = false;
        private _imageList _imageList = new _imageList();

        // for warning save form
        private bool _dirty = false;

        #endregion

        #region Public Field

        /// <summary>หน้าทั้งหมด</summary>
        public ArrayList _paperList = new ArrayList();
        public FormQuerys _query = new FormQuerys();
        public _queryForm __queryEdit = new _queryForm();

        #endregion

        #region Form Property

        public string FormName
        {
            get
            {
                return _formNameProperty;
            }
            set
            {
                _formNameProperty = value;
            }
        }

        public string FormCode
        {
            get
            {
                return _formCodeProperty;
            }
            set
            {
                _formCodeProperty = value;
            }
        }

        public string LastUpdate
        {
            get
            {
                return _lastUpdateProperty;
            }
        }

        public int PageCount
        {
            get
            {
                return _paperList.Count;
            }
        }

        #endregion

        public _formDesigner()
        {
            InitializeComponent();
            if (MyLib._myGlobal._isDesignMode == false)
            {
                this._toolStripFormManage.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
                this._toolStripFont.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
                this._toolStripPage.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
                this._toolStripObject.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
                this._toolStripControl.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            }
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.UseTextForAccessibility, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.CacheText, true);

            _paperAddNewPage(0);
            _vScrollBar.Visible = false;
            _hScrollBar.Visible = false;
            //_design._reportPanel _reportPanel = new _design._reportPanel();
            this.Resize += new EventHandler(_designReport_Resize);
            this.splitContainer1.Panel1.Resize += new EventHandler(Panel1_Resize);
            _myPropertyGrid.PropertyValueChanged += new PropertyValueChangedEventHandler(_myPropertyGrid_PropertyValueChanged);

            _toolStripObject.ItemClicked += new ToolStripItemClickedEventHandler(_toolBar1_ItemClicked);
            //
            _scaleComboBox.SelectedIndex = 3;
            _scaleComboBox.SelectedIndexChanged += new EventHandler(_scaleComboBox_SelectedIndexChanged);
            //
            _vScrollBar.ValueChanged += new EventHandler(_vScrollBar_ValueChanged);
            _hScrollBar.ValueChanged += new EventHandler(_hScrollBar_ValueChanged);
            //
            this.MouseWheel += new MouseEventHandler(_designReport_MouseWheel);
            //
            this._pageNameComboBox.SelectedIndexChanged += new EventHandler(_pageNameComboBox_SelectedIndexChanged);
            //
            this._fontNameComboBox.SelectedIndexChanged += new EventHandler(_fontNameComboBox_SelectedIndexChanged);
            this._fontSizeComboBox.SelectedIndexChanged += new EventHandler(_fontSizeComboBox_SelectedIndexChanged);

            this._statusStripLocationLabel.Text = "";


            this.__queryEdit._afterCloseQueryForm += new AfterCloseQueryForm(__queryEdit__afterCloseQueryForm);

            // status bar
            _formGuidStatusText.Text = "";
            _formCodeStatusText.Text = "";
            _formNameStatusText.Text = "";

            _imageList = new _imageList();

            if (MyLib._myGlobal._isUserTest == true || MyLib._myGlobal._isUserSupport == true)
            {
                _saveFromToServerButton.Visible = true;
            }

            this._formTypeCombobox.Items.Clear();
            this._formTypeCombobox.Items.Add(MyLib._myResource._findResource(_g.d.formdesign._table + "." + _g.d.formdesign._form_all)._str);
            this._formTypeCombobox.Items.Add(MyLib._myResource._findResource(_g.d.formdesign._table + "." + _g.d.formdesign._form_print)._str);
            this._formTypeCombobox.Items.Add(MyLib._myResource._findResource(_g.d.formdesign._table + "." + _g.d.formdesign._form_barcode)._str);
            this._formTypeCombobox.Items.Add(MyLib._myResource._findResource(_g.d.formdesign._table + "." + _g.d.formdesign._form_transportlabel)._str);

            this.Disposed += _formDesigner_Disposed;
        }

        void _formDesigner_Disposed(object sender, EventArgs e)
        {
            for (int __pageSave = this._paperList.Count - 1; __pageSave >= 0; __pageSave--)
            {
                _drawPaper __areaSave = (_drawPaper)this._paperList[__pageSave];
                __areaSave.Dispose();
            }
        }

        #region Private Method

        void _area__afterClickLostFocus(object sender)
        {
            _toolBarClearSelect();
        }

        void _area__afterClick(object sender, object[] senderObject)
        {
            _myPropertyGrid.SelectedObjects = senderObject;
            _toolBarClearSelect();
        }

        void _paper__afterClick(object[] sender)
        {
            _myPropertyGrid.SelectedObjects = sender;
            _toolBarClearSelect();
        }

        void __queryEdit__afterCloseQueryForm(object sender)
        {
            // update class utility
            _queryForm __queryForm = (_queryForm)sender;
            _query = __queryForm._getQueryFormDesign();
            //_reportUtility._queryRule = __query.QueryLists;
        }

        void _paper__afterMouseOverPanel(object sender, MouseEventArgs e, Point panelPoint)
        {
            this._statusStripLocationLabel.Text = " X : " + panelPoint.X + ", Y : " + panelPoint.Y;
        }

        /// <summary>
        /// หาค่า field จาก query rule ให้เลือก
        /// </summary>
        /// <param name="sender"></param>
        void _graphicsList__getDefaultFieldList(SMLReport._design._drawObject sender)
        {
            this._onGetFieldValue(sender);
        }

        void _graphicsList__getImageListNameConv(_design._drawObject sender)
        {
            if (sender.GetType() == typeof(SMLReport._design._drawImageField))
            {
                SMLReport._design._drawImageField __imageField = (SMLReport._design._drawImageField)sender;

                if (_imageList._collection.Count > 0)
                {
                    string[] __listImageName = new string[_imageList._collection.Count];
                    for (int __i = 0; __i < _imageList._collection.Count; __i++)
                    {
                        if (_imageList._collection[__i]._keyName != null)
                        {
                            __listImageName[__i] = _imageList._collection[__i]._keyName.ToString();
                        }
                    }

                    __imageField._nameImageList = __listImageName;
                }
                __imageField.__imageList = _imageList;
            }

            if (sender.GetType() == typeof(SMLReport._design._drawTable))
            {
                SMLReport._design._drawTable __table = (SMLReport._design._drawTable)sender;

                if (_imageList._collection.Count > 0)
                {
                    string[] __listImageName = new string[_imageList._collection.Count];
                    for (int __i = 0; __i < _imageList._collection.Count; __i++)
                    {
                        if (_imageList._collection[__i]._keyName != null)
                        {
                            __listImageName[__i] = _imageList._collection[__i]._keyName.ToString();
                        }
                    }

                    __table._nameImageList = __listImageName;
                }
                __table.__imageList = _imageList;
            }
        }

        void _toolBarClearSelect()
        {
            //_myPropertyGrid.SelectedObject = null;
            foreach (object __getControl in _toolStripObject.Items)
            {
                if (__getControl.GetType().Equals(typeof(ToolStripButton)))
                {
                    ((ToolStripButton)__getControl).Checked = false;
                }
            }
        }

        /// <summary>
        /// 0 = เปลี่ยนชื่อ Font
        /// 1 = เปลี่ยน Size
        /// 2 = Bold
        /// 3 = Italic
        /// 4 = UnderLine
        /// 5 = Align Left
        /// 6 = Align Center
        /// 7 = Align Right
        /// </summary>
        /// <param name="mode"></param>
        void _fontChangeSelectedObject(int mode)
        {
            //_drawPaper __paperSelect = (_drawPaper)this._paperList[_paperNumber];
            //__paperSelect._area._graphicsList;
            int __selectionCount = this._paper._area._graphicsList._selectionCount;
            if (__selectionCount > 0)
            {
                for (int __index = 0; __index < __selectionCount; __index++)
                {
                    SMLReport._design._drawObject __object = this._paper._area._graphicsList._getSelectedObject(__index);
                    if ((__object.GetType() == typeof(SMLReport._design._drawLabel)) || (__object.GetType() == typeof(SMLReport._design._drawTextField)))
                    {
                        SMLReport._design._drawLabel __drawLabel = (SMLReport._design._drawLabel)__object;
                        if (mode == 0)
                        {
                            __drawLabel._font = new Font(this._fontNameComboBox.SelectedItem.ToString(), __drawLabel._font.Size, __drawLabel._font.Style, GraphicsUnit.Point, 222);
                        }
                        else if (mode == 1)
                        {
                            __drawLabel._font = new Font(__drawLabel._font.Name, (float)MyLib._myGlobal._intPhase(this._fontSizeComboBox.SelectedItem.ToString()), __drawLabel._font.Style, GraphicsUnit.Point, 222);
                        }
                        else if (mode == 2 || mode == 3 || mode == 4)
                        {
                            FontStyle __tmpFontStyle = __drawLabel._font.Style;

                            if (__selectionCount > 1)
                            {
                                if (mode == 2)
                                    __drawLabel._font = new Font(__drawLabel._font.Name, (float)MyLib._myGlobal._intPhase(this._fontSizeComboBox.SelectedItem.ToString()), FontStyle.Bold, GraphicsUnit.Point, 222);
                                if (mode == 3)
                                    __drawLabel._font = new Font(__drawLabel._font.Name, (float)MyLib._myGlobal._intPhase(this._fontSizeComboBox.SelectedItem.ToString()), FontStyle.Italic, GraphicsUnit.Point, 222);
                                if (mode == 4)
                                    __drawLabel._font = new Font(__drawLabel._font.Name, (float)MyLib._myGlobal._intPhase(this._fontSizeComboBox.SelectedItem.ToString()), FontStyle.Underline, GraphicsUnit.Point, 222);
                            }
                            else
                            {
                                if (mode == 2)
                                {
                                    if (__drawLabel._font.Bold)
                                    {
                                        __drawLabel._font = new Font(__drawLabel._font.Name, (float)MyLib._myGlobal._intPhase(this._fontSizeComboBox.SelectedItem.ToString()), FontStyle.Regular, GraphicsUnit.Point, 222);
                                    }
                                    else
                                    {
                                        __drawLabel._font = new Font(__drawLabel._font.Name, (float)MyLib._myGlobal._intPhase(this._fontSizeComboBox.SelectedItem.ToString()), FontStyle.Bold, GraphicsUnit.Point, 222);
                                    }
                                }
                                if (mode == 3)
                                {
                                    if (__drawLabel._font.Italic)
                                    {
                                        __drawLabel._font = new Font(__drawLabel._font.Name, (float)MyLib._myGlobal._intPhase(this._fontSizeComboBox.SelectedItem.ToString()), FontStyle.Regular, GraphicsUnit.Point, 222);
                                    }
                                    else
                                    {
                                        __drawLabel._font = new Font(__drawLabel._font.Name, (float)MyLib._myGlobal._intPhase(this._fontSizeComboBox.SelectedItem.ToString()), FontStyle.Italic, GraphicsUnit.Point, 222);
                                    }
                                }
                                if (mode == 4)
                                {
                                    if (__drawLabel._font.Underline)
                                    {
                                        __drawLabel._font = new Font(__drawLabel._font.Name, (float)MyLib._myGlobal._intPhase(this._fontSizeComboBox.SelectedItem.ToString()), FontStyle.Regular, GraphicsUnit.Point, 222);
                                    }
                                    else
                                    {
                                        __drawLabel._font = new Font(__drawLabel._font.Name, (float)MyLib._myGlobal._intPhase(this._fontSizeComboBox.SelectedItem.ToString()), FontStyle.Underline, GraphicsUnit.Point, 222);
                                    }
                                }
                            }

                        }
                        else if (mode == 5)
                        {
                            __drawLabel._textAlign = ContentAlignment.MiddleLeft;
                        }
                        else if (mode == 6)
                        {
                            __drawLabel._textAlign = ContentAlignment.MiddleCenter;
                        }
                        else if (mode == 7)
                        {
                            __drawLabel._textAlign = ContentAlignment.MiddleRight;
                        }

                        __drawLabel._setTextSize();
                    }

                }
                this._paper._area.Invalidate();
            }
        }

        void _paperAddNewPage(int pageNumber)
        {
            string __pageName = "";
            int __pageCount = 0;
            while (__pageName.Length == 0)
            {
                string __name = string.Concat("Page ", ++__pageCount);
                bool __found = false;
                for (int __loop = 0; __loop < this._paperListName.Count; __loop++)
                {
                    if (__name.ToLower().Equals(this._paperListName[__loop].ToString().ToLower()))
                    {
                        __found = true;
                        break;
                    }
                }
                if (__found == false)
                {
                    __pageName = __name;
                    break;
                }
            }
            _drawPaper __newPaper = new _drawPaper();
            __newPaper._Owner = this;
            this._paperList.Add(__newPaper);

            this._paperListName.Add(string.Concat(__pageName));
            _pageComboBoxRefresh(pageNumber);
            _paperAddEvent(pageNumber);
        }

        void _pageComboBoxRefresh(int pageNumber)
        {
            this._pageNameComboBox.Items.Clear();
            for (int __loop = 0; __loop < this._paperListName.Count; __loop++)
            {
                this._pageNameComboBox.Items.Add(this._paperListName[__loop]);
            }
            this._pageNameComboBox.SelectedIndex = pageNumber;
        }

        void _paperAddEvent(int pageNumber)
        {
            //
            _drawPaper __paperSelect = (_drawPaper)this._paperList[pageNumber];
            __paperSelect._area._afterClick -= new SMLReport._design.AfterClickEventHandler(_area__afterClick);
            __paperSelect._area._afterClickLostFocus -= new SMLReport._design.AfterClickLostFocusEventHandler(_area__afterClickLostFocus);
            __paperSelect._area._afterPaint -= new SMLReport._design.AfterPaintFocusEventHandler(_area__afterPaint);
            __paperSelect._area._graphicsList._getDefaultFieldList -= new SMLReport._design.getCollectionTypeConverter(_graphicsList__getDefaultFieldList);
            __paperSelect._area._graphicsList._getImageListNameConv -= new _design.getImagesNameListConverter(_graphicsList__getImageListNameConv);

            __paperSelect._area._afterAddDrawObject -= new _design.addDrawObject(_graphicsList__afterAddDrawObject);

            __paperSelect._area._afterClick += new SMLReport._design.AfterClickEventHandler(_area__afterClick);
            __paperSelect._area._afterClickLostFocus += new SMLReport._design.AfterClickLostFocusEventHandler(_area__afterClickLostFocus);
            __paperSelect._area._afterPaint += new SMLReport._design.AfterPaintFocusEventHandler(_area__afterPaint);
            __paperSelect._area._graphicsList._getDefaultFieldList += new SMLReport._design.getCollectionTypeConverter(_graphicsList__getDefaultFieldList);
            __paperSelect._area._graphicsList._getImageListNameConv += new _design.getImagesNameListConverter(_graphicsList__getImageListNameConv);

            __paperSelect._area._afterAddDrawObject += new _design.addDrawObject(_graphicsList__afterAddDrawObject);

            this.splitContainer1.Panel1.Controls.Add(__paperSelect);
            __paperSelect.Location = new Point(this._rulerLeftControl.Width, this._rulerTopControl.Height);
            //
            this._paper = __paperSelect;
            this._paper._area._graphicsList._unselectAll();
            this._paper._afterMouseOverPanel += new MouseOverPanelEventHandler(_paper__afterMouseOverPanel);
            this._paper._afterRemoveObject += new AfterRemoveDrawObjectEventHandler(_paper__afterRemoveObject);
        }

        void _paper__afterRemoveObject(object sender, MouseEventArgs e)
        {
            this._myPropertyGrid.SelectedObject = null;
            _dirty = true;
            _displayOutline();
        }

        void _graphicsList__afterAddDrawObject(object sender)
        {
            _dirty = true;
            _displayOutline();
        }

        void _displayOutline()
        {
            //this._paper._area._graphicsList._count; 

            //for (int __i = 0; __i < this._paper._area._graphicsList._count; __i++)
            //{
            //    // check

            //    // draw
            //}
        }

        void _paperRemoveEvent(int pageNumber)
        {
            _drawPaper __paperSelect = (_drawPaper)this._paperList[pageNumber];
            __paperSelect._area._afterClick -= new SMLReport._design.AfterClickEventHandler(_area__afterClick);
            __paperSelect._area._afterClickLostFocus -= new SMLReport._design.AfterClickLostFocusEventHandler(_area__afterClickLostFocus);
            __paperSelect._area._graphicsList._getDefaultFieldList -= new SMLReport._design.getCollectionTypeConverter(_graphicsList__getDefaultFieldList);
            __paperSelect._area._graphicsList._getImageListNameConv -= new _design.getImagesNameListConverter(_graphicsList__getImageListNameConv);

            __paperSelect._area._afterAddDrawObject -= new _design.addDrawObject(_graphicsList__afterAddDrawObject);

            __paperSelect._afterMouseOverPanel -= new MouseOverPanelEventHandler(_paper__afterMouseOverPanel);
            __paperSelect._afterRemoveObject -= new AfterRemoveDrawObjectEventHandler(_paper__afterRemoveObject);
            this.splitContainer1.Panel1.Controls.Remove(this._paper);
        }

        void _calcDrawAreaSize()
        {
            this._paper.Size = new Size(this.splitContainer1.Panel1.Width - this._rulerLeftControl.Width, this.splitContainer1.Panel1.Height - this._rulerTopControl.Height);
            Size __getPaperSizeByPixel = this._paper._myPageSetup.PagePixel;
            int __calcWidth = (int)(__getPaperSizeByPixel.Width * _drawScale);
            int __calcHeight = (int)(__getPaperSizeByPixel.Height * _drawScale);
            int _newWidth = __calcWidth + (this._paper._topLeftPaper.X * 2);
            int _newHeigth = __calcHeight + (this._paper._topLeftPaper.Y * 2);
            //
            if (_newWidth < this.splitContainer1.Panel1.Width - this._rulerLeftControl.Width)
            {
                _newWidth = this.splitContainer1.Panel1.Width - this._rulerLeftControl.Width;
            }
            if (_newHeigth < this.splitContainer1.Panel1.Height - this._rulerTopControl.Height)
            {
                _newHeigth = this.splitContainer1.Panel1.Height - this._rulerTopControl.Height;
            }
            this._paper.Size = new Size(_newWidth, _newHeigth);
            //
            if (this._paper.Height > this.splitContainer1.Panel1.Height - this._paper._topLeftPaper.X)
            {
                _vScrollBar.Visible = true;
                _vScrollBar.Location = new Point(this.splitContainer1.Panel1.Width - _vScrollBar.Width, 0);
                _vScrollBar.Height = this.splitContainer1.Panel1.Height;
                _vScrollBar.Maximum = (this._paper.Height - this.splitContainer1.Panel1.Height) + (this._paper._topLeftPaper.X * 2);
            }
            else
            {
                _vScrollBar.Visible = false;
            }
            if (this._paper.Width > this.splitContainer1.Panel1.Width - this._paper._topLeftPaper.Y)
            {
                _hScrollBar.Visible = true;
                _hScrollBar.Location = new Point(0, this.splitContainer1.Panel1.Height - _hScrollBar.Height);
                _hScrollBar.Width = this.splitContainer1.Panel1.Width - ((_vScrollBar.Visible) ? _vScrollBar.Width : 0);
                _hScrollBar.Maximum = (this._paper.Width - this.splitContainer1.Panel1.Width) + (this._paper._topLeftPaper.Y * 2);
            }
            else
            {
                _hScrollBar.Visible = false;
            }
            this.splitContainer1.Panel1.Invalidate();
            this._paper._area.Invalidate();
            this._paper.Invalidate();
            this.Invalidate();
            _ruleRefresh();
        }

        void _ruleRefresh()
        {
            _rulerTopControl._unit = this._paper._myPageSetup.Unit;
            _rulerTopControl._ruleScale = _drawScale;
            _rulerTopControl.Location = new Point(this._rulerLeftControl.Width, 0);
            _rulerTopControl.Width = this.splitContainer1.Panel1.Width - this._rulerLeftControl.Width;
            _rulerTopControl._beginValue = _design._pageSetup._convertPixelToUnit(this._paper._myPageSetup.Unit, _paper._topLeftPaper.X - ((_hScrollBar.Visible) ? _hScrollBar.Value : 0), _drawScale);
            _rulerTopControl.Invalidate();
            //
            _rulerLeftControl._unit = this._paper._myPageSetup.Unit;
            _rulerLeftControl._ruleScale = _drawScale;
            _rulerLeftControl.Location = new Point(0, this._rulerTopControl.Height);
            _rulerLeftControl.Height = this.splitContainer1.Panel1.Height - this._rulerTopControl.Height;
            _rulerLeftControl._beginValue = _design._pageSetup._convertPixelToUnit(this._paper._myPageSetup.Unit, _paper._topLeftPaper.Y - ((_vScrollBar.Visible) ? _vScrollBar.Value : 0), _drawScale);
            _rulerLeftControl.Invalidate();
        }

        void _area__afterPaint(Graphics sender)
        {
            this._buttonRedo.Enabled = false;
            this._buttonUndo.Enabled = false;
            this._buttonCopy.Enabled = false;
            this._buttonRemove.Enabled = false;
            this._buttonPaste.Enabled = false;
            this._buttonBringToFront.Enabled = false;
            this._buttonSendToBack.Enabled = false;
            this._buttonAlignToBottom.Enabled = false;
            this._buttonAlignToLeft.Enabled = false;
            this._buttonAlignToRight.Enabled = false;
            this._buttonAlignToTop.Enabled = false;
            //
            int __selectionCount = this._paper._area._graphicsList._selectionCount;
            if (__selectionCount >= 1)
            {
                this._buttonCopy.Enabled = true;
                this._buttonRemove.Enabled = true;
                this._buttonBringToFront.Enabled = true;
                this._buttonSendToBack.Enabled = true;
                if (__selectionCount >= 2)
                {
                    this._buttonAlignToBottom.Enabled = true;
                    this._buttonAlignToLeft.Enabled = true;
                    this._buttonAlignToRight.Enabled = true;
                    this._buttonAlignToTop.Enabled = true;
                }
            }
            //
            if (this._objectForCutAndCopy.Count > 0)
            {
                this._buttonPaste.Enabled = true;
            }
            // Undo
            if (this._paper._objectForUndoAndRedoIndex >= 0)
            {
                this._buttonUndo.Enabled = true;
            }
            // Redo
            if (this._paper._objectForUndoAndRedoIndex < this._paper._objectForUndoAndRedo.Count - 1)
            {
                this._buttonRedo.Enabled = true;
            }
        }

        void _designReport_Resize(object sender, EventArgs e)
        {
            _calcDrawAreaSize();
        }

        void Panel1_Resize(object sender, EventArgs e)
        {
            _calcDrawAreaSize();
        }

        void _myPropertyGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            _paper._area.Invalidate();
            _paper.Invalidate();

            // notify no save
            _dirty = true;
        }

        void _scaleComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (_scaleComboBox.SelectedIndex)
            {
                case 0: _drawScale = 5f; break;
                case 1: _drawScale = 2f; break;
                case 2: _drawScale = 1.5f; break;
                case 3: _drawScale = 1f; break;
                case 4: _drawScale = 0.75f; break;
                case 5: _drawScale = 0.5f; break;
                case 6: _drawScale = 0.25f; break;
                case 7:
                    int __calcWidth = (int)((_paper._myPageSetup.PaperWidth * _design._pageSetup._ratioPointPixel));
                    _drawScale = _fixedScale((float)Math.Round(((decimal)(this.splitContainer1.Panel1.Width) - (decimal)80.0) / __calcWidth, 2));
                    break;
                case 8:
                    int __calcHeight = (int)((_paper._myPageSetup.PaperHeight * _design._pageSetup._ratioPointPixel));
                    _drawScale = _fixedScale((float)Math.Round(((decimal)(this.splitContainer1.Panel1.Height) - (decimal)80.0) / __calcHeight, 2));
                    break;
            }
            this._paper._drawScale = _drawScale;
            this._paper._topLeftPaper = new Point(20, 20);
            this._paper.Location = new Point(this._rulerLeftControl.Width, this._rulerTopControl.Height);
            _calcDrawAreaSize();
            this._paper._area.Invalidate();
        }

        void _hScrollBar_ValueChanged(object sender, EventArgs e)
        {
            _paper.Location = new Point(this._rulerTopControl.Height + (-_hScrollBar.Value), _paper.Location.Y);
            _ruleRefresh();
        }

        void _vScrollBar_ValueChanged(object sender, EventArgs e)
        {
            _paper.Location = new Point(_paper.Location.X, this._rulerLeftControl.Width + (-_vScrollBar.Value));
            _ruleRefresh();
        }

        void _designReport_MouseWheel(object sender, MouseEventArgs e)
        {
            if (_vScrollBar.Visible)
            {
                if (e.Delta < 0)
                {
                    if (_vScrollBar.Value + 50 > _vScrollBar.Maximum)
                    {
                        _vScrollBar.Value = _vScrollBar.Maximum;
                    }
                    else
                    {
                        _vScrollBar.Value += 50;
                    }
                }
                else
                {
                    if (_vScrollBar.Value - 50 < 0)
                    {
                        _vScrollBar.Value = 0;
                    }
                    else
                    {
                        _vScrollBar.Value -= 50;
                    }
                }
            }
        }

        void _pageNameComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.splitContainer1.Panel1.SuspendLayout();
            _paperRemoveEvent(this._paperNumber);
            this._paperNumber = this._pageNameComboBox.SelectedIndex;
            _paperAddEvent(this._paperNumber);
            _calcDrawAreaSize();
            this.splitContainer1.Panel1.ResumeLayout();
        }

        void _fontNameComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            MyLib._myGlobal._myFontFormDesigner = new Font(this._fontNameComboBox.SelectedItem.ToString(), MyLib._myGlobal._myFontFormDesigner.Size, MyLib._myGlobal._myFontFormDesigner.Style, GraphicsUnit.Point, 222);
            _fontChangeSelectedObject(0);
        }

        void _fontSizeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            MyLib._myGlobal._myFontFormDesigner = new Font(this._fontNameComboBox.SelectedItem.ToString(), (float)MyLib._myGlobal._intPhase(this._fontSizeComboBox.SelectedItem.ToString()), MyLib._myGlobal._myFontFormDesigner.Style, GraphicsUnit.Point, 222);
            _fontChangeSelectedObject(1);
        }

        void _objectCopy()
        {
            _formClipBoardObject _clipboardFormObject = new _formClipBoardObject();

            int __selectionCount = this._paper._area._graphicsList._selectionCount;
            if (__selectionCount > 0)
            {
                this._objectForCutAndCopy = new ArrayList();
                for (int __index = 0; __index < __selectionCount; __index++)
                {
                    this._objectForCutAndCopy.Add(this._paper._area._graphicsList._getSelectedObject(__index)._clone());

                    _xmlDrawObjectClass __newObject = new _xmlDrawObjectClass();
                    SMLReport._design._drawObject __object = (SMLReport._design._drawObject)this._paper._area._graphicsList._getSelectedObject(__index)._clone();
                    __newObject.Color = __object._lineColor.ToString();
                    __newObject.PrintOption = __object.PringPage;

                    // get XML from Drawobject 
                    __newObject = _getXMLDrawObject(__object, __newObject);


                    _clipboardFormObject.DrawObject.Add(__newObject);
                }
            }
            this._paper._area._drawNetRectangle = false;
            this._paper._area.Invalidate();

            XmlSerializer __xs = new XmlSerializer(typeof(_formClipBoardObject));
            MemoryStream __memoryStream = new MemoryStream();

            __xs.Serialize(__memoryStream, _clipboardFormObject);

            string __data = MyLib._myGlobal._convertMemoryStreamToString(__memoryStream);

            DataObject __clipboardObject = new DataObject();
            __clipboardObject.SetData("formclipboard", __data);
            Clipboard.SetDataObject(__clipboardObject, true);
        }

        void _objectPaste()
        {


            if (this._objectForCutAndCopy.Count > 0)
            {
                this._paper._objectForUndoAndRedoAdd();
                this._paper._area._graphicsList._unselectAll();
                ArrayList __objectPaste = new ArrayList();


                for (int __loop = this._objectForCutAndCopy.Count - 1; __loop >= 0; __loop--)
                {
                    SMLReport._design._drawObject __newObject = (SMLReport._design._drawObject)((SMLReport._design._drawObject)this._objectForCutAndCopy[__loop])._clone();
                    this._paper._area._graphicsList._add(__newObject);

                    __objectPaste.Add(__newObject);
                }

                _myPropertyGrid.SelectedObjects = null;
                _myPropertyGrid.SelectedObjects = (object[])__objectPaste.ToArray();
            }
            else
            {
                try
                {
                    DataObject __clipboardObject = (DataObject)Clipboard.GetDataObject();
                    if (__clipboardObject.GetDataPresent("formclipboard"))
                    {
                        string __objectPaste = (string)__clipboardObject.GetData("formclipboard");

                        XmlSerializer __xsLoad = new XmlSerializer(typeof(_formClipBoardObject));
                        using (TextReader sr = new StringReader(__objectPaste))
                        {
                            _formClipBoardObject __objectClipboard = (_formClipBoardObject)__xsLoad.Deserialize(sr);

                            for (int __loop = __objectClipboard.DrawObject.Count - 1; __loop >= 0; __loop--)
                            {
                                _xmlDrawObjectClass __newObject = (_xmlDrawObjectClass)__objectClipboard.DrawObject[__loop];
                                SMLReport._design._drawObject __object = _setDrawObjectXML(__newObject);
                                __object._Owner = this;

                                this._paper._area._graphicsList._add(__object);
                            }

                        }

                        /*for (int __loop = __objectPaste.Count - 1; __loop >= 0; __loop--)
                        {
                            SMLReport._design._drawObject __newObject = (SMLReport._design._drawObject)((SMLReport._design._drawObject)__objectPaste[__loop])._clone();
                            this._paper._area._graphicsList._add(__newObject);

                            __objectPaste.Add(__newObject);
                        }

                        _myPropertyGrid.SelectedObjects = null;
                        _myPropertyGrid.SelectedObjects = (object[])__objectPaste.ToArray();*/
                    }

                }
                catch
                {

                }
            }


            this._paper._area._drawNetRectangle = false;
            this._paper._area.Invalidate();

            // notify no save
            _dirty = true;

        }

        void _objectUndo()
        {
            this._paper._undo();
            /*if (this._paper._objectForUndoAndRedoIndex >= 0)
            {
                int __index = this._paper._objectForUndoAndRedoIndex;
                this._paper._area = (SMLReport._design._drawPanel)this._paper._objectForUndoAndRedo[__index];
                this._paper._area._addEvent();
                this._paper._area._graphicsList._clear();
                for (int __loop = 0; __loop < this._paper._area._graphicsListClone._count; __loop++)
                {
                    this._paper._area._graphicsList._add((SMLReport._design._drawObject)((SMLReport._design._drawObject)this._paper._area._graphicsListClone[__loop])._clone());
                }
                this._paper._area.Invalidate();
                this._paper._objectForUndoAndRedoIndex = __index - 1;
            }*/
        }

        void _objectRedo()
        {
            this._paper._redo();
            //if (this._paper._objectForUndoAndRedoIndex >= 0 && this._paper._objectForUndoAndRedoIndex < this._paper._objectForUndoAndRedo.Count - 1)
            //{
            //    this._paper._area = (SMLReport._design._drawPanel)this._paper._objectForUndoAndRedo[this._paper._objectForUndoAndRedoIndex];
            //    this._paper._area._addEvent();
            //    this._paper._area._graphicsList._clear();
            //    for (int __loop = 0; __loop < this._paper._area._graphicsListClone._count; __loop++)
            //    {
            //        this._paper._area._graphicsList._add((SMLReport._design._drawObject)((SMLReport._design._drawObject)this._paper._area._graphicsListClone[__loop])._clone());
            //    }
            //    this._paper._area.Invalidate();
            //    this._paper._objectForUndoAndRedoIndex++;
            //}
        }

        #region Button Click

        private void _buttonAddBox_Click(object sender, EventArgs e)
        {
            _buttonUncheck(sender);
            _paper._area._activeTool = _design._drawToolType.Rectangle;
        }

        private void _buttonAddRoundedRectangle_Click(object sender, EventArgs e)
        {
            _buttonUncheck(sender);
            _paper._area._activeTool = _design._drawToolType.RoundedRectangle;
        }

        private void _buttonPointer_Click(object sender, EventArgs e)
        {
            _buttonUncheck(sender);
            _paper._area._activeTool = _design._drawToolType.Pointer;
        }

        private void _buttonAddEllipse_Click(object sender, EventArgs e)
        {
            _buttonUncheck(sender);
            _paper._area._activeTool = _design._drawToolType.Ellipse;
        }

        private void _buttonAddLine_Click(object sender, EventArgs e)
        {
            _buttonUncheck(sender);
            _paper._area._activeTool = _design._drawToolType.Line;
        }

        private void _buttonAddLabel_Click(object sender, EventArgs e)
        {
            _buttonUncheck(sender);
            _paper._area._activeTool = _design._drawToolType.Label;
        }

        private void _buttonAddTextField_Click(object sender, EventArgs e)
        {
            _buttonUncheck(sender);
            _paper._area._activeTool = _design._drawToolType.TextField;
        }

        private void _buttonAddImage_Click(object sender, EventArgs e)
        {
            _buttonUncheck(sender);
            _paper._area._activeTool = _design._drawToolType.Picture;
        }

        private void _buttonAddTable_Click(object sender, EventArgs e)
        {
            _buttonUncheck(sender);
            _paper._area._activeTool = _design._drawToolType.Table;
        }

        private void _buttonAddImageField_Click(object sender, EventArgs e)
        {
            _buttonUncheck(sender);
            _paper._area._activeTool = _design._drawToolType.ImageField;
        }

        private void _menuItemLabelPage_Click(object sender, EventArgs e)
        {
            _paper._area._specialText = "&page&";
            _paper._area._activeTool = _design._drawToolType.SpecialLabel;
        }

        private void _menuItemLabelTotalPage_Click(object sender, EventArgs e)
        {
            _paper._area._specialText = "&totalpage&";
            _paper._area._activeTool = _design._drawToolType.SpecialLabel;
        }

        private void _menuItemLabelDate_Click(object sender, EventArgs e)
        {
            _paper._area._specialText = "&date[dd/MM/yyyy]&";
            _paper._area._activeTool = _design._drawToolType.SpecialLabel;
        }

        private void _menuItemLabelPageTotal_Click(object sender, EventArgs e)
        {
            _paper._area._specialText = "&page& / &totalpage&";
            _paper._area._activeTool = _design._drawToolType.SpecialLabel;
        }

        private void _buttonRemove_Click(object sender, EventArgs e)
        {
            this._paper._objectDelete();
        }

        private void _buttonCopy_Click(object sender, EventArgs e)
        {
            _objectCopy();
        }

        private void _buttonPaste_Click(object sender, EventArgs e)
        {
            _objectPaste();
        }

        private void _buttonUndo_Click(object sender, EventArgs e)
        {
            _objectUndo();
        }

        private void _buttonRedo_Click(object sender, EventArgs e)
        {
            _objectRedo();
        }

        private void _buttonBringToFront_Click(object sender, EventArgs e)
        {
            this._paper._area._graphicsList._moveSelectionToFront();
            this._paper._area.Invalidate();
        }

        private void _buttonSendToBack_Click(object sender, EventArgs e)
        {
            this._paper._area._graphicsList._moveSelectionToBack();
            this._paper._area.Invalidate();
        }

        private void _buttonAlignToLeft_Click(object sender, EventArgs e)
        {
            this._paper._area._graphicsList._alignSelectionLeftEdges();
            this._paper._area.Invalidate();
        }

        private void _buttonAlignToTop_Click(object sender, EventArgs e)
        {
            this._paper._area._graphicsList._alignSelectionTopEdges();
            this._paper._area.Invalidate();
        }

        private void _buttonAlignToRight_Click(object sender, EventArgs e)
        {
            this._paper._area._graphicsList._alignRightSelectionEdges();
            this._paper._area.Invalidate();
        }

        private void _buttonAlignToBottom_Click(object sender, EventArgs e)
        {
            this._paper._area._graphicsList._alignBottomSelectionEdges();
            this._paper._area.Invalidate();
        }

        private void _buttonPageNew_Click(object sender, EventArgs e)
        {
            _paperRemoveEvent(this._paperNumber);
            this._paperNumber = this._paperList.Count;
            _paperAddNewPage(this._paperNumber);
        }

        private void _buttonPagePrev_Click(object sender, EventArgs e)
        {
            if (this._pageNameComboBox.SelectedIndex > 0)
            {
                this._pageNameComboBox.SelectedIndex--;
            }
        }

        private void _buttonPageNext_Click(object sender, EventArgs e)
        {
            if (this._pageNameComboBox.SelectedIndex < this._pageNameComboBox.Items.Count - 1)
            {
                this._pageNameComboBox.SelectedIndex++;
            }
        }

        private void _buttonPageSort_Click(object sender, EventArgs e)
        {
            if (this._paperListName.Count > 1)
            {
                _pageSort __pageSort = new _pageSort();
                for (int __loop = 0; __loop < this._paperListName.Count; __loop++)
                {
                    __pageSort._listBoxPage.Items.Add(this._paperListName[__loop]);
                }
                __pageSort._listBoxPage.SelectedIndex = 0;
                __pageSort.ShowDialog();
                if (__pageSort._selectOK)
                {
                    ArrayList __temp = new ArrayList();
                    ArrayList __tempName = new ArrayList();
                    for (int __loop = 0; __loop < this._paperList.Count; __loop++)
                    {
                        __temp.Add(this._paperList[__loop]);
                        __tempName.Add(this._paperListName[__loop]);
                    }
                    this._paperListName.Clear();
                    this._paperList.Clear();
                    for (int __loop = 0; __loop < __pageSort._listBoxPage.Items.Count; __loop++)
                    {
                        for (int __find = 0; __find < __tempName.Count; __find++)
                        {
                            if (__pageSort._listBoxPage.Items[__loop].Equals(__tempName[__find]))
                            {
                                this._paperListName.Add(__tempName[__find]);
                                this._paperList.Add(__temp[__find]);
                                break;
                            }
                        }
                    }
                    this._pageNameComboBox.Items.Clear();
                    for (int __loop = 0; __loop < this._paperListName.Count; __loop++)
                    {
                        this._pageNameComboBox.Items.Add(this._paperListName[__loop]);
                    }
                    this._pageNameComboBox.SelectedIndex = 0;
                }
            }
            else
            {
                MessageBox.Show("have one page");
            }
        }

        private void _buttonPageRemove_Click(object sender, EventArgs e)
        {
            if (this._paperList.Count > 1)
            {
                DialogResult __result = MessageBox.Show("Delete this page.", "Warning", MessageBoxButtons.YesNo);
                if (__result == DialogResult.Yes)
                {
                    this._pageNameComboBox.Items.RemoveAt(this._paperNumber);
                    this._paperList.RemoveAt(this._paperNumber);
                    this._paperListName.RemoveAt(this._paperNumber);
                    if (--this._paperNumber < 0)
                    {
                        this._paperNumber++;
                    }
                    this._pageNameComboBox.SelectedIndex = this._paperNumber;
                }
            }
        }

        private void _toolStripClose_Click(object sender, EventArgs e)
        {
            // warning save

            if (_dirty)
            {
                DialogResult __result = MessageBox.Show("คุณยังไม่ได้ทำการบันทึกการเปลี่ยนแปลง คุณต้องการที่จะบันทึก หรือไม่", "เตือน", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);

                if (__result == DialogResult.Yes)
                {
                    _onClickSaveFromDesign();
                    this.Dispose();
                }
                else if (__result == DialogResult.No)
                {
                    this.Dispose();
                }
            }
            else
            {
                this.Dispose();
            }
        }

        void _toolBar1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            _toolBarClearSelect();
        }

        private void _buttonFontBold_Click(object sender, EventArgs e)
        {
            _fontChangeSelectedObject(2);
        }

        private void _buttonFontItalics_Click(object sender, EventArgs e)
        {
            _fontChangeSelectedObject(3);
        }

        private void _buttonFontUnderLine_Click(object sender, EventArgs e)
        {
            _fontChangeSelectedObject(4);
        }

        private void _buttonAlignLeft_Click(object sender, EventArgs e)
        {
            _fontChangeSelectedObject(5);
        }

        private void _buttonAlignRight_Click(object sender, EventArgs e)
        {
            _fontChangeSelectedObject(6);
        }

        private void _buttonAlignCenter_Click(object sender, EventArgs e)
        {
            _fontChangeSelectedObject(7);
        }

        private void _buttonBox_Click(object sender, EventArgs e)
        {
        }

        private void _toolBar2_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void _buttonPageSetup_Click(object sender, EventArgs e)
        {
            _paper._myPageSetup.ShowDialog(this);
            _calcDrawAreaSize();
        }

        private void _buttonSave_Click(object sender, EventArgs e)
        {
            _onClickSaveFromDesign();
        }

        private void _buttoSaveAs_Click(object sender, EventArgs e)
        {
            SaveFormDesignAs();
        }

        private void _buttonLoad_Click(object sender, EventArgs e)
        {
            this._onClickOpenFormDesign();
        }

        private void _buttonLoadLocalDrive_Click(object sender, EventArgs e)
        {
            OpenFileDialog __openFile = new OpenFileDialog() { DefaultExt = "xml", Filter = "XML Files (*.xml)|*.xml|All files (*.*)|*.*", FilterIndex = 0 };
            if (__openFile.ShowDialog() == DialogResult.OK)
            {
                Stream __readFileStream = __openFile.OpenFile();
                _loadFromStream(__readFileStream, _openFormMethod.OpenFormLocal);
                __readFileStream.Close();
            }

        }

        private void _buttonSaveLocalDrive_Click(object sender, EventArgs e)
        {
            _saveToLocalFile();
        }

        private void _buttonGrid_Click(object sender, EventArgs e)
        {
            this._paper._showGrid = (this._paper._showGrid) ? false : true;
            this._paper._area.Invalidate();
        }

        private void _buttonHighlightTextField_Click(object sender, EventArgs e)
        {
            this._paper._area._showHighlightTextField = (this._paper._area._showHighlightTextField) ? false : true;
            this._paper._area.Invalidate();
        }

        private void _buttonRule_Click(object sender, EventArgs e)
        {
            this._rulerLeftControl.Visible = (this._rulerLeftControl.Visible) ? false : true;
            this._rulerTopControl.Visible = (this._rulerTopControl.Visible) ? false : true;
        }

        private void _helpButton_Click(object sender, EventArgs e)
        {
            // MyLib._myGlobal._showHtml("https://docs.google.com/document/pub?id=1Tm5QMgjOLDADsk9MdsiVqs7gyZ89Y3Py0DAOUHd_XmI");

            int count = Enum.GetNames(typeof(_g.g._transControlTypeEnum)).Length;
            StringBuilder __html = new StringBuilder();
            __html.Append(@"<head>
<style type='text/css'>
body,table,tr,td {
	font-family: Tahoma,Arial, Helvetica, sans-serif;
    font-size:11px;
}
</style>
</head>
<body>");
            for (int __i = 0; __i < count; __i++)
            {
                _g.g._transControlTypeEnum __getNum = (_g.g._transControlTypeEnum)Enum.ToObject(typeof(_g.g._transControlTypeEnum), __i);
                if (__getNum != _g.g._transControlTypeEnum.ว่าง)
                    __html.Append(_g.g._transFlagGlobal._transFlag(__getNum).ToString() + ":" + _g.g._transFlagGlobal._transName(_g.g._transFlagGlobal._transFlag(__getNum)) + "<br />");
            }
            __html.Append("</body>");

            WebBrowser __doc = new WebBrowser();
            __doc.DocumentText = __html.ToString();
            Form __form = new Form();
            __doc.Dock = DockStyle.Fill;
            __form.Controls.Add(__doc);
            __form.WindowState = FormWindowState.Maximized;
            __form.ShowDialog();


        }

        private void _buttonImageList_Click(object sender, EventArgs e)
        {
            _imageListCollectionEditor.EditValue(this, _imageList, "_collection");
        }

        private void _loadStandradForm_Click(object sender, EventArgs e)
        {
            _loadStandardForm __standard = new _loadStandardForm();
            __standard._selectFormStandard += (s1, e1) =>
            {
                this.Cursor = Cursors.WaitCursor;
                if (e1 != "")
                {
                    MyLib._myFrameWork __fw = new MyLib._myFrameWork(MyLib._myGlobal._masterWebservice, MyLib._myGlobal._masterConfigXmlName, MyLib._myGlobal._databaseType.PostgreSql);

                    string __query = "select " + _g.d.formdesign._formdesigntext + " from " + _g.d.formdesign._table + " where upper(" + _g.d.formdesign._formcode + ") = upper('" + e1.ToString() + "')";

                    byte[] __byte = __fw._queryByte(MyLib._myGlobal._masterDatabaseName, __query);

                    try
                    {
                        // ลองดึงแบบ compress
                        MemoryStream __ms = new MemoryStream(MyLib._compress._decompressBytes((byte[])__byte));
                        _loadFromStream(__ms, _openFormMethod.OpenFormStandrad);

                        __ms.Close();
                    }
                    catch
                    {
                        // ดึงแบบเดิม
                        try
                        {
                            MemoryStream __ms = new MemoryStream(__byte);
                            _loadFromStream(__ms, _openFormMethod.OpenFormStandrad);

                            __ms.Close();
                        }
                        catch (Exception ex)
                        {
                            this.Cursor = Cursors.Default;
                            MessageBox.Show("ผิดพลาด โปรดแจ้งกลับเจ้าหน้าที่");
                        }
                    }
                }
                else
                {
                    this.Cursor = Cursors.Default;
                    MessageBox.Show("ผิดพลาด โปรดแจ้งกลับเจ้าหน้าที่");
                }

                this.Cursor = Cursors.Default;
            };
            __standard.ShowDialog();
        }

        private void _queryButton_Click(object sender, EventArgs e)
        {
            this._onClickQueryButton();
        }

        private void _buttonDeleteForm_Click(object sender, EventArgs e)
        {
            this._onClickdeleteFormDesign();
        }

        private void _backgroundButton_Click(object sender, EventArgs e)
        {
            this._selectBackground = new _selectBackgroundForm();
            this._selectBackground._pictureBox.Image = this._paper._backgroundImage;
            this._selectBackground._showBackgroundCheckBox.Checked = true;
            this._selectBackground._saveButton.Click -= new EventHandler(_saveButton_Click);
            this._selectBackground._saveButton.Click += new EventHandler(_saveButton_Click);
            this._selectBackground.ShowDialog();
        }

        void _saveButton_Click(object sender, EventArgs e)
        {
            this._paper._backgroundImage = this._selectBackground._pictureBox.Image;
            this._paper._backgroundShow = this._selectBackground._showBackgroundCheckBox.Checked;
            this._paper._backgroundLeftMargin = (float)MyLib._myGlobal._decimalPhase(this._selectBackground._leftMarginText.Text);
            this._paper._backgroundTopMargin = (float)MyLib._myGlobal._decimalPhase(this._selectBackground._topMarginText.Text);
            this._selectBackground.Dispose();
            this._paper._area.Invalidate();
        }

        private void _buttonPreview_Click(object sender, EventArgs e)
        {
            this._onClickPreviewButton();
        }

        #endregion

        private void _designReport_Load(object sender, EventArgs e)
        {
            this._fontNameComboBox.SelectedIndex = this._fontNameComboBox.FindString(this._fontNameComboBox._defaultFontName, 0);
            this._fontSizeComboBox.SelectedIndex = 4;
        }

        private void _buttonUncheck(object sender)
        {
            ToolStripButton __getButton = (ToolStripButton)sender;
            __getButton.Checked = true;
            _toolStripObject.Invalidate();
        }

        protected virtual void _onClickSaveFromDesign()
        {
            if (this.FormName == null)
            {
                _saveForm __frmSaveName = new _saveForm();
                __frmSaveName._afterSaveButtonClick += (s1, formCode, formName) =>
                {
                    this._GUID = Guid.NewGuid().ToString();
                    this.FormCode = formCode.ToString();
                    this.FormName = formName.ToString();
                    this._lastUpdateProperty = DateTime.Now.ToString();
                    this._formTypeCombobox.SelectedIndex = __frmSaveName._formTypeCombobox.SelectedIndex;
                    _saveXMLSerialize(_saveFormMethod.SaveNewForm);
                    _formGuidStatusText.Text = this._GUID;
                    _formCodeStatusText.Text = this.FormCode;
                    _formNameStatusText.Text = this.FormName;


                };

                __frmSaveName._formTypeCombobox.SelectedIndex = 0;
                __frmSaveName.ShowDialog();
            }
            else
            {
                _saveXMLSerialize(_saveFormMethod.UpdateForm);
            }

        }

        protected virtual void _onClickOpenFormDesign()
        {
            // open dialog 

            _openFormServer __openDialog = new _openFormServer();
            __openDialog._selectedFormDesign += (s1, e1) =>
            {

                this.Cursor = Cursors.WaitCursor;


                string __query = "SELECT * FROM " + _g.d.formdesign._table + " WHERE upper(" + _g.d.formdesign._formcode + ") = upper('" + e1.ToString() + "')";

                MyLib._myFrameWork __ws = new MyLib._myFrameWork();
                MyLib.SMLJAVAWS.formDesignType __formDesign = __ws._loadForm(MyLib._myGlobal._databaseName, __query);

                if (__formDesign._code != null)
                {
                    try
                    {
                        // ลองดึงดู ถ้าข้อมูล Compress แล้ว ก็ผ่าน ถ้าไม่ ก็ไปดึงแบบเดิม
                        MemoryStream __ms = new MemoryStream(MyLib._compress._decompressBytes((byte[])__formDesign._formdesign));
                        MemoryStream __bgms = null;


                        if (__formDesign._formbg != null)
                        {
                            try
                            {
                                __bgms = new MemoryStream(MyLib._compress._decompressBytes((byte[])__formDesign._formbg));
                            }
                            catch
                            {
                                __bgms = new MemoryStream((byte[])__formDesign._formbg);
                            }
                        }
                        _loadFromStream(__ms, __bgms, _openFormMethod.OpenFromServer);
                        __ms.Close();
                        if (__formDesign._formbg != null)
                            __bgms.Close();


                        // load type
                        string __queryShort = " select " + _g.d.formdesign._form_type + " from " + _g.d.formdesign._table + " WHERE upper(" + _g.d.formdesign._formcode + ") = upper('" + e1.ToString() + "')";
                        DataTable __result = __ws._queryShort(__queryShort).Tables[0];
                        if (__result.Rows.Count > 0)
                        {
                            int __type = MyLib._myGlobal._intPhase(__result.Rows[0][_g.d.formdesign._form_type].ToString());
                            this._formTypeCombobox.SelectedIndex = __type;
                        }
                    }
                    catch
                    {
                        // กรณีที่ดึงของเก่าที่ไม่ได้ Compress
                        try
                        {
                            MemoryStream __ms = new MemoryStream((byte[])__formDesign._formdesign);
                            MemoryStream __bgms = new MemoryStream((byte[])__formDesign._formbg);
                            _loadFromStream(__ms, __bgms, _openFormMethod.OpenFromServer);
                            __ms.Close();
                            __bgms.Close();
                        }
                        catch
                        {
                            //  หากไม่ได้อีก ไปทำ แบบดั้งเดิม
                            try
                            {
                                string result = System.Text.Encoding.UTF8.GetString((byte[])__formDesign._formdesign);
                                _loadFormString(result, _openFormMethod.OpenFromServer);
                                this.Cursor = Cursors.Default;
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.ToString());
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("ผิดพลาด โปรดแจ้งกลับเจ้าหน้าที่");
                }


                this.Cursor = Cursors.Default;
            };
            __openDialog.ShowDialog();

        }

        protected virtual void _onClickdeleteFormDesign()
        {
            _deleteFormServer __frmDelete = new _deleteFormServer();
            __frmDelete.ShowDialog();
        }

        protected virtual void _onClickQueryButton()
        {
            this.__queryEdit.ShowDialog();
        }

        protected virtual void _onClickPreviewButton()
        {
            _formPrint __print = new _formPrint();
            __print.formDesign = this;
            __print._printRangeType = System.Drawing.Printing.PrintRange.AllPages;
            __print._includeDocSeries = true;
            __print._query();
        }

        protected virtual void _onGetFieldValue(SMLReport._design._drawObject sender)
        {
            if (_query.QueryLists.Count == 0)
            {
                sender._defaultField = null;
                return;
            }

            if (sender.GetType() == typeof(SMLReport._design._drawTextField))
            {
                SMLReport._design._drawTextField __objectTextField = (SMLReport._design._drawTextField)sender;

                query __tmpQuery = (query)_query.QueryLists[(int)__objectTextField.query];

                if (__tmpQuery._fieldList.Count == 0)
                {
                    __objectTextField._defaultField = null;
                    return;
                }
                string[] __listfield = new string[__tmpQuery._fieldList.Count];

                for (int i = 0; i < __tmpQuery._fieldList.Count; i++)
                {
                    queryField __tmpField = (queryField)__tmpQuery._fieldList[i];
                    __listfield[i] = "[" + __tmpField.FieldName.ToString() + "], " + __tmpField.Resource.ToString();
                }
                __objectTextField._defaultField = __listfield;

            }
            else if (sender.GetType() == typeof(SMLReport._design._drawImageField))
            {
                SMLReport._design._drawImageField __objectTextField = (SMLReport._design._drawImageField)sender;

                query __tmpQuery = (query)_query.QueryLists[(int)__objectTextField.query];

                if (__tmpQuery._fieldList.Count == 0)
                {
                    __objectTextField._defaultField = null;
                    return;
                }

                string[] __listfield = new string[__tmpQuery._fieldList.Count];

                for (int i = 0; i < __tmpQuery._fieldList.Count; i++)
                {
                    queryField __tmpField = (queryField)__tmpQuery._fieldList[i];
                    __listfield[i] = "[" + __tmpField.FieldName.ToString() + "], " + __tmpField.Resource.ToString();
                }

                __objectTextField._defaultField = __listfield;

            }
            else if (sender.GetType() == typeof(SMLReport._design._drawTable))
            {
                SMLReport._design._drawTable __objectTable = (SMLReport._design._drawTable)sender;
                query __tmpQuery = (query)_query.QueryLists[(int)__objectTable.DataQuery];

                if (__tmpQuery._fieldList.Count == 0)
                {
                    __objectTable._defaultField = null;
                    return;
                }

                string[] __listfield = new string[__tmpQuery._fieldList.Count];

                for (int i = 0; i < __tmpQuery._fieldList.Count; i++)
                {
                    queryField __tmpField = (queryField)__tmpQuery._fieldList[i];
                    __listfield[i] = "[" + __tmpField.FieldName.ToString() + "], " + __tmpField.Resource.ToString();
                }

                __objectTable._defaultField = __listfield;
            }

        }

        private void SaveFormDesignAs()
        {
            _saveForm __frmSaveName = new _saveForm();
            __frmSaveName._afterSaveButtonClick += (s1, formCode, formName) =>
            {
                this._GUID = Guid.NewGuid().ToString();
                this.FormCode = formCode.ToString();
                this.FormName = formName.ToString();
                this._lastUpdateProperty = DateTime.Now.ToString();
                this._formTypeCombobox.SelectedIndex = __frmSaveName._formTypeCombobox.SelectedIndex;
                _saveXMLSerialize(_saveFormMethod.SaveNewForm);
                _formGuidStatusText.Text = this._GUID;
                _formCodeStatusText.Text = this.FormCode;
                _formNameStatusText.Text = this.FormName;

            };

            if (this._formTypeCombobox.SelectedIndex >= 0)
            {
                this._formTypeCombobox.SelectedIndex = this._formTypeCombobox.SelectedIndex;
            }
            __frmSaveName.ShowDialog();

        }

        private void _saveXMLSerialize(_saveFormMethod __formSaveType)
        {

            if (__formSaveType == _saveFormMethod.SaveFormAs || __formSaveType == _saveFormMethod.SaveNewForm)
            {
                StringBuilder __queryCheckCode = new StringBuilder(MyLib._myGlobal._xmlHeader + "<node>");
                __queryCheckCode.Append(MyLib._myUtil._convertTextToXmlForQuery("select count(" + _g.d.formdesign._formcode + ") as codecount from " + _g.d.formdesign._table + "  where upper(" + _g.d.formdesign._formcode + ") = upper('" + this.FormCode + "')"));
                __queryCheckCode.Append("</node>");

                MyLib._myFrameWork __ws = new MyLib._myFrameWork();
                ArrayList __result = __ws._queryListGetData(MyLib._myGlobal._databaseName, __queryCheckCode.ToString());

                DataTable __da = ((DataSet)__result[0]).Tables[0];
                if (MyLib._myGlobal._decimalPhase(__da.Rows[0]["codecount"].ToString()) > 0)
                {
                    MessageBox.Show("มีการใช้ Form Code ดังกล่าวไปแล้ว");
                    return;
                }
            }

            // flush ก่อน ตัดขยะออกไป
            this.__queryEdit._Flush();

            SMLFormDesignXml __source = _writeXMLSource(_writeXMLSourceOption.DrawObjectOnly);
            BackgroundPageXML __bgSource = _writeBackgroundPageXML();

            try
            {
                this.Cursor = Cursors.WaitCursor;

                XmlSerializer __xs = new XmlSerializer(typeof(SMLFormDesignXml));
                MemoryStream __memoryStream = new MemoryStream();



                __xs.Serialize(__memoryStream, __source);

                XmlSerializer __xsbg = new XmlSerializer(typeof(BackgroundPageXML));
                MemoryStream __bgstream = new MemoryStream();
                __xsbg.Serialize(__bgstream, __bgSource);


                MyLib._myFrameWork __ws = new MyLib._myFrameWork();

                string __saveType = (__formSaveType == _saveFormMethod.UpdateForm) ? "0" : "1";
                byte[] __memoryStreamCompress = MyLib._compress._compressString(MyLib._myGlobal._convertMemoryStreamToString(__memoryStream));
                byte[] __BGMemoryStreamCompress = MyLib._compress._compressString(MyLib._myGlobal._convertMemoryStreamToString(__bgstream));
                string __result = __ws._saveFormDesign(MyLib._myGlobal._databaseName, __saveType, this.FormCode.ToUpper(), this._GUID.ToUpper(), this.FormName, __memoryStreamCompress, __BGMemoryStreamCompress);
                // jead string __result = __ws._SaveFormDesign(MyLib._myGlobal._databaseName, __saveType, this.FormCode.ToUpper(), this._GUID.ToUpper(), this.FormName, __memoryStream.ToArray(), __bgstream.ToArray());

                this.Cursor = Cursors.Default;

                if (__result.Length == 0)
                {
                    if (this._formTypeCombobox.SelectedIndex > 0)
                    {
                        string __updateForm = "update " + _g.d.formdesign._table + " set " + _g.d.formdesign._form_type + "=" + this._formTypeCombobox.SelectedIndex + " where " + MyLib._myGlobal._addUpper(_g.d.formdesign._formcode) + "='" + this.FormCode.ToUpper() + "'";
                        __ws._queryInsertOrUpdate(MyLib._myGlobal._databaseName, __updateForm);
                    }

                    MessageBox.Show("Save FormDesign Success");
                }
                else
                {
                    MessageBox.Show(__result, "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                __memoryStream.Close();
                __bgstream.Close();

            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                MessageBox.Show(ex.ToString());
            }


            _dirty = false;

        }

        private void _saveToLocalFile()
        {
            SMLFormDesignXml __source = _writeXMLSource();

            SaveFileDialog __objFile = new SaveFileDialog() { DefaultExt = "xml", Filter = "XML Files (*.xml)|*.xml|All files (*.*)|*.*", FilterIndex = 0 };
            if (__objFile.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    XmlSerializer __xs = new XmlSerializer(typeof(SMLFormDesignXml));

                    //MemoryStream __memoryStream = new MemoryStream();
                    //TextWriter __memoryStream = new StreamWriter(@"c:\temp\xx.xml");

                    //XmlTextWriter __xmlTextWriter = new XmlTextWriter(__memoryStream, Encoding.UTF8);
                    //__xs.Serialize(__xmlTextWriter, __source);

                    TextWriter __memoryStream = new StreamWriter(__objFile.OpenFile());
                    __xs.Serialize(__memoryStream, __source);

                    __memoryStream.Close();

                    MessageBox.Show("Save File to local Drive Success");
                    /*__memoryStream = (MemoryStream)__xmlTextWriter.BaseStream;
                    string __XmlizedString = UTF8ByteArrayToString(__memoryStream.ToArray());*/
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }

        }

        private float _fixedScale(float old)
        {
            float __result = old * 100;
            do
            {
                __result = __result - 1f;
            } while (__result % 25 != 0);
            if (__result < 25)
            {
                __result = 25;
            }
            return __result / 100;
        }

        private BackgroundPageXML _writeBackgroundPageXML()
        {
            BackgroundPageXML __backgroundSource = new BackgroundPageXML();

            for (int __pageSave = 0; __pageSave < this._paperList.Count; __pageSave++)
            {
                _drawPaper __areaSave = (_drawPaper)this._paperList[__pageSave];
                _xmlBackground __bgSave = new _xmlBackground();

                if (__areaSave._backgroundImage != null)
                {
                    __bgSave._backgroundPage = MyLib._myGlobal._imageToBase64(__areaSave._backgroundImage, System.Drawing.Imaging.ImageFormat.Png);
                    __bgSave._backgroundPage = MyLib._myGlobal._imageToBase64(__areaSave._backgroundImage, System.Drawing.Imaging.ImageFormat.Jpeg);
                    __bgSave._backgroundLeftMargin = __areaSave._backgroundLeftMargin;
                    __bgSave._backgroundTopMargin = __areaSave._backgroundTopMargin;
                    __bgSave._backgroundShow = __areaSave._backgroundShow;
                }
                __backgroundSource.PageBackground.Add(__bgSave);
            }

            return __backgroundSource;
        }

        private FormQuerys _writeFormQuery()
        {
            FormQuerys _query = new FormQuerys();
            _query = this.__queryEdit._getQueryFormDesign();
            return _query;
        }

        private ContentAlignment _convertToTextAlignment(SMLReport._design._textAlign __align)
        {
            ContentAlignment __newAlignMent = ContentAlignment.MiddleLeft;
            switch (__align)
            {
                case SMLReport._design._textAlign.Left:
                    __newAlignMent = ContentAlignment.MiddleLeft;
                    break;

                case SMLReport._design._textAlign.Center:
                    __newAlignMent = ContentAlignment.MiddleCenter;
                    break;

                case SMLReport._design._textAlign.Right:
                    __newAlignMent = ContentAlignment.MiddleRight;
                    break;
            }

            return __newAlignMent;
        }

        private _imageList _loadImageListFromXML(xmlImageList __xmlSerialize)
        {
            _imageList __loadImageList = new _imageList();

            if (__xmlSerialize.ImagesObject != null)
            {
                for (int __i = 0; __i < __xmlSerialize.ImagesObject.Count; __i++)
                {
                    if (__xmlSerialize.ImagesObject[__i].GetType() == typeof(xmlImageObject))
                    {
                        xmlImageObject __xmlImageObject = (xmlImageObject)__xmlSerialize.ImagesObject[__i];
                        _imageObject __imageObject = new _imageObject()
                        {
                            _keyName = __xmlImageObject._imageName,
                            _image = MyLib._myGlobal._base64ToImage(__xmlImageObject._imageSource)
                        };

                        __loadImageList._collection.Add(__imageObject);
                    }
                }
            }

            return __loadImageList;
        }

        private Color _convertStringToColor(string source)
        {
            if (source == null)
                return Color.Transparent;

            Color __result = new Color();
            if (source.IndexOf('=') != -1)
            {
                source = source.Replace(']', ',');
                string[] __split = source.Split('=');
                __result = Color.FromArgb(MyLib._myGlobal._intPhase(__split[1].Substring(0, __split[1].IndexOf(','))), MyLib._myGlobal._intPhase(__split[2].Substring(0, __split[2].IndexOf(','))), MyLib._myGlobal._intPhase(__split[3].Substring(0, __split[3].IndexOf(','))), MyLib._myGlobal._intPhase(__split[4].Substring(0, __split[4].IndexOf(','))));
            }
            else
            {
                int __index1 = source.IndexOf('[') + 1;
                int __index2 = source.IndexOf(']') - 1;
                string __getName = source.Substring(__index1, (__index2 - __index1) + 1);
                __result = Color.FromName(__getName);
            }
            return __result;
        }

        private String UTF8ByteArrayToString(Byte[] characters)
        {

            UTF8Encoding encoding = new UTF8Encoding();
            String constructedString = encoding.GetString(characters);
            return (constructedString);
        }

        private Byte[] StringToUTF8ByteArray(String pXmlString)
        {
            UTF8Encoding encoding = new UTF8Encoding();
            Byte[] byteArray = encoding.GetBytes(pXmlString);
            return byteArray;
        }

        private xmlImageList _getSerializeImageList()
        {
            xmlImageList __xmlImageList = new xmlImageList();

            for (int __i = 0; __i < _imageList._collection.Count; __i++)
            {
                if ((_imageList._collection[__i]._keyName != null) && (_imageList._collection[__i]._image != null))
                {
                    xmlImageObject __imageListObject = new xmlImageObject()
                    {
                        _imageName = _imageList._collection[__i]._keyName,
                        _imageSource = MyLib._myGlobal._imageToBase64(_imageList._collection[__i]._image, System.Drawing.Imaging.ImageFormat.Png)
                    };

                    __xmlImageList.ImagesObject.Add(__imageListObject);
                }
            }

            return __xmlImageList;
        }

        /// <summary>
        /// get XMLDrawObject From DrawObject
        /// </summary>
        /// <param name="__object">Instance of _drawObject</param>
        /// <param name="__newObject">_xmlDrawObjectClass template</param>
        /// <returns></returns>
        private _xmlDrawObjectClass _getXMLDrawObject(SMLReport._design._drawObject __object, _xmlDrawObjectClass __newObject)
        {
            if (__object.GetType() == typeof(_design._drawImage))
            {
                // object is picture
                __newObject.ToolType = SMLReport._design._drawToolType.Picture;
                SMLReport._design._drawImage __imageObject = (SMLReport._design._drawImage)__object;
                __newObject.Top = __imageObject.Size.Top;
                __newObject.Left = __imageObject.Size.Left;
                __newObject.Bottom = __imageObject.Size.Bottom;
                __newObject.Right = __imageObject.Size.Right;
                __newObject.BorderStyle = __imageObject.BorderStyle;
                __newObject.LineColor = __imageObject._lineColor.ToString();
                __newObject.PictureBoxSizeMode = __imageObject.SizeMode;
                __newObject.Opacity = __imageObject.Opacity.ToString();
                __newObject.RotateFlip = __imageObject.RotateFlip;
                __newObject.Angle = __imageObject.Angle;
                if (__imageObject.Image != null)
                {
                    __newObject.Image = MyLib._myGlobal._imageToBase64(__imageObject.Image, System.Drawing.Imaging.ImageFormat.Jpeg);
                }
            }
            else if (__object.GetType() == typeof(_design._drawLabel))
            {
                // object is label
                SMLReport._design._drawLabel __labelObject = (SMLReport._design._drawLabel)__object;
                __newObject.ToolType = SMLReport._design._drawToolType.Label;
                __newObject.Text = __labelObject._text;
                //__newObject.TextAlign = (__labelObject._textAlign.GetType() == typeof(ContentAlignment)) ? SMLReport._design._textAlign.Center: __labelObject._textAlign;
                __newObject.ContentAlign = __labelObject._textAlign;
                __newObject.Top = __labelObject._actualSize.Top;
                __newObject.Left = __labelObject._actualSize.Left;
                __newObject.Bottom = __labelObject._actualSize.Bottom;
                __newObject.Right = __labelObject._actualSize.Right;
                __newObject.FontName = __labelObject._font.Name;
                __newObject.FontSize = __labelObject._font.Size;
                __newObject.FontStyle = __labelObject._font.Style;
                __newObject.Color = __labelObject._foreColor.ToString();
                __newObject.LineColor = __labelObject._lineColor.ToString();
                __newObject.Padding = __labelObject._padding;
                __newObject.CharSpace = __labelObject._charSpace;
                __newObject.CharWidth = __labelObject._charWidth;
                __newObject.AllowLineBreak = __labelObject._allowLineBreak;
                __newObject.overFlow = __labelObject._overFlow;

            }
            else if (__object.GetType() == typeof(_design._drawTextField))
            {
                // object is TextField
                __newObject.ToolType = SMLReport._design._drawToolType.TextField;
                SMLReport._design._drawTextField __fieldObject = (SMLReport._design._drawTextField)__object;
                __newObject.PenWidth = __fieldObject._penWidth;
                __newObject.Color = __fieldObject._foreColor.ToString();
                __newObject.LineColor = __fieldObject._lineColor.ToString();
                __newObject.BackgroundColor = __fieldObject._backColor.ToString();
                __newObject.Text = __fieldObject.Field;
                __newObject.FieldFormat = __fieldObject.FieldFormat;
                __newObject.FieldType = __fieldObject.FieldType;
                //__newObject.TextAlign = _convertToTextAlignment(__fieldObject._textAlign);
                __newObject.ContentAlign = __fieldObject._textAlign;
                __newObject.Top = __fieldObject._actualSize.Top;
                __newObject.Left = __fieldObject._actualSize.Left;
                __newObject.Bottom = __fieldObject._actualSize.Bottom;
                __newObject.Right = __fieldObject._actualSize.Right;
                __newObject.FontName = __fieldObject._font.Name;
                __newObject.FontSize = __fieldObject._font.Size;
                __newObject.FontStyle = __fieldObject._font.Style;
                __newObject.queryRule = __fieldObject.query;
                __newObject.AutoLineSpace = __fieldObject._autoLineSpace;
                __newObject.LineSpace = __fieldObject._lineSpace;
                __newObject.ShowIsZeroValue = __fieldObject._showIsNumberZero;
                __newObject.LineStyle = __fieldObject._lineStyle;
                __newObject.Padding = __fieldObject._padding;
                __newObject.CharSpace = __fieldObject._charSpace;
                __newObject.CharWidth = __fieldObject._charWidth;
                __newObject.AllowLineBreak = __fieldObject._allowLineBreak;
                __newObject.ReplaceText = __fieldObject._replaceText;
                __newObject.MultiLine = __fieldObject._multiLine;
                __newObject.Operation = __fieldObject._operation;
                __newObject._specialText = __fieldObject._specialField;
                __newObject._asField = __fieldObject._asField;
                __newObject.Currency_Field = __fieldObject.FieldCurrency;
            }
            else if (__object.GetType() == typeof(_design._drawEllipse))
            {
                // object is ellipse
                __newObject.ToolType = SMLReport._design._drawToolType.Ellipse;
            }
            else if (__object.GetType() == typeof(_design._drawLine))
            {
                // object is Line
                __newObject.ToolType = SMLReport._design._drawToolType.Line;
            }
            else if (__object.GetType() == typeof(_design._drawRectangle))
            {
                // object is Rectangel
                __newObject.ToolType = SMLReport._design._drawToolType.Rectangle;
                SMLReport._design._drawRectangle __rectangleObject = (SMLReport._design._drawRectangle)__object;
                __newObject.Top = __rectangleObject.Size.Top;
                __newObject.Left = __rectangleObject.Size.Left;
                __newObject.Right = __rectangleObject.Size.Right;
                __newObject.Bottom = __rectangleObject.Size.Bottom;
                __newObject.LineStyle = __rectangleObject._LineStyle;
                __newObject.PenWidth = __rectangleObject._penWidth;
            }
            else if (__object.GetType() == typeof(_design._drawImageField))
            {
                // object is ImageField
                __newObject.ToolType = SMLReport._design._drawToolType.ImageField;
                SMLReport._design._drawImageField __imageFieldObject = (SMLReport._design._drawImageField)__object;
                __newObject.Top = __imageFieldObject._actualSize.Top;
                __newObject.Left = __imageFieldObject._actualSize.Left;
                __newObject.Bottom = __imageFieldObject._actualSize.Bottom;
                __newObject.Right = __imageFieldObject._actualSize.Right;
                __newObject.LineStyle = __imageFieldObject._LineStyle;
                __newObject.PenWidth = __imageFieldObject._penWidth;

                __newObject.LineColor = __imageFieldObject._lineColor.ToString();
                __newObject.LineStyle = __imageFieldObject._LineStyle;

                __newObject.Image = __imageFieldObject._imageObjectName;
                __newObject.Text = __imageFieldObject.Field;
                __newObject.FieldType = __imageFieldObject.FieldType;
                __newObject.queryRule = __imageFieldObject.query;
                __newObject.PictureBoxSizeMode = __imageFieldObject.SizeMode;
                __newObject.Value = __imageFieldObject._fieldCompareValue;

                // alwaysshow use showHeaderTable in XML Serialize
                __newObject.showHeaderTable = __imageFieldObject._alwaysShow;

                // showBarcode Label use _columnMultifield in XML Serialize
                __newObject._multiField = __imageFieldObject._showBarcodeLabel;
                __newObject.TextAlign = __imageFieldObject._barcodeAlignment;
                __newObject.Color = __imageFieldObject._foreColor.ToString();
                __newObject.BarcodeType = __imageFieldObject._typeBarcode;
                __newObject.BarcodeLabelPosition = __imageFieldObject._barcodeLabelPosition;

                __newObject.FontName = __imageFieldObject._font.Name;
                __newObject.FontSize = __imageFieldObject._font.Size;
                __newObject.FontStyle = __imageFieldObject._font.Style;
                __newObject.ReplaceText = __imageFieldObject._showText;

            }
            else if (__object.GetType() == typeof(_design._drawRoundedRectangle))
            {
                // object is RoundedRectangle
                __newObject.ToolType = SMLReport._design._drawToolType.RoundedRectangle;
                SMLReport._design._drawRoundedRectangle __rectangleObject = (SMLReport._design._drawRoundedRectangle)__object;
                __newObject.Top = __rectangleObject.Size.Top;
                __newObject.Left = __rectangleObject.Size.Left;
                __newObject.Right = __rectangleObject.Size.Right;
                __newObject.Bottom = __rectangleObject.Size.Bottom;
                __newObject.LineStyle = __rectangleObject._LineStyle;
                __newObject.PenWidth = __rectangleObject._penWidth;
                __newObject.RoundedRectangleRadius = __rectangleObject.RoundedRadius;

            }
            else if (__object.GetType() == typeof(_design._drawTable))
            {
                // object is table
                __newObject.ToolType = SMLReport._design._drawToolType.Table;
                SMLReport._design._drawTable __tableObject = (SMLReport._design._drawTable)__object;
                __newObject.FontName = __tableObject._font.Name;
                __newObject.FontSize = __tableObject._font.Size;
                __newObject.FontStyle = __tableObject._font.Style;
                __newObject.Top = __tableObject.Size.Top;
                __newObject.Left = __tableObject.Size.Left;
                __newObject.Right = __tableObject.Size.Right;
                __newObject.Bottom = __tableObject.Size.Bottom;
                __newObject.LineStyle = __tableObject._LineStyle;
                __newObject.PenWidth = __tableObject._penWidth;
                __newObject.showHeaderTable = (Boolean)__tableObject.ShowHeaderColumns;
                __newObject.queryRule = __tableObject.DataQuery;
                __newObject.AutoLineSpace = __tableObject._autoLineSpace;
                __newObject.LineSpace = __tableObject.RowHeight;
                __newObject.LineColor = __tableObject._lineColor.ToString();
                __newObject.Color = __tableObject._foreColor.ToString();
                __newObject.RowsLineColor = __tableObject.RowLineColor.ToString();
                __newObject.RowsLineStyle = __tableObject.RowLineStyle;
                __newObject.RowPerPage = __tableObject.RowPerPage;
                __newObject.AllowLineBreak = __tableObject._averageRowHeight;
                __newObject._pageOverflowNewLine = __tableObject._pageOverflowNewLine;
                // columnMultiField usein showiszerovalue 

                // group row
                __newObject._multiField = __tableObject._columnsMultiField;
                __newObject._groupRowDetail = __tableObject._groupRowDetail;
                __newObject._GroupDetailFieldName = __tableObject._groupDetailFileName;

                // columns
                __newObject.ColumnsSeparatorLineColor = __tableObject.ColumnsSeparatorLineColor.ToString();
                __newObject.ColumnsSeparatorLine = __tableObject.ColumnsSeparatorLine;

                // header
                __newObject.HeaderBackground = __tableObject.HeaderBackground.ToString();
                __newObject.HeaderForeColor = __tableObject.HeaderForeColor.ToString();
                __newObject.HeaderLineColor = __tableObject.HeaderRowLineColor.ToString();
                __newObject.HeaderLineStyle = __tableObject.HeaderRowLineStyle;

                // footer
                __newObject.FooterHeight = __tableObject._footerHeight;
                __newObject.showFooterTable = __tableObject.ShowFooter;

                // serial number
                __newObject.SerialQuery = __tableObject.SerialQuery;
                __newObject._serialField = __tableObject._serialNumberField;
                __newObject._dataPriLink = __tableObject._dataPrimaryLink;
                __newObject._dataSecLink = __tableObject._dataSecondLink;
                __newObject.Currency_Field = __tableObject._dataCurrencyCode;

                foreach (SMLReport._design._tableColumns __col in __tableObject.Columns)
                {
                    _XmlTableColumns __tbCol = new _XmlTableColumns();

                    __tbCol.ColumnsWidth = __col.ColumnsWidth;
                    //__tbCol.HeaderAlignment = __col.HeaderAlignment;
                    __tbCol.ContentHeaderAlign = __col.HeaderAlignment;

                    __tbCol.HeaderBackground = __col.HeaderBackground.ToString();
                    __tbCol.HeaderText = __col.HeaderText;

                    __tbCol.ContentAlign = __col.TextAlignment;

                    __tbCol.Text = __col.Text;
                    __tbCol.FieldFormat = __col.FieldFormat;
                    __tbCol.FieldType = __col.FieldType;
                    __tbCol.ShowIsZeroValue = __col._showIsNumberZero;
                    __tbCol.Padding = __col._padding;
                    __tbCol.ReplaceText = __col._replaceText;
                    __tbCol._printSerial = __col._printSerialNumber;
                    __tbCol._serialNumberColumn = __col._serialNumberColumn;
                    __tbCol._serialDiaplay = __col._serialNumberDisplay;
                    __tbCol._showSerialNewLine = __col._showSerialNewLine;
                    __tbCol.BarcodeType = __col._typeBarcode;
                    __tbCol._groupType = __col._columnGroupType;
                    __tbCol._autoLineBreak = __col._autoLineBreak;

                    __tbCol._showLotNumber = __col.showLotNumber;
                    __tbCol._lotNumberField = __col.lotFieldName;
                    __tbCol._lotOperation = __col.lotGroupOperation;

                    foreach (SMLReport._design._drawObject __multiFieldObject in __col._multiFieldCollection)
                    {
                        _xmlDrawObjectClass __multiFieldXML = new _xmlDrawObjectClass();

                        __multiFieldXML.Color = __multiFieldObject._lineColor.ToString();
                        __multiFieldXML.PrintOption = __multiFieldObject.PringPage;

                        if (__multiFieldObject.GetType() == typeof(_design._drawTextField))
                        {
                            ((_design._drawTextField)__multiFieldObject).query = __tableObject.DataQuery;
                        }
                        else if (__multiFieldObject.GetType() == typeof(_design._drawImageField))
                        {
                            ((_design._drawImageField)__multiFieldObject).query = __tableObject.DataQuery;
                        }

                        __multiFieldXML = _getXMLDrawObject(__multiFieldObject, __multiFieldXML);

                        __tbCol.DrawObject.Add(__multiFieldXML);
                    }

                    //__tableObj.Columns.Add(__tbCol);
                    __newObject.TableColumns.Add(__tbCol);

                }

                foreach (SMLReport._design._tableFooters __footer in __tableObject.Footers)
                {
                    _XmlTableFooter __xml = new _XmlTableFooter()
                    {
                        Text = __footer.Text,
                        ReplaceText = __footer._replaceText,
                        BackgroundColor = __footer.Background.ToString(),
                        ColumnsWidth = __footer.ColumnsWidth,
                        ContentAlign = __footer.TextAlignment,
                        FieldFormat = __footer.FieldFormat,
                        FieldType = __footer.FieldType,
                        Operation = __footer._operation,
                        Padding = __footer._padding,
                        ShowIsZeroValue = __footer._showIsNumberZero
                    };

                    __newObject.TableFooters.Add(__xml);
                }

                // Header Font 
                __newObject.HeaderFontName = __tableObject._headerFont.Name;
                __newObject.HeaderFontSize = __tableObject._headerFont.Size;
                __newObject.HeaderFontStyle = __tableObject._headerFont.Style;

                //XmlSerializer __colXs = new XmlSerializer(typeof(XmlTableObject));
                //TextWriter __memoryStream = new StreamWriter(@"c:\temp\columns.xml");
                //__colXs.Serialize(__memoryStream, __tableObj);
                //__memoryStream.Close();


            }


            //__newObject.Color = __object._lineColor.ToString();
            __newObject.Height = (int)(__object._height / __object._drawScale);
            __newObject.Width = (int)(__object._width / __object._drawScale);
            __newObject.PenWidth = (int)(__object._penWidth / __object._drawScale);
            __newObject.BackgroundColor = __object._backColor.ToString();
            __newObject.Lock = __object._lock;

            // Line
            if (__newObject.ToolType == SMLReport._design._drawToolType.Line)
            {
                __newObject.StartPointX = __object._getHandleNoScale(1).X;
                __newObject.StartPointY = __object._getHandleNoScale(1).Y;
                __newObject.StopPointX = __object._getHandleNoScale(0).X;
                __newObject.StopPointY = __object._getHandleNoScale(0).Y;
            }
            else if (__newObject.ToolType != SMLReport._design._drawToolType.Label && __newObject.ToolType != SMLReport._design._drawToolType.Picture && __newObject.ToolType != SMLReport._design._drawToolType.TextField)
            {
                __newObject.Top = __object._getHandleRectangleNoScale(1).Top;
                __newObject.Left = __object._getHandleRectangleNoScale(1).Left;
                __newObject.Bottom = __object._getHandleRectangleNoScale(5).Bottom;
                __newObject.Right = __object._getHandleRectangleNoScale(5).Right;
            }

            return __newObject;
        }

        private SMLReport._design._drawObject _setDrawObjectXML(_xmlDrawObjectClass __getObject)
        {
            if (__getObject.ToolType == SMLReport._design._drawToolType.Rectangle)
            {
                // is Rectangle
                SMLReport._design._drawRectangle __newRectangle = new SMLReport._design._drawRectangle(__getObject.Left, __getObject.Top, __getObject.Right - __getObject.Left, __getObject.Bottom - __getObject.Top, 1);
                __newRectangle._penWidth = __getObject.PenWidth;
                __newRectangle._lineColor = _convertStringToColor(__getObject.Color);
                __newRectangle._backColor = _convertStringToColor(__getObject.BackgroundColor);
                __newRectangle._LineStyle = __getObject.LineStyle;
                //ColorTranslator.ToOle(__newRectangle._backColor);
                __newRectangle._lock = __getObject.Lock;

                __newRectangle.PringPage = (__getObject.PrintOption == null) ? "All" : __getObject.PrintOption;
                return __newRectangle;
            }
            else if (__getObject.ToolType == SMLReport._design._drawToolType.RoundedRectangle)
            {
                // is RoundedRectangle
                SMLReport._design._drawRoundedRectangle __newRectangle = new SMLReport._design._drawRoundedRectangle(__getObject.Left, __getObject.Top, __getObject.Right - __getObject.Left, __getObject.Bottom - __getObject.Top, 1);
                __newRectangle._penWidth = __getObject.PenWidth;
                __newRectangle._lineColor = _convertStringToColor(__getObject.Color);
                __newRectangle._backColor = _convertStringToColor(__getObject.BackgroundColor);
                __newRectangle._LineStyle = __getObject.LineStyle;
                __newRectangle.RoundedRadius = __getObject.RoundedRectangleRadius;
                //ColorTranslator.ToOle(__newRectangle._backColor);
                __newRectangle.PringPage = (__getObject.PrintOption == null) ? "All" : __getObject.PrintOption;
                __newRectangle._lock = __getObject.Lock;

                return __newRectangle;
            }
            else if (__getObject.ToolType == SMLReport._design._drawToolType.Line)
            {
                // is Line
                SMLReport._design._drawLine __newLine = new SMLReport._design._drawLine(__getObject.StartPointX, __getObject.StartPointY, __getObject.StopPointX, __getObject.StopPointY, 1);
                __newLine._penWidth = __getObject.PenWidth;
                __newLine._lineColor = _convertStringToColor(__getObject.Color);
                __newLine._backColor = _convertStringToColor(__getObject.BackgroundColor);
                __newLine.PringPage = (__getObject.PrintOption == null) ? "All" : __getObject.PrintOption;
                __newLine._lock = __getObject.Lock;

                return __newLine;
            }
            else if (__getObject.ToolType == SMLReport._design._drawToolType.Ellipse)
            {
                // is Ellipse
                SMLReport._design._drawEllipse __newEllipse = new SMLReport._design._drawEllipse(__getObject.Left, __getObject.Top, __getObject.Right - __getObject.Left, __getObject.Bottom - __getObject.Top, 1);
                __newEllipse._penWidth = __getObject.PenWidth;
                __newEllipse._lineColor = _convertStringToColor(__getObject.Color);
                __newEllipse._backColor = _convertStringToColor(__getObject.BackgroundColor);
                __newEllipse.PringPage = (__getObject.PrintOption == null) ? "All" : __getObject.PrintOption;
                __newEllipse._lock = __getObject.Lock;

                return __newEllipse;
            }
            else if (__getObject.ToolType == SMLReport._design._drawToolType.Label)
            {
                // is label
                SMLReport._design._drawLabel __newLabel = new SMLReport._design._drawLabel(__getObject.Left, __getObject.Top, __getObject.Right - __getObject.Left, __getObject.Bottom - __getObject.Top, 1);
                __newLabel._penWidth = __getObject.PenWidth;
                __newLabel._padding = __getObject.Padding;
                __newLabel._charSpace = __getObject.CharSpace;
                __newLabel._charWidth = __getObject.CharWidth;
                __newLabel._allowLineBreak = __getObject.AllowLineBreak;
                __newLabel._foreColor = _convertStringToColor(__getObject.Color);
                __newLabel._lineColor = _convertStringToColor(__getObject.LineColor);
                __newLabel._backColor = _convertStringToColor(__getObject.BackgroundColor);
                __newLabel._text = __getObject.Text;
                __newLabel._textAlign = (__getObject.ContentAlign == 0) ? _convertToTextAlignment(__getObject.TextAlign) : __getObject.ContentAlign;
                __newLabel._font = new Font(__getObject.FontName, __getObject.FontSize, __getObject.FontStyle, GraphicsUnit.Point, 222);
                __newLabel.PringPage = (__getObject.PrintOption == null) ? "All" : __getObject.PrintOption;
                __newLabel._setTextSize();
                __newLabel._lock = __getObject.Lock;
                __newLabel._overFlow = __getObject.overFlow;
                return __newLabel;
            }
            else if (__getObject.ToolType == SMLReport._design._drawToolType.TextField)
            {
                // is textfield
                SMLReport._design._drawTextField __newField = new _design._drawTextField(__getObject.Left, __getObject.Top, __getObject.Right - __getObject.Left, __getObject.Bottom - __getObject.Top, 1);
                __newField._penWidth = __getObject.PenWidth;
                __newField._padding = __getObject.Padding;
                __newField._charSpace = __getObject.CharSpace;
                __newField._charWidth = __getObject.CharWidth;
                __newField._allowLineBreak = __getObject.AllowLineBreak;
                __newField._foreColor = _convertStringToColor(__getObject.Color);
                __newField._lineColor = _convertStringToColor(__getObject.LineColor);
                __newField._lineStyle = __getObject.LineStyle;
                __newField._backColor = _convertStringToColor(__getObject.BackgroundColor);
                __newField.Field = __getObject.Text;
                __newField.FieldFormat = __getObject.FieldFormat;
                __newField.FieldType = __getObject.FieldType;
                __newField._textAlign = (__getObject.ContentAlign == 0) ? _convertToTextAlignment(__getObject.TextAlign) : __getObject.ContentAlign;
                __newField._font = new Font(__getObject.FontName, __getObject.FontSize, __getObject.FontStyle, GraphicsUnit.Point, 222);
                __newField.query = __getObject.queryRule;
                __newField._autoLineSpace = __getObject.AutoLineSpace;
                __newField._lineSpace = __getObject.LineSpace;
                __newField._showIsNumberZero = __getObject.ShowIsZeroValue;
                __newField.PringPage = (__getObject.PrintOption == null) ? "All" : __getObject.PrintOption;
                __newField._replaceText = (__getObject.ReplaceText == null) ? "" : __getObject.ReplaceText;
                __newField._multiLine = __getObject.MultiLine;
                __newField._operation = __getObject.Operation;
                __newField._setTextSize();
                __newField._lock = __getObject.Lock;
                __newField._specialField = (__getObject._specialText == null) ? "" : __getObject._specialText;
                __newField._asField = (__getObject._asField == null) ? "" : __getObject._asField;
                __newField.FieldCurrency = (__getObject.Currency_Field == null) ? "" : __getObject.Currency_Field;
                return __newField;
            }
            else if (__getObject.ToolType == SMLReport._design._drawToolType.Picture)
            {
                // is picture
                SMLReport._design._drawImage __newImage = new SMLReport._design._drawImage(__getObject.Left, __getObject.Top, __getObject.Right - __getObject.Left, __getObject.Bottom - __getObject.Top, 1);
                __newImage._penWidth = __getObject.PenWidth;
                __newImage._lineColor = _convertStringToColor(__getObject.LineColor);
                __newImage._lineColor = _convertStringToColor(__getObject.Color);
                __newImage._backColor = _convertStringToColor(__getObject.BackgroundColor);
                __newImage.BorderStyle = __getObject.BorderStyle;
                __newImage.Angle = __getObject.Angle;
                __newImage.Opacity = MyLib._myGlobal._intPhase(__getObject.Opacity);
                __newImage.RotateFlip = __getObject.RotateFlip;
                __newImage.SizeMode = __getObject.PictureBoxSizeMode;
                __newImage.PringPage = (__getObject.PrintOption == null) ? "All" : __getObject.PrintOption;
                __newImage._lock = __getObject.Lock;

                if (__getObject.Image != null)
                {
                    __newImage.Image = MyLib._myGlobal._base64ToImage(__getObject.Image);
                }

                return __newImage;
            }
            else if (__getObject.ToolType == SMLReport._design._drawToolType.ImageField)
            {
                // object is ImageField
                SMLReport._design._drawImageField __newImageField = new _design._drawImageField(__getObject.Left, __getObject.Top, __getObject.Right - __getObject.Left, __getObject.Bottom - __getObject.Top, 1);

                __newImageField._LineStyle = __getObject.LineStyle;
                __newImageField._penWidth = __getObject.PenWidth;
                __newImageField._lineColor = _convertStringToColor(__getObject.LineColor);
                __newImageField._LineStyle = __getObject.LineStyle;
                __newImageField._backColor = _convertStringToColor(__getObject.BackgroundColor);

                __newImageField._imageObjectName = __getObject.Image;
                __newImageField.Field = __getObject.Text;
                __newImageField.FieldType = __getObject.FieldType;
                __newImageField.query = __getObject.queryRule;
                __newImageField.SizeMode = __getObject.PictureBoxSizeMode;
                __newImageField._fieldCompareValue = __getObject.Value;
                __newImageField.__imageList = _imageList;

                __newImageField._alwaysShow = __getObject.showHeaderTable;

                __newImageField._showBarcodeLabel = __getObject._multiField;
                __newImageField._barcodeAlignment = __getObject.TextAlign;
                __newImageField._foreColor = _convertStringToColor(__getObject.Color);
                __newImageField._typeBarcode = __getObject.BarcodeType;
                __newImageField._barcodeLabelPosition = __getObject.BarcodeLabelPosition;
                __newImageField._showText = __getObject.ReplaceText;
                __newImageField._lock = __getObject.Lock;

                try
                {
                    __newImageField._font = new Font(__getObject.FontName, __getObject.FontSize, __getObject.FontStyle, GraphicsUnit.Point, 222);
                }
                catch
                {
                }

                return __newImageField;

            }
            else if (__getObject.ToolType == SMLReport._design._drawToolType.Table)
            {
                // is a table
                SMLReport._design._drawTable __newTable = new _design._drawTable(__getObject.Left, __getObject.Top, __getObject.Right - __getObject.Left, __getObject.Bottom - __getObject.Top, 1, this._paper._area);

                try
                {
                    __newTable._font = new Font(__getObject.FontName, __getObject.FontSize, __getObject.FontStyle, GraphicsUnit.Point, 222);
                }
                catch
                {
                }

                // header font
                try
                {
                    __newTable._headerFont = new Font(__getObject.HeaderFontName, __getObject.HeaderFontSize, __getObject.HeaderFontStyle, GraphicsUnit.Point, 222);
                }
                catch
                {
                    __newTable._headerFont = new Font(__getObject.FontName, __getObject.FontSize, __getObject.FontStyle, GraphicsUnit.Point, 222);
                }

                __newTable._penWidth = __getObject.PenWidth;
                __newTable._lineColor = _convertStringToColor(__getObject.LineColor);
                __newTable._backColor = _convertStringToColor(__getObject.BackgroundColor);
                __newTable._LineStyle = __getObject.LineStyle;
                __newTable.ShowHeaderColumns = __getObject.showHeaderTable;
                __newTable.DataQuery = __getObject.queryRule;
                __newTable._autoLineSpace = __getObject.AutoLineSpace;
                __newTable.RowHeight = __getObject.LineSpace;
                __newTable._foreColor = _convertStringToColor(__getObject.Color);
                __newTable.PringPage = (__getObject.PrintOption == null) ? "All" : __getObject.PrintOption;
                __newTable._lock = __getObject.Lock;

                __newTable.RowLineColor = _convertStringToColor(__getObject.RowsLineColor);
                __newTable.RowLineStyle = __getObject.RowsLineStyle;
                __newTable.RowPerPage = __getObject.RowPerPage;
                __newTable._averageRowHeight = __getObject.AllowLineBreak;
                __newTable._pageOverflowNewLine = __getObject._pageOverflowNewLine;
                // columnMultiField usein showiszerovalue 
                __newTable._columnsMultiField = __getObject._multiField;

                // columns
                __newTable.ColumnsSeparatorLineColor = _convertStringToColor(__getObject.ColumnsSeparatorLineColor);
                __newTable.ColumnsSeparatorLine = __getObject.ColumnsSeparatorLine;

                // header
                __newTable.HeaderBackground = _convertStringToColor(__getObject.HeaderBackground);
                __newTable.HeaderForeColor = _convertStringToColor(__getObject.HeaderForeColor);
                __newTable.HeaderRowLineColor = _convertStringToColor(__getObject.HeaderLineColor);
                __newTable.HeaderRowLineStyle = __getObject.HeaderLineStyle;

                // footer 
                __newTable.ShowFooter = __getObject.showFooterTable;
                __newTable._footerHeight = __getObject.FooterHeight;

                // serial number
                __newTable.SerialQuery = __getObject.SerialQuery;
                __newTable._serialNumberField = __getObject._serialField;
                __newTable._dataPrimaryLink = __getObject._dataPriLink;
                __newTable._dataSecondLink = __getObject._dataSecLink;
                __newTable._dataCurrencyCode = (__getObject.Currency_Field == null) ? "" : __getObject.Currency_Field;

                __newTable._groupRowDetail = __getObject._groupRowDetail;
                __newTable._groupDetailFileName = __getObject._GroupDetailFieldName;

                if (__getObject.TableColumns.Count > 0)
                {
                    foreach (SMLReport._formReport._XmlTableColumns __Col in __getObject.TableColumns)
                    {
                        SMLReport._design._tableColumns __tmpCol = new _design._tableColumns();
                        __tmpCol.HeaderText = __Col.HeaderText;
                        __tmpCol.HeaderAlignment = (__Col.ContentHeaderAlign == 0) ? _convertToTextAlignment(__Col.HeaderAlignment) : __Col.ContentHeaderAlign;
                        __tmpCol.HeaderBackground = _convertStringToColor(__Col.HeaderBackground);
                        __tmpCol.ColumnsWidth = __Col.ColumnsWidth;
                        __tmpCol.Text = __Col.Text;
                        __tmpCol.FieldFormat = __Col.FieldFormat;
                        __tmpCol.FieldType = __Col.FieldType;
                        __tmpCol.TextAlignment = (__Col.ContentAlign == 0) ? _convertToTextAlignment(__Col.TextAlignment) : __Col.ContentAlign;
                        __tmpCol._showIsNumberZero = __Col.ShowIsZeroValue;
                        __tmpCol._replaceText = (__Col.ReplaceText == null) ? "" : __Col.ReplaceText;
                        __tmpCol._padding = __Col.Padding;

                        __tmpCol._printSerialNumber = __Col._printSerial;
                        __tmpCol._serialNumberColumn = __Col._serialNumberColumn;
                        __tmpCol._serialNumberDisplay = __Col._serialDiaplay;
                        __tmpCol._showSerialNewLine = __Col._showSerialNewLine;
                        __tmpCol._typeBarcode = __Col.BarcodeType;
                        __tmpCol._columnGroupType = __Col._groupType;
                        __tmpCol._autoLineBreak = __Col._autoLineBreak;


                        __tmpCol.showLotNumber = __Col._showLotNumber;
                        __tmpCol.lotFieldName = __Col._lotNumberField;
                        __tmpCol.lotGroupOperation = __Col._lotOperation;


                        foreach (_xmlDrawObjectClass __multiFieldOjbect in __Col.DrawObject)
                        {
                            __tmpCol._multiFieldCollection.Add(_setDrawObjectXML(__multiFieldOjbect));
                        }

                        __newTable.Columns.Add(__tmpCol);
                    }
                }

                if (__getObject.TableFooters.Count > 0)
                {
                    foreach (SMLReport._formReport._XmlTableFooter __footer in __getObject.TableFooters)
                    {
                        SMLReport._design._tableFooters __obj = new _design._tableFooters();
                        __obj.Text = __footer.Text;
                        __obj.TextAlignment = __footer.ContentAlign;
                        __obj._operation = __footer.Operation;
                        __obj._padding = __footer.Padding;
                        __obj._replaceText = __footer.ReplaceText;
                        __obj._showIsNumberZero = __footer.ShowIsZeroValue;
                        __obj.Background = _convertStringToColor(__footer.BackgroundColor);
                        __obj.ColumnsWidth = __footer.ColumnsWidth;
                        __obj.FieldFormat = __footer.FieldFormat;
                        __obj.FieldType = __footer.FieldType;

                        __newTable.Footers.Add(__obj);
                    }
                }

                return __newTable;
            }

            return null;
        }

        #endregion

        #region Protected Method

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.PageUp:
                    if (this._pageNameComboBox.SelectedIndex > 0)
                    {
                        this._pageNameComboBox.SelectedIndex--;
                    }
                    return true;
                case Keys.PageDown:
                    if (this._pageNameComboBox.SelectedIndex < this._pageNameComboBox.Items.Count - 1)
                    {
                        this._pageNameComboBox.SelectedIndex++;
                    }
                    return true;
                //case Keys.Delete:
                //    _objectDelete();
                //    break;
                case Keys.Control | Keys.C:
                    _objectCopy();
                    return true;
                case Keys.Control | Keys.V:
                    _objectPaste();
                    return true;
                case Keys.Control | Keys.Z:
                    _objectUndo();
                    return true;
                case Keys.Control | Keys.Y:
                    _objectRedo();
                    return true;
                case Keys.Control | Keys.A:
                    this._paper._area._graphicsList._selectAll();
                    this._paper._area.Invalidate();
                    return true;
                case Keys.Escape:
                    this._paper._area._specialText = "";
                    _paper._area._activeTool = _design._drawToolType.Pointer;
                    this._myPropertyGrid.SelectedObject = null;
                    this._paper._area._graphicsList._unselectAll();
                    this._paper._area.Invalidate();
                    return true;
                case Keys.Control | Keys.S:
                    _onClickSaveFromDesign();
                    return true;
                case Keys.Control | Keys.L:
                    _buttonLoad_Click(this, null);
                    return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        #endregion

        #region Public Method

        public SMLFormDesignXml _writeXMLSource()
        {
            return this._writeXMLSource(_writeXMLSourceOption.All);
        }

        public SMLFormDesignXml _writeXMLSource(_writeXMLSourceOption __writeOption)
        {
            SMLFormDesignXml __source = new SMLFormDesignXml();

            __source._guid = this._GUID;
            __source._formCode = this.FormCode;
            __source._formName = this.FormName;
            __source._query = this._writeFormQuery();
            __source._imageList = this._getSerializeImageList();
            __source._lastUpdate = this.LastUpdate;
            for (int __pageSave = 0; __pageSave < this._paperList.Count; __pageSave++)
            {
                _xmlPageClass __paperSave = new _xmlPageClass();
                _drawPaper __areaSave = (_drawPaper)this._paperList[__pageSave];
                __paperSave.PageSetup.Orientation = __areaSave._myPageSetup.Orientation;
                __paperSave.PageSetup.MeasurementUnit = __areaSave._myPageSetup.MeasurementUnit;
                __paperSave.PageSetup.MarginBottom = __areaSave._myPageSetup.MarginBottom;
                __paperSave.PageSetup.MarginLeft = __areaSave._myPageSetup.MarginLeft;
                __paperSave.PageSetup.MarginRight = __areaSave._myPageSetup.MarginRight;
                __paperSave.PageSetup.MarginTop = __areaSave._myPageSetup.MarginTop;
                __paperSave.PageSetup.PaperWidth = __areaSave._myPageSetup.PaperWidth;
                __paperSave.PageSetup.PaperHeight = __areaSave._myPageSetup.PaperHeight;
                __paperSave.PageSetup.PaperSize = __areaSave._myPageSetup.PaperSize;
                __paperSave.PageSetup._autoPrinterPageSize = __areaSave._myPageSetup._autoPrinterPaperSize;

                for (int __index = 0; __index < __areaSave._area._graphicsList._count; __index++)
                {
                    _xmlDrawObjectClass __newObject = new _xmlDrawObjectClass();
                    SMLReport._design._drawObject __object = (SMLReport._design._drawObject)__areaSave._area._graphicsList[__index];

                    // default property for xmlobject
                    __newObject.Color = __object._lineColor.ToString();
                    __newObject.PrintOption = __object.PringPage;

                    // get XML from Drawobject 
                    __newObject = _getXMLDrawObject(__object, __newObject);

                    __paperSave.DrawObject.Add(__newObject);

                }
                // add page background
                if (__writeOption == _writeXMLSourceOption.All)
                {
                    if (__areaSave._backgroundImage != null)
                    {
                        //__source.
                        //__paperSave._backgroundPage = MyLib._myGlobal._imageToBase64(__areaSave._backgroundImage, System.Drawing.Imaging.ImageFormat.Png);
                        __paperSave._backgroundPage = MyLib._myGlobal._imageToBase64(__areaSave._backgroundImage, System.Drawing.Imaging.ImageFormat.Jpeg);
                        __paperSave._backgroundLeftMargin = __areaSave._backgroundLeftMargin;
                        __paperSave._backgroundTopMargin = __areaSave._backgroundTopMargin;
                        __paperSave._backgroundShow = __areaSave._backgroundShow;
                    }
                }
                __source.Page.Add(__paperSave);
            }

            return __source;

        }

        public void _loadFromStream(Stream __fileStream, _openFormMethod __openFrom)
        {
            this._loadFromStream(__fileStream, null, __openFrom);
        }

        public void _loadFromStream(Stream __fileStream, Stream __bgStream, _openFormMethod __openFrom)
        {
            //try
            //{
            XmlSerializer __xsLoad = new XmlSerializer(typeof(SMLFormDesignXml));
            //FileStream __readFileStream = new FileStream(@"c:\temp\xx.xml", FileMode.Open, FileAccess.Read, FileShare.Read);
            //SMLFormDesignXml __sourceLoad = (SMLFormDesignXml)__xsLoad.Deserialize(__readFileStream);

            SMLFormDesignXml __sourceLoad = (SMLFormDesignXml)__xsLoad.Deserialize(__fileStream);

            // load BG stream
            XmlSerializer __xsBG = new XmlSerializer(typeof(BackgroundPageXML));
            BackgroundPageXML __bgPage = null;

            if (__openFrom == _openFormMethod.OpenFromServer && __bgStream != null && __bgStream.Length > 0)
            {
                __bgPage = new BackgroundPageXML();
                __bgPage = (BackgroundPageXML)__xsBG.Deserialize(__bgStream);
            }

            this._loadForm(__sourceLoad, __bgPage, __openFrom);
            // }
            //catch (Exception ex)
            // {
            //MessageBox.Show(ex.ToString());
            // }
        }

        public void _loadFormString(string xml, _openFormMethod __openFrom)
        {
            XmlSerializer __xsLoad = new XmlSerializer(typeof(SMLFormDesignXml));


            /*BackgroundPageXML __bgPage = null;

            try
            {
                if (__openFrom == _openFormMethod.OpenFromServer && xmlBackground.Length > 0)
                {
                    using (TextReader bg = new StringReader(xmlBackground))
                    {
                        XmlSerializer __xsBG = new XmlSerializer(typeof(BackgroundPageXML));

                        __bgPage = new BackgroundPageXML();
                        __bgPage = (BackgroundPageXML)__xsBG.Deserialize(bg);

                    }
                }

            }
            catch
            {
            }*/


            using (TextReader sr = new StringReader(xml))
            {
                SMLFormDesignXml __sourceLoad = (SMLFormDesignXml)__xsLoad.Deserialize(sr);
                this._loadForm(__sourceLoad, null, __openFrom);
            }

        }

        public void _loadForm(SMLFormDesignXml __sourceLoad, BackgroundPageXML __bgPage, _openFormMethod __openFrom)
        {
            //
            this._GUID = __sourceLoad._guid;

            _formGuidStatusText.Text = "";
            _formCodeStatusText.Text = "";
            _formNameStatusText.Text = "";


            if (__openFrom == _openFormMethod.OpenFromServer)
            {
                this.FormCode = __sourceLoad._formCode;
                this.FormName = __sourceLoad._formName;
                this._lastUpdateProperty = __sourceLoad._lastUpdate;

                // set status text
                _formGuidStatusText.Text = __sourceLoad._guid;
                _formCodeStatusText.Text = __sourceLoad._formCode;
                _formNameStatusText.Text = __sourceLoad._formName;
            }

            // load query
            if (__sourceLoad._query != null)
            {
                this.__queryEdit._loadQueryFromXML(__sourceLoad._query);
            }

            // load ImageList
            if (__sourceLoad._imageList != null)
            {
                this._imageList = _loadImageListFromXML(__sourceLoad._imageList);
            }


            _scaleComboBox.SelectedIndex = 3;
            this._paperListName.Clear();
            this._paperNumber = 0;
            this._paperList.Clear();
            if (__sourceLoad.Page.Count == 0)
            {
                _paperAddNewPage(0);
            }
            else
            {
                for (int __pageIndex = 0; __pageIndex < __sourceLoad.Page.Count; __pageIndex++)
                {
                    _paperAddNewPage(__pageIndex);

                    // disable panel click 
                    this._paper._area._onPanelLoaded = true;

                    this._paper._myPageSetup.Orientation = ((_xmlPageClass)__sourceLoad.Page[__pageIndex]).PageSetup.Orientation;
                    this._paper._myPageSetup.MeasurementUnit = ((_xmlPageClass)__sourceLoad.Page[__pageIndex]).PageSetup.MeasurementUnit;
                    this._paper._myPageSetup.MarginBottom = ((_xmlPageClass)__sourceLoad.Page[__pageIndex]).PageSetup.MarginBottom;
                    this._paper._myPageSetup.MarginLeft = ((_xmlPageClass)__sourceLoad.Page[__pageIndex]).PageSetup.MarginLeft;
                    this._paper._myPageSetup.MarginRight = ((_xmlPageClass)__sourceLoad.Page[__pageIndex]).PageSetup.MarginRight;
                    this._paper._myPageSetup.MarginTop = ((_xmlPageClass)__sourceLoad.Page[__pageIndex]).PageSetup.MarginTop;
                    this._paper._myPageSetup.PaperWidth = ((_xmlPageClass)__sourceLoad.Page[__pageIndex]).PageSetup.PaperWidth;
                    this._paper._myPageSetup.PaperHeight = ((_xmlPageClass)__sourceLoad.Page[__pageIndex]).PageSetup.PaperHeight;
                    this._paper._myPageSetup.PaperSize = ((_xmlPageClass)__sourceLoad.Page[__pageIndex]).PageSetup.PaperSize;
                    this._paper._myPageSetup._autoPrinterPaperSize = ((_xmlPageClass)__sourceLoad.Page[__pageIndex]).PageSetup._autoPrinterPageSize;
                    for (int __objectIndex = ((_xmlPageClass)__sourceLoad.Page[__pageIndex]).DrawObject.Count - 1; __objectIndex >= 0; __objectIndex--)
                    {
                        _xmlDrawObjectClass __getObject = (_xmlDrawObjectClass)((_xmlPageClass)__sourceLoad.Page[__pageIndex]).DrawObject[__objectIndex];

                        // add drawobject to GraphicList
                        SMLReport._design._drawObject __object = _setDrawObjectXML(__getObject);
                        __object._Owner = this;
                        this._paper._area._graphicsList._add(__object);

                    }

                    try
                    {
                        if (__openFrom == _openFormMethod.OpenFormLocal)
                        {
                            _xmlPageClass __currentPage = (_xmlPageClass)__sourceLoad.Page[__pageIndex];
                            if (__currentPage._backgroundShow && __currentPage._backgroundPage != null)
                            {
                                this._paper._backgroundImage = MyLib._myGlobal._base64ToImage(__currentPage._backgroundPage);
                                this._paper._backgroundLeftMargin = __currentPage._backgroundLeftMargin;
                                this._paper._backgroundTopMargin = __currentPage._backgroundTopMargin;
                                this._paper._backgroundShow = __currentPage._backgroundShow;
                            }
                        }

                        if (__openFrom == _openFormMethod.OpenFromServer && __bgPage != null)
                        {
                            if ((__bgPage.PageBackground[__pageIndex] != null) && (((_xmlBackground)__bgPage.PageBackground[__pageIndex])._backgroundPage != null))
                            {
                                this._paper._backgroundImage = MyLib._myGlobal._base64ToImage(((_xmlBackground)__bgPage.PageBackground[__pageIndex])._backgroundPage);
                                this._paper._backgroundLeftMargin = ((_xmlBackground)__bgPage.PageBackground[__pageIndex])._backgroundLeftMargin;
                                this._paper._backgroundTopMargin = ((_xmlBackground)__bgPage.PageBackground[__pageIndex])._backgroundTopMargin;
                                this._paper._backgroundShow = ((_xmlBackground)__bgPage.PageBackground[__pageIndex])._backgroundShow;
                            }
                        }

                    }
                    catch (ArgumentException)
                    {
                        MessageBox.Show("Current Page Background is Largest");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }

                    // enable panel click
                    this._paper._area._onPanelLoaded = false;

                }
            }
            _pageComboBoxRefresh(0);
            _query = __queryEdit._getQueryFormDesign();

            //if (__sourceLoad._backgroundShow && __sourceLoad._backgroundPage != null)
            //{
            //    this._paper._backgroundImage = MyLib._myGlobal._base64ToImage(__sourceLoad._backgroundPage);
            //    this._paper._backgroundLeftMargin = __sourceLoad._backgroundLeftMargin;
            //    this._paper._backgroundTopMargin = __sourceLoad._backgroundTopMargin;
            //    this._paper._backgroundShow = __sourceLoad._backgroundShow;
            //}
            //__readFileStream.Close();
        }

        public string[] __getQueryFieldList(_queryRule __rule)
        {
            query __tmpQuery = (query)_query.QueryLists[(int)__rule];

            if (__tmpQuery._fieldList.Count == 0)
            {
                return null;
            }
            string[] __listfield = new string[__tmpQuery._fieldList.Count];

            for (int i = 0; i < __tmpQuery._fieldList.Count; i++)
            {
                queryField __tmpField = (queryField)__tmpQuery._fieldList[i];
                __listfield[i] = __tmpField.FieldName.ToString();
            }
            //__objectTextField._defaultField = __listfield;
            return __listfield;
        }

        #endregion

        private void _menuLabelItemCode_Click(object sender, EventArgs e)
        {
            _paper._area._specialText = "&item_code&";
            _paper._area._activeTool = _design._drawToolType.SpecialLabel;

        }

        private void _menuLabelItemName_Click(object sender, EventArgs e)
        {
            _paper._area._specialText = "&item_name&";
            _paper._area._activeTool = _design._drawToolType.SpecialLabel;

        }

        private void _menuLabelItemPrice_Click(object sender, EventArgs e)
        {
            _paper._area._specialText = "&price&";
            _paper._area._activeTool = _design._drawToolType.SpecialLabel;

        }

        private void _menuLabelItemBarcode_Click(object sender, EventArgs e)
        {
            //_paper._area._specialText = "&date[dd/MM/yyyy]&";
            //_paper._area._activeTool = _design._drawToolType.SpecialLabel;
            _paper._area._specialText = "&barcode&&ean13&";
            _paper._area._activeTool = _design._drawToolType.SpecialImageField;
        }

        private void _menuLabelItemUnitCode_Click(object sender, EventArgs e)
        {
            _paper._area._specialText = "&unit_code&";
            _paper._area._activeTool = _design._drawToolType.SpecialLabel;

        }

        private void _menuLabelItemUnitName_Click(object sender, EventArgs e)
        {
            _paper._area._specialText = "&unit_name&";
            _paper._area._activeTool = _design._drawToolType.SpecialLabel;

        }

        private void _menuLabelBarcodeText_Click(object sender, EventArgs e)
        {
            _paper._area._specialText = "&barcode&";
            _paper._area._activeTool = _design._drawToolType.SpecialLabel;
        }

        private void _menuLabelItemMemberPrice_Click(object sender, EventArgs e)
        {
            _paper._area._specialText = "&member_price&";
            _paper._area._activeTool = _design._drawToolType.SpecialLabel;
        }

        private void _menuLabelBarcodeLtd_Click(object sender, EventArgs e)
        {
            _paper._area._specialText = "&barcode_ltd_name&";
            _paper._area._activeTool = _design._drawToolType.SpecialLabel;
        }

        private void _menuLabelBarcodeDescription_Click(object sender, EventArgs e)
        {
            _paper._area._specialText = "&barcode_description&";
            _paper._area._activeTool = _design._drawToolType.SpecialLabel;
        }

        private void _saveFromToServerButton_Click(object sender, EventArgs e)
        {
            _saveForm __saveForm = new _saveForm();
            __saveForm._saveFormCode.Text = this.FormCode;
            __saveForm._saveFormName.Text = this.FormName;
            __saveForm._afterSaveButtonClick += (s1, formCode, formName) =>
            {
                string __formGuid = Guid.NewGuid().ToString();
                //this.FormCode = formCode.ToString();
                //this.FormName = formName.ToString();
                //this._lastUpdateProperty = DateTime.Now.ToString();
                //_saveXMLSerialize(_saveFormMethod.SaveNewForm);
                //_formGuidStatusText.Text = this._GUID;
                //_formCodeStatusText.Text = this.FormCode;
                //_formNameStatusText.Text = this.FormName;

                // check first 
                string __checkQuery = string.Format("select count(" + _g.d.formdesign._formcode + ") as countReport from " + _g.d.formdesign._table + " where UPPER(" + _g.d.formdesign._formcode + ") = '{0}'", formCode.ToUpper());
                MyLib._myFrameWork __ws = new MyLib._myFrameWork(MyLib._myGlobal._masterWebservice, MyLib._myGlobal._masterConfigXmlName, MyLib._myGlobal._masterDatabaseType);

                DataSet __ds = __ws._query(MyLib._myGlobal._masterDatabaseName, __checkQuery);
                if (__ds.Tables.Count > 0 && __ds.Tables[0].Rows.Count > 0 && (MyLib._myGlobal._decimalPhase(__ds.Tables[0].Rows[0]["countReport"].ToString()) > 0))
                {
                    if (MessageBox.Show(string.Format("มีการใช้ Form Code : {0} ไปแล้ว ต้องการจะเขียนทับหรือไม่", formCode), "ยืนยัน", MessageBoxButtons.YesNo) == DialogResult.No)
                    {
                        return;
                    }
                }

                // delete old record
                // flush ก่อน ตัดขยะออกไป
                this.__queryEdit._Flush();

                SMLFormDesignXml __source = _writeXMLSource(_writeXMLSourceOption.DrawObjectOnly);
                //BackgroundPageXML __bgSource = _writeBackgroundPageXML();

                try
                {
                    this.Cursor = Cursors.WaitCursor;

                    XmlSerializer __xs = new XmlSerializer(typeof(SMLFormDesignXml));
                    MemoryStream __memoryStream = new MemoryStream();



                    __xs.Serialize(__memoryStream, __source);

                    XmlSerializer __xsbg = new XmlSerializer(typeof(BackgroundPageXML));
                    //MemoryStream __bgstream = new MemoryStream();
                    //__xsbg.Serialize(__bgstream, __bgSource);


                    //MyLib._myFrameWork __ws = new MyLib._myFrameWork();

                    //string __saveType = (__formSaveType == _saveFormMethod.UpdateForm) ? "0" : "1";
                    byte[] __memoryStreamCompress = MyLib._compress._compressString(MyLib._myGlobal._convertMemoryStreamToString(__memoryStream));

                    //byte[] __BGMemoryStreamCompress = MyLib._compress._compressString(MyLib._myGlobal._convertMemoryStreamToString(__bgstream));
                    //string __result = __ws._saveFormDesign(MyLib._myGlobal._databaseName, __saveType, this.FormCode.ToUpper(), this._GUID.ToUpper(), this.FormName, __memoryStreamCompress, __BGMemoryStreamCompress);
                    // jead string __result = __ws._SaveFormDesign(MyLib._myGlobal._databaseName, __saveType, this.FormCode.ToUpper(), this._GUID.ToUpper(), this.FormName, __memoryStream.ToArray(), __bgstream.ToArray());

                    MyLib._myFrameWork __fw = new MyLib._myFrameWork(MyLib._myGlobal._masterWebservice, MyLib._myGlobal._masterConfigXmlName, MyLib._myGlobal._masterDatabaseType);
                    __fw._query(MyLib._myGlobal._masterDatabaseName, string.Format("delete from " + _g.d.formdesign._table + " where UPPER(" + _g.d.formdesign._formcode + ") = '{0}'", formCode.ToUpper()));

                    string __query = string.Format("insert into " + _g.d.formdesign._table + "(" + _g.d.formdesign._formcode + "," + _g.d.formdesign._formname + ", " + _g.d.formdesign._formguid_code + ", " + _g.d.formdesign._timeupdate + "," + _g.d.formdesign._formdesigntext + ") VALUES('{0}','{1}', '{2}', '{3}',?)", formCode, formName, __formGuid, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss", new CultureInfo("en-US")));

                    string __result = __ws._queryByteData(MyLib._myGlobal._masterDatabaseName, __query, new object[] { __memoryStreamCompress });
                    if (__result.Length == 0)
                    {
                        MessageBox.Show("Save FormDesign Success");
                    }
                    else
                    {
                        MessageBox.Show(__result, "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    __memoryStream.Close();
                    //__bgstream.Close();
                    this.Cursor = Cursors.Default;
                }
                catch (Exception ex)
                {
                    this.Cursor = Cursors.Default;
                    MessageBox.Show(ex.ToString());
                }

            };
            __saveForm.ShowDialog();
        }

        private void _menuLabelItemImage_Click(object sender, EventArgs e)
        {
            _paper._area._specialText = "&itemimage&";
            _paper._area._activeTool = _design._drawToolType.SpecialImageField;

        }

    }

    public enum _saveFormMethod
    {
        SaveNewForm,
        SaveFormAs,
        UpdateForm
    }

    public enum _openFormMethod
    {
        OpenFromServer,
        OpenFormLocal,
        OpenFormStandrad
    }

    public enum _writeXMLSourceOption
    {
        DrawObjectOnly,
        All
    }

    public enum _queryRule
    {
        A,
        B,
        C,
        D,
        E,
        F,
        G,
        H,
        I
    }

}
