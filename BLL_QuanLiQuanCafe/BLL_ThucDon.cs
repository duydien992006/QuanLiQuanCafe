using DAL_QuanLiQuanCafe;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL_QuanLiQuanCafe
{
    public class BLL_ThucDon
    {
        private DAL_ThucDon dalThucDon = new DAL_ThucDon();

        public DataTable LayThucDonTheoDanhMuc(int danhMucId)
        {
            return dalThucDon.LayThucDonTheoDanhMuc(danhMucId);
        }
        public int LayDanhMucIdTheoMon(int idMon)
        {
            return dalThucDon.LayDanhMucIdTheoMon(idMon);
        }
        public DataTable LayDanhSachThucDon() { return dalThucDon.LayDanhSachThucDon();}

        public bool ThemThucDon(string tenMon, int danhMucId, float gia) { return dalThucDon.ThemThucDon(tenMon, danhMucId, gia); }

        public bool SuaThucDon(int id, string tenMon, int danhMucId, float gia) { return dalThucDon.SuaThucDon(id, tenMon, danhMucId, gia); }

        public bool XoaThucDon(int id) { return dalThucDon.XoaThucDon(id); }

        public DataTable TimKiemThucDonDaNang(string tenMon, int danhMucId, float gia)
        {
            return dalThucDon.TimKiemThucDon(tenMon, danhMucId, gia);
        }
    }
}
