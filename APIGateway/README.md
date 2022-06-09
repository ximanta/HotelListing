#14 Create and configure Ocelot API Gateway
	Use NuGET Manager to install dependency of Ocelot
	Create ocelot.JSON to define routes.
		The DownstreamPathTemplate, DownstreamScheme and DownstreamHostAndPorts define the URL that a request will be forwarded to.
		The UpstreamPathTemplate is the URL that Ocelot will use to identify which DownstreamPathTemplate to use for a given request. 
        Load ocelot.json, register ocelot as service, and add ocelot to application pipeline in Program.cs
	To Test:
		Run the HotelListing.API Service
		Run the API Gateway Servicd
		Access with https://localhost:<API Gateway Port>/customers
