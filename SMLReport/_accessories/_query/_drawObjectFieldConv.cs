using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Windows.Forms.Design;
using System.Reflection;
using System.Windows.Forms;

namespace SMLReport
{
    class _drawObjectFieldConv : StringConverter
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
                SMLReport._design._drawTable __tableObj = (SMLReport._design._drawTable)propertyGrid.SelectedObject;
                _formReport._formDesigner __owner = __tableObj._Owner as _formReport._formDesigner;

                string[] __standardValue = __owner.__getQueryFieldList(__tableObj.DataQuery);

                if (__standardValue != null && __standardValue.Length > 0)
                {
                    return new StandardValuesCollection(__standardValue);
                }
            }

            if (propertyGrid.SelectedObject.GetType() == typeof(SMLReport._design._drawTextField))
            {
                SMLReport._design._drawTextField __textFieldObj = (SMLReport._design._drawTextField)propertyGrid.SelectedObject;
                _formReport._formDesigner __owner = __textFieldObj._Owner as _formReport._formDesigner;

                string[] __standardValue = __owner.__getQueryFieldList(__textFieldObj.query);

                if (__standardValue != null && __standardValue.Length > 0)
                {
                    return new StandardValuesCollection(__standardValue);
                }

            }

            return base.GetStandardValues(context);

        }

    }
}
