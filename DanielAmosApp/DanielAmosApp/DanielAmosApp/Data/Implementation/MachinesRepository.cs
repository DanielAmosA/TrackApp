using DanielAmosApp.Data.Interfaces;
using DanielAmosApp.Models;
using DanielAmosApp.Utills.Common;
using danielAmosServer_Core.Helpers.DB;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DanielAmosApp.Data.Implementation
{
    public class MachinesRepository : IMachinesRepository
    {
        private readonly string connectionString;
        private readonly DataHelper dataHelper;
        private readonly AppActionModel appActionModel;

        public MachinesRepository(string connectionString, DataHelper dataHelper, AppActionModel appActionModel)
        {
            this.connectionString = connectionString;
            this.dataHelper = dataHelper;
            this.appActionModel = appActionModel;
        }

        private List<Machine> CheckRes(DataTable res)
        {
            List<Machine> machines;

            if (res != null)
            {
                bool columnExists = res.Columns.Contains("ErrorMessage");
                if (columnExists)
                {
                    throw new CustomException(res.Rows[0]["ErrorMessage"].ToString()!, "401");
                }

                machines = new List<Machine>();

                foreach (DataRow row in res.Rows)
                {
                    // Creating a machine object of type `Machine` based on the machine type (according to the value in `MachineType`).
                    string machineType = row["MachineType"].ToString()!;

                    Machine machine;
                    if (machineType == MachineType.Robot.ToString())
                    {
                        machine = new RobotMachine();
                    }
                    else if (machineType == appActionModel.GetEnumDescription(MachineType.HumanOperated))
                    {
                        machine = new HumanOperatedMachine();
                    }
                    else
                    {
                        machine = new Machine(); //If there is no clear machine type, we will create a generic object.
                    }

                    //Converting the rows in the DataTable to a `Machine` object.
                    foreach (DataColumn column in res.Columns)
                    {
                        var property = typeof(Machine).GetProperty(column.ColumnName, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
                        if (property != null)
                        {
                            var value = row[column];
                            if (value == DBNull.Value)
                            {
                                property.SetValue(machine, null);
                            }
                            else
                            {
                                property.SetValue(machine, Convert.ChangeType(value, Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType));
                            }
                        }
                    }

                    machines.Add(machine); //Adding the machine to the list.
                }
            }
            else
            {
                machines = new List<Machine>(); // If the data does not exist, we will create an empty list.
            }

            return machines;
        }

        public async Task<string?> MachinesDelete(int iMachineID)
        {
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@MachineID", iMachineID);
            Object? res = await dataHelper.ExecSPAsScalar("Machines_Delete", sqlParameters);
            if (res != null)
            {
                return res as string;
            }
            else
            {
                return null;
            }

        }

        

        public async Task<List<Machine>> MachinesGetMachinesByStatus(string sStatusType)
        {
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@StatusType", sStatusType);
            DataTable? res = await dataHelper.ExecSPWithRes("MachinesGetMachinesByStatus", sqlParameters);
            return CheckRes(res);

        }

        public async Task<List<Machine>> MachinesGetMachinesByStatusId(int iCurrentStatusId)
        {
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@CurrentStatusId", iCurrentStatusId);
            DataTable? res = await dataHelper.ExecSPWithRes("Machines_GetMachinesByStatusId", sqlParameters);
            return CheckRes(res);

        }

        public async Task<List<Machine>> MachinesInsert(string sName, string sDescription, MachineType eMachineType, int iCurrentStatusId, string sNotes)
        {
            SqlParameter[] sqlParameters = new SqlParameter[5];
            sqlParameters[0] = new SqlParameter("@Name", sName);
            sqlParameters[1] = new SqlParameter("@Description", sDescription);
            sqlParameters[2] = new SqlParameter("@MachineType", appActionModel.GetEnumDescription(eMachineType));
            sqlParameters[3] = new SqlParameter("@CurrentStatusId", iCurrentStatusId);
            sqlParameters[4] = new SqlParameter("@Notes", sNotes);
            DataTable? res = await dataHelper.ExecSPWithRes("Machines_Insert", sqlParameters);
            return CheckRes(res);

        }

        public async Task<List<Machine>> MachinesUpdate(int iMachineID, string sNewName, string sNewDescription, 
                                                         MachineType eNewMachineType, int iNewCurrentStatusId, string sNewNotes)
        {
            SqlParameter[] sqlParameters = new SqlParameter[6];
            sqlParameters[0] = new SqlParameter("@MachineID", iMachineID);
            sqlParameters[1] = new SqlParameter("@New_Name", sNewName);
            sqlParameters[2] = new SqlParameter("@New_Description", sNewDescription);
            sqlParameters[3] = new SqlParameter("@New_MachineType", appActionModel.GetEnumDescription(eNewMachineType));
            sqlParameters[4] = new SqlParameter("@New_CurrentStatusId", iNewCurrentStatusId);
            sqlParameters[5] = new SqlParameter("@New_Notes", iNewCurrentStatusId);
            DataTable? res = await dataHelper.ExecSPWithRes("Machines_Update", sqlParameters);
            return CheckRes(res);
        }



    }
}
