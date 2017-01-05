using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLERPDashBoard
{
    public partial class _dashBoard : UserControl
    {
        public _dashBoard()
        {
            InitializeComponent();
            if (MyLib._myGlobal._isDesignMode == false)
            {
                this.toolStrip1.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            }
        }

        private void _addControl(Control newControl)
        {
            _borderControl __border = new _borderControl();
            __border.Size = new Size(newControl.Width + __border.Padding.Left + __border.Padding.Right, newControl.Height + __border.Padding.Top + __border.Padding.Bottom);
            newControl.Dock = DockStyle.Fill;
            __border.Controls.Add(newControl);
            newControl.SendToBack();
            this.Controls.Add(__border);
            __border.Invalidate();
        }

        private void information_Click(object sender, EventArgs e)
        {
            this._addControl(new _informationControl());
        }
    }
}
