using DAL_QuanLiQuanCafe;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL_QuanLiQuanCafe
{
    public class BLL_ThongKe
    {
        private DAL_ThongKe dalThongKe = new DAL_ThongKe();

        public DataTable LayDanhSachNam()
        {
            return dalThongKe.LayDanhSachNam();
        }

        public DataTable DoanhThuTheoThang(int nam)
        {
            return dalThongKe.DoanhThuTheoThang(nam);
        }
        public DataTable ThongKeMonAn(int danhMucId)
        {
            return dalThongKe.ThongKeMonAn(danhMucId);
        }
    }
}
