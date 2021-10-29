using System;
using System.ComponentModel.DataAnnotations;

namespace PersonalSite.Domain.API.Dtos
{
    public class CreateJobExperienceDto
    {
        public CreateJobExperienceDto()
        {
        }

        public CreateJobExperienceDto(string company, string description, DateTime jobPeriodStart, DateTime? jobPeriodEnd, string[] techStack)
        {
            Company = company;
            Description = description;
            JobPeriodStart = jobPeriodStart;
            JobPeriodEnd = jobPeriodEnd;
            TechStack = techStack;
        }

        [Required]
        public string Company { get; set; }
        [Required]
        public string Description { get; set; }
        public DateTime JobPeriodStart { get; set; }
        public DateTime? JobPeriodEnd { get; set; }
        public string[] TechStack { get; set; }
    }
}