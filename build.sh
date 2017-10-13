cd ./bBridgeAPISDK.Test
dotnet restore
dotnet test -p:ParallelizeTestCollections=false
