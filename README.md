# eLib
eLib (ASP.Net Core MVC Application) is a Library System. 
Implemented functionality related to view books availability, borrow and submit books and have unit tested them.
New features/functionalities will be added in incremental manner in near future.
---------------------------------------------------------------------------------------------------------

Application Structure :

> eLib

- Contains UI and View Models of Library.
- Utilised .Net Core Dependency Injection (IoC Container) for loose coupling between UI, BL and DAL.

> eLib.Core

- Contains Respository and Unit of Work interfaces.
- Contains model classes.
- Contains Library interface.

> eLib.BLayer

- Contains the class which has logic to retrieve and insert the books and borrowed books- - .
- Injected the DAL dependencies (repositories with UoW), retrieved the books and impleme- nted UoW pattern to post data to Database.

> eLib.DAL

- Implemented using Enity Framework Core Code First Approach.
- Contains generic Repository and Unit Of Work class implementation.
- Contains the entity configurations for table creation.
- Contains data to be seeded.

> AprQuote.Tests

- Utilised NUnit framework for unit testing.
- Created a in-memory database to perform testing with sample data.
- Contains both positive and negative scenarios.

---------------------------------------------------------------------------------------------------------
Technologies :

- ASP.NET Core Web Application (.Net Core 3.1)
- Database : Sql Server
- Unit Test Framework : NUnit
- Data Access : Entity Framework Core (Code First Approach)
