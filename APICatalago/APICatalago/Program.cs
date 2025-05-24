using APICatalago.Context;
using APICatalago.Extensions;
using APICatalago.Filters;
using APICatalago.Interfaces;
using APICatalago.Logging;
using APICatalago.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Configura��o para mapeamento dos controllers "AddControllers" e "MapControllers"
// Inclus�o do middleware de exce��es n�o tratadas
builder.Services.AddControllers(options => options.Filters.Add(typeof(ApiExceptionFilter)));
//Corre��o de erro por referencia ciclica
builder.Services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Definindo tempo de vida do servido para utiliza��o do log
builder.Services.AddScoped<ApiLoggingFilter>();

//Toda vez que ICategoriaRepository fore referenciada, passara a implementa��o definida em CategoriaRepository
// para cada escopo de solicita��o (request) ser� criada uma instancia isolada de CategoriaRepository
builder.Services.AddScoped<ICategoriaRepository, CategoriaRepository>();
builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Logging.AddProvider(new CustomLoggerProvider(new CustomLoggerProviderConfiguration
{
    LogLevel = LogLevel.Information,
}));

// C�digo de instancia de contexto para acessar o Etinty Framework Core
string mySqlConnection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options => options.UseMySql(mySqlConnection, ServerVersion.AutoDetect(mySqlConnection)));

var app = builder.Build();

// Configure the HTTP request pipeline, nesse ponto configurace os Middlewaree que o programa ira utiliza.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.ConfigureExceptionHandler();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
