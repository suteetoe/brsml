using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace SMLPosClient
{
    public partial class _posPromotionForm : Form
    {
        public _posPromotionForm()
        {
            InitializeComponent();

            this.button1.Click += new EventHandler(button1_Click);
            this.button2.Click += new EventHandler(button2_Click);
        }

        void button1_Click(object sender, EventArgs e)
        {
            if (this.textBox1.Text.Length == 0 || this.textBox2.Text.Length == 0) return;
            DataGridViewTextBoxCell __cell1 = new DataGridViewTextBoxCell();
            __cell1.Value = this.textBox1.Text;
            DataGridViewTextBoxCell __cell2 = new DataGridViewTextBoxCell();
            __cell2.Value = this.textBox2.Text;
            DataGridViewRow __dataGridViewRow = new DataGridViewRow();
            __dataGridViewRow.Cells.Add(__cell1);
            __dataGridViewRow.Cells.Add(__cell2);
            this._myDataGridView1.Rows.Add(__dataGridViewRow);
            this.textBox1.Text = "";
            this.textBox2.Text = "";
            this.textBox1.Select();
        }

        void button2_Click(object sender, EventArgs e)
        {
            // จัดกลุ่มเพื่อรวมจำนวนสินค้าเดียวกันก่อน
            this._groupRow();

            this.textBox3.Text = "";
            foreach (DataGridViewRow __row in this._myDataGridView1.Rows)
            {
                // เลือกสินค้า 1 รายการ
                _itemStruct __item = new _itemStruct();
                __item.__barcode = __row.Cells[0].Value.ToString();
                double.TryParse(__row.Cells[1].Value.ToString(), out __item.__qty);

                this.textBox3.Text += this._checkPromotion(ref __item);
            }
        }

        private void _groupRow()
        {
            for (int __index = 0; __index < this._myDataGridView1.Rows.Count; __index++)
            {
                DataGridViewRow __row = this._myDataGridView1.Rows[__index];
                if (__row.Index > 0)
                {
                    string __barcode = __row.Cells[0].Value.ToString();
                    string __qty = __row.Cells[1].Value.ToString();
                    foreach (DataGridViewRow __getRow in this._myDataGridView1.Rows)
                    {
                        if (__getRow.Index < __row.Index && __barcode == __getRow.Cells[0].Value.ToString())
                        {
                            __getRow.Cells[1].Value = double.Parse(__getRow.Cells[1].Value.ToString()) + double.Parse(__row.Cells[1].Value.ToString());
                            this._myDataGridView1.Rows.RemoveAt(__row.Index);
                            __index--;
                            break;
                        }
                    }
                }
            }
        }

        private string _checkPromotion(ref _itemStruct __item)
        {
            StringBuilder __premium = new StringBuilder();
            _promotionStruct __promotionStruct = new _promotionStruct();
            ArrayList __conditionArray = new ArrayList();
            DataTable __dataTablePromotions = this._loadPromotions(__item);

            foreach (DataRow __dataRow in __dataTablePromotions.Rows)
            {
                __promotionStruct.__promotionName = __dataRow[0].ToString();
                __promotionStruct.__promotionCondition = __dataRow[1].ToString();
                string __script = __promotionStruct.__promotionCondition;
                int __indexBegin = __script.IndexOf('[');
                int __indexEnd = 0;

                // แยก condition ในชุดนี้เก็บไว้ใน array
                __conditionArray.Clear();
                while (__indexBegin != -1 && __indexBegin < __script.Length)
                {
                    __indexEnd = __script.IndexOf(']', __indexBegin);
                    _conditionStruct __subCondition = new _conditionStruct();
                    __subCondition.__condition = __script.Substring(__indexBegin + 1, __indexEnd - __indexBegin - 1);
                    __subCondition = this._checkCondition(__item, __subCondition);
                    __conditionArray.Add(__subCondition);
                    __indexBegin = __script.IndexOf('[', __indexEnd);
                }

                // สรุป condition 0=false 1=true
                for (int __index = 0; __index < __conditionArray.Count; __index++)
                {
                    _conditionStruct __getCondition = (_conditionStruct)__conditionArray[__index];
                    __script = __script.Replace(__getCondition.__condition, ((__getCondition.__status) ? "1" : "0"));
                }
                __script = __script.Replace("[", "").Replace("]", "");
                _booleanParser __booleanParser = new _booleanParser();
                decimal __bool = __booleanParser.Calculate(__script);

                // ถ้าสินค้าใช้กับโปรโมชั่นนี้ได้
                if (__bool == 1)
                {
                    for (int __index = 0; __index < __conditionArray.Count; __index++)
                    {
                        _conditionStruct __getCondition = (_conditionStruct)__conditionArray[__index];
                        __premium.Append(__item.__barcode + " จำนวนตั้งแต่ " + __getCondition.__beginQty + "ชิ้น ถึง " + __getCondition.__endQty + "ชิ้น\r\n");
                        while (__item.__qty > 0)
                        {
                            if (__getCondition.__status && __item.__qty >= __getCondition.__beginQty)
                            {
                                __item.__qty -= __getCondition.__endQty;
                                if (__item.__qty < 0) __item.__qty = 0;
                                __premium.Append("\t" + __promotionStruct.__promotionName + "\r\n");
                            }
                            else break;
                        }
                    }
                }
            }
            return __premium.ToString();
        }

        private DataTable _loadPromotions(_itemStruct __item)
        {
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            string __query = String.Format("select {0},{1} from {2} where {3} and {4} and {5} order by {6} DESC",
                _g.d.ic_promotion._description,
                _g.d.ic_promotion._remark,
                _g.d.ic_promotion._table,
                _g.d.ic_promotion._promote_start + "<=\'" + MyLib._myGlobal._convertDateToQuery(DateTime.Now) + "\'",
                _g.d.ic_promotion._promote_stop + ">=\'" + MyLib._myGlobal._convertDateToQuery(DateTime.Now) + "\'",
                _g.d.ic_promotion._remark + " like \'%" + __item.__barcode + "%\'",
                _g.d.ic_promotion._promote_code);
            DataTable __dataTable = __myFrameWork._query("smlerpdemo", __query).Tables[0];
            return __dataTable;
        }

        private _conditionStruct _checkCondition(_itemStruct __itemStruct, _conditionStruct __conditionStruct)
        {
            string __itemBarcode = __itemStruct.__barcode;
            double __itemQty = __itemStruct.__qty;
            string __condition = __conditionStruct.__condition.ToUpper();
            string __conditionBarcode = __condition.Substring(0, __condition.IndexOf('B'));
            double __conditionBeginQty = Double.Parse(__condition.Substring(__condition.IndexOf('B') + 1, __condition.IndexOf('E') - __condition.IndexOf('B') - 1));
            double __conditionEndQty = Double.Parse(__condition.Substring(__condition.IndexOf('E') + 1));

            __conditionStruct.__barcode = __conditionBarcode;
            __conditionStruct.__beginQty = __conditionBeginQty;
            __conditionStruct.__endQty = __conditionEndQty;
            if (__itemBarcode == __conditionBarcode && __itemQty >= __conditionBeginQty)
            {
                __conditionStruct.__status = true;
            }
            return __conditionStruct;
        }
    }

    /// <summary>
    /// Struct สินค้า
    /// </summary>
    public struct _itemStruct
    {
        public string __barcode;
        public double __qty;
    }

    /// <summary>
    /// Struct โปรโมชั่น
    /// </summary>
    public struct _promotionStruct
    {
        public string __promotionName;
        public string __promotionCondition;
    }

    /// <summary>
    /// Struct เงื่อนไข
    /// </summary>
    public struct _conditionStruct
    {
        public string __condition;
        public string __barcode;
        public double __beginQty;
        public double __endQty;
        public bool __status;
    }
}