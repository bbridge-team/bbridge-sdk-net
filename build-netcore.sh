#!/bin/bash

projectPath="./bBridgeAPISDK-NETCore"
testsPath="./bBridgeAPISDK-NETCore.Test"
buildConfiguration="Release"

dotnet restore $testsPath
dotnet test $testsPath
dotnet build --configuration $buildConfiguration $projectPath
dotnet pack --configuration $buildConfiguration $projectPath