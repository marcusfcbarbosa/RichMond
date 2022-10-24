using MediatR;
using Microsoft.AspNetCore.Mvc;
using Richmond.Domain.Commands;
using System.Threading.Tasks;

namespace RichMond.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        [HttpPost]
        [Route("")]
        public async Task<ICommandResult> Post([FromServices] IMediator mediator,
                                                 [FromBody] AccountCommand command)
        {
            command.Validate();
            if (command.Valid)
                return await mediator.Send(command);
            return new CommandResult(false, "Errors", command.Notifications);
        }
    }
}
