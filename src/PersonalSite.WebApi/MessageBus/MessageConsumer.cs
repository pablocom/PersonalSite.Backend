using System;
using System.Threading.Tasks;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using PersonalSite.WebApi.Commands;

namespace PersonalSite.WebApi.MessageBus
{
    public class MessageConsumer : IConsumer<Message>
    {
        private readonly ILogger<MessageConsumer> _logger;
        private readonly IMediator _mediator;

        public MessageConsumer(ILogger<MessageConsumer> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        public Task Consume(ConsumeContext<Message> context)
        {
            _logger.LogInformation("Received Text: {Text}", context.Message.Text);

            var company = "Ryanair";
            var description = "Software Developer";
            var jobPeriodStart = new DateOnly(2020, 1, 1);
            var jobPeriodEnd = new DateOnly(2020, 5, 1);
            var techStack = new[] { ".Net Core", "NSubstitute" };
            var createJobExperienceCommand =
                new CreateJobExperienceCommand(company, description, jobPeriodStart, jobPeriodEnd, techStack);

            _mediator.Send(createJobExperienceCommand);

            return Task.CompletedTask;
        }
    }
}