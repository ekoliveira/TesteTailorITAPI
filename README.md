

# Teste TailorIT

**Componentes utilizados**

Back-End:

 - .NET CORE 2.2
 - Clean Architecture
 - EntityFrameWorkCore 2.2.0
 - SQL Server Local DB
 - Swagger
 - AutoFac
 - AutoMapper
 - Fluent Migrator
 - Fluent Validation

Testes TDD:

 - xUnit
 - EntityFrameworkCore.InMemory 2.2.0

**Passo-a-Passo para execução:**

Execução do back-end:

1º. Abra o projeto (**TesteTailorIT.sln**) no Visual Studio;

2º. Abra o Package Manager Console em **Tools > NuGet Package Manager > Package Manager Console**;

3º. No console selecione "**src\Api\TesteTailorIT.Api**" em Default Project;

4º. No console digite o seguinte comando sem aspas "**update-database**" depois pressione "**ENTER**"  e aguarde a criação do banco de dados;

5º. Feito isso, basta iniciar o projeto "**TesteTailorIT.Api**";
