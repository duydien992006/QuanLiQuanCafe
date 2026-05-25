using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_QuanLiQuanCafe
{
    public class DAL_ThucDon
    {
        public DataTable LayDanhSachThucDon()
        {
            string query = "SELECT * FROM ThucDon WHERE NgungBan = 0 OR NgungBan IS NULL";
            return DAL_Helper.Instance.ExecuteQuery(query);
        }
        public bool ThemThucDon(string tenMon, int danhMucId, float gia)
        {
            string query = "INSERT INTO ThucDon (TenMon, DanhMucId, Gia, NgungBan) VALUES (@TenMon, @DanhMucId, @Gia, 0)";
            SqlParameter[] paras = {
                new SqlParameter("@TenMon", tenMon),
                new SqlParameter("@DanhMucId", danhMucId),
                new SqlParameter("@Gia", gia)
            };
            return DAL_Helper.Instance.ExecuteNonQuery(query, paras) > 0;
        }
        public bool SuaThucDon(int id, string tenMon, int danhMucId, float gia)
        {
            string query = "UPDATE ThucDon SET TenMon = @TenMon, DanhMucId = @DanhMucId, Gia = @Gia WHERE Id = @Id";
            SqlParameter[] paras = {
                new SqlParameter("@TenMon", tenMon),
                new SqlParameter("@DanhMucId", danhMucId),
                new SqlParameter("@Gia", gia),
                new SqlParameter("@Id", id)
            };
            return DAL_Helper.Instance.ExecuteNonQuery(query, paras) > 0;
        }
        public bool XoaThucDon(int id)
        {
            string query = "UPDATE ThucDon SET NgungBan = 1 WHERE Id = @Id";
            SqlParameter[] paras = { new SqlParameter("@Id", id) };
            return DAL_Helper.Instance.ExecuteNonQuery(query, paras) > 0;
        }
        public DataTable LayThucDonTheoDanhMuc(int danhMucId)
        {
            string query = "SELECT * FROM ThucDon WHERE DanhMucId = @Id AND (NgungBan = 0 OR NgungBan IS NULL)";
            SqlParameter[] paras = { new SqlParameter("@Id", danhMucId) };
            return DAL_Helper.Instance.ExecuteQuery(query, paras);
        }
        public int LayDanhMucIdTheoMon(int idMon)
        {
            string query = "SELECT DanhMucId FROM ThucDon WHERE Id = @Id";
            SqlParameter[] paras = { new SqlParameter("@Id", idMon) };

            object result = DAL_Helper.Instance.ExecuteScalar(query, paras);

            if (result != null)
            {
                return Convert.ToInt32(result);
            }
            return 0; // Trả về 0 nếu có lỗi không tìm thấy
        }
        public DataTable TimKiemThucDon(string tenMon, int danhMucId, float gia)
        {
            string query = "SELECT * FROM ThucDon WHERE (NgungBan = 0 OR NgungBan IS NULL)";

            List<SqlParameter> paras = new List<SqlParameter>();

            if (!string.IsNullOrEmpty(tenMon))
            {
                query += " AND TenMon LIKE @TenMon";
                paras.Add(new SqlParameter("@TenMon", "%" + tenMon + "%"));
            }

            if (danhMucId > 0)
            {
                query += " AND DanhMucId = @DanhMucId";
                paras.Add(new SqlParameter("@DanhMucId", danhMucId));
            }

            if (gia >= 0)
            {
                query += " AND Gia = @Gia";
                paras.Add(new SqlParameter("@Gia", gia));
            }

            return DAL_Helper.Instance.ExecuteQuery(query, paras.ToArray());
        }
    }
}
