using AutoMapper;
using MegaMart.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MegaMart.API.Controllers
{

    public class BugController : BaseController
    {
        public BugController(IUnitOfWork work, IMapper mapper) : base(work, mapper)
        {
        }

        [HttpGet("GetNotFound")]
        public async Task<IActionResult> GetNotFound()
        {
            return NotFound();
        }
    }
}
