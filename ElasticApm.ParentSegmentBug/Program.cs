using ElasticApm.ParentSegmentBug.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAllElasticApm();
builder.Services.AddSingleton<FirstService>();
builder.Services.AddSingleton<SecondService>();
builder.Services.AddSingleton<ThridService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();
