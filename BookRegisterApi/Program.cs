using BookRegisterApi.DataContext;
using BookRegisterApi.GraphQL;
using BookRegisterApi.Implementations;
using BookRegisterApi.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connectionString));

builder.Services
    .AddGraphQLServer()
    .AddQueryType<Query>();

builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IBookRegisterService, BookRegisterService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseReDoc(c =>
    {
        c.RoutePrefix = "docs"; // URL: /docs
        c.DocumentTitle = "My API Documentation";
        c.SpecUrl("/swagger/v1/swagger.json"); // OpenAPI JSON
    });
}
app.UseHttpsRedirection();

// Enable GraphQL endpoint
app.UseRouting();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapGraphQL(); // Maps GraphQL to /graphql
});

app.MapControllers();

app.Run();
