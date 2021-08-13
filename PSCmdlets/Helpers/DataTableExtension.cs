using System;
using System.Data;
using FastMember;

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

        public static void AddRowFromObject(this DataTable table, object obj, bool useFastMember)
        {
            if (obj == null)
                throw new ArgumentNullException(nameof(obj), "The object must not be null");

            var row = table.NewRow();
            if (useFastMember)
            {
                FillRowUsingFastMamber(table, obj, row);
            }
            else
            {
                FillRowUsingReflection(table, obj, row);
            }
            table.Rows.Add(row);
        }

        private static void FillRowUsingReflection(DataTable table, object obj, DataRow row)
        {
            foreach (DataColumn column in table.Columns)
            {
                var value = obj.GetType().GetProperty(column.ColumnName).GetValue(obj);
                row[column.ColumnName] = value;
            }
        }

        private static void FillRowUsingFastMamber(DataTable table, object obj, DataRow row)
        {
            var accessor = TypeAccessor.Create(obj.GetType());
            foreach (DataColumn column in table.Columns)
            {
                row[column.ColumnName] = accessor[obj, column.ColumnName];
            }
        }
    }
}
