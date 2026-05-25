using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_QuanLiQuanCafe
{
    public class DAL_NhanVien
    {
        public DataTable LayDanhSachNhanVien()
        {
            string query = "SELECT * FROM NhanVien";
            return DAL_Helper.Instance.ExecuteQuery(query);
        }
        public bool ThemNhanVien(string tenNhanVien, string soDienThoai, string diaChi, DateTime ngayVaoLam)
        {
            string query = "INSERT INTO NhanVien (TenNhanVien, SoDienThoai, DiaChi, NgayVaoLam) VALUES (@TenNhanVien, @SoDienThoai, @DiaChi, @NgayVaoLam)";
            SqlParameter[] paras = {
                new SqlParameter("@TenNhanVien", tenNhanVien),
                new SqlParameter("@SoDienThoai", soDienThoai),
                new SqlParameter("@DiaChi", diaChi),
                new SqlParameter("@NgayVaoLam", ngayVaoLam.Date)
            };
            return DAL_Helper.Instance.ExecuteNonQuery(query, paras) > 0;
        }

        public bool SuaNhanVien(int id, string tenNhanVien, string soDienThoai, string diaChi, DateTime ngayVaoLam)
        {
            string query = "UPDATE NhanVien SET TenNhanVien = @TenNhanVien, SoDienThoai = @SoDienThoai, DiaChi = @DiaChi, NgayVaoLam = @NgayVaoLam WHERE Id = @Id";
            SqlParameter[] paras = {
                new SqlParameter("@Id", id),
                new SqlParameter("@TenNhanVien", tenNhanVien),
                new SqlParameter("@SoDienThoai", soDienThoai),
                new SqlParameter("@DiaChi", diaChi),
                new SqlParameter("@NgayVaoLam", ngayVaoLam.Date)
            };
            return DAL_Helper.Instance.ExecuteNonQuery(query, paras) > 0;
        }

        public bool KiemTraTonTaiTaiKhoan(int idNhanVien)
        {
            string query = "SELECT COUNT(*) FROM TaiKhoan WHERE NhanVienId = @Id";
            SqlParameter[] paras = { new SqlParameter("@Id", idNhanVien) };
            return Convert.ToInt32(DAL_Helper.Instance.ExecuteScalar(query, paras)) > 0;
        }

        public bool XoaNhanVien(int id)
        {
            string query = "DELETE FROM NhanVien WHERE Id = @Id";
            SqlParameter[] paras = { new SqlParameter("@Id", id) };
            return DAL_Helper.Instance.ExecuteNonQuery(query, paras) > 0;
        }

        public DataTable TimKiemNhanVien(string tenNhanVien, string soDienThoai, string diaChi)
        {
            string query = "SELECT * FROM NhanVien WHERE 1=1";
            List<SqlParameter> paras = new List<SqlParameter>();
            if (!string.IsNullOrEmpty(tenNhanVien))
            {
                query += " AND TenNhanVien LIKE @Ten";
                paras.Add(new SqlParameter("@Ten", "%" + tenNhanVien + "%"));
            }
            if (!string.IsNullOrEmpty(soDienThoai))
            {
                query += " AND SoDienThoai LIKE @Sdt";
                paras.Add(new SqlParameter("@Sdt", "%" + soDienThoai + "%"));
            }
            if (!string.IsNullOrEmpty(diaChi))
            {
                query += " AND DiaChi LIKE @DiaChi";
                paras.Add(new SqlParameter("@DiaChi", "%" + diaChi + "%"));
            }
            return DAL_Helper.Instance.ExecuteQuery(query, paras.ToArray());
        }

    }
}
