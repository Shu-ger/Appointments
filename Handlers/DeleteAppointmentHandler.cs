using QueueManagementSystem1.Commands;  
using MongoDB.Driver;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace QueueManagementSystem1.Handlers
{
    public class DeleteAppointmentHandler : IRequestHandler<DeleteAppointmentCommand, string>
    {
        private readonly IMongoCollection<Appointment> _appointments;

        public DeleteAppointmentHandler(IMongoDatabase database)
        {
            _appointments = database.GetCollection<Appointment>("Appointments");
        }

        public async Task<string> Handle(DeleteAppointmentCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.Id))
            {
                // Delete all appointments
                var result = await _appointments.DeleteManyAsync(_ => true, cancellationToken);
                return result.DeletedCount > 0 ? "All appointments deleted successfully." : "No appointments found to delete.";
            }
            else
            {
                // Check if the appointment exists
                var existingAppointment = await _appointments.Find(a => a.Id == request.Id).FirstOrDefaultAsync(cancellationToken);
                if (existingAppointment == null)
                {
                    return "Appointment not found.";
                }

                // Delete the specific appointment
                var result = await _appointments.DeleteOneAsync(a => a.Id == request.Id, cancellationToken);
                return result.DeletedCount > 0 ? "Appointment deleted successfully." : "Failed to delete appointment.";
            }
        }
    }
}
