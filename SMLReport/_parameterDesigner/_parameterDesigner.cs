using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace SMLReport._design
{
    public partial class _designCondition : UserControl
    {
        //
        public string _resourceConditionList = "Condition List";
        _selectCover _cover;
        Control _selectControl;
        //
        public _designCondition()
        {
            InitializeComponent();
            //
            _screenPreview.Controls.Add(_cover);
            _controlListView.ItemDrag += new ItemDragEventHandler(_controlListView_ItemDrag);
            _screenPreview.DragDrop += new DragEventHandler(_screenPreview_DragDrop);
            _screenPreview.DragOver += new DragEventHandler(_screenPreview_DragOver);
            _screenPreview.ControlAdded += new ControlEventHandler(_screenPreview_ControlAdded);
            _myPropertyGrid.PropertyValueChanged += new PropertyValueChangedEventHandler(_myPropertyGrid_PropertyValueChanged);
        }

        void _myPropertyGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            if (_cover != null)
            {
                _cover.Dispose();
            }
            _selectControl.Invalidate();
            if (_selectControl.Location.X < 0)
            {
                _selectControl.Location = new Point(0, _selectControl.Location.Y);
            }
            if (_selectControl.Location.Y < 0)
            {
                _selectControl.Location = new Point(_selectControl.Location.X, 0);
            }
            _cover = new _selectCover(_selectControl);
        }


        void _screenPreview_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }

        void _controlAdd(object sender, ControlEventArgs e)
        {
            if (e.Control.GetType() != typeof(_selectCover))
            {
                Point __newLocation = ((Control)sender).PointToClient(Control.MousePosition);
                Debug.WriteLine(__newLocation.ToString() + "  " + _oldPointForMove.ToString());
                if (_oldPointForMove.X != -1)
                {
                    e.Control.Location = new Point(__newLocation.X - _oldPointForMove.X, __newLocation.Y - _oldPointForMove.Y);
                    _oldPointForMove.X = -1;
                }
                else
                {
                    e.Control.Location = __newLocation;
                }
                e.Control.ControlAdded += new ControlEventHandler(Control_ControlAdded);
                e.Control.AllowDrop = true;
                e.Control.MouseDown += new MouseEventHandler(Control_MouseDown);
                e.Control.MouseMove += new MouseEventHandler(Control_MouseMove);
                e.Control.MouseUp += new MouseEventHandler(Control_MouseUp);
                e.Control.MouseLeave += new EventHandler(Control_MouseLeave);
                e.Control.MouseClick += new MouseEventHandler(Control_MouseClick);
                e.Control.Parent.ControlRemoved += new ControlEventHandler(Parent_ControlRemoved);
            }
            e.Control.KeyDown += new KeyEventHandler(Control_KeyDown);
        }

        void Control_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData== Keys.Delete)
            {
                if (_cover != null)
                {
                    _cover._converObject.Dispose();
                    _cover.Dispose();
                }
            }
        }

        void Control_MouseClick(object sender, MouseEventArgs e)
        {
            if (_cover != null)
            {
                _cover.Dispose();
            }
            _selectControl = (Control)sender;
            _selectControl.Focus();
            _cover = new _selectCover(_selectControl);
            _selectControl.Parent.Controls.Add(_cover);
            _myPropertyGrid.SelectedObject = sender;
        }

        void Parent_ControlRemoved(object sender, ControlEventArgs e)
        {
            try
            {
                e.Control.MouseClick -= new MouseEventHandler(Control_MouseClick);
                e.Control.ControlAdded -= new ControlEventHandler(Control_ControlAdded);
                e.Control.MouseDown -= new MouseEventHandler(Control_MouseDown);
                e.Control.MouseMove -= new MouseEventHandler(Control_MouseMove);
                e.Control.MouseUp -= new MouseEventHandler(Control_MouseUp);
                e.Control.MouseLeave -= new EventHandler(Control_MouseLeave);
                e.Control.Parent.ControlRemoved -= new ControlEventHandler(Parent_ControlRemoved);
            }
            catch
            {
            }
            Debug.WriteLine("REMOVE");
        }

        void _screenPreview_ControlAdded(object sender, ControlEventArgs e)
        {
            Debug.WriteLine("_screenPreview_ControlAdded");
            _controlAdd(sender, e);
        }

        void Control_ControlAdded(object sender, ControlEventArgs e)
        {
            _controlAdd(sender, e);
        }

        void Control_MouseLeave(object sender, EventArgs e)
        {
            _mouseDownForMove = false;
        }

        void Control_MouseUp(object sender, MouseEventArgs e)
        {
            _mouseDownForMove = false;
        }

        void Control_MouseMove(object sender, MouseEventArgs e)
        {
            if (_mouseDownForMove)
            {
                if (_cover != null)
                {
                    _cover.Dispose();
                }
                Control __getControl = ((Control)sender);
                object __sender = sender;
                if (__sender.GetType() == typeof(TextBox))
                {
                    __sender = ((TextBox)__sender).Parent;
                }
                __getControl.DoDragDrop(__sender, DragDropEffects.Move);
            }
        }

        bool _mouseDownForMove = false;
        Point _oldPointForMove;
        void Control_MouseDown(object sender, MouseEventArgs e)
        {
            _oldPointForMove = ((Control)sender).PointToClient(Control.MousePosition);
            Debug.WriteLine(_oldPointForMove.ToString());
            _mouseDownForMove = true;
        }

        void _dragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(ListView).ToString()))
            {
                ListView __getListView = (ListView)e.Data.GetData(typeof(ListView));
                _oldPointForMove.X = -1;
                switch (__getListView.FocusedItem.Text)
                {
                    case "ShadowLabel": MyLib._myShadowLabel newShadowLabelControl = new MyLib._myShadowLabel();
                        newShadowLabelControl.DrawGradient = false;
                        newShadowLabelControl.Text = "ShadowLabel";
                        newShadowLabelControl.BackColor = Color.Transparent;
                        ((Control)sender).Controls.Add(newShadowLabelControl);
                        break;
                    case "Label": MyLib._myLabel newLabelControl = new MyLib._myLabel();
                        newLabelControl.Text = "Label";
                        newLabelControl.BackColor = Color.Transparent;
                        ((Control)sender).Controls.Add(newLabelControl);
                        break;
                    case "LinkLabel": LinkLabel newLinkLabelControl = new LinkLabel();
                        newLinkLabelControl.Text = "LinkLabel";
                        newLinkLabelControl.BackColor = Color.Transparent;
                        ((Control)sender).Controls.Add(newLinkLabelControl);
                        break;
                    case "Panel": MyLib._myPanel newPanelControl = new MyLib._myPanel();
                        newPanelControl.BorderStyle = BorderStyle.FixedSingle;
                        newPanelControl.Text = "Panel";
                        newPanelControl.DragEnter += new DragEventHandler(newPanelControl_DragEnter);
                        newPanelControl.DragDrop += new DragEventHandler(newPanelControl_DragDrop);
                        ((Control)sender).Controls.Add(newPanelControl);
                        break;
                    case "TextBox": MyLib._myTextBox newTextBoxControl = new MyLib._myTextBox();
                        newTextBoxControl.textBox.Visible = false;
                        newTextBoxControl.Text = "TextBox";
                        newTextBoxControl.BackColor = Color.Transparent;
                        ((Control)sender).Controls.Add(newTextBoxControl);
                        break;
                    case "DateBox": MyLib._myDateBox newDateBoxControl = new MyLib._myDateBox();
                        newDateBoxControl.textBox.Visible = false;
                        newDateBoxControl.Text = "DateBox";
                        newDateBoxControl.BackColor = Color.Transparent;
                        ((Control)sender).Controls.Add(newDateBoxControl);
                        break;
                    case "NumberBox": MyLib._myNumberBox newNumberBoxControl = new MyLib._myNumberBox();
                        newNumberBoxControl.textBox.Visible = false;
                        newNumberBoxControl.Text = "NumberBox";
                        newNumberBoxControl.BackColor = Color.Transparent;
                        ((Control)sender).Controls.Add(newNumberBoxControl);
                        break;
                    case "Button": MyLib._myButton newButtonControl = new MyLib._myButton();
                        newButtonControl.Text = "Button";
                        newButtonControl.BackColor = Color.Transparent;
                        ((Control)sender).Controls.Add(newButtonControl);
                        break;
                    case "RadioButton": MyLib._myRadioButton newRadioButtonControl = new MyLib._myRadioButton();
                        newRadioButtonControl.Text = "RadioButton";
                        newRadioButtonControl.BackColor = Color.Transparent;
                        ((Control)sender).Controls.Add(newRadioButtonControl);
                        break;
                    case "CheckBox": MyLib._myCheckBox newCheckButtonControl = new MyLib._myCheckBox();
                        newCheckButtonControl.Text = "CheckBox";
                        newCheckButtonControl.BackColor = Color.Transparent;
                        ((Control)sender).Controls.Add(newCheckButtonControl);
                        break;
                    case "GroupBox": MyLib._myGroupBox newGroupBoxControl = new MyLib._myGroupBox();
                        newGroupBoxControl.Text = "GroupBox";
                        newGroupBoxControl.DragEnter += new DragEventHandler(newGroupBoxControl_DragEnter);
                        newGroupBoxControl.DragDrop += new DragEventHandler(newGroupBoxControl_DragDrop);
                        newGroupBoxControl.BackColor = Color.Transparent;
                        ((Control)sender).Controls.Add(newGroupBoxControl);
                        break;
                    case "PictureBox": PictureBox newPictureBoxControl = new PictureBox();
                        newPictureBoxControl.BorderStyle = BorderStyle.FixedSingle;
                        newPictureBoxControl.Text = "PictureBox";
                        newPictureBoxControl.BackColor = Color.Transparent;
                        ((Control)sender).Controls.Add(newPictureBoxControl);
                        break;
                    case "Grouper": CodeVendor.Controls.Grouper newGrouperControl = new CodeVendor.Controls.Grouper();
                        newGrouperControl.GroupTitle = "Grouper";
                        newGrouperControl.BackgroundColor = System.Drawing.Color.Azure;
                        newGrouperControl.BackgroundGradientColor = System.Drawing.SystemColors.InactiveCaptionText;
                        newGrouperControl.BackgroundGradientMode = CodeVendor.Controls.Grouper.GroupBoxGradientMode.Vertical;
                        newGrouperControl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
                        newGrouperControl.BorderColor = System.Drawing.SystemColors.HotTrack;
                        newGrouperControl.BorderThickness = 1F;
                        newGrouperControl.DragEnter += new DragEventHandler(newGrouperControl_DragEnter);
                        newGrouperControl.DragDrop += new DragEventHandler(newGrouperControl_DragDrop);
                        newGrouperControl.CustomGroupBoxColor = System.Drawing.Color.Black;
                        newGrouperControl.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
                        newGrouperControl.ForeColor = System.Drawing.SystemColors.ActiveCaption;
                        newGrouperControl.GroupImage = null;
                        newGrouperControl.PaintGroupBox = false;
                        newGrouperControl.RoundCorners = 5;
                        newGrouperControl.ShadowColor = System.Drawing.Color.DarkGray;
                        newGrouperControl.ShadowControl = true;
                        newGrouperControl.ShadowThickness = 4;
                        ((Control)sender).Controls.Add(newGrouperControl);
                        break;
                }
            }
            else
            {
                bool __nextProcess = true;
                if (e.Data.GetDataPresent(typeof(MyLib._myGrid).ToString()))
                {
                    __nextProcess = false;
                }
                if (__nextProcess)
                {
                    Control __getControl = (Control)e.Data.GetData(e.Data.GetFormats()[0]);
                    ((Control)sender).Controls.Remove(__getControl);
                    Debug.WriteLine("Remove");
                    ((Control)sender).Controls.Add(__getControl);
                }
            }
        }

        void newGrouperControl_DragDrop(object sender, DragEventArgs e)
        {
            _dragDrop(sender, e);
        }

        void newGrouperControl_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        void newGroupBoxControl_DragDrop(object sender, DragEventArgs e)
        {
            _dragDrop(sender, e);
        }

        void newGroupBoxControl_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        void _screenPreview_DragDrop(object sender, DragEventArgs e)
        {
            _dragDrop(sender, e);
        }

        void newPanelControl_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Move;
        }

        void newPanelControl_DragDrop(object sender, DragEventArgs e)
        {
            _dragDrop(sender, e);
        }

        void _controlListView_ItemDrag(object sender, ItemDragEventArgs e)
        {
            _controlListView.DoDragDrop(_controlListView, DragDropEffects.All | DragDropEffects.Link);
        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
