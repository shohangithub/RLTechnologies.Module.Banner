del "*.nupkg"
"..\..\oqtane.framework\oqtane.package\nuget.exe" pack RLTechnologies.Module.Banner.nuspec 
XCOPY "*.nupkg" "..\..\oqtane.framework\Oqtane.Server\Packages\" /Y

