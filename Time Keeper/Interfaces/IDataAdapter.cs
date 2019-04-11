using System;
using System.Collections.Generic;
using System.Data;

namespace Time_Keeper
{
    public interface IDataAdapter
    {
        void SetConnection();
        void CreateFile(string[] tables);
        void CheckColumnExists();
        List<Programs> ReadPrograms(string _filter = null);
        List<Entries> ReadEntries(DateTime? _filter = null);
        List<Totals> ReadTotals(DateTime? _filter = null);
        List<Dates> ReadDates(DateTime? _filter = null);
        void AddProgram(string _name, int _order, string _code, string _notes);
        void UpdateProgram(Programs _program, string _name, string _code, string _notes, int _order = -1);
        void DeleteProgram(Programs _program);
        void SwapPrograms(Programs _promoteProgram, Programs _demoteProgram);
        void AddEntry(Programs _program, DateTime _in, Dates _date, DateTime? _out = null, decimal? _hours = null);
        void UpdateEntry(int _entryID, Programs _program, DateTime _in, DateTime? _out = null, decimal? _hours = null);
        void DeleteEntry(Entries _entry);
        void AddTotal(Programs _program, string _comments, Dates _date, decimal? _hours = null);
        void UpdateTotal(int _totalID, Programs _program, string _comments = null, decimal? _hours = null);
        void DeleteTotal(Totals _total);
        void AddDate(DateTime _date);
        void DeleteDate(Dates _date);
        DataTable ListToTable<T>(List<T> _list);
    }
}