using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace SMLReport
{
    class _fieldConverter : StringConverter
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

            if ((context.Instance.GetType() == typeof(SMLReport._design._drawTextField)) || (context.Instance.GetType() == typeof(SMLReport._design._drawImageField)))
            {
                SMLReport._design._drawObject __parentObject = (SMLReport._design._drawObject)context.Instance;
                string[] __standardValue = __parentObject._getFieldStandardValue(__parentObject);
                if (__standardValue != null && __standardValue.Length > 0)
                    return new StandardValuesCollection(__standardValue);
            }

            return base.GetStandardValues(context);

        }
    }
}
