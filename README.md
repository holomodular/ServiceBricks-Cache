![ServiceBricks Logo](https://github.com/holomodular/ServiceBricks/blob/main/Logo.png)  

[![NuGet version](https://badge.fury.io/nu/ServiceBricks.Cache.svg)](https://badge.fury.io/nu/ServiceBricks.Cache)
![badge](https://img.shields.io/endpoint?url=https://gist.githubusercontent.com/holomodular-support/a4914be5332dc8c9536889edf1f00ace/raw/servicebrickscache-codecoverage.json)
[![License: MIT](https://img.shields.io/badge/License-MIT-389DA0.svg)](https://opensource.org/licenses/MIT)

# ServiceBricks Cache Microservice

## Overview

This repository contains a cache microservice built using the ServiceBricks foundation.
The cache microservice exposes a key/value pair object that can be used for simple data storage.
It also provides a background task for an expiration process to delete cache items once their expiration date occurs.

## Data Transfer Objects

### CacheDataDto - Admin Policy
Key and Value pair storage object along with an expiration date to denote when it can be deleted.

```csharp

    public partial class CacheDataDto : DataTransferObject
    {
        public string Key { get; set; }
        public DateTimeOffset CreateDate { get; set; }
        public DateTimeOffset UpdateDate { get; set; }
        public DateTimeOffset? ExpirationDate { get; set; }
        public string Value { get; set; }
    }

```

#### Business Rules

* DomainCreateUpdateRule - CreateDate and UpdateDate property
* DateTimeOffsetRule - ExpirationDate property
* ApiConcurrencyByUpdateRule - UpdateDate property


## Background Tasks and Timers

### CacheExpirationTimer class
This background timer runs by default every 30 minutes, with an initial delay of 30 minutes. Executes the CacheExpirationTask.

[View Source](https://github.com/holomodular/ServiceBricks-Cache/blob/main/src/V1/ServiceBricks.Cache/BackgroundTask/CacheExpirationTimer.cs)

### CacheExpirationTask class
This background task queries for all CacheData whos expiration date is less than now, then deletes those records.

[View Source](https://github.com/holomodular/ServiceBricks-Cache/blob/main/src/V1/ServiceBricks.Cache/BackgroundTask/CacheExpirationTask.cs)

## Events
None

## Processes
None

## Service Bus
None

## Additional

### SingleServerProcessService class
This class was initially designed to be used as a semaphore when deploying load-balanced applications so that only one server would process a background task at a time.
This is because not all storage providers provide a way to lock the underlying data store and use it as a queue, so that multiple, simultaneous connections do not return the same records.

The implementation currently uses the IApiService which would require the Cache service to be hosted along with the microservice that uses this.
It should be changed to support IApiClient, so that other microservices can make a client call instead of hosting the complete service. **Enhancement**

[View Source](https://github.com/holomodular/ServiceBricks-Cache/blob/main/src/V1/ServiceBricks.Cache/Service/SingleServerProcessService.cs)

## Application Settings
None

# About ServiceBricks

ServiceBricks is the cornerstone for building a microservices foundation.
Visit http://ServiceBricks.com to learn more.

