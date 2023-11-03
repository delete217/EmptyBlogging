using System.Text;
using EfCoreDemo.API.Entity;
using EfCoreDemo.API.Service;
using EfCoreDemo.API.Service.Impl;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables()
            .Build();
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDistributedRedisCache(options =>
{
    options.Configuration = "127.0.0.1:6379";
    options.InstanceName = "EfcoreDemo-redis";
});
builder.Services.AddCors(options =>
options.AddPolicy("cors",
p => p.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));
// 数据库连接
builder.Services.AddDbContext<BlogContext>(options =>
{
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"), new MySqlServerVersion(new Version(8, 0, 2)))
    // 项目全局关闭数据跟踪
    .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
});

//依赖注入      
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IArcticleBodyService, ArticleBodyService>();
builder.Services.AddTransient<IArticleService, ArticleService>();
builder.Services.AddTransient<ICategoryService, CategoryService>();
builder.Services.AddTransient<ILoginService, LoginService>();
builder.Services.AddTransient<IAdminService, AdminService>();
builder.Services.AddTransient<ITagService, TagService>();
builder.Services.AddTransient<ICommentService, CommentService>();
builder.Services.AddTransient<IArticleTagService, ArticleTagService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseCors("cors");
app.MapControllers();

app.Run();
