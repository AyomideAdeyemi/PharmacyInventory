# Pharmacy Inventory Application

A Complete Pharmacy Inventory Application Using CleanArchitecture

Welcome to the Pharmacy Inventory Application! This application allows you to manage medication inventory and user information for a pharmacy.

## Features

- User Management: Add, update, and delete user profiles.
- Medication Management: Manage information about different medications.
- Inventory Tracking: Keep track of medication quantities held by users.
- Unit Conversion: Define different units for medications (e.g., tablets, bottles, sachets).
- Relationships: Establish relationships between users and medications.

## Technologies Used

- ASP.NET Core
- Entity Framework Core
- SQL Server
- Swagger UI (API documentation)
- AutoMapper (object-object mapping)
- JWT 

## Getting Started

1. Clone the repository.
2. Install the required dependencies using [package manager](https://docs.microsoft.com/en-us/nuget/consume-packages/install-use-packages-powershell) or [dotnet CLI](https://docs.microsoft.com/en-us/dotnet/core/tools/dotnet-restore).
3. Set up the database by running migrations (`dotnet ef database update`).
4. Run the application (`dotnet run`) and access it at `http://localhost:<port>`.

## API Documentation

Explore the API endpoints and interact with them using Swagger UI. After running the application, access the Swagger documentation at `/swagger`.

## Contributing

Contributions are welcome! If you find any bugs or want to enhance the application, feel free to create issues or submit pull requests.

## License

This project is licensed under the [MIT License](LICENSE).
