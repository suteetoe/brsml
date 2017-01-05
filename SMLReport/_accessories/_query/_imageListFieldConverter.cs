using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace SMLReport
{
    class _imageListFieldConverter : StringConverter
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

            if (context.Instance.GetType() == typeof(SMLReport._design._drawImageField))
            {
                SMLReport._design._drawImageField __parentObject = (SMLReport._design._drawImageField)context.Instance;
                string[] __standardValue = __parentObject._getNameInImageList(__parentObject);
                if (__standardValue != null && __standardValue.Length > 0)
                    return new StandardValuesCollection(__standardValue);
            }

            return base.GetStandardValues(context);

        }
    }
}
