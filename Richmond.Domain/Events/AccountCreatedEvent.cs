using Amazon.SimpleNotificationService;
using Amazon.SimpleNotificationService.Model;
using MediatR;
using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Richmond.Domain.Events
{
    public class AccountCreatedEvent : INotification
    {
        public AccountCreatedEvent(DateTime createdDate, string message)
        {
            CreatedDate = createdDate;
            Message = message;
        }
        public DateTime CreatedDate { get; private set; }
        public string Message { get; private set; }
    }

    //This is a plus, it usually when raise a event, it send to a queue
    public class AccountCreatedEventHandler : INotificationHandler<AccountCreatedEvent>
    {
        //this Event should send this message into a SNS Lambda QUEUE
        public async Task Handle(AccountCreatedEvent notification, CancellationToken cancellationToken)
        {
            try
            {
                var snsClient = new AmazonSimpleNotificationServiceClient(Amazon.RegionEndpoint.SAEast1);
                var request = new PublishRequest
                {
                    TopicArn = "Your ARN NUMBER",
                    Message = JsonSerializer.Serialize(notification.Message),
                    Subject = $"Richmond Test Execution."
                };
                await snsClient.PublishAsync(request);
            }
            catch {}
        }
    }
}