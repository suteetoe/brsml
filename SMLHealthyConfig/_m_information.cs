using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace SMLHealthyConfig
{
    public partial class _m_information : MyLib._myForm
    {
        public _m_information()
        {
            InitializeComponent();
        }

        private void _option_Load(object sender, EventArgs e)
        {
            if (this.DesignMode == false)
            {
                //toe
                //_mInformation._saveKeyDown += new MyLib.SaveKeyDownHandler(_Healty_Standard__saveKeyDown);
                _m_informationScreen_body_weight._saveKeyDown += new MyLib.SaveKeyDownHandler(_Healty_Standard__saveKeyDown);
                _m_informationScreen_blood_pressure._saveKeyDown += new MyLib.SaveKeyDownHandler(_Healty_Standard__saveKeyDown);
                _m_informationScreen_blood_sugar._saveKeyDown += new MyLib.SaveKeyDownHandler(_Healty_Standard__saveKeyDown);
                _m_informationScreen_cholesterol._saveKeyDown += new MyLib.SaveKeyDownHandler(_Healty_Standard__saveKeyDown);
                _m_informationScreen_protein._saveKeyDown += new MyLib.SaveKeyDownHandler(_Healty_Standard__saveKeyDown);
                _m_informationScreen_uric_acid._saveKeyDown += new MyLib.SaveKeyDownHandler(_Healty_Standard__saveKeyDown);
                _m_informationScreen_bone_mass._saveKeyDown += new MyLib.SaveKeyDownHandler(_Healty_Standard__saveKeyDown);
                _m_informationScreen_renal_function._saveKeyDown += new MyLib.SaveKeyDownHandler(_Healty_Standard__saveKeyDown);
                _m_informationScreen_liver_function._saveKeyDown += new MyLib.SaveKeyDownHandler(_Healty_Standard__saveKeyDown);
                //_mInformation._exitKeyDown += new MyLib.ExitKeyDownHandler(_Healty_Standard__exitKeyDown);
                _m_informationScreen_body_weight._exitKeyDown += new MyLib.ExitKeyDownHandler(_Healty_Standard__exitKeyDown);
                _m_informationScreen_blood_pressure._exitKeyDown += new MyLib.ExitKeyDownHandler(_Healty_Standard__exitKeyDown);
                _m_informationScreen_blood_sugar._exitKeyDown += new MyLib.ExitKeyDownHandler(_Healty_Standard__exitKeyDown);
                _m_informationScreen_cholesterol._exitKeyDown += new MyLib.ExitKeyDownHandler(_Healty_Standard__exitKeyDown);
                _m_informationScreen_protein._exitKeyDown += new MyLib.ExitKeyDownHandler(_Healty_Standard__exitKeyDown);
                _m_informationScreen_uric_acid._exitKeyDown += new MyLib.ExitKeyDownHandler(_Healty_Standard__exitKeyDown);
                _m_informationScreen_bone_mass._exitKeyDown += new MyLib.ExitKeyDownHandler(_Healty_Standard__exitKeyDown);
                _m_informationScreen_renal_function._exitKeyDown += new MyLib.ExitKeyDownHandler(_Healty_Standard__exitKeyDown);
                _m_informationScreen_liver_function._exitKeyDown += new MyLib.ExitKeyDownHandler(_Healty_Standard__exitKeyDown);
                //_mInformation._refresh();
                //ArrayList __getDataHealty_Standard = _mInformation._createQueryForDatabase();
                MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
                string __query = "select * from " + _g.d.m_healty_standard._table;
                DataSet __result = __myFrameWork._query(MyLib._myGlobal._databaseName, __query);
                _m_informationScreen_body_weight._loadData(__result.Tables[0]);
                _m_informationScreen_blood_pressure._loadData(__result.Tables[0]);
                _m_informationScreen_blood_sugar._loadData(__result.Tables[0]);
                _m_informationScreen_cholesterol._loadData(__result.Tables[0]);
                _m_informationScreen_protein._loadData(__result.Tables[0]);
                _m_informationScreen_uric_acid._loadData(__result.Tables[0]);
                _m_informationScreen_bone_mass._loadData(__result.Tables[0]);
                _m_informationScreen_renal_function._loadData(__result.Tables[0]);
                _m_informationScreen_liver_function._loadData(__result.Tables[0]);
                _m_informationScreen_body_weight._focusFirst();
            }
        }
        void _Healty_Standard__saveKeyDown(object sender)
        {
            this.Close();
        }

        void _Healty_Standard__exitKeyDown(object sender)
        {
            save_data();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            // toe
            if (_m_informationScreen_body_weight._isChange ||
                _m_informationScreen_blood_pressure._isChange ||
                _m_informationScreen_blood_sugar._isChange ||
                _m_informationScreen_cholesterol._isChange ||
                _m_informationScreen_protein._isChange ||
                _m_informationScreen_uric_acid._isChange ||
                _m_informationScreen_bone_mass._isChange ||
                _m_informationScreen_renal_function._isChange ||
                _m_informationScreen_liver_function._isChange)
            {
                DialogResult result = MyLib._myGlobal._displayWarning(5, "");
                if (result == DialogResult.No) e.Cancel = true;
            }
        }

        private void menuExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void save_data()
        {
            // toe
            string __query = "";

            //ArrayList __getDataGlOption = _mInformation._createQueryForDatabase();
            ArrayList __getDataBodyWeight = _m_informationScreen_body_weight._createQueryForDatabase();
            ArrayList __getDataBloodPressure = _m_informationScreen_blood_pressure._createQueryForDatabase();
            ArrayList __getDataBloodSugar = _m_informationScreen_blood_sugar._createQueryForDatabase();
            ArrayList __getDataCholesterol = _m_informationScreen_cholesterol._createQueryForDatabase();
            ArrayList __getDataProtein = _m_informationScreen_protein._createQueryForDatabase();
            ArrayList __getDataRricAcid = _m_informationScreen_uric_acid._createQueryForDatabase();
            ArrayList __getDataBoneMass = _m_informationScreen_bone_mass._createQueryForDatabase();
            ArrayList __getDataRenalFunction = _m_informationScreen_renal_function._createQueryForDatabase();
            ArrayList __getDataLiverFunction = _m_informationScreen_liver_function._createQueryForDatabase();

            __query = "insert into " + _g.d.m_healty_standard._table + " (" +
                __getDataBodyWeight[0].ToString() + "," +
                __getDataBloodPressure[0].ToString() + "," +
                __getDataBloodSugar[0].ToString() + "," +
                __getDataCholesterol[0].ToString() + "," +
                __getDataProtein[0].ToString() + "," +
                __getDataRricAcid[0].ToString() + "," +
                __getDataBoneMass[0].ToString() + "," +
                __getDataRenalFunction[0].ToString() + "," +
                __getDataLiverFunction[0].ToString() + " ) values (" +
                __getDataBodyWeight[1].ToString() + "," +
                __getDataBloodPressure[1].ToString() + "," +
                __getDataBloodSugar[1].ToString() + "," +
                __getDataCholesterol[1].ToString() + "," +
                __getDataProtein[1].ToString() + "," +
                __getDataRricAcid[1].ToString() + "," +
                __getDataBoneMass[1].ToString() + "," +
                __getDataRenalFunction[1].ToString() + "," +
                __getDataLiverFunction[1].ToString() + ")";
            //
            string __myQuery = MyLib._myGlobal._xmlHeader + "<node>";
            __myQuery += "<query>delete from " + _g.d.m_healty_standard._table + "</query>";
            __myQuery += "<query>" + __query + "</query>";
            __myQuery += "</node>";
            MyLib._myFrameWork __myFrameWork = new MyLib._myFrameWork();
            string __result = __myFrameWork._queryList(MyLib._myGlobal._databaseName, __myQuery);
            if (__result.Length == 0)
            {
                MessageBox.Show(MyLib._myGlobal._resource("บันทึกข้อมูลเสร็จเรียบร้อย โปรแกรมจะปิดหน้าจอนี้ให้"), MyLib._myGlobal._resource("สำเร็จ"), MessageBoxButtons.OK, MessageBoxIcon.Information);
                _m_informationScreen_body_weight._isChange = false;
                _m_informationScreen_blood_pressure._isChange = false;
                _m_informationScreen_blood_sugar._isChange = false;
                _m_informationScreen_cholesterol._isChange = false;
                _m_informationScreen_protein._isChange = false;
                _m_informationScreen_uric_acid._isChange = false;
                _m_informationScreen_bone_mass._isChange = false;
                _m_informationScreen_renal_function._isChange = false;
                _m_informationScreen_liver_function._isChange = false;
                Close();
            }
            else
            {
                MessageBox.Show(__result, "Fail", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void menuSave_Click(object sender, EventArgs e)
        {
            save_data();
        }

        private void _buttonSave_Click(object sender, EventArgs e)
        {
            save_data();
        }

        private void _buttonClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }

    public class _m_informationScreen : MyLib._myScreen
    {
        public _m_informationScreen()
        {
            this._maxColumn = 2;
            this._table_name = _g.d.m_healty_standard._table;
            //this._addNumberBox(0, 0, 1, 0, _g.d.m_healty_standard._body_fat_max, 1, 2, true);
            //this._addNumberBox(0, 1, 1, 0, _g.d.m_healty_standard._body_fat_min, 1, 2, true);
            //this._addNumberBox(1, 0, 1, 0, _g.d.m_healty_standard._ldl_max, 1, 0, true);
            //this._addNumberBox(1, 1, 1, 0, _g.d.m_healty_standard._ldl_min, 1, 2, true);
            //this._addNumberBox(2, 0, 1, 0, _g.d.m_healty_standard._hdl_max, 1, 0, true);
            //this._addNumberBox(2, 1, 1, 0, _g.d.m_healty_standard._hdl_min, 1, 2, true);
            //this._addNumberBox(3, 0, 1, 0, _g.d.m_healty_standard._triglyceride_max, 1, 2, true);
            //this._addNumberBox(3, 1, 1, 0, _g.d.m_healty_standard._triglyceride_min, 1, 0, true);
            //this._addNumberBox(4, 0, 1, 0, _g.d.m_healty_standard._cholesterol_max, 1, 0, true);
            //this._addNumberBox(4, 1, 1, 0, _g.d.m_healty_standard._cholesterol_min, 1, 2, true);
            //this._addNumberBox(5, 0, 1, 0, _g.d.m_healty_standard._blood_sugar_max, 1, 2, true);
            //this._addNumberBox(5, 1, 1, 0, _g.d.m_healty_standard._blood_sugar_min, 1, 0, true);
            //this._addNumberBox(6, 0, 1, 0, _g.d.m_healty_standard._bun_max, 1, 2, true);
            //this._addNumberBox(6, 1, 1, 0, _g.d.m_healty_standard._bun_min, 1, 2, true);
            //this._addNumberBox(7, 0, 1, 0, _g.d.m_healty_standard._creatinine_max, 1, 0, true);
            //this._addNumberBox(7, 1, 1, 0, _g.d.m_healty_standard._creatinine_min, 1, 0, true);
            //this._addNumberBox(8, 0, 1, 0, _g.d.m_healty_standard._oxygen_max, 1, 0, true);
            //this._addNumberBox(8, 1, 1, 0, _g.d.m_healty_standard._oxygen_min, 1, 0, true);
            //this._addNumberBox(9, 0, 1, 0, _g.d.m_healty_standard._pulse_max, 1, 0, true);
            //this._addNumberBox(9, 1, 1, 0, _g.d.m_healty_standard._pulse_min, 1, 0, true);
            //this._addNumberBox(10, 0, 1, 0, _g.d.m_healty_standard._presure_max, 1, 0, true);
            //this._addNumberBox(10, 1, 1, 0, _g.d.m_healty_standard._presure_min, 1, 0, true);
        }
    }

    public class _m_informationScreen_body_weight : MyLib._myScreen
    {
        public _m_informationScreen_body_weight()
        {
            this._maxColumn = 2;
            this._table_name = _g.d.m_healty_standard._table;
            this._addNumberBox(0, 0, 1, 0, _g.d.m_healty_standard._body_weight_min, 1, 2, true);
            this._addNumberBox(0, 1, 1, 0, _g.d.m_healty_standard._body_weight_max, 1, 2, true);
            this._addNumberBox(1, 0, 1, 0, _g.d.m_healty_standard._bmi_min, 1, 2, true);
            this._addNumberBox(1, 1, 1, 0, _g.d.m_healty_standard._bmi_max, 1, 0, true);
        }
    }

    public class _m_informationScreen_blood_pressure : MyLib._myScreen
    {
        public _m_informationScreen_blood_pressure()
        {
            this._maxColumn = 2;
            this._table_name = _g.d.m_healty_standard._table;
            this._addNumberBox(0, 0, 1, 0, _g.d.m_healty_standard._cabondioxide_min, 1, 2, true);
            this._addNumberBox(0, 1, 1, 0, _g.d.m_healty_standard._cabondioxide_max, 1, 2, true);
            this._addNumberBox(1, 0, 1, 0, _g.d.m_healty_standard._oxygen_min, 1, 2, true);
            this._addNumberBox(1, 1, 1, 0, _g.d.m_healty_standard._oxygen_max, 1, 0, true);
            this._addNumberBox(2, 0, 1, 0, _g.d.m_healty_standard._bloodpressure_min, 1, 2, true);
            this._addNumberBox(2, 1, 1, 0, _g.d.m_healty_standard._bloodpressure_max, 1, 0, true);
            this._addNumberBox(3, 0, 1, 0, _g.d.m_healty_standard._heart_rate_min, 1, 0, true);
            this._addNumberBox(3, 1, 1, 0, _g.d.m_healty_standard._heart_rate_max, 1, 2, true);
        }
    }

    public class _m_informationScreen_blood_sugar : MyLib._myScreen
    {
        public _m_informationScreen_blood_sugar()
        {
            this._maxColumn = 2;
            this._table_name = _g.d.m_healty_standard._table;
            this._addNumberBox(0, 0, 1, 0, _g.d.m_healty_standard._blood_sugar_min, 1, 2, true);
            this._addNumberBox(0, 1, 1, 0, _g.d.m_healty_standard._blood_sugar_max, 1, 2, true);
            this._addNumberBox(1, 0, 1, 0, _g.d.m_healty_standard._hba1c_min, 1, 0, true);
            this._addNumberBox(1, 1, 1, 0, _g.d.m_healty_standard._hba1c_max, 1, 2, true);
        }
    }

    public class _m_informationScreen_cholesterol : MyLib._myScreen
    {
        public _m_informationScreen_cholesterol()
        {
            this._maxColumn = 2;
            this._table_name = _g.d.m_healty_standard._table;
            this._addNumberBox(0, 0, 1, 0, _g.d.m_healty_standard._cholesterol_min, 1, 2, true);
            this._addNumberBox(0, 1, 1, 0, _g.d.m_healty_standard._cholesterol_max, 1, 2, true);
            this._addNumberBox(1, 0, 1, 0, _g.d.m_healty_standard._triglyceride_min, 1, 0, true);
            this._addNumberBox(1, 1, 1, 0, _g.d.m_healty_standard._triglyceride_max, 1, 2, true);
            this._addNumberBox(2, 0, 1, 0, _g.d.m_healty_standard._hdl_min, 1, 0, true);
            this._addNumberBox(2, 1, 1, 0, _g.d.m_healty_standard._hdl_max, 1, 2, true);
            this._addNumberBox(3, 0, 1, 0, _g.d.m_healty_standard._ldl_min, 1, 2, true);
            this._addNumberBox(3, 1, 1, 0, _g.d.m_healty_standard._ldl_max, 1, 0, true);
            this._addNumberBox(4, 0, 1, 0, _g.d.m_healty_standard._body_fat_min, 1, 0, true);
            this._addNumberBox(4, 1, 1, 0, _g.d.m_healty_standard._body_fat_max, 1, 2, true);
        }
    }

    public class _m_informationScreen_protein : MyLib._myScreen
    {
        public _m_informationScreen_protein()
        {
            this._maxColumn = 2;
            this._table_name = _g.d.m_healty_standard._table;
            this._addNumberBox(0, 0, 1, 0, _g.d.m_healty_standard._total_protein_min, 1, 2, true);
            this._addNumberBox(0, 1, 1, 0, _g.d.m_healty_standard._total_protein_max, 1, 2, true);
            this._addNumberBox(1, 0, 1, 0, _g.d.m_healty_standard._albumin_min, 1, 0, true);
            this._addNumberBox(1, 1, 1, 0, _g.d.m_healty_standard._albumin_max, 1, 2, true);
            this._addNumberBox(2, 0, 1, 0, _g.d.m_healty_standard._globurin_min, 1, 0, true);
            this._addNumberBox(2, 1, 1, 0, _g.d.m_healty_standard._globurin_max, 1, 2, true);
        }
    }

    public class _m_informationScreen_uric_acid : MyLib._myScreen
    {
        public _m_informationScreen_uric_acid()
        {
            this._maxColumn = 2;
            this._table_name = _g.d.m_healty_standard._table;
            this._addNumberBox(0, 0, 1, 0, _g.d.m_healty_standard._uric_acid_min, 1, 2, true);
            this._addNumberBox(0, 1, 1, 0, _g.d.m_healty_standard._uric_acid_max, 1, 2, true);
        }
    }

    public class _m_informationScreen_bone_mass : MyLib._myScreen
    {
        public _m_informationScreen_bone_mass()
        {
            this._maxColumn = 2;
            this._table_name = _g.d.m_healty_standard._table;
            this._addNumberBox(0, 0, 1, 0, _g.d.m_healty_standard._bone_mass_min, 1, 2, true);
            this._addNumberBox(0, 1, 1, 0, _g.d.m_healty_standard._bone_mass_max, 1, 2, true);
        }
    }

    public class _m_informationScreen_renal_function : MyLib._myScreen
    {
        public _m_informationScreen_renal_function()
        {
            this._maxColumn = 2;
            this._table_name = _g.d.m_healty_standard._table;
            this._addNumberBox(0, 0, 1, 0, _g.d.m_healty_standard._sodium_min, 1, 2, true);
            this._addNumberBox(0, 1, 1, 0, _g.d.m_healty_standard._sodium_max, 1, 2, true);
            this._addNumberBox(1, 0, 1, 0, _g.d.m_healty_standard._potassium_min, 1, 0, true);
            this._addNumberBox(1, 1, 1, 0, _g.d.m_healty_standard._potassium_max, 1, 2, true);
            this._addNumberBox(2, 0, 1, 0, _g.d.m_healty_standard._chloride_min, 1, 0, true);
            this._addNumberBox(2, 1, 1, 0, _g.d.m_healty_standard._chloride_max, 1, 2, true);
            this._addNumberBox(3, 0, 1, 0, _g.d.m_healty_standard._bun_min, 1, 2, true);
            this._addNumberBox(3, 1, 1, 0, _g.d.m_healty_standard._bun_max, 1, 0, true);
            this._addNumberBox(4, 0, 1, 0, _g.d.m_healty_standard._creatinine_min, 1, 0, true);
            this._addNumberBox(4, 1, 1, 0, _g.d.m_healty_standard._creatinine_max, 1, 2, true);
        }
    }

    public class _m_informationScreen_liver_function : MyLib._myScreen
    {
        public _m_informationScreen_liver_function()
        {
            this._maxColumn = 2;
            this._table_name = _g.d.m_healty_standard._table;
            this._addNumberBox(0, 0, 1, 0, _g.d.m_healty_standard._total_bilirubin_min, 1, 2, true);
            this._addNumberBox(0, 1, 1, 0, _g.d.m_healty_standard._total_bilirubin_max, 1, 2, true);
            this._addNumberBox(1, 0, 1, 0, _g.d.m_healty_standard._direct_bilirubin_min, 1, 0, true);
            this._addNumberBox(1, 1, 1, 0, _g.d.m_healty_standard._direct_bilirubin_max, 1, 2, true);
            this._addNumberBox(2, 0, 1, 0, _g.d.m_healty_standard._sgot_ast_min, 1, 0, true);
            this._addNumberBox(2, 1, 1, 0, _g.d.m_healty_standard._sgot_ast_max, 1, 2, true);
            this._addNumberBox(3, 0, 1, 0, _g.d.m_healty_standard._sgpt_alt_min, 1, 2, true);
            this._addNumberBox(3, 1, 1, 0, _g.d.m_healty_standard._sgpt_alt_max, 1, 0, true);
            this._addNumberBox(4, 0, 1, 0, _g.d.m_healty_standard._alkaline_phosp_min, 1, 0, true);
            this._addNumberBox(4, 1, 1, 0, _g.d.m_healty_standard._alkaline_phosp_max, 1, 2, true);
        }
    }

}
