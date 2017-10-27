@echo off

rem Define tools path
set msBuild="%WINDIR%\Microsoft.NET\Framework\v4.0.30319\msbuild.exe"
set nuggetTools="packages\NuGet.CommandLine.4.3.0\tools\NuGet.exe"
set xunitPath="packages\xunit.runner.console.2.3.0\tools\net452\xunit.console"

rem Configure build
set projectName="bBridgeAPISDK"
set testsPath="%projectName%.Test\bin\Release\bBridgeAPISDKNET.Test.dll"
set buildConfiguration="Release"

call %msBuild% %projectName%.sln /p:Configuration=%buildConfiguration% /l:FileLogger,Microsoft.Build.Engine;logfile=Manual_MSBuild_ReleaseVersion_LOG.log
call %xunitPath% %testsPath%
call %nuggetTools% pack Package.nuspec