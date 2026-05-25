using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_QuanLiQuanCafe
{
    public class DAL_DanhMuc
    {
        public DataTable LayDanhSachDanhMuc()
        {
            string query = "SELECT * FROM DanhMuc";
            return DAL_Helper.Instance.ExecuteQuery(query);
        }
        public bool ThemDanhMuc(string tenDanhMuc)
        {
            string query = "INSERT INTO DanhMuc (TenDanhMuc) VALUES (@Ten)";
            SqlParameter[] paras = { new SqlParameter("@Ten", tenDanhMuc) };
            return DAL_Helper.Instance.ExecuteNonQuery(query, paras) > 0;
        }
        public bool SuaDanhMuc(int id, string tenDanhMuc)
        {
            string query = "UPDATE DanhMuc SET TenDanhMuc = @Ten WHERE Id = @Id";
            SqlParameter[] paras = {
                new SqlParameter("@Ten", tenDanhMuc),
                new SqlParameter("@Id", id)
            };
            return DAL_Helper.Instance.ExecuteNonQuery(query, paras) > 0;
        }
        public bool KiemTraTonTaiMonAn(int idDanhMuc)
        {
            string query = "SELECT COUNT(*) FROM ThucDon WHERE DanhMucId = @Id AND (NgungBan = 0 OR NgungBan IS NULL)";
            SqlParameter[] paras = { new SqlParameter("@Id", idDanhMuc) };

            int count = Convert.ToInt32(DAL_Helper.Instance.ExecuteScalar(query, paras));
            return count > 0;
        }
        public bool XoaDanhMuc(int id)
        {
            string query = "DELETE FROM DanhMuc WHERE Id = @Id";
            SqlParameter[] paras = { new SqlParameter("@Id", id) };
            return DAL_Helper.Instance.ExecuteNonQuery(query, paras) > 0;
        }
        public DataTable TimKiemDanhMuc(string tenDanhMuc)
        {
            string query = "SELECT * FROM DanhMuc WHERE TenDanhMuc LIKE @Ten";
            SqlParameter[] paras = { new SqlParameter("@Ten", "%" + tenDanhMuc + "%") };
            return DAL_Helper.Instance.ExecuteQuery(query, paras);
        }
    }
}
