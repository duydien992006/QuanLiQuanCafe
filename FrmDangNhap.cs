using BLL_QuanLiQuanCafe;
using DTO_QuanLiQuanCafe;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static DTO_QuanLiQuanCafe.DTO_TaiKhoan;

namespace QuanLiQuanCafe
{
    public partial class FrmDangNhap : Form
    {
        private BLL_TaiKhoan bllTK = new BLL_TaiKhoan();
        public FrmDangNhap()
        {
            InitializeComponent();
            // Đặt ký tự ẩn cho ô mật khẩu (chỉnh properties UseSystemPasswordChar = true cũng được)
            textBox2.PasswordChar = '*';
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Hỏi trước khi thoát
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thoát không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes) {
                                Application.Exit();
            }
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string user = textBox1.Text;
            string pass = textBox2.Text;

            DTO_TaiKhoan tkLogin = bllTK.DangNhap(user, pass);

            if (tkLogin != null) // Đăng nhập thành công
            {
                Session.TaiKhoanHienTai = tkLogin;
                // Truyền gói dữ liệu tkLogin sang Form Trang Chủ
                FrmTrangChu frm = new FrmTrangChu(tkLogin);

                this.Hide(); // Ẩn form đăng nhập
                frm.ShowDialog(); // Mở form trang chủ
            }
            else
            {
                MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

            if (checkBox1.Checked)
            {
                textBox2.PasswordChar = '\0';
            }
            else
            {
                textBox2.PasswordChar = '*';
            }
        }
    }
}
