using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DraftBook.MultiThreading;

namespace DraftBook.Console
{
    class MultiThreadingTest
    {
        static void Main(string[] args)
        {
            //TaskBusySymbol();
            //YieldTest();
            //CancelTest();
            //AsyncMethodBySelfDefinedTest();
            DataFlowPractice1();
        }

        static void TaskBusySymbol()
        {
            var busySymbolList = CodePractice.GetBusySymbols();
            foreach (var item in busySymbolList)
            {
                System.Console.Write(item);
            }
            System.Console.ReadKey();

        }

        static void YieldTest()
        {
            foreach (var item in CodePractice.YieldTest())
            {
                System.Console.WriteLine(item);
            }
            System.Console.ReadKey();
        }

        static void CancelTest()
        {
            CodePractice.TaskCancelTest();
        }

        static void AsyncMethodBySelfDefinedTest()
        {
            CodePractice.TestAsync().GetAwaiter().GetResult();
        }

        static void DataFlowPractice1()
        {
            DataFlow.BasePractice1().GetAwaiter().GetResult();
        }
    }
}
