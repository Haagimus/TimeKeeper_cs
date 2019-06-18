using System;
using System.Collections.Generic;
using System.Data;

namespace Time_Keeper
{
    public abstract class DataAdapter : IDataAdapter
    {
        public DataSet TKDS = new DataSet();
        public abstract List<Programs> ReadPrograms(string filter = null, bool sorted = false);
        public abstract List<Programs> ReadPrograms(Programs filter = null, bool sorted = false);
        public abstract List<Entries> ReadEntries(DateTime? filter = null);
        public abstract List<Totals> ReadTotals(DateTime? filter = null);
        public abstract List<Dates> ReadDates(DateTime? filter = null);
        public abstract void AddProgram(string name, int order, string code, string notes);
        public abstract void UpdateProgram(Programs program, string name, string code, string notes, int order = -1);
        public abstract void DeleteProgram(Programs program);
        public abstract void SwapPrograms(Programs promoteProgram, Programs demoteProgram);
        public abstract void AddEntry(Programs program, DateTime timeIn, Dates date, DateTime? timeOut = null, decimal? hours = default(decimal?));
        public abstract void UpdateEntry(int entryID, DateTime timeIn, Programs program = null, DateTime? timeOut = null, decimal? hours = default(decimal?));
        public abstract void DeleteEntry(Entries Entry);
        public abstract void AddTotal(Programs program, Dates date, string comments = default(string), decimal? hours = default(decimal?));
        public abstract void UpdateTotal(int totalID, string program, string comments = default(string), decimal? hours = default(decimal?));
        public abstract void DeleteTotal(Totals total);
        public abstract void AddDate(DateTime date);
        public abstract void DeleteDate(Dates date);
        public abstract DataTable ListToTable<T>(List<T> list);
    }
}