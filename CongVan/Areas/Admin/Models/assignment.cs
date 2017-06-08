namespace CongVan.Areas.Admin.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("assignment")]
    public partial class assignment
    {
        public int id { get; set; }

        public int? id_dispatch { get; set; }

        public int? id_department { get; set; }

        public DateTime? assingtime { get; set; }

        public DateTime? completiontime { get; set; }

        public int? userid_command { get; set; }

        public int? userid_follow { get; set; }

        public int? userid_perform { get; set; }

        [StringLength(500)]
        public string work_content { get; set; }

        [Column(TypeName = "text")]
        public string result { get; set; }

        [StringLength(255)]
        public string attach_file { get; set; }

        public virtual department department { get; set; }

        public virtual row row { get; set; }
    }
}
