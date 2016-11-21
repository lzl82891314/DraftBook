using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DraftBook.DesignPattern
{
    /// <summary>
    /// 备忘录模式
    /// </summary>
    class MementoPattern
    {

    }

    #region 模式抽象
    /// <summary>
    /// 为了便于定义抽象状态类型所定义的接口
    /// </summary>
    public interface IState { }

    /// <summary>
    /// 抽象备忘录对象接口
    /// </summary>
    /// <typeparam name="T">IState</typeparam>

    public interface IMemento<T> where T : IState
    {
        T State { get; set; }
    }

    public abstract class MementoBase<T> : IMemento<T> where T : IState
    {
        public virtual T State { get; set; }
    }

    /// <summary>
    /// 抽象的发起者对象接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="M"></typeparam>
    public interface IOriginator<T, M> 
        where T : IState 
        where M : IMemento<T>, new()
    {
        IMemento<T> Memento { get; set; }
    }

    public abstract class OriginatorBase<T, M> : IOriginator<T, M>
        where T : IState
        where M : IMemento<T>, new()
    {
        //发起者对象的状态
        protected T state;

        //把状态保存到备忘录，或者从备忘录恢复之前的状态
        public virtual IMemento<T> Memento
        {
            get
            {
                return new M() { State = state };
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException();
                }
                state = value.State;
            }
        }
    }
    #endregion

    #region 具体实现
    public struct Position : IState
    {
        public int xPoint;
        public int yPoint;
    }

    public class Memento : MementoBase<Position> { }

    public class Originator : OriginatorBase<Position, Memento>
    {
        public void UpdateX(int x)
        {
            state.xPoint = x;
        }

        public void DecreaseX()
        {
            state.xPoint--;
        }

        public void IncreaseY()
        {
            state.yPoint++;
        }

        public Position Current
        {
            get
            {
                return state;
            }
        }
    }
    #endregion
}
