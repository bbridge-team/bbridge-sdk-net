#!/bin/bash

projectPath="./bBridgeAPISDKNETCore"
testsPath="./bBridgeAPISDKNETCore.Test"
buildConfiguration="Release"

dotnet restore $testsPath
dotnet test $testsPath
dotnet build --configuration $buildConfiguration $projectPath
dotnet pack --configuration $buildConfiguration $projectPath