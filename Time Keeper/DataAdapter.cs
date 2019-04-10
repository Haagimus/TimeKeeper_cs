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
        public abstract void SetConnection();
        public abstract List<Program> ReadPrograms(string _filter = null);
        public abstract List<Entry> ReadEntries(DateTime? _filter = null);
        public abstract List<Total> ReadTotals(DateTime? _filter = null);
        public abstract List<Date> ReadDates(DateTime? _filter = null);
        public abstract void AddProgram(string _name, int _order, string _code, string _notes);
        public abstract void UpdateProgram(Program _program, string _name, string _code, string _notes, int _order = -1);
        public abstract void DeleteProgram(Program _program);
        public abstract void SwapPrograms(Program _promoteProgram, Program _demoteProgram);
        public abstract void AddEntry(Program _program, DateTime _in, Date _date, DateTime? _out = null, double? _hours = null);
        public abstract void UpdateEntry(int _entryID, Program _program, DateTime _in, DateTime? _out = null, double? _hours = null);
        public abstract void DeleteEntry(Entry _entry);
        public abstract void AddTotal(Program _program, string _comments, Date _date, double? _hours = null);
        public abstract void UpdateTotal(int _totalID, Program _program, string _comments = null, double? _hours = null);
        public abstract void DeleteTotal(Total _total);
        public abstract void AddDate(DateTime _date);
        public abstract void DeleteDate(Date _date);
        public abstract DataTable ListToTable<T>(List<T> _list);
    }
}