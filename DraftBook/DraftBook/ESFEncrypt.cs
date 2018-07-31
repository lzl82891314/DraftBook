using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DraftBook.Console
{
    public class ESFEncrypt
    {
        public static void Main(string[] args)
        {
            var paramList = new List<string>()
            {
                "上海", //城市
                "77778263", //B端Id
                "1532691320000" //时间戳
            };


            var verifyCode = GetVerifyCode(paramList);
            System.Console.WriteLine(verifyCode);
        }

        static string GetVerifyCode(List<string> paramList)
        {
            string strEncrypt = string.Join("_", paramList);
            string strKey = "esfInter";
            return Encrypt(strEncrypt, strKey);
        }

        /// <summary>
        /// 加密密码
        /// </summary>
        /// <param name="pToEncrypt">要加密的密码</param>
        /// <param name="sKey">加密密钥</param>
        public static string Encrypt(string pToEncrypt, string sKey)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            //把字符串放到byte数组中
            byte[] inputByteArray = Encoding.Default.GetBytes(pToEncrypt);

            //建立加密对象的密钥和偏移量
            des.Key = ASCIIEncoding.ASCII.GetBytes(sKey);
            des.IV = ASCIIEncoding.ASCII.GetBytes(sKey);

            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();

            StringBuilder ret = new StringBuilder();
            foreach (byte b in ms.ToArray())
            {
                ret.AppendFormat("{0:X2}", b);
            }
            return ret.ToString();
        }
    }
}
