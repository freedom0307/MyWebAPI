using Newtonsoft.Json;
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

    }
    public class Users
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
    }
}
