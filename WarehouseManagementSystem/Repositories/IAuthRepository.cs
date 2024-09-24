using WarehouseManagementSystem.Models;

namespace WarehouseManagementSystem.Repositories;

public interface IAuthRepository
{
    public string? RegisterNewUser(string email, string userName, string password);
    public string? LogIn(string email, string password);
    public List<Role> AssignUserRole(int userId, string role);
}