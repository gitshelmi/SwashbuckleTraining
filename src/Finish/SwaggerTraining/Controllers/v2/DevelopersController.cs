using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SwaggerTraining.DataAccess;
using SwaggerTraining.DTOs;
using SwaggerTraining.DTOs.V2;

namespace SwaggerTraining.Controllers.v2
{
    [Produces("application/json", "application/xml")]
    [Route("api/v{version:apiVersion}/developers")]
    [ApiVersion("2.0")]
    [ApiController]
    public class DevelopersControllerV2 : ControllerBase
    {
        private readonly IDeveloperRepository _developerRepository;
        private readonly IMapper _mapper;

        public DevelopersControllerV2(IDeveloperRepository developerRepository, IMapper mapper)
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
        public async Task<ActionResult<IEnumerable<DeveloperDtoV2>>> GetDevelopersAsync()
        {
            var developers = await _developerRepository.GetDevelopersAsync();
            return Ok(_mapper.Map<IEnumerable<DeveloperDtoV2>>(developers));
        }
    }
}
