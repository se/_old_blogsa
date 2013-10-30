using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Data;
using System.IO;
using System.Web;
using System.Web.Caching;

public class DataProcess : IDisposable
{
    private string _connectionString;
    private string _providerName;
    private string _connectionStringName;

    private DbProviderFactory _factory;

    private DbProviderFactory Factory
    {
        set
        {
            _factory = value;
        }
        get
        {
            if (String.IsNullOrEmpty(ProviderName))
                throw new Exception("Provider Name cannot be null!");

            if (_factory == null)
                _factory = DbProviderFactories.GetFactory(ProviderName);
            return _factory;
        }
    }

    private DbConnection _connection;
    private DbConnection Connection
    {
        get { return _connection; }
        set { _connection = value; }
    }

    public DataProcess()
    {
        Initialize("ConnectionString");
    }

    public DataProcess(string pConnectionStringName)
    {
        Initialize(pConnectionStringName);
    }

    private void Initialize(string pConnectionStringName)
    {
        ConnectionStringName = pConnectionStringName;
        ConnectionString = ConfigurationManager.AppSettings["ConnectionString"];
        ProviderName = ConfigurationManager.AppSettings["Provider"];

        Connection = Factory.CreateConnection();
        if (Connection != null)
        {
            Connection.ConnectionString = ConnectionString;
            Connection.Open();
        }
    }

    #region Paramaters

    private Dictionary<string, object> _parameters;
    public void AddParameter(string name, object value)
    {
        if (_parameters == null)
            _parameters = new Dictionary<string, object>();
        if (_parameters.ContainsKey(name))
            _parameters[name] = value;
        else
            _parameters.Add(name, value);
    }
    public void RemoveParameter(string name)
    {
        if (_parameters != null)
            _parameters.Remove(name);
    }
    private void ClearParameters()
    {
        if (_parameters != null)
            _parameters.Clear();
    }
    private void FillParameters(DbCommand dbCommand)
    {
        if (_parameters != null && _parameters.Count > 0)
        {
            foreach (string key in _parameters.Keys)
            {
                DbParameter dbParameter = Factory.CreateParameter();

                if (dbParameter != null)
                {
                    dbParameter.ParameterName = String.Format("{0}", key);

                    object value = _parameters[key];

                    dbParameter.Value = value ?? DBNull.Value;

                    if (ProviderName.ToLowerInvariant().Equals("system.data.oledb") && dbParameter.Value is DateTime)
                        dbParameter.Value = ((DateTime)dbParameter.Value).ToOADate();

                    dbCommand.Parameters.Add(dbParameter);
                }
            }
        }
        ClearParameters();
    }
    #endregion
    #region Others

    private DataReturnValue _return;

    public string ConnectionStringName
    {
        get { return _connectionStringName; }
        set { _connectionStringName = value; }
    }

    public string ProviderName
    {
        get { return _providerName; }
        set { _providerName = value; }
    }

    public string ConnectionString
    {
        get { return _connectionString; }
        set { _connectionString = value; }
    }

    /// <summary>
    /// Last Executed Process.
    /// </summary>
    public DataReturnValue Return
    {
        get { return _return; }
        set { _return = value; }
    }

    #endregion
    #region Process
    public DataReturnValue ExecuteDataset(string strSqlCommand)
    {
        DataReturnValue rv = new DataReturnValue();
        try
        {
            DataSet DS = new DataSet();
            DbDataAdapter DAP = Factory.CreateDataAdapter();
            DbCommand Comm = Factory.CreateCommand();
            Comm.CommandText = strSqlCommand;
            Comm.Connection = Connection;
            FillParameters(Comm);
            DAP.SelectCommand = Comm;
            DAP.Fill(DS);
            rv.Value = DS;
            rv.Status = DataProcessState.Success;
        }
        catch (Exception Ex)
        {
            rv.Error = Ex;
            rv.Status = DataProcessState.Error;
        }
        Return = rv;
        WriteLog(Return);
        return rv;
    }
    public DataReturnValue ExecuteReader(string strSqlCommand)
    {
        DataProcessLog dpl = new DataProcessLog();
        DataReturnValue rv = new DataReturnValue();
        try
        {
            dpl.ExecutionStart = DateTime.Now;
            dpl.Command = strSqlCommand;
            DataSet DS = new DataSet();
            DataTable DT = new DataTable();
            using (DbCommand Comm = Factory.CreateCommand())
            {
                if (Comm != null)
                {
                    Comm.CommandText = strSqlCommand;
                    Comm.Connection = Connection;
                    FillParameters(Comm);
                    rv.Value = Comm.ExecuteReader();
                }
            }
            rv.Status = DataProcessState.Success;
        }
        catch (Exception Ex)
        {
            rv.Status = DataProcessState.Error;
            rv.Error = Ex;
        }
        Return = rv;
        WriteLog(Return);
        dpl.ExecutionEnd = DateTime.Now;
        dpl.ExecutionTime = dpl.ExecutionStart - dpl.ExecutionEnd;
        WriteDPLog(dpl);
        return rv;
    }
    public DataReturnValue ExecuteScalar(string strSqlCommand)
    {
        DataReturnValue rv = new DataReturnValue();
        try
        {
            DbCommand Comm = Factory.CreateCommand();
            Comm.CommandText = strSqlCommand;
            Comm.Connection = Connection;
            FillParameters(Comm);
            rv.Value = Comm.ExecuteScalar();
            Comm.Dispose();
            rv.Status = DataProcessState.Success;
        }
        catch (Exception Ex)
        {
            rv.Status = DataProcessState.Error;
            rv.Error = Ex;
        }
        Return = rv;
        WriteLog(Return);
        return rv;
    }
    public DataReturnValue ExecuteNonQuery(string strSqlCommand)
    {
        DataReturnValue rv = new DataReturnValue();
        try
        {
            DataSet DS = new DataSet();
            DataTable DT = new DataTable();
            DbCommand Comm = Factory.CreateCommand();
            Comm.CommandText = strSqlCommand;
            Comm.Connection = Connection;
            FillParameters(Comm);
            Comm.ExecuteNonQuery();
            Comm.Dispose();
            rv.Status = DataProcessState.Success;
        }
        catch (Exception Ex)
        {
            rv.Status = DataProcessState.Error;
            rv.Error = Ex;
        }
        Return = rv;
        WriteLog(Return);
        return rv;
    }
    #endregion
    #region DataProcess Log
    #endregion
    #region IDisposable Members
    public void Dispose()
    {
        if (Connection != null)
        {
            Connection.Close();
            Connection.Dispose();
        }

        GC.SuppressFinalize(this);
    }
    #endregion
    #region Log
    /// <summary>
    /// If return value status type is Error then write log.
    /// </summary>
    /// <param name="returnValue"></param>
    private void WriteLog(DataReturnValue returnValue)
    {
        if (returnValue.Status == DataProcessState.Error)
        {
            BSLog l = new BSLog();
            l.CreateDate = DateTime.Now;
            l.LogID = Guid.NewGuid();
            l.LogType = BSLogType.Error;
            l.Message = returnValue.Error.Message;
            l.RawUrl = HttpContext.Current.Request.RawUrl;
            l.Source = returnValue.Error.Source;
            l.StackTrace = returnValue.Error.StackTrace;
            l.TargetSite = returnValue.Error.TargetSite;
            l.Url = HttpContext.Current.Request.Url.ToString();
            l.UserID = Blogsa.ActiveUser != null ? Blogsa.ActiveUser.UserID : 0;
            l.Save();
        }
    }

    private void WriteDPLog(DataProcessLog dpl)
    {
        string folder = HttpContext.Current.Server.MapPath("~/App_Data/Log/DataProcess/");
        DirectoryInfo di = new DirectoryInfo(folder);
        if (!di.Exists)
            di.Create();

        FileInfo fi = new FileInfo(Path.Combine(di.FullName, String.Format("{0:dd.MM.yyyy}.txt", DateTime.Now)));
        using (TextWriter tw = new StreamWriter(fi.FullName, true))
            tw.WriteLine(String.Format("{0}\t{1}\t{2}\t{3}", dpl.ExecutionStart, dpl.ExecutionEnd, dpl.ExecutionTime, dpl.Command));
    }
    #endregion
}

public class DataProcessLog
{
    private String command;
    private DateTime executionStart;
    private DateTime executionEnd;
    private TimeSpan executionTime;

    public string Command
    {
        get { return command; }
        set { command = value; }
    }

    public DateTime ExecutionStart
    {
        get { return executionStart; }
        set { executionStart = value; }
    }

    public DateTime ExecutionEnd
    {
        get { return executionEnd; }
        set { executionEnd = value; }
    }

    public TimeSpan ExecutionTime
    {
        get { return executionTime; }
        set { executionTime = value; }
    }
}