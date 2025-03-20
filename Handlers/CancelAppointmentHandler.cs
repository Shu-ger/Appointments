
using MongoDB.Driver;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using QueueManagementSystem1.Commands;
using static QueueManagementSystem1.StatusEnum;

namespace QueueManagementSystem1.Handlers
{
    public class CancelAppointmentHandler : IRequestHandler<CancelAppointmentCommand, string>
    {
        private readonly IMongoCollection<Appointment> _appointments;

        public CancelAppointmentHandler(IMongoDatabase database)
        {
            _appointments = database.GetCollection<Appointment>("Appointments");
        }

        public async Task<string> Handle(CancelAppointmentCommand request, CancellationToken cancellationToken)
        {
            var filter = Builders<Appointment>.Filter.Eq(a => a.Id, request.Id);
            var update = Builders<Appointment>.Update.Set(a => a.Status, AppointmentStatus.Cancelled);

            var result = await _appointments.UpdateOneAsync(filter, update, cancellationToken: cancellationToken);

            if (result.MatchedCount == 0)
                return "Appointment not found.";

            return result.ModifiedCount > 0 ? "Appointment canceled successfully." : "No changes made.";
        }
    }
}
