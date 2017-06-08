namespace CongVan.Areas.Admin.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class follow
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int id { get; set; }

        public int? id_dispatch { get; set; }

        public int? id_department { get; set; }

        [StringLength(255)]
        public string list_userid { get; set; }

        [Column(TypeName = "text")]
        public string list_timeview { get; set; }

        [StringLength(255)]
        public string list_hitstotal { get; set; }

        public virtual department department { get; set; }

        public virtual row row { get; set; }
    }
}
