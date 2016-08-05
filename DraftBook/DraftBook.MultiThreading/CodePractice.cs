using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace DraftBook.MultiThreading
{
    public class CodePractice
    {
        /// <summary>
        /// 使用线程池的概念输出100个"+"号
        /// </summary>
        public static void ThreadingPoolDemo()
        {
            int count = 100;
            //此处就类似于Thread thread = new Thread(委托); thread.Start();语句
            ThreadPool.QueueUserWorkItem((state) =>
            {
                for (int i = 0; i < count; i++)
                {
                    Console.WriteLine(state);
                }
            }, "+");
            //如果直接使用Thread，则方法之后应该调用Join方法等待其他线程完成
            //thread.Join();
        }

        /// <summary>
        /// 书中一个小示例：在轮询异步结果的时候主线程一直在Console中输出等待转圈标志
        /// 通过测试可以知道，该枚举是无限长的，所以使用到了yield return语句
        /// 该方法使用到了一个概念，IEnumerable枚举是在需要访问时才延迟执行的，所以在测试端使用的返回list其实就是一个指向该方法的一个引用。在最终遍历该枚举集合的时候才从方法中一个一个读取
        /// 这里又引出一个yield return语句的概念了。可以知道，这个枚举由于有while循环，所以不是每次访问都调用一个遍方法而是每次都从while循环体中查询数据。学到的知识点：yield return和循环连用返回IEnumberable
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<char> GetBusySymbols()
        {
            string busySymbolStr = @"-\|/-\|/";
            int nextIndex = 0;
            //int times = 100;
            while (true)
            {
                //输出旋转的每个光标
                yield return busySymbolStr[nextIndex];
                //移至下一个index位
                nextIndex = (nextIndex + 1) % busySymbolStr.Length;
                //输出退格符号
                yield return '\b';
            }
        }
    }
}
