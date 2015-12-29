#
# PostDeploy.ps1
#
$DatabaseServer = $OctopusParameters["DatabaseServer"]
$DatabaseName = $OctopusParameters["DatabaseName"]
& .\scripts\AliaSQL.exe Update $DatabaseServer $DatabaseName .\scripts