using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using System.Threading.Tasks;
using QueueManagementSystem1.Commands;
using QueueManagementSystem1.Queries;
using FluentValidation;

namespace QueueManagementSystem1.Controllers
{


    [ApiController]
    [Route("api/appointments")]
    public class AppointmentController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IValidator<CreateAppointmentCommand> _validator;

        public AppointmentController(IMediator mediator, IValidator<CreateAppointmentCommand> validator)
        {
            _mediator = mediator;
            _validator = validator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateAppointmentCommand command)
        {
            var validationResult = await _validator.ValidateAsync(command);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
            }

            var id = await _mediator.Send(command);
            return Ok(new { Id = id });
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var appointments = await _mediator.Send(new GetAppointmentsQuery());
            return Ok(appointments);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] string? id)
        {
            var resultMessage = await _mediator.Send(new DeleteAppointmentCommand { Id = id });

            if (resultMessage == "Appointment not found.")
                return NotFound(new { Message = resultMessage });

            return Ok(new { Message = resultMessage });
        }
        [HttpPut("status")]
        public async Task<IActionResult> UpdateStatuses([FromBody] UpdateAppointmentStatusCommand command)
        {
            var resultMessage = await _mediator.Send(command);

            if (resultMessage == "No appointment IDs provided." || resultMessage == "No appointments found for the given IDs.")
                return NotFound(new { Message = resultMessage });

            return Ok(new { Message = resultMessage });
        }
        [HttpPut("cancel")]
        public async Task<IActionResult> CancelAppointment([FromBody] CancelAppointmentCommand command)
        {
            var resultMessage = await _mediator.Send(command);

            if (resultMessage == "Appointment not found.")
                return NotFound(new { Message = resultMessage });

            return Ok(new { Message = resultMessage });
        }
    }
}
