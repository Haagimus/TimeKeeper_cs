using log4net;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SQLite;
using System.Linq;

namespace Time_Keeper
{
    public class SQLDataAdapter : DataAdapter
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

            WriteMultiDataQuery(new string[] { _logEntryQuery, _logTotalsQuery, _pgmsQuery, _datesQuery });
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

            WriteMultiDataQuery(queries.ToArray());
            
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

        public override List<object> SelectQuery(string[] select, string[] from, string[] where = null)
        {
            List<object> results = new List<object>();

            if (select.Length != from.Length && from.Length != where.Length)
            {
                results.Add("All arrays must have equal amounts of entries.");
            }
            else
            {
                SetConnection();
                sql_con.Open();
                for (int i = 0; i < select.Length; i++)
                {
                    sql_cmd = sql_con.CreateCommand();
                    if (where != null)
                    {
                        sql_cmd.CommandText = "SELECT " + select[i] + " FROM " + from[i] + " WHERE " + where[i];
                    }
                    else
                    {
                        sql_cmd.CommandText = "SELECT " + select[i] + " FROM " + from[i];
                    }
                    using (SQLiteDataReader reader = sql_cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            results.Add(Convert.ToString(reader[select[i]]));
                        }
                    }
                }
                sql_con.Close();
            }
            return results;
        }

        /// <summary>
        /// Performs a SELECT FROM WHERE operation on the database. All passed in variable arrays must be equal length.
        /// </summary>
        /// <param name="selectNames">An array of strings defining the column names you want to return</param>
        /// <param name="fromTables">An array of strings defining the table names you want to select from</param>
        /// <param name="whereFilters">An array of strings defining the columns and filter parameters</param>
        /// <returns></returns>
        public override List<object> ReadDataQuery(string[] selectNames, string[] fromTables, string[] whereFilters)
        {
            List<object> results = new List<object>();

            if (selectNames.Length != fromTables.Length && fromTables.Length != whereFilters.Length)
            {
                results.Add("All arrays must have equal amounts of entries.");
            }
            else
            {
                SetConnection();
                sql_con.Open();
                for (int i = 0; i < selectNames.Length; i++)
                {
                    sql_cmd = sql_con.CreateCommand();
                    sql_cmd.CommandText = "SELECT " + selectNames[i] + " FROM " + fromTables[i] + " WHERE " + whereFilters[i];

                    using (SQLiteDataReader reader = sql_cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            results.Add(Convert.ToString(reader[selectNames[i]]));
                        }
                    }
                }
                sql_con.Close();
            }
            return results;
        }

        /// <summary>
        /// Executes the passed in query after opening a new connection to the sequel database
        /// </summary>
        /// <param name="query">The string containing the SQL query you want executed</param>
        public override void WriteSingleDataQuery(string query)
        {
            SetConnection();
            sql_con.Open();
            try
            {
                sql_cmd = sql_con.CreateCommand();
                sql_cmd.CommandText = query;
                sql_cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                _logger.Error(string.Format("Error executing SQL Query: {0}", ex.Message));
                throw ex;
            }
            sql_con.Close();
        }

        /// <summary>
        /// Executes multiple passed in queries after opening a new connection to the sequel database
        /// </summary>
        /// <param name="queries">The strings containing the SQL queries you want executed</param>
        public override void WriteMultiDataQuery(string[] queries)
        {
            SetConnection();
            sql_con.Open();
            foreach (string query in queries)
            {
                try
                {
                    sql_cmd = sql_con.CreateCommand();
                    sql_cmd.CommandText = query;
                    sql_cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    _logger.Error(string.Format("Error executing SQL Query: {0}", ex.Message));
                    throw ex;
                }
            }
            sql_con.Close();
        }

        /// <summary>
        /// Returns the requested table datas as an array
        /// </summary>
        /// <param name="table">The table names you want to read the data from</param>
        public override void ReadData(string[] table, bool loading = false)
        {
            SetConnection();
            sql_con.Open();

            // Set the data tables using adapters to the DataTable
            foreach (string _table in table)
            {
                if (!loading) TKDS.Tables[_table].Clear();

                if (_table == "ProgramsTable")
                {
                    sql_cmd = new SQLiteCommand("SELECT * FROM " + _table + " ORDER BY [Order]", sql_con);
                }
                else
                {
                    sql_cmd = new SQLiteCommand("SELECT * FROM " + _table, sql_con);
                }

                adapter = new SQLiteDataAdapter
                {
                    SelectCommand = sql_cmd
                };
                adapter.Fill(TKDS, _table);
            }

            adapter.Dispose();
            sql_cmd.Dispose();
            sql_con.Close();
        }

        /// <summary>
        /// Returns the requested table datas as an array with an applied date filter
        /// </summary>
        /// <param name="table">The table names you want to read the data from</param>
        /// <param name="date">The specified date you want to return data from</param>
        public override void ReadFilteredData(string[] table, DateTime date, bool loading = false)
        {
            SetConnection();
            sql_con.Open();

            // Set the data tables using adapters to the DataTable
            foreach (string _table in table)
            {
                if (!loading) TKDS.Tables[_table].Clear();

                sql_cmd = new SQLiteCommand("SELECT * FROM " + _table + " WHERE Date LIKE '%" + date.ToShortDateString() + "%'", sql_con);
                adapter = new SQLiteDataAdapter
                {
                    SelectCommand = sql_cmd
                };
                adapter.Fill(TKDS, _table);
            }

            adapter.Dispose();
            sql_cmd.Dispose();
            sql_con.Close();
        }

        #region ToList queries
        /// <summary>
        /// Returns a list of program entries
        /// </summary>
        /// <returns></returns>
        public override List<ProgramEntry> ReadPrograms()
        {
            using (var TKDB = new TKDBEntities())
            {
                return TKDB.ProgramEntries.ToList();
            }
        }

        /// <summary>
        /// Returns a list of log entries
        /// </summary>
        /// <param name="filterDate">Used as filter criteria to parse the returned list, format should be short date time</param>
        /// <returns></returns>
        public override List<LogEntry> ReadLogs(DateTime filterDate)
        {
            using (var TKDB = new TKDBEntities())
            {
                return TKDB.LogEntries.ToList();
            }
        }

        /// <summary>
        /// Returns a list of total entries
        /// </summary>
        /// <param name="filterDate">Used as filter criteria to parse the returned list, format should be short date time</param>
        /// <returns></returns>
        public override List<TotalEntry> ReadTotals(DateTime filterDate)
        {
            using (var TKDB = new TKDBEntities())
            {
                return TKDB.TotalEntries.Where(t => t.Date.ToString().Contains(filterDate.ToString())).ToList();
            }
        }
        #endregion

        #region Programs Table control methods
        /// <summary>
        /// Adds a program to the ProgramEntry table
        /// </summary>
        /// <param name="Name">The program name you want to add</param>
        /// <param name="Code">The charge code for the program</param>
        /// <param name="Notes">Any notes for the program</param>
        public override void AddProgram(string Name, int Order, string Code, string Notes)
        {
            using (var TKDB = new TKDBEntities())
            {
                var program = new ProgramEntry()
                {
                    Name = Name,
                    Order = Order,
                    Code = Code,
                    Notes = Notes
                };

                try
                {
                    TKDB.ProgramEntries.Add(program);
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
        /// <param name="ID">The ID of the program to be updated in the database</param>
        /// <param name="Name">The new name of the program being updated</param>
        /// <param name="Code">The new code of the program being updated</param>
        /// <param name="Notes">The new notes of the program being updated</param>
        public override void UpdateProgram(int ID, string Name, string Code, string Notes)
        {
            using (var TKDB = new TKDBEntities())
            {
                var program = (from pgm in TKDB.ProgramEntries where pgm.ID == ID select pgm).FirstOrDefault();
                program.Name = Name;
                program.Code = Code;
                program.Notes = Notes;
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
        /// <param name="ID">The ID of the program being deleted</param>
        public override void DeleteProgram(int ID)
        {
            using (var TKDB = new TKDBEntities())
            {
                var program = (from pgm in TKDB.ProgramEntries where pgm.ID == ID select pgm).FirstOrDefault();
                
                try
                {
                    TKDB.ProgramEntries.Remove(program);
                    TKDB.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }
        #endregion

        #region Entry Log Table control methods
        /// <summary>
        /// Adds an entry log to the LogEntry table.
        /// </summary>
        /// <param name="Name">The program name for the log</param>
        /// <param name="In">The clock in time for the log</param>
        /// <param name="Out">The clock out time for the log</param>
        /// <param name="Hours">The hours for the log</param>
        public override void AddLog(string Name, DateTime In, DateTime? Out, double? Hours, DateTime Date)
        {
            using (var TKDB = new TKDBEntities())
            {
                var log = new LogEntry()
                {
                    ProgramName = Name,
                    In = In,
                    Out = Out,
                    Hours = Hours,
                    Date = Date
                };
                TKDB.LogEntries.Add(log);
                TKDB.SaveChanges();
            }
        }

        /// <summary>
        /// Updates a specified log entry details in the LogEntry table
        /// </summary>
        /// <param name="ID">The ID of the log to be updated in the database</param>
        /// <param name="Name">The new program name of the log being updated</param>
        /// <param name="In">The new clock in time of the log being updated</param>
        /// <param name="Out">The new clock out time of the log being updated</param>
        /// <param name="Hours">The new calculated hours of the log being updated</param>
        public override void UpdateLog(int ID, string Name, DateTime In, DateTime? Out, double? Hours)
        {
            using (var TKDB = new TKDBEntities())
            {
                var log = (from entry in TKDB.LogEntries where entry.ID == ID select entry).FirstOrDefault();
                log.ProgramName = Name;
                log.In = In;
                log.Out = Out;
                log.Hours = Hours;
                TKDB.SaveChanges();
            }
        }
        
        /// <summary>
        /// Deletes a specified log entry from the LogEntry table
        /// </summary>
        /// <param name="ID">The ID of the log being deleted</param>
        public override void DeleteLog(int ID)
        {
            using (var TKDB = new TKDBEntities())
            {
                var log = (from entry in TKDB.LogEntries where entry.ID == ID select entry).FirstOrDefault();

                try
                {
                    TKDB.LogEntries.Remove(log);
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
        /// <param name="Name">The program name you are adding a total for</param>
        /// <param name="Hours">The hours to add to the total entry</param>
        /// <param name="Comments">Comments to add for the total entry</param>
        public override void AddTotal(string Name, double? Hours, string Comments, DateTime Date)
        {
            using (var TKDB = new TKDBEntities())
            {
                var total = new TotalEntry()
                {
                    ProgramName = Name,
                    Hours = Hours,
                    Comments = Comments,
                    Date = Date
                };
                TKDB.TotalEntries.Add(total);
                TKDB.SaveChanges();
            }
        }

        /// <summary>
        /// Updates a specified Total entry details in the TotalEntry table
        /// </summary>
        /// <param name="ID">The ID of the total to be updated in the database</param>
        /// <param name="Name">The new program name of the total being updated</param>
        /// <param name="Hours">The new calculated hours of the total being updated</param>
        /// <param name="Comments">The new comments of the total being updated</param>
        public override void UpdateTotal(int ID, string Name, double? Hours, string Comments)
        {
            using (var TKDB = new TKDBEntities())
            {
                var total = (from entry in TKDB.TotalEntries where entry.ID == ID select entry).FirstOrDefault();
                total.Hours = Hours;
                total.Comments = Comments;
                TKDB.SaveChanges();
            }
        }
        #endregion

        /// <summary>
        /// Adds a date to the DateEntry table.
        /// </summary>
        /// <param name="Date">The date you want to add.</param>
        public override void Add_DateEntry(DateTime Date)
        {
            using (var TKDB = new TKDBEntities())
            {
                var date = new DateEntry()
                {
                    EntryDate = Date
                };
                TKDB.DateEntries.Add(date);
                TKDB.SaveChanges();
            }
        }
    }
}
