using DAL_QuanLiQuanCafe;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL_QuanLiQuanCafe
{
    public class BLL_NhanVien
    {
        private DAL_NhanVien dalNV = new DAL_NhanVien();
        public DataTable LayDanhSachNhanVien()
        {
            return dalNV.LayDanhSachNhanVien();
        }
        public bool ThemNhanVien(string tenNhanVien, string soDienThoai, string diaChi, DateTime ngayVaoLam)
        { return dalNV.ThemNhanVien(tenNhanVien, soDienThoai, diaChi, ngayVaoLam); }

        public bool SuaNhanVien(int id, string tenNhanVien, string soDienThoai, string diaChi, DateTime ngayVaoLam)
        { return dalNV.SuaNhanVien(id, tenNhanVien, soDienThoai, diaChi, ngayVaoLam); }

        public DataTable TimKiemNhanVien(string tenNhanVien, string soDienThoai, string diaChi)
        {
            return dalNV.TimKiemNhanVien(tenNhanVien, soDienThoai, diaChi);
        }

        public void XoaNhanVien(int id)
        {
            if (dalNV.KiemTraTonTaiTaiKhoan(id))
            {
                throw new Exception("Không thể xóa nhân viên này vì họ đang có tài khoản đăng nhập. Vui lòng xóa tài khoản trước!");
            }
            dalNV.XoaNhanVien(id);
        }
    }
}
