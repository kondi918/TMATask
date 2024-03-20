using Microsoft.EntityFrameworkCore;
using TMAWarehouseAPI.Data;
using TMAWarehouseAPI.Exceptions;
using TMAWarehouseAPI.Models;
using TMAWarehouseAPI.Models.DTO;

namespace TMAWarehouseAPI.Services
{
    public class UsersService
    {
        private readonly DatabaseContext _dbContext;


        public UsersService(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
        }

        private bool CheckRole(string role)
        {
            if (role == "ADM" || role == "EMP" || role=="CO")
            {
                return true;
            }
            return false;
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
        private User GetUser(AddingUserDTO userToAdd)
        {
            User user = new User
            {
                Username = userToAdd.Username,
                Password = BCrypt.Net.BCrypt.HashPassword(userToAdd.Password),
                Role = userToAdd.Role
            };
            return user;
        }

        public async Task<bool> AddDefaultUsers()
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

        public async Task<List<UserAdminDTO>> GetAllUsers()
        {
            try
            {
                List<UserAdminDTO> userResponse = new List<UserAdminDTO>();
                var users = await _dbContext.Users.ToListAsync();
                foreach (var user in users)
                {
                    UserAdminDTO userAdmin = new UserAdminDTO
                    {
                        Username = user.Username,
                        ID = user.ID,
                        Role = user.Role
                    };
                    userResponse.Add(userAdmin);
                }
                return userResponse;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occured while trying to get users");
            }
        }

        public async Task<bool> AddUser(AddingUserDTO userToAdd)
        {
            if (!CheckRole(userToAdd.Role))
            {
                throw new Exception("User role is not correct");
            }
            try
            {
                User user = GetUser(userToAdd);
                await _dbContext.AddAsync(user);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch(Exception ex)
            {
                throw new Exception("Error occured durning adding user to database");
            }
        }
        public async Task<bool> UpdateUser(UpdatingUserDTO userToUpdate)
        {
            if (!CheckRole(userToUpdate.Role))
            {
                throw new Exception("User role is not correct");
            }
            try
            {
                User user = await _dbContext.Users.Where(item => item.ID == userToUpdate.Id).FirstOrDefaultAsync();
                if(user == null)
                {
                    return false;
                }
                user.Username = userToUpdate.Username;
                user.Role = userToUpdate.Role;
                user.Password = BCrypt.Net.BCrypt.HashPassword(userToUpdate.Password);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occured durning updating user to database");
            }
        }

        public async Task<bool> RemoveUser(int id)
        {
            try
            {
                var user = await _dbContext.Users.Where(item =>item.ID == id).FirstOrDefaultAsync();
                if(user == null) 
                {
                    return false;
                }
                _dbContext.Users.Remove(user);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error occured durning removing user from database");

            }
        }

    }
}
