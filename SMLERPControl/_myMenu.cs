using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLERPControl
{
    public partial class _myMenu : Form
    {
        public OutlookStyleSidebar _outlookSidebar = new OutlookStyleSidebar();
        OutlookStyleTabButton tabMail;
        OutlookStyleTabButton tabCalendar;
        OutlookStyleTabButton tabContacts;
        OutlookStyleTabButton tabTasks;
        OutlookStyleTabButton tabShortcut;

        // For new TabButton

        Font newTabButtonFont = new Font("Tahoma", 8, FontStyle.Bold);
        Color newTabButtonForeColor = SystemColors.ControlText;

        Color newNormalLightColor = SystemColors.ControlLightLight;
        Color newNormalDarkColor = SystemColors.Control;

        Color newSelectedLightColor = SystemColors.ControlLightLight;
        Color newSelectedDarkColor = SystemColors.ControlLightLight;

        Color newOverLightColor = SystemColors.ControlLightLight;
        Color newOverDarkColor = SystemColors.ControlLightLight;

        public _myMenu()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.SetStyle(ControlStyles.UseTextForAccessibility, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.CacheText, true);

            InitSideBar();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            this.Visible = false;
            e.Cancel = true;
            base.OnClosing(e);
        }

        void InitSideBar()
        {

            //
            /*            outlookSidebar.OnTabButtonAdded += new OutlookStyleSidebar.OnTabButtonAddedEventHandler(outlookSidebar_OnTabButtonAdded);
                        outlookSidebar.OnTabButtonRemove += new OutlookStyleSidebar.OnTabButtonRemoveEventHandler(outlookSidebar_OnTabButtonRemove);
                        outlookSidebar.OnTabButtonSelected += new OutlookStyleSidebar.OnTabButtonSelectedEventHandler(outlookSidebar_OnTabButtonSelected);
                        outlookSidebar.OnTabHeightChanged += new OutlookStyleSidebar.OnTabHeightChangedEventHandler(outlookSidebar_OnTabHeightChanged);
                        outlookSidebar.OnVisibleItemCountChange += new OutlookStyleSidebar.OnVisibleItemCountChangeEventHandler(outlookSidebar_OnVisibleItemCountChange);*/

            this.Controls.Add(_outlookSidebar);

            // TabButtons

            tabMail = new OutlookStyleTabButton(new _sideBarMenu());
            tabMail.Caption = "Mail";
            tabMail.CaptionDescription = "MailBox";
            tabMail.Image = Properties.Resources.Mail24;
            /*            tabMail.OnTabMouseOver += new OutlookStyleTabButton.OnTabMouseOverEventHandler(TabButton_OnTabMouseOver);
                        tabMail.OnTabMouseLeave += new OutlookStyleTabButton.OnTabMouseLeaveEventHandler(TabButton_OnTabMouseLeave);*/

            DateTimePicker dtPicter = new DateTimePicker();

            tabCalendar = new OutlookStyleTabButton(dtPicter);
            tabCalendar.Caption = "Calendar";
            tabCalendar.CaptionDescription = "Calendar Description";
            tabCalendar.Image = Properties.Resources.Calendar24;
            //tabCalendar.TabButtonFont = new Font("Arial", 10, FontStyle.Italic | FontStyle.Strikeout | FontStyle.Bold);
            //tabCalendar.TabButtonForeColor = Color.Red;
            /*            tabCalendar.OnTabThemeColorChanged += new OutlookStyleTabButton.OnTabThemeColorChangeEventHandler(TabButton_OnTabThemeColorChanged);
                        tabCalendar.OnTabMouseOver += new OutlookStyleTabButton.OnTabMouseOverEventHandler(TabButton_OnTabMouseOver);
                        tabCalendar.OnTabMouseLeave += new OutlookStyleTabButton.OnTabMouseLeaveEventHandler(TabButton_OnTabMouseLeave);*/

            ListView lvContacts = new ListView();
            lvContacts.View = View.Details;
            lvContacts.FullRowSelect = true;

            ColumnHeader clmHeader = new ColumnHeader();
            clmHeader.Text = "My Contacts";
            clmHeader.Width = _outlookSidebar.Width - 5;
            lvContacts.Columns.Add(clmHeader);

            lvContacts.Items.Add("Spiderman");
            lvContacts.Items.Add("Superman");
            lvContacts.Items.Add("He-man");
            lvContacts.Items.Add("Cat-Woman");

            tabCalendar.SelectedImage = Properties.Resources.Contacts24;
            tabCalendar.HoverImage = Properties.Resources.Mail24;

            tabContacts = new OutlookStyleTabButton(lvContacts);
            tabContacts.Caption = "Contacts";
            tabContacts.CaptionDescription = "My Friends";
            tabContacts.Image = Properties.Resources.Contacts24;
            /*            tabContacts.OnTabMouseOver += new OutlookStyleTabButton.OnTabMouseOverEventHandler(TabButton_OnTabMouseOver);
                        tabContacts.OnTabMouseLeave += new OutlookStyleTabButton.OnTabMouseLeaveEventHandler(TabButton_OnTabMouseLeave);*/

            tabTasks = new OutlookStyleTabButton(new DataGridView());
            tabTasks.Caption = "Tasks";
            tabTasks.CaptionDescription = "Never finish";
            tabTasks.Image = Properties.Resources.Tasks24;
            /*            tabTasks.OnTabMouseOver += new OutlookStyleTabButton.OnTabMouseOverEventHandler(TabButton_OnTabMouseOver);
                        tabTasks.OnTabMouseLeave += new OutlookStyleTabButton.OnTabMouseLeaveEventHandler(TabButton_OnTabMouseLeave);*/

            //tabTasks.Enabled = false;

            tabShortcut = new OutlookStyleTabButton(new RichTextBox());
            tabShortcut.Caption = "Shortcut";
            tabShortcut.CaptionDescription = "";
            tabShortcut.Image = Properties.Resources.Shortcut24;
            /*            tabShortcut.OnTabMouseOver += new OutlookStyleTabButton.OnTabMouseOverEventHandler(TabButton_OnTabMouseOver);
                        tabShortcut.OnTabMouseLeave += new OutlookStyleTabButton.OnTabMouseLeaveEventHandler(TabButton_OnTabMouseLeave);*/

            _outlookSidebar.TabButtons.Add(tabMail);
            _outlookSidebar.TabButtons.Add(tabCalendar);
            _outlookSidebar.TabButtons.Add(tabContacts);
            _outlookSidebar.TabButtons.Add(tabTasks);
            _outlookSidebar.TabButtons.Add(tabShortcut);

        }
    }
}
