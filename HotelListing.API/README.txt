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
				Quickly Post a new country from POSTMAN.
				Again Get All Countries from Swagger. The new insert is not reflected as data is getting picked up from cache
				Post 30 sec or More, Again Get All Countries from Swagger. You will see the newly inserted data 

Issue #10 Enable Paging 
			Create PageQueryParameters class to represent request query parameters to be used for paging
			Create PagedResult class to represent what will be returned as response to a paged query
			Update IGenericRepository to add a GetAllAsync() method for paging
			Implement the GetAllAsync() method for paging in GenericRepository class
			Add action method in controller to accept query parameters as specified in 
				PageQueryParameters and to return PagedResult
			Test without and with different paging requirements:
			    https://localhost:7254/api/countries
				https://localhost:7254/api/countries?StartIndex=0&pagesize=2&PageNumber=1
				https://localhost:7254/api/countries?StartIndex=0&pagesize=1&PageNumber=1


Issue #16 Add OData for Custom Querying, Sorting, Ordering

         Add NuGet Dependency of OData - IMP Ensure version is 8.0.8
		 Add OData with options as dependency to controller in Program.cs
         In GetAll method of controller add [EnableQuery]
		 To Test:
		    In Postman access endpoint with query paramater key $select and value ()

Issue #17 Create UserProfileService to perform UserProfile CRUD, authentication and return JWT Token 
          Key Notes:
		  NuGet Dependency Jwt and JwtBearer
		  Add minimum 128 bit secret in appsettings.json
		  In Program.cs add AppSettings class as a service to IoC container
		  Test by making POST request to https://localhost:7030/api/Users/authenticate with request body of seed user.
			{
				"emailId": ""julia@example.com",
				"password": "Julia"
			}

Issue #19 Publish user details to RabbitMQ
	Pull and run RabbitMQ:3-management image
	NuGet Dependency: MassTransit; MassTransit.RabbitMQ
	Register MassTransit and RabbitMQ in Program.cs
	Change post mapping usercontroller to send data on to rabbit MQ.

Issue #24 Connecting Recommandation service to Neo4j
	Pull and run neo4j image from docker hub
	NuGet Dependency: Neo4jClient
	Register Neo4j client in program.cs
	create a model class
	create a controller to communicate
