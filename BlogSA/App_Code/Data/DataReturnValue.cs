using System;
using System.Collections.Generic;
using System.Text;

public class DataReturnValue
{
    private Exception _error;
    private DataProcessState _status;
    private object _value;

    public DataReturnValue()
    {

    }

    public DataReturnValue(object value, DataProcessState state)
    {
        this.Value = value;
        this.Status = state;
        this.Error = null;
    }

    public DataReturnValue(Exception error)
    {
        this.Value = null;
        this.Error = error;
        this.Status = DataProcessState.Error;
    }

    public Exception Error
    {
        get { return _error; }
        set { _error = value; }
    }

    public DataProcessState Status
    {
        get { return _status; }
        set { _status = value; }
    }

    public object Value
    {
        get { return _value; }
        set { _value = value; }
    }
}

public enum DataProcessState { Success = 1, Error = 0 }
