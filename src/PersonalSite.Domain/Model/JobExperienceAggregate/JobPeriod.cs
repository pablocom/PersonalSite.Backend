using System;
using System.Collections.Generic;

namespace PersonalSite.Domain.Model.JobExperienceAggregate;

public class JobPeriod : ValueObject
{
    public DateOnly Start { get; }
    public DateOnly? End { get; }

    protected JobPeriod() { }

    public JobPeriod(DateOnly start, DateOnly? end)
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
