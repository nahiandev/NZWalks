using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.Models.Domain;
using NZWalks.Models.DTO;
using NZWalks.Repository;

namespace NZWalks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IWalkRepository _walks;

        public WalksController(IMapper mapper, IWalkRepository walks)
        {
            _mapper = mapper;
            _walks = walks;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddWalkDTO walk)
        {
            var dto_to_domain_walk = _mapper.Map<Walk>(walk);

            var saved_walk = await _walks.CreateAsync(dto_to_domain_walk);

            var mapped_walk = _mapper.Map<WalkDTO>(saved_walk);

            return CreatedAtAction(nameof(Create), saved_walk.Id,mapped_walk);
        }
    }
}
