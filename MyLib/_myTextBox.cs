using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace MyLib
{
    public partial class _myTextBox : UserControl
    {
        public object _tempValue1 = null;
        /// <summary>ตำแหน่ง Row ของหน้าจอ (ในกรณีใช้ routine screen)</summary>
        public int _row { get { return _rowResult; } set { _rowResult = value; } }
        private int _rowResult;
        /// <summary>ตำแหน่ง Column ของหน้าจอ (ในกรณีใช้ routine screen)</summary>
        public int _column { get { return _columnResult; } set { _columnResult = value; } }
        private int _columnResult;
        /// <summary>ชื่อของ TextBox</summary>
        public string _name { get { return _nameResult; } set { _nameResult = value; } }
        private string _nameResult;
        /// <summary>true=แสดง Icon,false=ไม่แสดง</summary>
        public bool _icon { get { return _iconResult; } set { _iconResult = value; } }
        private bool _iconResult = false;
        /// <summary>จำนวน Column เอาไว้ให้ routine screen ใช้</summary>
        public int _maxColumn { get { return _maxColumnResult; } set { _maxColumnResult = value; } }
        private int _maxColumnResult;
        /// <summary>จำนวนบรรทัด ถ้ามีมากกว่า 1 ก็จะป้อนได้หลายบรรทัด</summary>
        public int _rowCount { get { return _rowCountResult; } set { _rowCountResult = value; } }
        private int _rowCountResult = 0;
        /// <summary>ยอมให้ว่างหรือไม่</summary>
        public Boolean _emtry { get { return _emtryResult; } set { _emtryResult = value; } }
        private Boolean _emtryResult = true;
        /// <summary>ปุ่มค้นหาหรือไม่</summary>
        public Boolean _isSearch { get { return _isSearchResult; } set { _isSearchResult = value; } }
        private Boolean _isSearchResult = false;
        /// <summary>เป็นของ Database (เอาไปใช้ตอน Query)</summary>
        public Boolean _isQuery { get { return _isQueryResult; } set { _isQueryResult = value; } }
        private Boolean _isQueryResult = true;
        public Boolean _isGetData { get { return _isGetDataResult; } set { _isGetDataResult = value; } }
        private Boolean _isGetDataResult = false;
        /// <summary>ความยาวของ Text</summary>
        public int MaxLength { get { return MaxLengthResult; } set { MaxLengthResult = value; } }
        private int MaxLengthResult;
        /// <summary>มีการแก้ไข Text</summary>
        public Boolean _isChange { get { return _isChangeResult; } set { _isChangeResult = value; } }
        private Boolean _upperCaseResult = false;
        /// <summary>กำหนดให้เป็นตัวใหญ่เท่านั้น</summary>
        public Boolean _upperCase { get { return _upperCaseResult; } set { _upperCaseResult = value; } }
        private Boolean _isChangeResult = false;
        /// <summary>
        /// ให้จำข้อมูลล่าสุด
        /// </summary>
        public Boolean _useRecentValue = false;
        /// <summary>
        /// 0=แสดง Icon ด้านขวา,1=แสดง Icon ด้านซ้าย
        /// </summary>
        public int _iconType = 0;
        public _myLabel _label;
        /// <summary>
        /// เก็บค่าเก่าไว้
        /// </summary>
        public string _oldValue;
        /// <summary>
        /// แสดง Label ด้านหน้า
        /// </summary>
        public bool _displayLabel = true;
        /// <summary>
        /// ปรับให้เป็นตัวใหญ่ (สำหรับรหัส)
        /// </summary>
        public Boolean IsUpperCase { get { return IsUpperCaseResult; } set { IsUpperCaseResult = value; } }
        private Boolean IsUpperCaseResult = false;
        private Boolean _isTimeResult = false;
        /// <summary>
        /// เป็นการป้อนรูปแบบเวลาหรือไม่
        /// </summary>
        public Boolean _isTime
        {
            get
            {
                return this._isTimeResult;
            }
            set
            {
                this._isTimeResult = value;
            }
        }

        /// <summary>ตัวแปรจริง</summary>
        public string _textFirst
        {
            get
            {
                return _textFirstResult;
            }
            set
            {
                _textFirstResult = value;
                this.textBox.Text = value;
            }
        }
        private string _textFirstResult = "";
        /// <summary>ตัวแปรแสดงเสริม</summary>
        public string _textSecond
        {
            get
            {
                return _textSecondResult;
            }
            set
            {
                _textSecondResult = value;
            }
        }
        private string _textSecondResult = "";
        /// <summary>
        /// รูปแบบ Icon (1=ปุ่มเดียว,2=2 ปุ่มคู่กัน)
        /// </summary>
        private int _iconNumberResult = 1;
        /// <summary>
        /// ค่าเดิม เอาไว้ตรวจสอบว่ามีการแก้ไขหรือไม่
        /// </summary>
        public string _textLast { get { return _textLastResult; } set { _textLastResult = value; } }
        private string _textLastResult = "";
        public bool _enterToTab { get { return _enterToTabResult; } set { _enterToTabResult = value; } }
        private bool _enterToTabResult = false;
        public string _labelName { get { return _labelNameResult; } set { _labelNameResult = value; } }
        private string _labelNameResult = "";
        public string _searchFormat = "";
        public Color _defaultBackGround { get { return _defaultBackGroundResult; } set { _defaultBackGroundResult = value; } }
        private Color _defaultBackGroundResult = Color.White;
        //
        private PictureBox _buttonUp = new PictureBox();
        private PictureBox _buttonDown = new PictureBox();
        private PictureBox _buttonAutoRun = new PictureBox();

        public _myTextBox()
        {
            InitializeComponent();
            /*this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.UseTextForAccessibility, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.CacheText, true);*/
            //
            this._buttonDown.Visible = false;
            this._buttonUp.Visible = false;
            this.Controls.Add(this._buttonDown);
            this.Controls.Add(this._buttonUp);
            this.Controls.Add(this._buttonAutoRun);
            this._buttonUp.Image = Properties.Resources.buttomup1;
            this._buttonDown.Image = Properties.Resources.buttomdown1;
            this._buttonAutoRun.Image = Properties.Resources.iconautorun;
            this.textBox.Width = this.Width;
            this.textBox.Font = this.Font;
            this.textBox.Margin = new Padding(0, 0, 0, 0);
            this.textBox.MaxLength = MaxLength;
            //
            this._buttonUp.MouseHover += new EventHandler(_buttonUp_MouseHover);
            this._buttonUp.MouseLeave += new EventHandler(_buttonUp_MouseLeave);
            this._buttonUp.MouseDown += new MouseEventHandler(_buttonUp_MouseDown);
            this._buttonUp.MouseUp += new MouseEventHandler(_buttonUp_MouseUp);
            this._buttonDown.MouseHover += new EventHandler(_buttonDown_MouseHover);
            this._buttonDown.MouseLeave += new EventHandler(_buttonDown_MouseLeave);
            this._buttonDown.MouseDown += new MouseEventHandler(_buttonDown_MouseDown);
            this._buttonDown.MouseUp += new MouseEventHandler(_buttonDown_MouseUp);
            this._iconSearch.MouseHover += new EventHandler(pictureBox1_MouseHover);
            this._iconSearch.MouseLeave += new EventHandler(IconSearch_MouseLeave);
            this._iconSearch.MouseClick += new MouseEventHandler(IconSearch_MouseClick);
            this._buttonAutoRun.MouseHover += new EventHandler(_buttonAutoRun_MouseHover);
            this._buttonAutoRun.MouseLeave += new EventHandler(_buttonAutoRun_MouseLeave);
            this._buttonAutoRun.MouseClick += new MouseEventHandler(_buttonAutoRun_MouseClick);
            this.textBox.KeyDown += new KeyEventHandler(_myTextBox_KeyDown);
            this.textBox.KeyUp += new KeyEventHandler(textBox_KeyUp);
            this.textBox.Enter += new EventHandler(textBox_Enter);
            this.textBox.GotFocus += new EventHandler(textBox_GotFocus);
            this.textBox.LostFocus += new EventHandler(textBox_LostFocus);
            this.textBox.Leave += new EventHandler(textBox_Leave);
            this.FontChanged += new EventHandler(_myTextBox_FontChanged);
            this.Paint += new PaintEventHandler(_myTextBox_Paint);
            this.SizeChanged += new EventHandler(_myTextBox_SizeChanged);
            //
        }

        void textBox_KeyUp(object sender, KeyEventArgs e)
        {
            _myGlobal._lastTextBox = this.textBox.Text;
        }

        void textBox_Leave(object sender, EventArgs e)
        {
            if (this.textBox != null) // toe fix on dispose object and cursor leave
            {
                if (this._upperCase)
                {
                    this.textBox.Text = this.textBox.Text.ToUpper();
                }
                if (this._isTime)
                {
                    string __s1 = this.textBox.Text.Replace(".", "").Replace(":", "");
                    if (__s1.Length == 4)
                    {
                        this.textBox.Text = __s1.Substring(0, 2).ToString() + ":" + __s1.Substring(2, 2).ToString();
                    }
                    else
                    {
                        this.textBox.Text = "";
                    }
                }
                if (this._oldValue == null || this._oldValue.Equals(this.textBox.Text) == false)
                {
                    this._isChange = true;
                }
            }
            this.Parent.Invalidate();
        }

        void _myTextBox_SizeChanged(object sender, EventArgs e)
        {
            _calcSize();
        }

        void _buttonAutoRun_MouseClick(object sender, MouseEventArgs e)
        {
            this.textBox.Focus();
            _cellSearchWork(this.textBox.Text);
        }

        void _buttonAutoRun_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Default;
            this._buttonAutoRun.BackColor = Color.WhiteSmoke;
        }

        void _buttonAutoRun_MouseHover(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Hand;
            this._buttonAutoRun.BackColor = Color.LightCyan;
        }

        void _buttonDown_MouseUp(object sender, MouseEventArgs e)
        {
            this._buttonDown.Image = Properties.Resources.buttomdown1;
        }

        void _buttonDown_MouseDown(object sender, MouseEventArgs e)
        {
            this._buttonDown.Image = Properties.Resources.buttomdown3;
        }

        void _buttonDown_MouseLeave(object sender, EventArgs e)
        {
            this._buttonDown.Image = Properties.Resources.buttomdown1;
        }

        void _buttonDown_MouseHover(object sender, EventArgs e)
        {
            this._buttonDown.Image = Properties.Resources.buttomdown2;
        }

        void _buttonUp_MouseUp(object sender, MouseEventArgs e)
        {
            this._buttonUp.Image = Properties.Resources.buttomup1;
        }

        void _buttonUp_MouseDown(object sender, MouseEventArgs e)
        {
            this._buttonUp.Image = Properties.Resources.buttomup3;
        }

        void _buttonUp_MouseLeave(object sender, EventArgs e)
        {
            this._buttonUp.Image = Properties.Resources.buttomup1;
        }

        void _buttonUp_MouseHover(object sender, EventArgs e)
        {
            this._buttonUp.Image = Properties.Resources.buttomup2;
        }

        void _myTextBox_Paint(object sender, PaintEventArgs e)
        {
            /*if (textBox.Visible == false)
            {
                Graphics __g = e.Graphics;
                Pen __myPen = new Pen(Color.RoyalBlue, 0);
                Point[] __point = {
                    new Point(0,0),
                    new Point(0,textBox.Height-1),
                    new Point(textBox.Width-1,textBox.Height-1),
                    new Point(textBox.Width-1,0),
                    new Point(0,0)
                };
                __g.DrawLines(__myPen, __point);
                __myPen.Dispose();
            }*/
        }

        void _myTextBox_FontChanged(object sender, EventArgs e)
        {
            this.textBox.Font = this.Font;
            this.textBox.Invalidate();
        }

        [Category("_SML")]
        [Description("1=ค้นหาแบบจอเล็ก,2=ปุ่มขึ้นลง,3=Autorun,4=ค้นหาแบบเต็มใหญ่")]
        [DefaultValue(0)]
        public int _iconNumber
        {
            get
            {
                return _iconNumberResult;
            }
            set
            {
                _iconNumberResult = value;
                _calcSize();
                this.Invalidate();
            }
        }

        [Category("_SML")]
        [Description("TextBox")]
        [DefaultValue(typeof(TextBox), "TextBox")]
        public TextBox TextBox
        {
            get
            {
                return textBox;
            }
            set
            {
                textBox = value;
                _calcSize();
                this.Invalidate();
            }
        }

        [Category("_SML")]
        [Description("แสดง Icon")]
        [DefaultValue(0)]
        public bool ShowIcon
        {
            get
            {
                return _icon;
            }
            set
            {
                _icon = value;
                _calcSize();
                this.Invalidate();
            }
        }

        [Category("_SML")]
        [Description("Number of Row จำนวนบรรทัด")]
        [DefaultValue(0)]
        public int NumberRow
        {
            get
            {
                return _rowCount;
            }
            set
            {
                _rowCount = value;
                _calcSize();
                this.Invalidate();
            }
        }

        void _calcSize()
        {
            this.SuspendLayout();
            this.textBox.Width = (_icon) ? (this.Width - 17) : this.Width;
            this.textBox.Multiline = (_rowCount > 1) ? true : false;
            this.textBox.Height = (_rowCount > 1) ? ((_rowCount * 21) + 3) : 21;
            this.Height = this.textBox.Height;
            if (_icon == false)
            {
                _iconSearch.Visible = false;
                _buttonUp.Visible = false;
                _buttonDown.Visible = false;
                _buttonAutoRun.Visible = false;
            }
            else
            {
                switch (_iconNumber)
                {
                    case 1:
                    case 4:
                        this._iconSearch.Visible = true;
                        this._buttonDown.Visible = false;
                        this._buttonUp.Visible = false;
                        this._buttonAutoRun.Visible = false;
                        if (this._iconType == 0)
                        {
                            this._iconSearch.Location = new Point(this.textBox.Location.X + (this.textBox.Width - 1), this.textBox.Location.Y);
                        }
                        else
                        {
                            this._iconSearch.Location = new Point(0, 0);
                            this.textBox.Location = new Point(this._iconSearch.Width, this.textBox.Location.Y);
                        }
                        break;
                    case 2:
                        this._iconSearch.Visible = false;
                        this._buttonDown.Visible = true;
                        this._buttonUp.Visible = true;
                        this._buttonAutoRun.Visible = false;
                        this._buttonUp.Location = new Point(this.textBox.Location.X + (this.textBox.Width - 1), this.textBox.Location.Y);
                        this._buttonDown.Location = new Point(this._buttonUp.Location.X, this.textBox.Location.Y + this._buttonUp.Image.Height + 1);
                        break;
                    case 3:
                        this._buttonAutoRun.Visible = true;
                        this._iconSearch.Visible = false;
                        this._buttonDown.Visible = false;
                        this._buttonUp.Visible = false;
                        this._buttonAutoRun.Location = new Point(this.textBox.Location.X + (this.textBox.Width - 1), this.textBox.Location.Y);
                        break;
                }
            }
            this.ResumeLayout();
        }

        void textBox_LostFocus(object sender, EventArgs e)
        {
            this.textBox.BackColor = (_emtry) ? _defaultBackGround : Color.OldLace;
            if (this.IsUpperCase)
            {
                this.textBox.Text = this.textBox.Text.ToUpper();
            }
        }

        void textBox_GotFocus(object sender, EventArgs e)
        {
            this.textBox.BackColor = Color.LightGoldenrodYellow;
        }

        void textBox_Enter(object sender, EventArgs e)
        {
            if (this.GetType() == typeof(MyLib._myDateBox))
            {
                MyLib._myDateBox __getControl = (MyLib._myDateBox)this;
                __getControl._beforeInput();
            }
            this._oldValue = this._textFirst;
            this._isChange = false;
            this.Parent.Invalidate();
            this.textBox.SelectAll();
        }

        /// <summary>
        /// เมื่อมีการเลื่อนขึ้น
        /// </summary>
        public event CellMoveUpHandler _cellMoveUp;
        /// <summary>
        /// เมื่อมีการเลื่อนลง
        /// </summary>
        public event CellMoveDownHandler _cellMoveDown;
        /// <summary>
        /// เมื่อมีการเลื่อนไปขวา
        /// </summary>
        public event CellMoveRightHandler _cellMoveRight;
        /// <summary>
        /// เมื่อมีการเลื่อนไปซ้าย
        /// </summary>
        public event CellMoveLeftHandler _cellMoveLeft;
        /// <summary>
        /// เมื่อกดปุ่มค้นหา
        /// </summary>
        public event CellSearchHandler _cellSearch;

        protected virtual void _cellMoveRightWork()
        {
            if (_cellMoveRight != null)
            {
                _cellMoveRight(this);
            }
            else
            {
                SendKeys.Send("{TAB}");
            }
        }

        protected virtual void _cellMoveLeftWork()
        {
            if (_cellMoveLeft != null)
            {
                _cellMoveLeft(this);
            }
            else
            {
                SendKeys.Send("+{TAB}");
            }
        }

        protected virtual void _cellMoveUpWork()
        {
            if (_cellMoveUp != null)
            {
                _cellMoveUp(this);
            }
        }

        protected virtual void _cellMoveDownWork()
        {
            if (_cellMoveDown != null)
            {
                _cellMoveDown(this);
            }
        }

        protected virtual void _cellSearchWork(string data)
        {
            if (_cellSearch != null) _cellSearch(this, data);
        }

        void IconSearch_MouseClick(object sender, MouseEventArgs e)
        {
            this.textBox.Focus();
            _cellSearchWork(this.textBox.Text);
        }

        void _myTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            TextBox myTextBox = (TextBox)sender;
            this.textBox.Text = myTextBox.Text;
            if (_enterToTab && e.KeyCode == Keys.Enter && e.Control == false)
            {
                if (this.IsUpperCase)
                {
                    myTextBox.Text = myTextBox.Text.ToUpper();
                }
                SendKeys.Send("{TAB}");
                e.Handled = true;
                return;
            }
            else
                if (e.KeyCode == Keys.Right && myTextBox.SelectionStart == myTextBox.Text.Length)
            {
                e.Handled = true;
                _cellMoveRightWork();
                return;
            }
            else
                    if (e.KeyCode == Keys.Down && this.textBox.Multiline == false)
            {
                e.Handled = true;
                _cellMoveDownWork();
                return;
            }
            else
                        if (e.KeyCode == Keys.Down && this.textBox.Multiline == true)
            {
                // ค้นหาตำแหน่งสุดท้ายของบรรทัดแรก
                int __lastAddr = 0;
                while (this.textBox.Text.IndexOf((Char)10, __lastAddr) != -1)
                {
                    int __getNewAddr = this.textBox.Text.IndexOf((Char)10, __lastAddr + 1);
                    if (__getNewAddr == -1)
                    {
                        break;
                    }
                    __lastAddr = __getNewAddr;
                }
                if (myTextBox.SelectionStart >= __lastAddr)
                {
                    e.Handled = true;
                    _cellMoveDownWork();
                    return;
                }
            }
            else
                            if (e.KeyCode == Keys.Up && this.textBox.Multiline == false)
            {
                e.Handled = true;
                _cellMoveUpWork();
                return;
            }
            else
                                if (e.KeyCode == Keys.Up && this.textBox.Multiline == true)
            {
                // ค้นหาตำแหน่งสุดท้ายของบรรทัดแรก
                int __lastAddr = this.textBox.Text.IndexOf((Char)10);
                if (__lastAddr == -1)
                {
                    __lastAddr = myTextBox.Text.Length;
                }
                if (myTextBox.SelectionStart <= __lastAddr)
                {
                    e.Handled = true;
                    _cellMoveUpWork();
                    return;
                }
            }
            else
                                    if (e.KeyCode == Keys.Left && myTextBox.SelectionStart == 0)
            {
                e.Handled = true;
                _cellMoveLeftWork();
                return;
            }
            else
                                        if (e.KeyCode == Keys.F2)
            {
                if (this.IsUpperCase)
                {
                    myTextBox.Text = myTextBox.Text.ToUpper();
                }
                _cellSearchWork(this.textBox.Text);
                return;
            }
        }

        void IconSearch_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Default;
            this._iconSearch.BackColor = Color.WhiteSmoke;
        }

        void pictureBox1_MouseHover(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Hand;
            this._iconSearch.BackColor = Color.LightCyan;
        }

        private void _myTextBox_Load(object sender, EventArgs e)
        {
            this.Parent.Paint += new PaintEventHandler(Parent_Paint);
        }

        void Parent_Paint(object sender, PaintEventArgs e)
        {
            /*if (textBox.Focused)
            {
                Graphics __g = e.Graphics;
                Point __getClient = this.Location;
                Pen __myPen = new Pen(Color.Orange, 1);
                Point[] __point = {
                    new Point(__getClient.X-1,__getClient.Y-1),
                    new Point(__getClient.X-1,__getClient.Y+(this.Height)),
                    new Point(__getClient.X+(this.Width),__getClient.Y+(this.Height)),
                    new Point(__getClient.X+(this.Width),__getClient.Y-1),
                    new Point(__getClient.X-1,__getClient.Y-1)
                };
                __g.DrawLines(__myPen, __point);
                __myPen.Dispose();
            }*/
        }

        private void IconSearch_Click(object sender, EventArgs e)
        {

        }
    }

    public delegate void CellMoveUpHandler(object sender);
    public delegate void CellMoveDownHandler(object sender);
    public delegate void CellMoveRightHandler(object sender);
    public delegate void CellMoveLeftHandler(object sender);
    public delegate void CellSearchHandler(object sender, string e);
}
