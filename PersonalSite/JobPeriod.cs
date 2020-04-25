using System;
using System.Collections.Generic;

namespace PersonalSite.Domain
{
    public class JobPeriod : ValueObject
    {
        public DateTime Start { get; private set; }
        public DateTime? End { get; private set; }


        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Start;
            yield return End;
        }
    }
}