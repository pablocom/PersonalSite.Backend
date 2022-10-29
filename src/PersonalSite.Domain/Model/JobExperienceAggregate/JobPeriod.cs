using System;
using System.Collections.Generic;

namespace PersonalSite.Domain.Model.JobExperienceAggregate;

public sealed class JobPeriod : ValueObject
{
    public DateOnly Start { get; }
    public DateOnly? End { get; }

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
