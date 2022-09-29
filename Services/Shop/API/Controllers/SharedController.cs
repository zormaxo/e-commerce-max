using Core.Entities;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers
{
    public class SharedController : BaseApiController
    {
        private readonly CachedItems _cachedItems;

        public SharedController(CachedItems cachedItems)
        {
            _cachedItems = cachedItems;
        }

        [HttpGet("cities")]
        public ActionResult<IReadOnlyList<City>> Cities()
        {
            return Ok(_cachedItems.Cities);
        }

        [HttpGet("counties/{id}")]
        public ActionResult<IReadOnlyList<County>> Counties(string id)
        {
            return Ok(_cachedItems.Counties.Where(x => x.City.Name == id));
        }
    }
}