using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyTools.MySqlDatabaseTools.Tables
{
    public class MySqlTable : IEnumerable<MySqlColumn>, IDataBaseTable, IReadOnlyDatabaseTable
    {
        private List<MySqlColumn> columns = new List<MySqlColumn>();
        private readonly static string[] joinStrings = new string[] { "INNER JOIN", "LEFT JOIN", "RIGHT JOIN", "OUTER JOIN" };
        private string name;
        private readonly bool isJoined;
        public bool IsJoined => isJoined;

        public int Count => columns.Count;
        public IReadOnlyCollection<MySqlColumn> Columns => columns;

        public MySqlColumn this[int index] => throw new NotImplementedException();

        public MySqlTable(string tableName)
        {
            name = tableName;
            isJoined = false;
        }


        public MySqlTable(string firstTableName, Join joinType, string secondTableName, string predicate)
        {
            isJoined = true;
            StringBuilder sb = new StringBuilder(firstTableName);
            sb.Append($" {joinStrings[(int)joinType]}");
            sb.Append($" {secondTableName}");
            sb.Append($" ON {predicate}");
            name = sb.ToString();
        }

        public MySqlTable(MySqlTable preTable, Join join, string otherTableName, string predicate)
        {
            isJoined = true;
            StringBuilder sb = new StringBuilder(preTable.ToString());
            sb.Append($" {joinStrings[(int)join]}");
            sb.Append($" {otherTableName}");
            sb.Append($" ON {predicate}");
            name = sb.ToString();
        }

        #region Columns
        public void AddColumn(string name)
        {
            columns.Add(new MySqlColumn(this, name, columns.Count));
        }
        public void AddColumnRange(ICollection<string> names)
        {
            for(int i = 0 ; i < names.Count; i++)
            {
                AddColumn(names.ElementAt(i));
            }
        }

        public string GetColumn(string name)
        {
            string comparison = $"{ToString()}.{name}";

            foreach(MySqlColumn column in columns)
            {
                if (column.ToString() == comparison)
                    return column.ToString();
            }
            return null;
        }

        public string GetColumn(int index)
        {
            return columns[index].ToString();
        }

        #endregion


        public MySqlTable Join(Join join, string otherTable, string predicate)
        {
            return new MySqlTable(this, join, otherTable, predicate);
        }

        

        public override string ToString()
        {
            return name;
        }

        public IEnumerator<MySqlColumn> GetEnumerator()
        {
            return columns.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
