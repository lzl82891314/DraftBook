using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DraftBook.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            #region 加密方法测试
            EncryptTest();
            #endregion
        }

        static void EncryptTest()
        {
            var encryptedStr = EncryptTool.EncryptHelper.DESEncrypt("加密方法测试", "Home.Eop", "Home.Eop").Result;
            System.Console.WriteLine(encryptedStr ?? "加密失败");
            if (encryptedStr != null)
            {
                var decryptedStr = EncryptTool.EncryptHelper.DESDecrypt(encryptedStr, "Home.Eop", "Home.Eop").Result;
                System.Console.WriteLine(decryptedStr);
            }

            System.Console.ReadKey();
        }
    }
}
