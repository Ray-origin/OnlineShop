using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineShop.Models;
using System.Web.Security;

namespace OnlineShop.Controllers
{
    public class HomeController : Controller
    {
        DataBanHangEntities1 db = new DataBanHangEntities1();
        // GET: Home
        public ActionResult Index()
        {
            //list dien thoai moi nhat
            var lstDTM = db.SanPhams.Where(n => n.MaLoai == 1 && n.Moi == 1);
            ViewBag.ListDTM = lstDTM;

            //list laptop moi nhat
            var lstLTM = db.SanPhams.Where(n => n.MaLoai == 2 && n.Moi == 1);
            ViewBag.ListLTM = lstLTM;

            //list may tinh ban moi nhat
            var lstMTBM = db.SanPhams.Where(n => n.MaLoai == 3 && n.Moi == 1);
            ViewBag.ListMTBM = lstMTBM;

            return View();
        }
        public ActionResult MenuPartial()
        {
            var lstSP = db.SanPhams;
            return PartialView(lstSP);
        }
        [HttpGet]
        public ActionResult DangKy()
        {

            return View();
        }
        [HttpGet]
        public ActionResult DangKy1()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangKy1(ThanhVien tv)
        {
            return View();
        }
        [HttpGet]
        public ActionResult DangNhap()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangNhap(FormCollection f)
        {
            string sTaiKhoan = f["txtTaiKhoan"].ToString();
            string sMatKhau = f["txtMatKhau"].ToString();
            ThanhVien tv = db.ThanhViens.SingleOrDefault(n => n.TaiKhoan == sTaiKhoan && n.MatKhau == sMatKhau);
            if (tv != null)
            {
                //truyen ma tv
                Session["TaiKhoan"] = tv.MaTV;
                // lay ra cac quyen cua thanh vien theo loại thành vien
                var lstQuyen = db.LoaiThanhVien_Quyen.Where(n => n.MaLoaiTV == tv.MaLoaiTV);
                //duyet list quyen
                string Quyen = "";
                foreach(var item in lstQuyen)
                {
                    Quyen += item.Quyen.MaQuyen + ",";
                }
                Quyen = Quyen.Substring(0, Quyen.Length - 1);// bỏ dấu phẩy
                PhanQuyen(tv.TaiKhoan,Quyen);//tao cookie
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        public void PhanQuyen(string taikhoan , string quyen)
        {
            ViewBag.userName = taikhoan;
            FormsAuthentication.Initialize();

            var ticket = new FormsAuthenticationTicket(1,
                                                taikhoan,
                                                DateTime.Now,
                                                DateTime.Now.AddHours(3),
                                                false,
                                                quyen,
                                                FormsAuthentication.FormsCookiePath);
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket));
            if (ticket.IsPersistent)
                cookie.Expires = ticket.Expiration;
            Response.Cookies.Add(cookie);
        }
        [HttpPost]
        public ActionResult DangKy(ThanhVien tv)
        {
            if (ModelState.IsValid) {
                ViewBag.thongbao = "Đăng ký thành công";
            db.ThanhViens.Add(tv);
            db.SaveChanges();
            }
            else
            {
                ViewBag.thongbao = "Đăng ký thất bại";
            }
            return View(tv);
        }
        //tạo trang ngăn quyền truy cập
        public ActionResult LoiPhanQuyen()
        {
            return View();
        }
        public ActionResult DangXuat()
        {
            //Session["TaiKhoan"] = null;
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
    }
}