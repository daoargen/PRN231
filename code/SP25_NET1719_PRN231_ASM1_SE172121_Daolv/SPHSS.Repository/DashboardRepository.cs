using Microsoft.EntityFrameworkCore;
using SPHSS.Repository.Base;
using SPHSS.Repository.Models;

namespace SPHSS.Repository
{
    public class DashboardRepository : GenericRepository<Dashboard>
    {
        public DashboardRepository() { }

        public async Task<List<Dashboard>> GetAll()
        {
            var items = await _context.Dashboards.Include(a => a.Report).ToListAsync();
            return items;
        }

        public async Task<List<Dashboard>> Search(string? MetricValue, string? MetricName)
        {
            var items = await _context.Dashboards
                .Include(a => a.Report)
                .Where(a =>
                (a.MetricName.Contains(MetricValue) || string.IsNullOrEmpty(MetricValue))
                && (a.MetricName.Contains(MetricName) || string.IsNullOrEmpty(MetricName))
                ).ToListAsync();
            return items;
        }

        public async Task<Dashboard> GetByIdAsync(Guid id)
        {
            var item = await _context.Dashboards.Include(a => a.Report).FirstOrDefaultAsync(a => a.Id == id);
            return item;
        }
    }
}
