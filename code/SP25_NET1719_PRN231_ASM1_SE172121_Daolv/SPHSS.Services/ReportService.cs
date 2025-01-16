using SPHSS.Repository;
using SPHSS.Repository.Models;

namespace SPHSS.Services
{
    public interface IReportService
    {
        Task<List<Report>> GetAll();
        Task<Report> GetById(Guid id);
        Task<int> Create(Report report);
        Task<int> Update(Report report);
        Task<bool> Delete(Guid id);
    }

    public class ReportService : IReportService
    {
        private ReportRepository _repository;
        public ReportService() => _repository = new ReportRepository();

        public async Task<int> Create(Report report)
        {
            return await _repository.CreateAsync(report);
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

        public async Task<List<Report>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<Report> GetById(Guid id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<int> Update(Report report)
        {
            var item = await _repository.GetByIdAsync(report.Id);
            if (item != null)
            {
                return await _repository.UpdateAsync(item);
            }
            return 0;
        }
    }
}
