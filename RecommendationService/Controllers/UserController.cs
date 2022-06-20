using Microsoft.AspNetCore.Mvc;
using Neo4jClient;
using RecommendationService.Entities;

namespace RecommendationService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IGraphClient _client;

        public UserController(IGraphClient client)
        {
            _client = client;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserProfile user) {
            await _client.Cypher.Create("(u: UserProfile $user)")
                .WithParam("user", user)
                .ExecuteWithoutResultsAsync();
            return Ok("User Saved Successfully");
        }

        [HttpGet("{uid}/assignRelationship/{cid}")]
        public async Task<IActionResult> AssignRelation(int uid, int cid) {
            await _client.Cypher.Match("(u:UserProfile), (c:Country)")
                .Where((UserProfile u, Country c) => u.Id == uid && c.Id == cid)
                .Create("(u)-[r:from]->(c)")
                .ExecuteWithoutResultsAsync();

            return Ok("Relation created");
        }
    }
}
