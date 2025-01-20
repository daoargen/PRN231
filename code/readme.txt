dotnet add package Microsoft.EntityFrameworkCore --version 8.0.5
dotnet add package Microsoft.EntityFrameworkCore.SqlServer --version 8.0.5
dotnet add package Microsoft.EntityFrameworkCore.Tools --version 8.0.5
dotnet add package Microsoft.Extensions.Configuration --version 8.0.0
dotnet add package Microsoft.Extensions.Configuration.Json --version 8.0.0

// install cái này trong service
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


// thêm code sau phía dưới phần AddScope trong program.cs

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });



builder.Services.AddSwaggerGen(option =>
{
    ////JWT Config
    option.DescribeAllParametersInCamelCase();
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

// tạo 1 UserAccountController
// nhét code sau vào trong đó 

[Route("api/[controller]")]
    [ApiController]
    public class UserAccountController : Controller
    {
        private readonly IConfiguration _config;
        private readonly UserAccountService _userAccountsService;

        public UserAccountController(IConfiguration config, UserAccountService userAccountsService)
        {
            _config = config;
            _userAccountsService = userAccountsService;
        }

        [HttpPost("Login")]
        public IActionResult Login([FromBody] LoginReqeust request)
        {
            var user = _userAccountsService.Authenticate(request.UserName, request.Password);

            if (user == null || user.Result == null)
                return Unauthorized();

            var token = GenerateJSONWebToken(user.Result);

            return Ok(token);
        }

        private string GenerateJSONWebToken(UserAccount systemUserAccount)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_config["Jwt:Issuer"]
                    , _config["Jwt:Audience"]
                    , new Claim[]
                    {
                new(ClaimTypes.Name, systemUserAccount.UserName),
                //new(ClaimTypes.Email, systemUserAccount.Email),
                new(ClaimTypes.Role, systemUserAccount.RoleId.ToString()),
                    },
                    expires: DateTime.Now.AddMinutes(120),
                    signingCredentials: credentials
                );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

            return tokenString;
        }

        public sealed record LoginReqeust(string UserName, string Password);


// thêm cái này dưới các api để phân quyền 
[Authorize(Roles = "1, 2")]

// như này này 

        // GET: api/<DashboardController>
        [HttpGet]
        [Authorize(Roles = "1, 2")]
        // public IEnumerable<string> Get()
        public async Task<IEnumerable<Dashboard>> Get()
        {
            return await _services.GetAll();
        }


// tạo project MVC đã ping 
// sau đó chọn chỉnh start up project, chọn multi, chọn start  với 2 project api và mvc
// tạo 1 class tên là LoginRequest trong models

public class LoginRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}

// tạo 1 AccountController trong controller và nhét code sau vào đó *nhớ chỉnh cái APIEndpoint


private string APIEndPoint = "http://localhost:5042/api/";
        public IActionResult Index()
        {
            return RedirectToAction("Login");
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequest login)
        {

            try
            {
                using (var httpClient = new HttpClient())
                {
                    using (var response = await httpClient.PostAsJsonAsync(APIEndPoint + "UserAccount/Login", login))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var tokenString = await response.Content.ReadAsStringAsync();

                            var tokenHandler = new JwtSecurityTokenHandler();
                            var jwtToken = tokenHandler.ReadToken(tokenString) as JwtSecurityToken;

                            if (jwtToken != null)
                            {
                                var userName = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name).Value;
                                var role = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;

                                var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, userName),
                            new Claim(ClaimTypes.Role, role),
                        };

                                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));

                                Response.Cookies.Append("UserName", userName);
                                Response.Cookies.Append("Role", role);

                                return RedirectToAction("Index", "Home");

                                //if (role == "4" || role == "3")
                                //{
                                //    return View(new List<CosmeticInformation>());
                                //}
                                //else
                                //{
                                //    response = await httpClient.GetAsync("CosmesticInformation/odata");
                                //}                                
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }

            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            ModelState.AddModelError("", "Login failure");
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }

        public async Task<IActionResult> Forbidden()
        {
            return View();
        }


// trong view tạo folder Account có 2 file view là Forbidden.cshtml và Login.cshtml

// code trong Forbidden.cshtml

@*
    For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
*@
@{
}

<div class="row text-center">
    <h3 class="text-danger">Forbidden</h3>
    <h4 class="text-danger">You do not have permission to do this function!</h4>
</div>

// code trong Login.cshtml
@*
    For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
*@
@{
}

@model SPHSS.MVCApp.FE.Models.LoginRequest

<div class="row">
    <div class="col-md-4">
    </div>
    <div class="col-md-4">
        <form asp-action="Login">
            <div asp-validation-summary="ModelOnly" class="text-danger"></div>
            <div class="form-group">
                <label asp-for="UserName" class="control-label"></label>
                <input asp-for="UserName" class="form-control" />
                <span asp-validation-for="UserName" class="text-danger"></span>
            </div>
            <div class="form-group">
                <label asp-for="Password" class="control-label"></label>
                <input type="password" asp-for="Password" class="form-control" />
                <span asp-validation-for="Password" class="text-danger"></span>
            </div>
            <div class="form-group">
                <input type="submit" value="Login" class="btn btn-primary" />
            </div>
        </form>
    </div>
    <div class="col-md-4">
    </div>
</div>

// trong program.cs thì thêm cái này 

builder.Services.AddAuthentication()
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
    {
        options.LoginPath = new PathString("/Account/Login");
        options.AccessDeniedPath = new PathString("/Account/Forbidden");
        options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

    });

// dưới phần 
// Add services to the container.
 builder.Services.AddControllersWithViews();

 // thêm 
 app.UseAuthentication();

//thêm cái 
 [Authorize(Roles = "1")]
// cho cả cái home controller để nó ko authen đc và tự động chuyển qua trang login 


// trong Shared/_Layout phía trên thẻ nav thêm cái code này 

<div class="nav-item text-nowrap">
                    Welcome
                    <strong>@Context.Request.Cookies["UserName"].ToString()</strong>
                    | <a href="/Account/Logout">Sign Out</a>
                </div>
            </div>
        </nav>
// nhìn thì thấy nó trên nav và div mới thêm code đó vào


