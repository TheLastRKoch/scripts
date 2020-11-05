Clear-Host

write-host "======================================"
write-host " Copy tool v1.0"
write-host "======================================"

$SharePointPath = "Local Path"

$RunningSystemsPath = "Remote Path"

$Inputs = @("config.xlsx","RPA_Inputs.xlsx")

Get-ChildItem $SharePointPath

$Resp = Read-Host -Prompt "`nDo you want to copy the inputs from Remote? (y/n)"

if ($Resp -eq "y")
{

    write-host "`nStart Copying files ...`n"
    
    for($i=0;$i-le $Inputs.length-1;$i++)
    {
    	$OriginPath = [IO.Path]::Combine($SharePointPath,$Inputs[$i])
    	$DestinationPath = [IO.Path]::Combine($RunningSystemsPath,$Inputs[$i])
    	Copy-Item $OriginPath $DestinationPath
    
    	write-host "[OK] " -ForeGroundColor Green -NoNewLine
    	write-host $Inputs[$i] -ForeGroundColor White 
    
    }
}
else
{
	write-host "`nNo action performed" -ForeGroundColor Green 
}



