using DanielAmosApp.Utills.Common;
using danielAmosServer_Core.Helpers.DB;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DanielAmosApp.Data
{
    class DbAction
    {
        private readonly string connectionString;
        private readonly DataHelper dataHelper;
        private readonly AppActionModel appActionModel;

        public DbAction()
        {
            DbConfigReader dbConfigReader = new DbConfigReader();
            dataHelper = new DataHelper();
            connectionString = dbConfigReader.GetConnectionString();
            appActionModel = new AppActionModel();
        }

        public async Task<List<object>> ManagerSelect()
        {
            SqlParameter[] sqlParameters = new SqlParameter[0];
            DataTable? res = await dataHelper.ExecSPWithRes("Manager_Select", sqlParameters);
            List<object> managers;
            if (res != null)
            {
                managers = appActionModel.ConvertDataTableToList<object>(res);
            }
            else
            {
                managers = new List<object>();
            }
            return managers;
        }

        public async Task<List<object>> ManagerFullDataSelectByID(int id)
        {
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@id", id);
            DataTable? res = await dataHelper.ExecSPWithRes("Manager_FullDataSelectByID", sqlParameters);
            List<object> managerFallDatas;
            if (res != null)
            {
                managerFallDatas = appActionModel.ConvertDataTableToList<object>(res);
            }
            else
            {
                managerFallDatas = new List<object>();
            }
            return managerFallDatas;
        }

        public async Task<string?> GetThePasswordByEmail(string email)
        {
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@email", email);
            Object? res = await dataHelper.ExecSPAsScalar("Employee_GetThePasswordByEmail", sqlParameters);
            if (res != null)
            {
                return res as string;
            }
            else
            {
                return null;
            }
        }

    }
}
