
using AI.Notebook.API.Routers;
using AI.Notebook.DataAccess.DBAccess;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

// Add Logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// Add services to the container.
builder.Services.AddAuthorization();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ISqlDataAccess, SqlDataAccess>();
builder.Services.AddScoped<IRequestData, RequestData>();
builder.Services.AddScoped<IResultData, ResultData>();
builder.Services.AddScoped<IAIResourceData, AIResourceData>();
builder.Services.AddScoped<IResultTypeData, ResultTypeData>();
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

// Add "Router" classes as a service
builder.Services.AddScoped<RouterBase, AIResourceRouter>();
builder.Services.AddScoped<RouterBase, ResultTypeRouter>();
builder.Services.AddScoped<RouterBase, RequestRouter>();
builder.Services.AddScoped<RouterBase, ServicesRouter>();
builder.Services.AddScoped<RouterBase, ResultRouter>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

//*************************************
// Add Routes from all "Router Classes"
//*************************************
using (var scope = app.Services.CreateScope())
{
	// Build collection of all RouterBase classes
	var services = scope.ServiceProvider.GetServices<RouterBase>();

	// Loop through each RouterBase class
	foreach (var item in services)
	{
		// Invoke the AddRoutes() method to add the routes
		item.AddRoutes(app);
	}

	// Make sure this is called within the application scope
	app.Run();
}
