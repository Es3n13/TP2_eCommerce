using Microsoft.EntityFrameworkCore;
using TP2.Application.UseCases.Declarations;
using TP2.Application.UseCases.TaxDeclarations;
using TP2.Application.UseCases.Users;
using TP2.Application.UseCases.Agents;
using TP2.Domain.Interfaces;
using TP2.Infrastructure.Data;
using TP2.Infrastructure.Repositories;
using TP2.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// 1. Ajout des services de base
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddOpenApi();

// 2. Configuration de la Base de Données (SQL Server)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        sqlOptions => sqlOptions.EnableRetryOnFailure()
    ));

// 3. Enregistrement des Repositories (Infrastructure)
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAgentRepository, AgentRepository>();
builder.Services.AddScoped<CreateAgentUseCase>();
builder.Services.AddScoped<ITaxDeclarationRepository, TaxDeclarationRepository>();
builder.Services.AddScoped<GetPendingReviewsUseCase>();
builder.Services.AddScoped<DecideDeclarationUseCase>();
builder.Services.AddScoped<AssignDeclarationUseCase>();
builder.Services.AddScoped<IDocumentRepository, DocumentRepository>();
builder.Services.AddScoped<INoticeOfAssessmentRepository, NoticeOfAssessmentRepository>();
builder.Services.AddScoped<IIntegrationLogRepository, IntegrationLogRepository>();
builder.Services.AddScoped<ICreateUserUseCase, CreateUserUseCase>();
builder.Services.AddScoped<ICanadaRevenueService, CanadaRevenueService>();
builder.Services.AddScoped<IProcessAutomaticValidationUseCase, ProcessAutomaticValidationUseCase>();
builder.Services.AddScoped<IInitializeDeclarationUseCase, InitializeDeclarationUseCase>();
builder.Services.AddScoped<ISaveDeclarationDraftUseCase, SaveDeclarationDraftUseCase>();
builder.Services.AddScoped<IUploadSupportingDocumentUseCase, UploadSupportingDocumentUseCase>();
builder.Services.AddScoped<IDownloadNoaUseCase, DownloadNoaUseCase>(); 

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
