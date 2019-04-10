using log4net;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SQLite;
using System.Linq;
using System.Reflection;

namespace Time_Keeper
{
    public class SQLDataController : DataAdapter
    {
        public static readonly ILog _logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        //internal string sql_con = "DataSource=" + Properties.Settings.Default.saveFile + "; Version=3; New=False; Compress=True; datetimeformat=CurrentCulture";
        public SQLiteConnection sql_con;
        public SQLiteCommand sql_cmd;
        public SQLiteDataAdapter adapter;
        private string[] tables = new string[] { "LogEntryTable", "LogTotalsTable", "ProgramsTable", "EntryDatesTable" };
        private string[] logEntryCols = new string[] { "ID", "Program", "In", "Out", "Hours", "Date" };
        private string[] logTotalCols = new string[] { "ID", "Program", "Hours", "Comments", "Date" };
        private string[] programsCols = new string[] { "ID", "Program", "Code STRING", "Notes STRING", "Order" };
        private string[] datesCols = new string[] { "EntryDate" };

        /// <summary>
        /// Create a new SQL database for the TimeKeeper app
        /// </summary>
        public override void CreateFile(string[] tables)
        {
            _logger.Info("SQL Database file not found, creating it now.");
            string _logEntryQuery, _logTotalsQuery, _pgmsQuery, _datesQuery;
            _logEntryQuery = _logTotalsQuery = _pgmsQuery = _datesQuery = string.Empty;
            SQLiteConnection.CreateFile(Properties.Settings.Default.saveFile);

            SetConnection();

            #region SQL Table Creation Query Strings
            foreach (string _table in tables)
            {
                switch (_table)
                {
                    case "LogEntryTable":
                        _logger.Info("Creating the Log Entry database table.");
                        // Create the Log Entries table
                        _logEntryQuery = "CREATE TABLE LogEntryTable (" +
                            "ID INTEGER PRIMARY KEY AUTOINCREMENT," +
                            "Program STRING NOT NULL REFERENCES ProgramsTable (Program)," +
                            "[In] DATETIME NOT NULL," +
                            "Out DATETIME," +
                            "Hours DOUBLE," +
                            "Date DATETIME NOT NULL)";
                        break;
                    case "LogTotalsTable":
                        _logger.Info("Creating the Totals database table.");
                        // Create the Log Totals table
                        _logTotalsQuery = "CREATE TABLE LogTotalsTable (" +
                            "ID INTEGER PRIMARY KEY AUTOINCREMENT," +
                            "Program STRING NOT NULL REFERENCES ProgramsTable (Program)," +
                            "Hours DOUBLE," +
                            "Comments STRING," +
                            "Date DATETIME NOT NULL)";
                        break;
                    case "ProgramsTable":
                        _logger.Info("Creating the Programs database table.");
                        // Create the Programs table
                        _pgmsQuery = "CREATE TABLE ProgramsTable (" +
                            "ID INTEGER PRIMARY KEY AUTOINCREMENT," +
                            "Program STRING NOT NULL UNIQUE COLLATE NOCASE," +
                            "Code STRING UNIQUE," +
                            "Notes STRING," +
                            "[Order] INT NOT NULL)";
                        break;
                    case "EntryDatesTable":
                        _logger.Info("Creating the Entry Dates database table.");
                        // Create the Entry Dates table
                        _datesQuery = "CREATE TABLE EntryDatesTable(" +
                            "EntryDate DATETIME NOT NULL PRIMARY KEY UNIQUE)";
                        break;
                }
            }

            #endregion
        }

        public override void CheckColumnExists()
        {
            List<string> queries = new List<string>();

            SetConnection();
            sql_con.Open();

            foreach (string _table in tables)
            {
                sql_cmd = new SQLiteCommand("SELECT * FROM " + _table + " LIMIT 1", sql_con);

                SQLiteDataReader dr = sql_cmd.ExecuteReader();

                switch (_table)
                {
                    case "LogEntryTable":
                        if (dr.FieldCount != logEntryCols.Length)
                        {
                            _logger.Warn("Entry table size mismatch, adding missing columns to table.");
                        }
                        break;
                    case "LogTotalsTable":
                        if (dr.FieldCount != logTotalCols.Length)
                        {
                            _logger.Warn("Totals table size mismatch, adding missing columns to table.");
                        }
                        break;
                    case "ProgramsTable":
                        if (dr.FieldCount != programsCols.Length)
                        {
                            _logger.Warn("Programs table size mismatch, adding missing columns to table.");
                            foreach (string col in programsCols)
                            {
                                // Figure out which columns are missing then add only those, the exception for matching columns causes the function to exit prematurely
                                queries.Add("ALTER TABLE ProgramsTable ADD COLUMN " + col);
                            }
                        }
                        break;
                    case "EntryDatesTable":
                        if (dr.FieldCount != datesCols.Length)
                        {
                            _logger.Warn("Dates table size mismatch, adding missing columns to table.");
                        }
                        break;
                    default:
                        _logger.Info("All Table sizes match, no database actions being taken.");
                        break;
                }
                dr.Close();
            }
            sql_cmd.Dispose();
            sql_con.Close();

            // WriteMultiDataQuery(queries.ToArray());

        }

        /// <summary>
        /// Set the connection up for the local SQL database
        /// </summary>
        public override void SetConnection()
        {
            sql_con = new SQLiteConnection("DataSource=" + Properties.Settings.Default.saveFile + ";" +
                "Version=3;" +
                "New=False;" +
                "Compress=True;" +
                "datetimeformat=CurrentCulture");
        }

        #region ToList queries
        /// <summary>
        /// Returns a list of program entries
        /// </summary>
        /// <returns>A complete list of all Programs in the database</returns>
        public override List<Program> ReadPrograms(string _programFilter = null)
        {
            using (var TKDB = new TimeKeeperContext())
            {
                if (_programFilter != null)
                {
                    return TKDB.Programs.Where(p => p.Name.Equals(_programFilter)).ToList();
                }
                else
                {
                    return TKDB.Programs.ToList();
                }
            }
        }

        /// <summary>
        /// Returns a list of log entries
        /// </summary>
        /// <param name="_filter">Used as filter criteria to parse the returned list, format should be short date time</param>
        /// <returns>Either a date filtered or complete list of logs in the database</returns>
        public override List<Entry> ReadEntries(DateTime? _filter = null)
        {
            using (var TKDB = new TimeKeeperContext())
            {
                if (_filter != null)
                {
                    return TKDB.Entries.Where(e => e.DateID.ToString().Contains(_filter.ToString())).ToList();
                }
                else
                {
                    return TKDB.Entries.ToList();
                }
            }
        }

        /// <summary>
        /// Returns a list of total entries
        /// </summary>
        /// <param name="_filter">Used as filter criteria to parse the returned list, format should be short date time</param>
        /// <returns>Either a date filtered or complete list of Totals in the database</returns>
        public override List<Total> ReadTotals(DateTime? _filter = null)
        {
            using (var TKDB = new TimeKeeperContext())
            {
                if (_filter != null)
                {
                    return TKDB.Totals.Where(t => t.DateID.ToString().Contains(_filter.ToString())).ToList();
                }
                else
                {
                    return TKDB.Totals.ToList();
                }
            }
        }

        public override List<Date> ReadDates(DateTime? _filter = null)
        {
            using (var TKDB = new TimeKeeperContext())
            {
                if (_filter != null)
                {
                    return TKDB.Dates.Where(t => t.DateID.ToString().Contains(_filter.ToString())).ToList();
                }
                else
                {
                    return TKDB.Dates.ToList();
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
            using (var TKDB = new TimeKeeperContext())
            {
                var program = new Program()
                {
                    Name = _name,
                    Order = _order,
                    Code = _code,
                    Notes = _notes
                };

                try
                {
                    TKDB.Programs.Add(program);
                    TKDB.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw ex;
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
        public override void UpdateProgram(Program _program, string _name, string _code, string _notes, int _order = -1)
        {
            using (var TKDB = new TimeKeeperContext())
            {
                var program = (from pgm in TKDB.Programs where pgm == _program select pgm).FirstOrDefault();
                program.Name = _name;
                program.Code = _code;
                program.Notes = _notes;
                if (_order != -1) program.Order = _order;
                try
                {
                    TKDB.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        /// <summary>
        /// Deletes a specified program value from the ProgramEntry table
        /// </summary>
        /// <param name="_program">The requested Program for deletion</param>
        public override void DeleteProgram(Program _program)
        {
            using (var TKDB = new TimeKeeperContext())
            {
                var program = (from pgm in TKDB.Programs where pgm == _program select pgm).FirstOrDefault();

                try
                {
                    TKDB.Programs.Remove(program);
                    TKDB.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        /// <summary>
        /// Swaps the order number of two programs in the database
        /// </summary>
        /// <param name="_promoteProgram">The Program you want swapped with the second Program</param>
        /// <param name="_demoteProgram">The Program you want swapped with the first Program</param>
        public override void SwapPrograms(Program _promoteProgram, Program _demoteProgram)
        {
            using (var TKDB = new TimeKeeperContext())
            {
                // First move the promote program to a -1 order for temporary holding
                UpdateProgram(_program: _promoteProgram,
                _name: _promoteProgram.Name,
                _code: _promoteProgram.Code,
                _notes: _promoteProgram.Notes,
                _order: -1);

                // Next move the demote program into the position we just freed up by moving the promote program
                UpdateProgram(_program: _demoteProgram,
                    _name: _demoteProgram.Name,
                    _code: _demoteProgram.Code,
                    _notes: _demoteProgram.Notes,
                    _order: _promoteProgram.Order);

                // Finally move the promote program into the order just freed up by the demote program
                UpdateProgram(_program: _promoteProgram,
                    _name: _promoteProgram.Name,
                    _code: _promoteProgram.Code,
                    _notes: _promoteProgram.Notes,
                    _order: _demoteProgram.Order);

                // Now save all changes
                TKDB.SaveChanges();
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
        public override void AddEntry(Program _program, DateTime _in, Date _date, DateTime? _out = null, double? _hours = null)
        {
            using (var TKDB = new TimeKeeperContext())
            {
                var log = new Entry()
                {
                    ProgramID = _program,
                    In = _in,
                    Out = _out,
                    Hours = _hours,
                    DateID = _date
                };
                TKDB.Entries.Add(log);
                TKDB.SaveChanges();
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
        public override void UpdateEntry(int _entryID, Program _program, DateTime _in, DateTime? _out = null, double? _hours = null)
        {
            using (var TKDB = new TimeKeeperContext())
            {
                // TODO: Finish this method
                //var test = new LogEntryTable { Program = "test", Date = DateTime.Now, In = DateTime.Now };
                //TKDB.LogEntryTables.Add(test);
                var log = TKDB.Entries.Find(_entryID);
                //var log = (from entry in TKDB.LogEntries where entry.ID == ID select entry).FirstOrDefault();
                //var log = TKDB.LogEntries.Where(l => l.ID.Equals(ID)).FirstOrDefault();
                //log.Program = Name;
                //log.In = In;
                //log.Out = Out;
                //log.Hours = Hours;
                TKDB.SaveChanges();
            }
        }

        /// <summary>
        /// Deletes a specified log entry from the LogEntry table
        /// </summary>
        /// <param name="_entry">The requested Entry for deletion</param>
        public override void DeleteEntry(Entry _entry)
        {
            using (var TKDB = new TimeKeeperContext())
            {
                var entry = (from e in TKDB.Entries where e == _entry select e).FirstOrDefault();

                try
                {
                    TKDB.Entries.Remove(entry);
                    TKDB.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw ex;
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
        public override void AddTotal(Program _program, string _comments, Date _date, double? _hours = null)
        {
            using (var TKDB = new TimeKeeperContext())
            {
                var total = new Total()
                {
                    ProgramID = _program,
                    Hours = _hours,
                    Comments = _comments,
                    DateID = _date
                };
                TKDB.Totals.Add(total);
                TKDB.SaveChanges();
            }
        }

        /// <summary>
        /// Updates a specified Total entry details in the TotalEntry table
        /// </summary>
        /// <param name="_totalID">The ID of the total to be updated in the database</param>
        /// <param name="_program">The new program name of the total being updated</param>
        /// <param name="Hours">The new calculated hours of the total being updated</param>
        /// <param name="Comments">The new comments of the total being updated</param>
        public override void UpdateTotal(int _totalID, Program _program, string Comments = null, double? Hours = null)
        {
            using (var TKDB = new TimeKeeperContext())
            {
                var total = (from t in TKDB.Totals where t.TotalID == _totalID select t).FirstOrDefault();
                total.ProgramID = _program;
                total.Hours = Hours;
                total.Comments = Comments;
                total.DateID = total.DateID;
                TKDB.SaveChanges();
            }
        }

        /// <summary>
        /// Deletes the specified total entry from the TotalEntry table 
        /// </summary>
        /// <param name="_total">The requested Total for deletion</param>
        public override void DeleteTotal(Total _total)
        {
            // TODO: Research if it is possible to generalize the delete method using DeleteObject<T>(T _object) instead of having four separate methods for each type
            using (var TKDB = new TimeKeeperContext())
            {
                var total = (from t in TKDB.Totals where t == _total select t).FirstOrDefault();

                try
                {
                    TKDB.Totals.Remove(total);
                    TKDB.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        #endregion

        #region Dates Table control methods
        /// <summary>
        /// Adds a date to the DateEntry table.
        /// </summary>
        /// <param name="Date">The date you want to add.</param>
        public override void AddDate(DateTime Date)
        {
            using (var TKDB = new TimeKeeperContext())
            {
                var date = new Date()
                {
                    DateID = Date
                };
                TKDB.Dates.Add(date);
                TKDB.SaveChanges();
            }
        }

        /// <summary>
        /// Deletes the specified date entry from the Dates table 
        /// </summary>
        /// <param name="_date">The requested Date for deletion</param>
        public override void DeleteDate(Date _date)
        {
            using (var TKDB = new TimeKeeperContext())
            {
                var date = (from d in TKDB.Dates where d == _date select d).FirstOrDefault();

                try
                {
                    TKDB.Dates.Remove(date);
                    TKDB.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw ex;
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
            foreach(PropertyInfo prop in Props)
            {
                dt.Columns.Add(prop.Name);
            }
            foreach(T item in _list)
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
