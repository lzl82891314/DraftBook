using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DraftBook.Console
{
    public class LinqGroupbyTest
    {
        public static void Main(string[] args)
        {
            Test2();
            System.Console.ReadKey();
        }

        static void Test1()
        {
            System.Console.WriteLine(2001 / 1000);
            System.Console.WriteLine(3002 / 1000);
            System.Console.WriteLine(4999 / 1000);
        }

        static void Test2()
        {
            var testList = new List<TestModel>();
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    testList.Add(new TestModel() { Index = i, Value = j.ToString() });
                }
            }

            var result = from item in testList
                         let index = testList.IndexOf(item) / 1000
                         group item by index
                         into groupedItem
                         select groupedItem.Select(x => x.Value);

            System.Console.WriteLine(result);
        }

        private class TestModel
        {
            public int Index { get; set; }
            public string Value { get; set; }
        }
    }
}
