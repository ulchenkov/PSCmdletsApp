using System;
using System.Data;
using System.Management.Automation;
using System.Xml.Linq;

namespace ETL
{
    [Cmdlet(VerbsCommon.Get, "HtmlTable")]
    [Alias("ght")]
    internal class NewHtmlTable : PSCmdlet
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
        }
    }
}
