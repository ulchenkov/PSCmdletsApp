using System;
using System.Data;

namespace ETL
{
    public static class DataTableExtension
    {
        public static void AddColumnsFromClass(this DataTable table, Type theClass)
        {
            if (theClass == null)
                throw new ArgumentNullException(nameof(theClass), "The class must not be null");

            var properties = theClass.GetProperties();
            foreach(var property in properties)
            {
                table.Columns.Add(property.Name, property.PropertyType);
            }
        }

        public static void AddRowFromObject(this DataTable table, object obj)
        {
            if (obj == null)
                throw new ArgumentNullException(nameof(obj), "The object must not be null");

            var row = table.NewRow();
            foreach (DataColumn column in table.Columns)
            {
                var value = obj.GetType().GetProperty(column.ColumnName).GetValue(obj);
                row[column.ColumnName] = value;
            }
            table.Rows.Add(row);
        }
    }
}
