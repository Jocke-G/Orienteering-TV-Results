using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using OrienteeringTvResults.Model;
using System;

namespace OrienteeringTvResults.Rest
{
    [EnableCors("defaultCorsPolicy")]
    [ApiController]
    public class CompetitionsController
        : Controller
    {
        private IResultsProvider _results;

        public CompetitionsController(ResultsAdapter adapter)
        {
            _results = adapter.Processor;
        }

        [HttpGet("classes")]
        public JsonResult GetClasses()
        {
            var competitionClasses = _results.GetClasses();
            return new JsonResult(competitionClasses);
        }

        [HttpGet("classes/{shortName}")]
        public JsonResult GetClasses(string shortName)
        {
            var competitionClass = _results.GetClass(shortName);
            return new JsonResult(competitionClass);
        }

        [HttpGet("classById/{competitionClassId}")]
        public JsonResult GetClasses(int competitionClassId)
        {
            var competitionClass = _results.GetClass(competitionClassId);
            return new JsonResult(competitionClass);
        }

        [HttpGet("classHasNewResults/{competitionClassId}/{lastCheckTime}")]
        public JsonResult ClassHasNewResults(int competitionClassId, DateTime lastCheckTime)
        {
            var competitionClass = _results.ClassHasNewResults(competitionClassId, lastCheckTime);
            return new JsonResult(competitionClass);
        }

        [HttpGet("finish/{limit}")]
        public JsonResult GetFinish(int limit)
        {
            var results = _results.Finish(limit);
            return new JsonResult(results);
        }
    }
}
