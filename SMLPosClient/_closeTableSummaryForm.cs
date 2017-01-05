using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml.Serialization;

namespace SMLPosClient
{
    public partial class _closeTableSummaryForm : Form
    {
        public string _saleCode = "";
        SMLPOSControl._tableSearchLevelMenuControl _table = null;
        _posClientForm _clientForm = null;
        bool _isCloseTable = true;

        public _closeTableSummaryForm(SMLPOSControl._tableSearchLevelMenuControl table, string saleCode, bool isCloseTable)
        {
            // ถามสมาชิก

            InitializeComponent();

            this._saleCode = saleCode;
            this._table = table;
            this._isCloseTable = isCloseTable;

            _clientForm = new _posClientForm(this._saleCode);
            // _clientForm._tableNumberSelect 
            _clientForm.Dock = DockStyle.Fill;
            _clientForm._afterCloseForm += (s1) =>
            {
                this.Dispose();
            };
            // ถาม สมาชิก

            _clientForm._getTableOrderForPay(table, this._isCloseTable);

            this._mainPosPanel.Controls.Add(_clientForm);
        }

        private void _processButton_Click(object sender, EventArgs e)
        {
            _process();
        }

        void _process()
        {
            string __foodDiscount = this._clientForm._foodDiscountWord;
            Boolean __isPromotionProcess = this._clientForm._isProcessPromotion;
            string __totalDiscount = this._clientForm._discountWord;
            string __serviceCharge = this._clientForm._serviceChargeWord;

            _clientForm._reset();

            _clientForm._setTableSelect(this._table);

            if (_cusidTextbox.Text != "")
            {
                _clientForm._command("&command&," + _cusidTextbox.Text, "");
            }
            _clientForm._getTableOrderForPay(this._table, this._isCloseTable);

            if (__serviceCharge.Length > 0)
            {
                _clientForm._command("f8@" + __serviceCharge, "");
            }

            // ลดอาหาร
            if (__foodDiscount.Length > 0)
            {
                _clientForm._command("f7@" + __foodDiscount, "");
            }

            // promotion process
            if (__isPromotionProcess)
            {
                _clientForm._command("&command&,*11", "");
            }
            else
            {
                _clientForm._command("&command&,*10" , "");
            }

            // total discount
            if (__totalDiscount.Length > 0)
            {
                _clientForm._command("&command&,*9*" + __totalDiscount, "");
            }

        }

        private MyLib._searchDataFull _searchItem;
        private void _findCusButton_Click(object sender, EventArgs e)
        {
            _searchItem = new MyLib._searchDataFull();
            this._searchItem.Text = MyLib._myGlobal._resource("ค้นหาสมาชิก");
            this._searchItem._dataList._loadViewFormat(_g.g._search_screen_ar_dealer, MyLib._myGlobal._userSearchScreenGroup, false);
            this._searchItem.WindowState = FormWindowState.Maximized;
            this._searchItem._dataList._gridData._mouseClick += (s1, e1) =>
            {
                string __arCode = this._searchItem._dataList._gridData._cellGet(this._searchItem._dataList._gridData._selectRow, _g.d.ar_dealer._table + "." + _g.d.ar_dealer._code).ToString();
                this._searchItem.Close();
                this._searchItem.Dispose();
                this._cusidTextbox.Text = __arCode;
                this._process();
                //this._orderScreen._setDataStr(_g.d.table_order_doc._cust_code, __arCode);
                //this._orderScreen._setDataStr(_g.d.table_order_doc._cust_name, __arName);
            };
            this._searchItem._searchEnterKeyPress += (s1, e1) =>
            {
                string __arCode = this._searchItem._dataList._gridData._cellGet(this._searchItem._dataList._gridData._selectRow, _g.d.ar_dealer._table + "." + _g.d.ar_dealer._code).ToString();
                this._searchItem.Close();
                this._searchItem.Dispose();
                this._cusidTextbox.Text = __arCode;
                this._process();
                //this._orderScreen._setDataStr(_g.d.table_order_doc._cust_code, __arCode);
                //this._orderScreen._setDataStr(_g.d.table_order_doc._cust_name, __arName);
            };
            this._searchItem.ShowDialog(MyLib._myGlobal._mainForm);

        }

        void _foodDiscount()
        {
            _clientForm._command("f7", "");
        }

        void _disablePromotion()
        {
            _clientForm._command("&command&,*10", "");
        }
        void _enablePromotin()
        {
            _clientForm._command("&command&,*11", "");
        }

        void _totalDiscount()
        {
            _clientForm._command("&command&,*9*", "");
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.F7:
                    {
                        _foodDiscount();
                        return false;
                    }
                case Keys.F8:
                    {
                        _serviceCharge();
                        return false;
                    }
                case Keys.F9 :
                    {
                        _totalDiscount();
                        return false;
                    }
                case Keys.Control | Keys.D1:
                    {
                        _disablePromotion();
                        return false;
                    }
                case Keys.Control | Keys.D2:
                    {
                        _enablePromotin();
                        return false;
                    }
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        void _serviceCharge()
        {
            _clientForm._command("f8", "");
        }

        private void _foodDiscountButton_Click(object sender, EventArgs e)
        {
            _foodDiscount();
        }

        private void _serviceChargeButton_Click(object sender, EventArgs e)
        {
            this._serviceCharge();
        }

    }
}
