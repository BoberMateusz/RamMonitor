# Ram Usage Monitor
## Overview:

Monitors your system's RAM usage and stores the data in a database.
Written in C#
## Requirements:

* .NET Framework 
* Microsoft SQL Server database
## How To Use:

### Database Setup:
Create a database table named *MemoryUsageData* using the provided SQL command:
```
CREATE TABLE MemoryUsageData (
  MemoryUsage BigInt,
  Time DATETIME2
);
```

### Configuration:

With the [App.config](RamMonitor/App.config) file:
1. Set the connection string to desired adress
2. Set monitorDelay and operationLimit variables to your needs
   

