using Dapper;
using Microsoft.Extensions.Options;
using SuperTutor.SharedLibraries.BuildingBlocks.Application.Repositories;
using SuperTutor.SharedLibraries.BuildingBlocks.Infrastructure.Options;
using System.Data.SqlClient;

namespace SuperTutor.SharedLibraries.BuildingBlocks.Infrastructure.Repositories;

internal class QueryRepository : IQueryRepository
{
    private readonly IOptionsSnapshot<DatabaseOptions> options;

    public QueryRepository(IOptionsSnapshot<DatabaseOptions> options) => this.options = options;

    public async Task<IEnumerable<TPayload>> GetAll<TPayload>(string query, object? parameters, CancellationToken cancellationToken)
    {
        using var sqlConnection = new SqlConnection(options.Value.ConnectionString);

        return await sqlConnection.QueryAsync<TPayload>(new CommandDefinition(query, parameters, cancellationToken: cancellationToken));
    }
}
