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

        [HttpGet("competitions")]
        public JsonResult GetCompetitions()
        {
            var competitions = _results.GetCompetitions();
            return new JsonResult(competitions);
        }

        [HttpGet("competitions/{id}")]
        public JsonResult Competition(int id)
        {
            Logger.LogInfo("REST: ");
            var competition = _results.GetCompetition(id);
            return new JsonResult(competition);
        }

        [HttpGet("competitions/{id}/{stageId}")]
        public JsonResult Stage(int id, int stageId)
        {
            var stage = _results.GetStage(id, stageId);
            return new JsonResult(stage);
        }

        [HttpGet("competitions/{id}/{stageId}/{classId}")]
        public IActionResult ClassResults(int id, int stageId, int classId)
        {
            var @class = _results.GetClass(id, stageId, classId);
            return new JsonResult(@class);
        }
    }
}
