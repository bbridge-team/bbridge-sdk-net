# bBridge .Net SDK for Somin API [![Build Status](https://travis-ci.org/bbridge-team/bbridge-sdk-net.svg?branch=master)](https://travis-ci.org/bbridge-team/bbridge-sdk-net) [![NuGet](https://img.shields.io/nuget/v/NUS.bBridge.bBridgeAPISDK.svg?style=flat)](https://www.nuget.org/packages/bBridgeAPISDK/)

bBridge API SDK for Somin API is a .Net (.Net Core 1.0.1 and .Net Framework 4.5.2) library for making calls to [Somin API](http://dev.somin.ai). The library enables users for making API calls from all .Net-enabled platforms.

## Example
```cs
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using bBridgeAPISDK.Common;
using bBridgeAPISDK.UserProfiling.Individual;
using bBridgeAPISDK.UserProfiling.Individual.Structs;

string authorizationURL = "http://bbridgeapi.cloudapp.net/v1/auth";
string apiBaseURL = "http://bbridgeapi.cloudapp.net/v1/";

IAuthorizer userPasswordAuthorizer = new LazyCredentialsAuthorizer(
    "<MyAPIUserName>",
    "<MyAPIPassword>",
    authorizationURL);

IndividualUserProfiler individualProfiler = new IndividualUserProfiler(
    new AuthorizedHttpRequester(apiBaseURL, userPasswordAuthorizer))
        {
            ResponseWaitNumAttempts = 60,
            ResponseWaitTime = TimeSpan.FromSeconds(1)
        };
        

IndividualUserProfiling userProfilingResponse = await individualProfiler.PredictIndividualUserProfileTask(
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
    $"Gender: {userProfilingResponse.Profile.Gender}," +
    $"Age group: {userProfilingResponse.Profile.AgeGroup}," +
    $"Relationship Status: {userProfilingResponse.Profile.RelationshipStatus}," +
    $"Education: {userProfilingResponse.Profile.EducationLevel}," +
    $"Income: {userProfilingResponse.Profile.IncomeLevel}," +
    $"Occupation: {userProfilingResponse.Profile.OccupationIndustry}");
```

## Testing
- Install [.Net Core](https://www.microsoft.com/net/core#windowsvs2017) for your platform
- Set environment variables **USER_NAME** and **PASSWORD** to SoMin API user name and password, respectively.
- run **dotnet test** in **bBridgeAPISDK.Test** directory

## API Documentation
More information can be found on the [SoMin developer web site](http://dev.somin.ai).
