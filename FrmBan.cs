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
    public partial class FrmBan : Form
    {
        public FrmBan()
        {
            InitializeComponent();
        }
        public BLL_BanCafe bllBan = new BLL_BanCafe();

        public DTO_TaiKhoan tkHienTai;
        public FrmBan(DTO_TaiKhoan tk)
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
        private void LoadDanhSachBan()
        {
            dataGridView1.DataSource = bllBan.LayDanhSachBan();

            // Làm đẹp và việt hóa tên các cột hiển thị
            if (dataGridView1.Columns.Count > 0)
            {
                dataGridView1.Columns["Id"].HeaderText = "ID Bàn";
                dataGridView1.Columns["TenBan"].HeaderText = "Tên bàn";
                dataGridView1.Columns["TrangThai"].HeaderText = "Trạng thái";

                // Tự động giãn cột cho lấp đầy khoảng trống
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                // Thu nhỏ cột ID lại cho cân đối
                dataGridView1.Columns["Id"].FillWeight = 30;
                dataGridView1.Columns["TrangThai"].FillWeight = 60;
            }
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            FrmTrangChu frmTrangChu = new FrmTrangChu(tkHienTai);
            this.Hide();
            frmTrangChu.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FrmBanHang frmBanHang = new FrmBanHang(tkHienTai);
            this.Hide();
            frmBanHang.Show();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Bạn có chắc chắn muốn đăng xuất không?", "Đăng xuất", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                FrmDangNhap frmDangNhap = new FrmDangNhap();
                this.Hide();
                frmDangNhap.Show();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FrmHoaDon frmHoaDon = new FrmHoaDon(tkHienTai);
            this.Hide();
            frmHoaDon.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FrmDanhMuc frmDanhMuc = new FrmDanhMuc(tkHienTai);
            this.Hide();
            frmDanhMuc.Show();
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
            DialogResult dialogResult = MessageBox.Show("Bạn có chắc chắn muốn thoát không?", "Thoát", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void FrmBan_Load(object sender, EventArgs e)
        {
            LoadDanhSachBan();

            cbTrangThai.Items.Add("Trống");
            cbTrangThai.Items.Add("Có người");
            cbTrangThai.SelectedIndex = 0; // Mặc định là Trống

            button3.BackColor = Color.LightSkyBlue;
            button3.Font = new Font(button3.Font, FontStyle.Bold);

            txtId.Enabled = false;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                txtId.Text = row.Cells["Id"].Value.ToString();
                txtTenBan.Text = row.Cells["TenBan"].Value.ToString();

                // Đưa trạng thái lên ComboBox
                cbTrangThai.Text = row.Cells["TrangThai"].Value.ToString();
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            if (txtTenBan.Text.Trim() == "")
            {
                MessageBox.Show("Vui lòng nhập tên bàn!"); return;
            }

            if (bllBan.ThemBan(txtTenBan.Text.Trim(), cbTrangThai.Text))
            {
                MessageBox.Show("Thêm bàn thành công!");
                LoadDanhSachBan();
                txtTenBan.Clear();
                cbTrangThai.SelectedIndex = 0;
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if (txtId.Text == "")
            {
                MessageBox.Show("Vui lòng chọn bàn cần sửa!"); return;
            }

            int id = Convert.ToInt32(txtId.Text);
            if (bllBan.SuaBan(id, txtTenBan.Text.Trim(), cbTrangThai.Text))
            {
                MessageBox.Show("Cập nhật thành công!");
                LoadDanhSachBan();
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            if (txtId.Text == "")
            {
                MessageBox.Show("Vui lòng chọn bàn để xóa!"); return;
            }

            if (MessageBox.Show("Bạn có chắc muốn xóa bàn này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    int id = Convert.ToInt32(txtId.Text);
                    bllBan.XoaBan(id);

                    MessageBox.Show("Xóa thành công!");
                    LoadDanhSachBan();
                    txtId.Clear();
                    txtTenBan.Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Cảnh báo an toàn", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            string tuKhoa = txtTenBan.Text.Trim();
            dataGridView1.DataSource = bllBan.TimKiemBan(tuKhoa);
        }
    }
}
