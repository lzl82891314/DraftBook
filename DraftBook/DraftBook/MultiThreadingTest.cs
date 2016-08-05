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
            TaskBusySymbol();
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
    }
}
