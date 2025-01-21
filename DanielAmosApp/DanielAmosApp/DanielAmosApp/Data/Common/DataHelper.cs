using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using Serilog;
using Microsoft.Data.SqlClient;
using DanielAmosApp.Utills.Common;
using System.Data;


namespace danielAmosServer_Core.Helpers.DB
{
    /// <summary>
    /// The class responsible for Sql action.
    /// </summary>

    public class DataHelper
    {
        private readonly string connectionString;
        private readonly AppActionModel appAction;
        private readonly LoggerModel logger;

        public DataHelper()
        {
            DbConfigReader dbConfigReader = new DbConfigReader();
            //Reading Connection String from the XML
            connectionString = dbConfigReader.GetConnectionString();
            appAction = new AppActionModel();
            logger = new LoggerModel();
        }

        /// <summary>
        /// Executing a procedure that does return scaler
        /// </summary>
        /// <param name="storedProcedureName">The stored Procedure Name.</param>
        /// <param name="parameters">The stored Procedure Paramters.</param>
        public async Task<object?> ExecSPAsScalar(string storedProcedureName, SqlParameter[] parameters)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand(storedProcedureName, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;


                        //Checking if parameters exist and adding them accordingly
                        if (parameters != null)
                        {
                            command.Parameters.AddRange(parameters);
                            // Retrieving all parameters as a string.

                            List<string> paramsList = parameters.Select(p => $"{p.ParameterName}={p.Value}").ToList();
                            string paramsString = string.Join(", ", paramsList);
                            logger.CreateLogInfo("DataHelper", $"Executing stored procedure {storedProcedureName} with parameters: {paramsString}");
                        }

                        else
                        {
                            logger.CreateLogInfo("DataHelper", $"Executing stored procedure {storedProcedureName}");
                        }

                        //Executing the procedure and check if get value
                        return command.ExecuteScalar();

                    }
                }
            }
            catch (Exception ex)
            {
                logger.CreateLogInfo("DataHelper", ex.Message);
                return null;
            }

        }

        /// <summary>
        /// Executing a procedure that does not return data
        /// </summary>
        /// <param name="storedProcedureName">The stored Procedure Name.</param>
        /// <param name="parameters">The stored Procedure Paramters.</param>
        public async Task<bool> ExecSPWithoutRes(string storedProcedureName, SqlParameter[] parameters)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand(storedProcedureName, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;


                        //Checking if parameters exist and adding them accordingly
                        if (parameters != null)
                        {
                            command.Parameters.AddRange(parameters);
                            // Retrieving all parameters as a string.

                            List<string> paramsList = parameters.Select(p => $"{p.ParameterName}={p.Value}").ToList();
                            string paramsString = string.Join(", ", paramsList);
                            logger.CreateLogInfo("DataHelper", $"Executing stored procedure {storedProcedureName} with parameters: {paramsString}");
                        }

                        else
                        {
                            logger.CreateLogInfo("DataHelper", $"Executing stored procedure {storedProcedureName}");
                        }

                        //Executing the procedure and check if affected
                        int rowsAffected = await command.ExecuteNonQueryAsync();

                        if (rowsAffected > 0)
                        {
                            logger.CreateLogInfo("DataHelper", $"Procedure executed successfully. Rows affected: {rowsAffected}");
                            return true;
                        }
                        else
                        {

                            logger.CreateLogInfo("DataHelper", "No rows were affected by the procedure.");
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                logger.CreateLogInfo("DataHelper", ex.Message);
                return false;
            }

        }

        /// <summary>
        /// Executing a procedure that does return data
        /// </summary>
        /// <param name="storedProcedureName">The stored Procedure Name.</param>
        /// <param name="parameters">The stored Procedure Paramters.</param>

        public async Task<DataTable?> ExecSPWithRes(string storedProcedureName, SqlParameter[] parameters)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();

                    using (SqlCommand command = new SqlCommand(storedProcedureName, connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        string paramsString = "";

                        //Checking if parameters exist and adding them accordingly
                        if (parameters != null)
                        {
                            command.Parameters.AddRange(parameters);

                            // Retrieving all parameters as a string.
                            List<string> paramsList = parameters.Select(p => $"{p.ParameterName}={p.Value}").ToList();
                            paramsString = string.Join(", ", paramsList);
                            logger.CreateLogInfo("DataHelper", $"Executing stored procedure {storedProcedureName} with parameters: {paramsString}");
                        }

                        else
                        {
                            logger.CreateLogInfo("DataHelper", $"Executing stored procedure {storedProcedureName}");
                        }

                        using (SqlDataAdapter dataAdapter = new SqlDataAdapter(command))
                        {
                            DataTable dataTable = new DataTable();

                            //We will fill a general table to represent the data we received
                            await Task.Run(() => dataAdapter.Fill(dataTable));
                            if (paramsString != "")
                            {
                                logger.CreateLogInfo("DataHelper", $"Procedure {storedProcedureName} completed successfully. Parameters used: {paramsString}");
                            }

                            else
                            {
                                logger.CreateLogInfo("DataHelper", $"Procedure {storedProcedureName} completed successfully");
                            }

                            return dataTable;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                logger.CreateLogInfo("DataHelper", ex.Message);
                return null;
            }

        }
    }
}
