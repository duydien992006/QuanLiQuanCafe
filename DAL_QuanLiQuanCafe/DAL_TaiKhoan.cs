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
    public class DAL_TaiKhoan
    {
        public DTO_TaiKhoan KiemTraDangNhap(string user, string pass)
        {
            string query = "SELECT * FROM TaiKhoan WHERE TenDangNhap = @User AND MatKhau = @Pass";
            SqlParameter[] paras = new SqlParameter[]
            {
                new SqlParameter("@User", user),
                new SqlParameter("@Pass", pass)
            };
            DataTable dt = DAL_Helper.Instance.ExecuteQuery(query, paras);

            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                DTO_TaiKhoan tk = new DTO_TaiKhoan();
                tk.TenDangNhap = row["TenDangNhap"].ToString();
                tk.MatKhau = row["MatKhau"].ToString();
                tk.TenHienThi = row["TenHienThi"].ToString();
                tk.LoaiTaiKhoan = Convert.ToInt32(row["LoaiTaiKhoan"]);
                if (row["NhanVienId"] != DBNull.Value)
                {
                    tk.NhanVienId = Convert.ToInt32(row["NhanVienId"]);
                }
                else
                {
                    tk.NhanVienId = null;
                }

                return tk;
            }
            return null;
        }
        public DataTable LayDanhSachTaiKhoan()
        {
            string query = "SELECT * FROM TaiKhoan";
            DataTable dt = DAL_Helper.Instance.ExecuteQuery(query);
            return dt;
        }
        public bool ThemTaiKhoan(string tenDangNhap, string matKhau, string tenHienThi, int loaiTaiKhoan, int nhanVienId)
        {
            string query = "INSERT INTO TaiKhoan (TenDangNhap, MatKhau, TenHienThi, LoaiTaiKhoan, NhanVienId) VALUES (@TenDangNhap, @MatKhau, @TenHienThi, @LoaiTaiKhoan, @NhanVienId)";
            SqlParameter[] paras = {
                new SqlParameter("@TenDangNhap", tenDangNhap),
                new SqlParameter("@MatKhau", matKhau),
                new SqlParameter("@TenHienThi", tenHienThi),
                new SqlParameter("@LoaiTaiKhoan", loaiTaiKhoan),
                new SqlParameter("@NhanVienId", nhanVienId)
            };
            return DAL_Helper.Instance.ExecuteNonQuery(query, paras) > 0;
        }

        public bool SuaTaiKhoan(string tenDangNhap, string matKhau, string tenHienThi, int loaiTaiKhoan, int nhanVienId)
        {
            string query = "UPDATE TaiKhoan SET MatKhau = @MatKhau, TenHienThi = @TenHienThi, LoaiTaiKhoan = @LoaiTaiKhoan, NhanVienId = @NhanVienId WHERE TenDangNhap = @TenDangNhap";
            SqlParameter[] paras = {
                new SqlParameter("@MatKhau", matKhau),
                new SqlParameter("@TenHienThi", tenHienThi),
                new SqlParameter("@LoaiTaiKhoan", loaiTaiKhoan),
                new SqlParameter("@NhanVienId", nhanVienId),
                new SqlParameter("@TenDangNhap", tenDangNhap)
            };
            return DAL_Helper.Instance.ExecuteNonQuery(query, paras) > 0;
        }

        public bool XoaTaiKhoan(string tenDangNhap)
        {
            string query = "DELETE FROM TaiKhoan WHERE TenDangNhap = @TenDangNhap";
            SqlParameter[] paras = {
                new SqlParameter("@TenDangNhap", tenDangNhap)
            };
            return DAL_Helper.Instance.ExecuteNonQuery(query, paras) > 0;
        }
    }
}
