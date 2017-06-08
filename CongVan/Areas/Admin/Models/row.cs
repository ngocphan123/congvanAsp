namespace CongVan.Areas.Admin.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class row
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public row()
        {
            assignments = new HashSet<assignment>();
            follows = new HashSet<follow>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }

        public int? type { get; set; }

        public int? idcat { get; set; }

        public int? idtype { get; set; }

        [StringLength(255)]
        public string title { get; set; }

        [Column("abstract", TypeName = "text")]
        public string _abstract { get; set; }

        [StringLength(255)]
        public string name_signer { get; set; }

        [StringLength(255)]
        public string name_initial { get; set; }

        public int? level_important { get; set; }

        [StringLength(255)]
        public string number_dispatch { get; set; }

        [StringLength(255)]
        public string number_text_come { get; set; }

        [Column(TypeName = "text")]
        public string note { get; set; }

        public DateTime? publtime { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? date_iss { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? date_first { get; set; }

        [Column(TypeName = "smalldatetime")]
        public DateTime? date_die { get; set; }

        [StringLength(255)]
        public string from_org { get; set; }

        [StringLength(255)]
        public string to_org { get; set; }

        public int? dep_catid { get; set; }

        public int? to_depid { get; set; }

        [StringLength(255)]
        public string attach_file { get; set; }

        public byte? status { get; set; }

        public DateTime? term_view { get; set; }

        public byte? reply { get; set; }

        [StringLength(50)]
        public string groups_view { get; set; }

        public int? views { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<assignment> assignments { get; set; }

        public virtual cat cat { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<follow> follows { get; set; }

        public virtual type type1 { get; set; }
    }
}
