using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.IO;

namespace SMLPosClient
{
    public partial class _posProductControl : UserControl
    {
        private Image _topPanelBackgroundImage;
        private Image _displayNamePanelBackgroundImage;
        private Image _displayPricePanelBackgroundImage;
        private Image _buttomPanelBackgroundImage;
        private Image _inputTextBoxPanelBackgroundImage;
        private Image _totalLabelBackgroundImage;

        public _posProductControl()
        {
            InitializeComponent();
            this.DoubleBuffered = true;
            this._inputTextBox.KeyDown += new KeyEventHandler(_inputTextBox_KeyDown);
            this.Load += new EventHandler(_posProductControl_Load);
            this._buttomPanel.Resize += new EventHandler(_buttomPanel_Resize);
            // ทำตีมแบบ Background
            this._topPanel.Paint += new PaintEventHandler(_topPanel_Paint);
            this._displayNamePanel.Paint += new PaintEventHandler(_displayNamePanel_Paint);
            this._displayPricePanel.Paint += new PaintEventHandler(_displayPricePanel_Paint);
            this._buttomPanel.Paint += new PaintEventHandler(_buttomPanel_Paint);
            this._inputTextBoxPanel.Paint += new PaintEventHandler(_inputTextBoxPanel_Paint);
            // พิเศษแบบ Label
            this._totalLabel.Paint += new PaintEventHandler(_totalLabel_Paint);
            // ขาย
            this._button_change_theme.Click += new EventHandler(_button_change_theme_Click);
        }

        void _button_change_theme_Click(object sender, EventArgs e)
        {
            _save_sell();
            _print();
        }

      

        private string _linenumber_dataGrid = "_linenumber";
        private string _code_dataGrid = "_code";
        private string _description_dataGrid = "_description";
        private string _qty_dataGrid = "_qty";
        private string _price_dataGrid = "_price";
        private string _subTotal_dataGrid = "_subTotal";



        string _docno = "";
        void _save_sell()
        {
            try
            {
                string __name_customer = this._nameLabel.Text;

                ArrayList __data_deetail = new ArrayList();
                DataTable __dt = new DataTable();

                int __row = this._dataGridView.Rows.Count;

                //  ic_trans
                int __trand_type = 2;
                int __trans_flaag = 52;
                DateTime __date_now = DateTime.Now;
                string __doc_date = "'" + __date_now.Year.ToString() + "-" + __date_now.Month + "-" + __date_now.Day + "'";
                string __doc_no = _gen_docno();
                _docno = __doc_no;
                int __inquiry_type = 1;
                double __total_value = double.Parse(this._totalLabel.Text);
                double __total_vat_value = double.Parse(this._totalLabel.Text) * 0.07;
                int __status = 1;
                string __ar_code = _get_ar_code();

                // ic_trans_detail
                for (int __i = 0; __i < __row; __i++)
                {
                    DataGridViewRow __dr = this._dataGridView.Rows[__i];
                    string __barcode = __dr.Cells[this._code_dataGrid].Value.ToString();
                    int __line_number = int.Parse(__dr.Cells[this._linenumber_dataGrid].Value.ToString());
                    DataTable __item_data = _get_item_data(__barcode);
                    string __item_code = __item_data.Rows[0][_g.d.ic_inventory_barcode._ic_code].ToString();
                    string __item_name = __item_data.Rows[0][_g.d.ic_inventory._name_1].ToString();

                    string __unit_code = __item_data.Rows[0][_g.d.ic_inventory_barcode._unit_code].ToString();
                    string __wh_code = __item_data.Rows[0][_g.d.ic_inventory_barcode._wh_code].ToString();
                    string __shelf_code = __item_data.Rows[0][_g.d.ic_inventory_barcode._shelf_code].ToString();
                    double __item_price = double.Parse(__dr.Cells[this._price_dataGrid].Value.ToString());
                    double __item_qty = double.Parse(__dr.Cells[this._qty_dataGrid].Value.ToString());
                    double __total = double.Parse(__dr.Cells[this._subTotal_dataGrid].Value.ToString());

                    string __data = "";
                    __data += __trand_type + " , " + __trans_flaag + " , " + __doc_date + " , " + __doc_no;
                    __data += " , " + __inquiry_type + " , " + __ar_code + " , '" + __item_code + "' , '" + __item_name + "'";
                    __data += " , '" + __unit_code + "' , " + __item_qty + " , " + __item_price + " , " + __total + " , " + __line_number + " , '" + __wh_code + "'";
                    __data += " , '" + __shelf_code + "'";
                    __data_deetail.Add(__data);
                }

                StringBuilder __my_query = new StringBuilder();
                if (__row > 0)
                {
                    string __column_head = _g.d.ic_trans._trans_type + " , " + _g.d.ic_trans._trans_flag + " , " + _g.d.ic_trans._doc_date;
                    __column_head += " , " + _g.d.ic_trans._doc_no + " , " + _g.d.ic_trans._inquiry_type + " , " + _g.d.ic_trans._total_value;
                    __column_head += " , " + _g.d.ic_trans._total_vat_value + " , " + _g.d.ic_trans._status + " , " + _g.d.ic_trans._cust_code;
                    string __data_head = __trand_type + " , " + __trans_flaag + " , " + __doc_date + " , " + __doc_no + " , " + __inquiry_type + " , " + __total_value + " , " + __total_vat_value + " , " + __status + " , " + __ar_code;

                    string __column_detail = _g.d.ic_trans_detail._trans_type + " , " + _g.d.ic_trans_detail._trans_flag + " , " + _g.d.ic_trans_detail._doc_date;
                    __column_detail += " , " + _g.d.ic_trans_detail._doc_no + " , " + _g.d.ic_trans_detail._inquiry_type + " , " + _g.d.ic_trans_detail._cust_code;
                    __column_detail += " , " + _g.d.ic_trans_detail._item_code + " , " + _g.d.ic_trans_detail._item_name + " , " + _g.d.ic_trans_detail._unit_code;
                    __column_detail += " , " + _g.d.ic_trans_detail._qty + " , " + _g.d.ic_trans_detail._price + " , " + _g.d.ic_trans_detail._sum_amount;
                    __column_detail += " , " + _g.d.ic_trans_detail._line_number + " , " + _g.d.ic_trans_detail._wh_code + " , " + _g.d.ic_trans_detail._shelf_code;

                    // pack query
                    __my_query.Append(MyLib._myGlobal._xmlHeader + "<node>");
                    //Head
                    __my_query.Append("<query>insert into " + _g.d.ic_trans._table + "(" + __column_head + ")" + " values (" + __data_head + ")" + "</query>");
                    //Detail
                    for (int __i = 0; __i < __row; __i++)
                    {
                        __my_query.Append("<query>insert into " + _g.d.ic_trans_detail._table + "(" + __column_detail + ")" + " values (" + __data_deetail[__i].ToString() + ")" + "</query>");
                    }
                    __my_query.Append("</node>");


                }

                MyLib._myFrameWork __my_framwork = new MyLib._myFrameWork();

                string __result = __my_framwork._queryList(MyLib._myGlobal._databaseName, __my_query.ToString());
                if (__result.Length == 0)
                {
                    this._dataGridView.Rows.Clear();
                    this._nameLabel.Text = "";
                    this._totalLabel.Text = "";
                    this._priceLabel.Text = "";
                    this._label.Text = "";
                }


            }
            catch (Exception __ex)
            {
                string __error = __ex.Message;
            }
        }

        void _print()
        {
            DataTable __dt_trans_head = new DataTable();
            DataTable __dt_trans_detail = new DataTable();
            string __where_main = _g.d.ic_trans._doc_no + " = " + this._docno;
            string __where_detail = _g.d.ic_trans_detail._doc_no + " = " + this._docno;

            string __column_head = _g.d.ic_trans._trans_type + " , " + _g.d.ic_trans._trans_flag + " , " + _g.d.ic_trans._doc_date;
            __column_head += " , " + _g.d.ic_trans._doc_no + " , " + _g.d.ic_trans._inquiry_type + " , " + _g.d.ic_trans._total_value;
            __column_head += " , " + _g.d.ic_trans._total_vat_value + " , " + _g.d.ic_trans._status + " , " + _g.d.ic_trans._cust_code;

            string __column_detail = _g.d.ic_trans_detail._trans_type + " , " + _g.d.ic_trans_detail._trans_flag + " , " + _g.d.ic_trans_detail._doc_date;
            __column_detail += " , " + _g.d.ic_trans_detail._doc_no + " , " + _g.d.ic_trans_detail._inquiry_type + " , " + _g.d.ic_trans_detail._cust_code;
            __column_detail += " , " + _g.d.ic_trans_detail._item_code + " , " + _g.d.ic_trans_detail._item_name + " , " + _g.d.ic_trans_detail._unit_code;
            __column_detail += " , " + _g.d.ic_trans_detail._qty + " , " + _g.d.ic_trans_detail._price + " , " + _g.d.ic_trans_detail._sum_amount;
            __column_detail += " , " + _g.d.ic_trans_detail._line_number;

            try
            {
                StringBuilder __my_query = new StringBuilder();
                __my_query.Append(MyLib._myGlobal._xmlHeader + "<node>");
                __my_query.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + __column_head + " from " + _g.d.ic_trans._table + " where " + __where_main));
                __my_query.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + __column_detail + " from " + _g.d.ic_trans_detail._table + " where " + __where_detail));
                __my_query.Append("</node>");

                MyLib._myFrameWork __my_framwork = new MyLib._myFrameWork();
                ArrayList __getData = __my_framwork._queryListGetData(MyLib._myGlobal._databaseName, __my_query.ToString());

                __dt_trans_head = (((DataSet)__getData[0]).Tables[0]);
                __dt_trans_detail = (((DataSet)__getData[1]).Tables[0]);
            }
            catch (Exception __e)
            {
                string __error = __e.Message;
            }
        }

        /// <summary>
        /// get Item Data
        /// </summary>
        /// <param name="__barcode"></param>
        /// <returns></returns>
        DataTable _get_item_data(string __barcode)
        {
            DataTable __result = new DataTable();
            string __where = _g.d.ic_inventory_barcode._barcode + " = " + "'" + __barcode + "'";
            try
            {
                StringBuilder __my_query = new StringBuilder();
                __my_query.Append(MyLib._myGlobal._xmlHeader + "<node>");


                __my_query.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.ic_inventory_barcode._ic_code + " , "
                    + _g.d.ic_inventory_barcode._unit_code + " , " + _g.d.ic_inventory_barcode._wh_code + " , " + _g.d.ic_inventory_barcode._shelf_code
                   + " , " + "(select " + _g.d.ic_inventory._name_1 + " from " + _g.d.ic_inventory._table + " where " + _g.d.ic_inventory._code + " = "
                   + _g.d.ic_inventory_barcode._table + "." + _g.d.ic_inventory_barcode._ic_code + ") as " + _g.d.ic_inventory._name_1
                    + " from " + _g.d.ic_inventory_barcode._table + " where " + __where));
                __my_query.Append("</node>");

                MyLib._myFrameWork __my_framwork = new MyLib._myFrameWork();
                ArrayList __getData = __my_framwork._queryListGetData(MyLib._myGlobal._databaseName, __my_query.ToString());

                __result = (((DataSet)__getData[0]).Tables[0]);

            }
            catch (Exception __e)
            {
                string __error = __e.Message;
            }
            return __result;
        }

        /// <summary>
        /// get ar code
        /// </summary>
        /// <returns></returns>
        string _get_ar_code()
        {
            string __result = "";
            string __where = _g.d.ar_customer._name_1 + " = " + "'" + this._nameLabel.Text + "'";
            try
            {
                StringBuilder __my_query = new StringBuilder();
                __my_query.Append(MyLib._myGlobal._xmlHeader + "<node>");


                __my_query.Append(MyLib._myUtil._convertTextToXmlForQuery("select max(" + _g.d.ar_customer._code + ") from " + _g.d.ar_customer._table + " where " + __where));
                __my_query.Append("</node>");

                MyLib._myFrameWork __my_framwork = new MyLib._myFrameWork();
                ArrayList __getData = __my_framwork._queryListGetData(MyLib._myGlobal._databaseName, __my_query.ToString());
                string __data = (((DataSet)__getData[0]).Tables[0]).Rows[0][0].ToString();
                __result = "'" + __data + "'";
            }
            catch (Exception __e)
            {
                string __error = __e.Message;
            }
            return __result;
        }

        /// <summary>
        /// generate doc no
        /// </summary>
        /// <returns></returns>
        string _gen_docno()
        {
            string __result = "";

            DateTime __date_now = DateTime.Now;
            string __doc_date = "'" + __date_now.Year.ToString() + "-" + __date_now.Month + "-" + __date_now.Day + "'";
            string __where = _g.d.ic_trans._doc_date + " = " + __doc_date;
            try
            {
                StringBuilder __my_query = new StringBuilder();
                __my_query.Append(MyLib._myGlobal._xmlHeader + "<node>");
                __my_query.Append(MyLib._myUtil._convertTextToXmlForQuery("select max(" + _g.d.ic_trans._doc_no + ") from " + _g.d.ic_trans._table + " where " + __where));
                __my_query.Append("</node>");

                MyLib._myFrameWork __my_framwork = new MyLib._myFrameWork();
                ArrayList __getData = __my_framwork._queryListGetData(MyLib._myGlobal._databaseName, __my_query.ToString());
                //this._icmainScreenTop._loadData(((DataSet)__getData[0]).Tables[0]);
                __result = (((DataSet)__getData[0]).Tables[0]).Rows[0][0].ToString();

                if (__result == "") __result = "M5208-00000";  //  format doc no
                string[] __split = __result.Split('-');
                int __number = int.Parse(__split[1].ToString()) + 1;
                string __gen_number = __number.ToString();

                if (__gen_number.Length == 1) __gen_number = "0000" + __number.ToString();
                else if (__gen_number.Length == 2) __gen_number = "000" + __number.ToString();
                else if (__gen_number.Length == 3) __gen_number = "00" + __number.ToString();
                else if (__gen_number.Length == 4) __gen_number = "0" + __number.ToString();
                else if (__gen_number.Length == 5) __gen_number = __number.ToString();

                __result = "'" + __split[0] + "-" + __gen_number + "'";
            }
            catch (Exception __e)
            {
                string __error = __e.Message;
            }
            return __result;
        }

        void _inputTextBoxPanel_Paint(object sender, PaintEventArgs e)
        {
            // กรอบป้อนข้อมูล
            if (_inputTextBoxPanelBackgroundImage != null)
            {
                e.Graphics.DrawImage(this._inputTextBoxPanelBackgroundImage, 0, 0, ((MyLib._myPanel)sender).Width, ((MyLib._myPanel)sender).Height);
            }
        }

        void _buttomPanel_Paint(object sender, PaintEventArgs e)
        {
            // กรอบใหญ่ด้านล่าง
            if (_buttomPanelBackgroundImage != null)
            {
                e.Graphics.DrawImage(_buttomPanelBackgroundImage, 0, 0, ((MyLib._myPanel)sender).Width, ((MyLib._myPanel)sender).Height);
            }
        }

        void _displayPricePanel_Paint(object sender, PaintEventArgs e)
        {
            // กรอบใหญ่ราคา
            if (_displayPricePanelBackgroundImage != null)
            {
                e.Graphics.DrawImage(this._displayPricePanelBackgroundImage, 0, 0, ((MyLib._myPanel)sender).Width, ((MyLib._myPanel)sender).Height);
            }
        }

        void _displayNamePanel_Paint(object sender, PaintEventArgs e)
        {
            // กรอบใหญ่ชื่อลูกค้า
            if (_displayNamePanelBackgroundImage != null)
            {
                e.Graphics.DrawImage(this._displayNamePanelBackgroundImage, 0, 0, ((MyLib._myPanel)sender).Width, ((MyLib._myPanel)sender).Height);
            }
        }

        void _topPanel_Paint(object sender, PaintEventArgs e)
        {
            // กรอบใหญ่บนสุด
            if (_topPanelBackgroundImage != null)
            {
                e.Graphics.DrawImage(this._topPanelBackgroundImage, 0, 0, ((MyLib._myPanel)sender).Width, ((MyLib._myPanel)sender).Height);
            }
        }

        void _totalLabel_Paint(object sender, PaintEventArgs e)
        {
            // กรอบยอดรวม
            if (_totalLabelBackgroundImage != null)
            {
                e.Graphics.DrawImage(this._totalLabelBackgroundImage, 0, 0, ((Label)sender).Width, ((Label)sender).Height);
            }
        }

        void _buttomPanel_Resize(object sender, EventArgs e)
        {
            // ขยับตำแหน่งยอดรวม
            this._totalLabel.Location = new Point(this._buttomPanel.Width - (this._totalLabel.Width + 10), this._totalLabel.Location.Y);
        }

        private string _backgroundImageFileNameTemp = "";
        /// <summary>
        /// ภาพพื้นหลัง
        /// </summary>
        public string _backgroundImageFileName
        {
            set
            {
                this._backgroundImageFileNameTemp = value;
                try
                {
                    this._dataGridView.OwnerBackgroundImage = MyLib._myGlobal._loadImageFromUrl(_backgroundImageFileName);
                    this._dataGridView.SetCellsTransparent();
                    this.Invalidate();
                }
                catch
                {
                    this._dataGridView.SetCellsNormal();
                }
            }
            get
            {
                return this._backgroundImageFileNameTemp;
            }
        }

        void _posProductControl_Load(object sender, EventArgs e)
        {
            //this._backgroundImageFileName = "http://www.smlsoft.com/posbackground/pos1.jpg";
            /*
            this._displayNamePanelBackgroundImage = MyLib._myGlobal._loadImageFromUrl("http://www.usmcstore.com/images/Ka-Bar/Ka-bar.gif");
            this._displayPricePanelBackgroundImage = MyLib._myGlobal._loadImageFromUrl("http://www.timesonline.co.uk/multimedia/archive/00600/woods_585x350_600971a.jpg");
            this._topPanelBackgroundImage = MyLib._myGlobal._loadImageFromUrl("http://www.imaxmelbourne.com.au/UploadedImages/zero_bar.jpg");
            this._buttomPanelBackgroundImage = MyLib._myGlobal._loadImageFromUrl("http://hilight.kapook.com/admin_hilight/spaw2/newimg/sport/Bar-Refaeli.preview.jpg");
            this._inputTextBoxPanelBackgroundImage = MyLib._myGlobal._loadImageFromUrl("http://images.forstudent.com/ic/controller_img_sensor_bar.gif");
            this._totalLabelBackgroundImage = MyLib._myGlobal._loadImageFromUrl("http://www.hosthit.net/hardware/2008/dell_soundbar/welcome.jpg");
             */
            this._comboBox_theme.Items.Add("yellow");
            this._comboBox_theme.Items.Add("green");
            this._comboBox_theme.TextChanged += new EventHandler(_comboBox_theme_TextChanged);
            _set_theme();
            _newTrans();
        }

        void _comboBox_theme_TextChanged(object sender, EventArgs e)
        {

            _theme = _comboBox_theme.Text;
            _set_theme();
        }

        /// <summary>
        /// กำหนด theme
        /// </summary>
        string _theme = "";

        void _set_theme()
        {
            if (_theme.Equals("")) _theme = "green";


            if (_theme.Equals("yellow"))
            {
                //   theme yellow

                this._dataGridView.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
                this._dataGridView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                this._dataGridView.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
                this._dataGridView.DefaultCellStyle.ForeColor = Color.FromArgb(247, 31, 107);
                this._dataGridView.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(108, 36, 228);
                this._label.ForeColor = Color.FromArgb(12, 204, 245);
                this._priceLabel.ForeColor = Color.FromArgb(133, 140, 198);
                this._nameLabel.ForeColor = Color.FromArgb(238, 24, 225);
                this._nameLabel.Font = new Font("tahoma", 12);
                this._totalLabel.ForeColor = Color.Tomato;
                this._backgroundImageFileName = "http://www.smlsoft.com/posbackground/dataGridView_yellow.jpg";
                this._topPanelBackgroundImage = MyLib._myGlobal._loadImageFromUrl("http://www.smlsoft.com/posbackground/top_buttom_Panel_yellow.png");
                this._buttomPanelBackgroundImage = MyLib._myGlobal._loadImageFromUrl("http://www.smlsoft.com/posbackground/top_buttom_Panel_yellow.png");

                this.Invalidate();
            }
            else if (_theme.Equals("green"))
            {
                //  theme green

                this._dataGridView.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
                this._dataGridView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                this._dataGridView.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(222)));
                this._dataGridView.DefaultCellStyle.ForeColor = Color.FromArgb(231, 9, 245);
                this._dataGridView.ColumnHeadersDefaultCellStyle.ForeColor = Color.Chocolate;
                this._label.ForeColor = Color.FromArgb(247, 141, 28);
                this._priceLabel.ForeColor = Color.FromArgb(173, 138, 220);
                this._nameLabel.ForeColor = Color.FromArgb(73, 185, 243);
                this._nameLabel.Font = new Font("tahoma", 12);
                this._totalLabel.ForeColor = Color.FromArgb(25, 40, 224);
                this._backgroundImageFileName = "http://www.smlsoft.com/posbackground/dataGridView_Green_1.jpg";
                this._topPanelBackgroundImage = MyLib._myGlobal._loadImageFromUrl("http://www.smlsoft.com/posbackground/buttomPanel_Green_1.png");
                this._buttomPanelBackgroundImage = MyLib._myGlobal._loadImageFromUrl("http://www.smlsoft.com/posbackground/buttomPanel_Green_1.png");
                this.Invalidate();
            }
        }

        /// <summary>
        /// ขึ้นรายการใหม่
        /// </summary>
        void _newTrans()
        {
            this._label.Text = "ยินดีต้อนรับ";
            this._label.Invalidate();
            //
            this._nameLabel.Text = "ลูกค้าไม่เป็นสมาชิก";
            this._nameLabel.Invalidate();
        }

        /// <summary>
        /// ดักไม่ให้เสียงดังเมื่อกด Enter
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void _inputTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
            }
        }

        private void _inputTextBox_TextChanged(object sender, EventArgs e)
        {
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                if (this._inputTextBox.Focused)
                {
                    _addRow();
                }
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

        /// <summary>
        /// เพิ่มรายการสินค้าใหม่
        /// </summary>
        void _addRow()
        {
            string __productCode = this._inputTextBox.Text.Trim().ToString().ToUpper();
            if (__productCode.Length > 0)
            {
                _posFindProductInfo __searchData = (_posFindProductInfo)_search(__productCode);
                if (__searchData._type == 2)
                {
                    // ลูกค้า
                    this._nameLabel.Text = __searchData._desc;
                    this._nameLabel.Invalidate();
                }
                else
                {
                    if (__searchData._type == 1)
                    {
                        this._label.Text = __searchData._desc + " (" + __searchData._packing + ")";
                        this._label.Invalidate();
                        //
                        string __qtyStr = __searchData._qty.ToString("#,###.##");
                        string __priceStr = __searchData._price.ToString("#,###.##");
                        this._priceLabel.Text = __qtyStr + " X @ " + __priceStr + " บาท";
                        this._priceLabel.Invalidate();
                        //
                        DataGridViewRow __newRow = new DataGridViewRow();
                        //
                        DataGridViewTextBoxCell __newNumber = new DataGridViewTextBoxCell();
                        int __number = this._dataGridView.Rows.Count + 1;
                        __newNumber.Value = __number.ToString();
                        __newRow.Cells.Add(__newNumber);
                        //
                        DataGridViewTextBoxCell __code = new DataGridViewTextBoxCell();
                        __code.Value = __searchData._productCode;
                        __newRow.Cells.Add(__code);
                        //
                        DataGridViewTextBoxCell __name = new DataGridViewTextBoxCell();
                        __name.Value = __searchData._desc + " (" + __searchData._packing + ")";
                        __newRow.Cells.Add(__name);
                        //
                        DataGridViewTextBoxCell __price = new DataGridViewTextBoxCell();
                        __price.Value = __searchData._price.ToString("#,###.00");
                        __newRow.Cells.Add(__price);
                        //
                        DataGridViewTextBoxCell __qty = new DataGridViewTextBoxCell();
                        __qty.Value = __searchData._qty.ToString("#,###.00");
                        __newRow.Cells.Add(__qty);
                        //
                        DataGridViewTextBoxCell __total = new DataGridViewTextBoxCell();
                        __total.Value = __searchData._total.ToString("#,###.00");
                        __newRow.Cells.Add(__total);
                        //
                        int __addr = this._dataGridView.Rows.Add(__newRow);
                        this._dataGridView.ClearSelection();
                        this._dataGridView.Rows[__addr].Selected = true;
                        this._dataGridView.FirstDisplayedScrollingRowIndex = __addr;
                        // for loop รวมเงินแล้วสะแดงด้วย ด้านล่าง
                        this._sumTotal();
                    }
                    else
                    {
                        // ไม่พบ ให้ popup หน้าจอ เอาสวยๆหน่อยละ ทำ form แล้ว showdialog
                    }
                }
            }
            this._inputTextBox.Clear();
        }

        private void _sumTotal()
        {
            double __total = 0;
            foreach (DataGridViewRow __row in this._dataGridView.Rows)
            {
                //if (double.TryParse(__row.Cells["_subTotal"].Value.ToString()))
                //{
                __total += double.Parse(__row.Cells["_subTotal"].Value.ToString());
                //}
            }
            this._totalLabel.Text = __total.ToString("#,###.00");

        }

        _posFindProductInfo _search(string source)
        {
            string __code = source;
            double __qty = 1;
            if (source.IndexOf('*') != -1)
            {
                string[] __split = source.Split('*');
                if (__split[0].ToString().Length > 0)
                {
                    __qty = Double.Parse(__split[0].ToString());
                }
                __code = __split[1].ToString();
            }
            _posFindProductInfo __result = new _posFindProductInfo();
            __result._type = 0;
            //
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            // barcode ไม่มีหน้าจอ อย่าลืมดึงหน่วยด้วย

            string __unit = " ( select " + _g.d.ic_unit._name_1 + " from " + _g.d.ic_unit._table + " where " + _g.d.ic_unit._code + " = " + _g.d.ic_inventory_barcode._unit_code + ") as "+_g.d.ic_unit._name_1;
            string __field = _g.d.ic_inventory_barcode._description + " , " + __unit + " , " + _g.d.ic_inventory_barcode._price;
            string __query = "select " + __field + " from " + _g.d.ic_inventory_barcode._table + " where " + MyLib._myGlobal._addUpper(_g.d.ic_inventory_barcode._barcode) + "='" + __code.ToUpper()+"'";
           
            DataSet __getProductData = __myFrameWork._query(MyLib._myGlobal._databaseName, __query);
            if (__getProductData.Tables[0].Rows.Count != 0)
            {
                // หาราคาสินค้าด้วย 
                __result._type = 1;
                __result._productCode = __code;
                __result._desc = __getProductData.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                __result._packing = __getProductData.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                __result._price = double.Parse(__getProductData.Tables[0].Rows[0].ItemArray.GetValue(2).ToString());
                __result._qty = __qty;
                __result._total = __result._price * __result._qty;
            }
            else
            {

                // ไม่พบสินค้า ไปค้นหาลูกค้าต่อ โดยใช้ barcode (ไม่มี เพิ่ม field ให้ด้วย)
                DataSet __getCustByBarCodeData = __myFrameWork._query(MyLib._myGlobal._databaseName, "select " + _g.d.ar_customer._name_1 + " from " + _g.d.ar_customer._table + " where " + MyLib._myGlobal._addUpper(_g.d.ar_customer._code) + "='" + __code.ToUpper() + "'");
                if (__getCustByBarCodeData.Tables[0].Rows.Count != 0)
                {
                    __result._type = 2;
                    __result._custCode = __code;
                    __result._desc = __getCustByBarCodeData.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                }
                else
                {
                    // ไม่พบสินค้า ไปค้นหาลูกค้าต่อ โดยใช้ barcode 
                    DataSet __getCustData = __myFrameWork._query(MyLib._myGlobal._databaseName, "select " + _g.d.ar_customer._name_1 + " from " + _g.d.ar_customer._table + " where " + MyLib._myGlobal._addUpper(_g.d.ar_customer._code) + "='" + __code.ToUpper() + "'");
                    if (__getCustData.Tables[0].Rows.Count != 0)
                    {
                        __result._type = 2;
                        __result._custCode = __code;
                        __result._desc = __getCustData.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    }
                }
            }
            return __result;
        }



    }

    public struct _posFindProductInfo
    {
        /// <summary>
        /// 0=ไม่พบ,1=Product,2=Customer
        /// </summary>
        public int _type;
        public string _productCode; // รหัสสินค้า
        public string _custCode; // รหัสสมาชิก
        public string _desc; // รายละเอียด
        public string _packing; // หน่วยๅ
        public double _qty; // จำนวน
        public double _price; // ราคา
        public double _total; // ยอดรวม
    }
}
