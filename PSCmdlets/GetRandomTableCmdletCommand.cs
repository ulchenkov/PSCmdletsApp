using System;
using System.Data;
using System.Management.Automation;

namespace ETL
{
    [Cmdlet(VerbsCommon.Get, "RandomTable")]
    [OutputType(typeof(DataTable))]
    [Alias("grt")]
    public class GetRandomTable : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = false)]
        public int Limit { get; set; } = 10;

        [Parameter(Mandatory = false)]
        public SwitchParameter Small { get; set; }

        DataTable table;
        Random random;

        protected override void BeginProcessing()
        {
            base.BeginProcessing();
            table = new DataTable("Person");
            random = new Random();
        }

        protected override void ProcessRecord()
        {
            table.AddColumnsFromClass(Small.IsPresent ? typeof(Person) : typeof(PersonExtended));

            for (var i = 0; i < Limit; i++)
            {
                table.AddRowFromObject(Small.IsPresent ? new Person(random) : new PersonExtended(random));
            }

            WriteObject(table);
        }

       
    }
}
