using System;

namespace FootballInformationSystem.Data.Model
{
    public interface IAuditable
    {
        DateTime? CreatedOn { get; set; }

        DateTime? ModifiedOn { get; set; }
    }
}
