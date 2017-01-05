using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SMLERPControl
{
    public partial class _companyProfilePicturecs : Form
    {
        public _companyProfilePicturecs()
        {
            InitializeComponent();
            if (MyLib._myGlobal._isDesignMode == false)
            {
                this.toolStrip1.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            }
            this._getPicture1._DisplayPictureAmount = 1;
        }
       
        private void _companyProfilePicturecs_Load(object sender, EventArgs e)
        {
            _companyProfileScreen1._saveKeyDown += new MyLib.SaveKeyDownHandler(_companyProfileScreen1__saveKeyDown);
            _companyProfileScreen1._exitKeyDown += new MyLib.ExitKeyDownHandler(_companyProfileScreen1__exitKeyDown);
            _companyProfileScreen1._refresh();
            try
            {
                ArrayList getData = _companyProfileScreen1._createQueryForDatabase();
                MyLib._myFrameWork myFrameWork = new MyLib._myFrameWork();
                string query = "select " + getData[0].ToString() + " from " + _companyProfileScreen1._table_name;
                DataSet result = myFrameWork._query(MyLib._myGlobal._databaseName, query);
                _companyProfileScreen1._loadData(result.Tables[0]);
                _companyProfileScreen1._focusFirst();
                string _codepic = this._companyProfileScreen1._getDataStr(_g.d.erp_company_profile._company_name_1);
                string _codepic_ = _codepic.Replace("/", "").Trim();
                this._getPicture1._loadImage(_codepic_);
                this._getPicture1._setEnable(true);                
            }
            catch (Exception ex)
            {
            }
        }

        void _companyProfileScreen1__saveKeyDown(object sender)
        {
            saveData();
        }

        void _companyProfileScreen1__exitKeyDown(object sender)
        {
            this.Close();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            if (_companyProfileScreen1._isChange)
            {
                DialogResult result = MyLib._myGlobal._displayWarning(5, "");
                if (result == DialogResult.No)
                {
                    e.Cancel = true;
                }
            }
        }

        public void saveData()
        {
            string getEmtry = _companyProfileScreen1._checkEmtryField();
            string __result = "";
            if (getEmtry.Length > 0)
            {
                MyLib._myGlobal._displayWarning(2, getEmtry);
            }
            else
            {
                string _codepic = this._companyProfileScreen1._getDataStr(_g.d.erp_company_profile._company_name_1);
                string _codepic_ = _codepic.Replace("/", "").Trim();
                __result = this._getPicture1._updateImage(_codepic_);
                
                if (__result.Length == 0)
                {
                    MyLib._myGlobal._displayWarning(1, null);               
                   // _getPicture1._clearpic();
                  //  _getPicture1._setEnable(false);
                }
                else
                {
                    MessageBox.Show(__result, "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }    
        }

        private void _saveButton_Click(object sender, EventArgs e)
        {
            saveData();
        }

        private void _closeButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }

    public class _companyProfileScreen : MyLib._myScreen
    {
        public _companyProfileScreen()
        {
            this._maxColumn = 1;
            this._table_name = _g.d.erp_company_profile._table;
            this._addTextBox(0, 0, 1, 0, _g.d.erp_company_profile._company_name_1, 1, 1, 0, true, false, false);
            this._addTextBox(1, 0, _g.d.erp_company_profile._company_name_2, 100);

            Control __nameControl1 = this._getControl(_g.d.erp_company_profile._company_name_1);
            Control __nameControl2 = this._getControl(_g.d.erp_company_profile._company_name_2);
            __nameControl1.Enabled = false;
            __nameControl2.Enabled = false;
        }
    }
}
