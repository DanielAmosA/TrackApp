using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DanielAmosApp.Services.Interfaces
{
    public interface IAuthService
    {
        bool IsAuthenticated { get; }
        string CurrentUsername { get; }
        Task<bool> LoginAsync(string username, string password);
        void Logout();
        event EventHandler<bool> AuthenticationChanged;
    }
}
