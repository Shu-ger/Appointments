namespace QueueManagementSystem1.Commands.Validators
{
    using FluentValidation;
    using System;

    public class CreateAppointmentValidator : AbstractValidator<CreateAppointmentCommand>
    {
        public CreateAppointmentValidator()
        {
            RuleFor(x => x.CustomerName)
                .NotEmpty().WithMessage("Customer name is required")
                .MaximumLength(50).WithMessage("Customer name cannot exceed 50 characters");

            RuleFor(x => x.Date)
                .GreaterThan(DateTime.UtcNow).WithMessage("Appointment date must be in the future");
        }
    }
}
