using DTO_QuanLiQuanCafe;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_QuanLiQuanCafe
{
    public class DAL_BanCafe
    {
        public DataTable LayDanhSachBan()
        {
            string query = "SELECT * FROM BanCafe";
            return DAL_Helper.Instance.ExecuteQuery(query);
        }
        public bool ThemBan(string tenBan, string trangThai)
        {
            string query = "INSERT INTO BanCafe (TenBan, TrangThai) VALUES (@TenBan, @TrangThai)";
            SqlParameter[] paras = {
                new SqlParameter("@TenBan", tenBan),
                new SqlParameter("@TrangThai", trangThai)
            };
            return DAL_Helper.Instance.ExecuteNonQuery(query, paras) > 0;
        }

        public bool SuaBan(int id, string tenBan, string trangThai)
        {
            string query = "UPDATE BanCafe SET TenBan = @TenBan, TrangThai = @TrangThai WHERE Id = @Id";
            SqlParameter[] paras = {
                new SqlParameter("@TenBan", tenBan),
                new SqlParameter("@TrangThai", trangThai),
                new SqlParameter("@Id", id)
            };
            return DAL_Helper.Instance.ExecuteNonQuery(query, paras) > 0;
        }

        public bool KiemTraBanDaCoHoaDon(int idBan)
        {
            string query = "SELECT COUNT(*) FROM HoaDon WHERE BanId = @Id";
            SqlParameter[] paras = { new SqlParameter("@Id", idBan) };

            int count = Convert.ToInt32(DAL_Helper.Instance.ExecuteScalar(query, paras));
            return count > 0;
        }
        public bool XoaBan(int id)
        {
            string query = "DELETE FROM BanCafe WHERE Id = @Id";
            SqlParameter[] paras = { new SqlParameter("@Id", id) };
            return DAL_Helper.Instance.ExecuteNonQuery(query, paras) > 0;
        }
        public DataTable TimKiemBan(string tenBan)
        {
            string query = "SELECT * FROM BanCafe WHERE TenBan LIKE @TenBan";
            SqlParameter[] paras = { new SqlParameter("@TenBan", "%" + tenBan + "%") };
            return DAL_Helper.Instance.ExecuteQuery(query, paras);
        }
    }
}
