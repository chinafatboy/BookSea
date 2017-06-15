using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Http;

namespace booksea.Infrastructure
{
    public class RemotePost
    {
        public static string getCheckValue(string rootPata, string merchantId, string returnUrl, string paymentTypeObjId, string amtStr, string merTransId)
        {
            string xmlKey = File.ReadAllText(rootPata +"\\" + merchantId + ".xml");
            RSAParameters PrvKeyInfo = RSAUtility.GetPrvKeyFromXmlString(xmlKey);
            RSACng rsa = new RSACng();
            rsa.ImportParameters(PrvKeyInfo);
            string orgString = merchantId + merTransId + paymentTypeObjId + amtStr + returnUrl;
            ASCIIEncoding byteConverter = new ASCIIEncoding();
            byte[] orgData = byteConverter.GetBytes(orgString);
            byte[] signedData = rsa.SignData(orgData, HashAlgorithmName.MD5, RSASignaturePadding.Pkcs1);
            return Convert.ToBase64String(signedData);
        }

        public static bool PaymentVerify(string rootPath, HttpRequest curRequest, out string merId, out string amt, out string merTransId, out string transId, out string transTime)
        {
            merId = curRequest.Form["merId"].ToString();
            amt = curRequest.Form["amt"].ToString();
            merTransId = curRequest.Form["merTransId"].ToString();
            transId = curRequest.Form["transId"].ToString();
            transTime = curRequest.Form["transTime"].ToString();
            string checkValue = curRequest.Form["checkValue"].ToString();
            string PaymentPublicKey = File.ReadAllText(rootPath + "\\PaymentPublicKey.txt");
            RSAParameters PubKeyInfo = RSAUtility.GetPubKeyFromXmlString(PaymentPublicKey);
            string orgString = merId + merTransId + amt + transId + transTime;
            ASCIIEncoding byteConverter = new ASCIIEncoding();
            byte[] orgData = byteConverter.GetBytes(orgString);
            byte[] signedData = Convert.FromBase64String(checkValue);
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.ImportParameters(PubKeyInfo);
            return rsa.VerifyData(orgData, signedData, HashAlgorithmName.MD5, RSASignaturePadding.Pkcs1);
        }
    }


    public class RSAUtility
    {
        public static RSAParameters GetPubKeyFromXmlString(string xmlString)
        {
            RSAParameters result = new RSAParameters();
            result.Modulus = getRSAKeyEle("Modulus", xmlString);
            result.Exponent = getRSAKeyEle("Exponent", xmlString);
            return result;
        }

        public static RSAParameters GetPrvKeyFromXmlString(string xmlString)
        {
            RSAParameters result = new RSAParameters();
            result.Modulus = getRSAKeyEle("Modulus", xmlString);
            result.Exponent = getRSAKeyEle("Exponent", xmlString);
            result.P = getRSAKeyEle("P", xmlString);
            result.Q = getRSAKeyEle("Q", xmlString);
            result.DP = getRSAKeyEle("DP", xmlString);
            result.DQ = getRSAKeyEle("DQ", xmlString);
            result.InverseQ = getRSAKeyEle("InverseQ", xmlString);
            result.D = getRSAKeyEle("D", xmlString);
            return result;
        }

        private static byte[] getRSAKeyEle(string keyName, string xmlString)
        {
            Regex r = new Regex("<" + keyName + @">[\w+=/]*</" + keyName + ">");
            string s = r.Match(xmlString).Value;
            return Convert.FromBase64String(s.Substring(keyName.Length + 2, s.Length - 2 * keyName.Length - 5));
        }
    }

}
