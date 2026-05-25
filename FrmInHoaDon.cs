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
    public partial class FrmInHoaDon : Form
    {
        public FrmInHoaDon(DataTable dtIn)
        {
            InitializeComponent();

            // Khởi tạo bản vẽ báo cáo bạn làm ở Bước 4
            rptHoaDon rpt = new rptHoaDon();
            // Đổ dữ liệu thật vào bản vẽ
            rpt.SetDataSource(dtIn);
            // Gắn lên Form
            crvHoaDon.ReportSource = rpt;
        }
    }
}
