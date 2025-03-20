namespace QueueManagementSystem1.Commands
{
    using MediatR;

    public class DeleteAppointmentCommand : IRequest<string>
    {
        public string? Id { get; set; }
    }
}
