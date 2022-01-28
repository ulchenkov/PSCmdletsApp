using System;
using System.Data;
using System.Management.Automation;

namespace ETL
{
    [Cmdlet(VerbsCommon.New, "HtmlTable")]
    [Alias("nht")]
    internal class NewHtmlTable : PSCmdlet
    {
        [Parameter(Position = 0, Mandatory = false)]
        public int Limit { get; set; } = 10;
    }
}
