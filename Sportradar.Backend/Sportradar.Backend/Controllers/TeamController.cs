using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Sportradar.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAllTeams()
        {
            return Ok("This is the TeamController. Implement team-related endpoints here.");
        }

        [HttpGet("{id}")]
        public IActionResult GetTeamById(Guid id)
        {
            return Ok($"This is the TeamController. Implement logic to retrieve team with ID: {id}");
        }

        [HttpGet("{id}/players")]
        public IActionResult GetPlayersByTeamId(Guid id)
        {
            return Ok($"This is the TeamController. Implement logic to retrieve players for team with ID: {id}");
        }
    }
}
