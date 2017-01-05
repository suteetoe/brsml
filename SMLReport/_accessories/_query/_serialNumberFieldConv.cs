using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Windows.Forms.Design;
using System.Reflection;
using System.Windows.Forms;

namespace SMLReport
{
    class _serialNumberFieldConv : StringConverter
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

                SMLReport._design._drawObject __parentObject = (SMLReport._design._drawObject)propertyGrid.SelectedObject;

                _formReport._formDesigner __owner = __parentObject._Owner as _formReport._formDesigner;

                if (propertyGrid.SelectedObject.GetType() == typeof(SMLReport._design._drawTable))
                {
                    string[] __standardValue = __owner.__getQueryFieldList(((SMLReport._design._drawTable)propertyGrid.SelectedObject).SerialQuery);

                    if (__standardValue != null && __standardValue.Length > 0)
                    {
                        return new StandardValuesCollection(__standardValue);
                    }
                }

            }

            return base.GetStandardValues(context);

        }

    }
}
