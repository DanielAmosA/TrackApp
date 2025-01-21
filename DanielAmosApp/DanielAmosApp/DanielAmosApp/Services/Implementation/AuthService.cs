using DanielAmosApp.Data.Implementation;
using DanielAmosApp.Data.Interfaces;
using DanielAmosApp.Models;
using DanielAmosApp.Services.Interfaces;
using DanielAmosApp.Utills.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DanielAmosApp.Services.Implementation
{
    /// <summary>
    /// The class is responsible for updating the user's status on the site.
    /// </summary>
    public class AuthService : IAuthService
    {
        private bool isAuthenticated;
        private string? currentUsername;

        private readonly IUsersRepository usersRepository;

        public AuthService(IUsersRepository usersRepository = null)
        {
            if(usersRepository == null)
            {
                usersRepository = new UsersRepository(null, null, null);
            }
            this.usersRepository = usersRepository;
        }

        public bool IsAuthenticated => isAuthenticated;
        public string CurrentUsername => currentUsername!;

        public event EventHandler<bool>? AuthenticationChanged;

        public async Task<bool> LoginAsync(string sEmail, string sPassword)
        {
            List<User> users = await usersRepository.UsersGetUserByEmail(sEmail);
            if (users == null || users.Count == 0)
            {
                return false;
            }

            AppActionModel appActionModel = new AppActionModel();
            bool isCorrect = appActionModel.VerifyPassword(sPassword, users.ElementAt(0).Password);
            if (isCorrect)
            {
                isAuthenticated = true;
                currentUsername = users.ElementAt(0).Name;
                AuthenticationChanged?.Invoke(this, true);
                return true;
            }

            return false;
        }

        public void Logout()
        {
            isAuthenticated = false;
            currentUsername = null;
            AuthenticationChanged?.Invoke(this, false);
        }
    }
}
