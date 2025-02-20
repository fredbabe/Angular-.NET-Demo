using Microsoft.EntityFrameworkCore;
using SupermarketProductBoardAPI.BusinessLogic.CategoryLogic;
using SupermarketProductBoardAPI.BusinessLogic.CompanyLogic;
using SupermarketProductBoardAPI.BusinessLogic.MealPlannerBusinessLogic;
using SupermarketProductBoardAPI.BusinessLogic.PaperBusinessLogic;
using SupermarketProductBoardAPI.BusinessLogic.ProductBusinessLogic;
using SupermarketProductBoardAPI.Data;
using SupermarketProductBoardAPI.Services.CategoryService;
using SupermarketProductBoardAPI.Services.CompanyService;
using SupermarketProductBoardAPI.Services.MealPlannerService;
using SupermarketProductBoardAPI.Services.PaperService;
using SupermarketProductBoardAPI.Services.ProductService;

var builder = WebApplication.CreateBuilder(args);

var environment = builder.Environment.EnvironmentName;
Console.WriteLine($"Current Environment: {environment}");

builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

// Add services to the container.
builder.Services.AddControllers();

// Business Logic
builder.Services.AddScoped<IProductBusinessLogic, ProductBusinessLogic>();
builder.Services.AddScoped<ICompanyBusinessLogic, CompanyBusinessLogic>();
builder.Services.AddScoped<IPaperBusinessLogic, PaperBusinessLogic>();
builder.Services.AddScoped<ICategoryBusinessLogic, CategoryBusinessLogic>();
builder.Services.AddScoped<IMealPlannerBusinessLogic, MealPlannerBusinessLogic>();


// Services
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<ICompanyService, CompanyService>();
builder.Services.AddScoped<IPaperService, PaperService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IMealPlannerService, MealPlannerService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add DbContext with the connection string
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
