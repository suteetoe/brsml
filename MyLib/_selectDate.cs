using System;
using System.Windows.Forms;

namespace MyLib
{
	public partial class _selectDate : _myForm
	{
		public _selectDate()
		{
			InitializeComponent();
			this.monthCalendar1.DateSelected += new DateRangeEventHandler(monthCalendar1_DateSelected);
		}

		void monthCalendar1_DateSelected(object sender, DateRangeEventArgs e)
		{
			if (_selectedDate != null)
			{
				_selectedDate(e.Start);
			}
			this.Close();
		}

		private void _selectDate_Load(object sender, EventArgs e)
		{

		}

		public event SelectDateEventHandler _selectedDate;

		private void monthCalendar1_DateChanged(object sender, DateRangeEventArgs e)
		{
		}
		/// <summary>
		/// ส่งค่าวันที่กลับเมื่อเลือก
		/// </summary>
		/// <param name="e"></param>
		public delegate void SelectDateEventHandler(DateTime e);
	}
}