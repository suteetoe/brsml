using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MyLib
{
    public enum MemoryBoxResult { Yes, YesToAll, No, NoToAll, Cancel }

    public enum MemoryBoxButton
    {
        YesToAllNoToAll,
        CustomOK,
        TwoButton,
        ThreeButton
    }

    public partial class _myMessageBox : Form
    {
        // Internal values
        MemoryBoxResult lastResult = MemoryBoxResult.Cancel;
        MemoryBoxResult result = MemoryBoxResult.Cancel;

        public _myMessageBox()
        {
            InitializeComponent();
        }

        public _myMessageBox(MemoryBoxButton _buttonType)
        {
            InitializeComponent();

            if (_buttonType == MemoryBoxButton.CustomOK)
            {
                this.buttonYes.Visible = false;
                this.buttonYestoAll.Visible = false;
                this.buttonNo.Visible = false;

                this.AcceptButton = this.buttonCancel;
                this.buttonCancel.Text = "OK";
                this.buttonNotoAll.Click -= new EventHandler(buttonNotoAll_Click);
            }
        }

        #region Properties
        public String LabelText
        {
            get { return this.labelBody.Text; }
            set
            {
                this.labelBody.Text = value;
                UpdateSize();
            }
        }

        public MemoryBoxResult Result
        {
            get { return this.result; }
            set { this.result = value; }
        }

        #endregion

        #region Public Methods
        /// <summary>
        /// Call this function instead of ShowDialog, to check for remembered
        /// result.
        /// </summary>
        /// <returns></returns>
        public MemoryBoxResult ShowMemoryDialog()
        {
            result = MemoryBoxResult.Cancel;
            if (lastResult == MemoryBoxResult.NoToAll)
            {
                result = MemoryBoxResult.No;
            }
            else if (lastResult == MemoryBoxResult.YesToAll)
            {
                result = MemoryBoxResult.Yes;
            }
            else
            {
                base.ShowDialog();
            }
            return result;
        }

        public MemoryBoxResult ShowMemoryDialog(String label, string title)
        {
            this.Text = title;
            LabelText = label;
            return ShowMemoryDialog();
        }

        public MemoryBoxResult ShowMemoryDialog(IWin32Window sender, String label, string title)
        {
            this.Owner = (Form)sender;
            this.Text = title;
            LabelText = label;
            return ShowMemoryDialog();
        }


        //public static MemoryBoxResult ShowForm(IWin32Window sender,String label, string title)
        //{
        //    _myMessageBox __messageBox = new _myMessageBox();
        //    __messageBox.Text = title;
        //    __messageBox.LabelText = label;

        //    //__messageBox.Owner = owner;
        //    __messageBox.ShowDialog(sender);
        //    return __messageBox.result;
        //}

        public static MemoryBoxResult ShowForm(String label, string title)
        {
            _myMessageBox __messageBox = new _myMessageBox();
            return __messageBox.ShowMemoryDialog(label, title);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// This call updates the size of the window based on certain factors,
        /// such as if an icon is present, and the size of label.
        /// </summary>
        private void UpdateSize()
        {
            int newWidth = labelBody.Size.Width + 40;

            // Add the width of the icon, and some padding.
            if (pictureBoxIcon.Image != null)
            {
                newWidth += pictureBoxIcon.Width + 20;
                labelBody.Location = new Point(118, labelBody.Location.Y);
            }
            else
            {
                labelBody.Location = new Point(12, labelBody.Location.Y);
            }
            if (newWidth >= 440)
            {
                this.Width = newWidth;
            }
            else
            {
                this.Width = 440;
            }

            int newHeight = labelBody.Size.Height + 100;
            if (newHeight >= 200)
            {
                this.Height = newHeight;
            }
            else
            {
                this.Height = 200;
            }
        }

        #endregion

        private void buttonYes_Click(object sender, EventArgs e)
        {
            result = MemoryBoxResult.Yes;
            lastResult = MemoryBoxResult.Yes;
            DialogResult = DialogResult.Yes;
        }

        private void buttonYestoAll_Click(object sender, EventArgs e)
        {
            result = MemoryBoxResult.YesToAll;
            lastResult = MemoryBoxResult.YesToAll;
            DialogResult = DialogResult.Yes;
        }

        private void buttonNo_Click(object sender, EventArgs e)
        {
            result = MemoryBoxResult.No;
            lastResult = MemoryBoxResult.No;
            DialogResult = DialogResult.No;
        }

        private void buttonNotoAll_Click(object sender, EventArgs e)
        {
            result = MemoryBoxResult.NoToAll;
            lastResult = MemoryBoxResult.NoToAll;
            DialogResult = DialogResult.No;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            result = MemoryBoxResult.Cancel;
            lastResult = MemoryBoxResult.Cancel;
            DialogResult = DialogResult.Cancel;
        }

    }
}
