using Model.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Utilities;
using WebMrKatsu.Areas.Admin.Data;

namespace WebMrKatsu.Areas.Admin.Controllers
{
    public class AuthenticateController : Controller
    {
        private UserDAO userDAO = new UserDAO();
        // GET: Admin/Authenticate
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(LoginModel model)
        {
            string message = "";
            if (ModelState.IsValid)
            {
                var result = userDAO.IsLogin(model.UserName, model.Password);
                try
                {
                    switch (result)
                    {
                        case 1:
                            var user = userDAO.GetUserByUserName(model.UserName);

                            //Save Session User
                            var userSession = new UserLogin();
                            userSession.Username = user.Username;
                            userSession.UserId = user.Id;
                            userSession.GroupId = user.GroupId;

                            Session.Add(CommonConstants.USER_SESSION, userSession);
                            return RedirectToAction("Index", "Home");
                        case 0:
                            message = "Don't exist account!";
                            break;
                        case -1:
                            message = "Account or password not valid";
                            break;
                        case -2:
                            message = "Account is locked";
                            break;
                        case -3:
                            message = "Account or password not valid";
                            break;
                        default:
                            message = "Login fail";
                            break;
                    }
                }
                catch
                {
                    return View(model);
                }
            }
            ViewBag.Message = message;
            return View(model);
        }
    }
}