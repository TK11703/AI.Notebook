using Dapper;
using System.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;

namespace AI.Notebook.DataAccess.DBAccess;
public class SqlDataAccess : ISqlDataAccess
{
	private readonly IConfiguration _configuration;

	public SqlDataAccess(IConfiguration configuration)
	{
		_configuration = configuration;
	}

	public async Task<IEnumerable<T>> LoadDataAsync<T, U>(string storedProcedure, U parameters, string connectionId = "Default")
	{
		using IDbConnection connection = new SqlConnection(_configuration.GetConnectionString(connectionId));

		return await connection.QueryAsync<T>(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
	}

	public async Task SaveDataAsync<T>(string storedProcedure, T parameters, string connectionId = "Default")
	{
		using IDbConnection connection = new SqlConnection(_configuration.GetConnectionString(connectionId));

		await connection.ExecuteAsync(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
	}

	public int SaveData<T>(string storedProcedure, T parameters, string connectionId = "Default")
	{
		using IDbConnection connection = new SqlConnection(_configuration.GetConnectionString(connectionId));

		return connection.Execute(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
	}
}
