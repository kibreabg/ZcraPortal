using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZcraPortal.Dtos {
    [Table ("guidelines")]
    public partial class GuidelineReadDto {
        [Key]
        [Column ("id", TypeName = "int(11)")]
        public int Id { get; set; }

        [Column ("type", TypeName = "int(11)")]
        public int? Type { get; set; }

        [Column ("title", TypeName = "varchar(255)")]
        public string Title { get; set; }

        [Column ("content", TypeName = "varchar(255)")]
        public string Content { get; set; }

        [Column ("order", TypeName = "int(11)")]
        public int? Order { get; set; }
    }
}