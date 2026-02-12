using MangaTracker.Dtos;
using MangaTracker.Models;
using MangaTracker.Services;
using MangaTracker.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;

namespace MangaTracker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MangaController(IMangaService service, ICreatorService creatorService) : ControllerBase
    {
        private readonly IMangaService _service = service;
        private readonly ICreatorService _creatorService = creatorService;

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllMangas()
        {
            var mangas = await _service.GetMangasAsync();
            var mangaDtos = new List<MangaDto>();
            foreach (var manga in mangas)
            {
                mangaDtos.Add(DtoMapper.MapToMangaDto(manga));
            }

            return Ok(mangaDtos);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetMangaById(int id)
        {
            var manga = await _service.GetMangaByIdAsync(id);
            if (manga != null)
            {
                return Ok(DtoMapper.MapToMangaDto(manga));
            }

            return NotFound($"Could not find manga with Id: {id}");
        }

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddManga(CreateMangaDto manga)
        {
            List<Creator> creators = [];

            foreach (var c in manga.Creators)
            {
                var creator = await _creatorService.GetCreatorByIdAsync(c.Id);

                if (creator != null)
                {
                    creators.Add(creator);
                }
            }

            var result = await _service.AddAsync(new Manga { Title = manga.Title, Description = manga.Description, Creators = creators, IsLightNovel = manga.IsLightNovel, ReleaseDate = manga.ReleaseDate, Volume = manga.Volume });
            return result.Errors.Any() ? BadRequest(result.Errors) : Created();
        }

        [HttpPut("{id:int}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateManga(int id, MangaForCreatorDto manga)
        {
            if (id != manga.Id)
            {
                return BadRequest();
            }

            var result = await _service.UpdateManga(manga);
            return result.Errors.Any() ? BadRequest(result.Errors) : NoContent();
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteManga(int id)
        {
            var result = await _service.DeleteMangaAsync(id);
            return result.Errors.Any() ? BadRequest(result.Errors) : NoContent();
        }
    }
}
