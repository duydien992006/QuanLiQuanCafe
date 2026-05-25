using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_QuanLiQuanCafe
{
    public class DAL_ThongKe
    {
        public DataTable LayDanhSachNam()
        {
            string query = "SELECT DISTINCT YEAR(ThoiGianVao) AS Nam FROM HoaDon WHERE TrangThai = 1 ORDER BY Nam DESC";
            return DAL_Helper.Instance.ExecuteQuery(query);
        }
        public DataTable DoanhThuTheoThang(int nam)
        {
            string query = @"
                SELECT 
                    sp.Thang, 
                    sp.SanPham, 
                    sp.SoLuongBan, 
                    dt.DoanhThu
                FROM (
                    -- BẢNG 1: Tính tổng số lượng từng món bán ra trong tháng
                    SELECT 
                        MONTH(h.ThoiGianVao) AS Thang,
                        t.TenMon AS SanPham,
                        SUM(c.SoLuong) AS SoLuongBan
                    FROM HoaDon h
                    JOIN ChiTietHoaDon c ON h.Id = c.HoaDonId
                    JOIN ThucDon t ON c.ThucDonId = t.Id
                    WHERE YEAR(h.ThoiGianVao) = @Nam AND h.TrangThai = 1
                    GROUP BY MONTH(h.ThoiGianVao), t.TenMon
                ) sp
                JOIN (
                    -- BẢNG 2: Tính tổng doanh thu THỰC TẾ của cả tháng (Chuẩn xác từ cột TongTien)
                    SELECT 
                        MONTH(ThoiGianVao) AS Thang,
                        SUM(TongTien) AS DoanhThu
                    FROM HoaDon
                    WHERE YEAR(ThoiGianVao) = @Nam AND TrangThai = 1
                    GROUP BY MONTH(ThoiGianVao)
                ) dt ON sp.Thang = dt.Thang
                ORDER BY sp.Thang ASC, sp.SoLuongBan DESC";

            SqlParameter[] paras = { new SqlParameter("@Nam", nam) };
            return DAL_Helper.Instance.ExecuteQuery(query, paras);
        }
        public DataTable ThongKeMonAn(int danhMucId)
        {
            string query = @"
                SELECT t.Id, t.TenMon, SUM(c.SoLuong) AS SoLanGoi
                FROM ChiTietHoaDon c
                JOIN HoaDon h ON c.HoaDonId = h.Id
                JOIN ThucDon t ON c.ThucDonId = t.Id
                WHERE h.TrangThai = 1 ";

            if (danhMucId > 0)
            {
                query += " AND t.DanhMucId = @DanhMucId ";
            }

            query += " GROUP BY t.Id, t.TenMon ORDER BY SoLanGoi DESC";

            if (danhMucId > 0)
            {
                SqlParameter[] paras = { new SqlParameter("@DanhMucId", danhMucId) };
                return DAL_Helper.Instance.ExecuteQuery(query, paras);
            }
            return DAL_Helper.Instance.ExecuteQuery(query);
        }
    }
}
