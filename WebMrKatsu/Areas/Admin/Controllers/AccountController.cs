using Model.DAO;
using Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using Utilities;

namespace WebMrKatsu.Areas.Admin.Controllers
{
    public class AccountController : BaseController
    {
        private UserDAO userDAO = new UserDAO();
        // GET: Admin/Account
        //public ActionResult Index()
        //{
        //    var list = userDAO.GetAll();
        //    return View(list);
        //}
        public ActionResult Index(string searchString = "", int page = 1, int pageSize = 10)
        {
            var list = userDAO.GetAll(searchString, page, pageSize, true);
            ViewBag.SearchString = searchString;
            return View(list);
        }
        [HttpGet]
        public ActionResult CreateUser()
        {
            return PartialView();
        }
        [HttpPost]
        public ActionResult CreateUser(Account account)
        {
            if (userDAO.IsUsenameExist(account.Username))
            {
                SetAlert("Tài khoản đã tồn tại", "error");
                return View(account);
            }
            else if (!userDAO.IsEmailExist(account.Email))
            {
                SetAlert("Địa chỉ email đã được sử dụng", "error");
                return View(account);
            }

            var encripPassword = Hash.GetInstance().HashPassword(account.Password);
            account.Password = encripPassword;

            long id = userDAO.Insert(account);
            if (id > 0)
            {
                SetAlert("Thêm User thành công", "sucess");
                return RedirectToAction("Index", "User");
            }
            else
            {
                SetAlert("Thêm User không thành công", "error");
                ModelState.AddModelError("", "Thêm người dùng không thành công");
            }
            return View("Index");
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Account account)
        {
            if (ModelState.IsValid)
            {
                if (userDAO.IsUsenameExist(account.Username))
                {
                    SetAlert("Tài khoản đã tồn tại", "error");
                    return View(account);
                }
                else if (userDAO.IsEmailExist(account.Email))
                {
                    SetAlert("Địa chỉ email đã được sử dụng", "error");
                    return View(account);
                }

                var encripPassword = Hash.GetInstance().HashPassword(account.Password);
                account.Password = encripPassword;

                long id = userDAO.Insert(account);
                if (id > 0)
                {
                    SetAlert("Thêm User thành công", "sucess");
                    return RedirectToAction("Index", "User");
                }
                else
                {
                    SetAlert("Thêm User không thành công", "error");
                    ModelState.AddModelError("", "Thêm người dùng không thành công");
                }
            }
            SetAlert("Thêm User không thành công", "error");
            return View("Index");
        }
        [HttpGet]
        public ActionResult Edit(long id)
        {
            var user = userDAO.GetUserById(id);

            ViewBag.CurrentPass = user.Password;
            return View(user);
        }
        [HttpPost]
        public ActionResult Edit(Account account)
        {
            if (ModelState.IsValid)
            {


                var result = userDAO.Update(account);
                if (result)
                {
                    SetAlert("Chỉnh sửa User thành công", "sucess");
                    return RedirectToAction("Index", "Account");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật người dùng không thành công");
                }
            }

            return View("Index");
        }
        [HttpDelete]
        public ActionResult Delete(long id)
        {
            var result = userDAO.Delete(id);
            return RedirectToAction("Index");
        }
    }
}