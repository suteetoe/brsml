using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Collections;
using System.Web.Services;

namespace SMLERPConfig
{
    public partial class _companyProfile : MyLib._myForm
    {
        bool _isEdit = true;
        public _companyProfile()
        {
            InitializeComponent();
            if (MyLib._myGlobal._isDesignMode == false)
            {
                this._toolStrip1.Font = new Font(MyLib._myGlobal._myFont.FontFamily, MyLib._myGlobal._myFont.Size);
            }

            string __mainMenuId = MyLib._myGlobal._mainMenuIdPassTrue;
            string __mainMenuCode = MyLib._myGlobal._mainMenuCodePassTrue;
            MyLib._PermissionsType __permission = MyLib._myGlobal._isAccessMenuPermision(__mainMenuId, __mainMenuCode);
            _isEdit = __permission._isEdit;

        }

        private void _companyProfile_Load(object sender, EventArgs e)
        {
            // add tab resource

            this._myTabControl1.TableName = _g.d.erp_company_profile._table;
            this._myTabControl1._getResource();

            //_myTabControl1.TabPages[0].Text = _g.d.erp_company_profile._tab_companyprofile;
            //_myTabControl1.TabPages[1].Text = _g.d.erp_company_profile._tab_companydetail;

            _companyProfileScreen1._saveKeyDown += new MyLib.SaveKeyDownHandler(_companyProfileScreen1__saveKeyDown);
            _companyProfileScreen1._exitKeyDown += new MyLib.ExitKeyDownHandler(_companyProfileScreen1__exitKeyDown);
            _companyProfileScreen1._refresh();
            //
            ArrayList getData = _companyProfileScreen1._createQueryForDatabase();
            ArrayList getDetail = _companyProfileDetailScreen1._createQueryForDatabase();

            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            string query = "select " + getData[0].ToString() + "," + getDetail[0].ToString() + " from " + _companyProfileScreen1._table_name;
            DataSet __result = __myFrameWork._query(MyLib._myGlobal._databaseName, query);
            _companyProfileScreen1._loadData(__result.Tables[0]);
            _companyProfileDetailScreen1._loadData(__result.Tables[0]);
            __result.Dispose();

            _companyProfileScreen1._focusFirst();
            //
            this.Disposed += (s1, e1) =>
            {
                this._companyProfileScreen1.Dispose();
                this._companyProfileDetailScreen1.Dispose();
                this._myTabControl1.Dispose();
            };
        }

        void _companyProfileScreen1__exitKeyDown(object sender)
        {
            this.Dispose();
        }

        void _companyProfileScreen1__saveKeyDown(object sender)
        {
            _saveData();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            try
            {
                if (_companyProfileScreen1._isChange)
                {
                    DialogResult __result = MyLib._myGlobal._displayWarning(5, "");
                    if (__result == DialogResult.No)
                    {
                        e.Cancel = true;
                    }
                }
            }
            catch
            {
            }
        }

        public void _saveData()
        {
            if (this._isEdit == true)
            {
                if (MyLib._myGlobal._isDesignMode == false)
                {
                    string getEmtry = _companyProfileScreen1._checkEmtryField();
                    if (getEmtry.Length > 0)
                    {
                        MyLib._myGlobal._displayWarning(2, getEmtry);
                    }
                    else
                    {
                        string __query = "";
                        ArrayList __getData = _companyProfileScreen1._createQueryForDatabase();
                        ArrayList __getDetail = _companyProfileDetailScreen1._createQueryForDatabase();

                        __query = "insert into " + _g.d.erp_company_profile._table + " (" + __getData[0].ToString() + "," + __getDetail[0].ToString() + ") values (" + __getData[1].ToString() + "," + __getDetail[1].ToString() + ")";
                        //
                        string _myQuery = MyLib._myGlobal._xmlHeader + "<node>";
                        _myQuery += MyLib._myUtil._convertTextToXmlForQuery("delete from " + _g.d.erp_company_profile._table + "");
                        _myQuery += MyLib._myUtil._convertTextToXmlForQuery(__query);
                        _myQuery += "</node>";
                        MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                        string result = __myFrameWork._queryList(MyLib._myGlobal._databaseName, _myQuery);
                        if (result.Length == 0)
                        {
                            MessageBox.Show(MyLib._myGlobal._resource("บันทึกข้อมูลเสร็จเรียบร้อย โปรแกรมจะปิดหน้าจอนี้ให้"), MyLib._myGlobal._resource("สำเร็จ"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                            _companyProfileScreen1._isChange = false;
                            this.Dispose();
                        }
                        else
                        {
                            MessageBox.Show(result, MyLib._myGlobal._resource("ล้มเหลว"), MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    _g.g._companyProfileLoad();
                }
            }
            else
            {
                MessageBox.Show("ไม่อนุญาติให้แก้ไขข้อมูล");
            }
        }

        private void _companyProfileScreen1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void _buttonSave_Click(object sender, EventArgs e)
        {
            _saveData();
        }

        private void _buttonClose_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }

    public class _companyProfileScreen : MyLib._myScreen
    {
        public _companyProfileScreen()
        {
            int __row = 0;

            this._maxColumn = 1;
            this._table_name = _g.d.erp_company_profile._table;
            this._addTextBox(__row++, 0, 1, 0, _g.d.erp_company_profile._company_name_1, 1, 1, 0, true, false, false);
            this._addTextBox(__row++, 0, _g.d.erp_company_profile._company_name_2, 100);
            this._addTextBox(__row++, 0, _g.d.erp_company_profile._business_name_1, 100);
            this._addTextBox(__row++, 0, _g.d.erp_company_profile._business_name_2, 100);
            this._addTextBox(__row++, 0, _g.d.erp_company_profile._workplace_1, 100);
            this._addTextBox(__row++, 0, _g.d.erp_company_profile._workplace_2, 100);
            this._addTextBox(__row++, 0, 3, _g.d.erp_company_profile._address_1, 1, 100);


            __row += 2;
            this._addTextBox(__row++, 0, 3, _g.d.erp_company_profile._address_2, 1, 100);

            __row += 2;
            this._addTextBox(__row++, 0, _g.d.erp_company_profile._telephone_number, 100);
            this._addTextBox(__row++, 0, _g.d.erp_company_profile._fax_number, 100);
            this._addTextBox(__row++, 0, _g.d.erp_company_profile._tax_number, 100);
            this._addComboBox(__row++, 0, _g.d.erp_company_profile._branch_type, true, _g.g._ap_ar_branch_type, true);
            this._addTextBox(__row++, 0, _g.d.erp_company_profile._branch_code, 100);
            this._addTextBox(__row++, 0, _g.d.erp_company_profile._profit_ctr, 100);
            this._addTextBox(__row++, 0, _g.d.erp_company_profile._cost_ctr, 100);

            if (MyLib._myGlobal._isVersionEnum != MyLib._myGlobal._versionType.SMLBIllFree)
            {
                this._addCheckBox(__row++, 0, _g.d.erp_company_profile._branch_status, true, false);
            }

            if (MyLib._myGlobal._OEMVersion.Equals("SINGHA"))
            {
                this._addTextBox(__row++, 0, _g.d.erp_company_profile._agent_code, 100);
                this._addTextBox(__row++, 0, _g.d.erp_company_profile._sap_code, 100);

                this._addTextBox(__row++, 0, _g.d.erp_company_profile._region, 100);
                this._addTextBox(__row++, 0, _g.d.erp_company_profile._club, 100);
                this._addTextBox(__row++, 0, _g.d.erp_company_profile._arm_code, 100);

            }
        }
    }

    public class _companyProfileDetailScreen : MyLib._myScreen
    {
        public _companyProfileDetailScreen()
        {
            this._maxColumn = 1;
            this._table_name = _g.d.erp_company_profile._table;

            this._addTextBox(0, 0, _g.d.erp_company_profile._house, 100);
            this._addTextBox(1, 0, _g.d.erp_company_profile._room_no, 100);
            this._addTextBox(2, 0, _g.d.erp_company_profile._floor_no, 100);
            this._addTextBox(3, 0, _g.d.erp_company_profile._village, 100);
            this._addTextBox(4, 0, _g.d.erp_company_profile._house_no, 100);
            this._addTextBox(5, 0, _g.d.erp_company_profile._crowd_no, 100);
            this._addTextBox(6, 0, _g.d.erp_company_profile._lane, 100);
            this._addTextBox(7, 0, _g.d.erp_company_profile._road, 100);
            this._addTextBox(8, 0, _g.d.erp_company_profile._locality, 100);
            this._addTextBox(9, 0, _g.d.erp_company_profile._amphur, 100);
            this._addTextBox(10, 0, _g.d.erp_company_profile._province, 100);
            this._addTextBox(11, 0, _g.d.erp_company_profile._postcode, 100);

        }
    }

}