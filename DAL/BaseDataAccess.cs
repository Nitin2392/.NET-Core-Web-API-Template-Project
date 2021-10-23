﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

public class BaseDataAccess
{
    protected string ConnectionString { get; set; }

    public BaseDataAccess()
    {
    }

    public BaseDataAccess(string connectionString)
    {
        this.ConnectionString = connectionString;
    }

    private SqlConnection GetConnection()
    {
        SqlConnection connection = new SqlConnection(this.ConnectionString);
        if (connection.State != ConnectionState.Open)
            connection.Open();
        return connection;
    }

    protected DbCommand GetCommand(DbConnection connection, string commandText, CommandType commandType)
    {
        SqlCommand command = new SqlCommand(commandText, connection as SqlConnection)
        {
            CommandType = commandType
        };
        return command;
    }

    protected SqlParameter GetParameter(string parameter, object value)
    {
        SqlParameter parameterObject = new SqlParameter(parameter, value ?? DBNull.Value)
        {
            Direction = ParameterDirection.Input
        };
        return parameterObject;
    }

    protected SqlParameter GetParameterOut(string parameter, SqlDbType type, object value = null, ParameterDirection parameterDirection = ParameterDirection.InputOutput)
    {
        SqlParameter parameterObject = new SqlParameter(parameter, type); ;

        if (type == SqlDbType.NVarChar || type == SqlDbType.VarChar || type == SqlDbType.NText || type == SqlDbType.Text)
        {
            parameterObject.Size = -1;
        }

        parameterObject.Direction = parameterDirection;

        if (value != null)
        {
            parameterObject.Value = value;
        }
        else
        {
            parameterObject.Value = DBNull.Value;
        }

        return parameterObject;
    }

    protected int ExecuteNonQuery(string procedureName, List<DbParameter> parameters, CommandType commandType = CommandType.StoredProcedure)
    {
        int returnValue = -1;

        using SqlConnection connection = this.GetConnection();
        DbCommand cmd = this.GetCommand(connection, procedureName, commandType);

        if (parameters != null && parameters.Count > 0)
        {
            cmd.Parameters.AddRange(parameters.ToArray());
        }

        returnValue = cmd.ExecuteNonQuery();

        return returnValue;
    }

    protected object ExecuteScalar(string procedureName, List<SqlParameter> parameters)
    {
        object returnValue = null;

        using (DbConnection connection = this.GetConnection())
        {
            DbCommand cmd = this.GetCommand(connection, procedureName, CommandType.StoredProcedure);

            if (parameters != null && parameters.Count > 0)
            {
                cmd.Parameters.AddRange(parameters.ToArray());
            }

            returnValue = cmd.ExecuteScalar();
        }

        return returnValue;
    }

    protected DbDataReader GetDataReader(string procedureName, List<DbParameter> parameters, CommandType commandType = CommandType.StoredProcedure)
    {
        DbDataReader ds;

        DbConnection connection = this.GetConnection();
        {
            DbCommand cmd = this.GetCommand(connection, procedureName, commandType);
            if (parameters != null && parameters.Count > 0)
            {
                cmd.Parameters.AddRange(parameters.ToArray());
            }

            ds = cmd.ExecuteReader(CommandBehavior.CloseConnection);
        }

        return ds;
    }
}