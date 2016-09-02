using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DraftBook.AppDomain.Model
{
    [Serializable]
    public sealed class MarshalByValueType
    {
        private DateTime createTime = DateTime.Now;

        public MarshalByValueType()
        {
            Console.WriteLine("类型：[{0}]在AppDomain：[{1}]中被初始化，初始化时间为：[{2}]", GetType(), System.Threading.Thread.GetDomain().FriendlyName, createTime);
        }

        public void SomeMethod()
        {
            Console.WriteLine("SomeMethod方法在AppDomain：[{0}]中被调用", System.Threading.Thread.GetDomain().FriendlyName);
        }
    }
}
