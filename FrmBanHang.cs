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
    public partial class FrmBanHang : Form
    {
        public FrmBanHang()
        {
            InitializeComponent();
        }
        private BLL_BanCafe bllBan = new BLL_BanCafe();
        public DTO_TaiKhoan tkHienTai;
        private BLL_HoaDon bllHoaDon = new BLL_HoaDon();
        private int banHienTaiId = -1;
        private float tongTienBill = 0;
        private BLL_DanhMuc bllDanhMuc = new BLL_DanhMuc();
        public FrmBanHang(DTO_TaiKhoan tk)
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
            flpBan.Controls.Clear();
            DataTable dtBan = bllBan.LayDanhSachBan();
            foreach (DataRow row in dtBan.Rows)
            {
                Button btn = new Button();
                btn.Width = 80;
                btn.Height = 80;
                string tenBan = row["TenBan"].ToString();
                string trangThai = row["TrangThai"].ToString();
                btn.Text = tenBan + Environment.NewLine + trangThai;
                btn.Font = new Font("Arial", 10, FontStyle.Bold);
                if (trangThai == "Trống")
                {
                    btn.BackColor = Color.LightGreen;
                }
                else
                {
                    btn.BackColor = Color.LightPink;
                }
                btn.Tag = row["Id"];
                btn.Click += btnBan_Click;
                flpBan.Controls.Add(btn);
                
            }
        }
        private void btnBan_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            banHienTaiId = Convert.ToInt32(btn.Tag);
            HienThiHoaDon(banHienTaiId);
        }
        private void HienThiHoaDon(int banId)
        {
            listView1.Items.Clear();
            float tongTien = 0;
            DataTable dtBill = bllHoaDon.LayChiTietHoaDonTheoBan(banId);
            foreach (DataRow row in dtBill.Rows)
            {
                ListViewItem lsvItem = new ListViewItem(row["TenMon"].ToString());
                lsvItem.Tag = row["ThucDonId"];
                lsvItem.SubItems.Add(row["SoLuong"].ToString());
                float gia = Convert.ToSingle(row["Gia"]);
                float thanhTien = Convert.ToSingle(row["ThanhTien"]);
                lsvItem.SubItems.Add(gia.ToString("N0"));
                lsvItem.SubItems.Add(thanhTien.ToString("N0"));
                listView1.Items.Add(lsvItem);
                tongTien += thanhTien;
            }
            tongTienBill = tongTien;
            txtTongTien.Text = tongTien.ToString("N0");
        }
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FrmHoaDon frm = new FrmHoaDon(tkHienTai);
            this.Hide();
            frm.Show();
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

        private void FrmBanHang_Load(object sender, EventArgs e)
        {
            button1.BackColor = Color.LightBlue;
            button1.Font = new Font(button1.Font, FontStyle.Bold);
            CauHinhListView();
            LoadDanhSachBan();
            cbDanhMuc.DataSource = bllDanhMuc.LayDanhSachDanhMuc();
            cbDanhMuc.DisplayMember = "TenDanhMuc"; // Chữ hiện lên
            cbDanhMuc.ValueMember = "Id"; // Giá trị ngầm bên dưới
            cbChuyenBan.DataSource = new BLL_BanCafe().LayDanhSachBan();
            cbChuyenBan.DisplayMember = "TenBan";
            cbChuyenBan.ValueMember = "Id";
        }
        private void CauHinhListView()
        {
            listView1.View = View.Details;
            listView1.GridLines = true;
            listView1.FullRowSelect = true;

            listView1.Columns.Add("Tên món", 150);
            listView1.Columns.Add("Số lượng", 70);
            listView1.Columns.Add("Đơn giá", 90);
            listView1.Columns.Add("Thành tiền", 100);
        }

        private void cbDanhMuc_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbDanhMuc.SelectedValue != null)
            {
                int idDanhmuc = 0;
                if (int.TryParse(cbDanhMuc.SelectedValue.ToString(), out idDanhmuc))
                {
                    cbMon.DataSource = new BLL_ThucDon().LayThucDonTheoDanhMuc(idDanhmuc);
                    cbMon.DisplayMember = "TenMon";
                    cbMon.ValueMember = "Id";
                }
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (banHienTaiId == -1)
            {
                MessageBox.Show("Vui lòng chọn một bàn trước khi gọi món!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int idMon = Convert.ToInt32(cbMon.SelectedValue);
            int soLuong = Convert.ToInt32(nmrSoLuong.Value);

            if (soLuong == 0) return;
            bllHoaDon.ThemMon(banHienTaiId, idMon, soLuong);
            HienThiHoaDon(banHienTaiId);
            LoadDanhSachBan();
        }

        private void btnXoaMon_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                ListViewItem item = listView1.SelectedItems[0];
                int idMon = Convert.ToInt32(item.Tag);
                bllHoaDon.XoaMon(banHienTaiId, idMon);
                HienThiHoaDon(banHienTaiId);
                LoadDanhSachBan();
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một món trong danh sách để xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                ListViewItem item = listView1.SelectedItems[0];
                int idMon = Convert.ToInt32(item.Tag);
                int soLuong = Convert.ToInt32(item.SubItems[1].Text);
                BLL_ThucDon bllThucDon = new BLL_ThucDon();
                int idDanhMuc = bllThucDon.LayDanhMucIdTheoMon(idMon);
                cbDanhMuc.SelectedValue = idDanhMuc;
                cbMon.SelectedValue = idMon;
                nmrSoLuong.Value = soLuong;
            }
        }

        private void btnChuyenBan_Click(object sender, EventArgs e)
        {
            if (banHienTaiId == -1)
            {
                MessageBox.Show("Vui lòng chọn bàn muốn chuyển!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning); return;
            }

            int banMoiId = Convert.ToInt32(cbChuyenBan.SelectedValue);
            if (banHienTaiId == banMoiId)
            {
                MessageBox.Show("Bàn chuyển đến phải khác bàn hiện tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning); return;
            }

            DialogResult result = MessageBox.Show($"Bạn có thực sự muốn chuyển từ Bàn {banHienTaiId} sang Bàn {banMoiId} không?",
                                                  "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                bllHoaDon.ChuyenBan(banHienTaiId, banMoiId);
                banHienTaiId = banMoiId;
                LoadDanhSachBan();
                HienThiHoaDon(banHienTaiId);
            }
        }

        private void btnThanhToan_Click(object sender, EventArgs e)
        {
            if (banHienTaiId == -1) return;
            int hoaDonId = bllHoaDon.LayIdHoaDonChuaThanhToan(banHienTaiId);
            if (hoaDonId == -1)
            {
                MessageBox.Show("Bàn này chưa có hóa đơn để thanh toán!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int giamGia = Convert.ToInt32(nmrGiamGia.Value);
            float tienGiam = (tongTienBill * giamGia) / 100;
            float tienPhaiTra = tongTienBill - tienGiam;

            // =========================================================================
            // TẠO CHUỖI HIỂN THỊ CHI TIẾT
            // =========================================================================
            string chiTietThanhToan = $"Thanh toán Bàn {banHienTaiId}\n\n";
            chiTietThanhToan += $"Tổng tiền: {tongTienBill:N0} đ\n";

            // Nếu có giảm giá thì mới hiển thị dòng này cho đỡ rác giao diện
            if (giamGia > 0)
            {
                chiTietThanhToan += $"Giảm giá ({giamGia}%): -{tienGiam:N0} đ\n";
            }

            chiTietThanhToan += $"----------------------------------\n";
            chiTietThanhToan += $"CẦN THANH TOÁN: {tienPhaiTra:N0} đ";

            // =========================================================================
            // HỘP THOẠI ĐỘNG: CHỌN HÌNH THỨC THANH TOÁN
            // =========================================================================
            Form fChonHinhThuc = new Form()
            {
                Text = "Chọn hình thức thanh toán",
                Width = 360,
                Height = 240, // Đã tăng chiều cao form lên 240 để chứa đủ chữ
                FormBorderStyle = FormBorderStyle.FixedDialog,
                StartPosition = FormStartPosition.CenterScreen,
                MaximizeBox = false,
                MinimizeBox = false
            };

            Label lblGoiY = new Label()
            {
                Text = chiTietThanhToan,
                Left = 25,
                Top = 15,
                Width = 300,
                AutoSize = true,
                Font = new Font("Arial", 10, FontStyle.Bold)
            };

            // Đã đẩy Top của 2 nút xuống 140 để không bị đè lên chữ
            Button btnTienMat = new Button() { Text = "💵 Tiền mặt", Left = 30, Top = 140, Width = 130, Height = 40, DialogResult = DialogResult.Yes, Font = new Font("Arial", 9, FontStyle.Bold) };
            Button btnMaQR = new Button() { Text = "📱 Quét mã QR", Left = 185, Top = 140, Width = 130, Height = 40, DialogResult = DialogResult.No, Font = new Font("Arial", 9, FontStyle.Bold) };

            fChonHinhThuc.Controls.AddRange(new Control[] { lblGoiY, btnTienMat, btnMaQR });

            DialogResult luaChon = fChonHinhThuc.ShowDialog();

            if (luaChon == DialogResult.Cancel) return;

            if (luaChon == DialogResult.No) // NGƯỜI DÙNG CHỌN QUÉT MÃ QR
            {
                // 1. Cấu hình thông tin tài khoản ngân hàng nhận tiền
                string nganHang = "MB";
                string stk = "0866963500";
                string tenChuTk = "NGUYEN DUY DIEN";
                string noiDung = $"THANH TOAN BAN {banHienTaiId}";

                // 2. Tạo đường link API VietQR động
                string urlQR = $"https://img.vietqr.io/image/{nganHang}-{stk}-compact2.png?amount={tienPhaiTra}&addInfo={noiDung}&accountName={tenChuTk}";

                // 3. Tạo nhanh một Form popup để hiển thị ảnh mã QR
                Form fHienThiQR = new Form()
                {
                    Text = $"Mã QR Thanh Toán Bàn {banHienTaiId} - Số tiền: {tienPhaiTra:N0} đ",
                    Width = 400,
                    Height = 440,
                    FormBorderStyle = FormBorderStyle.FixedDialog,
                    StartPosition = FormStartPosition.CenterScreen,
                    MaximizeBox = false,
                    MinimizeBox = false
                };

                PictureBox picQR = new PictureBox() { Left = 20, Top = 10, Width = 345, Height = 345, SizeMode = PictureBoxSizeMode.Zoom };
                picQR.LoadAsync(urlQR);

                Label lblHuongDan = new Label() { Text = "Mời khách quét mã. Sau khi hoàn tất, bấm [X] để đóng cửa sổ.", Left = 20, Top = 365, Width = 350, ForeColor = Color.Red, Font = new Font("Arial", 9, FontStyle.Italic) };

                fHienThiQR.Controls.AddRange(new Control[] { picQR, lblHuongDan });

                // Hiện mã QR lên màn hình
                fHienThiQR.ShowDialog();
            }

            // THỰC HIỆN CẬP NHẬT DOANH THU VÀO CƠ SỞ DỮ LIỆU
            bllHoaDon.ThanhToan(hoaDonId, banHienTaiId, giamGia, tienPhaiTra);

            // Hỏi xuất/in hóa đơn ra file giấy
            DialogResult hoiIn = MessageBox.Show("Thanh toán thành công! Bạn có muốn in hóa đơn không?", "In Bill", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (hoiIn == DialogResult.Yes)
            {
                DataTable dtIn = bllHoaDon.LayDuLieuInHoaDon(hoaDonId);
                dtIn.Columns.Add("TongTienChu", typeof(string));

                if (dtIn.Rows.Count > 0)
                {
                    double tongTien = Convert.ToDouble(dtIn.Rows[0]["TongTien"]);
                    string chuoiTien = DocTienHelper.ChuyenSoThanhChu(tongTien);
                    foreach (DataRow row in dtIn.Rows)
                    {
                        row["TongTienChu"] = chuoiTien;
                    }
                }
                FrmInHoaDon frmIn = new FrmInHoaDon(dtIn);
                frmIn.ShowDialog();
            }

            nmrGiamGia.Value = 0;
            LoadDanhSachBan();
            HienThiHoaDon(banHienTaiId);
        }
    }
}
