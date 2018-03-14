using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DraftBook.Console
{
    public class InterviewQuestion
    {
        /// <summary>
        /// a.Equals(b)与a == b有什么区别
        /// </summary>
        public static void Question1()
        {
            int aInt = 1;
            int bInt = 1;
            System.Console.WriteLine($"aInt==bInt：{ aInt == bInt }");
            System.Console.WriteLine($"aInt.Equals(bInt)：{ aInt.Equals(bInt) }");

            string aStr = "abc";
            string bStr = "abc";
            System.Console.WriteLine($"aStr==bStr：{ aStr == bStr }");
            System.Console.WriteLine($"aStr.Equals(bStr)：{ aStr.Equals(bStr) }");

            EqualsTestClass aObj = new EqualsTestClass();
            EqualsTestClass bObj = new EqualsTestClass();
            System.Console.WriteLine($"aObj==bObj：{ aObj == bObj }");
            System.Console.WriteLine($"aObj.Equals(bObj)：{ aObj.Equals(bObj) }");

            EqualsTestClass cObj = new EqualsTestClass();
            EqualsTestClass dObj = cObj;
            System.Console.WriteLine($"cObj==dObj：{ cObj == dObj }");
            System.Console.WriteLine($"cObj.Equals(dObj)：{ cObj.Equals(dObj) }");
        }

        private class EqualsTestClass
        {

        }
    }
}
