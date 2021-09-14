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
    [Route("api/developers/{developerId}/projects")]
    public class ProjectsController : ControllerBase
    {
        private readonly IDeveloperRepository _developerRepository;
        private readonly IProjectRepository _projectRepository;
        private readonly IMapper _mapper;

        public ProjectsController(IDeveloperRepository developerRepository,
            IProjectRepository projectRepository,
            IMapper mapper)
        {
            _developerRepository = developerRepository;
            _projectRepository = projectRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjectDto>>> GetDeveloperProjectsAsync(Guid developerId)
        {
            if (!await _developerRepository.DeveloperExists(developerId))
            {
                return NotFound();
            }

            var projects = await _projectRepository.GetDeveloperProjectsAsync(developerId);
            if (projects == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<IEnumerable<ProjectDto>>(projects));
        }

        [HttpGet("{projectId}", Name = "GetDeveloperProjectAsync")]
        public async Task<ActionResult<ProjectDto>> GetDeveloperProjectAsync(Guid developerId, Guid projectId)
        {
            if (!await _developerRepository.DeveloperExists(developerId))
            {
                return NotFound();
            }

            var project = await _projectRepository.GetDeveloperProjectAsync(developerId, projectId);
            if (project == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<ProjectDto>(project));
        }

        [HttpPost]
        public async Task<ActionResult<ProjectDto>> AddDeveloperProjectAsync(Guid developerId, NewProjectDto newProjectDto)
        {
            var newProject = _mapper.Map<Project>(newProjectDto);
            newProject.DeveloperId = developerId;
            if (!await _developerRepository.DeveloperExists(developerId))
            {
                return NotFound();
            }

            await _projectRepository.AddDeveloperProjectAsync(newProject);
            var addedProjectDto = _mapper.Map<ProjectDto>(newProject);

            return CreatedAtRoute("GetDeveloperProjectAsync",
            new { developerId = addedProjectDto.DeveloperId, projectId = addedProjectDto.Id },
            addedProjectDto);
        }

        [HttpPut("{projectId}")]
        public async Task<ActionResult> UpdateProjectAsync(Guid developerId,
            Guid projectId,
            UpdateProjectDto updateProjectDto)
        {
            var projectToUpdate = await _projectRepository.GetDeveloperProjectAsync(developerId, projectId);
            
            if (projectToUpdate == null)
            {
                return NotFound();
            }

            projectToUpdate = _mapper.Map(updateProjectDto, projectToUpdate);

            await _projectRepository.UpdateProjectAsync();

            return NoContent();
        }
    }
}

