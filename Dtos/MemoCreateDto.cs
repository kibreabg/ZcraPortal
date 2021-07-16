using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZcraPortal.Dtos {
    [Table ("memos")]
    public partial class MemoCreateDto {
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
    }
}