using System;

namespace OrienteeringTvResults.Common.Translators
{
    public static class OlaTimeSpanTranslator
    {
        public static TimeSpan? ToTimeSpan(int time)
        {
            if (time == 0)
                return null;

            return TimeSpan.FromSeconds(time / 100);
        }
    }
}
