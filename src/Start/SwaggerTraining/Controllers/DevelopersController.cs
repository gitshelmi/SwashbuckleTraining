using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SwaggerTraining.DataAccess;
using SwaggerTraining.DTOs;
using SwaggerTraining.Models;

namespace SwaggerTraining.Controllers
{
    [ApiController]
    [Route("api/developers")]
    public class DevelopersController : ControllerBase
    {
        private readonly IDeveloperRepository _developerRepository;
        private readonly IMapper _mapper;

        public DevelopersController(IDeveloperRepository developerRepository, IMapper mapper)
        {
            _developerRepository = developerRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DeveloperDto>>> GetDevelopersAsync()
        {
            var developers = await _developerRepository.GetDevelopersAsync();
            return Ok(_mapper.Map<IEnumerable<DeveloperDto>>(developers));
        }

        [HttpGet("{developerId}", Name = "GetDeveloperAsync")]
        public async Task<ActionResult<DeveloperDto>> GetDeveloperAsync(Guid developerId)
        {
            var developer = await _developerRepository.GetDeveloperAsync(developerId);
            if (developer == null)
            {
                return NotFound();
            }

            return _mapper.Map<DeveloperDto>(developer);
        }

        [HttpPost]
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
