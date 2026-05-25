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
    public partial class FrmThucDon : Form
    {
        public FrmThucDon()
        {
            InitializeComponent();
        }
        private BLL_ThucDon bllThucDon = new BLL_ThucDon();
        private BLL_DanhMuc bllDanhMuc = new BLL_DanhMuc();
        public DTO_TaiKhoan tkHienTai;
        public FrmThucDon(DTO_TaiKhoan tk)
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
            LoadDanhSachThucDon();
        }
        private void LoadDanhSachThucDon()
        {
            // Đổ dữ liệu vào lưới
            dataGridView1.DataSource = bllThucDon.LayDanhSachThucDon();

            // Căn chỉnh DataGridView cho đẹp và chuyên nghiệp
            if (dataGridView1.Columns.Count > 0)
            {
                dataGridView1.Columns["Id"].HeaderText = "ID";
                dataGridView1.Columns["TenMon"].HeaderText = "Tên món";
                dataGridView1.Columns["DanhMucId"].HeaderText = "Mã danh mục";

                // Cột Giá tiền
                dataGridView1.Columns["Gia"].HeaderText = "Giá tiền";
                // Ép kiểu định dạng hiển thị tiền tệ (VD: 25000 -> 25,000)
                dataGridView1.Columns["Gia"].DefaultCellStyle.Format = "N0";

                // Tự động giãn các cột cho vừa khung hình
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                // Thu nhỏ cột ID lại cho gọn
                dataGridView1.Columns["Id"].FillWeight = 30;
                dataGridView1.Columns["DanhMucId"].FillWeight = 50;
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

        private void FrmThucDon_Load(object sender, EventArgs e)
        {
            button7.BackColor = Color.LightSkyBlue;
            button7.Font = new Font(button7.Font, FontStyle.Bold);

            cbDanhMuc.DataSource = bllDanhMuc.LayDanhSachDanhMuc();
            cbDanhMuc.DisplayMember = "TenDanhMuc";
            cbDanhMuc.ValueMember = "Id";

            txtId.Enabled = false;

            LoadDanhSach();
        }
        private void LoadDanhSach()
        {
            dataGridView1.DataSource = bllThucDon.LayDanhSachThucDon();

            if (dataGridView1.Columns.Count > 0)
            {
                dataGridView1.Columns["Id"].HeaderText = "ID";
                dataGridView1.Columns["TenMon"].HeaderText = "Tên Món";
                dataGridView1.Columns["DanhMucId"].HeaderText = "ID Danh Mục";
                dataGridView1.Columns["Gia"].HeaderText = "Giá Tiền";
                dataGridView1.Columns["Gia"].DefaultCellStyle.Format = "N0";
                if (dataGridView1.Columns["NgungBan"] != null)
                {
                    dataGridView1.Columns["NgungBan"].Visible = false;
                }

                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                txtId.Text = row.Cells["Id"].Value.ToString();
                txtTenMon.Text = row.Cells["TenMon"].Value.ToString();
                txtGia.Text = row.Cells["Gia"].Value.ToString();
                cbDanhMuc.SelectedValue = Convert.ToInt32(row.Cells["DanhMucId"].Value);
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            if (txtTenMon.Text == "" || txtGia.Text == "")
            {
                MessageBox.Show("Vui lòng nhập đủ thông tin!"); return;
            }

            int idDanhMuc = Convert.ToInt32(cbDanhMuc.SelectedValue);
            float gia = Convert.ToSingle(txtGia.Text);

            if (bllThucDon.ThemThucDon(txtTenMon.Text, idDanhMuc, gia))
            {
                MessageBox.Show("Thêm món mới thành công!", "Thông báo");
                LoadDanhSach();
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if (txtId.Text == "")
            {
                MessageBox.Show("Vui lòng chọn món dưới bảng để sửa!"); return;
            }
               if (MessageBox.Show("Bạn có chắc muốn sửa món này không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
               {
                   int id = Convert.ToInt32(txtId.Text);
                   int idDanhMuc = Convert.ToInt32(cbDanhMuc.SelectedValue);
                   float gia = Convert.ToSingle(txtGia.Text);

                   if (bllThucDon.SuaThucDon(id, txtTenMon.Text, idDanhMuc, gia))
                   {
                       MessageBox.Show("Cập nhật thành công!", "Thông báo");
                       LoadDanhSach();
                   }
               }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            if (txtId.Text == "")
            {
                MessageBox.Show("Vui lòng chọn món để xóa!"); return;
            }

            if (MessageBox.Show("Bạn có chắc muốn xóa (ngừng bán) món này không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                int id = Convert.ToInt32(txtId.Text);
                if (bllThucDon.XoaThucDon(id))
                {
                    MessageBox.Show("Đã gỡ món khỏi thực đơn!", "Thông báo");
                    LoadDanhSach();
                    txtId.Clear(); txtTenMon.Clear(); txtGia.Clear();
                }
            }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            string tenMonCanTim = txtTenMon.Text.Trim();
            int danhMucCanTim = 0;
            if (cbDanhMuc.SelectedValue != null)
            {
                danhMucCanTim = Convert.ToInt32(cbDanhMuc.SelectedValue);
            }
            float giaCanTim = -1;

            if (!string.IsNullOrEmpty(txtGia.Text.Trim()))
            {
                if (!float.TryParse(txtGia.Text.Trim(), out giaCanTim))
                {
                    MessageBox.Show("Giá tiền phải là số hợp lệ!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            dataGridView1.DataSource = bllThucDon.TimKiemThucDonDaNang(tenMonCanTim, danhMucCanTim, giaCanTim);

            if (dataGridView1.Rows.Count == 0)
            {
                MessageBox.Show("Không tìm thấy món nào phù hợp với yêu cầu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            LoadDanhSach();
        }
    }
}
