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
using System.Windows.Forms.DataVisualization.Charting;

namespace QuanLiQuanCafe
{
    public partial class FrmThongKe : Form
    {
        public FrmThongKe()
        {
            InitializeComponent();
        }
        public DTO_TaiKhoan tkHienTai;
        public FrmThongKe(DTO_TaiKhoan tk)
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
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbDanhMucMon.SelectedValue != null)
            {
                int danhMucId;
                if (int.TryParse(cbDanhMucMon.SelectedValue.ToString(), out danhMucId))
                {
                    // Gọi hàm xử lý Bảng và Biểu đồ
                    VeBieuDoMonAn(danhMucId);
                }
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
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn thoát không?", "Xác nhận thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }
        private BLL_ThongKe bllThongKe = new BLL_ThongKe();
        private BLL_DanhMuc bllDanhMuc = new BLL_DanhMuc();
        private void LoadTabMonAn()
        {
            DataTable dtDanhMuc = bllDanhMuc.LayDanhSachDanhMuc();
            DataRow row = dtDanhMuc.NewRow();
            row["Id"] = 0;
            row["TenDanhMuc"] = "Tất cả";
            dtDanhMuc.Rows.InsertAt(row, 0);

            cbDanhMucMon.DataSource = dtDanhMuc;
            cbDanhMucMon.DisplayMember = "TenDanhMuc";
            cbDanhMucMon.ValueMember = "Id";
        }
        private void FrmThongKe_Load(object sender, EventArgs e)
        {
            button8.BackColor = Color.LightSkyBlue;
            button8.Font = new Font(button8.Font, FontStyle.Bold);

            LoadTabMonAn();
            DataTable dtNam = bllThongKe.LayDanhSachNam();
            if (dtNam.Rows.Count > 0)
            {
                cbNam.DataSource = dtNam;
                cbNam.DisplayMember = "Nam";
                cbNam.ValueMember = "Nam";
            }
            else
            {
                MessageBox.Show("Quán chưa có doanh thu nào để thống kê!", "Thông báo");
            }
        }

        private void cbNam_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbNam.SelectedValue != null)
            {
                int namDuocChon;
                if (int.TryParse(cbNam.SelectedValue.ToString(), out namDuocChon))
                {
                    VeBieuDoDoanhThu(namDuocChon);
                }
            }
        }
        private void VeBieuDoDoanhThu(int nam)
        {
            // 1. Lấy dữ liệu 4 cột từ SQL
            DataTable dtDoanhThu = bllThongKe.DoanhThuTheoThang(nam);

            // 2. ĐỔ DỮ LIỆU VÀO DATAGRIDVIEW
            dgvDoanhThu.DataSource = dtDoanhThu;

            if (dgvDoanhThu.Columns.Count > 0)
            {
                // Đổi tên 4 cột cho thuần Việt
                dgvDoanhThu.Columns["Thang"].HeaderText = "Tháng";
                dgvDoanhThu.Columns["SanPham"].HeaderText = "Sản phẩm";
                dgvDoanhThu.Columns["SoLuongBan"].HeaderText = "SL Bán";
                dgvDoanhThu.Columns["DoanhThu"].HeaderText = "Doanh thu tháng";

                // Căn lề và định dạng tiền tệ cho cột Doanh thu
                dgvDoanhThu.Columns["DoanhThu"].DefaultCellStyle.Format = "N0";
                dgvDoanhThu.Columns["SoLuongBan"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

                dgvDoanhThu.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }

            // 3. VẼ BIỂU ĐỒ DOANH THU (CHART)
            chartDoanhThu.Series.Clear();
            chartDoanhThu.ChartAreas[0].AxisX.Title = "Tháng";
            chartDoanhThu.ChartAreas[0].AxisY.Title = "Doanh thu (VNĐ)";

            chartDoanhThu.ChartAreas[0].AxisY.Minimum = double.NaN;
            chartDoanhThu.ChartAreas[0].AxisY.Maximum = double.NaN;
            chartDoanhThu.ChartAreas[0].RecalculateAxesScale();
            // ------------------------------------------------

            Series series = new Series("Tổng thu");
            series.ChartType = SeriesChartType.Column;
            series.Color = Color.DodgerBlue;
            series.IsValueShownAsLabel = true;
            series.LabelFormat = "N0";

            chartDoanhThu.ChartAreas[0].AxisY.IsMarginVisible = true;

            for (int i = 1; i <= 12; i++)
            {
                double doanhThuThang = 0;
                foreach (DataRow row in dtDoanhThu.Rows)
                {
                    if (Convert.ToInt32(row["Thang"]) == i)
                    {
                        doanhThuThang = Convert.ToDouble(row["DoanhThu"]);
                        break;
                    }
                }

                series.Points.AddXY(i.ToString(), doanhThuThang);
            }

            chartDoanhThu.Series.Add(series);
        }
        private void VeBieuDoMonAn(int danhMucId)
        {
            DataTable dtMonAn = bllThongKe.ThongKeMonAn(danhMucId);

            dgvThongKeMon.DataSource = dtMonAn;
            if (dgvThongKeMon.Columns.Count > 0)
            {
                dgvThongKeMon.Columns["Id"].HeaderText = "Mã món";
                dgvThongKeMon.Columns["TenMon"].HeaderText = "Tên món ăn";
                dgvThongKeMon.Columns["SoLanGoi"].HeaderText = "Số lần gọi";
                dgvThongKeMon.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }

            chartMonAn.Series.Clear();
            Series series = new Series("TyLeMonAn");
            series.ChartType = SeriesChartType.Pie;
            series.IsValueShownAsLabel = true;
            foreach (DataRow row in dtMonAn.Rows)
            {
                string tenMon = row["TenMon"].ToString();
                int soLanGoi = Convert.ToInt32(row["SoLanGoi"]);
                series.Points.AddXY(tenMon, soLanGoi);
            }
            chartMonAn.Series.Add(series);
        }

        private void btnInBaoCao_Click(object sender, EventArgs e)
        {
            if (cbNam.SelectedValue == null) return;
            int namDuocChon = Convert.ToInt32(cbNam.SelectedValue);

            DataTable dtDuLieu = bllThongKe.DoanhThuTheoThang(namDuocChon);

            if (dtDuLieu.Rows.Count == 0)
            {
                MessageBox.Show("Năm này không có doanh thu để in báo cáo!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            FrmInBaoCao frmIn = new FrmInBaoCao(dtDuLieu, namDuocChon.ToString());
            frmIn.ShowDialog();
        }
    }
}
