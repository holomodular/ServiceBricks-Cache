![ServiceBricks Logo](https://github.com/holomodular/ServiceBricks/blob/main/Logo.png)  

[![NuGet version](https://badge.fury.io/nu/ServiceBricks.Cache.Microservice.svg)](https://badge.fury.io/nu/ServiceBricks.Cache.Microservice)
![badge](https://img.shields.io/endpoint?url=https://gist.githubusercontent.com/holomodular-support/a4914be5332dc8c9536889edf1f00ace/raw/servicebrickscache-codecoverage.json)
[![License: MIT](https://img.shields.io/badge/License-MIT-389DA0.svg)](https://opensource.org/licenses/MIT)

# ServiceBricks Cache Microservice

## Overview

This repository contains a cache microservice built using the ServiceBricks foundation.
The cache microservice exposes a key/value pair object that can be used for simple data storage.
It also provides a background task for an expiration process to delete cache items once their expiration date occurs.
There are additional classes added for using this microservice as a semaphore, exposing a locking mechanism that can be used by multiple servers when needing to access a shared resource.

## Data Transfer Objects

### CacheDataDto - Admin Policy
Key and Value pair storage object along with an expiration date to denote when it can be deleted.

```csharp

    public partial class CacheDataDto : DataTransferObject
    {
        public DateTimeOffset CreateDate { get; set; }
        public DateTimeOffset UpdateDate { get; set; }
        public string CacheKey { get; set; }
        public string CacheValue { get; set; }
        public DateTimeOffset? ExpirationDate { get; set; }

    }

```


## Background Tasks and Timers

### CacheExpirationTimer class
This background timer runs by default every 2500 milliseconds, with an initial delay of 1 second. It executes the CacheExpirationTask.
The interval can be changed with the SemaphoreOptions, along with other options used for locking shared resources.

[View Source](https://github.com/holomodular/ServiceBricks-Cache/blob/main/src/V1/ServiceBricks.Cache/Background/CacheExpirationTimer.cs)

### CacheExpirationTask class
This background task queries for all CacheData records with an expiration date is less than now, then deletes those records.

[View Source](https://github.com/holomodular/ServiceBricks-Cache/blob/main/src/V1/ServiceBricks.Cache/Background/CacheExpirationTask.cs)

## Events
None

## Processes
None

## Service Bus
None

## Additional Services

### SemaphoreService
This provides a locking mechanism for shared resources in the infrastructure. Using the CacheData object, multiple concurrent services try creating the same key/record in the backing storage, the one that wins pulls records, then releases/deletes the record. Processes will delay and retry creating the lock until it obtains it or times out. See the SemaphoreOptions for the full list of values used.

[View Source](https://github.com/holomodular/ServiceBricks-Cache/blob/main/src/V1/ServiceBricks.Cache/Service/SemaphoreService.cs)

### SingleServerProcessService
This interface stores a key in cache used for syncing across load-balanced applications so that only one server would process records at a time (see SingleWorkService). It provides a heartbeat, so that other instances will pickup and start processing, should the main running server be shut down.

[View Source](https://github.com/holomodular/ServiceBricks-Cache/blob/main/src/V1/ServiceBricks.Cache/Service/SingleServerProcessService.cs)

### SingleWorkService class
This abstract class implements the WorkService and the ISingleServerProcessService to provide a single ordered work queue across multi-application instances.

[View Source](https://github.com/holomodular/ServiceBricks-Cache/blob/main/src/V1/ServiceBricks.Cache/Service/SingleWorkService.cs)

### LockedWorkService class
This abstract class provides a way to lock the underlying data store and use it as a queue, so that multiple, simultaneous running workers do not return the same records. While the APIConcurrrency rule provides some protection, this class will provide the full solution.

[View Source](https://github.com/holomodular/ServiceBricks-Cache/blob/main/src/V1/ServiceBricks.Cache/Service/LockedWorkService.cs)

## Application Settings

```csharp
{
    "ServiceBricks":{
	"Cache":{
	    "Semaphore":{
		"DelayMilliseconds": 300,
		"CancellationMilliseconds": 10000,
		"OrphanExpirationMilliseconds": 5000,
		"ExpirationTimerIntervalMilliseconds": 2500
	    }
	}
    }
}
```

# About ServiceBricks

ServiceBricks is the cornerstone for building a microservices foundation.
Visit http://ServiceBricks.com to learn more.

