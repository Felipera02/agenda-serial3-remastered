using Microsoft.Extensions.DependencyInjection;
using AgendaSerial3.Application.UseCases.Users;
using AgendaSerial3.Application.Interfaces.Repositories;
using AgendaSerial3.Infrastructure.Repositories;
using AutoMapper;
using AgendaSerial3.Application.UseCases.PersonalCalendars;
using AgendaSerial3.Application.UseCases.Appointments;

namespace AgendaSerial3.Infrastructure.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddUseCases(this IServiceCollection services)
        {
            // Users
            services.AddScoped<CreateUser>();
            services.AddScoped<GetUserById>();
            services.AddScoped<GetUserByUsername>();
            services.AddScoped<DeleteUser>();
            services.AddScoped<UpdateUser>();
            services.AddScoped<GetAllUsers>();

            // Personal Calendars
            services.AddScoped<GetCalendarById>();
            services.AddScoped<GetCalendarByUser>();
            services.AddScoped<CreateCalendar>();
            services.AddScoped<UpdateCalendar>();
            services.AddScoped<RemoveCalendar>();

            // Appointments
            services.AddScoped<CreateAppointment>();
            services.AddScoped<GetAppointmentById>();
            services.AddScoped<GetAppointmentByCalendar>();
            services.AddScoped<GetAppointmentByUser>();
            services.AddScoped<UpdateAppointment>();
            services.AddScoped<RemoveAppointment>();

            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ICalendarRepository, CalendarRepository>();
            services.AddScoped<IAppointmentRepository, AppointmentRepository>();
            return services;
        }
    }
}
