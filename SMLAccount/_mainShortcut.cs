using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.IO;

namespace SMLAccount
{
    public partial class _mainShortcut : UserControl
    {
        public _mainShortcut()
        {
            //this.ControlAdded += new ControlEventHandler(_mainShortcut_ControlAdded);

            InitializeComponent();

            if (MyLib._myGlobal._isVersionEnum == MyLib._myGlobal._versionType.SMLPOSStarter)
            {
                this._reportGroupbox.Visible = false;
            }

            this.DoubleBuffered = true;

            SetStyle(ControlStyles.DoubleBuffer, true);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.ResizeRedraw, true);

            //this.BackgroundImage = global::SMLPOS.Properties.Resources.Abstract_Blue_backgrounds_3;
            //this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;

            // bind item event
            _bindClickItem(this._beginFlowPanel);
            _bindClickItem(this._soFlowPanel);
            _bindClickItem(this._icFlowPanel);
            _bindClickItem(this._arFlowPanel);

            if (MyLib._myGlobal._OEMVersion.Equals("SINGHA"))
                this._labelProgramName.Text = MyLib._myGlobal._programName;

            this._checkboxShowWelcomeScreen.CheckedChanged += new EventHandler(_checkboxShowWelcomeScreen_CheckedChanged);

            this._initCheckBox();
            this._refreshCustomShortCut();
        }

        void _initCheckBox()
        {
            try
            {
                if (File.Exists(MyLib._myGlobal._posConfigOpenWelcomeScreen))
                {
                    XmlDocument xDoc = new XmlDocument();
                    xDoc.Load(MyLib._myGlobal._posConfigOpenWelcomeScreen);
                    xDoc.DocumentElement.Normalize();

                    XmlElement __xRoot = xDoc.DocumentElement;

                    XmlNodeList __xReader = __xRoot.GetElementsByTagName("openDisplay");
                    if (__xReader.Count > 0)
                    {
                        XmlNode __xFirstNode = __xReader.Item(0);
                        if (__xFirstNode.NodeType == XmlNodeType.Element)
                        {
                            XmlElement __xTable = (XmlElement)__xFirstNode;
                            if (((int)MyLib._myGlobal._decimalPhase(__xTable.InnerText)) == 1)
                            {
                                _checkboxShowWelcomeScreen.Checked = true;
                            }
                        }
                    }
                }

            }
            catch
            {
            }
        }

        void _checkboxShowWelcomeScreen_CheckedChanged(object sender, EventArgs e)
        {
            //write config to sml config
            StringBuilder __xmlStr = new StringBuilder(String.Concat(MyLib._myGlobal._xmlHeader, "<node>"));
            __xmlStr.Append(String.Concat("<openDisplay>", ((this._checkboxShowWelcomeScreen.Checked) ? "1" : "0"), "</openDisplay>"));
            __xmlStr.Append(String.Concat("</node>"));

            StreamWriter __sr = File.CreateText(MyLib._myGlobal._posConfigOpenWelcomeScreen);
            __sr.WriteLine(__xmlStr.ToString());
            __sr.Close();
        }

        void _myFlowLayoutPanel_ControlAdded(object sender, ControlEventArgs e)
        {
            if (e.Control.GetType() == typeof(MyLib._myImageButton))
            {
                MyLib._myImageButton __button = (MyLib._myImageButton)e.Control;
                __button.Click += new EventHandler(__buttonMenu_Click);
            }
        }

        void __buttonMenu_Click(object sender, System.EventArgs e)
        {
            MyLib._myImageButton __button = (MyLib._myImageButton)sender;
            MouseEventArgs clickEvent = (MouseEventArgs)e;
            if (clickEvent.Button == System.Windows.Forms.MouseButtons.Right)
            {
                if (__button._id != null)
                {
                    if (MessageBox.Show("คุณต้องการลบทางลัด " + __button.ButtonText + " หรือไม่", "ลบทางลัด", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                    {
                        // delete short cut
                        string __getshortcut = "delete from homeshortcut where roworder = " + __button._id;
                        MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                        string __result = __myFrameWork._queryInsertOrUpdate(MyLib._myGlobal._databaseName, __getshortcut);

                        if (__result.Length == 0)
                        {
                            MessageBox.Show("สำเร็จ");
                            this._refreshCustomShortCut();
                        }
                        else
                            MessageBox.Show(__result);


                    }
                }
            }
            else
            {
                if (this._menuButtonClick != null)
                {
                    this._menuButtonClick(__button.ButtonText, ((__button.Name != null && __button.Name.Trim().Length > 0) ? __button.Name : ""), ((__button.Tag != null && __button.Tag.ToString().Length > 0) ? __button.Tag.ToString() : ""));
                }
            }
        }

        void _bindClickItem(Control __obj)
        {
            for (int __i = 0; __i < __obj.Controls.Count; __i++)
            {
                // check tag versoin ด้วย
                if (__obj.Controls[__i].GetType() == typeof(MyLib._myImageButton))
                {

                    MyLib._myImageButton __button = (MyLib._myImageButton)__obj.Controls[__i];
                    _buttonRemoveByVersion(__button);
                    __button.Click += new EventHandler(__buttonMenu_Click);
                }
            }
        }

        void _buttonRemoveByVersion(MyLib._myImageButton button)
        {
            string __tag = "";
            switch (MyLib._myGlobal._isVersionEnum)
            {
                case MyLib._myGlobal._versionType.SMLAccount:
                    __tag = "&2&";
                    break;
                case MyLib._myGlobal._versionType.SMLAccountProfessional:
                    __tag = "&3&";
                    break;
                case MyLib._myGlobal._versionType.SMLPOS:
                    __tag = "&6&";
                    break;
                case MyLib._myGlobal._versionType.SMLPOSLite:
                    __tag = "&7&";
                    break;
                case MyLib._myGlobal._versionType.SMLTomYumGoong:
                    __tag = "&8&";
                    break;
                case MyLib._myGlobal._versionType.SMLPOSStarter:
                    __tag = "&10&";
                    break;
            }

            if (button.Tag.ToString().Length > 0)
            {
                if (button.Tag.ToString().IndexOf(__tag) == -1)
                {
                    button.Visible = false;
                }
            }
        }

        public event _menuButtonclick _menuButtonClick;

        private void _flowMenu_MouseMove(object sender, MouseEventArgs e)
        {
            MyLib._myFlowLayoutPanel __obj = (MyLib._myFlowLayoutPanel)sender;
            __obj.Focus();
        }

        int _addShortCutPanel = 0;

        private void _FlowPanel_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                MyLib._myFlowLayoutPanel __panel = (MyLib._myFlowLayoutPanel)sender;

                if (__panel.Name == this._beginFlowPanel.Name)
                {
                    _addShortCutPanel = 0;
                }
                else if (__panel.Name == this._soFlowPanel.Name)
                {
                    _addShortCutPanel = 1;

                }
                else if (__panel.Name == this._icFlowPanel.Name)
                {
                    _addShortCutPanel = 2;

                }
                else if (__panel.Name == this._arFlowPanel.Name)
                {
                    _addShortCutPanel = 3;

                }
                ContextMenu m = new ContextMenu();
                MenuItem __addshortCutMenuItem = new MenuItem("สร้างทางลัด");
                __addshortCutMenuItem.Click -= __addshortCutMenuItem_Click;
                __addshortCutMenuItem.Click += __addshortCutMenuItem_Click;
                m.MenuItems.Add(__addshortCutMenuItem);

                // Control panel = (Control)sender;
                /* int currentMouseOverRow = dataGridView1.HitTest(e.X, e.Y).RowIndex;

                 if (currentMouseOverRow >= 0)
                 {
                     m.MenuItems.Add(new MenuItem(string.Format("Do something to row {0}", currentMouseOverRow.ToString())));
                 }*/
                m.Show(__panel, new Point(e.X, e.Y));
            }
        }

        public event _addMenuShortcut _addShortcut;
        void __addshortCutMenuItem_Click(object sender, EventArgs e)
        {
            // show pop up tree menu

            if (this._addShortcut != null)
            {
                this._addShortcut(this, this._addShortCutPanel);
            }
        }

        public void _refreshCustomShortCut()
        {
            string __getshortcut = "select roworder, menu_code, menu_name, menu_tag, menu_group from homeshortcut order by menu_group ";
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            DataSet __result = __myFrameWork._query(MyLib._myGlobal._databaseName, __getshortcut);

            if (__result.Tables.Count > 0 && __result.Tables[0].Rows.Count > 0)
            {
                for (int panel = 0; panel <= 3; panel++)
                {
                    DataRow[] __rows = __result.Tables[0].Select("menu_group=" + panel);
                    if (__rows.Length > 0)
                    {
                        switch (panel)
                        {
                            case 0:
                                _drawShortcut(this._beginFlowPanel, __rows);
                                break;
                            case 1:
                                _drawShortcut(this._soFlowPanel, __rows);
                                break;
                            case 2:
                                _drawShortcut(this._icFlowPanel, __rows);
                                break;
                            case 3:
                                _drawShortcut(this._arFlowPanel, __rows);
                                break;
                        }
                    }
                }
            }
            // delete all custom and add


            /*
             * 
            this.menu_setup_staff._id = null;
            this.menu_setup_staff.BackColor = System.Drawing.Color.Transparent;
            this.menu_setup_staff.BaseColor = System.Drawing.Color.Transparent;
            this.menu_setup_staff.ButtonColor = System.Drawing.Color.Transparent;
            this.menu_setup_staff.ButtonText = "กำหนดพนักงาน";
            this.menu_setup_staff.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.menu_setup_staff.ForeColor = System.Drawing.Color.Black;
            this.menu_setup_staff.GlowColor = System.Drawing.Color.Transparent;
            this.menu_setup_staff.HighlightColor = System.Drawing.Color.Transparent;
            this.menu_setup_staff.Image = global::SMLAccount.Properties.Resources.users_family;
            this.menu_setup_staff.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.menu_setup_staff.ImageSize = new System.Drawing.Size(36, 36);
            this.menu_setup_staff.Location = new System.Drawing.Point(115, 0);
            this.menu_setup_staff.Margin = new System.Windows.Forms.Padding(0);
            this.menu_setup_staff.Name = "menu_setup_staff";
            this.menu_setup_staff.ResourceName = "กำหนดพนักงาน";
            this.menu_setup_staff.Size = new System.Drawing.Size(115, 90);
            this.menu_setup_staff.TabIndex = 2;
            this.menu_setup_staff.Tag = "&config&&1&&2&&3&&6&&7&&8&";
            this.menu_setup_staff.TextAlign = System.Drawing.ContentAlignment.TopCenter;

            */
        }

        void _drawShortcut(Control control, DataRow[] rows)
        {
            //MyLib._myFlowLayoutPanel __panel = (MyLib._myFlowLayoutPanel) control;

            // remove id = null
            for (int __i = control.Controls.Count - 1; __i >= 0; __i--)
            {
                // check tag versoin ด้วย
                if (control.Controls[__i].GetType() == typeof(MyLib._myImageButton))
                {

                    MyLib._myImageButton __button = (MyLib._myImageButton)control.Controls[__i];

                    if (__button._id != null)
                    {
                        control.Controls.RemoveAt(__i);
                    }
                    //_buttonRemoveByVersion(__button);
                    //__button.Click += new EventHandler(__buttonMenu_Click);
                }
            }

            // draw new icon

            foreach (DataRow row in rows)
            {
                MyLib._myImageButton __newButton = new MyLib._myImageButton();
                __newButton._id = row["roworder"].ToString();
                __newButton.BackColor = System.Drawing.Color.Transparent;
                __newButton.BaseColor = System.Drawing.Color.Transparent;
                __newButton.ButtonColor = System.Drawing.Color.Transparent;
                __newButton.ButtonText = row[_g.d.homeshortcut._menu_name].ToString();
                __newButton.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                __newButton.ForeColor = System.Drawing.Color.Black;
                __newButton.GlowColor = System.Drawing.Color.Transparent;
                __newButton.HighlightColor = System.Drawing.Color.Transparent;
                __newButton.Image = global::SMLAccount.Properties.Resources.window;
                __newButton.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
                __newButton.ImageSize = new System.Drawing.Size(36, 36);
                __newButton.Margin = new System.Windows.Forms.Padding(0);
                __newButton.Name = row[_g.d.homeshortcut._menu_code].ToString();
                __newButton.ResourceName = row[_g.d.homeshortcut._menu_name].ToString();
                __newButton.Size = new System.Drawing.Size(115, 90);
                __newButton.TabIndex = 2;
                if (row[_g.d.homeshortcut._menu_tag].ToString() != "")
                    __newButton.Tag = row[_g.d.homeshortcut._menu_tag].ToString();

                __newButton.TextAlign = System.Drawing.ContentAlignment.TopCenter;

                //__newButton.Click -= new EventHandler(__buttonMenu_Click);
                //__newButton.Click += new EventHandler(__buttonMenu_Click);

                control.Controls.Add(__newButton);
            }

            control.Invalidate();
        }

 
    }

    public delegate void _menuButtonclick(string menuId, string menuText, string menuTag);
    public delegate void _addMenuShortcut(object sender, int panelIndex);

}
