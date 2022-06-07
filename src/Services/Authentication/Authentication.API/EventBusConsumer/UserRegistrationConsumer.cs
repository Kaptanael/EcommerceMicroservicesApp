using Authentication.Application.Features.Users.Commands.Registration;
using AutoMapper;
using EventBus.Messages.Events;
using MassTransit;
using MediatR;

namespace Authentication.API.EventBusConsumer
{
    public class UserRegistrationConsumer: IConsumer<UserRegistrationEvent>
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly ILogger<UserRegistrationConsumer> _logger;

        public UserRegistrationConsumer(IMediator mediator, IMapper mapper, ILogger<UserRegistrationConsumer> logger)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task Consume(ConsumeContext<UserRegistrationEvent> context)
        {
            var command = _mapper.Map<UserRegistrationCommand>(context.Message);
            var result = await _mediator.Send(command);

            _logger.LogInformation("UserRegistrationEvent consumed successfully.", result);
        }
    }
}
