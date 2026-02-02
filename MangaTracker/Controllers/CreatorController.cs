using MangaTracker.Dtos;
using MangaTracker.Models;
using MangaTracker.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace MangaTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CreatorController(ICreatorService service) : ControllerBase
    {
        private readonly ICreatorService _service = service;

        [HttpGet]
        public async Task<IActionResult> GetAllCreators()
        {
            return Ok(await _service.GetCreatorsAsync());
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCreatorById(int id)
        {
            var creator = await _service.GetCreatorByIdAsync(id);
            if(creator != null)
            {
                return Ok(creator);
            }

            return NotFound($"Could not find creator with Id: {id}");
        }

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddCreator(CreateCreator creator)
        {
            var result = await _service.AddAsync(new Creator { FirstName = creator.FirstName, LastName = creator.LastName, IsIllustrator = creator.IsIllustrator, IsAuthor = creator.IsWriter });
            return result.Errors.Any() ? BadRequest(result.Errors) : Created();
        }
    }
}
