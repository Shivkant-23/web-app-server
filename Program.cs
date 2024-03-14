using Hangfire;
using Hangfire.PostgreSql;
using Hangfire.Dashboard;
using web_server;
using web_server.Utils.Common.Assets.Strings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//common db configuration
builder.Services.AddHangfire(configuration => configuration
       .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
       .UseSimpleAssemblyNameTypeSerializer()
       .UseRecommendedSerializerSettings()
       .UsePostgreSqlStorage(builder.Configuration.GetConnectionString("HangfireConnection")
       ));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}




var options = new DashboardOptions
{
    Authorization = new IDashboardAuthorizationFilter[] { }
};

app.UseHangfireDashboard("/hangfire", options);


// enqueuing background and recurring jobs
BackgroundJob.Enqueue(() => Console.WriteLine("This is the method....")); //will run once
RecurringJob.AddOrUpdate<ProBackground>( "sample-job-id_1", job => job.ClassInfoFunction(), "* * * * *"); //will run every minute
RecurringJob.AddOrUpdate<ErrorString>( "sample-job-id_2", job => job.NoInternetError(), "* * * * *"); //will run every minute



app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();