using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace SMLPOSControl._food
{
    public partial class _selectSaleUserControl : UserControl
    {
        public delegate void SelectSaleClick(object sender, EventArgs e);

        public event SelectSaleClick _selectSaleClick;
        private float _pictureSize = 100f;
        private float _zoomScale = 1f;
        public bool _disable_user_password = false;

        public _selectSaleUserControl()
        {
            InitializeComponent();
            if (MyLib._myGlobal._isDesignMode == false)
            {
                this.toolStrip1.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            }

            this.Load += new EventHandler(_selectSaleUserControl_Load);
        }

        public void _saleSelect(string userCode, string userName)
        {
            _selectSaleButtonControl saleObject = new _selectSaleButtonControl(userCode, userName);
            _selectSaleClick(saleObject, null);
        }

        void _selectSaleUserControl_Load(object sender, EventArgs e)
        {
            try
            {
                this._flowLayoutPanel.Controls.Clear();
                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                StringBuilder __myquery = new StringBuilder();
                __myquery.Append(MyLib._myGlobal._xmlHeader + "<node>");
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery("select " + _g.d.erp_user._code + "," + _g.d.erp_user._name_1 + "," + _g.d.erp_user._password + " from " + _g.d.erp_user._table + " order by " + _g.d.erp_user._code));

                string __xquery = "select " + _g.d.images._guid_code + "," + _g.d.images_erp_user._image_id + " from " + _g.d.images_erp_user._table + " where " + _g.d.images_erp_user._image_id + " in (select " + _g.d.erp_user._code + " from " + _g.d.erp_user._table + " order by " + _g.d.erp_user._code + " ) ";
                __myquery.Append(MyLib._myUtil._convertTextToXmlForQuery(__xquery));


                __myquery.Append("</node>");
                ArrayList __data = __myFrameWork._queryListGetData(MyLib._myGlobal._databaseName, __myquery.ToString());
                DataTable __data1 = ((DataSet)__data[0]).Tables[0];
                //
                for (int __row2 = 0; __row2 < __data1.Rows.Count; __row2++)
                {
                    string __saleCode = __data1.Rows[__row2][0].ToString();
                    _selectSaleButtonControl __menu2 = new _selectSaleButtonControl(__saleCode, __data1.Rows[__row2][1].ToString());
                    __menu2._passWord = __data1.Rows[__row2][_g.d.erp_user._password].ToString();
                    __menu2.myImageAlign = ContentAlignment.TopCenter;
                    __menu2.myTextAlign = ContentAlignment.BottomCenter;
                    __menu2.TextAlign = ContentAlignment.BottomCenter;
                    __menu2.Size = new System.Drawing.Size((int)(this._pictureSize * this._zoomScale), (int)(this._pictureSize * this._zoomScale));
                    // ดึงรูป
                    SMLERPControl._getImageData __getImage = new SMLERPControl._getImageData(__saleCode);
                    __getImage._guidList = ((DataSet)__data[1]).Tables[0];
                    __getImage._onLoadImageComplete += (s1, e1) =>
                    {
                        if (e1 != null)
                        {
                            __menu2.mText = "";
                            __menu2.myImage = e1;
                            __menu2.ImageSize = new System.Drawing.Size((int)((this._pictureSize * this._zoomScale) - 10f), (int)((this._pictureSize * this._zoomScale) - 50f));
                            __menu2.Invalidate();
                        }
                    };
                    __getImage._process();
                    //
                    __menu2.Click += (s2, e2) =>
                    {
                        if (_selectSaleClick != null)
                        {
                            Boolean _passwordCorrect = false;
                            _selectSaleButtonControl __saleButton = (_selectSaleButtonControl)s2;
                            if (__saleButton._passWord.Length == 0 || _disable_user_password == true)
                            {
                                _passwordCorrect = true;
                            }
                            else
                            {
                                SMLPOSControl._posInputNumberForm __inputNumberForm = new _posInputNumberForm(__saleButton.ButtonText);
                                __inputNumberForm.Text = "ป้อนรหัสผ่าน";
                                __inputNumberForm._textNumber.PasswordChar = '*';
                                __inputNumberForm._textNumber.TextAlign = HorizontalAlignment.Left;
                                __inputNumberForm._buttonPercent.Visible = false;
                                __inputNumberForm.ShowDialog();

                                if (__inputNumberForm.DialogResult == DialogResult.OK)
                                {
                                    if (__saleButton._passWord.Equals(__inputNumberForm._textNumber.Text))
                                    {
                                        _passwordCorrect = true;
                                    }
                                    else
                                    {
                                        MessageBox.Show("รหัสผ่านผิด กรุณาตรวจสอบ", "ผิดพลาก", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                }
                            }

                            if (_passwordCorrect)
                            {
                                // check password ก่อน
                                _selectSaleClick(s2, e2);
                            }
                        }
                    };
                    this._flowLayoutPanel.Controls.Add(__menu2);
                }
            }
            catch
            {
            }
        }

        void _redrawItem()
        {
            foreach (_selectSaleButtonControl __control in this._flowLayoutPanel.Controls)
            {
                __control.Size = new Size((int)(this._pictureSize * this._zoomScale), (int)(this._pictureSize * this._zoomScale));
                __control.ImageSize = new System.Drawing.Size((int)((this._pictureSize * this._zoomScale) - 10f), (int)((this._pictureSize * this._zoomScale) - 50f));
            };
        }

        private void _zoomInButton_Click(object sender, EventArgs e)
        {
            _zoomScale = _zoomScale + (_zoomScale * 0.1f);
            this._redrawItem();
        }

        private void _zoomOutButton_Click(object sender, EventArgs e)
        {
            _zoomScale = _zoomScale - (_zoomScale * 0.1f);
            this._redrawItem();
        }
    }
}
