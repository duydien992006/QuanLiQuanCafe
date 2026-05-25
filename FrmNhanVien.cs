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

namespace QuanLiQuanCafe
{
    public partial class FrmNhanVien : Form
    {
        private BLL_TaiKhoan bllTK = new BLL_TaiKhoan();
        private BLL_NhanVien bllNV = new BLL_NhanVien();
        private int idNhanVienDangChon = -1;
        public FrmNhanVien()
        {
            InitializeComponent();
        }
        public DTO_TaiKhoan tkHienTai;
        public FrmNhanVien(DTO_TaiKhoan tk)
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
        private void label3_Click(object sender, EventArgs e)
        {

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

        private void button1_Click(object sender, EventArgs e)
        {
            FrmBanHang frmBanHang = new FrmBanHang(tkHienTai);
            this.Hide();
            frmBanHang.Show();
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

        private void FrmNhanVien_Load(object sender, EventArgs e)
        {
            LoadDanhSachNhanVien();
            button9.BackColor = Color.LightSkyBlue;
            button9.Font = new Font(button9.Font, FontStyle.Bold);

            Thread.CurrentThread.CurrentCulture = new CultureInfo("vi-VN");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("vi-VN");

            dtpNgayVaoLam.Format = DateTimePickerFormat.Custom;
            dtpNgayVaoLam.CustomFormat = "dd/MM/yyyy";
        }
        private void LoadDanhSachNhanVien()
        {
            dataGridView1.DataSource = bllNV.LayDanhSachNhanVien();
            if (dataGridView1.Columns.Count > 0)
            {
                if (dataGridView1.Columns.Contains("Id"))
                    dataGridView1.Columns["Id"].HeaderText = "Mã NV";
                if (dataGridView1.Columns.Contains("TenNhanVien"))
                    dataGridView1.Columns["TenNhanVien"].HeaderText = "Họ và tên";
                if (dataGridView1.Columns.Contains("SoDienThoai"))
                    dataGridView1.Columns["SoDienThoai"].HeaderText = "Số điện thoại";
                if (dataGridView1.Columns.Contains("DiaChi"))
                    dataGridView1.Columns["DiaChi"].HeaderText = "Địa chỉ";
                if (dataGridView1.Columns.Contains("NgayVaoLam"))
                {
                    dataGridView1.Columns["NgayVaoLam"].HeaderText = "Ngày vào làm";
                    dataGridView1.Columns["NgayVaoLam"].DefaultCellStyle.Format = "dd/MM/yyyy";
                }
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if (txtHoTen.Text.Trim() == "")
            {
                MessageBox.Show("Vui lòng nhập Họ tên nhân viên!"); return;
            }

            if (bllNV.ThemNhanVien(txtHoTen.Text.Trim(), txtSoDienThoai.Text.Trim(), txtDiaChi.Text.Trim(), dtpNgayVaoLam.Value))
            {
                MessageBox.Show("Thêm thành công!");
                LoadDanhSachNhanVien();
                ClearInputs();
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            if (idNhanVienDangChon == -1)
            {
                MessageBox.Show("Vui lòng click chọn nhân viên dưới bảng để sửa!"); return;
            }

            if (bllNV.SuaNhanVien(idNhanVienDangChon, txtHoTen.Text.Trim(), txtSoDienThoai.Text.Trim(), txtDiaChi.Text.Trim(), dtpNgayVaoLam.Value))
            {
                MessageBox.Show("Cập nhật thành công!");
                LoadDanhSachNhanVien();
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            if (idNhanVienDangChon == -1) return;

            if (MessageBox.Show($"Xóa nhân viên: {txtHoTen.Text}?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                try
                {
                    bllNV.XoaNhanVien(idNhanVienDangChon);
                    MessageBox.Show("Xóa thành công!");
                    LoadDanhSachNhanVien();
                    ClearInputs();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            string ten = txtHoTen.Text.Trim();
            string sdt = txtSoDienThoai.Text.Trim();
            string diaChi = txtDiaChi.Text.Trim();

            DataTable dtKetQua = bllNV.TimKiemNhanVien(ten, sdt, diaChi);

            dataGridView1.DataSource = dtKetQua;

            if (dtKetQua.Rows.Count == 0)
            {
                MessageBox.Show("Không tìm thấy nhân viên nào khớp với thông tin bạn nhập!", "Kết quả tìm kiếm", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void ClearInputs()
        {
            txtHoTen.Clear();
            txtSoDienThoai.Clear();
            txtDiaChi.Clear();
            dtpNgayVaoLam.Value = DateTime.Now;
            idNhanVienDangChon = -1;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                idNhanVienDangChon = Convert.ToInt32(row.Cells["Id"].Value);
                txtHoTen.Text = row.Cells["TenNhanVien"].Value.ToString();
                txtSoDienThoai.Text = row.Cells["SoDienThoai"].Value.ToString();
                txtDiaChi.Text = row.Cells["DiaChi"].Value.ToString();

                if (row.Cells["NgayVaoLam"].Value != DBNull.Value)
                {
                    dtpNgayVaoLam.Value = Convert.ToDateTime(row.Cells["NgayVaoLam"].Value);
                }
            }
        }
    }
}
