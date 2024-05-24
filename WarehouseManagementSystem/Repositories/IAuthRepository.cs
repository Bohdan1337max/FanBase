namespace WarehouseManagementSystem.Repositories;

public interface IAuthRepository
{
    public string? RegisterNewUser(string email, string userName, string password);
}