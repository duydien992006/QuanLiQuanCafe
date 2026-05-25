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
    public class BLL_BanCafe
    {
        private DAL_BanCafe dalBan = new DAL_BanCafe();

        public DataTable LayDanhSachBan()
        {
            return dalBan.LayDanhSachBan();
        }
        public bool ThemBan(string tenBan, string trangThai) { return dalBan.ThemBan(tenBan, trangThai); }

        public bool SuaBan(int id, string tenBan, string trangThai) { return dalBan.SuaBan(id, tenBan, trangThai); }

        public DataTable TimKiemBan(string tenBan) { return dalBan.TimKiemBan(tenBan); }

        public void XoaBan(int id)
        {
            if (dalBan.KiemTraBanDaCoHoaDon(id))
            {
                throw new Exception("Không thể xóa bàn này vì nó đang chứa lịch sử hóa đơn của quán. Hành động này sẽ làm sai lệch doanh thu!");
            }

            dalBan.XoaBan(id);
        }
    }
}
