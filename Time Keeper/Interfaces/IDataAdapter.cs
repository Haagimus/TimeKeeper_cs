using System;
using System.Collections.Generic;
using System.Data;

namespace Time_Keeper
{
    public interface IDataAdapter
    {
        List<Programs> ReadPrograms(string _filter = null, bool _sorted = false);
        List<Programs> ReadPrograms(Programs _filter = null, bool _sorted = false);
        List<Entries> ReadEntries(DateTime? _filter = null);
        List<Totals> ReadTotals(DateTime? _filter = null);
        List<Dates> ReadDates(DateTime? _filter = null);
        void AddProgram(string _name, int _order, string _code, string _notes);
        void UpdateProgram(Programs _program, string _name, string _code, string _notes, int _order = -1);
        void DeleteProgram(Programs _program);
        void SwapPrograms(Programs _promoteProgram, Programs _demoteProgram);
        void AddEntry(Programs _program, DateTime _in, Dates _date, DateTime? _out = null, decimal? _hours = default(decimal?));
        void UpdateEntry(int _entryID,  DateTime _in, Programs _program = null, DateTime? _out = null, decimal? _hours = default(decimal?));
        void DeleteEntry(Entries _entry);
        void AddTotal(Programs _program, Dates _date, string _comments = default(string), decimal? _hours = default(decimal?));
        void UpdateTotal(int _totalID, string _program, string _comments = default(string), decimal? _hours = default(decimal?));
        void DeleteTotal(Totals _total);
        void AddDate(DateTime _date);
        void DeleteDate(Dates _date);
        DataTable ListToTable<T>(List<T> _list);
    }
}