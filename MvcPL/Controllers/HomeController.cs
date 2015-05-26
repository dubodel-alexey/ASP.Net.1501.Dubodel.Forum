using System;
using System.Linq;
using System.Web.Mvc;
using Bll.Interface;
using Bll.Interface.Entities;
using MvcPL.ViewModels;

namespace MvcPL.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/
        private readonly IUserService userService;
        private readonly IRoleService roleService;
        public HomeController(IUserService userService, IRoleService roleService)
        {
            this.userService = userService;
            this.roleService = roleService;
        }

        public ActionResult Index()
        {
            return View(userService.GetAllUsers()
                .Select(user => new UserViewModel
                {
                    Login = user.Login
                }));
        }

        public ActionResult GetAdmins()
        {
            return View(userService.GetUsersByRole(roleService.GetById(1))
                .Select(user => new UserViewModel
                {
                    Login = user.Login
                }));
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(RegisterViewModel user)
        {
            var blluser = new BllUser
            {
                Login = user.Login,
                Password = user.Password,
                RegistrationDateTime = DateTime.Now,
                //RoleId = (int)user.Role
            };
            userService.Create(blluser);
            return RedirectToAction("Index");
        }

    }
}
