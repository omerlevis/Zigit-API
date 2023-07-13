using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

//generate and valiadte tokens using JWT packges

namespace ZigitApi.Utils
{
    public class Authentication
    {

public static string GenerateJwtToken(string userId, string userName, string secretKey)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(secretKey); // Convert your secret key to bytes

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
            new Claim("userId", userId),
            new Claim("userName", userName)
            
        }),
            Expires = DateTime.UtcNow.AddDays(7), 
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        var jwtToken = tokenHandler.WriteToken(token);

        return jwtToken;
    }
        public static bool ValidateJwtToken(string token)
        {
            if (token == null)
                return false;

            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                var configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();

                var secretKey = configuration["JwtSettings:SecretKey"];

                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey)),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var userId = jwtToken.Claims.FirstOrDefault(x => x.Type == "userId")?.Value;

                if (userId != null)
                {
                    // Token validation successful
                    return true;
                }
                // UserId claim not found in the token
                return false;
            }
            catch
            {
                // Token validation failed
                return false;
            }
        }
    }

   
}


