using System.ComponentModel.DataAnnotations;

namespace PersonalSite.WebApi.Dtos;

public class CreateJobExperienceDto
{
    public CreateJobExperienceDto()
    {
    }

    public CreateJobExperienceDto(string company, string description, DateOnly jobPeriodStart, DateOnly? jobPeriodEnd, string[] techStack)
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
    public DateOnly JobPeriodStart { get; set; }
    public DateOnly? JobPeriodEnd { get; set; }
    public string[] TechStack { get; set; }
}
