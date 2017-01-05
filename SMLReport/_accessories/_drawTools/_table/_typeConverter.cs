using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace SMLReport._design
{
    internal class _tableColumnsCollectionConverter : ExpandableObjectConverter
    {
        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string) && value is _tableColumnsCollection)
            {
                if (context.Instance.GetType() == typeof(_drawTable))
                {
                    _drawTable __activeTool = (_drawTable) context.Instance;

                    // set default field
                    string[] __standardValue = __activeTool._getFieldStandardValue(__activeTool);

                    // set default image list
                    string[] __imageNameList = __activeTool._getNameInImageList(__activeTool);

                    _tableColumnsCollection __collection = (_tableColumnsCollection)value;
                    for (int __i = 0; __i < __collection.Count; __i++)
                    {
                        ((_tableColumns)__collection[__i]).__defaultValueTmp = __standardValue;
                        ((_tableColumns)__collection[__i])._nameImageListResultTmp = __imageNameList;
                    }

                }

                return "(Collection)";
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }

    internal class _tableFootersCollectionConverter : ExpandableObjectConverter
    {
        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string) && value is _tableFootersCollection)
            {
                if (context.Instance.GetType() == typeof(_drawTable))
                {
                    _drawTable __activeTool = (_drawTable)context.Instance;

                    // set default field
                    string[] __standardValue = __activeTool._getFieldStandardValue(__activeTool);


                    _tableFootersCollection __collection = (_tableFootersCollection)value;
                    for (int __i = 0; __i < __collection.Count; __i++)
                    {
                        ((_tableFooters)__collection[__i]).__defaultValueTmp = __standardValue;
                    }

                }

                return "(Collection)";
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }

    internal class _tableColumnsConverter : ExpandableObjectConverter
    {
        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string) && value is _tableColumns)
            {
                return value.ToString();
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }

    internal class _tableFooterConverter : ExpandableObjectConverter
    {
        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string) && value is _tableFooters)
            {
                return value.ToString();
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }

}
