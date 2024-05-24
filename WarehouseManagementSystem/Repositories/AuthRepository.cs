using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using WarehouseManagementSystem.DataBase;

namespace WarehouseManagementSystem.Repositories;

public class AuthRepository(WmsDbContext wmsDbContext, IConfiguration configuration) : IAuthRepository
{
    private static readonly TimeSpan TokeLifetime = TimeSpan.FromHours(8);
    public string? RegisterNewUser(string email, string userName, string password)
    {
        //fix salt
        var newUser = new Models.User()
        {
            UserName = userName,
            Email = email,
            Password = password,
            Salt = "test"
        };

        var userFormDb = wmsDbContext.Users.FirstOrDefault(u => u.Email == email);
        if (userFormDb != null)
            return null;

        wmsDbContext.Users.Add(newUser);
        wmsDbContext.SaveChanges();
        
        var jwt = GenerateToken(email);
        return jwt;
    }
    
    private string GenerateToken(string email)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(configuration["JwtSettings:Key"]!);

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Email, email)
        };
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.Add(TokeLifetime),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var jwt = tokenHandler.WriteToken(token);
        return jwt;
    }
}