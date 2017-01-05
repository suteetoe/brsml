using System;
using System.Collections.Generic;
using System.Text;

namespace SMLPOSControl._designer._object
{
    public class _posImage : SMLReport._design._drawImage
    {
        private string _tagResult = "";
        public string _Tag
        {
            get { return _tagResult; }
            set { _tagResult = value; }
        }

        private string _objectIdResult = "";
        public string _Id
        {
            get { return _objectIdResult; }
            set { _objectIdResult = value; }
        }
        


        public _posImage()
        {
        }
    }
}
