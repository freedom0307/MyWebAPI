﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Test.WebApiTest;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            //var res =WebApiTest.GetClient();
            //var res1 = WebApiTest.GetRequest();
            //var res2 = WebApiTest.PostClient();
            //var entity = JsonHelper.StringToObject<Users>(res);
            //var res3 = WebApiTest.PostRequest();
            var res4 = WebApiTest.PutRequest();
        }
    }
}
