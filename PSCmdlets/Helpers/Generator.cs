using System;
using System.Collections.Generic;

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
        }
    }
}
