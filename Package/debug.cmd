XCOPY "..\Client\bin\Debug\net5.0\Oqtane.Survey.Client.Oqtane.dll" "..\..\oqtane.framework\Oqtane.Server\bin\Debug\net5.0\" /Y
XCOPY "..\Client\bin\Debug\net5.0\Oqtane.Survey.Client.Oqtane.pdb" "..\..\oqtane.framework\Oqtane.Server\bin\Debug\net5.0\" /Y
XCOPY "..\Server\bin\Debug\net5.0\Oqtane.Survey.Server.Oqtane.dll" "..\..\oqtane.framework\Oqtane.Server\bin\Debug\net5.0\" /Y
XCOPY "..\Server\bin\Debug\net5.0\Oqtane.Survey.Server.Oqtane.pdb" "..\..\oqtane.framework\Oqtane.Server\bin\Debug\net5.0\" /Y
XCOPY "..\Shared\bin\Debug\net5.0\Oqtane.Survey.Shared.Oqtane.dll" "..\..\oqtane.framework\Oqtane.Server\bin\Debug\net5.0\" /Y
XCOPY "..\Shared\bin\Debug\net5.0\Oqtane.Survey.Shared.Oqtane.pdb" "..\..\oqtane.framework\Oqtane.Server\bin\Debug\net5.0\" /Y
XCOPY "..\Server\wwwroot\*" "..\..\oqtane.framework\Oqtane.Server\wwwroot\" /Y /S /I
XCOPY "..\Server\bin\Debug\net5.0\Radzen.Blazor.dll" "..\..\oqtane.framework\Oqtane.Server\bin\Debug\net5.0\" /Y