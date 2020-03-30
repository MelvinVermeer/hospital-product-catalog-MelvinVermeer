# Hospital Product Catalog

This is product api created for a fictional hospital. Created for an assignment described [over here](/assignment.md).

## Technology stack

- ASP.NET Core 3.1
- Entity Framework Core

## Requirements

To run this API from your local development machine, you need to have at least [.net core 3.1](https://dotnet.microsoft.com/download/dotnet-core/current) installed.
To see if the installation has completed succesfully, run the following command

```
dotnet --version
```

Any SDK version number higher than `3.1.100` will be able to run this API. 

## Running 

To run the API run the following command

```
dotnet run --project ./src/Hospital.ProductCatalog.API/Hospital.ProductCatalog.API.csproj
```

## Tests

If you want to run all tests (unit + inegration) you can do so by running the following command. 

```
dotnet test HospitalProductCatalog.sln
```

To help you with some manual testing you could use [this postman collection](/tests/postman_collection.json), it contains examples for all of the available endpoints.