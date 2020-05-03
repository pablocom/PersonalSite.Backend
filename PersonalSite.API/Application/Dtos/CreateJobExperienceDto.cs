using System;
using System.Collections.Generic;

namespace PersonalSite.API.Application.Dtos
{
    public class CreateJobExperienceDto
    {
        public string Company { get; set; }
        public string Description { get; set; }
        public DateTime JobPeriodStart { get; set; }
        public DateTime? JobPeriodEnd { get; set; }
        public ICollection<string> TechStack { get; set; }
    }
}