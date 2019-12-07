using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RTL.TVMaze.BLL.Models;
using RTL.TVMaze.BLL.Services;

namespace RTL.TVMaze.Api.Controllers
{
    /// <summary>
    /// Handles requests for shows
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class ShowsController : ControllerBase
    {
        private readonly ShowService ShowService;

        public ShowsController(ShowService showService)
        {
            ShowService = showService;
        }

        /// <summary>
        /// Gets a paginated list of shows with cast information
        /// </summary>
        /// <param name="rowsPerPage"> number of rows per page</param>
        /// <param name="pageNumber"> the page number to get</param>
        [HttpGet("GetShowsWithCast1")]
        public async Task<ActionResult<ShowDto>> GetShowsWithCast1(int rowsPerPage, int pageNumber)
        {
            if (rowsPerPage < 1 || pageNumber < 1) {
                return BadRequest();
            }
            var result = await ShowService.GetShows(pageNumber * rowsPerPage, rowsPerPage);
            return Ok(result);
        }
    }
}
