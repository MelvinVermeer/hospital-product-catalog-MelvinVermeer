# Hospital Product Catalog

This is product api created for a fictional hospital.  

The API is created for other development teams in the hospital so they can develop client applications and can use hospitals product data however they want.
The original requirements can be found on [this assignment page](/assignment.md).

This api exposes OpenApi 3 data about itself in the form of this [swagger.json](http://localhost:5000/swagger/v1/swagger.json) a more human readable version 
can be found on [/swagger/index.html](http://localhost:5000/swagger/index.html)

To help you with some manual testing/exploration you could use [this postman collection](/tests/postman_collection.json), it contains examples for all of the available endpoints.

## Technology stack

- ASP.NET Core 3.1
- Entity Framework Core
- Swashbuckle - to automatically generate Swagger/OpenApi3 documentation and user Interface.
- [MediatR](https://github.com/jbogard/MediatR) - to support CQRS Pattern (as talked about in our interview)
- [Moq](https://github.com/moq/moq4) - Mocking framework for testing
- [Automapper](https://github.com/AutoMapper/AutoMapper) - Used to convert Domain Objects to Data TransferObjects and Commands to Domain Objects
- [Fluent Validation](https://fluentvalidation.net/) - Used to validate incoming commands, example is shown in [CreateCategory.cs](/src/Hospital.ProductCatalog.BusinessLogic/Categories/Commands/CreateCategory.cs)

## Requirements

To run this API from your local development machine, you need to have at least [.net core 3.1](https://dotnet.microsoft.com/download/dotnet-core/current) installed.
To see if the installation has completed succesfully, run the following command.

```
dotnet --version
```

Any SDK version number higher than `3.1.100` will be able to run this API. 

## Running 

To run the API run the following command

```
dotnet run --project ./src/Hospital.ProductCatalog.API/Hospital.ProductCatalog.API.csproj
```

Or open the solution in Visusal Studio 2019 and press `CTRL + F5` (run without debugging)

## Tests

If you want to run all tests (unit + inegration) you can do so by running the following command. 

```
dotnet test HospitalProductCatalog.sln
```

Or open the solution in Visusal Studio 2019 and press `CTRL + R, A` (run all tests)

To run a test project run one of the following commands

```
dotnet test ./tests/Hospital.ProductCatalog.API.IntegrationTests/Hospital.ProductCatalog.API.IntegrationTests.csproj
dotnet test ./tests/Hospital.ProductCatalog.API.UnitTests/Hospital.ProductCatalog.API.UnitTests.csproj
dotnet test ./tests/Hospital.ProductCatalog.BusinessLogic.UnitTests/Hospital.ProductCatalog.BusinessLogic.UnitTests.csproj
```