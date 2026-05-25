using DTO_QuanLiQuanCafe;
using BLL_QuanLiQuanCafe;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLiQuanCafe
{
    public partial class FrmTrangChu : Form
    {
        public DTO_TaiKhoan tkHienTai;
        public FrmTrangChu()
        {
            InitializeComponent();
        }
        public FrmTrangChu(DTO_TaiKhoan tk)
        {
            InitializeComponent();
            this.tkHienTai = tk;
            this.Load += FrmTrangChu_Load;
        }
        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }
        private void FrmTrangChu_Load(object sender, EventArgs e)
        {
            if (tkHienTai != null)
            {
                label1.Text = tkHienTai.TenHienThi;

                if (tkHienTai.LoaiTaiKhoan == 1)
                {
                    label2.Text = "Quản lý";
                }
                else
                {
                    label2.Text = "Nhân viên";
                }
            }
                if (label2.Text == "Nhân viên")
                {
                    button9.Visible = false;
                    button10.Visible = false;

                }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thoát không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            FrmTrangChu frm = new FrmTrangChu(tkHienTai);
            this.Hide();
            frm.Show();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button11_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn đăng xuất không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                FrmDangNhap frm = new FrmDangNhap();
                this.Hide();
                frm.Show();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FrmHoaDon frm = new FrmHoaDon(tkHienTai);
            this.Hide();
            frm.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FrmBanHang frm = new FrmBanHang(tkHienTai);
            this.Hide();
            frm.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FrmBan frm = new FrmBan(tkHienTai);
            this.Hide();
            frm.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FrmDanhMuc frm = new FrmDanhMuc(tkHienTai);
            this.Hide();
            frm.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            FrmThucDon frm = new FrmThucDon(tkHienTai);
            this.Hide();
            frm.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            FrmThongKe frm = new FrmThongKe(tkHienTai);
            this.Hide();
            frm.Show();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            FrmNhanVien frm = new FrmNhanVien(tkHienTai);
            this.Hide();
            frm.Show(); 
        }

        private void button10_Click(object sender, EventArgs e)
        {
            FrmTaiKhoan frm = new FrmTaiKhoan(tkHienTai);
            this.Hide();
            frm.Show();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void FrmTrangChu_Load_1(object sender, EventArgs e)
        {
            button5.BackColor = Color.LightSkyBlue;
            button5.Font = new Font(button5.Font, FontStyle.Bold);
        }
    }
}
