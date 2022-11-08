using AutoMapper;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Shop.Core.Dtos;

namespace Application.Controllers
{
    public class SharedController : BaseApiController
    {
        private readonly CachedItems _cachedItems;
        private readonly IMapper _mapper;

        public SharedController(CachedItems cachedItems, IMapper mapper)
        {
            _cachedItems = cachedItems;
            _mapper = mapper;
        }

        [HttpGet("cities")]
        public ActionResult<IReadOnlyList<CityDto>> Cities()
        { return Ok(_mapper.Map<IReadOnlyList<CityDto>>(_cachedItems.Cities)); }

        [HttpGet("counties/{id}")]
        public ActionResult<IReadOnlyList<CountyDto>> Counties(string id)
        { return Ok(_mapper.Map<IReadOnlyList<CityDto>>(_cachedItems.Counties.Where(x => x.City.Name == id))); }
    }
}