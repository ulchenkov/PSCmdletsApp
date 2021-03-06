<#
.Synopsis
   converts data table to html table (try Get-Help new-htmltab -examples to see examples)
.DESCRIPTION
   converts data table to html table. You can add conditional formats, row banding and row numbers
   There are several built in styles for conditional formating. use .GetStyles() method to list then
   It's using inline css for proper work in email clients.

.EXAMPLE
    $dt = Get-RandomTable 20 
   ($dt | new-htmltab).body #quick html code
   ($dt | new-htmltab).Print()

   $html = New-HtmlTable $dt -showRowNumber -rowBanding
   $html.View() # Open in browser
   $html.Send() # will send email to test (using env variables for from/to/smtp
   $html.GetStyles() # check styles

.EXAMPLE

# conditional and custom formats

$dt = (Get-RandomTable 40) | select ID, Last_Name, First_Name, dob, account, phone, age, middle, gender | Out-DataTable

$t = new-htmltab $dt -showRowNumber -rowBanding

$t.AddCondition({$col -eq "age" -and $val -le 28}, "td_ok")
$t.AddCondition({$col -eq "age" -and $val -gt 30}, "td_warn")
$t.AddCondition({$col -eq "age" -and $val -gt 50}, "td_dang")

$t.AddCondition({$col -in ("Street", "Last_Name", "First_Name")}, "td_left")  #align columns

  
$t.AddCondition({$row.First_Name -like "Jo*"}, "td_high")     #highlight entire row
$t.AddCondition({$row.First_Name -like "Jo*" -and $col -in ("Street", "Last_Name", "First_Name")}, "td_high_left")     #highlight entire row
$t.AddCondition({$col -in ("middle", "gender")}, "td_high")

#--- change format of value (td)
$t.format = @{
 dob={$val.ToString("yyyy-MM-dd")}
 rate = {[math]::round($val,3)}
}

$t.update()
$t.view()

.INPUTS
   datatable
.OUTPUTS
   psobject
.NOTES
   General notes
.COMPONENT
   The component this cmdlet belongs to
.ROLE
   The role this cmdlet belongs to
.FUNCTIONALITY
   The functionality that best describes this cmdlet
#>


[Reflection.Assembly]::LoadWithPartialName("System.Xml.Linq") | Out-Null

function New-HtmlTable {[CmdletBinding()]param(
  [parameter(ValueFromPipeline=$true)]$dt
, [switch]$showRowNumber
, [switch]$rowBanding
)

if($dt.GetType().Name -eq "DataRow"){$dt = $dt.Table}

$global:val = $global:col = $global:row = $null

#----- Built in Styles. Can add more with AddStyle method  ($table.AddStyle ("name", "style def") )

$center = "text-align:center;vertical-align:center"; $dfsz = "font-size:12px"; $bgc = "background-color"; 
$td_base = "$dfsz;font-weight:normal;color:#222b35;border:1px solid #A9A9A9;padding:2px 4px"

$css = @{
 table = "border-collapse:collapse;border-spacing:0;font-family:sans-serif;" #width:100%
 head = "font-size:14px;font-weight:bold;$bgc`:#C0C0C0;color:#222b35;border:1px solid #A9A9A9;padding:2px 4px;height:0.5in;"

 td = "$td_base;$center;"
 td_bold = "$td_base;font-weight:bold;$center;"
 td_left = "$td_base; text-align:left;text-indent:4px;"
 td_bold_left = "$td_base;font-weight:bold;text-align:left;text-indent:4px;"

 td_dang = "$dfsz;$bgc`:#EE2C2C;color:white;font-weight:bold;border-color:#EE2C2C;$center"
 td_warn = "$dfsz;$bgc`:orange;color:brown;font-weight:bold;border-color:orange;$center"
 td_ok = "$dfsz;$bgc`:lightgreen;color:darkgreen;font-weight:bold;border-color:green;$center"
 td_high = "$td_base;$bgc`:#DCDCDC;font-weight:bold;$center;"
 td_head = "$dfsz;$bgc`:#C0C0C0;color:#222b35;font-weight:bold;border:1px solid #A9A9A9;padding:2px 2px;$center"
 
 td_dang_left = "$dfsz;$bgc`:#EE2C2C;color:white;font-weight:bold;border-color:#EE2C2C;text-align:left;text-indent:4px;"
 td_warn_left = "$dfsz;$bgc`:orange;color:brown;font-weight:bold;border-color:orange;text-align:left;text-indent:4px;"
 td_ok_left = "$dfsz;$bgc`:lightgreen;color:darkgreen;font-weight:bold;border-color:green;text-align:left;text-indent:4px;"
 td_high_left = "$td_base;$bgc`:#DCDCDC;font-weight:bold;text-align:left;text-indent:4px;"
 td_head_left = "$dfsz;$bgc`:#C0C0C0;color:#222b35;border:1px solid #A9A9A9;padding:2px 5px;text-align:left;text-indent:4px;"

 tr_odd = "$bgc`:white;height:0.2in;color:#010066;$center"
 tr_even = "$bgc`:#f2f2f2;height:0.2in;color:#010066;$center"
 tr_high = "$bgc`:#DCDCDC;height:0.2in;color:#010066 ;$center"
}


#--------------------- Load dt ----------------------------------------------------------------------

$Load = {param($dt)
$this.dt = $dt

$tab = [System.Xml.Linq.XElement]"<TABLE style=`"$($this.css.table)`" ></TABLE>"

#------- HEADERS

$head = [System.Xml.Linq.XElement]"<TR></TR>"

# dummy column to fix outlook render errors
if($true){ 
  $head.Add([System.Xml.Linq.XElement]"<TH></TH>")
}

#add row number column if requested
if($this.showRowNumber){ #row num column
$head.Add([System.Xml.Linq.XElement]"<TH style=`"$($this.css.head)`">#</TH>")
}


foreach($c in $dt.Columns.ColumnName) {
  $th = [System.Xml.Linq.XElement]"<TH></TH>"
  $th.SetAttributeValue("style", $this.css.head)
  $th.Value = $c
  $head.Add($th)
}

$tab.Add($head)

#------- ROWS

$row_no = $row_ord = 0

foreach($row in $dt.Rows) {
   $global:row = $row
   $tr = [System.Xml.Linq.XElement]"<TR></TR>"
   $row_no += 1
   $row_ord = ($row_no % 2)
   if($this.rowBanding) {
    if($row_ord -eq 0) { $tr.SetAttributeValue("style", $this.css.tr_even)}
    if($row_ord -eq 1) { $tr.SetAttributeValue("style", $this.css.tr_odd)}
   }


      #dummy column (fix outlook format)
   if($true){ 
     
      $tr.Add([System.Xml.Linq.XElement]"<TD></TD>")
   }


   if($this.showRowNumber){ #row num
     $td = [System.Xml.Linq.XElement]"<TD></TD>"
     $td.Value = $row_no
     $td.SetAttributeValue("style", $this.css.td_head)
     $tr.Add($td) | Out-Null 
   }



   foreach($col in $dt.Columns.ColumnName) {
       
       $val = $row.$col

       $global:val = $val
       $global:col = $col

       try { if($this.format.$col){$val = .$this.format.$col}} catch {$val = $r.$col}

       $td = [System.Xml.Linq.XElement]"<TD></TD>"
       $td.Value = $val

       #default style
       $style = $this.css.td

       #conditional styles
       try {
       foreach($cnd in $this.cond){
       if(.$cnd.cond){$style = if(!$this.css[$cnd.style]){$cnd.style}else{$this.css[$cnd.style]} }
        }
       } catch {$style = $this.css.td}

       #if($c -eq "last_name" -and $val -like "*ar*"){$styl = $css.td_dang}
       $td.SetAttributeValue("style", $style)
       $style = ""
       $tr.Add($td)
   }
   $tab.Add($tr)
 }
 $this.table = $tab
 $this.body = $tab.ToString()
}

$AddCondition = { param($cond, $style)
if($cond.GetType().Name -ne "ScriptBlock") {Write-Host "no valid scriptblock for condition"; return}
$this.cond += @{cond=$cond; style=$style}
}

$View = {
 $this.Load($this.dt) # refresh
 $f = "$home\Documents\htmlTab$((get-date).Ticks).html"
 $this.table.ToString() | Out-File $f
 try{ start chrome $f} catch { start iexplore $f}
 }

 #-----  Send table via email (using default settings or change mail property)

 $Send = {param([String]$subj, [String]$header, [String]$footer)
   $this.Load($this.dt) # refresh
   if(!$subj){$subj = "Test Html Table"}
   $body = $header  + $this.Body + $footer
   Send-MailMessage -to $this.to -from $this.from -Smtp $this.smtp -Subject $subj -Body $body  -BodyAsHtml
 }

 

#  ============================================ =========================================================== #

$obj = New-Object PSObject

$obj | Add-Member -MemberType NoteProperty -Name table -Value $xml
$obj | Add-Member -MemberType NoteProperty -Name dt -Value $dt
$obj | Add-Member -MemberType NoteProperty -Name email -Value $email
$obj | Add-Member -MemberType NoteProperty -Name from -Value $env:ETL_FROM
$obj | Add-Member -MemberType NoteProperty -Name to -Value @($env:ETL_TO)
$obj | Add-Member -MemberType NoteProperty -Name smtp -Value $env:ETL_SMTP
$obj | Add-Member -MemberType NoteProperty -Name body -Value $body

$obj | Add-Member -MemberType NoteProperty -Name css -Value $css
$obj | Add-Member -MemberType NoteProperty -Name cond -Value @()
$obj | Add-Member -MemberType NoteProperty -Name format -Value @{}
$obj | Add-Member -MemberType NoteProperty -Name rowBanding -Value $rowBanding
$obj | Add-Member -MemberType NoteProperty -Name showRowNumber -Value $showRowNumber


$obj | Add-Member -MemberType ScriptMethod -Name AddStyle -Value {param($name, $def); $this.css.add($name, $def)}
$obj | Add-Member -MemberType ScriptMethod -Name AddCondition -Value $AddCondition
$obj | Add-Member -MemberType ScriptMethod -Name ClearCondition -Value {$this.cond = @()}
$obj | Add-Member -MemberType ScriptMethod -Name Load -Value $Load
$obj | Add-Member -MemberType ScriptMethod -Name Update -Value {$this.Load($this.dt)} 
$obj | Add-Member -MemberType ScriptMethod -Name Print -Value { return $this.table.OuterXml}
$obj | Add-Member -MemberType ScriptMethod -Name View -Value $View
$obj | Add-Member -MemberType ScriptMethod -Name Send -Value $Send
$obj | Add-Member -MemberType ScriptMethod -Name GetStyles -Value {return $this.css.Keys}


# init table
if($dt.Rows.Count -gt 0) {$obj.Load($dt)}

return $obj
}