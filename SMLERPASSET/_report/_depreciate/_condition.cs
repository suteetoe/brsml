using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SMLERPASSET._report._depreciate
{
    public partial class _condition : Form
    {
        public _condition()
        {
            InitializeComponent();
        }

        private void _buttonOk_Click(object sender, EventArgs e)
        {
            DateTime __dateStart = MyLib._myGlobal._convertDate(this._screenAssetCondition1._getDataStr("as_resource.date_begin"));
            DateTime __dateStop = MyLib._myGlobal._convertDate(this._screenAssetCondition1._getDataStr("as_resource.date_end"));
            if (__dateStart.Year == 1000 || __dateStop.Year == 1000)
            {
                MessageBox.Show((MyLib._myGlobal._language == 0) ? "กรุณาบันทึก วันที่เริ่มต้น และ วันที่สิ้นสุด ก่อน" : "Please enter startdate and enddate");
            }
            else
            {
                int __dateCompare = DateTime.Compare(__dateStart, __dateStop);
                if (__dateCompare > 0)
                {
                    MessageBox.Show((MyLib._myGlobal._language == 0) ? "วันที่สิ้นสุดต้องมากกว่าหรือเท่ากับวันที่เริ่มต้นเท่านั้น" : "Enddate much more than or equal to Begindate");
                }
                else
                {
                    this.Close();
                }
            }
        }
    }
}