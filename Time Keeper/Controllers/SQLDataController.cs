using log4net;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;

namespace Time_Keeper
{
    public class SQLDataController : DataAdapter
    {
        public static readonly ILog _logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        
        #region DataTable queries
        /// <summary>
        /// Returns a list of program entries
        /// </summary>
        /// <returns>A complete list of all Programs in the database</returns>
        public override List<Programs> ReadPrograms(string _programFilter = null, bool _sorted = false)
        {
            using (var context = new TimeKeeperDBEntities())
            {
                try
                {
                    if (_programFilter != null)
                    {
                        if (_programFilter == null)
                        {
                            return new List<Programs>();
                        }
                        else
                        {
                            if (_sorted)
                            {
                                return context.Programs.Where(p => p.Name.Equals(_programFilter)).OrderBy(p => p.Order).ToList();
                            }
                            else
                            {
                                return context.Programs.Where(p => p.Name.Equals(_programFilter)).ToList();
                            }
                        }
                    }
                    else
                    {
                        if (_sorted)
                        {
                            return context.Programs.OrderBy(p => p.Order).ToList();
                        }
                        else
                        {
                            return context.Programs.ToList();
                        }
                    }
                }
                catch
                {
                    throw;
                }
            }
        }

        public override List<Programs> ReadPrograms(Programs _programFilter = null, bool _sorted = false)
        {

            using (var context = new TimeKeeperDBEntities())
            {
                try
                {
                    if (_programFilter != null)
                    {
                        if (_programFilter.Name == null)
                        {
                            return new List<Programs>();
                        }
                        else
                        {
                            if (_sorted)
                            {
                                return context.Programs.Where(p => p.Name.Equals(_programFilter.Name)).OrderBy(p => p.Order).ToList();
                            }
                            else
                            {
                                return context.Programs.Where(p => p.Name.Equals(_programFilter.Name)).ToList();
                            }
                        }
                    }
                    else
                    {
                        if (_sorted)
                        {
                            return context.Programs.OrderBy(p => p.Order).ToList();
                        }
                        else
                        {
                            return context.Programs.ToList();
                        }
                    }
                }
                catch
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// Returns a list of log entries
        /// </summary>
        /// <param name="_filter">Used as filter criteria to parse the returned list, format should be short date time</param>
        /// <returns>Either a date filtered or complete list of logs in the database</returns>
        public override List<Entries> ReadEntries(DateTime? _filter = null)
        {
            List<Entries> results = new List<Entries>();
            using (var context = new TimeKeeperDBEntities())
            {
                try
                {
                    List<Entries> result = new List<Entries>();
                    if (_filter != null)
                    {
                        var f = Convert.ToDateTime(_filter).Date;

                        result = context.Entries.Where(e => e.DateID.Equals(f)).ToList();

                        return result;
                    }
                    else
                    {
                        return context.Entries.ToList();
                    }
                }
                catch
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// Returns a list of total entries
        /// </summary>
        /// <param name="_filter">Used as filter criteria to parse the returned list, format should be short date time</param>
        /// <returns>Either a date filtered or complete list of Totals in the database</returns>
        public override List<Totals> ReadTotals(DateTime? _filter = null)
        {
            using (var context = new TimeKeeperDBEntities())
            {
                try
                {
                    if (_filter != null)
                    {
                        DateTime f = Convert.ToDateTime(_filter).Date;

                        return context.Totals.Where(t => t.DateID.Equals(f)).ToList();
                    }
                    else
                    {
                        return context.Totals.ToList();
                    }
                }
                catch
                {
                    throw;
                }
            }
        }

        public override List<Dates> ReadDates(DateTime? _filter = null)
        {
            using (var context = new TimeKeeperDBEntities())
            {
                try
                {
                    if (_filter != null)
                    {
                        DateTime f = Convert.ToDateTime(_filter).Date;

                        return context.Dates.Where(d => d.DateID.Equals(f)).ToList();
                    }
                    else
                    {
                        return context.Dates.ToList();
                    }
                }
                catch
                {
                    throw;
                }
            }
        }
        #endregion

        #region Programs Table control methods
        /// <summary>
        /// Adds a program to the ProgramEntry table
        /// </summary>
        /// <param name="_name">The program name you want to add</param>
        /// <param name="_code">The charge code for the program</param>
        /// <param name="_notes">Any notes for the program</param>
        public override void AddProgram(string _name, int _order, string _code, string _notes)
        {
            using (var context = new TimeKeeperDBEntities())
            {
                try
                {
                    Programs program = new Programs();
                    program.Name = _name;
                    program.Order = _order;
                    program.Code = _code;
                    program.Notes = _notes;
                    context.Programs.Add(program);
                    context.SaveChanges();
                }
                catch
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// Updates a specified programs values in the ProgramEntry table
        /// </summary>
        /// <param name="_program">The ID of the program to be updated in the database</param>
        /// <param name="_name">The new name of the program being updated</param>
        /// <param name="_code">The new code of the program being updated</param>
        /// <param name="_notes">The new notes of the program being updated</param>
        public override void UpdateProgram(Programs _program, string _name, string _code, string _notes, int _order = -1)
        {
            using (var context = new TimeKeeperDBEntities())
            {
                var program = context.Programs.Where(p => p.Name.Equals(_program.Name)).FirstOrDefault();
                program.Name = _name;
                program.Code = _code;
                program.Notes = _notes;
                program.Order = _order != -1 ? _order : -1;
                try
                {
                    context.SaveChanges();
                }
                catch
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// Deletes a specified program value from the ProgramEntry table
        /// </summary>
        /// <param name="_program">The requested Program for deletion</param>
        public override void DeleteProgram(Programs _program)
        {
            using (var context = new TimeKeeperDBEntities())
            {
                var program = context.Programs.Where(p => p.Name.Equals(_program.Name)).FirstOrDefault();

                try
                {
                    context.Programs.Remove(program);
                    context.SaveChanges();
                }
                catch
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// Swaps the order number of two programs in the database
        /// </summary>
        /// <param name="_promoteProgram">The Program you want swapped with the second Program</param>
        /// <param name="_demoteProgram">The Program you want swapped with the first Program</param>
        public override void SwapPrograms(Programs _promoteProgram, Programs _demoteProgram)
        {
            using (var context = new TimeKeeperDBEntities())
            {
                int tempPositP = _promoteProgram.Order;
                // First move the promote program to a -1 order for temporary holding
                UpdateProgram(_program: _promoteProgram,
                    _name: _promoteProgram.Name,
                    _code: _promoteProgram.Code,
                    _notes: _promoteProgram.Notes,
                    _order: -1);

                int tempPositD = _demoteProgram.Order;
                // Next move the demote program into the position we just freed up by moving the promote program
                UpdateProgram(_program: _demoteProgram,
                    _name: _demoteProgram.Name,
                    _code: _demoteProgram.Code,
                    _notes: _demoteProgram.Notes,
                    _order: tempPositP);

                // Finally move the promote program into the order just freed up by the demote program
                UpdateProgram(_program: _promoteProgram,
                    _name: _promoteProgram.Name,
                    _code: _promoteProgram.Code,
                    _notes: _promoteProgram.Notes,
                    _order: tempPositD);

                // Now save all changes
                context.SaveChanges();
            }
        }
        #endregion

        #region Entry Log Table control methods
        /// <summary>
        /// Adds an entry log to the LogEntry table.
        /// </summary>
        /// <param name="_program">The program name for the log</param>
        /// <param name="_in">The clock in time for the log</param>
        /// <param name="_out">The clock out time for the log</param>
        /// <param name="_hours">The hours for the log</param>
        public override void AddEntry(Programs _program, DateTime _in, Dates _date, DateTime? _out = default(DateTime?), decimal? _hours = default(decimal?))
        {

            using (var context = new TimeKeeperDBEntities())
            {
                Entries entry = new Entries();
                entry.ProgramName = _program.Name;
                entry.In = _in;
                entry.DateID = _date.DateID;
                if (_out != null) entry.Out = _out;
                if (_hours != null) entry.Hours = _hours;

                context.Entries.Add(entry);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Updates a specified log entry details in the LogEntry table
        /// </summary>
        /// <param name="_entry">The ID of the log to be updated in the database</param>
        /// <param name="_program">The new program name of the log being updated</param>
        /// <param name="_in">The new clock in time of the log being updated</param>
        /// <param name="_out">The new clock out time of the log being updated</param>
        /// <param name="_hours">The new calculated hours of the log being updated</param>
        public override void UpdateEntry(int _entryID, DateTime _in, Programs _program = null, DateTime? _out = default(DateTime?), decimal? _hours = default(decimal?))
        {
            using (var context = new TimeKeeperDBEntities())
            {
                var entry = context.Entries.Where(e => e.EntryID.Equals(_entryID)).FirstOrDefault();
                entry.ProgramName = entry.ProgramName;
                entry.In = _in;
                if (_out != null) entry.Out = (DateTime)_out;
                if (_hours != null) entry.Hours = (decimal)_hours;
                try
                {
                    context.SaveChanges();
                }
                catch
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// Deletes a specified log entry from the LogEntry table
        /// </summary>
        /// <param name="_entry">The requested Entry for deletion</param>
        public override void DeleteEntry(Entries _entry)
        {
            using (var context = new TimeKeeperDBEntities())
            {
                var entry = context.Entries.Where(e => e.EntryID.Equals(_entry.EntryID)).FirstOrDefault();

                try
                {
                    context.Entries.Remove(entry);
                    context.SaveChanges();
                }
                catch
                {
                    throw;
                }
            }
        }
        #endregion

        #region Total Log Table control methods
        /// <summary>
        /// Adds a total entry to the TotalEntry table. 
        /// </summary>
        /// <param name="_program">The program name you are adding a total for</param>
        /// <param name="_hours">The hours to add to the total entry</param>
        /// <param name="_comments">Comments to add for the total entry</param>
        /// <param name="_date">The DateID to add to the total</param>
        public override void AddTotal(Programs _program, Dates _date, string _comments = default(string), decimal? _hours = default(decimal?))
        {
            using (var context = new TimeKeeperDBEntities())
            {
                Totals total = new Totals();
                total.ProgramName = _program.Name;
                total.DateID = _date.DateID;
                total.Comments = _comments;
                if (_hours != null) total.Hours = _hours;

                context.Totals.Add(total);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Updates a specified Total entry details in the TotalEntry table
        /// </summary>
        /// <param name="_totalID">The ID of the total to be updated in the database</param>
        /// <param name="_program">The new program name of the total being updated</param>
        /// <param name="_hours">The new calculated hours of the total being updated</param>
        /// <param name="_comments">The new comments of the total being updated</param>
        public override void UpdateTotal(int _totalID, string _program, string _comments = default(string), decimal? _hours = default(decimal?))
        {
            using (var context = new TimeKeeperDBEntities())
            {
                var total = (from t in context.Totals where t.TotalID == _totalID select t).FirstOrDefault();
                total.ProgramName = _program;
                total.Hours = _hours;
                total.Comments = _comments;
                total.DateID = total.DateID;
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Deletes the specified total entry from the TotalEntry table 
        /// </summary>
        /// <param name="_total">The requested Total for deletion</param>
        public override void DeleteTotal(Totals _total)
        {
            // TODO: Research if it is possible to generalize the delete method using DeleteObject<T>(T _object) instead of having four separate methods for each type
            using (var context = new TimeKeeperDBEntities())
            {
                var total = context.Totals.Where(t => t.TotalID.Equals(_total.TotalID)).FirstOrDefault();

                try
                {
                    context.Totals.Remove(total);
                    context.SaveChanges();
                }
                catch
                {
                    throw;
                }
            }
        }
        #endregion

        #region Dates Table control methods
        /// <summary>
        /// Adds a date to the DateEntry table.
        /// </summary>
        /// <param name="_date">The date you want to add.</param>
        public override void AddDate(DateTime _date)
        {
            bool exists = false;
            DateTime today = new DateTime();

            // If the dates table is empty add the date selected to it, otherwise check if the selected date already exists then add it if it doesn't
            if (ReadDates().Count == 0)
            {
                exists = false;
            }
            else
            {
                foreach (Dates row in ReadDates())
                {
                    if (row.DateID.ToShortDateString() == _date.ToShortDateString())
                    {
                        exists = true;
                        break;
                    }
                }
            }
            if (!exists)
            {
                today = _date;
                using (var context = new TimeKeeperDBEntities())
                {
                    Dates date = new Dates();
                    date.DateID = _date;
                    context.Dates.Add(date);
                    try
                    {
                        context.SaveChanges();
                    }
                    catch
                    {
                        throw;
                    }
                }
            }
        }

        /// <summary>
        /// Deletes the specified date entry from the Dates table 
        /// </summary>
        /// <param name="_date">The requested Date for deletion</param>
        public override void DeleteDate(Dates _date)
        {
            using (var context = new TimeKeeperDBEntities())
            {
                var date = context.Dates.Where(d => d.DateID.Equals(_date.DateID)).FirstOrDefault();

                try
                {
                    context.Dates.Remove(date);
                    context.SaveChanges();
                }
                catch
                {
                    throw;
                }
            }
        }
        #endregion

        #region List to table converters
        public override DataTable ListToTable<T>(List<T> _list)
        {
            DataTable dt = new DataTable(typeof(T).Name);
            // Get all the properties
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                dt.Columns.Add(prop.Name);
            }
            foreach (T item in _list)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    values[i] = Props[i].GetValue(item, null);
                }
                dt.Rows.Add(values);
            }
            return dt;
        }
        #endregion
    }
}
