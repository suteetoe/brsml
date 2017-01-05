using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Calendar
{
    public partial class _calendar : UserControl
    {
        public delegate void AfterCreateEventHandler(object sender);
        public event AfterCreateEventHandler _afterCreate;
        int _maxWeek = 0;
        DateTime _date;

        public _calendar()
        {
            InitializeComponent();
            if (MyLib._myGlobal._isDesignMode == false)
            {
                this.toolStrip1.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            }
            UpdateStyles();
            this._dayCreate(DateTime.Now);
            this.SizeChanged += new EventHandler(_calendar_SizeChanged);
            this._date = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
        }

        int _dayOfWeek(DateTime date)
        {
            switch (date.DayOfWeek)
            {
                case DayOfWeek.Monday: return 0;
                case DayOfWeek.Tuesday: return 1;
                case DayOfWeek.Wednesday: return 2;
                case DayOfWeek.Thursday: return 3;
                case DayOfWeek.Friday: return 4;
                case DayOfWeek.Saturday: return 5;
                case DayOfWeek.Sunday: return 6;
            }
            return 0;
        }

        void _dayCreate(DateTime currentDate)
        {
            this._yearMonthLabel.Text = MyLib._myGlobal._monthName(currentDate, true) + " " + currentDate.Year.ToString();
            this._calendarPanel.Controls.Clear();
            int __dayInMonth = 0; // จำนวนวันใน 1 เดือน
            int __dayOfWeek = this._dayOfWeek(currentDate);
            int __dayBeginMonthOfWeek = this._dayOfWeek(new DateTime(currentDate.Year, currentDate.Month, 1));
            int __day = 1;
            int __month = currentDate.Month;
            int __year = currentDate.Year;
            DateTime __dateTrial = new DateTime(currentDate.Year, currentDate.Month, 1);
            while (__dateTrial.Month == __month)
            {
                __dayInMonth++;
                __dateTrial = __dateTrial.AddDays(1);
            }
            double __calc = ((double)(((double)__dayInMonth + (double)__dayBeginMonthOfWeek) / 7.0));
            this._maxWeek = (int)Math.Ceiling(__calc);
            DateTime __dateStart = new DateTime(currentDate.Year, currentDate.Month, 1).AddDays(__dayBeginMonthOfWeek * -1);
            for (int __row = 0; __row < this._maxWeek; __row++)
            {
                for (int __column = 0; __column < 7; __column++)
                {
                    _dayControl __dayNew = new _dayControl(__dateStart, __row, __column);
                    __dayNew.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                    __dayNew._showBackground = (currentDate.Month == __dateStart.Month) ? true : false;
                    if (__dayNew._showBackground == false)
                    {
                        __dayNew.BackColor = Color.Silver;
                    }
                    this._calendarPanel.Controls.Add(__dayNew);
                    __dateStart = __dateStart.AddDays(1);
                }
            }
            this._calcPosition();
            if (this._afterCreate != null)
            {
                this._afterCreate(this);
            }
        }

        void _calendar_SizeChanged(object sender, EventArgs e)
        {
            this._calcPosition();
        }

        void _calcPosition()
        {
            int __width = this._calendarPanel.Width / 7;
            int __height = this._calendarPanel.Height / this._maxWeek;
            foreach (Control __control in this._calendarPanel.Controls)
            {
                if (__control.GetType() == typeof(_dayControl))
                {
                    _dayControl __dayControl = (_dayControl)__control;
                    __dayControl.Size = new Size(__width, __height);
                    __dayControl.Location = new Point(__width * __dayControl._column, __height * __dayControl._row);
                }
            }
        }

        private void _prevMonthButton_Click(object sender, EventArgs e)
        {
            this._date = this._date.AddMonths(-1);
            this._dayCreate(this._date);
        }

        private void _nextMonthButton_Click(object sender, EventArgs e)
        {
            this._date = this._date.AddMonths(1);
            this._dayCreate(this._date);
        }

        private void _refreshButton_Click(object sender, EventArgs e)
        {

        }

        private void _closeButton_Click(object sender, EventArgs e)
        {
        }
    }
}
