using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using SMLReport._design;

namespace SMLPOSControl._designer
{
    public class _drawPanel : SMLReport._design._drawPanel
    {
        public _drawPanel()
        {
            // add scroll
        }

        void _drawPanel__afterClick(object sender, object[] senderObject)
        {
            // send object config to propertygrid
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Delete :
                    // delete object select
                    _deleteSelectedObject();
                    return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        public void _deleteSelectedObject()
        {
            this._graphicsList._deleteSelection();
            this.Invalidate();

            // เพิ่่ม event หลังจาก delete ด้วยนะ
        }

        protected override void _onDrawObjectOverBottomPanel(object __sender, SMLReport._design._drawObject __objOver)
        {
            //base._onDrawObjectOverBottomPanel(__sender, __objOver);
        }

        protected override void _onDrawObjectOverLeftPanel(object __sender, SMLReport._design._drawObject __objOver)
        {
            //base._onDrawObjectOverLeftPanel(__sender, __objOver);
        }

        protected override void _onDrawObjectOverRightPanel(object __sender, SMLReport._design._drawObject __objOver)
        {
            // ขยายความกว้างของ drawPanel ออกไป จะเกิด scroll bar
            //int __width = __objOver._getHandle(3).X;
            //this.Width = __width;
        }

        protected override void _onDrawObjectOverTopPanel(object __sender, SMLReport._design._drawObject __objOver)
        {
            //base._onDrawObjectOverTopPanel(__sender, __objOver);
        }

        public event AfterClickPosPanelEventHandler _AfterClickPosPanel;
        protected override void _onAfterClickDrawPanel(object sender, object[] __selectedObject)
        {
            //base._onAfterClickDrawPanel(sender, __selectedObject);
            if (_AfterClickPosPanel != null)
            {
                _AfterClickPosPanel(this.Parent, __selectedObject);
            }
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // _drawPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.BackColor = System.Drawing.Color.Transparent;
            this.Name = "_drawPanel";
            this.ResumeLayout(false);

        }

    }

    public delegate void AfterClickPosPanelEventHandler(object sender, object[] senderObject);
}
