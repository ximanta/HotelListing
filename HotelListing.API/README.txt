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
		
Issue #6 Implement Repository Pattern
	Create Generic Repository Interface and Implementation to use DbContext for CRUD
	Create entiy specific Repository Interface and implementation specific to entity
	Register as service (Program.cs)
	Update controller to use DI repository

Issue #7 Perform logging in code

Issue #8 Handle Exceptions Globally
			Create custom exceptions classes in exception folder
			Create a Global Try/Catch and use Switch to handle different exceptions 
				in an ExceptionMiddleware class in Middleware package
			Register the Middleware in Application pipeline in Program.cs
 
 Issue #9 Enable Response Caching
			Add AddResponseCaching to builder.Services with options in Program.cs
			Add caching middleware in Program.cs
			Configure caching middleware in Program.cs
			To test:
				Increase caching time from 10 secs to 30 secs or More. 
				Get All Countries from Swagger. Ensure Response header contains " cache-control: public,max-age=30 
				Quickly Post a new country from POSTMAN
				Again Get All Countries from Swagger. The new insert is not reflected as data is getting picked up from cache
				Post 30 sec or More, Again Get All Countries from Swagger. You will see the newly inserted data 
"