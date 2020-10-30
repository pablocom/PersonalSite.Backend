using System;
using System.ComponentModel.DataAnnotations;

namespace PersonalSite.Domain.API.Application.Dtos
{
    public class CreateJobExperienceDto
    {
        [Required]
        public string Company { get; set; }
        [Required]
        public string Description { get; set; }
        public DateTime JobPeriodStart { get; set; }
        public DateTime? JobPeriodEnd { get; set; }
        public string[] TechStack { get; set; }
    }
}