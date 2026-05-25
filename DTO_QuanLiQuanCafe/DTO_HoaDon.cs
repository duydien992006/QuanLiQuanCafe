using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO_QuanLiQuanCafe
{
    public class DTO_HoaDon
    {
        public int Id { get; set; }
        public int BanId { get; set; }
        public DateTime ThoiGianVao { get; set; }
        public DateTime? ThoiGianRa { get; set; } // Dùng DateTime? vì lúc khách đang uống thì chưa có giờ ra (NULL)
        public int TrangThai { get; set; }
        public int GiamGia { get; set; }
        public float TongTien { get; set; }

        public DTO_HoaDon() { }
    }
}
