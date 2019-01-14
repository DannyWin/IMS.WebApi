using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using IMS.Model.Model;
using IMS.Repository;
using IMS.IService;
using System.Security.Claims;
using IMS.Model.ViewModel;
using IMS.WebApi.AuthHelper;

namespace IMS.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController<User>
    {
        //private readonly DataContext _context;
        private IUserService UserService;
        public UserController(IUserService userService):base(userService)
        {
            UserService = userService;
        }

        [Route("Login")]
        [HttpPost]
        public async Task<ActionResult<bool>> Login(LoginViewModel lvm)
        {
            return await UserService.Login(lvm);
        }

        /// <summary>
        /// 获取JWT的重写方法，推荐这种，注意在文件夹OverWrite下
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="sub">角色</param>
        /// <returns></returns>
        [HttpGet]
        [Route("Token2")]
        public string GetJWTStr(long id = 1, string sub = "Admin")
        {
            //这里就是用户登录以后，通过数据库去调取数据，分配权限的操作
            TokenModelJWT tokenModel = new TokenModelJWT();
            tokenModel.Uid = id;
            tokenModel.Role = sub;

            string jwtStr = JwtHelper.IssueJWT(tokenModel);
            return jwtStr;
        }


        //    public UserController(DataContext context, IUserService userService)
        //{
        //    _context = context;
        //    UserService = userService;
        //}

        // GET: api/User
        //[HttpGet]
        //public async Task<IEnumerable<User>> GetUser()
        //{
        //    return await UserService.FindList(u=>u.Id>0);
        //}

        //// GET: api/User/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<User>> GetUser(int id)
        //{
        //    var user = await UserService.Find(id);
        //    if (user == null)
        //    {
        //        return NotFound();
        //    }
        //    return user;
        //}

        //// PUT: api/User/5
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutUser(int id, User user)
        //{
        //    var u = await UserService.Find(user.Id);
        //    if (u == null)
        //    {
        //        return NotFound();
        //    }
        //    await UserService.Update(user);
        //    //return CreatedAtAction("GetUser", new { id = user.Id }, user);

        //    return NoContent();
        //}
        //// PATCH: api/User
        //[HttpPatch]
        //public async Task<ActionResult<User>> PatchUser(User user)
        //{
        //    var u = await UserService.Find(user.Id);
        //    if (u == null)
        //    {
        //        return NotFound();
        //    }
        //    await UserService.Update(user);
        //    return CreatedAtAction("GetUser", new { id = user.Id }, user);
        //}
        //// POST: api/User
        //[HttpPost]
        //public async Task<ActionResult<User>> AddUser(User user)
        //{
        //    await UserService.Add(user);
        //    return CreatedAtAction("GetUser", new { id = user.Id }, user);
        //}

        //// DELETE: api/User/5
        //[HttpDelete("{id}")]
        //public async Task<ActionResult<bool>> DeleteUser(int id)
        //{
        //    var user = await Task.Run(() => UserService.Find(u => u.Id == id));
        //    if (user == null)
        //    {
        //        return NotFound();
        //    }
        //    return await Task.Run(() => UserService.Delete(user));

        //}

        //private bool UserExists(int id)
        //{
        //    return _context.User.Any(e => e.Id == id);
        //}
    }
}
