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
    public class BLL_HoaDon
    {
        private DAL_HoaDon dalHoaDon = new DAL_HoaDon();

        public DataTable LayChiTietHoaDonTheoBan(int banId)
        {
            return dalHoaDon.LayChiTietHoaDonTheoBan(banId);
        }
        public void ThemMon(int banId, int thucDonId, int soLuong)
        {
            dalHoaDon.ThemMon(banId, thucDonId, soLuong);
        }

        public void XoaMon(int banId, int thucDonId)
        {
            dalHoaDon.XoaMon(banId, thucDonId);
        }
        public int LayIdHoaDonChuaThanhToan(int banId)
        {
            return dalHoaDon.LayIdHoaDonChuaThanhToan(banId);
        }

        public void ThanhToan(int hoaDonId, int banId, int giamGia, float tongTien)
        {
            dalHoaDon.ThanhToan(hoaDonId, banId, giamGia, tongTien);
        }

        public void ChuyenBan(int banCuId, int banMoiId)
        {
            dalHoaDon.ChuyenBan(banCuId, banMoiId);
        }
        public DataTable TraCuuHoaDon(DateTime tuNgay, DateTime denNgay, string loaiTimKiem, string tuKhoa)
        {
            return dalHoaDon.TraCuuHoaDon(tuNgay, denNgay, loaiTimKiem, tuKhoa);
        }
        public DataTable LayDuLieuInHoaDon(int idHoaDon)
        {
            return dalHoaDon.LayDuLieuInHoaDon(idHoaDon);
        }
    }
}
