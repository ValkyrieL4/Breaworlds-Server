using System.Data;
using System.Data.SQLite;
using System.Collections.Generic;


public enum SQLiteConnectionType
{
    filename = 0,
    connectionString = 1
}

public enum ColumnType
{
    Int = 0,
    Float = 1,
    Datetime = 2,
    String = 3,
    Blob = 4
}

public class SQLiteDatabase
{
    private new SQLiteConnection con;

    public SQLiteDatabase() { }

    public SQLiteDatabase(string filename, SQLiteConnectionType connectionType)
    {
        if (connectionType == SQLiteConnectionType.filename)
        {
            string connectionString = @$"URI=file:{filename}";
            con = new SQLiteConnection(connectionString);
        }
        else
        {
            con = new SQLiteConnection(filename);
        }
        con.Open();
    }

    public new string[] GetTables()
    {
        if (con.State == ConnectionState.Open)
        {
            List<string> tablelist = new List<string>();
            var tables = Select("SELECT name FROM sqlite_master WHERE type = 'table' AND name NOT LIKE 'sqlite_%';");
            while (tables.Read()) tablelist.Add(tables.GetString(0));
            return tablelist.ToArray();
        }
        return new List<string>().ToArray();
    }

    public new string[] GetColumns(string table)
    {
        if (con.State == ConnectionState.Open)
        {
            List<string> collist = new List<string>();
            var stmt = Prepare("SELECT name FROM pragma_table_info(@table) ");
            stmt = Parameter("table", table, stmt);
            var columns = ExecuteDQL(stmt);
            while (columns.Read()) collist.Add(columns.GetString(0));
            return collist.ToArray();
        }
        return new List<string>().ToArray();
    }

    #region Query
    public new int Run(string query) 
    {
        var cmd = new SQLiteCommand(query, con);
        return cmd.ExecuteNonQuery();
    }

    public new SQLiteDataReader Select(string query)
    {
        var cmd = new SQLiteCommand(query, con);
        return cmd.ExecuteReader();
    }

    public new object ScalarSelect(string query)
    {
        var cmd = new SQLiteCommand(query, con);
        return cmd.ExecuteScalar();
    }
    #endregion Query

    #region Parameter Query
    public new SQLiteCommand Prepare(string query)
    {
        return new SQLiteCommand(query, con);
    }

    public SQLiteCommand Parameter(string variableName, object value, SQLiteCommand command)
    {
        command.Parameters.AddWithValue($"@{variableName}", value);
        command.Prepare();
        return command;
    }

    public int ExecuteDML(SQLiteCommand command)
    {
        command.Prepare();
        return command.ExecuteNonQuery();
    }

    public SQLiteDataReader ExecuteDQL(SQLiteCommand command)
    {
        command.Prepare();
        return command.ExecuteReader();
    }

    public object ExecuteScalarDQL(SQLiteCommand command)
    {
        command.Prepare();
        return command.ExecuteScalar();
    }
    #endregion Parameter Query
}