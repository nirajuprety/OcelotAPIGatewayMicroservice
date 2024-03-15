using OcelotAPIGatewayMicroservice.Services;

var builder = WebApplication.CreateBuilder(args);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
        {
            builder.WithOrigins("*")
                                .AllowAnyHeader()
                                .AllowAnyMethod();
        });
});


builder.Services.AddEndpointsApiExplorer();

builder.Services.AddOcelotServices(builder.Configuration);
builder.Services.ConfigureDownstreamHostAndPortsPlaceholders(builder.Configuration);
builder.Services.AddSwaggerForOcelot(builder.Configuration);

builder.Services.AddHealthChecks();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwaggerForOcelotUI(builder.Configuration);

}

string IsProduction = builder.Configuration["IsProduction"];
if (IsProduction == "True")
{
    app.UseSwaggerForOcelotUI(builder.Configuration);
}

app.UseCors();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.UseOcelot().Wait();
app.MapControllers();

app.UseHealthChecks("/health");


app.Run();
