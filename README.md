# Elevator Demo

This repo is a done as part of a coding challenge.

You are a part of a new development team that is supplying an API (over http) that will be used by multiple dependent teams. It is your task to design the API and implement a minimal set of code that the other teams can use to test their integration. Unblocking those teams by creating the interface is more important than building a complete set of business logic.
 
You are tasked with designing an API for an elevator control system. Your API needs to account for the following scenarios:

- A person requests an elevator be sent to their current floor
- A person requests that they be brought to a floor
- An elevator car requests all floors that it�s current passengers are servicing (e.g. to light up the buttons that show which floors the car is going to)
- An elevator car requests the next floor it needs to service
 
Another developer should be able to clone the git repo and invoke a single command that builds and runs the service. The developer should be able to make requests to localhost:8080 to test the services using a tool such as postman or curl. Be sure to include information about how the services should be called.
 
The code should be developed in the same way that you develop in day-to-day professional work and branched, committed and merged as you would a production service.

## Instructions

This is a standard ASP.NET WebAPI. Open the solution in Visual Studio and hit F5, or use dotnet run on a command line to execute it as you would any other ASP.NET Core WebAPI. It uses .NET 5 so your system will need the runtime.
Though the solution calls for port 8080 your port may be different.

NOTE: My system cannot run on 8080 as I have another service there so I have let Visual Studio determine the port.

