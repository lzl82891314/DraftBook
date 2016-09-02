using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;

namespace DraftBook.MultiThreading
{
    /// <summary>
    /// 数据流基础练习
    /// TPL数据流可以用来创建网格（mesh）和管道（pipleline），并通过它们以异步的方式发送数据
    /// </summary>
    public class DataFlow
    {
        /// <summary>
        /// 基础练习1，基本的数据流库类练习
        /// 创建网格时，需要把数据流块相互链接起来
        /// </summary>
        public static async Task BasePractice1()
        {
            try
            {
                var multiplyBlock = new TransformBlock<int, int>(item => item * 2);
                var subtractBlock = new TransformBlock<int, int>(item => item - 2);
                //对完成情况或者出错信息进行传递的对象
                var options = new DataflowLinkOptions() { PropagateCompletion = true };
                //链接数据流块，在建立链接后，从multiplyBlock出来的数据将进入substractBlock
                IDisposable linkObj = multiplyBlock.LinkTo(subtractBlock, options);

                //开始调用传递数据
                multiplyBlock.Post(1);
                multiplyBlock.Post(2);

                //第一个块的完成情况自动传递给第二个块
                multiplyBlock.Complete();

                //可以使用IDisposable断开链接
                linkObj.Dispose();
                await subtractBlock.Completion;
            }
            catch (AggregateException)
            {
                //发生的异常同样也是聚合异常
            }
        }
    }
}
