using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DraftBook.DesignPattern
{
    /// <summary>
    /// 中介者模式
    /// </summary>
    class MediatorPattern
    {
        //中介者模式的目的就是为了解决接口M:N的交互关系。产生一个中介为这些结果进行统一的管理。
        //个人理解的在SOA架构中，dubbo框架的作用就是一个大的中介者模式
        //作者在后文中也提到了：SOA环境中经常进行服务总线内部的消息路由以及服务总线的级联，从模式的角度看，他们都是具有"接力"能力中介者的典型应用。所以推翻上述的结论，只是总线的基本功能

        //使用C#实现：C#在实现自定义事件的时候，就已经潜移默化地实现了命令模式、中介者模式和观察者模式

        //小结：相对于之前的外观模式，中介者模式解耦的是"子系统"内部或者子系统内外间的调用关系，它的关键作用不是封装为统一的接口，而是减少因为调用带来的依赖关系
    }

    public class DataEventArgs<T> : EventArgs
    {
        public T Data;
        public DataEventArgs(T data)
        {
            Data = data;
        }
    }

    public abstract class ColleagueBase<T>
    {
        public virtual T Data { get; set; }
        public virtual void OnChanged(object sender, DataEventArgs<T> args)
        {
            Data = args.Data;
        }
    }

    public class ClassA : ColleagueBase<int>
    {
        public event EventHandler<DataEventArgs<int>> Changed;

        public override int Data
        {
            get
            {
                return base.Data;
            }

            set
            {
                base.Data = value;
                //把消息抛给作为中介的.NET事件机制
                Changed(this, new DataEventArgs<int>(value));
            }
        }
    }

    public class ClassB : ColleagueBase<int> { }
    public class ClassC : ColleagueBase<int> { }
}
