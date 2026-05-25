using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO_QuanLiQuanCafe
{
    public class DTO_ChiTietHoaDon
    {
        public int Id { get; set; }
        public int HoaDonId { get; set; }
        public int ThucDonId { get; set; }
        public int SoLuong { get; set; }

        public DTO_ChiTietHoaDon() { }
    }
}
