# Shopping Website

This is a simple shopping website built using .NET. The application allows users to view, create, update, and delete products. 

## Project Structure

- **Controllers**: Contains the `ProductsController` which manages product-related actions.
- **Models**: Contains the `Product` class that represents a product in the shopping website.
- **Views**: 
  - **Home**: Contains the `Index.cshtml` view for the home page displaying a list of products.
  - **Shared**: Contains the `_Layout.cshtml` file that defines the common structure for the views.
- **wwwroot**: Contains static files such as CSS.
- **appsettings.json**: Configuration settings for the application.
- **Program.cs**: Entry point of the application.
- **Startup.cs**: Configures services and the application's request pipeline.

## Setup Instructions

1. Clone the repository:
   ```
   git clone <repository-url>
   ```

2. Navigate to the project directory:
   ```
   cd shopping-website
   ```

3. Restore the dependencies:
   ```
   dotnet restore
   ```

4. Run the application:
   ```
   dotnet run
   ```

5. Open your web browser and navigate to `http://localhost:5000` to view the application.

## Features

- View a list of products
- Create new products
- Update existing products
- Delete products

## Technologies Used

- .NET 6
- ASP.NET Core MVC
- Entity Framework Core (if applicable)
- Razor Views
- CSS for styling

## Contributing

Feel free to submit issues or pull requests for improvements or bug fixes.