using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL_QuanLiQuanCafe
{
    public class DAL_Helper
    {
        // 1. Khởi tạo Singleton Pattern để đảm bảo chỉ có 1 luồng kết nối CSDL duy nhất chạy trong RAM
        private static DAL_Helper instance;
        public static DAL_Helper Instance
        {
            get { if (instance == null) instance = new DAL_Helper(); return DAL_Helper.instance; }
            private set { DAL_Helper.instance = value; }
        }

        private DAL_Helper() { } // Chặn không cho khởi tạo linh tinh bằng lệnh 'new' từ bên ngoài

        private string connectionString = @"Data Source=DESKTOP-KLF3EBO;Initial Catalog=QuanLyQuanCafe;Integrated Security=True";

        public DataTable ExecuteQuery(string query, SqlParameter[] parameters = null)
        {
            DataTable data = new DataTable();


            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(data);
                    }
                }
            }
            return data;
        }

        public int ExecuteNonQuery(string query, SqlParameter[] parameters = null)
        {
            int data = 0;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }
                    data = command.ExecuteNonQuery();
                }
            }
            return data;
        }
        public object ExecuteScalar(string query, SqlParameter[] parameters = null)
        {
            object data = 0;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    if (parameters != null)
                    {
                        command.Parameters.AddRange(parameters);
                    }
                    data = command.ExecuteScalar();
                }
            }
            return data;
        }
    }
}
