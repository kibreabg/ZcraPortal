using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZcraPortal.Model
{
    [Table("conditions")]
    public partial class Conditions
    {
        [Key]
        [Column("id", TypeName = "int(11)")]
        public int Id { get; set; }
        [Column("name", TypeName = "varchar(100)")]
        public string Name { get; set; }
        [Column("adverse_events_id", TypeName = "int(11)")]
        public int? AdverseEventsId { get; set; }
    }
}
