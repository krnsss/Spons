using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace egorDipl.Data.Models
{
    public class UserProvider : IUserProvider
    {
        public required string Name { get; set; }

        public required StaffRole Role { get; set; }

        public void ParseJwt(string token)
        {
            var handler = new JwtSecurityTokenHandler();

            if (handler.ReadToken(token) is JwtSecurityToken jsonToken)
            {
                var claims = jsonToken.Claims;

                var nameClaim = claims.FirstOrDefault(c => c.Type == ClaimTypes.Name);

                if (nameClaim != null)
                    Name = nameClaim.Value;
                var roleClaim = claims.FirstOrDefault(c => c.Type == "Role");

                if (roleClaim != null)
                    Role = new StaffRole() { Name = roleClaim.Value };
            }
        }
    }

    public interface IUserProvider
    {
        StaffRole Role { get; set; }

        string Name { get; set; }

        public void ParseJwt(string token);
    }
}
