using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Tiba.Domain.Data;
using Tiba.Web.Dtos;


namespace Tiba.Web.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RepositoryController : ControllerBase
    {
        private readonly IRepositoryRepo repo;
        private readonly ILogger logger;

        public RepositoryController(IRepositoryRepo repo, ILoggerFactory loggerFactory)
        {
            this.repo = repo;
            logger = loggerFactory.CreateLogger("RepositoryController");
        }

        [HttpGet]
        public async Task<IActionResult> GetRepositories()
        {
            try
            {
               
                var repositories = await repo.GetRepositoriesAsync();
                return Ok(repositories);
            }
            catch (Exception exp)
            {
                logger.LogError(exp.Message);
                return StatusCode(500);
            }
        }

        [HttpPost("Search")]
        public async Task<IActionResult> Search([FromBody] SearchRepoDto search)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var name = HttpContext.User.Identity.Name;
                var repositories = await repo.SearchRepositoryAsync(search.Term, name);
                return Ok(repositories);
            }
            catch (Exception exp)
            {
                logger.LogError(exp.Message);
                return StatusCode(500);
            }
        }

    }
}
