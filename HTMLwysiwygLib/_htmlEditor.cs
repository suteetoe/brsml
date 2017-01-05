using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace HTMLwysiwygLib
{
    public partial class _htmlEditor : UserControl
    {
        public _htmlEditor()
        {
            InitializeComponent();
        }

        public void _setHTML(string html)
        {
            try
            {
                this._browser.Document.InvokeScript("ajaxLoad", new object[] { html });
            }
            catch
            {
            }

        }

        public string _getHTML()
        {
            string __result = "";

            try
            {
                __result = this._browser.Document.InvokeScript("ajaxSave").ToString();
            }
            catch
            {
            }

            return __result;
        }

    }
}
