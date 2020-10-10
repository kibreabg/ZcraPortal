using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZcraPortal.Model
{
    [Table("guidelines")]
    public partial class Guidelines
    {
        [Key]
        [Column("id", TypeName = "int(11)")]
        public int Id { get; set; }
        [Column("type", TypeName = "int(11)")]
        public int? Type { get; set; }
        [Column("title", TypeName = "varchar(255)")]
        public string Title { get; set; }
        [Column("content", TypeName = "varchar(255)")]
        public string Content { get; set; }
        [Column("order", TypeName = "int(11)")]
        public int? Order { get; set; }
        [Column("created_at", TypeName = "date")]
        public DateTime? CreatedAt { get; set; }
        [Column("updated_at", TypeName = "date")]
        public DateTime? UpdatedAt { get; set; }
    }
}
