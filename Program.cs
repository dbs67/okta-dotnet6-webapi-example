var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<Okta_ClientFlowDotNetSix.Okta.OktaJwtVerificationOptions>(
    builder.Configuration.GetSection("Okta"));

builder.Services.AddTransient<Okta_ClientFlowDotNetSix.Okta.IJwtValidator, Okta_ClientFlowDotNetSix.Okta.OktaJwtValidation>();

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