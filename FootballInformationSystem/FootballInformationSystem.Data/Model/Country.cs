﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FootballInformationSystem.Data.Model
{
    public class Country : SystemInfo
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public string Name { get; set; }

        public virtual HashSet<Team> Teams { get; set; }
    }
}
