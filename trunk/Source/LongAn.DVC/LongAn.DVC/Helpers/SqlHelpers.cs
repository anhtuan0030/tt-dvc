using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LongAn.DVC.Helpers
{
    public class SqlHelpers
    {

        private static readonly string ConnectionString = "";

        #region Fill datatable

        public static DataTable Fill(String procedureName)
        {
            SqlConnection oConnection = new SqlConnection(ConnectionString);
            SqlCommand oCommand = new SqlCommand(procedureName, oConnection);
            oCommand.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter oAdapter = new SqlDataAdapter();
            oAdapter.SelectCommand = oCommand;
            oConnection.Open();
            DataTable dataTable = new DataTable();
            using (SqlTransaction oTransaction = oConnection.BeginTransaction())
            {
                try
                {
                    oAdapter.SelectCommand.Transaction = oTransaction;
                    oAdapter.Fill(dataTable);
                    oTransaction.Commit();
                }
                catch
                {
                    oTransaction.Rollback();
                }
                finally
                {
                    if (oConnection.State == ConnectionState.Open)
                        oConnection.Close(); oConnection.Dispose(); oAdapter.Dispose();
                }
            }
            return dataTable;
        }

        public static DataTable Fill(String procedureName, SqlParameter[] parameters)
        {

            SqlConnection oConnection = new SqlConnection(ConnectionString);
            SqlCommand oCommand = new SqlCommand(procedureName, oConnection);
            oCommand.CommandType = CommandType.StoredProcedure;
            if (parameters != null)
                oCommand.Parameters.AddRange(parameters);
            SqlDataAdapter oAdapter = new SqlDataAdapter();
            oAdapter.SelectCommand = oCommand;
            oConnection.Open();
            DataTable dataTable = new DataTable();
            using (SqlTransaction oTransaction = oConnection.BeginTransaction())
            {
                try
                {
                    oAdapter.SelectCommand.Transaction = oTransaction;
                    oAdapter.Fill(dataTable);
                    oTransaction.Commit();
                }
                catch
                {
                    oTransaction.Rollback();
                    throw;
                }
                finally
                {
                    if (oConnection.State == ConnectionState.Open)
                        oConnection.Close();
                    oConnection.Dispose();
                    oAdapter.Dispose();
                }
            }
            return dataTable;
        }


        public static DataTable Fill(String procedureName
            , SqlParameter[] parameters
            , ref SqlParameterCollection outParameters)
        {
            SqlConnection oConnection = new SqlConnection(ConnectionString);
            SqlCommand oCommand = new SqlCommand(procedureName, oConnection);
            oCommand.CommandType = CommandType.StoredProcedure;
            if (parameters != null)
                oCommand.Parameters.AddRange(parameters);
            SqlDataAdapter oAdapter = new SqlDataAdapter();
            oAdapter.SelectCommand = oCommand;
            oConnection.Open();
            DataTable dataTable = new DataTable();
            using (SqlTransaction oTransaction = oConnection.BeginTransaction())
            {
                try
                {
                    oAdapter.SelectCommand.Transaction = oTransaction;
                    oAdapter.Fill(dataTable);
                    oTransaction.Commit();
                    //foreach (SqlParameter item in oCommand.Parameters)
                    //{
                    //    if (item.Direction == ParameterDirection.Output)
                    //    {
                    //        outParameters.Add(item);
                    //    }
                    //}
                    outParameters = oCommand.Parameters;
                }
                catch
                {
                    oTransaction.Rollback();
                    throw;
                }
                finally
                {
                    if (oConnection.State == ConnectionState.Open)
                        oConnection.Close();
                    oConnection.Dispose();
                    oAdapter.Dispose();
                }
            }
            return dataTable;
        }
        #endregion Fill datatable

        #region Fill dataset
        public static void Fill(DataSet dataSet, String procedureName)
        {
            SqlConnection oConnection = new SqlConnection(ConnectionString);
            SqlCommand oCommand = new SqlCommand(procedureName, oConnection);
            oCommand.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter oAdapter = new SqlDataAdapter();
            oAdapter.SelectCommand = oCommand; oConnection.Open();
            using (SqlTransaction oTransaction = oConnection.BeginTransaction())
            {
                try
                {
                    oAdapter.SelectCommand.Transaction = oTransaction;
                    oAdapter.Fill(dataSet);
                    oTransaction.Commit();
                }
                catch
                {
                    oTransaction.Rollback();
                    throw;
                }
                finally
                {
                    if (oConnection.State == ConnectionState.Open)
                        oConnection.Close();
                    oConnection.Dispose();
                    oAdapter.Dispose();
                }
            }
        }

        public static void Fill(DataSet dataSet, String procedureName, SqlParameter[] parameters)
        {
            SqlConnection oConnection = new SqlConnection(ConnectionString);
            SqlCommand oCommand = new SqlCommand(procedureName, oConnection);
            oCommand.CommandType = CommandType.StoredProcedure;
            if (parameters != null)
                oCommand.Parameters.AddRange(parameters);
            SqlDataAdapter oAdapter = new SqlDataAdapter();
            oAdapter.SelectCommand = oCommand; oConnection.Open();
            using (SqlTransaction oTransaction = oConnection.BeginTransaction())
            {
                try
                {
                    oAdapter.SelectCommand.Transaction = oTransaction;
                    oAdapter.Fill(dataSet);
                    oTransaction.Commit();
                }
                catch
                {
                    oTransaction.Rollback();
                    throw;
                }
                finally
                {
                    if (oConnection.State == ConnectionState.Open)
                        oConnection.Close();
                    oConnection.Dispose();
                    oAdapter.Dispose();
                }
            }
        }
        #endregion Fill dataset

        #region Execute Scalar
        public static object ExecuteScalar(String procedureName)
        {
            SqlConnection oConnection = new SqlConnection(ConnectionString);
            SqlCommand oCommand = new SqlCommand(procedureName, oConnection);
            oCommand.CommandType = CommandType.StoredProcedure;
            object oReturnValue; oConnection.Open();
            using (SqlTransaction oTransaction = oConnection.BeginTransaction())
            {
                try
                {
                    oCommand.Transaction = oTransaction;
                    oReturnValue = oCommand.ExecuteScalar();
                    oTransaction.Commit();
                }
                catch
                {
                    oTransaction.Rollback();
                    throw;
                }
                finally
                {
                    if (oConnection.State == ConnectionState.Open)
                        oConnection.Close();
                    oConnection.Dispose();
                    oCommand.Dispose();
                }
            }
            return oReturnValue;
        }

        public static object ExecuteScalar(String procedureName, SqlParameter[] parameters)
        {
            SqlConnection oConnection = new SqlConnection(ConnectionString);
            SqlCommand oCommand = new SqlCommand(procedureName, oConnection);
            oCommand.CommandType = CommandType.StoredProcedure;
            object oReturnValue; oConnection.Open();
            using (SqlTransaction oTransaction = oConnection.BeginTransaction())
            {
                try
                {
                    if (parameters != null)
                        oCommand.Parameters.AddRange(parameters);
                    oCommand.Transaction = oTransaction;
                    oReturnValue = oCommand.ExecuteScalar();
                    oTransaction.Commit();
                }
                catch
                {
                    oTransaction.Rollback();
                    throw;
                }
                finally
                {
                    if (oConnection.State == ConnectionState.Open)
                        oConnection.Close();
                    oConnection.Dispose();
                    oCommand.Dispose();
                }
            }
            return oReturnValue;
        }
        #endregion

        #region Execute nonquery
        public static int ExecuteNonQuery(string procedureName)
        {
            SqlConnection oConnection = new SqlConnection(ConnectionString);
            SqlCommand oCommand = new SqlCommand(procedureName, oConnection);
            oCommand.CommandType = CommandType.StoredProcedure;
            int iReturnValue;
            oConnection.Open();
            using (SqlTransaction oTransaction = oConnection.BeginTransaction())
            {
                try
                {
                    oCommand.Transaction = oTransaction;
                    iReturnValue = oCommand.ExecuteNonQuery();
                    oTransaction.Commit();
                }
                catch
                {
                    oTransaction.Rollback();
                    throw;
                }
                finally
                {
                    if (oConnection.State == ConnectionState.Open)
                        oConnection.Close(); oConnection.Dispose();
                    oCommand.Dispose();
                }
            }
            return iReturnValue;
        }

        public static int ExecuteNonQuery(string procedureName, SqlParameter[] parameters)
        {
            SqlConnection oConnection = new SqlConnection(ConnectionString);
            SqlCommand oCommand = new SqlCommand(procedureName, oConnection);
            oCommand.CommandType = CommandType.StoredProcedure;
            int iReturnValue;
            oConnection.Open();
            using (SqlTransaction oTransaction = oConnection.BeginTransaction())
            {
                try
                {
                    if (parameters != null)
                        oCommand.Parameters.AddRange(parameters);
                    oCommand.Transaction = oTransaction;
                    iReturnValue = oCommand.ExecuteNonQuery();
                    oTransaction.Commit();
                }
                catch
                {
                    oTransaction.Rollback();
                    throw;
                }
                finally
                {
                    if (oConnection.State == ConnectionState.Open)
                        oConnection.Close();
                    oConnection.Dispose();
                    oCommand.Dispose();
                }
            }
            return iReturnValue;
        }
        #endregion Execute nonquery

        #region How to use

        //SqlParameter[] objParameter = new SqlParameter[2]; 
        //objParameter[0] = new SqlParameter("@param1", param1); 
        //objParameter[1] = new SqlParameter("@param2", param2); 

        #endregion How to use
    }
}
