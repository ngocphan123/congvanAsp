using CongVan.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CongVan.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        // GET: Areas/Home
        CongVanEntities db = new CongVanEntities();
        public ActionResult Index()
        {
            Session["current_url"] = Request.Url.AbsoluteUri;
            if (Session["Ad_TenDangNhap"] == null)
                return RedirectToAction("Login", "Account", null);
            return View();
        }
        public ActionResult LoadHeader()
        {
            // string id = Session["Ad_Id"].ToString().Trim();
            var data = db.users.Find(Session["Ad_Id"]);
            return PartialView("_Header", data);

        }
        public ActionResult LoadMenu()
        {
            // string id = Session["Ad_Id"].ToString().Trim();
            var data = db.users.Find(Session["Ad_Id"]);
            return PartialView("_Menu", data);

        }
    }
}