using SPHSS.Repository;
using SPHSS.Repository.Models;

namespace SPHSS.Services
{
    public interface IReportService
    {
        Task<List<Report>> GetAll();
        Task<Report> GetById(Guid id);
        Task<int> Create(Report dashboard);
        Task<int> Update(Report dashboard);
        Task<bool> Delete(Guid id);
    }

    public class ReportService : IReportService
    {
        private ReportRepository _repository;
        public ReportService() => _repository = new ReportRepository();

        public Task<int> Create(Report dashboard)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Report>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<Report> GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<int> Update(Report dashboard)
        {
            throw new NotImplementedException();
        }
    }
}
