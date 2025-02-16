using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SPHSS.Repository.Models;
using SPHSS.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SPHSS.APIServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IReportService _services;

        public ReportController(IReportService services)
        {
            _services = services;
        }

        // GET: api/<DashboardController>
        [HttpGet]
        [Authorize(Roles = "1, 2")]
        // public IEnumerable<string> Get()
        public async Task<IEnumerable<Report>> Get()
        {
            return await _services.GetAll();
        }
    }
}
