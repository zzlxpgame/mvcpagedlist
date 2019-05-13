//更改用于github 提交2
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcPagedList.Models;
using PagedList;

namespace MvcPagedList.Controllers
{
    public class HomeController : Controller
    {
        EFDbContext db = new EFDbContext();
        // GET: Home
        public ActionResult Index(int page = 1)
        {
            return View(db.Rkxxs.OrderBy(p => p.idzz ).ToPagedList(page, 10));
            //return View(db.Rkxxs.ToList());
        }
        public ActionResult IndexAjax()
        {
            ViewBag.maxrecord = db.Rkxxs.Count();
            return View();
        }
        [HttpPost]
        public ActionResult IndexAjax(int page = 1,int limit = 10)
        {
            var rkxxs = db.Rkxxs.OrderByDescending( d => d.idzz)
                                .Skip(limit * (page - 1))
                                .Take(limit);
            if (rkxxs != null)
            {
                return Json(new { sucess = true, code = 0, msg = "读取成功", count = db.Rkxxs.Count(), data = rkxxs });
            }
            else
            {
                return Json(new { sucess = false });
            }
        }

        public ActionResult Paged(int? page)
        {
            int pageIndex = page ?? 1;
            int pageSize = 10;
            int totalCount = 0;
            var rkxxs = GetRkxx(pageIndex, pageSize, ref totalCount);
            var personsAsIPageList = new StaticPagedList<Rkxx>(rkxxs, pageIndex, pageSize, totalCount);
            return View(personsAsIPageList);
        }

        /// <summary>
        /// 测试手机下拉刷新View
        /// </summary>
        /// <returns></returns>
        public ActionResult DownRef()
        {
            return View();
        }
        //响应Ajax请求
        public ActionResult AjaxPage(int? page)
        {
            int pageIndex = page ?? 1;
            int pageSize = 5;
            int totalCount = 0;
            var rkxxs = GetRkxx(pageIndex, pageSize, ref totalCount);
            var personsAsIPageList = new StaticPagedList<Rkxx>(rkxxs, pageIndex, pageSize, totalCount);
            return Json(new { rkxxs = personsAsIPageList }, JsonRequestBehavior.AllowGet);
        }

        public List<Rkxx> GetRkxx(int pageIndex, int pageSize, ref int totalCount)
        {
            var persons = (from p in db.Rkxxs
                           orderby p.idzz descending
                           select p).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            totalCount = db.Rkxxs.Count();
            return persons.ToList();
        }
    }
}