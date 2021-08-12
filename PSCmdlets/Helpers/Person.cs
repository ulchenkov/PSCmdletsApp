using System;
using System.Globalization;

namespace ETL
{
    public partial class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Gender { get; set; }

        public Person(Random random)
        {
            Id = random.Next(100000000, 190000000);

            Gender = random.Next(2) == 0 ? "Male" : "Female";

            FirstName = Gender == "Male" ?
                MaleFirstNames[random.Next(MaleFirstNames.Length)] :
                FemailFirstNames[random.Next(FemailFirstNames.Length)];
            MiddleName = ((char)(random.Next(65, 91))).ToString();
            LastName = CultureInfo.GetCultureInfo(CultureInfo.CurrentCulture.Name).TextInfo.ToTitleCase(
                LastNames[random.Next(LastNames.Length)].ToLowerInvariant());

            DateOfBirth = DateTime.Now.AddDays(-random.Next(MINIMUM_AGE * 365, MAXIMUM_AGE * 365));
        }
    }
}
