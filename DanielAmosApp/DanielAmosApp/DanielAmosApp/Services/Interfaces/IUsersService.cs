using DanielAmosApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DanielAmosApp.Services.Interfaces
{
    public interface IUsersService
    {
        Task<List<User>> UsersGetUserByEmailAndPassword(string sEmail, string sPassword);
        Task<List<User>> UsersInsert(User user);
    }
}
