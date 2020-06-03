# NetPostgreSQL
Sample project to connect EFCore with a PostgreSQL database

## Dependencies
The following dependencies were installed to make this project work:
- Microsoft.AspNetCore.Mvc.NewtonsoftJson (3.1.4)
- Microsoft.EntityFrameworkCore (3.1.4)
- Microsoft.EntityFrameworkCore.Tools (3.1.4)
- **Npgsql.EntityFrameworkCore.PostgreSQL (3.1.4)**

## Configuring EFCore to work with PostgreSQL
EFCore is an Object Relational Mapper (ORM), which will help us to work with code even when we need to access some data from the database. Because of this, we are able to make EFCore work with virtually any SQL engine out there, as long as we have the correct connectors.

This is where Npgsql comes in. It will help us to connect EFCore with a PostgreSQL database.

The `Startup.cs` class is the one that will let you connect to the database. This is how you do so:

```
public void ConfigureServices(IServiceCollection services)
{
  //Startup logic...
  
  services.AddDbContext<BloggingContext>(options =>
  {
    //This line will make the whole magic of making EFCore work with PostgreSQL
    //This is for Demo Purposes, since it's not a good practice to hardcode a connection string
    options.UseNpgsql(
        @"Host=127.0.0.1;Database=blogs;Username=postgres;Password=password");
  });
}
```
## Models
In the `Models` folder we will find some structure classes which will be used as models for our tables in the database.

## DbContext
The `BloggingContext.cs` class will generate an instance of the database to work with in the code. In there, we will configure our models as `DbSet` so we can map them to our tables.

## Migrations
The `Migrations` folder has all the migrations needed to create our database using the Database-First approach. Each time we modify the `BloggingContext.cs` class or we modify a model class, we will need to generate a new migration.

For this, we just run the following command in the Package Manager Console in Visual Studio:
```
add-migration [MigrationName]
```

Or if we are using Visual Studio Code, we can run this in the terminal:
```
dotnet ef migrations add [MigrationName]
```

## Demo Purposes
This project was setup (for Demo Purposes) to delete and migrate the database each time we run it, therefore we won't be able to keep the data once we stop it.

If you want to keep your data, then comment or remove this code block from the `Program.cs` file:
```
// migrate the database.  Best practice = in Main, using service scope
using (var scope = host.Services.CreateScope())
{
  try
  {
    var context = scope.ServiceProvider.GetService<BloggingContext>();
    // for demo purposes, delete the database & migrate on startup so 
    // we can start with a clean slate
    context.Database.EnsureDeleted();
    context.Database.Migrate();
  }
  catch (Exception ex)
  {
    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error occurred while migrating the database.");
  }
}
```

## Endpoints and Payloads
This project only has one endpoint which supports GET, POST, PUT and DELETE verbs.

### GET
- Endpoint: [http://localhost:5000](http://localhost:5000) 
- Payload: N/A
- Description: Will retrieve a list of blogs

### GET
- Endpoint: [http://localhost:5000/{blogId}](http://localhost:5000/{blogId}) 
- Payload: N/A
- Description: Will retrieve a specific blog

### POST
- Endpoint: [http://localhost:5000](http://localhost:5000)
- Payload: 
```
{
  "Url": "YourUrl"
}
```
- Description: Will create a blog

### PUT
- Endpoint: [http://localhost:5000/{blogId}](http://localhost:5000/{blogId}) 
- Payload: 
```
{
  "Url": "YourUrl"
}
```
- Description: Will update a specific blog

### DELETE
- Endpoint: [http://localhost:5000/{blogId}](http://localhost:5000/{blogId}) 
- Payload: N/A
- Description: Will delete a specific blog
