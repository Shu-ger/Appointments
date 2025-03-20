namespace QueueManagementSystem1.Commands
{
    using MediatR;

    public class CreateAppointmentCommand : IRequest<string>
    {
        public string? CustomerName { get; set; }
        public DateTime? Date { get; set; }
    }
}
