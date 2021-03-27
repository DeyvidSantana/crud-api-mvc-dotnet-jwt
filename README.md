# .NET API/MVC Crud with JWT Authentication

This repository contains a solution with API project and MVC project of a crud of users and their courses. JWT authentication has been implemented.

Note: authentication is not working. I can log in to a created user, but I cannot register courses for this user because error 501 (unauthorized) always occurs. The integration tests of the course also don't work due to authentication failure.

SQL Server was used in the project as a database. I used migrations to version the bank that works well.

Packages: 
- API: Microsoft.EntityFrameworkCore (self, SqlServer, Tools, Relational), Microsoft.AspNetCore.Authentication (Abstractions, JwtBearer), Swashbuckle.AspNetCore (self, Annotations).
- MVC: Refit (self, HttpClientFactory)
- XUnit: AutoBogus.
