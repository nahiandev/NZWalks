using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.Data;
using NZWalks.Models.Domain;
using NZWalks.Models.DTO;

namespace NZWalks.Controllers
{
    [Route("random/[controller]")]
    [ApiController]
    public class ProjectController : Controller
    {
        private readonly IMapper _mapper;
        private readonly List<Project> _data;

        public ProjectController(IMapper mapper)
        {
            _mapper = mapper;
            _data = ProjectListings.Data;
        }

        
        [HttpGet]
        public IActionResult GetRandomProject()
        {
            var mapped_project = _mapper.Map<List<ProjectDTO>>(_data);

            var project_length = mapped_project.Count();

            int index = new Random().Next(0, project_length -1 );

            var random_project = mapped_project[index];

            return Ok(random_project);
        }
    }
}