using BLL_QuanLiQuanCafe;
using DTO_QuanLiQuanCafe;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static DTO_QuanLiQuanCafe.DTO_TaiKhoan;

namespace QuanLiQuanCafe
{
    public partial class FrmHoaDon : Form
    {
        private BLL_HoaDon bllHoaDon = new BLL_HoaDon();
        public FrmHoaDon()
        {
            InitializeComponent();
        }
        public DTO_TaiKhoan tkHienTai;
        public FrmHoaDon(DTO_TaiKhoan tk)
        {
            InitializeComponent(); // Dòng này bắt buộc phải có
            this.tkHienTai = tk;   // Lưu gói dữ liệu nhận được vào biến tkHienTai
            this.Load +=FrmTrangChu_Load;
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
            if (result == DialogResult.Yes)
            {
                FrmDangNhap frmDangNhap = new FrmDangNhap();
                this.Hide(); // Ẩn form hiện tại
                frmDangNhap.Show(); // Hiển thị form đăng nhập
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            FrmTrangChu frmTrangChu = new FrmTrangChu(tkHienTai);
            this.Hide(); // Ẩn form hiện tại
            frmTrangChu.Show(); // Hiển thị form trang chủ
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FrmBanHang frmBanHang = new FrmBanHang(tkHienTai);
            this.Hide(); // Ẩn form hiện tại
            frmBanHang.Show(); // Hiển thị form bán hàng
        }

        private void button2_Click(object sender, EventArgs e)
        {

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
            FrmTaiKhoan frmTaiKhoan = new FrmTaiKhoan(tkHienTai);
            this.Hide(); // Ẩn form hiện tại
            frmTaiKhoan.Show(); // Hiển thị form tài khoản
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //hỏi người dùng có chắc chắn muốn thoát không
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thoát không?", "Xác nhận thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                Application.Exit(); // Thoát ứng dụng
            }
        }
        private void LoadDanhSachHoaDon()
        {
            DateTime tuNgay = dtpTuNgay.Value;
            DateTime denNgay = dtpDenNgay.Value;
            string tuKhoa = txtTimKiem.Text.Trim();

            string loaiTimKiem = "";
            if (radMaHoaDon.Checked) loaiTimKiem = "MaHoaDon";
            else if (radBan.Checked) loaiTimKiem = "Ban";

            // Gọi BLL lấy dữ liệu
            DataTable dt = bllHoaDon.TraCuuHoaDon(tuNgay, denNgay, loaiTimKiem, tuKhoa);
            dataGridView1.DataSource = dt;

            // Làm đẹp các cột hiển thị
            if (dataGridView1.Columns.Count > 0)
            {
                dataGridView1.Columns["MaHoaDon"].HeaderText = "Mã Hóa Đơn";
                dataGridView1.Columns["TenBan"].HeaderText = "Tên Bàn";

                dataGridView1.Columns["ThoiGianVao"].HeaderText = "Giờ Vào";
                dataGridView1.Columns["ThoiGianVao"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";

                dataGridView1.Columns["ThoiGianRa"].HeaderText = "Giờ Ra";
                dataGridView1.Columns["ThoiGianRa"].DefaultCellStyle.Format = "dd/MM/yyyy HH:mm";

                dataGridView1.Columns["GiamGia"].HeaderText = "Giảm giá (%)";

                dataGridView1.Columns["TongTien"].HeaderText = "Tổng Tiền";
                dataGridView1.Columns["TongTien"].DefaultCellStyle.Format = "N0"; // Format tiền tệ

                dataGridView1.Columns["TrangThai"].HeaderText = "Trạng thái";

                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
        }
        private void FrmHoaDon_Load(object sender, EventArgs e)
        {
            button2.BackColor = Color.LightSkyBlue;
            button2.Font = new Font(button2.Font, FontStyle.Bold);

            Thread.CurrentThread.CurrentCulture = new CultureInfo("vi-VN");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("vi-VN");

            dtpTuNgay.Format = DateTimePickerFormat.Custom;
            dtpTuNgay.CustomFormat = "dddd, dd/MM/yyyy";

            dtpDenNgay.Format = DateTimePickerFormat.Custom;
            dtpDenNgay.CustomFormat = "dddd, dd/MM/yyyy";

            radMaHoaDon.Checked = true;

            DateTime today = DateTime.Now;
            DateTime firstDayOfMonth = new DateTime(today.Year, today.Month, 1);
            DateTime lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

            dtpTuNgay.Value = firstDayOfMonth;
            dtpDenNgay.Value = lastDayOfMonth;

            LoadDanhSachHoaDon();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            LoadDanhSachHoaDon();
        }
    }
}
