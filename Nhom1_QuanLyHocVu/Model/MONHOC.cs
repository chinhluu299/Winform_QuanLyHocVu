//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Nhom1_QuanLyHocVu.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class MONHOC
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public MONHOC()
        {
            this.CHUONGTRINHMONHOCs = new HashSet<CHUONGTRINHMONHOC>();
            this.DAMNHIEMMONs = new HashSet<DAMNHIEMMON>();
            this.KHOAHOCMONs = new HashSet<KHOAHOCMON>();
        }
    
        public string MaMonHoc { get; set; }
        public string TenMonHoc { get; set; }
        public Nullable<int> SoTinChi { get; set; }
        public string MaKhoa { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CHUONGTRINHMONHOC> CHUONGTRINHMONHOCs { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DAMNHIEMMON> DAMNHIEMMONs { get; set; }
        public virtual KHOA KHOA { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<KHOAHOCMON> KHOAHOCMONs { get; set; }
    }
}
