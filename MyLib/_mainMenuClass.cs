using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.Collections;
namespace MyLib
{
    public class _mainMenuClass
    {

        [XmlArrayItem(ElementName = "_MainMenuList", Type = typeof(_menuListClass))]
        public ArrayList _MainMenuList = new ArrayList();
    }

    [Serializable]
    public class _menuListClass
    {
        [XmlAttribute]
        public String _menuMainname;
        [XmlArrayItem(ElementName = "_MenusubList", Type = typeof(_submenuListClass))]
        public ArrayList _munsubList = new ArrayList();
    }
    [Serializable]
    public class _submenuListClass
    {
        [XmlAttribute]
        public String _submeid;
        [XmlAttribute]
        public String _submenuname1;
        [XmlAttribute]
        public bool _isRead;
        [XmlAttribute]
        public bool _isAdd;
        [XmlAttribute]
        public bool _isEdit;
        [XmlAttribute]
        public bool _isDelete;
    }
}
