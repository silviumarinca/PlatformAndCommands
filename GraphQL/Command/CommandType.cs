using CommanderGQL.Data;
using CommanderGQL.Models;
using HotChocolate;
using HotChocolate.Types;
using System.Linq;

namespace CommanderGQL.GraphQL.Command
{
    public class CommandType:ObjectType<CommanderGQL.Models.Command>
    {
    protected override void Configure(IObjectTypeDescriptor<CommanderGQL.Models.Command> descriptor)
     {
        descriptor.Description("Represents any executable command");
        descriptor.Field(c=>c.Platform)
        .ResolveWith<Resolvers>(c => c.GetPlatform(default!, default!))
        .UseDbContext<AppDbContext>()
        .Description("This is the platform which the command belongs");
     
     }

      private class Resolvers
      {

          public Platform GetPlatform(CommanderGQL.Models.Command command, [ScopedService]AppDbContext context)
          {
              return  context.Platforms.FirstOrDefault(p => p.Id == command.PlatformId);
          }
      }
    }
   
}