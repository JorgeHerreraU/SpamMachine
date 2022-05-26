# Spam Machine

Demo project built in MVC ASP .NET 6 Core.

A very simple app to send predefined messages to your friends or colleagues.

No real email service implementation has been done to avoid missusage.

[SPAM MACHINE WEBSITE](https://spammachine.azurewebsites.net/)

## Installation
Download .NET 6 Runtime & SDK

[https://dotnet.microsoft.com/en-us/download/dotnet/6.0](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)

Once installed, download entity framework
```
dotnet tool install --global dotnet-ef
```

The following commands should be executed inside the project path, make sure to clone the repository first

Clone repository
```
git clone https://github.com/JorgeHerreraU/SpamMachine
```

Build the project
```
dotnet build
```

Create and execute database migrations
```
dotnet ef migrations add InitialCreate
dotnet ef database update
```

Run the project

```bash
dotnet run
```
