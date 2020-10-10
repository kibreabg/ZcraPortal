using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZcraPortal.Model
{
    [Table("diagnoses")]
    public partial class Diagnoses
    {
        [Key]
        [Column("id", TypeName = "int(11)")]
        public int Id { get; set; }
        [Column("conditions_id", TypeName = "int(11)")]
        public int? ConditionsId { get; set; }
        [Column("grade", TypeName = "int(11)")]
        public int? Grade { get; set; }
        [Column("antitb", TypeName = "text")]
        public string Antitb { get; set; }
        [Column("arvsplhivmed", TypeName = "text")]
        public string Arvsplhivmed { get; set; }
        [Column("normaltestresult", TypeName = "text")]
        public string Normaltestresult { get; set; }
        [Column("abnormaltestresult", TypeName = "text")]
        public string Abnormaltestresult { get; set; }
        [Column("severityscale", TypeName = "text")]
        public string Severityscale { get; set; }
        [Column("requiredaction", TypeName = "text")]
        public string Requiredaction { get; set; }
        [Column("requiredancillarymed", TypeName = "text")]
        public string Requiredancillarymed { get; set; }
        [Column("commonsideeffects", TypeName = "text")]
        public string Commonsideeffects { get; set; }
        [Column("contraindications", TypeName = "text")]
        public string Contraindications { get; set; }
        [Column("comments", TypeName = "text")]
        public string Comments { get; set; }
        [Column("monitoringfrequency", TypeName = "text")]
        public string Monitoringfrequency { get; set; }
    }
}
