using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OnlineShop.Models;

namespace OnlineShop.Controllers
{
    [Authorize(Roles = "QuanLySanPham")]
    public class QuanLySanPhamController : Controller
    {
        
        // GET: QuanLySanPham
        DataBanHangEntities1 db = new DataBanHangEntities1();
        public ActionResult Index()
        { 
            return View(db.SanPhams.OrderBy(n=>n.MaSp));
        }
        [HttpGet]
        public ActionResult TaoMoi()
        {
            //load menu dropdownlist
            ViewBag.MaNSX = new SelectList(db.NhaSanXuats.OrderBy(n=>n.TenNSX),"MaNSX","TenNSX");
            ViewBag.MaLoai = new SelectList(db.LoaiSanPhams.OrderBy(n =>n.TenLoai ), "MaLoai", "TenLoai");
            return View();
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult TaoMoi(SanPham sp)
        {
            //load menu dropdownlist
            ViewBag.MaNSX = new SelectList(db.NhaSanXuats.OrderBy(n => n.TenNSX), "MaNSX", "TenNSX");
            ViewBag.MaLoai = new SelectList(db.LoaiSanPhams.OrderBy(n => n.TenLoai), "MaLoai", "TenLoai");
            db.SanPhams.Add(sp);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult ChinhSua(int? id)
        {
            //lay san pham can chinh sua bang id
            if(id == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            SanPham sp = db.SanPhams.SingleOrDefault(n => n.MaSp == id);
            if(sp == null)
            {
                return HttpNotFound();
            }

            //load dropdown list
            ViewBag.MaNSX = new SelectList(db.NhaSanXuats.OrderBy(n => n.TenNSX), "MaNSX", "TenNSX",sp.MaNSX);
            ViewBag.MaLoai = new SelectList(db.LoaiSanPhams.OrderBy(n => n.TenLoai), "MaLoai", "TenLoai",sp.MaLoai);
            return View(sp);
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult ChinhSua(SanPham model)
        {
            ViewBag.MaNSX = new SelectList(db.NhaSanXuats.OrderBy(n => n.TenNSX), "MaNSX", "TenNSX", model.MaNSX);
            ViewBag.MaLoai = new SelectList(db.LoaiSanPhams.OrderBy(n => n.TenLoai), "MaLoai", "TenLoai", model.MaLoai);
            db.Entry(model).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Xoa(int? id)
        {
            //lay san pham can chinh sua bang id
            if (id == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            SanPham sp = db.SanPhams.SingleOrDefault(n => n.MaSp == id);
            if (sp == null)
            {
                return HttpNotFound();
            }

            //load dropdown list
            ViewBag.MaNSX = new SelectList(db.NhaSanXuats.OrderBy(n => n.TenNSX), "MaNSX", "TenNSX", sp.MaNSX);
            ViewBag.MaLoai = new SelectList(db.LoaiSanPhams.OrderBy(n => n.TenLoai), "MaLoai", "TenLoai", sp.MaLoai);
            return View(sp);
        }
        [HttpPost]
        public ActionResult Xoa (int? id, FormCollection f)
        {
            //lay san pham can chinh sua bang id
            if (id == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            SanPham sp = db.SanPhams.SingleOrDefault(n => n.MaSp == id);
            if (sp == null)
            {
                return HttpNotFound();
            }
            db.SanPhams.Remove(sp);
            db.SaveChanges();
            return RedirectToAction("Index"); 
        }
    }
}