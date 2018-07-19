using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace WebApi.Controllers
{
    public class UserController : ApiController
    {
        private List<Users> _userList = new List<Users>
        {
            new Users {UserID = 1, UserName = "Superman", UserEmail = "Superman@cnblogs.com"},
            new Users {UserID = 2, UserName = "Spiderman", UserEmail = "Spiderman@cnblogs.com"},
            new Users {UserID = 3, UserName = "Batman", UserEmail = "Batman@cnblogs.com"}
        };

        [HttpGet]
        public IEnumerable<Users>GetUserByName(string username)
        {
            var param = HttpContext.Current.Request.QueryString["username"];
            return _userList.Where(p => string.Equals(p.UserName, username, StringComparison.OrdinalIgnoreCase));
        }
        [HttpGet]
        public IEnumerable<Users> GetUserById(int id)
        {
            var param = HttpContext.Current.Request.QueryString["id"];
            return _userList.Where(p => p.UserID==id);
        }
        public IEnumerable<Users> GetUserByNameId(string username,int id)
        {
            var idparam = HttpContext.Current.Request.QueryString["id"];
            var nameparam = HttpContext.Current.Request.QueryString["username"];
            return _userList.Where(p => string.Equals(p.UserName, username, StringComparison.OrdinalIgnoreCase));
        }
        public IEnumerable<Users>Get()
        {
            return _userList;
        }
        public IEnumerable<Users> GetUserByModel(Users user)
        {
            var idparam = HttpContext.Current.Request.QueryString["id"];
            var nameparam = HttpContext.Current.Request.QueryString["username"];
            var email= HttpContext.Current.Request.QueryString["email"];
            return _userList;
        }
        public IEnumerable<Users> GetUserByModelFromUri([FromUri]Users user)
        {
            var idparam = HttpContext.Current.Request.QueryString["UserID"];
            var nameparam = HttpContext.Current.Request.QueryString["username"];
            var email = HttpContext.Current.Request.QueryString["UserEmail"];
            return _userList;
        }
        public IEnumerable<Users> GetUserByModelSerialize(string userString)
        {
            Users users = JsonConvert.DeserializeObject<Users>(userString);
            return _userList;
        }
        public IEnumerable<Users> GetUserByModelSerializeWithoutGet(string userString)
        {
            Users users = JsonConvert.DeserializeObject<Users>(userString);
            return _userList;
        }
        [HttpGet]
        public IEnumerable<Users> NoGetUserByModelSerializeWithoutGet(string userString)
        {
            Users users = JsonConvert.DeserializeObject<Users>(userString);
            return _userList;
        }
        #region HttpPost
        /// <summary>
        /// 不加[FromBody]，请求到不了服务控制器
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public Users RegisterNoKey([FromBody]int id)
        {
            string idParam = HttpContext.Current.Request.Form["id"];

            var user = _userList.FirstOrDefault(users => users.UserID == id);
            if (user == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return user;
        }
        public Users Register ([FromBody]int id)
        {
            string idParam = HttpContext.Current.Request.Form["id"];

            var user = _userList.FirstOrDefault(users => users.UserID == id);
            if (user == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return user;
        }
        [HttpPost]
        public Users RegisterUser(Users user)
        {
            string idParam = HttpContext.Current.Request.Form["UserID"];
            string nameParam = HttpContext.Current.Request.Form["UserName"];
            string emailParam = HttpContext.Current.Request.Form["UserEmail"];
            var stringContent = base.ControllerContext.Request.Content.ReadAsStringAsync().Result;
            return user;
        }
        [HttpPost]
        public string RegisterObject(JObject jData)
        {
            string idParam = HttpContext.Current.Request.Form["UserID"];
            string nameParam = HttpContext.Current.Request.Form["UserName"];
            string emailParam = HttpContext.Current.Request.Form["UserEmail"];
            var stringContent = base.ControllerContext.Request.Content.ReadAsStringAsync().Result;
            dynamic dn = jData;
            JObject juser = dn.User;
            string info = dn.Info;
            var user = juser.ToObject<Users>();
            return  string.Format("{0}_{1}_{2}_{3}", user.UserID, user.UserName, user.UserEmail, info); 
        }
        public string RegisterObjectDynamic(dynamic dynamicData)//可以来自FromBody   FromUri
        {
            string idParam = HttpContext.Current.Request.Form["User[UserID]"];
            string nameParam = HttpContext.Current.Request.Form["User[UserName]"];
            string emailParam = HttpContext.Current.Request.Form["User[UserEmail]"];
            string infoParam = HttpContext.Current.Request.Form["info"];
            dynamic json = dynamicData;
            JObject jUser = json.User;
            string info = json.Info;
            var user = jUser.ToObject<Users>();

            return string.Format("{0}_{1}_{2}_{3}", user.UserID, user.UserName, user.UserEmail, info);
        }
        #endregion
        #region HttpPut
        [HttpPut]
        public Users RegisterNoKeyPut([FromBody]int id)
        {
            string idParam = HttpContext.Current.Request.Form["id"];

            var user = _userList.FirstOrDefault(users => users.UserID == id);
            if (user == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return user;
        }

        //POST api/Users/registerPut
        //只接受一个参数的需要不给key才能拿到
        [HttpPut]
        public Users RegisterPut([FromBody]int id)//可以来自FromBody   FromUri
                                                  //public Users Register(int id)//可以来自url
        {
            string idParam = HttpContext.Current.Request.Form["id"];

            var user = _userList.FirstOrDefault(users => users.UserID == id);
            if (user == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return user;
        }

        //POST api/Users/RegisterUserPut
        [HttpPut]
        public Users RegisterUserPut(Users user)//可以来自FromBody   FromUri
        {
            string idParam = HttpContext.Current.Request.Form["UserID"];
            string nameParam = HttpContext.Current.Request.Form["UserName"];
            string emailParam = HttpContext.Current.Request.Form["UserEmail"];

            //var userContent = base.ControllerContext.Request.Content.ReadAsFormDataAsync().Result;
            var stringContent = base.ControllerContext.Request.Content.ReadAsStringAsync().Result;
            return user;
        }


        //POST api/Users/registerPut
        [HttpPut]
        public string RegisterObjectPut(JObject jData)//可以来自FromBody   FromUri
        {
            string idParam = HttpContext.Current.Request.Form["User[UserID]"];
            string nameParam = HttpContext.Current.Request.Form["User[UserName]"];
            string emailParam = HttpContext.Current.Request.Form["User[UserEmail]"];
            string infoParam = HttpContext.Current.Request.Form["info"];
            dynamic json = jData;
            JObject jUser = json.User;
            string info = json.Info;
            var user = jUser.ToObject<Users>();

            return string.Format("{0}_{1}_{2}_{3}", user.UserID, user.UserName, user.UserEmail, info);
        }

        [HttpPut]
        public string RegisterObjectDynamicPut(dynamic dynamicData)//可以来自FromBody   FromUri
        {
            string idParam = HttpContext.Current.Request.Form["User[UserID]"];
            string nameParam = HttpContext.Current.Request.Form["User[UserName]"];
            string emailParam = HttpContext.Current.Request.Form["User[UserEmail]"];
            string infoParam = HttpContext.Current.Request.Form["info"];
            dynamic json = dynamicData;
            JObject jUser = json.User;
            string info = json.Info;
            var user = jUser.ToObject<Users>();

            return string.Format("{0}_{1}_{2}_{3}", user.UserID, user.UserName, user.UserEmail, info);
        }
        #endregion
    }
    public class Users
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
    }
}
