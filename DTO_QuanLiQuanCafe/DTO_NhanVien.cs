using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO_QuanLiQuanCafe
{
    public class DTO_NhanVien
    {
        public int Id { get; set; }
        public string TenNhanVien { get; set; }
        public string DiaChi { get; set; }
        public string SoDienThoai { get; set; }
        public DateTime NgayVaoLam { get; set; }
        public DTO_NhanVien() { }
        public DTO_NhanVien(int id, string tenNhanVien, string diaChi, string soDienThoai, DateTime ngayVaoLam)
        {
            Id = id;
            TenNhanVien = tenNhanVien;
            DiaChi = diaChi;
            SoDienThoai = soDienThoai;
            NgayVaoLam = ngayVaoLam;
        }
    }
}
