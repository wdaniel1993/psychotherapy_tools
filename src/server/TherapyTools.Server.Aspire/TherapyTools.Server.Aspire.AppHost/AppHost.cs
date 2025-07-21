var builder = DistributedApplication.CreateBuilder(args);

var graphQL = builder.AddProject<Projects.TherapyTools_Server_GraphQL>("graphql");

builder.Build().Run();
