using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace SMLERPControl._ic
{
    public partial class _icTransItemGridSelectUnitForm : Form
    {
        public delegate void _selectUnitCodeEventHandler(int mode, string unitCode);
        public delegate DataTable _loadDataEventHandler();
        //
        public event _selectUnitCodeEventHandler _selectUnitCode;
        public event _loadDataEventHandler _loadData;
        //
        public string _itemCode = "";
        public string _lastCode = "";
        public Boolean _selected = false;

        public _icTransItemGridSelectUnitForm()
        {
            InitializeComponent();
            this.Shown += new EventHandler(_icTransItemGridSelectUnitForm_Shown);
            this._icmainGridUnit._mouseClick += new MyLib.MouseClickHandler(_icmainGridUnit__mouseClick);
        }

        void _icmainGridUnit__mouseClick(object sender, MyLib.GridCellEventArgs e)
        {
            _return(e._row);
        }

        void _return(int row)
        {
            string __unitCode = this._icmainGridUnit._cellGet(row, _g.d.ic_unit_use._code).ToString();
            if (this._selectUnitCode != null && __unitCode.Length > 0)
            {
                this._selected = true;
                this.Close();
                this._selectUnitCode(1,__unitCode);
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                this.Close();
                this._selectUnitCode(0, "");
                return true;
            }
            if (keyData == Keys.Enter)
            {
                _return(this._icmainGridUnit._selectRow);
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        void _icTransItemGridSelectUnitForm_Shown(object sender, EventArgs e)
        {
            if (this._loadData == null)
            {
                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                DataSet __result = __myFrameWork._query(MyLib._myGlobal._databaseName, this._icmainGridUnit._createQueryForLoad(this._itemCode));
                this._icmainGridUnit._loadFromDataTable(__result.Tables[0]);
            }
            else
            {
                this._icmainGridUnit._loadFromDataTable(this._loadData());
            }
            this._icmainGridUnit._selectColumn = 0;
            this._icmainGridUnit._selectRow = 0;
            this._icmainGridUnit._message = "เลือกหน่วยนับโดยใช้ Mouse หรือ Keyboard โดยเลื่อนขึ้นลง แล้วเลือกโดยกด <b>Enter</b> หรือ ยกเลิกโดยกด <b>Esc</b>";
            this._icmainGridUnit.Invalidate();
            this._icmainGridUnit.Focus();
            // Find Row
            this._icmainGridUnit._findAndGotoRow(_g.d.ic_unit_use._code, _lastCode);
        }
    }
}
