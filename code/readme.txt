dotnet add package Microsoft.EntityFrameworkCore --version 8.0.5
dotnet add package Microsoft.EntityFrameworkCore.SqlServer --version 8.0.5
dotnet add package Microsoft.EntityFrameworkCore.Tools --version 8.0.5
dotnet add package Microsoft.Extensions.Configuration --version 8.0.0
dotnet add package Microsoft.Extensions.Configuration.Json --version 8.0.0

dotnet add package  Microsoft.AspNetCore.Authentication.JwtBearer --version 8.0.10



// cái này add vào trong file context
// commnet hàm OnConfiguring này lại
public static string GetConnectionString(string connectionStringName)
{
    var config = new ConfigurationBuilder()
        .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
        .AddJsonFile("appsettings.json")
        .Build();

    string connectionString = config.GetConnectionString(connectionStringName);
    return connectionString;
}

protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    => optionsBuilder.UseSqlServer(GetConnectionString("DefaultConnection")).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);


// tạo folder dbContext nhằm tái sử dụng folder model
// kéo file context vào folder comtext và sửa namespace và các lỗi nhỏ có trong context file
// tạo folder base 
// tạo file GenericRepository.cs trong folder base


// nội dung file GenericRepository.cs
// lưu ý là chỉ copy phần phía trong namespace và đổi tên context cho phù hợp với bài
// xóa các commnet vì nó sẽ làm bài bị tình nghi là sao chép bài
// thay đổi các getById tương tự ví dụ như id là int thì chỉnh là int, string thì string 
// khúc cuối của phần này là phần nâng cao, nhằm làm tất cả các thao tác sau đó save 1 lần chung nhằm tăng hiệu năng 

public class GenericRepository<T> where T : class
{
    protected zPaymentContext _context;

    public GenericRepository()
    {
        _context ??= new zPaymentContext();
    }

    public GenericRepository(zPaymentContext context)
    {
        _context = context;
    }

    public List<T> GetAll()
    {
        return _context.Set<T>().ToList();
    }
    public async Task<List<T>> GetAllAsync()
    {
        return await _context.Set<T>().ToListAsync();
    }
    public void Create(T entity)
    {
        _context.Add(entity);
        _context.SaveChanges();
    }

    public async Task<int> CreateAsync(T entity)
    {
        _context.Add(entity);
        return await _context.SaveChangesAsync();
    }

    public void Update(T entity)
    {
        var tracker = _context.Attach(entity);
        tracker.State = EntityState.Modified;
        _context.SaveChanges();

        //if (_context.Entry(entity).State == EntityState.Detached)
        //{
        //    var tracker = _context.Attach(entity);
        //    tracker.State = EntityState.Modified;
        //}
        //_context.SaveChanges();
    }

    public async Task<int> UpdateAsync(T entity)
    {
        //var trackerEntity = _context.Set<T>().Local.FirstOrDefault(e => e == entity);
        //if (trackerEntity != null)
        //{
        //    _context.Entry(trackerEntity).State = EntityState.Detached;
        //}
        //var tracker = _context.Attach(entity);
        //tracker.State = EntityState.Modified;
        //return await _context.SaveChangesAsync();

        var tracker = _context.Attach(entity);
        tracker.State = EntityState.Modified;
        return await _context.SaveChangesAsync();

        //if (_context.Entry(entity).State == EntityState.Detached)
        //{
        //    var tracker = _context.Attach(entity);
        //    tracker.State = EntityState.Modified;
        //}

        //return await _context.SaveChangesAsync();
    }

    public bool Remove(T entity)
    {
        _context.Remove(entity);
        _context.SaveChanges();
        return true;
    }

    public async Task<bool> RemoveAsync(T entity)
    {
        _context.Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }

    public T GetById(int id)
    {
        var entity = _context.Set<T>().Find(id);
        if (entity != null)
        {
            _context.Entry(entity).State = EntityState.Detached;
        }

        return entity;

        //return _context.Set<T>().Find(id);
    }

    public async Task<T> GetByIdAsync(int id)
    {
        var entity = await _context.Set<T>().FindAsync(id);
        if (entity != null)
        {
            _context.Entry(entity).State = EntityState.Detached;
        }

        return entity;

        //return await _context.Set<T>().FindAsync(id);
    }

    public T GetById(string code)
    {
        var entity = _context.Set<T>().Find(code);
        if (entity != null)
        {
_context.Entry(entity).State = EntityState.Detached;
        }

        return entity;

        //return _context.Set<T>().Find(code);
    }

    public async Task<T> GetByIdAsync(string code)
    {
        var entity = await _context.Set<T>().FindAsync(code);
        if (entity != null)
        {
            _context.Entry(entity).State = EntityState.Detached;
        }

        return entity;

        //return await _context.Set<T>().FindAsync(code);
    }

    public T GetById(Guid code)
    {
        var entity = _context.Set<T>().Find(code);
        if (entity != null)
        {
            _context.Entry(entity).State = EntityState.Detached;
        }

        return entity;

        //return _context.Set<T>().Find(code);
    }

    public async Task<T> GetByIdAsync(Guid code)
    {
        var entity = await _context.Set<T>().FindAsync(code);
        if (entity != null)
        {
            _context.Entry(entity).State = EntityState.Detached;
        }

        return entity;

        //return await _context.Set<T>().FindAsync(code);
    }

    #region Separating asigned entity and save operators        

    public void PrepareCreate(T entity)
    {
        _context.Add(entity);
    }

    public void PrepareUpdate(T entity)
    {
        var tracker = _context.Attach(entity);
        tracker.State = EntityState.Modified;
    }

    public void PrepareRemove(T entity)
    {
        _context.Remove(entity);
    }

    public int Save()
    {
        return _context.SaveChanges();
    }

    public async Task<int> SaveAsync()
    {
        return await _context.SaveChangesAsync();
    }

    #endregion Separating asign entity and save operators
}

// hàm login cho userAccount


public async Task<UserAccount> GetUserAccountAsync(string userName,  string password)
        {
            return await _context.UserAccounts.FirstOrDefaultAsync(u =>
            u.UserName == userName &&
            u.Password ==  password &&
            u.IsActive == true);

            //return await _context.UserAccounts.FirstOrDefaultAsync(u =>
            //u.Email == email &&
            //u.Password == password &&
            //u.IsActive == true);

            //return await _context.UserAccounts.FirstOrDefaultAsync(u =>
            //u.Phone == phone &&
            //u.Password == password &&
            //u.IsActive == true);

            //return await _context.UserAccounts.FirstOrDefaultAsync(u =>
            //u.EmployCode == employCode &&
            //u.Password == password &&
            //u.IsActive == true);
        }


// hàm interface để trong file service luôn cho tiện 

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
        Task<List<Dashboard>> Search(Guid id, string MetricValue, string MetricName, Boolean IsDeleted);
    }

    public class DashboardService : IDashboardService
    {
        private DashboardRepository _repository;
        public DashboardService()
        {
            _repository = new DashboardRepository();
        }

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

        public Task<List<Dashboard>> Search(Guid id, string MetricValue, string MetricName, bool IsDeleted)
        {
            return _repository.Search(id, MetricValue, MetricName, IsDeleted);
        }

        public async Task<int> Update(Dashboard dashboard)
        {
            var item = await _repository.GetByIdAsync(dashboard.Id);
            if (item != null)
            {
                return await _repository.UpdateAsync(item);
            }
            return 0;
        }
    }
}


// tạo project api 
// tạo thêm controller = cách là chuột phải controller, chọn add => new scafford item => api => read/write controller 
// trong cotroller nhớ có 
    private readonly IDashboardService _services;

        public DashboardController(IDashboardService services)
        {
            _services = services;
        }


// nhớ add các dependency injection vào startup.cs 


using SPHSS.Services;
using System.Text.Json.Serialization;

namespace SPHSS.APIServices
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            // Dependency Injection

            builder.Services.AddScoped<IDashboardService, DashboardService>();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.Never;
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}


// thêm cái này tránh bị lập json trong startup.cs
  builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.Never;
            });


// chỉnh appsetting.json 










