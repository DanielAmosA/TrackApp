using DanielAmosApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DanielAmosApp.Data.Interfaces
{
    public interface IUsersRepository
    {
        Task<List<User>> UsersGetUserByEmailAndPassword(string sEmail, string sPassword);
        Task<List<User>> UsersInsert(User user);
        Task<List<User>> UsersGetUserByEmail(string sEmail);
    }
}
