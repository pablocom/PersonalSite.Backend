using System;
using System.Collections.Generic;

namespace PersonalSite.Domain.Model.JobExperienceAggregate;

public class JobPeriod : ValueObject
{
    public DateTime Start { get; }
    public DateTime? End { get; }

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
