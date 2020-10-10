using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZcraPortal.Model
{
    [Table("symptomadverseevents")]
    public partial class Symptomadverseevents
    {
        [Key]
        [Column("id", TypeName = "int(11)")]
        public int Id { get; set; }
        [Column("symptom_id", TypeName = "int(11)")]
        public int? SymptomId { get; set; }
        [Column("adverseevent_id", TypeName = "int(11)")]
        public int? AdverseeventId { get; set; }
    }
}
