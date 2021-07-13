using System;
using System.Collections.Generic;
using System.Data;
using System.Management.Automation;
using System.Text;

namespace PSCmdlets
{
    [Cmdlet(VerbsCommon.Get, "RandomTable")]
    [OutputType(typeof(string))]
    public class GetRandomTable : PSCmdlet
    {

        [Parameter(
            Position = 0,
            Mandatory = false,
            ValueFromPipeline = true,
            ValueFromPipelineByPropertyName = true)]
        public int RowQuantity { get; set; } = 10;

        DataTable table;
        Random random;

        public GetRandomTable()
        {
            table = new DataTable("Person");
            random = new Random();
        }

        
        protected override void ProcessRecord()
        {
            var result = new StringBuilder();
            for(var i=0; i < RowQuantity; i++)
            {
                result.Append($" ========\n{new Person(random).ToString()}\n\n\n");
            }
            WriteObject(result.ToString());
        }
    }
}
