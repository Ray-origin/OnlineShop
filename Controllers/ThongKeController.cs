using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineShop.Models;

namespace OnlineShop.Controllers
{
    public class ThongKeController : Controller
    {
        DataBanHangEntities1 db = new DataBanHangEntities1();
        // GET: ThongKe
        public ActionResult Index()
        {
            ViewBag.SoNguoiTruyCap = HttpContext.Application["SoNguoiTruyCap"].ToString();//lấy số lượng người truy cập từ application
            ViewBag.SoNguoiOnline = HttpContext.Application["SoNguoiOnline"].ToString();//lấy số lượng người đang online từ application
            ViewBag.SoLuongDonHang = ThongKeDonHang();
            ViewBag.SoLuongThanhVien = ThongKeThanhVien();
            ViewBag.ThongKeDoanhThu = ThongKeDoanhThu().ToString("#,##");
            return View();
        }
        public decimal ThongKeDoanhThu()
        {
            //thong ke tat cả doanh thu 
            decimal TongDoanhThu = decimal.Parse(db.ChiTietHoaDons.Sum(n => n.SoLuong * n.DonGia).ToString());
            return TongDoanhThu/1000;
        }
        public double ThongKeDonHang()
        {
            var ddh = db.HoaDons.Count();
            return ddh;
        }
        public double ThongKeThanhVien()
        {
            var tv = db.ThanhViens.Count();
            return tv;
        }
        //xoa bỏ những biến ko dùng nữa
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if(db != null)
                {
                    db.Dispose();
                }
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}