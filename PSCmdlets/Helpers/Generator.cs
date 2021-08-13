using System;
using System.Collections.Generic;
using System.Data;
using FastMember;

namespace ETL
{
    public partial class GetRandomObject
    {
        class Generator
        {
            public static IEnumerable<T> GetPersons<T>(Random random)
            {
                while (true)
                {
                    yield return (T)Activator.CreateInstance(typeof(T), new object[] { random });
                }
            }

            public static IDataReader GetIDataReader(IEnumerable<object> list)
            {
                return ObjectReader.Create(list);
            }
        }
    }
}
