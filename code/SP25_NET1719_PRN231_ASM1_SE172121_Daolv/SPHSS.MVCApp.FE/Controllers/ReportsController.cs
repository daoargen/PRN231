using Microsoft.AspNetCore.Mvc;

namespace SPHSS.MVCApp.FE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private string APIEndPoint = "http://localhost:5042/api/";

        public ReportsController()
        {
        }

        //public async Task<IActionResult> Index()
        //{
        //    using (var httpClient = new HttpClient())
        //    {
        //        // get token
        //        string token = HttpContext.Request.Cookies["Token"];

        //        httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

        //        using (var response = await httpClient.GetAsync(APIEndPoint + "Report"))
        //        {
        //            if (response.IsSuccessStatusCode)
        //            {
        //                var content = await response.Content.ReadAsStringAsync();
        //                var result = JsonConvert.DeserializeObject<List<Report>>(content);

        //                if (result != null)
        //                {
        //                    return View(result);
        //                }
        //            }
        //        }
        //    }

        //    return View(new List<Report>());
        //}
        /*private readonly NET1719_PRN231_PRJ_G3_SchoolPsychologicalHealthSupportSystemContext _context;

        public ReportsController(NET1719_PRN231_PRJ_G3_SchoolPsychologicalHealthSupportSystemContext context)
        {
            _context = context;
        }

        // GET: api/Reports
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Report>>> GetReports()
        {
            return await _context.Reports.ToListAsync();
        }

        // GET: api/Reports/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Report>> GetReport(Guid id)
        {
            var report = await _context.Reports.FindAsync(id);

            if (report == null)
            {
                return NotFound();
            }

            return report;
        }

        // PUT: api/Reports/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutReport(Guid id, Report report)
        {
            if (id != report.Id)
            {
                return BadRequest();
            }

            _context.Entry(report).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReportExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Reports
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Report>> PostReport(Report report)
        {
            _context.Reports.Add(report);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ReportExists(report.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetReport", new { id = report.Id }, report);
        }

        // DELETE: api/Reports/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReport(Guid id)
        {
            var report = await _context.Reports.FindAsync(id);
            if (report == null)
            {
                return NotFound();
            }

            _context.Reports.Remove(report);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ReportExists(Guid id)
        {
            return _context.Reports.Any(e => e.Id == id);
        }*/
    }
}
