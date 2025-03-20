using MediatR;

namespace QueueManagementSystem1.Commands
{

    public class CancelAppointmentCommand : IRequest<string>
    {
        public string Id { get; set; } = null!;
    }
}
