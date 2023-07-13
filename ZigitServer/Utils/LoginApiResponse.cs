using ZigitApi.Entities;
using System;
using System.Text.Json;

    //Crearte json respnse for client side in order to manipulate formatted response 

namespace ZigitApi.Utils
{
    public class LoginApiResponse
    {

        public static string GenerateApiResponse(string token, User user)
        {
            var personalDetails = new
            {
                user_id=user.Id,
                name = user.Name,
                Team = user.Team,
                joined_at = user.joined,
                avatar = user.Avatar
            };

            var response = new
            {
                token = token,
                personalDetails = personalDetails
            };

            return JsonSerializer.Serialize(response);
        }
    }
}
