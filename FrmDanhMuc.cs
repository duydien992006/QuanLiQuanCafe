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
    public partial class FrmDanhMuc : Form
    {
        public FrmDanhMuc()
        {
            InitializeComponent();
        }
        private BLL_DanhMuc bllDanhMuc = new BLL_DanhMuc();

        public DTO_TaiKhoan tkHienTai;
        public FrmDanhMuc(DTO_TaiKhoan tk)
        {
            InitializeComponent();
            this.tkHienTai = tk;
            this.Load += FrmTrangChu_Load;
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
        private void LoadDanhSachDanhMuc()
        {
            dataGridView1.DataSource = bllDanhMuc.LayDanhSachDanhMuc();
            if (dataGridView1.Columns.Count > 0)
            {
                dataGridView1.Columns["Id"].HeaderText = "ID Danh mục";
                dataGridView1.Columns["TenDanhMuc"].HeaderText = "Tên danh mục";
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridView1.Columns["Id"].FillWeight = 30;
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn đăng xuất không?", "Xác nhận đăng xuất", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                FrmDangNhap frmDangNhap = new FrmDangNhap();
                this.Hide();
                frmDangNhap.Show();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            FrmTrangChu frmTrangChu = new FrmTrangChu(tkHienTai);
            this.Hide();
            frmTrangChu.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            FrmHoaDon frmHoaDon = new FrmHoaDon(tkHienTai);
            this.Hide();
            frmHoaDon.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FrmBan frmBan = new FrmBan(tkHienTai);
            this.Hide();
            frmBan.Show();
        }
        private void button7_Click(object sender, EventArgs e)
        {
            FrmThucDon frmThucDon = new FrmThucDon(tkHienTai);
            this.Hide();
            frmThucDon.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            FrmThongKe frmThongKe = new FrmThongKe(tkHienTai);
            this.Hide();
            frmThongKe.Show();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            FrmNhanVien frmNhanVien = new FrmNhanVien(tkHienTai);
            this.Hide();
            frmNhanVien.Show();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            FrmTaiKhoan frmTaiKhoan = new FrmTaiKhoan(tkHienTai);
            this.Hide();
            frmTaiKhoan.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thoát không?", "Xác nhận thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void FrmDanhMuc_Load(object sender, EventArgs e)
        {
            LoadDanhSachDanhMuc();
            txtId.Enabled = false;
            button4.BackColor = Color.LightSkyBlue;
            button4.Font = new Font(button4.Font, FontStyle.Bold);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                txtId.Text = row.Cells["Id"].Value.ToString();
                txtTenDanhMuc.Text = row.Cells["TenDanhMuc"].Value.ToString();
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            if (txtTenDanhMuc.Text.Trim() == "")
            {
                MessageBox.Show("Vui lòng nhập tên danh mục!"); return;
            }

            if (bllDanhMuc.ThemDanhMuc(txtTenDanhMuc.Text.Trim()))
            {
                MessageBox.Show("Thêm thành công!");
                LoadDanhSachDanhMuc();
                txtTenDanhMuc.Clear();
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if (txtId.Text == "")
            {
                MessageBox.Show("Vui lòng chọn danh mục cần sửa!"); return;
            }

            int id = Convert.ToInt32(txtId.Text);
            if (bllDanhMuc.SuaDanhMuc(id, txtTenDanhMuc.Text.Trim()))
            {
                MessageBox.Show("Cập nhật thành công!");
                LoadDanhSachDanhMuc();
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            if (txtId.Text == "")
            {
                MessageBox.Show("Vui lòng chọn danh mục để xóa!"); return;
            }

            if (MessageBox.Show("Bạn có chắc muốn xóa danh mục này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    int id = Convert.ToInt32(txtId.Text);
                    bllDanhMuc.XoaDanhMuc(id);

                    MessageBox.Show("Xóa thành công!");
                    LoadDanhSachDanhMuc();
                    txtId.Clear();
                    txtTenDanhMuc.Clear();
                }
                catch (Exception ex)
                {
                    // Hứng cái Exception từ BLL ném lên và hiển thị cực kỳ lịch sự
                    MessageBox.Show(ex.Message, "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            string tuKhoa = txtTenDanhMuc.Text.Trim();
            dataGridView1.DataSource = bllDanhMuc.TimKiemDanhMuc(tuKhoa);
        }
    }
}
