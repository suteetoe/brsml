using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Diagnostics;

namespace MyLib
{
    [ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.All)]
    public class ToolStripMyButton : ToolStripButton
    {
        public string _name = "";
        public int _iconNumber = 0;
        private _languageEnum _lastLanguage = _languageEnum.Null;

        public ToolStripMyButton()
            : base()
        {
            this.AutoSize = true;
            this.Padding = new Padding(1, 1, 1, 1);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (_myGlobal._language != this._lastLanguage)
            {
                _lastLanguage = _myGlobal._language;
                if (this._name.Length > 0)
                {
                    this.Text = _myGlobal._resource(this._name);
                }
            }

            base.OnPaint(e);
        }

        [Category("_SML")]
        [Description("Name ใช้ในการหา Resource")]
        [DisplayName("Name ใช้ในการหา Resource")]
        public string ResourceName
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }
    }

    [ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.All)]
    public class ToolStripMyLabel : ToolStripLabel
    {
        public string _name = "";
        public int _iconNumber = 0;
        private _languageEnum _lastLanguage = _languageEnum.Null;

        public ToolStripMyLabel()
            : base()
        {
            this.AutoSize = true;
            this.Padding = new Padding(1, 1, 1, 1);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (_myGlobal._language != this._lastLanguage)
            {
                _lastLanguage = _myGlobal._language;
                if (this._name.Length > 0)
                {
                    this.Text = _myGlobal._resource(this._name);
                }
            }

            base.OnPaint(e);
        }

        [Category("_SML")]
        [Description("Name ใช้ในการหา Resource")]
        [DisplayName("Name ใช้ในการหา Resource")]
        public string ResourceName
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }
    }

    [ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.ToolStrip | ToolStripItemDesignerAvailability.StatusStrip)]
    public class ToolStripCheckedBox : ToolStripControlHost
    {
        public ToolStripCheckedBox()
            : base(new CheckBox())
        {
        }

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public CheckBox MyCheckBox
        {
            get { return (CheckBox)this.Control; }
        }
    }

    [ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.All)]
    public class ToolStripFontColor : ToolStripSplitButton
    {
        public ToolStripFontColor()
            : base() { }
    }

    [ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.All)]
    public class ToolStripFontComboBox : ToolStripComboBox
    {
        private int maxFavourites;
        private ArrayList favourites;
        private string[] nonreadable;
        private Image image;
        private Font defaultFont;
        private StringFormat nonReadableStringFormat;
        private StringFormat standardStringFormat;
        public string _defaultFontName = "Angsana New";

        public ToolStripFontComboBox()
            : base()
        {
            this.ComboBox.DrawMode = DrawMode.OwnerDrawFixed;
            this.ComboBox.DropDownStyle = ComboBoxStyle.DropDownList;

            maxFavourites = 5;
            image = null;
            defaultFont = new Font("Tahoma", (float)8.25);
            nonReadableStringFormat = new StringFormat();
            nonReadableStringFormat.LineAlignment = StringAlignment.Center;

            standardStringFormat = new StringFormat();
            standardStringFormat.FormatFlags = StringFormatFlags.NoWrap;
            favourites = new ArrayList();

            if (!DesignMode)
            {
                GetFonts(this.ComboBox.CreateGraphics());

                favourites.Add(this._defaultFontName);
                this.Items.Insert(0, favourites[0].ToString());
            }

            nonreadable = new string[]{"CommercialPi BT","GreekC","GreekS","Marlett", "Monotype Corsiva", "MS Outlook","Nokia PC Composer", 
											  "UniversalMath1 BT", "Symusic", "Symeteo", "Symbol", "Symath", "Symap", "Syastro",
											  "Webdings", "Wingdings", "Wingdings 2", "Wingdings 3"};
            this.ComboBox.DrawItem += new DrawItemEventHandler(ComboBox_DrawItem);
            this.ComboBox.SelectedIndexChanged += new EventHandler(ComboBox_SelectedIndexChanged);
            this.ComboBox.FontChanged += new EventHandler(ComboBox_FontChanged);
        }

        void ComboBox_FontChanged(object sender, EventArgs e)
        {
            base.OnFontChanged(e);
            this.ComboBox.ItemHeight = this.Font.Height + 3;
        }

        [DefaultValue(null)]
        public System.Drawing.Image MyImage
        {
            get
            {
                return image;
            }
            set
            {
                image = value;
                this.Invalidate();
            }
        }

        [Browsable(false)]
        public string[] NonReadableFonts
        {
            get
            {
                return nonreadable;
            }
            set
            {
                nonreadable = value;
                this.Invalidate();
            }
        }

        [DefaultValue(5)]
        public int MaximumFavourites
        {
            get
            {
                return maxFavourites;
            }
            set
            {
                maxFavourites = value;
                this.Invalidate();
            }
        }

        private void GetFonts(Graphics g)
        {
            System.Drawing.Text.InstalledFontCollection installedFonts = new System.Drawing.Text.InstalledFontCollection();

            FontFamily[] fonts = installedFonts.Families;
            this.BeginUpdate();
            string name = "";
            foreach (FontFamily font in fonts)
            {
                name = font.Name.Trim();
                if (!this.Items.Contains(name))
                {
                    this.Items.Add(name);
                }
            }
            this.EndUpdate();
        }

        private void DrawSeperator(Graphics g, Rectangle rect)
        {
            Pen pen = new Pen(ControlPaint.LightLight(this.ForeColor));
            g.DrawLine(pen, rect.X + 1, rect.Y + rect.Height - 3, rect.X + rect.Width - 2, rect.Y + rect.Height - 3);
            g.DrawLine(pen, rect.X + 1, rect.Y + rect.Height - 1, rect.X + rect.Width - 2, rect.Y + rect.Height - 1);
        }

        private bool IsNonReadableFont(string FontName)
        {
            foreach (string non in nonreadable)
            {
                if (FontName == non)
                {
                    return true;
                }
            }
            return false;
        }

        void ComboBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0)
            {
                return;
            }
            Graphics g = e.Graphics;
            string fontName = this.Items[e.Index].ToString();
            Rectangle imgRect;
            Rectangle textRect;

            Font font = null;

            try
            {
                font = new Font(fontName, this.Font.Size);
            }
            catch
            {
                // Debugger.Break();
                font = defaultFont;
            }
            SolidBrush brush = new SolidBrush(e.ForeColor);

            e.DrawBackground();

            if (image != null)
            {
                imgRect = new Rectangle(e.Bounds.X, e.Bounds.Y, image.Width, image.Height);
            }
            else
            {
                // No image
                imgRect = new Rectangle(e.Bounds.X, e.Bounds.Y, 0, 0);
            }

            textRect = new Rectangle(imgRect.X + imgRect.Width + 2, e.Bounds.Y, e.Bounds.Width, e.Bounds.Height);

            if (image != null)
            {
                g.DrawImage(image, imgRect);
            }

            string endStr = " : Hello - สวัสดี";
            if (IsNonReadableFont(fontName))
            {
                g.DrawString(fontName + endStr, defaultFont, brush, textRect, nonReadableStringFormat);
                int wordwidth = g.MeasureString(fontName, defaultFont).ToSize().Width;
                g.DrawString(fontName + endStr, font, brush, new Rectangle(textRect.X + wordwidth, textRect.Y, e.Bounds.Width, e.Bounds.Height), standardStringFormat);
            }
            else
            {
                g.DrawString(fontName + endStr, font, brush, textRect, standardStringFormat);
            }

            e.DrawFocusRectangle();

            if (favourites.Count - 1 == e.Index)
            {
                DrawSeperator(g, e.Bounds);
            }
        }

        void ComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            base.OnSelectedIndexChanged(e);

            string fontName = this.Text;

            if (fontName == "") { return; }
            int indexOf = favourites.IndexOf(fontName);
            if (indexOf == -1)
            {
                if (maxFavourites > favourites.Count)
                {
                    // Insert new
                    favourites.Insert(0, fontName);
                    this.Items.Insert(0, fontName);
                }
                else
                {
                    // Don't add any new fonts - replace instead
                    favourites.RemoveAt(maxFavourites - 1);
                    favourites.Insert(0, fontName);
                    this.Items.RemoveAt(maxFavourites - 1);
                    this.Items.Insert(0, fontName);
                }
            }
            else
            {
                // Move existing arount
                if (favourites.Count > 1)
                {
                    /*favourites.RemoveAt(indexOf);
                    favourites.Insert(0, fontName);
                    this.Items.RemoveAt(indexOf);
                    this.Items.Insert(0, fontName);*/
                }
            }
            this.EndUpdate();
        }
    }
}