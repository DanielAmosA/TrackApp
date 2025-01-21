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
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository usersRepository;
        
        public UsersService(IUsersRepository usersRepository)
        {
            this.usersRepository = usersRepository;
        }

        public async Task<List<User>> UsersGetUserByEmailAndPassword(string sEmail, string sPassword)
        {
            List<User> users = await usersRepository.UsersGetUserByEmail(sEmail);
            if( users == null || users.Count == 0)
            {
                throw new CustomException("The email does not exist.", "404");
            }
            AppActionModel appActionModel = new AppActionModel();
            bool isCorrect =  appActionModel.VerifyPassword(sPassword, users.ElementAt(0).Password);
            if( isCorrect )
            {
                return users;
            }
            else
            {
                throw new CustomException("The password does not correct.", "406");
            }           
        }

        public async Task<List<User>> UsersInsert(User user)
        {
            AppActionModel appActionModel = new AppActionModel();
            string sHashPassword = appActionModel.HashPassword(user.Password);
            user.Password = sHashPassword;
            return await usersRepository.UsersInsert(user);
        }
    }
}
