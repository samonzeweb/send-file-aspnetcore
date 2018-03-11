# Illustration of the different ways to send file content with ASP.NET Core

## What ?

The SendFile controller show different ways to send a file (or custom content).


## How to run this code

The code was writen using ASP.NET Core 2.1 Preview 1, but should be compatible with ASP.NET Core 2.0. Just clone the repository and execute `dotnet run` from the command line.

You can use `curl` to call the different actions, and see the response details (Windows users could use WSL or Git Bash) :

* curl -k -v https://localhost:5001/api/file/raw
* curl -k -v https://localhost:5001/api/file/withsendfile
* curl -k -v https://localhost:5001/api/file/withphysicalfile
* curl -k -v https://localhost:5001/api/file/withvirtualfile
* curl -k -v https://localhost:5001/api/file/withfilestream
* curl -k -v https://localhost:5001/api/file/withfilecontent


## To go deeper

Not all example behave the same, some response headers could be set or not, some request headers could by used, ... To know all the details, see ASP.NET Core source code.

Exemple with PhysicalFileResult and related parts :

* PhysicalFileResult : https://github.com/aspnet/Mvc/blob/dev/src/Microsoft.AspNetCore.Mvc.Core/PhysicalFileResult.cs
* PhysicalFileResultExecutor : https://github.com/aspnet/Mvc/blob/dev/src/Microsoft.AspNetCore.Mvc.Core/Infrastructure/PhysicalFileResultExecutor.cs
* FileResultExecutorBase : https://github.com/aspnet/Mvc/blob/dev/src/Microsoft.AspNetCore.Mvc.Core/Infrastructure/FileResultExecutorBase.cs
