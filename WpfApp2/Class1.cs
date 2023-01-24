using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp2
{
    internal class Class1
    {
        public static Page1 Page1;
        public static Page2 Page2;
        public static Page3 Page3;
        public static Page1 GetPage1()
        {
            if (Page1 == null)
            {
                Page1 = new Page1();
            }
            return Page1;
        }
        public static Page2 GetPage2()
        {
            if (Page2 == null)
            {
                Page2 = new Page2();
            }
            return Page2;
        }
        public static Page3 GetPage3()
        {
            if (Page3 == null)
            {
                Page3 = new Page3();
            }
            return Page3;
        }
    }
}
