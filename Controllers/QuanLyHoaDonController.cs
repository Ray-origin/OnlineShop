using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineShop.Models;

namespace OnlineShop.Controllers
{
    public class QuanLyHoaDonController : Controller
    {
        // GET: QuanLyHoaDon
        DataBanHangEntities1 db = new DataBanHangEntities1();
        public ActionResult Index()
        {
            return View(db.HoaDons.OrderBy(n => n.MaHD));
        }
        public ActionResult Xem(int? id)
        {
            //lay san pham can chinh sua bang id
            if (id == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            var cthd = db.ChiTietHoaDons.Where(n => n.MaHD == id);
            if (cthd == null)
            {
                return HttpNotFound();
            }
            return View(cthd);
        }
    }

}