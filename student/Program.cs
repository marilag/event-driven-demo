using System.Reflection;
using Azure.Identity;
using eventschool;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

if (builder.Environment.IsProduction())
{
    builder.Configuration.AddAzureKeyVault(
        new Uri($"https://{builder.Configuration["KeyVaultName"]}.vault.azure.net/"),
        new DefaultAzureCredential());
}
 
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
builder.Services.AddSingleton<IStudentRepository, StudentRepository>();
builder.Services.AddSingleton(typeof(IEventStoreRepository<>),typeof(EventStoreRepository<>));
builder.Services.AddSingleton(typeof(IOutboxRepository<>),typeof(OutboxRepository<>));


builder.Services.AddSingleton<IEventGridService, EventGridService>();

builder.Services.Configure<EventGridOptions>(
    builder.Configuration.GetSection(EventGridOptions.EventGrid));

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
