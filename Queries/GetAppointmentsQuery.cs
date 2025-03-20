namespace QueueManagementSystem1.Queries
{
    using MediatR;
    using System.Collections.Generic;

    public class GetAppointmentsQuery : IRequest<List<Appointment>> { }
}
