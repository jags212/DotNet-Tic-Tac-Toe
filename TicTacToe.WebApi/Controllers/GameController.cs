using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicTacToe.DAL.Interface;
using TicTacToe.Data.Model;

namespace TicTacToe.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GameController : ControllerBase
    {
        private readonly IGameRepository _gameRepositry;

        public GameController(IGameRepository gameRepositry)
        {
            _gameRepositry = gameRepositry;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Game>> AddGame([FromBody] Game game)
        {
            if (!ModelState.IsValid || game == null)
            {
                return BadRequest(new { errMsg = "Input Parameters are wrong" });
            }
            var newGame = await _gameRepositry.Add(game);
            return Created("GetGame", new { msg = "Success", transation = "Game Created" });
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<Game>>> GetGames()
        {
            try
            {
                return await _gameRepositry.GetAll();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error in reterving data");
            }
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<Game>> GetGameById(int id)
        {
            try
            {
                var game = await _gameRepositry.GetById(id);
                if (game == null)
                {
                    return BadRequest($"not found Id {id}");
                }
                return Ok(game);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error in reterving data");
            }
        }
    }
}