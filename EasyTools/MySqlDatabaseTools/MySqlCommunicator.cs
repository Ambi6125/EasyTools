using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyTools.Validation;
using MySql.Data;
using MySql.Data.MySqlClient;
using EasyTools.MySqlDatabaseTools.Queries;
using EasyTools.MySqlDatabaseTools.Tables;
using System.Runtime.CompilerServices;

namespace EasyTools.MySqlDatabaseTools
{
    public class MySqlCommunicator : IDisposable, IDatabaseCommunicator
    {
        private readonly string connectionString;

        private MySqlConnection connection;


        public MySqlCommunicator(string connectionString)
        {
            this.connectionString = connectionString;
            connection = new MySqlConnection(connectionString);
        }
        ~MySqlCommunicator()
        {
            Dispose();
        }

        public void Dispose()
        {
            if(connection.State != System.Data.ConnectionState.Closed)
            {
                connection.Close();
            }
            connection.Dispose();
        }

        #region Query execution

        //public IValidationResponse Execute(IExecutable<MySqlCommand> sqlQuery)
        //{
        //    using (connection)
        //    {
        //        try
        //        {
        //            MySqlCommand cmd = sqlQuery.Execute();
        //            cmd.Connection = connection;
        //            connection.Open();
        //            i
        //        }
        //    }
        //}

        /// <summary>
        /// Runs Execute() on the executable and adds this.connection to the returned result.
        /// </summary>
        /// <param name="executable">An object whose Execute() method returns a MySqlCommand, probably a
        /// sublass of MySqlQuery.</param>
        /// <returns>A fully working MySqlCommand ready to be executed.</returns>
        private MySqlCommand BuildConnection(IExecutable<MySqlCommand> executable)
        {
            MySqlCommand cmd = executable.Execute();
            cmd.Connection = connection;
            return cmd;
        }

        /// <summary>
        /// Executes an insert query using the connection set.
        /// </summary>
        /// <param name="query">The query for the database to execute.</param>
        /// <returns>A response containing a message describing what happened. If succesfull, will also contain an amount of rows affected.</returns>
        public IValidationResponse Insert(InsertQuery query)
        {
            using (connection)
            {
                try
                {
                    MySqlCommand cmd = query.Execute();
                    cmd.Connection = connection;
                    connection.Open();
                    int result = cmd.ExecuteNonQuery();
                    return new AmountValidationResponse(true, "Succesfully added", result); 
                }
                catch (MySqlException ex)
                {
                    return new ValidationResponse(false, ex.Message);
                }
                catch(AggregateException ex)
                {
                    return new ValidationResponse(false, "Could not find host.");
                }
                finally
                {
                    if (connection.State is not System.Data.ConnectionState.Closed)
                    {
                        connection.Close();
                    }
                }
            }
        }

        public IValidationResponse Update(UpdateQuery query)
        {
            using (connection)
            {
                try
                {
                    MySqlCommand cmd = query.Execute();
                    cmd.Connection = connection;
                    connection.Open();
                    int result = cmd.ExecuteNonQuery();
                    return new AmountValidationResponse(true, "Succesfully updated", result);
                }
                catch (MySqlException ex)
                {
                    return new ValidationResponse(false, ex.Message);
                }
                finally
                {
                    if (connection.State is not System.Data.ConnectionState.Closed)
                    {
                        connection.Close();
                    }
                }
            }
        }

        public IReadOnlyCollection<IReadOnlyParameterValueCollection> Select(SelectQuery query)
        {
            List<ParameterValueCollection> parameterValuePairs = new List<ParameterValueCollection>();
            using (connection)
            {
                MySqlCommand cmd = BuildConnection(query);
                try
                {
                    connection.Open();
                    MySqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        ParameterValueCollection valuePairs = new ParameterValueCollection();
                        for (int i = 0; i < reader.FieldCount; i++) //Iterates through every column in a row.
                        {
                            ParameterValuePair pvp = new ParameterValuePair(reader.GetName(i), reader.GetValue(i));
                            valuePairs.Add(pvp);
                        }
                        parameterValuePairs.Add(valuePairs);
                    }
                    reader.Close();
                }
                catch(MySqlException ex)
                {
                    return null;
                }
                finally
                {
                    if(connection.State is not System.Data.ConnectionState.Closed)
                    {
                        connection.Close();
                    }
                }
            }
            return parameterValuePairs;
        }

        public IValidationResponse Delete(DeleteQuery query)
        {
            MySqlCommand cmd = BuildConnection(query);
            try
            {
                using (connection)
                {
                    connection.Open();
                    int result = cmd.ExecuteNonQuery();
                    return new AmountValidationResponse(true, "Succesfully removed", result);
                }
            }
            catch(MySqlException e)
            {
                return new ValidationResponse(false, e.Message);
            }
            finally
            {
                if(connection.State is not System.Data.ConnectionState.Closed)
                {
                    connection.Close();
                }
            }
        }
        #endregion
    }
}
