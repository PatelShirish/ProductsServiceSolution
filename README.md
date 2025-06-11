# ProductsService

.NET 8 Web API for managing products

# Nuget Packages required

  - `Microsoft.EntityFrameworkCore.Sqlite`
  - `Microsoft.AspNetCore.Authentication`
  - `Microsoft.EntityFrameworkCore.InMemory`
  - `Microsoft.AspNetCore.Mvc.Testing`
  - `MSTest.TestFramework` & `MSTest.TestAdapter`
  - `Moq`

## Some areas of improvements

- Add model validation attributes & centralized exception middleware.  
- Add Logging & Metrics
- Secret key should be out of source (e.g. environment variables, Azure Key Vault).  
- Per-user/client API keys could be stored (hashed) in a database
