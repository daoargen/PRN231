using SPHSS.Repository;
using SPHSS.Repository.Models;

namespace SPHSS.Services
{
    public interface IDashboardService
    {
        Task<List<Dashboard>> GetAll();
        Task<Dashboard> GetById(Guid id);
        Task<int> Create(Dashboard dashboard);
        Task<int> Update(Dashboard dashboard);
        Task<bool> Delete(Guid id);
        Task<List<Dashboard>> Search(string? MetricValue, string? MetricName);
    }

    public class DashboardService : IDashboardService
    {
        private DashboardRepository _repository;
        public DashboardService() => _repository = new DashboardRepository();

        public async Task<int> Create(Dashboard dashboard)
        {
            return await _repository.CreateAsync(dashboard);
        }

        public async Task<bool> Delete(Guid id)
        {
            var item = await _repository.GetByIdAsync(id);
            if (item != null)
            {
                return await _repository.RemoveAsync(item);
            }
            return false;
        }

        public async Task<List<Dashboard>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<Dashboard> GetById(Guid id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public Task<List<Dashboard>> Search(string? MetricValue, string? MetricName)
        {
            return _repository.Search(MetricValue, MetricName);
        }

        public async Task<int> Update(Dashboard dashboard)
        {
            var item = await _repository.GetByIdAsync(dashboard.Id);
            if (item != null)
            {
                return await _repository.UpdateAsync(dashboard);
            }
            return 0;
        }
    }
}
