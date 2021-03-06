using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZcraPortal.Dtos
{
    [Table("prescriptiontools")]
    public partial class PrescriptiontoolUpdateDto
    {
        [Column("parent_id", TypeName = "int(11)")]
        public int? ParentId { get; set; }
        [Column("description", TypeName = "varchar(255)")]
        public string Description { get; set; }
        [Column("content", TypeName = "varchar(255)")]
        public string Content { get; set; }
        [Column("updated_at", TypeName = "date")]
        public DateTime? UpdatedAt { get; set; }
    }
}