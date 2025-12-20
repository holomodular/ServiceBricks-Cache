![ServiceBricks Logo](https://raw.githubusercontent.com/holomodular/ServiceBricks/main/Logo.png)   

[![NuGet version](https://badge.fury.io/nu/ServiceBricks.Cache.Microservice.svg)](https://badge.fury.io/nu/ServiceBricks.Cache.Microservice)
![badge](https://img.shields.io/endpoint?url=https://gist.githubusercontent.com/holomodular-support/a4914be5332dc8c9536889edf1f00ace/raw/servicebrickscache-codecoverage.json)
[![License: MIT](https://img.shields.io/badge/License-MIT-389DA0.svg)](https://opensource.org/licenses/MIT)

# ServiceBricks Cache Microservice

## Overview

This repository contains the cache microservice built using the ServiceBricks foundation.
The cache microservice exposes a key/value pair object that can be used for simple data storage. 
It includes a semaphore, exposing a locking mechanism that can be used by multiple servers when needing to access a shared resource.
A background expiration task can be enabled, with an initial delay and interval, to delete expired cache items once their expiration date occurs.
The expiration process duals as an orphaned lock cleanup, should a lock not be released within the timeout period.

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
This background timer can be enabled, with an initial delay and interval, to execute the CacheExpirationTask.

[View Source](https://github.com/holomodular/ServiceBricks-Cache/blob/main/src/V1/ServiceBricks.Cache/Background/CacheExpirationTimer.cs)

### CacheExpirationTask class
This background task queries for all CacheData records with an expiration date is less than the current date/time, then deletes expired records.

[View Source](https://github.com/holomodular/ServiceBricks-Cache/blob/main/src/V1/ServiceBricks.Cache/Background/CacheExpirationTask.cs)

## Events
None

## Processes
None

## Service Bus
None

## Additional Services

### SemaphoreService
This provides a locking mechanism for shared resources in the infrastructure. Using the CacheData object, multiple concurrent services try creating the same key/record in the backing storage, the one that wins pulls records, then releases/deletes the lock. Processes will delay and retry creating the lock until it obtains it or times out. See the SemaphoreOptions for the full list of values used.

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
	   "Expiration": {
		  "TimerEnabled": false,
		  "TimerIntervalMilliseconds": 7000,
		  "TimerDueMilliseconds": 1000
	   },
	   "Semaphore": {
		  "DelayMilliseconds": 3000,
		  "CancellationMilliseconds": 20000,
		  "OrphanTimeoutMilliseconds": 10000
	   }
    }
  }
}
```

# About ServiceBricks

ServiceBricks is the cornerstone for building a microservices foundation.
Visit http://ServiceBricks.com to learn more.

