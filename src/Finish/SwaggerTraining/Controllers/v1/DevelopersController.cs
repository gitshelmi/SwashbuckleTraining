using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SwaggerTraining.DataAccess;
using SwaggerTraining.DTOs;
using SwaggerTraining.Models;

namespace SwaggerTraining.Controllers.v1
{
    [Produces("application/json", "application/xml")]
    [Route("api/v{version:apiVersion}/developers")]
    [ApiController]
    public class DevelopersController : ControllerBase
    {
        private readonly IDeveloperRepository _developerRepository;
        private readonly IMapper _mapper;

        public DevelopersController(IDeveloperRepository developerRepository, IMapper mapper)
        {
            _developerRepository = developerRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Returns all developers.
        /// </summary>
        /// <returns>All developers as a IEnumerable of <see cref="DeveloperDto"/>.</returns>
        /// <response code="200">Returns the specified developer.</response>
        /// <response code="406">If the requested content-type (Accept) is not acceptable.</response>
        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status406NotAcceptable)]
        public async Task<ActionResult<IEnumerable<DeveloperDto>>> GetDevelopersAsync()
        {
            var developers = await _developerRepository.GetDevelopersAsync();
            return Ok(_mapper.Map<IEnumerable<DeveloperDto>>(developers));
        }

        /// <summary>
        /// Returns a specific developer by id.
        /// </summary>
        /// <param name="developerId"></param>
        /// <returns>The specified developer <see cref="DeveloperDto"/>.</returns>
        /// <response code="200">Returns the specified developer.</response>
        /// <response code="404">If the developer does not exist.</response>
        /// <response code="406">If the requested content-type (Accept) is not acceptable.</response>
        [HttpGet("{developerId}", Name = "GetDeveloperAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status406NotAcceptable)]
        public async Task<ActionResult<DeveloperDto>> GetDeveloperAsync(Guid developerId)
        {
            var developer = await _developerRepository.GetDeveloperAsync(developerId);
            if (developer == null)
            {
                return NotFound();
            }

            return _mapper.Map<DeveloperDto>(developer);
        }

        /// <summary>
        /// Adds a developer.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /NewDeveloperDto
        ///     {
        ///       "firstName": "Margaery",
        ///       "lastName": "Tyrell",
        ///       "dateOfBirth": "1653-01-01"
        ///     }
        ///
        /// </remarks>
        /// <returns>A newly created <see cref="DeveloperDto"/>.</returns>
        /// <response code="201">Returns the newly created DeveloperDto.</response>
        /// <response code="400">If the item is null or validation fails.</response>
        /// <response code="406">If the requested content-type (Accept) is not acceptable.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status406NotAcceptable)]
        public async Task<ActionResult<DeveloperDto>> AddDeveloperAsync(NewDeveloperDto newDeveloperDto)
        {
            var newDeveloper = _mapper.Map<Developer>(newDeveloperDto);
            await _developerRepository.AddDeveloperAsync(newDeveloper);
            var addedDeveloperDto = _mapper.Map<DeveloperDto>(newDeveloper);

            return CreatedAtRoute("GetDeveloperAsync",
                new { developerId = addedDeveloperDto.Id },
                addedDeveloperDto);
        }
    }
}
