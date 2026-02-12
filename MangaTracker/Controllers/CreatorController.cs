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
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllCreators()
        {
            var creators = await _service.GetCreatorsAsync();
            var creatorsDtos = new List<CreatorDto>();

            foreach (var creator in creators)
            {
                creatorsDtos.Add(DtoMapper.MapToCreatorDto(creator));
            }
            return Ok(creatorsDtos);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCreatorById(int id)
        {
            var creator =  await _service.GetCreatorByIdAsync(id);
            if (creator != null)
            {
                return Ok(DtoMapper.MapToCreatorDto(creator));
            }

            return NotFound($"Could not find creator with Id: {id}");
        }

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddCreator(CreateCreatorDto creator)
        {
            var result = await _service.AddAsync(new Creator { FirstName = creator.FirstName, LastName = creator.LastName, IsIllustrator = creator.IsIllustrator, IsAuthor = creator.IsWriter });
            return result.Errors.Any() ? BadRequest(result.Errors) : Created();
        }

        [HttpPut("{id:int}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateCreator(int id, CreatorForMangaDto creator)
        {
            if(id != creator.Id)
            {
                return BadRequest();
            }

            var result = await _service.UpdateCreator(creator);
            return result.Errors.Any() ? BadRequest(result.Errors) : NoContent();
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteCreator(int id)
        {
            var result = await _service.DeleteCreatorAsync(id);
            return result.Errors.Any() ? BadRequest(result.Errors) : NoContent();
        }


    }
}
