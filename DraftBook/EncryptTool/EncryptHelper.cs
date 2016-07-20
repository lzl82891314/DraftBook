using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DraftBook.EncryptTool
{
    public class EncryptHelper
    {
        public static async Task<string> DESEncrypt(string sourceStr, string key, string iv, string charset = "utf-8")
        {
            try
            {
                var encodingObj = Encoding.GetEncoding(charset);
                var keyArr = encodingObj.GetBytes(key);
                var ivArr = encodingObj.GetBytes(iv);

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
                        //LogFunc(innerEx.Message);
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                //LogFunc(ex.Message);
                return null;
            }
        }

        public static async Task<string> DESDecrypt(string encryptedStr, string key, string iv, string charset = "utf-8")
        {
            try
            {
                var encodingObj = Encoding.GetEncoding(charset);
                var keyArr = encodingObj.GetBytes(key);
                var ivArr = encodingObj.GetBytes(iv);

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
                        //LogFunc(innerEx.Message);
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                //LogFunc(ex.Message);
                return null;
            }
        }
    }
}
