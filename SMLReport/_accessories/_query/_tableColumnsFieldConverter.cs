using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Reflection;

namespace SMLReport
{
    class _tableColumnsFieldConverter : StringConverter
    {
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return true;
        }

        public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            var editorService = context.GetService(typeof(IWindowsFormsEditorService)) as IWindowsFormsEditorService;
            var editorServiceType = editorService.GetType();
            var ownerGridField = editorServiceType.GetField("ownerGrid", BindingFlags.Instance | BindingFlags.NonPublic);
            var propertyGrid = ownerGridField.GetValue(editorService) as PropertyGrid;

            if (propertyGrid.SelectedObject.GetType() == typeof(SMLReport._design._drawTable))
            {

                SMLReport._design._drawTable __parentObject = (SMLReport._design._drawTable)propertyGrid.SelectedObject;


                string[] __standardValue = __parentObject._getFieldStandardValue(__parentObject);
                if (__standardValue != null && __standardValue.Length > 0)
                {
                    // add standard value to column object
                    for (int __i = 0; __i < __parentObject.Columns.Count; __i++)
                    {
                        __parentObject.Columns[__i].__defaultValueTmp = __standardValue;
                    }

                    return new StandardValuesCollection(__standardValue);
                }

            }

            if (propertyGrid.SelectedObject.GetType() == typeof(SMLReport._design._tableColumns))
            {
                SMLReport._design._tableColumns __column = (SMLReport._design._tableColumns) propertyGrid.SelectedObject;

                if (__column.__defaultValueTmp != null && __column.__defaultValueTmp.Length > 0)
                    return new StandardValuesCollection(__column.__defaultValueTmp);
            }


            //if (__propertyGrid.SelectedObject.GetType() == typeof(SMLReport._design._drawTable))
            //{
            //    SMLReport._design._drawTable __tableObject = (SMLReport._design._drawTable)__propertyGrid.SelectedObject;

            //    if (_reportUtility._queryRule.Count > 0)
            //    {
            //        SMLReport._formReport.query __tmpQuery = (SMLReport._formReport.query)_reportUtility._queryRule[(int)__tableObject.DataQuery];

            //        string[] __listfield = new string[__tmpQuery._fieldList.Count];

            //        for (int i = 0; i < __tmpQuery._fieldList.Count; i++)
            //        {
            //            SMLReport._formReport.queryField __tmpField = (SMLReport._formReport.queryField)__tmpQuery._fieldList[i];
            //            __listfield[i] = "[" + __tmpField.FieldName.ToString() + "]";
            //        }

            //        return new StandardValuesCollection(__listfield);
            //    }

            //}

            return base.GetStandardValues(context);

        }
    }
}
