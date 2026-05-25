using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO_QuanLiQuanCafe;
namespace DAL_QuanLiQuanCafe
{
    public class DAL_HoaDon
    {
        public DataTable TraCuuHoaDon(DateTime tuNgay, DateTime denNgay, string loaiTimKiem, string tuKhoa)
        {
            string query = @"
                SELECT hd.Id AS MaHoaDon, b.TenBan, hd.ThoiGianVao, hd.ThoiGianRa, hd.GiamGia, hd.TongTien,
                       CASE WHEN hd.TrangThai = 1 THEN N'Đã thanh toán' ELSE N'Chưa thanh toán' END AS TrangThai
                FROM HoaDon hd
                JOIN BanCafe b ON hd.BanId = b.Id
                WHERE CAST(hd.ThoiGianVao AS DATE) >= @TuNgay 
                  AND CAST(hd.ThoiGianVao AS DATE) <= @DenNgay ";
            if (!string.IsNullOrEmpty(tuKhoa))
            {
                if (loaiTimKiem == "MaHoaDon")
                    query += " AND CAST(hd.Id AS VARCHAR) LIKE @TuKhoa ";
                else if (loaiTimKiem == "Ban")
                    query += " AND b.TenBan LIKE @TuKhoa ";
            }
            query += " ORDER BY hd.Id DESC";

            SqlParameter[] paras = {
                new SqlParameter("@TuNgay", tuNgay.Date),
                new SqlParameter("@DenNgay", denNgay.Date),
                new SqlParameter("@TuKhoa", "%" + tuKhoa + "%")
            };

            return DAL_Helper.Instance.ExecuteQuery(query, paras);
        }
        public DataTable LayChiTietHoaDonTheoBan(int banId)
        {
            string query = @"SELECT td.Id AS ThucDonId, td.TenMon, cthd.SoLuong, td.Gia, (cthd.SoLuong * td.Gia) AS ThanhTien
                             FROM ChiTietHoaDon cthd
                             JOIN ThucDon td ON cthd.ThucDonId = td.Id
                             JOIN HoaDon hd ON cthd.HoaDonId = hd.Id
                             WHERE hd.BanId = @BanId AND hd.TrangThai = 0";

            SqlParameter[] paras = new SqlParameter[]
            {
                new SqlParameter("@BanId", banId)
            };

            return DAL_Helper.Instance.ExecuteQuery(query, paras);
        }
        public void ThemMon(int banId, int thucDonId, int soLuong)
        {
            string query = @"
            -- Thêm TOP 1 và ORDER BY Id DESC để luôn lấy hóa đơn mới nhất, chống lỗi dính 2 bill
            DECLARE @hoaDonId INT = (SELECT TOP 1 Id FROM HoaDon WHERE BanId = @BanId AND TrangThai = 0 ORDER BY Id DESC);
            
            -- 1. Bàn trống: Tạo bill mới và đổi trạng thái bàn thành Có người
            IF (@hoaDonId IS NULL AND @SoLuong > 0)
            BEGIN
                INSERT INTO HoaDon (BanId, ThoiGianVao, TrangThai, TongTien) VALUES (@BanId, GETDATE(), 0, 0);
                SET @hoaDonId = SCOPE_IDENTITY();
                UPDATE BanCafe SET TrangThai = N'Có người' WHERE Id = @BanId;
            END

            -- 2. Xử lý món ăn (Thêm mới hoặc Cộng dồn)
            -- Thêm TOP 1 để chống dính 2 dòng chi tiết món bị trùng
            DECLARE @chiTietId INT = (SELECT TOP 1 Id FROM ChiTietHoaDon WHERE HoaDonId = @hoaDonId AND ThucDonId = @ThucDonId);

            IF (@chiTietId IS NOT NULL)
            BEGIN
                UPDATE ChiTietHoaDon SET SoLuong = SoLuong + @SoLuong WHERE Id = @chiTietId;
                
                -- Xóa nếu số lượng tụt xuống <= 0
                IF ((SELECT TOP 1 SoLuong FROM ChiTietHoaDon WHERE Id = @chiTietId) <= 0)
                    DELETE FROM ChiTietHoaDon WHERE Id = @chiTietId;
            END
            ELSE IF (@SoLuong > 0)
            BEGIN
                INSERT INTO ChiTietHoaDon (HoaDonId, ThucDonId, SoLuong) VALUES (@hoaDonId, @ThucDonId, @SoLuong);
            END

            -- 3. Hủy bill và trả bàn nếu xóa sạch món
            IF NOT EXISTS (SELECT 1 FROM ChiTietHoaDon WHERE HoaDonId = @hoaDonId)
            BEGIN
                DELETE FROM HoaDon WHERE Id = @hoaDonId;
                UPDATE BanCafe SET TrangThai = N'Trống' WHERE Id = @BanId;
            END";

            SqlParameter[] paras = {
                new SqlParameter("@BanId", banId),
                new SqlParameter("@ThucDonId", thucDonId),
                new SqlParameter("@SoLuong", soLuong)
            };
            DAL_Helper.Instance.ExecuteNonQuery(query, paras);
        }

        public void XoaMon(int banId, int thucDonId)
        {
            ThemMon(banId, thucDonId, -10000);
        }
        public int LayIdHoaDonChuaThanhToan(int banId)
        {
            string query = "SELECT Id FROM HoaDon WHERE BanId = @BanId AND TrangThai = 0";
            SqlParameter[] paras = { new SqlParameter("@BanId", banId) };

            object result = DAL_Helper.Instance.ExecuteScalar(query, paras);
            return result != null ? Convert.ToInt32(result) : -1;
        }
        public void ThanhToan(int hoaDonId, int banId, int giamGia, float tongTien)
        {
            string query = @"
                -- Cập nhật Hóa đơn thành Đã thanh toán (1)
                UPDATE HoaDon 
                SET TrangThai = 1, ThoiGianRa = GETDATE(), GiamGia = @GiamGia, TongTien = @TongTien 
                WHERE Id = @HoaDonId;
                
                -- Trả bàn về trạng thái Trống
                UPDATE BanCafe SET TrangThai = N'Trống' WHERE Id = @BanId;";

            SqlParameter[] paras = {
                new SqlParameter("@HoaDonId", hoaDonId),
                new SqlParameter("@BanId", banId),
                new SqlParameter("@GiamGia", giamGia),
                new SqlParameter("@TongTien", tongTien)
            };
            DAL_Helper.Instance.ExecuteNonQuery(query, paras);
        }
        public void ChuyenBan(int banCuId, int banMoiId)
        {
            string query = @"
                DECLARE @hoaDonId INT = (SELECT Id FROM HoaDon WHERE BanId = @BanCuId AND TrangThai = 0);
                IF (@hoaDonId IS NOT NULL)
                BEGIN
                    -- Đổi hóa đơn sang bàn mới
                    UPDATE HoaDon SET BanId = @BanMoiId WHERE Id = @hoaDonId;
                    -- Cập nhật trạng thái 2 bàn
                    UPDATE BanCafe SET TrangThai = N'Trống' WHERE Id = @BanCuId;
                    UPDATE BanCafe SET TrangThai = N'Có người' WHERE Id = @BanMoiId;
                END";

            SqlParameter[] paras = {
                new SqlParameter("@BanCuId", banCuId),
                new SqlParameter("@BanMoiId", banMoiId)
            };
            DAL_Helper.Instance.ExecuteNonQuery(query, paras);
        }
        public DataTable LayDuLieuInHoaDon(int idHoaDon)
        {
            string query = @"
                SELECT 
                    b.TenBan, 
                    t.TenMon, 
                    c.SoLuong, 
                    t.Gia AS DonGia, 
                    (c.SoLuong * t.Gia) AS ThanhTien, 
                    h.GiamGia,
                    h.TongTien, 
                    h.ThoiGianVao,
                    h.ThoiGianRa
                FROM HoaDon h
                JOIN BanCafe b ON h.BanId = b.Id
                JOIN ChiTietHoaDon c ON h.Id = c.HoaDonId
                JOIN ThucDon t ON c.ThucDonId = t.Id
                WHERE h.Id = @IdHoaDon";

            SqlParameter[] paras = { new SqlParameter("@IdHoaDon", idHoaDon) };
            return DAL_Helper.Instance.ExecuteQuery(query, paras);
        }
    }
}
