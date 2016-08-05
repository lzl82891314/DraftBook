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
            EncryptTestForErrData();
            #endregion
        }

        #region 加密方法测试
        static void EncryptTest()
        {
            var encryptedStr = EncryptTool.EncryptHelper.DESEncryptString("False@@7336@@上海悠悠建筑装饰工程有限公司@@202@@上海@@2@@8@@150@@电子合同章@@商户编号为：[7336]商户名称为：[上海悠悠建筑装饰工程有限公司]的商户购买[电子合同章]@@78803547@@悠悠建筑装饰@@0@@4", "Home.Eop");
            System.Console.WriteLine(encryptedStr ?? "加密失败");
            if (encryptedStr != null)
            {
                var decryptedStr = EncryptTool.EncryptHelper.DESDecryptString(encryptedStr, "Home.Eop");
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
}
