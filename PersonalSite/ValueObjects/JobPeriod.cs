using System;
using System.Collections.Generic;

namespace PersonalSite.Domain.ValueObjects
{
    public class JobPeriod : ValueObject
    {
        public DateTime Start { get; private set; }
        public DateTime? End { get; private set; }

        protected JobPeriod() { }

        public JobPeriod(DateTime start, DateTime? end)
        {
            Start = start;
            End = end;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Start;
            yield return End;
        }
    }
}