using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using DraftBook.AppDomain.Model;
using System.Runtime.Remoting;

namespace DraftBook.AppDomain
{
    public class CodePractice
    {
        public static void Marshalling()
        {
            System.AppDomain mainDomain = Thread.GetDomain();
            Console.WriteLine("默认AppDomain的FriendlyName为：[{0}]", mainDomain.FriendlyName);
            string assemblyName = Assembly.GetEntryAssembly().FullName;
            Console.WriteLine("主要程序集信息为：[{0}]", assemblyName);

            System.AppDomain newDomain = null;
            newDomain = System.AppDomain.CreateDomain("NewAppDomain", null, null);
            //默认AppDomain中的类型
            MarshalByRefType mbrt = null;

            //需要创建出代码所在的程序集
            var callingAssembly = Assembly.GetExecutingAssembly();
            mbrt = newDomain.CreateInstanceAndUnwrap(callingAssembly.FullName, "DraftBook.AppDomain.Model.MarshalByRefType") as MarshalByRefType;
            //作者说CLR在类型上撒了谎
            Console.WriteLine("Type={0}", mbrt.GetType());
            //证明得到的是对一个代理对象的引用，说明默认AppDomain上的类型实际上是一个代理类型
            Console.WriteLine("Is Proxy={0}", RemotingServices.IsTransparentProxy(mbrt));

            //看起来我们像是在MarshalByRefType上调用一个方法，其实不是这样的，我们是在代理类型上调用了一个方法，代理使线程切换到拥有对象的那个AppDomain，并在真实的对象上调用这个方法
            mbrt.SomeMethod();

            //调用返回值方法
            var resultObj = mbrt.ReturnValutType();

            //此处输出的是默认AppDomain的FriendlyName
            resultObj.SomeMethod();

            System.AppDomain.Unload(newDomain);

            try
            {
                mbrt.SomeMethod();
                Console.WriteLine("AppDomain卸载后任然调用成功");
            }
            catch (AppDomainUnloadedException)
            {
                Console.WriteLine("由于AppDomain已经卸载，所以调用失败");
            }
        }
    }
}
