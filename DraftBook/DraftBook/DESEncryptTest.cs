using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DraftBook.Console
{
    public class DESEncryptTest
    {
        public void Test(string text, string verifyCode)
        {
            var tempTime = GetTimestamp();
            long timestamp = long.Parse(text.Split('_')[2]);
            var dateTime = GetCurrentDateTime(timestamp);
            System.Console.WriteLine(dateTime);
            var encryptedStr = Encrypt(text, "esfInter");


            System.Console.WriteLine(text);
            System.Console.WriteLine(encryptedStr);
            System.Console.WriteLine(verifyCode);
        }

        private DateTime GetCurrentDateTime(long timestamp)
        {
            return new DateTime(1970, 1, 1, 8, 0, 0).AddMilliseconds(timestamp);
        }

        private long GetTimestamp()
        {
            return (long)(DateTime.Now - new DateTime(1970, 01, 01)).TotalMilliseconds;
        }

        /// <summary>
        /// 加密密码
        /// </summary>
        /// <param name="pToEncrypt">要加密的密码</param>
        /// <param name="sKey">加密密钥</param>
        public string Encrypt(string pToEncrypt, string sKey)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            //把字符串放到byte数组中
            byte[] inputByteArray = Encoding.Default.GetBytes(pToEncrypt);

            //建立加密对象的密钥和偏移量
            des.Key = Encoding.ASCII.GetBytes(sKey);
            des.IV = Encoding.ASCII.GetBytes(sKey);

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
