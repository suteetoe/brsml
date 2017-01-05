using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace SMLStockImport
{
    static class Program
    {
        public static NotifyIcon _icon = null;
        public static MenuItem _menuExit = null;
        static MainForm _mainForm = null;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            /*
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());*/
            try
            {
                _icon = new NotifyIcon();
                _icon.Text = "SML Stock Import";
                System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
                _icon.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
                _icon.Visible = true;

                _mainForm = new MainForm();

                _icon.MouseDoubleClick += delegate
                {
                    _mainForm.Show();
                    _mainForm.WindowState = FormWindowState.Normal;
                };

                _mainForm.Resize += delegate
                {
                    if (FormWindowState.Minimized == _mainForm.WindowState)
                        _mainForm.Hide();
                };

                Application.Run();
            }
            catch
            {

            }
        }
    }
}
