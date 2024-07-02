using Model.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebMrKatsu.Areas.Admin.Controllers
{
    public class AccountController : Controller
    {
        private UserDAO userDAO = new UserDAO();
        // GET: Admin/Account
        public ActionResult Index()
        {
            
            return View();
        }
        public ActionResult Create()
        {
            return View();
        }
    }
}