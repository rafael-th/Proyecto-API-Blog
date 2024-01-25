﻿using System.ComponentModel.DataAnnotations;

namespace ApiBlog.Models
{
    public class User
    {
        [Key] public int Id { get; set; }
        [Required] public string UserName { get; set; }
        [Required] public string Name { get; set; }
        [Required] public string Email { get; set; }
        [Required] public string Password { get; set; }
    }
}
