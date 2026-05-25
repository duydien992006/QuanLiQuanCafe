using DAL_QuanLiQuanCafe;
using DTO_QuanLiQuanCafe;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL_QuanLiQuanCafe
{
    public class BLL_TaiKhoan
    {
        private DAL_TaiKhoan dalTK = new DAL_TaiKhoan();

        public DTO_TaiKhoan DangNhap(string user, string pass)
        {
            if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(pass))
            {
                return null;
            }
            return dalTK.KiemTraDangNhap(user, pass);
        }
        public DataTable LayDanhSachTaiKhoan()
        {
            return dalTK.LayDanhSachTaiKhoan();
        }
        public bool ThemTaiKhoan(string tenDangNhap, string matKhau, string tenHienThi, int loaiTaiKhoan, int nhanVienId)
        {
            return dalTK.ThemTaiKhoan(tenDangNhap, matKhau, tenHienThi, loaiTaiKhoan, nhanVienId);
        }

        public bool SuaTaiKhoan(string tenDangNhap, string matKhau, string tenHienThi, int loaiTaiKhoan, int nhanVienId)
        {
            return dalTK.SuaTaiKhoan(tenDangNhap, matKhau, tenHienThi, loaiTaiKhoan, nhanVienId);
        }

        public bool XoaTaiKhoan(string tenDangNhap)
        {
            return dalTK.XoaTaiKhoan(tenDangNhap);
        }
    }
}
