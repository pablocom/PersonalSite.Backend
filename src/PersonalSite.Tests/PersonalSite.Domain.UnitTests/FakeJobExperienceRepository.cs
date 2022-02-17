﻿using System.Collections.Generic;
using System.Linq;
using PersonalSite.Application;
using PersonalSite.Domain.Model.JobExperienceAggregate;

namespace PersonalSite.UnitTests;

public class FakeJobExperienceRepository : IJobExperienceRepository
{
    private readonly FakeInMemoryPersonalSiteDbContext dbContext;

    public FakeJobExperienceRepository(FakeInMemoryPersonalSiteDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public void Add(JobExperience jobExperience)
    {
        dbContext.Add(jobExperience);
    }

    public IEnumerable<JobExperience> GetAllJobExperiences()
    {
        return dbContext.JobExperiences.ToArray();
    }
}
