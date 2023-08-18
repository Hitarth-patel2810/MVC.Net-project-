using ConnectionLibrary.Config;
using ConnectionLibrary.Extension;
using ConnectionLibrary.Interface;
using Microsoft.EntityFrameworkCore;
using project7thSem.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace ConnectionLibrary.Service
{
    public class ConnectionClass : IConnectionClass
    {
        private readonly SqlConnection _connection;
        public ConnectionClass()
        {
            _connection = new SqlConnection(AppSettings.ConnectionStrings.DB);
            _connection.Open();
        }


        public DataTable Select(string strQuery)
        {

            var dt = new DataTable();
            try
            {

                if (_connection.State == ConnectionState.Closed)
                {
                    _connection.Open();
                }
                var sqlCommand = new SqlCommand(strQuery, _connection);
                sqlCommand.CommandTimeout = 10000;
                var dataAdapter = new SqlDataAdapter(sqlCommand);
                dataAdapter.Fill(dt);
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                _connection.Close();
            }

            return dt;
        }


        public int Insert(string strQuery)
        {
            try
            {
                if (ConnectionState.Closed == _connection.State)
                {
                    _connection.Open();
                }
                using (SqlCommand cmd = new SqlCommand(strQuery, _connection))
                {
                    cmd.CommandTimeout = 99999;
                    cmd.ExecuteNonQuery();
                }
                return 1;
            }
            catch (Exception ex)
            {
                //LogManager.LogManager.ErrorInformation($"Method : Insert cause Exception : {ex.Message}");
                return 0;
            }
            finally
            {
                _connection.Close();
            }
        }

        public bool Update(string strQuery)
        {
            try
            {
                if (ConnectionState.Closed == _connection.State)
                {
                    _connection.Open();
                }
                using (SqlCommand cmd = new SqlCommand(strQuery, _connection))
                {
                    cmd.ExecuteNonQuery();
                }
                return true;
            }
            catch (Exception ex)
            {
                //LogManager.LogManager.ErrorInformation($"Method : Update cause Exception : {ex.Message}");
                return false;
            }
            finally
            {
                _connection.Close();
            }
        }


        //public SearchResult TenderNotice(string strQuery)
        //{

        //    var model = new SearchResult();
        //    var dt = new DataTable();
        //    try
        //    {

        //        if (_connection.State == ConnectionState.Closed)
        //        {
        //            _connection.Open();
        //        }
        //        var sqlCommand = new SqlCommand(strQuery, _connection);
        //        sqlCommand.CommandTimeout = 10000;
        //        var dataAdapter = new SqlDataAdapter(sqlCommand);
        //        dataAdapter.Fill(dt);
        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }
        //    finally
        //    {
        //        _connection.Close();
        //    }

        //    model.TenderDetails = Helper.ConvertDataTable<DataList>(dt);

        //    return model;
        //}
    }
}
