﻿using System;
using System.Collections.Generic;
using System.Linq;
using PersonalSite.Domain.Entities;
using PersonalSite.Domain.ValueObjects;
using PersonalSite.Persistence;

namespace PersonalSite.Services
{
    public interface IJobExperienceService
    {
        void CreateJobExperience(string company, string jobExperience, DateTime jobPeriodStart, DateTime? jobPeriodEnd, string[] techStack);
        IEnumerable<JobExperience> GetAll();
    }

    public class JobExperienceService : IJobExperienceService
    {
        private readonly IPersonalSiteRepository repository;

        public JobExperienceService(IPersonalSiteRepository repository)
        {
            this.repository = repository;
        }

        public void CreateJobExperience(string company, string jobExperience, DateTime jobPeriodStart, DateTime? jobPeriodEnd, string[] techStack)
        {
            repository.Add(new JobExperience(company, jobExperience, new JobPeriod(jobPeriodStart, jobPeriodEnd), techStack));
        }

        public IEnumerable<JobExperience> GetAll()
        {
            return repository.GetAll<JobExperience>().ToArray();
        }
    }
}