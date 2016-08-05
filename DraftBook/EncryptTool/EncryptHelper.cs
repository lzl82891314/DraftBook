using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DraftBook.EncryptTool
{
    public class EncryptHelper
    {
        #region DES加密
        public static string DESEncrypt(string sourceStr, string key, string charset = "utf-8", Action<string> logFunc = null)
        {
            try
            {
                var encodingObj = Encoding.GetEncoding(charset);
                var keyArr = encodingObj.GetBytes(key);
                var ivArr = encodingObj.GetBytes(key);

                DESCryptoServiceProvider desObj = new DESCryptoServiceProvider();
                using (var msObj = new MemoryStream())
                {
                    var sourceArr = encodingObj.GetBytes(sourceStr);
                    try
                    {
                        using (var csObj = new CryptoStream(msObj, desObj.CreateEncryptor(keyArr, ivArr), CryptoStreamMode.Write))
                        {
                            csObj.Write(sourceArr, 0, sourceArr.Length);
                            csObj.Flush();
                        }

                        return Convert.ToBase64String(msObj.ToArray());
                    }
                    catch (Exception innerEx)
                    {
                        logFunc?.Invoke(innerEx.Message);
                        throw innerEx;
                    }
                }
            }
            catch (Exception ex)
            {
                logFunc?.Invoke(ex.Message);
                throw ex;
            }
        }

        public static async Task<string> DESEncryptAsync(string sourceStr, string key, string charset = "utf-8", Action<string> logFunc = null)
        {
            try
            {
                var encodingObj = Encoding.GetEncoding(charset);
                var keyArr = encodingObj.GetBytes(key);
                var ivArr = encodingObj.GetBytes(key);

                DESCryptoServiceProvider desObj = new DESCryptoServiceProvider();
                using (var msObj = new MemoryStream())
                {
                    var sourceArr = encodingObj.GetBytes(sourceStr);
                    try
                    {
                        using (var csObj = new CryptoStream(msObj, desObj.CreateEncryptor(keyArr, ivArr), CryptoStreamMode.Write))
                        {
                            await csObj.WriteAsync(sourceArr, 0, sourceArr.Length);
                            await csObj.FlushAsync();
                        }

                        return Convert.ToBase64String(msObj.ToArray());
                    }
                    catch (Exception innerEx)
                    {
                        logFunc?.Invoke(innerEx.Message);
                        throw innerEx;
                    }
                }
            }
            catch (Exception ex)
            {
                logFunc?.Invoke(ex.Message);
                throw ex;
            }
        }

        public static string DESDecrypt(string encryptedStr, string key, string charset = "utf-8", Action<string> logFunc = null)
        {
            try
            {
                var encodingObj = Encoding.GetEncoding(charset);
                var keyArr = encodingObj.GetBytes(key);
                var ivArr = encodingObj.GetBytes(key);

                DESCryptoServiceProvider desObj = new DESCryptoServiceProvider();
                using (var msObj = new MemoryStream())
                {
                    var encryptedArr = Convert.FromBase64String(encryptedStr);
                    try
                    {
                        using (var csObj = new CryptoStream(msObj, desObj.CreateDecryptor(keyArr, ivArr), CryptoStreamMode.Write))
                        {
                            csObj.Write(encryptedArr, 0, encryptedArr.Length);
                            csObj.Flush();
                        }
                        return encodingObj.GetString(msObj.ToArray());
                    }
                    catch (Exception innerEx)
                    {
                        logFunc?.Invoke(innerEx.Message);
                        throw innerEx;
                    }
                }
            }
            catch (Exception ex)
            {
                logFunc?.Invoke(ex.Message);
                throw ex;
            }
        }

        public static async Task<string> DESDecryptAsync(string encryptedStr, string key, string charset = "utf-8", Action<string> logFunc = null)
        {
            try
            {
                var encodingObj = Encoding.GetEncoding(charset);
                var keyArr = encodingObj.GetBytes(key);
                var ivArr = encodingObj.GetBytes(key);

                DESCryptoServiceProvider desObj = new DESCryptoServiceProvider();
                using (var msObj = new MemoryStream())
                {
                    var encryptedArr = Convert.FromBase64String(encryptedStr);
                    try
                    {
                        using (var csObj = new CryptoStream(msObj, desObj.CreateDecryptor(keyArr, ivArr), CryptoStreamMode.Write))
                        {
                            await csObj.WriteAsync(encryptedArr, 0, encryptedArr.Length);
                            await csObj.FlushAsync();
                        }
                        return encodingObj.GetString(msObj.ToArray());
                    }
                    catch (Exception innerEx)
                    {
                        logFunc?.Invoke(innerEx.Message);
                        throw innerEx;
                    }
                }
            }
            catch (Exception ex)
            {
                logFunc?.Invoke(ex.Message);
                throw ex;
            }
        }
        #endregion

        public static string DESEncryptString(string sourceStr, string key, string charset = "utf-8", Action<string> logFunc = null)
        {
            try
            {
                var encodingObj = Encoding.GetEncoding(charset);
                var sourceArr = encodingObj.GetBytes(sourceStr);
                DESCryptoServiceProvider desObj = new DESCryptoServiceProvider();
                desObj.Key = encodingObj.GetBytes(key);
                desObj.IV = encodingObj.GetBytes(key);
                ICryptoTransform desEncrypt = desObj.CreateEncryptor();
                var resultArr = desEncrypt.TransformFinalBlock(sourceArr, 0, sourceArr.Length);
                return BitConverter.ToString(resultArr);
            }
            catch (Exception ex)
            {
                logFunc?.Invoke(ex.Message);
                return null;
            }
        }

        public static string DESDecryptString(string encryptedStr, string key, string charset = "utf-8", Action<string> logFunc = null)
        {
            try
            {
                var encryptedArr = encryptedStr.Split('-');
                var encryptedByteArr = new byte[encryptedArr.Length];
                for (int i = 0; i < encryptedArr.Length; i++)
                {
                    encryptedByteArr[i] = byte.Parse(encryptedArr[i], NumberStyles.HexNumber);
                }
                var encodingObj = Encoding.GetEncoding(charset);
                DESCryptoServiceProvider desObj = new DESCryptoServiceProvider();
                desObj.Key = encodingObj.GetBytes(key);
                desObj.IV = encodingObj.GetBytes(key);
                ICryptoTransform desencrypt = desObj.CreateDecryptor();
                var resultArr = desencrypt.TransformFinalBlock(encryptedByteArr, 0, encryptedByteArr.Length);
                return encodingObj.GetString(resultArr);
            }
            catch (Exception ex)
            {
                logFunc?.Invoke(ex.Message);
                return null;
            }
        }


        #region Base64加密
        ///<summary>
        ///Base64加密
        ///</summary>
        ///<paramname="Message"></param>
        ///<returns></returns>
        public static string Base64Code(string Message)
        {
            char[] Base64Code = new char[]
            {
                'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T',
                'U','V','W','X','Y','Z','a','b','c','d','e','f','g','h','i','j','k','l','m','n',
                'o','p','q','r','s','t','u','v','w','x','y','z','0','1','2','3','4','5','6','7',
                '8','9','+','/','='
            };
            byte empty = (byte)0;
            System.Collections.ArrayList byteMessage = new System.Collections.ArrayList(System.Text.Encoding.Default.GetBytes(Message));
            System.Text.StringBuilder outmessage;
            int messageLen = byteMessage.Count;
            //将字符分成3个字节一组，如果不足，则以0补齐
            int page = messageLen / 3;
            int use = 0;
            if ((use = messageLen % 3) > 0)
            {
                for (int i = 0; i < 3 - use; i++)
                    byteMessage.Add(empty);
                page++;
            }
            //将3个字节的每组字符转换成4个字节一组的。3个一组，一组一组变成4个字节一组
            //方法是：转换成ASCII码，按顺序排列24位数据，再把这24位数据分成4组，即每组6位。再在每组的的最高位前补两个0凑足一个字节。
            outmessage = new System.Text.StringBuilder(page * 4);
            for (int i = 0; i < page; i++)
            {
                //取一组3个字节的组
                byte[] instr = new byte[3];
                instr[0] = (byte)byteMessage[i * 3];
                instr[1] = (byte)byteMessage[i * 3 + 1];
                instr[2] = (byte)byteMessage[i * 3 + 2];
                //六个位为一组，补0变成4个字节
                int[] outstr = new int[4];
                //第一个输出字节：取第一输入字节的前6位，并且在高位补0，使其变成8位（一个字节）
                outstr[0] = instr[0] >> 2;
                //第二个输出字节：取第一输入字节的后2位和第二个输入字节的前4位（共6位），并且在高位补0，使其变成8位（一个字节）
                outstr[1] = ((instr[0] & 0x03) << 4) ^ (instr[1] >> 4);
                //第三个输出字节：取第二输入字节的后4位和第三个输入字节的前2位（共6位），并且在高位补0，使其变成8位（一个字节）
                if (!instr[1].Equals(empty))
                    outstr[2] = ((instr[1] & 0x0f) << 2) ^ (instr[2] >> 6);
                else
                    outstr[2] = 64;
                //第四个输出字节：取第三输入字节的后6位，并且在高位补0，使其变成8位（一个字节）
                if (!instr[2].Equals(empty))
                    outstr[3] = (instr[2] & 0x3f);
                else
                    outstr[3] = 64;
                outmessage.Append(Base64Code[outstr[0]]);
                outmessage.Append(Base64Code[outstr[1]]);
                outmessage.Append(Base64Code[outstr[2]]);
                outmessage.Append(Base64Code[outstr[3]]);
            }
            return outmessage.ToString();
        }

        ///<summary>
        ///Base64解密
        ///</summary>
        ///<paramname="Message"></param>
        ///<returns></returns>
        public static string Base64Decode(string Message)
        {
            if ((Message.Length % 4) != 0)
            {
                throw new ArgumentException("不是正确的BASE64编码，请检查。", "Message");
            }
            if (!System.Text.RegularExpressions.Regex.IsMatch(Message, "^[A-Z0-9/+=]*$", System.Text.RegularExpressions.RegexOptions.IgnoreCase))
            {
                throw new ArgumentException("包含不正确的BASE64编码，请检查。", "Message");
            }
            string Base64Code = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=";
            int page = Message.Length / 4;
            System.Collections.ArrayList outMessage = new System.Collections.ArrayList(page * 3);
            char[] message = Message.ToCharArray();
            for (int i = 0; i < page; i++)
            {
                byte[] instr = new byte[4];
                instr[0] = (byte)Base64Code.IndexOf(message[i * 4]);
                instr[1] = (byte)Base64Code.IndexOf(message[i * 4 + 1]);
                instr[2] = (byte)Base64Code.IndexOf(message[i * 4 + 2]);
                instr[3] = (byte)Base64Code.IndexOf(message[i * 4 + 3]);
                byte[] outstr = new byte[3];
                outstr[0] = (byte)((instr[0] << 2) ^ ((instr[1] & 0x30) >> 4));
                if (instr[2] != 64)
                {
                    outstr[1] = (byte)((instr[1] << 4) ^ ((instr[2] & 0x3c) >> 2));
                }
                else
                {
                    outstr[2] = 0;
                }
                if (instr[3] != 64)
                {
                    outstr[2] = (byte)((instr[2] << 6) ^ instr[3]);
                }
                else
                {
                    outstr[2] = 0;
                }
                outMessage.Add(outstr[0]);
                if (outstr[1] != 0)
                    outMessage.Add(outstr[1]);
                if (outstr[2] != 0)
                    outMessage.Add(outstr[2]);
            }
            byte[] outbyte = (byte[])outMessage.ToArray(Type.GetType("System.Byte"));
            return System.Text.Encoding.Default.GetString(outbyte);
        }
        #endregion
    }
}
