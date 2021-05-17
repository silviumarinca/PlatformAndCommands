using CommanderGQL.Data;
using CommanderGQL.GraphQL.Command;
using CommanderGQL.GraphQL.Platforms;
using CommanderGQL.Models;
using HotChocolate;
using HotChocolate.Data;
using HotChocolate.Subscriptions;
using System.Threading;
using System.Threading.Tasks;

namespace CommanderGQL.GraphQL
{
    public class Mutation
    {
        [UseDbContext(typeof(AppDbContext))]
        public async Task<AddPlatformPayload> AddPlatformAsync(
            AddPlatformInput input, 
            [ScopedService]AppDbContext context,
            [Service]ITopicEventSender eventSender,
            CancellationToken token)
        {
            var platform = new Platform{
                    Name = input.name  
            };
            context.Platforms.Add(platform);
            await context.SaveChangesAsync();
            await eventSender.SendAsync(nameof(Subscription.OnPlatformAdded),platform, token);

            return new AddPlatformPayload(platform);
            
        }

        [UseDbContext(typeof(AppDbContext))]
        public async Task<AddCommandPayload> AddCommandAsync(
            AddCommandInput input, 
            [ScopedService] AppDbContext context,
            [Service]ITopicEventSender eventSender,
            CancellationToken token)
        {
            var command = new CommanderGQL.Models.Command{
                HowTo = input.HowTo,
                CommandLine = input.CommandLine,
                PlatformId = input.PlatformId
            };

            context.Commands.Add(command);
            await context.SaveChangesAsync();
         

            return new AddCommandPayload(command);
        }
    }
}