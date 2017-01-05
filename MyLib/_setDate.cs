using System.Windows.Forms;

namespace MyLib
{
    public partial class _setDate : MyLib._myForm
    {
        public _setDate()
        {
            InitializeComponent();
            monthCalendar1.DateSelected += new DateRangeEventHandler(monthCalendar1_DateSelected);
        }

        void monthCalendar1_DateSelected(object sender, DateRangeEventArgs e)
        {
            MyLib._myGlobal._workingDate = e.Start;
            MessageBox.Show(MyLib._myGlobal._resource("warning24") + " : " + MyLib._myGlobal._workingDate, MyLib._myGlobal._resource("warning"), MessageBoxButtons.OK, MessageBoxIcon.Information);
            Close();
        }
    }
}