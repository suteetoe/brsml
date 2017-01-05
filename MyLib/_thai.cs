using System;
using System.Collections.Generic;
using System.Text;

namespace MyLib
{
    public class _thai
    {
        public _resultClass _thai3Line(string strSource)
        {
            _resultClass __result = new _resultClass();
            byte[] __sourceFirst = Encoding.GetEncoding("windows-874").GetBytes(strSource);
            int __trial = 0;
            Char __lastChar = (char)0;
            StringBuilder __sourceProcess = new StringBuilder();
            // ผสมสระ
            while (__trial < __sourceFirst.Length - 1)
            {
                Char __char1 = (char)__sourceFirst[__trial];
                Char __char2 = (char)__sourceFirst[__trial + 1];
                Char __charNew = (char)0;
                if (__char1 == 209 || (__char1 >= 212 && __char1 <= 215))
                {
                    if (__char2 >= 232 && __char2 <= 236)
                    {
                        switch ((int)__char1)
                        {
                            case 209:
                                // ผสม ไม้หันอากาศ
                                __charNew = (char)(128 + (__char2 - 232));
                                break;
                            case 212:
                                // ผสมสระอิ ถึง สะระอี
                                __charNew = (char)(132 + (__char2 - 232));
                                break;
                            case 213:
                                // ผสมสระอิ ถึง สะระอี
                                __charNew = (char)(237 + (__char2 - 232));
                                break;
                            case 214:
                                // ผสมสระอิ ถึง สะระอึ
                                __charNew = (char)(141 + (__char2 - 232));
                                break;
                            case 215:
                                // ผสมสระอิ ถึง สะระอี
                                __charNew = (char)(145 + (__char2 - 232));
                                break;
                        }
                    }
                }
                if (__charNew != 0)
                {
                    __sourceProcess.Append(__charNew);
                    __trial++;
                }
                else
                {
                    __sourceProcess.Append(__char1);
                }
                __trial++;
            }
            // ยก
            byte[] __source = Encoding.GetEncoding("windows-874").GetBytes(__sourceProcess.ToString());
            while (__trial < __source.Length)
            {
                Char __char = (char)__source[__trial];
                if (__char <= 208 || __char == 210 || __char == 211 || (__char >= 223 && __char <= 230) || __char >= 239)
                {
                    // ตัวกลางปรกติ
                    __result._top.Append((char)32);
                    __result._center.Append(__char);
                    __result._buttom.Append((char)32);
                }
                else
                {
                    // สระอุ,สระอู
                    if (__char >= 216 && __char <= 217)
                    {
                        __result._buttom[__result._buttom.Length - 1] = __char;
                    }
                    else
                    {
                        // สระอิ ถึง สระอือ หรือ (ไม้เอก-ไม้จัตวา ที่ไม่มีการผสม)
                        if (__char == 209 || (__char >= 212 && __char <= 215) || (__char >= 231 && __char <= 236) || (__char >= 128 && __char <= 148))
                        {
                            __result._top[__result._top.Length - 1] = __char;
                        }
                    }
                }
                __lastChar = __char;
                __trial++;
            }
            return __result;
        }

        public class _resultClass
        {
            public StringBuilder _top = new StringBuilder();
            public StringBuilder _center = new StringBuilder();
            public StringBuilder _buttom = new StringBuilder();
        }
    }
}
