using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SMLERPConfig._accountPeriod
{
    public partial class _accountPeriodSetTermAuto : Form
    {
        public _accountPeriodSetTermAuto()
        {
            InitializeComponent();
            //
            _richTextBox1.Text = MyLib._myGlobal._resource("กรุณาบันทึกวันที่เริ่มต้นของงวดแรก และกำหนดจำนวนงวดว่าเป็น 12 หรือ 13 งวดด้วย");
            _myLabelYear.Text = MyLib._myGlobal._resource("ประจำปี")+" :";
            _myLabelTerm.Text = MyLib._myGlobal._resource("วันที่เริ่มต้นงวดแรก") + " :";
            _myButtonProcess.Text = MyLib._myGlobal._resource("ประมวลผล");
            this.Name = MyLib._myGlobal._resource("กำหนดงวดบัญชี");
            DateTime getDate = System.DateTime.Today;
            _year.textBox.Text = ((int)(getDate.Year + MyLib._myGlobal._year_add)).ToString();
            _year.textBox.TextChanged += new EventHandler(textBox_TextChanged);
            dateSet();
        }

        void dateSet()
        {
            _dateBegin.textBox.Text = string.Format("{0}/{1}/{2}", 1, 1, _year.textBox.Text);
            _dateBegin._checkDate(true, false);

            _numOfPeriod._setDataNumber = 12;
        }

        void textBox_TextChanged(object sender, EventArgs e)
        {
            dateSet();
        }

        private void _accountPeriodSetTermAuto_Load(object sender, EventArgs e)
        {
        }

        public event CallBackHandler _callBack;

        private void _myButton1_Click(object sender, EventArgs e)
        {
            if (_callBack != null) _callBack(this._dateBegin._dateTime, (int)this._numOfPeriod._double);
            this.Close();
        }
        /// <summary>
        /// ส่งค่ากลับ
        /// </summary>
        /// <param name="dateBegin">วันที่เริ่มต้น</param>
        /// <param name="termType">1=เดือน,2=อาทิตย์</param>
        public delegate void CallBackHandler(DateTime dateBegin, int numOfPeriod);
    }
}