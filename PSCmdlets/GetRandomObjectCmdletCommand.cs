using System;
using System.Collections.Generic;
using System.Data;
using System.Management.Automation;
using System.Threading;

namespace ETL
{
    [Cmdlet(VerbsCommon.Get, "RandomObject")]
    [OutputType(typeof(PersonExtended))]
    [OutputType(typeof(Person))]
    [OutputType(typeof(IDataReader))]
    [Alias("gro")]
    public partial class GetRandomObject : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = false)]
        public int Limit { get; set; } = 10;

        [Parameter(Mandatory = false)]
        public int Lag { get; set; } = 0;

        [Parameter(Mandatory = false)]
        public SwitchParameter Slow { get; set; }

        [Parameter(Mandatory = false)]
        public SwitchParameter Small { get; set; }

        [Parameter(Mandatory = false)]
        public SwitchParameter GetIDataReader { get; set; }

        Random random;

        protected override void BeginProcessing()
        {
            base.BeginProcessing();
            random = new Random();
            if (Slow.IsPresent)
            {
                Lag = 1000;
            }
        }

        protected override void ProcessRecord()
        {
            IEnumerable<object> persons = Small.IsPresent ?
                Generator.GetPersons<Person>(random) :
                Generator.GetPersons<PersonExtended>(random);

            if (GetIDataReader.IsPresent)
            {
                WriteObject(Generator.GetIDataReader(persons));
            }
            else
            {
                var personIterator = persons.GetEnumerator();
                var iterationCounter = 0;

                while (iterationCounter < Limit)
                {
                    personIterator.MoveNext();
                    WriteObject(personIterator.Current);
                    iterationCounter++;
                    Thread.Sleep(Lag);
                }
            }
        }
    }
}
