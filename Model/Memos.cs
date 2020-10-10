using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZcraPortal.Model
{
    [Table("memos")]
    public partial class Memos
    {
        [Key]
        [Column("id", TypeName = "int(11)")]
        public int Id { get; set; }
        [Column("title", TypeName = "varchar(255)")]
        public string Title { get; set; }
        [Column("url", TypeName = "varchar(255)")]
        public string Url { get; set; }
        [Required]
        [Column("latest", TypeName = "varchar(50)")]
        public string Latest { get; set; }
        [Column("created_at", TypeName = "date")]
        public DateTime? CreatedAt { get; set; }
        [Column("updated_at", TypeName = "date")]
        public DateTime? UpdatedAt { get; set; }
    }
}
