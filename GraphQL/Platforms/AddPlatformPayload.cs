using CommanderGQL.Data;
using CommanderGQL.Models;
using HotChocolate;

namespace CommanderGQL.GraphQL.Platforms
{
    public record AddPlatformPayload(Platform platform);
}