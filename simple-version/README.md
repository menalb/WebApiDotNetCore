# WebApiDotNetCore simplified version

This is a very simple example of ASPNET webapi.

It features:
- GET all products
- GET product by `Id`
- POST a new product

It provide some starting points to:
- create a layer that thanks to ID is independent from external parts like other apis or database
- test the business logic

When it starts the app creates the database if not present and also seeds some fake data.