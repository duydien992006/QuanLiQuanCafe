using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL_QuanLiQuanCafe;
using DTO_QuanLiQuanCafe;

namespace BLL_QuanLiQuanCafe
{
    public class BLL_DanhMuc
    {
        private DAL_DanhMuc dalDanhMuc = new DAL_DanhMuc();
        public DataTable LayDanhSachDanhMuc()
        {
            return dalDanhMuc.LayDanhSachDanhMuc();
        }
        public bool ThemDanhMuc(string tenDanhMuc) { return dalDanhMuc.ThemDanhMuc(tenDanhMuc); }

        public bool SuaDanhMuc(int id, string tenDanhMuc) { return dalDanhMuc.SuaDanhMuc(id, tenDanhMuc); }

        public DataTable TimKiemDanhMuc(string tenDanhMuc) { return dalDanhMuc.TimKiemDanhMuc(tenDanhMuc); }
        public void XoaDanhMuc(int id)
        {
            if (dalDanhMuc.KiemTraTonTaiMonAn(id))
            {
                throw new Exception("Không thể xóa danh mục này vì vẫn còn món ăn đang thuộc danh mục này. Vui lòng xóa món ăn trước!");
            }
            dalDanhMuc.XoaDanhMuc(id);
        }
    }
}
