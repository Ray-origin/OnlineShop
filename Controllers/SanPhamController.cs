using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.Mvc;
using OnlineShop.Models;
using PagedList;
using System.Data;
using System.Web.UI.HtmlControls;

namespace OnlineShop.Controllers
{
    public class SanPhamController : Controller
    {
        // GET: SanPham
        DataBanHangEntities1 db = new DataBanHangEntities1();
        //[ChildActionOnly]
        ////partial view 1
        //public ActionResult SanPhamStyle1Partial()
        //{
        //    return PartialView();
        //}
        public ActionResult DanhSachSP(int? MaLoaiSP, int? MaNhaSX, int? page)
        {
            if(MaNhaSX == null || MaLoaiSP == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            var lstSP = db.SanPhams.Where(n => n.MaLoai == MaLoaiSP && n.MaNSX == MaNhaSX);
            if (lstSP.Count() == 0 )
            {
                //thong bao ko co san pham  
                return HttpNotFound();
            }
            // phan trang
            if(Request.HttpMethod != "GET")
            {
                page = 1;
            }
            int PageSize = 6;
            // neu nhu page = 0 hoac ko co page thi gan = 1
            int PageNumber = (page ?? 1);
            ViewBag.MaLoaiSP = MaLoaiSP;
            ViewBag.MaNhaSX = MaNhaSX;    
            return View(lstSP.OrderBy(n=>n.MaSp).ToPagedList(PageNumber,PageSize));
        }
        public ActionResult ChiTietSP(int? id,string tenUrl)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            var CTSP = db.SanPhams.Where(n => n.MaSp == id).Take(1);
            //SanPham sp = db.SanPhams.Where(n => n.MaSp == Id).SingleOrDefault();
            return View(CTSP);
        }

    }  
} 
