using Microsoft.EntityFrameworkCore;
using SPHSS.Repository.Base;
using SPHSS.Repository.Models;

namespace SPHSS.Repository
{
    public class ReportRepository : GenericRepository<Report>
    {
        public ReportRepository() { }

        public async Task<List<Report>> GetAll()
        {
            var items = await _context.Reports.Include(a => a.Dashboards).ToListAsync();
            return items;
        }
    }
}
