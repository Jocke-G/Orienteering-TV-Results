using OrienteeringTvResults.Model;

namespace OrienteeringTvResults.Common.Translators
{
    public static class OlaRunnerStatusTranslator
    {
        public static ResultStatus ToResultStatus(string status)
        {
            switch (status)
            {
                case "passed":
                    return ResultStatus.Passed;
                case "finished":
                    return ResultStatus.Finished;
                case "notValid":
                case "disqualified":
                    return ResultStatus.NotPassed;
                case "notStarted":
                    return ResultStatus.NotStarted;
                default:
                    /*
                     *finishedTimeOk
                     * finishedPunchOk
                     * movedUp
                     * walkOver
                     * started
                     * notActivated
                     * notParticipating
                     */
                    return ResultStatus.NotFinishedYet;
            }
        }
    }
}
