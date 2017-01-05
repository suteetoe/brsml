using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace MyLib
{
    public partial class _searchDataFull : Form
    {
        private string _nameResult = "";
        /// <summary>
        /// 0=ShowDialog 1=Show This
        /// </summary>
        public int _showMode = 0;
        //
        /// <summary>
        /// แสดงแล้วหรือยัง
        /// </summary>
        public Boolean _show = false;
        //
        public object _owner;
        //
        public _searchDataFull()
        {
            InitializeComponent();
            this.Shown += new EventHandler(_searchDataFull_Shown);
        }

        public string _name
        {
            get
            {
                return _nameResult;
            }
            set
            {
                _nameResult = value;
            }
        }

        public void _firstFocus()
        {
            _dataList._searchText.textBox.Focus();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (this._showMode == 1)
            {
                this.Visible = false;
                e.Cancel = true;
            }
            else
            {
                base.OnClosing(e);
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            const int WM_KEYDOWN = 0x100;
            const int WM_SYSKEYDOWN = 0x104;

            if ((msg.Msg == WM_KEYDOWN) || (msg.Msg == WM_SYSKEYDOWN))
            {
                Keys keyCode = (Keys)(int)keyData & Keys.KeyCode;
                if ((keyData & Keys.Control) == Keys.Control && (keyCode == Keys.F))
                {
                    _dataList._searchText.textBox.Focus();
                    return true;
                }
                else
                    if (keyData == Keys.Escape)
                    {
                        this.Close();
                        return true;
                    }
                    else
                        if (keyData == Keys.Enter)
                        {
                            _dataList.Invalidate();
                            if (_dataList.SelectRow != -1)
                            {
                                if (_searchEnterKeyPress != null)
                                {
                                    _searchEnterKeyPress(_dataList._gridData, _dataList.SelectRow);
                                }
                                return true;
                            }
                        }
                        else
                            if (keyData == Keys.F2)
                            {
                                this.Close();
                                return true;
                            }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        public event SearchEnterKeyPressEventHandler _searchEnterKeyPress;

        void _searchDataFull_Shown(object sender, EventArgs e)
        {
            if (this._showMode == 0)
            {
                this._dataList._searchText.textBox.Focus();
            }
        }

        private void _searchDataFull_Load(object sender, EventArgs e)
        {
        }
    }
    public delegate void SearchEnterKeyPressEventHandler(MyLib._myGrid sender, int row);
}