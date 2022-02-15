using FluentValidation.AspNetCore;
using MediatR;
using PaymentGateway.Api.Clients;
using PaymentGateway.Api.Handlers;
using PaymentGateway.Api.Repositories;
using PaymentGateway.Api.Validation;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddTransient<IPaymentsRepository, PaymentsRepository>();
builder.Services.AddHttpClient<IAcquiringBankClient, AcquiringBankClient>((serviceProvider, client) =>
{
    IConfiguration configuration = serviceProvider.GetRequiredService<IConfiguration>();
    client.BaseAddress = new Uri(configuration.GetValue<string>("AcquiringBankBaseUri"));
});
builder.Services.AddMediatR(typeof(ProcessPaymentHandler));

builder.Services
    .AddControllers()
    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<PaymentRequestValidator>());

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
