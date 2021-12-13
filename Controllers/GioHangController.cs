using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineShop.Models;


namespace OnlineShop.Controllers
{
    public class GioHangController : Controller
    {
        DataBanHangEntities1 db = new DataBanHangEntities1();
        // GET: GioHang
        
        public List<ItemGioHang> LayGioHang()
        {
            //giỏ hàng đã tồn tại
            List<ItemGioHang> lstGioHang = Session["GioHang"] as List<ItemGioHang>;
            if(lstGioHang == null)
            {
                //nếu session chưa tồn tại thì tạo giỏ hàng
                lstGioHang = new List<ItemGioHang>();
                Session["GioHang"] = lstGioHang;
            }
            return lstGioHang;
        } 
        //them giỏ hàng(không dùng ajax)
        public ActionResult ThemGioHang(int MaSP, string strURL)
        {
            //kiem tra SP tồn tại
            SanPham sp = db.SanPhams.SingleOrDefault(n => n.MaSp == MaSP);
            if(sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            //lấy giỏ hàng (tạo giỏ hàng)
            List<ItemGioHang> lstGioHang = LayGioHang();
            //trường hợp đã tồn tại trong giỏ hàng (tính số lượng sản phẩm)
            ItemGioHang spCheck = lstGioHang.SingleOrDefault(n => n.MaSP == MaSP);
            if(spCheck != null)
            {
                //kiem tra so lượng tồn trước khi đặt
                if (sp.SoLuongTon < spCheck.Soluong)
                {
                    return View("ThongBao");
                }
                spCheck.Soluong++;
                spCheck.ThanhTien = spCheck.Soluong * spCheck.DonGia;
                return Redirect(strURL);
            }           
            ItemGioHang itemGH = new ItemGioHang(MaSP);
            if (sp.SoLuongTon < itemGH.Soluong)
            {
                return View("ThongBao");
            }
            lstGioHang.Add(itemGH);
            return Redirect(strURL);

        }
        //tinh tong so luong
        public double TinhTongSoLuong()
        {
            //lay gio hang
            List<ItemGioHang> lstGioHang =LayGioHang();
            if(lstGioHang == null)
            {
                return 0;
            }
            return lstGioHang.Sum(n => n.Soluong);
        }
        //tinh tong so luong
        public decimal TinhTongTien()
        {
            //lay gio hang
            List<ItemGioHang> lstGioHang = LayGioHang();
            if (lstGioHang == null)
            {
                return 0;
            }
            return lstGioHang.Sum(n => n.ThanhTien);
        }
        public ActionResult XemGioHang()
        {
            List<ItemGioHang> lstGioHang = LayGioHang();
            ViewBag.TinhTongTien = 0;
            ViewBag.TinhTongTien = TinhTongTien();
            return View(lstGioHang);
        }
        public ActionResult GioHangPartial()
        {
            if(TinhTongSoLuong() == 0)
            {
                ViewBag.tinhTongSoLuong = 0;
                ViewBag.tinhTongTien = 0;
                return PartialView();
            }
            ViewBag.tinhTongSoluong = TinhTongSoLuong();
            ViewBag.tinhTongTien = TinhTongTien();
            return PartialView();
        }
        [HttpGet]
        public ActionResult SuaGioHang(int MaSP)
        {
            //kiem tra gio hang ton tai chua
            if(Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            //kiem tra san pham co ton tai trong co so du lieu
            SanPham sp = db.SanPhams.SingleOrDefault(n => n.MaSp == MaSP);
            if(sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            // lay gio hang tu session
            List<ItemGioHang> lstGioHang = LayGioHang();
            //kiem tra sản phẩm da tồn tại trong gio hàng hay chưa
            ItemGioHang spCheck = lstGioHang.SingleOrDefault(n => n.MaSP == MaSP);
            if(spCheck == null)
            {
                return RedirectToAction("Index", "Home");
            }
            //lấy list giỏ hàng cho giao dien
            ViewBag.GioHang = lstGioHang;
            return View(spCheck);
        }
        //cập nhật giỏ hàng (số lượng)
        [HttpPost]
        public ActionResult CapNhatGioHang(ItemGioHang itemGH)
        {
            //kiem tra so luong ton
            SanPham spCheck = db.SanPhams.SingleOrDefault(n => n.MaSp == itemGH.MaSP);
            if(spCheck.SoLuongTon < itemGH.Soluong)
            {
                return View("ThongBao");
            }
            //cap nhat so luong trong session gio hang

            //lay giỏ hàng
            List<ItemGioHang> lstGioHang = LayGioHang();
            //lấy sản phẩm cần cập nhật
            ItemGioHang itemAfterUpdate = lstGioHang.Find(n => n.MaSP == itemGH.MaSP);
            itemAfterUpdate.Soluong = itemGH.Soluong;
            itemAfterUpdate.ThanhTien = itemAfterUpdate.DonGia * itemAfterUpdate.Soluong;
            return RedirectToAction("XemGioHang","GioHang");
        }
        public ActionResult XoaGioHang(int MaSP)
        {
            //kiem tra gio hang ton tai chua
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            //kiem tra san pham co ton tai trong co so du lieu
            SanPham sp = db.SanPhams.SingleOrDefault(n => n.MaSp == MaSP);
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            // lay gio hang tu session
            List<ItemGioHang> lstGioHang = LayGioHang();
            //kiem tra sản phẩm da tồn tại trong gio hàng hay chưa
            ItemGioHang spCheck = lstGioHang.SingleOrDefault(n => n.MaSP == MaSP);
            if (spCheck == null)
            {
                return RedirectToAction("Index", "Home");
            }
            //Xóa sản phẩm
            lstGioHang.Remove(spCheck);
            return RedirectToAction("XemGioHang", "GioHang");
        }
        public ActionResult EmptyCart()
        {
            Session["GioHang"] = null;
            return RedirectToAction("Index","Home");
        }
        public ActionResult DatHang()
        {
            //kiem tra gio hang ton tai chua
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            //kiem tra dang nhap
            if (User.Identity.Name == "")
            {
                return RedirectToAction("DangNhap", "Home");
            }
            //thêm đơn hàng
            HoaDon hd = new HoaDon();
            hd.NgayXuat = DateTime.Now;
            var MaThanhVien = Session["TaiKhoan"];
            hd.MaTV = (int?)MaThanhVien;
            hd.DaThanhToan = true;
            db.HoaDons.Add(hd);
            db.SaveChanges();
            //them chi tiet hoa don
            List<ItemGioHang> lstGH = LayGioHang();
            foreach(var item in lstGH)
            {
                ChiTietHoaDon cthd = new ChiTietHoaDon();
                cthd.MaHD = hd.MaHD;
                cthd.MaSp = item.MaSP;
                cthd.TenSp = item.TenSp;
                cthd.SoLuong = item.Soluong;
                cthd.DonGia = item.DonGia;
                db.ChiTietHoaDons.Add(cthd);               
            }
            db.SaveChanges();
            Session["GioHang"] = null;
            return RedirectToAction("ThanhToanThanhCong", "GioHang");
        }
        /////////////////////////////////////////////////////////////////////////////////
        /// PAYPAL //////////////////////////////////////////////////////////////////////
        public ActionResult Payment()
        {
            return RedirectToAction("DatHang", "GioHang");
            //Session["GioHang"] = null;
            //return View();
        }
        public ActionResult ThanhToanThanhCong()
        {
            return View();
        }
    }
}