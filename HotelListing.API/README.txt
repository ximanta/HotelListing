CORS Configuration
	Add Cors Policy to builder Services(Program.cs)
	In app UseCors (Program.cs)

Logging with Serilog and SeQ
    Nuget dependencies: Serilog.ASPNetCore, Serilog.Expression, Serilog.SInks.Seq
	Install and run SeQ Log aggregator
	Add Serilog to builder (Program.cs)
	Add logging configuration at (appsettings.json)

Code First Entity Framework
    Nuget dependencies: Microsoft.EntityFrameworkCore.SqlServer
	                    Microsoft.EntityFrameworkCore.Tools
	Set connectionstring (appsettings.json)
	Add DbContext to builder Services (Program.cs)
	Create entities
	Create DbContext
	Perform migration
	Seed Data in DbContext
