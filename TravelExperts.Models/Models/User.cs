using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace TravelExperts.Models.Models;

[Index("Username", Name = "UQ__Users__F3DBC572DEFFFC32", IsUnique = true)]
public partial class User
{
    [Key]
    [Column("user_id")]
    public int UserId { get; set; }

    [Column("username")]
    [StringLength(25)]
    [Unicode(false)]
    public string Username { get; set; } = null!;

    [Column("password")]
    [StringLength(30)]
    [Unicode(false)]
    public string Password { get; set; } = null!;

    [Column("admin")]
    public bool Admin { get; set; }
}
