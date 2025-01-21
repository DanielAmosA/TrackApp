using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Documents;

namespace DanielAmosApp.Utills.Common
{

    /// <summary>
    /// The class responsible for General actions.
    /// </summary>

    public class AppActionModel
    {
        #region Hash
        //  dummy user => no storing data
        public class UserHasher { }

        // We will convert the received password to a hash.
        public string HashPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
                throw new CustomException("Password cannot be null or empty","402");
            PasswordHasher<UserHasher> passwordHasher = new PasswordHasher<UserHasher>();
            var userHasher = new UserHasher();
            string hashedPassword = passwordHasher.HashPassword(userHasher, password);
            return hashedPassword;
        }

        // After conversion,
        // we will check if the entered password matches the password stored in the hash.
        public bool VerifyPassword(string currentPassword, string storedHash)
        {
            if (string.IsNullOrEmpty(currentPassword))
                throw new CustomException("Password cannot be null or empty", "402");
            if (string.IsNullOrEmpty(storedHash))
                throw new CustomException("Password cannot be null or empty", "402");

            PasswordHasher<UserHasher> passwordHasher = new PasswordHasher<UserHasher>();
            var UserHasher = new UserHasher();
            var result = passwordHasher.VerifyHashedPassword(UserHasher, storedHash, currentPassword);

            return result == PasswordVerificationResult.Success;
        }

        #endregion

        #region Generic       
        public List<T> ConvertDataTableToList<T>(DataTable dataTable) where T : new()
        {
            var list = new List<T>();

            foreach (DataRow row in dataTable.Rows)
            {
                T obj = new T();

                foreach (DataColumn column in dataTable.Columns)
                {
                    // Get the property from the class (including base classes)
                    var property = typeof(T).GetProperty(column.ColumnName, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);

                    if (property != null)
                    {
                        var value = row[column];

                        // Check if the value is DBNull
                        if (value == DBNull.Value)
                        {
                            property.SetValue(obj, null);
                        }
                        else
                        {
                            // Set the value, accounting for nullable types
                            property.SetValue(obj, Convert.ChangeType(value, Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType));
                        }
                    }
                }

                // Add the object to the list
                list.Add(obj);
            }

            return list;
        }



        // We will create a generic function
        // that converts a Model
        // into a array of params of the desired model.
        public SqlParameter[] GenerateSqlParameters<T>(T model)
        {
            PropertyInfo[] properties = typeof(T).GetProperties();
            var parameters = new List<SqlParameter>();
            foreach (var property in properties)
            {
                var value = property.GetValue(model);

                if (value != null)
                {
                    string paramName = $"@{property.Name}";
                    parameters.Add(new SqlParameter(paramName, value));
                }
            }
            return parameters.ToArray();
        }

        public string GetEnumDescription<T>(T value) where T : Enum
        {
            var fieldInfo = value.GetType().GetField(value.ToString());
            var attribute = (EnumMemberAttribute)fieldInfo?.GetCustomAttribute(typeof(EnumMemberAttribute))!;

            return attribute?.Value ?? value.ToString();
        }

        public ObservableCollection<TEnum> GetEnumValues<TEnum>() where TEnum : Enum
        {
            var enumValues = Enum.GetValues(typeof(TEnum)).Cast<TEnum>();
            return new ObservableCollection<TEnum>(enumValues);
        }

        //The function returns an ObservableCollection<T>,
        //which will be a list of the objects you receive.

        public async Task<ObservableCollection<T>> GetItems<T>(Func<Task<List<T>>> serviceCall)
        {
            try
            {
                List<T> items = await serviceCall();

                if (items == null)
                {
                    throw new CustomException("No items were found", "404");
                }

                return new ObservableCollection<T>(items);
            }
            catch (Exception ex)
            {
                MessageBox.Show("There is a temporary issue, please try again soon...", "Error");
                LoggerModel loggerModel = new LoggerModel();
                loggerModel.CreateErrorInfo("ViewModel", $"Get Items failed {ex.Message}");
                return null;
            }
        }

        #endregion
    }
}
