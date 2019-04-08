using System;
using System.Data;
using System.Collections.Generic;

namespace Time_Keeper
{
    public abstract class DataAdapter : IDataAdapter
    {
        public DataSet TKDS = new DataSet();

        public abstract void CreateFile(string[] tables);
        public abstract void CheckColumnExists();
        public abstract List<object> SelectQuery(string[] select, string[] from, string[] where = null);
        public abstract List<object> ReadDataQuery(string[] queries, string[] tables, string[] filters);
        public abstract void WriteSingleDataQuery(string query);
        public abstract void WriteMultiDataQuery(string[] queries);
        public abstract void ReadData(string[] tables, bool loading = false);
        public abstract void ReadFilteredData(string[] tables, DateTime date, bool loading = false);
        public abstract void SetConnection();
        public abstract List<ProgramEntry> ReadPrograms();
        public abstract List<LogEntry> ReadLogs(DateTime filterDate);
        public abstract List<TotalEntry> ReadTotals(DateTime filterDate);
        public abstract void AddProgram(string Name, int Order, string Code, string Notes);
        public abstract void UpdateProgram(int ID, string Name, string Code, string Notes);
        public abstract void DeleteProgram(int ID);
        public abstract void AddLog(string Name, DateTime _in, DateTime? Out, double? Hours, DateTime Date);
        public abstract void UpdateLog(int ID, string Name, DateTime In, DateTime? Out, double? Hours);
        public abstract void DeleteLog(int ID);
        public abstract void AddTotal(string Name, double? Hours, string Comments, DateTime Date);
        public abstract void UpdateTotal(int ID, string Name, double? Hours, string Comments);
        public abstract void Add_DateEntry(DateTime Date);
    }
}