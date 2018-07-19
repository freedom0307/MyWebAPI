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

            var httpRequest = (HttpWebRequest)WebRequest.Create(url9);
            httpRequest.Timeout = 30 * 1000;
            string res = "";
            var result1 = httpRequest.GetResponseAsync().Result;
            var result = httpRequest.GetResponseAsync().Result as HttpWebResponse;
            Console.WriteLine(result.StatusCode);
            if (result.StatusCode == HttpStatusCode.OK)
            {
                StreamReader sr = new StreamReader(result.GetResponseStream(), Encoding.UTF8);
                res = sr.ReadToEnd();
            }
            sw.Stop();
            Console.WriteLine("耗时:{0}ms", sw.ElapsedMilliseconds);
            return res;


        }
        #region Get-Request
        #region Post-Client
        public static string PostClient()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            string url1 = "http://localhost:9008/api/user/register";
            //Dictionary<string, string> dic = new Dictionary<string, string>()
            //{
            //    {"","1" }
            //};
            string url2 = "http://localhost:9008/api/user/register";
            //Dictionary<string, string> dic = new Dictionary<string, string>()
            //{
            //    {"","1" }
            //};
            string url3 = "http://localhost:9008/api/user/RegisterObject";
            Dictionary<string, string> dic = new Dictionary<string, string>()
            {
                {"User[UserId]","1" },
                {"User[UserName]","Eleven" },
                {"User[UserEmail]","57265177@qq.com" },
                {"Info","this is muti model" }
            };
            var content = new FormUrlEncodedContent(dic);
            HttpClient httpClient = new HttpClient();
            var response = Task.Run(() => httpClient.PostAsync(url3, content)).Result;
            Console.WriteLine(response.StatusCode);
            sw.Stop();
            Console.WriteLine("耗时:{0}ms", sw.ElapsedMilliseconds);
            return response.Content.ReadAsStringAsync().Result;
        }

        #endregion
        #region Post-Requst
        public static string PostRequest()
        {
            Stopwatch sw = new Stopwatch();
            string result = "";
            sw.Start();
            string url1 = "http://localhost:9008/api/user/register";
            string url2 = "http://localhost:9008/api/user/register";
            string url3 = "http://localhost:9008/api/user/RegisterUser";
            string url4 = "http://localhost:9008/api/user/RegisterObject";
            Dictionary<string, string> dic = new Dictionary<string, string>()
            {
                {"","1" }
            };

            var user = new
            {
                UserID = "11",
                UserName = "Eleven",
                UserEmail = "57265177@qq.com"
            };
            var Other = new
            {
                User = user,
                Info = "this is muti model"
            };
            //var postData = "1";
            var postData = JsonHelper.ObjectToString(Other);
            var httpClient = WebRequest.Create(url4) as HttpWebRequest;
            httpClient.Timeout = 30 * 1000;
            httpClient.Method = "Post";
            httpClient.ContentType = "application/json";//不加这个会报错,任何情况都是
            httpClient.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/41.0.2272.118 Safari/537.36";
            byte[] data = Encoding.UTF8.GetBytes(postData);
            httpClient.ContentLength = data.Length;
            Stream requestStream = httpClient.GetRequestStream();
            requestStream.Write(data, 0, data.Length);
            requestStream.Close();
            var response = Task.Run(() => httpClient.GetResponse()).Result as HttpWebResponse;
            Console.WriteLine(response.StatusCode);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                result = reader.ReadToEnd();
            }
            sw.Stop();
            Console.WriteLine("耗时:{0}ms", sw.ElapsedMilliseconds);
            return result;
        }
        #endregion

        #region Put-Client
        public static string PutClient()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            string url1 = "http://localhost:9008/api/user/RegisterPut";
            string url2 = "http://localhost:9008/api/user/RegisterNoKeyPut";
            string url3 = "http://localhost:9008/api/user/RegisterUserPut";
            string url4 = "http://localhost:9008/api/user/RegisterObjectPut";
            Dictionary<string, string> dic = new Dictionary<string, string>()
            {
                {"","1" }
            };
            //Dictionary<string, string> dic = new Dictionary<string, string>()
            //{
            //    {"UserID","11" },
            //    {"UserName","Eleven" },
            //    {"UserEmail","57265177@qq.com" },
            //};
            //Dictionary<string, string> dic = new Dictionary<string, string>()
            //{
            //    {"User[UserId]","1" },
            //    {"User[UserName]","Eleven" },
            //    {"User[UserEmail]","57265177@qq.com" },
            //    {"Info","this is muti model" }
            //};
            var client = new HttpClient();
            var content = new FormUrlEncodedContent(dic);
            var respone = client.PutAsync(url1, content).Result;
            Console.WriteLine(respone.StatusCode);
            sw.Stop();
            Console.WriteLine("耗时:{0}ms", sw.ElapsedMilliseconds);
            return respone.Content.ReadAsStringAsync().Result;
        }
        #endregion
        #region Put-Request
        public static string PutRequest()
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            string result = "";
            string url1 = "http://localhost:9008/api/user/RegisterPut";
            string url2 = "http://localhost:9008/api/user/RegisterNoKeyPut";
            string url3 = "http://localhost:9008/api/user/RegisterUserPut";
            string url4 = "http://localhost:9008/api/user/RegisterObjectPut";
           
            var user = new
            {
                UserID = "11",
                UserName = "Eleven",
                UserEmail = "57265177@qq.com"
            };
            var info = new
            {
                Info = "this is muti model"
            };
            var other = new
            {
                User=user,
                info=info
            };
            var request = WebRequest.Create(url4) as HttpWebRequest;
            request.Timeout = 30 * 1000;
            request.Method = "Put";
            request.ContentType = "application/json";//不加这个会报错
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/41.0.2272.118 Safari/537.36";
            var postData = JsonHelper.ObjectToString(other);
            //postData = "1";
            byte[] data = Encoding.UTF8.GetBytes(postData);
            request.ContentLength = data.Length;
            var rstream = request.GetRequestStream();
            rstream.Write(data, 0, data.Length);
            rstream.Close();
            using (var response = request.GetResponse() as HttpWebResponse)
            {
                if (response.StatusCode==HttpStatusCode.OK)
                {
                    StreamReader stream = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                    result = stream.ReadToEnd();
                }
               
            }
            return result;

        }
        #endregion
        #endregion
        public class Users
        {
            public int UserID { get; set; }
            public string UserName { get; set; }
            public string UserEmail { get; set; }
        }
    }
}
