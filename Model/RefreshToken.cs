using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZcraPortal.Model
{
    [Table("RefreshToken")]
    public partial class RefreshToken
    {
        [Key]
        [Column("id", TypeName = "int(10)")]
        public int Id { get; set; }
        [Column("user_id", TypeName = "int(10)")]
        public int UserId { get; set; }
        [Column("token", TypeName = "varchar(200)")]
        public string Token { get; set; }
        [Column("expiry_date", TypeName = "timestamp")]
        public DateTime ExpiryDate { get; set; }

        public virtual Users User { get; set; }
    }
}