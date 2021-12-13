using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShop.Models
{
    public class ItemGioHang
    {
        public int MaSP { get; set; }
        public string TenSp { get; set; }
        public int Soluong { get; set; }
        public decimal DonGia { get; set; }
        public decimal ThanhTien { get; set; }
        public string HinhAnh { get; set; }
        public ItemGioHang(int iMaSp, int soluong)
        {
            using(DataBanHangEntities1 db = new DataBanHangEntities1())
            {
                this.MaSP = iMaSp;
                SanPham sp = db.SanPhams.Single(n => n.MaSp == iMaSp);
                this.TenSp = sp.TenSp;
                this.HinhAnh = sp.HinhAnh;
                this.DonGia = sp.DonGia.Value;
                this.Soluong = soluong;
                this.ThanhTien = DonGia * Soluong;
            }
        }
        public ItemGioHang(int iMaSp)
        {
            using (DataBanHangEntities1 db = new DataBanHangEntities1())
            {
                this.MaSP = iMaSp;
                SanPham sp = db.SanPhams.Single(n => n.MaSp == iMaSp);
                this.TenSp = sp.TenSp;
                this.HinhAnh = sp.HinhAnh;
                this.Soluong = 1;
                this.DonGia = sp.DonGia.Value;
                this.ThanhTien = DonGia * Soluong;
            }
        }
        public ItemGioHang()
        {

        }
    }
}