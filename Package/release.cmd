"..\..\oqtane.framework\oqtane.package\nuget.exe" pack Oqtane.Survey.nuspec 
XCOPY "*.nupkg" "..\..\oqtane.framework\Oqtane.Server\wwwroot\Modules\" /Y
