using CommanderGQL.Data;
using CommanderGQL.Models;
using HotChocolate;
using HotChocolate.Types;
using System.Linq;

namespace CommanderGQL.GraphQL.Platforms
{
   public class PlatformType:ObjectType<Platform>
   {
    protected override void Configure(IObjectTypeDescriptor<Platform> descriptor)
    {
      descriptor.Description("Represents any software or service that has a command line interface.");
      descriptor.Field(c => c.LicenseKey).Ignore();

      descriptor
        .Field(p=>p.Commands)
        .ResolveWith<Resolvers>(p => p.GetCommands(default!, default!))
        .UseDbContext<AppDbContext>()
        .Description("This is the list of available commands of this platforms");
    }

    private class Resolvers{
      public IQueryable<CommanderGQL.Models.Command> GetCommands(Platform platform, [ScopedService] AppDbContext context){
          return context.Commands.Where(c => c.PlatformId == platform.Id);
      }
    }

   }

}