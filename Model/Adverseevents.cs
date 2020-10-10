using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZcraPortal.Model
{
    [Table("adverseevents")]
    public partial class Adverseevents
    {
        [Key]
        [Column("id", TypeName = "int(11)")]
        public int Id { get; set; }
        [Column("name", TypeName = "varchar(50)")]
        public string Name { get; set; }
    }
}
