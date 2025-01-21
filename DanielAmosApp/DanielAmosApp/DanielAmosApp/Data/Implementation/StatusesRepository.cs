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
    public class StatusesRepository : IStatusesRepository
    {
        private readonly string connectionString;
        private readonly DataHelper dataHelper;
        private readonly AppActionModel appActionModel;

        public StatusesRepository(string connectionString, DataHelper dataHelper, AppActionModel appActionModel)
        {
            this.connectionString = connectionString;
            this.dataHelper = dataHelper;
            this.appActionModel = appActionModel;
        }

        public async Task<List<Status>> StatusesGetAllStatus()
        {
            SqlParameter[] sqlParameters = new SqlParameter[0];
            DataTable? res = await dataHelper.ExecSPWithRes("Statuses_GetAllStatus", sqlParameters);
            List<Status> statuses;
            if (res != null)
            {
                statuses = appActionModel.ConvertDataTableToList<Status>(res);
            }
            else
            {
                statuses = new List<Status>();
            }
            return statuses;

        }
    }
}
