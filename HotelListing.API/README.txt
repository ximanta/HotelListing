Issue #1 Enable CORS
CORS Configuration
	Add Cors Policy to builder Services(Program.cs)
	In app UseCors (Program.cs)

Issue #2 Configure Logging
Logging with Serilog and SeQ
    Nuget dependencies: Serilog.ASPNetCore, Serilog.Expression, Serilog.SInks.Seq
	Add Serilog to builder (Program.cs)
	Add logging configuration at (appsettings.json)
	Install and run SeQ Log aggregator

Issue #3 Set up Entity Framework
Code First Entity Framework
    Nuget dependencies: Microsoft.EntityFrameworkCore.SqlServer
	                    Microsoft.EntityFrameworkCore.Tools
	Set connectionstring (appsettings.json)
	Add DbContext to builder Services (Program.cs)
	Create entities
	Create DbContext
	Perform migration
		add-migration <InitialMigration>
		update-database
	Seed Data in DbContext
	  update-database
	  Update-Database -TargetMigration:"name_of_migration"
	
Issue #4 Scaffold Controllers and Actions	
	Perform scaffolding of controlle and action
	Test with POSTMAN and Swagger

Issue #5 Automapper and DTO
	Create DTO and use Auto Mapper
		Nuget AutoMapper.Extensions.Microsoft.DependencyInjection
		Create Incomming and Outgoing DTOs
		Create MapperConfig
		Register MapperConfig to builder service (Program.cs)
		Inject Mapper in controller constructor
		Use Mapper for DTO<->Entity conversions
		
	Issue #6