using Microsoft.AspNetCore.Mvc;
using MockingUnitTestsDemoApp.Impl.Models;
using MockingUnitTestsDemoApp.Impl.Services.Interfaces;
using System;
using System.Collections.Generic;

namespace MockingUnitTestsDemoApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private readonly ILeagueService _leagueService;
        private readonly IPlayerService _playerService;

        public PlayerController(ILeagueService leagueService,
                                IPlayerService playerService)
        {
            _leagueService = leagueService;
            _playerService = playerService;
        }

        [HttpGet("{id}")]
        public ActionResult<Player> Get(int id)
        {
            var player = id switch
            {
                1 => new Player { ID = 1, FirstName = "Player1" },
                2 => new Player { ID = 1, FirstName = "Player2" },
                _ => default
            };

            if (player is not null)
                return Ok(player);

            return NotFound();
        }

        public ActionResult<List<Player>> GetFromLeague(int id)
        {
            if (id == 1)
            {
                return Ok(new List<Player>
                {
                    new Player { ID = 1, FirstName = "Player1" },
                    new Player { ID = 2, FirstName = "Player2" },
                });
            }

            return NotFound();
        }
    }
}