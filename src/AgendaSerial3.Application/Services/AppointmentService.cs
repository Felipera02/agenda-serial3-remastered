using AgendaSerial3.Application.DTOs;
using AgendaSerial3.Domain.Entities;
using AgendaSerial3.Infrastructure.Data.Repository;

namespace AgendaSerial3.Application.Services;

public class AppointmentService(AppointmentRepository appointmentRepository, CategoryRepository categoryRepository)
{
    private readonly AppointmentRepository _appointmentRepository = appointmentRepository;
    private readonly CategoryRepository _categoryRepository = categoryRepository;

    public async Task<IEnumerable<AppointmentDto>> GetUserAppointmentsAsync(string userId, DateTime startDate, DateTime endDate)
    {
        var appointments = await _appointmentRepository.GetAppointmentsAndCategoriesByUserIdAndDatesAsync(userId, startDate, endDate);

        return appointments.Select(a => new AppointmentDto
        {
            Id = a.Id,
            Title = a.Title,
            Description = a.Description,
            StartDateTime = a.StartDateTime,
            EndDateTime = a.EndDateTime,
            CategoryId = a.CategoryId,
            CategoryName = a.Category.Name,
            CategoryColor = a.Category.Color,
            IsCompleted = a.IsCompleted
        });
    }

    public async Task<AppointmentDto> CreateAppointmentAsync(AppointmentDto appointmentDto, string userId)
    {
        // Verificar se a categoria pertence ao usuário
        var categoryExists = await _categoryRepository
            .ExistsAsync(c => c.Id == appointmentDto.CategoryId && c.UserId == userId);

        if (!categoryExists)
            throw new UnauthorizedAccessException("Categoria não encontrada ou não pertence ao usuário");

        var appointment = new Appointment
        {
            Title = appointmentDto.Title,
            Description = appointmentDto.Description,
            StartDateTime = appointmentDto.StartDateTime,
            EndDateTime = appointmentDto.EndDateTime,
            CategoryId = appointmentDto.CategoryId,
            UserId = userId,
            IsCompleted = appointmentDto.IsCompleted
        };

        var createdAppointment = await _appointmentRepository.AddAsync(appointment);

        var category = await _categoryRepository.GetByIdAsync(appointmentDto.CategoryId);

        return new AppointmentDto
        {
            Id = createdAppointment.Id,
            Title = createdAppointment.Title,
            Description = createdAppointment.Description,
            StartDateTime = createdAppointment.StartDateTime,
            EndDateTime = createdAppointment.EndDateTime,
            CategoryId = createdAppointment.CategoryId,
            CategoryName = category?.Name ?? "",
            CategoryColor = category?.Color ?? "",
            IsCompleted = createdAppointment.IsCompleted
        };
    }

    public async Task<AppointmentDto> UpdateAppointmentAsync(AppointmentDto appointmentDto, string userId)
    {
        var appointment = await _appointmentRepository
            .GetAppointmentAndCategoriesAsync(appointmentDto.Id, userId);

        if (appointment == null)
            throw new UnauthorizedAccessException("Compromisso não encontrado ou não pertence ao usuário");

        // Verificar se a nova categoria pertence ao usuário
        var categoryExists = await _categoryRepository
            .ExistsAsync(c => c.Id == appointmentDto.CategoryId && c.UserId == userId);

        if (!categoryExists)
            throw new UnauthorizedAccessException("Categoria não encontrada ou não pertence ao usuário");

        appointment.Title = appointmentDto.Title;
        appointment.Description = appointmentDto.Description;
        appointment.StartDateTime = appointmentDto.StartDateTime;
        appointment.EndDateTime = appointmentDto.EndDateTime;
        appointment.CategoryId = appointmentDto.CategoryId;
        appointment.IsCompleted = appointmentDto.IsCompleted;
        appointment.UpdatedAt = DateTime.UtcNow;

        var updatedAppointment = await _appointmentRepository.UpdateAsync(appointment);

        return new AppointmentDto
        {
            Id = updatedAppointment.Id,
            Title = updatedAppointment.Title,
            Description = updatedAppointment.Description,
            StartDateTime = updatedAppointment.StartDateTime,
            EndDateTime = updatedAppointment.EndDateTime,
            CategoryId = updatedAppointment.CategoryId,
            CategoryName = updatedAppointment.Category.Name,
            CategoryColor = updatedAppointment.Category.Color,
            IsCompleted = updatedAppointment.IsCompleted
        };
    }

    public async Task DeleteAppointmentAsync(int appointmentId, string userId)
    {
        var appointment = await _appointmentRepository
            .GetByExpression(a => a.Id == appointmentId && a.UserId == userId);

        if (appointment == null)
            throw new UnauthorizedAccessException("Compromisso não encontrado ou não pertence ao usuário");

        await _appointmentRepository.DeleteAsync(appointment);
    }
}
