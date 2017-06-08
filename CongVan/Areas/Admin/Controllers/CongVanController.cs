using CongVan.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace CongVan.Areas.Admin.Controllers
{
    public class CongVanController : Controller
    {
        // GET: Admin/CongVan
        NLP db = new NLP();
        public ActionResult Index()
        {
            if (Session["Ad_TenDangNhap"] == null)
                return RedirectToAction("Login", "Account", null);
            ViewModel mymodel = new ViewModel();
            mymodel.Cat = db.cats.ToList();
            mymodel.Department = db.departments.ToList();
            mymodel.Type = db.types.ToList();
            mymodel.Department_cat = db.department_cat.ToList();
            return View(mymodel);

        }


        // GET: Admin/CongVan/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }


        // GET: Admin/CongVan/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/CongVan/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection, HttpPostedFileBase file)
        {
            if (Session["Ad_TenDangNhap"] == null)
                return RedirectToAction("Login", "Account", null);
            string type = collection["types"];
            string idtype = collection["type"];
            string title = collection["title"];
            string idcat = collection["cat"];
            string publtime = collection["publtime"];
            string code = collection["code"];
            string number = collection["number"];
            string from_org = collection["from_org"];
            string to_org = collection["to_org"];
            string de_cat = collection["de_cat"];
            string de = collection["de"];
            string name_signer = collection["name_signer"];
            string name_initial = collection["name_initial"];
            string date_iss = collection["date_iss"];
            string date_first = collection["date_first"];
            string date_die = collection["date_die"];
            string abtract = collection["abtract"];
            string note = collection["note"];
            string level_important = collection["level_important"];
            row data = new row();
            if (file != null && file.ContentLength > 0)
            {
                // extract only the filename
                var fileName = Path.GetFileName(file.FileName);
                // store the file inside ~/App_Data/uploads folder
                var path = Path.Combine(Server.MapPath("~/uploads"), fileName);
                file.SaveAs(path);
                data.attach_file = fileName;
            }
            // string file = "heb";


            data.title = title;
            data.idtype = Int32.Parse(idtype);
            data.type = Int32.Parse(type);
            data.idcat = Int32.Parse(idcat);
            if (publtime != "") data.publtime = DateTime.Parse(publtime);
            data.number_dispatch = code;
            data.number_text_come = number;
            data.from_org = from_org;
            data.to_org = to_org;
            data.dep_catid = Int32.Parse(de_cat);
            data.to_depid = Int32.Parse(de);
            data.name_signer = name_signer;
            data.name_initial = name_initial;
            if (date_first != "") data.date_first = DateTime.Parse(date_first);
            if (date_die != "") data.date_die = DateTime.Parse(date_die);
            data._abstract = abtract;
            data.note = note;
            data.level_important = Int32.Parse(level_important);
            try
            {
                db.rows.Add(data);
                db.SaveChanges();

                return RedirectToAction("List");
            }
            catch (Exception e)
            {
                string x = e.InnerException.ToString();
                return RedirectToAction("Index");
            }
        }

        // GET: Admin/CongVan/Edit/5
        public ActionResult Edit(int id)
        {
            if (Session["Ad_TenDangNhap"] == null)
                return RedirectToAction("Login", "Account", null);
            var row = db.rows.First(r => r.id == id);
            IEnumerable<cat> Cat = (IEnumerable<cat>)db.cats;
            ViewBag.Cat = new SelectList(Cat, "id", "title", row.idcat);
            IEnumerable<type> Type = (IEnumerable<type>)db.types;
            ViewBag.Type = new SelectList(Type, "id", "title", row.idtype);
            IEnumerable<department_cat> De_ca = (IEnumerable<department_cat>)db.department_cat;
            ViewBag.De_ca = new SelectList(De_ca, "id", "title", row.dep_catid);
            IEnumerable<department> De = (IEnumerable<department>)db.departments;
            ViewBag.De = new SelectList(De, "id", "title", row.to_depid);
            ViewBag.dis_type = row.type;
            ViewBag.level_important = row.level_important;
            ViewBag.reply = row.reply;
            ViewBag.statusid = row.status;
            return View(row);
        }

        public ActionResult List()
        {
            if (Session["Ad_TenDangNhap"] == null)
                return RedirectToAction("Login", "Account", null);
            List<row> model = new List<row>();
            model = db.rows.OrderByDescending(row => row.id).ToList();

            return View(model);
        }

        [HttpPost]
        public ActionResult List(FormCollection collection)
        {
            if (Session["Ad_TenDangNhap"] == null)
                return RedirectToAction("Login", "Account", null);
            string keyword = collection["keyword"];
            string type = collection["type_se"];
            ViewBag.keyword = keyword;
            ViewBag.type = type;
            List<row> model = new List<row>();
            if (!string.IsNullOrEmpty(keyword))
            {
                switch (type)
                {
                    case "0":
                        model = (from x in db.rows
                                 where x.title.ToLower().Contains(keyword.ToLower())
                                 orderby x.publtime
                                 select x).ToList();
                        break;
                    case "1":
                        model = (from x in db.rows
                                 where x._abstract.Contains(keyword)
                                 orderby x.publtime
                                 select x).ToList();
                        break;
                    case "2":
                        model = (from x in db.rows
                                 where x.to_org.ToLower().Contains(keyword.ToLower())
                                 orderby x.publtime
                                 select x).ToList();
                        break;
                    case "3":
                        model = (from x in db.rows
                                 where x.number_dispatch.ToLower().Contains(keyword.ToLower())
                                 orderby x.publtime
                                 select x).ToList();
                        break;
                    default:
                        model = (from x in db.rows
                                 where x.title.ToLower().Contains(keyword.ToLower())
                                 orderby x.publtime
                                 select x).ToList();
                        break;
                }

            }
            return View(model);
        }

        // POST: Admin/CongVan/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection, HttpPostedFileBase file)
        {
            if (Session["Ad_TenDangNhap"] == null)
                return RedirectToAction("Login", "Account", null);
            string type = collection["types"];
            string idtype = collection["idtype"];
            string title = collection["title"];
            string idcat = collection["idcat"];
            string publtime = collection["publtime"];
            string code = collection["number_dispatch"];
            string number = collection["number_text_come"];
            string from_org = collection["from_org"];
            string to_org = collection["to_org"];
            string de_cat = collection["dep_catid"];
            string de = collection["to_depid"];
            string name_signer = collection["name_signer"];
            string name_initial = collection["name_initial"];
            string date_iss = collection["date_iss"];
            string date_first = collection["date_first"];
            string date_die = collection["date_die"];
            string abtract = collection["_abstract"];
            string note = collection["note"];
            string level_important = collection["level_important"];

            var data = db.rows.First(r => r.id == id);

            data.title = title;
            data.idtype = Int32.Parse(idtype);
            data.type = Int32.Parse(type);
            data.idcat = Int32.Parse(idcat);
            data.publtime = DateTime.Parse(publtime);
            data.number_dispatch = code;
            data.number_text_come = number;
            data.from_org = from_org;
            data.to_org = to_org;
            data.dep_catid = Int32.Parse(de_cat);
            data.to_depid = Int32.Parse(de);
            data.name_signer = name_signer;
            data.name_initial = name_initial;
            data.date_first = DateTime.Parse(date_first);
            data.date_die = DateTime.Parse(date_die);
            data._abstract = abtract;
            data.note = note;
            data.level_important = Int32.Parse(level_important);
            if (file != null && file.ContentLength > 0)
            {
                // extract only the filename
                var fileName = Path.GetFileName(file.FileName);
                // store the file inside ~/App_Data/uploads folder
                var path = Path.Combine(Server.MapPath("~/uploads"), fileName);
                file.SaveAs(path);
                data.attach_file = fileName;
            }
            try
            {
                // TODO: Add update logic here
                UpdateModel(data);
                db.SaveChanges();
                return RedirectToAction("List");
            }
            catch
            {
                return View(Edit(id));
            }
        }

        // GET: Admin/CongVan/Delete/5
        public ActionResult Delete(int id)
        {
            var data = db.rows.Find(id);
            db.rows.Remove(data);
            db.SaveChanges();
            return RedirectToAction("List");
        }

        // POST: Admin/CongVan/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            var data = db.rows.Find(id);
            db.rows.Remove(data);
            db.SaveChanges();
            return RedirectToAction("List");
        }

        public ActionResult Download(string fileName)
        {
            byte[] fileBytes = System.IO.File.ReadAllBytes("D:\\hoctap\\webasp\\CongVanT\\congvanAsp\\CongVan\\Areas\\Admin\\Files\\" + fileName);
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
        }

    }
}
