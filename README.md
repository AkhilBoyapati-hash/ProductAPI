# ProductAPI
* A .NET Web API for managing products in a store, allowing users to create, read, update, delete, and modify product stock.

# Features
* Get all products
* Get a product by ID
* Create, update, and delete products
* Increment and decrement product stock

# Getting Started

*Prerequisites*
* .NET 5.0+
* Visual Studio (or your preferred C# IDE)

# Setup
1. Clone the repository:
* git clone https://github.com/AkhilBoyapati-hash/ProductAPI.git
* cd product-api
2. Install dependencies:
* dotnet restore
* Run migrations (optional, for database setup):
* dotnet ef migrations add InitialCreate
* dotnet ef database update
3. Run the application:
* dotnet run
* The API will run on http://localhost:5000.

# API Endpoints
* GET /api/products: Get all products
* GET /api/products/{id}: Get a product by ID
* POST /api/products: Create a new product
* PUT /api/products/{id}: Update a product
* DELETE /api/products/{id}: Delete a product
* PATCH /api/products/{id}/decrement: Decrease stock
* PATCH /api/products/{id}/increment: Increase stock

# Exception Handling Middleware
* A custom middleware is used to handle all exceptions globally, ensuring consistent error responses.
Example error response:
{
  "statusCode": 500,
  "message": "An error occurred while processing your request."
}

# Testing
Unit tests are included using xUnit and Moq for mocking dependencies.
To run tests:
* dotnet test

# Technologies
* ASP.NET Core 5.0
* Entity Framework Core
* Moq for unit testing
* xUnit for writing tests


