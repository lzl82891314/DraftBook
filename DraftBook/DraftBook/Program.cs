using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DraftBook.Console
{
    /// <summary>
    /// 草稿本总启动方法
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            #region 加密方法测试
            //EncryptTest();
            //Base64EncryptTest();
            //EncryptTestForErrData();
            //DecryptTest();
            #endregion

            #region C#6语法
            ////新版本的String.Format()方法
            //var dateStr = $"{ DateTime.Now: yyyy-MM-dd}";
            //var nameStr = "Lizhenglong";
            //var testStr = $"my name is {nameStr}";
            //System.Console.WriteLine(testStr);//my name is Lizhenglong
            ////nameof运算符，可以获取变量的名称
            //var yytObj = new object();
            //var yytStr = nameof(yytObj);
            //System.Console.WriteLine(yytStr);//yytObj
            ////带索引的对象初始化器
            //var testDic = new Dictionary<int, string>() { [1] = "李正龙", [10] = "颜贻通", [11] = "田贺" };
            //foreach (var item in testDic)
            //{
            //    System.Console.Write("[Index={0},Value={1}] ", item.Key, item.Value);
            //}
            #endregion

            #region 引用 DotNetCore dll
            //new ConsoleApplication.Program().TestFunc("Test");

            #endregion

            #region 枚举测试
            //var result = Enum.GetName(typeof(TestEnum), 0);
            //System.Console.WriteLine(result);
            #endregion

            #region 笔试题验证
            //InterviewQuestion.Question1();

            var timestamp = (long)(DateTime.Now - TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 01, 01))).TotalMilliseconds;
            #endregion

            System.Console.ReadKey();
        }

        #region 加密方法测试
        static void EncryptTest()
        {
            var encryptedStr = EncryptTool.EncryptHelper.DESEncrypt("25052@@测试公司啊@@203@@北京@@2@@1@@0.01@@86164705@@测试公司啊@@4", "Home.Eop");
            System.Console.WriteLine(encryptedStr ?? "加密失败");
            if (encryptedStr != null)
            {
                var decryptedStr = EncryptTool.EncryptHelper.DESDecrypt(encryptedStr, "Home.Eop");
                System.Console.WriteLine(decryptedStr);
            }

            System.Console.ReadKey();
        }

        /// <summary>
        /// 测试未加密字符串解密结果
        /// 结果信息：异常
        /// </summary>
        static void EncryptTestForErrData()
        {
            string encryptedStr = "False@@7336@@上海悠悠建筑装饰工程有限公司@@202@@上海@@2@@8@@150@@电子合同章@@商户编号为：[7336]商户名称为：[上海悠悠建筑装饰工程有限公司]的商户购买[电子合同章]@@78803547@@悠悠建筑装饰@@0@@4";
            var decrpytedStr = EncryptTool.EncryptHelper.DESDecrypt(encryptedStr, "Home.Eop");
            System.Console.WriteLine(decrpytedStr);
        }

        static void DecryptTest()
        {
            string encryptedStr = "6cyDbgjzcfA3IcTOdh++OU3eKaI6fs3Ltm7FRoRg22EISluUBuGCivHYlWUjOmjjDfqOwdlL8fti+nI8jmqBUnXyv3eOQZH8E6cMi2dHnRk=";
            var decryptedStr = EncryptTool.EncryptHelper.DESDecrypt(encryptedStr, "Home.Eop");
            System.Console.WriteLine(decryptedStr);
        }

        static void Base64EncryptTest()
        {
            var encryptedStr = EncryptTool.EncryptHelper.Base64Code("5334@@测试充值@@203@@北京@@2@@1@@0.01@@商家Id为：[5334]，商家名称为：[测试充值]的商家[预存款]充值[0.01]元@@85517330@@测试充值@@5");
            System.Console.WriteLine(encryptedStr);
            var sourceStr = EncryptTool.EncryptHelper.Base64Decode(encryptedStr);
            System.Console.WriteLine(sourceStr);
            System.Console.ReadKey();
        }
        #endregion
    }

    public enum TestEnum
    {
        全部 = -1,
        测试数据1 = 1,
        测试数据2 = 2
    }
}
