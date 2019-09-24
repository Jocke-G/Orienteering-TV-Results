using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using OrienteeringTvResults.Model;
using System;

namespace OrienteeringTvResults.Rest
{
    [Route("api")]
    [EnableCors("MyPolicy")]
    [ApiController]
    public class CompetitionsController : Controller
    {
        private IResultsProvider _results;

        public CompetitionsController(ResultsAdapter adapter)
        {
            _results = adapter.Processor;
        }

        [HttpGet("competitions")]
        public JsonResult GetCompetitions()
        {
            var competitions = _results.GetCompetitions();
            return new JsonResult(competitions);
        }

        [HttpGet("competitions/{competitionId}")]
        public JsonResult Competition(int competitionId)
        {
            var competition = _results.GetCompetition(competitionId);
            return new JsonResult(competition);
        }

        [HttpGet("classes")]
        public JsonResult GetClasses()
        {
            var competitionClasses = _results.GetClasses();
            return new JsonResult(competitionClasses);
        }

        [HttpGet("classesSince/{since}")]
        public JsonResult GetClassesChangedSince(DateTime since)
        {
            var competitionClasses = _results.GetClassesChangedSince(since);
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

        [HttpGet("classhasnewresults/{competitionClassId}/{lastCheckTime}")]
        public JsonResult ClassHasNewResults(int competitionClassId, DateTime lastCheckTime)
        {
            var competitionClass = _results.ClassHasNewResults(competitionClassId, lastCheckTime);
            return new JsonResult(competitionClass);
        }
    }
}
