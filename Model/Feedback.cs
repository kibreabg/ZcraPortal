using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZcraPortal.Model
{
    [Table("feedback")]
    public partial class Feedback
    {
        [Key]
        [Column(TypeName = "int(11)")]
        public int Id { get; set; }
        [Column("name", TypeName = "varchar(255)")]
        public string Name { get; set; }
        [Column("surName", TypeName = "varchar(255)")]
        public string SurName { get; set; }
        [Column("contactNumber", TypeName = "int(11) unsigned")]
        public uint? ContactNumber { get; set; }
        [Column("emails", TypeName = "varchar(50)")]
        public string Emails { get; set; }
        [Column("enquiry", TypeName = "varchar(255)")]
        public string Enquiry { get; set; }
    }
}
