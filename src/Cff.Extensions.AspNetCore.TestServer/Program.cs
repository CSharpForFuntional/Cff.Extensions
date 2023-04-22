using Cff.Extensions.AspNetCore.TestServer.Controller;
using Cff.Extensions.AspNetCore.TestServer.Dto;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapPost("/api/NotificationMail", Controller.NewMethod).Accepts<PersonDto>("application/json");

await app.RunAsync();




public partial class Program { }
