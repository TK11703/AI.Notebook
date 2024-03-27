
namespace AI.Notebook.DataAccess.DBAccess;

public interface ISqlDataAccess
{
	Task<IEnumerable<T>> LoadDataAsync<T, U>(string storedProcedure, U parameters, string connectionId = "Default");
	Task SaveDataAsync<T>(string storedProcedure, T parameters, string connectionId = "Default");
	int SaveData<T>(string storedProcedure, T parameters, string connectionId = "Default");
}