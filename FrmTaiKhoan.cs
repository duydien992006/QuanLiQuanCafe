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

namespace QuanLiQuanCafe
{
    public partial class FrmTaiKhoan : Form
    {
        private BLL_TaiKhoan bllTK = new BLL_TaiKhoan();
        private void LoadDanhSachTaiKhoan()
        {
            // Cú pháp thần thánh để đổ dữ liệu phi kết nối lên WinForm
            dataGridView1.DataSource = bllTK.LayDanhSachTaiKhoan();

            // (Tùy chọn) Đổi lại tên cột cho tiếng Việt có dấu, cho đẹp giao diện
            if (dataGridView1.Columns.Count > 0)
            {
                dataGridView1.Columns["TenDangNhap"].HeaderText = "Tên đăng nhập";
                dataGridView1.Columns["MatKhau"].HeaderText = "Mật khẩu"; // Thực tế người ta hay ẩn cột này đi
                dataGridView1.Columns["TenHienThi"].HeaderText = "Tên hiển thị";
                dataGridView1.Columns["LoaiTaiKhoan"].HeaderText = "Loại tài khoản";
                dataGridView1.Columns["NhanVienId"].HeaderText = "ID Nhân viên";

                // Căn chỉnh độ rộng cột tự động lấp đầy khoảng trống
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
        }
        public FrmTaiKhoan()
        {
            InitializeComponent();
        }
        public DTO_TaiKhoan tkHienTai;
        public FrmTaiKhoan(DTO_TaiKhoan tk)
        {
            InitializeComponent(); // Dòng này bắt buộc phải có để nó vẽ Form
            this.tkHienTai = tk;   // Lưu gói dữ liệu nhận được vào biến tkHienTai
            this.Load += FrmTrangChu_Load;
        }
        private void FrmTrangChu_Load(object sender, EventArgs e)
        {
            if (tkHienTai != null)
            {
                label1.Text = tkHienTai.TenHienThi; // Hiện Tên hiển thị lên dòng trên

                if (tkHienTai.LoaiTaiKhoan == 1)
                {
                    label2.Text = "Quản lý"; // Hiện chức vụ lên dòng dưới
                }
                else
                {
                    label2.Text = "Nhân viên";
                }
            }
            //nếu label2 là nhân viên thì ẩn nút
            if (label2.Text == "Nhân viên")
            {
                button9.Visible = false; // Ẩn nút Quản lý nhân viên
                button10.Visible = false; // Ẩn nút Quản lý thực đơn

            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            //Hỏi người dùng có chắc chắn muốn đăng xuất không
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn đăng xuất không?", "Xác nhận đăng xuất", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes) {
                FrmDangNhap frmDangNhap = new FrmDangNhap();
                this.Hide(); // Ẩn form hiện tại
                frmDangNhap.Show(); // Hiển thị form đăng nhập
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FrmBanHang frmBanHang = new FrmBanHang(tkHienTai);
            this.Hide(); // Ẩn form hiện tại
            frmBanHang.Show(); // Hiển thị form bán hàng
        }

        private void button5_Click(object sender, EventArgs e)
        {
            FrmTrangChu frmTrangChu = new FrmTrangChu(tkHienTai);
            this.Hide(); // Ẩn form hiện tại
            frmTrangChu.Show(); // Hiển thị form trang chủ
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FrmHoaDon frmHoaDon = new FrmHoaDon(tkHienTai);
            this.Hide(); // Ẩn form hiện tại
            frmHoaDon.Show(); // Hiển thị form hóa đơn
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FrmBan frmBan = new FrmBan(tkHienTai);
            this.Hide(); // Ẩn form hiện tại
            frmBan.Show(); // Hiển thị form bàn
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FrmDanhMuc frmDanhMuc = new FrmDanhMuc(tkHienTai);
            this.Hide(); // Ẩn form hiện tại
            frmDanhMuc.Show(); // Hiển thị form danh mục
        }

        private void button7_Click(object sender, EventArgs e)
        {
            FrmThucDon frmThucDon = new FrmThucDon(tkHienTai);
            this.Hide(); // Ẩn form hiện tại
            frmThucDon.Show(); // Hiển thị form thực đơn
        }

        private void button8_Click(object sender, EventArgs e)
        {
            FrmThongKe frmThongKe = new FrmThongKe(tkHienTai);
            this.Hide(); // Ẩn form hiện tại
            frmThongKe.Show(); // Hiển thị form thống kê
        }

        private void button9_Click(object sender, EventArgs e)
        {
            FrmNhanVien frmNhanVien = new FrmNhanVien(tkHienTai);
            this.Hide(); // Ẩn form hiện tại
            frmNhanVien.Show(); // Hiển thị form nhân viên
        }

        private void button10_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            //hỏi người dùng muốn thoát không
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thoát không?", "Xác nhận thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Application.Exit(); // Thoát ứng dụng
            }
        }

        private void FrmTaiKhoan_Load(object sender, EventArgs e)
        {
            LoadDanhSachTaiKhoan();

            cbLoaiTaiKhoan.Items.Add("Nhân viên (0)");
            cbLoaiTaiKhoan.Items.Add("Quản trị viên (1)");
            cbLoaiTaiKhoan.SelectedIndex = 0;

            button10.BackColor = Color.LightSkyBlue;
            button10.Font = new Font(button10.Font, FontStyle.Bold);
        }

        private void button15_Click(object sender, EventArgs e)
        {
            txtTenDangNhap.Clear();
            txtMatKhau.Clear();
            txtTenHienThi.Clear();
            txtIdNhanVien.Clear();
            cbLoaiTaiKhoan.SelectedIndex = 0;
            txtTenDangNhap.Enabled = true;
            txtTenDangNhap.Focus();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                txtTenDangNhap.Text = row.Cells["TenDangNhap"].Value.ToString();
                txtMatKhau.Text = row.Cells["MatKhau"].Value.ToString();
                txtTenHienThi.Text = row.Cells["TenHienThi"].Value.ToString();
                txtIdNhanVien.Text = row.Cells["NhanVienId"].Value.ToString();
                cbLoaiTaiKhoan.SelectedIndex = Convert.ToInt32(row.Cells["LoaiTaiKhoan"].Value);
                txtTenDangNhap.Enabled = false;
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            if (txtTenDangNhap.Text == "" || txtMatKhau.Text == "" || txtIdNhanVien.Text == "")
            {
                MessageBox.Show("Vui lòng nhập đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                int loaiTK = cbLoaiTaiKhoan.SelectedIndex; // Sẽ lấy ra 0 hoặc 1
                int idNV = Convert.ToInt32(txtIdNhanVien.Text);

                if (bllTK.ThemTaiKhoan(txtTenDangNhap.Text, txtMatKhau.Text, txtTenHienThi.Text, loaiTK, idNV))
                {
                    MessageBox.Show("Đăng kí tài khoản thành công!", "Thông báo");
                    LoadDanhSachTaiKhoan();
                    button15_Click(sender, e);
                }
                else
                {
                    MessageBox.Show("Tên đăng nhập đã tồn tại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi định dạng ID Nhân viên hoặc dữ liệu: " + ex.Message);
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if (txtTenDangNhap.Text == "")
            {
                MessageBox.Show("Vui lòng chọn một tài khoản dưới bảng để cập nhật!", "Thông báo"); return;
            }

            int loaiTK = cbLoaiTaiKhoan.SelectedIndex;
            int idNV = Convert.ToInt32(txtIdNhanVien.Text);

            if (bllTK.SuaTaiKhoan(txtTenDangNhap.Text, txtMatKhau.Text, txtTenHienThi.Text, loaiTK, idNV))
            {
                MessageBox.Show("Cập nhật thành công!", "Thông báo");
                LoadDanhSachTaiKhoan();
                button15_Click(sender, e);
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            if (txtTenDangNhap.Text == "")
            {
                MessageBox.Show("Vui lòng chọn một tài khoản để xóa!", "Thông báo"); return;
            }

            // Tránh việc tự xóa chính tài khoản Admin đang đăng nhập
            if (Session.TaiKhoanHienTai != null && Session.TaiKhoanHienTai.TenDangNhap == txtTenDangNhap.Text)
            {
                MessageBox.Show("Bạn không thể tự xóa tài khoản đang đăng nhập!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Bạn có chắc chắn muốn xóa tài khoản " + txtTenDangNhap.Text + "?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (bllTK.XoaTaiKhoan(txtTenDangNhap.Text))
                {
                    MessageBox.Show("Xóa thành công!", "Thông báo");
                    LoadDanhSachTaiKhoan();
                    button15_Click(sender, e);
                }
            }
        }
    }
}
