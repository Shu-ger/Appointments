using FluentValidation;
using MongoDB.Driver;
using QueueManagementSystem1.Commands.Validators;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Configure MongoDB
var mongoClient = new MongoClient(builder.Configuration["MongoDb:ConnectionString"]);
var mongoDatabase = mongoClient.GetDatabase(builder.Configuration["MongoDb:DatabaseName"]);
builder.Services.AddSingleton(mongoDatabase);

// Add MediatR
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

// Add FluentValidation
builder.Services.AddValidatorsFromAssemblyContaining<CreateAppointmentValidator>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();
app.MapControllers();

app.Run();
