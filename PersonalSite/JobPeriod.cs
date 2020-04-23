using System;

namespace PersonalSite.Domain
{
    public class JobPeriod : ValueObject<JobPeriod>
    {
        public DateTime Start { get; private set; }
        public DateTime? End { get; private set; }

        protected override bool EqualsCore(JobPeriod other)
        {
            throw new NotImplementedException();
        }

        protected override int GetHashCodeCore()
        {
            throw new NotImplementedException();
        }
    }
}