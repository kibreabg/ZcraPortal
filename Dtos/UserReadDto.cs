using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ZcraPortal.Dtos
{
    [Table("users")]
    public partial class UserReadDto
    {
        [Key]
        [Column("id", TypeName = "int(10)")]
        public int Id { get; set; }
        [Required]
        [Column("username", TypeName = "varchar(191)")]
        public string Username { get; set; }
        [Required]
        [Column("email", TypeName = "varchar(191)")]
        public string Email { get; set; }
        [Required]
        [Column("password", TypeName = "varchar(191)")]
        public string Password { get; set; }
        [Column("remember_token", TypeName = "varchar(100)")]
        public string RememberToken { get; set; }
    }
}
