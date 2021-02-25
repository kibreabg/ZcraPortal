using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZcraPortal.Dtos {
    [Table ("guidelinetypes")]
    public partial class GuidelineTypeCreateDto {

        [Column ("name", TypeName = "varchar(100)")]
        public string Name { get; set; }

        [Column ("folder", TypeName = "varchar(255)")]
        public string Folder { get; set; }

        [Column ("icon", TypeName = "varchar(255)")]
        public string Icon { get; set; }

        [Column ("created_at", TypeName = "date")]
        public DateTime? CreatedAt { get; set; }
    }
}