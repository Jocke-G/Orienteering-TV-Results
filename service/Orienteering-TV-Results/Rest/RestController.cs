using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using OrienteeringTvResults.OlaAdapter;

namespace OrienteeringTvResults.Rest
{
    [Route("api")]
    [EnableCors("MyPolicy")]
    [ApiController]
    public class CompetitionsController : Controller
    {
        private ResultsProcessor _results;

        public CompetitionsController(ResultsAdapter adapter)
        {
            _results = adapter.Processor;
        }

        [HttpGet("classes")]
        public JsonResult GetClasses()
        {
            var classes = _results.GetClasses();
            return new JsonResult(classes);
        }

        [HttpGet("classes/{shortName}")]
        public JsonResult GetClasses(string shortName)
        {
            var competitionClass = _results.GetClass(shortName);
            return new JsonResult(competitionClass);
        }

        [HttpGet("splittimecontrols/{competitionId}/{stageId}")]
        public JsonResult GetSplitTimeControls(int competitionId, int stageId)
        {
            var splitTimeControls = _results.GetSplitTimeControls(competitionId, stageId);
            var result = new JsonResult(splitTimeControls);
            return result;
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

        [HttpGet("competitions/{competitionId}/{stageId}")]
        public JsonResult Stage(int competitionId, int stageId)
        {
            var stage = _results.GetStage(competitionId, stageId);
            return new JsonResult(stage);
        }

        [HttpGet("competitions/{competitionId}/{stageId}/{classId}")]
        public IActionResult ClassResults(int competitionId, int stageId, int classId)
        {
            var competitionClass = _results.GetClass(competitionId, stageId, classId);
            return new JsonResult(competitionClass);
        }
    }
}
