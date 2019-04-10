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
        List<Program> ReadPrograms(string _filter = null);
        List<Entry> ReadEntries(DateTime? _filter = null);
        List<Total> ReadTotals(DateTime? _filter = null);
        List<Date> ReadDates(DateTime? _filter = null);
        void AddProgram(string _name, int _order, string _code, string _notes);
        void UpdateProgram(Program _program, string _name, string _code, string _notes, int _order = -1);
        void DeleteProgram(Program _program);
        void SwapPrograms(Program _promoteProgram, Program _demoteProgram);
        void AddEntry(Program _program, DateTime _in, Date _date, DateTime? _out = null, double? _hours = null);
        void UpdateEntry(int _entryID, Program _program, DateTime _in, DateTime? _out = null, double? _hours = null);
        void DeleteEntry(Entry _entry);
        void AddTotal(Program _program, string _comments, Date _date, double? _hours = null);
        void UpdateTotal(int _totalID, Program _program, string _comments = null, double? _hours = null);
        void DeleteTotal(Total _total);
        void AddDate(DateTime _date);
        void DeleteDate(Date _date);
        DataTable ListToTable<T>(List<T> _list);
    }
}