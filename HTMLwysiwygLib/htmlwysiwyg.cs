using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace HTMLwysiwygLib
{
    public partial class htmlwysiwyg : UserControl
    {
        //private mshtml.IHTMLDocument2 doc;
        private bool edits = true;
        public htmlwysiwyg()
        {
            InitializeComponent();
        }

        /*
        /// <summary>
        /// Returns the inner html generated
        /// </summary>
        /// <returns>String html</returns>
        public String getHTML()
        {
            return doc.body.innerHTML;
        }

        /// <summary>
        /// Sets the Inner HTML of the documents (used to load docs into the editor)
        /// </summary>
        /// <param name="html"></param>
        public void setHTML(String html)
        {
            doc.body.innerHTML = html;
        }

        /// <summary>
        /// Returns the plain text without any formatting
        /// </summary>
        /// <returns>String plainText</returns>
        public String getPlainText()
        {
            return doc.body.innerText;
        }

        //Sets the editor to either allow or dissalow edit.
        public void allowEdit(bool edit)
        {
            edits = edit;
            if (edit)
                doc.designMode = "On";
            else
                doc.designMode = "Off";
        }




        private void htmlwysiwyg_Load(object sender, EventArgs e)
        {
            doc = (mshtml.IHTMLDocument2)this.htmlRenderer.Document.DomDocument;
            doc.designMode = "On";
        }

        private void redToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (edits)
                doc.execCommand("BackColor", false, "red");
        }

        /// <summary>
        ///     Show the back color button
        /// </summary>
        [Description("Show the back color button or not"),
        Category("Toolbar")]
        public bool ShowBackColorButton
        {
            get { return tsBackColor.Visible; }
            set { tsBackColor.Visible = value; UpdateToolbarSeperators(); }
        }

        private void UpdateToolbarSeperators()
        {
            if (newTS.Visible == true || printToolStripButton.Visible == true)
                tsSeparator1.Visible = true;
            else
                tsSeparator1.Visible = false;

            if (tsCut.Visible == true || tsCopy.Visible == true || tsPaste.Visible == true)
                toolStripSeparator1.Visible = true;
            else
                toolStripSeparator1.Visible = false;

            if (tsBold.Visible == true || tsUnderline.Visible == true || tsItalics.Visible == true)
                toolStripSeparator2.Visible = true;
            else
                toolStripSeparator2.Visible = false;
            if (tsCenter.Visible == true || tsJustify.Visible == true || tsLeft.Visible == true || tsRight.Visible == true)
                toolStripSeparator3.Visible = true;
            else
                toolStripSeparator3.Visible = false;
            if (tsIndent.Visible == true || tsOutdent.Visible == true)
                toolStripSeparator4.Visible = true;
            else
                toolStripSeparator4.Visible = false;
            if (tsBullets.Visible == true || tsNumeric.Visible == true)
                toolStripSeparator5.Visible = true;
            else
                toolStripSeparator5.Visible = false;

            if (tsBackColor.Visible == true || tsTextColor.Visible == true)
                toolStripSeparator6.Visible = true;
            else
                toolStripSeparator6.Visible = false;

            if (tsLink.Visible == true || tsRemoveLink.Visible == true)
                toolStripSeparator7.Visible = true;
            else
                toolStripSeparator7.Visible = false;

        }


        private void newTS_Click(object sender, EventArgs e)
        {
            if (edits)
                doc.body.innerText = "";
        }
        /// <summary>
        ///     Show the new button
        /// </summary>
        [Description("Show the new button or not"),
        Category("Toolbar")]
        public bool ShowNewButton
        {
            get { return newTS.Visible; }
            set { newTS.Visible = value; UpdateToolbarSeperators(); }
        }

        private void printToolStripButton_Click(object sender, EventArgs e)
        {
            if (edits)
                doc.execCommand("Print", true, null);
        }

        /// <summary>
        ///     Show the print button
        /// </summary>
        [Description("Show the new button or not"),
        Category("Toolbar")]
        public bool ShowPrintButton
        {
            get { return printToolStripButton.Visible; }
            set { printToolStripButton.Visible = value; UpdateToolbarSeperators(); }
        }


        private void cutToolStripButton_Click(object sender, EventArgs e)
        {
            if (edits)
                try
                {
                    doc.execCommand("Cut", false, null);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error Couldn't Cut\n\r" + ex.Message, "Erro Executing Cut Command", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
        }

        /// <summary>
        ///     Show the cut button
        /// </summary>
        [Description("Show the cut button or not"),
        Category("Toolbar")]
        public bool ShowCutButton
        {
            get { return tsCut.Visible; }
            set { tsCut.Visible = value; UpdateToolbarSeperators(); }
        }

        private void copyToolStripButton_Click(object sender, EventArgs e)
        {
            if (edits)
                doc.execCommand("Copy", false, null);
        }

        /// <summary>
        ///     Show the copy button
        /// </summary>
        [Description("Show the copy button or not"),
        Category("Toolbar")]
        public bool ShowCopyButton
        {
            get { return tsCopy.Visible; }
            set { tsCopy.Visible = value; UpdateToolbarSeperators(); }
        }

        private void pasteToolStripButton_Click(object sender, EventArgs e)
        {
            if (edits)
                doc.execCommand("Paste", false, null);
        }

        /// <summary>
        ///     Show the cut button
        /// </summary>
        [Description("Show the paste button or not"),
        Category("Toolbar")]
        public bool ShowPasteButton
        {
            get { return tsPaste.Visible; }
            set { tsPaste.Visible = value; UpdateToolbarSeperators(); }
        }

        private void tsBold_Click(object sender, EventArgs e)
        {
            if (edits)
                doc.execCommand("Bold", false, null);
        }

        /// <summary>
        ///     Show the cut button
        /// </summary>
        [Description("Show the bold button or not"),
        Category("Toolbar")]
        public bool ShowBolButton
        {
            get { return tsBold.Visible; }
            set { tsBold.Visible = value; UpdateToolbarSeperators(); }
        }

        private void tsUnderline_Click(object sender, EventArgs e)
        {
            if (edits)
                doc.execCommand("Underline", false, null);
        }

        /// <summary>
        ///     Show the cut button
        /// </summary>
        [Description("Show the underline button or not"),
        Category("Toolbar")]
        public bool ShowUnderlineButton
        {
            get { return tsUnderline.Visible; }
            set { tsUnderline.Visible = value; UpdateToolbarSeperators(); }
        }



        private void tsItalics_Click(object sender, EventArgs e)
        {
            if (edits)
                doc.execCommand("Italic", false, null);
        }

        /// <summary>
        ///     Show the cut button
        /// </summary>
        [Description("Show the italics button or not"),
        Category("Toolbar")]
        public bool ShowItalicButton
        {
            get { return tsItalics.Visible; }
            set { tsItalics.Visible = value; UpdateToolbarSeperators(); }
        }

        private void tsLeft_Click(object sender, EventArgs e)
        {
            if (edits)
                doc.execCommand("JustifyLeft", false, null);
        }

        /// <summary>
        ///     Show the cut button
        /// </summary>
        [Description("Show the Align Left button or not"),
        Category("Toolbar")]
        public bool ShowAlignLeftButton
        {
            get { return tsLeft.Visible; }
            set { tsLeft.Visible = value; UpdateToolbarSeperators(); }
        }

        private void tsCenter_Click(object sender, EventArgs e)
        {
            if (edits)
                doc.execCommand("JustifyCenter", false, null);
        }

        /// <summary>
        ///     Show the cut button
        /// </summary>
        [Description("Show the Align Center button or not"),
        Category("Toolbar")]
        public bool ShowAlignCenterButton
        {
            get { return tsCenter.Visible; }
            set { tsCenter.Visible = value; UpdateToolbarSeperators(); }
        }

        private void tsJustify_Click(object sender, EventArgs e)
        {
            if (edits)
                doc.execCommand("JustifyFull", false, null);
        }
        /// <summary>
        ///     Show the cut button
        /// </summary>
        [Description("Show the Justify button or not"),
        Category("Toolbar")]
        public bool ShowJustifyButton
        {
            get { return tsJustify.Visible; }
            set { tsJustify.Visible = value; UpdateToolbarSeperators(); }
        }


        private void tsRight_Click(object sender, EventArgs e)
        {
            if (edits)
                doc.execCommand("JustifyRight", false, null);
        }

        /// <summary>
        ///     Show the cut button
        /// </summary>
        [Description("Show the Align Right button or not"),
        Category("Toolbar")]
        public bool ShowAlignRightButton
        {
            get { return tsRight.Visible; }
            set { tsRight.Visible = value; UpdateToolbarSeperators(); }
        }

        private void tsIndent_Click(object sender, EventArgs e)
        {
            if (edits)
                doc.execCommand("Indent", false, null);
        }

        /// <summary>
        ///     Show the cut button
        /// </summary>
        [Description("Show the Indent button or not"),
        Category("Toolbar")]
        public bool ShowIndentButton
        {
            get { return tsIndent.Visible; }
            set { tsIndent.Visible = value; UpdateToolbarSeperators(); }
        }

        private void tsOutdent_Click(object sender, EventArgs e)
        {
            if (edits)
                doc.execCommand("Outdent", false, null);
        }
        /// <summary>
        ///     Show the cut button
        /// </summary>
        [Description("Show the Outdent button or not"),
        Category("Toolbar")]
        public bool ShowOutdentButton
        {
            get { return tsOutdent.Visible; }
            set { tsOutdent.Visible = value; UpdateToolbarSeperators(); }
        }



        private void tsBullets_Click(object sender, EventArgs e)
        {
            if (edits)
                doc.execCommand("InsertUnorderedList", false, null);
        }

        [Description("Show the Bullet button or not"),
        Category("Toolbar")]
        public bool ShowBulletButton
        {
            get { return tsBullets.Visible; }
            set { tsBullets.Visible = value; UpdateToolbarSeperators(); }
        }

        private void tsNumeric_Click(object sender, EventArgs e)
        {
            if (edits)
                doc.execCommand("InsertOrderedList", false, null);
        }
        [Description("Show the OutdentOrdered List button or not"),
        Category("Toolbar")]
        public bool ShowOrderedListButton
        {
            get { return tsNumeric.Visible; }
            set { tsNumeric.Visible = value; UpdateToolbarSeperators(); }
        }

        private void blueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (edits)
                doc.execCommand("BackColor", false, "blue");
        }

        private void blackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (edits)
                doc.execCommand("BackColor", false, "black");
        }

        private void yellowToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (edits)
                doc.execCommand("BackColor", false, "yellow");
        }

        private void orangeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (edits)
                doc.execCommand("BackColor", false, "orange");
        }

        private void greenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (edits)
                doc.execCommand("BackColor", false, "green");
        }

        private void brownToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (edits)
                doc.execCommand("BackColor", false, "brown");
        }

        private void redToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (edits)
                doc.execCommand("ForeColor", false, "red");
        }

        private void blueToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (edits)
                doc.execCommand("ForeColor", false, "blue");
        }

        private void blackToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (edits)
                doc.execCommand("ForeColor", false, "black");
        }

        private void yellowToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (edits)
                doc.execCommand("ForeColor", false, "yellow");
        }

        private void orangeToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (edits)
                doc.execCommand("ForeColor", false, "orange");
        }

        private void greenToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (edits)
                doc.execCommand("ForeColor", false, "green");
        }

        private void brownToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (edits)
                doc.execCommand("ForeColor", false, "brown");
        }

        private void tsLink_Click(object sender, EventArgs e)
        {
            if (edits)
                doc.execCommand("CreateLink", true, null);
        }

        [Description("Show the Link button"),
       Category("Toolbar")]
        public bool ShowLinkButton
        {
            get { return tsLink.Visible; }
            set { tsLink.Visible = value; UpdateToolbarSeperators(); }
        }

        private void tsRemoveLink_Click(object sender, EventArgs e)
        {
            if (edits)
                doc.execCommand("Unlink", true, null);
        }

        [Description("Show the Unlink button"),
       Category("Toolbar")]
        public bool ShowUnlinkButton
        {
            get { return tsRemoveLink.Visible; }
            set { tsRemoveLink.Visible = value; UpdateToolbarSeperators(); }
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (edits)
                doc.execCommand("FontSize", false, 1);
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            if (edits)
                doc.execCommand("FontSize", false, 2);
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            if (edits)
                doc.execCommand("FontSize", false, 3);
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            if (edits)
                doc.execCommand("FontSize", false, 4);
        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            if (edits)
                doc.execCommand("FontSize", false, 5);
        }

        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {
            if (edits)
                doc.execCommand("FontSize", false, 6);
        }

        private void toolStripMenuItem8_Click(object sender, EventArgs e)
        {
            if (edits)
                doc.execCommand("FontSize", false, 7);
        }

        private void verdanaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (edits)
                doc.execCommand("FontName", false, "Verdana");
        }

        private void ariaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (edits)
                doc.execCommand("FontName", false, "Arial");
        }

        private void timesNewRomanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (edits)
                doc.execCommand("FontName", false, "Times New Roman");
        }

        private void currierToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (edits)
                doc.execCommand("FontName", false, "Currier New");
        }

        private void comicSansToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (edits)
                doc.execCommand("FontName", false, "Cambria");
        }

        private void helveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (edits)
                doc.execCommand("FontName", false, "Tahoma");
        }

        private void bookAntiquaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (edits)
                doc.execCommand("FontName", false, "Book Antiqua");
        }

        [Description("Show the Text Background color button"),
        Category("Toolbar")]
        public bool ShowTxtBGButton
        {
            get { return tsTextColor.Visible; }
            set { tsTextColor.Visible = value; UpdateToolbarSeperators(); }
        }

        [Description("Show the Text color button"),
       Category("Toolbar")]
        public bool ShowTxtColorButton
        {

            get { return tsTextColor.Visible; }
            set { tsTextColor.Visible = value; UpdateToolbarSeperators(); }
        }

        private void tsFontSize_ButtonClick(object sender, EventArgs e)
        {

        }
        [Description("Show the Font Size button"),
       Category("Toolbar")]
        public bool ShowFontSizeButton
        {
            get { return tsFontSize.Visible; }
            set { tsFontSize.Visible = value; UpdateToolbarSeperators(); }
        }



        [Description("Show the Font Family button"),
       Category("Toolbar")]
        public bool ShowFontFamilyButton
        {
            get { return tsFontFamily.Visible; }
            set { tsFontFamily.Visible = value; UpdateToolbarSeperators(); }
        }
        /// <summary>
        /// Allows you to add custome fonts to the control
        /// </summary>
        /// <param name="fontName"> Name of the font to add</param>
        public void addFont(String fontName)
        {
            ToolStripMenuItem tsMi = new ToolStripMenuItem();
            tsMi.Font = new System.Drawing.Font(fontName, 9F);
            tsMi.Name = fontName + "ToolStripMenuItem";
            tsMi.Size = new System.Drawing.Size(167, 22);
            tsMi.Text = fontName;
            tsMi.Click += new System.EventHandler(addFont_click);
            tsFontFamily.DropDownItems.Add(tsMi);
        }

        private void addFont_click(object sender, EventArgs e)
        {
            if (edits)
                doc.execCommand("FontName", false, ((ToolStripMenuItem)sender).Text);
        }

        private void tsTextColor_Click(object sender, EventArgs e)
        {
            if (edits)
            {
                ColorDialog colorDialog = new ColorDialog();
                if (colorDialog.ShowDialog() != DialogResult.Cancel)
                {
                    doc.execCommand("ForeColor", false, ColorTranslator.ToHtml(colorDialog.Color).ToString());
                }
            }
        }

        private void tsBackColor_Click(object sender, EventArgs e)
        {
            if (edits)
            {
                ColorDialog colorDialog = new ColorDialog();
                if (colorDialog.ShowDialog() != DialogResult.Cancel)
                {
                    doc.execCommand("BackColor", false, ColorTranslator.ToHtml(colorDialog.Color).ToString());
                }
            }
        }*/
        
    }
}
