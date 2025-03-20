
using MongoDB.Driver;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using QueueManagementSystem1.Commands;

namespace QueueManagementSystem1.Handlers
{
    public class UpdateAppointmentStatusHandler : IRequestHandler<UpdateAppointmentStatusCommand, string>
    {
        private readonly IMongoCollection<Appointment> _appointments;

        public UpdateAppointmentStatusHandler(IMongoDatabase database)
        {
            _appointments = database.GetCollection<Appointment>("Appointments");
        }

        public async Task<string> Handle(UpdateAppointmentStatusCommand request, CancellationToken cancellationToken)
        {
            if (request.Ids == null || request.Ids.Count == 0)
                return "No appointment IDs provided.";

            var filter = Builders<Appointment>.Filter.In(a => a.Id, request.Ids);
            var update = Builders<Appointment>.Update.Set(a => a.Status, request.Status);

            var result = await _appointments.UpdateManyAsync(filter, update, cancellationToken: cancellationToken);

            if (result.MatchedCount == 0)
                return "No appointments found for the given IDs.";

            return $"Updated {result.ModifiedCount} appointments to status '{request.Status}'.";
        }
    }
}
