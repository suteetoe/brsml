using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Crom.Controls.Docking;
using MyLib;

namespace SMLMIS
{
    public partial class calendar_trans : UserControl
    {
        private Calendar._calendar _calendar = new Calendar._calendar();
        private Form _calendarForm = new Form();
        private DataTable _calendarTable;
        private int[] _calendarListTransFlag = { 12, 44 };

        public calendar_trans()
        {
            InitializeComponent();
            //
            this._calendar.Dock = DockStyle.Fill;
            this._calendarForm.Controls.Add(this._calendar);
            DockableFormInfo __formRight = this._dock.Add(this._calendarForm, Crom.Controls.Docking.zAllowedDock.All, Guid.NewGuid());
            __formRight.ShowCloseButton = false;
            __formRight.ShowContextMenuButton = false;
            this._dock.DockForm(__formRight, DockStyle.Fill, zDockMode.Inner);
            this._calendar._refreshButton.Click += new EventHandler(_refreshButton_Click);
            this._calendar._afterCreate += new Calendar._calendar.AfterCreateEventHandler(_calendar__afterCreate);
            this._loadDataForCalendar();
            this._calendar._closeButton.Click += new EventHandler(_closeButton_Click);
        }

        void _closeButton_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        void _refreshButton_Click(object sender, EventArgs e)
        {
            this._loadDataForCalendar();
        }

        void _loadDataForCalendar()
        {
            try
            {
                MyLib._myFrameWork __myFrameWork = new _myFrameWork();
                this._calendarTable = __myFrameWork._queryShort("select count(*) as xcount,doc_date,trans_flag from ic_trans group by doc_date,trans_flag order by doc_date").Tables[0];
                this._createControlInDay();
            }
            catch
            {
            }
        }

        void _createControlInDay()
        {
            if (this._calendarTable != null)
            {
                foreach (Control __control in this._calendar._calendarPanel.Controls)
                {
                    if (__control.GetType() == typeof(Calendar._dayControl))
                    {
                        Calendar._dayControl __day = (Calendar._dayControl)__control;
                        __day._flowLayout.Controls.Clear();
                        DateTime __date = __day._date;
                        for (int __loop = 0; __loop < this._calendarListTransFlag.Length; __loop++)
                        {
                            string __where = "doc_date=\'" + __date.Year.ToString("00") + "-" + __date.Month.ToString("00") + "-" + __date.Day.ToString("00") + "\' and trans_flag=" + this._calendarListTransFlag[__loop].ToString();
                            DataRow[] __getData = this._calendarTable.Select(__where);
                            if (__getData.Length > 0)
                            {
                                MyLib.VistaButton __button = new VistaButton();
                                __button.AutoSize = true;
                                __button.ButtonText = _g.g._transFlagGlobal._transName(this._calendarListTransFlag[__loop]) + " (" + __getData[0]["xcount"].ToString() + ")";
                                __day._flowLayout.Controls.Add(__button);
                            }
                        }
                    }
                }
            }
        }

        void _calendar__afterCreate(object sender)
        {
            this._createControlInDay();
        }
    }
}
