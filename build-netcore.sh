#!/bin/bash

projectPath="./bBridgeAPISDK-NETCore"
testsPath="./bBridgeAPISDK-NETCore.Test"
buildConfiguration="Release"

dotnet test $testsPath
dotnet build --configuration $buildConfiguration $projectPath