using System;
using System.Collections.Generic;
using System.Data;

namespace Time_Keeper
{
    public interface IDataAdapter
    {
        List<Programs> ReadPrograms(string filter = null, bool sorted = false);
        List<Programs> ReadPrograms(Programs filter = null, bool sorted = false);
        List<Entries> ReadEntries(DateTime? filter = null);
        List<Totals> ReadTotals(DateTime? filter = null);
        List<Dates> ReadDates(DateTime? filter = null);
        void AddProgram(string name, int order, string code, string notes);
        void UpdateProgram(Programs program, string name, string code, string notes, int order = -1);
        void DeleteProgram(Programs program);
        void SwapPrograms(Programs promoteProgram, Programs demoteProgram);
        void AddEntry(Programs program, DateTime timeIn, Dates date, DateTime? timeOut = null, decimal? hours = default(decimal?));
        void UpdateEntry(int entryID,  DateTime timeIn, Programs program = null, DateTime? timeOut = null, decimal? hours = default(decimal?));
        void DeleteEntry(Entries entry);
        void AddTotal(Programs program, Dates date, string comments = default(string), decimal? hours = default(decimal?));
        void UpdateTotal(int totalID, string program, string comments = default(string), decimal? hours = default(decimal?));
        void DeleteTotal(Totals total);
        void AddDate(DateTime date);
        void DeleteDate(Dates date);
        DataTable ListToTable<T>(List<T> list);
    }
}