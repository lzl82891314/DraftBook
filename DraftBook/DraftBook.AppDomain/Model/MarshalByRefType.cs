using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DraftBook.AppDomain.Model
{
    public sealed class MarshalByRefType : MarshalByRefObject
    {
        public MarshalByRefType()
        {
            Console.WriteLine("类型：[{0}]在AppDomain：[{1}]中被初始化", GetType(), System.Threading.Thread.GetDomain().FriendlyName);
        }

        public void SomeMethod()
        {
            Console.WriteLine("SomeMethod方法在AppDomain：[{0}]中被调用", System.Threading.Thread.GetDomain().FriendlyName);
        }

        public MarshalByValueType ReturnValutType()
        {
            Console.WriteLine("ReturnValutType方法在AppDomain：[{0}]中被调用", System.Threading.Thread.GetDomain().FriendlyName);
            return new MarshalByValueType();
        }
    }
}
