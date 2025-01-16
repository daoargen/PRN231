using Microsoft.AspNetCore.Mvc;
using SPHSS.Repository.Models;
using SPHSS.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SPHSS.APIServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly IDashboardService _services;

        public DashboardController(IDashboardService services)
        {
            _services = services;
        }


        // GET: api/<DashboardController>
        [HttpGet]
        // public IEnumerable<string> Get()
        public async Task<IEnumerable<Dashboard>> Get()
        {
            return await _services.GetAll();
        }

        // GET api/<DashboardController>/5
        [HttpGet("{id}")]
        // public string Get(int id)
        public async Task<Dashboard> Get(Guid id)
        {
            return await _services.GetById(id);
        }

        // GET api/<DashboardController>/5
        [HttpGet("search")]
        // public string Get(int id)
        public async Task<IEnumerable<Dashboard>> Search(string? MetricValue, string? MetricName)
        {
            return await _services.Search(MetricValue, MetricName);
        }

        // POST api/<DashboardController>
        [HttpPost]
        public async Task<int> Post(Dashboard dashboard)
        {
            return await _services.Create(dashboard);
        }

        // PUT api/<DashboardController>/5
        [HttpPut("{id}")]
        public async Task<int> Put(Dashboard dashboard)
        {
            return await _services.Update(dashboard);
        }

        // DELETE api/<DashboardController>/5
        [HttpDelete("{id}")]
        public async Task<bool> Delete(Guid id)
        {
            return await _services.Delete(id);
        }
    }
}
