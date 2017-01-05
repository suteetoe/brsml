using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Windows.Forms;
using System.Globalization;
using System.Collections;

namespace SMLProcess
{
    public class _payrollProcess
    {
        MyLib._myFrameWork myFrameWork = new MyLib._myFrameWork();
        /// <summary>
        /// คำนวนหักค่าแรง(ขาดงาน)
        /// </summary>
        /// <param name="getSowType"></param>
        /// <param name="gettxtEmployee"></param>
        /// <param name="getManyDay"></param>
        /// <returns></returns>
        public decimal _processTranShortOfWork(string getSowType, string gettxtEmployee, decimal getManyDay , string getWoringDate)
        {
            decimal getSumMoney = 0;//จำนวนเงินรวม
            string employeecode = _splitWord(gettxtEmployee);//รหัสพนักงาน
            string sowtypecode = _splitWord(getSowType); //รหัสการลา

            string queryMothod = "select "
                + "coalesce((" + _g._dataPayroll.payroll_company_config._hourly + "),0) as " + _g._dataPayroll.payroll_company_config._hourly + ","
                + "coalesce((" + _g._dataPayroll.payroll_company_config._hourly_day + "),0) as " + _g._dataPayroll.payroll_company_config._hourly_day + ","
                + "coalesce((" + _g._dataPayroll.payroll_company_config._hourly_week + "),0) as " + _g._dataPayroll.payroll_company_config._hourly_week + ","
                + "coalesce((" + _g._dataPayroll.payroll_company_config._work_day + "),0) as " + _g._dataPayroll.payroll_company_config._work_day
                + " from " + _g._dataPayroll.payroll_company_config._table;

            string querySalary = "select coalesce((" + _g._dataPayroll.payroll_employee._salary + "),0) as " + _g._dataPayroll.payroll_employee._salary + ","
                + "coalesce((" + _g._dataPayroll.payroll_employee._salary_status + "),0) as "+ _g._dataPayroll.payroll_employee._salary_status + ","
                + _g._dataPayroll.payroll_employee._employee_type
                + " from " + _g._dataPayroll.payroll_employee._table
                + " where " + _g._dataPayroll.payroll_employee._code + " =\'" + employeecode + "\'";

            string querySow = "select coalesce((" + _g._dataPayroll.payroll_short_of_work._wages_cut_rate + "),0) as " + _g._dataPayroll.payroll_short_of_work._wages_cut_rate
                + ",coalesce((" + _g._dataPayroll.payroll_short_of_work._wages_cut_money + "),0) as " + _g._dataPayroll.payroll_short_of_work._wages_cut_money
                + ",coalesce((" + _g._dataPayroll.payroll_short_of_work._select_rate_or_money + "),0) as " + _g._dataPayroll.payroll_short_of_work._select_rate_or_money
                + " from " + _g._dataPayroll.payroll_short_of_work._table
                + " where " + _g._dataPayroll.payroll_short_of_work._code + " =\'" + sowtypecode + "\'";

            string querySalaryVary = "select coalesce((" + _g._dataPayroll.payroll_employee_salary._salary + "),0) as " + _g._dataPayroll.payroll_employee_salary._salary + ","
                + _g._dataPayroll.payroll_employee_salary._start_date + ","
                + _g._dataPayroll.payroll_employee_salary._end_date
                + " from " + _g._dataPayroll.payroll_employee_salary._table
                + " where " + _g._dataPayroll.payroll_employee_salary._code + " ='" + employeecode + "'"
                + " and " + _g._dataPayroll.payroll_employee_salary._start_date + " <= '" + getWoringDate +"'"
                + " and (" + _g._dataPayroll.payroll_employee_salary._end_date + " >= '" + getWoringDate + "' or " + _g._dataPayroll.payroll_employee_salary._end_date + " = '1000-01-01')";

            try
            {
                DataSet dataResultMothod = myFrameWork._query(MyLib._myGlobal._databaseName, queryMothod);
                decimal getDataHourlyMonth = decimal.Parse(dataResultMothod.Tables[0].Rows[0][_g._dataPayroll.payroll_company_config._hourly].ToString());
                decimal getDataHourlyDay = decimal.Parse(dataResultMothod.Tables[0].Rows[0][_g._dataPayroll.payroll_company_config._hourly_day].ToString());
                decimal getDataHourlyWeek = decimal.Parse(dataResultMothod.Tables[0].Rows[0][_g._dataPayroll.payroll_company_config._hourly_week].ToString());
                decimal getDataWorkDay = decimal.Parse(dataResultMothod.Tables[0].Rows[0][_g._dataPayroll.payroll_company_config._work_day].ToString());

                DataSet dataResult = myFrameWork._query(MyLib._myGlobal._databaseName, querySalary);                   
                decimal getDataEmployeeType = decimal.Parse(dataResult.Tables[0].Rows[0][_g._dataPayroll.payroll_employee._employee_type].ToString());//ประเภทลูกจ้าง
                int getDataSalaryStatus = MyLib._myGlobal._intPhase(dataResult.Tables[0].Rows[0][_g._dataPayroll.payroll_employee._salary_status].ToString());//สถานะเงินเดือนว่าใช้แบบไหน

                DataSet dataResultSow = myFrameWork._query(MyLib._myGlobal._databaseName, querySow);
                decimal getDataCutRate = decimal.Parse(dataResultSow.Tables[0].Rows[0][_g._dataPayroll.payroll_short_of_work._wages_cut_rate].ToString());//อัตราหักค่าแรง    
                decimal getDataCutMoney = decimal.Parse(dataResultSow.Tables[0].Rows[0][_g._dataPayroll.payroll_short_of_work._wages_cut_money].ToString());//จำนวนเงินที่หัก 
                decimal getDataSelect = decimal.Parse(dataResultSow.Tables[0].Rows[0][_g._dataPayroll.payroll_short_of_work._select_rate_or_money].ToString());//วิธีคำนวน

                DataSet dataResultSalaryVary = myFrameWork._query(MyLib._myGlobal._databaseName, querySalaryVary);

                decimal getDataSalary  = 0.0M;
                if (getDataSalaryStatus == 0)
                {
                    getDataSalary = decimal.Parse(dataResult.Tables[0].Rows[0][_g._dataPayroll.payroll_employee._salary].ToString());//เงินเดือน
                }
                else
                {
                    getDataSalary = decimal.Parse(dataResultSalaryVary.Tables[0].Rows[0][_g._dataPayroll.payroll_employee_salary._salary].ToString());//เงินเดือน
                }
               // decimal mySalary = 0;
                //คิดเงินค่าจ้าง ประมาณ 1 เดือน เพื่อนเอามาคำนวนหา การหักค่าแรง 
                if (getDataSelect == 0)
                {
                    if (getDataEmployeeType == 0)
                    {
                        getSumMoney = (((getDataSalary / 30) * getManyDay) * getDataCutRate);
                    }
                    else if (getDataEmployeeType == 1)
                    {
                        getSumMoney = (((getDataSalary / getDataWorkDay) * getManyDay) * getDataCutRate);
                    }
                    else
                    {
                        getSumMoney = ((getDataSalary * getManyDay) * getDataCutRate);
                    }
                }
                else
                {
                    getSumMoney = getDataCutMoney;

                }               

            }
            catch (Exception ex)
            {
                MessageBox.Show(" ข้อมูลผิดพลาด ไม่มีข้อมูล ค่าจ้าง ในวันที่ " + getWoringDate);
            }
            return getSumMoney;
        }
        /// <summary>
        /// คำนวนเงินหักการลางาน
        /// </summary>
        /// <param name="getLeaveType"></param>
        /// <param name="getTextEmployee"></param>
        /// <param name="getLeaveHours"></param>
        /// <param name="getLeaveMinute"></param>
        /// <returns></returns>
        public decimal _processLeave(string getLeaveType, string getTextEmployee, decimal getLeaveHours, decimal getLeaveMinute, string getWoringDate)
        {
            decimal getSumMoney = 0;//จำนวนเงินรวม
            string employeecode = _splitWord(getTextEmployee); //รหัสพนักงาน
            string leavetypecode = _splitWord(getLeaveType);  //รหัส การลา

            string queryMothod = "select "
                + "coalesce((" + _g._dataPayroll.payroll_company_config._hourly + "),0) as " + _g._dataPayroll.payroll_company_config._hourly + ","
                + "coalesce((" + _g._dataPayroll.payroll_company_config._hourly_day + "),0) as " + _g._dataPayroll.payroll_company_config._hourly_day + ","
                + "coalesce((" + _g._dataPayroll.payroll_company_config._hourly_week + "),0) as " + _g._dataPayroll.payroll_company_config._hourly_week + ","
                + "coalesce((" + _g._dataPayroll.payroll_company_config._work_day + "),0) as " + _g._dataPayroll.payroll_company_config._work_day
                + " from " + _g._dataPayroll.payroll_company_config._table;

            string querySalary = "select coalesce((" + _g._dataPayroll.payroll_employee._salary + "),0) as " + _g._dataPayroll.payroll_employee._salary + ","
                + "coalesce((" + _g._dataPayroll.payroll_employee._salary_status + "),0) as " + _g._dataPayroll.payroll_employee._salary_status + ","
                + _g._dataPayroll.payroll_employee._employee_type
                + " from " + _g._dataPayroll.payroll_employee._table
                + " where " + _g._dataPayroll.payroll_employee._code + " =\'" + employeecode + "\'";

            string queryLeave = "select coalesce((" + _g._dataPayroll.payroll_leave._leave_rate + "),0) as " + _g._dataPayroll.payroll_leave._leave_rate + ","
                + "coalesce((" + _g._dataPayroll.payroll_leave._leave_money + "),0) as " + _g._dataPayroll.payroll_leave._leave_money + ","
                + "coalesce((" + _g._dataPayroll.payroll_leave._select_rate_or_money + "),0) as " + _g._dataPayroll.payroll_leave._select_rate_or_money
                + " from " + _g._dataPayroll.payroll_leave._table
                + "  where " + _g._dataPayroll.payroll_leave._code + " =\'" + leavetypecode + "\'";

            string querySalaryVary = "select coalesce((" + _g._dataPayroll.payroll_employee_salary._salary + "),0) as " + _g._dataPayroll.payroll_employee_salary._salary + ","
                + _g._dataPayroll.payroll_employee_salary._start_date + ","
                + _g._dataPayroll.payroll_employee_salary._end_date
                + " from " + _g._dataPayroll.payroll_employee_salary._table
                + " where " + _g._dataPayroll.payroll_employee_salary._code + " ='" + employeecode + "'"
                + " and " + _g._dataPayroll.payroll_employee_salary._start_date + " <= '" + getWoringDate + "'"
                + " and (" + _g._dataPayroll.payroll_employee_salary._end_date + " >= '" + getWoringDate + "' or " + _g._dataPayroll.payroll_employee_salary._end_date + " = '1000-01-01')";

            try
            {
                DataSet dataResultMothod = myFrameWork._query(MyLib._myGlobal._databaseName, queryMothod);
                decimal getDataHourlyMonth = decimal.Parse(dataResultMothod.Tables[0].Rows[0][_g._dataPayroll.payroll_company_config._hourly].ToString());
                decimal getDataHourlyDay = decimal.Parse(dataResultMothod.Tables[0].Rows[0][_g._dataPayroll.payroll_company_config._hourly_day].ToString());
                decimal getDataHourlyWeek = decimal.Parse(dataResultMothod.Tables[0].Rows[0][_g._dataPayroll.payroll_company_config._hourly_week].ToString());
                decimal getDataWorkDay = decimal.Parse(dataResultMothod.Tables[0].Rows[0][_g._dataPayroll.payroll_company_config._work_day].ToString());

                DataSet dataResult = myFrameWork._query(MyLib._myGlobal._databaseName, querySalary);
                //decimal getDataSalary = decimal.Parse(dataResult.Tables[0].Rows[0][_g._dataPayroll.payroll_employee._salary].ToString());//เงินเดือน  
                decimal getDataEmployeeType = decimal.Parse(dataResult.Tables[0].Rows[0][_g._dataPayroll.payroll_employee._employee_type].ToString());//ประเภทลูกจ้าง
                int getDataSalaryStatus = MyLib._myGlobal._intPhase(dataResult.Tables[0].Rows[0][_g._dataPayroll.payroll_employee._salary_status].ToString());//สถานะเงินเดือนว่าใช้แบบไหน

                DataSet dataResultLeave = myFrameWork._query(MyLib._myGlobal._databaseName, queryLeave);
                decimal getDataRate = decimal.Parse(dataResultLeave.Tables[0].Rows[0][_g._dataPayroll.payroll_leave._leave_rate].ToString());//อัตรา
                decimal getDataCutMoney = decimal.Parse(dataResultLeave.Tables[0].Rows[0][_g._dataPayroll.payroll_leave._leave_money].ToString());//จำนวนเงินที่หัก 
                decimal getDataSelect = decimal.Parse(dataResultLeave.Tables[0].Rows[0][_g._dataPayroll.payroll_leave._select_rate_or_money].ToString());//วิธีคำนวน
                //คำนวนเวลามาสาย
                decimal sumLeaveMinute = (getLeaveHours * 60) + getLeaveMinute;
                decimal sumLeaveHours = decimal.Parse(String.Format("{0:0.######}", (sumLeaveMinute / 60)));

                DataSet dataResultSalaryVary = myFrameWork._query(MyLib._myGlobal._databaseName, querySalaryVary);

                decimal getDataSalary = 0.0M;
                if (getDataSalaryStatus == 0)
                {
                    getDataSalary = decimal.Parse(dataResult.Tables[0].Rows[0][_g._dataPayroll.payroll_employee._salary].ToString());//เงินเดือน
                }
                else
                {
                    getDataSalary = decimal.Parse(dataResultSalaryVary.Tables[0].Rows[0][_g._dataPayroll.payroll_employee_salary._salary].ToString());//เงินเดือน
                }
                //คิดว่าได้ค่าล่วงเวลาเท่าไรก่อน
                if (getDataSelect == 0)
                {
                    if (getDataEmployeeType == 0)
                    {
                        getSumMoney = ((((getDataSalary / 30) / getDataHourlyMonth) * sumLeaveHours) * getDataRate);
                    }
                    else if (getDataEmployeeType == 1)
                    {
                        getSumMoney = ((((getDataSalary / getDataWorkDay) / getDataHourlyWeek) * sumLeaveHours) * getDataRate);
                    }
                    else
                    {
                        getSumMoney = (((getDataSalary / getDataHourlyDay) * sumLeaveHours) * getDataRate);
                    }
                }
                else
                {
                    getSumMoney = getDataCutMoney;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(" ข้อมูลผิดพลาด ไม่มีข้อมูล ค่าจ้าง ในวันที่ " + getWoringDate);
            }
            return getSumMoney;
        }
        /// <summary>
        /// คำนวนเงินหักมาสาย
        /// </summary>
        /// <param name="getLateType"></param>
        /// <param name="getTextEmployee"></param>
        /// <param name="getLateHours"></param>
        /// <param name="getLateMinute"></param>
        /// <returns></returns>
        public decimal _processArriveLate(string getLateType, string getTextEmployee, decimal getLateHours, decimal getLateMinute, string getWoringDate)
        {
            decimal getSumMoney = 0;//จำนวนเงินรวม
            string employeecode = _splitWord(getTextEmployee); //รหัสพนักงาน
            string latetypecode = _splitWord(getLateType);  //รหัส มาสาย

            string queryMothod = "select "
                + "coalesce((" + _g._dataPayroll.payroll_company_config._hourly + "),0) as " + _g._dataPayroll.payroll_company_config._hourly + ","
                + "coalesce((" + _g._dataPayroll.payroll_company_config._hourly_day + "),0) as " + _g._dataPayroll.payroll_company_config._hourly_day + ","
                + "coalesce((" + _g._dataPayroll.payroll_company_config._hourly_week + "),0) as " + _g._dataPayroll.payroll_company_config._hourly_week + ","
                + "coalesce((" + _g._dataPayroll.payroll_company_config._work_day + "),0) as " + _g._dataPayroll.payroll_company_config._work_day
                + " from " + _g._dataPayroll.payroll_company_config._table;

            string querySalary = "select coalesce((" + _g._dataPayroll.payroll_employee._salary + "),0) as " + _g._dataPayroll.payroll_employee._salary + ","
                + "coalesce((" + _g._dataPayroll.payroll_employee._salary_status + "),0) as " + _g._dataPayroll.payroll_employee._salary_status + ","
                + _g._dataPayroll.payroll_employee._employee_type
                + " from " + _g._dataPayroll.payroll_employee._table
                + " where " + _g._dataPayroll.payroll_employee._code + " =\'" + employeecode + "\'";

            string queryLate = "select coalesce((" + _g._dataPayroll.payroll_arrive_late._arrive_late_rate + "),0) as " + _g._dataPayroll.payroll_arrive_late._arrive_late_rate + ","
                + "coalesce((" + _g._dataPayroll.payroll_arrive_late._arrive_late_money + "),0) as " + _g._dataPayroll.payroll_arrive_late._arrive_late_money + ","
                + "coalesce((" + _g._dataPayroll.payroll_arrive_late._select_rate_or_money + "),0) as " + _g._dataPayroll.payroll_arrive_late._select_rate_or_money
                + " from " + _g._dataPayroll.payroll_arrive_late._table
                + "  where " + _g._dataPayroll.payroll_arrive_late._code + " =\'" + latetypecode + "\'";

            string querySalaryVary = "select coalesce((" + _g._dataPayroll.payroll_employee_salary._salary + "),0) as " + _g._dataPayroll.payroll_employee_salary._salary + ","
                + _g._dataPayroll.payroll_employee_salary._start_date + ","
                + _g._dataPayroll.payroll_employee_salary._end_date
                + " from " + _g._dataPayroll.payroll_employee_salary._table
                + " where " + _g._dataPayroll.payroll_employee_salary._code + " ='" + employeecode + "'"
                + " and " + _g._dataPayroll.payroll_employee_salary._start_date + " <= '" + getWoringDate + "'"
                + " and (" + _g._dataPayroll.payroll_employee_salary._end_date + " >= '" + getWoringDate + "' or " + _g._dataPayroll.payroll_employee_salary._end_date + " = '1000-01-01')";

            try
            {
                DataSet dataResultMothod = myFrameWork._query(MyLib._myGlobal._databaseName, queryMothod);
                decimal getDataHourlyMonth = decimal.Parse(dataResultMothod.Tables[0].Rows[0][_g._dataPayroll.payroll_company_config._hourly].ToString());
                decimal getDataHourlyDay = decimal.Parse(dataResultMothod.Tables[0].Rows[0][_g._dataPayroll.payroll_company_config._hourly_day].ToString());
                decimal getDataHourlyWeek = decimal.Parse(dataResultMothod.Tables[0].Rows[0][_g._dataPayroll.payroll_company_config._hourly_week].ToString());
                decimal getDataWorkDay = decimal.Parse(dataResultMothod.Tables[0].Rows[0][_g._dataPayroll.payroll_company_config._work_day].ToString());

                DataSet dataResult = myFrameWork._query(MyLib._myGlobal._databaseName, querySalary);
                //decimal getDataSalary = decimal.Parse(dataResult.Tables[0].Rows[0][_g._dataPayroll.payroll_employee._salary].ToString());//เงินเดือน  
                decimal getDataEmployeeType = decimal.Parse(dataResult.Tables[0].Rows[0][_g._dataPayroll.payroll_employee._employee_type].ToString());//ประเภทลูกจ้าง
                int getDataSalaryStatus = MyLib._myGlobal._intPhase(dataResult.Tables[0].Rows[0][_g._dataPayroll.payroll_employee._salary_status].ToString());//สถานะเงินเดือนว่าใช้แบบไหน

                DataSet dataResultLate = myFrameWork._query(MyLib._myGlobal._databaseName, queryLate);
                decimal getDataRate = decimal.Parse(dataResultLate.Tables[0].Rows[0][_g._dataPayroll.payroll_arrive_late._arrive_late_rate].ToString());//อัตรา
                decimal getDataCutMoney = decimal.Parse(dataResultLate.Tables[0].Rows[0][_g._dataPayroll.payroll_arrive_late._arrive_late_money].ToString());//จำนวนเงินที่หัก 
                decimal getDataSelect = decimal.Parse(dataResultLate.Tables[0].Rows[0][_g._dataPayroll.payroll_arrive_late._select_rate_or_money].ToString());//วิธีคำนวน
                //คำนวนเวลามาสาย
                decimal sumLateMinute = (getLateHours * 60) + getLateMinute;
                decimal sumLateHours = decimal.Parse(String.Format("{0:0.######}", (sumLateMinute / 60)));

                DataSet dataResultSalaryVary = myFrameWork._query(MyLib._myGlobal._databaseName, querySalaryVary);

                decimal getDataSalary = 0.0M;
                if (getDataSalaryStatus == 0)
                {
                    getDataSalary = decimal.Parse(dataResult.Tables[0].Rows[0][_g._dataPayroll.payroll_employee._salary].ToString());//เงินเดือน
                }
                else
                {
                    getDataSalary = decimal.Parse(dataResultSalaryVary.Tables[0].Rows[0][_g._dataPayroll.payroll_employee_salary._salary].ToString());//เงินเดือน
                }
                //คิดว่าได้ค่าล่วงเวลาเท่าไรก่อน
                if (getDataSelect == 0)
                {
                    if (getDataEmployeeType == 0)
                    {
                        getSumMoney = ((((getDataSalary / 30) / getDataHourlyMonth) * sumLateHours) * getDataRate);
                    }
                    else if (getDataEmployeeType == 1)
                    {
                        getSumMoney = ((((getDataSalary / getDataWorkDay) / getDataHourlyWeek) * sumLateHours) * getDataRate);
                    }
                    else
                    {
                        getSumMoney = (((getDataSalary / getDataHourlyDay) * sumLateHours) * getDataRate);
                    }
                }
                else
                {
                    getSumMoney = getDataCutMoney;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(" ข้อมูลผิดพลาด ไม่มีข้อมูล ค่าจ้าง ในวันที่ " + getWoringDate);
            }
            return getSumMoney;
        }
        /// <summary>
        /// คำนวนค่าล่วงเวลา
        /// </summary>
        /// <param name="getOtType"></param>
        /// <param name="getTextEmployee"></param>
        /// <param name="getOtHours"></param>
        /// <param name="getOtMinute"></param>
        /// <returns></returns>
        public decimal _processOverTime(string getOtType, string getTextEmployee, decimal getOtHours, decimal getOtMinute, string getWoringDate)
        {
            decimal getSumMoney = 0;//จำนวนเงินรวม
            string employeecode = _splitWord(getTextEmployee); //รหัสพนักงาน
            string ottypecode = _splitWord(getOtType);  //รหัส โอที

            string queryMothod = "select "
                + "coalesce((" + _g._dataPayroll.payroll_company_config._hourly + "),0) as " + _g._dataPayroll.payroll_company_config._hourly + ","
                + "coalesce((" + _g._dataPayroll.payroll_company_config._hourly_day + "),0) as " + _g._dataPayroll.payroll_company_config._hourly_day + ","
                + "coalesce((" + _g._dataPayroll.payroll_company_config._hourly_week + "),0) as " + _g._dataPayroll.payroll_company_config._hourly_week + ","
                + "coalesce((" + _g._dataPayroll.payroll_company_config._work_day + "),0) as " + _g._dataPayroll.payroll_company_config._work_day
                + " from " + _g._dataPayroll.payroll_company_config._table;

            string querySalary = "select coalesce((" + _g._dataPayroll.payroll_employee._salary + "),0) as " + _g._dataPayroll.payroll_employee._salary + ","
                + "coalesce((" + _g._dataPayroll.payroll_employee._salary_status + "),0) as " + _g._dataPayroll.payroll_employee._salary_status + ","
                + _g._dataPayroll.payroll_employee._employee_type
                + " from " + _g._dataPayroll.payroll_employee._table
                + " where " + _g._dataPayroll.payroll_employee._code + " =\'" + employeecode + "\'";

            string queryOt = "select coalesce((" + _g._dataPayroll.payroll_over_time._ot_rate + "),0) as " + _g._dataPayroll.payroll_over_time._ot_rate
                + " from " + _g._dataPayroll.payroll_over_time._table
                + "  where " + _g._dataPayroll.payroll_over_time._code + " =\'" + ottypecode + "\'";

            string querySalaryVary = "select coalesce((" + _g._dataPayroll.payroll_employee_salary._salary + "),0) as " + _g._dataPayroll.payroll_employee_salary._salary + ","
                + _g._dataPayroll.payroll_employee_salary._start_date + ","
                + _g._dataPayroll.payroll_employee_salary._end_date
                + " from " + _g._dataPayroll.payroll_employee_salary._table
                + " where " + _g._dataPayroll.payroll_employee_salary._code + " ='" + employeecode + "'"
                + " and " + _g._dataPayroll.payroll_employee_salary._start_date + " <= '" + getWoringDate + "'"
                + " and (" + _g._dataPayroll.payroll_employee_salary._end_date + " >= '" + getWoringDate + "' or " + _g._dataPayroll.payroll_employee_salary._end_date + " = '1000-01-01')";

            try
            {
                DataSet dataResultMothod = myFrameWork._query(MyLib._myGlobal._databaseName, queryMothod);
                decimal getDataHourlyMonth = decimal.Parse(dataResultMothod.Tables[0].Rows[0][_g._dataPayroll.payroll_company_config._hourly].ToString());
                decimal getDataHourlyDay = decimal.Parse(dataResultMothod.Tables[0].Rows[0][_g._dataPayroll.payroll_company_config._hourly_day].ToString());
                decimal getDataHourlyWeek = decimal.Parse(dataResultMothod.Tables[0].Rows[0][_g._dataPayroll.payroll_company_config._hourly_week].ToString());
                decimal getDataWorkDay = decimal.Parse(dataResultMothod.Tables[0].Rows[0][_g._dataPayroll.payroll_company_config._work_day].ToString());

                DataSet dataResult = myFrameWork._query(MyLib._myGlobal._databaseName, querySalary);
                //decimal getDataSalary = decimal.Parse(dataResult.Tables[0].Rows[0][_g._dataPayroll.payroll_employee._salary].ToString());//เงินเดือน  
                decimal getDataEmployeeType = decimal.Parse(dataResult.Tables[0].Rows[0][_g._dataPayroll.payroll_employee._employee_type].ToString());//ประเภทลูกจ้าง
                int getDataSalaryStatus = MyLib._myGlobal._intPhase(dataResult.Tables[0].Rows[0][_g._dataPayroll.payroll_employee._salary_status].ToString());//สถานะเงินเดือนว่าใช้แบบไหน

                DataSet dataResultOt = myFrameWork._query(MyLib._myGlobal._databaseName, queryOt);
                decimal getDataRate = decimal.Parse(dataResultOt.Tables[0].Rows[0][_g._dataPayroll.payroll_over_time._ot_rate].ToString());//อัตราล่วงเวลา

                //คำนวนเวลาโอที
                decimal sumOTMinute = (getOtHours * 60) + getOtMinute;
                decimal sumOTHours = decimal.Parse(String.Format("{0:0.######}", (sumOTMinute / 60)));

                DataSet dataResultSalaryVary = myFrameWork._query(MyLib._myGlobal._databaseName, querySalaryVary);

                decimal getDataSalary = 0.0M;
                if (getDataSalaryStatus == 0)
                {
                    getDataSalary = decimal.Parse(dataResult.Tables[0].Rows[0][_g._dataPayroll.payroll_employee._salary].ToString());//เงินเดือน
                }
                else
                {
                    getDataSalary = decimal.Parse(dataResultSalaryVary.Tables[0].Rows[0][_g._dataPayroll.payroll_employee_salary._salary].ToString());//เงินเดือน
                }

                //คิดว่าได้ค่าล่วงเวลาเท่าไรก่อน
                if (getDataEmployeeType == 0)
                {
                    getSumMoney = ((((getDataSalary / 30) / getDataHourlyMonth) * sumOTHours) * getDataRate);
                }
                else if (getDataEmployeeType == 1)
                {
                    getSumMoney = ((((getDataSalary / getDataWorkDay) / getDataHourlyWeek) * sumOTHours) * getDataRate);
                }
                else
                {
                    getSumMoney = (((getDataSalary / getDataHourlyDay) * sumOTHours) * getDataRate);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(" ข้อมูลผิดพลาด ไม่มีข้อมูล ค่าจ้าง ในวันที่ " + getWoringDate);
            }
            return getSumMoney;
        }
       /// <summary>
        /// Function รวมทั้งหมด
       /// </summary>
       /// <param name="FromDate"></param>
       /// <param name="ToDate"></param>
       /// <param name="FromEmployee"></param>
       /// <param name="ToEmployee"></param>
       /// <param name="EmployeeType"></param>
       /// <param name="InMonth"></param>
       /// <param name="InYear"></param>
       /// <param name="Calculate"></param>
       /// <param name="ScreenName"></param>
       /// <returns></returns>
        public DataSet _processCalculate(string FromDate, string ToDate, string FromEmployee, string ToEmployee, string EmployeeType, int InMonth, string InYear, int Option, bool Calculate,int Recorded, _g.g._screenPayrollEnum ScreenName)
        {
            DataSet _processDS = new DataSet();
            StringBuilder _myQuery = new StringBuilder();
            StringBuilder query = new StringBuilder();
            string WhereEmployee = "";
            string WhereTrans = "";
            string WhereTransSub = "";
            string WhereTransDetail = "";
            string WhereTransSubInMonth = "";
            string WhereTransDetailInMonth = "";
            //error
            //string error_employee_code = "";
            //string error_employee_name = "";
            
            //                
            string StartDay = "";
            string EndDay = "";
            switch (ScreenName)
            {
                case _g.g._screenPayrollEnum.รายงานภาษีเงินได้_รายเดือน:
                case _g.g._screenPayrollEnum.ภงด_1:
                case _g.g._screenPayrollEnum.ภงด_1_ใบแนบ:
                case _g.g._screenPayrollEnum.คำนวนภาษีรายเดือน:
                case _g.g._screenPayrollEnum.คำนวนเงินเดือน:
                case _g.g._screenPayrollEnum.รายละเอียดเงินเดือน_ค่าจ้าง_ตามประเภทลูกจ้าง:
                case _g.g._screenPayrollEnum.สลิปเงินเดือน:
                    StartDay = MyLib._myGlobal._convertDateToQuery(this._processStartDate("01", InMonth.ToString(), InYear));
                    EndDay = MyLib._myGlobal._convertDateToQuery(this._processEndDate("28", InMonth.ToString(), InYear, true));
                    break;
            }
            //เช็คว่า ดึงข้อมูลจาก Trans = 1 หรือ Employee = 0
            if (Recorded == 0)
            {               
                
                if (!FromEmployee.Equals("") && !ToEmployee.Equals(""))
                {                   
                    WhereEmployee += " and (" + _g._dataPayroll.payroll_employee._code + " between '" + FromEmployee + "' and '" + ToEmployee + "')";
                }
                else
                {
                    if (!FromEmployee.Equals(""))
                    {
                        WhereEmployee += " and (" + _g._dataPayroll.payroll_employee._code + " >= '" + FromEmployee + "')";
                    }
                    if (!ToEmployee.Equals(""))
                    {
                        WhereEmployee += " and (" + _g._dataPayroll.payroll_employee._code + " =< '" + ToEmployee + "')";
                    }
                }
                if (!FromDate.Equals("") && !ToDate.Equals(""))
                {                   
                    WhereTransSub += " and (" + _g._dataPayroll.payroll_trans._date_pay_salary + " between '" + FromDate + "' and '" + ToDate + "')";
                    WhereTransDetail += " and (" + _g._dataPayroll.payroll_trans_detail._working_date + " between '" + FromDate + "' and '" + ToDate + "')";
                }
                else
                {
                    if (!FromDate.Equals(""))
                    {                        
                        WhereTransSub += " and (" + _g._dataPayroll.payroll_trans._date_pay_salary + " >= '" + FromDate + "')";
                        WhereTransDetail += " and (" + _g._dataPayroll.payroll_trans_detail._working_date + " >= '" + FromDate + "')";
                    }
                    if (!ToDate.Equals(""))
                    {                        
                        WhereTransSub += " and (" + _g._dataPayroll.payroll_trans._date_pay_salary + " =< '" + ToDate + "')";
                        WhereTransDetail += " and (" + _g._dataPayroll.payroll_trans_detail._working_date + " =< '" + ToDate + "')";

                    }
                }
                if (!InYear.Equals(""))
                {                    
                    WhereTransSub += " and (" + _g._dataPayroll.payroll_trans._select_year + " = '" + InYear + "')";
                    WhereTransDetail += " and (" + _g._dataPayroll.payroll_trans_detail._select_year + " = '" + InYear + "')";
                    WhereTransSubInMonth += " and (" + _g._dataPayroll.payroll_trans._select_year + " = '" + InYear + "')";
                    WhereTransDetailInMonth += " and (" + _g._dataPayroll.payroll_trans_detail._select_year + " = '" + InYear + "')";
                }
                if (InMonth < 12)
                {                    
                    WhereTransSub += " and (" + _g._dataPayroll.payroll_trans._select_month + " = " + InMonth + ")";
                    WhereTransDetail += " and (" + _g._dataPayroll.payroll_trans_detail._select_month + " = " + InMonth + ")";
                    WhereTransSubInMonth += " and (" + _g._dataPayroll.payroll_trans._select_month + " = " + InMonth + ")";
                    WhereTransDetailInMonth += " and (" + _g._dataPayroll.payroll_trans_detail._select_month + " = " + InMonth + ")";
                }
                if (!EmployeeType.Equals(""))
                {                    
                    WhereTransSub += " and (" + _g._dataPayroll.payroll_trans._employee_type + " = " + EmployeeType + ")";
                    WhereTransDetail += " and (" + _g._dataPayroll.payroll_trans_detail._employee_type + " = " + EmployeeType + ")";
                    WhereTransSubInMonth += " and (" + _g._dataPayroll.payroll_trans._employee_type + " = " + EmployeeType + ")";
                    WhereTransDetailInMonth += " and (" + _g._dataPayroll.payroll_trans_detail._employee_type + " = " + EmployeeType + ")";
                }
                //Field Per Page
                StringBuilder FieldEmployeeHead = new StringBuilder();
                StringBuilder FieldEmployeeData = new StringBuilder();
                StringBuilder FieldSalarySlipHead = new StringBuilder();
                StringBuilder FieldSalarySlipData = new StringBuilder();
                StringBuilder FileFormonth = new StringBuilder();
                StringBuilder DataFormonth = new StringBuilder();
                switch (ScreenName)
                {
                    case _g.g._screenPayrollEnum.รายละเอียดเงินเดือน_ค่าจ้างทั้งปี:                  
                                            
                        StringBuilder _queryEmployeeDetail = new StringBuilder();

                                    //string StartDayForPriod = "";
                                    //string EndDayForPriod = "";
                                    //if (Option == 1)
                                    //{
                                    //    StartDay = MyLib._myGlobal._convertDateToQuery(this._processStartDate("01", "0", InYear));
                                    //    EndDay = MyLib._myGlobal._convertDateToQuery(this._processEndDate("28", "0", InYear, true));                                      
                                    //}
                                    //else if (Option == 2)
                                    //{
                                    //    StartDay = MyLib._myGlobal._convertDateToQuery(this._processStartDate("01", "0", InYear));
                                    //    EndDay = MyLib._myGlobal._convertDateToQuery(this._processEndDate("28", "0", InYear, true));                                      
                                    //}

                                    _queryEmployeeDetail.Append("select ");
                                    _queryEmployeeDetail.Append("reserve_fund_status as reserve_fund_status,");
                                    _queryEmployeeDetail.Append("salary_status as salary_status,");
                                    _queryEmployeeDetail.Append("reduce_social_security as reduce_social_security,");
                                    _queryEmployeeDetail.Append("reduce_reserve_fund as reduce_reserve_fund,");
                                    _queryEmployeeDetail.Append("reserve_fund_max as reserve_fund_max,");
                                    _queryEmployeeDetail.Append("reserve_fund_min as reserve_fund_min,");
                                    _queryEmployeeDetail.Append("customer_rf_rate as customer_rf_rate,");
                                    _queryEmployeeDetail.Append("employer_rf_rate as employer_rf_rate,");
                                    _queryEmployeeDetail.Append("social_security_max as social_security_max,");
                                    _queryEmployeeDetail.Append("social_security_min as social_security_min,");
                                    _queryEmployeeDetail.Append("customer_ss_rate as customer_ss_rate,");
                                    _queryEmployeeDetail.Append("employer_ss_rate as employer_ss_rate,");
                                    _queryEmployeeDetail.Append("reduce_myself as reduce_myself,");
                                    _queryEmployeeDetail.Append("reduce_spouse as reduce_spouse,");
                                    _queryEmployeeDetail.Append("situation as situation,");
                                    _queryEmployeeDetail.Append("spouse_work_status as spouse_work_status,");
                                    _queryEmployeeDetail.Append("spouse_tax as spouse_tax,");
                                    _queryEmployeeDetail.Append("reduce_child_study as reduce_child_study,");
                                    _queryEmployeeDetail.Append("reduce_child_no_study as reduce_child_no_study,");
                                    _queryEmployeeDetail.Append("reduce_parents as reduce_parents,");
                                    _queryEmployeeDetail.Append("insurance_parents as insurance_parents,");
                                    _queryEmployeeDetail.Append("donation as donation,");
                                    _queryEmployeeDetail.Append("donation_education as donation_education,");
                                    _queryEmployeeDetail.Append("insurance as insurance,");
                                    _queryEmployeeDetail.Append("loan as loan,");
                                    _queryEmployeeDetail.Append("buy_funds_reserves as buy_funds_reserves,");
                                    _queryEmployeeDetail.Append("buy_share as buy_share,");
                                    _queryEmployeeDetail.Append("except_funds_reserves as except_funds_reserves,");
                                    _queryEmployeeDetail.Append("except_gbk as except_gbk,");
                                    _queryEmployeeDetail.Append("except_teacher as except_teacher,");
                                    _queryEmployeeDetail.Append("except_65year as except_65year,");
                                    _queryEmployeeDetail.Append("except_spouse_65year as except_spouse_65year,");
                                    _queryEmployeeDetail.Append("except_labour_legislation as except_labour_legislation,");
                                    _queryEmployeeDetail.Append("reduce_other as reduce_other,");
                                    _queryEmployeeDetail.Append("(0) as tax_income,");
                                    _queryEmployeeDetail.Append("(0) as reduce_40,");
                                    _queryEmployeeDetail.Append("(0) as reduce_child,");
                                    _queryEmployeeDetail.Append("(0) as remark,");
                                    _queryEmployeeDetail.Append("(" + _g._dataPayroll.payroll_resource._select_month + ") as " + _g._dataPayroll.payroll_resource._select_month + ",");
                                    _queryEmployeeDetail.Append(" " + _g._dataPayroll.payroll_trans._sow_money + " as " + _g._dataPayroll.payroll_resource._sow_money + ",");
                                    _queryEmployeeDetail.Append(" " + _g._dataPayroll.payroll_trans._leave_money + " as " + _g._dataPayroll.payroll_resource._leave_money + ",");
                                    _queryEmployeeDetail.Append(" " + _g._dataPayroll.payroll_trans._late_money + " as " + _g._dataPayroll.payroll_resource._late_money + ",");
                                    _queryEmployeeDetail.Append(" " + _g._dataPayroll.payroll_trans._ot_money + " as " + _g._dataPayroll.payroll_resource._ot_money + ",");
                                    _queryEmployeeDetail.Append(" " + _g._dataPayroll.payroll_trans._diligent_money + " as " + _g._dataPayroll.payroll_resource._diligent_money + ",");
                                    _queryEmployeeDetail.Append(" " + _g._dataPayroll.payroll_trans._prepay_money + " as " + _g._dataPayroll.payroll_resource._prepay_money + ",");
                                    _queryEmployeeDetail.Append(" " + _g._dataPayroll.payroll_trans._bonus_money + " as " + _g._dataPayroll.payroll_resource._bonus_money + ",");
                                    _queryEmployeeDetail.Append(" " + _g._dataPayroll.payroll_trans._other_income_money + " as " + _g._dataPayroll.payroll_resource._other_income_money + ",");
                                    _queryEmployeeDetail.Append(" " + _g._dataPayroll.payroll_trans._advance_money + " as " + _g._dataPayroll.payroll_resource._advance_money + ",");
                                    _queryEmployeeDetail.Append(" " + _g._dataPayroll.payroll_trans._insure_money + " as " + _g._dataPayroll.payroll_resource._insure_money + ",");
                                    _queryEmployeeDetail.Append(" " + _g._dataPayroll.payroll_trans._other_payout_money + " as " + _g._dataPayroll.payroll_resource._other_payout_money + ",");
                                    _queryEmployeeDetail.Append("(0) as " + _g._dataPayroll.payroll_resource._net_wages + ",");
                                    _queryEmployeeDetail.Append("coalesce(( CASE salary_status WHEN 0 THEN reserve_money_in_month ELSE reserve_money_in_month_vary  END  ),0) as reserve_money_in_month,");
                                    _queryEmployeeDetail.Append("coalesce(( CASE salary_status WHEN 0 THEN society_insure_money_in_month  ELSE society_insure_money_in_month_vary  END ),0) as society_insure_money_in_month,");
                                    _queryEmployeeDetail.Append("coalesce(( CASE salary_status WHEN 0 THEN wages_money_in_month ELSE wages_money_in_month_vary END ),0) as wages_money_in_month,");
                                    _queryEmployeeDetail.Append("coalesce(( CASE salary_status WHEN 0 THEN reserve_money  ELSE reserve_money_vary  END  ),0) as reserve_money,");
                                    _queryEmployeeDetail.Append("coalesce(( CASE salary_status WHEN 0 THEN society_insure_money ELSE society_insure_money_vary END ),0) as society_insure_money,");
                                    _queryEmployeeDetail.Append("coalesce(( CASE salary_status WHEN 0 THEN wages_money ELSE wages_money_vary  END ),0) as wages_money,");
                                    _queryEmployeeDetail.Append("diligent_money_in_month as diligent_money_in_month,");
                                    _queryEmployeeDetail.Append("prepay_money_in_month as prepay_money_in_month,");
                                    _queryEmployeeDetail.Append("bonus_money_in_month as bonus_money_in_month,");
                                    _queryEmployeeDetail.Append("other_income_money_in_month as other_income_money_in_month,");
                                    _queryEmployeeDetail.Append("advance_money_in_month as advance_money_in_month,");
                                    _queryEmployeeDetail.Append("insure_money_in_month as insure_money_in_month,");
                                    _queryEmployeeDetail.Append("other_payout_money_in_month as other_payout_money_in_month,");
                                    _queryEmployeeDetail.Append("sow_money_in_month as sow_money_in_month,");
                                    _queryEmployeeDetail.Append("leave_money_in_month as leave_money_in_month,");
                                    _queryEmployeeDetail.Append("late_money_in_month as late_money_in_month,");
                                    _queryEmployeeDetail.Append("ot_money_in_month as ot_money_in_month,");

                                    _queryEmployeeDetail.Append(" " + _g._dataPayroll.payroll_employee._code + " as " + _g._dataPayroll.payroll_resource._employee + ",");
                                    _queryEmployeeDetail.Append(" " + _g._dataPayroll.payroll_employee._employee_type + " as " + _g._dataPayroll.payroll_resource._employee_type + ",");
                                    _queryEmployeeDetail.Append(" " + _g._dataPayroll.payroll_employee._name + " as " + _g._dataPayroll.payroll_resource._employee_name);

                                    _queryEmployeeDetail.Append(" from ");
                                    _queryEmployeeDetail.Append("( ");

                                    for (int i = 0; i < 11; i++)
                                    {
                                        string WhereTransSub_1 = " and (" + _g._dataPayroll.payroll_trans._date_pay_salary + " between '" + MyLib._myGlobal._convertDateToQuery(this._processStartDate("01", i.ToString(), InYear)) + "' and '" + MyLib._myGlobal._convertDateToQuery(this._processEndDate("28", i.ToString(), InYear, true)) + "')";
                                        string WhereTransDetail_1 = " and (" + _g._dataPayroll.payroll_trans_detail._working_date + " between '" + MyLib._myGlobal._convertDateToQuery(this._processStartDate("01", i.ToString(), InYear)) + "' and '" + MyLib._myGlobal._convertDateToQuery(this._processEndDate("28", i.ToString(), InYear, true)) + "')";
                                        string WhereTransSubInMonth_1 = " and (" + _g._dataPayroll.payroll_trans._date_pay_salary + "  between '" + MyLib._myGlobal._convertDateToQuery(this._processStartDate("01", i.ToString(), InYear)) + "' and '" + MyLib._myGlobal._convertDateToQuery(this._processEndDate("28", i.ToString(), InYear, true)) + "')";
                                        string WhereTransDetailInMonth_1 = " and (" + _g._dataPayroll.payroll_trans_detail._select_year + " between '" + MyLib._myGlobal._convertDateToQuery(this._processStartDate("01", i.ToString(), InYear)) + "' and '" + MyLib._myGlobal._convertDateToQuery(this._processEndDate("28", i.ToString(), InYear, true)) + "')";
                                        string StartDay_1 = MyLib._myGlobal._convertDateToQuery(this._processStartDate("01", i.ToString(), InYear));
                                        string EndDay_1 = MyLib._myGlobal._convertDateToQuery(this._processEndDate("28", i.ToString(), InYear, true));
                                       
                                        _queryEmployeeDetail.Append("(select ");
                                        _queryEmployeeDetail.Append("(" + i + ") as " + _g._dataPayroll.payroll_resource._select_month + ",");
                                        _queryEmployeeDetail.Append("coalesce(" + _g._dataPayroll.payroll_employee._reserve_fund_status + ",0)  as reserve_fund_status,");
                                        _queryEmployeeDetail.Append("coalesce(( " + _g._dataPayroll.payroll_employee._salary_status + "),0) as " + _g._dataPayroll.payroll_employee._salary_status + ",");
                                        _queryEmployeeDetail.Append("coalesce((select " + _g._dataPayroll.payroll_company_config._reduce_social_security + " from " + _g._dataPayroll.payroll_company_config._table + " ),0) as reduce_social_security,");
                                        _queryEmployeeDetail.Append("coalesce((select " + _g._dataPayroll.payroll_company_config._reduce_reserve_fund + " from " + _g._dataPayroll.payroll_company_config._table + " ),0) as reduce_reserve_fund,");
                                        _queryEmployeeDetail.Append("coalesce((select  ceiling((coalesce((" + _g._dataPayroll.payroll_company_config._social_security_max + "),0) * coalesce((" + _g._dataPayroll.payroll_company_config._customer_rf_rate + "),0))/100)  from " + _g._dataPayroll.payroll_company_config._table + " ),0) as reserve_fund_max,");
                                        _queryEmployeeDetail.Append("coalesce((select  ceiling((coalesce((" + _g._dataPayroll.payroll_company_config._social_security_min + "),0) * coalesce((" + _g._dataPayroll.payroll_company_config._customer_rf_rate + "),0))/100)  from " + _g._dataPayroll.payroll_company_config._table + " ),0) as reserve_fund_min,");
                                        _queryEmployeeDetail.Append("coalesce((select " + _g._dataPayroll.payroll_company_config._customer_rf_rate + " from " + _g._dataPayroll.payroll_company_config._table + " ),0) as customer_rf_rate,");
                                        _queryEmployeeDetail.Append("coalesce((select " + _g._dataPayroll.payroll_company_config._employer_rf_rate + " from " + _g._dataPayroll.payroll_company_config._table + " ),0) as employer_rf_rate,");
                                        _queryEmployeeDetail.Append("coalesce((select  ceiling((coalesce((" + _g._dataPayroll.payroll_company_config._social_security_max + "),0) * coalesce((" + _g._dataPayroll.payroll_company_config._customer_ss_rate + "),0))/100)  from " + _g._dataPayroll.payroll_company_config._table + " ),0) as social_security_max,");
                                        _queryEmployeeDetail.Append("coalesce((select  ceiling((coalesce((" + _g._dataPayroll.payroll_company_config._social_security_min + "),0) * coalesce((" + _g._dataPayroll.payroll_company_config._customer_ss_rate + "),0))/100)  from " + _g._dataPayroll.payroll_company_config._table + " ),0) as social_security_min,");
                                        _queryEmployeeDetail.Append("coalesce((select " + _g._dataPayroll.payroll_company_config._customer_ss_rate + " from " + _g._dataPayroll.payroll_company_config._table + " ),0) as customer_ss_rate,");
                                        _queryEmployeeDetail.Append("coalesce((select " + _g._dataPayroll.payroll_company_config._employer_ss_rate + " from " + _g._dataPayroll.payroll_company_config._table + " ),0) as employer_ss_rate,");
                                        _queryEmployeeDetail.Append("coalesce((select " + _g._dataPayroll.payroll_employee_detail._reduce_myself + " from " + _g._dataPayroll.payroll_employee_detail._table + " where " + _g._dataPayroll.payroll_employee_detail._code + " = " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + "),0) as reduce_myself ,");
                                        _queryEmployeeDetail.Append("coalesce((select " + _g._dataPayroll.payroll_employee_detail._reduce_spouse + " from " + _g._dataPayroll.payroll_employee_detail._table + " where " + _g._dataPayroll.payroll_employee_detail._code + " = " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + "),0) as reduce_spouse ,");
                                        _queryEmployeeDetail.Append("(select " + _g._dataPayroll.payroll_employee_detail._situation + " from " + _g._dataPayroll.payroll_employee_detail._table + " where " + _g._dataPayroll.payroll_employee_detail._code + " = " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + ") as situation ,");
                                        _queryEmployeeDetail.Append("(select " + _g._dataPayroll.payroll_employee_detail._spouse_work_status + " from " + _g._dataPayroll.payroll_employee_detail._table + " where " + _g._dataPayroll.payroll_employee_detail._code + " = " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + ") as spouse_work_status ,");
                                        _queryEmployeeDetail.Append("(select " + _g._dataPayroll.payroll_employee_detail._spouse_tax + " from " + _g._dataPayroll.payroll_employee_detail._table + " where " + _g._dataPayroll.payroll_employee_detail._code + " = " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + ") as spouse_tax ,");
                                        _queryEmployeeDetail.Append("coalesce((select " + _g._dataPayroll.payroll_employee_detail._reduce_child_study + " from " + _g._dataPayroll.payroll_employee_detail._table + " where " + _g._dataPayroll.payroll_employee_detail._code + " = " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + "),0) as reduce_child_study ,");
                                        _queryEmployeeDetail.Append("coalesce((select " + _g._dataPayroll.payroll_employee_detail._reduce_child_no_study + " from " + _g._dataPayroll.payroll_employee_detail._table + " where " + _g._dataPayroll.payroll_employee_detail._code + " = " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + "),0) as reduce_child_no_study, ");
                                        _queryEmployeeDetail.Append("coalesce((select " + _g._dataPayroll.payroll_employee_detail._reduce_parents + " from " + _g._dataPayroll.payroll_employee_detail._table + " where " + _g._dataPayroll.payroll_employee_detail._code + " = " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + "),0) as reduce_parents ,");
                                        _queryEmployeeDetail.Append("coalesce((select " + _g._dataPayroll.payroll_employee_detail._insurance_parents + " from " + _g._dataPayroll.payroll_employee_detail._table + " where " + _g._dataPayroll.payroll_employee_detail._code + " = " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + "),0) as insurance_parents ,");
                                        _queryEmployeeDetail.Append("coalesce((select " + _g._dataPayroll.payroll_employee_detail._insurance + " from " + _g._dataPayroll.payroll_employee_detail._table + " where " + _g._dataPayroll.payroll_employee_detail._code + " = " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + "),0) as insurance ,");
                                        _queryEmployeeDetail.Append("coalesce((select " + _g._dataPayroll.payroll_employee_detail._loan + " from " + _g._dataPayroll.payroll_employee_detail._table + " where " + _g._dataPayroll.payroll_employee_detail._code + " = " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + "),0) as loan, ");
                                        _queryEmployeeDetail.Append("coalesce((select " + _g._dataPayroll.payroll_employee_detail._donation + " from " + _g._dataPayroll.payroll_employee_detail._table + " where " + _g._dataPayroll.payroll_employee_detail._code + " = " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + "),0) as donation, ");
                                        _queryEmployeeDetail.Append("coalesce((select " + _g._dataPayroll.payroll_employee_detail._donation_education + " from " + _g._dataPayroll.payroll_employee_detail._table + " where " + _g._dataPayroll.payroll_employee_detail._code + " = " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + "),0) as donation_education, ");
                                        _queryEmployeeDetail.Append("coalesce((select " + _g._dataPayroll.payroll_employee_detail._except_labour_legislation + " from " + _g._dataPayroll.payroll_employee_detail._table + " where " + _g._dataPayroll.payroll_employee_detail._code + " = " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + "),0) as except_labour_legislation, ");
                                        _queryEmployeeDetail.Append("coalesce((select " + _g._dataPayroll.payroll_employee_detail._buy_funds_reserves + " from " + _g._dataPayroll.payroll_employee_detail._table + " where " + _g._dataPayroll.payroll_employee_detail._code + " = " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + "),0) as buy_funds_reserves, ");
                                        _queryEmployeeDetail.Append("coalesce((select " + _g._dataPayroll.payroll_employee_detail._buy_share + " from " + _g._dataPayroll.payroll_employee_detail._table + " where " + _g._dataPayroll.payroll_employee_detail._code + " = " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + "),0) as buy_share, ");
                                        _queryEmployeeDetail.Append("coalesce((select " + _g._dataPayroll.payroll_employee_detail._except_funds_reserves + " from " + _g._dataPayroll.payroll_employee_detail._table + " where " + _g._dataPayroll.payroll_employee_detail._code + " = " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + "),0) as except_funds_reserves, ");
                                        _queryEmployeeDetail.Append("coalesce((select " + _g._dataPayroll.payroll_employee_detail._except_gbk + " from " + _g._dataPayroll.payroll_employee_detail._table + " where " + _g._dataPayroll.payroll_employee_detail._code + " = " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + "),0) as except_gbk, ");
                                        _queryEmployeeDetail.Append("coalesce((select " + _g._dataPayroll.payroll_employee_detail._except_teacher + " from " + _g._dataPayroll.payroll_employee_detail._table + " where " + _g._dataPayroll.payroll_employee_detail._code + " = " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + "),0) as except_teacher, ");
                                        _queryEmployeeDetail.Append("coalesce((select " + _g._dataPayroll.payroll_employee_detail._except_65year + " from " + _g._dataPayroll.payroll_employee_detail._table + " where " + _g._dataPayroll.payroll_employee_detail._code + " = " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + "),0) as except_65year, ");
                                        _queryEmployeeDetail.Append("coalesce((select " + _g._dataPayroll.payroll_employee_detail._except_spouse_65year + " from " + _g._dataPayroll.payroll_employee_detail._table + " where " + _g._dataPayroll.payroll_employee_detail._code + " = " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + "),0) as except_spouse_65year, ");
                                        _queryEmployeeDetail.Append("coalesce((select (" + _g._dataPayroll.payroll_employee_detail._buy_funds_reserves + "+" + _g._dataPayroll.payroll_employee_detail._buy_share + "+" + _g._dataPayroll.payroll_employee_detail._except_funds_reserves + "+" + _g._dataPayroll.payroll_employee_detail._except_gbk + "+" + _g._dataPayroll.payroll_employee_detail._except_teacher + "+" + _g._dataPayroll.payroll_employee_detail._except_65year + "+" + _g._dataPayroll.payroll_employee_detail._except_spouse_65year + "+" + _g._dataPayroll.payroll_employee_detail._except_labour_legislation + ") from " + _g._dataPayroll.payroll_employee_detail._table + " where " + _g._dataPayroll.payroll_employee_detail._code + " = " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + "),0) as reduce_other,");

                                        _queryEmployeeDetail.Append("coalesce((select sum(" + _g._dataPayroll.payroll_trans_detail._diligent_money + ") from " + _g._dataPayroll.payroll_trans_detail._table + " where " + _g._dataPayroll.payroll_trans_detail._trans_flag + "=1 and " + _g._dataPayroll.payroll_trans_detail._trans_type + "=5 and " + _g._dataPayroll.payroll_trans_detail._employee + " =  " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + " " + WhereTransDetail_1 + "),0) as " + _g._dataPayroll.payroll_resource._diligent_money + ",");
                                        _queryEmployeeDetail.Append("coalesce((select sum(" + _g._dataPayroll.payroll_trans_detail._prepay_money + ") from " + _g._dataPayroll.payroll_trans_detail._table + " where " + _g._dataPayroll.payroll_trans_detail._trans_flag + "=1 and " + _g._dataPayroll.payroll_trans_detail._trans_type + "=5 and " + _g._dataPayroll.payroll_trans_detail._employee + " =  " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + " " + WhereTransDetail_1 + "),0) as " + _g._dataPayroll.payroll_resource._prepay_money + ",");
                                        _queryEmployeeDetail.Append("coalesce((select sum(" + _g._dataPayroll.payroll_trans_detail._bonus_money + ") from " + _g._dataPayroll.payroll_trans_detail._table + " where " + _g._dataPayroll.payroll_trans_detail._trans_flag + "=1 and " + _g._dataPayroll.payroll_trans_detail._trans_type + "=5 and " + _g._dataPayroll.payroll_trans_detail._employee + " =  " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + " " + WhereTransDetail_1 + "),0) as " + _g._dataPayroll.payroll_resource._bonus_money + ",");
                                        _queryEmployeeDetail.Append("coalesce((select sum(" + _g._dataPayroll.payroll_trans_detail._other_income_money + ") from " + _g._dataPayroll.payroll_trans_detail._table + " where " + _g._dataPayroll.payroll_trans_detail._trans_flag + "=1 and " + _g._dataPayroll.payroll_trans_detail._trans_type + "=5 and " + _g._dataPayroll.payroll_trans_detail._employee + " =  " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + " " + WhereTransDetail_1 + "),0) as " + _g._dataPayroll.payroll_resource._other_income_money + ",");
                                        _queryEmployeeDetail.Append("coalesce((select sum(" + _g._dataPayroll.payroll_trans_detail._advance_money + ") from " + _g._dataPayroll.payroll_trans_detail._table + " where " + _g._dataPayroll.payroll_trans_detail._trans_flag + "=1 and " + _g._dataPayroll.payroll_trans_detail._trans_type + "=5 and " + _g._dataPayroll.payroll_trans_detail._employee + " =  " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + " " + WhereTransDetail_1 + "),0) as " + _g._dataPayroll.payroll_resource._advance_money + ",");
                                        _queryEmployeeDetail.Append("coalesce((select sum(" + _g._dataPayroll.payroll_trans_detail._insure_money + ") from " + _g._dataPayroll.payroll_trans_detail._table + " where " + _g._dataPayroll.payroll_trans_detail._trans_flag + "=1 and " + _g._dataPayroll.payroll_trans_detail._trans_type + "=5 and " + _g._dataPayroll.payroll_trans_detail._employee + " =  " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + " " + WhereTransDetail_1 + "),0) as " + _g._dataPayroll.payroll_resource._insure_money + ",");
                                        _queryEmployeeDetail.Append("coalesce((select sum(" + _g._dataPayroll.payroll_trans_detail._other_payout_money + ") from " + _g._dataPayroll.payroll_trans_detail._table + " where " + _g._dataPayroll.payroll_trans_detail._trans_flag + "=1 and " + _g._dataPayroll.payroll_trans_detail._trans_type + "=5 and " + _g._dataPayroll.payroll_trans_detail._employee + " =  " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + " " + WhereTransDetail_1 + "),0) as " + _g._dataPayroll.payroll_resource._other_payout_money + ",");
                                        _queryEmployeeDetail.Append("coalesce((select sum(" + _g._dataPayroll.payroll_trans_detail._money + ") from " + _g._dataPayroll.payroll_trans_detail._table + " where " + _g._dataPayroll.payroll_trans_detail._trans_flag + "=1 and " + _g._dataPayroll.payroll_trans_detail._trans_type + "=1 and " + _g._dataPayroll.payroll_trans_detail._employee + " =  " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + " " + WhereTransDetail_1 + "),0) as " + _g._dataPayroll.payroll_trans._sow_money + ",");
                                        _queryEmployeeDetail.Append("coalesce((select sum(" + _g._dataPayroll.payroll_trans_detail._money + ") from " + _g._dataPayroll.payroll_trans_detail._table + " where " + _g._dataPayroll.payroll_trans_detail._trans_flag + "=1 and " + _g._dataPayroll.payroll_trans_detail._trans_type + "=2 and " + _g._dataPayroll.payroll_trans_detail._employee + " =  " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + " " + WhereTransDetail_1 + "),0) as " + _g._dataPayroll.payroll_trans._leave_money + ",");
                                        _queryEmployeeDetail.Append("coalesce((select sum(" + _g._dataPayroll.payroll_trans_detail._money + ") from " + _g._dataPayroll.payroll_trans_detail._table + " where " + _g._dataPayroll.payroll_trans_detail._trans_flag + "=1 and " + _g._dataPayroll.payroll_trans_detail._trans_type + "=3 and " + _g._dataPayroll.payroll_trans_detail._employee + " =  " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + " " + WhereTransDetail_1 + "),0) as " + _g._dataPayroll.payroll_trans._late_money + ",");
                                        _queryEmployeeDetail.Append("coalesce((select sum(" + _g._dataPayroll.payroll_trans_detail._money + ") from " + _g._dataPayroll.payroll_trans_detail._table + " where " + _g._dataPayroll.payroll_trans_detail._trans_flag + "=1 and " + _g._dataPayroll.payroll_trans_detail._trans_type + "=4 and " + _g._dataPayroll.payroll_trans_detail._employee + " =  " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + " " + WhereTransDetail_1 + "),0) as " + _g._dataPayroll.payroll_resource._ot_money + ",");
                                        _queryEmployeeDetail.Append("coalesce((select sum(" + _g._dataPayroll.payroll_trans._working_day + ") from " + _g._dataPayroll.payroll_trans._table + " where " + _g._dataPayroll.payroll_trans._employee + "=" + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + " " + WhereTransSub_1 + "),0) as working_day,");
                                        _queryEmployeeDetail.Append("coalesce((select sum(" + _g._dataPayroll.payroll_trans_detail._diligent_money + ") from " + _g._dataPayroll.payroll_trans_detail._table + " where " + _g._dataPayroll.payroll_trans_detail._trans_flag + "=1 and " + _g._dataPayroll.payroll_trans_detail._trans_type + "=5 and " + _g._dataPayroll.payroll_trans_detail._employee + " =  " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + " " + WhereTransDetailInMonth_1 + "),0) as diligent_money_in_month,");
                                        _queryEmployeeDetail.Append("coalesce((select sum(" + _g._dataPayroll.payroll_trans_detail._prepay_money + ") from " + _g._dataPayroll.payroll_trans_detail._table + " where " + _g._dataPayroll.payroll_trans_detail._trans_flag + "=1 and " + _g._dataPayroll.payroll_trans_detail._trans_type + "=5 and " + _g._dataPayroll.payroll_trans_detail._employee + " =  " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + " " + WhereTransDetailInMonth_1 + "),0) as prepay_money_in_month,");
                                        _queryEmployeeDetail.Append("coalesce((select sum(" + _g._dataPayroll.payroll_trans_detail._bonus_money + ") from " + _g._dataPayroll.payroll_trans_detail._table + " where " + _g._dataPayroll.payroll_trans_detail._trans_flag + "=1 and " + _g._dataPayroll.payroll_trans_detail._trans_type + "=5 and " + _g._dataPayroll.payroll_trans_detail._employee + " =  " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + " " + WhereTransDetailInMonth_1 + "),0) as bonus_money_in_month,");
                                        _queryEmployeeDetail.Append("coalesce((select sum(" + _g._dataPayroll.payroll_trans_detail._other_income_money + ") from " + _g._dataPayroll.payroll_trans_detail._table + " where " + _g._dataPayroll.payroll_trans_detail._trans_flag + "=1 and " + _g._dataPayroll.payroll_trans_detail._trans_type + "=5 and " + _g._dataPayroll.payroll_trans_detail._employee + " =  " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + " " + WhereTransDetailInMonth_1 + "),0) as other_income_money_in_month,");
                                        _queryEmployeeDetail.Append("coalesce((select sum(" + _g._dataPayroll.payroll_trans_detail._advance_money + ") from " + _g._dataPayroll.payroll_trans_detail._table + " where " + _g._dataPayroll.payroll_trans_detail._trans_flag + "=1 and " + _g._dataPayroll.payroll_trans_detail._trans_type + "=5 and " + _g._dataPayroll.payroll_trans_detail._employee + " =  " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + " " + WhereTransDetailInMonth_1 + "),0) as advance_money_in_month,");
                                        _queryEmployeeDetail.Append("coalesce((select sum(" + _g._dataPayroll.payroll_trans_detail._insure_money + ") from " + _g._dataPayroll.payroll_trans_detail._table + " where " + _g._dataPayroll.payroll_trans_detail._trans_flag + "=1 and " + _g._dataPayroll.payroll_trans_detail._trans_type + "=5 and " + _g._dataPayroll.payroll_trans_detail._employee + " =  " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + " " + WhereTransDetailInMonth_1 + "),0) as insure_money_in_month,");
                                        _queryEmployeeDetail.Append("coalesce((select sum(" + _g._dataPayroll.payroll_trans_detail._other_payout_money + ") from " + _g._dataPayroll.payroll_trans_detail._table + " where " + _g._dataPayroll.payroll_trans_detail._trans_flag + "=1 and " + _g._dataPayroll.payroll_trans_detail._trans_type + "=5 and " + _g._dataPayroll.payroll_trans_detail._employee + " =  " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + " " + WhereTransDetailInMonth_1 + "),0) as other_payout_money_in_month,");
                                        _queryEmployeeDetail.Append("coalesce((select sum(" + _g._dataPayroll.payroll_trans_detail._money + ") from " + _g._dataPayroll.payroll_trans_detail._table + " where " + _g._dataPayroll.payroll_trans_detail._trans_flag + "=1 and " + _g._dataPayroll.payroll_trans_detail._trans_type + "=1 and " + _g._dataPayroll.payroll_trans_detail._employee + " =  " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + " " + WhereTransDetailInMonth_1 + "),0) as sow_money_in_month,");
                                        _queryEmployeeDetail.Append("coalesce((select sum(" + _g._dataPayroll.payroll_trans_detail._money + ") from " + _g._dataPayroll.payroll_trans_detail._table + " where " + _g._dataPayroll.payroll_trans_detail._trans_flag + "=1 and " + _g._dataPayroll.payroll_trans_detail._trans_type + "=2 and " + _g._dataPayroll.payroll_trans_detail._employee + " =  " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + " " + WhereTransDetailInMonth_1 + "),0) as leave_money_in_month,");
                                        _queryEmployeeDetail.Append("coalesce((select sum(" + _g._dataPayroll.payroll_trans_detail._money + ") from " + _g._dataPayroll.payroll_trans_detail._table + " where " + _g._dataPayroll.payroll_trans_detail._trans_flag + "=1 and " + _g._dataPayroll.payroll_trans_detail._trans_type + "=3 and " + _g._dataPayroll.payroll_trans_detail._employee + " =  " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + " " + WhereTransDetailInMonth_1 + "),0) as late_money_in_month,");
                                        _queryEmployeeDetail.Append("coalesce((select sum(" + _g._dataPayroll.payroll_trans_detail._money + ") from " + _g._dataPayroll.payroll_trans_detail._table + " where " + _g._dataPayroll.payroll_trans_detail._trans_flag + "=1 and " + _g._dataPayroll.payroll_trans_detail._trans_type + "=4 and " + _g._dataPayroll.payroll_trans_detail._employee + " =  " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + " " + WhereTransDetailInMonth_1 + "),0) as ot_money_in_month,");
                                        _queryEmployeeDetail.Append("coalesce((CASE " + _g._dataPayroll.payroll_employee._employee_type);
                                        _queryEmployeeDetail.Append(" WHEN 0 THEN ");
                                        _queryEmployeeDetail.Append("((select coalesce((" + _g._dataPayroll.payroll_employee_salary._salary + "),0) as " + _g._dataPayroll.payroll_employee_salary._salary);
                                        _queryEmployeeDetail.Append(" from " + _g._dataPayroll.payroll_employee_salary._table);
                                        _queryEmployeeDetail.Append(" where " + _g._dataPayroll.payroll_employee_salary._code + " = " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + " ");
                                        _queryEmployeeDetail.Append(" and " + _g._dataPayroll.payroll_employee_salary._start_date + " <= '" + StartDay_1 + "'");
                                        _queryEmployeeDetail.Append(" and (" + _g._dataPayroll.payroll_employee_salary._end_date + " >= '" + EndDay_1 + "' or " + _g._dataPayroll.payroll_employee_salary._end_date + " = '1000-01-01'))  * ((select " + _g._dataPayroll.payroll_company_config._customer_rf_rate + " from " + _g._dataPayroll.payroll_company_config._table + " ) / 100))");
                                        _queryEmployeeDetail.Append(" WHEN 1 THEN ");
                                        _queryEmployeeDetail.Append("((");
                                        _queryEmployeeDetail.Append("select coalesce((" + _g._dataPayroll.payroll_employee_salary._salary + "),0) as " + _g._dataPayroll.payroll_employee_salary._salary);
                                        _queryEmployeeDetail.Append(" from " + _g._dataPayroll.payroll_employee_salary._table);
                                        _queryEmployeeDetail.Append(" where " + _g._dataPayroll.payroll_employee_salary._code + " = " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + "");
                                        _queryEmployeeDetail.Append(" and " + _g._dataPayroll.payroll_employee_salary._start_date + " <= '" + StartDay_1 + "'");
                                        _queryEmployeeDetail.Append(" and (" + _g._dataPayroll.payroll_employee_salary._end_date + " >= '" + EndDay_1 + "' or " + _g._dataPayroll.payroll_employee_salary._end_date + " = '1000-01-01')");
                                        _queryEmployeeDetail.Append(") * 4) * ((select " + _g._dataPayroll.payroll_company_config._customer_rf_rate + " from " + _g._dataPayroll.payroll_company_config._table + " ) / 100)");
                                        _queryEmployeeDetail.Append(" ELSE ");
                                        _queryEmployeeDetail.Append("((");
                                        _queryEmployeeDetail.Append("select coalesce((" + _g._dataPayroll.payroll_employee_salary._salary + "),0) as " + _g._dataPayroll.payroll_employee_salary._salary);
                                        _queryEmployeeDetail.Append(" from " + _g._dataPayroll.payroll_employee_salary._table);
                                        _queryEmployeeDetail.Append(" where " + _g._dataPayroll.payroll_employee_salary._code + " = " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + " ");
                                        _queryEmployeeDetail.Append(" and " + _g._dataPayroll.payroll_employee_salary._start_date + " <= '" + StartDay_1 + "'");
                                        _queryEmployeeDetail.Append(" and (" + _g._dataPayroll.payroll_employee_salary._end_date + " >= '" + EndDay_1 + "' or " + _g._dataPayroll.payroll_employee_salary._end_date + " = '1000-01-01')");
                                        _queryEmployeeDetail.Append(") * (select sum(" + _g._dataPayroll.payroll_trans._working_day + ") from " + _g._dataPayroll.payroll_trans._table + " where " + _g._dataPayroll.payroll_trans._employee + "=" + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + " " + WhereTransSubInMonth_1 + ")) * ((select " + _g._dataPayroll.payroll_company_config._customer_rf_rate + " from " + _g._dataPayroll.payroll_company_config._table + " ) / 100)");
                                        _queryEmployeeDetail.Append(" END");
                                        _queryEmployeeDetail.Append(" ),0) as reserve_money_in_month_vary,");
                                        _queryEmployeeDetail.Append(" coalesce(( CASE " + _g._dataPayroll.payroll_employee._employee_type);
                                        _queryEmployeeDetail.Append(" WHEN 0 THEN (" + _g._dataPayroll.payroll_employee._salary + " * ((select " + _g._dataPayroll.payroll_company_config._customer_rf_rate + " from " + _g._dataPayroll.payroll_company_config._table + " ) / 100))");
                                        _queryEmployeeDetail.Append(" WHEN 1 THEN ((" + _g._dataPayroll.payroll_employee._salary + " * 4) * ((select " + _g._dataPayroll.payroll_company_config._customer_rf_rate + " from " + _g._dataPayroll.payroll_company_config._table + " ) / 100))");
                                        _queryEmployeeDetail.Append(" ELSE ((" + _g._dataPayroll.payroll_employee._salary + " * (select sum(" + _g._dataPayroll.payroll_trans._working_day + ") from " + _g._dataPayroll.payroll_trans._table + " where " + _g._dataPayroll.payroll_trans._employee + "=" + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + " " + WhereTransSubInMonth_1 + ")) * ((select " + _g._dataPayroll.payroll_company_config._customer_rf_rate + " from " + _g._dataPayroll.payroll_company_config._table + " ) / 100))");
                                        _queryEmployeeDetail.Append(" END");
                                        _queryEmployeeDetail.Append("),0) as reserve_money_in_month,");
                                        _queryEmployeeDetail.Append("coalesce(( CASE " + _g._dataPayroll.payroll_employee._employee_type);
                                        _queryEmployeeDetail.Append(" WHEN 0 THEN ");
                                        _queryEmployeeDetail.Append("((select coalesce((" + _g._dataPayroll.payroll_employee_salary._salary + "),0) as " + _g._dataPayroll.payroll_employee_salary._salary);
                                        _queryEmployeeDetail.Append(" from " + _g._dataPayroll.payroll_employee_salary._table);
                                        _queryEmployeeDetail.Append(" where " + _g._dataPayroll.payroll_employee_salary._code + " = " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + "");
                                        _queryEmployeeDetail.Append(" and " + _g._dataPayroll.payroll_employee_salary._start_date + " <= '" + StartDay_1 + "'");
                                        _queryEmployeeDetail.Append(" and (" + _g._dataPayroll.payroll_employee_salary._end_date + " >= '" + EndDay_1 + "' or " + _g._dataPayroll.payroll_employee_salary._end_date + " = '1000-01-01'))  * ((select " + _g._dataPayroll.payroll_company_config._customer_rf_rate + " from " + _g._dataPayroll.payroll_company_config._table + " ) / 100))");
                                        _queryEmployeeDetail.Append(" WHEN 1 THEN ");
                                        _queryEmployeeDetail.Append("((");
                                        _queryEmployeeDetail.Append("select coalesce((" + _g._dataPayroll.payroll_employee_salary._salary + "),0) as " + _g._dataPayroll.payroll_employee_salary._salary);
                                        _queryEmployeeDetail.Append(" from " + _g._dataPayroll.payroll_employee_salary._table);
                                        _queryEmployeeDetail.Append(" where " + _g._dataPayroll.payroll_employee_salary._code + " = " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + "");
                                        _queryEmployeeDetail.Append(" and " + _g._dataPayroll.payroll_employee_salary._start_date + " <= '" + StartDay_1 + "'");
                                        _queryEmployeeDetail.Append(" and (" + _g._dataPayroll.payroll_employee_salary._end_date + " >= '" + EndDay_1 + "' or " + _g._dataPayroll.payroll_employee_salary._end_date + " = '1000-01-01')");
                                        _queryEmployeeDetail.Append(") * 4) * ((select " + _g._dataPayroll.payroll_company_config._customer_rf_rate + " from " + _g._dataPayroll.payroll_company_config._table + " ) / 100)");
                                        _queryEmployeeDetail.Append(" ELSE ");
                                        _queryEmployeeDetail.Append("((");
                                        _queryEmployeeDetail.Append("select coalesce((" + _g._dataPayroll.payroll_employee_salary._salary + "),0) as " + _g._dataPayroll.payroll_employee_salary._salary);
                                        _queryEmployeeDetail.Append(" from " + _g._dataPayroll.payroll_employee_salary._table);
                                        _queryEmployeeDetail.Append(" where " + _g._dataPayroll.payroll_employee_salary._code + " = " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + "");
                                        _queryEmployeeDetail.Append(" and " + _g._dataPayroll.payroll_employee_salary._start_date + " <= '" + StartDay_1 + "'");
                                        _queryEmployeeDetail.Append(" and (" + _g._dataPayroll.payroll_employee_salary._end_date + " >= '" + EndDay_1 + "' or " + _g._dataPayroll.payroll_employee_salary._end_date + " = '1000-01-01')");
                                        _queryEmployeeDetail.Append(") * (select sum(" + _g._dataPayroll.payroll_trans._working_day + ") from " + _g._dataPayroll.payroll_trans._table + " where " + _g._dataPayroll.payroll_trans._employee + "=" + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + " " + WhereTransSub_1 + ")) * ((select " + _g._dataPayroll.payroll_company_config._customer_rf_rate + " from " + _g._dataPayroll.payroll_company_config._table + " ) / 100)");
                                        _queryEmployeeDetail.Append(" END");
                                        _queryEmployeeDetail.Append(" ),0) as reserve_money_vary,");
                                        _queryEmployeeDetail.Append(" coalesce(( CASE " + _g._dataPayroll.payroll_employee._employee_type);
                                        _queryEmployeeDetail.Append(" WHEN 0 THEN (" + _g._dataPayroll.payroll_employee._salary + " * ((select " + _g._dataPayroll.payroll_company_config._customer_rf_rate + " from " + _g._dataPayroll.payroll_company_config._table + " ) / 100))");
                                        _queryEmployeeDetail.Append(" WHEN 1 THEN ((" + _g._dataPayroll.payroll_employee._salary + " * 4 ) * ((select " + _g._dataPayroll.payroll_company_config._customer_rf_rate + " from " + _g._dataPayroll.payroll_company_config._table + " ) / 100))");
                                        _queryEmployeeDetail.Append(" ELSE ((" + _g._dataPayroll.payroll_employee._salary + " * (select sum(" + _g._dataPayroll.payroll_trans._working_day + ") from " + _g._dataPayroll.payroll_trans._table + " where " + _g._dataPayroll.payroll_trans._employee + "=" + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + " " + WhereTransSub_1 + ")) * ((select " + _g._dataPayroll.payroll_company_config._customer_rf_rate + " from " + _g._dataPayroll.payroll_company_config._table + " ) / 100))");
                                        _queryEmployeeDetail.Append(" END");
                                        _queryEmployeeDetail.Append(" ),0) as " + _g._dataPayroll.payroll_trans._reserve_money + ",");
                                        _queryEmployeeDetail.Append(" coalesce((CASE " + _g._dataPayroll.payroll_employee._employee_type);
                                        _queryEmployeeDetail.Append(" WHEN 0 THEN ");
                                        _queryEmployeeDetail.Append("((select coalesce((" + _g._dataPayroll.payroll_employee_salary._salary + "),0) as " + _g._dataPayroll.payroll_employee_salary._salary);
                                        _queryEmployeeDetail.Append(" from " + _g._dataPayroll.payroll_employee_salary._table);
                                        _queryEmployeeDetail.Append(" where " + _g._dataPayroll.payroll_employee_salary._code + " = " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + "");
                                        _queryEmployeeDetail.Append(" and " + _g._dataPayroll.payroll_employee_salary._start_date + " <= '" + StartDay_1 + "'");
                                        _queryEmployeeDetail.Append(" and (" + _g._dataPayroll.payroll_employee_salary._end_date + " >= '" + EndDay_1 + "' or " + _g._dataPayroll.payroll_employee_salary._end_date + " = '1000-01-01'))  * ((select " + _g._dataPayroll.payroll_company_config._customer_ss_rate + " from " + _g._dataPayroll.payroll_company_config._table + " ) / 100))");
                                        _queryEmployeeDetail.Append(" WHEN 1 THEN ");
                                        _queryEmployeeDetail.Append("((");
                                        _queryEmployeeDetail.Append("select coalesce((" + _g._dataPayroll.payroll_employee_salary._salary + "),0) as " + _g._dataPayroll.payroll_employee_salary._salary);
                                        _queryEmployeeDetail.Append(" from " + _g._dataPayroll.payroll_employee_salary._table);
                                        _queryEmployeeDetail.Append(" where " + _g._dataPayroll.payroll_employee_salary._code + " = " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + "");
                                        _queryEmployeeDetail.Append(" and " + _g._dataPayroll.payroll_employee_salary._start_date + " <= '" + StartDay_1 + "'");
                                        _queryEmployeeDetail.Append(" and (" + _g._dataPayroll.payroll_employee_salary._end_date + " >= '" + EndDay_1 + "' or " + _g._dataPayroll.payroll_employee_salary._end_date + " = '1000-01-01')");
                                        _queryEmployeeDetail.Append(") * 4) * ((select " + _g._dataPayroll.payroll_company_config._customer_ss_rate + " from " + _g._dataPayroll.payroll_company_config._table + " ) / 100)");
                                        _queryEmployeeDetail.Append(" ELSE ");
                                        _queryEmployeeDetail.Append("((");
                                        _queryEmployeeDetail.Append("select coalesce((" + _g._dataPayroll.payroll_employee_salary._salary + "),0) as " + _g._dataPayroll.payroll_employee_salary._salary);
                                        _queryEmployeeDetail.Append(" from " + _g._dataPayroll.payroll_employee_salary._table);
                                        _queryEmployeeDetail.Append(" where " + _g._dataPayroll.payroll_employee_salary._code + " = " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + "");
                                        _queryEmployeeDetail.Append(" and " + _g._dataPayroll.payroll_employee_salary._start_date + " <= '" + StartDay_1 + "'");
                                        _queryEmployeeDetail.Append(" and (" + _g._dataPayroll.payroll_employee_salary._end_date + " >= '" + EndDay_1 + "' or " + _g._dataPayroll.payroll_employee_salary._end_date + " = '1000-01-01')");
                                        _queryEmployeeDetail.Append(") * (select sum(" + _g._dataPayroll.payroll_trans._working_day + ") from " + _g._dataPayroll.payroll_trans._table + " where " + _g._dataPayroll.payroll_trans._employee + "=" + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + " " + WhereTransSubInMonth_1 + ")) * ((select " + _g._dataPayroll.payroll_company_config._customer_ss_rate + " from " + _g._dataPayroll.payroll_company_config._table + " ) / 100)");
                                        _queryEmployeeDetail.Append(" END");
                                        _queryEmployeeDetail.Append("),0) as society_insure_money_in_month_vary,");
                                        _queryEmployeeDetail.Append(" coalesce((CASE " + _g._dataPayroll.payroll_employee._employee_type);
                                        _queryEmployeeDetail.Append(" WHEN 0 THEN (" + _g._dataPayroll.payroll_employee._salary + " * ((select " + _g._dataPayroll.payroll_company_config._customer_ss_rate + " from " + _g._dataPayroll.payroll_company_config._table + " ) / 100))");
                                        _queryEmployeeDetail.Append(" WHEN 1 THEN ((" + _g._dataPayroll.payroll_employee._salary + " * 4 ) * ((select " + _g._dataPayroll.payroll_company_config._customer_ss_rate + " from " + _g._dataPayroll.payroll_company_config._table + " ) / 100))");
                                        _queryEmployeeDetail.Append(" ELSE ((" + _g._dataPayroll.payroll_employee._salary + " * (select sum(" + _g._dataPayroll.payroll_trans._working_day + ") from " + _g._dataPayroll.payroll_trans._table + " where " + _g._dataPayroll.payroll_trans._employee + "=" + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + " " + WhereTransSubInMonth_1 + ")) * ((select " + _g._dataPayroll.payroll_company_config._customer_ss_rate + " from " + _g._dataPayroll.payroll_company_config._table + " ) / 100))");
                                        _queryEmployeeDetail.Append(" END");
                                        _queryEmployeeDetail.Append(" ),0) as society_insure_money_in_month,");
                                        _queryEmployeeDetail.Append("coalesce(( CASE " + _g._dataPayroll.payroll_employee._employee_type);
                                        _queryEmployeeDetail.Append(" WHEN 0 THEN ");
                                        _queryEmployeeDetail.Append("((select coalesce((" + _g._dataPayroll.payroll_employee_salary._salary + "),0) as " + _g._dataPayroll.payroll_employee_salary._salary);
                                        _queryEmployeeDetail.Append(" from " + _g._dataPayroll.payroll_employee_salary._table);
                                        _queryEmployeeDetail.Append(" where " + _g._dataPayroll.payroll_employee_salary._code + " = " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + "");
                                        _queryEmployeeDetail.Append(" and " + _g._dataPayroll.payroll_employee_salary._start_date + " <= '" + StartDay_1 + "'");
                                        _queryEmployeeDetail.Append(" and (" + _g._dataPayroll.payroll_employee_salary._end_date + " >= '" + EndDay_1 + "' or " + _g._dataPayroll.payroll_employee_salary._end_date + " = '1000-01-01'))  * ((select " + _g._dataPayroll.payroll_company_config._customer_ss_rate + " from " + _g._dataPayroll.payroll_company_config._table + " ) / 100))");
                                        _queryEmployeeDetail.Append(" WHEN 1 THEN ");
                                        _queryEmployeeDetail.Append("((");
                                        _queryEmployeeDetail.Append("select coalesce((" + _g._dataPayroll.payroll_employee_salary._salary + "),0) as " + _g._dataPayroll.payroll_employee_salary._salary);
                                        _queryEmployeeDetail.Append(" from " + _g._dataPayroll.payroll_employee_salary._table);
                                        _queryEmployeeDetail.Append(" where " + _g._dataPayroll.payroll_employee_salary._code + " = " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + "");
                                        _queryEmployeeDetail.Append(" and " + _g._dataPayroll.payroll_employee_salary._start_date + " <= '" + StartDay_1 + "'");
                                        _queryEmployeeDetail.Append(" and (" + _g._dataPayroll.payroll_employee_salary._end_date + " >= '" + EndDay_1 + "' or " + _g._dataPayroll.payroll_employee_salary._end_date + " = '1000-01-01')");
                                        _queryEmployeeDetail.Append(") * 4) * ((select " + _g._dataPayroll.payroll_company_config._customer_ss_rate + " from " + _g._dataPayroll.payroll_company_config._table + " ) / 100)");
                                        _queryEmployeeDetail.Append(" ELSE ");
                                        _queryEmployeeDetail.Append("((");
                                        _queryEmployeeDetail.Append("select coalesce((" + _g._dataPayroll.payroll_employee_salary._salary + "),0) as " + _g._dataPayroll.payroll_employee_salary._salary);
                                        _queryEmployeeDetail.Append(" from " + _g._dataPayroll.payroll_employee_salary._table);
                                        _queryEmployeeDetail.Append(" where " + _g._dataPayroll.payroll_employee_salary._code + " = " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + "");
                                        _queryEmployeeDetail.Append(" and " + _g._dataPayroll.payroll_employee_salary._start_date + " <= '" + StartDay_1 + "'");
                                        _queryEmployeeDetail.Append(" and (" + _g._dataPayroll.payroll_employee_salary._end_date + " >= '" + EndDay_1 + "' or " + _g._dataPayroll.payroll_employee_salary._end_date + " = '1000-01-01')");
                                        _queryEmployeeDetail.Append(") * (select sum(" + _g._dataPayroll.payroll_trans._working_day + ") from " + _g._dataPayroll.payroll_trans._table + " where " + _g._dataPayroll.payroll_trans._employee + "=" + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + " " + WhereTransSub_1 + ")) * ((select " + _g._dataPayroll.payroll_company_config._customer_ss_rate + " from " + _g._dataPayroll.payroll_company_config._table + " ) / 100)");
                                        _queryEmployeeDetail.Append(" END");
                                        _queryEmployeeDetail.Append("),0) as society_insure_money_vary,");
                                        _queryEmployeeDetail.Append(" coalesce(( CASE " + _g._dataPayroll.payroll_employee._employee_type);
                                        _queryEmployeeDetail.Append(" WHEN 0 THEN (" + _g._dataPayroll.payroll_employee._salary + " * ((select " + _g._dataPayroll.payroll_company_config._customer_ss_rate + " from " + _g._dataPayroll.payroll_company_config._table + " ) / 100))");
                                        _queryEmployeeDetail.Append(" WHEN 1 THEN ((" + _g._dataPayroll.payroll_employee._salary + " * 4 ) * ((select " + _g._dataPayroll.payroll_company_config._customer_ss_rate + " from " + _g._dataPayroll.payroll_company_config._table + " ) / 100))");
                                        _queryEmployeeDetail.Append(" ELSE ((" + _g._dataPayroll.payroll_employee._salary + " * (select sum(" + _g._dataPayroll.payroll_trans._working_day + ") from " + _g._dataPayroll.payroll_trans._table + " where " + _g._dataPayroll.payroll_trans._employee + "=" + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + " " + WhereTransSub_1 + ")) * ((select " + _g._dataPayroll.payroll_company_config._customer_ss_rate + " from " + _g._dataPayroll.payroll_company_config._table + " ) / 100))");
                                        _queryEmployeeDetail.Append(" END");
                                        _queryEmployeeDetail.Append(" ),0) as " + _g._dataPayroll.payroll_trans._society_insure_money + ",");
                                        _queryEmployeeDetail.Append(" coalesce((CASE " + _g._dataPayroll.payroll_employee._employee_type);
                                        _queryEmployeeDetail.Append(" WHEN 0 THEN ");
                                        _queryEmployeeDetail.Append("(select coalesce((" + _g._dataPayroll.payroll_employee_salary._salary + "),0) as " + _g._dataPayroll.payroll_employee_salary._salary);
                                        _queryEmployeeDetail.Append(" from " + _g._dataPayroll.payroll_employee_salary._table);
                                        _queryEmployeeDetail.Append(" where " + _g._dataPayroll.payroll_employee_salary._code + " = " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + "");
                                        _queryEmployeeDetail.Append(" and " + _g._dataPayroll.payroll_employee_salary._start_date + " <= '" + StartDay_1 + "'");
                                        _queryEmployeeDetail.Append(" and (" + _g._dataPayroll.payroll_employee_salary._end_date + " >= '" + EndDay_1 + "' or " + _g._dataPayroll.payroll_employee_salary._end_date + " = '1000-01-01'))");
                                        _queryEmployeeDetail.Append(" WHEN 1 THEN ");
                                        _queryEmployeeDetail.Append("((");
                                        _queryEmployeeDetail.Append("select coalesce((" + _g._dataPayroll.payroll_employee_salary._salary + "),0) as " + _g._dataPayroll.payroll_employee_salary._salary);
                                        _queryEmployeeDetail.Append(" from " + _g._dataPayroll.payroll_employee_salary._table);
                                        _queryEmployeeDetail.Append(" where " + _g._dataPayroll.payroll_employee_salary._code + " = " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + "");
                                        _queryEmployeeDetail.Append(" and " + _g._dataPayroll.payroll_employee_salary._start_date + " <= '" + StartDay_1 + "'");
                                        _queryEmployeeDetail.Append(" and (" + _g._dataPayroll.payroll_employee_salary._end_date + " >= '" + EndDay_1 + "' or " + _g._dataPayroll.payroll_employee_salary._end_date + " = '1000-01-01')");
                                        _queryEmployeeDetail.Append(") * 4)");
                                        _queryEmployeeDetail.Append(" ELSE ");
                                        _queryEmployeeDetail.Append("((");
                                        _queryEmployeeDetail.Append("select coalesce((" + _g._dataPayroll.payroll_employee_salary._salary + "),0) as " + _g._dataPayroll.payroll_employee_salary._salary);
                                        _queryEmployeeDetail.Append(" from " + _g._dataPayroll.payroll_employee_salary._table);
                                        _queryEmployeeDetail.Append(" where " + _g._dataPayroll.payroll_employee_salary._code + " = " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + "");
                                        _queryEmployeeDetail.Append(" and " + _g._dataPayroll.payroll_employee_salary._start_date + " <= '" + StartDay_1 + "'");
                                        _queryEmployeeDetail.Append(" and (" + _g._dataPayroll.payroll_employee_salary._end_date + " >= '" + EndDay_1 + "' or " + _g._dataPayroll.payroll_employee_salary._end_date + " = '1000-01-01')");
                                        _queryEmployeeDetail.Append(") * (select sum(" + _g._dataPayroll.payroll_trans._working_day + ") from " + _g._dataPayroll.payroll_trans._table + " where " + _g._dataPayroll.payroll_trans._employee + "=" + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + " " + WhereTransSubInMonth_1 + "))");
                                        _queryEmployeeDetail.Append(" END ");
                                        _queryEmployeeDetail.Append(" ),0) as wages_money_in_month_vary,");
                                        _queryEmployeeDetail.Append(" coalesce(( CASE " + _g._dataPayroll.payroll_employee._employee_type);
                                        _queryEmployeeDetail.Append(" WHEN 0 THEN " + _g._dataPayroll.payroll_employee._salary);
                                        _queryEmployeeDetail.Append(" WHEN 1 THEN (" + _g._dataPayroll.payroll_employee._salary + " * 4)");
                                        _queryEmployeeDetail.Append(" ELSE (" + _g._dataPayroll.payroll_employee._salary + "*(select sum(" + _g._dataPayroll.payroll_trans._working_day + ") from " + _g._dataPayroll.payroll_trans._table + " where " + _g._dataPayroll.payroll_trans._employee + "=" + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + " " + WhereTransSubInMonth_1 + "))");
                                        _queryEmployeeDetail.Append(" END");
                                        _queryEmployeeDetail.Append(" ),0) as wages_money_in_month,");
                                        _queryEmployeeDetail.Append(" coalesce((CASE " + _g._dataPayroll.payroll_employee._employee_type);
                                        _queryEmployeeDetail.Append(" WHEN 0 THEN ");
                                        _queryEmployeeDetail.Append("(select coalesce((" + _g._dataPayroll.payroll_employee_salary._salary + "),0) as " + _g._dataPayroll.payroll_employee_salary._salary);
                                        _queryEmployeeDetail.Append(" from " + _g._dataPayroll.payroll_employee_salary._table);
                                        _queryEmployeeDetail.Append(" where " + _g._dataPayroll.payroll_employee_salary._code + " = " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + "");
                                        _queryEmployeeDetail.Append(" and " + _g._dataPayroll.payroll_employee_salary._start_date + " <= '" + StartDay_1 + "'");
                                        _queryEmployeeDetail.Append(" and (" + _g._dataPayroll.payroll_employee_salary._end_date + " >= '" + EndDay_1 + "' or " + _g._dataPayroll.payroll_employee_salary._end_date + " = '1000-01-01'))");
                                        _queryEmployeeDetail.Append(" WHEN 1 THEN ");
                                        _queryEmployeeDetail.Append("((");
                                        _queryEmployeeDetail.Append("select coalesce((" + _g._dataPayroll.payroll_employee_salary._salary + "),0) as " + _g._dataPayroll.payroll_employee_salary._salary);
                                        _queryEmployeeDetail.Append(" from " + _g._dataPayroll.payroll_employee_salary._table);
                                        _queryEmployeeDetail.Append(" where " + _g._dataPayroll.payroll_employee_salary._code + " = " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + "");
                                        _queryEmployeeDetail.Append(" and " + _g._dataPayroll.payroll_employee_salary._start_date + " <= '" + StartDay_1 + "'");
                                        _queryEmployeeDetail.Append(" and (" + _g._dataPayroll.payroll_employee_salary._end_date + " >= '" + EndDay_1 + "' or " + _g._dataPayroll.payroll_employee_salary._end_date + " = '1000-01-01')");
                                        _queryEmployeeDetail.Append(") * 4)");
                                        _queryEmployeeDetail.Append(" ELSE ");
                                        _queryEmployeeDetail.Append("((");
                                        _queryEmployeeDetail.Append("select coalesce((" + _g._dataPayroll.payroll_employee_salary._salary + "),0) as " + _g._dataPayroll.payroll_employee_salary._salary);
                                        _queryEmployeeDetail.Append(" from " + _g._dataPayroll.payroll_employee_salary._table);
                                        _queryEmployeeDetail.Append(" where " + _g._dataPayroll.payroll_employee_salary._code + " = " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + "");
                                        _queryEmployeeDetail.Append(" and " + _g._dataPayroll.payroll_employee_salary._start_date + " <= '" + StartDay_1 + "'");
                                        _queryEmployeeDetail.Append(" and (" + _g._dataPayroll.payroll_employee_salary._end_date + " >= '" + EndDay_1 + "' or " + _g._dataPayroll.payroll_employee_salary._end_date + " = '1000-01-01')");
                                        _queryEmployeeDetail.Append(") * (select sum(" + _g._dataPayroll.payroll_trans._working_day + ") from " + _g._dataPayroll.payroll_trans._table + " where " + _g._dataPayroll.payroll_trans._employee + "=" + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + " " + WhereTransSub_1 + "))");
                                        _queryEmployeeDetail.Append(" END");
                                        _queryEmployeeDetail.Append(" ),0) as wages_money_vary,");
                                        _queryEmployeeDetail.Append(" coalesce((CASE " + _g._dataPayroll.payroll_employee._employee_type);
                                        _queryEmployeeDetail.Append(" WHEN 0 THEN " + _g._dataPayroll.payroll_employee._salary);
                                        _queryEmployeeDetail.Append(" WHEN 1 THEN (" + _g._dataPayroll.payroll_employee._salary + " * 4)");
                                        _queryEmployeeDetail.Append(" ELSE (" + _g._dataPayroll.payroll_employee._salary + "*(select sum(" + _g._dataPayroll.payroll_trans._working_day + ") from " + _g._dataPayroll.payroll_trans._table + " where " + _g._dataPayroll.payroll_trans._employee + "=" + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + " " + WhereTransSub_1 + "))");
                                        _queryEmployeeDetail.Append(" END");
                                        _queryEmployeeDetail.Append(" ),0) as " + _g._dataPayroll.payroll_trans._wages_money + ",");


                                        _queryEmployeeDetail.Append(" employee_type,");
                                        _queryEmployeeDetail.Append("code,");
                                        _queryEmployeeDetail.Append(" name");

                                        _queryEmployeeDetail.Append(" from " + _g._dataPayroll.payroll_employee._table);
                                        _queryEmployeeDetail.Append(" where " + _g._dataPayroll.payroll_employee._employee_type + " = " + EmployeeType + WhereEmployee + "");
                                        _queryEmployeeDetail.Append(" and to_char(" + _g._dataPayroll.payroll_employee._start_work + ",'MM') <= " + (i + 1) + "");
                                        _queryEmployeeDetail.Append(" and to_char(" + _g._dataPayroll.payroll_employee._start_work + ",'yyyy') <= '" + (MyLib._myGlobal._intPhase(InYear) - 543).ToString() + "'");
                                        _queryEmployeeDetail.Append(" order by code)");
                                        if (i < 10)
                                        {
                                            _queryEmployeeDetail.Append(" union all ");
                                        }
                                    }                                    

                                    _queryEmployeeDetail.Append(") as temp1 order by code");
                                   
                                   _myQuery.Append(_queryEmployeeDetail.ToString());                       
                        break;
                    case _g.g._screenPayrollEnum.รายงานภาษีเงินได้_รายปี:
                    case _g.g._screenPayrollEnum.คำนวนภาษีรายปี:
                    case _g.g._screenPayrollEnum.ภงด_91:
                    case _g.g._screenPayrollEnum.ภงด_1_ก_ใบแนบ:
                    case _g.g._screenPayrollEnum.ภงด_1_ก:
                        //WhereTransDetail = "";
                        //WhereTransSub = " and (" + _g._dataPayroll.payroll_trans._select_year + " = '" + InYear + "') and (" + _g._dataPayroll.payroll_trans._select_month + " = " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._select_month + ")";
                        //WhereTransDetail = " and (" + _g._dataPayroll.payroll_trans_detail._select_year + " = '" + InYear + "') and (" + _g._dataPayroll.payroll_trans_detail._select_month + " = " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._select_month + ")";
                        //เงินหัก-เงินได้
                        FileFormonth.Append("(0) as " + _g._dataPayroll.payroll_resource._net_wages + ",");
                        FileFormonth.Append(" " + _g._dataPayroll.payroll_trans._sow_money + " as " + _g._dataPayroll.payroll_resource._sow_money + ",");
                        FileFormonth.Append(" " + _g._dataPayroll.payroll_trans._leave_money + " as " + _g._dataPayroll.payroll_resource._leave_money + ",");
                        FileFormonth.Append(" " + _g._dataPayroll.payroll_trans._late_money + " as " + _g._dataPayroll.payroll_resource._late_money + ",");
                        FileFormonth.Append(" " + _g._dataPayroll.payroll_trans._ot_money + " as " + _g._dataPayroll.payroll_resource._ot_money + ",");
                        FileFormonth.Append(" " + _g._dataPayroll.payroll_trans._diligent_money + " as " + _g._dataPayroll.payroll_resource._diligent_money + ",");
                        FileFormonth.Append(" " + _g._dataPayroll.payroll_trans._prepay_money + " as " + _g._dataPayroll.payroll_resource._prepay_money + ",");
                        FileFormonth.Append(" " + _g._dataPayroll.payroll_trans._bonus_money + " as " + _g._dataPayroll.payroll_resource._bonus_money + ",");
                        FileFormonth.Append(" " + _g._dataPayroll.payroll_trans._other_income_money + " as " + _g._dataPayroll.payroll_resource._other_income_money + ",");
                        FileFormonth.Append(" " + _g._dataPayroll.payroll_trans._advance_money + " as " + _g._dataPayroll.payroll_resource._advance_money + ",");
                        FileFormonth.Append(" " + _g._dataPayroll.payroll_trans._insure_money + " as " + _g._dataPayroll.payroll_resource._insure_money + ",");
                        FileFormonth.Append(" " + _g._dataPayroll.payroll_trans._other_payout_money + " as " + _g._dataPayroll.payroll_resource._other_payout_money + ",");
                        //เงินกองทุน
                        FileFormonth.Append(" coalesce((");
                        FileFormonth.Append(" CASE salary_status");
                        FileFormonth.Append(" WHEN 0 THEN ");
                        FileFormonth.Append(" CASE " + _g._dataPayroll.payroll_employee._employee_type);
                        FileFormonth.Append(" WHEN 0 THEN (((salary * month) * customer_rf_rate) / 100)");
                        FileFormonth.Append(" WHEN 1 THEN ((((salary * 4 ) * month) * customer_rf_rate) / 100)");
                        FileFormonth.Append(" ELSE (((salary * working_day) * customer_rf_rate) / 100)");
                        FileFormonth.Append(" END");
                        FileFormonth.Append(" ELSE ");   
                        FileFormonth.Append(" CASE " + _g._dataPayroll.payroll_employee._employee_type);
                        FileFormonth.Append(" WHEN 0 THEN (((salary_vary_1 +  salary_vary_2 + salary_vary_3 + salary_vary_4 + salary_vary_5 + salary_vary_6 + salary_vary_7 + salary_vary_8 + salary_vary_9 + salary_vary_10 + salary_vary_11 + salary_vary_12) * customer_rf_rate) / 100)");
                        FileFormonth.Append(" WHEN 1 THEN ((((salary_vary_1 * 4 ) + (salary_vary_2 * 4 ) + (salary_vary_3 * 4 ) + (salary_vary_4 * 4 ) + (salary_vary_5 * 4 ) + (salary_vary_6 * 4 ) + (salary_vary_7 * 4 ) + (salary_vary_8 * 4 ) + (salary_vary_9 * 4 ) + (salary_vary_10 * 4 ) + (salary_vary_11 * 4 ) + (salary_vary_12 * 4 )) * customer_rf_rate) / 100)");
                        FileFormonth.Append(" ELSE ((((salary_vary_1 * working_day_1) + (salary_vary_2 * working_day_2) + (salary_vary_3 * working_day_3) + (salary_vary_4 * working_day_4) + (salary_vary_5 * working_day_5) + (salary_vary_6 * working_day_6) + (salary_vary_7 * working_day_7) + (salary_vary_8 * working_day_8) + (salary_vary_9 * working_day_9) + (salary_vary_10 * working_day_10) + (salary_vary_11 * working_day_11) + (salary_vary_12 * working_day_12)) * customer_rf_rate) / 100)");
                        FileFormonth.Append(" END");
                        FileFormonth.Append(" END");
                        FileFormonth.Append(" ),0) as " + _g._dataPayroll.payroll_trans._reserve_money + ",");
                        //เงินประกันสังคม
                        FileFormonth.Append(" coalesce(( ");
                        FileFormonth.Append(" CASE salary_status");
                        FileFormonth.Append(" WHEN 0 THEN ");
                        FileFormonth.Append(" CASE " + _g._dataPayroll.payroll_employee._employee_type);
                        FileFormonth.Append(" WHEN 0 THEN (((salary * month) * customer_ss_rate) / 100)");
                        FileFormonth.Append(" WHEN 1 THEN ((((salary * 4 ) * month) * customer_ss_rate) / 100)");
                        FileFormonth.Append(" ELSE (((salary * working_day) * customer_ss_rate) / 100)");
                        FileFormonth.Append(" END");
                        FileFormonth.Append(" ELSE ");
                        FileFormonth.Append(" CASE " + _g._dataPayroll.payroll_employee._employee_type);
                        FileFormonth.Append(" WHEN 0 THEN (((salary_vary_1 +  salary_vary_2 + salary_vary_3 + salary_vary_4 + salary_vary_5 + salary_vary_6 + salary_vary_7 + salary_vary_8 + salary_vary_9 + salary_vary_10 + salary_vary_11 + salary_vary_12) * customer_ss_rate) / 100)");
                        FileFormonth.Append(" WHEN 1 THEN ((((salary_vary_1 * 4 ) + (salary_vary_2 * 4 ) + (salary_vary_3 * 4 ) + (salary_vary_4 * 4 ) + (salary_vary_5 * 4 ) + (salary_vary_6 * 4 ) + (salary_vary_7 * 4 ) + (salary_vary_8 * 4 ) + (salary_vary_9 * 4 ) + (salary_vary_10 * 4 ) + (salary_vary_11 * 4 ) + (salary_vary_12 * 4 )) * customer_ss_rate) / 100)");
                        FileFormonth.Append(" ELSE ((((salary_vary_1 * working_day_1) + (salary_vary_2 * working_day_2) + (salary_vary_3 * working_day_3) + (salary_vary_4 * working_day_4) + (salary_vary_5 * working_day_5) + (salary_vary_6 * working_day_6) + (salary_vary_7 * working_day_7) + (salary_vary_8 * working_day_8) + (salary_vary_9 * working_day_9) + (salary_vary_10 * working_day_10) + (salary_vary_11 * working_day_11) + (salary_vary_12 * working_day_12)) * customer_ss_rate) / 100)");
                        FileFormonth.Append(" END");
                        FileFormonth.Append(" END");
                        FileFormonth.Append(" ),0) as " + _g._dataPayroll.payroll_trans._society_insure_money + ",");
                        //เงินค่าจ้าง
                        FileFormonth.Append(" coalesce((");
                        FileFormonth.Append(" CASE salary_status");
                        FileFormonth.Append(" WHEN 0 THEN ");
                        FileFormonth.Append(" CASE " + _g._dataPayroll.payroll_employee._employee_type);
                        FileFormonth.Append(" WHEN 0 THEN (salary * month) ");
                        FileFormonth.Append(" WHEN 1 THEN ((salary * 4 ) * month)");
                        FileFormonth.Append(" ELSE (salary * working_day)");
                        FileFormonth.Append(" END");
                        FileFormonth.Append(" ELSE ");
                        FileFormonth.Append(" CASE " + _g._dataPayroll.payroll_employee._employee_type);
                        FileFormonth.Append(" WHEN 0 THEN (salary_vary_1 +  salary_vary_2 + salary_vary_3 + salary_vary_4 + salary_vary_5 + salary_vary_6 + salary_vary_7 + salary_vary_8 + salary_vary_9 + salary_vary_10 + salary_vary_11 + salary_vary_12)");
                        FileFormonth.Append(" WHEN 1 THEN ((salary_vary_1 * 4 ) + (salary_vary_2 * 4 ) + (salary_vary_3 * 4 ) + (salary_vary_4 * 4 ) + (salary_vary_5 * 4 ) + (salary_vary_6 * 4 ) + (salary_vary_7 * 4 ) + (salary_vary_8 * 4 ) + (salary_vary_9 * 4 ) + (salary_vary_10 * 4 ) + (salary_vary_11 * 4 ) + (salary_vary_12 * 4 ))");
                        FileFormonth.Append(" ELSE ((salary_vary_1 * working_day_1) + (salary_vary_2 * working_day_2) + (salary_vary_3 * working_day_3) + (salary_vary_4 * working_day_4) + (salary_vary_5 * working_day_5) + (salary_vary_6 * working_day_6) + (salary_vary_7 * working_day_7) + (salary_vary_8 * working_day_8) + (salary_vary_9 * working_day_9) + (salary_vary_10 * working_day_10) + (salary_vary_11 * working_day_11) + (salary_vary_12 * working_day_12))");
                        FileFormonth.Append(" END");
                        FileFormonth.Append(" END");   
                        FileFormonth.Append(" ),0) as " + _g._dataPayroll.payroll_trans._wages_money + ",");

                        //คำนวนเงินหัก-เงินได้
                        DataFormonth.Append(" coalesce((select sum(" + _g._dataPayroll.payroll_trans_detail._diligent_money + ") from " + _g._dataPayroll.payroll_trans_detail._table + " where " + _g._dataPayroll.payroll_trans_detail._trans_flag + "=1 and " + _g._dataPayroll.payroll_trans_detail._trans_type + "=5 and " + _g._dataPayroll.payroll_trans_detail._employee + " =  " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + " " + WhereTransDetail + "),0) as " + _g._dataPayroll.payroll_resource._diligent_money + ",");
                        DataFormonth.Append(" coalesce((select sum(" + _g._dataPayroll.payroll_trans_detail._prepay_money + ") from " + _g._dataPayroll.payroll_trans_detail._table + " where " + _g._dataPayroll.payroll_trans_detail._trans_flag + "=1 and " + _g._dataPayroll.payroll_trans_detail._trans_type + "=5 and " + _g._dataPayroll.payroll_trans_detail._employee + " =  " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + " " + WhereTransDetail + "),0) as " + _g._dataPayroll.payroll_resource._prepay_money + ",");
                        DataFormonth.Append(" coalesce((select sum(" + _g._dataPayroll.payroll_trans_detail._bonus_money + ") from " + _g._dataPayroll.payroll_trans_detail._table + " where " + _g._dataPayroll.payroll_trans_detail._trans_flag + "=1 and " + _g._dataPayroll.payroll_trans_detail._trans_type + "=5 and " + _g._dataPayroll.payroll_trans_detail._employee + " =  " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + " " + WhereTransDetail + "),0) as " + _g._dataPayroll.payroll_resource._bonus_money + ",");
                        DataFormonth.Append(" coalesce((select sum(" + _g._dataPayroll.payroll_trans_detail._other_income_money + ") from " + _g._dataPayroll.payroll_trans_detail._table + " where " + _g._dataPayroll.payroll_trans_detail._trans_flag + "=1 and " + _g._dataPayroll.payroll_trans_detail._trans_type + "=5 and " + _g._dataPayroll.payroll_trans_detail._employee + " =  " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + " " + WhereTransDetail + "),0) as " + _g._dataPayroll.payroll_resource._other_income_money + ",");
                        DataFormonth.Append(" coalesce((select sum(" + _g._dataPayroll.payroll_trans_detail._advance_money + ") from " + _g._dataPayroll.payroll_trans_detail._table + " where " + _g._dataPayroll.payroll_trans_detail._trans_flag + "=1 and " + _g._dataPayroll.payroll_trans_detail._trans_type + "=5 and " + _g._dataPayroll.payroll_trans_detail._employee + " =  " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + " " + WhereTransDetail + "),0) as " + _g._dataPayroll.payroll_resource._advance_money + ",");
                        DataFormonth.Append(" coalesce((select sum(" + _g._dataPayroll.payroll_trans_detail._insure_money + ") from " + _g._dataPayroll.payroll_trans_detail._table + " where " + _g._dataPayroll.payroll_trans_detail._trans_flag + "=1 and " + _g._dataPayroll.payroll_trans_detail._trans_type + "=5 and " + _g._dataPayroll.payroll_trans_detail._employee + " =  " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + " " + WhereTransDetail + "),0) as " + _g._dataPayroll.payroll_resource._insure_money + ",");
                        DataFormonth.Append(" coalesce((select sum(" + _g._dataPayroll.payroll_trans_detail._other_payout_money + ") from " + _g._dataPayroll.payroll_trans_detail._table + " where " + _g._dataPayroll.payroll_trans_detail._trans_flag + "=1 and " + _g._dataPayroll.payroll_trans_detail._trans_type + "=5 and " + _g._dataPayroll.payroll_trans_detail._employee + " =  " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + " " + WhereTransDetail + "),0) as " + _g._dataPayroll.payroll_resource._other_payout_money + ",");
                        DataFormonth.Append(" coalesce((select sum(" + _g._dataPayroll.payroll_trans_detail._money + ") from " + _g._dataPayroll.payroll_trans_detail._table + " where " + _g._dataPayroll.payroll_trans_detail._trans_flag + "=1 and " + _g._dataPayroll.payroll_trans_detail._trans_type + "=1 and " + _g._dataPayroll.payroll_trans_detail._employee + " =  " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + " " + WhereTransDetail + "),0) as " + _g._dataPayroll.payroll_trans._sow_money + ",");
                        DataFormonth.Append(" coalesce((select sum(" + _g._dataPayroll.payroll_trans_detail._money + ") from " + _g._dataPayroll.payroll_trans_detail._table + " where " + _g._dataPayroll.payroll_trans_detail._trans_flag + "=1 and " + _g._dataPayroll.payroll_trans_detail._trans_type + "=2 and " + _g._dataPayroll.payroll_trans_detail._employee + " =  " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + " " + WhereTransDetail + "),0) as " + _g._dataPayroll.payroll_trans._leave_money + ",");
                        DataFormonth.Append(" coalesce((select sum(" + _g._dataPayroll.payroll_trans_detail._money + ") from " + _g._dataPayroll.payroll_trans_detail._table + " where " + _g._dataPayroll.payroll_trans_detail._trans_flag + "=1 and " + _g._dataPayroll.payroll_trans_detail._trans_type + "=3 and " + _g._dataPayroll.payroll_trans_detail._employee + " =  " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + " " + WhereTransDetail + "),0) as " + _g._dataPayroll.payroll_trans._late_money + ",");
                        DataFormonth.Append(" coalesce((select sum(" + _g._dataPayroll.payroll_trans_detail._money + ") from " + _g._dataPayroll.payroll_trans_detail._table + " where " + _g._dataPayroll.payroll_trans_detail._trans_flag + "=1 and " + _g._dataPayroll.payroll_trans_detail._trans_type + "=4 and " + _g._dataPayroll.payroll_trans_detail._employee + " =  " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + " " + WhereTransDetail + "),0) as " + _g._dataPayroll.payroll_resource._ot_money + ",");
                        //จำนวนวันทำงาน
                        DataFormonth.Append("coalesce((select sum(" + _g._dataPayroll.payroll_trans._working_day + ") from " + _g._dataPayroll.payroll_trans._table + " where " + _g._dataPayroll.payroll_trans._employee + "=" + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + " " + WhereTransDetail + "),0) as working_day,");
                        DataFormonth.Append("coalesce((select sum(" + _g._dataPayroll.payroll_trans._working_day + ") from " + _g._dataPayroll.payroll_trans._table + " where " + _g._dataPayroll.payroll_trans._employee + "=" + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + " and (" + _g._dataPayroll.payroll_trans_detail._select_month + " = 0)),0) as working_day_1,");
                        DataFormonth.Append("coalesce((select sum(" + _g._dataPayroll.payroll_trans._working_day + ") from " + _g._dataPayroll.payroll_trans._table + " where " + _g._dataPayroll.payroll_trans._employee + "=" + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + " and (" + _g._dataPayroll.payroll_trans_detail._select_month + " = 1)),0) as working_day_2,");
                        DataFormonth.Append("coalesce((select sum(" + _g._dataPayroll.payroll_trans._working_day + ") from " + _g._dataPayroll.payroll_trans._table + " where " + _g._dataPayroll.payroll_trans._employee + "=" + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + " and (" + _g._dataPayroll.payroll_trans_detail._select_month + " = 2)),0) as working_day_3,");
                        DataFormonth.Append("coalesce((select sum(" + _g._dataPayroll.payroll_trans._working_day + ") from " + _g._dataPayroll.payroll_trans._table + " where " + _g._dataPayroll.payroll_trans._employee + "=" + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + " and (" + _g._dataPayroll.payroll_trans_detail._select_month + " = 3)),0) as working_day_4,");
                        DataFormonth.Append("coalesce((select sum(" + _g._dataPayroll.payroll_trans._working_day + ") from " + _g._dataPayroll.payroll_trans._table + " where " + _g._dataPayroll.payroll_trans._employee + "=" + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + " and (" + _g._dataPayroll.payroll_trans_detail._select_month + " = 4)),0) as working_day_5,");
                        DataFormonth.Append("coalesce((select sum(" + _g._dataPayroll.payroll_trans._working_day + ") from " + _g._dataPayroll.payroll_trans._table + " where " + _g._dataPayroll.payroll_trans._employee + "=" + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + " and (" + _g._dataPayroll.payroll_trans_detail._select_month + " = 5)),0) as working_day_6,");
                        DataFormonth.Append("coalesce((select sum(" + _g._dataPayroll.payroll_trans._working_day + ") from " + _g._dataPayroll.payroll_trans._table + " where " + _g._dataPayroll.payroll_trans._employee + "=" + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + " and (" + _g._dataPayroll.payroll_trans_detail._select_month + " = 6)),0) as working_day_7,");
                        DataFormonth.Append("coalesce((select sum(" + _g._dataPayroll.payroll_trans._working_day + ") from " + _g._dataPayroll.payroll_trans._table + " where " + _g._dataPayroll.payroll_trans._employee + "=" + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + " and (" + _g._dataPayroll.payroll_trans_detail._select_month + " = 7)),0) as working_day_8,");
                        DataFormonth.Append("coalesce((select sum(" + _g._dataPayroll.payroll_trans._working_day + ") from " + _g._dataPayroll.payroll_trans._table + " where " + _g._dataPayroll.payroll_trans._employee + "=" + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + " and (" + _g._dataPayroll.payroll_trans_detail._select_month + " = 8)),0) as working_day_9,");
                        DataFormonth.Append("coalesce((select sum(" + _g._dataPayroll.payroll_trans._working_day + ") from " + _g._dataPayroll.payroll_trans._table + " where " + _g._dataPayroll.payroll_trans._employee + "=" + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + " and (" + _g._dataPayroll.payroll_trans_detail._select_month + " = 9)),0) as working_day_10,");
                        DataFormonth.Append("coalesce((select sum(" + _g._dataPayroll.payroll_trans._working_day + ") from " + _g._dataPayroll.payroll_trans._table + " where " + _g._dataPayroll.payroll_trans._employee + "=" + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + " and (" + _g._dataPayroll.payroll_trans_detail._select_month + " = 10)),0) as working_day_11,");
                        DataFormonth.Append("coalesce((select sum(" + _g._dataPayroll.payroll_trans._working_day + ") from " + _g._dataPayroll.payroll_trans._table + " where " + _g._dataPayroll.payroll_trans._employee + "=" + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + " and (" + _g._dataPayroll.payroll_trans_detail._select_month + " = 11)),0) as working_day_12,");
                        //เงินค่าจ้าง  
                        DataFormonth.Append("coalesce((" + _g._dataPayroll.payroll_employee._salary + "),0) as salary,");
                        DataFormonth.Append("coalesce((CASE ");
                        DataFormonth.Append(" WHEN to_char(" + _g._dataPayroll.payroll_employee._start_work + ",'yyyy') < '" + (MyLib._myGlobal._intPhase(InYear) - 543).ToString() + "' THEN 12 ");
                        DataFormonth.Append(" ELSE (13 - (to_number(to_char(" + _g._dataPayroll.payroll_employee._start_work + ",'MM'),'99')))");
                        DataFormonth.Append(" END),0) as month,");
                        DataFormonth.Append("(select coalesce((" + _g._dataPayroll.payroll_employee_salary._salary + "),0) as " + _g._dataPayroll.payroll_employee_salary._salary);
                        DataFormonth.Append(" from " + _g._dataPayroll.payroll_employee_salary._table);
                        DataFormonth.Append(" where " + _g._dataPayroll.payroll_employee_salary._code + " = " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + "");
                        DataFormonth.Append(" and to_char(" + _g._dataPayroll.payroll_employee_salary._start_date + ",'yyyy') <= '" + (MyLib._myGlobal._intPhase(InYear) - 543).ToString() + "'");
                        DataFormonth.Append(" and to_char(" + _g._dataPayroll.payroll_employee_salary._end_date + ",'yyyy') >= '" + (MyLib._myGlobal._intPhase(InYear) - 543).ToString() + "'");
                        DataFormonth.Append(" and to_char(" + _g._dataPayroll.payroll_employee_salary._start_date + ",'MM') <= '01'");
                        DataFormonth.Append(" and (to_char(" + _g._dataPayroll.payroll_employee_salary._end_date + ",'MM') >= '01' or " + _g._dataPayroll.payroll_employee_salary._end_date + " = '1000-01-01')) as salary_vary_1,");
                        DataFormonth.Append("(select coalesce((" + _g._dataPayroll.payroll_employee_salary._salary + "),0) as " + _g._dataPayroll.payroll_employee_salary._salary);
                        DataFormonth.Append(" from " + _g._dataPayroll.payroll_employee_salary._table);
                        DataFormonth.Append(" where " + _g._dataPayroll.payroll_employee_salary._code + " = " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + "");
                        DataFormonth.Append(" and to_char(" + _g._dataPayroll.payroll_employee_salary._start_date + ",'yyyy') <= '" + (MyLib._myGlobal._intPhase(InYear) - 543).ToString() + "'");
                        DataFormonth.Append(" and to_char(" + _g._dataPayroll.payroll_employee_salary._end_date + ",'yyyy') >= '" + (MyLib._myGlobal._intPhase(InYear) - 543).ToString() + "'");
                        DataFormonth.Append(" and to_char(" + _g._dataPayroll.payroll_employee_salary._start_date + ",'MM') <= '02'");
                        DataFormonth.Append(" and (to_char(" + _g._dataPayroll.payroll_employee_salary._end_date + ",'MM') >= '02' or " + _g._dataPayroll.payroll_employee_salary._end_date + " = '1000-01-01')) as salary_vary_2,");
                        DataFormonth.Append("(select coalesce((" + _g._dataPayroll.payroll_employee_salary._salary + "),0) as " + _g._dataPayroll.payroll_employee_salary._salary);
                        DataFormonth.Append(" from " + _g._dataPayroll.payroll_employee_salary._table);
                        DataFormonth.Append(" where " + _g._dataPayroll.payroll_employee_salary._code + " = " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + "");
                        DataFormonth.Append(" and to_char(" + _g._dataPayroll.payroll_employee_salary._start_date + ",'yyyy') <= '" + (MyLib._myGlobal._intPhase(InYear) - 543).ToString() + "'");
                        DataFormonth.Append(" and to_char(" + _g._dataPayroll.payroll_employee_salary._end_date + ",'yyyy') >= '" + (MyLib._myGlobal._intPhase(InYear) - 543).ToString() + "'");
                        DataFormonth.Append(" and to_char(" + _g._dataPayroll.payroll_employee_salary._start_date + ",'MM') <= '03'");
                        DataFormonth.Append(" and (to_char(" + _g._dataPayroll.payroll_employee_salary._end_date + ",'MM') >= '03' or " + _g._dataPayroll.payroll_employee_salary._end_date + " = '1000-01-01')) as salary_vary_3,");
                        DataFormonth.Append("(select coalesce((" + _g._dataPayroll.payroll_employee_salary._salary + "),0) as " + _g._dataPayroll.payroll_employee_salary._salary);
                        DataFormonth.Append(" from " + _g._dataPayroll.payroll_employee_salary._table);
                        DataFormonth.Append(" where " + _g._dataPayroll.payroll_employee_salary._code + " = " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + "");
                        DataFormonth.Append(" and to_char(" + _g._dataPayroll.payroll_employee_salary._start_date + ",'yyyy') <= '" + (MyLib._myGlobal._intPhase(InYear) - 543).ToString() + "'");
                        DataFormonth.Append(" and to_char(" + _g._dataPayroll.payroll_employee_salary._end_date + ",'yyyy') >= '" + (MyLib._myGlobal._intPhase(InYear) - 543).ToString() + "'");
                        DataFormonth.Append(" and to_char(" + _g._dataPayroll.payroll_employee_salary._start_date + ",'MM') <= '04'");
                        DataFormonth.Append(" and (to_char(" + _g._dataPayroll.payroll_employee_salary._end_date + ",'MM') >= '04' or " + _g._dataPayroll.payroll_employee_salary._end_date + " = '1000-01-01')) as salary_vary_4,");
                        DataFormonth.Append("(select coalesce((" + _g._dataPayroll.payroll_employee_salary._salary + "),0) as " + _g._dataPayroll.payroll_employee_salary._salary);
                        DataFormonth.Append(" from " + _g._dataPayroll.payroll_employee_salary._table);
                        DataFormonth.Append(" where " + _g._dataPayroll.payroll_employee_salary._code + " = " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + "");
                        DataFormonth.Append(" and to_char(" + _g._dataPayroll.payroll_employee_salary._start_date + ",'yyyy') <= '" + (MyLib._myGlobal._intPhase(InYear) - 543).ToString() + "'");
                        DataFormonth.Append(" and to_char(" + _g._dataPayroll.payroll_employee_salary._end_date + ",'yyyy') >= '" + (MyLib._myGlobal._intPhase(InYear) - 543).ToString() + "'");
                        DataFormonth.Append(" and to_char(" + _g._dataPayroll.payroll_employee_salary._start_date + ",'MM') <= '05'");
                        DataFormonth.Append(" and (to_char(" + _g._dataPayroll.payroll_employee_salary._end_date + ",'MM') >= '05' or " + _g._dataPayroll.payroll_employee_salary._end_date + " = '1000-01-01')) as salary_vary_5,");
                        DataFormonth.Append("(select coalesce((" + _g._dataPayroll.payroll_employee_salary._salary + "),0) as " + _g._dataPayroll.payroll_employee_salary._salary);
                        DataFormonth.Append(" from " + _g._dataPayroll.payroll_employee_salary._table);
                        DataFormonth.Append(" where " + _g._dataPayroll.payroll_employee_salary._code + " = " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + "");
                        DataFormonth.Append(" and to_char(" + _g._dataPayroll.payroll_employee_salary._start_date + ",'yyyy') <= '" + (MyLib._myGlobal._intPhase(InYear) - 543).ToString() + "'");
                        DataFormonth.Append(" and to_char(" + _g._dataPayroll.payroll_employee_salary._end_date + ",'yyyy') >= '" + (MyLib._myGlobal._intPhase(InYear) - 543).ToString() + "'");
                        DataFormonth.Append(" and to_char(" + _g._dataPayroll.payroll_employee_salary._start_date + ",'MM') <= '06'");
                        DataFormonth.Append(" and (to_char(" + _g._dataPayroll.payroll_employee_salary._end_date + ",'MM') >= '06' or " + _g._dataPayroll.payroll_employee_salary._end_date + " = '1000-01-01')) as salary_vary_6,");
                        DataFormonth.Append("(select coalesce((" + _g._dataPayroll.payroll_employee_salary._salary + "),0) as " + _g._dataPayroll.payroll_employee_salary._salary);
                        DataFormonth.Append(" from " + _g._dataPayroll.payroll_employee_salary._table);
                        DataFormonth.Append(" where " + _g._dataPayroll.payroll_employee_salary._code + " = " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + "");
                        DataFormonth.Append(" and to_char(" + _g._dataPayroll.payroll_employee_salary._start_date + ",'yyyy') <= '" + (MyLib._myGlobal._intPhase(InYear) - 543).ToString() + "'");
                        DataFormonth.Append(" and to_char(" + _g._dataPayroll.payroll_employee_salary._end_date + ",'yyyy') >= '" + (MyLib._myGlobal._intPhase(InYear) - 543).ToString() + "'");
                        DataFormonth.Append(" and to_char(" + _g._dataPayroll.payroll_employee_salary._start_date + ",'MM') <= '07'");
                        DataFormonth.Append(" and (to_char(" + _g._dataPayroll.payroll_employee_salary._end_date + ",'MM') >= '07' or " + _g._dataPayroll.payroll_employee_salary._end_date + " = '1000-01-01')) as salary_vary_7,");
                        DataFormonth.Append("(select coalesce((" + _g._dataPayroll.payroll_employee_salary._salary + "),0) as " + _g._dataPayroll.payroll_employee_salary._salary);
                        DataFormonth.Append(" from " + _g._dataPayroll.payroll_employee_salary._table);
                        DataFormonth.Append(" where " + _g._dataPayroll.payroll_employee_salary._code + " = " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + "");
                        DataFormonth.Append(" and to_char(" + _g._dataPayroll.payroll_employee_salary._start_date + ",'yyyy') <= '" + (MyLib._myGlobal._intPhase(InYear) - 543).ToString() + "'");
                        DataFormonth.Append(" and to_char(" + _g._dataPayroll.payroll_employee_salary._end_date + ",'yyyy') >= '" + (MyLib._myGlobal._intPhase(InYear) - 543).ToString() + "'");
                        DataFormonth.Append(" and to_char(" + _g._dataPayroll.payroll_employee_salary._start_date + ",'MM') <= '08'");
                        DataFormonth.Append(" and (to_char(" + _g._dataPayroll.payroll_employee_salary._end_date + ",'MM') >= '08' or " + _g._dataPayroll.payroll_employee_salary._end_date + " = '1000-01-01')) as salary_vary_8,");
                        DataFormonth.Append("(select coalesce((" + _g._dataPayroll.payroll_employee_salary._salary + "),0) as " + _g._dataPayroll.payroll_employee_salary._salary);
                        DataFormonth.Append(" from " + _g._dataPayroll.payroll_employee_salary._table);
                        DataFormonth.Append(" where " + _g._dataPayroll.payroll_employee_salary._code + " = " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + "");
                        DataFormonth.Append(" and to_char(" + _g._dataPayroll.payroll_employee_salary._start_date + ",'yyyy') <= '" + (MyLib._myGlobal._intPhase(InYear) - 543).ToString() + "'");
                        DataFormonth.Append(" and to_char(" + _g._dataPayroll.payroll_employee_salary._end_date + ",'yyyy') >= '" + (MyLib._myGlobal._intPhase(InYear) - 543).ToString() + "'");
                        DataFormonth.Append(" and to_char(" + _g._dataPayroll.payroll_employee_salary._start_date + ",'MM') <= '09'");
                        DataFormonth.Append(" and (to_char(" + _g._dataPayroll.payroll_employee_salary._end_date + ",'MM') >= '09' or " + _g._dataPayroll.payroll_employee_salary._end_date + " = '1000-01-01')) as salary_vary_9,");
                        DataFormonth.Append("(select coalesce((" + _g._dataPayroll.payroll_employee_salary._salary + "),0) as " + _g._dataPayroll.payroll_employee_salary._salary);
                        DataFormonth.Append(" from " + _g._dataPayroll.payroll_employee_salary._table);
                        DataFormonth.Append(" where " + _g._dataPayroll.payroll_employee_salary._code + " = " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + "");
                        DataFormonth.Append(" and to_char(" + _g._dataPayroll.payroll_employee_salary._start_date + ",'yyyy') <= '" + (MyLib._myGlobal._intPhase(InYear) - 543).ToString() + "'");
                        DataFormonth.Append(" and to_char(" + _g._dataPayroll.payroll_employee_salary._end_date + ",'yyyy') >= '" + (MyLib._myGlobal._intPhase(InYear) - 543).ToString() + "'");
                        DataFormonth.Append(" and to_char(" + _g._dataPayroll.payroll_employee_salary._start_date + ",'MM') <= '10'");
                        DataFormonth.Append(" and (to_char(" + _g._dataPayroll.payroll_employee_salary._end_date + ",'MM') >= '10' or " + _g._dataPayroll.payroll_employee_salary._end_date + " = '1000-01-01')) as salary_vary_10,");
                        DataFormonth.Append("(select coalesce((" + _g._dataPayroll.payroll_employee_salary._salary + "),0) as " + _g._dataPayroll.payroll_employee_salary._salary);
                        DataFormonth.Append(" from " + _g._dataPayroll.payroll_employee_salary._table);
                        DataFormonth.Append(" where " + _g._dataPayroll.payroll_employee_salary._code + " = " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + "");
                        DataFormonth.Append(" and to_char(" + _g._dataPayroll.payroll_employee_salary._start_date + ",'yyyy') <= '" + (MyLib._myGlobal._intPhase(InYear) - 543).ToString() + "'");
                        DataFormonth.Append(" and to_char(" + _g._dataPayroll.payroll_employee_salary._end_date + ",'yyyy') >= '" + (MyLib._myGlobal._intPhase(InYear) - 543).ToString() + "'");
                        DataFormonth.Append(" and to_char(" + _g._dataPayroll.payroll_employee_salary._start_date + ",'MM') <= '11'");
                        DataFormonth.Append(" and (to_char(" + _g._dataPayroll.payroll_employee_salary._end_date + ",'MM') >= '11' or " + _g._dataPayroll.payroll_employee_salary._end_date + " = '1000-01-01')) as salary_vary_11,");
                        DataFormonth.Append("(select coalesce((" + _g._dataPayroll.payroll_employee_salary._salary + "),0) as " + _g._dataPayroll.payroll_employee_salary._salary);
                        DataFormonth.Append(" from " + _g._dataPayroll.payroll_employee_salary._table);
                        DataFormonth.Append(" where " + _g._dataPayroll.payroll_employee_salary._code + " = " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + "");
                        DataFormonth.Append(" and to_char(" + _g._dataPayroll.payroll_employee_salary._start_date + ",'yyyy') <= '" + (MyLib._myGlobal._intPhase(InYear) - 543).ToString() + "'");
                        DataFormonth.Append(" and to_char(" + _g._dataPayroll.payroll_employee_salary._end_date + ",'yyyy') >= '" + (MyLib._myGlobal._intPhase(InYear) - 543).ToString() + "'");
                        DataFormonth.Append(" and to_char(" + _g._dataPayroll.payroll_employee_salary._start_date + ",'MM') <= '12'");
                        DataFormonth.Append(" and (to_char(" + _g._dataPayroll.payroll_employee_salary._end_date + ",'MM') >= '12' or " + _g._dataPayroll.payroll_employee_salary._end_date + " = '1000-01-01')) as salary_vary_12,");
                            
                        
                        switch (ScreenName)
                        {                            
                            case _g.g._screenPayrollEnum.ภงด_91:
                            case _g.g._screenPayrollEnum.ภงด_1_ก_ใบแนบ:
                            case _g.g._screenPayrollEnum.ภงด_1_ก:
                                FieldEmployeeHead.Append("title_name as title_name,");
                                FieldEmployeeHead.Append("tax_no as tax_no,");
                                FieldEmployeeHead.Append("id_code as id_code,");
                                FieldEmployeeHead.Append("house as house,");
                                FieldEmployeeHead.Append("room_no as room_no,");
                                FieldEmployeeHead.Append("floor_no as floor_no,");
                                FieldEmployeeHead.Append("village as village,");
                                FieldEmployeeHead.Append("house_no as house_no,");
                                FieldEmployeeHead.Append("crowd_no as crowd_no,");
                                FieldEmployeeHead.Append("lane as lane,");
                                FieldEmployeeHead.Append("road as road,");
                                FieldEmployeeHead.Append("locality as locality,");
                                FieldEmployeeHead.Append("amphur as amphur,");
                                FieldEmployeeHead.Append("province as province,");
                                FieldEmployeeHead.Append("postcode as postcode,");
                                FieldEmployeeHead.Append("telephone as telephone,");
                                FieldEmployeeHead.Append("birth_day as birth_day,");
                                FieldEmployeeHead.Append("spouse_name as spouse_name,");
                                FieldEmployeeHead.Append("spouse_id_code as spouse_id_code,");
                                FieldEmployeeHead.Append("spouse_birth_day as spouse_birth_day,");
                                FieldEmployeeHead.Append("father_id as father_id,");
                                FieldEmployeeHead.Append("mother_id as mother_id,");
                                FieldEmployeeHead.Append("father_law_id as father_law_id,");
                                FieldEmployeeHead.Append("mother_law_id as mother_law_id,");
                                FieldEmployeeHead.Append("child_study_qty as child_study_qty,");
                                FieldEmployeeHead.Append("child_no_study_qty as child_no_study_qty,");



                                FieldEmployeeData.Append("(title_name) as title_name,");
                                FieldEmployeeData.Append("(tax_no) as tax_no,");
                                FieldEmployeeData.Append("(select id_code from payroll_employee_detail where payroll_employee_detail.code = payroll_employee.code) as id_code,");
                                FieldEmployeeData.Append("(select house from payroll_employee_address where payroll_employee_address.code = payroll_employee.code) as house,");
                                FieldEmployeeData.Append("(select room_no from payroll_employee_address where payroll_employee_address.code = payroll_employee.code) as room_no,");
                                FieldEmployeeData.Append("(select floor_no from payroll_employee_address where payroll_employee_address.code = payroll_employee.code) as floor_no,");
                                FieldEmployeeData.Append("(select village from payroll_employee_address where payroll_employee_address.code = payroll_employee.code) as village,");
                                FieldEmployeeData.Append("(select house_no from payroll_employee_address where payroll_employee_address.code = payroll_employee.code) as house_no,");
                                FieldEmployeeData.Append("(select crowd_no from payroll_employee_address where payroll_employee_address.code = payroll_employee.code) as crowd_no,");
                                FieldEmployeeData.Append("(select lane from payroll_employee_address where payroll_employee_address.code = payroll_employee.code) as lane,");
                                FieldEmployeeData.Append("(select road from payroll_employee_address where payroll_employee_address.code = payroll_employee.code) as road,");
                                FieldEmployeeData.Append("(select locality from payroll_employee_address where payroll_employee_address.code = payroll_employee.code) as locality,");
                                FieldEmployeeData.Append("(select amphur from payroll_employee_address where payroll_employee_address.code = payroll_employee.code) as amphur,");
                                FieldEmployeeData.Append("(select province from payroll_employee_address where payroll_employee_address.code = payroll_employee.code) as province,");
                                FieldEmployeeData.Append("(select postcode from payroll_employee_address where payroll_employee_address.code = payroll_employee.code) as postcode,");
                                FieldEmployeeData.Append("(select telephone from payroll_employee_address where payroll_employee_address.code = payroll_employee.code) as telephone,");
                                FieldEmployeeData.Append("(select birth_day from payroll_employee_detail where payroll_employee_detail.code = payroll_employee.code) as birth_day,");
                                FieldEmployeeData.Append("(select spouse_name from payroll_employee_detail where payroll_employee_detail.code = payroll_employee.code) as spouse_name,");
                                FieldEmployeeData.Append("(select spouse_birth_day from payroll_employee_detail where payroll_employee_detail.code = payroll_employee.code) as spouse_birth_day,");
                                FieldEmployeeData.Append("(select spouse_id_code from payroll_employee_detail where payroll_employee_detail.code = payroll_employee.code) as spouse_id_code,");
                                FieldEmployeeData.Append("(select father_id from payroll_employee_detail where payroll_employee_detail.code = payroll_employee.code) as father_id,");
                                FieldEmployeeData.Append("(select mother_id from payroll_employee_detail where payroll_employee_detail.code = payroll_employee.code) as mother_id,");
                                FieldEmployeeData.Append("(select father_law_id from payroll_employee_detail where payroll_employee_detail.code = payroll_employee.code) as father_law_id,");
                                FieldEmployeeData.Append("(select mother_law_id from payroll_employee_detail where payroll_employee_detail.code = payroll_employee.code) as mother_law_id,");
                                FieldEmployeeData.Append("(select child_study_qty from payroll_employee_detail where payroll_employee_detail.code = payroll_employee.code) as child_study_qty,");
                                FieldEmployeeData.Append("(select child_no_study_qty from payroll_employee_detail where payroll_employee_detail.code = payroll_employee.code) as child_no_study_qty,");
                               
                                break;
                        }
                        break;
                    case _g.g._screenPayrollEnum.รายงานภาษีเงินได้_รายเดือน:
                    case _g.g._screenPayrollEnum.ภงด_1:
                    case _g.g._screenPayrollEnum.ภงด_1_ใบแนบ:
                    case _g.g._screenPayrollEnum.คำนวนภาษีรายเดือน:
                    case _g.g._screenPayrollEnum.คำนวนเงินเดือน:
                    case _g.g._screenPayrollEnum.รายละเอียดเงินเดือน_ค่าจ้าง_ตามประเภทลูกจ้าง:
                    case _g.g._screenPayrollEnum.สลิปเงินเดือน:
                        //เงินหัก-เงินได้
                        FileFormonth.Append(_g._dataPayroll.payroll_trans._sow_money + " as " + _g._dataPayroll.payroll_resource._sow_money + ",");                                              
                        FileFormonth.Append(_g._dataPayroll.payroll_trans._leave_money + " as " + _g._dataPayroll.payroll_resource._leave_money + ",");
                        FileFormonth.Append(_g._dataPayroll.payroll_trans._late_money + " as " + _g._dataPayroll.payroll_resource._late_money + ",");
                        FileFormonth.Append(_g._dataPayroll.payroll_trans._ot_money + " as " + _g._dataPayroll.payroll_resource._ot_money + ",");
                        FileFormonth.Append(_g._dataPayroll.payroll_trans._diligent_money + " as " + _g._dataPayroll.payroll_resource._diligent_money + ",");
                        FileFormonth.Append(_g._dataPayroll.payroll_trans._prepay_money + " as " + _g._dataPayroll.payroll_resource._prepay_money + ",");
                        FileFormonth.Append(_g._dataPayroll.payroll_trans._bonus_money + " as " + _g._dataPayroll.payroll_resource._bonus_money + ",");
                        FileFormonth.Append(_g._dataPayroll.payroll_trans._other_income_money + " as " + _g._dataPayroll.payroll_resource._other_income_money + ",");
                        FileFormonth.Append(_g._dataPayroll.payroll_trans._advance_money + " as " + _g._dataPayroll.payroll_resource._advance_money + ",");
                        FileFormonth.Append(_g._dataPayroll.payroll_trans._insure_money + " as " + _g._dataPayroll.payroll_resource._insure_money + ",");
                        FileFormonth.Append(_g._dataPayroll.payroll_trans._other_payout_money + " as " + _g._dataPayroll.payroll_resource._other_payout_money + ",");
                        //ค่าจ้างสุทธิ
                        FileFormonth.Append("(0) as " + _g._dataPayroll.payroll_resource._net_wages + ",");

                        //คำนวนเงินหัก-เงินได้
                        DataFormonth.Append(" coalesce((select sum(" + _g._dataPayroll.payroll_trans_detail._diligent_money + ") from " + _g._dataPayroll.payroll_trans_detail._table + " where " + _g._dataPayroll.payroll_trans_detail._trans_flag + "=1 and " + _g._dataPayroll.payroll_trans_detail._trans_type + "=5 and " + _g._dataPayroll.payroll_trans_detail._employee + " =  " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + " " + WhereTransDetail + "),0) as " + _g._dataPayroll.payroll_resource._diligent_money + ",");
                        DataFormonth.Append(" coalesce((select sum(" + _g._dataPayroll.payroll_trans_detail._prepay_money + ") from " + _g._dataPayroll.payroll_trans_detail._table + " where " + _g._dataPayroll.payroll_trans_detail._trans_flag + "=1 and " + _g._dataPayroll.payroll_trans_detail._trans_type + "=5 and " + _g._dataPayroll.payroll_trans_detail._employee + " =  " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + " " + WhereTransDetail + "),0) as " + _g._dataPayroll.payroll_resource._prepay_money + ",");
                        DataFormonth.Append(" coalesce((select sum(" + _g._dataPayroll.payroll_trans_detail._bonus_money + ") from " + _g._dataPayroll.payroll_trans_detail._table + " where " + _g._dataPayroll.payroll_trans_detail._trans_flag + "=1 and " + _g._dataPayroll.payroll_trans_detail._trans_type + "=5 and " + _g._dataPayroll.payroll_trans_detail._employee + " =  " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + " " + WhereTransDetail + "),0) as " + _g._dataPayroll.payroll_resource._bonus_money + ",");
                        DataFormonth.Append(" coalesce((select sum(" + _g._dataPayroll.payroll_trans_detail._other_income_money + ") from " + _g._dataPayroll.payroll_trans_detail._table + " where " + _g._dataPayroll.payroll_trans_detail._trans_flag + "=1 and " + _g._dataPayroll.payroll_trans_detail._trans_type + "=5 and " + _g._dataPayroll.payroll_trans_detail._employee + " =  " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + " " + WhereTransDetail + "),0) as " + _g._dataPayroll.payroll_resource._other_income_money + ",");
                        DataFormonth.Append(" coalesce((select sum(" + _g._dataPayroll.payroll_trans_detail._advance_money + ") from " + _g._dataPayroll.payroll_trans_detail._table + " where " + _g._dataPayroll.payroll_trans_detail._trans_flag + "=1 and " + _g._dataPayroll.payroll_trans_detail._trans_type + "=5 and " + _g._dataPayroll.payroll_trans_detail._employee + " =  " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + " " + WhereTransDetail + "),0) as " + _g._dataPayroll.payroll_resource._advance_money + ",");
                        DataFormonth.Append(" coalesce((select sum(" + _g._dataPayroll.payroll_trans_detail._insure_money + ") from " + _g._dataPayroll.payroll_trans_detail._table + " where " + _g._dataPayroll.payroll_trans_detail._trans_flag + "=1 and " + _g._dataPayroll.payroll_trans_detail._trans_type + "=5 and " + _g._dataPayroll.payroll_trans_detail._employee + " =  " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + " " + WhereTransDetail + "),0) as " + _g._dataPayroll.payroll_resource._insure_money + ",");
                        DataFormonth.Append(" coalesce((select sum(" + _g._dataPayroll.payroll_trans_detail._other_payout_money + ") from " + _g._dataPayroll.payroll_trans_detail._table + " where " + _g._dataPayroll.payroll_trans_detail._trans_flag + "=1 and " + _g._dataPayroll.payroll_trans_detail._trans_type + "=5 and " + _g._dataPayroll.payroll_trans_detail._employee + " =  " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + " " + WhereTransDetail + "),0) as " + _g._dataPayroll.payroll_resource._other_payout_money + ",");
                        DataFormonth.Append(" coalesce((select sum(" + _g._dataPayroll.payroll_trans_detail._money + ") from " + _g._dataPayroll.payroll_trans_detail._table + " where " + _g._dataPayroll.payroll_trans_detail._trans_flag + "=1 and " + _g._dataPayroll.payroll_trans_detail._trans_type + "=1 and " + _g._dataPayroll.payroll_trans_detail._employee + " =  " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + " " + WhereTransDetail + "),0) as " + _g._dataPayroll.payroll_trans._sow_money + ",");
                        DataFormonth.Append(" coalesce((select sum(" + _g._dataPayroll.payroll_trans_detail._money + ") from " + _g._dataPayroll.payroll_trans_detail._table + " where " + _g._dataPayroll.payroll_trans_detail._trans_flag + "=1 and " + _g._dataPayroll.payroll_trans_detail._trans_type + "=2 and " + _g._dataPayroll.payroll_trans_detail._employee + " =  " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + " " + WhereTransDetail + "),0) as " + _g._dataPayroll.payroll_trans._leave_money + ",");
                        DataFormonth.Append(" coalesce((select sum(" + _g._dataPayroll.payroll_trans_detail._money + ") from " + _g._dataPayroll.payroll_trans_detail._table + " where " + _g._dataPayroll.payroll_trans_detail._trans_flag + "=1 and " + _g._dataPayroll.payroll_trans_detail._trans_type + "=3 and " + _g._dataPayroll.payroll_trans_detail._employee + " =  " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + " " + WhereTransDetail + "),0) as " + _g._dataPayroll.payroll_trans._late_money + ",");                     
                        DataFormonth.Append( " coalesce((select sum(" + _g._dataPayroll.payroll_trans_detail._money + ") from " + _g._dataPayroll.payroll_trans_detail._table + " where " + _g._dataPayroll.payroll_trans_detail._trans_flag + "=1 and " + _g._dataPayroll.payroll_trans_detail._trans_type + "=4 and " + _g._dataPayroll.payroll_trans_detail._employee + " =  " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + " " + WhereTransDetail + "),0) as " + _g._dataPayroll.payroll_resource._ot_money + ",");                     
                       
                        switch (ScreenName)
                        {
                            case _g.g._screenPayrollEnum.คำนวนเงินเดือน:
                            case _g.g._screenPayrollEnum.รายละเอียดเงินเดือน_ค่าจ้าง_ตามประเภทลูกจ้าง:
                            case _g.g._screenPayrollEnum.สลิปเงินเดือน:
                                FieldSalarySlipHead.Append("side_name as side_name,");
                                FieldSalarySlipHead.Append("section_name as section_name,");
                                FieldSalarySlipHead.Append("work_title_name as work_title_name,");
                                FieldSalarySlipHead.Append("book_bank_no as book_bank_no,");
                                FieldSalarySlipHead.Append("working_day as working_day,");
                                FieldSalarySlipHead.Append("(0) as sumincome,");
                                FieldSalarySlipHead.Append("(0) as sumpayout,");

                                FieldSalarySlipHead.Append(" coalesce((");
                                FieldSalarySlipHead.Append(" CASE salary_status");
                                FieldSalarySlipHead.Append(" WHEN 0 THEN reserve_money_in_month ");
                                FieldSalarySlipHead.Append(" ELSE reserve_money_in_month_vary");
                                FieldSalarySlipHead.Append(" END");
                                FieldSalarySlipHead.Append(" ),0) as reserve_money_in_month,");

                                FieldSalarySlipHead.Append(" coalesce((");
                                FieldSalarySlipHead.Append(" CASE salary_status");
                                FieldSalarySlipHead.Append(" WHEN 0 THEN society_insure_money_in_month ");
                                FieldSalarySlipHead.Append(" ELSE society_insure_money_in_month_vary");
                                FieldSalarySlipHead.Append(" END");
                                FieldSalarySlipHead.Append(" ),0) as society_insure_money_in_month,");

                                FieldSalarySlipHead.Append(" coalesce((");
                                FieldSalarySlipHead.Append(" CASE salary_status");
                                FieldSalarySlipHead.Append(" WHEN 0 THEN wages_money_in_month ");
                                FieldSalarySlipHead.Append(" ELSE wages_money_in_month_vary");
                                FieldSalarySlipHead.Append(" END");
                                FieldSalarySlipHead.Append(" ),0) as wages_money_in_month,");

                                FieldSalarySlipHead.Append(" coalesce((");
                                FieldSalarySlipHead.Append(" CASE salary_status");
                                FieldSalarySlipHead.Append(" WHEN 0 THEN reserve_money ");
                                FieldSalarySlipHead.Append(" ELSE reserve_money_vary");
                                FieldSalarySlipHead.Append(" END");
                                FieldSalarySlipHead.Append(" ),0) as reserve_money,");

                                FieldSalarySlipHead.Append(" coalesce((");
                                FieldSalarySlipHead.Append(" CASE salary_status");
                                FieldSalarySlipHead.Append(" WHEN 0 THEN society_insure_money ");
                                FieldSalarySlipHead.Append(" ELSE society_insure_money_vary");
                                FieldSalarySlipHead.Append(" END");
                                FieldSalarySlipHead.Append(" ),0) as society_insure_money,");

                                FieldSalarySlipHead.Append(" coalesce((");
                                FieldSalarySlipHead.Append(" CASE salary_status");
                                FieldSalarySlipHead.Append(" WHEN 0 THEN wages_money ");
                                FieldSalarySlipHead.Append(" ELSE wages_money_vary");
                                FieldSalarySlipHead.Append(" END");
                                FieldSalarySlipHead.Append(" ),0) as wages_money,");

                                                   
                                FieldSalarySlipHead.Append("diligent_money_in_month as diligent_money_in_month,");
                                FieldSalarySlipHead.Append("prepay_money_in_month as prepay_money_in_month,");
                                FieldSalarySlipHead.Append("bonus_money_in_month as bonus_money_in_month,");
                                FieldSalarySlipHead.Append("other_income_money_in_month as other_income_money_in_month,");
                                FieldSalarySlipHead.Append("advance_money_in_month as advance_money_in_month,");
                                FieldSalarySlipHead.Append("insure_money_in_month as insure_money_in_month,");
                                FieldSalarySlipHead.Append("other_payout_money_in_month as other_payout_money_in_month,");
                                FieldSalarySlipHead.Append("sow_money_in_month as sow_money_in_month,");
                                FieldSalarySlipHead.Append("leave_money_in_month as leave_money_in_month,");
                                FieldSalarySlipHead.Append("late_money_in_month as late_money_in_month,");
                                FieldSalarySlipHead.Append("ot_money_in_month as ot_money_in_month,");

                                FieldSalarySlipData.Append("(select " + _g._dataPayroll.payroll_side_list._name + " from " + _g._dataPayroll.payroll_side_list._table + " where " + _g._dataPayroll.payroll_side_list._table + "." + _g._dataPayroll.payroll_side_list._code + "= " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._side_code + ") as side_name,");
                                FieldSalarySlipData.Append("(select " + _g._dataPayroll.payroll_section_list._name + " from " + _g._dataPayroll.payroll_section_list._table + " where " + _g._dataPayroll.payroll_section_list._table + "." + _g._dataPayroll.payroll_section_list._code + "= " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._section_code + ")  as section_name,");
                                FieldSalarySlipData.Append("(select " + _g._dataPayroll.payroll_work_title._name + " from " + _g._dataPayroll.payroll_work_title._table + " where " + _g._dataPayroll.payroll_work_title._table + "." + _g._dataPayroll.payroll_work_title._code + "= " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._work_title + ")  as work_title_name,");
                                FieldSalarySlipData.Append("" + _g._dataPayroll.payroll_employee._book_bank_no + ",");
                                    //จำนวนวันทำงาน
                                FieldSalarySlipData.Append("coalesce((select sum(" + _g._dataPayroll.payroll_trans._working_day + ") from " + _g._dataPayroll.payroll_trans._table + " where " + _g._dataPayroll.payroll_trans._employee + "=" + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + " " + WhereTransSub + "),0) as working_day,");
                                    //เงินหัก เงินได้
                                FieldSalarySlipData.Append(" coalesce((select sum(" + _g._dataPayroll.payroll_trans_detail._diligent_money + ") from " + _g._dataPayroll.payroll_trans_detail._table + " where " + _g._dataPayroll.payroll_trans_detail._trans_flag + "=1 and " + _g._dataPayroll.payroll_trans_detail._trans_type + "=5 and " + _g._dataPayroll.payroll_trans_detail._employee + " =  " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + " " + WhereTransDetailInMonth + "),0) as diligent_money_in_month,");
                                FieldSalarySlipData.Append(" coalesce((select sum(" + _g._dataPayroll.payroll_trans_detail._prepay_money + ") from " + _g._dataPayroll.payroll_trans_detail._table + " where " + _g._dataPayroll.payroll_trans_detail._trans_flag + "=1 and " + _g._dataPayroll.payroll_trans_detail._trans_type + "=5 and " + _g._dataPayroll.payroll_trans_detail._employee + " =  " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + " " + WhereTransDetailInMonth + "),0) as prepay_money_in_month,");
                                FieldSalarySlipData.Append(" coalesce((select sum(" + _g._dataPayroll.payroll_trans_detail._bonus_money + ") from " + _g._dataPayroll.payroll_trans_detail._table + " where " + _g._dataPayroll.payroll_trans_detail._trans_flag + "=1 and " + _g._dataPayroll.payroll_trans_detail._trans_type + "=5 and " + _g._dataPayroll.payroll_trans_detail._employee + " =  " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + " " + WhereTransDetailInMonth + "),0) as bonus_money_in_month,");
                                FieldSalarySlipData.Append(" coalesce((select sum(" + _g._dataPayroll.payroll_trans_detail._other_income_money + ") from " + _g._dataPayroll.payroll_trans_detail._table + " where " + _g._dataPayroll.payroll_trans_detail._trans_flag + "=1 and " + _g._dataPayroll.payroll_trans_detail._trans_type + "=5 and " + _g._dataPayroll.payroll_trans_detail._employee + " =  " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + " " + WhereTransDetailInMonth + "),0) as other_income_money_in_month,");
                                FieldSalarySlipData.Append(" coalesce((select sum(" + _g._dataPayroll.payroll_trans_detail._advance_money + ") from " + _g._dataPayroll.payroll_trans_detail._table + " where " + _g._dataPayroll.payroll_trans_detail._trans_flag + "=1 and " + _g._dataPayroll.payroll_trans_detail._trans_type + "=5 and " + _g._dataPayroll.payroll_trans_detail._employee + " =  " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + " " + WhereTransDetailInMonth + "),0) as advance_money_in_month,");
                                FieldSalarySlipData.Append(" coalesce((select sum(" + _g._dataPayroll.payroll_trans_detail._insure_money + ") from " + _g._dataPayroll.payroll_trans_detail._table + " where " + _g._dataPayroll.payroll_trans_detail._trans_flag + "=1 and " + _g._dataPayroll.payroll_trans_detail._trans_type + "=5 and " + _g._dataPayroll.payroll_trans_detail._employee + " =  " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + " " + WhereTransDetailInMonth + "),0) as insure_money_in_month,");
                                FieldSalarySlipData.Append(" coalesce((select sum(" + _g._dataPayroll.payroll_trans_detail._other_payout_money + ") from " + _g._dataPayroll.payroll_trans_detail._table + " where " + _g._dataPayroll.payroll_trans_detail._trans_flag + "=1 and " + _g._dataPayroll.payroll_trans_detail._trans_type + "=5 and " + _g._dataPayroll.payroll_trans_detail._employee + " =  " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + " " + WhereTransDetailInMonth + "),0) as other_payout_money_in_month,");
                                FieldSalarySlipData.Append(" coalesce((select sum(" + _g._dataPayroll.payroll_trans_detail._money + ") from " + _g._dataPayroll.payroll_trans_detail._table + " where " + _g._dataPayroll.payroll_trans_detail._trans_flag + "=1 and " + _g._dataPayroll.payroll_trans_detail._trans_type + "=1 and " + _g._dataPayroll.payroll_trans_detail._employee + " =  " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + " " + WhereTransDetailInMonth + "),0) as sow_money_in_month,");
                                FieldSalarySlipData.Append(" coalesce((select sum(" + _g._dataPayroll.payroll_trans_detail._money + ") from " + _g._dataPayroll.payroll_trans_detail._table + " where " + _g._dataPayroll.payroll_trans_detail._trans_flag + "=1 and " + _g._dataPayroll.payroll_trans_detail._trans_type + "=2 and " + _g._dataPayroll.payroll_trans_detail._employee + " =  " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + " " + WhereTransDetailInMonth + "),0) as leave_money_in_month,");
                                FieldSalarySlipData.Append(" coalesce((select sum(" + _g._dataPayroll.payroll_trans_detail._money + ") from " + _g._dataPayroll.payroll_trans_detail._table + " where " + _g._dataPayroll.payroll_trans_detail._trans_flag + "=1 and " + _g._dataPayroll.payroll_trans_detail._trans_type + "=3 and " + _g._dataPayroll.payroll_trans_detail._employee + " =  " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + " " + WhereTransDetailInMonth + "),0) as late_money_in_month,");
                                FieldSalarySlipData.Append(" coalesce((select sum(" + _g._dataPayroll.payroll_trans_detail._money + ") from " + _g._dataPayroll.payroll_trans_detail._table + " where " + _g._dataPayroll.payroll_trans_detail._trans_flag + "=1 and " + _g._dataPayroll.payroll_trans_detail._trans_type + "=4 and " + _g._dataPayroll.payroll_trans_detail._employee + " =  " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + " " + WhereTransDetailInMonth + "),0) as ot_money_in_month,");
                                    //คิดกองทุนสำรองฯ

                                FieldSalarySlipData.Append(" coalesce((");
                                FieldSalarySlipData.Append(" CASE " + _g._dataPayroll.payroll_employee._employee_type);
                                FieldSalarySlipData.Append(" WHEN 0 THEN ");
                                FieldSalarySlipData.Append("((select coalesce((" + _g._dataPayroll.payroll_employee_salary._salary + "),0) as " + _g._dataPayroll.payroll_employee_salary._salary);
                                FieldSalarySlipData.Append(" from " + _g._dataPayroll.payroll_employee_salary._table);
                                FieldSalarySlipData.Append(" where " + _g._dataPayroll.payroll_employee_salary._code + " = " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + "");
                                FieldSalarySlipData.Append(" and " + _g._dataPayroll.payroll_employee_salary._start_date + " <= '" + StartDay + "'");
                                FieldSalarySlipData.Append(" and (" + _g._dataPayroll.payroll_employee_salary._end_date + " >= '" + EndDay + "' or " + _g._dataPayroll.payroll_employee_salary._end_date + " = '1000-01-01'))  * ((select " + _g._dataPayroll.payroll_company_config._customer_rf_rate + " from " + _g._dataPayroll.payroll_company_config._table + " ) / 100))");
                                FieldSalarySlipData.Append(" WHEN 1 THEN ");
                                FieldSalarySlipData.Append("((");
                                FieldSalarySlipData.Append("select coalesce((" + _g._dataPayroll.payroll_employee_salary._salary + "),0) as " + _g._dataPayroll.payroll_employee_salary._salary);
                                FieldSalarySlipData.Append(" from " + _g._dataPayroll.payroll_employee_salary._table);
                                FieldSalarySlipData.Append(" where " + _g._dataPayroll.payroll_employee_salary._code + " = " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + "");
                                FieldSalarySlipData.Append(" and " + _g._dataPayroll.payroll_employee_salary._start_date + " <= '" + StartDay + "'");
                                FieldSalarySlipData.Append(" and (" + _g._dataPayroll.payroll_employee_salary._end_date + " >= '" + EndDay + "' or " + _g._dataPayroll.payroll_employee_salary._end_date + " = '1000-01-01')");
                                FieldSalarySlipData.Append(") * 4) * ((select " + _g._dataPayroll.payroll_company_config._customer_rf_rate + " from " + _g._dataPayroll.payroll_company_config._table + " ) / 100)");
                                FieldSalarySlipData.Append(" ELSE ");
                                FieldSalarySlipData.Append("((");
                                FieldSalarySlipData.Append("select coalesce((" + _g._dataPayroll.payroll_employee_salary._salary + "),0) as " + _g._dataPayroll.payroll_employee_salary._salary);
                                FieldSalarySlipData.Append(" from " + _g._dataPayroll.payroll_employee_salary._table);
                                FieldSalarySlipData.Append(" where " + _g._dataPayroll.payroll_employee_salary._code + " = " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + "");
                                FieldSalarySlipData.Append(" and " + _g._dataPayroll.payroll_employee_salary._start_date + " <= '" + StartDay + "'");
                                FieldSalarySlipData.Append(" and (" + _g._dataPayroll.payroll_employee_salary._end_date + " >= '" + EndDay + "' or " + _g._dataPayroll.payroll_employee_salary._end_date + " = '1000-01-01')");
                                FieldSalarySlipData.Append(") * (select sum(" + _g._dataPayroll.payroll_trans._working_day + ") from " + _g._dataPayroll.payroll_trans._table + " where " + _g._dataPayroll.payroll_trans._employee + "=" + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + " " + WhereTransSubInMonth + ")) * ((select " + _g._dataPayroll.payroll_company_config._customer_rf_rate + " from " + _g._dataPayroll.payroll_company_config._table + " ) / 100)");
                                FieldSalarySlipData.Append(" END");
                                FieldSalarySlipData.Append(" ),0) as reserve_money_in_month_vary,");

                                FieldSalarySlipData.Append(" coalesce(( ");
                                FieldSalarySlipData.Append(" CASE " + _g._dataPayroll.payroll_employee._employee_type);
                                FieldSalarySlipData.Append(" WHEN 0 THEN (" + _g._dataPayroll.payroll_employee._salary + " * ((select " + _g._dataPayroll.payroll_company_config._customer_rf_rate + " from " + _g._dataPayroll.payroll_company_config._table + " ) / 100))");
                                FieldSalarySlipData.Append(" WHEN 1 THEN ((" + _g._dataPayroll.payroll_employee._salary + " * 4) * ((select " + _g._dataPayroll.payroll_company_config._customer_rf_rate + " from " + _g._dataPayroll.payroll_company_config._table + " ) / 100))");
                                FieldSalarySlipData.Append(" ELSE ((" + _g._dataPayroll.payroll_employee._salary + " * (select sum(" + _g._dataPayroll.payroll_trans._working_day + ") from " + _g._dataPayroll.payroll_trans._table + " where " + _g._dataPayroll.payroll_trans._employee + "=" + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + " " + WhereTransSubInMonth + ")) * ((select " + _g._dataPayroll.payroll_company_config._customer_rf_rate + " from " + _g._dataPayroll.payroll_company_config._table + " ) / 100))");
                                FieldSalarySlipData.Append(" END");
                                FieldSalarySlipData.Append("),0) as reserve_money_in_month,");

                                     //คิดกองทุนสำรองฯ
                                FieldSalarySlipData.Append(" coalesce(( ");
                                FieldSalarySlipData.Append(" CASE " + _g._dataPayroll.payroll_employee._employee_type);
                                FieldSalarySlipData.Append(" WHEN 0 THEN ");
                                FieldSalarySlipData.Append("((select coalesce((" + _g._dataPayroll.payroll_employee_salary._salary + "),0) as " + _g._dataPayroll.payroll_employee_salary._salary);
                                FieldSalarySlipData.Append(" from " + _g._dataPayroll.payroll_employee_salary._table);
                                FieldSalarySlipData.Append(" where " + _g._dataPayroll.payroll_employee_salary._code + " = " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + "");
                                FieldSalarySlipData.Append(" and " + _g._dataPayroll.payroll_employee_salary._start_date + " <= '" + FromDate + "'");
                                FieldSalarySlipData.Append(" and (" + _g._dataPayroll.payroll_employee_salary._end_date + " >= '" + ToDate + "' or " + _g._dataPayroll.payroll_employee_salary._end_date + " = '1000-01-01'))  * ((select " + _g._dataPayroll.payroll_company_config._customer_rf_rate + " from " + _g._dataPayroll.payroll_company_config._table + " ) / 100))");
                                FieldSalarySlipData.Append(" WHEN 1 THEN ");
                                FieldSalarySlipData.Append("((");
                                FieldSalarySlipData.Append("select coalesce((" + _g._dataPayroll.payroll_employee_salary._salary + "),0) as " + _g._dataPayroll.payroll_employee_salary._salary);
                                FieldSalarySlipData.Append(" from " + _g._dataPayroll.payroll_employee_salary._table);
                                FieldSalarySlipData.Append(" where " + _g._dataPayroll.payroll_employee_salary._code + " = " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + "");
                                FieldSalarySlipData.Append(" and " + _g._dataPayroll.payroll_employee_salary._start_date + " <= '" + FromDate + "'");
                                FieldSalarySlipData.Append(" and (" + _g._dataPayroll.payroll_employee_salary._end_date + " >= '" + ToDate + "' or " + _g._dataPayroll.payroll_employee_salary._end_date + " = '1000-01-01')");
                                FieldSalarySlipData.Append(") * 4) * ((select " + _g._dataPayroll.payroll_company_config._customer_rf_rate + " from " + _g._dataPayroll.payroll_company_config._table + " ) / 100)");
                                FieldSalarySlipData.Append(" ELSE ");
                                FieldSalarySlipData.Append("((");
                                FieldSalarySlipData.Append("select coalesce((" + _g._dataPayroll.payroll_employee_salary._salary + "),0) as " + _g._dataPayroll.payroll_employee_salary._salary);
                                FieldSalarySlipData.Append(" from " + _g._dataPayroll.payroll_employee_salary._table);
                                FieldSalarySlipData.Append(" where " + _g._dataPayroll.payroll_employee_salary._code + " = " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + "");
                                FieldSalarySlipData.Append(" and " + _g._dataPayroll.payroll_employee_salary._start_date + " <= '" + FromDate + "'");
                                FieldSalarySlipData.Append(" and (" + _g._dataPayroll.payroll_employee_salary._end_date + " >= '" + ToDate + "' or " + _g._dataPayroll.payroll_employee_salary._end_date + " = '1000-01-01')");
                                FieldSalarySlipData.Append(") * (select sum(" + _g._dataPayroll.payroll_trans._working_day + ") from " + _g._dataPayroll.payroll_trans._table + " where " + _g._dataPayroll.payroll_trans._employee + "=" + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + " " + WhereTransSub + ")) * ((select " + _g._dataPayroll.payroll_company_config._customer_rf_rate + " from " + _g._dataPayroll.payroll_company_config._table + " ) / 100)");
                                FieldSalarySlipData.Append(" END");
                                FieldSalarySlipData.Append(" ),0) as reserve_money_vary,");

                                FieldSalarySlipData.Append(" coalesce(( ");
                                FieldSalarySlipData.Append(" CASE " + _g._dataPayroll.payroll_employee._employee_type);
                                FieldSalarySlipData.Append(" WHEN 0 THEN (" + _g._dataPayroll.payroll_employee._salary + " * ((select " + _g._dataPayroll.payroll_company_config._customer_rf_rate + " from " + _g._dataPayroll.payroll_company_config._table + " ) / 100))");
                                FieldSalarySlipData.Append(" WHEN 1 THEN ((" + _g._dataPayroll.payroll_employee._salary + " * 4 ) * ((select " + _g._dataPayroll.payroll_company_config._customer_rf_rate + " from " + _g._dataPayroll.payroll_company_config._table + " ) / 100))");
                                FieldSalarySlipData.Append(" ELSE ((" + _g._dataPayroll.payroll_employee._salary + " * (select sum(" + _g._dataPayroll.payroll_trans._working_day + ") from " + _g._dataPayroll.payroll_trans._table + " where " + _g._dataPayroll.payroll_trans._employee + "=" + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + " " + WhereTransSub + ")) * ((select " + _g._dataPayroll.payroll_company_config._customer_rf_rate + " from " + _g._dataPayroll.payroll_company_config._table + " ) / 100))");
                                FieldSalarySlipData.Append(" END");
                                FieldSalarySlipData.Append(" ),0) as " + _g._dataPayroll.payroll_trans._reserve_money + ",");

                                    //คิดประกันสังคม

                                FieldSalarySlipData.Append(" coalesce(( ");
                                FieldSalarySlipData.Append(" CASE " + _g._dataPayroll.payroll_employee._employee_type);
                                FieldSalarySlipData.Append(" WHEN 0 THEN ");
                                FieldSalarySlipData.Append("((select coalesce((" + _g._dataPayroll.payroll_employee_salary._salary + "),0) as " + _g._dataPayroll.payroll_employee_salary._salary);
                                FieldSalarySlipData.Append(" from " + _g._dataPayroll.payroll_employee_salary._table);
                                FieldSalarySlipData.Append(" where " + _g._dataPayroll.payroll_employee_salary._code + " = " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + "");
                                FieldSalarySlipData.Append(" and " + _g._dataPayroll.payroll_employee_salary._start_date + " <= '" + StartDay + "'");
                                FieldSalarySlipData.Append(" and (" + _g._dataPayroll.payroll_employee_salary._end_date + " >= '" + EndDay + "' or " + _g._dataPayroll.payroll_employee_salary._end_date + " = '1000-01-01'))  * ((select " + _g._dataPayroll.payroll_company_config._customer_ss_rate + " from " + _g._dataPayroll.payroll_company_config._table + " ) / 100))");
                                FieldSalarySlipData.Append(" WHEN 1 THEN ");
                                FieldSalarySlipData.Append("((");
                                FieldSalarySlipData.Append("select coalesce((" + _g._dataPayroll.payroll_employee_salary._salary + "),0) as " + _g._dataPayroll.payroll_employee_salary._salary);
                                FieldSalarySlipData.Append(" from " + _g._dataPayroll.payroll_employee_salary._table);
                                FieldSalarySlipData.Append(" where " + _g._dataPayroll.payroll_employee_salary._code + " = " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + "");
                                FieldSalarySlipData.Append(" and " + _g._dataPayroll.payroll_employee_salary._start_date + " <= '" + StartDay + "'");
                                FieldSalarySlipData.Append(" and (" + _g._dataPayroll.payroll_employee_salary._end_date + " >= '" + EndDay + "' or " + _g._dataPayroll.payroll_employee_salary._end_date + " = '1000-01-01')");
                                FieldSalarySlipData.Append(") * 4) * ((select " + _g._dataPayroll.payroll_company_config._customer_ss_rate + " from " + _g._dataPayroll.payroll_company_config._table + " ) / 100)");
                                FieldSalarySlipData.Append(" ELSE ");
                                FieldSalarySlipData.Append("((");
                                FieldSalarySlipData.Append("select coalesce((" + _g._dataPayroll.payroll_employee_salary._salary + "),0) as " + _g._dataPayroll.payroll_employee_salary._salary);
                                FieldSalarySlipData.Append(" from " + _g._dataPayroll.payroll_employee_salary._table);
                                FieldSalarySlipData.Append(" where " + _g._dataPayroll.payroll_employee_salary._code + " = " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + "");
                                FieldSalarySlipData.Append(" and " + _g._dataPayroll.payroll_employee_salary._start_date + " <= '" + StartDay + "'");
                                FieldSalarySlipData.Append(" and (" + _g._dataPayroll.payroll_employee_salary._end_date + " >= '" + EndDay + "' or " + _g._dataPayroll.payroll_employee_salary._end_date + " = '1000-01-01')");
                                FieldSalarySlipData.Append(") * (select sum(" + _g._dataPayroll.payroll_trans._working_day + ") from " + _g._dataPayroll.payroll_trans._table + " where " + _g._dataPayroll.payroll_trans._employee + "=" + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + " " + WhereTransSubInMonth + ")) * ((select " + _g._dataPayroll.payroll_company_config._customer_ss_rate + " from " + _g._dataPayroll.payroll_company_config._table + " ) / 100)");
                                FieldSalarySlipData.Append(" END");
                                FieldSalarySlipData.Append("),0) as society_insure_money_in_month_vary,");

                                FieldSalarySlipData.Append(" coalesce(( ");
                                FieldSalarySlipData.Append(" CASE " + _g._dataPayroll.payroll_employee._employee_type);
                                FieldSalarySlipData.Append(" WHEN 0 THEN (" + _g._dataPayroll.payroll_employee._salary + " * ((select " + _g._dataPayroll.payroll_company_config._customer_ss_rate + " from " + _g._dataPayroll.payroll_company_config._table + " ) / 100))");
                                FieldSalarySlipData.Append(" WHEN 1 THEN ((" + _g._dataPayroll.payroll_employee._salary + " * 4 ) * ((select " + _g._dataPayroll.payroll_company_config._customer_ss_rate + " from " + _g._dataPayroll.payroll_company_config._table + " ) / 100))");
                                FieldSalarySlipData.Append(" ELSE ((" + _g._dataPayroll.payroll_employee._salary + " * (select sum(" + _g._dataPayroll.payroll_trans._working_day + ") from " + _g._dataPayroll.payroll_trans._table + " where " + _g._dataPayroll.payroll_trans._employee + "=" + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + " " + WhereTransSubInMonth + ")) * ((select " + _g._dataPayroll.payroll_company_config._customer_ss_rate + " from " + _g._dataPayroll.payroll_company_config._table + " ) / 100))");
                                FieldSalarySlipData.Append(" END");
                                FieldSalarySlipData.Append(" ),0) as society_insure_money_in_month,");

                                      //คิดประกันสังคม

                                FieldSalarySlipData.Append(" coalesce(( ");
                                FieldSalarySlipData.Append(" CASE " + _g._dataPayroll.payroll_employee._employee_type);
                                FieldSalarySlipData.Append(" WHEN 0 THEN ");
                                FieldSalarySlipData.Append("((select coalesce((" + _g._dataPayroll.payroll_employee_salary._salary + "),0) as " + _g._dataPayroll.payroll_employee_salary._salary);
                                FieldSalarySlipData.Append(" from " + _g._dataPayroll.payroll_employee_salary._table);
                                FieldSalarySlipData.Append(" where " + _g._dataPayroll.payroll_employee_salary._code + " = " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + "");
                                FieldSalarySlipData.Append(" and " + _g._dataPayroll.payroll_employee_salary._start_date + " <= '" + FromDate + "'");
                                FieldSalarySlipData.Append(" and (" + _g._dataPayroll.payroll_employee_salary._end_date + " >= '" + ToDate + "' or " + _g._dataPayroll.payroll_employee_salary._end_date + " = '1000-01-01'))  * ((select " + _g._dataPayroll.payroll_company_config._customer_ss_rate + " from " + _g._dataPayroll.payroll_company_config._table + " ) / 100))");
                                FieldSalarySlipData.Append(" WHEN 1 THEN ");
                                FieldSalarySlipData.Append("((");
                                FieldSalarySlipData.Append("select coalesce((" + _g._dataPayroll.payroll_employee_salary._salary + "),0) as " + _g._dataPayroll.payroll_employee_salary._salary);
                                FieldSalarySlipData.Append(" from " + _g._dataPayroll.payroll_employee_salary._table);
                                FieldSalarySlipData.Append(" where " + _g._dataPayroll.payroll_employee_salary._code + " = " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + "");
                                FieldSalarySlipData.Append(" and " + _g._dataPayroll.payroll_employee_salary._start_date + " <= '" + FromDate + "'");
                                FieldSalarySlipData.Append(" and (" + _g._dataPayroll.payroll_employee_salary._end_date + " >= '" + ToDate + "' or " + _g._dataPayroll.payroll_employee_salary._end_date + " = '1000-01-01')");
                                FieldSalarySlipData.Append(") * 4) * ((select " + _g._dataPayroll.payroll_company_config._customer_ss_rate + " from " + _g._dataPayroll.payroll_company_config._table + " ) / 100)");
                                FieldSalarySlipData.Append(" ELSE ");
                                FieldSalarySlipData.Append("((");
                                FieldSalarySlipData.Append("select coalesce((" + _g._dataPayroll.payroll_employee_salary._salary + "),0) as " + _g._dataPayroll.payroll_employee_salary._salary);
                                FieldSalarySlipData.Append(" from " + _g._dataPayroll.payroll_employee_salary._table);
                                FieldSalarySlipData.Append(" where " + _g._dataPayroll.payroll_employee_salary._code + " = " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + "");
                                FieldSalarySlipData.Append(" and " + _g._dataPayroll.payroll_employee_salary._start_date + " <= '" + FromDate + "'");
                                FieldSalarySlipData.Append(" and (" + _g._dataPayroll.payroll_employee_salary._end_date + " >= '" + ToDate + "' or " + _g._dataPayroll.payroll_employee_salary._end_date + " = '1000-01-01')");
                                FieldSalarySlipData.Append(") * (select sum(" + _g._dataPayroll.payroll_trans._working_day + ") from " + _g._dataPayroll.payroll_trans._table + " where " + _g._dataPayroll.payroll_trans._employee + "=" + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + " " + WhereTransSub + ")) * ((select " + _g._dataPayroll.payroll_company_config._customer_ss_rate + " from " + _g._dataPayroll.payroll_company_config._table + " ) / 100)");
                                FieldSalarySlipData.Append(" END");
                                FieldSalarySlipData.Append("),0) as society_insure_money_vary,");

                                FieldSalarySlipData.Append(" coalesce(( ");
                                FieldSalarySlipData.Append(" CASE " + _g._dataPayroll.payroll_employee._employee_type);
                                FieldSalarySlipData.Append(" WHEN 0 THEN (" + _g._dataPayroll.payroll_employee._salary + " * ((select " + _g._dataPayroll.payroll_company_config._customer_ss_rate + " from " + _g._dataPayroll.payroll_company_config._table + " ) / 100))");
                                FieldSalarySlipData.Append(" WHEN 1 THEN ((" + _g._dataPayroll.payroll_employee._salary + " * 4 ) * ((select " + _g._dataPayroll.payroll_company_config._customer_ss_rate + " from " + _g._dataPayroll.payroll_company_config._table + " ) / 100))");
                                FieldSalarySlipData.Append(" ELSE ((" + _g._dataPayroll.payroll_employee._salary + " * (select sum(" + _g._dataPayroll.payroll_trans._working_day + ") from " + _g._dataPayroll.payroll_trans._table + " where " + _g._dataPayroll.payroll_trans._employee + "=" + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + " " + WhereTransSub + ")) * ((select " + _g._dataPayroll.payroll_company_config._customer_ss_rate + " from " + _g._dataPayroll.payroll_company_config._table + " ) / 100))");
                                FieldSalarySlipData.Append(" END");
                                FieldSalarySlipData.Append(" ),0) as " + _g._dataPayroll.payroll_trans._society_insure_money + ",");
                                    //คิดค่าแรง    
                                FieldSalarySlipData.Append(" coalesce(( ");
                                FieldSalarySlipData.Append(" CASE " + _g._dataPayroll.payroll_employee._employee_type);
                                FieldSalarySlipData.Append(" WHEN 0 THEN ");
                                FieldSalarySlipData.Append("(select coalesce((" + _g._dataPayroll.payroll_employee_salary._salary + "),0) as " + _g._dataPayroll.payroll_employee_salary._salary);
                                FieldSalarySlipData.Append(" from " + _g._dataPayroll.payroll_employee_salary._table);
                                FieldSalarySlipData.Append(" where " + _g._dataPayroll.payroll_employee_salary._code + " = " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + "");
                                FieldSalarySlipData.Append(" and " + _g._dataPayroll.payroll_employee_salary._start_date + " <= '" + StartDay + "'");
                                FieldSalarySlipData.Append(" and (" + _g._dataPayroll.payroll_employee_salary._end_date + " >= '" + EndDay + "' or " + _g._dataPayroll.payroll_employee_salary._end_date + " = '1000-01-01'))");
                                FieldSalarySlipData.Append(" WHEN 1 THEN ");
                                FieldSalarySlipData.Append("((");
                                FieldSalarySlipData.Append("select coalesce((" + _g._dataPayroll.payroll_employee_salary._salary + "),0) as " + _g._dataPayroll.payroll_employee_salary._salary);
                                FieldSalarySlipData.Append(" from " + _g._dataPayroll.payroll_employee_salary._table);
                                FieldSalarySlipData.Append(" where " + _g._dataPayroll.payroll_employee_salary._code + " = " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + "");
                                FieldSalarySlipData.Append(" and " + _g._dataPayroll.payroll_employee_salary._start_date + " <= '" + StartDay + "'");
                                FieldSalarySlipData.Append(" and (" + _g._dataPayroll.payroll_employee_salary._end_date + " >= '" + EndDay + "' or " + _g._dataPayroll.payroll_employee_salary._end_date + " = '1000-01-01')");
                                FieldSalarySlipData.Append(") * 4)");
                                FieldSalarySlipData.Append(" ELSE ");
                                FieldSalarySlipData.Append("((");
                                FieldSalarySlipData.Append("select coalesce((" + _g._dataPayroll.payroll_employee_salary._salary + "),0) as " + _g._dataPayroll.payroll_employee_salary._salary);
                                FieldSalarySlipData.Append(" from " + _g._dataPayroll.payroll_employee_salary._table);
                                FieldSalarySlipData.Append(" where " + _g._dataPayroll.payroll_employee_salary._code + " = " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + "");
                                FieldSalarySlipData.Append(" and " + _g._dataPayroll.payroll_employee_salary._start_date + " <= '" + StartDay + "'");
                                FieldSalarySlipData.Append(" and (" + _g._dataPayroll.payroll_employee_salary._end_date + " >= '" + EndDay + "' or " + _g._dataPayroll.payroll_employee_salary._end_date + " = '1000-01-01')");
                                FieldSalarySlipData.Append(") * (select sum(" + _g._dataPayroll.payroll_trans._working_day + ") from " + _g._dataPayroll.payroll_trans._table + " where " + _g._dataPayroll.payroll_trans._employee + "=" + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + " " + WhereTransSubInMonth + "))");
                                FieldSalarySlipData.Append(" END");
                                FieldSalarySlipData.Append(" ),0) as wages_money_in_month_vary,");

                                FieldSalarySlipData.Append(" coalesce(( ");
                                FieldSalarySlipData.Append(" CASE " + _g._dataPayroll.payroll_employee._employee_type);
                                FieldSalarySlipData.Append(" WHEN 0 THEN " + _g._dataPayroll.payroll_employee._salary);
                                FieldSalarySlipData.Append(" WHEN 1 THEN (" + _g._dataPayroll.payroll_employee._salary + " * 4)");
                                FieldSalarySlipData.Append(" ELSE (" + _g._dataPayroll.payroll_employee._salary + "*(select sum(" + _g._dataPayroll.payroll_trans._working_day + ") from " + _g._dataPayroll.payroll_trans._table + " where " + _g._dataPayroll.payroll_trans._employee + "=" + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + " " + WhereTransSubInMonth + "))");
                                FieldSalarySlipData.Append(" END");
                                FieldSalarySlipData.Append(" ),0) as wages_money_in_month,");

                                       //คิดค่าแรง    
                                FieldSalarySlipData.Append(" coalesce(( ");
                                FieldSalarySlipData.Append(" CASE " + _g._dataPayroll.payroll_employee._employee_type);
                                FieldSalarySlipData.Append(" WHEN 0 THEN ");
                                FieldSalarySlipData.Append("(select coalesce((" + _g._dataPayroll.payroll_employee_salary._salary + "),0) as " + _g._dataPayroll.payroll_employee_salary._salary);
                                FieldSalarySlipData.Append(" from " + _g._dataPayroll.payroll_employee_salary._table);
                                FieldSalarySlipData.Append(" where " + _g._dataPayroll.payroll_employee_salary._code + " = " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + "");
                                FieldSalarySlipData.Append(" and " + _g._dataPayroll.payroll_employee_salary._start_date + " <= '" + FromDate + "'");
                                FieldSalarySlipData.Append(" and (" + _g._dataPayroll.payroll_employee_salary._end_date + " >= '" + ToDate + "' or " + _g._dataPayroll.payroll_employee_salary._end_date + " = '1000-01-01'))");
                                FieldSalarySlipData.Append(" WHEN 1 THEN ");
                                FieldSalarySlipData.Append("((");
                                FieldSalarySlipData.Append("select coalesce((" + _g._dataPayroll.payroll_employee_salary._salary + "),0) as " + _g._dataPayroll.payroll_employee_salary._salary);
                                FieldSalarySlipData.Append(" from " + _g._dataPayroll.payroll_employee_salary._table);
                                FieldSalarySlipData.Append(" where " + _g._dataPayroll.payroll_employee_salary._code + " = " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + "");
                                FieldSalarySlipData.Append(" and " + _g._dataPayroll.payroll_employee_salary._start_date + " <= '" + FromDate + "'");
                                FieldSalarySlipData.Append(" and (" + _g._dataPayroll.payroll_employee_salary._end_date + " >= '" + ToDate + "' or " + _g._dataPayroll.payroll_employee_salary._end_date + " = '1000-01-01')");
                                FieldSalarySlipData.Append(") * 4)");
                                FieldSalarySlipData.Append(" ELSE ");
                                FieldSalarySlipData.Append("((");
                                FieldSalarySlipData.Append("select coalesce((" + _g._dataPayroll.payroll_employee_salary._salary + "),0) as " + _g._dataPayroll.payroll_employee_salary._salary);
                                FieldSalarySlipData.Append(" from " + _g._dataPayroll.payroll_employee_salary._table);
                                FieldSalarySlipData.Append(" where " + _g._dataPayroll.payroll_employee_salary._code + " = " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + "");
                                FieldSalarySlipData.Append(" and " + _g._dataPayroll.payroll_employee_salary._start_date + " <= '" + FromDate + "'");
                                FieldSalarySlipData.Append(" and (" + _g._dataPayroll.payroll_employee_salary._end_date + " >= '" + ToDate + "' or " + _g._dataPayroll.payroll_employee_salary._end_date + " = '1000-01-01')");
                                FieldSalarySlipData.Append(") * (select sum(" + _g._dataPayroll.payroll_trans._working_day + ") from " + _g._dataPayroll.payroll_trans._table + " where " + _g._dataPayroll.payroll_trans._employee + "=" + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + " " + WhereTransSub + "))");
                                FieldSalarySlipData.Append(" END");
                                FieldSalarySlipData.Append(" ),0) as wages_money_vary,");

                                       //คิดค่าแรง    
                                FieldSalarySlipData.Append(" coalesce(( ");
                                FieldSalarySlipData.Append(" CASE " + _g._dataPayroll.payroll_employee._employee_type);
                                FieldSalarySlipData.Append(" WHEN 0 THEN " + _g._dataPayroll.payroll_employee._salary);
                                FieldSalarySlipData.Append(" WHEN 1 THEN (" + _g._dataPayroll.payroll_employee._salary + " * 4)");
                                FieldSalarySlipData.Append(" ELSE (" + _g._dataPayroll.payroll_employee._salary + "*(select sum(" + _g._dataPayroll.payroll_trans._working_day + ") from " + _g._dataPayroll.payroll_trans._table + " where " + _g._dataPayroll.payroll_trans._employee + "=" + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + " " + WhereTransSub + "))");
                                FieldSalarySlipData.Append(" END");
                                FieldSalarySlipData.Append(" ),0) as " + _g._dataPayroll.payroll_trans._wages_money + ",");

                                break;
                            case _g.g._screenPayrollEnum.รายงานภาษีเงินได้_รายเดือน:
                            case _g.g._screenPayrollEnum.ภงด_1:
                            case _g.g._screenPayrollEnum.ภงด_1_ใบแนบ:
                            case _g.g._screenPayrollEnum.คำนวนภาษีรายเดือน:
                                FieldEmployeeHead.Append("title_name as title_name,");
                                FieldEmployeeHead.Append("tax_no as tax_no,");
                                FieldEmployeeHead.Append("id_code as id_code,");
                                FieldEmployeeHead.Append("house as house,");
                                FieldEmployeeHead.Append("room_no as room_no,");
                                FieldEmployeeHead.Append("floor_no as floor_no,");
                                FieldEmployeeHead.Append("village as village,");
                                FieldEmployeeHead.Append("house_no as house_no,");
                                FieldEmployeeHead.Append("crowd_no as crowd_no,");
                                FieldEmployeeHead.Append("lane as lane,");
                                FieldEmployeeHead.Append("road as road,");
                                FieldEmployeeHead.Append("locality as locality,");
                                FieldEmployeeHead.Append("amphur as amphur,");
                                FieldEmployeeHead.Append("province as province,");
                                FieldEmployeeHead.Append("postcode as postcode,");
                                FieldEmployeeHead.Append("telephone as telephone,");
                                FieldEmployeeHead.Append("birth_day as birth_day,");
                                FieldEmployeeHead.Append("spouse_name as spouse_name,");
                                FieldEmployeeHead.Append("spouse_id_code as spouse_id_code,");
                                FieldEmployeeHead.Append("spouse_birth_day as spouse_birth_day,");
                                FieldEmployeeHead.Append("father_id as father_id,");
                                FieldEmployeeHead.Append("mother_id as mother_id,");
                                FieldEmployeeHead.Append("father_law_id as father_law_id,");
                                FieldEmployeeHead.Append("mother_law_id as mother_law_id,");
                                FieldEmployeeHead.Append("child_study_qty as child_study_qty,");
                                FieldEmployeeHead.Append("child_no_study_qty as child_no_study_qty,");

                                FieldEmployeeData.Append("(title_name) as title_name,");
                                FieldEmployeeData.Append("(tax_no) as tax_no,");
                                FieldEmployeeData.Append("(select id_code from payroll_employee_detail where payroll_employee_detail.code = payroll_employee.code) as id_code,");
                                FieldEmployeeData.Append("(select house from payroll_employee_address where payroll_employee_address.code = payroll_employee.code) as house,");
                                FieldEmployeeData.Append("(select room_no from payroll_employee_address where payroll_employee_address.code = payroll_employee.code) as room_no,");
                                FieldEmployeeData.Append("(select floor_no from payroll_employee_address where payroll_employee_address.code = payroll_employee.code) as floor_no,");
                                FieldEmployeeData.Append("(select village from payroll_employee_address where payroll_employee_address.code = payroll_employee.code) as village,");
                                FieldEmployeeData.Append("(select house_no from payroll_employee_address where payroll_employee_address.code = payroll_employee.code) as house_no,");
                                FieldEmployeeData.Append("(select crowd_no from payroll_employee_address where payroll_employee_address.code = payroll_employee.code) as crowd_no,");
                                FieldEmployeeData.Append("(select lane from payroll_employee_address where payroll_employee_address.code = payroll_employee.code) as lane,");
                                FieldEmployeeData.Append("(select road from payroll_employee_address where payroll_employee_address.code = payroll_employee.code) as road,");
                                FieldEmployeeData.Append("(select locality from payroll_employee_address where payroll_employee_address.code = payroll_employee.code) as locality,");
                                FieldEmployeeData.Append("(select amphur from payroll_employee_address where payroll_employee_address.code = payroll_employee.code) as amphur,");
                                FieldEmployeeData.Append("(select province from payroll_employee_address where payroll_employee_address.code = payroll_employee.code) as province,");
                                FieldEmployeeData.Append("(select postcode from payroll_employee_address where payroll_employee_address.code = payroll_employee.code) as postcode,");
                                FieldEmployeeData.Append("(select telephone from payroll_employee_address where payroll_employee_address.code = payroll_employee.code) as telephone,");
                                FieldEmployeeData.Append("(select birth_day from payroll_employee_detail where payroll_employee_detail.code = payroll_employee.code) as birth_day,");
                                FieldEmployeeData.Append("(select spouse_name from payroll_employee_detail where payroll_employee_detail.code = payroll_employee.code) as spouse_name,");
                                FieldEmployeeData.Append("(select spouse_birth_day from payroll_employee_detail where payroll_employee_detail.code = payroll_employee.code) as spouse_birth_day,");
                                FieldEmployeeData.Append("(select spouse_id_code from payroll_employee_detail where payroll_employee_detail.code = payroll_employee.code) as spouse_id_code,");
                                FieldEmployeeData.Append("(select father_id from payroll_employee_detail where payroll_employee_detail.code = payroll_employee.code) as father_id,");
                                FieldEmployeeData.Append("(select mother_id from payroll_employee_detail where payroll_employee_detail.code = payroll_employee.code) as mother_id,");
                                FieldEmployeeData.Append("(select father_law_id from payroll_employee_detail where payroll_employee_detail.code = payroll_employee.code) as father_law_id,");
                                FieldEmployeeData.Append("(select mother_law_id from payroll_employee_detail where payroll_employee_detail.code = payroll_employee.code) as mother_law_id,");
                                FieldEmployeeData.Append("(select child_study_qty from payroll_employee_detail where payroll_employee_detail.code = payroll_employee.code) as child_study_qty,");
                                FieldEmployeeData.Append("(select child_no_study_qty from payroll_employee_detail where payroll_employee_detail.code = payroll_employee.code) as child_no_study_qty,");
                               
                                FieldSalarySlipHead.Append(" coalesce((");
                                FieldSalarySlipHead.Append(" CASE salary_status");
                                FieldSalarySlipHead.Append(" WHEN 0 THEN reserve_money ");
                                FieldSalarySlipHead.Append(" ELSE reserve_money_vary");
                                FieldSalarySlipHead.Append(" END");
                                FieldSalarySlipHead.Append(" ),0) as reserve_money,");

                                FieldSalarySlipHead.Append(" coalesce((");
                                FieldSalarySlipHead.Append(" CASE salary_status");
                                FieldSalarySlipHead.Append(" WHEN 0 THEN society_insure_money ");
                                FieldSalarySlipHead.Append(" ELSE society_insure_money_vary");
                                FieldSalarySlipHead.Append(" END");
                                FieldSalarySlipHead.Append(" ),0) as society_insure_money,");

                                FieldSalarySlipHead.Append(" coalesce((");
                                FieldSalarySlipHead.Append(" CASE salary_status");
                                FieldSalarySlipHead.Append(" WHEN 0 THEN wages_money ");
                                FieldSalarySlipHead.Append(" ELSE wages_money_vary");
                                FieldSalarySlipHead.Append(" END");
                                FieldSalarySlipHead.Append(" ),0) as wages_money,");

                                
                                    //จำนวนวันทำงาน
                                FieldSalarySlipData.Append("coalesce((select sum(" + _g._dataPayroll.payroll_trans._working_day + ") from " + _g._dataPayroll.payroll_trans._table + " where " + _g._dataPayroll.payroll_trans._employee + "=" + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + " " + WhereTransSub + "),0) as working_day,");
                                    //คิดกองทุนสำรองฯ
                                FieldSalarySlipData.Append(" coalesce(( ");
                                FieldSalarySlipData.Append(" CASE " + _g._dataPayroll.payroll_employee._employee_type);
                                FieldSalarySlipData.Append(" WHEN 0 THEN ");
                                FieldSalarySlipData.Append("((select coalesce((" + _g._dataPayroll.payroll_employee_salary._salary + "),0) as " + _g._dataPayroll.payroll_employee_salary._salary);
                                FieldSalarySlipData.Append(" from " + _g._dataPayroll.payroll_employee_salary._table);
                                FieldSalarySlipData.Append(" where " + _g._dataPayroll.payroll_employee_salary._code + " = " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + "");
                                FieldSalarySlipData.Append(" and " + _g._dataPayroll.payroll_employee_salary._start_date + " <= '" + StartDay + "'");
                                FieldSalarySlipData.Append(" and (" + _g._dataPayroll.payroll_employee_salary._end_date + " >= '" + EndDay + "' or " + _g._dataPayroll.payroll_employee_salary._end_date + " = '1000-01-01'))  * ((select " + _g._dataPayroll.payroll_company_config._customer_rf_rate + " from " + _g._dataPayroll.payroll_company_config._table + " ) / 100))");
                                FieldSalarySlipData.Append(" WHEN 1 THEN ");
                                FieldSalarySlipData.Append("((");
                                FieldSalarySlipData.Append("select coalesce((" + _g._dataPayroll.payroll_employee_salary._salary + "),0) as " + _g._dataPayroll.payroll_employee_salary._salary);
                                FieldSalarySlipData.Append(" from " + _g._dataPayroll.payroll_employee_salary._table);
                                FieldSalarySlipData.Append(" where " + _g._dataPayroll.payroll_employee_salary._code + " = " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + "");
                                FieldSalarySlipData.Append(" and " + _g._dataPayroll.payroll_employee_salary._start_date + " <= '" + StartDay + "'");
                                FieldSalarySlipData.Append(" and (" + _g._dataPayroll.payroll_employee_salary._end_date + " >= '" + EndDay + "' or " + _g._dataPayroll.payroll_employee_salary._end_date + " = '1000-01-01')");
                                FieldSalarySlipData.Append(") * 4) * ((select " + _g._dataPayroll.payroll_company_config._customer_rf_rate + " from " + _g._dataPayroll.payroll_company_config._table + " ) / 100)");
                                FieldSalarySlipData.Append(" ELSE ");
                                FieldSalarySlipData.Append("((");
                                FieldSalarySlipData.Append("select coalesce((" + _g._dataPayroll.payroll_employee_salary._salary + "),0) as " + _g._dataPayroll.payroll_employee_salary._salary);
                                FieldSalarySlipData.Append(" from " + _g._dataPayroll.payroll_employee_salary._table);
                                FieldSalarySlipData.Append(" where " + _g._dataPayroll.payroll_employee_salary._code + " = " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + "");
                                FieldSalarySlipData.Append(" and " + _g._dataPayroll.payroll_employee_salary._start_date + " <= '" + StartDay + "'");
                                FieldSalarySlipData.Append(" and (" + _g._dataPayroll.payroll_employee_salary._end_date + " >= '" + EndDay + "' or " + _g._dataPayroll.payroll_employee_salary._end_date + " = '1000-01-01')");
                                FieldSalarySlipData.Append(") * (select sum(" + _g._dataPayroll.payroll_trans._working_day + ") from " + _g._dataPayroll.payroll_trans._table + " where " + _g._dataPayroll.payroll_trans._employee + "=" + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + " " + WhereTransSub + ")) * ((select " + _g._dataPayroll.payroll_company_config._customer_rf_rate + " from " + _g._dataPayroll.payroll_company_config._table + " ) / 100)");
                                FieldSalarySlipData.Append(" END");
                                FieldSalarySlipData.Append("),0) as reserve_money_vary,");

                                FieldSalarySlipData.Append(" coalesce(( ");
                                FieldSalarySlipData.Append(" CASE " + _g._dataPayroll.payroll_employee._employee_type);
                                FieldSalarySlipData.Append(" WHEN 0 THEN (" + _g._dataPayroll.payroll_employee._salary + " * ((select " + _g._dataPayroll.payroll_company_config._customer_rf_rate + " from " + _g._dataPayroll.payroll_company_config._table + " ) / 100))");
                                FieldSalarySlipData.Append(" WHEN 1 THEN ((" + _g._dataPayroll.payroll_employee._salary + " * 4) * ((select " + _g._dataPayroll.payroll_company_config._customer_rf_rate + " from " + _g._dataPayroll.payroll_company_config._table + " ) / 100))");
                                FieldSalarySlipData.Append(" ELSE ((" + _g._dataPayroll.payroll_employee._salary + " * (select sum(" + _g._dataPayroll.payroll_trans._working_day + ") from " + _g._dataPayroll.payroll_trans._table + " where " + _g._dataPayroll.payroll_trans._employee + "=" + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + " " + WhereTransSub + ")) * ((select " + _g._dataPayroll.payroll_company_config._customer_rf_rate + " from " + _g._dataPayroll.payroll_company_config._table + " ) / 100))");
                                FieldSalarySlipData.Append(" END");
                                FieldSalarySlipData.Append(" ),0) as reserve_money,");



                                    //คิดประกันสังคม

                                FieldSalarySlipData.Append(" coalesce(( ");
                                FieldSalarySlipData.Append(" CASE " + _g._dataPayroll.payroll_employee._employee_type);
                                FieldSalarySlipData.Append(" WHEN 0 THEN ");
                                FieldSalarySlipData.Append("((select coalesce((" + _g._dataPayroll.payroll_employee_salary._salary + "),0) as " + _g._dataPayroll.payroll_employee_salary._salary);
                                FieldSalarySlipData.Append(" from " + _g._dataPayroll.payroll_employee_salary._table);
                                FieldSalarySlipData.Append(" where " + _g._dataPayroll.payroll_employee_salary._code + " = " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + "");
                                FieldSalarySlipData.Append(" and " + _g._dataPayroll.payroll_employee_salary._start_date + " <= '" + StartDay + "'");
                                FieldSalarySlipData.Append(" and (" + _g._dataPayroll.payroll_employee_salary._end_date + " >= '" + EndDay + "' or " + _g._dataPayroll.payroll_employee_salary._end_date + " = '1000-01-01'))  * ((select " + _g._dataPayroll.payroll_company_config._customer_ss_rate + " from " + _g._dataPayroll.payroll_company_config._table + " ) / 100))");
                                FieldSalarySlipData.Append(" WHEN 1 THEN ");
                                FieldSalarySlipData.Append("((");
                                FieldSalarySlipData.Append("select coalesce((" + _g._dataPayroll.payroll_employee_salary._salary + "),0) as " + _g._dataPayroll.payroll_employee_salary._salary);
                                FieldSalarySlipData.Append(" from " + _g._dataPayroll.payroll_employee_salary._table);
                                FieldSalarySlipData.Append(" where " + _g._dataPayroll.payroll_employee_salary._code + " = " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + "");
                                FieldSalarySlipData.Append(" and " + _g._dataPayroll.payroll_employee_salary._start_date + " <= '" + StartDay + "'");
                                FieldSalarySlipData.Append(" and (" + _g._dataPayroll.payroll_employee_salary._end_date + " >= '" + EndDay + "' or " + _g._dataPayroll.payroll_employee_salary._end_date + " = '1000-01-01')");
                                FieldSalarySlipData.Append(") * 4) * ((select " + _g._dataPayroll.payroll_company_config._customer_ss_rate + " from " + _g._dataPayroll.payroll_company_config._table + " ) / 100)");
                                FieldSalarySlipData.Append(" ELSE ");
                                FieldSalarySlipData.Append("((");
                                FieldSalarySlipData.Append("select coalesce((" + _g._dataPayroll.payroll_employee_salary._salary + "),0) as " + _g._dataPayroll.payroll_employee_salary._salary);
                                FieldSalarySlipData.Append(" from " + _g._dataPayroll.payroll_employee_salary._table);
                                FieldSalarySlipData.Append(" where " + _g._dataPayroll.payroll_employee_salary._code + " = " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + "");
                                FieldSalarySlipData.Append(" and " + _g._dataPayroll.payroll_employee_salary._start_date + " <= '" + StartDay + "'");
                                FieldSalarySlipData.Append(" and (" + _g._dataPayroll.payroll_employee_salary._end_date + " >= '" + EndDay + "' or " + _g._dataPayroll.payroll_employee_salary._end_date + " = '1000-01-01')");
                                FieldSalarySlipData.Append(") * (select sum(" + _g._dataPayroll.payroll_trans._working_day + ") from " + _g._dataPayroll.payroll_trans._table + " where " + _g._dataPayroll.payroll_trans._employee + "=" + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + " " + WhereTransSub + ")) * ((select " + _g._dataPayroll.payroll_company_config._customer_ss_rate + " from " + _g._dataPayroll.payroll_company_config._table + " ) / 100)");
                                FieldSalarySlipData.Append(" END");
                                FieldSalarySlipData.Append("),0) as society_insure_money_vary,");

                                FieldSalarySlipData.Append(" coalesce(( ");
                                FieldSalarySlipData.Append(" CASE " + _g._dataPayroll.payroll_employee._employee_type);
                                FieldSalarySlipData.Append(" WHEN 0 THEN (" + _g._dataPayroll.payroll_employee._salary + " * ((select " + _g._dataPayroll.payroll_company_config._customer_ss_rate + " from " + _g._dataPayroll.payroll_company_config._table + " ) / 100))");
                                FieldSalarySlipData.Append(" WHEN 1 THEN ((" + _g._dataPayroll.payroll_employee._salary + " * 4 ) * ((select " + _g._dataPayroll.payroll_company_config._customer_ss_rate + " from " + _g._dataPayroll.payroll_company_config._table + " ) / 100))");
                                FieldSalarySlipData.Append(" ELSE ((" + _g._dataPayroll.payroll_employee._salary + " * (select sum(" + _g._dataPayroll.payroll_trans._working_day + ") from " + _g._dataPayroll.payroll_trans._table + " where " + _g._dataPayroll.payroll_trans._employee + "=" + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + " " + WhereTransSub + ")) * ((select " + _g._dataPayroll.payroll_company_config._customer_ss_rate + " from " + _g._dataPayroll.payroll_company_config._table + " ) / 100))");
                                FieldSalarySlipData.Append(" END");
                                FieldSalarySlipData.Append(" ),0) as society_insure_money,");


                                    //คิดค่าแรง    
                                FieldSalarySlipData.Append(" coalesce((");
                                FieldSalarySlipData.Append(" CASE " + _g._dataPayroll.payroll_employee._employee_type);
                                FieldSalarySlipData.Append(" WHEN 0 THEN ");
                                FieldSalarySlipData.Append("(select coalesce((" + _g._dataPayroll.payroll_employee_salary._salary + "),0) as " + _g._dataPayroll.payroll_employee_salary._salary);
                                FieldSalarySlipData.Append(" from " + _g._dataPayroll.payroll_employee_salary._table);
                                FieldSalarySlipData.Append(" where " + _g._dataPayroll.payroll_employee_salary._code + " = " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + "");
                                FieldSalarySlipData.Append(" and " + _g._dataPayroll.payroll_employee_salary._start_date + " <= '" + StartDay + "'");
                                FieldSalarySlipData.Append(" and (" + _g._dataPayroll.payroll_employee_salary._end_date + " >= '" + EndDay + "' or " + _g._dataPayroll.payroll_employee_salary._end_date + " = '1000-01-01'))");
                                FieldSalarySlipData.Append(" WHEN 1 THEN ");
                                FieldSalarySlipData.Append("((");
                                FieldSalarySlipData.Append("select coalesce((" + _g._dataPayroll.payroll_employee_salary._salary + "),0) as " + _g._dataPayroll.payroll_employee_salary._salary);
                                FieldSalarySlipData.Append(" from " + _g._dataPayroll.payroll_employee_salary._table);
                                FieldSalarySlipData.Append(" where " + _g._dataPayroll.payroll_employee_salary._code + " = " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + "");
                                FieldSalarySlipData.Append(" and " + _g._dataPayroll.payroll_employee_salary._start_date + " <= '" + StartDay + "'");
                                FieldSalarySlipData.Append(" and (" + _g._dataPayroll.payroll_employee_salary._end_date + " >= '" + EndDay + "' or " + _g._dataPayroll.payroll_employee_salary._end_date + " = '1000-01-01')");
                                FieldSalarySlipData.Append(") * 4)");
                                FieldSalarySlipData.Append(" ELSE ");
                                FieldSalarySlipData.Append("((");
                                FieldSalarySlipData.Append("select coalesce((" + _g._dataPayroll.payroll_employee_salary._salary + "),0) as " + _g._dataPayroll.payroll_employee_salary._salary);
                                FieldSalarySlipData.Append(" from " + _g._dataPayroll.payroll_employee_salary._table);
                                FieldSalarySlipData.Append(" where " + _g._dataPayroll.payroll_employee_salary._code + " = " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + "");
                                FieldSalarySlipData.Append(" and " + _g._dataPayroll.payroll_employee_salary._start_date + " <= '" + StartDay + "'");
                                FieldSalarySlipData.Append(" and (" + _g._dataPayroll.payroll_employee_salary._end_date + " >= '" + EndDay + "' or " + _g._dataPayroll.payroll_employee_salary._end_date + " = '1000-01-01')");
                                FieldSalarySlipData.Append(") * (select sum(" + _g._dataPayroll.payroll_trans._working_day + ") from " + _g._dataPayroll.payroll_trans._table + " where " + _g._dataPayroll.payroll_trans._employee + "=" + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + " " + WhereTransSub + "))");
                                FieldSalarySlipData.Append(" END");
                                FieldSalarySlipData.Append("),0) as wages_money_vary,");

                                FieldSalarySlipData.Append(" coalesce((");
                                FieldSalarySlipData.Append(" CASE " + _g._dataPayroll.payroll_employee._employee_type);
                                FieldSalarySlipData.Append(" WHEN 0 THEN " + _g._dataPayroll.payroll_employee._salary);
                                FieldSalarySlipData.Append(" WHEN 1 THEN (" + _g._dataPayroll.payroll_employee._salary + " * 4)");
                                FieldSalarySlipData.Append(" ELSE (" + _g._dataPayroll.payroll_employee._salary + "*(select sum(" + _g._dataPayroll.payroll_trans._working_day + ") from " + _g._dataPayroll.payroll_trans._table + " where " + _g._dataPayroll.payroll_trans._employee + "=" + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + " " + WhereTransSub + "))");
                                FieldSalarySlipData.Append(" END");
                                FieldSalarySlipData.Append("),0) as wages_money,");

                                break;
                        }
                        break;
                }
                switch (ScreenName)
                {
                    case _g.g._screenPayrollEnum.รายละเอียดเงินเดือน_ค่าจ้างทั้งปี:
                        query.Append(_myQuery);
                        break;
                    case _g.g._screenPayrollEnum.รายงานภาษีเงินได้_รายเดือน:
                    case _g.g._screenPayrollEnum.ภงด_1:
                    case _g.g._screenPayrollEnum.ภงด_1_ใบแนบ:
                    case _g.g._screenPayrollEnum.คำนวนภาษีรายเดือน:
                    case _g.g._screenPayrollEnum.คำนวนเงินเดือน:
                    case _g.g._screenPayrollEnum.รายละเอียดเงินเดือน_ค่าจ้าง_ตามประเภทลูกจ้าง:
                    case _g.g._screenPayrollEnum.สลิปเงินเดือน:
                    case _g.g._screenPayrollEnum.รายงานภาษีเงินได้_รายปี:
                    case _g.g._screenPayrollEnum.คำนวนภาษีรายปี:
                    case _g.g._screenPayrollEnum.ภงด_91:
                    case _g.g._screenPayrollEnum.ภงด_1_ก_ใบแนบ:
                    case _g.g._screenPayrollEnum.ภงด_1_ก:
                        query.Append("select ");
                        //เงินเดือนและภาษีแบบปกติ
                        query.Append(FileFormonth.ToString());
                        //สลิป
                        query.Append(FieldSalarySlipHead.ToString());
                        //เช็คสถานะกองทุน
                        query.Append("reserve_fund_status as reserve_fund_status,");
                        //query.Append("working_day as working_day,");
                        query.Append("salary_status as salary_status,");
                        //เช็คลดหย่อนฯ ประกันสังคม กองทุนสำรองฯ
                        query.Append("reduce_social_security as reduce_social_security,");
                        query.Append("reduce_reserve_fund as reduce_reserve_fund,");
                        //กองทุนสำรองเลี้ยงชีพ 
                        query.Append("reserve_fund_max as reserve_fund_max,");
                        query.Append("reserve_fund_min as reserve_fund_min,");
                        query.Append("customer_rf_rate as customer_rf_rate,");
                        query.Append("employer_rf_rate as employer_rf_rate,");
                        //ประกันสังคม
                        query.Append("social_security_max as social_security_max,");
                        query.Append("social_security_min as social_security_min,");
                        query.Append("customer_ss_rate as customer_ss_rate,");
                        query.Append("employer_ss_rate as employer_ss_rate,");
                        //ภาษี                   
                        query.Append("reduce_myself as reduce_myself,");
                        query.Append("reduce_spouse as reduce_spouse,");
                        query.Append("situation as situation,");
                        query.Append("spouse_work_status as spouse_work_status,");
                        query.Append("spouse_tax as spouse_tax,");
                        query.Append("reduce_child_study as reduce_child_study,");
                        query.Append("reduce_child_no_study as reduce_child_no_study,");
                        query.Append("reduce_parents as reduce_parents,");
                        query.Append("insurance_parents as insurance_parents,");
                        query.Append("donation as donation,");
                        query.Append("donation_education as donation_education,");
                        query.Append("insurance as insurance,");
                        query.Append("loan as loan,");
                        query.Append("buy_funds_reserves as buy_funds_reserves,");
                        query.Append("buy_share as buy_share,");
                        query.Append("except_funds_reserves as except_funds_reserves,");
                        query.Append("except_gbk as except_gbk,");
                        query.Append("except_teacher as except_teacher,");
                        query.Append("except_65year as except_65year,");
                        query.Append("except_spouse_65year as except_spouse_65year,");
                        query.Append("except_labour_legislation as except_labour_legislation,");
                        query.Append("reduce_other as reduce_other,");
                        query.Append("(0) as tax_income,");
                        query.Append("(0) as reduce_40,");
                        query.Append("(0) as reduce_child,");
                        query.Append("(0) as remark,");
                        //ทุุกจอ
                        query.Append(FieldEmployeeHead.ToString());

                        query.Append(" " + _g._dataPayroll.payroll_employee._code + " as " + _g._dataPayroll.payroll_resource._employee + ",");
                        query.Append(" " + _g._dataPayroll.payroll_employee._employee_type + " as " + _g._dataPayroll.payroll_resource._employee_type + ",");
                        query.Append(" " + _g._dataPayroll.payroll_employee._name + " as " + _g._dataPayroll.payroll_resource._employee_name);
                        query.Append(" from ");
                        query.Append("(select ");
                        //เงินสะสม
                        query.Append(DataFormonth.ToString());
                        //สลิปเงินเดือน
                        query.Append(FieldSalarySlipData.ToString());
                        //เช็คสถานะกองทุน
                        query.Append("coalesce(" + _g._dataPayroll.payroll_employee._reserve_fund_status + ",0)  as reserve_fund_status,");
                        //เช็คสถานะเงินเดือน
                        query.Append("coalesce(( " + _g._dataPayroll.payroll_employee._salary_status + "),0) as " + _g._dataPayroll.payroll_employee._salary_status + ",");
                        //เช็คลดหย่อนฯ ประกันสังคม กองทุนสำรองฯ
                        query.Append("coalesce((select " + _g._dataPayroll.payroll_company_config._reduce_social_security + " from " + _g._dataPayroll.payroll_company_config._table + " ),0) as reduce_social_security,");
                        query.Append("coalesce((select " + _g._dataPayroll.payroll_company_config._reduce_reserve_fund + " from " + _g._dataPayroll.payroll_company_config._table + " ),0) as reduce_reserve_fund,");
                        //คิดระยะกองทุนสำรองฯ
                        query.Append("coalesce((select  ceiling((coalesce((" + _g._dataPayroll.payroll_company_config._social_security_max + "),0) * coalesce((" + _g._dataPayroll.payroll_company_config._customer_rf_rate + "),0))/100)  from " + _g._dataPayroll.payroll_company_config._table + " ),0) as reserve_fund_max,");
                        query.Append("coalesce((select  ceiling((coalesce((" + _g._dataPayroll.payroll_company_config._social_security_min + "),0) * coalesce((" + _g._dataPayroll.payroll_company_config._customer_rf_rate + "),0))/100)  from " + _g._dataPayroll.payroll_company_config._table + " ),0) as reserve_fund_min,");
                        query.Append("coalesce((select " + _g._dataPayroll.payroll_company_config._customer_rf_rate + " from " + _g._dataPayroll.payroll_company_config._table + " ),0) as customer_rf_rate,");
                        query.Append("coalesce((select " + _g._dataPayroll.payroll_company_config._employer_rf_rate + " from " + _g._dataPayroll.payroll_company_config._table + " ),0) as employer_rf_rate,");
                        //คิดระยะประกันสังคม
                        query.Append("coalesce((select  ceiling((coalesce((" + _g._dataPayroll.payroll_company_config._social_security_max + "),0) * coalesce((" + _g._dataPayroll.payroll_company_config._customer_ss_rate + "),0))/100)  from " + _g._dataPayroll.payroll_company_config._table + " ),0) as social_security_max,");
                        query.Append("coalesce((select  ceiling((coalesce((" + _g._dataPayroll.payroll_company_config._social_security_min + "),0) * coalesce((" + _g._dataPayroll.payroll_company_config._customer_ss_rate + "),0))/100)  from " + _g._dataPayroll.payroll_company_config._table + " ),0) as social_security_min,");
                        query.Append("coalesce((select " + _g._dataPayroll.payroll_company_config._customer_ss_rate + " from " + _g._dataPayroll.payroll_company_config._table + " ),0) as customer_ss_rate,");
                        query.Append("coalesce((select " + _g._dataPayroll.payroll_company_config._employer_ss_rate + " from " + _g._dataPayroll.payroll_company_config._table + " ),0) as employer_ss_rate,");
                        //คำนวนภาษี

                        query.Append("coalesce((select ");
                        query.Append(_g._dataPayroll.payroll_employee_detail._reduce_myself);
                        query.Append(" from " + _g._dataPayroll.payroll_employee_detail._table);
                        query.Append(" where " + _g._dataPayroll.payroll_employee_detail._code);
                        query.Append(" = " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + "),0) as reduce_myself ,");

                        query.Append("coalesce((select ");
                        query.Append(_g._dataPayroll.payroll_employee_detail._reduce_spouse);
                        query.Append(" from " + _g._dataPayroll.payroll_employee_detail._table);
                        query.Append(" where " + _g._dataPayroll.payroll_employee_detail._code);
                        query.Append(" = " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + "),0) as reduce_spouse ,");

                        query.Append("(select ");
                        query.Append(_g._dataPayroll.payroll_employee_detail._situation);
                        query.Append(" from " + _g._dataPayroll.payroll_employee_detail._table);
                        query.Append(" where " + _g._dataPayroll.payroll_employee_detail._code);
                        query.Append(" = " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + ") as situation ,");

                        query.Append("(select " + _g._dataPayroll.payroll_employee_detail._spouse_work_status + " from ");
                        query.Append(_g._dataPayroll.payroll_employee_detail._table);
                        query.Append(" where " + _g._dataPayroll.payroll_employee_detail._code);
                        query.Append(" = " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + ") as spouse_work_status ,");

                        query.Append("(select " + _g._dataPayroll.payroll_employee_detail._spouse_tax + " from ");
                        query.Append(_g._dataPayroll.payroll_employee_detail._table);
                        query.Append(" where " + _g._dataPayroll.payroll_employee_detail._code);
                        query.Append(" = " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + ") as spouse_tax ,");

                        query.Append("coalesce((select " + _g._dataPayroll.payroll_employee_detail._reduce_child_study + " from ");
                        query.Append(_g._dataPayroll.payroll_employee_detail._table);
                        query.Append(" where " + _g._dataPayroll.payroll_employee_detail._code);
                        query.Append(" = " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + "),0) as reduce_child_study ,");

                        query.Append("coalesce((select " + _g._dataPayroll.payroll_employee_detail._reduce_child_no_study + " from ");
                        query.Append(_g._dataPayroll.payroll_employee_detail._table);
                        query.Append(" where " + _g._dataPayroll.payroll_employee_detail._code);
                        query.Append(" = " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + "),0) as reduce_child_no_study, ");

                        query.Append("coalesce((select " + _g._dataPayroll.payroll_employee_detail._reduce_parents + " from ");
                        query.Append(_g._dataPayroll.payroll_employee_detail._table);
                        query.Append(" where " + _g._dataPayroll.payroll_employee_detail._code);
                        query.Append(" = " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + "),0) as reduce_parents ,");

                        query.Append("coalesce((select " + _g._dataPayroll.payroll_employee_detail._insurance_parents + " from ");
                        query.Append(_g._dataPayroll.payroll_employee_detail._table);
                        query.Append(" where " + _g._dataPayroll.payroll_employee_detail._code);
                        query.Append(" = " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + "),0) as insurance_parents ,");

                        query.Append("coalesce((select " + _g._dataPayroll.payroll_employee_detail._insurance + " from ");
                        query.Append(_g._dataPayroll.payroll_employee_detail._table);
                        query.Append(" where " + _g._dataPayroll.payroll_employee_detail._code);
                        query.Append(" = " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + "),0) as insurance ,");

                        query.Append("coalesce((select " + _g._dataPayroll.payroll_employee_detail._loan + " from ");
                        query.Append(_g._dataPayroll.payroll_employee_detail._table);
                        query.Append(" where " + _g._dataPayroll.payroll_employee_detail._code);
                        query.Append(" = " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + "),0) as loan, ");

                        query.Append("coalesce((select " + _g._dataPayroll.payroll_employee_detail._donation + " from ");
                        query.Append(_g._dataPayroll.payroll_employee_detail._table);
                        query.Append(" where " + _g._dataPayroll.payroll_employee_detail._code);
                        query.Append(" = " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + "),0) as donation, ");

                        query.Append("coalesce((select " + _g._dataPayroll.payroll_employee_detail._donation_education + " from ");
                        query.Append(_g._dataPayroll.payroll_employee_detail._table);
                        query.Append(" where " + _g._dataPayroll.payroll_employee_detail._code);
                        query.Append(" = " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + "),0) as donation_education, ");

                        query.Append("coalesce((select " + _g._dataPayroll.payroll_employee_detail._except_labour_legislation + " from ");
                        query.Append(_g._dataPayroll.payroll_employee_detail._table);
                        query.Append(" where " + _g._dataPayroll.payroll_employee_detail._code);
                        query.Append(" = " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + "),0) as except_labour_legislation, ");

                        query.Append("coalesce((select " + _g._dataPayroll.payroll_employee_detail._buy_funds_reserves + " from ");
                        query.Append(_g._dataPayroll.payroll_employee_detail._table);
                        query.Append(" where " + _g._dataPayroll.payroll_employee_detail._code);
                        query.Append(" = " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + "),0) as buy_funds_reserves, ");

                        query.Append("coalesce((select " + _g._dataPayroll.payroll_employee_detail._buy_share + " from ");
                        query.Append(_g._dataPayroll.payroll_employee_detail._table);
                        query.Append(" where " + _g._dataPayroll.payroll_employee_detail._code);
                        query.Append(" = " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + "),0) as buy_share, ");

                        query.Append("coalesce((select " + _g._dataPayroll.payroll_employee_detail._except_funds_reserves + " from ");
                        query.Append(_g._dataPayroll.payroll_employee_detail._table);
                        query.Append(" where " + _g._dataPayroll.payroll_employee_detail._code);
                        query.Append(" = " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + "),0) as except_funds_reserves, ");

                        query.Append("coalesce((select " + _g._dataPayroll.payroll_employee_detail._except_gbk + " from ");
                        query.Append(_g._dataPayroll.payroll_employee_detail._table);
                        query.Append(" where " + _g._dataPayroll.payroll_employee_detail._code);
                        query.Append(" = " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + "),0) as except_gbk, ");

                        query.Append("coalesce((select " + _g._dataPayroll.payroll_employee_detail._except_teacher + " from ");
                        query.Append(_g._dataPayroll.payroll_employee_detail._table);
                        query.Append(" where " + _g._dataPayroll.payroll_employee_detail._code);
                        query.Append(" = " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + "),0) as except_teacher, ");

                        query.Append("coalesce((select " + _g._dataPayroll.payroll_employee_detail._except_65year + " from ");
                        query.Append(_g._dataPayroll.payroll_employee_detail._table);
                        query.Append(" where " + _g._dataPayroll.payroll_employee_detail._code);
                        query.Append(" = " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + "),0) as except_65year, ");

                        query.Append("coalesce((select " + _g._dataPayroll.payroll_employee_detail._except_spouse_65year + " from ");
                        query.Append(_g._dataPayroll.payroll_employee_detail._table);
                        query.Append(" where " + _g._dataPayroll.payroll_employee_detail._code);
                        query.Append(" = " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + "),0) as except_spouse_65year, ");

                        query.Append("coalesce((select (");
                        query.Append(_g._dataPayroll.payroll_employee_detail._buy_funds_reserves + "+");
                        query.Append(_g._dataPayroll.payroll_employee_detail._buy_share + "+");
                        query.Append(_g._dataPayroll.payroll_employee_detail._except_funds_reserves + "+");
                        query.Append(_g._dataPayroll.payroll_employee_detail._except_gbk + "+");
                        query.Append(_g._dataPayroll.payroll_employee_detail._except_teacher + "+");
                        query.Append(_g._dataPayroll.payroll_employee_detail._except_65year + "+");
                        query.Append(_g._dataPayroll.payroll_employee_detail._except_spouse_65year + "+");
                        query.Append(_g._dataPayroll.payroll_employee_detail._except_labour_legislation);
                        query.Append(") from ");
                        query.Append(_g._dataPayroll.payroll_employee_detail._table);
                        query.Append(" where " + _g._dataPayroll.payroll_employee_detail._code);
                        query.Append(" = " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + "),0) as reduce_other,");

                        query.Append(FieldEmployeeData);
                        query.Append(" employee_type,");
                        query.Append("code,");
                        query.Append(" name");
                        query.Append(" from " + _g._dataPayroll.payroll_employee._table);
                        query.Append(" where " + _g._dataPayroll.payroll_employee._employee_type + " = " + EmployeeType + WhereEmployee + " order by code) as temp1");
                        break;
                }              
            }
            else
            {                
                string GroupBy = "";
                string OrderBy = "";
                //Condition
                if (!FromEmployee.Equals("") && !ToEmployee.Equals(""))
                {
                    WhereTrans += " where (" + _g._dataPayroll.payroll_trans._employee + " between '" + FromEmployee + "' and '" + ToEmployee + "')";
                }
                else
                {
                    if (!FromEmployee.Equals(""))
                    {
                        WhereTrans += " where (" + _g._dataPayroll.payroll_trans._employee + " >= '" + FromEmployee + "')";
                    }
                    if (!ToEmployee.Equals(""))
                    {
                        WhereTrans += " where (" + _g._dataPayroll.payroll_trans._employee + " =< '" + ToEmployee + "')";
                    }
                }
                if (!FromDate.Equals("") && !ToDate.Equals(""))
                {
                    //if (MyLib._myGlobal._intPhase(EmployeeType) > 0)
                    //{
                    if (!WhereTrans.Equals(""))
                    {
                        WhereTrans += " and (" + _g._dataPayroll.payroll_trans._date_pay_salary + " between '" + FromDate + "' and '" + ToDate + "')";
                    }
                    else
                    {
                        WhereTrans += " where (" + _g._dataPayroll.payroll_trans._date_pay_salary + " between '" + FromDate + "' and '" + ToDate + "')";
                    }
                    //}
                    WhereTransSub += " and (" + _g._dataPayroll.payroll_trans._date_pay_salary + " between '" + FromDate + "' and '" + ToDate + "')";
                    WhereTransDetail += " and (" + _g._dataPayroll.payroll_trans_detail._working_date + " between '" + FromDate + "' and '" + ToDate + "')";
                }
                else
                {
                    if (!FromDate.Equals(""))
                    {
                        //if (MyLib._myGlobal._intPhase(EmployeeType) > 0)
                        //{
                        if (!WhereTrans.Equals(""))
                        {
                            WhereTrans += " and (" + _g._dataPayroll.payroll_trans._date_pay_salary + " >= '" + FromDate + "')";
                        }
                        else
                        {
                            WhereTrans += " where (" + _g._dataPayroll.payroll_trans._date_pay_salary + " >= '" + FromDate + "')";
                        }
                        //}
                        WhereTransSub += " and (" + _g._dataPayroll.payroll_trans._date_pay_salary + " >= '" + FromDate + "')";
                        WhereTransDetail += " and (" + _g._dataPayroll.payroll_trans_detail._working_date + " >= '" + FromDate + "')";


                    }
                    if (!ToDate.Equals(""))
                    {
                        //if (MyLib._myGlobal._intPhase(EmployeeType) > 0)
                        //{
                        if (!WhereTrans.Equals(""))
                        {
                            WhereTrans += " and (" + _g._dataPayroll.payroll_trans._date_pay_salary + " =< '" + ToDate + "')";
                        }
                        else
                        {
                            WhereTrans += " where (" + _g._dataPayroll.payroll_trans._date_pay_salary + " =< '" + ToDate + "')";
                        }
                        //}
                        WhereTransSub += " and (" + _g._dataPayroll.payroll_trans._date_pay_salary + " =< '" + ToDate + "')";
                        WhereTransDetail += " and (" + _g._dataPayroll.payroll_trans_detail._working_date + " =< '" + ToDate + "')";


                    }
                }
                if (!InYear.Equals(""))
                {
                    if (!WhereTrans.Equals(""))
                    {
                        WhereTrans += " and (" + _g._dataPayroll.payroll_trans._select_year + " = '" + InYear + "')";
                    }
                    else
                    {
                        WhereTrans += " where (" + _g._dataPayroll.payroll_trans._select_year + " = '" + InYear + "')";
                    }
                    WhereTransSub += " and (" + _g._dataPayroll.payroll_trans._select_year + " = '" + InYear + "')";
                    WhereTransDetail += " and (" + _g._dataPayroll.payroll_trans_detail._select_year + " = '" + InYear + "')";
                    WhereTransSubInMonth += " and (" + _g._dataPayroll.payroll_trans._select_year + " = '" + InYear + "')";
                    WhereTransDetailInMonth += " and (" + _g._dataPayroll.payroll_trans_detail._select_year + " = '" + InYear + "')";
                }
                if (InMonth < 12)
                {
                    if (!WhereTrans.Equals(""))
                    {
                        WhereTrans += " and (" + _g._dataPayroll.payroll_trans._select_month + " = " + InMonth + ")";
                    }
                    else
                    {
                        WhereTrans += " where (" + _g._dataPayroll.payroll_trans._select_month + " = " + InMonth + ")";
                    }
                    WhereTransSub += " and (" + _g._dataPayroll.payroll_trans._select_month + " = " + InMonth + ")";
                    WhereTransDetail += " and (" + _g._dataPayroll.payroll_trans_detail._select_month + " = " + InMonth + ")";
                    WhereTransSubInMonth += " and (" + _g._dataPayroll.payroll_trans._select_month + " = " + InMonth + ")";
                    WhereTransDetailInMonth += " and (" + _g._dataPayroll.payroll_trans_detail._select_month + " = " + InMonth + ")";

                }
                if (!EmployeeType.Equals(""))
                {
                    if (!WhereTrans.Equals(""))
                    {
                        WhereTrans += " and (" + _g._dataPayroll.payroll_trans._employee_type + " = " + EmployeeType + ")";
                    }
                    else
                    {
                        WhereTrans += " where (" + _g._dataPayroll.payroll_trans._employee_type + " = " + EmployeeType + ")";
                    }
                    WhereTransSub += " and (" + _g._dataPayroll.payroll_trans._employee_type + " = " + EmployeeType + ")";
                    WhereTransDetail += " and (" + _g._dataPayroll.payroll_trans_detail._employee_type + " = " + EmployeeType + ")";
                    WhereTransSubInMonth += " and (" + _g._dataPayroll.payroll_trans._employee_type + " = " + EmployeeType + ")";
                    WhereTransDetailInMonth += " and (" + _g._dataPayroll.payroll_trans_detail._employee_type + " = " + EmployeeType + ")";

                }
                //Field Per Page
                StringBuilder FieldEmployeeHead = new StringBuilder();
                StringBuilder FieldEmployeeData = new StringBuilder();
                StringBuilder FieldSalarySlipHead = new StringBuilder();
                StringBuilder FieldSalarySlipData = new StringBuilder();
                StringBuilder FileFormonth = new StringBuilder();
                StringBuilder DataFormonth = new StringBuilder();
                StringBuilder FileTemp2 = new StringBuilder();
                StringBuilder dateTemp2 = new StringBuilder();
                switch (ScreenName)
                {
                    case _g.g._screenPayrollEnum.รายละเอียดเงินเดือน_ค่าจ้างทั้งปี:
                        StringBuilder periodFiled = new StringBuilder();
                        StringBuilder periodData = new StringBuilder();
                        //string subWhere = "";
                        if (Option == 1)
                        {
                            GroupBy = " group by employee,select_month ";
                            OrderBy = " order by employee ";
                        }
                        else if (Option == 2)
                        {                            
                            WhereTransDetailInMonth = " and (" + _g._dataPayroll.payroll_trans_detail._select_year + " = '" + InYear + "') and (" + _g._dataPayroll.payroll_trans_detail._select_month + " = " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._select_month + ")";
                            GroupBy = " group by employee,select_month,period ";
                            OrderBy = " order by employee ";
                            periodFiled.Append("period as period,");
                                //+ "start_date as start_date,"
                                //+ "end_date as end_date,"
                            periodFiled.Append(" coalesce((");
                            periodFiled.Append(" CASE salary_status");
                            periodFiled.Append(" WHEN 0 THEN reserve_money_in_month ");
                            periodFiled.Append(" ELSE reserve_money_in_month_vary");
                            periodFiled.Append(" END");
                            periodFiled.Append(" ),0) as reserve_money_in_month,");

                            periodFiled.Append(" coalesce((");
                            periodFiled.Append(" CASE salary_status");
                            periodFiled.Append(" WHEN 0 THEN society_insure_money_in_month ");
                            periodFiled.Append(" ELSE society_insure_money_in_month_vary");
                            periodFiled.Append(" END");
                            periodFiled.Append(" ),0) as society_insure_money_in_month,");

                            periodFiled.Append(" coalesce((");
                            periodFiled.Append(" CASE salary_status");
                            periodFiled.Append(" WHEN 0 THEN wages_money_in_month ");
                            periodFiled.Append(" ELSE wages_money_in_month_vary");
                            periodFiled.Append(" END");
                            periodFiled.Append(" ),0) as wages_money_in_month,");

                            periodFiled.Append("diligent_money_in_month as diligent_money_in_month,");
                            periodFiled.Append("prepay_money_in_month as prepay_money_in_month,");
                            periodFiled.Append("bonus_money_in_month as bonus_money_in_month,");
                            periodFiled.Append("other_income_money_in_month as other_income_money_in_month,");
                            periodFiled.Append("advance_money_in_month as advance_money_in_month,");
                            periodFiled.Append("insure_money_in_month as insure_money_in_month,");
                            periodFiled.Append("other_payout_money_in_month as other_payout_money_in_month,");
                            periodFiled.Append("sow_money_in_month as sow_money_in_month,");
                            periodFiled.Append("leave_money_in_month as leave_money_in_month,");
                            periodFiled.Append("late_money_in_month as late_money_in_month,");
                            periodFiled.Append("ot_money_in_month as ot_money_in_month,");

                periodFiled.Append(" coalesce((");
                periodFiled.Append(" CASE employee_type");
                periodFiled.Append(" WHEN 0 THEN ");

                periodFiled.Append("case period when 1 then (select start_one_month from payroll_company_config)");
                periodFiled.Append("when 2 then (select start_two_month from payroll_company_config)");
                periodFiled.Append("when 3 then (select start_three_month from payroll_company_config)");
                periodFiled.Append("else (select start_four_month from payroll_company_config)");
                periodFiled.Append("end");

                periodFiled.Append(" WHEN 1 THEN ");

                periodFiled.Append("case period when 1 then (select start_one_week from payroll_company_config)");
                periodFiled.Append("when  2 then (select start_two_week from payroll_company_config)");
                periodFiled.Append("when  3 then (select start_three_week from payroll_company_config)");
                periodFiled.Append("else (select start_four_week from payroll_company_config)");
                periodFiled.Append("end");

                periodFiled.Append(" ELSE ");

                periodFiled.Append("case period when 1 then (select start_one_day from payroll_company_config)");
                periodFiled.Append("when  2 then (select start_two_day from payroll_company_config)");
                periodFiled.Append("when  3 then (select start_three_day from payroll_company_config)");
                periodFiled.Append("else (select start_four_day from payroll_company_config)");
                periodFiled.Append("end");

                periodFiled.Append(" END");
                periodFiled.Append(" ),0) as start_date,");

                periodFiled.Append(" coalesce((");
                periodFiled.Append(" CASE employee_type");
                periodFiled.Append(" WHEN 0 THEN ");

                periodFiled.Append("case period when 1 then (select end_one_month from payroll_company_config)");
                periodFiled.Append("when  2 then (select end_two_month from payroll_company_config)");
                periodFiled.Append("when  3 then (select end_three_month from payroll_company_config)");
                periodFiled.Append("else (select end_four_month from payroll_company_config)");
                periodFiled.Append("end");

                periodFiled.Append(" WHEN 1 THEN ");

                periodFiled.Append("case period when  1 then (select end_one_week from payroll_company_config)");
                periodFiled.Append("when  2 then (select end_two_week from payroll_company_config)");
                periodFiled.Append("when  3 then (select end_three_week from payroll_company_config)");
                periodFiled.Append("else (select end_four_week from payroll_company_config)");
                periodFiled.Append("end");

                periodFiled.Append(" ELSE ");

                periodFiled.Append("case period when  1 then (select end_one_day from payroll_company_config)");
                periodFiled.Append("when  2 then (select end_two_day from payroll_company_config)");
                periodFiled.Append("when  3 then (select end_three_day from payroll_company_config)");
                periodFiled.Append("else (select end_four_day from payroll_company_config)");
                periodFiled.Append("end");

                periodFiled.Append(" END");
                periodFiled.Append(" ),0) as end_date,");

                periodData.Append(" coalesce((");
                periodData.Append(" CASE " + _g._dataPayroll.payroll_employee._employee_type);
                periodData.Append(" WHEN 0 THEN ");

                periodData.Append("case when  (to_char(date_pay_salary,'DD') between to_char((select start_one_month from payroll_company_config),'00')  and  to_char((select end_one_month from payroll_company_config),'00')) then 1");
                periodData.Append("when  (to_char(date_pay_salary,'DD') between to_char((select start_two_month from payroll_company_config),'00')  and  to_char((select end_two_month from payroll_company_config),'00')) then 2");
                periodData.Append("when  (to_char(date_pay_salary,'DD') between to_char((select start_three_month from payroll_company_config),'00') and to_char((select end_three_month from payroll_company_config),'00')) then 3");
                periodData.Append("else 4");
                periodData.Append("end");

                periodData.Append(" WHEN 1 THEN ");

                periodData.Append("case when  to_char(date_pay_salary,'DD') between to_char((select start_one_week from payroll_company_config),'00')  and  to_char((select end_one_week from payroll_company_config),'00') then 1");
                periodData.Append("when  to_char(date_pay_salary,'DD') between to_char((select start_two_week from payroll_company_config),'00')  and  to_char((select end_two_week from payroll_company_config),'00') then 2");
                periodData.Append("when  to_char(date_pay_salary,'DD') between to_char((select start_three_week from payroll_company_config),'00')  and  to_char((select end_three_week from payroll_company_config),'00') then 3");
                periodData.Append("else 4");
                periodData.Append("end");

                periodData.Append(" ELSE ");

                periodData.Append("case when  to_char(date_pay_salary,'DD') between to_char((select start_one_day from payroll_company_config),'00')  and  to_char((select end_one_day from payroll_company_config),'00') then 1");
                periodData.Append("when  to_char(date_pay_salary,'DD') between to_char((select start_two_day from payroll_company_config),'00')  and   to_char((select end_two_day from payroll_company_config),'00') then 2");
                periodData.Append("when  to_char(date_pay_salary,'DD') between to_char((select start_three_day from payroll_company_config),'00')  and  to_char((select end_three_day from payroll_company_config),'00') then 3");
                periodData.Append("else 4");
                periodData.Append("end");

                periodData.Append(" END");
                periodData.Append(" ),0) as period,");



                periodData.Append(" coalesce((select sum(" + _g._dataPayroll.payroll_trans_detail._diligent_money + ") from " + _g._dataPayroll.payroll_trans_detail._table + " where " + _g._dataPayroll.payroll_trans_detail._trans_flag + "=1 and " + _g._dataPayroll.payroll_trans_detail._trans_type + "=5 and " + _g._dataPayroll.payroll_trans_detail._employee + " =  " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + " " + WhereTransDetailInMonth + "),0) as diligent_money_in_month,");
                periodData.Append(" coalesce((select sum(" + _g._dataPayroll.payroll_trans_detail._prepay_money + ") from " + _g._dataPayroll.payroll_trans_detail._table + " where " + _g._dataPayroll.payroll_trans_detail._trans_flag + "=1 and " + _g._dataPayroll.payroll_trans_detail._trans_type + "=5 and " + _g._dataPayroll.payroll_trans_detail._employee + " =  " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + " " + WhereTransDetailInMonth + "),0) as prepay_money_in_month,");
                periodData.Append(" coalesce((select sum(" + _g._dataPayroll.payroll_trans_detail._bonus_money + ") from " + _g._dataPayroll.payroll_trans_detail._table + " where " + _g._dataPayroll.payroll_trans_detail._trans_flag + "=1 and " + _g._dataPayroll.payroll_trans_detail._trans_type + "=5 and " + _g._dataPayroll.payroll_trans_detail._employee + " =  " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + " " + WhereTransDetailInMonth + "),0) as bonus_money_in_month,");
                periodData.Append(" coalesce((select sum(" + _g._dataPayroll.payroll_trans_detail._other_income_money + ") from " + _g._dataPayroll.payroll_trans_detail._table + " where " + _g._dataPayroll.payroll_trans_detail._trans_flag + "=1 and " + _g._dataPayroll.payroll_trans_detail._trans_type + "=5 and " + _g._dataPayroll.payroll_trans_detail._employee + " =  " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + " " + WhereTransDetailInMonth + "),0) as other_income_money_in_month,");
                periodData.Append(" coalesce((select sum(" + _g._dataPayroll.payroll_trans_detail._advance_money + ") from " + _g._dataPayroll.payroll_trans_detail._table + " where " + _g._dataPayroll.payroll_trans_detail._trans_flag + "=1 and " + _g._dataPayroll.payroll_trans_detail._trans_type + "=5 and " + _g._dataPayroll.payroll_trans_detail._employee + " =  " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + " " + WhereTransDetailInMonth + "),0) as advance_money_in_month,");
                periodData.Append(" coalesce((select sum(" + _g._dataPayroll.payroll_trans_detail._insure_money + ") from " + _g._dataPayroll.payroll_trans_detail._table + " where " + _g._dataPayroll.payroll_trans_detail._trans_flag + "=1 and " + _g._dataPayroll.payroll_trans_detail._trans_type + "=5 and " + _g._dataPayroll.payroll_trans_detail._employee + " =  " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + " " + WhereTransDetailInMonth + "),0) as insure_money_in_month,");
                periodData.Append(" coalesce((select sum(" + _g._dataPayroll.payroll_trans_detail._other_payout_money + ") from " + _g._dataPayroll.payroll_trans_detail._table + " where " + _g._dataPayroll.payroll_trans_detail._trans_flag + "=1 and " + _g._dataPayroll.payroll_trans_detail._trans_type + "=5 and " + _g._dataPayroll.payroll_trans_detail._employee + " =  " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + " " + WhereTransDetailInMonth + "),0) as other_payout_money_in_month,");
                periodData.Append(" coalesce((select sum(" + _g._dataPayroll.payroll_trans_detail._money + ") from " + _g._dataPayroll.payroll_trans_detail._table + " where " + _g._dataPayroll.payroll_trans_detail._trans_flag + "=1 and " + _g._dataPayroll.payroll_trans_detail._trans_type + "=1 and " + _g._dataPayroll.payroll_trans_detail._employee + " =  " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + " " + WhereTransDetailInMonth + "),0) as sow_money_in_month,");
                periodData.Append(" coalesce((select sum(" + _g._dataPayroll.payroll_trans_detail._money + ") from " + _g._dataPayroll.payroll_trans_detail._table + " where " + _g._dataPayroll.payroll_trans_detail._trans_flag + "=1 and " + _g._dataPayroll.payroll_trans_detail._trans_type + "=2 and " + _g._dataPayroll.payroll_trans_detail._employee + " =  " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + " " + WhereTransDetailInMonth + "),0) as leave_money_in_month,");
                periodData.Append(" coalesce((select sum(" + _g._dataPayroll.payroll_trans_detail._money + ") from " + _g._dataPayroll.payroll_trans_detail._table + " where " + _g._dataPayroll.payroll_trans_detail._trans_flag + "=1 and " + _g._dataPayroll.payroll_trans_detail._trans_type + "=3 and " + _g._dataPayroll.payroll_trans_detail._employee + " =  " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + " " + WhereTransDetailInMonth + "),0) as late_money_in_month,");
                periodData.Append(" coalesce((select sum(" + _g._dataPayroll.payroll_trans_detail._money + ") from " + _g._dataPayroll.payroll_trans_detail._table + " where " + _g._dataPayroll.payroll_trans_detail._trans_flag + "=1 and " + _g._dataPayroll.payroll_trans_detail._trans_type + "=4 and " + _g._dataPayroll.payroll_trans_detail._employee + " =  " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + " " + WhereTransDetailInMonth + "),0) as ot_money_in_month,");

                                      //คิดกองทุนสำรองฯ

                             periodData.Append(" coalesce((select ");
                             periodData.Append(" CASE " + _g._dataPayroll.payroll_employee._employee_type);
                             periodData.Append(" WHEN 0 THEN ");
                             periodData.Append("((select coalesce((" + _g._dataPayroll.payroll_employee_salary._salary + "),0) as " + _g._dataPayroll.payroll_employee_salary._salary);
                             periodData.Append(" from " + _g._dataPayroll.payroll_employee_salary._table);
                             periodData.Append(" where " + _g._dataPayroll.payroll_employee_salary._code + " = " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + "");
                                //+ " and to_char(" + _g._dataPayroll.payroll_employee_salary._start_date + ",'yyyy') = '" + (MyLib._myGlobal._intPhase(InYear) - 543).ToString() + "'"
                                //+ " and to_char(" + _g._dataPayroll.payroll_employee_salary._end_date + ",'yyyy') = '" + (MyLib._myGlobal._intPhase(InYear) - 543).ToString() + "'"
                             periodData.Append(" and to_char(" + _g._dataPayroll.payroll_employee_salary._start_date + ",'MM') <= to_char(" + _g._dataPayroll.payroll_trans._select_month + " + 1,'00')");
                             periodData.Append(" and (to_char(" + _g._dataPayroll.payroll_employee_salary._end_date + ",'MM') >= to_char(" + _g._dataPayroll.payroll_trans._select_month + ",'00') or " + _g._dataPayroll.payroll_employee_salary._end_date + " = '1000-01-01'))  * ((select " + _g._dataPayroll.payroll_company_config._customer_rf_rate + " from " + _g._dataPayroll.payroll_company_config._table + " ) / 100))");
                             periodData.Append(" WHEN 1 THEN ");
                             periodData.Append("((");
                             periodData.Append("select coalesce((" + _g._dataPayroll.payroll_employee_salary._salary + "),0) as " + _g._dataPayroll.payroll_employee_salary._salary);
                             periodData.Append(" from " + _g._dataPayroll.payroll_employee_salary._table);
                             periodData.Append(" where " + _g._dataPayroll.payroll_employee_salary._code + " = " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + "");
                                //+ " and to_char(" + _g._dataPayroll.payroll_employee_salary._start_date + ",'yyyy') = '" + (MyLib._myGlobal._intPhase(InYear) - 543).ToString() + "'"
                                //+ " and to_char(" + _g._dataPayroll.payroll_employee_salary._end_date + ",'yyyy') = '" + (MyLib._myGlobal._intPhase(InYear) - 543).ToString() + "'"
                             periodData.Append(" and to_char(" + _g._dataPayroll.payroll_employee_salary._start_date + ",'MM') <= to_char(" + _g._dataPayroll.payroll_trans._select_month + " + 1,'00')");
                             periodData.Append(" and (to_char(" + _g._dataPayroll.payroll_employee_salary._end_date + ",'MM') >= to_char(" + _g._dataPayroll.payroll_trans._select_month + ",'00') or " + _g._dataPayroll.payroll_employee_salary._end_date + " = '1000-01-01')");
                             periodData.Append(") * 4) * ((select " + _g._dataPayroll.payroll_company_config._customer_rf_rate + " from " + _g._dataPayroll.payroll_company_config._table + " ) / 100)");
                             periodData.Append(" ELSE ");
                             periodData.Append("((");
                             periodData.Append("select coalesce((" + _g._dataPayroll.payroll_employee_salary._salary + "),0) as " + _g._dataPayroll.payroll_employee_salary._salary);
                             periodData.Append(" from " + _g._dataPayroll.payroll_employee_salary._table);
                             periodData.Append(" where " + _g._dataPayroll.payroll_employee_salary._code + " = " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + "");
                                //+ " and to_char(" + _g._dataPayroll.payroll_employee_salary._start_date + ",'yyyy') = '" + (MyLib._myGlobal._intPhase(InYear) - 543).ToString() + "'"
                                //+ " and to_char(" + _g._dataPayroll.payroll_employee_salary._end_date + ",'yyyy') = '" + (MyLib._myGlobal._intPhase(InYear) - 543).ToString() + "'"
                             periodData.Append(" and to_char(" + _g._dataPayroll.payroll_employee_salary._start_date + ",'MM') <= to_char(" + _g._dataPayroll.payroll_trans._select_month + " + 1,'00')");
                             periodData.Append(" and (to_char(" + _g._dataPayroll.payroll_employee_salary._end_date + ",'MM') >= to_char(" + _g._dataPayroll.payroll_trans._select_month + ",'00') or " + _g._dataPayroll.payroll_employee_salary._end_date + " = '1000-01-01')");
                             periodData.Append(") * (select sum(" + _g._dataPayroll.payroll_trans._working_day + ") from " + _g._dataPayroll.payroll_trans._table + " where " + _g._dataPayroll.payroll_trans._employee + "=" + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + " " + WhereTransSubInMonth + ")) * ((select " + _g._dataPayroll.payroll_company_config._customer_rf_rate + " from " + _g._dataPayroll.payroll_company_config._table + " ) / 100)");
                             periodData.Append(" END");
                             periodData.Append(" from " + _g._dataPayroll.payroll_employee._table + " where " + _g._dataPayroll.payroll_employee._code + " = " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + "),0) as reserve_money_in_month_vary,");

                             periodData.Append(" coalesce((select ");
                             periodData.Append(" CASE " + _g._dataPayroll.payroll_employee._employee_type);
                             periodData.Append(" WHEN 0 THEN (" + _g._dataPayroll.payroll_employee._salary + " * ((select " + _g._dataPayroll.payroll_company_config._customer_rf_rate + " from " + _g._dataPayroll.payroll_company_config._table + " ) / 100))");
                             periodData.Append(" WHEN 1 THEN ((" + _g._dataPayroll.payroll_employee._salary + " * 4) * ((select " + _g._dataPayroll.payroll_company_config._customer_rf_rate + " from " + _g._dataPayroll.payroll_company_config._table + " ) / 100))");
                             periodData.Append(" ELSE ((" + _g._dataPayroll.payroll_employee._salary + " * (select sum(" + _g._dataPayroll.payroll_trans._working_day + ") from " + _g._dataPayroll.payroll_trans._table + " where " + _g._dataPayroll.payroll_trans._employee + "=" + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + " " + WhereTransSubInMonth + ")) * ((select " + _g._dataPayroll.payroll_company_config._customer_rf_rate + " from " + _g._dataPayroll.payroll_company_config._table + " ) / 100))");
                             periodData.Append(" END");
                             periodData.Append(" from " + _g._dataPayroll.payroll_employee._table + " where " + _g._dataPayroll.payroll_employee._code + " = " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + "),0) as reserve_money_in_month,");



                                    //คิดประกันสังคม

                             periodData.Append(" coalesce((select ");
                             periodData.Append(" CASE " + _g._dataPayroll.payroll_employee._employee_type);
                             periodData.Append(" WHEN 0 THEN ");
                             periodData.Append("((select coalesce((" + _g._dataPayroll.payroll_employee_salary._salary + "),0) as " + _g._dataPayroll.payroll_employee_salary._salary);
                             periodData.Append(" from " + _g._dataPayroll.payroll_employee_salary._table);
                             periodData.Append(" where " + _g._dataPayroll.payroll_employee_salary._code + " = " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + "");
                                //+ " and to_char(" + _g._dataPayroll.payroll_employee_salary._start_date + ",'yyyy') = '" + (MyLib._myGlobal._intPhase(InYear) - 543).ToString() + "'"
                                //+ " and to_char(" + _g._dataPayroll.payroll_employee_salary._end_date + ",'yyyy') = '" + (MyLib._myGlobal._intPhase(InYear) - 543).ToString() + "'"
                             periodData.Append(" and to_char(" + _g._dataPayroll.payroll_employee_salary._start_date + ",'MM') <= to_char(" + _g._dataPayroll.payroll_trans._select_month + " + 1,'00')");
                             periodData.Append(" and (to_char(" + _g._dataPayroll.payroll_employee_salary._end_date + ",'MM') >= to_char(" + _g._dataPayroll.payroll_trans._select_month + ",'00') or " + _g._dataPayroll.payroll_employee_salary._end_date + " = '1000-01-01'))  * ((select " + _g._dataPayroll.payroll_company_config._customer_ss_rate + " from " + _g._dataPayroll.payroll_company_config._table + " ) / 100))");
                             periodData.Append(" WHEN 1 THEN ");
                             periodData.Append("((");
                             periodData.Append("select coalesce((" + _g._dataPayroll.payroll_employee_salary._salary + "),0) as " + _g._dataPayroll.payroll_employee_salary._salary);
                             periodData.Append(" from " + _g._dataPayroll.payroll_employee_salary._table);
                             periodData.Append(" where " + _g._dataPayroll.payroll_employee_salary._code + " = " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + "");
                                //+ " and to_char(" + _g._dataPayroll.payroll_employee_salary._start_date + ",'yyyy') = '" + (MyLib._myGlobal._intPhase(InYear) - 543).ToString() + "'"
                                //+ " and to_char(" + _g._dataPayroll.payroll_employee_salary._end_date + ",'yyyy') = '" + (MyLib._myGlobal._intPhase(InYear) - 543).ToString() + "'"
                             periodData.Append(" and to_char(" + _g._dataPayroll.payroll_employee_salary._start_date + ",'MM') <= to_char(" + _g._dataPayroll.payroll_trans._select_month + " + 1,'00')");
                             periodData.Append(" and (to_char(" + _g._dataPayroll.payroll_employee_salary._end_date + ",'MM') >= to_char(" + _g._dataPayroll.payroll_trans._select_month + ",'00') or " + _g._dataPayroll.payroll_employee_salary._end_date + " = '1000-01-01')");
                             periodData.Append(") * 4) * ((select " + _g._dataPayroll.payroll_company_config._customer_ss_rate + " from " + _g._dataPayroll.payroll_company_config._table + " ) / 100)");
                             periodData.Append(" ELSE ");
                             periodData.Append("((");
                             periodData.Append("select coalesce((" + _g._dataPayroll.payroll_employee_salary._salary + "),0) as " + _g._dataPayroll.payroll_employee_salary._salary);
                             periodData.Append(" from " + _g._dataPayroll.payroll_employee_salary._table);
                             periodData.Append(" where " + _g._dataPayroll.payroll_employee_salary._code + " = " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + "");
                                //+ " and to_char(" + _g._dataPayroll.payroll_employee_salary._start_date + ",'yyyy') = '" + (MyLib._myGlobal._intPhase(InYear) - 543).ToString() + "'"
                                //+ " and to_char(" + _g._dataPayroll.payroll_employee_salary._end_date + ",'yyyy') = '" + (MyLib._myGlobal._intPhase(InYear) - 543).ToString() + "'"
                             periodData.Append(" and to_char(" + _g._dataPayroll.payroll_employee_salary._start_date + ",'MM') <= to_char(" + _g._dataPayroll.payroll_trans._select_month + " + 1,'00')");
                             periodData.Append(" and (to_char(" + _g._dataPayroll.payroll_employee_salary._end_date + ",'MM') >= to_char(" + _g._dataPayroll.payroll_trans._select_month + ",'00') or " + _g._dataPayroll.payroll_employee_salary._end_date + " = '1000-01-01')");
                             periodData.Append(") * (select sum(" + _g._dataPayroll.payroll_trans._working_day + ") from " + _g._dataPayroll.payroll_trans._table + " where " + _g._dataPayroll.payroll_trans._employee + "=" + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + " " + WhereTransSubInMonth + ")) * ((select " + _g._dataPayroll.payroll_company_config._customer_ss_rate + " from " + _g._dataPayroll.payroll_company_config._table + " ) / 100)");
                             periodData.Append(" END");
                             periodData.Append(" from " + _g._dataPayroll.payroll_employee._table + " where " + _g._dataPayroll.payroll_employee._code + " = " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + "),0) as society_insure_money_in_month_vary,");

                             periodData.Append(" coalesce((select ");
                             periodData.Append(" CASE " + _g._dataPayroll.payroll_employee._employee_type);
                             periodData.Append(" WHEN 0 THEN (" + _g._dataPayroll.payroll_employee._salary + " * ((select " + _g._dataPayroll.payroll_company_config._customer_ss_rate + " from " + _g._dataPayroll.payroll_company_config._table + " ) / 100))");
                             periodData.Append(" WHEN 1 THEN ((" + _g._dataPayroll.payroll_employee._salary + " * 4 ) * ((select " + _g._dataPayroll.payroll_company_config._customer_ss_rate + " from " + _g._dataPayroll.payroll_company_config._table + " ) / 100))");
                             periodData.Append(" ELSE ((" + _g._dataPayroll.payroll_employee._salary + " * (select sum(" + _g._dataPayroll.payroll_trans._working_day + ") from " + _g._dataPayroll.payroll_trans._table + " where " + _g._dataPayroll.payroll_trans._employee + "=" + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + " " + WhereTransSubInMonth + ")) * ((select " + _g._dataPayroll.payroll_company_config._customer_ss_rate + " from " + _g._dataPayroll.payroll_company_config._table + " ) / 100))");
                             periodData.Append(" END");
                             periodData.Append(" from " + _g._dataPayroll.payroll_employee._table + " where " + _g._dataPayroll.payroll_employee._code + " = " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + "),0) as society_insure_money_in_month,");


                                //คิดค่าแรง    
                             periodData.Append(" coalesce((select ");
                             periodData.Append(" CASE " + _g._dataPayroll.payroll_employee._employee_type);
                             periodData.Append(" WHEN 0 THEN ");
                             periodData.Append("(select coalesce((" + _g._dataPayroll.payroll_employee_salary._salary + "),0) as " + _g._dataPayroll.payroll_employee_salary._salary);
                             periodData.Append(" from " + _g._dataPayroll.payroll_employee_salary._table);
                             periodData.Append(" where " + _g._dataPayroll.payroll_employee_salary._code + " = " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + "");
                                //+ " and to_char(" + _g._dataPayroll.payroll_employee_salary._start_date + ",'yyyy') = '" + (MyLib._myGlobal._intPhase(InYear) - 543).ToString() + "'"
                                //+ " and to_char(" + _g._dataPayroll.payroll_employee_salary._end_date + ",'yyyy') = '" + (MyLib._myGlobal._intPhase(InYear) - 543).ToString() + "'")
                             periodData.Append(" and to_char(" + _g._dataPayroll.payroll_employee_salary._start_date + ",'MM') <= to_char(" + _g._dataPayroll.payroll_trans._select_month + " + 1,'00')");
                             periodData.Append(" and (to_char(" + _g._dataPayroll.payroll_employee_salary._end_date + ",'MM') >= to_char(" + _g._dataPayroll.payroll_trans._select_month + ",'00') or " + _g._dataPayroll.payroll_employee_salary._end_date + " = '1000-01-01'))");
                             periodData.Append(" WHEN 1 THEN ");
                             periodData.Append("((");
                             periodData.Append("select coalesce((" + _g._dataPayroll.payroll_employee_salary._salary + "),0) as " + _g._dataPayroll.payroll_employee_salary._salary);
                             periodData.Append(" from " + _g._dataPayroll.payroll_employee_salary._table);
                             periodData.Append(" where " + _g._dataPayroll.payroll_employee_salary._code + " = " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + "");
                                //+ " and to_char(" + _g._dataPayroll.payroll_employee_salary._start_date + ",'yyyy') = '" + (MyLib._myGlobal._intPhase(InYear) - 543).ToString() + "'"
                                //+ " and to_char(" + _g._dataPayroll.payroll_employee_salary._end_date + ",'yyyy') = '" + (MyLib._myGlobal._intPhase(InYear) - 543).ToString() + "'"
                             periodData.Append(" and to_char(" + _g._dataPayroll.payroll_employee_salary._start_date + ",'MM') <= to_char(" + _g._dataPayroll.payroll_trans._select_month + " + 1,'00')");
                             periodData.Append(" and (to_char(" + _g._dataPayroll.payroll_employee_salary._end_date + ",'MM') >= to_char(" + _g._dataPayroll.payroll_trans._select_month + ",'00') or " + _g._dataPayroll.payroll_employee_salary._end_date + " = '1000-01-01')");
                             periodData.Append(") * 4)");
                             periodData.Append(" ELSE ");
                             periodData.Append("((");
                             periodData.Append("select coalesce((" + _g._dataPayroll.payroll_employee_salary._salary + "),0) as " + _g._dataPayroll.payroll_employee_salary._salary);
                             periodData.Append(" from " + _g._dataPayroll.payroll_employee_salary._table);
                             periodData.Append(" where " + _g._dataPayroll.payroll_employee_salary._code + " = " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + "");
                                //+ " and to_char(" + _g._dataPayroll.payroll_employee_salary._start_date + ",'yyyy') = '" + (MyLib._myGlobal._intPhase(InYear) - 543).ToString() + "'"
                                //+ " and to_char(" + _g._dataPayroll.payroll_employee_salary._end_date + ",'yyyy') = '" + (MyLib._myGlobal._intPhase(InYear) - 543).ToString() + "'"
                             periodData.Append(" and to_char(" + _g._dataPayroll.payroll_employee_salary._start_date + ",'MM') <= to_char(" + _g._dataPayroll.payroll_trans._select_month + " + 1,'00')");
                             periodData.Append(" and (to_char(" + _g._dataPayroll.payroll_employee_salary._end_date + ",'MM') >= to_char(" + _g._dataPayroll.payroll_trans._select_month + ",'00') or " + _g._dataPayroll.payroll_employee_salary._end_date + " = '1000-01-01')");
                             periodData.Append(") * (select sum(" + _g._dataPayroll.payroll_trans._working_day + ") from " + _g._dataPayroll.payroll_trans._table + " where " + _g._dataPayroll.payroll_trans._employee + "=" + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + " " + WhereTransSubInMonth + "))");
                             periodData.Append(" END");
                             periodData.Append(" from " + _g._dataPayroll.payroll_employee._table + " where " + _g._dataPayroll.payroll_employee._code + " = " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + "),0) as wages_money_in_month_vary,");

                             periodData.Append(" coalesce((select ");
                             periodData.Append(" CASE " + _g._dataPayroll.payroll_employee._employee_type);
                             periodData.Append(" WHEN 0 THEN " + _g._dataPayroll.payroll_employee._salary);
                             periodData.Append(" WHEN 1 THEN (" + _g._dataPayroll.payroll_employee._salary + " * 4)");
                             periodData.Append(" ELSE (" + _g._dataPayroll.payroll_employee._salary + "*(select sum(" + _g._dataPayroll.payroll_trans._working_day + ") from " + _g._dataPayroll.payroll_trans._table + " where " + _g._dataPayroll.payroll_trans._employee + "=" + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + " " + WhereTransSubInMonth + "))");
                             periodData.Append(" END");
                             periodData.Append(" from " + _g._dataPayroll.payroll_employee._table + " where " + _g._dataPayroll.payroll_employee._code + " = " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + "),0) as wages_money_in_month,");
                        }
                        else
                        {
                            GroupBy = " group by employee,select_month ";
                            OrderBy = " order by employee ";
                        }
                        WhereTransDetail = "";
                        WhereTransSub = " and (" + _g._dataPayroll.payroll_trans._select_year + " = '" + InYear + "') and (" + _g._dataPayroll.payroll_trans._select_month + " =  " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._select_month + ")";
                        WhereTransDetail = " and (" + _g._dataPayroll.payroll_trans_detail._select_year + " = '" + InYear + "') and (" + _g._dataPayroll.payroll_trans._select_month + " = " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._select_month + ")";
                       
                            //เงินหัก-เงินได้
                        FileFormonth.Append(" " + _g._dataPayroll.payroll_trans._sow_money + " as " + _g._dataPayroll.payroll_resource._sow_money + ",");
                        FileFormonth.Append(" " + _g._dataPayroll.payroll_trans._leave_money + " as " + _g._dataPayroll.payroll_resource._leave_money + ",");
                        FileFormonth.Append(" " + _g._dataPayroll.payroll_trans._late_money + " as " + _g._dataPayroll.payroll_resource._late_money + ",");
                        FileFormonth.Append(" " + _g._dataPayroll.payroll_trans._ot_money + " as " + _g._dataPayroll.payroll_resource._ot_money + ",");
                        FileFormonth.Append(" " + _g._dataPayroll.payroll_trans._diligent_money + " as " + _g._dataPayroll.payroll_resource._diligent_money + ",");
                        FileFormonth.Append(" " + _g._dataPayroll.payroll_trans._prepay_money + " as " + _g._dataPayroll.payroll_resource._prepay_money + ",");
                        FileFormonth.Append(" " + _g._dataPayroll.payroll_trans._bonus_money + " as " + _g._dataPayroll.payroll_resource._bonus_money + ",");
                        FileFormonth.Append(" " + _g._dataPayroll.payroll_trans._other_income_money + " as " + _g._dataPayroll.payroll_resource._other_income_money + ",");
                        FileFormonth.Append(" " + _g._dataPayroll.payroll_trans._advance_money + " as " + _g._dataPayroll.payroll_resource._advance_money + ",");
                        FileFormonth.Append(" " + _g._dataPayroll.payroll_trans._insure_money + " as " + _g._dataPayroll.payroll_resource._insure_money + ",");
                        FileFormonth.Append(" " + _g._dataPayroll.payroll_trans._other_payout_money + " as " + _g._dataPayroll.payroll_resource._other_payout_money + ",");
                            //+ " coalesce((select sum(" + _g._dataPayroll.payroll_trans_detail._diligent_money + ") from " + _g._dataPayroll.payroll_trans_detail._table + " where " + _g._dataPayroll.payroll_trans_detail._trans_flag + "=1 and " + _g._dataPayroll.payroll_trans_detail._trans_type + "=5 and " + _g._dataPayroll.payroll_trans_detail._employee + " =  employee " + WhereTransDetail + "),0) as " + _g._dataPayroll.payroll_resource._diligent_money + ","
                            //+ " coalesce((select sum(" + _g._dataPayroll.payroll_trans_detail._prepay_money + ") from " + _g._dataPayroll.payroll_trans_detail._table + " where " + _g._dataPayroll.payroll_trans_detail._trans_flag + "=1 and " + _g._dataPayroll.payroll_trans_detail._trans_type + "=5 and " + _g._dataPayroll.payroll_trans_detail._employee + " =  employee " + WhereTransDetail + "),0) as " + _g._dataPayroll.payroll_resource._prepay_money + ","
                            //+ " coalesce((select sum(" + _g._dataPayroll.payroll_trans_detail._bonus_money + ") from " + _g._dataPayroll.payroll_trans_detail._table + " where " + _g._dataPayroll.payroll_trans_detail._trans_flag + "=1 and " + _g._dataPayroll.payroll_trans_detail._trans_type + "=5 and " + _g._dataPayroll.payroll_trans_detail._employee + " =  employee " + WhereTransDetail + "),0) as " + _g._dataPayroll.payroll_resource._bonus_money + ","
                            //+ " coalesce((select sum(" + _g._dataPayroll.payroll_trans_detail._other_income_money + ") from " + _g._dataPayroll.payroll_trans_detail._table + " where " + _g._dataPayroll.payroll_trans_detail._trans_flag + "=1 and " + _g._dataPayroll.payroll_trans_detail._trans_type + "=5 and " + _g._dataPayroll.payroll_trans_detail._employee + " =  employee " + WhereTransDetail + "),0) as " + _g._dataPayroll.payroll_resource._other_income_money + ","
                            //+ " coalesce((select sum(" + _g._dataPayroll.payroll_trans_detail._advance_money + ") from " + _g._dataPayroll.payroll_trans_detail._table + " where " + _g._dataPayroll.payroll_trans_detail._trans_flag + "=1 and " + _g._dataPayroll.payroll_trans_detail._trans_type + "=5 and " + _g._dataPayroll.payroll_trans_detail._employee + " =  employee " + WhereTransDetail + "),0) as " + _g._dataPayroll.payroll_resource._advance_money + ","
                            //+ " coalesce((select sum(" + _g._dataPayroll.payroll_trans_detail._insure_money + ") from " + _g._dataPayroll.payroll_trans_detail._table + " where " + _g._dataPayroll.payroll_trans_detail._trans_flag + "=1 and " + _g._dataPayroll.payroll_trans_detail._trans_type + "=5 and " + _g._dataPayroll.payroll_trans_detail._employee + " =  employee " + WhereTransDetail + "),0) as " + _g._dataPayroll.payroll_resource._insure_money + ","
                            //+ " coalesce((select sum(" + _g._dataPayroll.payroll_trans_detail._other_payout_money + ") from " + _g._dataPayroll.payroll_trans_detail._table + " where " + _g._dataPayroll.payroll_trans_detail._trans_flag + "=1 and " + _g._dataPayroll.payroll_trans_detail._trans_type + "=5 and " + _g._dataPayroll.payroll_trans_detail._employee + " =  employee " + WhereTransDetail + "),0) as " + _g._dataPayroll.payroll_resource._other_payout_money + ","

                                      //+ " coalesce((select sum(" + _g._dataPayroll.payroll_trans_detail._money + ") from " + _g._dataPayroll.payroll_trans_detail._table + " where " + _g._dataPayroll.payroll_trans_detail._trans_flag + "=1 and " + _g._dataPayroll.payroll_trans_detail._trans_type + "=1 and " + _g._dataPayroll.payroll_trans_detail._employee + " =  employee " + WhereTransDetail + "),0) as " + _g._dataPayroll.payroll_trans._sow_money + ","
                            //+ " coalesce((select sum(" + _g._dataPayroll.payroll_trans_detail._money + ") from " + _g._dataPayroll.payroll_trans_detail._table + " where " + _g._dataPayroll.payroll_trans_detail._trans_flag + "=1 and " + _g._dataPayroll.payroll_trans_detail._trans_type + "=2 and " + _g._dataPayroll.payroll_trans_detail._employee + " =  employee " + WhereTransDetail + "),0) as " + _g._dataPayroll.payroll_trans._leave_money + ","
                            //+ " coalesce((select sum(" + _g._dataPayroll.payroll_trans_detail._money + ") from " + _g._dataPayroll.payroll_trans_detail._table + " where " + _g._dataPayroll.payroll_trans_detail._trans_flag + "=1 and " + _g._dataPayroll.payroll_trans_detail._trans_type + "=3 and " + _g._dataPayroll.payroll_trans_detail._employee + " =  employee " + WhereTransDetail + "),0) as " + _g._dataPayroll.payroll_trans._late_money + ","
                            //+ " coalesce((select sum(" + _g._dataPayroll.payroll_trans_detail._money + ") from " + _g._dataPayroll.payroll_trans_detail._table + " where " + _g._dataPayroll.payroll_trans_detail._trans_flag + "=1 and " + _g._dataPayroll.payroll_trans_detail._trans_type + "=4 and " + _g._dataPayroll.payroll_trans_detail._employee + " =  employee " + WhereTransDetail + "),0) as " + _g._dataPayroll.payroll_resource._ot_money + ","
                        FileFormonth.Append(periodFiled.ToString());
                            //เงินกองทุน
                        FileFormonth.Append(" coalesce((");
                        FileFormonth.Append(" CASE salary_status");
                        FileFormonth.Append(" WHEN 0 THEN ");

                        FileFormonth.Append(" CASE " + _g._dataPayroll.payroll_employee._employee_type);
                        FileFormonth.Append(" WHEN 0 THEN ((salary * customer_rf_rate) / 100)");
                        FileFormonth.Append(" WHEN 1 THEN (((salary  * 4)*customer_rf_rate) / 100)");
                        FileFormonth.Append(" ELSE (((salary * working_day)*customer_rf_rate) / 100)");
                        FileFormonth.Append(" END");

                        FileFormonth.Append(" ELSE ");

                        FileFormonth.Append(" CASE " + _g._dataPayroll.payroll_employee._employee_type);
                        FileFormonth.Append(" WHEN 0 THEN ((salary_vary * customer_rf_rate) / 100)");
                        FileFormonth.Append(" WHEN 1 THEN (((salary_vary  * 4)*customer_rf_rate) / 100)");
                        FileFormonth.Append(" ELSE (((salary_vary * working_day)*customer_rf_rate) / 100)");
                        FileFormonth.Append(" END");

                        FileFormonth.Append(" END");
                        FileFormonth.Append(" ),0) as " + _g._dataPayroll.payroll_trans._reserve_money + ",");
                            //เงินประกันสังคม
                        FileFormonth.Append(" coalesce(( ");
                        FileFormonth.Append(" CASE salary_status");
                        FileFormonth.Append(" WHEN 0 THEN ");

                        FileFormonth.Append(" CASE " + _g._dataPayroll.payroll_employee._employee_type);
                        FileFormonth.Append(" WHEN 0 THEN ((salary * customer_ss_rate) / 100)");
                        FileFormonth.Append(" WHEN 1 THEN (((salary  * 4)*customer_ss_rate) / 100)");
                        FileFormonth.Append(" ELSE (((salary * working_day)*customer_ss_rate) / 100)");
                        FileFormonth.Append(" END");

                        FileFormonth.Append(" ELSE ");

                        FileFormonth.Append(" CASE " + _g._dataPayroll.payroll_employee._employee_type);
                        FileFormonth.Append(" WHEN 0 THEN ((salary_vary * customer_ss_rate) / 100)");
                        FileFormonth.Append(" WHEN 1 THEN (((salary_vary  * 4)*customer_ss_rate) / 100)");
                        FileFormonth.Append(" ELSE (((salary_vary * working_day)*customer_ss_rate) / 100)");
                        FileFormonth.Append(" END");

                        FileFormonth.Append(" END");
                        FileFormonth.Append(" ),0) as " + _g._dataPayroll.payroll_trans._society_insure_money + ",");
                            //เงินค่าจ้าง
                        FileFormonth.Append(" coalesce((");
                        FileFormonth.Append(" CASE salary_status");
                        FileFormonth.Append(" WHEN 0 THEN ");

                        FileFormonth.Append(" CASE " + _g._dataPayroll.payroll_employee._employee_type);
                        FileFormonth.Append(" WHEN 0 THEN salary ");
                        FileFormonth.Append(" WHEN 1 THEN (salary * 4 )");
                        FileFormonth.Append(" ELSE (salary) *(working_day)");
                        FileFormonth.Append(" END");

                        FileFormonth.Append(" ELSE ");

                        FileFormonth.Append(" CASE " + _g._dataPayroll.payroll_employee._employee_type);
                        FileFormonth.Append(" WHEN 0 THEN salary_vary ");
                        FileFormonth.Append(" WHEN 1 THEN (salary_vary * 4 )");
                        FileFormonth.Append(" ELSE (salary_vary) *(working_day)");
                        FileFormonth.Append(" END");

                        FileFormonth.Append(" END");
                        FileFormonth.Append(" ),0) as " + _g._dataPayroll.payroll_trans._wages_money + ",");

                        FileFormonth.Append("salary_vary as salary_vary,");

                            //ค่าจ้างสุทธิ                                  
                        FileFormonth.Append(" " + _g._dataPayroll.payroll_trans._select_month + " as " + _g._dataPayroll.payroll_resource._select_month + ",");

                        FileFormonth.Append(" salary as salary,");
                        FileFormonth.Append(" work_day as work_day,");
                       
                            //คำนวนเงินหัก-เงินได้   

                        DataFormonth.Append(" coalesce((select sum(" + _g._dataPayroll.payroll_trans_detail._diligent_money + ") from " + _g._dataPayroll.payroll_trans_detail._table + " where " + _g._dataPayroll.payroll_trans_detail._trans_flag + "=1 and " + _g._dataPayroll.payroll_trans_detail._trans_type + "=5 and " + _g._dataPayroll.payroll_trans_detail._employee + " =  " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + " " + WhereTransDetail + "),0) as " + _g._dataPayroll.payroll_resource._diligent_money + ",");
                        DataFormonth.Append(" coalesce((select sum(" + _g._dataPayroll.payroll_trans_detail._prepay_money + ") from " + _g._dataPayroll.payroll_trans_detail._table + " where " + _g._dataPayroll.payroll_trans_detail._trans_flag + "=1 and " + _g._dataPayroll.payroll_trans_detail._trans_type + "=5 and " + _g._dataPayroll.payroll_trans_detail._employee + " =  " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + " " + WhereTransDetail + "),0) as " + _g._dataPayroll.payroll_resource._prepay_money + ",");
                        DataFormonth.Append(" coalesce((select sum(" + _g._dataPayroll.payroll_trans_detail._bonus_money + ") from " + _g._dataPayroll.payroll_trans_detail._table + " where " + _g._dataPayroll.payroll_trans_detail._trans_flag + "=1 and " + _g._dataPayroll.payroll_trans_detail._trans_type + "=5 and " + _g._dataPayroll.payroll_trans_detail._employee + " =  " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + " " + WhereTransDetail + "),0) as " + _g._dataPayroll.payroll_resource._bonus_money + ",");
                        DataFormonth.Append(" coalesce((select sum(" + _g._dataPayroll.payroll_trans_detail._other_income_money + ") from " + _g._dataPayroll.payroll_trans_detail._table + " where " + _g._dataPayroll.payroll_trans_detail._trans_flag + "=1 and " + _g._dataPayroll.payroll_trans_detail._trans_type + "=5 and " + _g._dataPayroll.payroll_trans_detail._employee + " =  " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + " " + WhereTransDetail + "),0) as " + _g._dataPayroll.payroll_resource._other_income_money + ",");
                        DataFormonth.Append(" coalesce((select sum(" + _g._dataPayroll.payroll_trans_detail._advance_money + ") from " + _g._dataPayroll.payroll_trans_detail._table + " where " + _g._dataPayroll.payroll_trans_detail._trans_flag + "=1 and " + _g._dataPayroll.payroll_trans_detail._trans_type + "=5 and " + _g._dataPayroll.payroll_trans_detail._employee + " =  " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + " " + WhereTransDetail + "),0) as " + _g._dataPayroll.payroll_resource._advance_money + ",");
                        DataFormonth.Append(" coalesce((select sum(" + _g._dataPayroll.payroll_trans_detail._insure_money + ") from " + _g._dataPayroll.payroll_trans_detail._table + " where " + _g._dataPayroll.payroll_trans_detail._trans_flag + "=1 and " + _g._dataPayroll.payroll_trans_detail._trans_type + "=5 and " + _g._dataPayroll.payroll_trans_detail._employee + " =  " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + " " + WhereTransDetail + "),0) as " + _g._dataPayroll.payroll_resource._insure_money + ",");
                        DataFormonth.Append(" coalesce((select sum(" + _g._dataPayroll.payroll_trans_detail._other_payout_money + ") from " + _g._dataPayroll.payroll_trans_detail._table + " where " + _g._dataPayroll.payroll_trans_detail._trans_flag + "=1 and " + _g._dataPayroll.payroll_trans_detail._trans_type + "=5 and " + _g._dataPayroll.payroll_trans_detail._employee + " =  " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + " " + WhereTransDetail + "),0) as " + _g._dataPayroll.payroll_resource._other_payout_money + ",");

                        DataFormonth.Append(" coalesce((select sum(" + _g._dataPayroll.payroll_trans_detail._money + ") from " + _g._dataPayroll.payroll_trans_detail._table + " where " + _g._dataPayroll.payroll_trans_detail._trans_flag + "=1 and " + _g._dataPayroll.payroll_trans_detail._trans_type + "=1 and " + _g._dataPayroll.payroll_trans_detail._employee + " =  " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + " " + WhereTransDetail + "),0) as " + _g._dataPayroll.payroll_trans._sow_money + ",");

                        DataFormonth.Append(" coalesce((select sum(" + _g._dataPayroll.payroll_trans_detail._money + ") from " + _g._dataPayroll.payroll_trans_detail._table + " where " + _g._dataPayroll.payroll_trans_detail._trans_flag + "=1 and " + _g._dataPayroll.payroll_trans_detail._trans_type + "=2 and " + _g._dataPayroll.payroll_trans_detail._employee + " =  " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + " " + WhereTransDetail + "),0) as " + _g._dataPayroll.payroll_trans._leave_money + ",");
                        DataFormonth.Append(" coalesce((select sum(" + _g._dataPayroll.payroll_trans_detail._money + ") from " + _g._dataPayroll.payroll_trans_detail._table + " where " + _g._dataPayroll.payroll_trans_detail._trans_flag + "=1 and " + _g._dataPayroll.payroll_trans_detail._trans_type + "=3 and " + _g._dataPayroll.payroll_trans_detail._employee + " =  " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + " " + WhereTransDetail + "),0) as " + _g._dataPayroll.payroll_trans._late_money + ",");
                        DataFormonth.Append(" coalesce((select sum(" + _g._dataPayroll.payroll_trans_detail._money + ") from " + _g._dataPayroll.payroll_trans_detail._table + " where " + _g._dataPayroll.payroll_trans_detail._trans_flag + "=1 and " + _g._dataPayroll.payroll_trans_detail._trans_type + "=4 and " + _g._dataPayroll.payroll_trans_detail._employee + " =  " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + " " + WhereTransDetail + "),0) as " + _g._dataPayroll.payroll_resource._ot_money + ",");


                        DataFormonth.Append(" " + _g._dataPayroll.payroll_trans._select_month + ",");
                            //จำนวนวันทำงาน
                        DataFormonth.Append("coalesce((sum(" + _g._dataPayroll.payroll_trans._working_day + ")),0) as working_day,");

                            //คิดงวด
                        DataFormonth.Append(periodData.ToString());

                            //เงินค่าจ้าง                           
                        DataFormonth.Append("coalesce((select " + _g._dataPayroll.payroll_employee._salary + " from " + _g._dataPayroll.payroll_employee._table + " where " + _g._dataPayroll.payroll_employee._code + " = " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + " ),0) as salary,");
                        DataFormonth.Append("(select coalesce((" + _g._dataPayroll.payroll_employee_salary._salary + "),0) as " + _g._dataPayroll.payroll_employee_salary._salary);
                        DataFormonth.Append(" from " + _g._dataPayroll.payroll_employee_salary._table);
                        DataFormonth.Append(" where " + _g._dataPayroll.payroll_employee_salary._code + " = " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + "");
                            //+ " and to_char(" + _g._dataPayroll.payroll_employee_salary._start_date + ",'yyyy') <= '" + (MyLib._myGlobal._intPhase(InYear) - 543).ToString() + "'"
                            //+ " and to_char(" + _g._dataPayroll.payroll_employee_salary._end_date + ",'yyyy') >= '" + (MyLib._myGlobal._intPhase(InYear) - 543).ToString() + "'"
                        DataFormonth.Append(" and to_char(" + _g._dataPayroll.payroll_employee_salary._start_date + ",'MM') <= to_char(" + _g._dataPayroll.payroll_trans._select_month + " + 1,'00')");
                        DataFormonth.Append(" and (to_char(" + _g._dataPayroll.payroll_employee_salary._end_date + ",'MM') >= to_char(" + _g._dataPayroll.payroll_trans._select_month + ",'00') or " + _g._dataPayroll.payroll_employee_salary._end_date + " = '1000-01-01')) as salary_vary,");
                            //จำนวนวันทำงาน
                        DataFormonth.Append("coalesce((select " + _g._dataPayroll.payroll_company_config._work_day + " from " + _g._dataPayroll.payroll_company_config._table + "),0) as work_day,");


                        break;
                    case _g.g._screenPayrollEnum.รายงานภาษีเงินได้_รายปี:
                    case _g.g._screenPayrollEnum.คำนวนภาษีรายปี:
                    case _g.g._screenPayrollEnum.ภงด_91:
                    case _g.g._screenPayrollEnum.ภงด_1_ก_ใบแนบ:
                    case _g.g._screenPayrollEnum.ภงด_1_ก:
                        GroupBy = " group by employee,select_month ";
                        OrderBy = " order by employee ";
                        WhereTransDetail = "";
                        WhereTransSub = " and (" + _g._dataPayroll.payroll_trans._select_year + " = '" + InYear + "') and (" + _g._dataPayroll.payroll_trans._select_month + " = " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._select_month + ")";
                        WhereTransDetail = " and (" + _g._dataPayroll.payroll_trans_detail._select_year + " = '" + InYear + "') and (" + _g._dataPayroll.payroll_trans._select_month + " = " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._select_month + ")";
                        
                            //เงินหัก-เงินได้
                        FileFormonth.Append(" " + _g._dataPayroll.payroll_trans._sow_money + " as " + _g._dataPayroll.payroll_resource._sow_money + ",");
                        FileFormonth.Append(" " + _g._dataPayroll.payroll_trans._leave_money + " as " + _g._dataPayroll.payroll_resource._leave_money + ",");
                        FileFormonth.Append(" " + _g._dataPayroll.payroll_trans._late_money + " as " + _g._dataPayroll.payroll_resource._late_money + ",");
                        FileFormonth.Append(" " + _g._dataPayroll.payroll_trans._ot_money + " as " + _g._dataPayroll.payroll_resource._ot_money + ",");
                        FileFormonth.Append(" " + _g._dataPayroll.payroll_trans._diligent_money + " as " + _g._dataPayroll.payroll_resource._diligent_money + ",");
                        FileFormonth.Append(" " + _g._dataPayroll.payroll_trans._prepay_money + " as " + _g._dataPayroll.payroll_resource._prepay_money + ",");
                        FileFormonth.Append(" " + _g._dataPayroll.payroll_trans._bonus_money + " as " + _g._dataPayroll.payroll_resource._bonus_money + ",");
                        FileFormonth.Append(" " + _g._dataPayroll.payroll_trans._other_income_money + " as " + _g._dataPayroll.payroll_resource._other_income_money + ",");
                        FileFormonth.Append(" " + _g._dataPayroll.payroll_trans._advance_money + " as " + _g._dataPayroll.payroll_resource._advance_money + ",");
                        FileFormonth.Append(" " + _g._dataPayroll.payroll_trans._insure_money + " as " + _g._dataPayroll.payroll_resource._insure_money + ",");
                        FileFormonth.Append(" " + _g._dataPayroll.payroll_trans._other_payout_money + " as " + _g._dataPayroll.payroll_resource._other_payout_money + ",");
                            //เงินกองทุน
                        FileFormonth.Append(" coalesce((");
                        FileFormonth.Append(" CASE salary_status");
                        FileFormonth.Append(" WHEN 0 THEN ");

                        FileFormonth.Append(" CASE " + _g._dataPayroll.payroll_employee._employee_type);
                        FileFormonth.Append(" WHEN 0 THEN ((salary * customer_rf_rate) / 100)");
                        FileFormonth.Append(" WHEN 1 THEN (((salary  * 4)*customer_rf_rate) / 100)");
                        FileFormonth.Append(" ELSE (((salary * working_day)*customer_rf_rate) / 100)");
                        FileFormonth.Append(" END");

                        FileFormonth.Append(" ELSE ");

                        FileFormonth.Append(" CASE " + _g._dataPayroll.payroll_employee._employee_type);
                        FileFormonth.Append(" WHEN 0 THEN ((salary_vary * customer_rf_rate) / 100)");
                        FileFormonth.Append(" WHEN 1 THEN (((salary_vary  * 4)*customer_rf_rate) / 100)");
                        FileFormonth.Append(" ELSE (((salary_vary * working_day)*customer_rf_rate) / 100)");
                        FileFormonth.Append(" END");

                        FileFormonth.Append(" END");
                        FileFormonth.Append(" ),0) as " + _g._dataPayroll.payroll_trans._reserve_money + ",");
                            //เงินประกันสังคม
                        FileFormonth.Append(" coalesce(( ");
                        FileFormonth.Append(" CASE salary_status");
                        FileFormonth.Append(" WHEN 0 THEN ");

                        FileFormonth.Append(" CASE " + _g._dataPayroll.payroll_employee._employee_type);
                        FileFormonth.Append(" WHEN 0 THEN ((salary * customer_ss_rate) / 100)");
                        FileFormonth.Append(" WHEN 1 THEN (((salary  * 4)*customer_ss_rate) / 100)");
                        FileFormonth.Append(" ELSE (((salary * working_day)*customer_ss_rate) / 100)");
                        FileFormonth.Append(" END");

                        FileFormonth.Append(" ELSE ");

                        FileFormonth.Append(" CASE " + _g._dataPayroll.payroll_employee._employee_type);
                        FileFormonth.Append(" WHEN 0 THEN ((salary_vary * customer_ss_rate) / 100)");
                        FileFormonth.Append(" WHEN 1 THEN (((salary_vary  * 4)*customer_ss_rate) / 100)");
                        FileFormonth.Append(" ELSE (((salary_vary * working_day)*customer_ss_rate) / 100)");
                        FileFormonth.Append(" END");

                        FileFormonth.Append(" END");
                        FileFormonth.Append(" ),0) as " + _g._dataPayroll.payroll_trans._society_insure_money + ",");
                            //เงินค่าจ้าง
                        FileFormonth.Append(" coalesce((");
                        FileFormonth.Append(" CASE salary_status");
                        FileFormonth.Append(" WHEN 0 THEN ");

                        FileFormonth.Append(" CASE " + _g._dataPayroll.payroll_employee._employee_type);
                        FileFormonth.Append(" WHEN 0 THEN salary ");
                        FileFormonth.Append(" WHEN 1 THEN (salary * 4 )");
                        FileFormonth.Append(" ELSE (salary) *(working_day)");
                        FileFormonth.Append(" END");

                        FileFormonth.Append(" ELSE ");

                        FileFormonth.Append(" CASE " + _g._dataPayroll.payroll_employee._employee_type);
                        FileFormonth.Append(" WHEN 0 THEN salary_vary ");
                        FileFormonth.Append(" WHEN 1 THEN (salary_vary * 4 )");
                        FileFormonth.Append(" ELSE (salary_vary) *(working_day)");
                        FileFormonth.Append(" END");

                        FileFormonth.Append(" END");
                        FileFormonth.Append(" ),0) as " + _g._dataPayroll.payroll_trans._wages_money + ",");

                        
                            //คำนวนเงินหัก-เงินได้                  
                        DataFormonth.Append(" coalesce((select sum(" + _g._dataPayroll.payroll_trans_detail._diligent_money + ") from " + _g._dataPayroll.payroll_trans_detail._table + " where " + _g._dataPayroll.payroll_trans_detail._trans_flag + "=1 and " + _g._dataPayroll.payroll_trans_detail._trans_type + "=5 and " + _g._dataPayroll.payroll_trans_detail._employee + " =  " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + " " + WhereTransDetail + "),0) as " + _g._dataPayroll.payroll_resource._diligent_money + ",");
                        DataFormonth.Append(" coalesce((select sum(" + _g._dataPayroll.payroll_trans_detail._prepay_money + ") from " + _g._dataPayroll.payroll_trans_detail._table + " where " + _g._dataPayroll.payroll_trans_detail._trans_flag + "=1 and " + _g._dataPayroll.payroll_trans_detail._trans_type + "=5 and " + _g._dataPayroll.payroll_trans_detail._employee + " =  " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + " " + WhereTransDetail + "),0) as " + _g._dataPayroll.payroll_resource._prepay_money + ",");
                        DataFormonth.Append(" coalesce((select sum(" + _g._dataPayroll.payroll_trans_detail._bonus_money + ") from " + _g._dataPayroll.payroll_trans_detail._table + " where " + _g._dataPayroll.payroll_trans_detail._trans_flag + "=1 and " + _g._dataPayroll.payroll_trans_detail._trans_type + "=5 and " + _g._dataPayroll.payroll_trans_detail._employee + " =  " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + " " + WhereTransDetail + "),0) as " + _g._dataPayroll.payroll_resource._bonus_money + ",");
                        DataFormonth.Append(" coalesce((select sum(" + _g._dataPayroll.payroll_trans_detail._other_income_money + ") from " + _g._dataPayroll.payroll_trans_detail._table + " where " + _g._dataPayroll.payroll_trans_detail._trans_flag + "=1 and " + _g._dataPayroll.payroll_trans_detail._trans_type + "=5 and " + _g._dataPayroll.payroll_trans_detail._employee + " =  " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + " " + WhereTransDetail + "),0) as " + _g._dataPayroll.payroll_resource._other_income_money + ",");
                        DataFormonth.Append(" coalesce((select sum(" + _g._dataPayroll.payroll_trans_detail._advance_money + ") from " + _g._dataPayroll.payroll_trans_detail._table + " where " + _g._dataPayroll.payroll_trans_detail._trans_flag + "=1 and " + _g._dataPayroll.payroll_trans_detail._trans_type + "=5 and " + _g._dataPayroll.payroll_trans_detail._employee + " =  " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + " " + WhereTransDetail + "),0) as " + _g._dataPayroll.payroll_resource._advance_money + ",");
                        DataFormonth.Append(" coalesce((select sum(" + _g._dataPayroll.payroll_trans_detail._insure_money + ") from " + _g._dataPayroll.payroll_trans_detail._table + " where " + _g._dataPayroll.payroll_trans_detail._trans_flag + "=1 and " + _g._dataPayroll.payroll_trans_detail._trans_type + "=5 and " + _g._dataPayroll.payroll_trans_detail._employee + " =  " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + " " + WhereTransDetail + "),0) as " + _g._dataPayroll.payroll_resource._insure_money + ",");
                        DataFormonth.Append(" coalesce((select sum(" + _g._dataPayroll.payroll_trans_detail._other_payout_money + ") from " + _g._dataPayroll.payroll_trans_detail._table + " where " + _g._dataPayroll.payroll_trans_detail._trans_flag + "=1 and " + _g._dataPayroll.payroll_trans_detail._trans_type + "=5 and " + _g._dataPayroll.payroll_trans_detail._employee + " =  " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + " " + WhereTransDetail + "),0) as " + _g._dataPayroll.payroll_resource._other_payout_money + ",");
                        DataFormonth.Append(" coalesce((select sum(" + _g._dataPayroll.payroll_trans_detail._money + ") from " + _g._dataPayroll.payroll_trans_detail._table + " where " + _g._dataPayroll.payroll_trans_detail._trans_flag + "=1 and " + _g._dataPayroll.payroll_trans_detail._trans_type + "=1 and " + _g._dataPayroll.payroll_trans_detail._employee + " =  " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + " " + WhereTransDetail + "),0) as " + _g._dataPayroll.payroll_trans._sow_money + ",");
                        DataFormonth.Append(" coalesce((select sum(" + _g._dataPayroll.payroll_trans_detail._money + ") from " + _g._dataPayroll.payroll_trans_detail._table + " where " + _g._dataPayroll.payroll_trans_detail._trans_flag + "=1 and " + _g._dataPayroll.payroll_trans_detail._trans_type + "=2 and " + _g._dataPayroll.payroll_trans_detail._employee + " =  " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + " " + WhereTransDetail + "),0) as " + _g._dataPayroll.payroll_trans._leave_money + ",");
                        DataFormonth.Append(" coalesce((select sum(" + _g._dataPayroll.payroll_trans_detail._money + ") from " + _g._dataPayroll.payroll_trans_detail._table + " where " + _g._dataPayroll.payroll_trans_detail._trans_flag + "=1 and " + _g._dataPayroll.payroll_trans_detail._trans_type + "=3 and " + _g._dataPayroll.payroll_trans_detail._employee + " =  " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + " " + WhereTransDetail + "),0) as " + _g._dataPayroll.payroll_trans._late_money + ",");
                        DataFormonth.Append(" coalesce((select sum(" + _g._dataPayroll.payroll_trans_detail._money + ") from " + _g._dataPayroll.payroll_trans_detail._table + " where " + _g._dataPayroll.payroll_trans_detail._trans_flag + "=1 and " + _g._dataPayroll.payroll_trans_detail._trans_type + "=4 and " + _g._dataPayroll.payroll_trans_detail._employee + " =  " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + " " + WhereTransDetail + "),0) as " + _g._dataPayroll.payroll_resource._ot_money + ",");
                        DataFormonth.Append(" " + _g._dataPayroll.payroll_trans._select_month + ",");
                            //จำนวนวันทำงาน
                        DataFormonth.Append("coalesce((sum(" + _g._dataPayroll.payroll_trans._working_day + ")),0) as working_day,");

                            //เงินค่าจ้าง                           
                        DataFormonth.Append("coalesce((select " + _g._dataPayroll.payroll_employee._salary + " from " + _g._dataPayroll.payroll_employee._table + " where " + _g._dataPayroll.payroll_employee._code + " = " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + " ),0) as salary,");
                        DataFormonth.Append("(select coalesce((" + _g._dataPayroll.payroll_employee_salary._salary + "),0) as " + _g._dataPayroll.payroll_employee_salary._salary);
                        DataFormonth.Append(" from " + _g._dataPayroll.payroll_employee_salary._table);
                        DataFormonth.Append(" where " + _g._dataPayroll.payroll_employee_salary._code + " = " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + "");
                        DataFormonth.Append(" and to_char(" + _g._dataPayroll.payroll_employee_salary._start_date + ",'yyyy') = '" + (MyLib._myGlobal._intPhase(InYear) - 543).ToString() + "'");
                        DataFormonth.Append(" and to_char(" + _g._dataPayroll.payroll_employee_salary._end_date + ",'yyyy') = '" + (MyLib._myGlobal._intPhase(InYear) - 543).ToString() + "'");
                        DataFormonth.Append(" and to_char(" + _g._dataPayroll.payroll_employee_salary._start_date + ",'MM') <= to_char(" + _g._dataPayroll.payroll_trans._select_month + " + 1,'00')");
                        DataFormonth.Append(" and (to_char(" + _g._dataPayroll.payroll_employee_salary._end_date + ",'MM') >= to_char(" + _g._dataPayroll.payroll_trans._select_month + ",'00') or " + _g._dataPayroll.payroll_employee_salary._end_date + " = '1000-01-01')) as salary_vary,");
                        //จำนวนวันทำงาน
                        //+ "coalesce((select " + _g._dataPayroll.payroll_company_config._work_day + " from " + _g._dataPayroll.payroll_company_config._table + "),0) as work_day,";
                        switch (ScreenName)
                        {
                            case _g.g._screenPayrollEnum.รายงานภาษีเงินได้_รายปี:
                            case _g.g._screenPayrollEnum.คำนวนภาษีรายปี:
                            case _g.g._screenPayrollEnum.ภงด_91:
                            case _g.g._screenPayrollEnum.ภงด_1_ก_ใบแนบ:
                            case _g.g._screenPayrollEnum.ภงด_1_ก:
                                FieldEmployeeHead.Append("title_name as title_name,");
                      FieldEmployeeHead.Append("tax_no as tax_no,");
                      FieldEmployeeHead.Append("id_code as id_code,");
                      FieldEmployeeHead.Append("house as house,");
                      FieldEmployeeHead.Append("room_no as room_no,");
                      FieldEmployeeHead.Append("floor_no as floor_no,");
                      FieldEmployeeHead.Append("village as village,");
                      FieldEmployeeHead.Append("house_no as house_no,");
                      FieldEmployeeHead.Append("crowd_no as crowd_no,");
                      FieldEmployeeHead.Append("lane as lane,");
                      FieldEmployeeHead.Append("road as road,");
                      FieldEmployeeHead.Append("locality as locality,");
                      FieldEmployeeHead.Append("amphur as amphur,");
                      FieldEmployeeHead.Append("province as province,");
                      FieldEmployeeHead.Append("postcode as postcode,");
                      FieldEmployeeHead.Append("telephone as telephone,");
                      FieldEmployeeHead.Append("birth_day as birth_day,");
                      FieldEmployeeHead.Append("spouse_name as spouse_name,");
                      FieldEmployeeHead.Append("spouse_id_code as spouse_id_code,");
                      FieldEmployeeHead.Append("spouse_birth_day as spouse_birth_day,");
                      FieldEmployeeHead.Append("father_id as father_id,");
                      FieldEmployeeHead.Append("mother_id as mother_id,");
                      FieldEmployeeHead.Append("father_law_id as father_law_id,");
                      FieldEmployeeHead.Append("mother_law_id as mother_law_id,");
                      FieldEmployeeHead.Append("child_study_qty as child_study_qty,");
                      FieldEmployeeHead.Append("child_no_study_qty as child_no_study_qty,");
                      FieldEmployeeHead.Append("buy_funds_reserves as buy_funds_reserves,");
                      FieldEmployeeHead.Append("buy_share as buy_share,");
                      FieldEmployeeHead.Append("except_funds_reserves as except_funds_reserves,");
                      FieldEmployeeHead.Append("except_gbk as except_gbk,");
                      FieldEmployeeHead.Append("except_teacher as except_teacher,");
                      FieldEmployeeHead.Append("except_65year as except_65year,");
                      FieldEmployeeHead.Append("except_spouse_65year as except_spouse_65year,");
                      FieldEmployeeHead.Append("except_labour_legislation as except_labour_legislation,");


                                FieldEmployeeData.Append("(select title_name from payroll_employee where payroll_employee.code = employee) as title_name,");
                      FieldEmployeeData.Append("(select tax_no from payroll_employee where payroll_employee.code = employee) as tax_no,");
                      FieldEmployeeData.Append("(select id_code from payroll_employee_detail where payroll_employee_detail.code = employee) as id_code,");
                      FieldEmployeeData.Append("(select house from payroll_employee_address where payroll_employee_address.code = employee) as house,");
                      FieldEmployeeData.Append("(select room_no from payroll_employee_address where payroll_employee_address.code = employee) as room_no,");
                      FieldEmployeeData.Append("(select floor_no from payroll_employee_address where payroll_employee_address.code = employee) as floor_no,");
                      FieldEmployeeData.Append("(select village from payroll_employee_address where payroll_employee_address.code = employee) as village,");
                      FieldEmployeeData.Append("(select house_no from payroll_employee_address where payroll_employee_address.code = employee) as house_no,");
                      FieldEmployeeData.Append("(select crowd_no from payroll_employee_address where payroll_employee_address.code = employee) as crowd_no,");
                      FieldEmployeeData.Append("(select lane from payroll_employee_address where payroll_employee_address.code = employee) as lane,");
                      FieldEmployeeData.Append("(select road from payroll_employee_address where payroll_employee_address.code = employee) as road,");
                      FieldEmployeeData.Append("(select locality from payroll_employee_address where payroll_employee_address.code = employee) as locality,");
                      FieldEmployeeData.Append("(select amphur from payroll_employee_address where payroll_employee_address.code = employee) as amphur,");
                      FieldEmployeeData.Append("(select province from payroll_employee_address where payroll_employee_address.code = employee) as province,");
                      FieldEmployeeData.Append("(select postcode from payroll_employee_address where payroll_employee_address.code = employee) as postcode,");
                      FieldEmployeeData.Append("(select telephone from payroll_employee_address where payroll_employee_address.code = employee) as telephone,");
                      FieldEmployeeData.Append("(select birth_day from payroll_employee_detail where payroll_employee_detail.code = employee) as birth_day,");
                      FieldEmployeeData.Append("(select spouse_name from payroll_employee_detail where payroll_employee_detail.code = employee) as spouse_name,");
                      FieldEmployeeData.Append("(select spouse_birth_day from payroll_employee_detail where payroll_employee_detail.code = employee) as spouse_birth_day,");
                      FieldEmployeeData.Append("(select spouse_id_code from payroll_employee_detail where payroll_employee_detail.code = employee) as spouse_id_code,");
                      FieldEmployeeData.Append("(select father_id from payroll_employee_detail where payroll_employee_detail.code = employee) as father_id,");
                      FieldEmployeeData.Append("(select mother_id from payroll_employee_detail where payroll_employee_detail.code = employee) as mother_id,");
                      FieldEmployeeData.Append("(select father_law_id from payroll_employee_detail where payroll_employee_detail.code = employee) as father_law_id,");
                      FieldEmployeeData.Append("(select mother_law_id from payroll_employee_detail where payroll_employee_detail.code = employee) as mother_law_id,");
                      FieldEmployeeData.Append("(select child_study_qty from payroll_employee_detail where payroll_employee_detail.code = employee) as child_study_qty,");
                      FieldEmployeeData.Append("(select child_no_study_qty from payroll_employee_detail where payroll_employee_detail.code = employee) as child_no_study_qty,");
                      FieldEmployeeData.Append("(select buy_funds_reserves from payroll_employee_detail where payroll_employee_detail.code = employee) as buy_funds_reserves,");
                      FieldEmployeeData.Append("(select buy_share from payroll_employee_detail where payroll_employee_detail.code = employee) as buy_share,");
                      FieldEmployeeData.Append("(select except_funds_reserves from payroll_employee_detail where payroll_employee_detail.code = employee) as except_funds_reserves,");
                      FieldEmployeeData.Append("(select except_gbk from payroll_employee_detail where payroll_employee_detail.code = employee) as except_gbk,");
                      FieldEmployeeData.Append("(select except_teacher from payroll_employee_detail where payroll_employee_detail.code = employee) as except_teacher,");
                      FieldEmployeeData.Append("(select except_65year from payroll_employee_detail where payroll_employee_detail.code = employee) as except_65year,");
                      FieldEmployeeData.Append("(select except_spouse_65year from payroll_employee_detail where payroll_employee_detail.code = employee) as except_spouse_65year,");
                      FieldEmployeeData.Append("(select except_labour_legislation from payroll_employee_detail where payroll_employee_detail.code = employee) as except_labour_legislation,");



                                break;
                        }
                        break;
                    case _g.g._screenPayrollEnum.รายงานภาษีเงินได้_รายเดือน:
                    case _g.g._screenPayrollEnum.ภงด_1:
                    case _g.g._screenPayrollEnum.ภงด_1_ใบแนบ:
                    case _g.g._screenPayrollEnum.คำนวนภาษีรายเดือน:
                    case _g.g._screenPayrollEnum.คำนวนเงินเดือน:
                    case _g.g._screenPayrollEnum.รายละเอียดเงินเดือน_ค่าจ้าง_ตามประเภทลูกจ้าง:
                    case _g.g._screenPayrollEnum.สลิปเงินเดือน:
                        
                            //เงินหัก-เงินได้
                                 FileFormonth.Append(" " + _g._dataPayroll.payroll_trans._sow_money + " as " + _g._dataPayroll.payroll_resource._sow_money + ",");
                                 FileFormonth.Append(" " + _g._dataPayroll.payroll_trans._leave_money + " as " + _g._dataPayroll.payroll_resource._leave_money + ",");
                                 FileFormonth.Append(" " + _g._dataPayroll.payroll_trans._late_money + " as " + _g._dataPayroll.payroll_resource._late_money + ",");
                                 FileFormonth.Append(" " + _g._dataPayroll.payroll_trans._ot_money + " as " + _g._dataPayroll.payroll_resource._ot_money + ",");
                                 FileFormonth.Append(" " + _g._dataPayroll.payroll_trans._diligent_money + " as " + _g._dataPayroll.payroll_resource._diligent_money + ",");
                                 FileFormonth.Append(" " + _g._dataPayroll.payroll_trans._prepay_money + " as " + _g._dataPayroll.payroll_resource._prepay_money + ",");
                                 FileFormonth.Append(" " + _g._dataPayroll.payroll_trans._bonus_money + " as " + _g._dataPayroll.payroll_resource._bonus_money + ",");
                                 FileFormonth.Append(" " + _g._dataPayroll.payroll_trans._other_income_money + " as " + _g._dataPayroll.payroll_resource._other_income_money + ",");
                                 FileFormonth.Append(" " + _g._dataPayroll.payroll_trans._advance_money + " as " + _g._dataPayroll.payroll_resource._advance_money + ",");
                                 FileFormonth.Append(" " + _g._dataPayroll.payroll_trans._insure_money + " as " + _g._dataPayroll.payroll_resource._insure_money + ",");
                                 FileFormonth.Append(" " + _g._dataPayroll.payroll_trans._other_payout_money + " as " + _g._dataPayroll.payroll_resource._other_payout_money + ",");

                            //ค่าจ้างสุทธิ
                                 FileFormonth.Append("(0) as " + _g._dataPayroll.payroll_resource._net_wages + ",");

                        
                            //คำนวนเงินหัก-เงินได้                  
                         DataFormonth.Append(" coalesce((select sum(" + _g._dataPayroll.payroll_trans_detail._diligent_money + ") from " + _g._dataPayroll.payroll_trans_detail._table + " where " + _g._dataPayroll.payroll_trans_detail._trans_flag + "=1 and " + _g._dataPayroll.payroll_trans_detail._trans_type + "=5 and " + _g._dataPayroll.payroll_trans_detail._employee + " =  " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + " " + WhereTransDetail + "),0) as " + _g._dataPayroll.payroll_resource._diligent_money + ",");
                         DataFormonth.Append(" coalesce((select sum(" + _g._dataPayroll.payroll_trans_detail._prepay_money + ") from " + _g._dataPayroll.payroll_trans_detail._table + " where " + _g._dataPayroll.payroll_trans_detail._trans_flag + "=1 and " + _g._dataPayroll.payroll_trans_detail._trans_type + "=5 and " + _g._dataPayroll.payroll_trans_detail._employee + " =  " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + " " + WhereTransDetail + "),0) as " + _g._dataPayroll.payroll_resource._prepay_money + ",");
                         DataFormonth.Append(" coalesce((select sum(" + _g._dataPayroll.payroll_trans_detail._bonus_money + ") from " + _g._dataPayroll.payroll_trans_detail._table + " where " + _g._dataPayroll.payroll_trans_detail._trans_flag + "=1 and " + _g._dataPayroll.payroll_trans_detail._trans_type + "=5 and " + _g._dataPayroll.payroll_trans_detail._employee + " =  " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + " " + WhereTransDetail + "),0) as " + _g._dataPayroll.payroll_resource._bonus_money + ",");
                         DataFormonth.Append(" coalesce((select sum(" + _g._dataPayroll.payroll_trans_detail._other_income_money + ") from " + _g._dataPayroll.payroll_trans_detail._table + " where " + _g._dataPayroll.payroll_trans_detail._trans_flag + "=1 and " + _g._dataPayroll.payroll_trans_detail._trans_type + "=5 and " + _g._dataPayroll.payroll_trans_detail._employee + " =  " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + " " + WhereTransDetail + "),0) as " + _g._dataPayroll.payroll_resource._other_income_money + ",");
                         DataFormonth.Append(" coalesce((select sum(" + _g._dataPayroll.payroll_trans_detail._advance_money + ") from " + _g._dataPayroll.payroll_trans_detail._table + " where " + _g._dataPayroll.payroll_trans_detail._trans_flag + "=1 and " + _g._dataPayroll.payroll_trans_detail._trans_type + "=5 and " + _g._dataPayroll.payroll_trans_detail._employee + " =  " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + " " + WhereTransDetail + "),0) as " + _g._dataPayroll.payroll_resource._advance_money + ",");
                         DataFormonth.Append(" coalesce((select sum(" + _g._dataPayroll.payroll_trans_detail._insure_money + ") from " + _g._dataPayroll.payroll_trans_detail._table + " where " + _g._dataPayroll.payroll_trans_detail._trans_flag + "=1 and " + _g._dataPayroll.payroll_trans_detail._trans_type + "=5 and " + _g._dataPayroll.payroll_trans_detail._employee + " =  " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + " " + WhereTransDetail + "),0) as " + _g._dataPayroll.payroll_resource._insure_money + ",");
                         DataFormonth.Append(" coalesce((select sum(" + _g._dataPayroll.payroll_trans_detail._other_payout_money + ") from " + _g._dataPayroll.payroll_trans_detail._table + " where " + _g._dataPayroll.payroll_trans_detail._trans_flag + "=1 and " + _g._dataPayroll.payroll_trans_detail._trans_type + "=5 and " + _g._dataPayroll.payroll_trans_detail._employee + " =  " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + " " + WhereTransDetail + "),0) as " + _g._dataPayroll.payroll_resource._other_payout_money + ",");
                         DataFormonth.Append(" coalesce((select sum(" + _g._dataPayroll.payroll_trans_detail._money + ") from " + _g._dataPayroll.payroll_trans_detail._table + " where " + _g._dataPayroll.payroll_trans_detail._trans_flag + "=1 and " + _g._dataPayroll.payroll_trans_detail._trans_type + "=1 and " + _g._dataPayroll.payroll_trans_detail._employee + " =  " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + " " + WhereTransDetail + "),0) as " + _g._dataPayroll.payroll_trans._sow_money + ",");
                         DataFormonth.Append(" coalesce((select sum(" + _g._dataPayroll.payroll_trans_detail._money + ") from " + _g._dataPayroll.payroll_trans_detail._table + " where " + _g._dataPayroll.payroll_trans_detail._trans_flag + "=1 and " + _g._dataPayroll.payroll_trans_detail._trans_type + "=2 and " + _g._dataPayroll.payroll_trans_detail._employee + " =  " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + " " + WhereTransDetail + "),0) as " + _g._dataPayroll.payroll_trans._leave_money + ",");
                         DataFormonth.Append(" coalesce((select sum(" + _g._dataPayroll.payroll_trans_detail._money + ") from " + _g._dataPayroll.payroll_trans_detail._table + " where " + _g._dataPayroll.payroll_trans_detail._trans_flag + "=1 and " + _g._dataPayroll.payroll_trans_detail._trans_type + "=3 and " + _g._dataPayroll.payroll_trans_detail._employee + " =  " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + " " + WhereTransDetail + "),0) as " + _g._dataPayroll.payroll_trans._late_money + ",");
                         DataFormonth.Append(" coalesce((select sum(" + _g._dataPayroll.payroll_trans_detail._money + ") from " + _g._dataPayroll.payroll_trans_detail._table + " where " + _g._dataPayroll.payroll_trans_detail._trans_flag + "=1 and " + _g._dataPayroll.payroll_trans_detail._trans_type + "=4 and " + _g._dataPayroll.payroll_trans_detail._employee + " =  " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + " " + WhereTransDetail + "),0) as " + _g._dataPayroll.payroll_resource._ot_money + ",");


                        switch (ScreenName)
                        {
                            case _g.g._screenPayrollEnum.คำนวนเงินเดือน:
                            case _g.g._screenPayrollEnum.รายละเอียดเงินเดือน_ค่าจ้าง_ตามประเภทลูกจ้าง:
                            case _g.g._screenPayrollEnum.สลิปเงินเดือน:
                                GroupBy = " group by employee ";
                                OrderBy = " order by employee ";
                                FieldSalarySlipHead.Append("side_name as side_name,");
                                     FieldSalarySlipHead.Append("section_name as section_name,");
                                     FieldSalarySlipHead.Append("work_title_name as work_title_name,");
                                     FieldSalarySlipHead.Append("book_bank_no as book_bank_no,");
                                    //+ "working_day as working_day,"
                                     FieldSalarySlipHead.Append("(0) as sumincome,");
                                     FieldSalarySlipHead.Append("(0) as sumpayout,");

                                                   //+ "reserve_money_in_month as reserve_money_in_month,"
                                    //+ "society_insure_money_in_month as society_insure_money_in_month,"

                                     FieldSalarySlipHead.Append(" coalesce((");
                                     FieldSalarySlipHead.Append(" CASE salary_status");
                                     FieldSalarySlipHead.Append(" WHEN 0 THEN reserve_money_in_month ");
                                     FieldSalarySlipHead.Append(" ELSE reserve_money_in_month_vary");
                                     FieldSalarySlipHead.Append(" END");
                                     FieldSalarySlipHead.Append(" ),0) as reserve_money_in_month,");

                                     FieldSalarySlipHead.Append(" coalesce((");
                                     FieldSalarySlipHead.Append(" CASE salary_status");
                                     FieldSalarySlipHead.Append(" WHEN 0 THEN society_insure_money_in_month ");
                                     FieldSalarySlipHead.Append(" ELSE society_insure_money_in_month_vary");
                                     FieldSalarySlipHead.Append(" END");
                                     FieldSalarySlipHead.Append(" ),0) as society_insure_money_in_month,");

                                     FieldSalarySlipHead.Append(" coalesce((");
                                     FieldSalarySlipHead.Append(" CASE salary_status");
                                     FieldSalarySlipHead.Append(" WHEN 0 THEN wages_money_in_month ");
                                     FieldSalarySlipHead.Append(" ELSE wages_money_in_month_vary");
                                     FieldSalarySlipHead.Append(" END");
                                     FieldSalarySlipHead.Append(" ),0) as wages_money_in_month,");

                                     FieldSalarySlipHead.Append(" coalesce((");
                                     FieldSalarySlipHead.Append(" CASE salary_status");
                                     FieldSalarySlipHead.Append(" WHEN 0 THEN reserve_money ");
                                     FieldSalarySlipHead.Append(" ELSE reserve_money_vary");
                                     FieldSalarySlipHead.Append(" END");
                                     FieldSalarySlipHead.Append(" ),0) as reserve_money,");

                                     FieldSalarySlipHead.Append(" coalesce((");
                                     FieldSalarySlipHead.Append(" CASE salary_status");
                                     FieldSalarySlipHead.Append(" WHEN 0 THEN society_insure_money ");
                                     FieldSalarySlipHead.Append(" ELSE society_insure_money_vary");
                                     FieldSalarySlipHead.Append(" END");
                                     FieldSalarySlipHead.Append(" ),0) as society_insure_money,");

                                     FieldSalarySlipHead.Append(" coalesce((");
                                     FieldSalarySlipHead.Append(" CASE salary_status");
                                     FieldSalarySlipHead.Append(" WHEN 0 THEN wages_money ");
                                     FieldSalarySlipHead.Append(" ELSE wages_money_vary");
                                     FieldSalarySlipHead.Append(" END");
                                     FieldSalarySlipHead.Append(" ),0) as wages_money,");

                                                   //+ "wages_money_in_month as wages_money_in_month,"
                                     FieldSalarySlipHead.Append("diligent_money_in_month as diligent_money_in_month,");
                                     FieldSalarySlipHead.Append("prepay_money_in_month as prepay_money_in_month,");
                                     FieldSalarySlipHead.Append("bonus_money_in_month as bonus_money_in_month,");
                                     FieldSalarySlipHead.Append("other_income_money_in_month as other_income_money_in_month,");
                                     FieldSalarySlipHead.Append("advance_money_in_month as advance_money_in_month,");
                                     FieldSalarySlipHead.Append("insure_money_in_month as insure_money_in_month,");
                                     FieldSalarySlipHead.Append("other_payout_money_in_month as other_payout_money_in_month,");
                                     FieldSalarySlipHead.Append("sow_money_in_month as sow_money_in_month,");
                                     FieldSalarySlipHead.Append("leave_money_in_month as leave_money_in_month,");
                                     FieldSalarySlipHead.Append("late_money_in_month as late_money_in_month,");
                                     FieldSalarySlipHead.Append("ot_money_in_month as ot_money_in_month,");

                                FieldSalarySlipData.Append("(select (select " + _g._dataPayroll.payroll_side_list._name + " from " + _g._dataPayroll.payroll_side_list._table + " where " + _g._dataPayroll.payroll_side_list._table + "." + _g._dataPayroll.payroll_side_list._code + "= " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._side_code + ") as side_name from " + _g._dataPayroll.payroll_employee._table + " where " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + " = " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + ") as side_name,");
                                FieldSalarySlipData.Append("(select (select " + _g._dataPayroll.payroll_section_list._name + " from " + _g._dataPayroll.payroll_section_list._table + " where " + _g._dataPayroll.payroll_section_list._table + "." + _g._dataPayroll.payroll_section_list._code + "= " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._section_code + ") as section_name from " + _g._dataPayroll.payroll_employee._table + " where " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + " = " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + ") as section_name,");
                                FieldSalarySlipData.Append("(select (select " + _g._dataPayroll.payroll_work_title._name + " from " + _g._dataPayroll.payroll_work_title._table + " where " + _g._dataPayroll.payroll_work_title._table + "." + _g._dataPayroll.payroll_work_title._code + "= " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._work_title + ") as work_title_name from " + _g._dataPayroll.payroll_employee._table + " where " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + " = " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + ") as work_title_name,");
                                FieldSalarySlipData.Append("(select " + _g._dataPayroll.payroll_employee._book_bank_no + " from " + _g._dataPayroll.payroll_employee._table + " where " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + " = " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + ") as book_bank_no,");
                                    //จำนวนวันทำงาน
                                FieldSalarySlipData.Append("coalesce((sum(" + _g._dataPayroll.payroll_trans._working_day + ")),0) as working_day,");
                                    //เงินหัก เงินได้
                                FieldSalarySlipData.Append(" coalesce((select sum(" + _g._dataPayroll.payroll_trans_detail._diligent_money + ") from " + _g._dataPayroll.payroll_trans_detail._table + " where " + _g._dataPayroll.payroll_trans_detail._trans_flag + "=1 and " + _g._dataPayroll.payroll_trans_detail._trans_type + "=5 and " + _g._dataPayroll.payroll_trans_detail._employee + " =  " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + " " + WhereTransDetailInMonth + "),0) as diligent_money_in_month,");
                                FieldSalarySlipData.Append(" coalesce((select sum(" + _g._dataPayroll.payroll_trans_detail._prepay_money + ") from " + _g._dataPayroll.payroll_trans_detail._table + " where " + _g._dataPayroll.payroll_trans_detail._trans_flag + "=1 and " + _g._dataPayroll.payroll_trans_detail._trans_type + "=5 and " + _g._dataPayroll.payroll_trans_detail._employee + " =  " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + " " + WhereTransDetailInMonth + "),0) as prepay_money_in_month,");
                                FieldSalarySlipData.Append(" coalesce((select sum(" + _g._dataPayroll.payroll_trans_detail._bonus_money + ") from " + _g._dataPayroll.payroll_trans_detail._table + " where " + _g._dataPayroll.payroll_trans_detail._trans_flag + "=1 and " + _g._dataPayroll.payroll_trans_detail._trans_type + "=5 and " + _g._dataPayroll.payroll_trans_detail._employee + " =  " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + " " + WhereTransDetailInMonth + "),0) as bonus_money_in_month,");
                                FieldSalarySlipData.Append(" coalesce((select sum(" + _g._dataPayroll.payroll_trans_detail._other_income_money + ") from " + _g._dataPayroll.payroll_trans_detail._table + " where " + _g._dataPayroll.payroll_trans_detail._trans_flag + "=1 and " + _g._dataPayroll.payroll_trans_detail._trans_type + "=5 and " + _g._dataPayroll.payroll_trans_detail._employee + " =  " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + " " + WhereTransDetailInMonth + "),0) as other_income_money_in_month,");
                                FieldSalarySlipData.Append(" coalesce((select sum(" + _g._dataPayroll.payroll_trans_detail._advance_money + ") from " + _g._dataPayroll.payroll_trans_detail._table + " where " + _g._dataPayroll.payroll_trans_detail._trans_flag + "=1 and " + _g._dataPayroll.payroll_trans_detail._trans_type + "=5 and " + _g._dataPayroll.payroll_trans_detail._employee + " =  " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + " " + WhereTransDetailInMonth + "),0) as advance_money_in_month,");
                                FieldSalarySlipData.Append(" coalesce((select sum(" + _g._dataPayroll.payroll_trans_detail._insure_money + ") from " + _g._dataPayroll.payroll_trans_detail._table + " where " + _g._dataPayroll.payroll_trans_detail._trans_flag + "=1 and " + _g._dataPayroll.payroll_trans_detail._trans_type + "=5 and " + _g._dataPayroll.payroll_trans_detail._employee + " =  " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + " " + WhereTransDetailInMonth + "),0) as insure_money_in_month,");
                                FieldSalarySlipData.Append(" coalesce((select sum(" + _g._dataPayroll.payroll_trans_detail._other_payout_money + ") from " + _g._dataPayroll.payroll_trans_detail._table + " where " + _g._dataPayroll.payroll_trans_detail._trans_flag + "=1 and " + _g._dataPayroll.payroll_trans_detail._trans_type + "=5 and " + _g._dataPayroll.payroll_trans_detail._employee + " =  " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + " " + WhereTransDetailInMonth + "),0) as other_payout_money_in_month,");
                                FieldSalarySlipData.Append(" coalesce((select sum(" + _g._dataPayroll.payroll_trans_detail._money + ") from " + _g._dataPayroll.payroll_trans_detail._table + " where " + _g._dataPayroll.payroll_trans_detail._trans_flag + "=1 and " + _g._dataPayroll.payroll_trans_detail._trans_type + "=1 and " + _g._dataPayroll.payroll_trans_detail._employee + " =  " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + " " + WhereTransDetailInMonth + "),0) as sow_money_in_month,");
                                FieldSalarySlipData.Append(" coalesce((select sum(" + _g._dataPayroll.payroll_trans_detail._money + ") from " + _g._dataPayroll.payroll_trans_detail._table + " where " + _g._dataPayroll.payroll_trans_detail._trans_flag + "=1 and " + _g._dataPayroll.payroll_trans_detail._trans_type + "=2 and " + _g._dataPayroll.payroll_trans_detail._employee + " =  " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + " " + WhereTransDetailInMonth + "),0) as leave_money_in_month,");
                                FieldSalarySlipData.Append(" coalesce((select sum(" + _g._dataPayroll.payroll_trans_detail._money + ") from " + _g._dataPayroll.payroll_trans_detail._table + " where " + _g._dataPayroll.payroll_trans_detail._trans_flag + "=1 and " + _g._dataPayroll.payroll_trans_detail._trans_type + "=3 and " + _g._dataPayroll.payroll_trans_detail._employee + " =  " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + " " + WhereTransDetailInMonth + "),0) as late_money_in_month,");
                                FieldSalarySlipData.Append(" coalesce((select sum(" + _g._dataPayroll.payroll_trans_detail._money + ") from " + _g._dataPayroll.payroll_trans_detail._table + " where " + _g._dataPayroll.payroll_trans_detail._trans_flag + "=1 and " + _g._dataPayroll.payroll_trans_detail._trans_type + "=4 and " + _g._dataPayroll.payroll_trans_detail._employee + " =  " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + " " + WhereTransDetailInMonth + "),0) as ot_money_in_month,");
                                    //คิดกองทุนสำรองฯ

                                FieldSalarySlipData.Append(" coalesce((select ");
                                FieldSalarySlipData.Append(" CASE " + _g._dataPayroll.payroll_employee._employee_type);
                                FieldSalarySlipData.Append(" WHEN 0 THEN ");
                                FieldSalarySlipData.Append("((select coalesce((" + _g._dataPayroll.payroll_employee_salary._salary + "),0) as " + _g._dataPayroll.payroll_employee_salary._salary);
                                FieldSalarySlipData.Append(" from " + _g._dataPayroll.payroll_employee_salary._table);
                                FieldSalarySlipData.Append(" where " + _g._dataPayroll.payroll_employee_salary._code + " = " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + "");
                                FieldSalarySlipData.Append(" and " + _g._dataPayroll.payroll_employee_salary._start_date + " <= '" + StartDay + "'");
                                FieldSalarySlipData.Append(" and (" + _g._dataPayroll.payroll_employee_salary._end_date + " >= '" + EndDay + "' or " + _g._dataPayroll.payroll_employee_salary._end_date + " = '1000-01-01'))  * ((select " + _g._dataPayroll.payroll_company_config._customer_rf_rate + " from " + _g._dataPayroll.payroll_company_config._table + " ) / 100))");
                                FieldSalarySlipData.Append(" WHEN 1 THEN ");
                                FieldSalarySlipData.Append("((");
                                FieldSalarySlipData.Append("select coalesce((" + _g._dataPayroll.payroll_employee_salary._salary + "),0) as " + _g._dataPayroll.payroll_employee_salary._salary);
                                FieldSalarySlipData.Append(" from " + _g._dataPayroll.payroll_employee_salary._table);
                                FieldSalarySlipData.Append(" where " + _g._dataPayroll.payroll_employee_salary._code + " = " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + "");
                                FieldSalarySlipData.Append(" and " + _g._dataPayroll.payroll_employee_salary._start_date + " <= '" + StartDay + "'");
                                FieldSalarySlipData.Append(" and (" + _g._dataPayroll.payroll_employee_salary._end_date + " >= '" + EndDay + "' or " + _g._dataPayroll.payroll_employee_salary._end_date + " = '1000-01-01')");
                                FieldSalarySlipData.Append(") * 4) * ((select " + _g._dataPayroll.payroll_company_config._customer_rf_rate + " from " + _g._dataPayroll.payroll_company_config._table + " ) / 100)");
                                FieldSalarySlipData.Append(" ELSE ");
                                FieldSalarySlipData.Append("((");
                                FieldSalarySlipData.Append("select coalesce((" + _g._dataPayroll.payroll_employee_salary._salary + "),0) as " + _g._dataPayroll.payroll_employee_salary._salary);
                                FieldSalarySlipData.Append(" from " + _g._dataPayroll.payroll_employee_salary._table);
                                FieldSalarySlipData.Append(" where " + _g._dataPayroll.payroll_employee_salary._code + " = " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + "");
                                FieldSalarySlipData.Append(" and " + _g._dataPayroll.payroll_employee_salary._start_date + " <= '" + StartDay + "'");
                                FieldSalarySlipData.Append(" and (" + _g._dataPayroll.payroll_employee_salary._end_date + " >= '" + EndDay + "' or " + _g._dataPayroll.payroll_employee_salary._end_date + " = '1000-01-01')");
                                FieldSalarySlipData.Append(") * (select sum(" + _g._dataPayroll.payroll_trans._working_day + ") from " + _g._dataPayroll.payroll_trans._table + " where " + _g._dataPayroll.payroll_trans._employee + "=" + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + " " + WhereTransSubInMonth + ")) * ((select " + _g._dataPayroll.payroll_company_config._customer_rf_rate + " from " + _g._dataPayroll.payroll_company_config._table + " ) / 100)");
                                FieldSalarySlipData.Append(" END");
                                FieldSalarySlipData.Append(" from " + _g._dataPayroll.payroll_employee._table + " where " + _g._dataPayroll.payroll_employee._code + " = " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + "),0) as reserve_money_in_month_vary,");

                                FieldSalarySlipData.Append(" coalesce((select ");
                                FieldSalarySlipData.Append(" CASE " + _g._dataPayroll.payroll_employee._employee_type);
                                FieldSalarySlipData.Append(" WHEN 0 THEN (" + _g._dataPayroll.payroll_employee._salary + " * ((select " + _g._dataPayroll.payroll_company_config._customer_rf_rate + " from " + _g._dataPayroll.payroll_company_config._table + " ) / 100))");
                                FieldSalarySlipData.Append(" WHEN 1 THEN ((" + _g._dataPayroll.payroll_employee._salary + " * 4) * ((select " + _g._dataPayroll.payroll_company_config._customer_rf_rate + " from " + _g._dataPayroll.payroll_company_config._table + " ) / 100))");
                                FieldSalarySlipData.Append(" ELSE ((" + _g._dataPayroll.payroll_employee._salary + " * (select sum(" + _g._dataPayroll.payroll_trans._working_day + ") from " + _g._dataPayroll.payroll_trans._table + " where " + _g._dataPayroll.payroll_trans._employee + "=" + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + " " + WhereTransSubInMonth + ")) * ((select " + _g._dataPayroll.payroll_company_config._customer_rf_rate + " from " + _g._dataPayroll.payroll_company_config._table + " ) / 100))");
                                FieldSalarySlipData.Append(" END");
                                FieldSalarySlipData.Append(" from " + _g._dataPayroll.payroll_employee._table + " where " + _g._dataPayroll.payroll_employee._code + " = " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + "),0) as reserve_money_in_month,");

                                     //คิดกองทุนสำรองฯ
                                FieldSalarySlipData.Append(" coalesce((select ");
                                FieldSalarySlipData.Append(" CASE " + _g._dataPayroll.payroll_employee._employee_type);
                                FieldSalarySlipData.Append(" WHEN 0 THEN ");
                                FieldSalarySlipData.Append("((select coalesce((" + _g._dataPayroll.payroll_employee_salary._salary + "),0) as " + _g._dataPayroll.payroll_employee_salary._salary);
                                FieldSalarySlipData.Append(" from " + _g._dataPayroll.payroll_employee_salary._table);
                                FieldSalarySlipData.Append(" where " + _g._dataPayroll.payroll_employee_salary._code + " = " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + "");
                                FieldSalarySlipData.Append(" and " + _g._dataPayroll.payroll_employee_salary._start_date + " <= '" + FromDate + "'");
                                FieldSalarySlipData.Append(" and (" + _g._dataPayroll.payroll_employee_salary._end_date + " >= '" + ToDate + "' or " + _g._dataPayroll.payroll_employee_salary._end_date + " = '1000-01-01'))  * ((select " + _g._dataPayroll.payroll_company_config._customer_rf_rate + " from " + _g._dataPayroll.payroll_company_config._table + " ) / 100))");
                                FieldSalarySlipData.Append(" WHEN 1 THEN ");
                                FieldSalarySlipData.Append("((");
                                FieldSalarySlipData.Append("select coalesce((" + _g._dataPayroll.payroll_employee_salary._salary + "),0) as " + _g._dataPayroll.payroll_employee_salary._salary);
                                FieldSalarySlipData.Append(" from " + _g._dataPayroll.payroll_employee_salary._table);
                                FieldSalarySlipData.Append(" where " + _g._dataPayroll.payroll_employee_salary._code + " = " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + "");
                                FieldSalarySlipData.Append(" and " + _g._dataPayroll.payroll_employee_salary._start_date + " <= '" + FromDate + "'");
                                FieldSalarySlipData.Append(" and (" + _g._dataPayroll.payroll_employee_salary._end_date + " >= '" + ToDate + "' or " + _g._dataPayroll.payroll_employee_salary._end_date + " = '1000-01-01')");
                                FieldSalarySlipData.Append(") * 4) * ((select " + _g._dataPayroll.payroll_company_config._customer_rf_rate + " from " + _g._dataPayroll.payroll_company_config._table + " ) / 100)");
                                FieldSalarySlipData.Append(" ELSE ");
                                FieldSalarySlipData.Append("((");
                                FieldSalarySlipData.Append("select coalesce((" + _g._dataPayroll.payroll_employee_salary._salary + "),0) as " + _g._dataPayroll.payroll_employee_salary._salary);
                                FieldSalarySlipData.Append(" from " + _g._dataPayroll.payroll_employee_salary._table);
                                FieldSalarySlipData.Append(" where " + _g._dataPayroll.payroll_employee_salary._code + " = " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + "");
                                FieldSalarySlipData.Append(" and " + _g._dataPayroll.payroll_employee_salary._start_date + " <= '" + FromDate + "'");
                                FieldSalarySlipData.Append(" and (" + _g._dataPayroll.payroll_employee_salary._end_date + " >= '" + ToDate + "' or " + _g._dataPayroll.payroll_employee_salary._end_date + " = '1000-01-01')");
                                FieldSalarySlipData.Append(") * (select sum(" + _g._dataPayroll.payroll_trans._working_day + ") from " + _g._dataPayroll.payroll_trans._table + " where " + _g._dataPayroll.payroll_trans._employee + "=" + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + " " + WhereTransSub + ")) * ((select " + _g._dataPayroll.payroll_company_config._customer_rf_rate + " from " + _g._dataPayroll.payroll_company_config._table + " ) / 100)");
                                FieldSalarySlipData.Append(" END");
                                FieldSalarySlipData.Append(" from " + _g._dataPayroll.payroll_employee._table + " where " + _g._dataPayroll.payroll_employee._code + " = " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + "),0) as reserve_money_vary,");

                                FieldSalarySlipData.Append(" coalesce((select ");
                                FieldSalarySlipData.Append(" CASE " + _g._dataPayroll.payroll_employee._employee_type);
                                FieldSalarySlipData.Append(" WHEN 0 THEN (" + _g._dataPayroll.payroll_employee._salary + " * ((select " + _g._dataPayroll.payroll_company_config._customer_rf_rate + " from " + _g._dataPayroll.payroll_company_config._table + " ) / 100))");
                                FieldSalarySlipData.Append(" WHEN 1 THEN ((" + _g._dataPayroll.payroll_employee._salary + " * 4 ) * ((select " + _g._dataPayroll.payroll_company_config._customer_rf_rate + " from " + _g._dataPayroll.payroll_company_config._table + " ) / 100))");
                                FieldSalarySlipData.Append(" ELSE ((" + _g._dataPayroll.payroll_employee._salary + " * (select sum(" + _g._dataPayroll.payroll_trans._working_day + ") from " + _g._dataPayroll.payroll_trans._table + " where " + _g._dataPayroll.payroll_trans._employee + "=" + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + " " + WhereTransSub + ")) * ((select " + _g._dataPayroll.payroll_company_config._customer_rf_rate + " from " + _g._dataPayroll.payroll_company_config._table + " ) / 100))");
                                FieldSalarySlipData.Append(" END");
                                FieldSalarySlipData.Append(" from " + _g._dataPayroll.payroll_employee._table + " where " + _g._dataPayroll.payroll_employee._code + " = " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + "),0) as " + _g._dataPayroll.payroll_trans._reserve_money + ",");

                                    //คิดประกันสังคม

                                FieldSalarySlipData.Append(" coalesce((select ");
                                FieldSalarySlipData.Append(" CASE " + _g._dataPayroll.payroll_employee._employee_type);
                                FieldSalarySlipData.Append(" WHEN 0 THEN ");
                                FieldSalarySlipData.Append("((select coalesce((" + _g._dataPayroll.payroll_employee_salary._salary + "),0) as " + _g._dataPayroll.payroll_employee_salary._salary);
                                FieldSalarySlipData.Append(" from " + _g._dataPayroll.payroll_employee_salary._table);
                                FieldSalarySlipData.Append(" where " + _g._dataPayroll.payroll_employee_salary._code + " = " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + "");
                                FieldSalarySlipData.Append(" and " + _g._dataPayroll.payroll_employee_salary._start_date + " <= '" + StartDay + "'");
                                FieldSalarySlipData.Append(" and (" + _g._dataPayroll.payroll_employee_salary._end_date + " >= '" + EndDay + "' or " + _g._dataPayroll.payroll_employee_salary._end_date + " = '1000-01-01'))  * ((select " + _g._dataPayroll.payroll_company_config._customer_ss_rate + " from " + _g._dataPayroll.payroll_company_config._table + " ) / 100))");
                                FieldSalarySlipData.Append(" WHEN 1 THEN ");
                                FieldSalarySlipData.Append("((");
                                FieldSalarySlipData.Append("select coalesce((" + _g._dataPayroll.payroll_employee_salary._salary + "),0) as " + _g._dataPayroll.payroll_employee_salary._salary);
                                FieldSalarySlipData.Append(" from " + _g._dataPayroll.payroll_employee_salary._table);
                                FieldSalarySlipData.Append(" where " + _g._dataPayroll.payroll_employee_salary._code + " = " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + "");
                                FieldSalarySlipData.Append(" and " + _g._dataPayroll.payroll_employee_salary._start_date + " <= '" + StartDay + "'");
                                FieldSalarySlipData.Append(" and (" + _g._dataPayroll.payroll_employee_salary._end_date + " >= '" + EndDay + "' or " + _g._dataPayroll.payroll_employee_salary._end_date + " = '1000-01-01')");
                                FieldSalarySlipData.Append(") * 4) * ((select " + _g._dataPayroll.payroll_company_config._customer_ss_rate + " from " + _g._dataPayroll.payroll_company_config._table + " ) / 100)");
                                FieldSalarySlipData.Append(" ELSE ");
                                FieldSalarySlipData.Append("((");
                                FieldSalarySlipData.Append("select coalesce((" + _g._dataPayroll.payroll_employee_salary._salary + "),0) as " + _g._dataPayroll.payroll_employee_salary._salary);
                                FieldSalarySlipData.Append(" from " + _g._dataPayroll.payroll_employee_salary._table);
                                FieldSalarySlipData.Append(" where " + _g._dataPayroll.payroll_employee_salary._code + " = " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + "");
                                FieldSalarySlipData.Append(" and " + _g._dataPayroll.payroll_employee_salary._start_date + " <= '" + StartDay + "'");
                                FieldSalarySlipData.Append(" and (" + _g._dataPayroll.payroll_employee_salary._end_date + " >= '" + EndDay + "' or " + _g._dataPayroll.payroll_employee_salary._end_date + " = '1000-01-01')");
                                FieldSalarySlipData.Append(") * (select sum(" + _g._dataPayroll.payroll_trans._working_day + ") from " + _g._dataPayroll.payroll_trans._table + " where " + _g._dataPayroll.payroll_trans._employee + "=" + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + " " + WhereTransSubInMonth + ")) * ((select " + _g._dataPayroll.payroll_company_config._customer_ss_rate + " from " + _g._dataPayroll.payroll_company_config._table + " ) / 100)");
                                FieldSalarySlipData.Append(" END");
                                FieldSalarySlipData.Append(" from " + _g._dataPayroll.payroll_employee._table + " where " + _g._dataPayroll.payroll_employee._code + " = " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + "),0) as society_insure_money_in_month_vary,");

                                FieldSalarySlipData.Append(" coalesce((select ");
                                FieldSalarySlipData.Append(" CASE " + _g._dataPayroll.payroll_employee._employee_type);
                                FieldSalarySlipData.Append(" WHEN 0 THEN (" + _g._dataPayroll.payroll_employee._salary + " * ((select " + _g._dataPayroll.payroll_company_config._customer_ss_rate + " from " + _g._dataPayroll.payroll_company_config._table + " ) / 100))");
                                FieldSalarySlipData.Append(" WHEN 1 THEN ((" + _g._dataPayroll.payroll_employee._salary + " * 4 ) * ((select " + _g._dataPayroll.payroll_company_config._customer_ss_rate + " from " + _g._dataPayroll.payroll_company_config._table + " ) / 100))");
                                FieldSalarySlipData.Append(" ELSE ((" + _g._dataPayroll.payroll_employee._salary + " * (select sum(" + _g._dataPayroll.payroll_trans._working_day + ") from " + _g._dataPayroll.payroll_trans._table + " where " + _g._dataPayroll.payroll_trans._employee + "=" + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + " " + WhereTransSubInMonth + ")) * ((select " + _g._dataPayroll.payroll_company_config._customer_ss_rate + " from " + _g._dataPayroll.payroll_company_config._table + " ) / 100))");
                                FieldSalarySlipData.Append(" END");
                                FieldSalarySlipData.Append(" from " + _g._dataPayroll.payroll_employee._table + " where " + _g._dataPayroll.payroll_employee._code + " = " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + "),0) as society_insure_money_in_month,");

                                      //คิดประกันสังคม

                                FieldSalarySlipData.Append(" coalesce((select ");
                                FieldSalarySlipData.Append(" CASE " + _g._dataPayroll.payroll_employee._employee_type);
                                FieldSalarySlipData.Append(" WHEN 0 THEN ");
                                FieldSalarySlipData.Append("((select coalesce((" + _g._dataPayroll.payroll_employee_salary._salary + "),0) as " + _g._dataPayroll.payroll_employee_salary._salary);
                                FieldSalarySlipData.Append(" from " + _g._dataPayroll.payroll_employee_salary._table);
                                FieldSalarySlipData.Append(" where " + _g._dataPayroll.payroll_employee_salary._code + " = " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + "");
                                FieldSalarySlipData.Append(" and " + _g._dataPayroll.payroll_employee_salary._start_date + " <= '" + FromDate + "'");
                                FieldSalarySlipData.Append(" and (" + _g._dataPayroll.payroll_employee_salary._end_date + " >= '" + ToDate + "' or " + _g._dataPayroll.payroll_employee_salary._end_date + " = '1000-01-01'))  * ((select " + _g._dataPayroll.payroll_company_config._customer_ss_rate + " from " + _g._dataPayroll.payroll_company_config._table + " ) / 100))");
                                FieldSalarySlipData.Append(" WHEN 1 THEN ");
                                FieldSalarySlipData.Append("((");
                                FieldSalarySlipData.Append("select coalesce((" + _g._dataPayroll.payroll_employee_salary._salary + "),0) as " + _g._dataPayroll.payroll_employee_salary._salary);
                                FieldSalarySlipData.Append(" from " + _g._dataPayroll.payroll_employee_salary._table);
                                FieldSalarySlipData.Append(" where " + _g._dataPayroll.payroll_employee_salary._code + " = " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + "");
                                FieldSalarySlipData.Append(" and " + _g._dataPayroll.payroll_employee_salary._start_date + " <= '" + FromDate + "'");
                                FieldSalarySlipData.Append(" and (" + _g._dataPayroll.payroll_employee_salary._end_date + " >= '" + ToDate + "' or " + _g._dataPayroll.payroll_employee_salary._end_date + " = '1000-01-01')");
                                FieldSalarySlipData.Append(") * 4) * ((select " + _g._dataPayroll.payroll_company_config._customer_ss_rate + " from " + _g._dataPayroll.payroll_company_config._table + " ) / 100)");
                                FieldSalarySlipData.Append(" ELSE ");
                                FieldSalarySlipData.Append("((");
                                FieldSalarySlipData.Append("select coalesce((" + _g._dataPayroll.payroll_employee_salary._salary + "),0) as " + _g._dataPayroll.payroll_employee_salary._salary);
                                FieldSalarySlipData.Append(" from " + _g._dataPayroll.payroll_employee_salary._table);
                                FieldSalarySlipData.Append(" where " + _g._dataPayroll.payroll_employee_salary._code + " = " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + "");
                                FieldSalarySlipData.Append(" and " + _g._dataPayroll.payroll_employee_salary._start_date + " <= '" + FromDate + "'");
                                FieldSalarySlipData.Append(" and (" + _g._dataPayroll.payroll_employee_salary._end_date + " >= '" + ToDate + "' or " + _g._dataPayroll.payroll_employee_salary._end_date + " = '1000-01-01')");
                                FieldSalarySlipData.Append(") * (select sum(" + _g._dataPayroll.payroll_trans._working_day + ") from " + _g._dataPayroll.payroll_trans._table + " where " + _g._dataPayroll.payroll_trans._employee + "=" + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + " " + WhereTransSub + ")) * ((select " + _g._dataPayroll.payroll_company_config._customer_ss_rate + " from " + _g._dataPayroll.payroll_company_config._table + " ) / 100)");
                                FieldSalarySlipData.Append(" END");
                                FieldSalarySlipData.Append(" from " + _g._dataPayroll.payroll_employee._table + " where " + _g._dataPayroll.payroll_employee._code + " = " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + "),0) as society_insure_money_vary,");

                                FieldSalarySlipData.Append(" coalesce((select ");
                                FieldSalarySlipData.Append(" CASE " + _g._dataPayroll.payroll_employee._employee_type);
                                FieldSalarySlipData.Append(" WHEN 0 THEN (" + _g._dataPayroll.payroll_employee._salary + " * ((select " + _g._dataPayroll.payroll_company_config._customer_ss_rate + " from " + _g._dataPayroll.payroll_company_config._table + " ) / 100))");
                                FieldSalarySlipData.Append(" WHEN 1 THEN ((" + _g._dataPayroll.payroll_employee._salary + " * 4 ) * ((select " + _g._dataPayroll.payroll_company_config._customer_ss_rate + " from " + _g._dataPayroll.payroll_company_config._table + " ) / 100))");
                                FieldSalarySlipData.Append(" ELSE ((" + _g._dataPayroll.payroll_employee._salary + " * (select sum(" + _g._dataPayroll.payroll_trans._working_day + ") from " + _g._dataPayroll.payroll_trans._table + " where " + _g._dataPayroll.payroll_trans._employee + "=" + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + " " + WhereTransSub + ")) * ((select " + _g._dataPayroll.payroll_company_config._customer_ss_rate + " from " + _g._dataPayroll.payroll_company_config._table + " ) / 100))");
                                FieldSalarySlipData.Append(" END");
                                FieldSalarySlipData.Append(" from " + _g._dataPayroll.payroll_employee._table + " where " + _g._dataPayroll.payroll_employee._code + " = " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + "),0) as " + _g._dataPayroll.payroll_trans._society_insure_money + ",");
                                    //คิดค่าแรง    
                                FieldSalarySlipData.Append(" coalesce((select ");
                                FieldSalarySlipData.Append(" CASE " + _g._dataPayroll.payroll_employee._employee_type);
                                FieldSalarySlipData.Append(" WHEN 0 THEN ");
                                FieldSalarySlipData.Append("(select coalesce((" + _g._dataPayroll.payroll_employee_salary._salary + "),0) as " + _g._dataPayroll.payroll_employee_salary._salary);
                                FieldSalarySlipData.Append(" from " + _g._dataPayroll.payroll_employee_salary._table);
                                FieldSalarySlipData.Append(" where " + _g._dataPayroll.payroll_employee_salary._code + " = " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + "");
                                FieldSalarySlipData.Append(" and " + _g._dataPayroll.payroll_employee_salary._start_date + " <= '" + StartDay + "'");
                                FieldSalarySlipData.Append(" and (" + _g._dataPayroll.payroll_employee_salary._end_date + " >= '" + EndDay + "' or " + _g._dataPayroll.payroll_employee_salary._end_date + " = '1000-01-01'))");
                                FieldSalarySlipData.Append(" WHEN 1 THEN ");
                                FieldSalarySlipData.Append("((");
                                FieldSalarySlipData.Append("select coalesce((" + _g._dataPayroll.payroll_employee_salary._salary + "),0) as " + _g._dataPayroll.payroll_employee_salary._salary);
                                FieldSalarySlipData.Append(" from " + _g._dataPayroll.payroll_employee_salary._table);
                                FieldSalarySlipData.Append(" where " + _g._dataPayroll.payroll_employee_salary._code + " = " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + "");
                                FieldSalarySlipData.Append(" and " + _g._dataPayroll.payroll_employee_salary._start_date + " <= '" + StartDay + "'");
                                FieldSalarySlipData.Append(" and (" + _g._dataPayroll.payroll_employee_salary._end_date + " >= '" + EndDay + "' or " + _g._dataPayroll.payroll_employee_salary._end_date + " = '1000-01-01')");
                                FieldSalarySlipData.Append(") * 4)");
                                FieldSalarySlipData.Append(" ELSE ");
                                FieldSalarySlipData.Append("((");
                                FieldSalarySlipData.Append("select coalesce((" + _g._dataPayroll.payroll_employee_salary._salary + "),0) as " + _g._dataPayroll.payroll_employee_salary._salary);
                                FieldSalarySlipData.Append(" from " + _g._dataPayroll.payroll_employee_salary._table);
                                FieldSalarySlipData.Append(" where " + _g._dataPayroll.payroll_employee_salary._code + " = " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + "");
                                FieldSalarySlipData.Append(" and " + _g._dataPayroll.payroll_employee_salary._start_date + " <= '" + StartDay + "'");
                                FieldSalarySlipData.Append(" and (" + _g._dataPayroll.payroll_employee_salary._end_date + " >= '" + EndDay + "' or " + _g._dataPayroll.payroll_employee_salary._end_date + " = '1000-01-01')");
                                FieldSalarySlipData.Append(") * (select sum(" + _g._dataPayroll.payroll_trans._working_day + ") from " + _g._dataPayroll.payroll_trans._table + " where " + _g._dataPayroll.payroll_trans._employee + "=" + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + " " + WhereTransSubInMonth + "))");
                                FieldSalarySlipData.Append(" END");
                                FieldSalarySlipData.Append(" from " + _g._dataPayroll.payroll_employee._table + " where " + _g._dataPayroll.payroll_employee._code + " = " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + "),0) as wages_money_in_month_vary,");

                                FieldSalarySlipData.Append(" coalesce((select ");
                                FieldSalarySlipData.Append(" CASE " + _g._dataPayroll.payroll_employee._employee_type);
                                FieldSalarySlipData.Append(" WHEN 0 THEN " + _g._dataPayroll.payroll_employee._salary);
                                FieldSalarySlipData.Append(" WHEN 1 THEN (" + _g._dataPayroll.payroll_employee._salary + " * 4)");
                                FieldSalarySlipData.Append(" ELSE (" + _g._dataPayroll.payroll_employee._salary + "*(select sum(" + _g._dataPayroll.payroll_trans._working_day + ") from " + _g._dataPayroll.payroll_trans._table + " where " + _g._dataPayroll.payroll_trans._employee + "=" + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + " " + WhereTransSubInMonth + "))");
                                FieldSalarySlipData.Append(" END");
                                FieldSalarySlipData.Append(" from " + _g._dataPayroll.payroll_employee._table + " where " + _g._dataPayroll.payroll_employee._code + " = " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + "),0) as wages_money_in_month,");

                                       //คิดค่าแรง    
                                FieldSalarySlipData.Append(" coalesce((select ");
                                FieldSalarySlipData.Append(" CASE " + _g._dataPayroll.payroll_employee._employee_type);
                                FieldSalarySlipData.Append(" WHEN 0 THEN ");
                                FieldSalarySlipData.Append("(select coalesce((" + _g._dataPayroll.payroll_employee_salary._salary + "),0) as " + _g._dataPayroll.payroll_employee_salary._salary);
                                FieldSalarySlipData.Append(" from " + _g._dataPayroll.payroll_employee_salary._table);
                                FieldSalarySlipData.Append(" where " + _g._dataPayroll.payroll_employee_salary._code + " = " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + "");
                                FieldSalarySlipData.Append(" and " + _g._dataPayroll.payroll_employee_salary._start_date + " <= '" + FromDate + "'");
                                FieldSalarySlipData.Append(" and (" + _g._dataPayroll.payroll_employee_salary._end_date + " >= '" + ToDate + "' or " + _g._dataPayroll.payroll_employee_salary._end_date + " = '1000-01-01'))");
                                FieldSalarySlipData.Append(" WHEN 1 THEN ");
                                FieldSalarySlipData.Append("((");
                                FieldSalarySlipData.Append("select coalesce((" + _g._dataPayroll.payroll_employee_salary._salary + "),0) as " + _g._dataPayroll.payroll_employee_salary._salary);
                                FieldSalarySlipData.Append(" from " + _g._dataPayroll.payroll_employee_salary._table);
                                FieldSalarySlipData.Append(" where " + _g._dataPayroll.payroll_employee_salary._code + " = " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + "");
                                FieldSalarySlipData.Append(" and " + _g._dataPayroll.payroll_employee_salary._start_date + " <= '" + FromDate + "'");
                                FieldSalarySlipData.Append(" and (" + _g._dataPayroll.payroll_employee_salary._end_date + " >= '" + ToDate + "' or " + _g._dataPayroll.payroll_employee_salary._end_date + " = '1000-01-01')");
                                FieldSalarySlipData.Append(") * 4)");
                                FieldSalarySlipData.Append(" ELSE ");
                                FieldSalarySlipData.Append("((");
                                FieldSalarySlipData.Append("select coalesce((" + _g._dataPayroll.payroll_employee_salary._salary + "),0) as " + _g._dataPayroll.payroll_employee_salary._salary);
                                FieldSalarySlipData.Append(" from " + _g._dataPayroll.payroll_employee_salary._table);
                                FieldSalarySlipData.Append(" where " + _g._dataPayroll.payroll_employee_salary._code + " = " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + "");
                                FieldSalarySlipData.Append(" and " + _g._dataPayroll.payroll_employee_salary._start_date + " <= '" + FromDate + "'");
                                FieldSalarySlipData.Append(" and (" + _g._dataPayroll.payroll_employee_salary._end_date + " >= '" + ToDate + "' or " + _g._dataPayroll.payroll_employee_salary._end_date + " = '1000-01-01')");
                                FieldSalarySlipData.Append(") * (select sum(" + _g._dataPayroll.payroll_trans._working_day + ") from " + _g._dataPayroll.payroll_trans._table + " where " + _g._dataPayroll.payroll_trans._employee + "=" + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + " " + WhereTransSub + "))");
                                FieldSalarySlipData.Append(" END");
                                FieldSalarySlipData.Append(" from " + _g._dataPayroll.payroll_employee._table + " where " + _g._dataPayroll.payroll_employee._code + " = " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + "),0) as wages_money_vary,");

                                       //คิดค่าแรง    
                                FieldSalarySlipData.Append(" coalesce((select ");
                                FieldSalarySlipData.Append(" CASE " + _g._dataPayroll.payroll_employee._employee_type);
                                FieldSalarySlipData.Append(" WHEN 0 THEN " + _g._dataPayroll.payroll_employee._salary);
                                FieldSalarySlipData.Append(" WHEN 1 THEN (" + _g._dataPayroll.payroll_employee._salary + " * 4)");
                                FieldSalarySlipData.Append(" ELSE (" + _g._dataPayroll.payroll_employee._salary + "*(select sum(" + _g._dataPayroll.payroll_trans._working_day + ") from " + _g._dataPayroll.payroll_trans._table + " where " + _g._dataPayroll.payroll_trans._employee + "=" + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + " " + WhereTransSub + "))");
                                FieldSalarySlipData.Append(" END");
                                FieldSalarySlipData.Append(" from " + _g._dataPayroll.payroll_employee._table + " where " + _g._dataPayroll.payroll_employee._code + " = " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + "),0) as " + _g._dataPayroll.payroll_trans._wages_money + ",");

                                break;
                            case _g.g._screenPayrollEnum.รายงานภาษีเงินได้_รายเดือน:
                            case _g.g._screenPayrollEnum.ภงด_1:
                            case _g.g._screenPayrollEnum.ภงด_1_ใบแนบ:
                            case _g.g._screenPayrollEnum.คำนวนภาษีรายเดือน:
                                GroupBy = " group by employee ";
                                OrderBy = " order by employee ";
                                FieldSalarySlipHead.Append(" coalesce((");
                                FieldSalarySlipHead.Append(" CASE salary_status");
                                FieldSalarySlipHead.Append(" WHEN 0 THEN reserve_money ");
                                FieldSalarySlipHead.Append(" ELSE reserve_money_vary");
                                FieldSalarySlipHead.Append(" END");
                                FieldSalarySlipHead.Append(" ),0) as reserve_money,");

                                FieldSalarySlipHead.Append(" coalesce((");
                                FieldSalarySlipHead.Append(" CASE salary_status");
                                FieldSalarySlipHead.Append(" WHEN 0 THEN society_insure_money ");
                                FieldSalarySlipHead.Append(" ELSE society_insure_money_vary");
                                FieldSalarySlipHead.Append(" END");
                                FieldSalarySlipHead.Append(" ),0) as society_insure_money,");

                                FieldSalarySlipHead.Append(" coalesce((");
                                FieldSalarySlipHead.Append(" CASE salary_status");
                                FieldSalarySlipHead.Append(" WHEN 0 THEN wages_money ");
                                FieldSalarySlipHead.Append(" ELSE wages_money_vary");
                                FieldSalarySlipHead.Append(" END");
                                FieldSalarySlipHead.Append(" ),0) as wages_money,");



                                
                                    //จำนวนวันทำงาน
                                FieldSalarySlipData.Append("coalesce((sum(" + _g._dataPayroll.payroll_trans._working_day + ")),0) as working_day,");
                                    //คิดกองทุนสำรองฯ

                                FieldSalarySlipData.Append(" coalesce((select ");
                                FieldSalarySlipData.Append(" CASE " + _g._dataPayroll.payroll_employee._employee_type);
                                FieldSalarySlipData.Append(" WHEN 0 THEN ");
                                FieldSalarySlipData.Append("((select coalesce((" + _g._dataPayroll.payroll_employee_salary._salary + "),0) as " + _g._dataPayroll.payroll_employee_salary._salary);
                                FieldSalarySlipData.Append(" from " + _g._dataPayroll.payroll_employee_salary._table);
                                FieldSalarySlipData.Append(" where " + _g._dataPayroll.payroll_employee_salary._code + " = " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + "");
                                FieldSalarySlipData.Append(" and " + _g._dataPayroll.payroll_employee_salary._start_date + " <= '" + StartDay + "'");
                                FieldSalarySlipData.Append(" and (" + _g._dataPayroll.payroll_employee_salary._end_date + " >= '" + EndDay + "' or " + _g._dataPayroll.payroll_employee_salary._end_date + " = '1000-01-01'))  * ((select " + _g._dataPayroll.payroll_company_config._customer_rf_rate + " from " + _g._dataPayroll.payroll_company_config._table + " ) / 100))");
                                FieldSalarySlipData.Append(" WHEN 1 THEN ");
                                FieldSalarySlipData.Append("((");
                                FieldSalarySlipData.Append("select coalesce((" + _g._dataPayroll.payroll_employee_salary._salary + "),0) as " + _g._dataPayroll.payroll_employee_salary._salary);
                                FieldSalarySlipData.Append(" from " + _g._dataPayroll.payroll_employee_salary._table);
                                FieldSalarySlipData.Append(" where " + _g._dataPayroll.payroll_employee_salary._code + " = " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + "");
                                FieldSalarySlipData.Append(" and " + _g._dataPayroll.payroll_employee_salary._start_date + " <= '" + StartDay + "'");
                                FieldSalarySlipData.Append(" and (" + _g._dataPayroll.payroll_employee_salary._end_date + " >= '" + EndDay + "' or " + _g._dataPayroll.payroll_employee_salary._end_date + " = '1000-01-01')");
                                FieldSalarySlipData.Append(") * 4) * ((select " + _g._dataPayroll.payroll_company_config._customer_rf_rate + " from " + _g._dataPayroll.payroll_company_config._table + " ) / 100)");
                                FieldSalarySlipData.Append(" ELSE ");
                                FieldSalarySlipData.Append("((");
                                FieldSalarySlipData.Append("select coalesce((" + _g._dataPayroll.payroll_employee_salary._salary + "),0) as " + _g._dataPayroll.payroll_employee_salary._salary);
                                FieldSalarySlipData.Append(" from " + _g._dataPayroll.payroll_employee_salary._table);
                                FieldSalarySlipData.Append(" where " + _g._dataPayroll.payroll_employee_salary._code + " = " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + "");
                                FieldSalarySlipData.Append(" and " + _g._dataPayroll.payroll_employee_salary._start_date + " <= '" + StartDay + "'");
                                FieldSalarySlipData.Append(" and (" + _g._dataPayroll.payroll_employee_salary._end_date + " >= '" + EndDay + "' or " + _g._dataPayroll.payroll_employee_salary._end_date + " = '1000-01-01')");
                                FieldSalarySlipData.Append(") * (select sum(" + _g._dataPayroll.payroll_trans._working_day + ") from " + _g._dataPayroll.payroll_trans._table + " where " + _g._dataPayroll.payroll_trans._employee + "=" + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + " " + WhereTransSub + ")) * ((select " + _g._dataPayroll.payroll_company_config._customer_rf_rate + " from " + _g._dataPayroll.payroll_company_config._table + " ) / 100)");
                                FieldSalarySlipData.Append(" END");
                                FieldSalarySlipData.Append(" from " + _g._dataPayroll.payroll_employee._table + " where " + _g._dataPayroll.payroll_employee._code + " = " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + "),0) as reserve_money_vary,");

                                FieldSalarySlipData.Append(" coalesce((select ");
                                FieldSalarySlipData.Append(" CASE " + _g._dataPayroll.payroll_employee._employee_type);
                                FieldSalarySlipData.Append(" WHEN 0 THEN (" + _g._dataPayroll.payroll_employee._salary + " * ((select " + _g._dataPayroll.payroll_company_config._customer_rf_rate + " from " + _g._dataPayroll.payroll_company_config._table + " ) / 100))");
                                FieldSalarySlipData.Append(" WHEN 1 THEN ((" + _g._dataPayroll.payroll_employee._salary + " * 4) * ((select " + _g._dataPayroll.payroll_company_config._customer_rf_rate + " from " + _g._dataPayroll.payroll_company_config._table + " ) / 100))");
                                FieldSalarySlipData.Append(" ELSE ((" + _g._dataPayroll.payroll_employee._salary + " * (select sum(" + _g._dataPayroll.payroll_trans._working_day + ") from " + _g._dataPayroll.payroll_trans._table + " where " + _g._dataPayroll.payroll_trans._employee + "=" + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + " " + WhereTransSub + ")) * ((select " + _g._dataPayroll.payroll_company_config._customer_rf_rate + " from " + _g._dataPayroll.payroll_company_config._table + " ) / 100))");
                                FieldSalarySlipData.Append(" END");
                                FieldSalarySlipData.Append(" from " + _g._dataPayroll.payroll_employee._table + " where " + _g._dataPayroll.payroll_employee._code + " = " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + "),0) as reserve_money,");



                                    //คิดประกันสังคม

                                FieldSalarySlipData.Append(" coalesce((select ");
                                FieldSalarySlipData.Append(" CASE " + _g._dataPayroll.payroll_employee._employee_type);
                                FieldSalarySlipData.Append(" WHEN 0 THEN ");
                                FieldSalarySlipData.Append("((select coalesce((" + _g._dataPayroll.payroll_employee_salary._salary + "),0) as " + _g._dataPayroll.payroll_employee_salary._salary);
                                FieldSalarySlipData.Append(" from " + _g._dataPayroll.payroll_employee_salary._table);
                                FieldSalarySlipData.Append(" where " + _g._dataPayroll.payroll_employee_salary._code + " = " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + "");
                                FieldSalarySlipData.Append(" and " + _g._dataPayroll.payroll_employee_salary._start_date + " <= '" + StartDay + "'");
                                FieldSalarySlipData.Append(" and (" + _g._dataPayroll.payroll_employee_salary._end_date + " >= '" + EndDay + "' or " + _g._dataPayroll.payroll_employee_salary._end_date + " = '1000-01-01'))  * ((select " + _g._dataPayroll.payroll_company_config._customer_ss_rate + " from " + _g._dataPayroll.payroll_company_config._table + " ) / 100))");
                                FieldSalarySlipData.Append(" WHEN 1 THEN ");
                                FieldSalarySlipData.Append("((");
                                FieldSalarySlipData.Append("select coalesce((" + _g._dataPayroll.payroll_employee_salary._salary + "),0) as " + _g._dataPayroll.payroll_employee_salary._salary);
                                FieldSalarySlipData.Append(" from " + _g._dataPayroll.payroll_employee_salary._table);
                                FieldSalarySlipData.Append(" where " + _g._dataPayroll.payroll_employee_salary._code + " = " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + "");
                                FieldSalarySlipData.Append(" and " + _g._dataPayroll.payroll_employee_salary._start_date + " <= '" + StartDay + "'");
                                FieldSalarySlipData.Append(" and (" + _g._dataPayroll.payroll_employee_salary._end_date + " >= '" + EndDay + "' or " + _g._dataPayroll.payroll_employee_salary._end_date + " = '1000-01-01')");
                                FieldSalarySlipData.Append(") * 4) * ((select " + _g._dataPayroll.payroll_company_config._customer_ss_rate + " from " + _g._dataPayroll.payroll_company_config._table + " ) / 100)");
                                FieldSalarySlipData.Append(" ELSE ");
                                FieldSalarySlipData.Append("((");
                                FieldSalarySlipData.Append("select coalesce((" + _g._dataPayroll.payroll_employee_salary._salary + "),0) as " + _g._dataPayroll.payroll_employee_salary._salary);
                                FieldSalarySlipData.Append(" from " + _g._dataPayroll.payroll_employee_salary._table);
                                FieldSalarySlipData.Append(" where " + _g._dataPayroll.payroll_employee_salary._code + " = " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + "");
                                FieldSalarySlipData.Append(" and " + _g._dataPayroll.payroll_employee_salary._start_date + " <= '" + StartDay + "'");
                                FieldSalarySlipData.Append(" and (" + _g._dataPayroll.payroll_employee_salary._end_date + " >= '" + EndDay + "' or " + _g._dataPayroll.payroll_employee_salary._end_date + " = '1000-01-01')");
                                FieldSalarySlipData.Append(") * (select sum(" + _g._dataPayroll.payroll_trans._working_day + ") from " + _g._dataPayroll.payroll_trans._table + " where " + _g._dataPayroll.payroll_trans._employee + "=" + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + " " + WhereTransSub + ")) * ((select " + _g._dataPayroll.payroll_company_config._customer_ss_rate + " from " + _g._dataPayroll.payroll_company_config._table + " ) / 100)");
                                FieldSalarySlipData.Append(" END");
                                FieldSalarySlipData.Append(" from " + _g._dataPayroll.payroll_employee._table + " where " + _g._dataPayroll.payroll_employee._code + " = " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + "),0) as society_insure_money_vary,");

                                FieldSalarySlipData.Append(" coalesce((select ");
                                FieldSalarySlipData.Append(" CASE " + _g._dataPayroll.payroll_employee._employee_type);
                                FieldSalarySlipData.Append(" WHEN 0 THEN (" + _g._dataPayroll.payroll_employee._salary + " * ((select " + _g._dataPayroll.payroll_company_config._customer_ss_rate + " from " + _g._dataPayroll.payroll_company_config._table + " ) / 100))");
                                FieldSalarySlipData.Append(" WHEN 1 THEN ((" + _g._dataPayroll.payroll_employee._salary + " * 4 ) * ((select " + _g._dataPayroll.payroll_company_config._customer_ss_rate + " from " + _g._dataPayroll.payroll_company_config._table + " ) / 100))");
                                FieldSalarySlipData.Append(" ELSE ((" + _g._dataPayroll.payroll_employee._salary + " * (select sum(" + _g._dataPayroll.payroll_trans._working_day + ") from " + _g._dataPayroll.payroll_trans._table + " where " + _g._dataPayroll.payroll_trans._employee + "=" + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + " " + WhereTransSub + ")) * ((select " + _g._dataPayroll.payroll_company_config._customer_ss_rate + " from " + _g._dataPayroll.payroll_company_config._table + " ) / 100))");
                                FieldSalarySlipData.Append(" END");
                                FieldSalarySlipData.Append(" from " + _g._dataPayroll.payroll_employee._table + " where " + _g._dataPayroll.payroll_employee._code + " = " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + "),0) as society_insure_money,");


                                    //คิดค่าแรง    
                                FieldSalarySlipData.Append(" coalesce((select ");
                                FieldSalarySlipData.Append(" CASE " + _g._dataPayroll.payroll_employee._employee_type);
                                FieldSalarySlipData.Append(" WHEN 0 THEN ");
                                FieldSalarySlipData.Append("(select coalesce((" + _g._dataPayroll.payroll_employee_salary._salary + "),0) as " + _g._dataPayroll.payroll_employee_salary._salary);
                                FieldSalarySlipData.Append(" from " + _g._dataPayroll.payroll_employee_salary._table);
                                FieldSalarySlipData.Append(" where " + _g._dataPayroll.payroll_employee_salary._code + " = " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + "");
                                FieldSalarySlipData.Append(" and " + _g._dataPayroll.payroll_employee_salary._start_date + " <= '" + StartDay + "'");
                                FieldSalarySlipData.Append(" and (" + _g._dataPayroll.payroll_employee_salary._end_date + " >= '" + EndDay + "' or " + _g._dataPayroll.payroll_employee_salary._end_date + " = '1000-01-01'))");
                                FieldSalarySlipData.Append(" WHEN 1 THEN ");
                                FieldSalarySlipData.Append("((");
                                FieldSalarySlipData.Append("select coalesce((" + _g._dataPayroll.payroll_employee_salary._salary + "),0) as " + _g._dataPayroll.payroll_employee_salary._salary);
                                FieldSalarySlipData.Append(" from " + _g._dataPayroll.payroll_employee_salary._table);
                                FieldSalarySlipData.Append(" where " + _g._dataPayroll.payroll_employee_salary._code + " = " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + "");
                                FieldSalarySlipData.Append(" and " + _g._dataPayroll.payroll_employee_salary._start_date + " <= '" + StartDay + "'");
                                FieldSalarySlipData.Append(" and (" + _g._dataPayroll.payroll_employee_salary._end_date + " >= '" + EndDay + "' or " + _g._dataPayroll.payroll_employee_salary._end_date + " = '1000-01-01')");
                                FieldSalarySlipData.Append(") * 4)");
                                FieldSalarySlipData.Append(" ELSE ");
                                FieldSalarySlipData.Append("((");
                                FieldSalarySlipData.Append("select coalesce((" + _g._dataPayroll.payroll_employee_salary._salary + "),0) as " + _g._dataPayroll.payroll_employee_salary._salary);
                                FieldSalarySlipData.Append(" from " + _g._dataPayroll.payroll_employee_salary._table);
                                FieldSalarySlipData.Append(" where " + _g._dataPayroll.payroll_employee_salary._code + " = " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + "");
                                FieldSalarySlipData.Append(" and " + _g._dataPayroll.payroll_employee_salary._start_date + " <= '" + StartDay + "'");
                                FieldSalarySlipData.Append(" and (" + _g._dataPayroll.payroll_employee_salary._end_date + " >= '" + EndDay + "' or " + _g._dataPayroll.payroll_employee_salary._end_date + " = '1000-01-01')");
                                FieldSalarySlipData.Append(") * (select sum(" + _g._dataPayroll.payroll_trans._working_day + ") from " + _g._dataPayroll.payroll_trans._table + " where " + _g._dataPayroll.payroll_trans._employee + "=" + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + " " + WhereTransSub + "))");
                                FieldSalarySlipData.Append(" END");
                                FieldSalarySlipData.Append(" from " + _g._dataPayroll.payroll_employee._table + " where " + _g._dataPayroll.payroll_employee._code + " = " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + "),0) as wages_money_vary,");

                                FieldSalarySlipData.Append(" coalesce((select ");
                                FieldSalarySlipData.Append(" CASE " + _g._dataPayroll.payroll_employee._employee_type);
                                FieldSalarySlipData.Append(" WHEN 0 THEN " + _g._dataPayroll.payroll_employee._salary);
                                FieldSalarySlipData.Append(" WHEN 1 THEN (" + _g._dataPayroll.payroll_employee._salary + " * 4)");
                                FieldSalarySlipData.Append(" ELSE (" + _g._dataPayroll.payroll_employee._salary + "*(select sum(" + _g._dataPayroll.payroll_trans._working_day + ") from " + _g._dataPayroll.payroll_trans._table + " where " + _g._dataPayroll.payroll_trans._employee + "=" + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + " " + WhereTransSub + "))");
                                FieldSalarySlipData.Append(" END");
                                FieldSalarySlipData.Append(" from " + _g._dataPayroll.payroll_employee._table + " where " + _g._dataPayroll.payroll_employee._code + " = " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + "),0) as wages_money,");



                                break;
                        }
                        break;
                }
                switch (ScreenName)
                {
                    case _g.g._screenPayrollEnum.รายงานภาษีเงินได้_รายปี:
                    case _g.g._screenPayrollEnum.คำนวนภาษีรายปี:
                    case _g.g._screenPayrollEnum.ภงด_91:
                    case _g.g._screenPayrollEnum.ภงด_1_ก_ใบแนบ:
                    case _g.g._screenPayrollEnum.ภงด_1_ก:
                        query.Append("select ");
                            //เงินเดือนและภาษีแบบปกติ
                        query.Append(" sum(" + _g._dataPayroll.payroll_trans._sow_money + ") as " + _g._dataPayroll.payroll_resource._sow_money + ",");
                        query.Append(" sum(" + _g._dataPayroll.payroll_trans._leave_money + ") as " + _g._dataPayroll.payroll_resource._leave_money + ",");
                        query.Append(" sum(" + _g._dataPayroll.payroll_trans._late_money + ") as " + _g._dataPayroll.payroll_resource._late_money + ",");
                        query.Append(" sum(" + _g._dataPayroll.payroll_trans._ot_money + ") as " + _g._dataPayroll.payroll_resource._ot_money + ",");
                        query.Append(" sum(" + _g._dataPayroll.payroll_trans._diligent_money + ") as " + _g._dataPayroll.payroll_resource._diligent_money + ",");
                        query.Append(" sum(" + _g._dataPayroll.payroll_trans._prepay_money + ") as " + _g._dataPayroll.payroll_resource._prepay_money + ",");
                        query.Append(" sum(" + _g._dataPayroll.payroll_trans._bonus_money + ") as " + _g._dataPayroll.payroll_resource._bonus_money + ",");
                        query.Append(" sum(" + _g._dataPayroll.payroll_trans._other_income_money + ") as " + _g._dataPayroll.payroll_resource._other_income_money + ",");
                        query.Append(" sum(" + _g._dataPayroll.payroll_trans._advance_money + ") as " + _g._dataPayroll.payroll_resource._advance_money + ",");
                        query.Append(" sum(" + _g._dataPayroll.payroll_trans._insure_money + ") as " + _g._dataPayroll.payroll_resource._insure_money + ",");
                        query.Append(" sum(" + _g._dataPayroll.payroll_trans._other_payout_money + ") as " + _g._dataPayroll.payroll_resource._other_payout_money + ",");
                        query.Append("sum(reserve_money) as " + _g._dataPayroll.payroll_resource._reserve_money + ",");
                        query.Append("sum(society_insure_money) as " + _g._dataPayroll.payroll_resource._society_insure_money + ",");
                        query.Append("sum(wages_money) as " + _g._dataPayroll.payroll_resource._wages_money + ",");

                            //เช็คสถานะกองทุน
                        query.Append("coalesce((select " + _g._dataPayroll.payroll_employee._reserve_fund_status + " from ");
                        query.Append(_g._dataPayroll.payroll_employee._table);
                        query.Append(" where " + _g._dataPayroll.payroll_employee._code);
                        query.Append(" = employee),0)  as reserve_fund_status,");

                        query.Append("(select " + _g._dataPayroll.payroll_employee._reserve_fund_start_date + " from ");
                        query.Append(_g._dataPayroll.payroll_employee._table);
                        query.Append(" where " + _g._dataPayroll.payroll_employee._code);
                        query.Append(" = employee) as reserve_fund_start_date,");

                        query.Append("(select " + _g._dataPayroll.payroll_employee._reserve_fund_end_date + " from ");
                        query.Append(_g._dataPayroll.payroll_employee._table);
                        query.Append(" where " + _g._dataPayroll.payroll_employee._code);
                        query.Append(" = employee) as reserve_fund_end_date,");
                            //เช็คลดหย่อนฯ ประกันสังคม กองทุนสำรองฯ
                        query.Append("coalesce((select " + _g._dataPayroll.payroll_company_config._reduce_social_security + " from " + _g._dataPayroll.payroll_company_config._table + " ),0) as reduce_social_security,");
                        query.Append("coalesce((select " + _g._dataPayroll.payroll_company_config._reduce_reserve_fund + " from " + _g._dataPayroll.payroll_company_config._table + " ),0) as reduce_reserve_fund,");
                            //กองทุนสำรองเลี้ยงชีพ 
                        query.Append("coalesce((select  ceiling((coalesce((" + _g._dataPayroll.payroll_company_config._social_security_max + "),0) * coalesce((" + _g._dataPayroll.payroll_company_config._customer_rf_rate + "),0))/100)  from " + _g._dataPayroll.payroll_company_config._table + " ),0) as reserve_fund_max,");
                        query.Append("coalesce((select  ceiling((coalesce((" + _g._dataPayroll.payroll_company_config._social_security_min + "),0) * coalesce((" + _g._dataPayroll.payroll_company_config._customer_rf_rate + "),0))/100)  from " + _g._dataPayroll.payroll_company_config._table + " ),0) as reserve_fund_min,");
                        query.Append("coalesce((select " + _g._dataPayroll.payroll_company_config._customer_rf_rate + " from " + _g._dataPayroll.payroll_company_config._table + " ),0) as customer_rf_rate,");
                        query.Append("coalesce((select " + _g._dataPayroll.payroll_company_config._employer_rf_rate + " from " + _g._dataPayroll.payroll_company_config._table + " ),0) as employer_rf_rate,");
                            //ประกันสังคม
                        query.Append("coalesce((select  ceiling((coalesce((" + _g._dataPayroll.payroll_company_config._social_security_max + "),0) * coalesce((" + _g._dataPayroll.payroll_company_config._customer_ss_rate + "),0))/100)  from " + _g._dataPayroll.payroll_company_config._table + " ),0) as social_security_max,");
                        query.Append("coalesce((select  ceiling((coalesce((" + _g._dataPayroll.payroll_company_config._social_security_min + "),0) * coalesce((" + _g._dataPayroll.payroll_company_config._customer_ss_rate + "),0))/100)  from " + _g._dataPayroll.payroll_company_config._table + " ),0) as social_security_min,");
                        query.Append("coalesce((select " + _g._dataPayroll.payroll_company_config._customer_ss_rate + " from " + _g._dataPayroll.payroll_company_config._table + " ),0) as customer_ss_rate,");
                        query.Append("coalesce((select " + _g._dataPayroll.payroll_company_config._employer_ss_rate + " from " + _g._dataPayroll.payroll_company_config._table + " ),0) as employer_ss_rate,");
                            //ภาษี                                
                        query.Append("(0) as tax_income,");
                        query.Append("(0) as reduce_40,");
                        query.Append("(0) as reduce_child,");
                        query.Append("(0) as remark,");

                        query.Append("coalesce((select ");
                        query.Append(_g._dataPayroll.payroll_employee_detail._reduce_myself);
                        query.Append(" from " + _g._dataPayroll.payroll_employee_detail._table);
                        query.Append(" where " + _g._dataPayroll.payroll_employee_detail._code);
                        query.Append(" = employee),0) as reduce_myself ,");

                        query.Append("coalesce((select ");
                        query.Append(_g._dataPayroll.payroll_employee_detail._reduce_spouse);
                        query.Append(" from " + _g._dataPayroll.payroll_employee_detail._table);
                        query.Append(" where " + _g._dataPayroll.payroll_employee_detail._code);
                        query.Append(" = employee),0) as reduce_spouse ,");

                        query.Append("(select ");
                        query.Append(_g._dataPayroll.payroll_employee_detail._situation);
                        query.Append(" from " + _g._dataPayroll.payroll_employee_detail._table);
                        query.Append(" where " + _g._dataPayroll.payroll_employee_detail._code);
                        query.Append(" = employee) as situation ,");

                        query.Append("(select " + _g._dataPayroll.payroll_employee_detail._spouse_work_status + " from ");
                        query.Append(_g._dataPayroll.payroll_employee_detail._table);
                        query.Append(" where " + _g._dataPayroll.payroll_employee_detail._code);
                        query.Append(" = employee) as spouse_work_status ,");

                        query.Append("(select " + _g._dataPayroll.payroll_employee_detail._spouse_tax + " from ");
                        query.Append(_g._dataPayroll.payroll_employee_detail._table);
                        query.Append(" where " + _g._dataPayroll.payroll_employee_detail._code);
                        query.Append(" = employee) as spouse_tax ,");

                        query.Append("coalesce((select " + _g._dataPayroll.payroll_employee_detail._reduce_child_study + " from ");
                        query.Append(_g._dataPayroll.payroll_employee_detail._table);
                        query.Append(" where " + _g._dataPayroll.payroll_employee_detail._code);
                        query.Append(" = employee),0) as reduce_child_study ,");

                        query.Append("coalesce((select " + _g._dataPayroll.payroll_employee_detail._reduce_child_no_study + " from ");
                        query.Append(_g._dataPayroll.payroll_employee_detail._table);
                        query.Append(" where " + _g._dataPayroll.payroll_employee_detail._code);
                        query.Append(" = employee),0) as reduce_child_no_study, ");

                        query.Append("coalesce((select " + _g._dataPayroll.payroll_employee_detail._reduce_parents + " from ");
                        query.Append(_g._dataPayroll.payroll_employee_detail._table);
                        query.Append(" where " + _g._dataPayroll.payroll_employee_detail._code);
                        query.Append(" = employee),0) as reduce_parents ,");

                        query.Append("coalesce((select " + _g._dataPayroll.payroll_employee_detail._insurance_parents + " from ");
                        query.Append(_g._dataPayroll.payroll_employee_detail._table);
                        query.Append(" where " + _g._dataPayroll.payroll_employee_detail._code);
                        query.Append(" = employee),0) as insurance_parents ,");

                        query.Append("coalesce((select " + _g._dataPayroll.payroll_employee_detail._insurance + " from ");
                        query.Append(_g._dataPayroll.payroll_employee_detail._table);
                        query.Append(" where " + _g._dataPayroll.payroll_employee_detail._code);
                        query.Append(" = employee),0) as insurance ,");

                        query.Append("coalesce((select " + _g._dataPayroll.payroll_employee_detail._loan + " from ");
                        query.Append(_g._dataPayroll.payroll_employee_detail._table);
                        query.Append(" where " + _g._dataPayroll.payroll_employee_detail._code);
                        query.Append(" = employee),0) as loan, ");

                        query.Append("coalesce((select " + _g._dataPayroll.payroll_employee_detail._donation + " from ");
                        query.Append(_g._dataPayroll.payroll_employee_detail._table);
                        query.Append(" where " + _g._dataPayroll.payroll_employee_detail._code);
                        query.Append(" = employee),0) as donation, ");

                        query.Append("coalesce((select " + _g._dataPayroll.payroll_employee_detail._donation_education + " from ");
                        query.Append(_g._dataPayroll.payroll_employee_detail._table);
                        query.Append(" where " + _g._dataPayroll.payroll_employee_detail._code);
                        query.Append(" = employee),0) as donation_education, ");

                        query.Append("coalesce((select (");
                        query.Append(_g._dataPayroll.payroll_employee_detail._buy_funds_reserves + "+");
                        query.Append(_g._dataPayroll.payroll_employee_detail._buy_share + "+");
                        query.Append(_g._dataPayroll.payroll_employee_detail._except_funds_reserves + "+");
                        query.Append(_g._dataPayroll.payroll_employee_detail._except_gbk + "+");
                        query.Append(_g._dataPayroll.payroll_employee_detail._except_teacher + "+");
                        query.Append(_g._dataPayroll.payroll_employee_detail._except_65year + "+");
                        query.Append(_g._dataPayroll.payroll_employee_detail._except_spouse_65year + "+");
                        query.Append(_g._dataPayroll.payroll_employee_detail._except_labour_legislation);
                        query.Append(") from ");
                        query.Append(_g._dataPayroll.payroll_employee_detail._table);
                        query.Append(" where " + _g._dataPayroll.payroll_employee_detail._code);
                        query.Append(" = employee),0) as reduce_other,");
                        query.Append(FieldEmployeeData.ToString());
                        query.Append("(0) as " + _g._dataPayroll.payroll_resource._net_wages + ",");
                        query.Append(" " + _g._dataPayroll.payroll_trans._employee + " as " + _g._dataPayroll.payroll_resource._employee + ",");
                        query.Append(" (select " + _g._dataPayroll.payroll_employee._employee_type + " from " + _g._dataPayroll.payroll_employee._table + " where " + _g._dataPayroll.payroll_employee._code + " = employee)  as " + _g._dataPayroll.payroll_resource._employee_type + ",");
                        query.Append(" (select " + _g._dataPayroll.payroll_employee._name + " from " + _g._dataPayroll.payroll_employee._table + " where " + _g._dataPayroll.payroll_employee._code + " = employee)  as " + _g._dataPayroll.payroll_resource._employee_name);
                        query.Append(" from ");
                        query.Append("(select ");
                            //เงินเดือนและภาษีแบบปกติ
                        query.Append(FileFormonth.ToString());
                        query.Append(" " + _g._dataPayroll.payroll_trans._employee + " as " + _g._dataPayroll.payroll_resource._employee);
                        query.Append(" from ");
                        query.Append("(select ");
                            //เงินสะสม
                        query.Append(DataFormonth.ToString());
                            //เช็คสถานะเงินเดือน
                        query.Append("coalesce((select " + _g._dataPayroll.payroll_employee._salary_status + " from ");
                        query.Append(_g._dataPayroll.payroll_employee._table);
                        query.Append(" where " + _g._dataPayroll.payroll_employee._code);
                        query.Append(" = " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee);
                        query.Append("),0) as " + _g._dataPayroll.payroll_employee._salary_status + ",");
                            //คิดระยะกองทุนสำรองฯ                              
                        query.Append("coalesce((select " + _g._dataPayroll.payroll_company_config._customer_rf_rate + " from " + _g._dataPayroll.payroll_company_config._table + " ),0) as customer_rf_rate,");
                            //คิดระยะประกันสังคม                               
                        query.Append("coalesce((select " + _g._dataPayroll.payroll_company_config._customer_ss_rate + " from " + _g._dataPayroll.payroll_company_config._table + " ),0) as customer_ss_rate,");

                        query.Append(_g._dataPayroll.payroll_trans._employee + ",");
                        query.Append(" (select " + _g._dataPayroll.payroll_employee._employee_type + " from " + _g._dataPayroll.payroll_employee._table + " where " + _g._dataPayroll.payroll_employee._code + " = " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + ")  as " + _g._dataPayroll.payroll_resource._employee_type);
                        query.Append(" from " + _g._dataPayroll.payroll_trans._table);
                        query.Append(" " + WhereTrans + " " + GroupBy + " " + OrderBy);
                        query.Append(") as temp1 ) as temp2  group by employee order by employee");
                        //,sow_money,leave_money,late_money,ot_money,diligent_money,prepay_money,bonus_money,other_income_money,advance_money,insure_money,other_payout_money,reserve_money,society_insure_money,wages_money

                        break;
                    case _g.g._screenPayrollEnum.รายละเอียดเงินเดือน_ค่าจ้างทั้งปี:
                    case _g.g._screenPayrollEnum.รายงานภาษีเงินได้_รายเดือน:
                    case _g.g._screenPayrollEnum.ภงด_1:
                    case _g.g._screenPayrollEnum.ภงด_1_ใบแนบ:
                    case _g.g._screenPayrollEnum.คำนวนภาษีรายเดือน:
                    case _g.g._screenPayrollEnum.คำนวนเงินเดือน:
                    case _g.g._screenPayrollEnum.รายละเอียดเงินเดือน_ค่าจ้าง_ตามประเภทลูกจ้าง:
                    case _g.g._screenPayrollEnum.สลิปเงินเดือน:

                        //Query รายละเอียด และเงินได้

                        query.Append("select ");
                            //เงินเดือนและภาษีแบบปกติ
                        query.Append(FileFormonth.ToString());
                            //สลิป
                        query.Append(FieldSalarySlipHead.ToString());
                            //เช็คสถานะกองทุน
                        query.Append("reserve_fund_status as reserve_fund_status,");
                        query.Append("reserve_fund_start_date as reserve_fund_start_date,");
                        query.Append("reserve_fund_end_date as reserve_fund_end_date,");
                        query.Append("working_day as working_day,");
                        query.Append("salary_status as salary_status,");
                            //เช็คลดหย่อนฯ ประกันสังคม กองทุนสำรองฯ
                        query.Append("reduce_social_security as reduce_social_security,");
                        query.Append("reduce_reserve_fund as reduce_reserve_fund,");
                            //กองทุนสำรองเลี้ยงชีพ 
                        query.Append("reserve_fund_max as reserve_fund_max,");
                        query.Append("reserve_fund_min as reserve_fund_min,");
                        query.Append("customer_rf_rate as customer_rf_rate,");
                        query.Append("employer_rf_rate as employer_rf_rate,");
                            //ประกันสังคม
                        query.Append("social_security_max as social_security_max,");
                        query.Append("social_security_min as social_security_min,");
                        query.Append("customer_ss_rate as customer_ss_rate,");
                        query.Append("employer_ss_rate as employer_ss_rate,");
                            //ภาษี                   
                        query.Append("reduce_myself as reduce_myself,");
                        query.Append("reduce_spouse as reduce_spouse,");
                        query.Append("situation as situation,");
                        query.Append("spouse_work_status as spouse_work_status,");
                        query.Append("spouse_tax as spouse_tax,");
                        query.Append("reduce_child_study as reduce_child_study,");
                        query.Append("reduce_child_no_study as reduce_child_no_study,");
                        query.Append("reduce_parents as reduce_parents,");
                        query.Append("insurance_parents as insurance_parents,");
                        query.Append("donation as donation,");
                        query.Append("donation_education as donation_education,");
                        query.Append("insurance as insurance,");
                        query.Append("loan as loan,");
                        query.Append("buy_funds_reserves as buy_funds_reserves,");
                        query.Append("buy_share as buy_share,");
                        query.Append("except_funds_reserves as except_funds_reserves,");
                        query.Append("except_gbk as except_gbk,");
                        query.Append("except_teacher as except_teacher,");
                        query.Append("except_65year as except_65year,");
                        query.Append("except_spouse_65year as except_spouse_65year,");
                        query.Append("except_labour_legislation as except_labour_legislation,");
                        query.Append("reduce_other as reduce_other,");
                        query.Append("(0) as tax_income,");
                        query.Append("(0) as reduce_40,");
                        query.Append("(0) as reduce_child,");
                        query.Append("(0) as remark,");
                            //ทุุกจอ
                        query.Append(FieldEmployeeHead.ToString());

                        query.Append(" " + _g._dataPayroll.payroll_trans._employee + " as " + _g._dataPayroll.payroll_resource._employee + ",");
                        query.Append(" " + _g._dataPayroll.payroll_trans._employee_type + " as " + _g._dataPayroll.payroll_resource._employee_type + ",");
                        query.Append(" " + _g._dataPayroll.payroll_trans._employee_name + " as " + _g._dataPayroll.payroll_resource._employee_name);
                        query.Append(" from ");
                        query.Append("(select ");
                            //เงินสะสม
                        query.Append(DataFormonth.ToString());
                            //สลิปเงินเดือน
                        query.Append(FieldSalarySlipData.ToString());
                            //เช็คสถานะกองทุน
                        query.Append("coalesce((select " + _g._dataPayroll.payroll_employee._reserve_fund_status + " from ");
                        query.Append(_g._dataPayroll.payroll_employee._table);
                        query.Append(" where " + _g._dataPayroll.payroll_employee._code);
                        query.Append(" = " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + "),0)  as reserve_fund_status,");

                        query.Append("(select " + _g._dataPayroll.payroll_employee._reserve_fund_start_date + " from ");
                        query.Append(_g._dataPayroll.payroll_employee._table);
                        query.Append(" where " + _g._dataPayroll.payroll_employee._code);
                        query.Append(" = " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + ") as reserve_fund_start_date,");

                        query.Append("(select " + _g._dataPayroll.payroll_employee._reserve_fund_end_date + " from ");
                        query.Append(_g._dataPayroll.payroll_employee._table);
                        query.Append(" where " + _g._dataPayroll.payroll_employee._code);
                        query.Append(" = " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + ") as reserve_fund_end_date,");
                            //เช็คสถานะเงินเดือน
                        query.Append("coalesce((select " + _g._dataPayroll.payroll_employee._salary_status + " from ");
                        query.Append(_g._dataPayroll.payroll_employee._table);
                        query.Append(" where " + _g._dataPayroll.payroll_employee._code);
                        query.Append(" = " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee);
                        query.Append("),0) as " + _g._dataPayroll.payroll_employee._salary_status + ",");
                            //เช็คลดหย่อนฯ ประกันสังคม กองทุนสำรองฯ
                        query.Append("coalesce((select " + _g._dataPayroll.payroll_company_config._reduce_social_security + " from " + _g._dataPayroll.payroll_company_config._table + " ),0) as reduce_social_security,");
                        query.Append("coalesce((select " + _g._dataPayroll.payroll_company_config._reduce_reserve_fund + " from " + _g._dataPayroll.payroll_company_config._table + " ),0) as reduce_reserve_fund,");
                            //คิดระยะกองทุนสำรองฯ
                        query.Append("coalesce((select  ceiling((coalesce((" + _g._dataPayroll.payroll_company_config._social_security_max + "),0) * coalesce((" + _g._dataPayroll.payroll_company_config._customer_rf_rate + "),0))/100)  from " + _g._dataPayroll.payroll_company_config._table + " ),0) as reserve_fund_max,");
                        query.Append("coalesce((select  ceiling((coalesce((" + _g._dataPayroll.payroll_company_config._social_security_min + "),0) * coalesce((" + _g._dataPayroll.payroll_company_config._customer_rf_rate + "),0))/100)  from " + _g._dataPayroll.payroll_company_config._table + " ),0) as reserve_fund_min,");
                        query.Append("coalesce((select " + _g._dataPayroll.payroll_company_config._customer_rf_rate + " from " + _g._dataPayroll.payroll_company_config._table + " ),0) as customer_rf_rate,");
                        query.Append("coalesce((select " + _g._dataPayroll.payroll_company_config._employer_rf_rate + " from " + _g._dataPayroll.payroll_company_config._table + " ),0) as employer_rf_rate,");
                            //คิดระยะประกันสังคม
                        query.Append("coalesce((select  ceiling((coalesce((" + _g._dataPayroll.payroll_company_config._social_security_max + "),0) * coalesce((" + _g._dataPayroll.payroll_company_config._customer_ss_rate + "),0))/100)  from " + _g._dataPayroll.payroll_company_config._table + " ),0) as social_security_max,");
                        query.Append("coalesce((select  ceiling((coalesce((" + _g._dataPayroll.payroll_company_config._social_security_min + "),0) * coalesce((" + _g._dataPayroll.payroll_company_config._customer_ss_rate + "),0))/100)  from " + _g._dataPayroll.payroll_company_config._table + " ),0) as social_security_min,");
                        query.Append("coalesce((select " + _g._dataPayroll.payroll_company_config._customer_ss_rate + " from " + _g._dataPayroll.payroll_company_config._table + " ),0) as customer_ss_rate,");
                        query.Append("coalesce((select " + _g._dataPayroll.payroll_company_config._employer_ss_rate + " from " + _g._dataPayroll.payroll_company_config._table + " ),0) as employer_ss_rate,");
                            //คำนวนภาษี

                        query.Append("coalesce((select ");
                        query.Append(_g._dataPayroll.payroll_employee_detail._reduce_myself);
                        query.Append(" from " + _g._dataPayroll.payroll_employee_detail._table);
                        query.Append(" where " + _g._dataPayroll.payroll_employee_detail._code);
                        query.Append(" = " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + "),0) as reduce_myself ,");

                        query.Append("coalesce((select ");
                        query.Append(_g._dataPayroll.payroll_employee_detail._reduce_spouse);
                        query.Append(" from " + _g._dataPayroll.payroll_employee_detail._table);
                        query.Append(" where " + _g._dataPayroll.payroll_employee_detail._code);
                        query.Append(" = " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + "),0) as reduce_spouse ,");

                        query.Append("(select ");
                        query.Append(_g._dataPayroll.payroll_employee_detail._situation);
                        query.Append(" from " + _g._dataPayroll.payroll_employee_detail._table);
                        query.Append(" where " + _g._dataPayroll.payroll_employee_detail._code);
                        query.Append(" = " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + ") as situation ,");

                        query.Append("(select " + _g._dataPayroll.payroll_employee_detail._spouse_work_status + " from ");
                        query.Append(_g._dataPayroll.payroll_employee_detail._table);
                        query.Append(" where " + _g._dataPayroll.payroll_employee_detail._code);
                        query.Append(" = " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + ") as spouse_work_status ,");

                        query.Append("(select " + _g._dataPayroll.payroll_employee_detail._spouse_tax + " from ");
                        query.Append(_g._dataPayroll.payroll_employee_detail._table);
                        query.Append(" where " + _g._dataPayroll.payroll_employee_detail._code);
                        query.Append(" = " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + ") as spouse_tax ,");

                        query.Append("coalesce((select " + _g._dataPayroll.payroll_employee_detail._reduce_child_study + " from ");
                        query.Append(_g._dataPayroll.payroll_employee_detail._table);
                        query.Append(" where " + _g._dataPayroll.payroll_employee_detail._code);
                        query.Append(" = " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + "),0) as reduce_child_study ,");

                        query.Append("coalesce((select " + _g._dataPayroll.payroll_employee_detail._reduce_child_no_study + " from ");
                        query.Append(_g._dataPayroll.payroll_employee_detail._table);
                        query.Append(" where " + _g._dataPayroll.payroll_employee_detail._code);
                        query.Append(" = " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + "),0) as reduce_child_no_study, ");

                        query.Append("coalesce((select " + _g._dataPayroll.payroll_employee_detail._reduce_parents + " from ");
                        query.Append(_g._dataPayroll.payroll_employee_detail._table);
                        query.Append(" where " + _g._dataPayroll.payroll_employee_detail._code);
                        query.Append(" = " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + "),0) as reduce_parents ,");

                        query.Append("coalesce((select " + _g._dataPayroll.payroll_employee_detail._insurance_parents + " from ");
                        query.Append(_g._dataPayroll.payroll_employee_detail._table);
                        query.Append(" where " + _g._dataPayroll.payroll_employee_detail._code);
                        query.Append(" = " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + "),0) as insurance_parents ,");

                        query.Append("coalesce((select " + _g._dataPayroll.payroll_employee_detail._insurance + " from ");
                        query.Append(_g._dataPayroll.payroll_employee_detail._table);
                        query.Append(" where " + _g._dataPayroll.payroll_employee_detail._code);
                        query.Append(" = " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + "),0) as insurance ,");

                        query.Append("coalesce((select " + _g._dataPayroll.payroll_employee_detail._loan + " from ");
                        query.Append(_g._dataPayroll.payroll_employee_detail._table);
                        query.Append(" where " + _g._dataPayroll.payroll_employee_detail._code);
                        query.Append(" = " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + "),0) as loan, ");

                        query.Append("coalesce((select " + _g._dataPayroll.payroll_employee_detail._donation + " from ");
                        query.Append(_g._dataPayroll.payroll_employee_detail._table);
                        query.Append(" where " + _g._dataPayroll.payroll_employee_detail._code);
                        query.Append(" = " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + "),0) as donation, ");

                        query.Append("coalesce((select " + _g._dataPayroll.payroll_employee_detail._donation_education + " from ");
                        query.Append(_g._dataPayroll.payroll_employee_detail._table);
                        query.Append(" where " + _g._dataPayroll.payroll_employee_detail._code);
                        query.Append(" = " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + "),0) as donation_education, ");

                        query.Append("coalesce((select " + _g._dataPayroll.payroll_employee_detail._except_labour_legislation + " from ");
                        query.Append(_g._dataPayroll.payroll_employee_detail._table);
                        query.Append(" where " + _g._dataPayroll.payroll_employee_detail._code);
                        query.Append(" = " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + "),0) as except_labour_legislation, ");

                        query.Append("coalesce((select " + _g._dataPayroll.payroll_employee_detail._buy_funds_reserves + " from ");
                        query.Append(_g._dataPayroll.payroll_employee_detail._table);
                        query.Append(" where " + _g._dataPayroll.payroll_employee_detail._code);
                        query.Append(" = " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + "),0) as buy_funds_reserves, ");

                        query.Append("coalesce((select " + _g._dataPayroll.payroll_employee_detail._buy_share + " from ");
                        query.Append(_g._dataPayroll.payroll_employee_detail._table);
                        query.Append(" where " + _g._dataPayroll.payroll_employee_detail._code);
                        query.Append(" = " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + "),0) as buy_share, ");

                        query.Append("coalesce((select " + _g._dataPayroll.payroll_employee_detail._except_funds_reserves + " from ");
                        query.Append(_g._dataPayroll.payroll_employee_detail._table);
                        query.Append(" where " + _g._dataPayroll.payroll_employee_detail._code);
                        query.Append(" = " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + "),0) as except_funds_reserves, ");

                        query.Append("coalesce((select " + _g._dataPayroll.payroll_employee_detail._except_gbk + " from ");
                        query.Append(_g._dataPayroll.payroll_employee_detail._table);
                        query.Append(" where " + _g._dataPayroll.payroll_employee_detail._code);
                        query.Append(" = " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + "),0) as except_gbk, ");

                        query.Append("coalesce((select " + _g._dataPayroll.payroll_employee_detail._except_teacher + " from ");
                        query.Append(_g._dataPayroll.payroll_employee_detail._table);
                        query.Append(" where " + _g._dataPayroll.payroll_employee_detail._code);
                        query.Append(" = " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + "),0) as except_teacher, ");

                        query.Append("coalesce((select " + _g._dataPayroll.payroll_employee_detail._except_65year + " from ");
                        query.Append(_g._dataPayroll.payroll_employee_detail._table);
                        query.Append(" where " + _g._dataPayroll.payroll_employee_detail._code);
                        query.Append(" = " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + "),0) as except_65year, ");

                        query.Append("coalesce((select " + _g._dataPayroll.payroll_employee_detail._except_spouse_65year + " from ");
                        query.Append(_g._dataPayroll.payroll_employee_detail._table);
                        query.Append(" where " + _g._dataPayroll.payroll_employee_detail._code);
                        query.Append(" = " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + "),0) as except_spouse_65year, ");

                        query.Append("coalesce((select (");
                        query.Append(_g._dataPayroll.payroll_employee_detail._buy_funds_reserves + "+");
                        query.Append(_g._dataPayroll.payroll_employee_detail._buy_share + "+");
                        query.Append(_g._dataPayroll.payroll_employee_detail._except_funds_reserves + "+");
                        query.Append(_g._dataPayroll.payroll_employee_detail._except_gbk + "+");
                        query.Append(_g._dataPayroll.payroll_employee_detail._except_teacher + "+");
                        query.Append(_g._dataPayroll.payroll_employee_detail._except_65year + "+");
                        query.Append(_g._dataPayroll.payroll_employee_detail._except_spouse_65year + "+");
                        query.Append(_g._dataPayroll.payroll_employee_detail._except_labour_legislation);
                        query.Append(") from ");
                        query.Append(_g._dataPayroll.payroll_employee_detail._table);
                        query.Append(" where " + _g._dataPayroll.payroll_employee_detail._code);
                        query.Append(" = " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + "),0) as reduce_other,");

                        query.Append(FieldEmployeeData.ToString());
                        query.Append(_g._dataPayroll.payroll_trans._employee + ",");
                        query.Append(" (select " + _g._dataPayroll.payroll_employee._employee_type + " from " + _g._dataPayroll.payroll_employee._table + " where " + _g._dataPayroll.payroll_employee._code + " = " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + ")  as " + _g._dataPayroll.payroll_resource._employee_type + ",");
                        query.Append(" (select " + _g._dataPayroll.payroll_employee._name + " from " + _g._dataPayroll.payroll_employee._table + " where " + _g._dataPayroll.payroll_employee._code + " = " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + ")  as " + _g._dataPayroll.payroll_resource._employee_name);
                        query.Append(" from " + _g._dataPayroll.payroll_trans._table);
                        query.Append(" " + WhereTrans + " " + GroupBy + " " + OrderBy + ") as temp1");
                        break;
                }
            }
            
            //Query อัตราภาษี
            string queryIncomeTax = "select "
                        + "coalesce((" + _g._dataPayroll.payroll_income_tax._income_from + "),0) as income_from,"
                        + "coalesce((" + _g._dataPayroll.payroll_income_tax._income_to + "),0) as income_to,"
                        + "coalesce((" + _g._dataPayroll.payroll_income_tax._tax_rate + "),0) as tax_rate"
                        + " from "
                        + _g._dataPayroll.payroll_income_tax._table
                        + " order by " + _g._dataPayroll.payroll_income_tax._income_from + " asc ";

            //Query เช็คหักรายจ่าย และอื่นๆ ที่อยู่ใน Table Company config
            string queryCheckCosts = "select "
                        + "coalesce((" + _g._dataPayroll.payroll_company_config._costs_sow + "),0) as " + _g._dataPayroll.payroll_company_config._costs_sow + ","
                        + "coalesce((" + _g._dataPayroll.payroll_company_config._costs_leave + "),0) as " + _g._dataPayroll.payroll_company_config._costs_leave + ","
                        + "coalesce((" + _g._dataPayroll.payroll_company_config._costs_late + "),0) as " + _g._dataPayroll.payroll_company_config._costs_late + ","
                        + "coalesce((" + _g._dataPayroll.payroll_company_config._costs_advance + "),0) as " + _g._dataPayroll.payroll_company_config._costs_advance + ","
                        + "coalesce((" + _g._dataPayroll.payroll_company_config._costs_insure + "),0) as " + _g._dataPayroll.payroll_company_config._costs_insure + ","
                        + "coalesce((" + _g._dataPayroll.payroll_company_config._costs_payout + "),0) as " + _g._dataPayroll.payroll_company_config._costs_payout + ","
                //เช็คสถานะคำนวนภาษี กี่งวด
                        + "coalesce((" + _g._dataPayroll.payroll_company_config._tax_period_month + "),0) as " + _g._dataPayroll.payroll_company_config._tax_period_month + ","
                        + "coalesce((" + _g._dataPayroll.payroll_company_config._tax_period_week + "),0) as " + _g._dataPayroll.payroll_company_config._tax_period_week + ","
                        + "coalesce((" + _g._dataPayroll.payroll_company_config._tax_period_day + "),0) as " + _g._dataPayroll.payroll_company_config._tax_period_day + ","
                        + "coalesce((" + _g._dataPayroll.payroll_company_config._period_number_month + "),0) as " + _g._dataPayroll.payroll_company_config._period_number_month + ","
                        + "coalesce((" + _g._dataPayroll.payroll_company_config._period_number_week + "),0) as " + _g._dataPayroll.payroll_company_config._period_number_week + ","
                        + "coalesce((" + _g._dataPayroll.payroll_company_config._period_number_day + "),0) as " + _g._dataPayroll.payroll_company_config._period_number_day
                        + " from " + _g._dataPayroll.payroll_company_config._table;            

            try
            {
                //Data Set
                //ค้นหาอัตราภาษี (ตัวเสริม คำนวน)                      
                DataSet _ds = myFrameWork._query(MyLib._myGlobal._databaseName, queryIncomeTax);//check Tax rate
                //เช็คหักรายจ่าย (ตัวเสริม คำนวน)
                DataSet _dsCosts = myFrameWork._query(MyLib._myGlobal._databaseName, queryCheckCosts);
                //รายได้ และ ภาษี (ตัวหลัก)               
                _processDS = myFrameWork._query(MyLib._myGlobal._databaseName, query.ToString());                          
                //สร้าง Format
                string _formatNumber = "{0:0.00}";
                //นับข้อมูล
                int _checkRow = _processDS.Tables[0].Rows.Count;
                //ทำการวน เช็ค และ คำนวน
                for (int i = 0; i < _checkRow; i++)
                {
                    //จับข้อมูลใส่ตัวแปร เตรียม  ฟ้อง error
                    //error_employee_code = _processDS.Tables[0].Rows[i][_g._dataPayroll.payroll_resource._employee].ToString();
                    //error_employee_name = _processDS.Tables[0].Rows[i][_g._dataPayroll.payroll_resource._employee_name].ToString();                
                    //เช็คสถานะคำนวนภาษี
                    bool checkPeriod = false;
                    int employeeType = MyLib._myGlobal._intPhase(_processDS.Tables[0].Rows[i][_g._dataPayroll.payroll_resource._employee_type].ToString());
                    int taxPeriodMonth = MyLib._myGlobal._intPhase(_dsCosts.Tables[0].Rows[0][_g._dataPayroll.payroll_company_config._tax_period_month].ToString());
                    int taxPeriodWeek = MyLib._myGlobal._intPhase(_dsCosts.Tables[0].Rows[0][_g._dataPayroll.payroll_company_config._tax_period_week].ToString());
                    int taxPeriodDay = MyLib._myGlobal._intPhase(_dsCosts.Tables[0].Rows[0][_g._dataPayroll.payroll_company_config._tax_period_day].ToString());
                    //เช็คสถานะงวดต่อเดือน
                    int periodNumberMonth = MyLib._myGlobal._intPhase(_dsCosts.Tables[0].Rows[0][_g._dataPayroll.payroll_company_config._period_number_month].ToString());
                    int periodNumberWeek = MyLib._myGlobal._intPhase(_dsCosts.Tables[0].Rows[0][_g._dataPayroll.payroll_company_config._period_number_week].ToString());
                    int periodNumberDay = MyLib._myGlobal._intPhase(_dsCosts.Tables[0].Rows[0][_g._dataPayroll.payroll_company_config._period_number_day].ToString());
                    //ใส่ค่าก่อนคำนวน รับ (+)
                    decimal wages_money = decimal.Parse(String.Format(_formatNumber, decimal.Parse(_processDS.Tables[0].Rows[i][_g._dataPayroll.payroll_resource._wages_money].ToString())));                    
                    decimal ot_money = decimal.Parse(String.Format(_formatNumber, decimal.Parse(_processDS.Tables[0].Rows[i][_g._dataPayroll.payroll_resource._ot_money].ToString())));
                    decimal diligent_money = decimal.Parse(String.Format(_formatNumber, decimal.Parse(_processDS.Tables[0].Rows[i][_g._dataPayroll.payroll_resource._diligent_money].ToString())));
                    decimal prepay_money = decimal.Parse(String.Format(_formatNumber, decimal.Parse(_processDS.Tables[0].Rows[i][_g._dataPayroll.payroll_resource._prepay_money].ToString())));
                    decimal bonus_money = decimal.Parse(String.Format(_formatNumber, decimal.Parse(_processDS.Tables[0].Rows[i][_g._dataPayroll.payroll_resource._bonus_money].ToString())));
                    decimal other_income_money = decimal.Parse(String.Format(_formatNumber, decimal.Parse(_processDS.Tables[0].Rows[i][_g._dataPayroll.payroll_resource._other_income_money].ToString())));
                    //ใส่ค่าก่อนคำนวน จ่าย (-)
                    decimal sow_money = decimal.Parse(String.Format(_formatNumber, decimal.Parse(_processDS.Tables[0].Rows[i][_g._dataPayroll.payroll_resource._sow_money].ToString())));
                    decimal leave_money = decimal.Parse(String.Format(_formatNumber, decimal.Parse(_processDS.Tables[0].Rows[i][_g._dataPayroll.payroll_resource._leave_money].ToString())));
                    decimal late_money = decimal.Parse(String.Format(_formatNumber, decimal.Parse(_processDS.Tables[0].Rows[i][_g._dataPayroll.payroll_resource._late_money].ToString())));
                    decimal advance_money = decimal.Parse(String.Format(_formatNumber, decimal.Parse(_processDS.Tables[0].Rows[i][_g._dataPayroll.payroll_resource._advance_money].ToString())));
                    decimal insure_money = decimal.Parse(String.Format(_formatNumber, decimal.Parse(_processDS.Tables[0].Rows[i][_g._dataPayroll.payroll_resource._insure_money].ToString())));
                    decimal other_payout_money = decimal.Parse(String.Format(_formatNumber, decimal.Parse(_processDS.Tables[0].Rows[i][_g._dataPayroll.payroll_resource._other_payout_money].ToString())));
                    //รายการหักลดหย่อน
                    decimal getDataSituation = decimal.Parse(_processDS.Tables[0].Rows[i]["situation"].ToString());//สถานะภาพ
                    decimal getDataSpouseWork = decimal.Parse(_processDS.Tables[0].Rows[i]["spouse_work_status"].ToString());//สถานะคู่สมรส
                    decimal getDataSpouseTax = decimal.Parse(_processDS.Tables[0].Rows[i]["spouse_tax"].ToString());//การยื่นภาษีของคู่สมรส
                    decimal getDataReduceChildStudy = decimal.Parse(_processDS.Tables[0].Rows[i]["reduce_child_study"].ToString());//ลดหย่อนบุตรที่ศึกษา
                    decimal getDataReduceChildNoStudy = decimal.Parse(_processDS.Tables[0].Rows[i]["reduce_child_no_study"].ToString());//ลดหย่อนบุตรที่ไม่ได้ศึกษา                   
                    decimal getDataReduceParents = decimal.Parse(_processDS.Tables[0].Rows[i]["reduce_parents"].ToString());//ลดหย่อนบิดามารดา
                    decimal getDataReduceInsurenceParents = decimal.Parse(_processDS.Tables[0].Rows[i]["insurance_parents"].ToString());//ลดหย่อนเบี๊ยประกันบิดามารดา
                    decimal getDataReduceDonation = decimal.Parse(_processDS.Tables[0].Rows[i]["donation"].ToString());//ลดหย่อนเงินบริจาค
                    decimal getDataReduceDonationEducation = decimal.Parse(_processDS.Tables[0].Rows[i]["donation_education"].ToString());//ลดหย่อนเงินบริจาคเพื่อการศึกษา
                    decimal getDataReduceInsurence = decimal.Parse(_processDS.Tables[0].Rows[i]["insurance"].ToString());//ลดหย่อนเบี้ยประกัน
                    decimal getDataReduceLoan = decimal.Parse(_processDS.Tables[0].Rows[i]["loan"].ToString());//ลดหย่อนเบี้ยเงินกู้                                    
                    decimal getDataReduceOther = decimal.Parse(_processDS.Tables[0].Rows[i]["reduce_other"].ToString());//ลดหย่อนอื่นๆ  
                    //คำนวนประกันสังคม                    
                    decimal social_security_max = decimal.Parse(_processDS.Tables[0].Rows[i]["social_security_max"].ToString());
                    decimal social_security_min = decimal.Parse(_processDS.Tables[0].Rows[i]["social_security_min"].ToString());
                    decimal customer_ss_rate = decimal.Parse(_processDS.Tables[0].Rows[i]["customer_ss_rate"].ToString());
                    decimal customer_rf_rate = decimal.Parse(_processDS.Tables[0].Rows[i]["customer_rf_rate"].ToString());
                    //เช็คลดหย่อน ประกันสังคม กองทุนสำรองฯ 1 ปีไม่เกิน
                    decimal getReduceReserveFund = decimal.Parse(_processDS.Tables[0].Rows[i]["reduce_reserve_fund"].ToString());
                    decimal getReduceSocialSecurity = decimal.Parse(_processDS.Tables[0].Rows[i]["reduce_social_security"].ToString());
                    //ตัวแปร ในการคำนวน 
                    decimal count_month = 0.0M;
                    //ตัวแปร (หนึ่งเดือน)
                    //ประกันสังคม กับ กองทุนสำรอง แสดงเฉพาะเงินเดือนเท่านั้น                    
                    decimal society_insure_money_in_month = 0.0M;
                    decimal reserve_fund_money_in_month = 0.0M;
                    //เฉพาะสลิปเงินเดือน คำนวนเงินเดือน ออก ภงด.1 เท่านั้น
                    decimal Society_month = 0.0M;
                    decimal reserve_fund_month = 0.0M;
                    decimal sumCosts_month = 0.0M;
                    decimal wages_month = 0.0M;
                    //ตัวแปร (ตามจริง)
                    //เช็คสถานะคำนวนภาษี
                    int periodInYear = 0;
                    int periodInMonth = 12;
                    //ประกันสังคม กับ กองทุนสำรอง
                    decimal society_insure_money = 0.0M;
                    decimal reserve_fund_money = 0.0M;
                    //รวมเงินได้ ไม่หัก รายจ่าย ออกภาษี
                    decimal wages_for_tax = 0.0M;
                    //เช็คหักค่าใช้จ่าย (เพิ่มใหม่ ปกติไม่มี)
                    decimal sumCosts = 0.0M;
                    //รายได้ ทั้งหมด รวม OT // ถ้ามีหักค่าใช้จ่ายจะหักตอนนี้
                    decimal getDataSalary = 0.0M;
                    //รวม รับ (+)
                    decimal sumincome = 0.0M;
                    //รวม จ่าย (-)
                    decimal sumpayout = 0.0M;
                    //รวม ขาด ลา มาสาย
                    decimal sumsow = 0.0M;
                    //รวมเงินได้ สุทธิ ออกรายงาน สลิปเงินเดือน และ คำนวนเงินเดือน
                    decimal net_wages = 0.0M;
                    //Function การคำนวนหาค่าต่างๆ

                    //คำนวนภาษี
                    decimal SumChild = getDataReduceChildStudy + getDataReduceChildNoStudy;//รวมเงินลดหย่อนบุตร                   
                    decimal myMoney = decimal.Parse(_processDS.Tables[0].Rows[i]["reduce_myself"].ToString()); ;//เงินลดย่อนผู้มีเงินได้
                    decimal SpouseWork = decimal.Parse(_processDS.Tables[0].Rows[i]["reduce_spouse"].ToString()); ;//คู่สมรสมไม่มีเงินได้
                    decimal Salary = 0;//รวมยอดเงินเดือนที่หักลดหย่อน 
                    string Remark = "";
                    //เงินเดือน คำนวนภาษีงวดสุดท้าย เท่านั้น
                    decimal _sum_tax_month = 0.0M;
                    //คำนวนภาษีทุกงวด ปกติ
                    decimal mySalary = 0.0M; //เงินเดือนหลัง คูณ ด้วย การคิด ภาษี ทั้งปี
                    decimal CheckSalary = 0.0M;// เช็ค ลดหย่อนส่วนตัว 40%
                    decimal Check40percen = 0.0M;// จำนวนเงินลดหย่อนส่วนตัว 40%   
                    //เรทภาษี
                    decimal _saraly = 0.0M;
                    decimal _sum = 0.0M;
                    //Data Rows Table เรทภาษี
                    DataRow[] _dr = _ds.Tables[0].Select("");

                    switch (ScreenName)
                    {                        
                        case _g.g._screenPayrollEnum.รายงานภาษีเงินได้_รายเดือน:
                        case _g.g._screenPayrollEnum.ภงด_1:
                        case _g.g._screenPayrollEnum.ภงด_1_ใบแนบ:
                        case _g.g._screenPayrollEnum.คำนวนภาษีรายเดือน:
                        case _g.g._screenPayrollEnum.รายงานภาษีเงินได้_รายปี:
                        case _g.g._screenPayrollEnum.คำนวนภาษีรายปี:
                        case _g.g._screenPayrollEnum.ภงด_91:
                        case _g.g._screenPayrollEnum.ภงด_1_ก_ใบแนบ:
                        case _g.g._screenPayrollEnum.ภงด_1_ก:
                            switch (ScreenName)
                            {
                                case _g.g._screenPayrollEnum.รายงานภาษีเงินได้_รายปี:
                                case _g.g._screenPayrollEnum.คำนวนภาษีรายปี:
                                case _g.g._screenPayrollEnum.ภงด_91:
                                case _g.g._screenPayrollEnum.ภงด_1_ก_ใบแนบ:
                                case _g.g._screenPayrollEnum.ภงด_1_ก:
                                    //string Queryperiod = "select count(period) as period from(select period as period from(select "
                                    //      + " coalesce(("
                                    //      + " CASE " + _g._dataPayroll.payroll_employee._employee_type
                                    //      + " WHEN 0 THEN "

                                    //      + "case when  (to_char(date_pay_salary,'DD') between to_char((select start_one_month from payroll_company_config),'00')  and  to_char((select end_one_month from payroll_company_config),'00')) then 1"
                                    //      + "when  (to_char(date_pay_salary,'DD') between to_char((select start_two_month from payroll_company_config),'00')  and  to_char((select end_two_month from payroll_company_config),'00')) then 2"
                                    //      + "when  (to_char(date_pay_salary,'DD') between to_char((select start_three_month from payroll_company_config),'00') and to_char((select end_three_month from payroll_company_config),'00')) then 3"
                                    //      + "else 4"
                                    //      + "end"

                                    //      + " WHEN 1 THEN "

                                    //      + "case when  to_char(date_pay_salary,'DD') between to_char((select start_one_week from payroll_company_config),'00')  and  to_char((select end_one_week from payroll_company_config),'00') then 1"
                                    //      + "when  to_char(date_pay_salary,'DD') between to_char((select start_two_week from payroll_company_config),'00')  and  to_char((select end_two_week from payroll_company_config),'00') then 2"
                                    //      + "when  to_char(date_pay_salary,'DD') between to_char((select start_three_week from payroll_company_config),'00')  and  to_char((select end_three_week from payroll_company_config),'00') then 3"
                                    //      + "else 4"
                                    //      + "end"

                                    //      + " ELSE "

                                    //      + "case when  to_char(date_pay_salary,'DD') between to_char((select start_one_day from payroll_company_config),'00')  and  to_char((select end_one_day from payroll_company_config),'00') then 1"
                                    //      + "when  to_char(date_pay_salary,'DD') between to_char((select start_two_day from payroll_company_config),'00')  and   to_char((select end_two_day from payroll_company_config),'00') then 2"
                                    //      + "when  to_char(date_pay_salary,'DD') between to_char((select start_three_day from payroll_company_config),'00')  and  to_char((select end_three_day from payroll_company_config),'00') then 3"
                                    //      + "else 4"
                                    //      + "end"

                                    //      + " END"
                                    //      + " ),0) as period,"
                                    //      + "select_month"
                                    //      + " from payroll_trans "
                                    //      + " where employee = '" + _processDS.Tables[0].Rows[i][_g._dataPayroll.payroll_resource._employee].ToString() + "'"                                          
                                    //      + " and select_year = " + InYear
                                    //      + ") as temp1 group by period,select_month) as temp2";

                                    //DataSet _dsPeriod = myFrameWork._query(MyLib._myGlobal._databaseName, Queryperiod);
                                    //if (employeeType == 0)
                                    //{
                                    //    if (periodNumberMonth > 0)
                                    //    {
                                    //        wages_money = (wages_money / periodNumberMonth) * decimal.Parse(_dsPeriod.Tables[0].Rows[0]["period"].ToString());
                                    //    }
                                    //}
                                    //else if (employeeType == 1)
                                    //{
                                    //    if (periodNumberWeek > 0)
                                    //    {
                                    //        wages_money = (wages_money / periodNumberWeek) * decimal.Parse(_dsPeriod.Tables[0].Rows[0]["period"].ToString());
                                    //    }
                                    //}
                                    if (employeeType == 0)
                                    {
                                        //สำหรับพนักงานรายเดือนเท่านั้น
                                        //string employee = _processDS.Tables[0].Rows[i][_g._dataPayroll.payroll_resource._employee].ToString();
                                        //string queryCountMonth = "select count(select_month) as count_month from (select select_month from payroll_trans where employee = '" + employee + "'  " + WhereTransSub + " group by select_month) as temp_count_month";
                                        //DataSet _dsCountMonth = myFrameWork._query(MyLib._myGlobal._databaseName, queryCountMonth);
                                        //count_month = decimal.Parse(_dsCountMonth.Tables[0].Rows[0]["count_month"].ToString());
                                        //wages_money = wages_money * count_month;
                                        //คำนวนกองทุนสำรองฯ ต่อเดือน ต่อปี
                                        reserve_fund_money = (wages_money * customer_rf_rate) / 100;
                                        //คำนวนประกันสังคม ต่อปี
                                        society_insure_money = (wages_money * customer_ss_rate) / 100;
                                        //เช็คลดหย่อน ประกันสังคม ต่อปี
                                        if (society_insure_money > getReduceSocialSecurity)
                                        {
                                            society_insure_money = getReduceSocialSecurity;
                                        }
                                        //เช็คลดหย่อน กองทุนสำรองฯ ต่อปี
                                        if (reserve_fund_money > getReduceReserveFund)
                                        {
                                            reserve_fund_money = getReduceReserveFund;
                                        }
                                    }
                                    else if (employeeType == 1)
                                    {
                                        //สำหรับพนักงานรายสัปดาห์เท่านั้น
                                        //string employee = _processDS.Tables[0].Rows[i][_g._dataPayroll.payroll_resource._employee].ToString();
                                        //string queryCountMonth = "select count(select_month) as count_month from (select select_month from payroll_trans where employee = '" + employee + "'  " + WhereTransSub + " group by select_month) as temp_count_month";
                                        //DataSet _dsCountMonth = myFrameWork._query(MyLib._myGlobal._databaseName, queryCountMonth);
                                        //count_month = decimal.Parse(_dsCountMonth.Tables[0].Rows[0]["count_month"].ToString());
                                        //wages_money = wages_money * count_month;
                                        //คำนวนกองทุนสำรองฯ ต่อเดือน ต่อปี
                                        reserve_fund_money = (wages_money * customer_rf_rate) / 100;
                                        //คำนวนประกันสังคม ต่อปี
                                        society_insure_money = (wages_money * customer_ss_rate) / 100;
                                        //เช็คลดหย่อน ประกันสังคม ต่อปี
                                        if (society_insure_money > getReduceSocialSecurity)
                                        {
                                            society_insure_money = getReduceSocialSecurity;
                                        }
                                        //เช็คลดหย่อน กองทุนสำรองฯ ต่อปี
                                        if (reserve_fund_money > getReduceReserveFund)
                                        {
                                            reserve_fund_money = getReduceReserveFund;
                                        }
                                    }
                                    else
                                    {
                                        //คำนวนกองทุนสำรองฯ ต่อเดือน ต่อปี
                                        reserve_fund_money = decimal.Parse(_processDS.Tables[0].Rows[i][_g._dataPayroll.payroll_resource._reserve_money].ToString());
                                        //คำนวนประกันสังคม ต่อปี
                                        society_insure_money = decimal.Parse(_processDS.Tables[0].Rows[i][_g._dataPayroll.payroll_resource._society_insure_money].ToString());
                                        //เช็คลดหย่อน ประกันสังคม ต่อปี
                                        if (society_insure_money > getReduceSocialSecurity)
                                        {
                                            society_insure_money = getReduceSocialSecurity;
                                        }
                                        //เช็คลดหย่อน กองทุนสำรองฯ ต่อปี
                                        if (reserve_fund_money > getReduceReserveFund)
                                        {
                                            reserve_fund_money = getReduceReserveFund;
                                        }
                                    }
                                    //เช็คหักค่าใช้จ่าย (เพิ่มใหม่ ปกติไม่มี) (ตามจริง)                   
                                    if (decimal.Parse(_dsCosts.Tables[0].Rows[0][_g._dataPayroll.payroll_company_config._costs_sow].ToString()) == 1) sumCosts = sumCosts + decimal.Parse(_processDS.Tables[0].Rows[i][_g._dataPayroll.payroll_resource._sow_money].ToString());
                                    if (decimal.Parse(_dsCosts.Tables[0].Rows[0][_g._dataPayroll.payroll_company_config._costs_leave].ToString()) == 1) sumCosts = sumCosts + decimal.Parse(_processDS.Tables[0].Rows[i][_g._dataPayroll.payroll_resource._leave_money].ToString());
                                    if (decimal.Parse(_dsCosts.Tables[0].Rows[0][_g._dataPayroll.payroll_company_config._costs_late].ToString()) == 1) sumCosts = sumCosts + decimal.Parse(_processDS.Tables[0].Rows[i][_g._dataPayroll.payroll_resource._late_money].ToString());
                                    if (decimal.Parse(_dsCosts.Tables[0].Rows[0][_g._dataPayroll.payroll_company_config._costs_advance].ToString()) == 1) sumCosts = sumCosts + decimal.Parse(_processDS.Tables[0].Rows[i][_g._dataPayroll.payroll_resource._advance_money].ToString());
                                    if (decimal.Parse(_dsCosts.Tables[0].Rows[0][_g._dataPayroll.payroll_company_config._costs_insure].ToString()) == 1) sumCosts = sumCosts + decimal.Parse(_processDS.Tables[0].Rows[i][_g._dataPayroll.payroll_resource._insure_money].ToString());
                                    if (decimal.Parse(_dsCosts.Tables[0].Rows[0][_g._dataPayroll.payroll_company_config._costs_payout].ToString()) == 1) sumCosts = sumCosts + decimal.Parse(_processDS.Tables[0].Rows[i][_g._dataPayroll.payroll_resource._other_payout_money].ToString());
                                    //รวมเงินได้ ไม่หัก รายจ่าย ออกภาษี
                                    wages_for_tax = ((wages_money + ot_money + diligent_money + bonus_money + other_income_money + prepay_money));
                                    //ใส่รายการอื่น
                                    getDataSalary = wages_for_tax - sumCosts;//รายได้ ทั้งหมด รวม OT // ถ้ามีหักค่าใช้จ่ายจะหักตอนนี้                                   


                                    ////หักลด ประกันสังคมกับ กองทุนสำรองฯ ก่อนคิดลดหย่อนอื่นๆ                                   
                                    ////เช็คว่าหัก กองทุนสำรองฯ หรือ ไม่                   
                                    //if (MyLib._myGlobal._intPhase(_processDS.Tables[0].Rows[i]["reserve_fund_status"].ToString()) == 0) reserve_fund_money = 0;
                                    ////เงินได้ = เงินได้ต่อปี - ประกันสังคมต่อปี - กองทุนสำรองต่อปี
                                    //getDataSalary = getDataSalary - society_insure_money - reserve_fund_money;
                                    break;                                
                                case _g.g._screenPayrollEnum.รายงานภาษีเงินได้_รายเดือน:
                                case _g.g._screenPayrollEnum.ภงด_1:
                                case _g.g._screenPayrollEnum.ภงด_1_ใบแนบ:
                                case _g.g._screenPayrollEnum.คำนวนภาษีรายเดือน:
                                    //เช็คหักค่าใช้จ่าย (เพิ่มใหม่ ปกติไม่มี) (ตามจริง)                   
                                    if (decimal.Parse(_dsCosts.Tables[0].Rows[0][_g._dataPayroll.payroll_company_config._costs_sow].ToString()) == 1) sumCosts = sumCosts + decimal.Parse(_processDS.Tables[0].Rows[i][_g._dataPayroll.payroll_resource._sow_money].ToString());
                                    if (decimal.Parse(_dsCosts.Tables[0].Rows[0][_g._dataPayroll.payroll_company_config._costs_leave].ToString()) == 1) sumCosts = sumCosts + decimal.Parse(_processDS.Tables[0].Rows[i][_g._dataPayroll.payroll_resource._leave_money].ToString());
                                    if (decimal.Parse(_dsCosts.Tables[0].Rows[0][_g._dataPayroll.payroll_company_config._costs_late].ToString()) == 1) sumCosts = sumCosts + decimal.Parse(_processDS.Tables[0].Rows[i][_g._dataPayroll.payroll_resource._late_money].ToString());
                                    if (decimal.Parse(_dsCosts.Tables[0].Rows[0][_g._dataPayroll.payroll_company_config._costs_advance].ToString()) == 1) sumCosts = sumCosts + decimal.Parse(_processDS.Tables[0].Rows[i][_g._dataPayroll.payroll_resource._advance_money].ToString());
                                    if (decimal.Parse(_dsCosts.Tables[0].Rows[0][_g._dataPayroll.payroll_company_config._costs_insure].ToString()) == 1) sumCosts = sumCosts + decimal.Parse(_processDS.Tables[0].Rows[i][_g._dataPayroll.payroll_resource._insure_money].ToString());
                                    if (decimal.Parse(_dsCosts.Tables[0].Rows[0][_g._dataPayroll.payroll_company_config._costs_payout].ToString()) == 1) sumCosts = sumCosts + decimal.Parse(_processDS.Tables[0].Rows[i][_g._dataPayroll.payroll_resource._other_payout_money].ToString());
                                    //รวมเงินได้ ไม่หัก รายจ่าย ออกภาษี
                                    wages_for_tax = ((wages_money + ot_money + diligent_money + bonus_money + other_income_money + prepay_money));
                                    //ใส่รายการอื่น
                                    getDataSalary = wages_for_tax - sumCosts;//รายได้ ทั้งหมด รวม OT // ถ้ามีหักค่าใช้จ่ายจะหักตอนนี้
                                    //คำนวนกองทุนสำรองฯ ต่อเดือน ต่อปี
                                    reserve_fund_money = decimal.Parse(_processDS.Tables[0].Rows[i][_g._dataPayroll.payroll_resource._reserve_money].ToString());
                                    //คำนวนประกันสังคม ต่อเดือน  
                                    decimal checkSociety = decimal.Parse(_processDS.Tables[0].Rows[i][_g._dataPayroll.payroll_resource._society_insure_money].ToString());
                                    if (checkSociety > social_security_max)
                                    {
                                        //Modify a Row
                                        _processDS.Tables[0].Rows[i][_g._dataPayroll.payroll_resource._society_insure_money] = (social_security_max);
                                        society_insure_money = (social_security_max);
                                    }
                                    else if (checkSociety < (social_security_min))
                                    {
                                        //Modify a Row
                                        _processDS.Tables[0].Rows[i][_g._dataPayroll.payroll_resource._society_insure_money] = (social_security_min);
                                        society_insure_money = (social_security_min);
                                    }
                                    else
                                    {
                                        society_insure_money = decimal.Parse(_processDS.Tables[0].Rows[i][_g._dataPayroll.payroll_resource._society_insure_money].ToString());
                                    }

                                    //คิดเงินเดือน เป็น 1 ปี                           
                                    getDataSalary = getDataSalary * 12;
                                    //คิดประกันสังคม 1 ปี
                                    if ((society_insure_money * 12) > getReduceSocialSecurity)
                                    {
                                        society_insure_money = getReduceSocialSecurity;
                                    }
                                    else
                                    {
                                        society_insure_money = society_insure_money * 12;
                                    }
                                    //คิดกองทุนสำรองฯ 1 ปี
                                    if ((reserve_fund_money * 12) > getReduceReserveFund)
                                    {
                                        reserve_fund_money = getReduceReserveFund;
                                    }
                                    else
                                    {
                                        reserve_fund_money = reserve_fund_money * 12;
                                    }

                                    break;
                            }
                            //คำนวนภาษีทุกงวด ปกติ
                            mySalary = getDataSalary + decimal.Parse(_processDS.Tables[0].Rows[i]["except_labour_legislation"].ToString()); //เงินเดือนหลัง คูณ ด้วย การคิด ภาษี ทั้งปี
                            CheckSalary = ((mySalary * 40) / 100);// เช็ค ลดหย่อนส่วนตัว 40%
                            Check40percen = 0;// จำนวนเงินลดหย่อนส่วนตัว 40%                        
                            //เช็คค่าใช้จ่ายส่วนตัวว่าเกิน 60,000 รึไม่
                            if (CheckSalary > 60000)
                            {
                                //เกิน 60,000 ให้ได้แค่ 60,000
                                Check40percen = 60000;
                            }
                            else
                            {
                                //ไม่เกินใช้ตามจริง
                                Check40percen = CheckSalary;
                            }
                            //หักลด ประกันสังคมกับ กองทุนสำรองฯ ก่อนคิดลดหย่อนอื่นๆ
                            //เช็คว่าหัก กองทุนสำรองฯ หรือ ไม่                   
                            if (MyLib._myGlobal._intPhase(_processDS.Tables[0].Rows[i]["reserve_fund_status"].ToString()) == 0) reserve_fund_money = 0;
                            //เงินได้ = เงินได้ต่อปี - ประกันสังคมต่อปี - กองทุนสำรองต่อปี
                            mySalary = mySalary - society_insure_money - reserve_fund_money;

                            //เช็คสถานะภาพก่อน 0 = โสด  1 = สมรส  2 = หย่า
                            if (getDataSituation == 0)
                            {
                                //โสด
                                //ค่าแรงสุทธิ = ค่าแรง - ลดหย่อน40% - ลดหย่อนส่วนตัว30,000 - ลดหย่อนบิดา/มารดา - ลดหย่อนเบี๊ยประกันบิดา/มารดา - ลดหย่อนเบี๊ยประกัน - ลดหย่อนเงินกู้ - ลดหย่อนอื่นๆ
                                Salary = (((((mySalary - Check40percen) - myMoney) - (getDataReduceParents + getDataReduceInsurenceParents)) - getDataReduceInsurence) - getDataReduceLoan) - getDataReduceOther;
                                SumChild = 0; SpouseWork = 0;
                            }
                            else if (getDataSituation == 1)
                            {
                                //สมรส
                                //0 มีเงินได้ 1 ไม่มีเงินได้
                                if (getDataSpouseWork == 0)
                                {
                                    //0 = รวมยื่น 1 = แยกยื่น
                                    if (getDataSpouseTax == 0)
                                    {
                                        //สมรส
                                        //มีเงินได้
                                        //รวมยื่น
                                        //ค่าแรงสุทธิ = ค่าแรง - ลดหย่อน40% - ลดหย่อนส่วนตัว30,000 - ลดหย่อนคู่สมรส30,000 - รวมลดหย่อนบุตร - ลดหย่อนบิดา/มารดา  - ลดหย่อนเบี๊ยประกันบิดา/มารดา - ลดหย่อนเบี๊ยประกัน - ลดหย่อนเงินกู้ - ลดหย่อนอื่นๆ
                                        Salary = ((((((mySalary - Check40percen) - (myMoney + SpouseWork)) - SumChild) - (getDataReduceParents + getDataReduceInsurenceParents)) - getDataReduceInsurence) - getDataReduceLoan) - getDataReduceOther;
                                    }
                                    else
                                    {
                                        //สมรส
                                        //มีเงินได้
                                        //แยกยื่น
                                        //ค่าแรงสุทธิ = ค่าแรง - ลดหย่อน40% - ลดหย่อนส่วนตัว30,000  - รวมลดหย่อนบุตร - ลดหย่อนบิดา/มารดา - ลดหย่อนเบี๊ยประกันบิดา/มารดา  - ลดหย่อนเบี๊ยประกัน - ลดหย่อนเงินกู้ - ลดหย่อนอื่นๆ
                                        Salary = ((((((mySalary - Check40percen) - myMoney) - SumChild) - (getDataReduceParents + getDataReduceInsurenceParents)) - getDataReduceInsurence) - getDataReduceLoan) - getDataReduceOther;
                                        SpouseWork = 0;
                                    }
                                }
                                else
                                {
                                    //สมรส
                                    //ไม่มีเงินได้
                                    //ค่าแรงสุทธิ = ค่าแรง - ลดหย่อน40% - ลดหย่อนส่วนตัว30,000 - ลดหย่อนคู่สมรส30,000 - รวมลดหย่อนบุตร - ลดหย่อนบิดา/มารดา - ลดหย่อนเบี๊ยประกันบิดา/มารดา - ลดหย่อนเบี๊ยประกัน - ลดหย่อนเงินกู้ - ลดหย่อนอื่นๆ
                                    Salary = ((((((mySalary - Check40percen) - (myMoney + SpouseWork)) - SumChild) - (getDataReduceParents + getDataReduceInsurenceParents)) - getDataReduceInsurence) - getDataReduceLoan) - getDataReduceOther;
                                }
                            }
                            else
                            {
                                //หย่าร้าง
                                //ค่าแรงสุทธิ = ค่าแรง - ลดหย่อน40% - ลดหย่อนส่วนตัว30,000  - รวมลดหย่อนบุตร - ลดหย่อนบิดา/มารดา  - ลดหย่อนเบี๊ยประกันบิดา/มารดา - ลดหย่อนเบี๊ยประกัน - ลดหย่อนเงินกู้ - ลดหย่อนอื่นๆ
                                Salary = ((((((mySalary - Check40percen) - myMoney) - SumChild) - (getDataReduceParents + getDataReduceInsurenceParents)) - getDataReduceInsurence) - getDataReduceLoan) - getDataReduceOther;
                                SpouseWork = 0;
                            }
                            //คิดเงินบริจาค
                            if (Salary > 0)
                            {
                                if (getDataReduceDonation > 0) if (getDataReduceDonation > ((Salary * 10) / 100)) getDataReduceDonation = ((Salary * 10) / 100);
                                if (getDataReduceDonationEducation > 0) if (getDataReduceDonationEducation > ((Salary * 10) / 100)) getDataReduceDonationEducation = ((Salary * 10) / 100);
                            }
                            Salary = Salary - (getDataReduceDonation + getDataReduceDonationEducation);
                            //เริ่มคำนวน เช็ค rate Tax ตามอัตรา ก้าวหน้า
                            _saraly = Salary;
                            _sum = 0;
                            if (decimal.Parse(_dr[0]["tax_rate"].ToString()) == 0 && Salary < decimal.Parse(_dr[0]["income_to"].ToString()))
                            {
                                //รายได้หักลดหย่อนแล้วน้อยกว่า ภาษีกำหนด
                                Remark = "ได้รับยกเว้นภาษี";
                            }
                            else
                            {
                                for (int _row = 0; _row < _dr.Length; _row++)
                                {
                                    decimal _income_from = decimal.Parse(_dr[_row]["income_from"].ToString());//เงินได้จาก
                                    decimal _income_to = decimal.Parse(_dr[_row]["income_to"].ToString()); //เงินได้ถึง
                                    decimal _tax_rate = decimal.Parse(_dr[_row]["tax_rate"].ToString());//เรทภาษี
                                    //โอ๋
                                    if (_saraly > 1)
                                    {

                                        if (_saraly > _income_to)
                                        {

                                            _sum += (((_income_to - (_income_from - 1)) * _tax_rate) / 100);
                                            _saraly = _saraly - (_income_to - (_income_from - 1));
                                        }
                                        else if (_saraly < _income_to)
                                        {
                                            _sum += ((_saraly * _tax_rate) / 100);
                                            _saraly = _income_to - _income_to;
                                        }

                                    }

                                }
                            }
                            break;
                        case _g.g._screenPayrollEnum.รายละเอียดเงินเดือน_ค่าจ้างทั้งปี:
                        case _g.g._screenPayrollEnum.สลิปเงินเดือน:
                        case _g.g._screenPayrollEnum.คำนวนเงินเดือน:
                        case _g.g._screenPayrollEnum.รายละเอียดเงินเดือน_ค่าจ้าง_ตามประเภทลูกจ้าง:
                            //เช็คจำนวนงวดเงินเดือน คิดประกันสังคม กรณี จ่ายเป็นงวด  
                            if (Option == 1)
                            {
                                //string Queryperiod = "select count(period) as period from(select period as period from(select "
                                //      + " coalesce(("
                                //      + " CASE " + _g._dataPayroll.payroll_employee._employee_type
                                //      + " WHEN 0 THEN "

                                //      + "case when  (to_char(date_pay_salary,'DD') between to_char((select start_one_month from payroll_company_config),'00')  and  to_char((select end_one_month from payroll_company_config),'00')) then 1"
                                //      + "when  (to_char(date_pay_salary,'DD') between to_char((select start_two_month from payroll_company_config),'00')  and  to_char((select end_two_month from payroll_company_config),'00')) then 2"
                                //      + "when  (to_char(date_pay_salary,'DD') between to_char((select start_three_month from payroll_company_config),'00') and to_char((select end_three_month from payroll_company_config),'00')) then 3"
                                //      + "else 4"
                                //      + "end"

                                //      + " WHEN 1 THEN "

                                //      + "case when  to_char(date_pay_salary,'DD') between to_char((select start_one_week from payroll_company_config),'00')  and  to_char((select end_one_week from payroll_company_config),'00') then 1"
                                //      + "when  to_char(date_pay_salary,'DD') between to_char((select start_two_week from payroll_company_config),'00')  and  to_char((select end_two_week from payroll_company_config),'00') then 2"
                                //      + "when  to_char(date_pay_salary,'DD') between to_char((select start_three_week from payroll_company_config),'00')  and  to_char((select end_three_week from payroll_company_config),'00') then 3"
                                //      + "else 4"
                                //      + "end"

                                //      + " ELSE "

                                //      + "case when  to_char(date_pay_salary,'DD') between to_char((select start_one_day from payroll_company_config),'00')  and  to_char((select end_one_day from payroll_company_config),'00') then 1"
                                //      + "when  to_char(date_pay_salary,'DD') between to_char((select start_two_day from payroll_company_config),'00')  and   to_char((select end_two_day from payroll_company_config),'00') then 2"
                                //      + "when  to_char(date_pay_salary,'DD') between to_char((select start_three_day from payroll_company_config),'00')  and  to_char((select end_three_day from payroll_company_config),'00') then 3"
                                //      + "else 4"
                                //      + "end"

                                //      + " END"
                                //      + " ),0) as period"
                                //      + " from payroll_trans "
                                //      + " where employee = '" + _processDS.Tables[0].Rows[i][_g._dataPayroll.payroll_resource._employee].ToString() + "'"
                                //      + " and select_month = " + _processDS.Tables[0].Rows[i]["select_month"].ToString()
                                //      + " and select_year = " + InYear
                                //      + ") as temp1 group by period) as temp2";

                                //DataSet _dsPeriod = myFrameWork._query(MyLib._myGlobal._databaseName, Queryperiod);
                                //if (employeeType == 0)
                                //{
                                //    if (periodNumberMonth > 0)
                                //    {
                                //        wages_money = (wages_money / periodNumberMonth) * decimal.Parse(_dsPeriod.Tables[0].Rows[0]["period"].ToString());
                                //    }
                                //}
                                //else if (employeeType == 1)
                                //{
                                //    if (periodNumberWeek > 0)
                                //    {
                                //        wages_money = (wages_money / periodNumberWeek) * decimal.Parse(_dsPeriod.Tables[0].Rows[0]["period"].ToString());
                                //    }
                                //}

                                society_insure_money_in_month = (wages_money * customer_ss_rate) / 100;
                                reserve_fund_money_in_month = (wages_money * customer_rf_rate) / 100;
                                //คำนวนประกันสังคม ต่องวด
                                if (society_insure_money_in_month > (social_security_max))
                                {
                                    society_insure_money_in_month = (social_security_max);
                                }
                                else if (society_insure_money_in_month < (social_security_min))
                                {
                                    society_insure_money_in_month = (social_security_min);
                                }
                            }
                            else
                            {
                                if (Option == 2)
                                {
                                    try
                                    {
                                        bool lastPriod = false;
                                        int period = MyLib._myGlobal._intPhase(_processDS.Tables[0].Rows[i]["period"].ToString());
                                        if (employeeType == 0 && period >= periodNumberMonth) lastPriod = true;
                                        if (employeeType == 1 && period >= periodNumberWeek) lastPriod = true;
                                        if (employeeType == 2 && period >= periodNumberDay) lastPriod = true;

                                        string code = _processDS.Tables[0].Rows[i][_g._dataPayroll.payroll_resource._employee].ToString();
                                        string month = _processDS.Tables[0].Rows[i]["select_month"].ToString();

                                        string startDate = MyLib._myGlobal._convertDateToQuery(this._processStartDate(_processDS.Tables[0].Rows[i]["start_date"].ToString(), _processDS.Tables[0].Rows[i]["select_month"].ToString(), InYear));
                                        string endDate = MyLib._myGlobal._convertDateToQuery(this._processEndDate(_processDS.Tables[0].Rows[i]["end_date"].ToString(), _processDS.Tables[0].Rows[i]["select_month"].ToString(), InYear, lastPriod));
                                        string TransDetailWhere = " and (" + _g._dataPayroll.payroll_trans_detail._select_year + " = '" + InYear + "') and (" + _g._dataPayroll.payroll_trans._select_month + " = " + month + ") and working_date between '" + startDate + "' and '" + endDate + "'";
                                        string QueryOtherMoney = "select "
                                          + " coalesce((select sum(" + _g._dataPayroll.payroll_trans_detail._diligent_money + ") from " + _g._dataPayroll.payroll_trans_detail._table + " where " + _g._dataPayroll.payroll_trans_detail._trans_flag + "=1 and " + _g._dataPayroll.payroll_trans_detail._trans_type + "=5 and " + _g._dataPayroll.payroll_trans_detail._employee + " =  " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + " " + TransDetailWhere + "),0) as " + _g._dataPayroll.payroll_resource._diligent_money + ","
                                          + " coalesce((select sum(" + _g._dataPayroll.payroll_trans_detail._prepay_money + ") from " + _g._dataPayroll.payroll_trans_detail._table + " where " + _g._dataPayroll.payroll_trans_detail._trans_flag + "=1 and " + _g._dataPayroll.payroll_trans_detail._trans_type + "=5 and " + _g._dataPayroll.payroll_trans_detail._employee + " =  " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + " " + TransDetailWhere + "),0) as " + _g._dataPayroll.payroll_resource._prepay_money + ","
                                          + " coalesce((select sum(" + _g._dataPayroll.payroll_trans_detail._bonus_money + ") from " + _g._dataPayroll.payroll_trans_detail._table + " where " + _g._dataPayroll.payroll_trans_detail._trans_flag + "=1 and " + _g._dataPayroll.payroll_trans_detail._trans_type + "=5 and " + _g._dataPayroll.payroll_trans_detail._employee + " =  " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + " " + TransDetailWhere + "),0) as " + _g._dataPayroll.payroll_resource._bonus_money + ","
                                          + " coalesce((select sum(" + _g._dataPayroll.payroll_trans_detail._other_income_money + ") from " + _g._dataPayroll.payroll_trans_detail._table + " where " + _g._dataPayroll.payroll_trans_detail._trans_flag + "=1 and " + _g._dataPayroll.payroll_trans_detail._trans_type + "=5 and " + _g._dataPayroll.payroll_trans_detail._employee + " =  " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + " " + TransDetailWhere + "),0) as " + _g._dataPayroll.payroll_resource._other_income_money + ","
                                          + " coalesce((select sum(" + _g._dataPayroll.payroll_trans_detail._advance_money + ") from " + _g._dataPayroll.payroll_trans_detail._table + " where " + _g._dataPayroll.payroll_trans_detail._trans_flag + "=1 and " + _g._dataPayroll.payroll_trans_detail._trans_type + "=5 and " + _g._dataPayroll.payroll_trans_detail._employee + " =  " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + " " + TransDetailWhere + "),0) as " + _g._dataPayroll.payroll_resource._advance_money + ","
                                          + " coalesce((select sum(" + _g._dataPayroll.payroll_trans_detail._insure_money + ") from " + _g._dataPayroll.payroll_trans_detail._table + " where " + _g._dataPayroll.payroll_trans_detail._trans_flag + "=1 and " + _g._dataPayroll.payroll_trans_detail._trans_type + "=5 and " + _g._dataPayroll.payroll_trans_detail._employee + " =  " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + " " + TransDetailWhere + "),0) as " + _g._dataPayroll.payroll_resource._insure_money + ","
                                          + " coalesce((select sum(" + _g._dataPayroll.payroll_trans_detail._other_payout_money + ") from " + _g._dataPayroll.payroll_trans_detail._table + " where " + _g._dataPayroll.payroll_trans_detail._trans_flag + "=1 and " + _g._dataPayroll.payroll_trans_detail._trans_type + "=5 and " + _g._dataPayroll.payroll_trans_detail._employee + " =  " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + " " + TransDetailWhere + "),0) as " + _g._dataPayroll.payroll_resource._other_payout_money + ","
                                          + " coalesce((select sum(" + _g._dataPayroll.payroll_trans_detail._money + ") from " + _g._dataPayroll.payroll_trans_detail._table + " where " + _g._dataPayroll.payroll_trans_detail._trans_flag + "=1 and " + _g._dataPayroll.payroll_trans_detail._trans_type + "=1 and " + _g._dataPayroll.payroll_trans_detail._employee + " =  " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + " " + TransDetailWhere + "),0) as " + _g._dataPayroll.payroll_trans._sow_money + ","
                                          + " coalesce((select sum(" + _g._dataPayroll.payroll_trans_detail._money + ") from " + _g._dataPayroll.payroll_trans_detail._table + " where " + _g._dataPayroll.payroll_trans_detail._trans_flag + "=1 and " + _g._dataPayroll.payroll_trans_detail._trans_type + "=2 and " + _g._dataPayroll.payroll_trans_detail._employee + " =  " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + " " + TransDetailWhere + "),0) as " + _g._dataPayroll.payroll_trans._leave_money + ","
                                          + " coalesce((select sum(" + _g._dataPayroll.payroll_trans_detail._money + ") from " + _g._dataPayroll.payroll_trans_detail._table + " where " + _g._dataPayroll.payroll_trans_detail._trans_flag + "=1 and " + _g._dataPayroll.payroll_trans_detail._trans_type + "=3 and " + _g._dataPayroll.payroll_trans_detail._employee + " =  " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + " " + TransDetailWhere + "),0) as " + _g._dataPayroll.payroll_trans._late_money + ","
                                          + " coalesce((select sum(" + _g._dataPayroll.payroll_trans_detail._money + ") from " + _g._dataPayroll.payroll_trans_detail._table + " where " + _g._dataPayroll.payroll_trans_detail._trans_flag + "=1 and " + _g._dataPayroll.payroll_trans_detail._trans_type + "=4 and " + _g._dataPayroll.payroll_trans_detail._employee + " =  " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + " " + TransDetailWhere + "),0) as " + _g._dataPayroll.payroll_resource._ot_money
                                          + " from payroll_trans "
                                          + " where employee = '" + code + "'"
                                          + " and select_month = " + month
                                          + " and select_year = " + InYear
                                          + " and date_pay_salary between '" + startDate + "' and '" + endDate + "'"
                                          + " group by employee";

                                        DataSet _dsOtherMoney = myFrameWork._query(MyLib._myGlobal._databaseName, QueryOtherMoney);

                                        //ใส่ค่าก่อนคำนวน รับ (+)
                                        _processDS.Tables[0].Rows[i][_g._dataPayroll.payroll_resource._ot_money] = _dsOtherMoney.Tables[0].Rows[0][_g._dataPayroll.payroll_resource._ot_money].ToString();
                                        _processDS.Tables[0].Rows[i][_g._dataPayroll.payroll_resource._diligent_money] = _dsOtherMoney.Tables[0].Rows[0][_g._dataPayroll.payroll_resource._diligent_money].ToString();
                                        _processDS.Tables[0].Rows[i][_g._dataPayroll.payroll_resource._prepay_money] = _dsOtherMoney.Tables[0].Rows[0][_g._dataPayroll.payroll_resource._prepay_money].ToString();
                                        _processDS.Tables[0].Rows[i][_g._dataPayroll.payroll_resource._bonus_money] = _dsOtherMoney.Tables[0].Rows[0][_g._dataPayroll.payroll_resource._bonus_money].ToString();
                                        _processDS.Tables[0].Rows[i][_g._dataPayroll.payroll_resource._other_income_money] = _dsOtherMoney.Tables[0].Rows[0][_g._dataPayroll.payroll_resource._other_income_money].ToString();

                                        ot_money = decimal.Parse(String.Format(_formatNumber, decimal.Parse(_processDS.Tables[0].Rows[i][_g._dataPayroll.payroll_resource._ot_money].ToString())));
                                        diligent_money = decimal.Parse(String.Format(_formatNumber, decimal.Parse(_processDS.Tables[0].Rows[i][_g._dataPayroll.payroll_resource._diligent_money].ToString())));
                                        prepay_money = decimal.Parse(String.Format(_formatNumber, decimal.Parse(_processDS.Tables[0].Rows[i][_g._dataPayroll.payroll_resource._prepay_money].ToString())));
                                        bonus_money = decimal.Parse(String.Format(_formatNumber, decimal.Parse(_processDS.Tables[0].Rows[i][_g._dataPayroll.payroll_resource._bonus_money].ToString())));
                                        other_income_money = decimal.Parse(String.Format(_formatNumber, decimal.Parse(_processDS.Tables[0].Rows[i][_g._dataPayroll.payroll_resource._other_income_money].ToString())));
                                        //ใส่ค่าก่อนคำนวน จ่าย (-)
                                        _processDS.Tables[0].Rows[i][_g._dataPayroll.payroll_resource._sow_money] = _dsOtherMoney.Tables[0].Rows[0][_g._dataPayroll.payroll_resource._sow_money].ToString();
                                        _processDS.Tables[0].Rows[i][_g._dataPayroll.payroll_resource._leave_money] = _dsOtherMoney.Tables[0].Rows[0][_g._dataPayroll.payroll_resource._leave_money].ToString();
                                        _processDS.Tables[0].Rows[i][_g._dataPayroll.payroll_resource._late_money] = _dsOtherMoney.Tables[0].Rows[0][_g._dataPayroll.payroll_resource._late_money].ToString();
                                        _processDS.Tables[0].Rows[i][_g._dataPayroll.payroll_resource._advance_money] = _dsOtherMoney.Tables[0].Rows[0][_g._dataPayroll.payroll_resource._advance_money].ToString();
                                        _processDS.Tables[0].Rows[i][_g._dataPayroll.payroll_resource._insure_money] = _dsOtherMoney.Tables[0].Rows[0][_g._dataPayroll.payroll_resource._insure_money].ToString();
                                        _processDS.Tables[0].Rows[i][_g._dataPayroll.payroll_resource._other_payout_money] = _dsOtherMoney.Tables[0].Rows[0][_g._dataPayroll.payroll_resource._other_payout_money].ToString();
                                        
                                        sow_money = decimal.Parse(String.Format(_formatNumber, decimal.Parse(_processDS.Tables[0].Rows[i][_g._dataPayroll.payroll_resource._sow_money].ToString())));
                                        leave_money = decimal.Parse(String.Format(_formatNumber, decimal.Parse(_processDS.Tables[0].Rows[i][_g._dataPayroll.payroll_resource._leave_money].ToString())));
                                        late_money = decimal.Parse(String.Format(_formatNumber, decimal.Parse(_processDS.Tables[0].Rows[i][_g._dataPayroll.payroll_resource._late_money].ToString())));
                                        advance_money = decimal.Parse(String.Format(_formatNumber, decimal.Parse(_processDS.Tables[0].Rows[i][_g._dataPayroll.payroll_resource._advance_money].ToString())));
                                        insure_money = decimal.Parse(String.Format(_formatNumber, decimal.Parse(_processDS.Tables[0].Rows[i][_g._dataPayroll.payroll_resource._insure_money].ToString())));
                                        other_payout_money = decimal.Parse(String.Format(_formatNumber, decimal.Parse(_processDS.Tables[0].Rows[i][_g._dataPayroll.payroll_resource._other_payout_money].ToString())));
                                        // 
                                    }
                                    catch (Exception ex)
                                    {
                                        //MessageBox.Show(" กรุณา ตรวจสอบ วันที่ทำงานในแต่ละงวด หรือ มีการบันทึก รายละเอียดการทำงาน ไม่ถูกต้อง " + error_employee_code + " " + error_employee_name);
                                    }
                                }
                                if (employeeType == 0)
                                {
                                    if (periodNumberMonth > 0)
                                    {
                                        wages_money = wages_money / periodNumberMonth;
                                        society_insure_money_in_month = (wages_money * customer_ss_rate) / 100;
                                        reserve_fund_money_in_month = (wages_money * customer_rf_rate) / 100;
                                        //คำนวนประกันสังคม ต่องวด
                                        if (society_insure_money_in_month > (social_security_max / periodNumberMonth))
                                        {
                                            society_insure_money_in_month = (social_security_max / periodNumberMonth);
                                        }
                                        else if (society_insure_money_in_month < (social_security_min / periodNumberMonth))
                                        {
                                            society_insure_money_in_month = (social_security_min / periodNumberMonth);
                                        }
                                    }
                                }
                                else if (employeeType == 1)
                                {
                                    if (periodNumberWeek > 0)
                                    {
                                        wages_money = wages_money / periodNumberWeek;
                                        society_insure_money_in_month = (wages_money * customer_ss_rate) / 100;
                                        reserve_fund_money_in_month = (wages_money * customer_rf_rate) / 100;
                                        //คำนวนประกันสังคม ต่องวด
                                        if (society_insure_money_in_month > (social_security_max / periodNumberWeek))
                                        {
                                            society_insure_money_in_month = (social_security_max / periodNumberWeek);
                                        }
                                        else if (society_insure_money_in_month < (social_security_min / periodNumberWeek))
                                        {
                                            society_insure_money_in_month = (social_security_min / periodNumberWeek);
                                        }
                                    }
                                }
                                else
                                {
                                    reserve_fund_money_in_month = decimal.Parse(_processDS.Tables[0].Rows[i][_g._dataPayroll.payroll_resource._reserve_money].ToString());
                                    //คำนวนประกันสังคม ต่องวด
                                    society_insure_money_in_month = decimal.Parse(_processDS.Tables[0].Rows[i][_g._dataPayroll.payroll_resource._society_insure_money].ToString());

                                    if (society_insure_money_in_month > (social_security_max / periodNumberDay))
                                    {
                                        society_insure_money_in_month = (social_security_max / periodNumberDay);
                                    }
                                    else if (society_insure_money_in_month < (social_security_min / periodNumberDay))
                                    {
                                        society_insure_money_in_month = (social_security_min / periodNumberDay);
                                    }

                                }
                            }
                            //เช็คว่าหัก กองทุนสำรองฯ หรือ ไม่                   
                            if (MyLib._myGlobal._intPhase(_processDS.Tables[0].Rows[i]["reserve_fund_status"].ToString()) == 0) reserve_fund_money_in_month = 0;
                            //รวม รับ (+)
                            sumincome = (wages_money + ot_money + diligent_money + bonus_money + other_income_money + prepay_money);
                            //รวม จ่าย (-)
                            sumpayout = (reserve_fund_money_in_month + advance_money + insure_money + other_payout_money + society_insure_money_in_month);
                            //รวม ขาด ลา มาสาย
                            sumsow = (sow_money + leave_money + late_money);
                            //รวมเงินได้ สุทธิ ออกรายงาน สลิปเงินเดือน และ คำนวนเงินเดือน
                            net_wages = ((wages_money + ot_money + diligent_money + bonus_money + other_income_money + prepay_money) - (sow_money + leave_money + late_money + reserve_fund_money_in_month + advance_money + insure_money + other_payout_money + society_insure_money_in_month));
                            //สำหรับ คิดภาษีงวดสุดท้ายของเดือน
                            switch (ScreenName)
                            {
                                case _g.g._screenPayrollEnum.รายละเอียดเงินเดือน_ค่าจ้างทั้งปี:                                    
                                    if (Option == 1)
                                    {
                                        if (employeeType == 0 && taxPeriodMonth == 0) checkPeriod = false;
                                        if (employeeType == 1 && taxPeriodWeek == 0) checkPeriod = false;
                                        if (employeeType == 2 && taxPeriodDay == 0) checkPeriod = false;
                                    }
                                    else
                                    {
                                        int periodNumber = MyLib._myGlobal._intPhase(_processDS.Tables[0].Rows[i]["period"].ToString());
                                        if (employeeType == 0 && taxPeriodMonth == 0 && periodNumber == periodNumberMonth) checkPeriod = true;
                                        if (employeeType == 1 && taxPeriodWeek == 0 && periodNumber == periodNumberWeek) checkPeriod = true;
                                        if (employeeType == 2 && taxPeriodDay == 0 && periodNumber == periodNumberDay) checkPeriod = true;
                                    }
                                    break;
                                case _g.g._screenPayrollEnum.สลิปเงินเดือน:
                                case _g.g._screenPayrollEnum.คำนวนเงินเดือน:
                                case _g.g._screenPayrollEnum.รายละเอียดเงินเดือน_ค่าจ้าง_ตามประเภทลูกจ้าง:                                                          
                                    if (employeeType == 0 && taxPeriodMonth == 0) checkPeriod = true;
                                    if (employeeType == 1 && taxPeriodWeek == 0) checkPeriod = true;
                                    if (employeeType == 2 && taxPeriodDay == 0) checkPeriod = true;
                                    break;
                            }
                            
                            if (checkPeriod)
                            {
                                Society_month = decimal.Parse(_processDS.Tables[0].Rows[i]["society_insure_money_in_month"].ToString());
                                reserve_fund_month = decimal.Parse(_processDS.Tables[0].Rows[i]["reserve_money_in_month"].ToString());
                                //รวมเงินได้ ไม่หัก รายจ่าย ออกภาษี
                                //string xxx1 = _processDS.Tables[0].Rows[i]["wages_money_in_month"].ToString();
                                //string xxx2 = _processDS.Tables[0].Rows[i]["ot_money_in_month"].ToString();
                                //string xxx3 = _processDS.Tables[0].Rows[i]["diligent_money_in_month"].ToString();
                                //string xxx4 = _processDS.Tables[0].Rows[i]["bonus_money_in_month"].ToString();
                                //string xxx5 = _processDS.Tables[0].Rows[i]["other_income_money_in_month"].ToString();
                                //string xxx6 = _processDS.Tables[0].Rows[i]["prepay_money_in_month"].ToString();                              
                                wages_month = (decimal.Parse(_processDS.Tables[0].Rows[i]["wages_money_in_month"].ToString()) + decimal.Parse(_processDS.Tables[0].Rows[i]["ot_money_in_month"].ToString()) + decimal.Parse(_processDS.Tables[0].Rows[i]["diligent_money_in_month"].ToString()) + decimal.Parse(_processDS.Tables[0].Rows[i]["bonus_money_in_month"].ToString()) + decimal.Parse(_processDS.Tables[0].Rows[i]["other_income_money_in_month"].ToString()) + decimal.Parse(_processDS.Tables[0].Rows[i]["prepay_money_in_month"].ToString()));
                                //เช็คหักค่าใช้จ่าย (เพิ่มใหม่ ปกติไม่มี)

                                if (decimal.Parse(_dsCosts.Tables[0].Rows[0][_g._dataPayroll.payroll_company_config._costs_sow].ToString()) == 1) sumCosts_month = sumCosts_month + decimal.Parse(_processDS.Tables[0].Rows[i]["sow_money_in_month"].ToString());
                                if (decimal.Parse(_dsCosts.Tables[0].Rows[0][_g._dataPayroll.payroll_company_config._costs_leave].ToString()) == 1) sumCosts_month = sumCosts_month + decimal.Parse(_processDS.Tables[0].Rows[i]["leave_money_in_month"].ToString());
                                if (decimal.Parse(_dsCosts.Tables[0].Rows[0][_g._dataPayroll.payroll_company_config._costs_late].ToString()) == 1) sumCosts_month = sumCosts_month + decimal.Parse(_processDS.Tables[0].Rows[i]["late_money_in_month"].ToString());
                                if (decimal.Parse(_dsCosts.Tables[0].Rows[0][_g._dataPayroll.payroll_company_config._costs_advance].ToString()) == 1) sumCosts_month = sumCosts_month + decimal.Parse(_processDS.Tables[0].Rows[i]["advance_money_in_month"].ToString());
                                if (decimal.Parse(_dsCosts.Tables[0].Rows[0][_g._dataPayroll.payroll_company_config._costs_insure].ToString()) == 1) sumCosts_month = sumCosts_month + decimal.Parse(_processDS.Tables[0].Rows[i]["insure_money_in_month"].ToString());
                                if (decimal.Parse(_dsCosts.Tables[0].Rows[0][_g._dataPayroll.payroll_company_config._costs_payout].ToString()) == 1) sumCosts_month = sumCosts_month + decimal.Parse(_processDS.Tables[0].Rows[i]["other_payout_money_in_month"].ToString());
                                //คำนวนประกันสังคม ต่อเดือน

                                if (Society_month > social_security_max)
                                {
                                    Society_month = social_security_max;
                                }
                                else if (Society_month < social_security_min)
                                {
                                    Society_month = social_security_min;
                                }
                                else
                                {
                                    Society_month = decimal.Parse(_processDS.Tables[0].Rows[i]["society_insure_money_in_month"].ToString());
                                }
                                wages_month = wages_month - sumCosts_month;
                                //คิดเงินเดือน เป็น 1 ปี
                                wages_month = wages_month * periodInMonth;
                                //คิดประกันสังคม 1 ปี
                                if ((Society_month * periodInMonth) > getReduceSocialSecurity)
                                {
                                    Society_month = getReduceSocialSecurity;
                                }
                                else
                                {
                                    Society_month = Society_month * periodInMonth;
                                }
                                //คิดกองทุนสำรองฯ 1 ปี
                                if ((reserve_fund_month * periodInMonth) > getReduceReserveFund)
                                {
                                    reserve_fund_month = getReduceReserveFund;
                                }
                                else
                                {
                                    reserve_fund_month = reserve_fund_month * periodInMonth;
                                }

                                decimal Salary_month = 0.0M;
                                decimal CheckSalary_month = (((wages_month + decimal.Parse(_processDS.Tables[0].Rows[i]["except_labour_legislation"].ToString())) * 40) / 100);
                                decimal Check40percen_month = 0.0M;
                                if (CheckSalary_month > 60000)
                                {
                                    //เกิน 60,000 ให้ได้แค่ 60,000
                                    Check40percen_month = 60000;
                                }
                                else
                                {
                                    //ไม่เกินใช้ตามจริง
                                    Check40percen_month = CheckSalary_month;
                                }

                                //หักลด ประกันสังคมกับ กองทุนสำรองฯ ก่อนคิดลดหย่อนอื่นๆ
                                //เช็คว่าหัก กองทุนสำรองฯ หรือ ไม่                   
                                if (MyLib._myGlobal._intPhase(_processDS.Tables[0].Rows[i]["reserve_fund_status"].ToString()) == 0) reserve_fund_month = 0;
                                //เงินได้ = เงินได้ต่อปี - ประกันสังคมต่อปี - กองทุนสำรองต่อปี
                                wages_month = wages_month - Society_month - reserve_fund_month;

                                //เช็คสถานะภาพก่อน 0 = โสด  1 = สมรส  2 = หย่า
                                if (getDataSituation == 0)
                                {
                                    //โสด
                                    //ค่าแรงสุทธิ = ค่าแรง - ลดหย่อน40% - ลดหย่อนส่วนตัว30,000 - ลดหย่อนบิดา/มารดา - ลดหย่อนเบี๊ยประกันบิดา/มารดา - ลดหย่อนเบี๊ยประกัน - ลดหย่อนเงินกู้ - ลดหย่อนอื่นๆ                            
                                    Salary_month = (((((wages_month - Check40percen_month) - myMoney) - (getDataReduceParents + getDataReduceInsurenceParents)) - getDataReduceInsurence) - getDataReduceLoan) - getDataReduceOther;
                                    SumChild = 0; SpouseWork = 0;
                                }
                                else if (getDataSituation == 1)
                                {
                                    //สมรส
                                    //0 มีเงินได้ 1 ไม่มีเงินได้
                                    if (getDataSpouseWork == 0)
                                    {
                                        //0 = รวมยื่น 1 = แยกยื่น
                                        if (getDataSpouseTax == 0)
                                        {
                                            //สมรส
                                            //มีเงินได้
                                            //รวมยื่น
                                            //ค่าแรงสุทธิ = ค่าแรง - ลดหย่อน40% - ลดหย่อนส่วนตัว30,000 - ลดหย่อนคู่สมรส30,000 - รวมลดหย่อนบุตร - ลดหย่อนบิดา/มารดา  - ลดหย่อนเบี๊ยประกันบิดา/มารดา - ลดหย่อนเบี๊ยประกัน - ลดหย่อนเงินกู้ - ลดหย่อนอื่นๆ                                    
                                            Salary_month = ((((((wages_month - Check40percen_month) - (myMoney + SpouseWork)) - SumChild) - (getDataReduceParents + getDataReduceInsurenceParents)) - getDataReduceInsurence) - getDataReduceLoan) - getDataReduceOther;
                                        }
                                        else
                                        {
                                            //สมรส
                                            //มีเงินได้
                                            //แยกยื่น
                                            //ค่าแรงสุทธิ = ค่าแรง - ลดหย่อน40% - ลดหย่อนส่วนตัว30,000  - รวมลดหย่อนบุตร - ลดหย่อนบิดา/มารดา - ลดหย่อนเบี๊ยประกันบิดา/มารดา  - ลดหย่อนเบี๊ยประกัน - ลดหย่อนเงินกู้ - ลดหย่อนอื่นๆ                                   
                                            Salary_month = ((((((wages_month - Check40percen_month) - myMoney) - SumChild) - (getDataReduceParents + getDataReduceInsurenceParents)) - getDataReduceInsurence) - getDataReduceLoan) - getDataReduceOther;
                                            SpouseWork = 0;
                                        }
                                    }
                                    else
                                    {
                                        //สมรส
                                        //ไม่มีเงินได้
                                        //ค่าแรงสุทธิ = ค่าแรง - ลดหย่อน40% - ลดหย่อนส่วนตัว30,000 - ลดหย่อนคู่สมรส30,000 - รวมลดหย่อนบุตร - ลดหย่อนบิดา/มารดา - ลดหย่อนเบี๊ยประกันบิดา/มารดา - ลดหย่อนเบี๊ยประกัน - ลดหย่อนเงินกู้ - ลดหย่อนอื่นๆ                               
                                        Salary_month = ((((((wages_month - Check40percen_month) - (myMoney + SpouseWork)) - SumChild) - (getDataReduceParents + getDataReduceInsurenceParents)) - getDataReduceInsurence) - getDataReduceLoan) - getDataReduceOther;
                                    }
                                }
                                else
                                {
                                    //หย่าร้าง
                                    //ค่าแรงสุทธิ = ค่าแรง - ลดหย่อน40% - ลดหย่อนส่วนตัว30,000  - รวมลดหย่อนบุตร - ลดหย่อนบิดา/มารดา  - ลดหย่อนเบี๊ยประกันบิดา/มารดา - ลดหย่อนเบี๊ยประกัน - ลดหย่อนเงินกู้ - ลดหย่อนอื่นๆ                            
                                    Salary_month = ((((((wages_month - Check40percen_month) - myMoney) - SumChild) - (getDataReduceParents + getDataReduceInsurenceParents)) - getDataReduceInsurence) - getDataReduceLoan) - getDataReduceOther;
                                    SpouseWork = 0;
                                }
                                //คิดเงินบริจาค
                                //เงินเดือน คำนวนภาษีงวดสุดท้าย เท่านั้น
                                decimal donation_month = 0.0M;
                                decimal donation_education_month = 0.0M;
                                if (Salary_month > 0)
                                {
                                    if (getDataReduceDonation > 0) if (getDataReduceDonation > ((Salary_month * 10) / 100)) donation_month = ((Salary_month * 10) / 100);
                                    if (getDataReduceDonationEducation > 0) if (getDataReduceDonationEducation > ((Salary_month * 10) / 100)) donation_education_month = ((Salary_month * 10) / 100);
                                }
                                Salary_month = Salary_month - (donation_month + donation_education_month);

                                if (decimal.Parse(_dr[0]["tax_rate"].ToString()) == 0 && Salary_month < decimal.Parse(_dr[0]["income_to"].ToString()))
                                {
                                    //รายได้หักลดหย่อนแล้วน้อยกว่า ภาษีกำหนด
                                    Remark = "ได้รับยกเว้นภาษี";
                                }
                                else
                                {
                                    for (int _row = 0; _row < _dr.Length; _row++)
                                    {
                                        decimal _income_from = decimal.Parse(_dr[_row]["income_from"].ToString());//เงินได้จาก
                                        decimal _income_to = decimal.Parse(_dr[_row]["income_to"].ToString()); //เงินได้ถึง
                                        decimal _tax_rate = decimal.Parse(_dr[_row]["tax_rate"].ToString());//เรทภาษี
                                        //โอ๋
                                        if (Salary_month > 1)
                                        {

                                            if (Salary_month > _income_to)
                                            {

                                                _sum_tax_month += (((_income_to - (_income_from - 1)) * _tax_rate) / 100);
                                                Salary_month = Salary_month - (_income_to - (_income_from - 1));
                                            }
                                            else if (Salary_month < _income_to)
                                            {
                                                _sum_tax_month += ((Salary_month * _tax_rate) / 100);
                                                Salary_month = _income_to - _income_to;
                                            }

                                        }

                                    }
                                }

                            }
                            else
                            {
                                //สำหรับ ภาษีทุกงวด
                                if (Option == 1)
                                {
                                    periodInYear = 12;
                                }
                                else
                                {
                                    //เช็คสถานะคำนวนภาษี รวมงวดต่อปี
                                    if (taxPeriodMonth == 1 && employeeType == 0)
                                    {
                                        //คำนวนทุกงวด งวดรายเดือน
                                        periodInYear = periodNumberMonth * 12;
                                    }
                                    else if (taxPeriodWeek == 1 && employeeType == 1)
                                    {
                                        //คำนวนทุกงวด งวดรายสัปดาห์
                                        periodInYear = periodNumberWeek * 12;
                                    }
                                    else if (taxPeriodDay == 1 && employeeType == 2)
                                    {
                                        //คำนวนทุกงวด งวดรายวัน
                                        periodInYear = periodNumberDay * 12;
                                    }
                                    else
                                    {
                                        //คำนวน งวดสุดท้าย (เดือนละครั้ง)
                                        periodInYear = 12;
                                    }
                                }

                                //เช็คหักค่าใช้จ่าย (เพิ่มใหม่ ปกติไม่มี) (ตามจริง)                   
                                if (decimal.Parse(_dsCosts.Tables[0].Rows[0][_g._dataPayroll.payroll_company_config._costs_sow].ToString()) == 1) sumCosts = sumCosts + decimal.Parse(_processDS.Tables[0].Rows[i][_g._dataPayroll.payroll_resource._sow_money].ToString());
                                if (decimal.Parse(_dsCosts.Tables[0].Rows[0][_g._dataPayroll.payroll_company_config._costs_leave].ToString()) == 1) sumCosts = sumCosts + decimal.Parse(_processDS.Tables[0].Rows[i][_g._dataPayroll.payroll_resource._leave_money].ToString());
                                if (decimal.Parse(_dsCosts.Tables[0].Rows[0][_g._dataPayroll.payroll_company_config._costs_late].ToString()) == 1) sumCosts = sumCosts + decimal.Parse(_processDS.Tables[0].Rows[i][_g._dataPayroll.payroll_resource._late_money].ToString());
                                if (decimal.Parse(_dsCosts.Tables[0].Rows[0][_g._dataPayroll.payroll_company_config._costs_advance].ToString()) == 1) sumCosts = sumCosts + decimal.Parse(_processDS.Tables[0].Rows[i][_g._dataPayroll.payroll_resource._advance_money].ToString());
                                if (decimal.Parse(_dsCosts.Tables[0].Rows[0][_g._dataPayroll.payroll_company_config._costs_insure].ToString()) == 1) sumCosts = sumCosts + decimal.Parse(_processDS.Tables[0].Rows[i][_g._dataPayroll.payroll_resource._insure_money].ToString());
                                if (decimal.Parse(_dsCosts.Tables[0].Rows[0][_g._dataPayroll.payroll_company_config._costs_payout].ToString()) == 1) sumCosts = sumCosts + decimal.Parse(_processDS.Tables[0].Rows[i][_g._dataPayroll.payroll_resource._other_payout_money].ToString());


                                //รวมเงินได้ ไม่หัก รายจ่าย ออกภาษี
                                wages_for_tax = ((wages_money + ot_money + diligent_money + bonus_money + other_income_money + prepay_money));
                                //ใส่รายการอื่น
                                getDataSalary = wages_for_tax - sumCosts;//รายได้ ทั้งหมด รวม OT // ถ้ามีหักค่าใช้จ่ายจะหักตอนนี้                                

                                //คิดเงินเดือน เป็น 1 ปี                           
                                getDataSalary = getDataSalary * periodInYear;
                                //คิดประกันสังคม 1 ปี
                                if ((society_insure_money_in_month * periodInYear) > getReduceSocialSecurity)
                                {
                                    society_insure_money = getReduceSocialSecurity;
                                }
                                else
                                {
                                    society_insure_money = society_insure_money_in_month * periodInYear;
                                }
                                //คิดกองทุนสำรองฯ 1 ปี
                                if ((reserve_fund_money_in_month * periodInYear) > getReduceReserveFund)
                                {
                                    reserve_fund_money = getReduceReserveFund;
                                }
                                else
                                {
                                    reserve_fund_money = reserve_fund_money_in_month * periodInYear;
                                }

                                //คำนวนภาษีทุกงวด ปกติ
                                mySalary = getDataSalary + decimal.Parse(_processDS.Tables[0].Rows[i]["except_labour_legislation"].ToString()); //เงินเดือนหลัง คูณ ด้วย การคิด ภาษี ทั้งปี
                                CheckSalary = ((mySalary * 40) / 100);// เช็ค ลดหย่อนส่วนตัว 40%
                                Check40percen = 0;// จำนวนเงินลดหย่อนส่วนตัว 40%                        
                                //เช็คค่าใช้จ่ายส่วนตัวว่าเกิน 60,000 รึไม่
                                if (CheckSalary > 60000)
                                {
                                    //เกิน 60,000 ให้ได้แค่ 60,000
                                    Check40percen = 60000;
                                }
                                else
                                {
                                    //ไม่เกินใช้ตามจริง
                                    Check40percen = CheckSalary;
                                }
                                //หักลด ประกันสังคมกับ กองทุนสำรองฯ ก่อนคิดลดหย่อนอื่นๆ
                                //เช็คว่าหัก กองทุนสำรองฯ หรือ ไม่                   
                                if (MyLib._myGlobal._intPhase(_processDS.Tables[0].Rows[i]["reserve_fund_status"].ToString()) == 0) reserve_fund_money = 0;
                                //เงินได้ = เงินได้ต่อปี - ประกันสังคมต่อปี - กองทุนสำรองต่อปี
                                mySalary = mySalary - society_insure_money - reserve_fund_money;
                                //เช็คสถานะภาพก่อน 0 = โสด  1 = สมรส  2 = หย่า
                                if (getDataSituation == 0)
                                {
                                    //โสด
                                    //ค่าแรงสุทธิ = ค่าแรง - ลดหย่อน40% - ลดหย่อนส่วนตัว30,000 - ลดหย่อนบิดา/มารดา - ลดหย่อนเบี๊ยประกันบิดา/มารดา - ลดหย่อนเบี๊ยประกัน - ลดหย่อนเงินกู้ - ลดหย่อนอื่นๆ
                                    Salary = (((((mySalary - Check40percen) - myMoney) - (getDataReduceParents + getDataReduceInsurenceParents)) - getDataReduceInsurence) - getDataReduceLoan) - getDataReduceOther;
                                    SumChild = 0; SpouseWork = 0;
                                }
                                else if (getDataSituation == 1)
                                {
                                    //สมรส
                                    //0 มีเงินได้ 1 ไม่มีเงินได้
                                    if (getDataSpouseWork == 0)
                                    {
                                        //0 = รวมยื่น 1 = แยกยื่น
                                        if (getDataSpouseTax == 0)
                                        {
                                            //สมรส
                                            //มีเงินได้
                                            //รวมยื่น
                                            //ค่าแรงสุทธิ = ค่าแรง - ลดหย่อน40% - ลดหย่อนส่วนตัว30,000 - ลดหย่อนคู่สมรส30,000 - รวมลดหย่อนบุตร - ลดหย่อนบิดา/มารดา  - ลดหย่อนเบี๊ยประกันบิดา/มารดา - ลดหย่อนเบี๊ยประกัน - ลดหย่อนเงินกู้ - ลดหย่อนอื่นๆ
                                            Salary = ((((((mySalary - Check40percen) - (myMoney + SpouseWork)) - SumChild) - (getDataReduceParents + getDataReduceInsurenceParents)) - getDataReduceInsurence) - getDataReduceLoan) - getDataReduceOther;
                                        }
                                        else
                                        {
                                            //สมรส
                                            //มีเงินได้
                                            //แยกยื่น
                                            //ค่าแรงสุทธิ = ค่าแรง - ลดหย่อน40% - ลดหย่อนส่วนตัว30,000  - รวมลดหย่อนบุตร - ลดหย่อนบิดา/มารดา - ลดหย่อนเบี๊ยประกันบิดา/มารดา  - ลดหย่อนเบี๊ยประกัน - ลดหย่อนเงินกู้ - ลดหย่อนอื่นๆ
                                            Salary = ((((((mySalary - Check40percen) - myMoney) - SumChild) - (getDataReduceParents + getDataReduceInsurenceParents)) - getDataReduceInsurence) - getDataReduceLoan) - getDataReduceOther;
                                            SpouseWork = 0;
                                        }
                                    }
                                    else
                                    {
                                        //สมรส
                                        //ไม่มีเงินได้
                                        //ค่าแรงสุทธิ = ค่าแรง - ลดหย่อน40% - ลดหย่อนส่วนตัว30,000 - ลดหย่อนคู่สมรส30,000 - รวมลดหย่อนบุตร - ลดหย่อนบิดา/มารดา - ลดหย่อนเบี๊ยประกันบิดา/มารดา - ลดหย่อนเบี๊ยประกัน - ลดหย่อนเงินกู้ - ลดหย่อนอื่นๆ
                                        Salary = ((((((mySalary - Check40percen) - (myMoney + SpouseWork)) - SumChild) - (getDataReduceParents + getDataReduceInsurenceParents)) - getDataReduceInsurence) - getDataReduceLoan) - getDataReduceOther;
                                    }
                                }
                                else
                                {
                                    //หย่าร้าง
                                    //ค่าแรงสุทธิ = ค่าแรง - ลดหย่อน40% - ลดหย่อนส่วนตัว30,000  - รวมลดหย่อนบุตร - ลดหย่อนบิดา/มารดา  - ลดหย่อนเบี๊ยประกันบิดา/มารดา - ลดหย่อนเบี๊ยประกัน - ลดหย่อนเงินกู้ - ลดหย่อนอื่นๆ
                                    Salary = ((((((mySalary - Check40percen) - myMoney) - SumChild) - (getDataReduceParents + getDataReduceInsurenceParents)) - getDataReduceInsurence) - getDataReduceLoan) - getDataReduceOther;
                                    SpouseWork = 0;
                                }
                                //คิดเงินบริจาค
                                if (Salary > 0)
                                {
                                    if (getDataReduceDonation > 0) if (getDataReduceDonation > ((Salary * 10) / 100)) getDataReduceDonation = ((Salary * 10) / 100);
                                    if (getDataReduceDonationEducation > 0) if (getDataReduceDonationEducation > ((Salary * 10) / 100)) getDataReduceDonationEducation = ((Salary * 10) / 100);
                                }
                                Salary = Salary - (getDataReduceDonation + getDataReduceDonationEducation);
                                //เริ่มคำนวน เช็ค rate Tax ตามอัตรา ก้าวหน้า
                                _saraly = Salary;
                                _sum = 0;
                                if (decimal.Parse(_dr[0]["tax_rate"].ToString()) == 0 && Salary < decimal.Parse(_dr[0]["income_to"].ToString()))
                                {
                                    //รายได้หักลดหย่อนแล้วน้อยกว่า ภาษีกำหนด
                                    Remark = "ได้รับยกเว้นภาษี";
                                }
                                else
                                {
                                    for (int _row = 0; _row < _dr.Length; _row++)
                                    {
                                        decimal _income_from = decimal.Parse(_dr[_row]["income_from"].ToString());//เงินได้จาก
                                        decimal _income_to = decimal.Parse(_dr[_row]["income_to"].ToString()); //เงินได้ถึง
                                        decimal _tax_rate = decimal.Parse(_dr[_row]["tax_rate"].ToString());//เรทภาษี
                                        //โอ๋
                                        if (_saraly > 1)
                                        {

                                            if (_saraly > _income_to)
                                            {

                                                _sum += (((_income_to - (_income_from - 1)) * _tax_rate) / 100);
                                                _saraly = _saraly - (_income_to - (_income_from - 1));
                                            }
                                            else if (_saraly < _income_to)
                                            {
                                                _sum += ((_saraly * _tax_rate) / 100);
                                                _saraly = _income_to - _income_to;
                                            }

                                        }

                                    }
                                }
                            }
                            break;
                    }

                    //Modify a Row
                    switch (ScreenName)
                    {                                               
                        case _g.g._screenPayrollEnum.คำนวนเงินเดือน:
                        case _g.g._screenPayrollEnum.รายละเอียดเงินเดือน_ค่าจ้าง_ตามประเภทลูกจ้าง:
                        case _g.g._screenPayrollEnum.สลิปเงินเดือน:
                            if (Calculate == true)
                            {
                                if (checkPeriod)
                                {
                                    if (_sum_tax_month > 0)
                                    {
                                        _processDS.Tables[0].Rows[i][_g._dataPayroll.payroll_resource._tax_income] = (_sum_tax_month / periodInMonth).ToString();
                                        _sum_tax_month = (_sum_tax_month / periodInMonth);
                                    }
                                }
                                else
                                {
                                    if (_sum > 0)
                                    {
                                        _processDS.Tables[0].Rows[i][_g._dataPayroll.payroll_resource._tax_income] = (_sum / periodInYear).ToString();
                                        _sum_tax_month = (_sum / periodInYear);
                                    }

                                }
                            }
                            else
                            {
                                _sum_tax_month = 0;
                            }
                            _processDS.Tables[0].Rows[i][_g._dataPayroll.payroll_resource._net_wages] = (net_wages - _sum_tax_month).ToString();
                            _processDS.Tables[0].Rows[i][_g._dataPayroll.payroll_resource._wages_money] = wages_money.ToString();
                            switch (ScreenName)
                            {
                                case _g.g._screenPayrollEnum.สลิปเงินเดือน:
                                    _processDS.Tables[0].Rows[i][_g._dataPayroll.payroll_resource._sow_money] = sumsow.ToString();                                    
                                    break;
                            }
                            _processDS.Tables[0].Rows[i]["sumincome"] = sumincome.ToString();
                            _processDS.Tables[0].Rows[i]["sumpayout"] = (sumpayout + _sum_tax_month).ToString();
                            _processDS.Tables[0].Rows[i][_g._dataPayroll.payroll_resource._society_insure_money] = society_insure_money_in_month.ToString();
                            _processDS.Tables[0].Rows[i][_g._dataPayroll.payroll_resource._reserve_money] = reserve_fund_money_in_month.ToString();                            
                            break;
                        case _g.g._screenPayrollEnum.รายละเอียดเงินเดือน_ค่าจ้างทั้งปี:
                            if (Calculate == true)
                            {
                                if (checkPeriod)
                                {
                                    if (_sum_tax_month > 0)
                                    {
                                        _processDS.Tables[0].Rows[i][_g._dataPayroll.payroll_resource._tax_income] = (_sum_tax_month / periodInMonth).ToString();
                                        _sum_tax_month = (_sum_tax_month / periodInMonth);
                                    }
                                }
                                else
                                {
                                    if (_sum > 0)
                                    {
                                        _processDS.Tables[0].Rows[i][_g._dataPayroll.payroll_resource._tax_income] = (_sum / periodInYear).ToString();
                                        _sum_tax_month = (_sum / periodInYear);
                                    }

                                }
                            }
                            else
                            {
                                _sum_tax_month = 0;
                            }                            
                            _processDS.Tables[0].Rows[i][_g._dataPayroll.payroll_resource._wages_money] = wages_money.ToString();
                            _processDS.Tables[0].Rows[i][_g._dataPayroll.payroll_resource._society_insure_money] = society_insure_money_in_month.ToString();
                            _processDS.Tables[0].Rows[i][_g._dataPayroll.payroll_resource._reserve_money] = reserve_fund_money_in_month.ToString(); 
                            break;
                        case _g.g._screenPayrollEnum.ภงด_1:
                        case _g.g._screenPayrollEnum.ภงด_1_ใบแนบ:
                            if (_sum > 0)
                            {
                                _processDS.Tables[0].Rows[i][_g._dataPayroll.payroll_resource._tax_income] = (_sum / 12).ToString();
                                _sum = (_sum / 12);
                            }
                            _processDS.Tables[0].Rows[i][_g._dataPayroll.payroll_resource._net_wages] = (wages_for_tax).ToString();
                            break;
                        case _g.g._screenPayrollEnum.รายงานภาษีเงินได้_รายเดือน:
                        case _g.g._screenPayrollEnum.คำนวนภาษีรายเดือน:
                            _processDS.Tables[0].Rows[i][_g._dataPayroll.payroll_resource._net_wages] = (getDataSalary + society_insure_money + reserve_fund_money).ToString();
                            _processDS.Tables[0].Rows[i][_g._dataPayroll.payroll_resource._reduce_40] = Check40percen.ToString();
                            _processDS.Tables[0].Rows[i][_g._dataPayroll.payroll_resource._reduce_myself] = (myMoney).ToString();
                            _processDS.Tables[0].Rows[i][_g._dataPayroll.payroll_resource._reduce_spouse] = (SpouseWork).ToString();
                            _processDS.Tables[0].Rows[i][_g._dataPayroll.payroll_resource._reduce_child] = SumChild.ToString();
                            _processDS.Tables[0].Rows[i][_g._dataPayroll.payroll_resource._reduce_other] = (getDataReduceOther + getDataReduceDonation + getDataReduceDonationEducation).ToString();
                            _processDS.Tables[0].Rows[i][_g._dataPayroll.payroll_resource._society_insure_money] = society_insure_money.ToString();
                            _processDS.Tables[0].Rows[i][_g._dataPayroll.payroll_resource._reserve_money] = reserve_fund_money.ToString();
                            if (_sum > 0)
                            {
                                _processDS.Tables[0].Rows[i][_g._dataPayroll.payroll_resource._tax_income] = (_sum / 12).ToString();
                            }
                            _processDS.Tables[0].Rows[i][_g._dataPayroll.payroll_resource._remark] = Remark;
                            break;
                        case _g.g._screenPayrollEnum.ภงด_91:
                        case _g.g._screenPayrollEnum.ภงด_1_ก_ใบแนบ:
                        case _g.g._screenPayrollEnum.ภงด_1_ก:
                        case _g.g._screenPayrollEnum.รายงานภาษีเงินได้_รายปี:
                        case _g.g._screenPayrollEnum.คำนวนภาษีรายปี:
                            _processDS.Tables[0].Rows[i][_g._dataPayroll.payroll_resource._net_wages] = getDataSalary.ToString();
                            _processDS.Tables[0].Rows[i][_g._dataPayroll.payroll_resource._reduce_40] = Check40percen.ToString();
                            _processDS.Tables[0].Rows[i][_g._dataPayroll.payroll_resource._reduce_myself] = (myMoney).ToString();
                            _processDS.Tables[0].Rows[i][_g._dataPayroll.payroll_resource._reduce_spouse] = (SpouseWork).ToString();
                            _processDS.Tables[0].Rows[i][_g._dataPayroll.payroll_resource._reduce_child] = SumChild.ToString();
                            _processDS.Tables[0].Rows[i][_g._dataPayroll.payroll_resource._reduce_other] = (getDataReduceOther + getDataReduceDonation + getDataReduceDonationEducation).ToString();
                            _processDS.Tables[0].Rows[i][_g._dataPayroll.payroll_resource._tax_income] = _sum.ToString();
                            _processDS.Tables[0].Rows[i][_g._dataPayroll.payroll_resource._society_insure_money] = society_insure_money.ToString();
                            _processDS.Tables[0].Rows[i][_g._dataPayroll.payroll_resource._reserve_money] = reserve_fund_money.ToString();
                            _processDS.Tables[0].Rows[i][_g._dataPayroll.payroll_resource._remark] = Remark;
                            break;
                    }
                    //end 
                }
                return _processDS;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(" กรุณา ตรวจสอบ วันที่ทำงานในแต่ละงวด หรือ มีการบันทึก รายละเอียดการทำงาน ไม่ถูกต้อง " + error_employee_code + " " + error_employee_name);                
            }
            return _processDS;
        }

        /// <summary>
        /// คำนวนวันขาดงานลางาน
        /// </summary>
        /// <param name="dateStart"></param>
        /// <param name="dateEnd"></param>
        /// <returns></returns>
        public decimal _processWorkingDay(DateTime dateStart, DateTime dateEnd)
        {
            if (dateEnd > dateStart)
            {
                TimeSpan sumDay = dateEnd.Date - dateStart.Date;
                decimal workingDay = sumDay.Days + 1;
                return workingDay;
            }
            return 0;
        }
        /// <summary>
        /// ใส่เดือนใน Text
        /// </summary>
        /// <param name="getMonth"></param>
        /// <returns></returns>
        public string _processGetMonth(string getMonth)
        {
            string __month = "";
            if (getMonth.Equals("0")) { __month = "มกราคม"; }
            else if (getMonth.Equals("1")) { __month = "กุมภาพันธ์"; }
            else if (getMonth.Equals("2")) { __month = "มีนาคม"; }
            else if (getMonth.Equals("3")) { __month = "เมษายน"; }
            else if (getMonth.Equals("4")) { __month = "พฤษภาคม"; }
            else if (getMonth.Equals("5")) { __month = "มิถุนายน"; }
            else if (getMonth.Equals("6")) { __month = "กรกฎาคม"; }
            else if (getMonth.Equals("7")) { __month = "สิงหาคม"; }
            else if (getMonth.Equals("8")) { __month = "กันยายน"; }
            else if (getMonth.Equals("9")) { __month = "ตุลาคม"; }
            else if (getMonth.Equals("10")) { __month = "พฤศจิกายน"; }
            else if (getMonth.Equals("11")) { __month = "ธันวาคม"; }
            else { __month = ""; }
            return __month;
        }        
        /// <summary>
        /// กำหนดวันที่เริ่มต้น ของ แต่ละเดือน
        /// </summary>
        /// <param name="getMonth"></param>
        /// <param name="getYear"></param>
        /// <returns></returns>
        public string _processStartDate(string getDate,string getMonth, string getYear)
        {
            string _startDate = "";
            Calendar myCal = CultureInfo.InvariantCulture.Calendar;
            if (!getYear.Equals(""))
            {
                decimal dayinmonth = myCal.GetDaysInMonth((MyLib._myGlobal._intPhase(getYear) - 543), MyLib._myGlobal._intPhase(getMonth) + 1);
                _startDate = getDate + "/" + (MyLib._myGlobal._intPhase(getMonth) + 1).ToString() + "/" + getYear;
                return _startDate;
            }
            else
            {
                getYear = MyLib._myGlobal._year_current.ToString();
                decimal dayinmonth = myCal.GetDaysInMonth((MyLib._myGlobal._intPhase(getYear) - 543), MyLib._myGlobal._intPhase(getMonth) + 1);
                _startDate = getDate + "/" + (MyLib._myGlobal._intPhase(getMonth) + 1).ToString() + "/" + getYear;
                return _startDate;
            }
        }
        /// <summary>
        /// กำหนดสิ้นสุดของ แต่ละเดือน
        /// </summary>
        /// <param name="getDate"></param>
        /// <param name="getMonth"></param>
        /// <param name="getYear"></param>
        /// <param name="lastPeriod"></param>
        /// <returns></returns>
        public string _processEndDate(string getDate, string getMonth, string getYear,bool lastPeriod)
        {
            string _endDate = "";
            Calendar myCal = CultureInfo.InvariantCulture.Calendar;
            if (!getYear.Equals(""))
            {
                decimal dayinmonth = decimal.Parse(getDate);
                if (lastPeriod == true) dayinmonth = myCal.GetDaysInMonth((MyLib._myGlobal._intPhase(getYear) - 543), MyLib._myGlobal._intPhase(getMonth) + 1);
                _endDate = dayinmonth.ToString() + "/" + (MyLib._myGlobal._intPhase(getMonth) + 1).ToString() + "/" + getYear;
                return _endDate;
            }
            else
            {
                getYear = MyLib._myGlobal._year_current.ToString();
                decimal dayinmonth = decimal.Parse(getDate);
                if (lastPeriod == true) dayinmonth = myCal.GetDaysInMonth((MyLib._myGlobal._intPhase(getYear) - 543), MyLib._myGlobal._intPhase(getMonth) + 1);
                _endDate = dayinmonth.ToString() + "/" + (MyLib._myGlobal._intPhase(getMonth) + 1).ToString() + "/" + getYear;
                return _endDate;
            }
        }
        /// <summary>
        /// ตัดเอา code ออกจาก name
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static String _splitWord(String source)
        {
            try
            {
                if ((source != null) || (source.Length > 0))
                {
                    String __result = source.Trim().Split('~')[0].ToString();
                    return (__result.Trim());
                }
                return "";
            }
            catch
            {
                return "";
            }
        }
        /// <summary>
        /// เปลี่ยน จำนวนเงินตัวเลข ให้เป็นตัวหนังสือ
        /// </summary>
        /// <param name="_str"></param>
        /// <returns></returns>
        public string _getDecimalValue(string _str)
        {
            string _formatNumber = "{0:0.00}";
            string[] _baht = { "", "สิบ", "ร้อย", "พัน", "หมื่น", "แสน", "ล้าน" };
            string[] _num = { "", "หนื่ง", "สอง", "สาม", "สี่", "ห้า", "หก", "เจ็ด", "แปด", "เก้า", "สิบ", "เอ็ด", "ยี่" };
            string[] _satang = { "บาท", "สตางค์" };
            string result = "";
            try
            {
                if (!_str.Equals(""))
                {
                    _str = string.Format(_formatNumber, decimal.Parse(_str));
                    string[] _strsplit = _str.Split('.');
                    for (int xloop = 0; xloop < 2; xloop++)
                    {
                        int _loop = 0;
                        int _si = _strsplit[xloop].Length;
                        string _res = "", reb = "";
                        while (_loop < _si)
                        {
                            string _sp = _strsplit[xloop].Substring(_loop, 1);
                            int i = (_si - _loop) - 1;
                            if ((MyLib._myGlobal._intPhase(_sp) == 1) && (i == 0)) _res += _num[11];
                            if ((MyLib._myGlobal._intPhase(_sp) == 1) && (i == 1)) _res += _num[0];
                            if ((MyLib._myGlobal._intPhase(_sp) == 1) && ((i > 1) && (i != 7))) _res += _num[MyLib._myGlobal._intPhase(_sp)];
                            if ((MyLib._myGlobal._intPhase(_sp) == 2) && (i == 1)) _res += _num[12];
                            if ((MyLib._myGlobal._intPhase(_sp) == 2) && (i != 1)) _res += _num[MyLib._myGlobal._intPhase(_sp)];
                            if ((MyLib._myGlobal._intPhase(_sp) > 2)) _res += _num[MyLib._myGlobal._intPhase(_sp)];
                            if (MyLib._myGlobal._intPhase(_sp) > 0) _res += _baht[(i > 6) ? (i - 6) : i];
                            _loop++;
                        }
                        result += _res + _satang[xloop];
                    }
                }
            }
            catch (Exception ex)
            {
                string xx = ex.Message;
            }
            return result;
        }
        /// <summary>
        /// ข้อมูลบริษัท
        /// </summary>
        /// <returns></returns>
        public DataSet _getCompanyValue()
        {
            DataSet companyDS = new DataSet();
            string _query = "select "
                + _g._dataPayroll.payroll_company_config._company_name_th + ","                
                + _g._dataPayroll.payroll_company_config._tax_no + ","                
                + _g._dataPayroll.payroll_company_config._social_security_no + ","
                + _g._dataPayroll.payroll_company_config._social_security_rate + ","
                + _g._dataPayroll.payroll_company_config._customer_ss_rate + ","
                + _g._dataPayroll.payroll_company_config._employer_ss_rate + ","
                + _g._dataPayroll.payroll_company_config._reserve_fund_no + ","
                + _g._dataPayroll.payroll_company_config._reserve_fund_rate + ","
                + _g._dataPayroll.payroll_company_config._customer_rf_rate + ","
                + _g._dataPayroll.payroll_company_config._employer_rf_rate + ","
                + _g._dataPayroll.payroll_company_config._address_th + ","
                + _g._dataPayroll.payroll_company_config._house + ","
                + _g._dataPayroll.payroll_company_config._room_no + ","
                + _g._dataPayroll.payroll_company_config._floor_no + ","
                + _g._dataPayroll.payroll_company_config._village + ","
                + _g._dataPayroll.payroll_company_config._house_no + ","
                + _g._dataPayroll.payroll_company_config._crowd_no + ","
                + _g._dataPayroll.payroll_company_config._lane + ","
                + _g._dataPayroll.payroll_company_config._road + ","
                + _g._dataPayroll.payroll_company_config._locality + ","
                + _g._dataPayroll.payroll_company_config._amphur + ","
                + _g._dataPayroll.payroll_company_config._province + ","
                + _g._dataPayroll.payroll_company_config._postcode + ","
                + _g._dataPayroll.payroll_company_config._telephone
                + "  from  "
                + _g._dataPayroll.payroll_company_config._table
                + " order by "
                + _g._dataPayroll.payroll_company_config._company_name_th + " asc";
            companyDS = myFrameWork._query(MyLib._myGlobal._databaseName, _query);

            return companyDS;
        }
        public DataSet _getConfig()
        {
            DataSet Config = new DataSet();
            string _query = "select coalesce(" + _g._dataPayroll.payroll_company_config._tax_period_month + ",0) as " + _g._dataPayroll.payroll_company_config._tax_period_month + ","
                + "coalesce(" + _g._dataPayroll.payroll_company_config._tax_period_week + ",0) as " + _g._dataPayroll.payroll_company_config._tax_period_week + ","
                + "coalesce(" + _g._dataPayroll.payroll_company_config._tax_period_day + ",0) as " + _g._dataPayroll.payroll_company_config._tax_period_day + ","

                + "coalesce(" + _g._dataPayroll.payroll_company_config._period_number_month + ",0) as " + _g._dataPayroll.payroll_company_config._period_number_month + ","
                + "coalesce(" + _g._dataPayroll.payroll_company_config._period_number_week + ",0) as " + _g._dataPayroll.payroll_company_config._period_number_week + ","
                + "coalesce(" + _g._dataPayroll.payroll_company_config._period_number_day + ",0) as " + _g._dataPayroll.payroll_company_config._period_number_day + ","

                + "coalesce(" + _g._dataPayroll.payroll_company_config._start_one_month + ",0) as " + _g._dataPayroll.payroll_company_config._start_one_month + ","
                + "coalesce(" + _g._dataPayroll.payroll_company_config._end_one_month + ",0) as " + _g._dataPayroll.payroll_company_config._end_one_month + ","
                + "coalesce(" + _g._dataPayroll.payroll_company_config._start_two_month + ",0) as " + _g._dataPayroll.payroll_company_config._start_two_month + ","
                + "coalesce(" + _g._dataPayroll.payroll_company_config._end_two_month + ",0) as " + _g._dataPayroll.payroll_company_config._end_two_month + ","
                + "coalesce(" + _g._dataPayroll.payroll_company_config._start_three_month + ",0) as " + _g._dataPayroll.payroll_company_config._start_three_month + ","
                + "coalesce(" + _g._dataPayroll.payroll_company_config._end_three_month + ",0) as " + _g._dataPayroll.payroll_company_config._end_three_month + ","
                + "coalesce(" + _g._dataPayroll.payroll_company_config._start_four_month + ",0) as " + _g._dataPayroll.payroll_company_config._start_four_month + ","
                + "coalesce(" + _g._dataPayroll.payroll_company_config._end_four_month + ",0) as " + _g._dataPayroll.payroll_company_config._end_four_month + ","

                 + "coalesce(" + _g._dataPayroll.payroll_company_config._start_one_week + ",0) as " + _g._dataPayroll.payroll_company_config._start_one_week + ","
                + "coalesce(" + _g._dataPayroll.payroll_company_config._end_one_week + ",0) as " + _g._dataPayroll.payroll_company_config._end_one_week + ","
                + "coalesce(" + _g._dataPayroll.payroll_company_config._start_two_week + ",0) as " + _g._dataPayroll.payroll_company_config._start_two_week + ","
                + "coalesce(" + _g._dataPayroll.payroll_company_config._end_two_week + ",0) as " + _g._dataPayroll.payroll_company_config._end_two_week + ","
                + "coalesce(" + _g._dataPayroll.payroll_company_config._start_three_week + ",0) as " + _g._dataPayroll.payroll_company_config._start_three_week + ","
                + "coalesce(" + _g._dataPayroll.payroll_company_config._end_three_week + ",0) as " + _g._dataPayroll.payroll_company_config._end_three_week + ","
                + "coalesce(" + _g._dataPayroll.payroll_company_config._start_four_week + ",0) as " + _g._dataPayroll.payroll_company_config._start_four_week + ","
                + "coalesce(" + _g._dataPayroll.payroll_company_config._end_four_week + ",0) as " + _g._dataPayroll.payroll_company_config._end_four_week + ","

                 + "coalesce(" + _g._dataPayroll.payroll_company_config._start_one_day + ",0) as " + _g._dataPayroll.payroll_company_config._start_one_day + ","
                + "coalesce(" + _g._dataPayroll.payroll_company_config._end_one_day + ",0) as " + _g._dataPayroll.payroll_company_config._end_one_day + ","
                + "coalesce(" + _g._dataPayroll.payroll_company_config._start_two_day + ",0) as " + _g._dataPayroll.payroll_company_config._start_two_day + ","
                + "coalesce(" + _g._dataPayroll.payroll_company_config._end_two_day + ",0) as " + _g._dataPayroll.payroll_company_config._end_two_day + ","
                + "coalesce(" + _g._dataPayroll.payroll_company_config._start_three_day + ",0) as " + _g._dataPayroll.payroll_company_config._start_three_day + ","
                + "coalesce(" + _g._dataPayroll.payroll_company_config._end_three_day + ",0) as " + _g._dataPayroll.payroll_company_config._end_three_day + ","
                + "coalesce(" + _g._dataPayroll.payroll_company_config._start_four_day + ",0) as " + _g._dataPayroll.payroll_company_config._start_four_day + ","
                + "coalesce(" + _g._dataPayroll.payroll_company_config._end_four_day + ",0) as " + _g._dataPayroll.payroll_company_config._end_four_day + ","

                + "coalesce(" + _g._dataPayroll.payroll_company_config._calculate_salary_month + ",0) as " + _g._dataPayroll.payroll_company_config._calculate_salary_month + ","
                + "coalesce(" + _g._dataPayroll.payroll_company_config._calculate_salary_week + ",0) as " + _g._dataPayroll.payroll_company_config._calculate_salary_week + ","
                + "1 as " + _g._dataPayroll.payroll_company_config._calculate_salary_day
                //+ "coalesce(" + _g._dataPayroll.payroll_company_config._calculate_salary_day + ",0) as " + _g._dataPayroll.payroll_company_config._calculate_salary_day

                + " from " + _g._dataPayroll.payroll_company_config._table;
            Config = myFrameWork._query(MyLib._myGlobal._databaseName, _query);
            return Config;
        }
        /// <summary>
        /// รายงานเกี่ยวกับพนักงาน
        /// </summary>
        /// <param name="FromDate"></param>
        /// <param name="ToDate"></param>
        /// <param name="FromEmployee"></param>
        /// <param name="ToEmployee"></param>
        /// <param name="EmployeeType"></param>
        /// <param name="FromSalary"></param>
        /// <param name="ToSalary"></param>
        /// <param name="ScreenName"></param>
        /// <returns></returns>
        public DataSet _processReportEmployee(string FromDate, string ToDate, string FromEmployee, string ToEmployee, string EmployeeType, decimal FromSalary, decimal ToSalary, _g.g._screenPayrollEnum ScreenName)
        {
            DataSet _processDS = new DataSet();
            string WhereEmployee = "";
            if (!FromEmployee.Equals("") && !ToEmployee.Equals(""))
            {
                WhereEmployee += " where (" + _g._dataPayroll.payroll_employee._code + " between '" + FromEmployee + "' and '" + ToEmployee + "')";
            }
            else
            {
                if (!FromEmployee.Equals(""))
                {
                    WhereEmployee += " where (" + _g._dataPayroll.payroll_employee._code + " >= '" + FromEmployee + "')";
                }
                if (!ToEmployee.Equals(""))
                {
                    WhereEmployee += " where (" + _g._dataPayroll.payroll_employee._code + " =< '" + ToEmployee + "')";
                }
            }
            if (!FromDate.Equals("") && !ToDate.Equals(""))
            {
                if (!WhereEmployee.Equals(""))
                {
                    WhereEmployee += " and (" + _g._dataPayroll.payroll_employee._start_work + " between " + FromDate + " and " + ToDate + ")";
                }
                else
                {
                    WhereEmployee += " where (" + _g._dataPayroll.payroll_employee._start_work + " between " + FromDate + " and " + ToDate + ")";
                }
            }
            else
            {
                if (!FromDate.Equals(""))
                {
                    if (!WhereEmployee.Equals(""))
                    {
                        WhereEmployee += " and (" + _g._dataPayroll.payroll_employee._start_work + " >= " + FromDate + ")";
                    }
                    else
                    {
                        WhereEmployee += " where (" + _g._dataPayroll.payroll_employee._start_work + " >= " + FromDate + ")";
                    }

                }
                if (!ToDate.Equals(""))
                {
                    if (!WhereEmployee.Equals(""))
                    {
                        WhereEmployee += " and (" + _g._dataPayroll.payroll_employee._start_work + " =< " + ToDate + ")";
                    }
                    else
                    {
                        WhereEmployee += " where (" + _g._dataPayroll.payroll_employee._start_work + " =< " + ToDate + ")";
                    }
                }
            }
            if (!EmployeeType.Equals(""))
            {
                if (!WhereEmployee.Equals(""))
                {
                    WhereEmployee += " and (" + _g._dataPayroll.payroll_employee._employee_type + " = " + EmployeeType + ")";
                }
                else
                {
                    WhereEmployee += " where (" + _g._dataPayroll.payroll_employee._employee_type + " = " + EmployeeType + ")";
                }               
            }
            if (FromSalary > 0 && ToSalary > 0)
            {
                if (!WhereEmployee.Equals(""))
                {
                    WhereEmployee += " and (" + _g._dataPayroll.payroll_employee._salary + " between " + FromSalary + " and " + ToSalary + ")";
                }
                else
                {
                    WhereEmployee += " where (" + _g._dataPayroll.payroll_employee._salary + " between " + FromSalary + " and " + ToSalary + ")";
                }
            }
            else
            {
                if (FromSalary > 0)
                {
                    if (!WhereEmployee.Equals(""))
                    {
                        WhereEmployee += " and (" + _g._dataPayroll.payroll_employee._salary + " >= " + FromSalary + ")";
                    }
                    else
                    {
                        WhereEmployee += " where (" + _g._dataPayroll.payroll_employee._salary + " >= " + FromSalary + ")";
                    }
                }
                if (ToSalary > 0)
                {
                    if (!WhereEmployee.Equals(""))
                    {
                        WhereEmployee += " and (" + _g._dataPayroll.payroll_employee._salary + " =< " + ToSalary + ")";
                    }
                    else
                    {
                        WhereEmployee += " where (" + _g._dataPayroll.payroll_employee._salary + " =< " + ToSalary + ")";
                    }
                }
            }


            string _query = "select "
                    + _g._dataPayroll.payroll_employee._code + ","
                    + _g._dataPayroll.payroll_employee._name + ","
                    + "(select "
                    + _g._dataPayroll.payroll_side_list._name
                    + " from "
                    + _g._dataPayroll.payroll_side_list._table
                    + " where "
                    + _g._dataPayroll.payroll_side_list._code
                    + " = " + _g._dataPayroll.payroll_employee._table
                    + "."
                    + _g._dataPayroll.payroll_employee._side_code + ") as "
                    + _g._dataPayroll.payroll_employee._side_code + ","
                    + "(select "
                    + _g._dataPayroll.payroll_section_list._name
                    + " from "
                    + _g._dataPayroll.payroll_section_list._table
                    + " where " + _g._dataPayroll.payroll_section_list._code
                    + " = " + _g._dataPayroll.payroll_employee._table
                    + "."
                    + _g._dataPayroll.payroll_employee._section_code + ") as "
                    + _g._dataPayroll.payroll_employee._section_code + ","
                    + "(select " + _g._dataPayroll.payroll_work_title._name
                    + " from " + _g._dataPayroll.payroll_work_title._table
                    + " where "
                    + _g._dataPayroll.payroll_work_title._code
                    + " = "
                    + _g._dataPayroll.payroll_employee._table
                    + "."
                    + _g._dataPayroll.payroll_employee._work_title
                    + ") as "
                    + _g._dataPayroll.payroll_employee._work_title + ","
                    + _g._dataPayroll.payroll_employee._salary + ","
                    + _g._dataPayroll.payroll_employee._start_work
                    + " from " + _g._dataPayroll.payroll_employee._table
                    + WhereEmployee
                    + " order by " + _g._dataPayroll.payroll_employee._code + " asc ";

            _processDS = myFrameWork._query(MyLib._myGlobal._databaseName, _query);
            return _processDS;
        }
       /// <summary>
        /// รายงาน การทำงาน
       /// </summary>
       /// <param name="FromDate"></param>
       /// <param name="ToDate"></param>
       /// <param name="FromEmployee"></param>
       /// <param name="ToEmployee"></param>
       /// <param name="EmployeeType"></param>
       /// <param name="InMonth"></param>
       /// <param name="InYear"></param>
       /// <param name="ScreenName"></param>
       /// <returns></returns>
        public DataSet _processReportTrans(string FromDate, string ToDate, string FromEmployee, string ToEmployee, string EmployeeType, int InMonth, string InYear, _g.g._screenPayrollEnum ScreenName)
        {
            DataSet _processDS = new DataSet();
            string WhereTransDetail = "";
            string WorkingType = "";
            string Money = "";
            string _query = "";
            switch (ScreenName)
            {
                case _g.g._screenPayrollEnum.รายละเอียดการขาดงาน:
                    WhereTransDetail = " where " + _g._dataPayroll.payroll_trans_detail._trans_flag + " = 1 and " + _g._dataPayroll.payroll_trans_detail._trans_type + " = 1 ";
                    WorkingType = "(select "
                        //+ _g._dataPayroll.payroll_short_of_work._code
                        //+ "||'~'||"
                        + _g._dataPayroll.payroll_short_of_work._name
                        + " from "
                        + _g._dataPayroll.payroll_short_of_work._table
                        + " where "
                        + _g._dataPayroll.payroll_short_of_work._code
                        + " = "
                        + _g._dataPayroll.payroll_trans_detail._table + "." + _g._dataPayroll.payroll_trans_detail._working_type + ") as "
                        + _g._dataPayroll.payroll_trans_detail._working_type + ",";
                    Money = _g._dataPayroll.payroll_trans_detail._money;
                    break;
                case _g.g._screenPayrollEnum.รายละเอียดการลางาน:
                    WhereTransDetail = " where " + _g._dataPayroll.payroll_trans_detail._trans_flag + " = 1 and " + _g._dataPayroll.payroll_trans_detail._trans_type + " = 2 ";
                    WorkingType = "(select "
                       //+ _g._dataPayroll.payroll_leave._code
                       //+ "||'~'||"
                       + _g._dataPayroll.payroll_leave._name
                       + " from "
                       + _g._dataPayroll.payroll_leave._table
                       + " where "
                       + _g._dataPayroll.payroll_leave._code
                       + " = "
                       + _g._dataPayroll.payroll_trans_detail._table + "." + _g._dataPayroll.payroll_trans_detail._working_type + ") as "
                       + _g._dataPayroll.payroll_trans_detail._working_type + ",";
                    Money = _g._dataPayroll.payroll_trans_detail._money;
                    break;
                case _g.g._screenPayrollEnum.รายละเอียดการมาสาย:
                    WhereTransDetail = " where " + _g._dataPayroll.payroll_trans_detail._trans_flag + " = 1 and " + _g._dataPayroll.payroll_trans_detail._trans_type + " = 3 ";
                    WorkingType = "(select "
                      //+ _g._dataPayroll.payroll_arrive_late._code
                      //+ "||'~'||"
                      + _g._dataPayroll.payroll_arrive_late._name
                      + " from "
                      + _g._dataPayroll.payroll_arrive_late._table
                      + " where "
                      + _g._dataPayroll.payroll_arrive_late._code
                      + " = "
                      + _g._dataPayroll.payroll_trans_detail._table + "." + _g._dataPayroll.payroll_trans_detail._working_type + ") as "
                      + _g._dataPayroll.payroll_trans_detail._working_type + ",";
                    Money = _g._dataPayroll.payroll_trans_detail._money;
                    break;
                case _g.g._screenPayrollEnum.รายละเอียดการทำงานล่วงเวลา:
                    WhereTransDetail = " where " + _g._dataPayroll.payroll_trans_detail._trans_flag + " = 1 and " + _g._dataPayroll.payroll_trans_detail._trans_type + " = 4 ";
                    WorkingType = "(select "
                      //+ _g._dataPayroll.payroll_over_time._code
                      //+ "||'~'||"
                      + _g._dataPayroll.payroll_over_time._name
                      + " from "
                      + _g._dataPayroll.payroll_over_time._table
                      + " where "
                      + _g._dataPayroll.payroll_over_time._code
                      + " = "
                      + _g._dataPayroll.payroll_trans_detail._table + "." + _g._dataPayroll.payroll_trans_detail._working_type + ") as "
                      + _g._dataPayroll.payroll_trans_detail._working_type + ",";
                    Money = _g._dataPayroll.payroll_trans_detail._money;
                    break;
                case _g.g._screenPayrollEnum.รายละเอียดเงินได้_เงินหัก_ประจำเดือน:
                    WhereTransDetail = " where " + _g._dataPayroll.payroll_trans_detail._trans_flag + " = 1 and " + _g._dataPayroll.payroll_trans_detail._trans_type + " = 5 ";
                    WorkingType = "";
                    Money = "((coalesce(" + _g._dataPayroll.payroll_trans_detail._diligent_money + ",0)+"
                        + "coalesce(" + _g._dataPayroll.payroll_trans_detail._bonus_money + ",0)+"
                        + "coalesce(" + _g._dataPayroll.payroll_trans_detail._prepay_money + ",0)+"
                        + "coalesce(" + _g._dataPayroll.payroll_trans_detail._other_income_money + ",0))-("
                        + "coalesce(" + _g._dataPayroll.payroll_trans_detail._advance_money + ",0)+"
                        + "coalesce(" + _g._dataPayroll.payroll_trans_detail._insure_money + ",0)+"
                        + "coalesce(" + _g._dataPayroll.payroll_trans_detail._other_payout_money + ",0))) as " + _g._dataPayroll.payroll_trans_detail._money;
                    break;
            }

            if (!FromEmployee.Equals("") && !ToEmployee.Equals(""))
            {
                WhereTransDetail += " and (" + _g._dataPayroll.payroll_trans_detail._employee + " between '" + FromEmployee + "' and '" + ToEmployee + "')";
            }
            else
            {
                if (!FromEmployee.Equals(""))
                {
                    WhereTransDetail += " and (" + _g._dataPayroll.payroll_trans_detail._employee + " >= '" + FromEmployee + "')";
                }
                if (!ToEmployee.Equals(""))
                {
                    WhereTransDetail += " and (" + _g._dataPayroll.payroll_trans_detail._employee + " =< '" + ToEmployee + "')";
                }
            }
            if (!FromDate.Equals("") && !ToDate.Equals(""))
            {
                WhereTransDetail += " and (" + _g._dataPayroll.payroll_trans_detail._working_date + " between '" + FromDate + "' and '" + ToDate + "')";
            }
            else
            {
                if (!FromDate.Equals(""))
                {
                    WhereTransDetail += " and (" + _g._dataPayroll.payroll_trans_detail._working_date + " >= '" + FromDate + "')";
                }
                if (!ToDate.Equals(""))
                {
                    WhereTransDetail += " and (" + _g._dataPayroll.payroll_trans_detail._working_date + " =< '" + ToDate + "')";
                }
            }
            if (!InYear.Equals(""))
            {
                WhereTransDetail += " and (" + _g._dataPayroll.payroll_trans_detail._select_year + " = '" + InYear + "')";
            }
            if (InMonth < 12)
            {
                WhereTransDetail += " and (" + _g._dataPayroll.payroll_trans_detail._select_month + " = " + InMonth + ")";
            }
            if (!EmployeeType.Equals(""))
            {                
                WhereTransDetail += " and (" + _g._dataPayroll.payroll_trans_detail._employee_type + " = " + EmployeeType + ")";
            }
            //String Query
            _query = "select "
                + _g._dataPayroll.payroll_trans_detail._doc_no + ","
                + _g._dataPayroll.payroll_trans_detail._doc_date + ","
                + "(select "
                + _g._dataPayroll.payroll_employee._code
                + "||'~'||"
                + _g._dataPayroll.payroll_employee._name
                + " from "
                + _g._dataPayroll.payroll_employee._table
                + " where "
                + _g._dataPayroll.payroll_employee._code + " = " + _g._dataPayroll.payroll_trans_detail._table + "." + _g._dataPayroll.payroll_trans_detail._employee + ") as "
                + _g._dataPayroll.payroll_trans_detail._employee + ","
                + "(select "
                + " CASE " + _g._dataPayroll.payroll_employee._employee_type
                + " WHEN 0 THEN 'พนักงานรายเดือน'"
                + " WHEN 1 THEN 'พนักงานรายสัปดาห์'"
                + " ELSE 'พนักงานรายวัน'"
                + " END"
                + " from "
                + _g._dataPayroll.payroll_employee._table
                + " where "
                + _g._dataPayroll.payroll_employee._code
                + " = "
                + _g._dataPayroll.payroll_trans_detail._table + "." + _g._dataPayroll.payroll_trans_detail._employee + ") as "
                + _g._dataPayroll.payroll_trans_detail._employee_type + ","
                + _g._dataPayroll.payroll_trans_detail._working_date + ","
                + "coalesce(" + _g._dataPayroll.payroll_trans_detail._working_day + ",0) as " + _g._dataPayroll.payroll_trans_detail._working_day + ","
                + "coalesce(" + _g._dataPayroll.payroll_trans_detail._working_hours + ",0) as " + _g._dataPayroll.payroll_trans_detail._working_hours + ","
                + "coalesce(" + _g._dataPayroll.payroll_trans_detail._working_minute + ",0) as " + _g._dataPayroll.payroll_trans_detail._working_minute + ","

                + WorkingType

                + "coalesce(" + _g._dataPayroll.payroll_trans_detail._diligent_money + ",0) as " + _g._dataPayroll.payroll_trans_detail._diligent_money + ","
                + "coalesce(" + _g._dataPayroll.payroll_trans_detail._bonus_money + ",0) as " + _g._dataPayroll.payroll_trans_detail._bonus_money + ","
                + "coalesce(" + _g._dataPayroll.payroll_trans_detail._prepay_money + ",0) as " + _g._dataPayroll.payroll_trans_detail._prepay_money + ","
                + "coalesce(" + _g._dataPayroll.payroll_trans_detail._other_income_money + ",0) as " + _g._dataPayroll.payroll_trans_detail._other_income_money + ","
                + "coalesce(" + _g._dataPayroll.payroll_trans_detail._advance_money + ",0) as " + _g._dataPayroll.payroll_trans_detail._advance_money + ","
                + "coalesce(" + _g._dataPayroll.payroll_trans_detail._insure_money + ",0) as " + _g._dataPayroll.payroll_trans_detail._insure_money + ","
                + "coalesce(" + _g._dataPayroll.payroll_trans_detail._other_payout_money + ",0) as " + _g._dataPayroll.payroll_trans_detail._other_payout_money + ","

                + Money

                + " from "
                + _g._dataPayroll.payroll_trans_detail._table
                + WhereTransDetail
                + " order by " + _g._dataPayroll.payroll_trans_detail._doc_no + " asc ";

            _processDS = myFrameWork._query(MyLib._myGlobal._databaseName, _query);
            return _processDS;
        }
       /// <summary>
        /// รายงานสรุปต่างๆ
       /// </summary>
       /// <param name="FromDate"></param>
       /// <param name="ToDate"></param>
       /// <param name="FromEmployee"></param>
       /// <param name="ToEmployee"></param>
       /// <param name="EmployeeType"></param>
       /// <param name="InMonth"></param>
       /// <param name="InYear"></param>
       /// <param name="ScreenName"></param>
       /// <returns></returns>
        public DataSet _processReportTransSummarize(string FromDate, string ToDate, string FromEmployee, string ToEmployee, string EmployeeType, int InMonth, string InYear, _g.g._screenPayrollEnum ScreenName)
        {
            DataSet _processDS = new DataSet();
            string WhereTrans = "";            
            string WhereTransDetail = "";
            if (!FromEmployee.Equals("") && !ToEmployee.Equals(""))
            {
                WhereTrans += " where (" + _g._dataPayroll.payroll_trans._employee + " between '" + FromEmployee + "' and '" + ToEmployee + "')";
            }
            else
            {
                if (!FromEmployee.Equals(""))
                {
                    WhereTrans += " where (" + _g._dataPayroll.payroll_trans._employee + " >= '" + FromEmployee + "')";
                }
                if (!ToEmployee.Equals(""))
                {
                    WhereTrans += " where (" + _g._dataPayroll.payroll_trans._employee + " =< '" + ToEmployee + "')";
                }
            }
            if (!FromDate.Equals("") && !ToDate.Equals(""))
            {
                if (!WhereTrans.Equals(""))
                {
                    WhereTrans += " and (" + _g._dataPayroll.payroll_trans._date_pay_salary + " between '" + FromDate + "' and '" + ToDate + "')";
                }
                else
                {
                    WhereTrans += " where (" + _g._dataPayroll.payroll_trans._date_pay_salary + " between '" + FromDate + "' and '" + ToDate + "')";
                }                
                WhereTransDetail += " and (" + _g._dataPayroll.payroll_trans_detail._working_date + " between '" + FromDate + "' and '" + ToDate + "')";

            }
            else
            {
                if (!FromDate.Equals(""))
                {
                    if (!WhereTrans.Equals(""))
                    {
                        WhereTrans += " and (" + _g._dataPayroll.payroll_trans._date_pay_salary + " >= '" + FromDate + "')";
                    }
                    else
                    {
                        WhereTrans += " where (" + _g._dataPayroll.payroll_trans._date_pay_salary + " >= '" + FromDate + "')";
                    }                   
                    WhereTransDetail += " and (" + _g._dataPayroll.payroll_trans_detail._working_date + " >= '" + FromDate + "')";
                }
                if (!ToDate.Equals(""))
                {
                    if (!WhereTrans.Equals(""))
                    {
                        WhereTrans += " and (" + _g._dataPayroll.payroll_trans._date_pay_salary + " =< '" + ToDate + "')";
                    }
                    else
                    {
                        WhereTrans += " where (" + _g._dataPayroll.payroll_trans._date_pay_salary + " =< '" + ToDate + "')";
                    }                   
                    WhereTransDetail += " and (" + _g._dataPayroll.payroll_trans_detail._working_date + " =< '" + ToDate + "')";
                }
            }
            if (!InYear.Equals(""))
            {
                if (!WhereTrans.Equals(""))
                {
                    WhereTrans += " and (" + _g._dataPayroll.payroll_trans._select_year + " = '" + InYear + "')";
                }
                else
                {
                    WhereTrans += " where (" + _g._dataPayroll.payroll_trans._select_year + " = '" + InYear + "')";
                }                
                WhereTransDetail += " and (" + _g._dataPayroll.payroll_trans_detail._select_year + " = '" + InYear + "')";
            }
            if (InMonth < 12)
            {
                if (!WhereTrans.Equals(""))
                {
                    WhereTrans += " and (" + _g._dataPayroll.payroll_trans._select_month + " = " + InMonth + ")";
                }
                else
                {
                    WhereTrans += " where (" + _g._dataPayroll.payroll_trans._select_month + " = " + InMonth + ")";
                }               
                WhereTransDetail += " and (" + _g._dataPayroll.payroll_trans_detail._select_month + " = " + InMonth + ")";
            }
            if (!EmployeeType.Equals(""))
            {
                if (!WhereTrans.Equals(""))
                {
                    WhereTrans += " and (" + _g._dataPayroll.payroll_trans._employee_type + " = " + EmployeeType + ")";
                }
                else
                {
                    WhereTrans += " where (" + _g._dataPayroll.payroll_trans._employee_type + " = " + EmployeeType + ")";
                }               
                WhereTransDetail += " and (" + _g._dataPayroll.payroll_trans_detail._employee_type + " = " + EmployeeType + ")";
            }

            string _query = "select "               

                + " coalesce((select sum(" + _g._dataPayroll.payroll_trans_detail._working_day + ") from " + _g._dataPayroll.payroll_trans_detail._table + " where " + _g._dataPayroll.payroll_trans_detail._trans_flag + "=1 and " + _g._dataPayroll.payroll_trans_detail._trans_type + "=1 and " + _g._dataPayroll.payroll_trans_detail._employee + " =  " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + " " + WhereTransDetail + "),0) as sow_day,"
                + " coalesce((select sum(" + _g._dataPayroll.payroll_trans_detail._working_day + ") from " + _g._dataPayroll.payroll_trans_detail._table + " where " + _g._dataPayroll.payroll_trans_detail._trans_flag + "=1 and " + _g._dataPayroll.payroll_trans_detail._trans_type + "=2 and " + _g._dataPayroll.payroll_trans_detail._employee + " =  " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + " " + WhereTransDetail + "),0) as leave_day,"
                + " coalesce((select sum(" + _g._dataPayroll.payroll_trans_detail._working_day + ") from " + _g._dataPayroll.payroll_trans_detail._table + " where " + _g._dataPayroll.payroll_trans_detail._trans_flag + "=1 and " + _g._dataPayroll.payroll_trans_detail._trans_type + "=3 and " + _g._dataPayroll.payroll_trans_detail._employee + " =  " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + " " + WhereTransDetail + "),0) as late_day,"
                + " coalesce((select sum(" + _g._dataPayroll.payroll_trans_detail._working_day + ") from " + _g._dataPayroll.payroll_trans_detail._table + " where " + _g._dataPayroll.payroll_trans_detail._trans_flag + "=1 and " + _g._dataPayroll.payroll_trans_detail._trans_type + "=4 and " + _g._dataPayroll.payroll_trans_detail._employee + " =  " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + " " + WhereTransDetail + "),0) as ot_day,"


                + " coalesce((select sum(" + _g._dataPayroll.payroll_trans_detail._working_hours + ") from " + _g._dataPayroll.payroll_trans_detail._table + " where " + _g._dataPayroll.payroll_trans_detail._trans_flag + "=1 and " + _g._dataPayroll.payroll_trans_detail._trans_type + "=2 and " + _g._dataPayroll.payroll_trans_detail._employee + " =  " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + " " + WhereTransDetail + "),0) as leave_hours,"
                + " coalesce((select sum(" + _g._dataPayroll.payroll_trans_detail._working_hours + ") from " + _g._dataPayroll.payroll_trans_detail._table + " where " + _g._dataPayroll.payroll_trans_detail._trans_flag + "=1 and " + _g._dataPayroll.payroll_trans_detail._trans_type + "=3 and " + _g._dataPayroll.payroll_trans_detail._employee + " =  " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + " " + WhereTransDetail + "),0) as late_hours,"
                + " coalesce((select sum(" + _g._dataPayroll.payroll_trans_detail._working_hours + ") from " + _g._dataPayroll.payroll_trans_detail._table + " where " + _g._dataPayroll.payroll_trans_detail._trans_flag + "=1 and " + _g._dataPayroll.payroll_trans_detail._trans_type + "=4 and " + _g._dataPayroll.payroll_trans_detail._employee + " =  " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + " " + WhereTransDetail + "),0) as ot_hours,"

                + " coalesce((select sum(" + _g._dataPayroll.payroll_trans_detail._working_minute + ") from " + _g._dataPayroll.payroll_trans_detail._table + " where " + _g._dataPayroll.payroll_trans_detail._trans_flag + "=1 and " + _g._dataPayroll.payroll_trans_detail._trans_type + "=2 and " + _g._dataPayroll.payroll_trans_detail._employee + " =  " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + " " + WhereTransDetail + "),0) as leave_minute,"
                + " coalesce((select sum(" + _g._dataPayroll.payroll_trans_detail._working_minute + ") from " + _g._dataPayroll.payroll_trans_detail._table + " where " + _g._dataPayroll.payroll_trans_detail._trans_flag + "=1 and " + _g._dataPayroll.payroll_trans_detail._trans_type + "=3 and " + _g._dataPayroll.payroll_trans_detail._employee + " =  " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + " " + WhereTransDetail + "),0) as late_minute,"
                + " coalesce((select sum(" + _g._dataPayroll.payroll_trans_detail._working_minute + ") from " + _g._dataPayroll.payroll_trans_detail._table + " where " + _g._dataPayroll.payroll_trans_detail._trans_flag + "=1 and " + _g._dataPayroll.payroll_trans_detail._trans_type + "=4 and " + _g._dataPayroll.payroll_trans_detail._employee + " =  " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + " " + WhereTransDetail + "),0) as ot_minute,"

               
                + "(select (select " + _g._dataPayroll.payroll_side_list._name + " from " + _g._dataPayroll.payroll_side_list._table + " where " + _g._dataPayroll.payroll_side_list._table + "." + _g._dataPayroll.payroll_side_list._code + "= " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._side_code + ") as side_name from " + _g._dataPayroll.payroll_employee._table + " where " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + " = " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + ") as side_name,"
                + "(select (select " + _g._dataPayroll.payroll_section_list._name + " from " + _g._dataPayroll.payroll_section_list._table + " where " + _g._dataPayroll.payroll_section_list._table + "." + _g._dataPayroll.payroll_section_list._code + "= " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._section_code + ") as section_name from " + _g._dataPayroll.payroll_employee._table + " where " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + " = " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + ") as section_name,"
                + "(select (select " + _g._dataPayroll.payroll_work_title._name + " from " + _g._dataPayroll.payroll_work_title._table + " where " + _g._dataPayroll.payroll_work_title._table + "." + _g._dataPayroll.payroll_work_title._code + "= " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._work_title + ") as work_title_name from " + _g._dataPayroll.payroll_employee._table + " where " + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + " = " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + ") as work_title_name,"
                       
                + "coalesce((sum(" + _g._dataPayroll.payroll_trans._working_day + ")),0) as working_day,"
                + _g._dataPayroll.payroll_trans._employee + ","
                + " (select " + _g._dataPayroll.payroll_employee._name + " from " + _g._dataPayroll.payroll_employee._table + " where " + _g._dataPayroll.payroll_employee._code + " = " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + ")  as " + _g._dataPayroll.payroll_resource._employee_name
                + " from " + _g._dataPayroll.payroll_trans._table
                + " " + WhereTrans + " group by " + _g._dataPayroll.payroll_trans._employee + " order by " + _g._dataPayroll.payroll_trans._employee;
                
            
            _processDS = myFrameWork._query(MyLib._myGlobal._databaseName, _query);
            return _processDS;
        }
       /// <summary>
        /// ฟอร์ม รายงาน สปส.1-10
       /// </summary>
       /// <param name="FromDate"></param>
       /// <param name="ToDate"></param>
       /// <param name="FromEmployee"></param>
       /// <param name="ToEmployee"></param>
       /// <param name="EmployeeType"></param>
       /// <param name="InMonth"></param>
       /// <param name="InYear"></param>
       /// <param name="ScreenName"></param>
       /// <returns></returns>
        public DataSet _processSociety(string FromDate, string ToDate, string FromEmployee, string ToEmployee, string EmployeeType, int InMonth, string InYear, _g.g._screenPayrollEnum ScreenName)
        {
            DataSet _processDS = new DataSet();
            string WhereTrans = "";
            string WhereTransSub = "";
            string WhereTransDetail = "";
            string FieldEmployeeHead = "";
            string FieldEmployeeData = "";

            FromDate = MyLib._myGlobal._convertDateToQuery(this._processStartDate("01", InMonth.ToString(), InYear));
            ToDate = MyLib._myGlobal._convertDateToQuery(this._processEndDate("28", InMonth.ToString(), InYear, true));

            FieldEmployeeHead = "title_name as title_name,"
                + "tax_no as tax_no,"
                + "id_code as id_code,"
                + "house as house,"
                + "room_no as room_no,"
                + "floor_no as floor_no,"
                + "village as village,"
                + "house_no as house_no,"
                + "crowd_no as crowd_no,"
                + "lane as lane,"
                + "road as road,"
                + "locality as locality,"
                + "amphur as amphur,"
                + "province as province,"
                + "postcode as postcode,"
                + "telephone as telephone,";

            FieldEmployeeData = "(select title_name from payroll_employee where payroll_employee.code = " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + ") as title_name,"
                + "(select tax_no from payroll_employee where payroll_employee.code = " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + ") as tax_no,"
                + "(select id_code from payroll_employee_detail where payroll_employee_detail.code = " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + ") as id_code,"
                + "(select house from payroll_employee_address where payroll_employee_address.code = " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + ") as house,"
                + "(select room_no from payroll_employee_address where payroll_employee_address.code = " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + ") as room_no,"
                + "(select floor_no from payroll_employee_address where payroll_employee_address.code = " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + ") as floor_no,"
                + "(select village from payroll_employee_address where payroll_employee_address.code = " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + ") as village,"
                + "(select house_no from payroll_employee_address where payroll_employee_address.code = " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + ") as house_no,"
                + "(select crowd_no from payroll_employee_address where payroll_employee_address.code = " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + ") as crowd_no,"
                + "(select lane from payroll_employee_address where payroll_employee_address.code = " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + ") as lane,"
                + "(select road from payroll_employee_address where payroll_employee_address.code = " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + ") as road,"
                + "(select locality from payroll_employee_address where payroll_employee_address.code = " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + ") as locality,"
                + "(select amphur from payroll_employee_address where payroll_employee_address.code = " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + ") as amphur,"
                + "(select province from payroll_employee_address where payroll_employee_address.code = " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + ") as province,"
                + "(select postcode from payroll_employee_address where payroll_employee_address.code = " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + ") as postcode,"
                + "(select telephone from payroll_employee_address where payroll_employee_address.code = " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + ") as telephone,";

            if (!FromEmployee.Equals("") && !ToEmployee.Equals(""))
            {
                WhereTrans += " where (" + _g._dataPayroll.payroll_trans._employee + " between '" + FromEmployee + "' and '" + ToEmployee + "')";
            }
            else
            {
                if (!FromEmployee.Equals(""))
                {
                    WhereTrans += " where (" + _g._dataPayroll.payroll_trans._employee + " >= '" + FromEmployee + "')";
                }
                if (!ToEmployee.Equals(""))
                {
                    WhereTrans += " where (" + _g._dataPayroll.payroll_trans._employee + " =< '" + ToEmployee + "')";
                }
            }
            if (!FromDate.Equals("") && !ToDate.Equals(""))
            {
                if (!WhereTrans.Equals(""))
                {
                    WhereTrans += " and (" + _g._dataPayroll.payroll_trans._date_pay_salary + " between '" + FromDate + "' and '" + ToDate + "')";
                }
                else
                {
                    WhereTrans += " where (" + _g._dataPayroll.payroll_trans._date_pay_salary + " between '" + FromDate + "' and '" + ToDate + "')";
                }
                WhereTransSub += " and (" + _g._dataPayroll.payroll_trans._date_pay_salary + " between '" + FromDate + "' and '" + ToDate + "')";
                WhereTransDetail += " and (" + _g._dataPayroll.payroll_trans_detail._working_date + " between '" + FromDate + "' and '" + ToDate + "')";

            }
            else
            {
                if (!FromDate.Equals(""))
                {
                    if (!WhereTrans.Equals(""))
                    {
                        WhereTrans += " and (" + _g._dataPayroll.payroll_trans._date_pay_salary + " >= '" + FromDate + "')";
                    }
                    else
                    {
                        WhereTrans += " where (" + _g._dataPayroll.payroll_trans._date_pay_salary + " >= '" + FromDate + "')";
                    }
                    WhereTransSub += " and (" + _g._dataPayroll.payroll_trans._date_pay_salary + " >= '" + FromDate + "')";
                    WhereTransDetail += " and (" + _g._dataPayroll.payroll_trans_detail._working_date + " >= '" + FromDate + "')";
                }
                if (!ToDate.Equals(""))
                {
                    if (!WhereTrans.Equals(""))
                    {
                        WhereTrans += " and (" + _g._dataPayroll.payroll_trans._date_pay_salary + " =< '" + ToDate + "')";
                    }
                    else
                    {
                        WhereTrans += " where (" + _g._dataPayroll.payroll_trans._date_pay_salary + " =< '" + ToDate + "')";
                    }
                    WhereTransSub += " and (" + _g._dataPayroll.payroll_trans._date_pay_salary + " =< '" + ToDate + "')";
                    WhereTransDetail += " and (" + _g._dataPayroll.payroll_trans_detail._working_date + " =< '" + ToDate + "')";
                }
            }
            if (!InYear.Equals(""))
            {
                if (!WhereTrans.Equals(""))
                {
                    WhereTrans += " and (" + _g._dataPayroll.payroll_trans._select_year + " = '" + InYear + "')";
                }
                else
                {
                    WhereTrans += " where (" + _g._dataPayroll.payroll_trans._select_year + " = '" + InYear + "')";
                }
                WhereTransSub += " and (" + _g._dataPayroll.payroll_trans._select_year + " = '" + InYear + "')";
                WhereTransDetail += " and (" + _g._dataPayroll.payroll_trans_detail._select_year + " = '" + InYear + "')";
            }
            if (InMonth < 12)
            {
                if (!WhereTrans.Equals(""))
                {
                    WhereTrans += " and (" + _g._dataPayroll.payroll_trans._select_month + " = " + InMonth + ")";
                }
                else
                {
                    WhereTrans += " where (" + _g._dataPayroll.payroll_trans._select_month + " = " + InMonth + ")";
                }
                WhereTransSub += " and (" + _g._dataPayroll.payroll_trans._select_month + " = " + InMonth + ")";
                WhereTransDetail += " and (" + _g._dataPayroll.payroll_trans_detail._select_month + " = " + InMonth + ")";
            }
            if (!EmployeeType.Equals(""))
            {
                if (!WhereTrans.Equals(""))
                {
                    WhereTrans += " and (" + _g._dataPayroll.payroll_trans._employee_type + " = " + EmployeeType + ")";
                }
                else
                {
                    WhereTrans += " where (" + _g._dataPayroll.payroll_trans._employee_type + " = " + EmployeeType + ")";
                }
                WhereTransSub += " and (" + _g._dataPayroll.payroll_trans._employee_type + " = " + EmployeeType + ")";
                WhereTransDetail += " and (" + _g._dataPayroll.payroll_trans_detail._employee_type + " = " + EmployeeType + ")";
            }
            string _query = "select "
                       + "customer_ss_rate as customer_ss_rate,"
                       + "social_security_max as social_security_max,"
                       + "social_security_min as social_security_min,"
                       + FieldEmployeeHead
                       + " coalesce(("
                       + " CASE salary_status"
                       + " WHEN 0 THEN society_insure_money_employer "
                       + " ELSE society_insure_money_employer_vary"
                       + " END"
                       + " ),0) as society_insure_money_employer,"

                       + " coalesce(("
                       + " CASE salary_status"
                       + " WHEN 0 THEN society_insure_money_customer "
                       + " ELSE society_insure_money_customer_vary"
                       + " END"
                       + " ),0) as society_insure_money_customer,"
                       
                       + " coalesce(("
                       + " CASE salary_status"
                       + " WHEN 0 THEN wages_money "
                       + " ELSE wages_money_vary"
                       + " END"
                       + " ),0) as wages_money,"

                       //+ " society_insure_money_employer as society_insure_money_employer,"
                       //+ " society_insure_money_customer as society_insure_money_customer,"
                       //+ " " + _g._dataPayroll.payroll_trans._wages_money + " as " + _g._dataPayroll.payroll_resource._wages_money + ","
                       + " " + _g._dataPayroll.payroll_trans._employee + " as " + _g._dataPayroll.payroll_resource._employee + ","
                       + " " + _g._dataPayroll.payroll_trans._employee_name + " as " + _g._dataPayroll.payroll_resource._employee_name
                       + " from "
                       + "(select "
                //คิดระยะประกันสังคม
                       + "coalesce((select  ceiling((coalesce((" + _g._dataPayroll.payroll_company_config._social_security_max + "),0) * coalesce((" + _g._dataPayroll.payroll_company_config._customer_ss_rate + "),0))/100)  from " + _g._dataPayroll.payroll_company_config._table + " ),0) as social_security_max,"
                       + "coalesce((select  ceiling((coalesce((" + _g._dataPayroll.payroll_company_config._social_security_min + "),0) * coalesce((" + _g._dataPayroll.payroll_company_config._customer_ss_rate + "),0))/100)  from " + _g._dataPayroll.payroll_company_config._table + " ),0) as social_security_min,"
                       + "coalesce((select " + _g._dataPayroll.payroll_company_config._customer_ss_rate + " from " + _g._dataPayroll.payroll_company_config._table + " ),0) as customer_ss_rate,"
                       + "coalesce((select " + _g._dataPayroll.payroll_company_config._employer_ss_rate + " from " + _g._dataPayroll.payroll_company_config._table + " ),0) as employer_ss_rate,"
                //เช็คสถานะเงินเดือน
                       + "coalesce((select " + _g._dataPayroll.payroll_employee._salary_status + " from "
                       + _g._dataPayroll.payroll_employee._table
                       + " where " + _g._dataPayroll.payroll_employee._code
                       + " = " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee
                       + "),0) as " + _g._dataPayroll.payroll_employee._salary_status + ","
                //คิดเงินประกันสังคม(ลูกจ้างจ่าย)
                       + " coalesce((select "
                       + " CASE " + _g._dataPayroll.payroll_employee._employee_type
                       + " WHEN 0 THEN "
                       + "((select coalesce((" + _g._dataPayroll.payroll_employee_salary._salary + "),0) as " + _g._dataPayroll.payroll_employee_salary._salary
                       + " from " + _g._dataPayroll.payroll_employee_salary._table
                       + " where " + _g._dataPayroll.payroll_employee_salary._code + " = " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + ""
                       + " and " + _g._dataPayroll.payroll_employee_salary._start_date + " <= '" + FromDate + "'"
                       + " and (" + _g._dataPayroll.payroll_employee_salary._end_date + " >= '" + ToDate + "' or " + _g._dataPayroll.payroll_employee_salary._end_date + " = '1000-01-01'))  * ((select " + _g._dataPayroll.payroll_company_config._customer_ss_rate + " from " + _g._dataPayroll.payroll_company_config._table + " ) / 100))"
                       + " WHEN 1 THEN "
                       + "(("
                       + "select coalesce((" + _g._dataPayroll.payroll_employee_salary._salary + "),0) as " + _g._dataPayroll.payroll_employee_salary._salary
                       + " from " + _g._dataPayroll.payroll_employee_salary._table
                       + " where " + _g._dataPayroll.payroll_employee_salary._code + " = " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + ""
                       + " and " + _g._dataPayroll.payroll_employee_salary._start_date + " <= '" + FromDate + "'"
                       + " and (" + _g._dataPayroll.payroll_employee_salary._end_date + " >= '" + ToDate + "' or " + _g._dataPayroll.payroll_employee_salary._end_date + " = '1000-01-01')"
                       + ") * 4) * ((select " + _g._dataPayroll.payroll_company_config._customer_ss_rate + " from " + _g._dataPayroll.payroll_company_config._table + " ) / 100)"
                       + " ELSE "
                       + "(("
                       + "select coalesce((" + _g._dataPayroll.payroll_employee_salary._salary + "),0) as " + _g._dataPayroll.payroll_employee_salary._salary
                       + " from " + _g._dataPayroll.payroll_employee_salary._table
                       + " where " + _g._dataPayroll.payroll_employee_salary._code + " = " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + ""
                       + " and " + _g._dataPayroll.payroll_employee_salary._start_date + " <= '" + FromDate + "'"
                       + " and (" + _g._dataPayroll.payroll_employee_salary._end_date + " >= '" + ToDate + "' or " + _g._dataPayroll.payroll_employee_salary._end_date + " = '1000-01-01')"
                       + ") * (select sum(" + _g._dataPayroll.payroll_trans._working_day + ") from " + _g._dataPayroll.payroll_trans._table + " where " + _g._dataPayroll.payroll_trans._employee + "=" + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + " " + WhereTransSub + ")) * ((select " + _g._dataPayroll.payroll_company_config._customer_ss_rate + " from " + _g._dataPayroll.payroll_company_config._table + " ) / 100)"
                       + " END"
                       + " from " + _g._dataPayroll.payroll_employee._table + " where " + _g._dataPayroll.payroll_employee._code + " = " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + "),0) as society_insure_money_customer_vary,"

                       + " coalesce((select "
                       + " CASE " + _g._dataPayroll.payroll_employee._employee_type
                       + " WHEN 0 THEN (" + _g._dataPayroll.payroll_employee._salary + " * ((select " + _g._dataPayroll.payroll_company_config._customer_ss_rate + " from " + _g._dataPayroll.payroll_company_config._table + " ) / 100))"
                       + " WHEN 1 THEN (((" + _g._dataPayroll.payroll_employee._salary + " / (select " + _g._dataPayroll.payroll_company_config._work_day + " from " + _g._dataPayroll.payroll_company_config._table + ")) * (select sum(" + _g._dataPayroll.payroll_trans._working_day + ") from " + _g._dataPayroll.payroll_trans._table + " where " + _g._dataPayroll.payroll_trans._employee + "=" + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + " " + WhereTransSub + ")) * ((select " + _g._dataPayroll.payroll_company_config._customer_ss_rate + " from " + _g._dataPayroll.payroll_company_config._table + " ) / 100))"
                       + " ELSE ((" + _g._dataPayroll.payroll_employee._salary + " * (select sum(" + _g._dataPayroll.payroll_trans._working_day + ") from " + _g._dataPayroll.payroll_trans._table + " where " + _g._dataPayroll.payroll_trans._employee + "=" + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + " " + WhereTransSub + ")) * ((select " + _g._dataPayroll.payroll_company_config._customer_ss_rate + " from " + _g._dataPayroll.payroll_company_config._table + " ) / 100))"
                       + " END"
                       + " from " + _g._dataPayroll.payroll_employee._table + " where " + _g._dataPayroll.payroll_employee._code + " = " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + "),0) as  society_insure_money_customer,"
                //คิดเงินประกันสังคม(นายจ้างจ่าย)
                       + " coalesce((select "
                       + " CASE " + _g._dataPayroll.payroll_employee._employee_type
                       + " WHEN 0 THEN "
                       + "((select coalesce((" + _g._dataPayroll.payroll_employee_salary._salary + "),0) as " + _g._dataPayroll.payroll_employee_salary._salary
                       + " from " + _g._dataPayroll.payroll_employee_salary._table
                       + " where " + _g._dataPayroll.payroll_employee_salary._code + " = " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + ""
                       + " and " + _g._dataPayroll.payroll_employee_salary._start_date + " <= '" + FromDate + "'"
                       + " and (" + _g._dataPayroll.payroll_employee_salary._end_date + " >= '" + ToDate + "' or " + _g._dataPayroll.payroll_employee_salary._end_date + " = '1000-01-01'))  * ((select " + _g._dataPayroll.payroll_company_config._employer_ss_rate + " from " + _g._dataPayroll.payroll_company_config._table + " ) / 100))"
                       + " WHEN 1 THEN "
                       + "(("
                       + "select coalesce((" + _g._dataPayroll.payroll_employee_salary._salary + "),0) as " + _g._dataPayroll.payroll_employee_salary._salary
                       + " from " + _g._dataPayroll.payroll_employee_salary._table
                       + " where " + _g._dataPayroll.payroll_employee_salary._code + " = " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + ""
                       + " and " + _g._dataPayroll.payroll_employee_salary._start_date + " <= '" + FromDate + "'"
                       + " and (" + _g._dataPayroll.payroll_employee_salary._end_date + " >= '" + ToDate + "' or " + _g._dataPayroll.payroll_employee_salary._end_date + " = '1000-01-01')"
                       + ") * 4) * ((select " + _g._dataPayroll.payroll_company_config._employer_ss_rate + " from " + _g._dataPayroll.payroll_company_config._table + " ) / 100)"
                       + " ELSE "
                       + "(("
                       + "select coalesce((" + _g._dataPayroll.payroll_employee_salary._salary + "),0) as " + _g._dataPayroll.payroll_employee_salary._salary
                       + " from " + _g._dataPayroll.payroll_employee_salary._table
                       + " where " + _g._dataPayroll.payroll_employee_salary._code + " = " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + ""
                       + " and " + _g._dataPayroll.payroll_employee_salary._start_date + " <= '" + FromDate + "'"
                       + " and (" + _g._dataPayroll.payroll_employee_salary._end_date + " >= '" + ToDate + "' or " + _g._dataPayroll.payroll_employee_salary._end_date + " = '1000-01-01')"
                       + ") * (select sum(" + _g._dataPayroll.payroll_trans._working_day + ") from " + _g._dataPayroll.payroll_trans._table + " where " + _g._dataPayroll.payroll_trans._employee + "=" + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + " " + WhereTransSub + ")) * ((select " + _g._dataPayroll.payroll_company_config._employer_ss_rate + " from " + _g._dataPayroll.payroll_company_config._table + " ) / 100)"
                       + " END"
                       + " from " + _g._dataPayroll.payroll_employee._table + " where " + _g._dataPayroll.payroll_employee._code + " = " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + "),0) as society_insure_money_employer_vary,"

                       + " coalesce((select "
                       + " CASE " + _g._dataPayroll.payroll_employee._employee_type
                       + " WHEN 0 THEN (" + _g._dataPayroll.payroll_employee._salary + " * ((select " + _g._dataPayroll.payroll_company_config._employer_ss_rate + " from " + _g._dataPayroll.payroll_company_config._table + " ) / 100))"
                       + " WHEN 1 THEN (((" + _g._dataPayroll.payroll_employee._salary + " / (select " + _g._dataPayroll.payroll_company_config._work_day + " from " + _g._dataPayroll.payroll_company_config._table + ")) * (select sum(" + _g._dataPayroll.payroll_trans._working_day + ") from " + _g._dataPayroll.payroll_trans._table + " where " + _g._dataPayroll.payroll_trans._employee + "=" + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + " " + WhereTransSub + ")) * ((select " + _g._dataPayroll.payroll_company_config._employer_ss_rate + " from " + _g._dataPayroll.payroll_company_config._table + " ) / 100))"
                       + " ELSE ((" + _g._dataPayroll.payroll_employee._salary + " * (select sum(" + _g._dataPayroll.payroll_trans._working_day + ") from " + _g._dataPayroll.payroll_trans._table + " where " + _g._dataPayroll.payroll_trans._employee + "=" + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + " " + WhereTransSub + ")) * ((select " + _g._dataPayroll.payroll_company_config._employer_ss_rate + " from " + _g._dataPayroll.payroll_company_config._table + " ) / 100))"
                       + " END"
                       + " from " + _g._dataPayroll.payroll_employee._table + " where " + _g._dataPayroll.payroll_employee._code + " = " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + "),0) as society_insure_money_employer,"
                  //คิดค่าแรง  

                       + " coalesce((select "
                       + " CASE " + _g._dataPayroll.payroll_employee._employee_type
                       + " WHEN 0 THEN "
                       + "(select coalesce((" + _g._dataPayroll.payroll_employee_salary._salary + "),0) as " + _g._dataPayroll.payroll_employee_salary._salary
                       + " from " + _g._dataPayroll.payroll_employee_salary._table
                       + " where " + _g._dataPayroll.payroll_employee_salary._code + " = " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + ""
                       + " and " + _g._dataPayroll.payroll_employee_salary._start_date + " <= '" + FromDate + "'"
                       + " and (" + _g._dataPayroll.payroll_employee_salary._end_date + " >= '" + ToDate + "' or " + _g._dataPayroll.payroll_employee_salary._end_date + " = '1000-01-01'))"
                       + " WHEN 1 THEN "
                       + "(("
                       + "select coalesce((" + _g._dataPayroll.payroll_employee_salary._salary + "),0) as " + _g._dataPayroll.payroll_employee_salary._salary
                       + " from " + _g._dataPayroll.payroll_employee_salary._table
                       + " where " + _g._dataPayroll.payroll_employee_salary._code + " = " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + ""
                       + " and " + _g._dataPayroll.payroll_employee_salary._start_date + " <= '" + FromDate + "'"
                       + " and (" + _g._dataPayroll.payroll_employee_salary._end_date + " >= '" + ToDate + "' or " + _g._dataPayroll.payroll_employee_salary._end_date + " = '1000-01-01')"
                       + ") * 4)"
                       + " ELSE "
                       + "(("
                       + "select coalesce((" + _g._dataPayroll.payroll_employee_salary._salary + "),0) as " + _g._dataPayroll.payroll_employee_salary._salary
                       + " from " + _g._dataPayroll.payroll_employee_salary._table
                       + " where " + _g._dataPayroll.payroll_employee_salary._code + " = " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + ""
                       + " and " + _g._dataPayroll.payroll_employee_salary._start_date + " <= '" + FromDate + "'"
                       + " and (" + _g._dataPayroll.payroll_employee_salary._end_date + " >= '" + ToDate + "' or " + _g._dataPayroll.payroll_employee_salary._end_date + " = '1000-01-01')"
                       + ") * (select sum(" + _g._dataPayroll.payroll_trans._working_day + ") from " + _g._dataPayroll.payroll_trans._table + " where " + _g._dataPayroll.payroll_trans._employee + "=" + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + " " + WhereTransSub + "))"
                       + " END"
                       + " from " + _g._dataPayroll.payroll_employee._table + " where " + _g._dataPayroll.payroll_employee._code + " = " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + "),0) as wages_money_vary,"
                        
                       + " coalesce((select "
                       + " CASE " + _g._dataPayroll.payroll_employee._employee_type
                       + " WHEN 0 THEN " + _g._dataPayroll.payroll_employee._salary
                       + " WHEN 1 THEN ((" + _g._dataPayroll.payroll_employee._salary + "/(select " + _g._dataPayroll.payroll_company_config._work_day + " from " + _g._dataPayroll.payroll_company_config._table + ")) *(select sum(" + _g._dataPayroll.payroll_trans._working_day + ") from " + _g._dataPayroll.payroll_trans._table + " where " + _g._dataPayroll.payroll_trans._employee + "=" + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + " " + WhereTransSub + "))"
                       + " ELSE (" + _g._dataPayroll.payroll_employee._salary + "*(select sum(" + _g._dataPayroll.payroll_trans._working_day + ") from " + _g._dataPayroll.payroll_trans._table + " where " + _g._dataPayroll.payroll_trans._employee + "=" + _g._dataPayroll.payroll_employee._table + "." + _g._dataPayroll.payroll_employee._code + " " + WhereTransSub + "))"
                       + " END"
                       + " from " + _g._dataPayroll.payroll_employee._table + " where " + _g._dataPayroll.payroll_employee._code + " = " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + "),0) as " + _g._dataPayroll.payroll_trans._wages_money + ","
                  //ข้อมูลพนักงาน
                       + FieldEmployeeData
                       + _g._dataPayroll.payroll_trans._employee + ","
                       + " (select " + _g._dataPayroll.payroll_employee._name + " from " + _g._dataPayroll.payroll_employee._table + " where " + _g._dataPayroll.payroll_employee._code + " = " + _g._dataPayroll.payroll_trans._table + "." + _g._dataPayroll.payroll_trans._employee + ")  as " + _g._dataPayroll.payroll_resource._employee_name
                       + " from " + _g._dataPayroll.payroll_trans._table
                       + " " + WhereTrans + " group by " + _g._dataPayroll.payroll_trans._employee + " order by " + _g._dataPayroll.payroll_trans._employee + "  ) as temp1";
            
            try
            {
                //ตัวแปร ในการคำนวน                 
                decimal society_insure_money_customer = 0.0M;
                decimal society_insure_money_employer = 0.0M;
                string _formatNumber = "{0:0.00}";  
                //รายได้ และ ภาษี
                _processDS = myFrameWork._query(MyLib._myGlobal._databaseName, _query);
                int _checkRow = _processDS.Tables[0].Rows.Count;
                for (int i = 0; i < _checkRow; i++)
                {
                    //คำนวนประกันสังคม
                    decimal SocietyCustomer = decimal.Parse(_processDS.Tables[0].Rows[i]["society_insure_money_customer"].ToString());
                    decimal SocietyEmployer = decimal.Parse(_processDS.Tables[0].Rows[i]["society_insure_money_employer"].ToString());
                    decimal social_security_max = decimal.Parse(_processDS.Tables[0].Rows[i]["social_security_max"].ToString());
                    decimal social_security_min = decimal.Parse(_processDS.Tables[0].Rows[i]["social_security_min"].ToString());                    

                    //ลูกจ้างจ่าย
                    if (SocietyCustomer > social_security_max)
                    {
                        //Modify a Row
                        _processDS.Tables[0].Rows[i][_g._dataPayroll.payroll_resource._society_insure_money] = social_security_max;
                        society_insure_money_customer = social_security_max;
                    }
                    else if (SocietyCustomer < social_security_min)
                    {
                        //Modify a Row
                        _processDS.Tables[0].Rows[i][_g._dataPayroll.payroll_resource._society_insure_money] = social_security_min;
                        society_insure_money_customer = social_security_min;
                    }
                    else
                    {
                        society_insure_money_customer = decimal.Parse(_processDS.Tables[0].Rows[i]["society_insure_money_customer"].ToString());
                    }
                    //นายจ้างจ่าย
                    if (SocietyEmployer > social_security_max)
                    {
                        //Modify a Row
                        _processDS.Tables[0].Rows[i][_g._dataPayroll.payroll_resource._society_insure_money] = social_security_max;
                        society_insure_money_employer = social_security_max;
                    }
                    else if (SocietyEmployer < social_security_min)
                    {
                        //Modify a Row
                        _processDS.Tables[0].Rows[i][_g._dataPayroll.payroll_resource._society_insure_money] = social_security_min;
                        society_insure_money_employer = social_security_min;
                    }
                    else
                    {
                        society_insure_money_employer = decimal.Parse(_processDS.Tables[0].Rows[i]["society_insure_money_employer"].ToString());
                    }
                    //Modify a Row
                    switch (ScreenName)
                    {

                        case _g.g._screenPayrollEnum.สปส_1_10_ส่วนที่1:
                        case _g.g._screenPayrollEnum.สปส_1_10_ส่วนที่2:
                            _processDS.Tables[0].Rows[i]["society_insure_money_employer"] = string.Format(_formatNumber, decimal.Parse(society_insure_money_employer.ToString()));
                            _processDS.Tables[0].Rows[i]["society_insure_money_customer"] = string.Format(_formatNumber, decimal.Parse(society_insure_money_customer.ToString())); 
                            break;
                      
                    }
                    //end 
                }
                return _processDS;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(" กรุณา ตรวจสอบ พนักงาน ที่ ยังไม่ได้ทำการบันทึก รายละเอียดการทำงาน ");                
            }
            return _processDS;
        }
    }
}
