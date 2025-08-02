using FluentValidation;
using LoanSimulator.Application.CORS.Commands;
using LoanSimulator.Infrastructure.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Add Swagger services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



// Register MediatR handlers from Application assembly
builder.Services.AddMediatR(typeof(CreateLoanCommand).Assembly);

// Register FluentValidation validators from Application assembly
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<CreateLoanCommandValidator>();



// Register DbContext before building the app
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


// Add CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy =>
        {
            policy
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseDefaultFiles();
app.MapStaticAssets();

app.UseHttpsRedirection();
app.UseCors("AllowFrontend");
app.UseAuthorization();
app.MapControllers();
app.MapFallbackToFile("/index.html");

app.Run();