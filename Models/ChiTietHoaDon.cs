//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OnlineShop.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ChiTietHoaDon
    {
        public int MaCTHD { get; set; }
        public Nullable<int> MaSp { get; set; }
        public string TenSp { get; set; }
        public Nullable<int> SoLuong { get; set; }
        public Nullable<decimal> DonGia { get; set; }
        public Nullable<int> MaHD { get; set; }
    
        public virtual HoaDon HoaDon { get; set; }
    }
}
