using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SMLPosClient
{
    public partial class _selectLanguageForm : Form
    {
        /// <summary>
        /// เลือก ภาษาอะไร 0=ไทย,1=en,2=จีน,3=fn
        /// </summary>
        public int _languageIndex = 0;
 
        public _selectLanguageForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)
        {
        }

        private void button3_Click(object sender, EventArgs e)
        {
        }

        private void button4_Click(object sender, EventArgs e)
        {
            _languageIndex = 3;
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
        }

        private void button6_Click(object sender, EventArgs e)
        {
            _languageIndex = 5;
            this.Close();
        }

        private void _fancyButton1_Click(object sender, EventArgs e)
        {
            _languageIndex = 0;
            this.Close();
        }

        private void _fancyButton3_Click(object sender, EventArgs e)
        {
            _languageIndex = 1;
            this.Close();
        }

        private void _fancyButton5_Click(object sender, EventArgs e)
        {
            _languageIndex = 2;
            this.Close();
        }

        private void _fancyButton4_Click(object sender, EventArgs e)
        {
            _languageIndex = 4;
            this.Close();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData.Equals(Keys.Escape))
            {
                this.Close();
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
