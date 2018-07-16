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
        public IEnumerable<Users>GetUserByName(string name)
        {
            var param = HttpContext.Current.Request.QueryString["name"];
            return _userList.Where(p => string.Equals(p.UserName, name, StringComparison.OrdinalIgnoreCase));
        }
       
    }
    public class Users
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
    }
}
