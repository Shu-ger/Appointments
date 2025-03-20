namespace QueueManagementSystem1.Handlers
{
    using MongoDB.Driver;
    using MediatR;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;
    using QueueManagementSystem1.Queries;

    public class GetAppointmentsHandler : IRequestHandler<GetAppointmentsQuery, List<Appointment>>
    {
        private readonly IMongoCollection<Appointment> _appointments;

        public GetAppointmentsHandler(IMongoDatabase database)
        {
            _appointments = database.GetCollection<Appointment>("Appointments");
        }

        public async Task<List<Appointment>> Handle(GetAppointmentsQuery request, CancellationToken cancellationToken)
        {
            return await _appointments.Find(_ => true).ToListAsync(cancellationToken);
        }
    }
}
