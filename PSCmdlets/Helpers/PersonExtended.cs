using System;
using System.Globalization;


namespace ETL
{
    public partial class PersonExtended : Person
    {
        public int Age { get => (DateTime.Now - DateOfBirth).Days / 365; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public int ZipCode { get; set; }
        public string PhoneNumber { get; set; }
        public int Account { get; set; }
        public decimal Rate { get; set; }
        public decimal Amount { get; set; }
        public decimal Balance { get; set; }
        public DateTime LastUpdate { get; set; }
        public int StrNumber { get; set; }

        public PersonExtended(Random random) : base(random)
        {
            Street = Streets[random.Next(Streets.Length)];
            City = Cities[random.Next(Cities.Length)];
            State = States[random.Next(States.Length)];
            ZipCode = random.Next(10000, 90000);
            PhoneNumber = (3470000000L + random.NextLong(5000000000L)).ToString("000-000-0000");

            Account = random.Next(1000000, 2000000);
            Rate = (decimal)random.Next(1000) / 1017;
            Amount = Math.Round(((decimal)200 / (10 + random.Next(17))), 2);
            Balance = Math.Round(((decimal)200 / (10 + random.Next(17))), 4);
            LastUpdate = DateTime.Now.AddSeconds(-random.Next(10000));

            StrNumber = 1 + random.Next(10000);
        }

        public override string ToString() =>
            $"Id={Id}\n" +
            $"Name: {FirstName} {MiddleName}. {LastName}\n" +
            $"Date of Birth: {DateOfBirth:yyyy-MM-dd} ({Age})\tGender: {Gender}\n\n" +
            $"Address: {Street}, {City}, {State} {ZipCode}\n" +
            $"Phone number: {PhoneNumber}\n\n" +
            $"Account: {Account}\tRate: {Rate}\n" +
            $"Amount: {Amount}\tBalance: {Balance}\n" +
            $"LastUpdated: {LastUpdate:f}\n\n" +
            $"StrNumber: {StrNumber}";
    }
}
