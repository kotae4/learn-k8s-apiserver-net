using learn_k8s_apiserver_net.BusinessLogic;
using learn_k8s_apiserver_net.Database;
using System.Reflection;
using System.Text.Json.Serialization;

namespace learn_k8s_apiserver_net
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Configuration.AddEnvironmentVariables();

            // Add services to the container.
            builder.Services.ConfigureDatabase(builder.Configuration, builder.Environment.IsDevelopment());
            builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
            builder.Services.AddDbLogic();
            builder.Services.RegisterBusinessLogic();

            builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    // TODO see if this has a performance hit (ignorecycles vs marking fields with [JsonIgnore])
                    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                });
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddHealthChecks();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            //app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapHealthChecks("/healthz");
            app.MapControllers();

            app.CreateDatabase();

            app.Run();
        }
    }
}
