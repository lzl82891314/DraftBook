using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;
using System.Runtime.ExceptionServices;

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
            //string busySymbolStr = "9876543210";
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

        /// <summary>
        /// 测试yield return的功能
        /// </summary>
        /// <returns>foreach循环中输出的结果是1 2 3</returns>
        public static IEnumerable<int> YieldTest()
        {
            yield return 1;
            yield return 2;
            yield return 3;
        }

        /// <summary>
        /// 测试任务取消的功能CancellationToken
        /// </summary>
        public static void TaskCancelTest()
        {
            Console.Write("按Enter键结束：");
            CancellationTokenSource cancelTokeSource = new CancellationTokenSource();
            Task task = Task.Run(() => TodoSomeThing(cancelTokeSource.Token), cancelTokeSource.Token);
            Console.ReadLine();
            cancelTokeSource.Cancel();
            task.Wait();

            Console.WriteLine("任务已结束");

            Console.ReadKey();
        }

        private static void TodoSomeThing(CancellationToken cancelToken)
        {
            foreach (var item in GetBusySymbols())
            {
                if (cancelToken.IsCancellationRequested)
                {
                    break;
                }
                Console.Write(item);
            }
        }
        
        /// <summary>
        /// 自定义异步方法
        /// 使用异步方法运行命令行程序
        /// </summary>
        /// <returns></returns>
        public static Task<Process> RunProcessAsync(string fileName, string arguments = null, CancellationToken cancellationToken = default(CancellationToken), IProgress<ProcessProgressEventArgs> progress = null, object objectState = null)
        {
            TaskCompletionSource<Process> taskCS = new TaskCompletionSource<Process>();
            Console.WriteLine("RunProcessAsync:" + Thread.CurrentThread.ManagedThreadId);
            Process process = new Process()
            {
                StartInfo = new ProcessStartInfo(fileName)
                {
                    UseShellExecute =  false,
                    Arguments = arguments,
                    RedirectStandardOutput = progress != null
                },
                EnableRaisingEvents = true
            };

            process.Exited += (sender, localEventArgs) =>
            {
                taskCS.SetResult(process);
            };

            if (progress != null)
            {
                process.OutputDataReceived += (sender, localEventArgs) =>
                {
                    progress.Report(new ProcessProgressEventArgs(localEventArgs.Data, objectState));
                };
            }
            if (cancellationToken.IsCancellationRequested)
            {
                cancellationToken.ThrowIfCancellationRequested();
            }

            process.Start();

            if (progress != null)
            {
                process.BeginOutputReadLine();
            }

            cancellationToken.Register(() =>
            {
                process.CloseMainWindow();
                cancellationToken.ThrowIfCancellationRequested();
            });

            return taskCS.Task;
        }

        public static async Task TestAsync()
        {
            try
            {
                Console.WriteLine("TestAsync:" + Thread.CurrentThread.ManagedThreadId);
                var result = await RunProcessAsync(@"E:\BaiduYunDownload\wcf从入门到精通\视频教程\1.WCF出现的背景及快速搭建.exe");
                for (int i = 0; i < 100; i++)
                {
                    Console.WriteLine(i);
                }
                Console.WriteLine("Id:[{0}], SessionId:[{1}], ProcessName:[{2}], PriorityClass:[{3}], ExitTime:[{4}]", result.Id, result.SessionId, result.ProcessName, result.PriorityClass, result.ExitTime);
            }
            catch (AggregateException agEx)
            {
                agEx.Flatten().Handle(innerException =>
                {
                    Console.WriteLine(innerException.Message);
                    ExceptionDispatchInfo.Capture(agEx.InnerException).Throw();
                    return true;
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public class ProcessProgressEventArgs : EventArgs
        {
            public string Data { get; private set; }
            public object State { get; private set; }

            public ProcessProgressEventArgs(string data, object state)
            {
                Data = data;
                State = state;
            }

            public ProcessProgressEventArgs() : this(string.Empty, null)
            {

            }
        }
    }
}
