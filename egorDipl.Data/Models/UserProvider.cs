using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace egorDipl.Data.Models
{
    public class UserProvider : IUserProvider
    {
        public required string Name { get; set; }

        public required int UserId { get; set; }

        public required int CompanyId { get; set; }

        public required StaffRole Role { get; set; }

        public void ParseJwt(string token)
        {
            var handler = new JwtSecurityTokenHandler();

            if (handler.ReadToken(token) is JwtSecurityToken jsonToken)
            {
                var claims = jsonToken.Claims;

                var nameClaim = claims.FirstOrDefault(c => c.Type == "UniCode");

                if (nameClaim != null)
                    Name = nameClaim.Value;
                
                var roleClaim = claims.FirstOrDefault(c => c.Type == "Role");

                if (roleClaim != null)
                    Role = new StaffRole() { Name = roleClaim.Value };

                var companyIdClaim = claims.FirstOrDefault(c => c.Type == "CompanyId");

                if (companyIdClaim != null)
                    CompanyId = Convert.ToInt32(companyIdClaim.Value);

                var userIdClaim = claims.FirstOrDefault(c => c.Type == "UserId");

                if (userIdClaim != null)
                    UserId = Convert.ToInt32(userIdClaim.Value);
            }
        }
    }

    public interface IUserProvider
    {
        StaffRole Role { get; set; }

        string Name { get; set; }

        int CompanyId { get; set; }

        int UserId { get; set; }

        public void ParseJwt(string token);
    }
}
