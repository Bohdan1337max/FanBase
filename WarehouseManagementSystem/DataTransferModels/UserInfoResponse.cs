namespace WarehouseManagementSystem.DataTransferModels;

public class UserInfoResponse
{
    public int Id { get; set; }
    public string? UserName { get; set; }
    public string? Email { get; set; }
    public string Bio { get; set; }
    public string ProfileImageUrl { get; set; }
}