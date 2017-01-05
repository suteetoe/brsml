using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SMLERPIC
{
    public partial class _icBarcodeDiscountSelectDayForm : Form
    {
        public _icBarcodeDiscountSelectDayForm()
        {
            InitializeComponent();
        }

        private void _cancelButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }

        private void _okButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;

        }

        private void checkboxChanged(object sender, EventArgs e)
        {
            CheckBox __checkbox = (CheckBox)sender;
            if (__checkbox.Name == this._allDayCheckbox.Name)
            {
                if (this._allDayCheckbox.Checked)
                {
                    this._sunCheckbox.Checked =
                        this._monCheckbox.Checked =
                        this._tueCheckbox.Checked =
                        this._webCheckbox.Checked =
                        this._thuCheckbox.Checked =
                        this._friCheckbox.Checked =
                        this._satCheckbox.Checked = true;
                }
                else
                {
                    if (this._sunCheckbox.Checked && this._monCheckbox.Checked && this._tueCheckbox.Checked && this._webCheckbox.Checked && this._thuCheckbox.Checked && this._friCheckbox.Checked && this._satCheckbox.Checked)
                    {
                        this._allDayCheckbox.Checked = true;
                    }
                }
            }
            else
            {
                if (this._sunCheckbox.Checked && this._monCheckbox.Checked && this._tueCheckbox.Checked && this._webCheckbox.Checked && this._thuCheckbox.Checked && this._friCheckbox.Checked && this._satCheckbox.Checked)
                {
                    this._allDayCheckbox.Checked = true;
                }
                else
                {
                    this._allDayCheckbox.Checked = false;
                }
            }
        }

        private void checkboxStateChange(object sender, EventArgs e)
        {

        }
    }
}
