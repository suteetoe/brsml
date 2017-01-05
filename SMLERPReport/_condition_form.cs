using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SMLERPReport
{
    public partial class _condition_form : Form
    {
        public delegate void _processClickHandler();
        public event _processClickHandler _processClick;
        public bool _process = false;

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.F11)
            {
                this._clickButtonProcess(); 
                return true;
            }
            else if (keyData == Keys.Escape)
            {
                this.Close(); 
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        public _condition_form()
        {
            InitializeComponent();

            this._button_process.Click += new EventHandler(_button_process_Click);
            this._button_exit.Click += new EventHandler(_button_exit_Click);
        }

        void _button_process_Click(object sender, EventArgs e)
        {
            this._clickButtonProcess();
        }

        void _button_exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void _clickButtonProcess()
        {
            if (this._processClick != null) this._processClick();
        }

        private void _setScreenSize()
        {
            /*if (this._screenCondotionStructureVisible && this._screenCondotionGridVisible)
            {
                this._screen_condition_tab_structure.Width = (int)(this._grouper1.Width / 4);
                this._screen_condition_grid_condition.Width = (int)(this._grouper1.Width / 4);
            }
            else if (!this._screenCondotionStructureVisible && this._screenCondotionGridVisible)
            {
                this._screen_condition_grid_condition.Width = (int)(this._grouper1.Width / 2);
            }
            else if (this._screenCondotionStructureVisible && !this._screenCondotionGridVisible)
            {
                this._screen_condition_tab_structure.Width = (int)(this._grouper1.Width / 2);
            }*/
        }
    }
}