﻿using System;

namespace FootballInformationSystem.Data.Model
{
    public abstract class SystemInfo : IDeletable, IAuditable
    {
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public DateTime? CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}
