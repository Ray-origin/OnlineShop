using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineShop.Models;
using PagedList.Mvc;
using PagedList;
namespace OnlineShop.Controllers
{
    public class TimKiemController : Controller
    {
        DataBanHangEntities1 db = new DataBanHangEntities1();
        // GET: TimKiem
        [HttpGet]
        public ActionResult KQTimKiem(string sTuKhoa, int? page)
        {
            if(Request.HttpMethod != "GET")
            {
                page = 1;
            }
            int PageSize = 6;
            // neu nhu page = 0 hoac ko co page thi gan = 1
            int PageNumber = (page ?? 1);
            //tim kiem theo ten sp
            ViewBag.TuKhoa = sTuKhoa;
            var listSP = db.SanPhams.Where(n => n.TenSp.Contains(sTuKhoa));
            return View(listSP.OrderBy(n=>n.TenSp).ToPagedList(PageNumber,PageSize));
        }
        [HttpPost]
        public ActionResult LayTuKhoaTimKiem(string sTuKhoa)
        {
            //redirect ve ham get

            return RedirectToAction("KQTimKiem",new {@sTuKhoa = sTuKhoa });
        }
        public ActionResult KQTimKiemPartial(string sTuKhoa)
        {
            var listSP = db.SanPhams.Where(n => n.TenSp.Contains(sTuKhoa));
            ViewBag.TuKhoa = sTuKhoa;
            return PartialView(listSP.OrderBy(n=>n.DonGia));
        }
    }
}