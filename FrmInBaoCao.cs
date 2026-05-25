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
    public partial class FrmInBaoCao : Form
    {
        public FrmInBaoCao(DataTable dtBaoCao, string nam)
        {
            InitializeComponent();

            // Khởi tạo bản thiết kế A4
            rptBaoCaoDoanhThu rpt = new rptBaoCaoDoanhThu();

            // Đổ dữ liệu các tháng vào
            rpt.SetDataSource(dtBaoCao);

            // Truyền con số "2026" vào cái Parameter pNam trên tiêu đề
            rpt.SetParameterValue("pNam", nam);

            crvBaoCao.ReportSource = rpt;
        }

        private void FrmInBaoCao_Load(object sender, EventArgs e)
        {

        }
    }
}
