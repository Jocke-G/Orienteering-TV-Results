using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using OrienteeringTvResults.OlaAdapter;
using Orienteering_TV_Results.Models;

namespace Orienteering_TV_Results.Controllers
{
    [Route("Home")]
    public class HomeController : Controller
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(HomeController));


        public ResultsProcessor ResultsProcessor { get; }

        public HomeController()
        {
            ResultsProcessor = new ResultsProcessor();
        }

        [HttpGet("/", Name = "Index")]
        public IActionResult Competitions()
        {
            return Redirect("/competition/2/2/58");
            //log.Info("______ Begin Home");
            //var model = ResultsProcessor.GetCompetitions();
            //log.Info("______ End Home");
            //return View(model);
        }

        [HttpGet("/competition/{id}", Name = "Competition")]
        public IActionResult Competition(int id)
        {
            log.Info("______ Begin Competition");

            var model = ResultsProcessor.GetCompetition(id);
            return View(model);
        }

        [HttpGet("/competition/{id}/{stageId}", Name = "Stage")]
        public IActionResult Stage(int id, int stageId)
        {
            var model = ResultsProcessor.GetStage(id, stageId);
            return View(model);
        }

        [HttpGet("/competition/{id}/{stageId}/{classId}", Name = "Class")]
        public IActionResult ClassResults(int id, int stageId, int classId)
        {
            var model = ResultsProcessor.GetClass(id, stageId, classId);
            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
