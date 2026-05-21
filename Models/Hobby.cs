using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AME.Models
{
    public class Hobby
    {
        [Key]
        public int HobbyID { get; set; }

        [Required]
        public string HobbyName { get; set; }

    }
}
