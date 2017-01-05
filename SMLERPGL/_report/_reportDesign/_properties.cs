using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SMLERPGL._reportDesign
{
    public partial class _properties : Form
    {
        public _properties()
        {
            InitializeComponent();
        }

        private void _properties_Load(object sender, EventArgs e)
        {

        }
    }

    public class _propertiesScreen : MyLib._myScreen
    {
        public _propertiesScreen()
        {
            this.AutoSize = true;
            this._table_name = _g.d.gl_design_report._table;
            this._maxColumn = 1;
            this._addTextBox(0, 0, 1, 0, _g.d.gl_design_report._code, 1, 10, 0, true, false, true);
            this._addTextBox(1, 0, 1, 0, _g.d.gl_design_report._name1, 1, 10, 0, true, false, true);
            this._addTextBox(2, 0, 1, 0, _g.d.gl_design_report._name2, 1, 10, 0, true, false, true);
        }
    }
}