# Library Management System

## Overview
The **Library Management System** is a simple application built using **ASP.NET Core** and follows the principles of **Clean Architecture**. The system allows library staff to manage books, users, and transactions efficiently, ensuring a seamless user experience and maintainable codebase.

## Features
1. **Book Management**
   - Add, edit, delete books.
   - View the list of books with filtering and search by title, author, or genre.

2. **User Management**
   - Add, edit, delete users.
   - View user details, including their borrowing history.

3. **Transaction Management**
   - Record book borrowing and return transactions.
   - Notify users of overdue books.

4. **Notifications**
   - Email notifications for overdue books.

## Technology Stack
- **Backend:** ASP.NET Core 7.0
- **Database:** SQL Server (or SQLite for development)
- **ORM:** Entity Framework Core
- **Frontend:** Razor Pages (or MVC Views)
- **API Documentation:** Swagger (OpenAPI)
- **Dependency Injection:** Built-in DI in ASP.NET Core
- **Background Jobs:** Hangfire (for scheduling and managing background tasks)

## Architecture
The project adheres to the principles of **Clean Architecture**, ensuring separation of concerns and testability:

1. **Core** (Domain and Application Logic):
   - Entities, Use Cases, and Interfaces.

2. **Infrastructure**:
   - Data access implementations, repositories, and external services (e.g., email notifications).

3. **Presentation**:
   - User interface, controllers, and API endpoints.


## Background Jobs (Hangfire)
The system uses **Hangfire** to run background jobs, such as notifying users of overdue books.

### **Overdue Book Notifications**
A scheduled job runs **daily** to check for overdue books and send email notifications.

#### **Job Registration**
The job is registered in `Program.cs` as follows:
```csharp
RecurringJob.AddOrUpdate<NotifyLateReturns>(
    "notify-late-returns",
    job => job.ExecuteAsync(CancellationToken.None),
    Cron.Daily);
```
This ensures that the `NotifyLateReturns` use case is executed once every 24 hours.

#### **Implementation of NotifyLateReturns**
The `NotifyLateReturns` class retrieves overdue transactions and sends email notifications to users:
```csharp
public class NotifyLateReturns
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly IEmailService _emailService;

    public NotifyLateReturns(ITransactionRepository transactionRepository, IEmailService emailService)
    {
        _transactionRepository = transactionRepository;
        _emailService = emailService;
    }

    public async Task ExecuteAsync(CancellationToken cancellationToken)
    {
        var lateTransactions = await _transactionRepository.GetLateTransactionsAsync();
        foreach (var transaction in lateTransactions)
        {
            await _emailService.SendOverdueNotification(transaction.UserId, transaction.BookId);
        }
    }
}
```

### **Running Hangfire Dashboard**
To monitor and manage jobs, Hangfire provides a dashboard. Start the application and navigate to:
```
http://localhost:5000/hangfire
```


### Directory Structure
```
src/
  BookShelf.Core/          # Core domain and application logic
  BookShelf.Infrastructure/ # Data access and external services
  BookShelf.API/           # Presentation layer (UI and API)
```

## Installation
1. Clone the repository:
   ```bash
   git clone https://github.com/your-username/library-management-system.git
   cd library-management-system
   ```

2. Navigate to the API project:
   ```bash
   cd src/BookShelf.API
   ```

3. Install dependencies:
   ```bash
   dotnet restore
   ```

4. Update the connection string in `appsettings.json`:
   ```json
   "ConnectionStrings": {
       "DefaultConnection": "Server=localhost;Database=BookShelfDb;Trusted_Connection=True;MultipleActiveResultSets=true"
   }
   ```

5. Apply migrations to create the database:
   ```bash
   dotnet ef database update
   ```

6. Run the application:
   ```bash
   dotnet run
   ```

7. Open your browser and navigate to `http://localhost:5000` (or the port specified).

## Usage
- Use the Swagger UI at `http://localhost:5000/swagger` to test API endpoints.
- Access the frontend to manage books, users, and transactions.

## Key Use Cases
### Book Management
- **Add Book:** Enter book details (title, author, genre, publication date) and save.
- **Edit/Delete Book:** Update or remove existing books.
- **Search Books:** Filter by title, author, or genre.

### User Management
- **Add User:** Enter user details (name, email, phone) and save.
- **Edit/Delete User:** Update or remove user information.
- **View User:** Check user details and borrowing history.

### Transaction Management
- **Borrow Book:** Record borrowing with due date.
- **Return Book:** Mark books as returned.
- **Overdue Notifications:** Automatically send email notifications for overdue books.

## Testing
- Unit tests are located in the `BookShelf.Tests` project.
- Run tests with:
  ```bash
  dotnet test
  ```

## Contributing
1. Fork the repository.
2. Create a new branch for your feature or bugfix.
3. Submit a pull request describing your changes.

## License
This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.

---
**Author:** Fatemeh Qanbari 
**Email:** fqanbari919@example.com

