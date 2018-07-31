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

        /// <summary>
        /// 过桥问题，一座桥最多两人过，a过1分钟，b过2分钟，c过5分钟，d过10分钟，问最短的过桥时间
        /// </summary>
        public static void Question2()
        {
            int senderMin = 1;
            var customerArr = new int[] { 2, 5, 10 };
            int totalMin = 0;
            unsafe
            {
                fixed (int* index = customerArr)
                {
                    int* next = index;
                    do
                    {
                        totalMin += senderMin;
                        totalMin += *next;
                        next = next + 1;
                    }
                    while (*next > 0);
                }
            }

            System.Console.WriteLine(totalMin);
        }

        public static uint r()
        {
            var random = new Random().Next();
            if (random <= 0)
            {
                return r();
            }
            return (uint)random;
        }

        public static uint R(uint X, uint Y)
        {
            uint A;
            do
            {
                A = r();
            }
            while (A <= X || A > Y);
            return A;
        }


        private class EqualsTestClass
        {

        }
    }
}
