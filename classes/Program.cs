using System.Reflection;
using Azure.Identity;
using eventschool;
using MediatR;
using Microsoft.Extensions.Azure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
builder.Services.AddSingleton<IClassesRepository,ClassesRepository>();
builder.Services.AddSingleton<IEventGridService, EventGridService>();
builder.Services.Configure<EventGridOptions>(
    builder.Configuration.GetSection(EventGridOptions.EventGrid));

builder.Services.AddAzureClients(b => 
    b.AddServiceBusClient(builder.Configuration.GetValue<string>("ServiceBus:TopicEndpoint")));

builder.Services.Configure<ServiceBusOptions>(
    builder.Configuration.GetSection(ServiceBusOptions.ServiceBus));

 builder.Services.AddHostedService<ServiceBusWorker>();

if (builder.Environment.IsProduction())
{
    builder.Configuration.AddAzureKeyVault(
        new Uri($"https://{builder.Configuration["KeyVaultName"]}.vault.azure.net/"),
        new DefaultAzureCredential());
}


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
