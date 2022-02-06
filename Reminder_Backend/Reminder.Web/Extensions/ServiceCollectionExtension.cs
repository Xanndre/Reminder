using AutoMapper;
using Hangfire;
using Hangfire.MemoryStorage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Reminder.Core.Config;
using Reminder.Core.Interfaces.Repositories;
using Reminder.Core.Interfaces.Services;
using Reminder.Core.Mappings;
using Reminder.Core.Services;
using Reminder.Infrastructure.DataAccess.Repositories;

namespace Reminder.Web.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void AddMapper(this IServiceCollection services)
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            var mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
        }

        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<ITodoService, TodoService>();
            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<IMailService, MailService>();
            services.AddScoped<IWeatherService, WeatherService>();
        }

        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<ITodoRepository, TodoRepository>();
            services.AddScoped<INotificationRepository, NotificationRepository>();
        }

        public static void AddHangfire(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHangfire(x =>
                x.SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseDefaultTypeSerializer()
                .UseMemoryStorage());

            services.AddHangfireServer();
        }

        public static void ConfigureOptions(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MailOptions>(configuration.GetSection("Mail"));
            services.Configure<OpenWeatherOptions>(configuration.GetSection("OpenWeather"));
        }
    }
}
