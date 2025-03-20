using MediatR;
using static QueueManagementSystem1.StatusEnum;

namespace QueueManagementSystem1.Commands
{
    public class UpdateAppointmentStatusCommand : IRequest<string>
    {
        public List<string> Ids { get; set; } = new();
        public AppointmentStatus Status { get; set; }
    }
}
