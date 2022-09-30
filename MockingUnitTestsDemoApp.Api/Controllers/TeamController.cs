using Microsoft.AspNetCore.Mvc;
using MockingUnitTestsDemoApp.Impl.Models;
using MockingUnitTestsDemoApp.Impl.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace MockingUnitTestsDemoApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private readonly ITeamService _teamService;

        public TeamController(ITeamService teamService)
        {
            _teamService = teamService;
        }
                
        [HttpPost]
        public IActionResult Search(TeamSearch search)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            var results = new List<Team>
            {
                new Team{ID = 1, Name = "Team1", LeagueID = 1},
                new Team{ID = 2, Name = "Team2", LeagueID = 1},
            };

            
            search.Results = results;
            return Ok(search);            
        }
    }
}