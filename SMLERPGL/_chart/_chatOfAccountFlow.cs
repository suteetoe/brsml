using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLERPGL._chart
{
    public partial class _chatOfAccountFlow : UserControl
    {
        MyLib._myFrameWork _frameWork = new MyLib._myFrameWork();

        public _chatOfAccountFlow()
        {
            InitializeComponent();
            if (MyLib._myGlobal._isDesignMode == false)
            {
                this._toolBar.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            }
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.UseTextForAccessibility, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.CacheText, true);
            //
            _chartOfAccountTreeView.SuspendLayout();
            SMLProcess._glProcess __process = new SMLProcess._glProcess();
            _chartOfAccountTreeView = __process._getChartOfAccountTreeView(_chartOfAccountTreeView);
            _changeBackground(_chartOfAccountTreeView.Nodes[0]);
            _chartOfAccountTreeView.ExpandAll();
            _chartOfAccountTreeView.ResumeLayout(false);
            //
        }

        public void _changeBackground(TreeNode getNode)
        {
            getNode.ForeColor = Color.Black;
            for (int loop = 0; loop < getNode.Nodes.Count; loop++)
            {
                switch (getNode.Level)
                {
                    case 0: getNode.ForeColor = Color.Blue; break;
                    case 1: getNode.ForeColor = Color.Brown; break;
                    case 2: getNode.ForeColor = Color.BlueViolet; break;
                    case 3: getNode.ForeColor = Color.Chocolate; break;
                    case 4: getNode.ForeColor = Color.DarkBlue; break;
                    default: getNode.ForeColor = Color.DarkGreen; break;
                }
                _changeBackground(getNode.Nodes[loop]);
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            const int WM_KEYDOWN = 0x100;
            const int WM_SYSKEYDOWN = 0x104;

            if ((msg.Msg == WM_KEYDOWN) || (msg.Msg == WM_SYSKEYDOWN))
            {
                if (keyData == Keys.Escape)
                {
                    this.Dispose();
                    return true;
                }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void _buttonClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
