using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace MyLib
{
    public static class _dataTableExtension
    {
        #region Select Distinct
        /// <summary>
        /// "SELECT DISTINCT" over a DataTable
        /// </summary>
        /// <param name="SourceTable">Input DataTable</param>
        /// <param name="FieldNames">Fields to select (distinct)</param>
        /// <returns></returns>
        public static DataTable _selectDistinct(DataTable SourceTable, String FieldName)
        {
            return _selectDistinct(SourceTable, FieldName, String.Empty);
        }

        /// <summary>
        ///"SELECT DISTINCT" over a DataTable
        /// </summary>
        /// <param name="SourceTable">Input DataTable</param>
        /// <param name="FieldNames">Fields to select (distinct)</param>
        /// <param name="Filter">Optional filter to be applied to the selection</param>
        /// <returns></returns>
        public static DataTable _selectDistinct(DataTable SourceTable, String FieldNames, String Filter)
        {
            DataTable dt = new DataTable();

            if (SourceTable.Columns.Count == 0)
                return dt;

            String[] arrFieldNames = FieldNames.Replace(" ", "").Split(',');
            foreach (String s in arrFieldNames)
            {
                if (SourceTable.Columns.Contains(s))
                    dt.Columns.Add(s, SourceTable.Columns[s].DataType);
                else
                    return null;
            }

            Object[] LastValues = null;
            //foreach (DataRow dr in SourceTable.Select(Filter, FieldNames))
            foreach (DataRow dr in SourceTable.Select(Filter))
            {
                Object[] NewValues = _getRowFields(dr, arrFieldNames);
                if (LastValues == null || !(_objectComparison(LastValues, NewValues)))
                {
                    LastValues = NewValues;
                    dt.Rows.Add(LastValues);
                }
            }

            return dt;
        }
        #endregion

        #region Private Methods
        private static Object[] _getRowFields(DataRow dr, String[] arrFieldNames)
        {
            if (arrFieldNames.Length == 1)
                return new Object[] { dr[arrFieldNames[0]] };
            else
            {
                ArrayList itemArray = new ArrayList();
                foreach (String field in arrFieldNames)
                    itemArray.Add(dr[field]);

                return itemArray.ToArray();
            }
        }

        /// <summary>
        /// Compares two values to see if they are equal. Also compares DBNULL.Value.
        /// </summary>
        /// <param name="A">Object A</param>
        /// <param name="B">Object B</param>
        /// <returns></returns>
        private static Boolean _objectComparison(Object a, Object b)
        {
            if (a == DBNull.Value && b == DBNull.Value) //  both are DBNull.Value
                return true;
            if (a == DBNull.Value || b == DBNull.Value) //  only one is DBNull.Value
                return false;
            return (a.Equals(b));  // value type standard comparison
        }

        /// <summary>
        /// Compares two value arrays to see if they are equal. Also compares DBNULL.Value.
        /// </summary>
        /// <param name="A">Object Array A</param>
        /// <param name="B">Object Array B</param>
        /// <returns></returns>
        private static Boolean _objectComparison(Object[] a, Object[] b)
        {
            Boolean retValue = true;
            Boolean singleCheck = false;

            if (a.Length == b.Length)
                for (Int32 i = 0; i < a.Length; i++)
                {
                    if (!(singleCheck = _objectComparison(a[i], b[i])))
                    {
                        retValue = false;
                        break;
                    }
                    retValue = retValue && singleCheck;
                }

            return retValue;
        }
        #endregion
    }
}
