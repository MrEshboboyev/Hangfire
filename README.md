# Hangfire Email Service

This project demonstrates an efficient email service using Hangfire for background job processing in .NET applications. The service integrates SMTP for sending emails, PostgreSQL for persistent data storage, and the Options pattern for configurable application settings.

## Features

- **Email Sending via Hangfire**: Leverage Hangfire to schedule and execute background email jobs.
- **SMTP Integration**: Easily configurable for any SMTP provider to send emails.
- **PostgreSQL Database**: Reliable data storage for job tracking and application state.
- **Options Pattern**: Simplifies configuration and keeps code maintainable.
- **Scalable Job Processing**: Add and manage background jobs effortlessly.

## Prerequisites

- [.NET 6 or higher](https://dotnet.microsoft.com/download)
- [PostgreSQL](https://www.postgresql.org/) database
- A valid SMTP configuration (e.g., Gmail, SendGrid, etc.)

## Installation

1. Clone the repository:
   ```bash
   git clone https://github.com/MrEshboboyev/Hangfire.git
   ```
2. Navigate to the project directory:
   ```bash
   cd Hangfire
   ```
3. Restore dependencies:
   ```bash
   dotnet restore
   ```
4. Configure the application settings in `appsettings.json`.

## Configuration

Update the `appsettings.json` file with the necessary settings:

- **Database Connection**:
  ```json
  "ConnectionStrings": {
    "PostgresConnectionString": "Host=localhost;Database=YourDatabaseName;Username=YourUsername;Password=YourPassword"
  }
  ```

- **SMTP Configuration**:
  ```json
  "ServerOptions": {
    "Email": "youremail@example.com",
    "Password": "YourEmailPassword"
  }
  ```

- **Hangfire Dashboard** (optional):
  ```json
  "Hangfire": {
    "DashboardPath": "/hangfire",
    "Username": "admin",
    "Password": "adminpassword"
  }
  ```

## Usage

### Running the Application
Run the application in development mode:
```bash
dotnet run --environment Development
```

### Access Hangfire Dashboard
Monitor and manage background jobs through the Hangfire dashboard. By default, the dashboard is available at:
```
http://localhost:5000/hangfire
```
Log in using the configured credentials in `appsettings.json`.

### Adding Background Email Jobs
To send an email through a Hangfire job, use the following code snippet in your application:
```csharp
BackgroundJob.Enqueue(() => emailService.SendEmailAsync("recipient@example.com", "Password"));
```

## Database Migrations

To apply database migrations for PostgreSQL:
```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

## Contributing

Contributions are welcome! To contribute:
1. Fork the repository.
2. Create a feature branch: `git checkout -b feature-name`.
3. Commit your changes: `git commit -m "Add feature"`.
4. Push to the branch: `git push origin feature-name`.
5. Open a pull request.

## License

This project is licensed under the MIT License. See the `LICENSE` file for more details.

## Acknowledgements

- [Hangfire](https://www.hangfire.io/)
- [PostgreSQL](https://www.postgresql.org/)
- [Options Pattern](https://learn.microsoft.com/en-us/dotnet/core/extensions/options)
- [SMTP Email Configuration](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/host/generic-host)

---

Happy coding! 🚀
