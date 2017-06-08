namespace CongVan.Areas.Admin.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class user
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public user()
        {
            employees = new HashSet<employee>();
        }

        public int id { get; set; }

        public int? group_id { get; set; }

        [StringLength(50)]
        public string username { get; set; }

        [StringLength(150)]
        public string password { get; set; }

        [StringLength(150)]
        public string email { get; set; }

        [StringLength(150)]
        public string first_name { get; set; }

        [StringLength(150)]
        public string last_name { get; set; }

        public byte? gender { get; set; }

        [StringLength(255)]
        public string photo { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? birthday { get; set; }

        public byte? active { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<employee> employees { get; set; }

        public virtual groups_user groups_user { get; set; }
    }
}
