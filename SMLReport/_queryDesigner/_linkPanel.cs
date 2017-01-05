using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLReport._design
{
    [Serializable]
    public partial class _linkPanel : UserControl
    {
        public _tablePanel _source;
        public string _sourceTableName;
        public string _sourceFieldName;
        public int _sourceRowNumber;
        public _tablePanel _target;
        public string _targetTableName;
        public string _targetFieldName;
        public int _targetRowNumber;
        public Guid _guid;
        public Control _thisControl;
        public string[] _conditionList = { "=", "=+", "+=", ">", ">=", "<", "<=", "!=" };

        public _linkPanel()
        {
            InitializeComponent();
            if (MyLib._myGlobal._isDesignMode == false)
            {
                this._toolBar.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            }
            _thisControl = this;
            this.AutoSize = true;
            this._joinComboBox.SelectedIndex = 0;
            this.MouseEnter += new EventHandler(_linkPanel_MouseEnter);
            this._listBox.MouseEnter += new EventHandler(_listBox_MouseEnter);
            this._listBox.Visible = false;
            this._listBox.VisibleChanged += new EventHandler(_listBox_VisibleChanged);
            this._listBox.SelectedIndexChanged += new EventHandler(_listBox_SelectedIndexChanged);
        }

        void _listBox_MouseEnter(object sender, EventArgs e)
        {
            this.BringToFront();
        }

        void _linkPanel_MouseEnter(object sender, EventArgs e)
        {
            this.BringToFront();
        }

        void _listBox_VisibleChanged(object sender, EventArgs e)
        {
            _calcSize();
        }

        void _listBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            this._label.Text = _conditionList[this._listBox.SelectedIndex];
            this._listBox.Visible = false;
        }

        public void _calcSize()
        {
            int __heigth = _toolBar.Height + ((_listBox.Visible) ? _listBox.Height : 0);
            int __width = (_listBox.Visible) ? _listBox.Width : _toolBar.Width;
            this.Size = new Size(__width, __heigth);
        }

        public event RemoveEvent _removeLink;

        private void _remove_Click(object sender, EventArgs e)
        {
            if (_removeLink != null)
            {
                _removeLink(this);
            }
        }

        public delegate void RemoveEvent(_linkPanel sender);

        private void _linkPanel_Load(object sender, EventArgs e)
        {

        }

        private void _label_Click(object sender, EventArgs e)
        {
            this.BringToFront();
            this._listBox.Visible = (this._listBox.Visible) ? false : true;
        }
    }
}
