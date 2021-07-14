using System;
using System.Collections.Generic;
using System.Data;
using System.Management.Automation;
using System.Text;

namespace PSCmdlets
{
    [Cmdlet(VerbsCommon.Get, "RandomTable")]
    [OutputType(typeof(DataTable))]
    [Alias("grt")]
    public class GetRandomTable : PSCmdlet
    {

        [Parameter(
            Position = 0,
            Mandatory = false,
            ValueFromPipeline = true,
            ValueFromPipelineByPropertyName = true)]
        public int RowQuantity { get; set; } = 10;

        readonly DataTable table;
        readonly Random random;

        public GetRandomTable()
        {
            table = new DataTable("Person");
            random = new Random();
        }
        
        protected override void ProcessRecord()
        {
            table.AddColumnsFromClass(typeof(Person));

            for(var i=0; i < RowQuantity; i++)
            {
                table.AddRowFromObject(new Person(random));
            }

            WriteObject(table);
        }
    }
}
