using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    public class WebApiTest
    {
        #region Get-Client
        public static string GetClient()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            string url1 = "http://localhost:9008/api/user/GetUserByName?username=Superman";
            string url2 = "http://localhost:9008/api/user/GetUserById?id=123";
            string url3 = "http://localhost:9008/api/user/GetUserByNameId?username=Superman&id=123";
            string url4 = "http://localhost:9008/api/user/Get";
            string url5 = "http://localhost:9008/api/user/GetUserByModel?UserID=123&UserName=Superman&UserEmail=121670370@qq.com";
            string url6 = "http://localhost:9008/api/user/GetUserByModelFromUri?UserID=123&UserName=Superman&UserEmail=121670370@qq.com";
            string url7 = "http://localhost:9008/api/user/GetUserByModelSerialize?userString=%7B%22UserID%22%3A%2211%22%2C%22UserName%22%3A%22Eleven%22%2C%22UserEmail%22%3A%2257265177%40qq.com%22%7D";
            string url8 = "http://localhost:9008/api/user/GetUserByModelSerializeWithoutGet?userString=%7B%22UserID%22%3A%2211%22%2C%22UserName%22%3A%22Eleven%22%2C%22UserEmail%22%3A%2257265177%40qq.com%22%7D";
            string url9 = "http://localhost:9008/api/user/NoGetUserByModelSerializeWithoutGet?userString=%7B%22UserID%22%3A%2211%22%2C%22UserName%22%3A%22Eleven%22%2C%22UserEmail%22%3A%2257265177%40qq.com%22%7D";
            var httpClient = new HttpClient();

            var result = httpClient.GetAsync(url9).Result;
            Console.WriteLine(result.StatusCode);
            sw.Stop();
            Console.WriteLine("耗时:{0}ms", sw.ElapsedMilliseconds);
            return result.Content.ReadAsStringAsync().Result;


        }
        #endregion
        public static string GetRequest()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            string url1 = "http://localhost:9008/api/user/GetUserByName?username=Superman";
            string url2 = "http://localhost:9008/api/user/GetUserById?id=123";
            string url3 = "http://localhost:9008/api/user/GetUserByNameId?username=Superman&id=123";
            string url4 = "http://localhost:9008/api/user/Get";
            string url5 = "http://localhost:9008/api/user/GetUserByModel?UserID=123&UserName=Superman&UserEmail=121670370@qq.com";
            string url6 = "http://localhost:9008/api/user/GetUserByModelFromUri?UserID=123&UserName=Superman&UserEmail=121670370@qq.com";
            string url7 = "http://localhost:9008/api/user/GetUserByModelSerialize?userString=%7B%22UserID%22%3A%2211%22%2C%22UserName%22%3A%22Eleven%22%2C%22UserEmail%22%3A%2257265177%40qq.com%22%7D";
            string url8 = "http://localhost:9008/api/user/GetUserByModelSerializeWithoutGet?userString=%7B%22UserID%22%3A%2211%22%2C%22UserName%22%3A%22Eleven%22%2C%22UserEmail%22%3A%2257265177%40qq.com%22%7D";
            string url9 = "http://localhost:9008/api/user/NoGetUserByModelSerializeWithoutGet?userString=%7B%22UserID%22%3A%2211%22%2C%22UserName%22%3A%22Eleven%22%2C%22UserEmail%22%3A%2257265177%40qq.com%22%7D";

            var httpRequest=(HttpWebRequest)WebRequest.Create(url1);
            string res= "";
            var result1 = httpRequest.GetResponseAsync().Result;
            var result = httpRequest.GetResponseAsync().Result as HttpWebResponse;
            Console.WriteLine(result.StatusCode);
            if (result.StatusCode==HttpStatusCode.OK)
            {
                StreamReader sr = new StreamReader(result.GetResponseStream(),Encoding.UTF8);
                res = sr.ReadToEnd();
            }
            sw.Stop();
            Console.WriteLine("耗时:{0}ms", sw.ElapsedMilliseconds);
            return res;


        }
        #region Get-Request

        #endregion
        public class Users
        {
            public int UserID { get; set; }
            public string UserName { get; set; }
            public string UserEmail { get; set; }
        }
    }
}
