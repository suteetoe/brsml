using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SMLPosClient
{
    public partial class _posFinishForm : Form
    {
        [DllImport("user32.dll", EntryPoint = "SetForegroundWindow")]
        static extern bool SetForegroundWindow(int hWnd);

        public _posFinishForm(SMLPOSControl._posScreenConfig config)
        {
            InitializeComponent();
            Timer __time = new Timer();
            __time.Interval = ((config._pos_delay_finish_screen > 0 ) ? config._pos_delay_finish_screen : 5) * 1000; 
            __time.Tick += new EventHandler(__time_Tick);

            if (config._manual_close_finish_screen == false)
            {
                __time.Start();
            }

            this.Load += new EventHandler(_posFinishForm_Load);
        }

        void _posFinishForm_Load(object sender, EventArgs e)
        {
            SetForegroundWindow(this.Handle.ToInt32());
            this.Focus();
        }

        void __time_Tick(object sender, EventArgs e)
        {
            this.Dispose();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                this.Dispose();
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
