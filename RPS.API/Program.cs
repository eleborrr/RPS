using RPS.API.Hubs;
using RPS.API.ServicesExtensions.ServicesPipeline;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddServicesPipeline(builder.Configuration);

builder.Configuration.AddEnvironmentVariables();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapHub<GameRoomHub>("/gameRoomHub");

app.UseHttpsRedirection();

app.UseCors("testSpecific");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();