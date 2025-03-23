using RepairManagementSystem.Models.DTOs;
using RepairManagementSystem.Services.Interfaces;

namespace RepairManagementSystem.Services
{
    public class UserService : IUserService
    {
        /*TODO: add dbcontext here*/

        public async Task<UserDTO?> GetUserAsync(string email, string password)
        {
            /*TODO: implement this method*/
            /*if user exists return it */
            /*else return null*/
            /* mock functionality for now*/
            if (email == "email@tab.com" && password == "password")
            {
                return new UserDTO
                {
                    Email = email,
                    UserName = "username",
                    Role = "role"
                };
            }
            else
            {
                return null;
            }
        }

    }
}
