using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace SMLReport._formReport
{
    public partial class _openFormServer : Form
    {
        public _openFormServer()
        {
            InitializeComponent();
            this.Load += new EventHandler(_openFormServer_Load);
        }

        void _openFormServer_Load(object sender, EventArgs e)
        {
            // disable tools button
            this._myDataList1._buttonDelete.Enabled = false;
            this._myDataList1._buttonSelectAll.Enabled = false;
            this._myDataList1._buttonNewFromTemp.Enabled = false;
            this._myDataList1._buttonNew.Enabled = false;
            this._myDataList1._buttonClose.Click += new EventHandler(_buttonClose_Click);

            // _dataList Use
            this._myDataList1._gridData._isEdit = false;
            this._myDataList1._lockRecord = true;
            this._myDataList1._loadViewFormat(_g.g._open_formdesign_screen, MyLib._myGlobal._userSearchScreenGroup, false);
            this._myDataList1._referFieldAdd(_g.d.formdesign._formcode, 1);
            //this._myDataList1._loadViewData(0);

            this._myDataList1._gridData._mouseDoubleClick += new MyLib.MouseDoubleClickHandler(_gridData__mouseDoubleClick);

        }

        void _buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void _gridData__mouseDoubleClick(object sender, MyLib.GridCellEventArgs e)
        {
            if (e._row != -1)
            {
                if (_selectedFormDesign != null)
                {
                    string __tmp = ((MyLib._myGrid)sender)._cellGet(e._row, 0).ToString();
                    _selectedFormDesign(this, ((MyLib._myGrid)sender)._cellGet(e._row, 0).ToString());
                }
                this.Close();
            }
        }

        public event AfterSelectOpenFormDesign _selectedFormDesign;

        private void _manualLoadData_Click(object sender, EventArgs e)
        {
            if (_selectedFormDesign != null)
            {
                _selectedFormDesign(this, textBox1.Text);
            }
            this.Close();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Escape:
                    this.Close();
                    return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

    }

    public delegate void AfterSelectOpenFormDesign(object sender, string formDesignGuid);
}
