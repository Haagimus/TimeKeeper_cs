using System;
using System.Collections.Generic;
using System.Data;

namespace Time_Keeper
{
    public abstract class DataAdapter : IDataAdapter
    {
        public DataSet TKDS = new DataSet();
        public abstract List<Programs> ReadPrograms(string _filter = null, bool _sorted = false);
        public abstract List<Programs> ReadPrograms(Programs _filter = null, bool _sorted = false);
        public abstract List<Entries> ReadEntries(DateTime? _filter = null);
        public abstract List<Totals> ReadTotals(DateTime? _filter = null);
        public abstract List<Dates> ReadDates(DateTime? _filter = null);
        public abstract void AddProgram(string _name, int _order, string _code, string _notes);
        public abstract void UpdateProgram(Programs _program, string _name, string _code, string _notes, int _order = -1);
        public abstract void DeleteProgram(Programs _program);
        public abstract void SwapPrograms(Programs _promoteProgram, Programs _demoteProgram);
        public abstract void AddEntry(Programs _program, DateTime _in, Dates _date, DateTime? _out = null, decimal? _hours = default(decimal?));
        public abstract void UpdateEntry(int _entryID, DateTime _in, Programs _program = null, DateTime? _out = null, decimal? _hours = default(decimal?));
        public abstract void DeleteEntry(Entries _Entry);
        public abstract void AddTotal(Programs _program, Dates _date, string _comments = default(string), decimal? _hours = default(decimal?));
        public abstract void UpdateTotal(int _totalID, string _program, string _comments = default(string), decimal? _hours = default(decimal?));
        public abstract void DeleteTotal(Totals _total);
        public abstract void AddDate(DateTime _date);
        public abstract void DeleteDate(Dates _date);
        public abstract DataTable ListToTable<T>(List<T> _list);
    }
}