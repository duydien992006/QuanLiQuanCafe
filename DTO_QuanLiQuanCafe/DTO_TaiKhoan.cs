using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO_QuanLiQuanCafe
{
    public static class Session
    {
        // Biến static này sẽ lưu giữ tài khoản đang đăng nhập
        public static DTO_TaiKhoan TaiKhoanHienTai { get; set; }
    }
    public class DTO_TaiKhoan
    {
        public string TenDangNhap { get; set; }
        public string MatKhau { get; set; }
        public string TenHienThi { get; set; }
        public int LoaiTaiKhoan { get; set; }
        public int? NhanVienId { get; set; }

        public DTO_TaiKhoan() { }
        public DTO_TaiKhoan(string tenDangNhap, string matKhau, string tenHienThi, int loaiTaiKhoan, int? nhanVienId)
        {
            TenDangNhap = tenDangNhap;
            MatKhau = matKhau;
            TenHienThi = tenHienThi;
            LoaiTaiKhoan = loaiTaiKhoan;
            NhanVienId = nhanVienId;
        }
    }
}
