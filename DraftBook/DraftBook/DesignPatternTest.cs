using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DraftBook.DesignPattern;

namespace DraftBook.Console
{
    public class DesignPatternTest
    {
        static void Main(string[] args)
        {
            MediatorTest();

            System.Console.ReadKey();
        }

        static void MediatorTest()
        {
            var a = new ClassA();
            var b = new ClassB();
            var c = new ClassC();

            a.Changed += b.OnChanged;
            a.Changed += c.OnChanged;

            //由于重写了Data的set方法，在Set的时候会触发Changed事件，所以此时b.Data 和 c.Data都是20
            a.Data = 20;

            //更改协作关系
            a.Changed -= c.OnChanged;
            //此时将C解绑，所以a.Data 和 b.Data 都是30，C不变
            a.Data = 30;

        }
    }
}
