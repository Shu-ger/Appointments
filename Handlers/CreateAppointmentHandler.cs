
    using MongoDB.Driver;
    using MediatR;
    using System.Threading;
    using System.Threading.Tasks;
    using QueueManagementSystem1.Commands;
    using static QueueManagementSystem1.StatusEnum;
namespace QueueManagementSystem1.Handlers
{
    public class CreateAppointmentHandler(IMongoDatabase database) : IRequestHandler<CreateAppointmentCommand, string>
    {
        private readonly IMongoCollection<Appointment> _appointments = database.GetCollection<Appointment>("Appointments");

        public async Task<string> Handle(CreateAppointmentCommand request, CancellationToken cancellationToken)
        {
            var appointment = new Appointment
            {
                CustomerName = request.CustomerName,
                Date = request.Date,
                Status = AppointmentStatus.Scheduled
            };

            await _appointments.InsertOneAsync(appointment, cancellationToken);
            return appointment.Id ?? string.Empty;
        }
    }
}
