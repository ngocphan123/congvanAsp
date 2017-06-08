using CongVan.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CongVan.Areas.Admin.Controllers
{
    public class AccountController : Controller
    {
        // GET: Areas/Account
        NLP db = new NLP();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login(FormCollection collection)
        {
            string user = collection["UserName"];
            string pass = collection["PassWord"];
            string remember = collection["RememberMe"];
            bool rb = false;
            if (remember == "false")
            {
                rb = false;
            }
            else
            {
                rb = true;
            }

            if (user != null && pass != null)
            {
                if (CheckLogin(user, pass, rb))
                {
                    return RedirectToAction("Index", "Home", null);
                }
                else
                {
                    ModelState.AddModelError("Error", "Tên đăng nhập hoặc mật khẩu không đúng.");
                    // return RedirectToAction("Login", "Account", null);
                }
            }
            return View();
        }

        bool CheckLogin(string TenDangNhap, string MatKhau, bool remember = false)
        {
            if (TenDangNhap != null && MatKhau != null)
            {

                var users = (from p in db.users
                             where p.username == TenDangNhap && p.password == MatKhau
                             select p
                            );

                if (users.Count() == 0)
                {
                    ModelState.AddModelError("Error", "Tên đăng nhập hoặc mật khẩu không đúng.");
                    return false;
                }
                else
                {
                    Session["Ad_TenDangNhap"] = users.First().username;
                    Session["Ad_Id"] = users.First().id;
                    return true;
                }
            }
            return false;
        }
    }
}