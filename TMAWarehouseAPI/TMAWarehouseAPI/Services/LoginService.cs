using Microsoft.EntityFrameworkCore;
using TMAWarehouseAPI.Data;
using TMAWarehouseAPI.Exceptions;
using TMAWarehouseAPI.Models;

namespace TMAWarehouseAPI.Services
{
    public class LoginService
    {
        private readonly DatabaseContext _dbContext;
        

        public LoginService(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<User> GetUser(string login, string password)
        {
            User user;
            try
            {
                user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Username == login);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            if (user != null)
            {
                if (BCrypt.Net.BCrypt.Verify(password, user.Password))
                {
                    return user;
                }
                throw new UserNotFoundException("Password incorrect");
            }
            else
            {
                throw new UserNotFoundException("Username incorrect");
            }                     
        }

        // METHOD BELOVE SHOULD BE USED ONLY ONCE TO CREATE INITIAL USERS WITH HASHED PASSWORDS!
        // I LEFT THAT METHOD ONLY BECAUSE OF TEST TASK SOLUTION
        // SO IF SOMEONE WOULD LIKE TO TEST IT THEN WILL BE EASIER TO INIT USERS WITH THAT
        // FOR INIT USERS IN LOGIN PANEL YOU SHOULD PUT THESE INFORMATIONS:
        // Username: inituserstmawarehouse
        // Password: initusershashedpasswords
        // You can read more in README file
        private User[] FillUsersInformations()
        {
            string[] usernames = { "employee", "coordinator", "administrator" };
            string[] passwords = { "emp123", "cor123", "adm123" };
            string[] roles = { "EMP", "CO", "ADM" };
            User[] users = new User[3];
            for(int i =0; i < users.Length; i++)
            {
                users[i] = new User
                {
                    Username = usernames[i],
                    Password = BCrypt.Net.BCrypt.HashPassword(passwords[i]),
                    Role = roles[i]
                };
            }
            return users;
        }

        public async Task<bool> AddUsers()
        {
            try
            {
                User[] users = FillUsersInformations();
                foreach (var user in users)
                {
                    await _dbContext.AddAsync(user);

                }
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
    }
}
