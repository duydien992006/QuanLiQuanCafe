using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO_QuanLiQuanCafe
{
    public class DTO_ThucDon
    {
        public int Id { get; set; }
        public string TenMon { get; set; }
        public int DanhMucId { get; set; }
        public float Gia { get; set; }
        public bool NgungBan { get; set; }

        public DTO_ThucDon() { }
    }
}
