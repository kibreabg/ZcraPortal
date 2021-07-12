using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZcraPortal.Dtos
{
    [Table("quickaccesstools")]
    public partial class QuickAccessToolCreateDto
    {

        [Column("title", TypeName = "varchar(255)")]
        public string Title { get; set; }
        [Column("content", TypeName = "varchar(255)")]
        public string Content { get; set; }
        [Column("icon", TypeName = "varchar(255)")]
        public string Icon { get; set; }
        [Column("created_at", TypeName = "date")]
        public DateTime? CreatedAt { get; set; }
    }
}