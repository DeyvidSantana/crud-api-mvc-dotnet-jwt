# Crud .NET API/MVC com testes e autenticação JWT

Este repositório contém uma solução com projeto API, projeto MVC e suíte de testes de uma crud de usuários e seus cursos. A autenticação JWT foi implementada.

Nota: a autenticação não está funcionando. Consigo fazer login com um usuário criado, mas não consigo registrar cursos para esse usuário porque o erro 501 (não autorizado) sempre ocorre. Os testes de integração do curso também não funcionam devido a falha de autenticação.

O SQL Server foi usado no projeto como um banco de dados. Usei migrações para versionar o banco que funciona bem.

Pacotes: 
- API: Microsoft.EntityFrameworkCore (self, SqlServer, Tools, Relational), Microsoft.AspNetCore.Authentication (Abstractions, JwtBearer), Swashbuckle.AspNetCore (self, Annotations).
- MVC: Refit (self, HttpClientFactory)
- XUnit: AutoBogus.
