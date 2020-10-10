using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZcraPortal.Model
{
    [Table("faqs")]
    public partial class Faqs
    {
        [Key]
        [Column("id", TypeName = "int(11)")]
        public int Id { get; set; }
        [Column("title", TypeName = "varchar(255)")]
        public string Title { get; set; }
        [Column("file", TypeName = "varchar(255)")]
        public string File { get; set; }
        [Column("created_at", TypeName = "date")]
        public DateTime? CreatedAt { get; set; }
        [Column("updated_at", TypeName = "date")]
        public DateTime? UpdatedAt { get; set; }
    }
}
