# Description
bBridge API SDK is a .Net (.Net Core 1.0.1 and .Net Framework 4.5.2) library for making calls to [bBridge (NExT) API](http://bbridge.cloudapp.net/developer). The library enables users for making API calls from all .Net-enabled platforms.

# Example
```cs
string authorizationURL = "http://bbridgeapi.cloudapp.net/v1/auth";

IAuthorizer userPasswordAuthorizer = new LazyCredentialsAuthorizer(
    "<MyAPIUserName>",
    "<MyAPIPassword>",
    authorizationURL);

IndividualUserProfiling userProfile = await individualProfiler.PredictIndividualUserProfileTask(
    new UserGeneratedContent(
        new List<string> { "Hello friend!", "The weather is good :)" },
            new List<string>
            {
                "https://pbs.twimg.com/media/C6ij4CLUwAAxu9r.jpg",
                "https://pbs.twimg.com/media/C6jO3UiVoAQYc_8.jpg"
            }
        ),
        new IndividualUserProfilingSettings
            {
                AgeGroup = true,
                EducationLevel = true,
                Gender = true,
                IncomeLevel = true,
                OccupationIndustry = true,
                RelationshipStatus = true
            });
            
Console.WriteLine(
    $"Gender: {result.Profile.Gender}," +
    $"Age group: {result.Profile.AgeGroup}," +
    $"Relationship Status: {result.Profile.RelationshipStatus}," +
    $"Education: {result.Profile.EducationLevel}," +
    $"Income: {result.Profile.IncomeLevel}," +
    $"Occupation: {result.Profile.OccupationIndustry}");
```

# Testing
- Install [.Net Core](https://www.microsoft.com/net/core#windowsvs2017) for your platform
- Set environment variables **BBRIDGE_API_USER_NAME** and **BBRIDGE_API_PASSWORD** to bBridge API user name and password, respectively.
- run **dotnet test** in **bBridgeAPISDK.Test** directory

# API Documentation
More information can be found on the [bBridge developer site](http://bbridge.cloudapp.net/developer).
