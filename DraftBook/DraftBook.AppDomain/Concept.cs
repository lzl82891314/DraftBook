using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DraftBook.AppDomain
{
    /// <summary>
    /// 对于CLR功能AppDomain的一些逻辑概念
    /// </summary>
    class Concept
    {
        //初步了解的概念：
        //1、AppDomain是一个CLR创建的一个程序集的逻辑容器，其功能就是对程序集进行逻辑隔离，保证当前AppDomain中运行的程序如果异常了不会影响到其他AppDomain中的逻辑。因为是CLR的功能，所以Windows不认识这个概念。
        //2、在CLR COM服务器初始化的时候会创建一个默认的AppDomain，因此，在我没有AppDomain概念的时候，所以的程序逻辑都是写在这个默认的AppDomain中的。并且这个AppDomain只有在Windows终止进程的时候才会被销毁。
        //3、一个进程可以创建多个AppDomain，线程和AppDomain没有一一对应的关系，但是一个线程在同一时刻只能执行一个AppDomain中的代码。并且我们在CLR中使用的线程其实不是Windows的实际线程，而是被AppDomain软封装之后的线程。
        //4、AppDomain可以被创建、卸载，并且可以跨边界访问其他AppDomain中的对象，有两种方法可以跨边界访问对象：MarshalByRefType按引用封送以及MarshalByValueType按值类型封送（被调用的类型需要继承上述的两个类）。

        //书中列出的关于AppDomain的一个功能：
        //1、隔离，一个AppDomain的代码不能直接访问另一个AppDomain的代码创建的对象。但是可以通过上述的两种方法进行跨边界访问；
        //2、AppDomain可以卸载。CLR不支持从AppDomain中卸载程序集，但是可以直接卸载AppDomain从而达到卸载包含在内的所有程序集的功能；
        //3、AppDomain可以单独保护。AppDomain创建后会应用一个权限集，它决定了向这个AppDomain中运行的程序集授予的最大权限；
        //4、AppDomain可以单独配置。

        //一些进阶的概念：
        //1、AppDomain创建之后会有自己的Loader堆，每个Loader堆都记录了自AppDomain从创建以来访问过哪些类型。
        //2、如果两个AppDomain需要同时加载一个相同的程序集，如System.dll，则两个AppDomain的Loader堆会分别分配一个该类型的对象。
        //3、有些类型天生就需要被多个程序集所共享，比如MSCorLib.dll，其中包含了System.Int32和System.Object等类型，所以这种程序集会以"AppDomain中立"的形式存在被多个AppDomain所共享，但是有一个问题，这个"AppDomain中立"不能被卸载，只有进程结束时才会被销毁。



    }
}
