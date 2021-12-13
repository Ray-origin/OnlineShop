using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineShop.Models;

namespace OnlineShop.Controllers
{
    public class QuanLyTaiKhoanController : Controller
    {
        // GET: QuanLyTaiKhoan
        DataBanHangEntities1 db = new DataBanHangEntities1();
        public ActionResult Index()
        {
            return View(db.ThanhViens.OrderBy(n => n.MaTV));
        }
    }
}