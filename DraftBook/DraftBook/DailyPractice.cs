using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DraftBook.Console
{
    /// <summary>
    /// 日常练习
    /// </summary>
    class DailyPractice
    {
        private static string testStr = "";
        /// <summary>
        /// 简单测试一道笔试题：try中的return和finally谁先执行
        /// 结果：try中的return先执行，但是并没有直接返回而是将值暂存，然后执行finally，然后再返回之前保存的值
        /// 结论：先return后finally
        /// 附加：如果finally中有return，则方法会提前退出，会返回finally中的return的值而放弃之前暂存的try中return的值
        /// </summary>
        /// <returns></returns>
        static string SingleTest()
        {
            try
            {
                return "try";
            }
            finally
            {
                testStr = "finally";
            }
        }
    }
}
