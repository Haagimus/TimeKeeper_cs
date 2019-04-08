using System.Collections.Generic;

namespace Time_Keeper
{
    public interface IDataAdapter
    {
        void SetConnection();
        void CreateFile(string[] tables);
        void CheckColumnExists();
        List<object> SelectQuery(string[] select, string[] from, string[] where = null);
        List<object> ReadDataQuery(string[] queries, string[] tables, string[] filters);
        void WriteSingleDataQuery(string query);
        void WriteMultiDataQuery(string[] queries);
        void ReadData(string[] tables, bool loading = false);
        void ReadFilteredData(string[] tables, System.DateTime date, bool loading = false);
    }
}