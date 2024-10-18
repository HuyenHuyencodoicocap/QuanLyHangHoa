namespace ThuHanh4.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tblHang
    {
        [Key]
        [StringLength(50)]
        public string MaHang { get; set; }

        [Required]
        [StringLength(50)]
        public string TenHang { get; set; }

        [StringLength(50)]
        public string ChatLieu { get; set; }

        public int SoLuong { get; set; }

        public double DonGiaNhap { get; set; }

        public double DonGiaBan { get; set; }

        public string Anh { get; set; }

    }
}
