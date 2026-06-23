using Microsoft.EntityFrameworkCore;
using TP2.Infrastructure.Data;
using TP2.Infrastructure.Repositories;
using TP2.Domain.Interfaces;
using TP2.Application.UseCases.Declarations;

var builder = WebApplication.CreateBuilder(args);

// 1. Ajout des services de base
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddOpenApi();

// 2. Configuration de la Base de Données (SQL Server)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 3. Enregistrement des Repositories (Infrastructure)
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAgentRepository, AgentRepository>();
builder.Services.AddScoped<ITaxDeclarationRepository, TaxDeclarationRepository>();
builder.Services.AddScoped<IDocumentRepository, DocumentRepository>();
builder.Services.AddScoped<INoticeOfAssessmentRepository, NoticeOfAssessmentRepository>();
builder.Services.AddScoped<IIntegrationLogRepository, IntegrationLogRepository>();

// 4. Enregistrement des Use Cases (Application)
builder.Services.AddScoped<ISubmitTaxDeclarationUseCase, SubmitTaxDeclarationUseCase>();
builder.Services.AddScoped<IGetUserDeclarationsUseCase, GetUserDeclarationsUseCase>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
