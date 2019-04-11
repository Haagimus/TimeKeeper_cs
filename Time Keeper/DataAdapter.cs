using System;
using System.Collections.Generic;
using System.Data;

namespace Time_Keeper
{
    public abstract class DataAdapter : IDataAdapter
    {
        public DataSet TKDS = new DataSet();

        public abstract void CreateFile(string[] tables);
        public abstract void CheckColumnExists();
        public abstract void SetConnection();
        public abstract List<Programs> ReadPrograms(string _filter = null);
        public abstract List<Entries> ReadEntries(DateTime? _filter = null);
        public abstract List<Totals> ReadTotals(DateTime? _filter = null);
        public abstract List<Dates> ReadDates(DateTime? _filter = null);
        public abstract void AddProgram(string _name, int _order, string _code, string _notes);
        public abstract void UpdateProgram(Programs _program, string _name, string _code, string _notes, int _order = -1);
        public abstract void DeleteProgram(Programs _program);
        public abstract void SwapPrograms(Programs _promoteProgram, Programs _demoteProgram);
        public abstract void AddEntry(Programs _program, DateTime _in, Dates _date, DateTime? _out = null, decimal? _hours = null);
        public abstract void UpdateEntry(int _entryID, Programs _program, DateTime _in, DateTime? _out = null, decimal? _hours = null);
        public abstract void DeleteEntry(Entries _entry);
        public abstract void AddTotal(Programs _program, string _comments, Dates _date, decimal? _hours = null);
        public abstract void UpdateTotal(int _totalID, Programs _program, string _comments = null, decimal? _hours = null);
        public abstract void DeleteTotal(Totals _total);
        public abstract void AddDate(DateTime _date);
        public abstract void DeleteDate(Dates _date);
        public abstract DataTable ListToTable<T>(List<T> _list);
    }
}