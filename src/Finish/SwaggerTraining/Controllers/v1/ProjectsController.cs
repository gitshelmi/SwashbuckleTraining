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
    [Route("api/v{version:apiVersion}/developers/{developerId}/Projects")]
    [ApiController]
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

        /// <summary>
        /// Returns all projects for the specified developer.
        /// </summary>
        /// <returns>All developers as a IEnumerable of <see cref="ProjectDto"/>.</returns>
        /// <response code="200">Returns the specified projects.</response>
        /// <response code="404">If the specified developer does not exist.</response>
        /// <response code="406">If the requested content-type (Accept) is not acceptable.</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status406NotAcceptable)]
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

        /// <summary>
        /// Returns a project using the given developer and project IDs.
        /// </summary>
        /// <returns>The specified project as a <see cref="ProjectDto"/>.</returns>
        /// <response code="200">Returns the specified developer.</response>
        /// <response code="404">If the specified developer or project does not exist.</response>
        /// <response code="406">If the requested content-type (Accept) is not acceptable.</response>
        [HttpGet("{projectId}", Name = "GetDeveloperProjectAsync")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status406NotAcceptable)]
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

        /// <summary>
        /// Adds a new projects.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /newProjectDto
        ///     {
        ///       "Name":"Azure Basics Training",
        ///       "Description":"A basic training for Azure"
        ///     }
        ///
        /// </remarks>
        /// <returns>A newly created <see cref="ProjectDto"/>.</returns>
        /// <response code="201">Returns the newly created DeveloperDto.</response>
        /// <response code="400">If the item is null or validation fails.</response>
        /// <response code="406">If the requested content-type (Accept) is not acceptable.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status406NotAcceptable)]
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

        /// <summary>
        /// Updates a project.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT
        ///     {
        ///       "Name":"Docker Training",
        ///       "Description":"A great Docker Training."
        ///     }
        ///
        /// </remarks>
        /// <returns>The status code.</returns>
        /// <response code="204">If the update is successful.</response>
        /// <response code="400">If the item is null or validation fails (e.g., same title and description).</response>
        /// <response code="406">If the requested content-type (Accept) is not acceptable.</response>
        [HttpPut("{projectId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status406NotAcceptable)]
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

