using System;
using System.Data;
using System.Management.Automation;

namespace ETL
{
    [Cmdlet(VerbsCommon.Get, "HtmlTable")]
    [OutputType(typeof(DataTable))]
    [Alias("ght")]
    public class GetHtmlTable : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = true)]
        public DataTable dataTable { get; set; }

        protected override void ProcessRecord()
        {
            foreach (DataRow row in dataTable.Rows)
            {
                string output = "";
                foreach (DataColumn column in dataTable.Columns)
                {
                    output = $"{output} | {row[column]}";
                }
                WriteDebug(output);

            }
            //WriteObject(dataTable);
        }
    }
}
