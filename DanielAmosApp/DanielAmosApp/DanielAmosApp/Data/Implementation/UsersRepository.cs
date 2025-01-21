using DanielAmosApp.Data.Interfaces;
using DanielAmosApp.Models;
using DanielAmosApp.Utills.Common;
using danielAmosServer_Core.Helpers.DB;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DanielAmosApp.Data.Implementation
{
    public class UsersRepository : IUsersRepository
    {
        private readonly string connectionString;
        private readonly DataHelper dataHelper;
        private readonly AppActionModel appActionModel;

        public UsersRepository(string connectionString, DataHelper dataHelper, AppActionModel appActionModel)
        {
            this.connectionString = connectionString;
            this.dataHelper = dataHelper;
            this.appActionModel = appActionModel;
        }

        private List<User> CheckRes(DataTable? res)
        {
            List<User> users;
            if (res != null)
            {
                bool columnExists = res.Columns.Contains("ErrorMessage");

                if (columnExists)
                {
                    throw new CustomException(res.Rows[0]["ErrorMessage"].ToString()!, "401");
                }
                users = appActionModel.ConvertDataTableToList<User>(res);
            }
            else
            {
                users = new List<User>();
            }
            return users;
        }

        public async Task<List<User>> UsersGetUserByEmailAndPassword(string sEmail, string sPassword)
        {
            SqlParameter[] sqlParameters = new SqlParameter[2];
            sqlParameters[0] = new SqlParameter("@Email", sEmail);
            sqlParameters[1] = new SqlParameter("@Password", sPassword);
            DataTable? res = await dataHelper.ExecSPWithRes("Users_GetUserByEmailAndPassword", sqlParameters);
            return CheckRes(res);
        }

        public async Task<List<User>> UsersGetUserByEmail(string sEmail)
        {
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@Email", sEmail);
            DataTable? res = await dataHelper.ExecSPWithRes("Users_GetUserByEmail", sqlParameters);
            return CheckRes(res);
        }

        public async Task<List<User>> UsersInsert(User user)
        {
            SqlParameter[] sqlParameters = new SqlParameter[3];
            sqlParameters[0] = new SqlParameter("@Name", user.Name);
            sqlParameters[1] = new SqlParameter("@Email", user.Email);
            sqlParameters[2] = new SqlParameter("@Password", user.Password);
            DataTable? res = await dataHelper.ExecSPWithRes("Users_Insert", sqlParameters);
            return CheckRes(res);
        }
    }
}
