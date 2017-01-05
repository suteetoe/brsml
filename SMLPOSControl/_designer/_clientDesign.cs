using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Crom.Controls.Docking;
using System.Xml.Serialization;
using System.IO;
using System.Collections;

namespace SMLPOSControl._designer
{
    public partial class _clientDesign : UserControl
    {
        private string _posScreenCode;
        private string _posScreenName;

        //private ContextMenuStrip _menu2 = new ContextMenuStrip();
        public _object._propertyForm _propertyForm = new _object._propertyForm();
        _object._form _t1 = new _object._form();
        private ArrayList _objectForCutAndCopy = new ArrayList();


        /// <summary>ฟอร์มล่าสุด ที่ Active</summary>
        private _object._form _activeForm;

        public _clientDesign()
        {
            InitializeComponent();
            if (MyLib._myGlobal._isDesignMode == false)
            {
                this.toolStrip1.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            }
            _dock.FormActive += new EventHandler<FormEventArgs>(_dock_FormActive);

            this.Disposed += new EventHandler(_clientDesign_Disposed);
            // add menu control
            //_buttonMenuControl.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { _toolsPosManagerMenuItem });

            //
            /*
            ToolStripMenuItem __mAddPanelNumber = new ToolStripMenuItem("Add Panel Number");
            ToolStripMenuItem __mAddNumber0 = new ToolStripMenuItem("Add Number 0");
            ToolStripMenuItem __mAddNumber1 = new ToolStripMenuItem("Add Number 1");
            ToolStripMenuItem __mAddNumber2 = new ToolStripMenuItem("Add Number 2");
            ToolStripMenuItem __mAddNumber3 = new ToolStripMenuItem("Add Number 3");
            ToolStripMenuItem __mAddNumber4 = new ToolStripMenuItem("Add Number 4");
            ToolStripMenuItem __mAddNumber5 = new ToolStripMenuItem("Add Number 5");
            ToolStripMenuItem __mAddNumber6 = new ToolStripMenuItem("Add Number 6");
            ToolStripMenuItem __mAddNumber7 = new ToolStripMenuItem("Add Number 7");
            ToolStripMenuItem __mAddNumber8 = new ToolStripMenuItem("Add Number 8");
            ToolStripMenuItem __mAddNumber9 = new ToolStripMenuItem("Add Number 9");
             * */
            //this._menu2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { __mAddPanelNumber, __mAddNumber0, __mAddNumber1, __mAddNumber2, __mAddNumber3, __mAddNumber4, __mAddNumber5, __mAddNumber6, __mAddNumber7, __mAddNumber8, __mAddNumber9 });

            // define click menu stript button
            //__mAddPanelNumber.Click += new EventHandler(__mAddPanelNumber_Click);
            //__mAddNumber0.Click += new EventHandler(__mAddNumber0_Click);
            //

            _t1.Text = "test";
            //_t1._drawPanel._afterClick += new SMLReport._design.AfterClickEventHandler(__containerForm_drawPanel__afterClick);
            _t1._drawPanel._AfterClickPosPanel += new AfterClickPosPanelEventHandler(_drawPanel__AfterClickPosPanel);
            DockableFormInfo _info1 = _dock.Add(_t1, zAllowedDock.All, Guid.NewGuid());
            _dock.ShowContextMenu += new EventHandler<FormContextMenuEventArgs>(_dock_ShowContextMenu);

            // property change from propertygrid
            _propertyForm._propertyGrid.PropertyValueChanged += new PropertyValueChangedEventHandler(_propertyGrid_PropertyValueChanged);
        }


        void _clientDesign_Disposed(object sender, EventArgs e)
        {
            _propertyForm.Dispose();
        }

        void _drawPanel__AfterClickPosPanel(object sender, object[] senderObject)
        {
            try
            {
                if (senderObject != null)
                {
                    _propertyForm._propertyGrid.SelectedObjects = senderObject;
                }
                else
                {
                    // select dock แทน
                    if (sender.GetType() == typeof(_object._form))
                    {
                        // เอา draw panel ไปแสดงใน property grid แทน
                        //_drawPanel __panel = ((_object._form)sender)._drawPanel;
                        _propertyForm._propertyGrid.SelectedObject = sender;
                    }
                }
            }
            catch
            {
            }

        }

        void _dock_FormActive(object sender, FormEventArgs e)
        {
            if (e.Form.GetType() == typeof(_object._form))
            {
                _activeForm = e.Form as _object._form;
            }

            //DockableFormInfo __info = ((DockContainer)sender).GetFormInfo(e.Form);

            //_propertyForm._propertyGrid.SelectedObject = __info.DockableForm;
        }

        void _propertyGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {

            //this._t1._drawPanel.Invalidate();
            //throw new NotImplementedException();
            if (_activeForm != null)
            {
                _activeForm._drawPanel.Invalidate();
            }

            if (((PropertyGrid)s).SelectedObject.GetType() == typeof(_object._form))
            {
                _object._form __form = (_object._form)((PropertyGrid)s).SelectedObject;
                //__form.Invalidate();

                //_dock.Invalidate();
                //this.Invalidate();
            }

            //_propertyForm._propertyGrid.SelectedObject = null;

        }

        void __mAddNumber0_Click(object sender, EventArgs e)
        {
            _designer._object._button __button = new _object._button();
            __button._actualSize = new Rectangle(this._t1._drawPanel.Width / 2, this._t1._drawPanel.Height / 2, 100, 100);
            __button._text = "0";
            __button._parentControl = this._t1._drawPanel;
            __button._ContainerControl = this;

            if (_activeForm != null)
            {
                _activeForm._drawPanel._graphicsList._add((SMLReport._design._drawObject)__button);
                _activeForm._drawPanel.Invalidate();
            }

            //this._t1._drawPanel._graphicsList._add((SMLReport._design._drawObject)__button);
            //this._t1._drawPanel.Invalidate();
        }

        void __mAddPanelNumber_Click(object sender, EventArgs e)
        {
            _designer._object._number __xx = new _designer._object._number();
            __xx._actualSize = new Rectangle(this._t1._drawPanel.Width / 2, this._t1._drawPanel.Height / 2, 100, 100);

            if (_activeForm != null)
            {
                _activeForm._drawPanel._graphicsList._add((SMLReport._design._drawObject)__xx);
                _activeForm._drawPanel.Invalidate();
            }

            /*
            this._t1._drawPanel._graphicsList._add((SMLReport._design._drawObject)__xx);
            this._t1._drawPanel.Invalidate();
             * */
        }

        void _dock_ShowContextMenu(object sender, FormContextMenuEventArgs e)
        {
            //_menu2.Show(e.Form, e.MenuLocation);
            _menu.Show(e.Form, e.MenuLocation);

        }

        private void _panelNumber_Click(object sender, EventArgs e)
        {

        }

        private void _buttonShowPropertyGrid_Click(object sender, EventArgs e)
        {
            if (_propertyForm._showState == false)
            {
                _propertyForm._showState = true;
                _propertyForm.Show(MyLib._myGlobal._mainForm);
            }
        }

        //void __containerForm_drawPanel__afterClick(object sender, object[] senderObject)
        //{
        //    try
        //    {
        //        if (senderObject != null)
        //        {
        //            _propertyForm._propertyGrid.SelectedObjects = senderObject;
        //        }
        //        else
        //        {
        //            // select dock แทน

        //        }
        //    }
        //    catch
        //    {
        //    }
        //}

        private void _menu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Tag != null)
            {
                _menuPosSelect(e.ClickedItem.Tag.ToString());
            }
        }

        private void _menuPosSelect(string __menuTag)
        {
            if (__menuTag.IndexOf("&panel_number&") != -1)
            {
                _addNumberPanel();
            }
            else if (__menuTag.IndexOf("&button_number&") != -1)
            {
                string __numberButton = __menuTag.Replace("&button_number&", string.Empty).Replace("&", string.Empty);
                _addButtonNumber(__numberButton);
            }
            else if (__menuTag.IndexOf("&sumprice&") != -1)
            {
                _addSumPriceText();
            }
            else if (__menuTag.IndexOf("&itemstable&") != -1)
            {
                _addItemsTable();
            }
            else if (__menuTag.IndexOf("&itempanel&") != -1)
            {
                _addItemPanel();
            }
            else if (__menuTag.IndexOf("&Panel&") != -1)
            {
                _addPanel();
            }
            else if (__menuTag.IndexOf("&Label&") != -1)
            {
                _addLabel();
            }
            else if (__menuTag.Equals("&Image&"))
            {
                _addImage();
            }
            else if (__menuTag.Equals("&html&"))
            {
                _addHTMLPanel();
            }
            else if (__menuTag.Equals("&button&"))
            {
                _addButton();
            }
            else if (__menuTag.Equals("&textbox&"))
            {
                _addTextBox();
            }
            else if (__menuTag.Equals("&searchlevel&"))
            {
                _addSearchLevel();
            }
        }

        private void _menuItem_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Tag != null)
            {
                _menuPosSelect(e.ClickedItem.Tag.ToString());
            }
        }

        private void _buttonAddContainner_Click(object sender, EventArgs e)
        {
            _object._form __containerForm = new _object._form();
            //__containerForm._drawPanel._afterClick += new SMLReport._design.AfterClickEventHandler(__containerForm_drawPanel__afterClick);
            __containerForm._drawPanel._AfterClickPosPanel += new AfterClickPosPanelEventHandler(_drawPanel__AfterClickPosPanel);
            DockableFormInfo _info1 = _dock.Add(__containerForm, zAllowedDock.All, Guid.NewGuid());
        }

        #region Add Control

        private void _addSearchLevel()
        {
            _object._posSearchLevel __searchPanel = new _object._posSearchLevel();
            __searchPanel._actualSize = new Rectangle(_activeForm._drawPanel.Width / 2, _activeForm._drawPanel.Height / 2, 200, 100);

            if (_activeForm != null)
            {
                _activeForm._drawPanel._graphicsList._add((SMLReport._design._drawObject)__searchPanel);
                _activeForm._drawPanel.Invalidate();
            }
        }

        private void _addItemsTable()
        {
            _designer._object._posItemsTable __table = new _object._posItemsTable();
            __table._actualSize = new Rectangle(_activeForm._drawPanel.Width / 2, _activeForm._drawPanel.Height / 2, 200, 100);

            _designer._object._posItemsTableColumn __colItemCode = new _object._posItemsTableColumn(_designer._object._posItemsFieldEnum.ItemCode, _designer._object._posItemsFieldEnum.ItemCode.ToString(), 20);
            _designer._object._posItemsTableColumn __colItemName = new _object._posItemsTableColumn(_designer._object._posItemsFieldEnum.ItemName, _designer._object._posItemsFieldEnum.ItemName.ToString(), 40);
            _designer._object._posItemsTableColumn __colItemQty = new _object._posItemsTableColumn(_designer._object._posItemsFieldEnum.Qty, _designer._object._posItemsFieldEnum.Qty.ToString(), 20);
            _designer._object._posItemsTableColumn __colItemTotal = new _object._posItemsTableColumn(_designer._object._posItemsFieldEnum.Total, _designer._object._posItemsFieldEnum.Total.ToString(), 20);

            __table._ItemsColumns.Add(__colItemCode);
            __table._ItemsColumns.Add(__colItemName);
            __table._ItemsColumns.Add(__colItemQty);
            __table._ItemsColumns.Add(__colItemTotal);

            if (_activeForm != null)
            {
                _activeForm._drawPanel._graphicsList._add((SMLReport._design._drawObject)__table);
                _activeForm._drawPanel.Invalidate();
            }
        }

        private void _addSumPriceText()
        {
            //_designer._object._posLabel __xx = new _designer._object._posLabel();
            //__xx._Tag = "&sumprice&";
            //__xx._actualSize = new Rectangle(this._t1._drawPanel.Width / 2, this._t1._drawPanel.Height / 2, 100, 100);
            //__xx._text = "#SUM PRICE#";
            //if (_activeForm != null)
            //{
            //    _activeForm._drawPanel._graphicsList._add((SMLReport._design._drawObject)__xx);
            //    _activeForm._drawPanel.Invalidate();
            //}

            _addLabel("#SUM PRICE#", "&sumprice&");
        }

        private void _addNumberPanel()
        {
            // panel 
            _designer._object._posPanel __panel = new _designer._object._posPanel();
            __panel._Tag = "&Panel&";
            __panel._actualSize = new Rectangle(this._t1._drawPanel.Width / 2, this._t1._drawPanel.Height / 2, 100, 100);

            if (_activeForm != null)
            {
                _activeForm._drawPanel._graphicsList._add((SMLReport._design._drawObject)__panel);
                _activeForm._drawPanel.Invalidate();
            }
            // button 0 - 9
            _addButtonNumber("0");
            _addButtonNumber("1");
            _addButtonNumber("2");
            _addButtonNumber("3");
            _addButtonNumber("4");
            _addButtonNumber("5");
            _addButtonNumber("6");
            _addButtonNumber("7");
            _addButtonNumber("8");
            _addButtonNumber("9");

        }

        private void _addButtonNumber(string _numberButton)
        {
            _designer._object._button __button = new _object._button();
            __button._actualSize = new Rectangle(this._t1._drawPanel.Width / 2, this._t1._drawPanel.Height / 2, 100, 100);
            __button._text = _numberButton;
            __button._textAlign = ContentAlignment.MiddleCenter;
            __button._parentControl = this._t1._drawPanel;
            __button._ContainerControl = this;
            __button._Tag = "numberPad=" + _numberButton; ;


            if (_activeForm != null)
            {
                _activeForm._drawPanel._graphicsList._add((SMLReport._design._drawObject)__button);
                _activeForm._drawPanel.Invalidate();
            }
        }

        private void _addButton()
        {
            _designer._object._button __button = new _object._button();
            __button._actualSize = new Rectangle(this._t1._drawPanel.Width / 2, this._t1._drawPanel.Height / 2, 100, 100);
            __button._text = "";
            __button._textAlign = ContentAlignment.MiddleCenter;
            __button._ContainerControl = this;


            if (_activeForm != null)
            {
                __button._parentControl = _activeForm._drawPanel;
                _activeForm._drawPanel._graphicsList._add((SMLReport._design._drawObject)__button);
                _activeForm._drawPanel.Invalidate();
            }
        }

        private void _addPanel()
        {
            _designer._object._posPanel __panel = new _designer._object._posPanel();
            __panel._Tag = "&Panel&";
            __panel._actualSize = new Rectangle(this._t1._drawPanel.Width / 2, this._t1._drawPanel.Height / 2, 100, 100);
            //__xx._text = "#SUM PRICE#";
            if (_activeForm != null)
            {
                _activeForm._drawPanel._graphicsList._add((SMLReport._design._drawObject)__panel);
                _activeForm._drawPanel.Invalidate();
            }

        }

        private void _addLabel()
        {
            _addLabel("Label", "&Label&");
        }

        private void _addLabel(String __text, String __tag)
        {
            _designer._object._posLabel __label = new _designer._object._posLabel();
            __label._Tag = __tag;
            __label._text = __text;
            __label._useShadow = true;
            __label._font = new System.Drawing.Font(__label._font.Name, 25, __label._font.Style, GraphicsUnit.Point, 222);
            __label._foreColor = Color.Blue;
            __label._actualSize = new Rectangle(this._t1._drawPanel.Width / 2, this._t1._drawPanel.Height / 2, 100, 100);

            if (_activeForm != null)
            {
                _activeForm._drawPanel._graphicsList._add((SMLReport._design._drawObject)__label);
                _activeForm._drawPanel.Invalidate();
            }
        }

        private void _addImage()
        {
            _designer._object._posImage __image = new _object._posImage();
            __image._actualSize = new Rectangle(this._t1._drawPanel.Width / 2, this._t1._drawPanel.Height / 2, 100, 100);

            if (_activeForm != null)
            {
                _activeForm._drawPanel._graphicsList._add((SMLReport._design._drawObject)__image);
                _activeForm._drawPanel.Invalidate();
            }
        }

        private void _addHTMLPanel()
        {
            _designer._object._posHTML __html = new _object._posHTML();
            __html._actualSize = new Rectangle(this._t1._drawPanel.Width / 2, this._t1._drawPanel.Height / 2, 100, 100);

            if (_activeForm != null)
            {
                _activeForm._drawPanel._graphicsList._add((SMLReport._design._drawObject)__html);
                _activeForm._drawPanel.Invalidate();
            }

        }

        private void _addItemPanel()
        {
            _object._posItemPanel __itemPanel = new _object._posItemPanel();
            __itemPanel._actualSize = new Rectangle(_activeForm._drawPanel.Width / 2, _activeForm._drawPanel.Height / 2, 200, 100);

            if (_activeForm != null)
            {
                _activeForm._drawPanel._graphicsList._add((SMLReport._design._drawObject)__itemPanel);
                _activeForm._drawPanel.Invalidate();
            }
        }

        private void _addTextBox()
        {
            _object._posTextbox __textbox = new _object._posTextbox();
            __textbox._actualSize = new Rectangle(this._t1._drawPanel.Width / 2, this._t1._drawPanel.Height / 2, 100, 100);

            if (_activeForm != null)
            {
                _activeForm._drawPanel._graphicsList._add((SMLReport._design._drawObject)__textbox);
                _activeForm._drawPanel.Invalidate();
            }

        }

        #endregion

        private void _buttonBringToFront_Click(object sender, EventArgs e)
        {
            if (_activeForm != null)
            {
                _activeForm._drawPanel._graphicsList._moveSelectionToFront();
                _activeForm._drawPanel.Invalidate();
            }
        }

        private void _buttonSendToBack_Click(object sender, EventArgs e)
        {
            if (_activeForm != null)
            {
                _activeForm._drawPanel._graphicsList._moveSelectionToBack();
                _activeForm._drawPanel.Invalidate();
            }
        }

        /// <summary>
        /// get POS Design XML จาก design 
        /// </summary>
        /// <returns></returns>
        public _posDesignXML _gertPosDesignXML()
        {
            _posDesignXML __design = new _posDesignXML();

            //int __ab = _dock.Count
            for (int __formIndex = 0; __formIndex < _dock.Count; __formIndex++)
            {
                DockableFormInfo __dockInfo = _dock.GetFormInfoAt(__formIndex);
                _object._form __formDock = (_object._form)__dockInfo.DockableForm;

                _posFormXML __formXML = new _posFormXML();
                __formXML._text = __formDock.Text;
                __formXML._dock = __dockInfo.Dock;
                __formXML._dockMode = __dockInfo.DockMode;
                __formXML._width = __formDock.Width;
                __formXML._height = __formDock.Height;

                __formXML._backColor = __formDock.BackColor.ToString();
                if (__formDock.BackgroundImage != null)
                    __formXML._backImage = MyLib._myGlobal._imageToBase64(__formDock.BackgroundImage, System.Drawing.Imaging.ImageFormat.Jpeg);
                __formXML._backImageLayout = __formDock.BackgroundImageLayout;


                // get draw object
                for (int __i = 0; __i < __formDock._drawPanel._graphicsList._count; __i++)
                {
                    SMLReport._design._drawObject __object = (SMLReport._design._drawObject)__formDock._drawPanel._graphicsList[__i];

                    _posDrawObjectXML __xmlObject = _getPOSObjectXML(__object);
                    __formXML._drawObject.Add(__xmlObject);
                }

                __design._form.Add(__formXML);
            }

            return __design;
        }

        public void _setPosDesignXML(_posDesignXML __xml)
        {
            // clear dock
            _dock.Clear();

            for (int __i = 0; __i < __xml._form.Count; __i++)
            {
                // add form
                _posFormXML __xmlForm = (_posFormXML)__xml._form[__i];

                _object._form __form = new _object._form();
                __form.Text = __xmlForm._text;
                __form.Width = __xmlForm._width;
                __form.Height = __xmlForm._height;
                try
                {
                    __form.BackColor = MyLib._myGlobal._convertStringToColor(__xmlForm._backColor);
                    if (__xmlForm._backImage != null)
                        __form.BackgroundImage = MyLib._myGlobal._base64ToImage(__xmlForm._backImage);
                    __form.BackgroundImageLayout = __xmlForm._backImageLayout;

                }
                catch
                {
                }

                // add drawobject in form
                for (int __obj = __xmlForm._drawObject.Count - 1; __obj >= 0; __obj--)
                {
                    SMLReport._design._drawObject __object = _setPOSObjectXML((_posDrawObjectXML)__xmlForm._drawObject[__obj]);
                    if (__object != null)
                    {
                        __form._drawPanel._graphicsList._add(__object);
                    }
                }

                __form._drawPanel.Invalidate();

                // add form to dock
                //__form._drawPanel._afterClick += new SMLReport._design.AfterClickEventHandler(__containerForm_drawPanel__afterClick);
                __form._drawPanel._AfterClickPosPanel += new AfterClickPosPanelEventHandler(_drawPanel__AfterClickPosPanel);

                DockableFormInfo _info1 = _dock.Add(__form, zAllowedDock.All, Guid.NewGuid());

                if (__xmlForm._dock != DockStyle.None)
                    _dock.DockForm(_info1, __xmlForm._dock, __xmlForm._dockMode);

            }
        }

        /// <summary>
        /// เอา posDrawObject ไปแปลงเป็น object ก่อนที่จะ เอาไป serizlize
        /// </summary>
        /// <param name="__object">posDrawObect</param>
        /// <returns>_posDrawObjectXML</returns>
        public _posDrawObjectXML _getPOSObjectXML(SMLReport._design._drawObject __object)
        {
            _posDrawObjectXML __posXML = new _posDrawObjectXML();

            __posXML.X = __object._actualSize.X;
            __posXML.Y = __object._actualSize.Y;
            __posXML.Width = __object._width;
            __posXML.Height = __object._height;


            if (__object.GetType() == typeof(_object._posPanel))
            {
                #region Get Object Pos Panel

                _object._posPanel __panel = (_object._posPanel)__object;
                __posXML._controlType = _posControls.Panel;
                __posXML._color1 = __panel._GradientStartColor.ToString();
                __posXML._color2 = __panel._GradientEndColor.ToString();
                __posXML.LineColor = __panel._lineColor.ToString();
                __posXML.RoundedRectangleRadius = __panel.RoundedRadius;
                __posXML.Angle = __panel._GradientAngle;
                __posXML._id = __panel._Id;
                #endregion

            }
            else if (__object.GetType() == typeof(_object._posLabel))
            {
                #region Get Object Pos Label

                _object._posLabel __label = (_object._posLabel)__object;
                __posXML._controlType = _posControls.Label;
                __posXML.Text = __label._text;
                __posXML.ContentAlign = __label._textAlign;
                __posXML.BackgroundColor = __label._backColor.ToString();
                __posXML.LineColor = __label._lineColor.ToString();
                __posXML._xOffset = __label._shadowXOffset;
                __posXML._yOffset = __label._shadowYOffset;
                __posXML.FontName = __label._font.Name;
                __posXML.FontSize = __label._font.Size;
                __posXML.FontStyle = __label._font.Style;
                __posXML.Color = __label._foreColor.ToString();
                __posXML._tag = __label._Tag;
                __posXML.showHeaderTable = __label._useShadow;
                __posXML._id = __label._Id;
                __posXML._drawShadow = __label._useShadow;

                #endregion
            }
            else if (__object.GetType() == typeof(_object._button))
            {
                #region Get Object Button

                _object._button __button = (_object._button)__object;
                __posXML._controlType = _posControls.Button;
                __posXML.FontName = __button._font.Name;
                __posXML.FontSize = __button._font.Size;
                __posXML.FontStyle = __button._font.Style;
                __posXML.Color = __button._foreColor.ToString();
                if (__button.Image != null)
                    __posXML.Image = MyLib._myGlobal._imageToBase64(__button.Image, System.Drawing.Imaging.ImageFormat.Png);

                __posXML._imageSize = __button.ImageSize;
                __posXML._imageIconAlignment = __button.ImageAlign;
                __posXML._imageSize = __button.ImageSize;
                __posXML.RoundedRectangleRadius = new SMLReport._design.RoundedRectangleRadius(__button.CornerRadius);
                __posXML._color1 = __button.HighlightColor.ToString();
                __posXML.BackgroundColor = __button._backColor.ToString();
                __posXML.LineColor = __button._lineColor.ToString();
                __posXML._color2 = __button.BaseColor.ToString();
                __posXML._color3 = __button.GlowColor.ToString();
                if (__button.BackImage != null)
                    __posXML._backImage = MyLib._myGlobal._imageToBase64(__button.BackImage, System.Drawing.Imaging.ImageFormat.Png);
                __posXML.Text = __button._text;
                __posXML.ContentAlign = __button._textAlign;
                __posXML._tag = __button._Tag;
                __posXML._id = __button._Id;
                #endregion

            }
            else if (__object.GetType() == typeof(_object._posItemsTable))
            {
                #region Get Object POS Item Table
                _object._posItemsTable __table = (_object._posItemsTable)__object;
                __posXML._controlType = _posControls.ItemsTable;
                __posXML.FontName = __table._font.Name;
                __posXML.FontSize = __table._font.Size;
                __posXML.FontStyle = __table._font.Style;
                __posXML.HeaderFontName = __table._headerFont.Name;
                __posXML.HeaderFontSize = __table._headerFont.Size;
                __posXML.HeaderFontStyle = __table._headerFont.Style;
                __posXML._buttonHeight = __table._cellHeight;

                for (int __i = 0; __i < __table._ItemsColumns.Count; __i++)
                {
                    _object._posItemsTableColumn __col = __table._ItemsColumns[__i];

                    _posItemsTableColumnXML __column = new _posItemsTableColumnXML();
                    __column.HeaderText = __col._Header;
                    __column.ContentAlign = __col.TextAlignment;
                    __column.ColumnsWidth = __col._colWidtPercentRatio;
                    __column._tag = __col._Tag;
                    __column._columnType = __col._columnType;
                    __column.FieldFormat = __col._columnFormat;

                    __posXML._posTableColumn.Add(__column);
                }

                __posXML._id = __table._Id;

                #endregion
            }
            else if (__object.GetType() == typeof(_object._posItemPanel))
            {
                #region Get Object POS Item Panel Button

                _object._posItemPanel __itemPanel = (_object._posItemPanel)__object;

                __posXML._controlType = _posControls.ItemButtonPanel;
                __posXML.Padding = __itemPanel._padding;
                __posXML.BackgroundColor = __itemPanel._backColor.ToString();

                // button inner
                __posXML.FontName = __itemPanel._font.Name;
                __posXML.FontSize = __itemPanel._font.Size;
                __posXML.FontStyle = __itemPanel._font.Style;
                __posXML.Color = __itemPanel._foreColor.ToString();
                __posXML._buttonWidth = __itemPanel._buttonWidth;
                __posXML._buttonHeight = __itemPanel._buttonHeight;
                __posXML._buttonMargin = __itemPanel._buttonMargin;
                __posXML._color1 = __itemPanel.HighlightColor.ToString();
                __posXML.LineColor = __itemPanel._lineColor.ToString();
                __posXML._color2 = __itemPanel.BaseColor.ToString();
                __posXML._color3 = __itemPanel.GlowColor.ToString();
                __posXML._color4 = __itemPanel._buttonBackColor.ToString();
                __posXML.ContentAlign = __itemPanel._textAlign;
                __posXML.RoundedRectangleRadius = new SMLReport._design.RoundedRectangleRadius(__itemPanel._buttonCornerRadius);
                __posXML._id = __itemPanel._Id;
                #endregion
            }
            else if (__object.GetType() == typeof(_object._posImage))
            {
                #region Get Object Image

                _object._posImage __image = (_object._posImage)__object;

                __posXML._controlType = _posControls.Image;
                __posXML.BorderStyle = __image.BorderStyle;
                __posXML.LineColor = __image._lineColor.ToString();
                __posXML.PictureBoxSizeMode = __image.SizeMode;
                __posXML.Opacity = __image.Opacity.ToString();
                __posXML.RotateFlip = __image.RotateFlip;
                __posXML.Angle = __image.Angle;
                if (__image.Image != null)
                {
                    __posXML.Image = MyLib._myGlobal._imageToBase64(__image.Image, System.Drawing.Imaging.ImageFormat.Jpeg);
                }
                __posXML._id = __image._Id;

                #endregion
            }
            else if (__object.GetType() == typeof(_object._posHTML))
            {
                #region Get Object Html

                _object._posHTML __html = (_object._posHTML)__object;
                __posXML._controlType = _posControls.HTML;
                __posXML.Text = __html._contentURL;
                __posXML._id = __html._Id;
                __posXML._scrollBarEnable = __html._scrollBarEnable;
                __posXML.Value = __html._htmlSchema;
                #endregion
            }
            else if (__object.GetType() == typeof(_object._posTextbox))
            {
                #region Get Object Textbox

                _object._posTextbox __textbox = (_object._posTextbox)__object;
                __posXML._controlType = _posControls.TextBox;
                __posXML._tag = __textbox._Tag;
                __posXML.FontName = __textbox._font.Name;
                __posXML.FontSize = __textbox._font.Size;
                __posXML.FontStyle = __textbox._font.Style;
                __posXML.ContentAlign = __textbox._textAlign;
                __posXML._tag = __textbox._Tag;
                __posXML._id = __textbox._Id;

                __posXML.BackgroundColor = __textbox._backColor.ToString();
                __posXML.Color = __textbox._foreColor.ToString();

                #endregion
            }
            else if (__object.GetType() == typeof(_object._posSearchLevel))
            {
                _object._posSearchLevel __search = (_object._posSearchLevel)__object;

                __posXML._controlType = _posControls.SearchLevel;
                __posXML.Padding = __search._padding;
                __posXML.BackgroundColor = __search._backColor.ToString();

                // button inner
                __posXML.FontName = __search._font.Name;
                __posXML.FontSize = __search._font.Size;
                __posXML.FontStyle = __search._font.Style;
                __posXML.Color = __search._foreColor.ToString();
                __posXML._buttonWidth = __search._buttonWidth;
                __posXML._buttonHeight = __search._buttonHeight;
                __posXML._buttonMargin = __search._buttonMargin;
                __posXML._color1 = __search.HighlightColor.ToString();
                __posXML.LineColor = __search._lineColor.ToString();
                __posXML._color2 = __search.BaseColor.ToString();
                __posXML._color3 = __search.GlowColor.ToString();
                __posXML._color4 = __search._buttonBackColor.ToString();
                __posXML.ContentAlign = __search._textAlign;
                __posXML.RoundedRectangleRadius = new SMLReport._design.RoundedRectangleRadius(__search._buttonCornerRadius);
                __posXML._id = __search._Id;
            }


            return __posXML;
        }

        public SMLReport._design._drawObject _setPOSObjectXML(_posDrawObjectXML __xml)
        {
            if (__xml._controlType == _posControls.Panel)
            {
                #region Set Object Panel
                _object._posPanel __panel = new _object._posPanel();
                __panel._actualSize = new Rectangle(__xml.X, __xml.Y, __xml.Width, __xml.Height);
                __panel._GradientStartColor = MyLib._myGlobal._convertStringToColor(__xml._color1);
                __panel._GradientEndColor = MyLib._myGlobal._convertStringToColor(__xml._color2);
                __panel._lineColor = MyLib._myGlobal._convertStringToColor(__xml.LineColor);
                __panel.RoundedRadius = __xml.RoundedRectangleRadius;
                __panel._GradientAngle = (int)__xml.Angle;
                __panel._Id = __xml._id;
                return __panel;
                #endregion
            }
            else if (__xml._controlType == _posControls.Label)
            {
                #region Set object Label

                _object._posLabel __label = new _object._posLabel();
                __label._actualSize = new Rectangle(__xml.X, __xml.Y, __xml.Width, __xml.Height);
                try
                {
                    __label._font = new Font(__xml.FontName, __xml.FontSize, __xml.FontStyle, GraphicsUnit.Point, 222);
                }
                catch
                {
                }
                __label._useShadow = __xml.showHeaderTable;
                __label._text = __xml.Text;
                __label._textAlign = __xml.ContentAlign;
                __label._backColor = MyLib._myGlobal._convertStringToColor(__xml.BackgroundColor);
                __label._lineColor = MyLib._myGlobal._convertStringToColor(__xml.LineColor);
                __label._shadowXOffset = __xml._xOffset;
                __label._shadowYOffset = __xml._yOffset;
                __label._foreColor = MyLib._myGlobal._convertStringToColor(__xml.Color);
                __label._Tag = __xml._tag;
                __label._Id = __xml._id;
                return __label;

                #endregion
            }
            else if (__xml._controlType == _posControls.Button)
            {
                #region Set Object Button

                _object._button __button = new _object._button();
                __button._actualSize = new Rectangle(__xml.X, __xml.Y, __xml.Width, __xml.Height);
                try
                {
                    __button._font = new Font(__xml.FontName, __xml.FontSize, __xml.FontStyle, GraphicsUnit.Point, 222);
                }
                catch
                {
                }

                __button._foreColor = MyLib._myGlobal._convertStringToColor(__xml.Color);
                if (__xml.Image != null)
                    __button.Image = MyLib._myGlobal._base64ToImage(__xml.Image);
                __button.ImageSize = __xml._imageSize;
                __button.ImageAlign = __xml._imageIconAlignment;
                __button.CornerRadius = __xml.RoundedRectangleRadius.All;
                __button.HighlightColor = MyLib._myGlobal._convertStringToColor(__xml._color1);
                __button._backColor = MyLib._myGlobal._convertStringToColor(__xml.BackgroundColor);
                __button._lineColor = MyLib._myGlobal._convertStringToColor(__xml.LineColor);
                __button.BaseColor = MyLib._myGlobal._convertStringToColor(__xml._color2);
                __button.GlowColor = MyLib._myGlobal._convertStringToColor(__xml._color3);
                if (__xml._backImage != null)
                    __button.BackImage = MyLib._myGlobal._base64ToImage(__xml._backImage);
                __button._text = __xml.Text;
                __button._textAlign = __xml.ContentAlign;
                __button._Tag = __xml._tag;
                __button._Id = __xml._id;

                return __button;

                #endregion
            }
            else if (__xml._controlType == _posControls.ItemsTable)
            {
                #region Set Object Item Table

                _object._posItemsTable __table = new _object._posItemsTable();
                __table._actualSize = new Rectangle(__xml.X, __xml.Y, __xml.Width, __xml.Height);

                try
                {
                    __table._font = new Font(__xml.FontName, __xml.FontSize, __xml.FontStyle, GraphicsUnit.Point, 222);
                    __table._headerFont = new Font(__xml.HeaderFontName, __xml.HeaderFontSize, __xml.HeaderFontStyle, GraphicsUnit.Point, 222);
                }
                catch
                {
                }

                for (int __i = 0; __i < __xml._posTableColumn.Count; __i++)
                {
                    _posItemsTableColumnXML __col = (_posItemsTableColumnXML)__xml._posTableColumn[__i];

                    _object._posItemsTableColumn __column = new _object._posItemsTableColumn();
                    __column._Header = __col.HeaderText;
                    __column.TextAlignment = __col.ContentAlign;
                    __column._columnWidth = __col.ColumnsWidth;
                    __column._Tag = __col._tag;
                    __column._columnFormat = __col.FieldFormat;
                    __column._columnType = __col._columnType;

                    __table._ItemsColumns.Add(__column);
                }
                __table._Id = __xml._id;

                return __table;

                #endregion
            }
            else if (__xml._controlType == _posControls.ItemButtonPanel)
            {
                #region Set Object Item Button Panel

                _object._posItemPanel __itemPanel = new _object._posItemPanel();
                __itemPanel._actualSize = new Rectangle(__xml.X, __xml.Y, __xml.Width, __xml.Height);
                __itemPanel._padding = __xml.Padding;
                __itemPanel._backColor = MyLib._myGlobal._convertStringToColor(__xml.BackgroundColor);

                // button inner property
                try
                {
                    __itemPanel._font = new Font(__xml.FontName, __xml.FontSize, __xml.FontStyle, GraphicsUnit.Point, 222);
                }
                catch
                {
                }

                __itemPanel._foreColor = MyLib._myGlobal._convertStringToColor(__xml.Color);
                __itemPanel._buttonWidth = __xml._buttonWidth;
                __itemPanel._buttonHeight = __xml._buttonHeight;
                __itemPanel._buttonMargin = __xml._buttonMargin;
                __itemPanel.HighlightColor = MyLib._myGlobal._convertStringToColor(__xml._color1);
                __itemPanel._lineColor = MyLib._myGlobal._convertStringToColor(__xml.LineColor);
                __itemPanel.BaseColor = MyLib._myGlobal._convertStringToColor(__xml._color2);
                __itemPanel.GlowColor = MyLib._myGlobal._convertStringToColor(__xml._color3);
                __itemPanel._buttonBackColor = MyLib._myGlobal._convertStringToColor(__xml._color4);
                __itemPanel._textAlign = __xml.ContentAlign;
                __itemPanel._buttonCornerRadius = __xml.RoundedRectangleRadius.All;
                __itemPanel._Id = __xml._id;
                return __itemPanel;

                #endregion
            }
            else if (__xml._controlType == _posControls.Image)
            {
                #region Set Object Image

                _object._posImage __image = new _object._posImage();
                __image._actualSize = new Rectangle(__xml.X, __xml.Y, __xml.Width, __xml.Height);
                __image.BorderStyle = __xml.BorderStyle;

                __image._lineColor = MyLib._myGlobal._convertStringToColor(__xml.LineColor);
                __image._backColor = MyLib._myGlobal._convertStringToColor(__xml.BackgroundColor);
                __image.Angle = __xml.Angle;
                __image.Opacity = MyLib._myGlobal._intPhase(__xml.Opacity);
                __image.RotateFlip = __xml.RotateFlip;
                __image.SizeMode = __xml.PictureBoxSizeMode;

                if (__xml.Image != null)
                {
                    __image.Image = MyLib._myGlobal._base64ToImage(__xml.Image);
                }

                __image._Id = __xml._id;
                return __image;

                #endregion
            }
            else if (__xml._controlType == _posControls.HTML)
            {
                #region Set Object HTML

                _object._posHTML __html = new _object._posHTML();
                __html._actualSize = new Rectangle(__xml.X, __xml.Y, __xml.Width, __xml.Height);
                __html._contentURL = __xml.Text;
                __html._Id = __xml._id;
                __html._scrollBarEnable = __xml._scrollBarEnable;
                __html._htmlSchema = __xml.Value;
                return __html;

                #endregion
            }
            else if (__xml._controlType == _posControls.TextBox)
            {
                _object._posTextbox __textbox = new _object._posTextbox();
                __textbox._actualSize = new Rectangle(__xml.X, __xml.Y, __xml.Width, __xml.Height);

                // button inner property
                try
                {
                    __textbox._font = new Font(__xml.FontName, __xml.FontSize, __xml.FontStyle, GraphicsUnit.Point, 222);
                }
                catch
                {
                }
                __textbox._backColor = MyLib._myGlobal._convertStringToColor(__xml.BackgroundColor);
                if (__xml.Color == null)
                    __textbox._foreColor = Color.Black;
                else
                    __textbox._foreColor = MyLib._myGlobal._convertStringToColor(__xml.Color);
                __textbox._textAlign = __xml.ContentAlign;
                __textbox._Tag = __xml._tag;
                __textbox._Id = __xml._id;

                return __textbox;
            }
            else if (__xml._controlType == _posControls.SearchLevel)
            {
                _object._posSearchLevel __searchLevel = new _object._posSearchLevel();
                __searchLevel._actualSize = new Rectangle(__xml.X, __xml.Y, __xml.Width, __xml.Height);
                __searchLevel._padding = __xml.Padding;
                __searchLevel._backColor = MyLib._myGlobal._convertStringToColor(__xml.BackgroundColor);

                // button inner property
                try
                {
                    __searchLevel._font = new Font(__xml.FontName, __xml.FontSize, __xml.FontStyle, GraphicsUnit.Point, 222);
                }
                catch
                {
                }

                __searchLevel._foreColor = MyLib._myGlobal._convertStringToColor(__xml.Color);
                __searchLevel._buttonWidth = __xml._buttonWidth;
                __searchLevel._buttonHeight = __xml._buttonHeight;
                __searchLevel._buttonMargin = __xml._buttonMargin;
                __searchLevel.HighlightColor = MyLib._myGlobal._convertStringToColor(__xml._color1);
                __searchLevel._lineColor = MyLib._myGlobal._convertStringToColor(__xml.LineColor);
                __searchLevel.BaseColor = MyLib._myGlobal._convertStringToColor(__xml._color2);
                __searchLevel.GlowColor = MyLib._myGlobal._convertStringToColor(__xml._color3);
                __searchLevel._buttonBackColor = MyLib._myGlobal._convertStringToColor(__xml._color4);
                __searchLevel._textAlign = __xml.ContentAlign;
                __searchLevel._buttonCornerRadius = __xml.RoundedRectangleRadius.All;
                __searchLevel._Id = __xml._id;
                return __searchLevel;

            }

            return null;
        }

        public void _savedata()
        {
            // ตรวจสอบ ซ้ำ
            string __checkQuery = string.Format("select count(" + _g.d.sml_posdesign._table + ") as countReport from " + _g.d.sml_posdesign._table + " where UPPER(" + _g.d.sml_posdesign._screen_code + ") = '{0}'", this._posScreenCode.ToUpper());
            MyLib._myFrameWork __ws = new MyLib._myFrameWork();

            DataSet __ds = __ws._query(MyLib._myGlobal._databaseName, __checkQuery);
            if (__ds.Tables.Count > 0 && __ds.Tables[0].Rows.Count > 0 && (MyLib._myGlobal._decimalPhase(__ds.Tables[0].Rows[0]["countReport"].ToString()) > 0))
            {
                MessageBox.Show(string.Format("มีการใช้ Menuid : {0} ไปแล้ว", this._posScreenCode));
                this._posScreenCode = null;
                this._posScreenName = null;
                return;
            }

            // get xml serialsize
            _posDesignXML __design = _gertPosDesignXML();

            //serialize 
            XmlSerializer __xs = new XmlSerializer(typeof(_posDesignXML));
            MemoryStream __memoryStream = new MemoryStream();
            __xs.Serialize(__memoryStream, __design);

            string _query = string.Format("insert into " + _g.d.sml_posdesign._table + "(" + _g.d.sml_posdesign._screen_code + "," + _g.d.sml_posdesign._screen_name + "," + _g.d.sml_posdesign._screen_data + ") VALUES('{0}','{1}',?)", this._posScreenCode, this._posScreenName);

            byte[] __memoryStreamCompress = MyLib._compress._compressString(MyLib._myGlobal._convertMemoryStreamToString(__memoryStream));

            string __result = __ws._queryByteData(MyLib._myGlobal._databaseName, _query, new object[] { __memoryStreamCompress });
            if (__result.Equals(""))
            {
                MessageBox.Show("บันทึกข้อมูลเรียบร้อย");
            }
            else
            {
                MessageBox.Show(__result, "wraning");
            }

        }

        public void _loaddata()
        {
            string __query = string.Format("select " + _g.d.sml_posdesign._screen_data + " from " + _g.d.sml_posdesign._table + " where UPPER(" + _g.d.sml_posdesign._screen_code + ") = '{0}'", this._posScreenCode.ToUpper());

            MyLib._myFrameWork __fw = new MyLib._myFrameWork();
            byte[] __getByte = __fw._queryByte(MyLib._myGlobal._databaseName, __query);

            try
            {
                MemoryStream __ms = new MemoryStream(MyLib._compress._decompressBytes((byte[])__getByte));
                XmlSerializer __xs = new XmlSerializer(typeof(_posDesignXML));
                _posDesignXML __xml = (_posDesignXML)__xs.Deserialize(__ms);

                // set screen pos from xml
                _setPosDesignXML(__xml);

                __ms.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                this._posScreenCode = null;
                this._posScreenName = null;
            }

        }

        private void _buttonLoadDesign_Click(object sender, EventArgs e)
        {
            MyLib._searchDataFull __search = new MyLib._searchDataFull();
            __search.Text = "Load Pos Screen";
            __search._dataList._buttonClose.Click += (s1, e1) => __search.Dispose();
            __search.StartPosition = FormStartPosition.CenterScreen;
            __search.Size = new System.Drawing.Size(480, 420);
            __search._dataList._gridData._isEdit = false;
            __search._dataList._lockRecord = true;
            __search._dataList._loadViewFormat("screen_posdesign_loaddesign", MyLib._myGlobal._userSearchScreenGroup, false);
            __search._dataList._referFieldAdd(_g.d.sml_posdesign._screen_code, 1);
            __search._dataList._gridData._mouseDoubleClick += (s1, e1) =>
            {
                string __code = ((MyLib._myGrid)s1)._cellGet(e1._row, 0).ToString();
                string __name = ((MyLib._myGrid)s1)._cellGet(e1._row, 1).ToString();

                this._posScreenCode = __code;
                this._posScreenName = __name;
                __search.Dispose();
                _loaddata();
            };

            __search.ShowDialog();

        }

        private void _buttonSaveDesign_Click(object sender, EventArgs e)
        {
            if (_posScreenCode == null)
            {
                _posSaveScreen __save = new _posSaveScreen();
                __save.Text = "Save Pos Screen";
                __save._beforeClose += (s1, e1) =>
                {
                    if (__save.DialogResult == DialogResult.Yes)
                    {
                        string __screenCode = __save._dialogScreen._getDataStr(_g.d.sml_posdesign._screen_code);
                        string __screenName = __save._dialogScreen._getDataStr(_g.d.sml_posdesign._screen_name);

                        if (__screenCode != null && __screenName != null)
                        {
                            this._posScreenCode = __screenCode;
                            this._posScreenName = __screenName;

                            string _query = string.Format("delete from " + _g.d.sml_posdesign._table + " where upper(" + _g.d.sml_posdesign._screen_code + ") = '{0}'", this._posScreenCode.ToUpper());

                            MyLib._myFrameWork __fw = new MyLib._myFrameWork();
                            __fw._query(MyLib._myGlobal._databaseName, _query);

                            _savedata();
                        }
                    }
                };
                __save.ShowDialog();
            }
            else
            {
                string _query = string.Format("delete from " + _g.d.sml_posdesign._table + " where upper(" + _g.d.sml_posdesign._screen_code + ") = '{0}'", this._posScreenCode.ToUpper());

                MyLib._myFrameWork __fw = new MyLib._myFrameWork();
                __fw._query(MyLib._myGlobal._databaseName, _query);

                _savedata();
            }
        }

        private void _buttonClose_Click(object sender, EventArgs e)
        {
            // warning save

            this.Dispose();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            _posSaveScreen __save = new _posSaveScreen();
            __save.Text = "Save Pos Screen";
            __save._beforeClose += (s1, e1) =>
            {
                if (__save.DialogResult == DialogResult.Yes)
                {
                    string __screenCode = __save._dialogScreen._getDataStr(_g.d.sml_posdesign._screen_code);
                    string __screenName = __save._dialogScreen._getDataStr(_g.d.sml_posdesign._screen_name);

                    if (__screenCode != null && __screenName != null)
                    {
                        this._posScreenCode = __screenCode;
                        this._posScreenName = __screenName;

                        _savedata();
                    }
                }
            };
            __save.ShowDialog();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Control | Keys.C:
                    _objectCopy();
                    return true;
                case Keys.Control | Keys.V:
                    _objectPaste();
                    return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        public void _objectCopy()
        {
            if (_activeForm != null)
            {
                int __selectionCount = _activeForm._drawPanel._graphicsList._selectionCount;
                if (__selectionCount > 0)
                {
                    this._objectForCutAndCopy = new ArrayList();
                    for (int __index = 0; __index < __selectionCount; __index++)
                    {
                        this._objectForCutAndCopy.Add(_activeForm._drawPanel._graphicsList._getSelectedObject(__index)._clone());
                    }
                }
                _activeForm._drawPanel._drawNetRectangle = false;
                _activeForm._drawPanel.Invalidate();
            }
        }

        public void _objectPaste()
        {
            if (_activeForm != null)
            {
                if (this._objectForCutAndCopy.Count > 0)
                {
                    //this._paper._objectForUndoAndRedoAdd();
                    _activeForm._drawPanel._graphicsList._unselectAll();
                    ArrayList __objectPaste = new ArrayList();


                    for (int __loop = this._objectForCutAndCopy.Count - 1; __loop >= 0; __loop--)
                    {
                        SMLReport._design._drawObject __newObject = (SMLReport._design._drawObject)((SMLReport._design._drawObject)this._objectForCutAndCopy[__loop])._clone();
                        _activeForm._drawPanel._graphicsList._add(__newObject);

                        __objectPaste.Add(__newObject);
                    }

                    //_myPropertyGrid.SelectedObjects = null;
                    //_myPropertyGrid.SelectedObjects = (object[])__objectPaste.ToArray();
                }
                _activeForm._drawPanel._drawNetRectangle = false;
                _activeForm._drawPanel.Invalidate();
            }

        }

        private void _buttonAlignToLeft_Click(object sender, EventArgs e)
        {
            _activeForm._drawPanel._graphicsList._alignSelectionLeftEdges();
            _activeForm._drawPanel.Invalidate();
        }

        private void _buttonAlignToTop_Click(object sender, EventArgs e)
        {
            _activeForm._drawPanel._graphicsList._alignSelectionTopEdges();
            _activeForm._drawPanel.Invalidate();
        }

        private void _buttonAlignToRight_Click(object sender, EventArgs e)
        {
            _activeForm._drawPanel._graphicsList._alignRightSelectionEdges();
            _activeForm._drawPanel.Invalidate();

        }

        private void _buttonAlignToBottom_Click(object sender, EventArgs e)
        {
            _activeForm._drawPanel._graphicsList._alignBottomSelectionEdges();
            _activeForm._drawPanel.Invalidate();

        }

        private void _loadDesignServerButton_Click(object sender, EventArgs e)
        {
            _loadStandardDesign __loadMaster = new _loadStandardDesign();
            __loadMaster._afterSelect += (s1, e1) =>
            {
                MyLib._myFrameWork __fw = new MyLib._myFrameWork(MyLib._myGlobal._masterWebservice, MyLib._myGlobal._masterConfigXmlName, MyLib._myGlobal._databaseType.PostgreSql);

                string __query = "select " + _g.d.sml_posdesign._screen_data + " from " + _g.d.sml_posdesign._table + " where " + MyLib._myGlobal._addUpper(_g.d.sml_posdesign._screen_code) + " = '" + e1.ToUpper() + "'";

                byte[] __result = __fw._queryByte(MyLib._myGlobal._masterDatabaseName, __query);

                try
                {
                    MemoryStream __ms = new MemoryStream(MyLib._compress._decompressBytes((byte[])__result));
                    XmlSerializer __xs = new XmlSerializer(typeof(_posDesignXML));
                    _posDesignXML __xml = (_posDesignXML)__xs.Deserialize(__ms);

                    // set screen pos from xml
                    _setPosDesignXML(__xml);

                    __ms.Close();

                }
                catch (Exception ex)
                {
                }


            };

            __loadMaster.ShowDialog(MyLib._myGlobal._mainForm);
        }

        private void _deleteDesignButton_Click(object sender, EventArgs e)
        {
            _deletePOSDesign __deleteForm = new _deletePOSDesign();
            __deleteForm.ShowDialog(MyLib._myGlobal._mainForm);
        }

        private void _saveXMLButton_Click(object sender, EventArgs e)
        {
            _posDesignXML __design = _gertPosDesignXML();

            SaveFileDialog __objFile = new SaveFileDialog() { DefaultExt = "xml", Filter = "XML Files (*.xml)|*.xml|All files (*.*)|*.*", FilterIndex = 0 };
            if (__objFile.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    XmlSerializer __xs = new XmlSerializer(typeof(_posDesignXML));

                    TextWriter __memoryStream = new StreamWriter(__objFile.OpenFile());
                    __xs.Serialize(__memoryStream, __design);
                    __memoryStream.Close();

                    MessageBox.Show("Save XML Success");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }

        }

        private void _loadXMLButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog __openFile = new OpenFileDialog() { DefaultExt = "xml", Filter = "XML Files (*.xml)|*.xml|All files (*.*)|*.*", FilterIndex = 0 };
            if (__openFile.ShowDialog() == DialogResult.OK)
            {
                Stream __readFileStream = __openFile.OpenFile();
                XmlSerializer __xs = new XmlSerializer(typeof(_posDesignXML));
                _posDesignXML __xml = (_posDesignXML)__xs.Deserialize(__readFileStream);

                // set screen pos from xml
                _setPosDesignXML(__xml);
                __readFileStream.Close();
            }

        }


    }
}
