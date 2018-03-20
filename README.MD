# VigoBas Identitet Adapter

## Introduction
This project is based on a skeleton for developing FINT .NET SSE adapter. You can find more information about adapter development in
the following links:

* [Overview](https://fintprosjektet.github.io/adapter/overview/)
* [Tutorial](https://fintprosjektet.github.io/adapter/tut-dotnet-sse/)

## Packages and files
The solution Fint.SSE.Adapter is divided into 2 main projects. The `Fint.Sse.Adapter.Console` is a console application for with log information. The `Fint.SSe.Adapter` is the actual adapter.
In this project there is a test implementation of a PwfaService. In order to implement your own service you can take this as a starting point.  

### EventHandlerService.cs
The actions is handled in the `handleEvent()` method. The actions are defined as enums in the models:

### EventStatusService.cs
The status events are handled in the `VerifyEvent` method. 

## Adapter configuration
| Key | Description | Example |
|-----|-------------|---------|
| fint.adapter.organizations | List of orgIds the adapter handles. | rogfk.no, vaf.no, ofk.no |
| fint.adapter.sse-endpoint | Url to the sse endpoint. | https://play-with-fint-adapter.felleskomponent.no/provider/sse/%s |
| fint.adapter.status-endpoint | Url to the status endpoint. | https://play-with-fint-adapter.felleskomponent.no//provider/status |
| fint.adapter.response-endpoint | Url to the response endpoint. | https://play-with-fint-adapter.felleskomponent.no/provider/response |

## Links
[App settings as environment varibles](https://www.scottbrady91.com/Docker/ASPNET-Core-and-Docker-Environment-Variables)
