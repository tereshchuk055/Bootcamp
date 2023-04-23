using ToDoApp.Services;
using ToDoApp.Repository;
using ToDoApp.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<DapperContext>();
builder.Services.AddSingleton<ChosenRepositoryService>();
builder.Services.AddScoped<CategoryRepositoryFactory>();
builder.Services.AddScoped<TaskRepositoryFactory>();

//builder.Services.AddScoped<ITaskRepository, TaskSqlRepository>();
//builder.Services.AddScoped<ICategoryRepository, CategorySqlRepository>();

builder.Services.AddAutoMapper(typeof(Program).Assembly);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
