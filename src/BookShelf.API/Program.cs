
using BookShelf.Core.Interfaces;
using BookShelf.Core.UseCases;
using BookShelf.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("Database")));


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
builder.Services.AddSingleton<INotificationService>(provider =>
    new EmailNotificationService(
        smtpHost: "smtp.gmail.com",
        smtpPort: 587,
        smtpUser: "your-email@example.com",
        smtpPassword: "your-email-password"
    ));

// Register other dependencies...
builder.Services.AddScoped<NotifyLateReturns>();

builder.Services.AddScoped<AddBook>();
builder.Services.AddScoped<AddUser>();
builder.Services.AddScoped<BorrowBook>();
builder.Services.AddScoped<DeleteBook>();
builder.Services.AddScoped<DeleteUser>();
builder.Services.AddScoped<EditBook>();
builder.Services.AddScoped<EditUser>();
builder.Services.AddScoped<GetBooks>();
builder.Services.AddScoped<GetTransactionDetails>();
builder.Services.AddScoped<ListTransactions>();
builder.Services.AddScoped<NotifyLateReturns>();
builder.Services.AddScoped<ReturnBook>();
builder.Services.AddScoped<ViewUserDetails>();


var app = builder.Build();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        options.RoutePrefix = string.Empty; // Swagger UI at the root
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();
