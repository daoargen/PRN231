using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using SPHSS.Repository.Models;

namespace SPHSS.MVCApp.FE.Controllers
{
    public class DashboardsController : Controller
    {
        private string APIEndPoint = "http://localhost:5042/api/";

        public DashboardsController()
        {
        }

        public async Task<IActionResult> Index()
        {
            using (var httpClient = new HttpClient())
            {
                // get token
                string token = HttpContext.Request.Cookies["Token"];

                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

                using (var response = await httpClient.GetAsync(APIEndPoint + "Dashboard"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<List<Dashboard>>(content);

                        if (result != null)
                        {
                            return View(result);
                        }
                    }
                }
            }

            return View(new List<Dashboard>());
        }

        //private readonly NET1719_PRN231_PRJ_G3_SchoolPsychologicalHealthSupportSystemContext _context;

        /*
        public DashboardsController(NET1719_PRN231_PRJ_G3_SchoolPsychologicalHealthSupportSystemContext context)
        {
            _context = context;
        }

        // GET: Dashboards
        public async Task<IActionResult> Index()
        {
            var nET1719_PRN231_PRJ_G3_SchoolPsychologicalHealthSupportSystemContext = _context.Dashboards.Include(d => d.Report);
            return View(await nET1719_PRN231_PRJ_G3_SchoolPsychologicalHealthSupportSystemContext.ToListAsync());
        }*/

        // GET: Dashboards/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            using (var httpClient = new HttpClient())
            {
                // get token
                string token = HttpContext.Request.Cookies["Token"];

                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

                using (var response = await httpClient.GetAsync(APIEndPoint + "Dashboard/" + id))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<Dashboard>(content);

                        if (result != null)
                        {
                            return View(result);
                        }
                    }
                }
            }

            return View(new Dashboard());
        }

        public async Task<List<Report>> GetReports()
        {
            using (var httpClient = new HttpClient())
            {
                // get token
                string token = HttpContext.Request.Cookies["Token"];

                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

                using (var response = await httpClient.GetAsync(APIEndPoint + "Report"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<List<Report>>(content);

                        if (result != null)
                        {
                            return result;
                        }
                    }
                }
            }
            return new List<Report>();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id,ReportId,MetricName,MetricValue,MetricCategory,IsDeleted,CreatedAt,UpdateAt")] Dashboard dashboard)
        {
            if (!ModelState.IsValid)
            {
                return View(dashboard);
            }
            using (var httpClient = new HttpClient())
            {
                // get token
                string token = HttpContext.Request.Cookies["Token"];
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

                // chuyển thành json
                using (var response = await httpClient.PostAsJsonAsync(APIEndPoint + "Dashboard", dashboard))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var createdDashboard = JsonConvert.DeserializeObject<int>(content);

                        if (createdDashboard > 0)
                        {
                            // Chuyển hướng đến trang Index sau khi tạo thành công
                            return RedirectToAction(nameof(Index));
                        }
                    }
                }
            }
            return View(dashboard);
        }

        [HttpGet]
        // GET: Dashboards/Create
        public async Task<IActionResult> CreateAsync()
        {
            var reports = await GetReports();
            ViewData["ReportId"] = new SelectList(reports, "Id", "Tittle");
            return View();
        }

        /*// POST: Dashboards/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ReportId,MetricName,MetricValue,MetricCategory,IsDeleted,CreatedAt,UpdateAt")] Dashboard dashboard)
        {
            if (ModelState.IsValid)
            {
                dashboard.Id = Guid.NewGuid();
                _context.Add(dashboard);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ReportId"] = new SelectList(_context.Reports, "Id", "Tittle", dashboard.ReportId);
            return View(dashboard);
        }

        // GET: Dashboards/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dashboard = await _context.Dashboards.FindAsync(id);
            if (dashboard == null)
            {
                return NotFound();
            }
            ViewData["ReportId"] = new SelectList(_context.Reports, "Id", "Tittle", dashboard.ReportId);
            return View(dashboard);
        }

        // POST: Dashboards/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,ReportId,MetricName,MetricValue,MetricCategory,IsDeleted,CreatedAt,UpdateAt")] Dashboard dashboard)
        {
            if (id != dashboard.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dashboard);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DashboardExists(dashboard.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ReportId"] = new SelectList(_context.Reports, "Id", "Tittle", dashboard.ReportId);
            return View(dashboard);
        }

        // GET: Dashboards/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dashboard = await _context.Dashboards
                .Include(d => d.Report)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dashboard == null)
            {
                return NotFound();
            }

            return View(dashboard);
        }

        // POST: Dashboards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var dashboard = await _context.Dashboards.FindAsync(id);
            if (dashboard != null)
            {
                _context.Dashboards.Remove(dashboard);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DashboardExists(Guid id)
        {
            return _context.Dashboards.Any(e => e.Id == id);
        }
    }
       */
    }
}
